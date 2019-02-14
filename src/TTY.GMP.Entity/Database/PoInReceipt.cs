using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 入库单据
    /// </summary>
    [Table("po_in_receipt")]
    public class PoInReceipt
    {
        /// <summary>
        /// 单据ID
        /// </summary>
        [Key]
        public long in_id { get; set; }

        /// <summary>
        /// 单据编码
        /// </summary>
        public string in_code { get; set; }

        /// <summary>
        /// 开单日期
        /// </summary>
        public long in_date { get; set; }

        /// <summary>
        /// 组织
        /// </summary>
        public long in_org_id { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public long in_offer_id { get; set; }

        /// <summary>
        /// 结算单位
        /// </summary>
        /// 
        public long in_setterlment_id { get; set; }

        /// <summary>
        /// 采购员
        /// </summary>
        public long in_buyer { get; set; }

        /// <summary>
        /// 采购部门
        /// </summary>
        public long in_department_id { get; set; }

        /// <summary>
        /// 入库仓库
        /// </summary>
        public long stock_id { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public long currency_id { get; set; }

        /// <summary>
        /// 付款期限
        /// </summary>
        public int expiration_pay { get; set; }

        /// <summary>
        /// 单据说明
        /// </summary>
        public string in_explain { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string in_summary { get; set; }

        /// <summary>
        /// 往来科目
        /// </summary>
        public long? contacts_subjects_id { get; set; }

        /// <summary>
        /// 对货条码
        /// </summary>
        public string in_barcode { get; set; }

        /// <summary>
        /// 条码录入
        /// </summary>
        public string input_barcode { get; set; }

        /// <summary>
        /// 付款账户
        /// </summary>
        public long? in_accout_id { get; set; }

        /// <summary>
        /// 整单折扣
        /// </summary>
        public decimal? discount { get; set; }

        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal? in_actucal_pay { get; set; }

        /// <summary>
        /// 抹零金额
        /// </summary>
        public decimal? in_erase { get; set; }

        /// <summary>
        /// 抹零后金额
        /// </summary>
        public decimal? in_erased { get; set; }

        /// <summary>
        /// 经手人
        /// </summary>
        public long? handlerPerson { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>

        public long? creater { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public long? createdtime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public long? modifier { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public long? modifiedtime { get; set; }

        /// <summary>
        /// 过账日期
        /// </summary>
        public long posting_time { get; set; }

        /// <summary>
        /// 过账操作人
        /// </summary>
        public long posted_man { get; set; }

        /// <summary>
        /// 绑定门店id
        /// </summary>
        public long user_shop_id { get; set; }

        /// <summary>
        /// 状态：0 表示草稿，1 表示提交，2 表示审核，3 表示复核
        /// </summary>
        public long? state_id { get; set; }

        /// <summary>
        /// 红冲单据表示1，原始单据表示0，非红冲单据表示-1
        /// </summary>
        public int? red { get; set; }

        /// <summary>
        /// 红冲前原始单据ID，没有则为null
        /// </summary>
        public long red_original { get; set; }
    }
}
