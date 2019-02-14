using System;
using System.Threading.Tasks;
using TTY.GMP.Entity.Config;
using TTY.GMP.ICache;
using TTY.GMP.LOG;

namespace TTY.GMP.DataAccess.Common
{
    /// <summary>
    /// 数据缓存操作基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseCacheDAL<T> where T : class, ICacheDataContract, new()
    {
        /// <summary>
        /// 缓存过期时间(0代表永久不过期)
        /// </summary>
        protected readonly TimeSpan _timeOut;

        /// <summary>
        /// 缓存提供者
        /// </summary>
        private ICacheProvider _cacheProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheProvider">缓存提供者</param>
        public BaseCacheDAL(ICacheProvider cacheProvider) : this(cacheProvider, TimeSpan.Zero)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheProvider"></param>
        /// <param name="timeSpan">缓存过期时间</param>
        public BaseCacheDAL(ICacheProvider cacheProvider, TimeSpan timeSpan)
        {
            this._cacheProvider = cacheProvider;
            this._timeOut = timeSpan;
        }

        /// <summary>
        /// 更新缓存信息
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected virtual async Task<T> UpdateCache(params object[] keys)
        {
            var data = await GetDb(keys);
            if (data == null)
            {
                return null;
            }
            return await UpdateCache(data, keys);
        }

        /// <summary>
        /// 更新缓存信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected virtual async Task<T> UpdateCache(T data, params object[] keys)
        {
            if (_timeOut == TimeSpan.Zero)
            {
                await _cacheProvider.Set(data.GetKeyFormat(keys), data);
            }
            else
            {
                await _cacheProvider.Set(data.GetKeyFormat(keys), data, _timeOut);
            }
            return data;
        }

        /// <summary>
        /// 获取缓存信息
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected virtual async Task<T> GetCache(params object[] keys)
        {
            var key = new T().GetKeyFormat(keys);
            var data = await _cacheProvider.Get<T>(key);
            if (data == null)
            {
                Log.Warn($"未从缓存得到数据，key={key}", this.GetType());
                data = await UpdateCache(keys);
            }
            return data;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="keys"></param>
        protected virtual async Task RemoveCache(params object[] keys)
        {
            await _cacheProvider.Remove(new T().GetKeyFormat(keys));
        }

        /// <summary>
        /// 从数据库中获取需要缓存的信息
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected abstract Task<T> GetDb(params object[] keys);
    }
}
