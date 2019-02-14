using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.EventConsumer.GovernmentBusiness.GuangDong;

namespace TTY.GMP.EventConsumer.GovernmentBusiness
{
    /// <summary>
    /// 采购退货处理者工厂类
    /// </summary>
    public class SaveReceilBackBillCheckFactory
    {
        /// <summary>
        /// 创建采购退货处理类
        /// </summary>
        /// <param name="provinceName"></param>
        public static ISaveReceilBackBillCheck CreateExecutor(string provinceName)
        {
            if (string.IsNullOrEmpty(provinceName))
            {
                return null;
            }
            switch (provinceName)
            {
                case "广东省":
                    return new GdSaveReceilBackBillCheck();
            }
            return null;
        }
    }
}
