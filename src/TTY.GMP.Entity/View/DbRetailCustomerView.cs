using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 客户信息
    /// </summary>
    public class DbRetailCustomerView
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public long RetailId { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string RetailCustomerName { get; set; }

        /// <summary>
        /// 客户联系号码
        /// </summary>
        public string RetailCustomerTel { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string Identification { get; set; }
    }
}
