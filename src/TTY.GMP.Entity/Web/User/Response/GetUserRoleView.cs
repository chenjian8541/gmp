using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Web.User.Response
{
    /// <summary>
    /// 获取角色信息返回内容
    /// </summary>
    public class GetUserRoleView
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///拥有的权限
        /// </summary>
        public List<int> MyMenus { get; set; }

        /// <summary>
        /// 菜单权限
        /// </summary>
        public List<RoleMenu> Menus { get; set; }
    }

    /// <summary>
    /// 角色菜单
    /// </summary>
    public class RoleMenu
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int Id { get; set; }

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
        /// 子菜单
        /// </summary>
        public List<RoleMenu> Children { get; set; }
    }
}
