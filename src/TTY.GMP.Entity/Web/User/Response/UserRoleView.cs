using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.User.Response
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class UserRoleView
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rowNumber"></param>
        /// <param name="userRoleId"></param>
        /// <param name="name"></param>
        public UserRoleView(int rowNumber, long userRoleId, string name)
        {
            this.RowNumber = rowNumber;
            this.UserRoleId = userRoleId;
            this.Name = name;
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int RowNumber { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long UserRoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
    }
}
