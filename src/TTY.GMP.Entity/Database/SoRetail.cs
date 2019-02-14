using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 零售订单
    /// </summary>
    [Table("so_retail")]
    public class SoRetail
    {
        /// <summary>
        /// 单据id
        /// </summary>
        [Key]
        [Column("retail_id")]
        public long RetailId { get; set; }

        /// <summary>
        /// 单据编码
        /// </summary>
        [Column("bill_code")]
        public string BillCode { get; set; }

        /// <summary>
        /// 开单日期
        /// </summary>
        [Column("bill_date")]
        public long BillDate { get; set; }

        /// <summary>
        /// 经销人，或者销售人
        /// </summary>
        [Column("salesman")]
        public long Salesman { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        [Column("currency")]
        public long Currency { get; set; }

        /// <summary>
        /// 单据说明
        /// </summary>
        [Column("explain")]
        public string Explain { get; set; }

        /// <summary>
        /// 单据摘要
        /// </summary>
        [Column("summary")]
        public string Summary { get; set; }

        /// <summary>
        /// 单据条码或者二维码
        /// </summary>
        [Column("barcode")]
        public string Barcode { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        [Column("receivable")]
        public decimal Receivable { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        [Column("paid_in")]
        public decimal PaidIn { get; set; }

        /// <summary>
        /// 整单折扣
        /// </summary>
        [Column("discount")]
        public decimal Discount { get; set; }

        /// <summary>
        /// 整单折扣率
        /// </summary>
        [Column("discount_rate")]
        public decimal DiscountRate { get; set; }

        /// <summary>
        /// 抹零金额
        /// </summary>
        [Column("erase")]
        public decimal Erase { get; set; }

        /// <summary>
        /// 找零
        /// </summary>
        [Column("zase")]
        public decimal Zase { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        [Column("real")]
        public decimal Real { get; set; }

        /// <summary>
        /// 出库仓库ID
        /// </summary>
        [Column("stock_id")]
        public long StockId { get; set; }

        /// <summary>
        /// 状态：0 表示草稿，1 表示提交，2 表示审核，3 表示复核
        /// </summary>
        [Column("state_id")]
        public int StateId { get; set; }

        /// <summary>
        /// 会员id
        /// </summary>
        [Column("member_id")]
        public long MemberId { get; set; }

        /// <summary>
        /// 默认为0，表示非赠送，赠送则为1
        /// </summary>
        [Column("is_give")]
        public int? IsGive { get; set; }

        /// <summary>
        /// 红冲单据表示1，原始单据表示0，非红冲单据表示-1
        /// </summary>
        [Column("red")]
        public int? Red { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [Column("department_id")]
        public long DepartmentId { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        [Column("org_id")]
        public long OrgId { get; set; }

        /// <summary>
        /// 收款账户
        /// </summary>
        [Column("accout_id")]
        public long AccoutId { get; set; }

        /// <summary>
        /// 退货前原始单据ID，没有则为null
        /// </summary>
        [Column("back_original")]
        public long BackOriginal { get; set; }

        /// <summary>
        /// 红冲前原始单据ID，没有则为null
        /// </summary>
        [Column("red_original")]
        public long RedOriginal { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        [Column("tax")]
        public decimal Tax { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        [Column("modifier")]
        public long? Modifier { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [Column("creater")]
        public long? Creater { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Column("created_date")]
        public long? CreatedDate { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [Column("modified_date")]
        public long? ModifiedDate { get; set; }

        /// <summary>
        /// 过账日期
        /// </summary>
        [Column("posting_time")]
        public long? PostingTime { get; set; }

        /// <summary>
        /// 过账操作人
        /// </summary>
        [Column("posted_man")]
        public long? PostedMan { get; set; }
    }
}
