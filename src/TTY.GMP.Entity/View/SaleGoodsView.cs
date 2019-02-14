using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 销售商品
    /// </summary>
    public class SaleGoodsView
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public long GoodsId { get; set; }

        /// <summary>
        /// 商品类别
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string GoodsSpec { get; set; }

        /// <summary>
        /// 登记证号
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// 生产企业
        /// </summary>
        public string RegistrationHolder { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public decimal Qty { get; set; }
    }
}
