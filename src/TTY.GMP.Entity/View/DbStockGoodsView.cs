using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 库存信息
    /// </summary>
    public class DbStockGoodsView
    {
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
        /// 省
        /// </summary>
        public long Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public long City { get; set; }

        /// <summary>
        /// 区
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
        /// 商品含量
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
    }
}
