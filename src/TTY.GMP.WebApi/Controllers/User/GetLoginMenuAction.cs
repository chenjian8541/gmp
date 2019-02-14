using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Authority;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.Entity.Web.User.Response;
using TTY.GMP.IBusiness;
using TTY.GMP.WebApi.Common;
using TTY.GMP.WebApi.Core;

namespace TTY.GMP.WebApi.Controllers.User
{
    /// <summary>
    /// 获取登录者的菜单权限配置信息
    /// </summary>
    public class GetLoginMenuAction
    {
        /// <summary>
        /// 用户角色业务访问
        /// </summary>
        private readonly ISysUserRoleBLL _sysUserRoleBll;

        /// <summary>
        /// 获取登录者的菜单权限配置信息
        /// </summary>
        /// <param name="sysUserRoleBll"></param>
        public GetLoginMenuAction(ISysUserRoleBLL sysUserRoleBll)
        {
            this._sysUserRoleBll = sysUserRoleBll;
        }

        /// <summary>
        /// 获取登录者菜单
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public ResponseBase ProcessAction(HttpContext httpContext)
        {
            var ticket = AppTicket.GetAppTicket(httpContext);
            var authorityCore = new AuthorityCore(ticket.WeightSum);
            HandleShowAction(authorityCore);
            InitFatherMenu(authorityCore);
            var menus = MenuLib.MenuConfigs.Where(p => authorityCore.Validation(p.Id)
            )
            .Select(p => new MenuView()
            {
                Id = p.Id,
                IsOwner = true,
                Name = p.Name,
                PerCode = p.PerCode,
                Type = p.Type
            }).ToList();
            return ResponseBase.Success(menus);
        }

        /// <summary>
        /// 处理“查看”动作权限
        /// 必须拥有页面“查看”动作权限，该页面其它子权限才会生效
        /// </summary>
        /// <param name="authorityCore"></param>
        private void HandleShowAction(AuthorityCore authorityCore)
        {
            var showActionMenus = MenuLib.MenuConfigs.Where(p => p.Type == (int)MenuEnum.ShowAction).ToList();
            foreach (var showActionMenu in showActionMenus)
            {
                if (!authorityCore.Validation(showActionMenu.Id))
                {
                    RemoveChildrenMenu(authorityCore, showActionMenu.FatherId);
                }
            }
        }

        /// <summary>
        /// 移除所有子权限
        /// </summary>
        /// <param name="authorityCore"></param>
        /// <param name="fatherId"></param>
        private void RemoveChildrenMenu(AuthorityCore authorityCore, int fatherId)
        {
            var menus = MenuLib.MenuConfigs.Where(p => p.FatherId == fatherId).ToList();
            foreach (var menu in menus)
            {
                authorityCore.WeakenAuthority(menu.Id);
                RemoveChildrenMenu(authorityCore, menu.Id);
            }
        }

        /// <summary>
        /// 添加父权限
        /// </summary>
        /// <param name="authorityCore"></param>
        private void InitFatherMenu(AuthorityCore authorityCore)
        {
            foreach (var menu in MenuLib.MenuConfigs)
            {
                if (authorityCore.Validation(menu.Id))
                {
                    AddFatherMenu(authorityCore, menu.FatherId);
                }
            }
        }

        /// <summary>
        /// 增加父ID
        /// </summary>
        /// <param name="authorityCore"></param>
        /// <param name="fatherId"></param>
        private void AddFatherMenu(AuthorityCore authorityCore, int fatherId)
        {
            if (fatherId == 0)
            {
                return;
            }
            var father = MenuLib.MenuConfigs.FirstOrDefault(p => p.Id == fatherId);
            if (father != null)
            {
                authorityCore.RegisterAuthority(fatherId);
                AddFatherMenu(authorityCore, father.FatherId);
            }
        }
    }
}
