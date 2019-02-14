using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.Utility;

namespace TTY.GMP.WebApi.Controllers.User
{
    /// <summary>
    /// 保存用户
    /// </summary>
    public class SaveUserAction
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
        /// 构造函数
        /// </summary>
        /// <param name="sysUserBll"></param>
        /// <param name="sysUserRoleBll"></param>
        public SaveUserAction(ISysUserBLL sysUserBll, ISysUserRoleBLL sysUserRoleBll)
        {
            this._sysUserBll = sysUserBll;
            this._sysUserRoleBll = sysUserRoleBll;
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(SaveUserRequest request)
        {
            if (request.UserId > 0)
            {
                return await EditUser(request);
            }
            return await AddUser(request);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponseBase> EditUser(SaveUserRequest request)
        {
            var user = await _sysUserBll.GetSysUser(request.UserId);
            if (user == null)
            {
                return new ResponseBase().GetResponseError(StatusCode.User40001, "用户不存在");
            }
            if (await _sysUserBll.ExistSysUserByAccount(request.Account, user.UserId))
            {
                return new ResponseBase().GetResponseError(StatusCode.User40002, "用户帐号已存在");
            }
            if (user.UserRoleId != request.UserRoleId)
            {
                var userRole = _sysUserRoleBll.GetSysUserRole(request.UserRoleId);
                if (userRole == null)
                {
                    return new ResponseBase().GetResponseError(StatusCode.UserRole30001, "角色不存在");
                }
            }
            user.NickName = request.NickName;
            user.Account = request.Account;
            user.UserRoleId = request.UserRoleId;
            user.StatusFlag = request.StatusFlag;
            await _sysUserBll.UpdateUser(user);
            return ResponseBase.Success();
        }

        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponseBase> AddUser(SaveUserRequest request)
        {
            if (await _sysUserBll.ExistSysUserByAccount(request.Account))
            {
                return new ResponseBase().GetResponseError(StatusCode.User40002, "用户帐号已存在");
            }
            var userRole = _sysUserRoleBll.GetSysUserRole(request.UserRoleId);
            if (userRole == null)
            {
                return new ResponseBase().GetResponseError(StatusCode.UserRole30001, "角色不存在");
            }
            var user = new SysUser()
            {
                NickName = request.NickName,
                AuthorityValue = string.Empty,
                Account = request.Account,
                UserRoleId = request.UserRoleId,
                DataFlag = (int)DataFlagEnum.Normal,
                DataLimitArea = string.Empty,
                DataLimitShop = string.Empty,
                LastLoginTime = null,
                StatusFlag = request.StatusFlag,
                Pwd = CryptogramHelper.Encrypt3DES(request.Password),
                DataLimitType = (int)DataLimitTypeEnum.Area
            };
            await _sysUserBll.AddUser(user);
            return ResponseBase.Success();
        }
    }
}
