using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Open.View
{
    /// <summary>
    /// 商品及库存信息
    /// </summary>
    public class OpGoodsView
    {
        /// <summary>
        /// 商品类别
        /// </summary>
        public string GoodsCategoryName { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string GoodsCode { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品简称
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
        /// 商品品牌
        /// </summary>
        public string GoodsBrand { get; set; }

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
        /// 登记证号
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// 登记证持有人
        /// </summary>
        public string RegistrationHolder { get; set; }

        /// <summary>
        /// 生成厂家
        /// </summary>
        public string GoodsProduct { get; set; }

        /// <summary>
        /// 是否为限制性农药
        /// </summary>
        public int GoodsRestrictive { get; set; }

        /// <summary>
        /// 库存单位
        /// </summary>
        public string RepertoryUnit { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public decimal RepertoryQty { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 乡镇、街道
        /// </summary>
        public string Street { get; set; }
    }
}
