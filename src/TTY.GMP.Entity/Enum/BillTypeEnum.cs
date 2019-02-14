using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Enum
{
    /// <summary>
    /// 订单类型
    /// </summary>
    public enum BillTypeEnum
    {
        /// <summary>
        /// 充值
        /// </summary>
        Recharge = 1,

        /// <summary>
        /// 采购入库
        /// </summary>
        ReceiptIn = 555,

        /// <summary>
        /// 其它入库
        /// </summary>
        ReceiptInOther = 556,

        /// <summary>
        /// 其它出库
        /// </summary>
        ReceiptOutOther = 557,

        /// <summary>
        /// 零售单
        /// </summary>
        Retail = 666,

        /// <summary>
        /// 零售退货单
        /// </summary>
        RetailBack = 667,

        /// <summary>
        /// 采购退货
        /// </summary>
        ReceiptBack = 888,

        /// <summary>
        /// 采购付款
        /// </summary>
        ReceiptPay = 444,

        /// <summary>
        /// 采购收款
        /// </summary>
        ReceiptCollection = 445,

        /// <summary>
        /// 积分兑换单
        /// </summary>
        IntegralExchange = 777,
        /// <summary>
        /// 积分调整单
        /// </summary>
        IntegralChange = 778,

        /// <summary>
        /// 充值撤销单
        /// </summary>
        RechargeBack = 998,

        /// <summary>
        /// 会员还款
        /// </summary>
        MemberRepayment = 999,
    }
}
