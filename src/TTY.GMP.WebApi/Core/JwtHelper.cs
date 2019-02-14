using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Config;

namespace TTY.GMP.WebApi.Core
{
    /// <summary>
    /// Jwt帮助类
    /// </summary>
    internal class JwtHelper
    {
        /// <summary>
        /// 通过userid创建一个访问Token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="expiresTime"></param>
        /// <returns></returns>
        internal static string GenerateToken(long userId, out DateTime expiresTime)
        {
            var claims = new[]
               {
                   new Claim(SystemConfig.AuthenticationConfig.ClaimType,userId.ToString())
               };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SystemConfig.AuthenticationConfig.IssuerSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            expiresTime = DateTime.Now.AddHours(SystemConfig.AuthenticationConfig.ExpiresHours);
            var token = new JwtSecurityToken(
                issuer: SystemConfig.AuthenticationConfig.ValidIssuer,
                audience: SystemConfig.AuthenticationConfig.ValidAudience,
                claims: claims,
                expires: expiresTime,
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
