using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YazOkulu.Data.Interfaces;

namespace YazOkulu.Data.Extensions
{
    public static class SoftDeleteQueryExtension
    {
        public static void AddSoftDeleteQueryFilter(this IMutableEntityType entityData)
        {
            var methodToCall = typeof(SoftDeleteQueryExtension).GetMethod(nameof(GetSoftDeleteFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).MakeGenericMethod(entityData.ClrType);
            var filter = methodToCall.Invoke(null, []);
            entityData.SetQueryFilter((LambdaExpression)filter);
            entityData.AddIndex(entityData.FindProperty(nameof(ISoftDelete.IsDeleted)));
        }
        private static Expression<Func<TEntity, bool>> GetSoftDeleteFilter<TEntity>() where TEntity : class, ISoftDelete
        {
            Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
            return filter;
        }
    }
}
