using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 商品信息
    /// </summary>
    public class DbGoodsView
    {
        /// <summary>
        /// 单位
        /// </summary>
        public long BaseUnitId { get; set; }

        /// <summary>
        /// 品牌ID
        /// </summary>
        public long BrandId { get; set; }

        /// <summary>
        /// 库存ID
        /// </summary>
        public long StockId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public long GoodsId { get; set; }

        /// <summary>
        /// 商品类别ID
        /// </summary>
        public long GoodsCategoryId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string GoodsCode { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string GoodsShortName { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        public string GoodsSpec { get; set; }

        /// <summary>
        /// 商品产地
        /// </summary>
        public string GoodsOrigin { get; set; }

        /// <summary>
        /// 商品成分
        /// </summary>
        public string GoodsIngredient { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        public string DosageForms { get; set; }

        /// <summary>
        /// 含量
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 登记证持有人
        /// </summary>
        public string RegistrationHolder { get; set; }

        /// <summary>
        /// 登记证号
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// 生成厂家
        /// </summary>
        public string GoodsProduct { get; set; }

        /// <summary>
        /// 是否为限制性农药
        /// </summary>
        public int GoodsRestrictive { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

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
        /// 乡镇、街道
        /// </summary>
        public long Street { get; set; }
    }
}
