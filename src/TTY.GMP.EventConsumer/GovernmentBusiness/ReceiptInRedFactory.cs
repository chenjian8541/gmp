using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.EventConsumer.GovernmentBusiness.GuangDong;

namespace TTY.GMP.EventConsumer.GovernmentBusiness
{
    /// <summary>
    /// 采购撤销处理者工厂类
    /// </summary>
    public class ReceiptInRedFactory
    {
        /// <summary>
        /// 创建采购撤销处理者
        /// </summary>
        /// <param name="provinceName"></param>
        public static IErpReceiptInRed CreateExecutor(string provinceName)
        {
            if (string.IsNullOrEmpty(provinceName))
            {
                return null;
            }
            switch (provinceName)
            {
                case "广东省":
                    return new GdErpReceiptInRed();
            }
            return null;
        }
    }
}
