using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Enum;

namespace TTY.GMP.Entity.Web.User.Response
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginView
    {
        /// <summary>
        /// Token类型
        /// </summary>
        public string TokenType { get; set; }

        /// <summary>
        /// 授权Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime ExpiresTime { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public LoginUserView User { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public List<LoginUserArea> ProvinceIds { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public List<LoginUserArea> CityIds { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public List<LoginUserArea> DistrictIds { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        public List<LoginUserArea> StreetIds { get; set; }

        /// <summary>
        /// 地区类型
        /// </summary>
        public string AreaLevel { get; set; }

        /// <summary>
        /// 地区 <see cref="DataLimitTypeEnum"/>
        /// </summary>
        public int AreaType { get; set; }
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class LoginUserView
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
    }

    public class LoginUserArea
    {
        public long AreaId { get; set; }

        public string AreaName { get; set; }
    }
}
