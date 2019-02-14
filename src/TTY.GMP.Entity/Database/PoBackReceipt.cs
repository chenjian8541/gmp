using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 采购退货单
    /// </summary>
    [Table("po_back_receipt")]
    public class PoBackReceipt
    {
        /// <summary>
        /// 退货单ID
        /// </summary>
        [Key]
        public long back_id { get; set; }

        /// <summary>
        /// 退货单开单日期
        /// </summary>
        public long back_date { get; set; }

        /// <summary>
        /// 退货单编码
        /// </summary>
        public string back_code { get; set; }

        /// <summary>
        /// 组织
        /// </summary>
        public long back_org { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public long back_offer_id { get; set; }

        /// <summary>
        /// 结算单位
        /// </summary>
        public long back_settelment_id { get; set; }

        /// <summary>
        /// 采购员
        /// </summary>
        public long back_buyer { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public long back_department { get; set; }

        /// <summary>
        /// 出库仓库
        /// </summary>
        public long stock_id { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public long back_currency { get; set; }

        /// <summary>
        /// 付款期限
        /// </summary>
        public int? back_pay_limt { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string back_explain { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string back_summary { get; set; }

        /// <summary>
        /// 条码或者二维码
        /// </summary>
        public string back_barcode { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public decimal? back_tax { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public decimal? back_discount { get; set; }

        /// <summary>
        /// 收款账号
        /// </summary>
        public long back_account { get; set; }

        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal? back_actual_pay { get; set; }

        /// <summary>
        /// 抹零金额
        /// </summary>
        public decimal? back_erase { get; set; }

        /// <summary>
        /// 抹零后金额
        /// </summary>
        public decimal? back_erased { get; set; }

        /// <summary>
        /// 原始单据id
        /// </summary>
        public long bill_original_id { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public long creater { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long createtime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public long modifier { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public long modifiedtime { get; set; }

        /// <summary>
        /// 过账日期
        /// </summary>
        public long posting_time { get; set; }

        /// <summary>
        /// 过账操作人
        /// </summary>
        public long posted_man { get; set; }

        /// <summary>
        /// 状态：0 表示草稿，1 表示提交，2 表示审核，3 表示复核
        /// </summary>
        public int? state_id { get; set; }

        /// <summary>
        /// 红冲单据表示1，原始单据表示0，非红冲单据表示-1
        /// </summary>
        public int? receipt_back_red { get; set; }

        /// <summary>
        /// 红冲前原始单据ID，没有则为null
        /// </summary>
        public long receipt_back_red_original { get; set; }

        /// <summary>
        /// 绑定门店id
        /// </summary>
        public long user_shop_id { get; set; }

        /// <summary>
        /// 经手人
        /// </summary>
        public long handle_man { get; set; }
    }
}
