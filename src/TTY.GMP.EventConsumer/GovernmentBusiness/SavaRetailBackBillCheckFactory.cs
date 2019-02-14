using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.EventConsumer.GovernmentBusiness.GuangDong;

namespace TTY.GMP.EventConsumer.GovernmentBusiness
{
    /// <summary>
    /// 零售退货处理者工厂类
    /// </summary>
    public class SavaRetailBackBillCheckFactory
    {
        /// <summary>
        /// 创建零售退货处理类
        /// </summary>
        /// <param name="provinceName"></param>
        public static ISavaRetailBackBillCheck CreateExecutor(string provinceName)
        {
            if (string.IsNullOrEmpty(provinceName))
            {
                return null;
            }
            switch (provinceName)
            {
                case "广东省":
                    return new GdSavaRetailBackBillCheck();
            }
            return null;
        }
    }
}
