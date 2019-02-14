using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    ///  门店销售排名限制
    /// </summary>
    [Table("shopretailranklimit")]
    public class ShopRetailRankLimit
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Key]
        public long ShopId { get; set; }

        /// <summary>
        /// 类型  <see cref="TTY.GMP.Entity.Enum.ShopRetailRankLimitTypeEnum"/>
        /// </summary>
        public int Type { get; set; }
    }
}
