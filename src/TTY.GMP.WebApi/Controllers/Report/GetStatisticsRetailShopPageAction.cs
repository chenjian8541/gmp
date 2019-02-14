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
    /// 区域门店销售看板
    /// </summary>
    public class GetStatisticsRetailShopPageAction
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
        public GetStatisticsRetailShopPageAction(IAreaBLL areaBll, IReportBLL reportBll)
        {
            this._areaBll = areaBll;
            this._reportBll = reportBll;
        }

        /// <summary>
        /// 区域门店销售看板
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponsePagingBase> ProcessAction(HttpContext httpContext, GetStatisticsRetailShopPageRequest request)
        {
            _appTicket = AppTicket.GetAppTicket(httpContext);
            if (request.AreaId == 0 || _appTicket.DataLimitType == (int)DataLimitTypeEnum.Shop)
            {
                return await GetStatisticsRetailShopDefault(request);
            }
            return await GetStatisticsRetailShopLevel(request);
        }

        /// <summary>
        /// 获取默认数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponsePagingBase> GetStatisticsRetailShopDefault(GetStatisticsRetailShopPageRequest request)
        {
            Tuple<IEnumerable<DbStatisticsRetailViewShop>, int> statisticsRetailShop = null;
            var statisticsTime = ComLib.GetAreaStatisticsTime(request.StartTime, request.EndTime);
            var arealevel = string.Empty;
            switch (_appTicket.DataLimitType)
            {
                case (int)DataLimitTypeEnum.All:
                    var province = await _areaBll.GetProvince();
                    statisticsRetailShop = await _reportBll.GetStatisticsRetailShop(request, string.Join(',', province.Select(p => p.AreaId)),
                        statisticsTime.Item1, statisticsTime.Item2, AreaLevelEnum.Province);
                    arealevel = AreaLevelEnum.Province;
                    break;
                case (int)DataLimitTypeEnum.Area:
                    if (!string.IsNullOrEmpty(_appTicket.DataLimitArea))
                    {
                        var areaInfo = await ComLib.GetGetAreaStatisticsAreaId(_areaBll, _appTicket.DataLimitArea);
                        statisticsRetailShop = await _reportBll.GetStatisticsRetailShop(request, areaInfo.Item1, statisticsTime.Item1,
                            statisticsTime.Item2, areaInfo.Item2);
                        arealevel = areaInfo.Item2;
                    }
                    break;
                case (int)DataLimitTypeEnum.Shop:
                    if (!string.IsNullOrEmpty(_appTicket.DataLimitShop))
                    {
                        statisticsRetailShop = await _reportBll.GetStatisticsRetailShopByShop(request, _appTicket.DataLimitShop,
                            statisticsTime.Item1, statisticsTime.Item2);
                        arealevel = AreaLevelEnum.Street;
                    }
                    break;
            }
            var result = await GetStatisticsRetailViewShop(statisticsTime.Item1, statisticsTime.Item2, statisticsRetailShop, request.Type, arealevel);
            var recordCount = statisticsRetailShop == null ? 0 : statisticsRetailShop.Item2;
            return ResponsePagingBase.Success(result, recordCount);
        }

        /// <summary>
        /// 获取选择的地区下一级数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponsePagingBase> GetStatisticsRetailShopLevel(GetStatisticsRetailShopPageRequest request)
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
            var statisticsRetailShop = await _reportBll.GetStatisticsRetailShop(request, string.Join(',', areas.Select(p => p.AreaId)),
                statisticsTime.Item1, statisticsTime.Item2, newLevel);
            var result = await GetStatisticsRetailViewShop(statisticsTime.Item1, statisticsTime.Item2, statisticsRetailShop, request.Type, newLevel);
            var recordCount = statisticsRetailShop == null ? 0 : statisticsRetailShop.Item2;
            return ResponsePagingBase.Success(result, recordCount);
        }

        /// <summary>
        /// 获取空数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private StatisticsRetailShopView GetEmptyData(DateTime startTime, DateTime endTime)
        {
            var statisticsRetailShopView = new StatisticsRetailShopView()
            {
                AreaShopRetails = new List<AreaShopRetailView>(),
                StartTime = startTime,
                EndTime = endTime
            };
            return statisticsRetailShopView;
        }

        /// <summary>
        /// 获取销售统计信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="statisticsRetailShop"></param>
        /// <param name="type"></param>
        /// <param name="areaLevel"></param>
        /// <returns></returns>
        private async Task<StatisticsRetailShopView> GetStatisticsRetailViewShop(DateTime startTime, DateTime endTime, Tuple<IEnumerable<DbStatisticsRetailViewShop>, int> statisticsRetailShop, int type,
            string areaLevel)
        {
            var statisticsRetailShopView = new StatisticsRetailShopView()
            {
                AreaShopRetails = new List<AreaShopRetailView>(),
                StartTime = startTime,
                EndTime = endTime
            };
            if (statisticsRetailShop == null || statisticsRetailShop.Item1 == null || !statisticsRetailShop.Item1.Any())
            {
                return statisticsRetailShopView;
            }
            var statisticsRetail = statisticsRetailShop.Item1.ToList();
            var areaIds = statisticsRetail.Select(p => p.AreaId).Distinct().ToList();
            var areas = await _areaBll.GetArea(areaIds);
            var shopIds = statisticsRetail.Select(p => p.ShopId).Distinct().ToList();
            var statisticsRetailShopCounts = await _reportBll.GetStatisticsRetailCount(string.Join(',', shopIds), startTime, endTime);
            foreach (var shopId in shopIds)
            {
                var statisticsRetailAboutShopId = _reportBll.GetStatisticsRetailByShop(shopId.ToString(), startTime, endTime).Result;
                var shop = statisticsRetail.First(p => p.ShopId == shopId); //只为获取此门店的省市区等信息
                var area = areas.FirstOrDefault(p => p.AreaId == shop.AreaId);
                var areaRetailView = new AreaShopRetailView()
                {
                    AreaId = shop.AreaId,
                    AreaLevel = area?.Level,
                    AreaName = area?.AreaName,
                    Herbicide = GetGoodsCategoryNameWeight(statisticsRetailAboutShopId, StatisticsCategoryName.Herbicide, type),
                    Fungicide = GetGoodsCategoryNameWeight(statisticsRetailAboutShopId, StatisticsCategoryName.Fungicide, type),
                    Insecticide = GetGoodsCategoryNameWeight(statisticsRetailAboutShopId, StatisticsCategoryName.Insecticide, type),
                    Acaricide = GetGoodsCategoryNameWeight(statisticsRetailAboutShopId, StatisticsCategoryName.Acaricide, type),
                    PlantGrowthRegulator = GetGoodsCategoryNameWeight(statisticsRetailAboutShopId, StatisticsCategoryName.PlantGrowthRegulator, type),
                    HygienicInsecticide = GetGoodsCategoryNameWeight(statisticsRetailAboutShopId, StatisticsCategoryName.HygienicInsecticide, type),
                    ShopName = shop.ShopName,
                    ShopId = shopId
                };
                areaRetailView.Sum = statisticsRetail.Where(p => p.AreaId == shop.AreaId && p.ShopId == shopId).Sum(c => type == (int)StatisticsTypeEnum.ContentsWeight
                          ? c.TotalContentsWeight
                          : c.TotalWeight);
                areaRetailView.Other = areaRetailView.Sum - areaRetailView.Herbicide - areaRetailView.Fungicide -
                                       areaRetailView.Insecticide - areaRetailView.Acaricide
                                       - areaRetailView.PlantGrowthRegulator - areaRetailView.HygienicInsecticide;
                areaRetailView.Count = GetStatisticsRetailCount(statisticsRetailShopCounts, shopId, areaLevel, shop.AreaId);
                statisticsRetailShopView.AreaShopRetails.Add(areaRetailView);
            }
            return statisticsRetailShopView;
        }

        /// <summary>
        /// 获取销售单数
        /// </summary>
        /// <param name="statisticsRetailCounts"></param>
        /// <param name="shopId"></param>
        /// <param name="level"></param>
        /// <param name="areaId"></param>
        /// <returns></returns>
        private int GetStatisticsRetailCount(List<StatisticsRetailCount> statisticsRetailCounts, long shopId, string level, long areaId)
        {
            if (statisticsRetailCounts == null)
            {
                return 0;
            }
            List<StatisticsRetailCount> statisticsRetail = null;
            switch (level)
            {
                case AreaLevelEnum.Province:
                    statisticsRetail = statisticsRetailCounts.Where(p => p.Province == areaId && p.ShopId == shopId).ToList();
                    break;
                case AreaLevelEnum.City:
                    statisticsRetail = statisticsRetailCounts.Where(p => p.City == areaId && p.ShopId == shopId).ToList();
                    break;
                case AreaLevelEnum.District:
                    statisticsRetail = statisticsRetailCounts.Where(p => p.District == areaId && p.ShopId == shopId).ToList();
                    break;
                case AreaLevelEnum.Street:
                    statisticsRetail = statisticsRetailCounts.Where(p => p.Street == areaId && p.ShopId == shopId).ToList();
                    break;
                default:
                    statisticsRetail = statisticsRetailCounts.Where(p => p.Street == areaId && p.ShopId == shopId).ToList();
                    break;
            }
            if (!statisticsRetail.Any())
            {
                return 0;
            }
            return statisticsRetail.Sum(p => p.BillCount);
        }

        /// <summary>
        /// 获取某类别下的重量
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="statisticsRetailShop"></param>
        /// <param name="categoryName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private decimal GetGoodsCategoryNameWeight(List<DbStatisticsRetailView> statisticsRetailShop, string categoryName,
            int type)
        {
            if (statisticsRetailShop == null || !statisticsRetailShop.Any())
            {
                return 0;
            }
            List<DbStatisticsRetailView> statistics = null;
            switch (categoryName)
            {
                case StatisticsCategoryName.Herbicide:
                    statistics = statisticsRetailShop.Where(p => p.GoodsCategoryName == StatisticsCategoryName.Herbicide).ToList();
                    break;
                case StatisticsCategoryName.Fungicide:
                    statistics = statisticsRetailShop.Where(p => p.GoodsCategoryName == StatisticsCategoryName.Fungicide).ToList();
                    break;
                case StatisticsCategoryName.Insecticide:
                    statistics = statisticsRetailShop.Where(p => p.GoodsCategoryName == StatisticsCategoryName.Insecticide).ToList();
                    break;
                case StatisticsCategoryName.Acaricide:
                    statistics = statisticsRetailShop.Where(p => p.GoodsCategoryName == StatisticsCategoryName.Acaricide).ToList();
                    break;
                case StatisticsCategoryName.PlantGrowthRegulator:
                    statistics = statisticsRetailShop.Where(p => p.GoodsCategoryName == StatisticsCategoryName.PlantGrowthRegulator).ToList();
                    break;
                case StatisticsCategoryName.HygienicInsecticide:
                    statistics = statisticsRetailShop.Where(p => p.GoodsCategoryName == StatisticsCategoryName.HygienicInsecticide).ToList();
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
