﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.IBusiness
{
    /// <summary>
    /// 销售退货单业务访问
    /// </summary>
    public interface IRetailBackBLL
    {
        /// <summary>
        /// 获取销售退货单
        /// </summary>
        /// <param name="retailBackId"></param>
        /// <returns></returns>
        Task<SoRetailBack> GetSoRetailBack(long retailBackId);

        /// <summary>
        /// 获取销售退货单详情
        /// </summary>
        /// <param name="retailBackId"></param>
        /// <returns></returns>
        Task<List<SoRetailBackDetail>> GetSoRetailBackDetail(long retailBackId);
    }
}
