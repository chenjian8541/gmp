using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Platform.Response
{
    /// <summary>
    /// 平台部门
    /// </summary>
    public class PfDepartmentModel
    {
        public long dept_id { get; set; }

        public long org_id { get; set; }

        public string dept_pid { get; set; }

        public string dept_code { get; set; }

        public string dept_name { get; set; }

        public string dept_short_name { get; set; }

        public int status { get; set; }

        public string emp_id { get; set; }

        public int tree_type { get; set; }

        public List<PfDepartmentModel> children { get; set; }
    }
}
