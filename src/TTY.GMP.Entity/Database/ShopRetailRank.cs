using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 门店销售单排名
    /// </summary>
    [Table("shopretailrank")]
    public class ShopRetailRank
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Key]
        public long ShopId { get; set; }

        /// <summary>
        /// 类型 <see cref="TTY.GMP.Entity.Enum.ShopRetailRankTypeEnum"/>
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 排名
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 门店联系人
        /// </summary>
        public string ShopLinkMan { get; set; }

        /// <summary>
        /// 门店联系电话
        /// </summary>
        public string ShopTelphone { get; set; }

        /// <summary>
        /// 门店联系地址
        /// </summary>
        public string ShopAddress { get; set; }

        /// <summary>
        /// 订单数
        /// </summary>
        public int BillCount { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public long OrgId { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgNo { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 是否为被限制者
        /// </summary>
        public bool IsLimit { get; set; }

        /// <summary>
        /// 是否最新
        /// </summary>
        public bool IsLast { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// 推荐人名称
        /// </summary>
        public string RecommendName { get; set; }
    }
}
