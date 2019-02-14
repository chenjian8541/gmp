using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 零售退货
    /// </summary>
    [Table("so_retail_back")]
    public class SoRetailBack
    {
        /// <summary>
        /// 退货单ID
        /// </summary>
        [Key]
        public long retail_back_id { get; set; }

        /// <summary>
        /// 退货日期
        /// </summary>
        public long bill_date { get; set; }

        /// <summary>
        /// 入库仓库
        /// </summary>
        public long stock_id { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public long department_id { get; set; }

        /// <summary>
        /// 组织Id
        /// </summary>
        public long org_id { get; set; }

        /// <summary>
        /// 经手人(业务员)
        /// </summary>
        public long salesman { get; set; }

        /// <summary>
        /// 退货单编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 单据条码或者二维码
        /// </summary>
        public string barcode { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public long curreny { get; set; }

        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal need_pay { get; set; }

        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal real_pay { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public decimal discount_rate { get; set; }

        /// <summary>
        /// 折扣总额
        /// </summary>
        public decimal discount { get; set; }

        /// <summary>
        /// 抹零金额
        /// </summary>
        public decimal erase { get; set; }

        /// <summary>
        /// 会员id
        /// </summary>
        public long member_id { get; set; }

        /// <summary>
        /// 红冲单据表示1，原始单据表示0，非红冲单据表示-1
        /// </summary>
        public int red { get; set; }

        /// <summary>
        /// 状态：0 表示草稿，1 表示提交，2 表示审核，3 表示复核
        /// </summary>
        public long state_id { get; set; }

        /// <summary>
        /// 收款账户
        /// </summary>
        public long accout_id { get; set; }

        /// <summary>
        /// 红冲前原始单据ID，没有则为null
        /// </summary>
        public long red_original { get; set; }

        /// <summary>
        /// 退货前原始单据ID，没有则为null
        /// </summary>
        public long back_original { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string explain { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public long? creater { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public long? created_date { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public long? modifier { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public long? modified_date { get; set; }

        /// <summary>
        /// 过账日期
        /// </summary>
        public long posting_time { get; set; }

        /// <summary>
        /// 过账操作人
        /// </summary>
        public long posted_man { get; set; }

        /// <summary>
        /// 零售原单ID
        /// </summary>
        public long retail_bill_id { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public long shop_id { get; set; }
    }
}
