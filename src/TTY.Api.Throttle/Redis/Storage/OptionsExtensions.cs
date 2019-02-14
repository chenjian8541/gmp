using TTY.Api.Throttle;
using TTY.Api.Throttle.Redis.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OptionsExtensionsForRedisStorage
    {
        public static ApiThrottleOptions UseRedisStorage(this ApiThrottleOptions options, Action<RedisStorageOptions> configure)
        {
            options.AddExtension(new RedisStorageOptionsExtension(configure));
            return options;
        }
    }
}
