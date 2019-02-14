using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TTY.GMP.ICache
{
    /// <summary>
    /// 缓存使用约定
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> Set<T>(string key, T t) where T : class;

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        Task<bool> Set<T>(string key, T t, TimeSpan timeSpan) where T : class;

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> Get<T>(string key) where T : class;

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        Task<bool> Remove(string key);
    }
}
