using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Text;
using YazOkulu.Core.Enums;
using YazOkulu.Data.Context;
using YazOkulu.Data.Halpers;
using YazOkulu.Data.Interfaces;
using YazOkulu.GENAppService.Interfaces;
using YazOkulu.GENAppService.Mappings;
using YazOkulu.GENAppService.Services;

var builder = WebApplication.CreateBuilder(args);
Env.Load();


#region ILogger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);
#endregion


#region Database
var host = Env.GetString("MSSQL_HOST");
var db = Env.GetString("MSSQL_DATABASE");
var user = Env.GetString("MSSQL_USERNAME");
var pass = Env.GetString("MSSQL_PASSWORD");

var sqlConnectionString =
    $"Server={host};Database={db};User Id={user};Password={pass};TrustServerCertificate=True;";

builder.Services.AddDbContext<YazOkuluDbContext>(options =>
{
    options.UseSqlServer(sqlConnectionString);
});
#endregion
#region cors
builder.Services.AddCors(options => { options.AddPolicy("MyAllowSpecificOrigins", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }); });
#endregion cors
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICourseAppService, CourseAppService>();
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<IApplicationAppService, ApplicationAppService>();
builder.Services.AddAutoMapper(x=>x.AddProfile<MappingProfile>());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "YazOkulu.GEN.API", Version = "v1", }); });


#region JWT Authentication Check
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        ),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole($"{(int)RoleTypeEnum.admin}"));
});
#endregion



var app = builder.Build();
app.UseCors("MyAllowSpecificOrigins");
#region scalar (scalar/v1) (swagger/index.html)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });

    // Scalar UI endpoint
    app.MapScalarApiReference(options =>
    {
        options.Title = "YazOkulu.GEN.API";
        options.Theme = ScalarTheme.Mars;
        options.ShowSidebar = true;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}
#endregion
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();