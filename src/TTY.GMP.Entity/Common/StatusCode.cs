using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Common
{
    /// <summary>
    /// 状态码
    /// </summary>
    public struct StatusCode
    {
        /// <summary>
        /// 成功标识
        /// </summary>
        public const string Succeed = "10000";

        /// <summary>
        /// 错误请求,数据校验错误
        /// </summary>
        public const string BadRequest = "10001";

        /// <summary>
        /// 代码异常(未知错误)
        /// </summary>
        public const string CodeError = "10002";

        /// <summary>
        /// 无权限访问
        /// </summary>
        public const string Forbidden = "10003";

        /// <summary>
        /// 未登录
        /// </summary>
        public const string NotLogin = "10004";

        /// <summary>
        /// 未查询到符合的数据
        /// </summary>
        public const string DataNotQueried = "10005";

        /// <summary>
        /// 数据无权访问 
        /// </summary>
        public const string DataForbidden = "10006";

        /// <summary>
        /// 访问过于频繁
        /// </summary>
        public const string TooFrequent = "10007";

        #region 用户登录

        /// <summary>
        /// 帐号或密码错误
        /// </summary>
        public const string Login20001 = "20001";

        /// <summary>
        /// 用户被禁用
        /// </summary>
        public const string Login20002 = "20002";

        /// <summary>
        /// 登录次数超过限制
        /// </summary>
        public const string Login20003 = "20003";

        #endregion

        #region 用户角色

        /// <summary>
        /// 角色不存在
        /// </summary>
        public const string UserRole30001 = "30001";

        /// <summary>
        /// 角色下存在用户
        /// </summary>
        public const string UserRole30002 = "30002";

        #endregion

        #region 用户

        /// <summary>
        /// 用户不存在
        /// </summary>
        public const string User40001 = "40001";

        /// <summary>
        /// 用户帐号已存在
        /// </summary>
        public const string User40002 = "40002";

        /// <summary>
        /// 用户数据权限类型错误
        /// </summary>
        public const string User40003 = "40003";

        /// <summary>
        /// 旧密码不正确
        /// </summary>
        public const string User40004 = "40004";

        #endregion

        #region 门店

        /// <summary>
        /// 门店不存在
        /// </summary>
        public const string Shop50001 = "50001";

        #endregion

        #region 获取门店销售单排名

        /// <summary>
        /// 未查询到排名信息
        /// </summary>
        public const string ShopRetailRank60001 = "60001";

        #endregion
    }
}
