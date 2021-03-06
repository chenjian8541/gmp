﻿using TTY.Api.Throttle;
using TTY.Api.Throttle.Redis;
using TTY.Api.Throttle.Redis.Cache;
using TTY.Api.Throttle.Redis.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OptionsExtensionsForRedisCacheAndStorage
    {
        public static ApiThrottleOptions UseRedisCacheAndStorage(this ApiThrottleOptions options, Action<RedisOptions> configure)
        {
            Action<RedisCacheOptions> opt1 = (opt) => { configure(opt); opt.SameWithStorage = true; };
            options.AddExtension(new RedisCacheOptionsExtension(opt1));

            Action<RedisStorageOptions> opt2 = (opt) => { configure(opt); opt.SameWithCache = true; };
            options.AddExtension(new RedisStorageOptionsExtension(opt2));

            return options;
        }
    }
}
