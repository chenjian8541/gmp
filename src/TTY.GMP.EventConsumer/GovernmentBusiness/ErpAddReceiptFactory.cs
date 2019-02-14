using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.EventConsumer.GovernmentBusiness.GuangDong;

namespace TTY.GMP.EventConsumer.GovernmentBusiness
{
    /// <summary>
    /// 采购入库业务工厂类
    /// </summary>
    public class ErpAddReceiptFactory
    {
        /// <summary>
        /// 创建采购入库处理类
        /// </summary>
        /// <param name="provinceName"></param>
        public static IErpAddReceipt CreateExecutor(string provinceName)
        {
            if (string.IsNullOrEmpty(provinceName))
            {
                return null;
            }
            switch (provinceName)
            {
                case "广东省":
                    return new GdErpAddReceipt();
            }
            return null;
        }
    }
}
