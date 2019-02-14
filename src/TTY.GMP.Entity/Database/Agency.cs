using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 经销商信息
    /// </summary>
    [Table("agency")]
    public class Agency
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Key]
        public long ShopId { get; set; }

        /// <summary>
        /// 经销商ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 经销商编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 经销商名称
        /// </summary>
        public string Name { get; set; }
    }
}
