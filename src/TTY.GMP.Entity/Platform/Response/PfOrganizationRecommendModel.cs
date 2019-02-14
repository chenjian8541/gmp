using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Platform.Response
{
    /// <summary>
    /// 机构推荐人信息
    /// </summary>
    public class PfOrganizationRecommendModel
    {
        /// <summary>
        /// 工号或者手机号
        /// </summary>
        public string reference_code { get; set; }

        /// <summary>
        /// 推荐人名称
        /// </summary>
        public string reference_name { get; set; }
    }
}
