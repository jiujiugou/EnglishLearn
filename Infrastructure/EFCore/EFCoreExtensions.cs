using DomainCommons.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFCore
{
    public static class EFCoreExtensions
    {
        /// <summary>
        /// set global 'IsDeleted=false' queryfilter for every entity
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void EnableSoftDeletionGlobalFilter(this ModelBuilder modelBuilder)
        {
            var entityTypesHasSoftDeletion = modelBuilder.Model.GetEntityTypes()
                .Where(e => e.ClrType.IsAssignableTo(typeof(ISoftDelete)));
            //这里通过 modelBuilder.Model.GetEntityTypes() 获取当前模型中所有的实体类型。
            foreach (var entityType in entityTypesHasSoftDeletion)
            {
                var isDeletedProperty = entityType.FindProperty(nameof(ISoftDelete.IsDeleted));
                //对每个符合条件的实体类型，查找 IsDeleted 属性，这是 ISoftDelete 接口中的一个布尔值属性，表示实体是否被“软删除”。
                var parameter = Expression.Parameter(entityType.ClrType, "p");
                var filter = Expression.Lambda(Expression.Not(Expression.Property(parameter, isDeletedProperty.PropertyInfo)), parameter);
                //使用表达式树 (Expression Tree) 创建一个过滤器，这个过滤器会在查询数据库时自动应用。
                //Expression.Parameter(entityType.ClrType, "p") 创建一个参数表达式，代表实体类型。
                //Expression.Property(parameter, isDeletedProperty.PropertyInfo) 获取 IsDeleted 属性的值。
                //Expression.Not(...) 对 IsDeleted 属性取反，表示过滤掉所有 IsDeleted == true 的记录。
                entityType.SetQueryFilter(filter);
                //最后，通过 SetQueryFilter(filter) 为每个实体类型设置一个查询过滤器。
                //这意味着在查询这些实体时，
                //EF Core 会自动过滤掉所有 IsDeleted 为 true 的记录      
            }
        }

        public static IQueryable<T> Query<T>(this DbContext ctx) where T : class, IEntity
        {
            return ctx.Set<T>().AsNoTracking();
        }
    }
}
