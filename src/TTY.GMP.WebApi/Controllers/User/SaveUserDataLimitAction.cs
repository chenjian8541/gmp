using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.IBusiness;
using System.Text;

namespace TTY.GMP.WebApi.Controllers.User
{
    /// <summary>
    /// 设置用户数据权限
    /// </summary>
    public class SaveUserDataLimitAction
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        private readonly ISysUserBLL _sysUserBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserBll"></param>
        public SaveUserDataLimitAction(ISysUserBLL sysUserBll)
        {
            this._sysUserBll = sysUserBll;
        }

        /// <summary>
        /// 设置用户数据权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(SaveUserDataLimitRequest request)
        {
            var newDataLimitArea = string.Empty;
            var newDataLimitShop = string.Empty;
            switch (request.Type)
            {
                case (int)DataLimitTypeEnum.All:
                    break;
                case (int)DataLimitTypeEnum.Area:
                    newDataLimitArea = GetDataLimitArea(request);
                    break;
                case (int)DataLimitTypeEnum.Shop:
                    newDataLimitShop = GetDataLimitShop(request);
                    break;
            }
            await _sysUserBll.SetDataLimit(request.UserId, request.Type, newDataLimitArea, newDataLimitShop);
            return ResponseBase.Success();
        }

        /// <summary>
        /// 获取地区限制设置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GetDataLimitArea(SaveUserDataLimitRequest request)
        {
            var newDataLimitArea = string.Empty;
            if (request.Areas != null && request.Areas.Any())
            {
                var areas = new StringBuilder();
                request.Areas.ForEach(p =>
                {
                    areas.AppendFormat("{0}|{1},", p.Id, p.Level);
                });
                newDataLimitArea = areas.ToString().TrimEnd(',');
            }
            return newDataLimitArea;
        }

        /// <summary>
        /// 获取门店限制设置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GetDataLimitShop(SaveUserDataLimitRequest request)
        {
            var newDataLimitShop = string.Empty;
            if (request.Shops != null && request.Shops.Any())
            {
                var shops = new StringBuilder();
                request.Shops.ForEach(p =>
                {
                    shops.AppendFormat("{0},", p.Id);
                });
                newDataLimitShop = shops.ToString().TrimEnd(',');
            }
            return newDataLimitShop;
        }
    }
}
