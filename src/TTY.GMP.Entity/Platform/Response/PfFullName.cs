using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Platform.Response
{
    /// <summary>
    /// 平台管理员名称
    /// </summary>
    public class PfFullName
    {
        /// <summary>
        /// ID
        /// </summary>
        public long org_id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string org_legal_person { get; set; }
    }
}
