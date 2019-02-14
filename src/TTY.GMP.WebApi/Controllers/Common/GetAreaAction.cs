using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Web.Common.Response;
using TTY.GMP.IBusiness;

namespace TTY.GMP.WebApi.Controllers.Common
{
    /// <summary>
    /// 获取地区信息
    /// </summary>
    public class GetAreaAction
    {
        /// <summary>
        /// 地区业务访问
        /// </summary>
        private readonly IAreaBLL _areaBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_areaBll"></param>
        public GetAreaAction(IAreaBLL _areaBll)
        {
            this._areaBll = _areaBll;
        }

        /// <summary>
        /// 获取地区信息
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction()
        {
            var areas =await _areaBll.GetArea();
            var areaViews = GetChildren(areas, 0);
            return ResponseBase.Success(areaViews);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="areas"></param>
        /// <param name="parentId"></param>
        private List<GetAreaView> GetChildren(List<Area> areas, long parentId)
        {
            var areaViews = new List<GetAreaView>();
            var myAreas = areas.Where(p => p.ParentId == parentId).ToList();
            foreach (var area in myAreas)
            {
                areaViews.Add(new GetAreaView()
                {
                    AreaId = area.AreaId,
                    AreaName = area.AreaName,
                    ParentId = area.ParentId,
                    Level = area.Level,
                    Children = GetChildren(areas, area.AreaId)
                });
            }
            return areaViews;
        }
    }
}
