using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 商品品牌
    /// </summary>
    [Table("base_brand")]
    public class GoodsBrand
    {
        /// <summary>
        /// 商品品牌ID
        /// </summary>
        /// <returns></returns>
        [Key]
        [Column("brand_id")]
        public long BrandId { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        /// <returns></returns>
        [Column("org_id")]
        public long OrgId { get; set; }

        /// <summary>
        /// 商品品牌编码
        /// </summary>
        /// <returns></returns>
        [Column("brand_code")]
        public string BrandCode { get; set; }

        /// <summary>
        /// 商品品牌名称
        /// </summary>
        /// <returns></returns>
        [Column("brand_name")]
        public string BrandName { get; set; }

        /// <summary>
        /// 状态（1：启用；0：停用）
        /// </summary>
        /// <returns></returns>
        [Column("status")]
        public int? Status { get; set; }

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
        /// 品牌LOGO地址
        /// </summary>
        /// <returns></returns>
        [Column("logo_path")]
        public string LogoPath { get; set; }

        /// <summary>
        /// 官网地址
        /// </summary>
        /// <returns></returns>
        [Column("official_website_url")]
        public string OfficialWebsiteUrl { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        /// <returns></returns>
        [Column("profile")]
        public string Profile { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        /// <returns></returns>
        [Column("address")]
        public string Address { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        /// <returns></returns>
        [Column("link_man")]
        public string LinkMan { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        /// <returns></returns>
        [Column("link_tel")]
        public string LinkTel { get; set; }

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
