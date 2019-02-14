using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTY.Api.Throttle;
using TTY.GMP.Cache.Redis;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Config;
using TTY.GMP.ICache;

namespace TTY.GMP.WebApi.Extensions
{
    /// <summary>
    /// ServiceCollection扩展类
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加JWT授权
        /// </summary>
        /// <param name="services"></param>
        internal static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = SystemConfig.AuthenticationConfig.DefaultAuthenticateScheme;
                options.DefaultChallengeScheme = SystemConfig.AuthenticationConfig.DefaultChallengeScheme;
            }).AddJwtBearer(SystemConfig.AuthenticationConfig.DefaultAuthenticateScheme, jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SystemConfig.AuthenticationConfig.IssuerSigningKey)),
                    ValidateIssuer = true,
                    ValidIssuer = SystemConfig.AuthenticationConfig.ValidIssuer,
                    ValidateAudience = true,
                    ValidAudience = SystemConfig.AuthenticationConfig.ValidAudience,
                    ValidateLifetime = SystemConfig.AuthenticationConfig.ValidateLifetime,
                    ClockSkew = TimeSpan.FromMinutes(SystemConfig.AuthenticationConfig.ClockSkew)
                };
            });
        }

        /// <summary>
        /// 添加Redis
        /// </summary>
        /// <param name="services"></param>
        internal static void AddRedis(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<AppSettings>>().Value;
                var configuration = ConfigurationOptions.Parse(settings.RedisConfig.ServerList, true);
                configuration.ResolveDns = true;
                return ConnectionMultiplexer.Connect(configuration);
            });
            services.AddTransient<ICacheProvider, RedisProvider>();
        }

        /// <summary>
        /// 添加API限流
        /// </summary>
        /// <param name="services"></param>
        internal static void AddApiThrottle(this IServiceCollection services)
        {
            var settings = services.BuildServiceProvider().GetRequiredService<IOptions<AppSettings>>().Value;
            services.AddApiThrottle(options =>
            {
                options.UseRedisCacheAndStorage(opts =>
                {
                    opts.ConnectionString = settings.RedisConfig.ServerList;
                    opts.KeyPrefix = "ttyapithrottle";
                    opts.DefaultDb = settings.RedisConfig.DefaultDb;
                });
                options.OnIpAddress = (context) =>
                {
                    var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                    if (string.IsNullOrEmpty(ip))
                    {
                        ip = context.Connection.RemoteIpAddress.ToString();
                    }
                    return ip;
                };
                options.onIntercepted = (context, valve, where) =>
                {
                    if (where == IntercepteWhere.Middleware)
                    {
                        return new JsonResult(new ResponseBase().GetResponseError(StatusCode.TooFrequent, "访问过于频繁，请稍后重试！"));
                    }
                    else
                    {
                        return new JsonResult(new ResponseBase().GetResponseError(StatusCode.TooFrequent, "访问过于频繁，请稍后重试！"));
                    }
                };
            });
        }
    }
}
