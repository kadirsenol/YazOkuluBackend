
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;
using YazOkulu.Data.Base;
using YazOkulu.Data.Extensions;
using YazOkulu.Data.Interfaces;
using YazOkulu.Data.Models;
using YazOkulu.Extensions;

namespace YazOkulu.Data.Context
{
    public class YazOkuluDbContext : DbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public YazOkuluDbContext() : base() { }
        public YazOkuluDbContext(DbContextOptions<YazOkuluDbContext> options, IHttpContextAccessor _httpContextAccessor = null) : base(options) { if (_httpContextAccessor != null) httpContextAccessor = _httpContextAccessor; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer("connectionstrign"); } // migration sonrası connection string kalkacak
        #region GEN Models
        public DbSet<Course> Courses { get; set; } 
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<GsmOtp> GsmOtps { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Application> Applications { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("YazOkulu.Data")); //for custom entity config
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) { if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType)) entityType.AddSoftDeleteQueryFilter(); }
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?))) { property.SetColumnType("decimal(18,4)"); }
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?))) { property.SetColumnType("datetime2"); }
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().CamelCaseToUnderscore());
                foreach (var property in entity.GetProperties()) { property.SetColumnName(property.Name.CamelCaseToUnderscore()); }
                foreach (var key in entity.GetKeys()) { key.SetName(key.GetName().ToLower(CultureInfo.InvariantCulture)); }
                foreach (var index in entity.GetIndexes()) { index.SetDatabaseName(index.GetDatabaseName().CamelCaseToUnderscore()); }
                foreach (var key in entity.GetForeignKeys())
                {
                    key.PrincipalKey.SetName(key.PrincipalKey.GetName().CamelCaseToUnderscore());
                    key.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ConvertDatetimeToUTC();
            AddAuditInfo();
            return await base.SaveChangesAsync(cancellationToken);
        }
        public virtual void SaveChanges(bool isAudit = false, long? userID = null)
        {
            ConvertDatetimeToUTC();
            if (isAudit) OnBeforeSaveChanges(userID);
            base.SaveChanges();
        }
        private void ConvertDatetimeToUTC()
        {
            var entities = ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            foreach (var entity in entities)
            {
                var properties = entity.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(DateTime) && property.CanWrite)
                    {
                        var dateTimeValue = (DateTime)property.GetValue(entity);
                        if (dateTimeValue.Kind == DateTimeKind.Local || dateTimeValue.Kind == DateTimeKind.Unspecified)
                        {
                            DateTime standardizedDate = new DateTime(dateTimeValue.Ticks - (dateTimeValue.Ticks % TimeSpan.TicksPerSecond), DateTimeKind.Utc).AddMilliseconds(dateTimeValue.Millisecond);
                            property.SetValue(entity, standardizedDate);
                        }
                    }
                    else if (property.PropertyType == typeof(DateTime?) && property.CanWrite)
                    {
                        var dateTimeValue = (DateTime?)property.GetValue(entity);
                        if (dateTimeValue.HasValue && (dateTimeValue.Value.Kind == DateTimeKind.Local || dateTimeValue.Value.Kind == DateTimeKind.Unspecified))
                        {
                            DateTime standardizedDate = new DateTime(dateTimeValue.Value.Ticks - (dateTimeValue.Value.Ticks % TimeSpan.TicksPerSecond), DateTimeKind.Utc).AddMilliseconds(dateTimeValue.Value.Millisecond);
                            property.SetValue(entity, standardizedDate);
                        }
                    }
                }
            }
        }
        private void AddAuditInfo()
        {
            var entities = ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            var utcNow = DateTime.UtcNow;
            var user = httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "0";
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreateDate = utcNow;
                    entity.Entity.CreateUserID = Convert.ToInt32(user);
                }
                if (entity.State == EntityState.Modified)
                {
                    entity.Entity.ModifyDate = utcNow;
                    entity.Entity.ModifyUserID = Convert.ToInt32(user);
                }
            }
        }
        private void OnBeforeSaveChanges(long? userID)
        {
            ChangeTracker.DetectChanges();
            // TODO: audit may save here
            foreach (var entry in ChangeTracker.Entries())
            {
                // TODO: check for project will be use audit table, if then add parameterin the condition as type check
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged) continue;
                // Audt Data create and insert steps will be adding by depends
            }
        }
    }
}