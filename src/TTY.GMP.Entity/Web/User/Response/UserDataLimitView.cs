using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Enum;

namespace TTY.GMP.Entity.Web.User.Response
{
    /// <summary>
    /// 用户数据权限信息
    /// </summary>
    public class UserDataLimitView
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="areas"></param>
        /// <param name="shops"></param>
        public UserDataLimitView(int type, List<DataLimitAreaView> areas, List<DataLimitShopView> shops)
        {
            this.Type = type;
            this.Areas = areas;
            this.Shops = shops;
        }

        /// <summary>
        /// 类型
        /// <see cref="DataLimitTypeEnum"/>
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public List<DataLimitAreaView> Areas { get; set; }

        /// <summary>
        /// 选择的地区
        /// </summary>
        public List<long> MyAreas { get; set; }

        /// <summary>
        /// 门店
        /// </summary>
        public List<DataLimitShopView> Shops { get; set; }
    }

    /// <summary>
    /// 地区
    /// </summary>
    public class DataLimitAreaView
    {
        public long Id { get; set; }

        /// <summary>
        /// 类型
        /// <see cref="Enum.AreaLevelEnum"/>
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 子地区
        /// </summary>
        public List<DataLimitAreaView> Children { get; set; }
    }

    /// <summary>
    /// 门店
    /// </summary>
    public class DataLimitShopView
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
