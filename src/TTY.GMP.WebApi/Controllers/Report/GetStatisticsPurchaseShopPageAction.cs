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
    /// 区域门店采购看板
    /// </summary>
    public class GetStatisticsPurchaseShopPageAction
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
        public GetStatisticsPurchaseShopPageAction(IAreaBLL areaBll, IReportBLL reportBll)
        {
            this._areaBll = areaBll;
            this._reportBll = reportBll;
        }

        /// <summary>
        /// 区域门店采购看板
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponsePagingBase> ProcessAction(HttpContext httpContext, GetStatisticsPurchaseShopPageRequest request)
        {
            _appTicket = AppTicket.GetAppTicket(httpContext);
            if (request.AreaId == 0 || _appTicket.DataLimitType == (int)DataLimitTypeEnum.Shop)
            {
                return await GetStatisticsPurchaseShopDefault(request);
            }
            return await GetStatisticsPurchaseShopLevel(request);
        }

        /// <summary>
        /// 获取默认数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponsePagingBase> GetStatisticsPurchaseShopDefault(GetStatisticsPurchaseShopPageRequest request)
        {
            Tuple<IEnumerable<DbStatisticsPurchaseShopView>, int> statisticsPurchase = null;
            var statisticsTime = ComLib.GetAreaStatisticsTime(request.StartTime, request.EndTime);
            var arealevel = string.Empty;
            switch (_appTicket.DataLimitType)
            {
                case (int)DataLimitTypeEnum.All:
                    var province = await _areaBll.GetProvince();
                    statisticsPurchase = await _reportBll.GetStatisticsPurchaseShop(request, string.Join(',', province.Select(p => p.AreaId)),
                        statisticsTime.Item1, statisticsTime.Item2, AreaLevelEnum.Province);
                    arealevel = AreaLevelEnum.Province;
                    break;
                case (int)DataLimitTypeEnum.Area:
                    if (!string.IsNullOrEmpty(_appTicket.DataLimitArea))
                    {
                        var areaInfo = await ComLib.GetGetAreaStatisticsAreaId(_areaBll, _appTicket.DataLimitArea);
                        statisticsPurchase = await _reportBll.GetStatisticsPurchaseShop(request, areaInfo.Item1, statisticsTime.Item1,
                            statisticsTime.Item2, areaInfo.Item2);
                        arealevel = areaInfo.Item2;
                    }
                    break;
                case (int)DataLimitTypeEnum.Shop:
                    if (!string.IsNullOrEmpty(_appTicket.DataLimitShop))
                    {
                        statisticsPurchase = await _reportBll.GetStatisticsPurchaseShopByShop(request, _appTicket.DataLimitShop,
                            statisticsTime.Item1, statisticsTime.Item2);
                        arealevel = AreaLevelEnum.Street;
                    }
                    break;
            }
            var result = await GetStatisticsPurchaseShopView(statisticsTime.Item1, statisticsTime.Item2, statisticsPurchase, request.Type, arealevel);
            var recordCount = statisticsPurchase == null ? 0 : statisticsPurchase.Item2;
            return ResponsePagingBase.Success(result, recordCount);
        }

        /// <summary>
        /// 获取选择的地区下一级数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponsePagingBase> GetStatisticsPurchaseShopLevel(GetStatisticsPurchaseShopPageRequest request)
        {
            if (_appTicket.DataLimitType == (int)DataLimitTypeEnum.Area && _appTicket.DataLimitArea.IndexOf(request.AreaId.ToString()) < 0)
            {
                return new ResponsePagingBase().GetResponseError(StatusCode.DataForbidden, "数据无权访问");
            }
            var statisticsTime = ComLib.GetAreaStatisticsTime(request.StartTime, request.EndTime);
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
                return ResponsePagingBase.Success(GetEmptyData(statisticsTime.Item1, statisticsTime.Item2), 0);
            }
            var statisticsPurchase = await _reportBll.GetStatisticsPurchaseShop(request, string.Join(',', areas.Select(p => p.AreaId)),
                statisticsTime.Item1, statisticsTime.Item2, newLevel);
            var result = await GetStatisticsPurchaseShopView(statisticsTime.Item1, statisticsTime.Item2, statisticsPurchase, request.Type, newLevel);
            var recordCount = statisticsPurchase == null ? 0 : statisticsPurchase.Item2;
            return ResponsePagingBase.Success(result, recordCount);
        }

        /// <summary>
        /// 返回空数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private StatisticsPurchaseShopView GetEmptyData(DateTime startTime, DateTime endTime)
        {
            var statisticsPurchaseShopView = new StatisticsPurchaseShopView()
            {
                AreaShopPurchases = new List<AreaShopPurchaseView>(),
                StartTime = startTime,
                EndTime = endTime
            };
            return statisticsPurchaseShopView;
        }

        /// <summary>
        /// 获取采购统计信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="statisticsPurchaseShop"></param>
        /// <param name="type"></param>
        /// <param name="areaLevel"></param>
        /// <returns></returns>
        private async Task<StatisticsPurchaseShopView> GetStatisticsPurchaseShopView(DateTime startTime, DateTime endTime,
            Tuple<IEnumerable<DbStatisticsPurchaseShopView>, int> statisticsPurchaseShop, int type, string areaLevel)
        {
            var statisticsPurchaseShopView = new StatisticsPurchaseShopView()
            {
                AreaShopPurchases = new List<AreaShopPurchaseView>(),
                StartTime = startTime,
                EndTime = endTime
            };
            if (statisticsPurchaseShop == null || statisticsPurchaseShop.Item1 == null || !statisticsPurchaseShop.Item1.Any())
            {
                return statisticsPurchaseShopView;
            }
            var statisticsPurchase = statisticsPurchaseShop.Item1.ToList();
            var areaIds = statisticsPurchase.Select(p => p.AreaId).Distinct().ToList();
            var areas = await _areaBll.GetArea(areaIds);
            var shopIds = statisticsPurchase.Select(p => p.ShopId).Distinct().ToList();
            var statisticsRetailShopCounts = await _reportBll.GetStatisticsPurchaseCount(string.Join(',', shopIds), startTime, endTime);
            foreach (var shopId in shopIds)
            {
                var statisticsPurchaseAboutShopId = _reportBll.GetStatisticsPurchaseByShop(shopId.ToString(), startTime, endTime).Result;
                var shop = statisticsPurchase.First(p => p.ShopId == shopId);
                var area = areas.FirstOrDefault(p => p.AreaId == shop.AreaId);
                var areaRetailView = new AreaShopPurchaseView()
                {
                    AreaId = shop.AreaId,
                    AreaLevel = area?.Level,
                    AreaName = area?.AreaName,
                    Herbicide = GetGoodsCategoryNameWeight(statisticsPurchaseAboutShopId, StatisticsCategoryName.Herbicide, type),
                    Fungicide = GetGoodsCategoryNameWeight(statisticsPurchaseAboutShopId, StatisticsCategoryName.Fungicide, type),
                    Insecticide = GetGoodsCategoryNameWeight(statisticsPurchaseAboutShopId, StatisticsCategoryName.Insecticide, type),
                    Acaricide = GetGoodsCategoryNameWeight(statisticsPurchaseAboutShopId, StatisticsCategoryName.Acaricide, type),
                    PlantGrowthRegulator = GetGoodsCategoryNameWeight(statisticsPurchaseAboutShopId, StatisticsCategoryName.PlantGrowthRegulator, type),
                    HygienicInsecticide = GetGoodsCategoryNameWeight(statisticsPurchaseAboutShopId, StatisticsCategoryName.HygienicInsecticide, type),
                    ShopName = shop.ShopName,
                    ShopId = shop.ShopId
                };
                areaRetailView.Sum = statisticsPurchase.Where(p => p.AreaId == shop.AreaId && p.ShopId == shop.ShopId).Sum(c => type == (int)StatisticsTypeEnum.ContentsWeight
                          ? c.TotalContentsWeight
                          : c.TotalWeight);
                areaRetailView.Other = areaRetailView.Sum - areaRetailView.Herbicide - areaRetailView.Fungicide -
                                       areaRetailView.Insecticide - areaRetailView.Acaricide
                                       - areaRetailView.PlantGrowthRegulator - areaRetailView.HygienicInsecticide;
                areaRetailView.Count = GetStatisticsPurchaseCount(statisticsRetailShopCounts, shop.ShopId, areaLevel, shop.AreaId);
                statisticsPurchaseShopView.AreaShopPurchases.Add(areaRetailView);
            }
            return statisticsPurchaseShopView;
        }

        /// <summary>
        /// 获取销售单数
        /// </summary>
        /// <param name="statisticsPurchaseCounts"></param>
        /// <param name="shopId"></param>
        /// <param name="level"></param>
        /// <param name="areaId"></param>
        /// <returns></returns>
        private int GetStatisticsPurchaseCount(List<StatisticsPurchaseCount> statisticsPurchaseCounts, long shopId, string level, long areaId)
        {
            if (statisticsPurchaseCounts == null)
            {
                return 0;
            }
            List<StatisticsPurchaseCount> statisticsPurchase = null;
            switch (level)
            {
                case AreaLevelEnum.Province:
                    statisticsPurchase = statisticsPurchaseCounts.Where(p => p.Province == areaId && p.ShopId == shopId).ToList();
                    break;
                case AreaLevelEnum.City:
                    statisticsPurchase = statisticsPurchaseCounts.Where(p => p.City == areaId && p.ShopId == shopId).ToList();
                    break;
                case AreaLevelEnum.District:
                    statisticsPurchase = statisticsPurchaseCounts.Where(p => p.District == areaId && p.ShopId == shopId).ToList();
                    break;
                case AreaLevelEnum.Street:
                    statisticsPurchase = statisticsPurchaseCounts.Where(p => p.Street == areaId && p.ShopId == shopId).ToList();
                    break;
                default:
                    statisticsPurchase = statisticsPurchaseCounts.Where(p => p.Street == areaId && p.ShopId == shopId).ToList();
                    break;
            }
            if (!statisticsPurchase.Any())
            {
                return 0;
            }
            return statisticsPurchase.Sum(p => p.BillCount);
        }

        /// <summary>
        /// 获取某类别下的重量
        /// </summary>
        /// <param name="statisticsPurchaseShop"></param>
        /// <param name="categoryName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private decimal GetGoodsCategoryNameWeight(List<DbStatisticsPurchaseView> statisticsPurchaseShop, string categoryName,
            int type)
        {
            if (statisticsPurchaseShop == null || !statisticsPurchaseShop.Any())
            {
                return 0;
            }
            List<DbStatisticsPurchaseView> statistics = null;
            switch (categoryName)
            {
                case StatisticsCategoryName.Herbicide:
                    statistics = statisticsPurchaseShop.Where(p => p.GoodsCategoryName == StatisticsCategoryName.Herbicide).ToList();
                    break;
                case StatisticsCategoryName.Fungicide:
                    statistics = statisticsPurchaseShop.Where(p => p.GoodsCategoryName == StatisticsCategoryName.Fungicide).ToList();
                    break;
                case StatisticsCategoryName.Insecticide:
                    statistics = statisticsPurchaseShop.Where(p => p.GoodsCategoryName == StatisticsCategoryName.Insecticide).ToList();
                    break;
                case StatisticsCategoryName.Acaricide:
                    statistics = statisticsPurchaseShop.Where(p => p.GoodsCategoryName == StatisticsCategoryName.Acaricide).ToList();
                    break;
                case StatisticsCategoryName.PlantGrowthRegulator:
                    statistics = statisticsPurchaseShop.Where(p => p.GoodsCategoryName == StatisticsCategoryName.PlantGrowthRegulator).ToList();
                    break;
                case StatisticsCategoryName.HygienicInsecticide:
                    statistics = statisticsPurchaseShop.Where(p => p.GoodsCategoryName == StatisticsCategoryName.HygienicInsecticide).ToList();
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
