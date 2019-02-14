using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Enum;

namespace TTY.GMP.Entity.Web.User.Request
{
    /// <summary>
    /// 设置数据权限
    /// </summary>
    public class SaveUserDataLimitRequest : RequestBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 数据权限类型
        /// <see cref="DataLimitTypeEnum"/>
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 地区设置
        /// </summary>
        public List<DataLimitAreaRequest> Areas { get; set; }

        /// <summary>
        /// 门店设置
        /// </summary>
        public List<DataLimitShopRequest> Shops { get; set; }

        /// <summary>
        /// 数据较验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (UserId <= 0)
            {
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// 地区数据权限结构
    /// </summary>
    public class DataLimitAreaRequest
    {
        /// <summary>
        /// 地区ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 地区类型
        /// <see cref="AreaLevelEnum"/>
        /// </summary>
        public string Level { get; set; }
    }

    /// <summary>
    ///  门店
    /// </summary>
    public class DataLimitShopRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
