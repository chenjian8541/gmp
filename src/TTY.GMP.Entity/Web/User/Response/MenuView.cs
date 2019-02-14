using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Web.User.Response
{
    /// <summary>
    /// 登录者的菜单权限配置信息
    /// </summary>
    public class MenuView
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 约定编码
        /// </summary>
        public string PerCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单类型
        /// <see cref="TTY.GMP.Entity.Enum.MenuEnum"/>
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 是否拥有此菜单权限
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// 父Id (默认0)
        /// </summary>
        public int FatherId { get; set; }
    }
}
