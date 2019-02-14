using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Config;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Open.Request;
using TTY.GMP.Entity.Open.Response;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.Entity.Web.User.Response;
using TTY.GMP.IBusiness;
using TTY.GMP.Utility;
using TTY.GMP.WebApi.Common;
using TTY.GMP.WebApi.Core;

namespace TTY.GMP.WebApi.Controllers.Open
{
    /// <summary>
    /// 获取访问AccessToken
    /// </summary>
    public class GetAccessTokenAction
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        private readonly ISysUserBLL _sysUserBll;

        /// <summary>
        /// 用户角色业务访问
        /// </summary>
        private readonly ISysUserRoleBLL _sysUserRoleBll;

        /// <summary>
        /// 用户日志操作
        /// </summary>
        private readonly ISysUserLogBLL _sysUserLogBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserBll"></param>
        /// <param name="sysUserRoleBll"></param>
        /// <param name="sysUserLogBll"></param>
        public GetAccessTokenAction(ISysUserBLL sysUserBll, ISysUserRoleBLL sysUserRoleBll, ISysUserLogBLL sysUserLogBll)
        {
            this._sysUserBll = sysUserBll;
            this._sysUserRoleBll = sysUserRoleBll;
            this._sysUserLogBll = sysUserLogBll;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(HttpContext httpContext, GetAccessTokenRequest request)
        {
            var response = new ResponseBase();
            if (string.IsNullOrEmpty(request.AppId) || string.IsNullOrEmpty(request.Secret))
            {
                return response.GetResponseBadRequest();
            }
            if (!await CheckUserLoginFailedRecord(request.AppId))
            {
                return response.GetResponseError(StatusCode.Login20003, "登录失败次数超过限制");
            }
            var pwd = CryptogramHelper.Encrypt3DES(request.Secret);
            var user = await _sysUserBll.GetSysUser(request.AppId, pwd);
            if (user == null)
            {
                await _sysUserBll.AddUserLoginFailedRecord(request.AppId, SystemConfig.UserLoginConfig.LoginFailedMaxCount, SystemConfig.UserLoginConfig.LoginFailedTimeOut);
                return response.GetResponseError(StatusCode.Login20001, "帐号或密码错误");
            }
            if (user.StatusFlag == (int)UserStatusFlagEnum.Disable)
            {
                return response.GetResponseError(StatusCode.Login20002, "帐号被禁用");
            }
            var accessToken = GetAccessTokenResponse(user);
            var userRole = await _sysUserRoleBll.GetSysUserRole(user.UserRoleId);
            AppTicket.SetAppTicket(httpContext, user, userRole);
            await _sysUserBll.UpdateUserLastLoginTime(user.UserId, DateTime.Now);
            await _sysUserBll.RemoveUserLoginFailedRecord(request.AppId);
            AddUserLoginLog(user);
            return response.GetResponseSuccess(accessToken);
        }

        /// <summary>
        /// 账户是否允许登录
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private async Task<bool> CheckUserLoginFailedRecord(string account)
        {
            var record = await _sysUserBll.GetUserLoginFailedRecord(account);
            if (record == null)
            {
                return true;
            }
            if (record.ExpireAtTime > DateTime.Now)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 增加用户登录日志
        /// </summary>
        /// <param name="sysUser"></param>
        private void AddUserLoginLog(SysUser sysUser)
        {
            this._sysUserLogBll.AddSysUserLog(new SysUserLog()
            {
                UserId = sysUser.UserId,
                Type = (int)UserLogEnum.Login,
                Ot = DateTime.Now,
                IpAddress = string.Empty,
                Describe = "开放接口用户登录"
            });
        }

        /// <summary>
        /// 构造返回的数据
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public GetAccessTokenResponse GetAccessTokenResponse(SysUser sysUser)
        {
            var token = JwtHelper.GenerateToken(sysUser.UserId, out DateTime expiresTime);
            var accessToken = new GetAccessTokenResponse()
            {
                AccessToken = $"Bearer {token}",
                ExpiresIn = SystemConfig.AuthenticationConfig.ExpiresHours * 3600 - 600
            };
            return accessToken;
        }
    }
}
