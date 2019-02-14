using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.View;
using TTY.GMP.Entity.Web.Report.Request;
using TTY.GMP.Entity.Web.Report.Response;
using TTY.GMP.IBusiness;
using TTY.GMP.WebApi.Common;
using TTY.GMP.WebApi.Core;

namespace TTY.GMP.WebApi.Controllers.Report
{
    /// <summary>
    /// 区域农药库存统计
    /// </summary>
    public class GetStatisticsStockAction
    {
        /// <summary>
        /// 地区业务访问
        /// </summary>
        private readonly IAreaBLL _areaBll;

        /// <summary>
        /// 报表业务访问
        /// </summary>
        private readonly IReportBLL _reportBll;

        /// <summary>
        /// 用户登录凭据
        /// </summary>
        private AppTicket _appTicket;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="areaBll"></param>
        /// <param name="reportBll"></param>
        public GetStatisticsStockAction(IAreaBLL areaBll, IReportBLL reportBll)
        {
            this._areaBll = areaBll;
            this._reportBll = reportBll;
        }

        /// <summary>
        /// 区域农药库存统计
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(HttpContext httpContext, GetStatisticsStockRequest request)
        {
            _appTicket = AppTicket.GetAppTicket(httpContext);
            if (request.AreaId == 0 || _appTicket.DataLimitType == (int)DataLimitTypeEnum.Shop)
            {
                return await GetStatisticsStockDefault(request);
            }
            return await GetStatisticsStockLevel(request);
        }

        /// <summary>
        /// 获取默认显示的库存信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponseBase> GetStatisticsStockDefault(GetStatisticsStockRequest request)
        {
            List<DbStatisticsStockView> statisticsStockView = null;
            var limitShops = string.Empty;
            var arealevel = string.Empty;
            switch (_appTicket.DataLimitType)
            {
                case (int)DataLimitTypeEnum.All:
                    var province = await _areaBll.GetProvince();
                    statisticsStockView = await _reportBll.GetStatisticsStock(string.Join(',', province.Select(p => p.AreaId)), AreaLevelEnum.Province);
                    arealevel = AreaLevelEnum.Province;
                    break;
                case (int)DataLimitTypeEnum.Area:
                    if (!string.IsNullOrEmpty(_appTicket.DataLimitArea))
                    {
                        var areaInfo = await ComLib.GetGetAreaStatisticsAreaId(_areaBll, _appTicket.DataLimitArea);
                        statisticsStockView = await _reportBll.GetStatisticsStock(areaInfo.Item1, areaInfo.Item2);
                        arealevel = areaInfo.Item2;
                    }
                    break;
                case (int)DataLimitTypeEnum.Shop:
                    limitShops = _appTicket.DataLimitShop;
                    if (!string.IsNullOrEmpty(_appTicket.DataLimitShop))
                    {
                        statisticsStockView = await _reportBll.GetStatisticsStockByShop(_appTicket.DataLimitShop);
                        arealevel = AreaLevelEnum.Street;
                    }
                    break;
            }
            var result = await GetStatisticsStockView(statisticsStockView, request.Type, limitShops, arealevel);
            return ResponseBase.Success(result);
        }

        /// <summary>
        /// 通过区域级别获取库存信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponseBase> GetStatisticsStockLevel(GetStatisticsStockRequest request)
        {
            if (_appTicket.DataLimitType == (int)DataLimitTypeEnum.Area && _appTicket.DataLimitArea.IndexOf(request.AreaId.ToString()) < 0)
            {
                return new ResponseBase().GetResponseError(StatusCode.DataForbidden, "数据无权访问");
            }
            var newLevel = string.Empty;
            List<Area> areas = null;
            switch (request.Level)
            {
                case AreaLevelEnum.Province:
                    //获取市
                    newLevel = AreaLevelEnum.City;
                    areas = await _areaBll.GetCity(request.AreaId);
                    break;
                case AreaLevelEnum.City:
                    //获取县、区
                    newLevel = AreaLevelEnum.District;
                    areas = await _areaBll.GetDistrict(request.AreaId);
                    break;
                case AreaLevelEnum.District:
                    //获取乡镇
                    newLevel = AreaLevelEnum.Street;
                    areas = await _areaBll.GetStreet(request.AreaId);
                    break;
            }
            if (areas == null || !areas.Any())
            {
                return ResponseBase.Success(GetEmptyData());
            }
            var statisticsRetail = await _reportBll.GetStatisticsStock(string.Join(',', areas.Select(p => p.AreaId)), newLevel);
            var result = await GetStatisticsStockView(statisticsRetail, request.Type, string.Empty, newLevel);
            return ResponseBase.Success(result);
        }

        /// <summary>
        /// 获取空数据
        /// </summary>
        /// <returns></returns>
        private StatisticsStockView GetEmptyData()
        {
            var statisticsStockView = new StatisticsStockView()
            {
                AreaStocks = new List<AreaStockView>(),
                AreaStockSum = new AreaStockSumView()
            };
            return statisticsStockView;
        }

