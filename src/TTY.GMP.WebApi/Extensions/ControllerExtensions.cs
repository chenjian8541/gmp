using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Config;

namespace TTY.GMP.WebApi.Extensions
{
    /// <summary>
    /// Controller扩展类
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// 获取cookie解密得到UserId
        /// </summary>
        /// <param name="this"> </param>
        /// <returns></returns>
        public static long GetUserId(this HttpRequest @this)
        {
            var stringId = @this.HttpContext.User?.Claims?.FirstOrDefault(p => p.Type.Equals(SystemConfig.AuthenticationConfig.ClaimType))?.Value;
            if (string.IsNullOrEmpty(stringId))
            {
                return 0L;
            }
            long.TryParse(stringId, out long userId);
            return userId;
        }
    }
}
