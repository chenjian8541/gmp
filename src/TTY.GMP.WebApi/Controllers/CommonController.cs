using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.View;
using TTY.GMP.Entity.Web.Common.Request;
using TTY.GMP.Entity.Web.Common.Response;
using TTY.GMP.IBusiness;
using TTY.GMP.LOG;
using TTY.GMP.WebApi.Attribute;
using TTY.GMP.WebApi.Common;
using TTY.GMP.WebApi.Controllers.Common;

namespace TTY.GMP.WebApi.Controllers
{
    /// <summary>
    /// 公共数据访问控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class CommonController : Controller
    {
        /// <summary>
        /// 地区业务访问
        /// </summary>
        private readonly IAreaBLL _areaBll;

        /// <summary>
        /// 系统数据访问
        /// </summary>
        private readonly ISystemBLL _systemBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="areaBll"></param>
        /// <param name="systemBll"></param>
        public CommonController(IAreaBLL areaBll, ISystemBLL systemBll)
        {
            this._areaBll = areaBll;
            this._systemBll = systemBll;
        }

        /// <summary>
        /// 获取地区信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseBase> GetArea()
        {
            try
            {
                var action = new GetAreaAction(_areaBll);
                return await action.ProcessAction();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 获取省
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseBase> GetProvince()
        {
            try
            {

                var areas = await _areaBll.GetProvince();
                return ResponseBase.Success(areas.Select(p =>
                    new GetAreaView()
                    {
                        AreaId = p.AreaId,
                        AreaName = p.AreaName,
                        ParentId = p.ParentId,
                        Level = p.Level
                    }
                ));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 获取城市
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseBase> GetCity([FromBody] GetCityRequest request)
        {
            try
            {

                var areas = await _areaBll.GetCity(request.Province);
                return ResponseBase.Success(areas.Select(p =>
                    new GetAreaView()
                    {
                        AreaId = p.AreaId,
                        AreaName = p.AreaName,
                        ParentId = p.ParentId,
                        Level = p.Level
                    }
                ));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 获取地区
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseBase> GetDistrict([FromBody]GetDistrictRequest request)
        {
            try
            {
                var areas = await _areaBll.GetDistrict(request.City);
                return ResponseBase.Success(areas.Select(p =>
                    new GetAreaView()
                    {
                        AreaId = p.AreaId,
                        AreaName = p.AreaName,
                        ParentId = p.ParentId,
                        Level = p.Level
                    }
                ));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 获取系统统计信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseBase> GetSystemStatistics()
        {
            try
            {
                var request = new GetSystemStatisticsRequest();
                if (!ComLib.HandleRequest(HttpContext, request))
                {
                    return ResponseBase.Success(new SystemStatisticsView());
                }
                var data = await _systemBll.GetSystemStatistics(request);
                return ResponseBase.Success(data);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }
    }
}