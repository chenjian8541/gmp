using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.Http;
using TTY.GMP.IBusiness;
using TTY.GMP.LOG;
using TTY.GMP.WebApi.Attribute;
using TTY.GMP.WebApi.Controllers.User;
using TTY.GMP.WebApi.Core;

namespace TTY.GMP.WebApi.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class UserController : Controller
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        private readonly ISysUserBLL _sysUserBll;

        /// <summary>
        /// 用户角色业务访问
        /// </summary>
        private readonly ISysUserRoleBLL _sysUserRoleBll;

        /// <summary>
        /// 地区业务访问
        /// </summary>
        private readonly IAreaBLL _areaBll;

        /// <summary>
        /// 店面业务逻辑
        /// </summary>
        private readonly IShopBLL _shopBll;

        /// <summary>
        /// 用户日志操作
        /// </summary>
        private readonly ISysUserLogBLL _sysUserLogBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserBll"></param>
        /// <param name="sysUserRoleBll"></param>
        /// <param name="areaBll"></param>
        /// <param name="shopBll"></param>
        /// <param name="sysUserLogBll"></param>
        public UserController(ISysUserBLL sysUserBll, ISysUserRoleBLL sysUserRoleBll, IAreaBLL areaBll, IShopBLL shopBll, ISysUserLogBLL sysUserLogBll)
        {
            this._sysUserBll = sysUserBll;
            this._sysUserRoleBll = sysUserRoleBll;
            this._areaBll = areaBll;
            this._shopBll = shopBll;
            this._sysUserLogBll = sysUserLogBll;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseBase> Login([FromBody]LoginRequest request)
        {
            try
            {
                var action = new LoginAction(_sysUserBll, _sysUserRoleBll, _sysUserLogBll, _areaBll);
                return await action.ProcessAction(HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 获取登录者的菜单权限配置信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase GetLoginMenu()
        {
            try
            {
                var action = new GetLoginMenuAction(_sysUserRoleBll);
                return action.ProcessAction(HttpContext);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.RoleMgr, "角色管理")]
        public async Task<ResponseBase> GetUserRoleList([FromBody]GetUserRoleListRequest request)
        {
            try
            {
                var action = new GetUserRoleListAction(_sysUserRoleBll);
                return await action.ProcessAction(request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.RoleGet, "查看角色")]
        public async Task<ResponseBase> GetUserRole([FromBody]GetUserRoleRequest request)
        {
            try
            {
                var action = new GetUserRoleAction(_sysUserRoleBll);
                return await action.ProcessAction(request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 保存用户角色信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.RoleEdit, "编辑角色")]
        public async Task<ResponseBase> SaveUserRole([FromBody]SaveUserRoleRequest request)
        {
            try
            {
                var action = new SaveUserRoleAction(_sysUserRoleBll);
                return await action.ProcessAction(request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.RoleDel, "删除角色")]
        public async Task<ResponseBase> DelUserRole([FromBody] DelUserRoleRequest request)
        {
            try
            {
                var action = new DelUserRoleAction(_sysUserRoleBll);
                return await action.ProcessAction(request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.UserEdit, "编辑用户")]
        public async Task<ResponseBase> SaveUser([FromBody]SaveUserRequest request)
        {
            try
            {
                var action = new SaveUserAction(_sysUserBll, _sysUserRoleBll);
                return await action.ProcessAction(request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.UserResetPassword, "重置用户密码")]
        public async Task<ResponseBase> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                var action = new ResetPasswordAction(_sysUserBll);
                return await action.ProcessAction(request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.UserChangePassword, "修改用户密码")]
        public async Task<ResponseBase> ChangePassword([FromBody]ChangePasswordRequest request)
        {
            try
            {
                var action = new ChangePasswordAction(_sysUserBll);
                return await action.ProcessAction(request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 改变用户状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.UserChangeStatusFlag, "修改用户状态")]
        public async Task<ResponseBase> ChangeStatusFlag([FromBody] ChangeStatusFlagRequest request)
        {
            try
            {
                var action = new ChangeStatusFlagAction(_sysUserBll);
                return await action.ProcessAction(request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 设置用户数据权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.UserDataLimitSave, "设置数据权限")]
        public async Task<ResponseBase> SaveUserDataLimit([FromBody]SaveUserDataLimitRequest request)
        {
            try
            {
                var action = new SaveUserDataLimitAction(_sysUserBll);
                return await action.ProcessAction(request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 获取用户数据权限信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.UserDataLimitGet, "查看用户数据权限")]
        public async Task<ResponseBase> GetUserDataLimit([FromBody] GetUserDataLimitRequest request)
        {
            try
            {
                var action = new GetUserDataLimitAction(_sysUserBll, _areaBll, _shopBll);
                return await action.ProcessAction(request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 通过ID获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.UserGet, "查看用户")]
        public async Task<ResponseBase> GetUser([FromBody]GetUserRequest request)
        {
            try
            {
                var action = new GetUserAction(_sysUserBll);
                return await action.ProcessAction(request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.UserMgr, "用户管理")]
        public async Task<ResponsePagingBase> GetUserPage([FromBody]GetUserPageRequest request)
        {
            try
            {
                var action = new GetUserPageAction(_sysUserBll, _sysUserRoleBll);
                return await action.ProcessAction(request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }
    }
}