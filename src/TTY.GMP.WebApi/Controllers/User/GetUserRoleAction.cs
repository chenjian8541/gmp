using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.Entity.Web.User.Response;
using TTY.GMP.IBusiness;
using TTY.GMP.WebApi.Common;
using TTY.GMP.Utility;
using TTY.GMP.Authority;

namespace TTY.GMP.WebApi.Controllers.User
{
    /// <summary>
    /// 获取角色信息
    /// </summary>
    public class GetUserRoleAction
    {
        /// <summary>
        /// 用户角色业务访问
        /// </summary>
        private readonly ISysUserRoleBLL _sysUserRoleBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserRoleBll"></param>
        public GetUserRoleAction(ISysUserRoleBLL sysUserRoleBll)
        {
            this._sysUserRoleBll = sysUserRoleBll;
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(GetUserRoleRequest request)
        {
            if (request.UserRoleId.Equals(0))
            {
                return GetAnNewRole();
            }
            return await GetRoleView(request.UserRoleId);
        }

        /// <summary>
        /// 获取一个新角色信息
        /// </summary>
        /// <returns></returns>
        private ResponseBase GetAnNewRole()
        {
            var menu = MenuLib.MenuConfigs.Select(p => new MenuView()
            {
                Id = p.Id,
                IsOwner = false,
                Name = p.Name,
                PerCode = p.PerCode,
                Type = p.Type,
                FatherId = p.FatherId
            }).ToList();
            var roleView = new GetUserRoleView()
            {
                Name = string.Empty,
                Menus = ConvertToRoleMenu(menu)
            };
            return ResponseBase.Success(roleView);
        }

        /// <summary>
        /// 通过角色ID获取角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        private async Task<ResponseBase> GetRoleView(long roleId)
        {
            var role = await _sysUserRoleBll.GetSysUserRole(roleId);
            if (role == null)
            {
                return new ResponseBase().GetResponseError(StatusCode.UserRole30001, "角色不存在");
            }
            var authorityCore = new AuthorityCore(role.AuthorityValue.ToBigInteger());
            var menu = MenuLib.MenuConfigs.Select(p => new MenuView()
            {
                Id = p.Id,
                Name = p.Name,
                PerCode = p.PerCode,
                Type = p.Type,
                IsOwner = authorityCore.Validation(p.Id),
                FatherId = p.FatherId
            }).ToList();
            var roleView = new GetUserRoleView()
            {
                Name = role.Name,
                Menus = ConvertToRoleMenu(menu),
                MyMenus = menu.Where(p => p.IsOwner).Select(p => p.Id).ToList()
            };
            return ResponseBase.Success(roleView);
        }

        /// <summary>
        /// 转换成RoleMenu
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="fatherId"></param>
        /// <returns></returns>
        private List<RoleMenu> ConvertToRoleMenu(List<MenuView> menus, int fatherId = 0)
        {
            var roleMenus = new List<RoleMenu>();
            if (!menus.Any())
            {
                return roleMenus;
            }
            var myMenus = menus.Where(p => p.FatherId == fatherId);
            foreach (var menu in myMenus)
            {
                var roleMenu = new RoleMenu()
                {
                    Id = menu.Id,
                    IsOwner = menu.IsOwner,
                    Name = menu.Name,
                    Type = menu.Type
                };
                roleMenu.Children = ConvertToRoleMenu(menus, menu.Id);
                roleMenus.Add(roleMenu);
            }
            return roleMenus;
        }
    }
}
