using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 店面
    /// </summary>
    [Table("cm_shop")]
    public class Shop
    {
        /// <summary>
        /// 店面ID
        /// </summary>
        [Key]
        [Column("shop_id")]
        public long ShopId { get; set; }

        /// <summary>
        /// 绑定端编码
        /// </summary>
        [Column("shop_code")]
        public string ShopCode { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        [Column("shop_name")]
        public string ShopName { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        [Column("stock_id")]
        public long StockId { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        [Column("org_id")]
        public long OrgId { get; set; }

        /// <summary>
        /// 现金账户ID
        /// </summary>
        [Column("cash_id")]
        public long CashId { get; set; }

        /// <summary>
        /// 银行存款账户ID
        /// </summary>
        [Column("bank_id")]
        public long BankId { get; set; }

        /// <summary>
        /// 会员支付账户ID
        /// </summary>
        [Column("member_pay_id")]
        public long MemberPayId { get; set; }

        /// <summary>
        /// 0表示未启用，1表示启用
        /// </summary>
        [Column("status")]
        public int? Status { get; set; }

        /// <summary>
        /// 拼音助记码
        /// </summary>
        [Column("pycode")]
        public string Pycode { get; set; }

        /// <summary>
        /// 门店电话
        /// </summary>
        [Column("shop_telphone")]
        public string ShopTelphone { get; set; }

        /// <summary>
        /// 门店地址
        /// </summary>
        [Column("shop_address")]
        public string ShopAddress { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [Column("creater")]
        public long Creater { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Column("createdtime")]
        public long CreatedTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        [Column("modifier")]
        public long? Modifier { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [Column("modifiedtime")]
        public long? ModifiedTime { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [Column("province")]
        public long? Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [Column("city")]
        public long? City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        [Column("district")]
        public long? District { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        [Column("street")]
        public long? Street { get; set; }

        /// <summary>
        /// 门店联系人
        /// </summary>
        [Column("shop_linkMan")]
        public string ShopLinkMan { get; set; }
    }
}
