using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TTY.GMP.DataCore;
using System.Linq;
using System.Threading.Tasks;

namespace TTY.GMP.DataAccess.Common
{
    /// <summary>
    /// DbContext扩展
    /// 实现最基本的增、删、查、改
    /// </summary>
    internal static class DbContextExtensions
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="entity"></param>
        /// <param name="finishWork"></param>
        /// <returns></returns>
        internal static async Task<bool> Insert<U, T>(this IDbContext<U> @this, T entity, Action finishWork = null)
            where U : DbContext, new()
            where T : class
        {
            var result = false;
            using (var content = @this.GetDbContext())
            {
                content.Add(entity);
                result = await content.SaveChangesAsync() > 0;
            }
            if (result)
            {
                finishWork?.Invoke();
            }
            return result;
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="entitys"></param>
        /// <param name="finishWork"></param>
        /// <returns></returns>
        internal static async Task<bool> InsertRange<U, T>(this IDbContext<U> @this, IEnumerable<T> entitys, Action finishWork = null)
            where U : DbContext, new()
            where T : class
        {
            var result = false;
            using (var content = @this.GetDbContext())
            {
                await content.AddRangeAsync(entitys);
                result = await content.SaveChangesAsync() > 0;
            }
            if (result)
            {
                finishWork?.Invoke();
            }
            return result;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="entity"></param>
        /// <param name="finishWork"></param>
        /// <returns></returns>
        internal static async Task<bool> Delete<U, T>(this IDbContext<U> @this, T entity, Action finishWork = null)
            where U : DbContext, new()
            where T : class
        {
            var result = false;
            using (var content = @this.GetDbContext())
            {
                content.Remove(entity);
                result = await content.SaveChangesAsync() > 0;
            }
            if (result)
            {
                finishWork?.Invoke();
            }
            return result;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="entity"></param>
        /// <param name="finishWork"></param>
        /// <returns></returns>
        internal static async Task<bool> Update<U, T>(this IDbContext<U> @this, T entity, Action finishWork = null)
            where U : DbContext, new()
            where T : class
        {
            var result = false;
            using (var content = @this.GetDbContext())
            {
                content.Update(entity);
                result = await content.SaveChangesAsync() > 0;
            }
            if (result)
            {
                finishWork?.Invoke();
            }
            return result;
        }

        /// <summary>
        /// 通过主键查询记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        internal static async Task<T> Find<U, T>(this IDbContext<U> @this, long pk)
            where U : DbContext, new()
            where T : class
        {
            using (var content = @this.GetDbContext())
            {
                return await content.Set<T>().FindAsync(pk);
            }
        }

        /// <summary>
        /// 通过查询条件查询记录
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        internal static async Task<T> Find<U, T>(this IDbContext<U> @this, Expression<Func<T, bool>> condition)
            where U : DbContext, new()
            where T : class
        {
            using (var content = @this.GetDbContext())
            {
                return await content.Set<T>().FirstOrDefaultAsync(condition);
            }
        }

        /// <summary>
        /// 获取所有记录集合
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        internal static async Task<List<T>> FindList<U, T>(this IDbContext<U> @this)
            where U : DbContext, new()
            where T : class
        {
            using (var content = @this.GetDbContext())
            {
                return await content.Set<T>().ToListAsync();
            }
        }

        /// <summary>
        /// 通过查询提交获取记录集合
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        internal static async Task<List<T>> FindList<U, T>(this IDbContext<U> @this, Expression<Func<T, bool>> condition)
            where U : DbContext, new()
            where T : class
        {
            using (var content = @this.GetDbContext())
            {
                return await content.Set<T>().Where(condition).ToListAsync();
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOrderBy"></typeparam>
        /// <param name="this"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        internal static async Task<Tuple<List<T>, int>> FindPage<U, T, TOrderBy>(this IDbContext<U> @this, int pageIndex, int pageSize,
            Expression<Func<T, bool>> condition, Func<T, TOrderBy> orderby, bool isDesc = true)
            where U : DbContext, new()
            where T : class
        {
            using (var content = @this.GetDbContext())
            {
                var total = await content.Set<T>().Where(condition).CountAsync();
                List<T> data = null;
                if (isDesc)
                {
                    data = content.Set<T>().Where(condition).OrderByDescending(orderby).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                }
                else
                {
                    data = content.Set<T>().Where(condition).OrderBy(orderby).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                }
                return Tuple.Create(data, total);
            }
        }
    }
}
