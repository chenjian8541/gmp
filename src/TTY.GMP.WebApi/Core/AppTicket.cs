using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using TTY.GMP.Authority;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using TTY.GMP.WebApi.Extensions;
using TTY.GMP.Utility;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.WebApi.Core
{
    /// <summary>
    /// 应用程序登录凭证
    /// </summary>
    [Serializable]
    public class AppTicket
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AppTicket()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="weightSum"></param>
        /// <param name="dataLimitArea"></param>
        /// <param name="dataLimitShop"></param>
        /// <param name="dataLimitType"></param>
        public AppTicket(BigInteger weightSum, string dataLimitArea, string dataLimitShop, int dataLimitType)
        {
            this.WeightSum = weightSum;
            this.DataLimitArea = dataLimitArea;
            this.DataLimitShop = dataLimitShop;
            this.DataLimitType = dataLimitType;
        }

        /// <summary>
        /// 登录人权限总值
        /// </summary>
        public BigInteger WeightSum { get; set; }

        /// <summary>
        /// 数据权限类型
        /// <see cref="TTY.GMP.Entity.Enum.DataLimitTypeEnum"/>
        /// </summary>
        public int DataLimitType { get; set; }

        /// <summary>
        /// 数据权限（地区）
        /// </summary>
        public string DataLimitArea { get; set; }

        /// <summary>
        /// 数据权限（门店）
        /// </summary>
        public string DataLimitShop { get; set; }

        /// <summary>
        /// 获取登录凭证
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        internal static AppTicket GetAppTicket(HttpContext httpContext)
        {
            var tempTicket = httpContext.Session.GetObject<AppTicket>("app_ticket");
            if (tempTicket != null)
            {
                return tempTicket;
            }
            var sysUserBll = CustomServiceLocator.GetInstance<ISysUserBLL>();
            var user = sysUserBll.GetSysUser(httpContext.Request.GetUserId()).Result;
            var sysUserRoleBll = CustomServiceLocator.GetInstance<ISysUserRoleBLL>();
            var userRole = sysUserRoleBll.GetSysUserRole(user.UserRoleId).Result;
            return SetAppTicket(httpContext, user, userRole);
        }

        /// <summary>
        /// 设置登录凭证
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="sysUser"></param>
        /// <param name="sysUserRole"></param>
        internal static AppTicket SetAppTicket(HttpContext httpContext, SysUser sysUser, SysUserRole sysUserRole)
        {
            var weightSum = GetWeightSum(sysUser.AuthorityValue, sysUserRole.AuthorityValue);
            var appTicket = new AppTicket(weightSum, sysUser.DataLimitArea, sysUser.DataLimitShop, sysUser.DataLimitType);
            httpContext.Session.SetObject("app_ticket", appTicket);
            return appTicket;
        }

        /// <summary>
        /// 计算用户的总权限值
        /// </summary>
        /// <param name="authorityValues"></param>
        /// <returns></returns>
        private static BigInteger GetWeightSum(params string[] authorityValues)
        {
            BigInteger userValue = 0;
            foreach (var value in authorityValues)
            {
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
                userValue = userValue | value.ToBigInteger();
            }
            return userValue;
        }
    }
}
