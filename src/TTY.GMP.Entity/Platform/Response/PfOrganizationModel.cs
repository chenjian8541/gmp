using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Platform.Response
{
    /// <summary>
    /// 机构信息
    /// </summary>
    public class PfOrganizationModel
    {
        /// <summary>
        /// 组织ID
        /// </summary>
        public long org_id { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string org_name { get; set; }

        /// <summary>
        /// 上级组织ID
        /// </summary>
        public long parent_id { get; set; }

        /// <summary>
        /// 组织简称
        /// </summary>
        public string org_short_name { get; set; }

        /// <summary>
        /// 组织法人
        /// </summary>
        public string org_legal_person { get; set; }

        /// <summary>
        /// 组织类型ID
        /// </summary>
        public string org_type_id { get; set; }

        /// <summary>
        /// 组织编码
        /// </summary>
        public string org_code { get; set; }

        /// <summary>
        /// 组织代码
        /// </summary>
        public string org_number { get; set; }

        /// <summary>
        /// 组织拼音码或者助记码
        /// </summary>
        public string org_py_code { get; set; }

        /// <summary>
        /// 是否启用辅助核算：1：表示启用，0：表示不启用
        /// </summary>
        public string org_if_auxiliary { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string org_tel { get; set; }

        /// <summary>
        /// 组织传真
        /// </summary>
        public string org_fax { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string org_address { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string org_postcode { get; set; }

        /// <summary>
        /// 税务登记码
        /// </summary>
        public string org_tax_no { get; set; }

        /// <summary>
        /// 状态：  1. 启用  0：停用
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 是否逻辑删除：1:表示已经逻辑删除，0：表示逻辑删除
        /// </summary>
        public int is_deleted { get; set; }
    }
}
