using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 商品库存统计
    /// </summary>
    [Table("statisticsstockgoods")]
    public class StatisticsStockGoods
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public long StatisticsId { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        public long OrgId { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public long GoodsId { get; set; }

        /// <summary>
        /// 商品分类ID
        /// </summary>
        public long GoodsCategoryId { get; set; }

        /// <summary>
        /// 省ID
        /// </summary>
        public long Province { get; set; }

        /// <summary>
        /// 市ID 
        /// </summary>
        public long City { get; set; }

        /// <summary>
        ///  区
        /// </summary>
        public long District { get; set; }

        /// <summary>
        /// 街道、乡镇
        /// </summary>
        public long Street { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        public string GoodsSpec { get; set; }

        /// <summary>
        /// 商品总含量
        /// </summary>
        public string GoodsContents { get; set; }

        /// <summary>
        /// 商品类别名称
        /// </summary>
        public string GoodsCategoryName { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public decimal TotalCount { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public decimal TotalWeight { get; set; }

        /// <summary>
        /// 折百比总重量
        /// </summary>
        public decimal TotalContentsWeight { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
