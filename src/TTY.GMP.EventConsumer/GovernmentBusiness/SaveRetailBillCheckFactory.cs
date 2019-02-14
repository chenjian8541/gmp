using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.EventConsumer.GovernmentBusiness.GuangDong;

namespace TTY.GMP.EventConsumer.GovernmentBusiness
{
    /// <summary>
    /// 零售下单业务工厂类
    /// </summary>
    public class SaveRetailBillCheckFactory
    {
        /// <summary>
        /// 创建零售下单处理类
        /// </summary>
        /// <param name="provinceName"></param>
        public static ISaveRetailBillCheck CreateExecutor(string provinceName)
        {
            if (string.IsNullOrEmpty(provinceName))
            {
                return null;
            }
            switch (provinceName)
            {
                case "广东省":
                    return new GdSaveRetailBillCheck();
            }
            return null;
        }
    }
}
