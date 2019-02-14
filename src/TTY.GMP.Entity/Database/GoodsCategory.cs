using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 商品类别
    /// </summary>
    [Table("sys_goods_category")]
    public class GoodsCategory
    {
        /// <summary>
        /// 商品类别ID
        /// </summary>
        /// <returns></returns>
        [Key]
        [Column("goods_category_id")]
        public long GoodsCategoryId { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        /// <returns></returns>
        [Column("org_id")]
        public long OrgId { get; set; }

        /// <summary>
        /// 父商品类别ID
        /// </summary>
        /// <returns></returns>
        [Column("parent_id")]
        public long ParentId { get; set; }

        /// <summary>
        /// 商品类别编码
        /// </summary>
        /// <returns></returns>
        [Column("goods_category_code")]
        public string GoodsCategoryCode { get; set; }

        /// <summary>
        /// 商品类别名称
        /// </summary>
        /// <returns></returns>
        [Column("goods_category_name")]
        public string GoodsCategoryName { get; set; }
        /// <summary>
        /// 状态（1：启用；0：停用）
        /// </summary>
        /// <returns></returns>
        [Column("status")]
        public byte? Status { get; set; }

        /// <summary>
        /// 1:表示已经逻辑删除，0：表示为未删除。
        /// </summary>
        /// <returns></returns>
        [Column("is_deleted")]
        public int? IsDeleted { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        /// <returns></returns>
        [Column("comment")]
        public string Comment { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        /// <returns></returns>
        [Column("show_index")]
        public int? ShowIndex { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        /// <returns></returns>
        [Column("creater")]
        public long? Creater { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("created_time")]
        public long? CreatedTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        /// <returns></returns>
        [Column("modifier")]
        public long? Modifier { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [Column("modified_time")]
        public long? ModifiedTime { get; set; }
    }
}
