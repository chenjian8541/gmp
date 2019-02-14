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
    /// 区域门店库存看板
    /// </summary>
    public class GetStatisticsStockShopPageAction
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
        public GetStatisticsStockShopPageAction(IAreaBLL areaBll, IReportBLL reportBll)
        {
            this._areaBll = areaBll;
            this._reportBll = reportBll;
        }

        /// <summary>
        /// 区域门店库存看板
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponsePagingBase> ProcessAction(HttpContext httpContext, GetStatisticsStockShopPageRequest request)
        {
            _appTicket = AppTicket.GetAppTicket(httpContext);
            if (request.AreaId == 0 || _appTicket.DataLimitType == (int)DataLimitTypeEnum.Shop)
            {
                return await GetStatisticsStockShopDefault(request);
            }
            return await GetStatisticsStockShopLevel(request);
        }

        /// <summary>
        /// 获取默认显示的库存信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponsePagingBase> GetStatisticsStockShopDefault(GetStatisticsStockShopPageRequest request)
        {
            Tuple<IEnumerable<DbStatisticsStockViewShop>, int> statisticsStockShopView = null;
            switch (_appTicket.DataLimitType)
            {
                case (int)DataLimitTypeEnum.All:
                    var province = await _areaBll.GetProvince();
                    statisticsStockShopView = await _reportBll.GetStatisticsStockShop(request, string.Join(',', province.Select(p => p.AreaId)), AreaLevelEnum.Province);
                    break;
                case (int)DataLimitTypeEnum.Area:
                    if (!string.IsNullOrEmpty(_appTicket.DataLimitArea))
                    {
                        var areaInfo = await ComLib.GetGetAreaStatisticsAreaId(_areaBll, _appTicket.DataLimitArea);
                        statisticsStockShopView = await _reportBll.GetStatisticsStockShop(request, areaInfo.Item1, areaInfo.Item2);
                    }
                    break;
                case (int)DataLimitTypeEnum.Shop:
                    if (!string.IsNullOrEmpty(_appTicket.DataLimitShop))
                    {
                        statisticsStockShopView = await _reportBll.GetStatisticsStockShopByShop(request, _appTicket.DataLimitShop);
                    }
                    break;
            }
            var result = await GetStatisticsStockShopView(statisticsStockShopView, request.Type);
            var recordCount = statisticsStockShopView == null ? 0 : statisticsStockShopView.Item2;
            return ResponsePagingBase.Success(result, recordCount);
        }

        /// <summary>
        /// 通过区域级别获取库存信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponsePagingBase> GetStatisticsStockShopLevel(GetStatisticsStockShopPageRequest request)
        {
            if (_appTicket.DataLimitType == (int)DataLimitTypeEnum.Area && _appTicket.DataLimitArea.IndexOf(request.AreaId.ToString()) < 0)
            {
                return new ResponsePagingBase().GetResponseError(StatusCode.DataForbidden, "数据无权访问");
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
                return ResponsePagingBase.Success(GetEmptyData(), 0);
            }
            var statisticsRetail = await _reportBll.GetStatisticsStockShop(request, string.Join(',', areas.Select(p => p.AreaId)), newLevel);
            var result = await GetStatisticsStockShopView(statisticsRetail, request.Type);
            var recordCount = statisticsRetail == null ? 0 : statisticsRetail.Item2;
            return ResponsePagingBase.Success(result, recordCount);
        }

        /// <summary>
        /// 获取空数据
        /// </summary>
        /// <returns></returns>
        private StatisticsStockShopView GetEmptyData()
        {
            var statisticsStockShopView = new StatisticsStockShopView()
            {
                AreaStockShops = new List<AreaStockShopView>()
            };
            return statisticsStockShopView;
        }

        /// <summary>
        /// 获取StatisticsStockShopView
        /// </summary>
        /// <param name="statisticsStockShop"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private async Task<StatisticsStockShopView> GetStatisticsStockShopView(Tuple<IEnumerable<DbStatisticsStockViewShop>, int> statisticsStockShop, int type)
        {
            var statisticsStockShopView = new StatisticsStockShopView()
            {
                AreaStockShops = new List<AreaStockShopView>()
            };
            if (statisticsStockShop == null || statisticsStockShop.Item1 == null || !statisticsStockShop.Item1.Any())
            {
                return statisticsStockShopView;
            }
            var statisticsView = statisticsStockShop.Item1.ToList();
            var areaIds = statisticsView.Select(p => p.AreaId).Distinct().ToList();
            var areas = await _areaBll.GetArea(areaIds);
            var shopIds = statisticsView.Select(p => p.ShopId).Distinct().ToList();
            foreach (var shopId in shopIds)
            {
                var statisticsViewAboutShopId = _reportBll.GetStatisticsStockByShop(shopId.ToString()).Result;
                var shop = statisticsView.First(p => p.ShopId == shopId);
                var area = areas.FirstOrDefault(p => p.AreaId == shop.AreaId);
                var areaRetailView = new AreaStockShopView()
                {
                    AreaId = shop.AreaId,
                    AreaLevel = area?.Level,
                    AreaName = area?.AreaName,
                    Herbicide = GetGoodsCategoryNameWeight(statisticsViewAboutShopId, StatisticsCategoryName.Herbicide, type),
                    Fungicide = GetGoodsCategoryNameWeight(statisticsViewAboutShopId, StatisticsCategoryName.Fungicide, type),
                    Insecticide = GetGoodsCategoryNameWeight(statisticsViewAboutShopId, StatisticsCategoryName.Insecticide, type),
                    Acaricide = GetGoodsCategoryNameWeight(statisticsViewAboutShopId, StatisticsCategoryName.Acaricide, type),
                    PlantGrowthRegulator = GetGoodsCategoryNameWeight(statisticsViewAboutShopId, StatisticsCategoryName.PlantGrowthRegulator, type),
                    HygienicInsecticide = GetGoodsCategoryNameWeight(statisticsViewAboutShopId, StatisticsCategoryName.HygienicInsecticide, type),
                    ShopName = shop.ShopName,
                    ShopId = shop.ShopId
                };
                areaRetailView.Sum = statisticsView.Where(p => p.AreaId == shop.AreaId && p.ShopId == shop.ShopId).Sum(c => type == (int)StatisticsTypeEnum.ContentsWeight
                          ? c.TotalContentsWeight
                          : c.TotalWeight);
                areaRetailView.Other = areaRetailView.Sum - areaRetailView.Herbicide - areaRetailView.Fungicide -
                                       areaRetailView.Insecticide - areaRetailView.Acaricide
                                       - areaRetailView.PlantGrowthRegulator - areaRetailView.HygienicInsecticide;
                statisticsStockShopView.AreaStockShops.Add(areaRetailView);
            }
            return statisticsStockShopView;
        }

        /// <summary>
        /// 获取某类别下的重量
        /// </summary>
        /// <param name="dbStatisticsStockShopView"></param>
        /// <param name="categoryName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private decimal GetGoodsCategoryNameWeight(List<DbStatisticsStockView> dbStatisticsStockShopView, string categoryName,
            int type)
        {
            if (dbStatisticsStockShopView == null || !dbStatisticsStockShopView.Any())
            {
                return 0;
            }
            List<DbStatisticsStockView> statistics = null;
            switch (categoryName)
            {
                case StatisticsCategoryName.Herbicide:
                    statistics = dbStatisticsStockShopView.Where(p =>
                         p.GoodsCategoryName == StatisticsCategoryName.Herbicide).ToList();
                    break;
                case StatisticsCategoryName.Fungicide:
                    statistics = dbStatisticsStockShopView.Where(p =>
                        p.GoodsCategoryName == StatisticsCategoryName.Fungicide).ToList();
                    break;
                case StatisticsCategoryName.Insecticide:
                    statistics = dbStatisticsStockShopView.Where(p =>
                        p.GoodsCategoryName == StatisticsCategoryName.Insecticide).ToList();
                    break;
                case StatisticsCategoryName.Acaricide:
                    statistics = dbStatisticsStockShopView.Where(p =>
                         p.GoodsCategoryName == StatisticsCategoryName.Acaricide).ToList();
                    break;
                case StatisticsCategoryName.PlantGrowthRegulator:
                    statistics = dbStatisticsStockShopView.Where(p =>
                       p.GoodsCategoryName == StatisticsCategoryName.PlantGrowthRegulator).ToList();
                    break;
                case StatisticsCategoryName.HygienicInsecticide:
                    statistics = dbStatisticsStockShopView.Where(p =>
                        p.GoodsCategoryName == StatisticsCategoryName.HygienicInsecticide).ToList();
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
