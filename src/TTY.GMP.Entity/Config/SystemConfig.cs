using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Config
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfig
    {
        /// <summary>
        /// Jwt授权配置
        /// </summary>
        public static AuthenticationConfig AuthenticationConfig;

        /// <summary>
        /// 用户登录配置
        /// </summary>
        public static UserLoginConfig UserLoginConfig;

        /// <summary>
        /// 静态构造函数
        /// 初始化内部静态成员
        /// </summary>
        static SystemConfig()
        {
            AuthenticationConfig = new AuthenticationConfig();
            UserLoginConfig = new UserLoginConfig();
        }
    }

    /// <summary>
    /// JWT授权配置
    /// </summary>
    public class AuthenticationConfig
    {
        /// <summary>
        /// jwt用户类型名称
        /// </summary>
        public string ClaimType = "gmp_user";

        /// <summary>
        /// 默认的身份验证方案名称
        /// </summary>
        public string DefaultAuthenticateScheme = "JwtBearer";

        /// <summary>
        /// 默认方案名称
        /// </summary>
        public string DefaultChallengeScheme = "JwtBearer";

        /// <summary>
        /// 令牌发行者名称
        /// </summary>
        public string ValidIssuer = "www.ttyun.com";

        /// <summary>
        /// 获取令牌用户名称
        /// </summary>
        public string ValidAudience = "gmp_user";

        /// <summary>
        /// 签名密钥
        /// </summary>
        public string IssuerSigningKey = "TTY.GMP.JWT.2d88d1def5494c81ac5ce548fcd7a4ae";

        /// <summary>
        /// 是否验证过期时间
        /// </summary>
        public bool ValidateLifetime = true;

        /// <summary>
        /// 默认过期时间(48小时后)
        /// </summary>
        public int ExpiresHours = 48;

        /// <summary>
        /// 验证过期时间时应用的时钟偏差(单位分钟)
        /// </summary>
        public int ClockSkew = 2;
    }

    /// <summary>
    /// 用户登录配置
    /// </summary>
    public class UserLoginConfig
    {
        /// <summary>
        /// 每个时间段内登录失败的最大数
        /// </summary>
        public int LoginFailedMaxCount = 5;

        /// <summary>
        /// 登录失败时禁止登录时长(单位分钟)
        /// </summary>
        public int LoginFailedTimeOut = 10;
    }
}
