using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Config;
using TTY.GMP.ICache;
using TTY.GMP.IOC;

namespace TTY.GMP.Cache.Redis
{
    /// <summary>
    /// 使用Redis实现缓存服务
    /// </summary>
    public class RedisProvider : ICacheProvider
    {
        /// <summary>
        /// 连接实例
        /// </summary>
        private readonly ConnectionMultiplexer _redis;

        /// <summary>
        /// 访问数据库
        /// </summary>
        private readonly IDatabase _database;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="redis"></param>
        public RedisProvider(ConnectionMultiplexer redis)
        {
            _redis = redis;
            var redisConfig = CustomServiceLocator.GetInstance<IAppConfigurtaionServices>().AppSettings.RedisConfig;
            _database = redis.GetDatabase(redisConfig.DefaultDb);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> Set<T>(string key, T t) where T : class
        {
            return await _database.StringSetAsync(key, JsonConvert.SerializeObject(t));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public async Task<bool> Set<T>(string key, T t, TimeSpan timeSpan) where T : class
        {
            return await _database.StringSetAsync(key, JsonConvert.SerializeObject(t), timeSpan);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> Get<T>(string key) where T : class
        {
            var s = await _database.StringGetAsync(key);
            if (string.IsNullOrEmpty(s))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(s);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public async Task<bool> Remove(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }
    }
}
