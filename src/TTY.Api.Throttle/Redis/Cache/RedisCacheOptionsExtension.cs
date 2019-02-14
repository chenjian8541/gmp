using System;
using System.Collections.Generic;
using System.Text;
using TTY.Api.Throttle.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TTY.Api.Throttle.Redis.Cache
{
    public class RedisCacheOptionsExtension : IApiThrottleOptionsExtension
    {
        private readonly Action<RedisCacheOptions> _options;

        public RedisCacheOptionsExtension(Action<RedisCacheOptions> options)
        {
            _options = options;
        }

        public void AddServices(IServiceCollection services)
        {
            services.TryAddSingleton<IRedisCacheDatabaseProvider, RedisCacheDatabaseProvider>();
            services.TryAddSingleton<IThrottleCacheProvider, RedisCacheProvider>();
            services.Configure<RedisCacheOptions>(_options);
        }
    }
}
