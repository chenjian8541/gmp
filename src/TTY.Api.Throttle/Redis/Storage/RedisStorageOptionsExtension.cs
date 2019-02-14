﻿using System;
using System.Collections.Generic;
using System.Text;
using TTY.Api.Throttle.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TTY.Api.Throttle.Redis.Storage
{
    public class RedisStorageOptionsExtension : IApiThrottleOptionsExtension
    {
        private readonly Action<RedisStorageOptions> _options;

        public RedisStorageOptionsExtension(Action<RedisStorageOptions> options)
        {
            _options = options;
        }

        public void AddServices(IServiceCollection services)
        {
            services.TryAddSingleton<IRedisStorageDatabaseProvider, RedisStorageDatabaseProvider>();
            services.TryAddSingleton<IStorageProvider, RedisStorageProvider>();
            services.Configure(_options);
        }
    }
}
