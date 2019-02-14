using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.Entity.Web.User.Response;
using TTY.GMP.IBusiness;
using TTY.GMP.Utility;

namespace TTY.GMP.WebApi.Controllers.User
{
    /// <summary>
    /// 获取用户数据权限信息
    /// </summary>
    public class GetUserDataLimitAction
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        private readonly ISysUserBLL _sysUserBll;

        /// <summary>
        /// 地区业务访问
        /// </summary>
        private readonly IAreaBLL _areaBll;

        /// <summary>
        /// 店面业务逻辑
        /// </summary>
        private readonly IShopBLL _shopBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserBll"></param>
        /// <param name="areaBll"></param>
        /// <param name="shopBll"></param>
        public GetUserDataLimitAction(ISysUserBLL sysUserBll, IAreaBLL areaBll, IShopBLL shopBll)
        {
            this._sysUserBll = sysUserBll;
            this._areaBll = areaBll;
            this._shopBll = shopBll;
        }

        /// <summary>
        /// 获取用户数据权限信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(GetUserDataLimitRequest request)
        {
            var user = await _sysUserBll.GetSysUser(request.UserId);
            if (user == null)
            {
                return new ResponseBase(StatusCode.User40001, "用户不存在");
            }
            switch (user.DataLimitType)
            {
                case (int)DataLimitTypeEnum.All:
                    return await GetUserDataLimitAll();
                case (int)DataLimitTypeEnum.Area:
                    return await GetUserDataLimitArea(user.DataLimitArea); ;
                case (int)DataLimitTypeEnum.Shop:
                    return await GetUserDataLimitShop(user.DataLimitShop);
            }
            return new ResponseBase().GetResponseError(StatusCode.User40003, "用户数据权限类型错误");
        }

        /// <summary>
        /// 获取用户数据权限（所有）
        /// </summary>
        /// <returns></returns>
        private async Task<ResponseBase> GetUserDataLimitAll()
        {
            return ResponseBase.Success(new UserDataLimitView((int)DataLimitTypeEnum.All, await GetAreaView(), new List<DataLimitShopView>()));
        }

        /// <summary>
        /// 获取用户数据权限（门店）
        /// </summary>
        /// <param name="dataLimitShop"></param>
        /// <returns></returns>
        private async Task<ResponseBase> GetUserDataLimitShop(string dataLimitShop)
        {
            var shops = await _shopBll.GetShopByIds(dataLimitShop);
            var shopViews = shops.Select(p => new DataLimitShopView()
            {
                Id = p.ShopId,
                Name = p.ShopName
            }).ToList();
            return ResponseBase.Success(new UserDataLimitView((int)DataLimitTypeEnum.Shop, await GetAreaView(), shopViews));
        }

        /// <summary>
        /// 获取用户数据权限（地区）
        /// </summary>
        /// <param name="dataLimitArea"></param>
        /// <returns></returns>
        private async Task<ResponseBase> GetUserDataLimitArea(string dataLimitArea)
        {
            var limitAreas = dataLimitArea.Split(',');
            var limitAreaIds = new List<long>();
            foreach (var limitArea in limitAreas)
            {
                if (string.IsNullOrEmpty(limitArea))
                {
                    continue;
                }
                limitAreaIds.Add(limitArea.Split('|')[0].ToLong());
            }
            return ResponseBase.Success(new UserDataLimitView((int)DataLimitTypeEnum.Area, await GetAreaView(), new List<DataLimitShopView>())
            {
                MyAreas = limitAreaIds
            });
        }

        /// <summary>
        /// 获取地区
        /// </summary>
        /// <returns></returns>
        private async Task<List<DataLimitAreaView>> GetAreaView()
        {
            var areas = await _areaBll.GetArea();
            return GetChildren(areas, 0);
        }

        /// <summary>
        /// 获取地区数据限制信息
        /// </summary>
        /// <param name="areas"></param>
        /// <param name="parentId"></param>
        private List<DataLimitAreaView> GetChildren(List<Area> areas, long parentId)
        {
            var areaViews = new List<DataLimitAreaView>();
            var myAreas = areas.Where(p => p.ParentId == parentId).ToList();
            foreach (var area in myAreas)
            {
                areaViews.Add(new DataLimitAreaView()
                {
                    Id = area.AreaId,
                    Level = area.Level,
                    Name = area.AreaName,
                    Children = GetChildren(areas, area.AreaId)
                });
            }
            return areaViews;
        }
    }
}
