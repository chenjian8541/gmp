using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Platform.Response
{
    /// <summary>
    /// 平台用户
    /// </summary>
    public class PfUser
    {
        /// <summary>
        /// 账户id
        /// </summary>
        public long user_id { get; set; }

        /// <summary>
        /// 组织id
        /// </summary> 
        public long org_id { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string org_name { get; set; }

        /// <summary>
        /// 雇员id
        /// </summary>
        public long emp_id { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string login_account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string full_name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 最近一次登录时间
        /// </summary>
        public long? last_login_time { get; set; }

        /// <summary>
        /// 状态 1开启 0关闭
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public PfDepartmentModel department_info { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string rolename { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long root_Id { get; set; }

        /// <summary>
        /// 角色IDs
        /// </summary>
        public string role_ids { get; set; }
    }
}