        /// <summary>
        /// 获取StatisticsStockView
        /// </summary>
        /// <param name="dbStatisticsStockView"></param>
        /// <param name="type"></param>
        /// <param name="limitShops"></param>
        /// <param name="areaLevel"></param>
        /// <returns></returns>
        private async Task<StatisticsStockView> GetStatisticsStockView(List<DbStatisticsStockView> dbStatisticsStockView, int type, string limitShops, string areaLevel)
        {
            var statisticsStockView = new StatisticsStockView()
            {
                AreaStocks = new List<AreaStockView>(),
                AreaStockSum = new AreaStockSumView()
            };
            if (dbStatisticsStockView == null || !dbStatisticsStockView.Any())
            {
                return statisticsStockView;
            }
            var areaIds = dbStatisticsStockView.Select(p => p.AreaId).Distinct().ToList();
            var areas = await _areaBll.GetArea(areaIds);
            foreach (var areaId in areaIds)
            {
                var area = areas.FirstOrDefault(p => p.AreaId == areaId);
                var areaRetailView = new AreaStockView()
                {
                    AreaId = areaId,
                    AreaLevel = area?.Level,
                    AreaName = area?.AreaName,
                    Herbicide = GetGoodsCategoryNameWeight(areaId, dbStatisticsStockView, StatisticsCategoryName.Herbicide, type),
                    Fungicide = GetGoodsCategoryNameWeight(areaId, dbStatisticsStockView, StatisticsCategoryName.Fungicide, type),
                    Insecticide = GetGoodsCategoryNameWeight(areaId, dbStatisticsStockView, StatisticsCategoryName.Insecticide, type),
                    Acaricide = GetGoodsCategoryNameWeight(areaId, dbStatisticsStockView, StatisticsCategoryName.Acaricide, type),
                    PlantGrowthRegulator = GetGoodsCategoryNameWeight(areaId, dbStatisticsStockView, StatisticsCategoryName.PlantGrowthRegulator, type),
                    HygienicInsecticide = GetGoodsCategoryNameWeight(areaId, dbStatisticsStockView, StatisticsCategoryName.HygienicInsecticide, type)
                };
                areaRetailView.Sum = dbStatisticsStockView.Where(p => p.AreaId == areaId).Sum(c => type == (int)StatisticsTypeEnum.ContentsWeight
                       ? c.TotalContentsWeight
                       : c.TotalWeight);
                areaRetailView.Other = areaRetailView.Sum - areaRetailView.Herbicide - areaRetailView.Fungicide -
                                       areaRetailView.Insecticide - areaRetailView.Acaricide
                                       - areaRetailView.PlantGrowthRegulator - areaRetailView.HygienicInsecticide;
                statisticsStockView.AreaStocks.Add(areaRetailView);
            }
            statisticsStockView.AreaStockSum = new AreaStockSumView()
            {
                Sum = statisticsStockView.AreaStocks.Sum(p => p.Sum),
                Other = statisticsStockView.AreaStocks.Sum(p => p.Other),
                Acaricide = statisticsStockView.AreaStocks.Sum(p => p.Acaricide),
                Fungicide = statisticsStockView.AreaStocks.Sum(p => p.Fungicide),
                Herbicide = statisticsStockView.AreaStocks.Sum(p => p.Herbicide),
                HygienicInsecticide = statisticsStockView.AreaStocks.Sum(p => p.HygienicInsecticide),
                Insecticide = statisticsStockView.AreaStocks.Sum(p => p.Insecticide),
                PlantGrowthRegulator = statisticsStockView.AreaStocks.Sum(p => p.PlantGrowthRegulator)
            };
            return statisticsStockView;
        }

        /// <summary>
        /// 获取某类别下的重量
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="dbStatisticsStockView"></param>
        /// <param name="categoryName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private decimal GetGoodsCategoryNameWeight(long areaId, List<DbStatisticsStockView> dbStatisticsStockView, string categoryName,
            int type)
        {
            if (dbStatisticsStockView == null || !dbStatisticsStockView.Any())
            {
                return 0;
            }
            List<DbStatisticsStockView> statistics = null;
            switch (categoryName)
            {
                case StatisticsCategoryName.Herbicide:
                    statistics = dbStatisticsStockView.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.Herbicide).ToList();
                    break;
                case StatisticsCategoryName.Fungicide:
                    statistics = dbStatisticsStockView.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.Fungicide).ToList();
                    break;
                case StatisticsCategoryName.Insecticide:
                    statistics = dbStatisticsStockView.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.Insecticide).ToList();
                    break;
                case StatisticsCategoryName.Acaricide:
                    statistics = dbStatisticsStockView.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.Acaricide).ToList();
                    break;
                case StatisticsCategoryName.PlantGrowthRegulator:
                    statistics = dbStatisticsStockView.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.PlantGrowthRegulator).ToList();
                    break;
                case StatisticsCategoryName.HygienicInsecticide:
                    statistics = dbStatisticsStockView.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.HygienicInsecticide).ToList();
                    break;
            }
            if (statistics == null || !statistics.Any())
            {
                return 0;
            }
            if (type == (int)StatisticsTypeEnum.ContentsWeight)
            {
                return statistics.Sum(p => p.TotalContentsWeight);
            }
            return statistics.Sum(p => p.TotalWeight);
        }
    }
}
