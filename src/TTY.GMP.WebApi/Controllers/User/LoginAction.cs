using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Config;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.Entity.Web.User.Response;
using TTY.GMP.Http;
using TTY.GMP.IBusiness;
using TTY.GMP.Utility;
using TTY.GMP.WebApi.Common;
using TTY.GMP.WebApi.Core;

namespace TTY.GMP.WebApi.Controllers.User
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class LoginAction
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
        /// 地区业务访问
        /// </summary>
        private readonly IAreaBLL _areaBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserBll"></param>
        /// <param name="sysUserRoleBll"></param>
        /// <param name="sysUserLogBll"></param>
        /// <param name="areaBll"></param>
        public LoginAction(ISysUserBLL sysUserBll, ISysUserRoleBLL sysUserRoleBll, ISysUserLogBLL sysUserLogBll, IAreaBLL areaBll)
        {
            this._sysUserBll = sysUserBll;
            this._sysUserRoleBll = sysUserRoleBll;
            this._sysUserLogBll = sysUserLogBll;
            this._areaBll = areaBll;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(HttpContext httpContext, LoginRequest request)
        {
            var response = new ResponseBase();
            if (!await CheckUserLoginFailedRecord(request.UserAccount))
            {
                return response.GetResponseError(StatusCode.Login20003, "登录失败次数超过限制");
            }
            if (string.IsNullOrEmpty(request.UserAccount) || string.IsNullOrEmpty(request.UserPassword))
            {
                return response.GetResponseBadRequest();
            }
            var pwd = CryptogramHelper.Encrypt3DES(request.UserPassword);
            var user = await _sysUserBll.GetSysUser(request.UserAccount, pwd);
            if (user == null)
            {
                await _sysUserBll.AddUserLoginFailedRecord(request.UserAccount, SystemConfig.UserLoginConfig.LoginFailedMaxCount, SystemConfig.UserLoginConfig.LoginFailedTimeOut);
                return response.GetResponseError(StatusCode.Login20001, "帐号或密码错误");
            }
            if (user.StatusFlag == (int)UserStatusFlagEnum.Disable)
            {
                return response.GetResponseError(StatusCode.Login20002, "帐号被禁用");
            }
            var loginView = await GetLoginView(user);
            var userRole = await _sysUserRoleBll.GetSysUserRole(user.UserRoleId);
            AppTicket.SetAppTicket(httpContext, user, userRole);
            await _sysUserBll.UpdateUserLastLoginTime(user.UserId, DateTime.Now);
            await _sysUserBll.RemoveUserLoginFailedRecord(request.UserAccount);
            AddUserLoginLog(user);
            return response.GetResponseSuccess(loginView);
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
                Describe = "用户登录"
            });
        }

        /// <summary>
        /// 构造返回的LoginView
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public async Task<LoginView> GetLoginView(SysUser sysUser)
        {
            var area = await GetArea(sysUser);
            var token = JwtHelper.GenerateToken(sysUser.UserId, out DateTime expiresTime);
            var loginView = new LoginView()
            {
                TokenType = "Bearer",
                Token = token,
                ExpiresTime = expiresTime,
                User = new LoginUserView()
                {
                    UserId = sysUser.UserId,
                    NickName = sysUser.NickName
                },
                AreaLevel = area.Item2,
                AreaType = area.Item3
            };
            loginView = await HandleLoginViewArea(loginView, area.Item1);
            return loginView;
        }

        /// <summary>
        /// 获取地区信息
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        private async Task<Tuple<List<string>, string, int>> GetArea(SysUser sysUser)
        {
            switch (sysUser.DataLimitType)
            {
                case (int)DataLimitTypeEnum.All:
                    return Tuple.Create(new List<string>(), AreaLevelEnum.Province, sysUser.DataLimitType);
                case (int)DataLimitTypeEnum.Area:
                    if (string.IsNullOrEmpty(sysUser.DataLimitArea))
                    {
                        return Tuple.Create(new List<string>(), "", 404);
                    }
                    var area = await ComLib.GetGetAreaStatisticsAreaId(_areaBll, sysUser.DataLimitArea);
                    return Tuple.Create(area.Item1.Split(',').ToList(), area.Item2, sysUser.DataLimitType);
                case (int)DataLimitTypeEnum.Shop:
                    if (string.IsNullOrEmpty(sysUser.DataLimitShop))
                    {
                        return Tuple.Create(new List<string>(), "", 404);
                    }
                    return Tuple.Create(new List<string>(), "", sysUser.DataLimitType);
            }
            return Tuple.Create(new List<string>(), "", 404);
        }

        /// <summary>
        /// 处理地区信息
        /// </summary>
        /// <param name="loginView"></param>
        /// <param name="areaIds"></param>
        /// <returns></returns>
        private async Task<LoginView> HandleLoginViewArea(LoginView loginView, List<string> areaIds)
        {
            if (areaIds == null || !areaIds.Any())
            {
                return loginView;
            }
            Tuple<long, string> province;
            Tuple<long, string> city;
            switch (loginView.AreaLevel)
            {
                case AreaLevelEnum.Province:
                    loginView.ProvinceIds = await GetAreas(areaIds);
                    break;
                case AreaLevelEnum.City:
                    loginView.CityIds = await GetAreas(areaIds);
                    province = await GetAreaParentId(areaIds[0].ToLong());
                    loginView.ProvinceIds = new List<LoginUserArea>() { new LoginUserArea()
                    {
                         AreaId = province.Item1,
                         AreaName = province.Item2
                    }  };
                    break;
                case AreaLevelEnum.District:
                    loginView.DistrictIds = await GetAreas(areaIds);
                    city = await GetAreaParentId(areaIds[0].ToLong());
                    loginView.CityIds = new List<LoginUserArea>() { new LoginUserArea()
                    {
                         AreaId = city.Item1,
                         AreaName = city.Item2
                    } };
                    province = await GetAreaParentId(city.Item1);
                    loginView.ProvinceIds = new List<LoginUserArea>() { new LoginUserArea()
                    {
                         AreaId = province.Item1,
                         AreaName = province.Item2
                    } };
                    break;
                case AreaLevelEnum.Street:
                    loginView.StreetIds = await GetAreas(areaIds);
                    var district = await GetAreaParentId(areaIds[0].ToLong());
                    loginView.DistrictIds = new List<LoginUserArea>() { new LoginUserArea()
                    {
                         AreaId= district.Item1,
                         AreaName = district.Item2
                    } };
                    city = await GetAreaParentId(district.Item1);
                    loginView.CityIds = new List<LoginUserArea>() { new LoginUserArea()
                    {
                         AreaId = city.Item1,
                         AreaName = city.Item2
                    } };
                    province = await GetAreaParentId(city.Item1);
                    loginView.ProvinceIds = new List<LoginUserArea>() { new LoginUserArea()
                    {
                         AreaId = province.Item1,
                         AreaName = province.Item2
                    } };
                    break;
            }
            return loginView;
        }

        /// <summary>
        /// 获取地区信息
        /// </summary>
        /// <param name="areaIds"></param>
        /// <returns></returns>
        private async Task<List<LoginUserArea>> GetAreas(List<string> areaIds)
        {
            var areas = await _areaBll.GetArea(areaIds.Select(p => p.ToLong()).ToList());
            var loginUserAreas = new List<LoginUserArea>();
            foreach (var a in areas)
            {
                loginUserAreas.Add(new LoginUserArea()
                {
                    AreaId = a.AreaId,
                    AreaName = a.AreaName
                });
            }
            return loginUserAreas;
        }

        /// <summary>
        /// 获取父节点地区信息
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        private async Task<Tuple<long, string>> GetAreaParentId(long areaId)
        {
            if (areaId == 0)
            {
                return Tuple.Create(0L, string.Empty);
            }
            var area = await _areaBll.GetArea(areaId);
            if (area == null)
            {
                return Tuple.Create(0L, string.Empty);
            }
            if (area.ParentId == 0)
            {
                return Tuple.Create(0L, string.Empty);
            }
            var areaParent = await _areaBll.GetArea(area.ParentId);
            if (areaParent == null)
            {
                return Tuple.Create(0L, string.Empty);
            }
            return Tuple.Create(area.ParentId, areaParent.AreaName);
        }
    }
}
