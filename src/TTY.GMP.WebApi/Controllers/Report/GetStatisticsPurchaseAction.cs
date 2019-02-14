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
    /// 区域农药采购统计
    /// </summary>
    public class GetStatisticsPurchaseAction
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
        public GetStatisticsPurchaseAction(IAreaBLL areaBll, IReportBLL reportBll)
        {
            this._areaBll = areaBll;
            this._reportBll = reportBll;
        }

        /// <summary>
        /// 区域采购统计
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(HttpContext httpContext, GetStatisticsPurchaseRequest request)
        {
            _appTicket = AppTicket.GetAppTicket(httpContext);
            if (request.AreaId == 0 || _appTicket.DataLimitType == (int)DataLimitTypeEnum.Shop)
            {
                return await GetStatisticsPurchaseDefault(request);
            }
            return await GetStatisticsPurchaseLevel(request);
        }

        /// <summary>
        /// 获取默认数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponseBase> GetStatisticsPurchaseDefault(GetStatisticsPurchaseRequest request)
        {
            List<DbStatisticsPurchaseView> statisticsPurchase = null;
            var statisticsTime = ComLib.GetAreaStatisticsTime(request.StartTime, request.EndTime);
            var limitShops = string.Empty;
            var arealevel = string.Empty;
            switch (_appTicket.DataLimitType)
            {
                case (int)DataLimitTypeEnum.All:
                    var province = await _areaBll.GetProvince();
                    statisticsPurchase = await _reportBll.GetStatisticsPurchase(string.Join(',', province.Select(p => p.AreaId)),
                        statisticsTime.Item1, statisticsTime.Item2, AreaLevelEnum.Province);
                    arealevel = AreaLevelEnum.Province;
                    break;
                case (int)DataLimitTypeEnum.Area:
                    if (!string.IsNullOrEmpty(_appTicket.DataLimitArea))
                    {
                        var areaInfo = await ComLib.GetGetAreaStatisticsAreaId(_areaBll, _appTicket.DataLimitArea);
                        statisticsPurchase = await _reportBll.GetStatisticsPurchase(areaInfo.Item1, statisticsTime.Item1,
                            statisticsTime.Item2, areaInfo.Item2);
                        arealevel = areaInfo.Item2;
                    }
                    break;
                case (int)DataLimitTypeEnum.Shop:
                    limitShops = _appTicket.DataLimitShop;
                    if (!string.IsNullOrEmpty(_appTicket.DataLimitShop))
                    {
                        statisticsPurchase = await _reportBll.GetStatisticsPurchaseByShop(_appTicket.DataLimitShop,
                            statisticsTime.Item1, statisticsTime.Item2);
                        arealevel = AreaLevelEnum.Street;
                    }
                    break;
            }
            var result = await GetStatisticsPurchaseView(statisticsTime.Item1, statisticsTime.Item2, statisticsPurchase, request.Type, limitShops, arealevel);
            return ResponseBase.Success(result);
        }

        /// <summary>
        /// 获取选择的地区下一级数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponseBase> GetStatisticsPurchaseLevel(GetStatisticsPurchaseRequest request)
        {
            if (_appTicket.DataLimitType == (int)DataLimitTypeEnum.Area && _appTicket.DataLimitArea.IndexOf(request.AreaId.ToString()) < 0)
            {
                return new ResponseBase().GetResponseError(StatusCode.DataForbidden, "数据无权访问");
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
                return ResponseBase.Success(GetEmptyData(statisticsTime.Item1, statisticsTime.Item2));
            }
            var statisticsPurchase = await _reportBll.GetStatisticsPurchase(string.Join(',', areas.Select(p => p.AreaId)),
                statisticsTime.Item1, statisticsTime.Item2, newLevel);
            var result = await GetStatisticsPurchaseView(statisticsTime.Item1, statisticsTime.Item2, statisticsPurchase, request.Type, string.Empty, newLevel);
            return ResponseBase.Success(result);
        }

        /// <summary>
        /// 返回空数据
        /// </summary>
        /// <param name="endTime"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        private StatisticsPurchaseView GetEmptyData(DateTime startTime, DateTime endTime)
        {
            var statisticsPurchaseView = new StatisticsPurchaseView()
            {
                AreaPurchases = new List<AreaPurchaseView>(),
                AreaPurchaseSum = new AreaPurchaseSumView(),
                StartTime = startTime,
                EndTime = endTime
            };
            return statisticsPurchaseView;
        }

        /// <summary>
        /// 获取销售统计信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="statisticsPurchase"></param>
        /// <param name="type"></param>
        /// <param name="limitShops"></param>
        /// <param name="areaLevel"></param>
        /// <returns></returns>
        private async Task<StatisticsPurchaseView> GetStatisticsPurchaseView(DateTime startTime, DateTime endTime, List<DbStatisticsPurchaseView> statisticsPurchase, int type, string
             limitShops, string areaLevel)
        {
            var statisticsPurchaseView = new StatisticsPurchaseView()
            {
                AreaPurchases = new List<AreaPurchaseView>(),
                AreaPurchaseSum = new AreaPurchaseSumView(),
                StartTime = startTime,
                EndTime = endTime
            };
            if (statisticsPurchase == null || !statisticsPurchase.Any())
            {
                return statisticsPurchaseView;
            }
            var areaIds = statisticsPurchase.Select(p => p.AreaId).Distinct().ToList();
            var areas = await _areaBll.GetArea(areaIds);
            List<StatisticsPurchaseCount> statisticsPurchaseCounts = null;
            if (string.IsNullOrEmpty(limitShops))
            {
                statisticsPurchaseCounts = await _reportBll.GetStatisticsPurchaseCount(string.Join(',', areaIds), startTime, endTime,
                    areaLevel, string.Empty);
            }
            else
            {
                statisticsPurchaseCounts = await _reportBll.GetStatisticsPurchaseCount(limitShops, startTime, endTime);
            }
            foreach (var areaId in areaIds)
            {
                var area = areas.FirstOrDefault(p => p.AreaId == areaId);
                var areaPurchaseView = new AreaPurchaseView()
                {
                    AreaId = areaId,
                    AreaLevel = area?.Level,
                    AreaName = area?.AreaName,
                    Herbicide = GetGoodsCategoryNameWeight(areaId, statisticsPurchase, StatisticsCategoryName.Herbicide, type),
                    Fungicide = GetGoodsCategoryNameWeight(areaId, statisticsPurchase, StatisticsCategoryName.Fungicide, type),
                    Insecticide = GetGoodsCategoryNameWeight(areaId, statisticsPurchase, StatisticsCategoryName.Insecticide, type),
                    Acaricide = GetGoodsCategoryNameWeight(areaId, statisticsPurchase, StatisticsCategoryName.Acaricide, type),
                    PlantGrowthRegulator = GetGoodsCategoryNameWeight(areaId, statisticsPurchase, StatisticsCategoryName.PlantGrowthRegulator, type),
                    HygienicInsecticide = GetGoodsCategoryNameWeight(areaId, statisticsPurchase, StatisticsCategoryName.HygienicInsecticide, type)
                };
                areaPurchaseView.Sum = statisticsPurchase.Where(p => p.AreaId == areaId).Sum(c => type == (int)StatisticsTypeEnum.ContentsWeight
                       ? c.TotalContentsWeight
                       : c.TotalWeight);
                areaPurchaseView.Other = areaPurchaseView.Sum - areaPurchaseView.Herbicide - areaPurchaseView.Fungicide -
                                       areaPurchaseView.Insecticide - areaPurchaseView.Acaricide
                                       - areaPurchaseView.PlantGrowthRegulator - areaPurchaseView.HygienicInsecticide;
                areaPurchaseView.Count = GetStatisticsPurchaseCount(statisticsPurchaseCounts, areaLevel, areaId);
                statisticsPurchaseView.AreaPurchases.Add(areaPurchaseView);
            }
            statisticsPurchaseView.AreaPurchaseSum = new AreaPurchaseSumView()
            {
                Sum = statisticsPurchaseView.AreaPurchases.Sum(p => p.Sum),
                Count = statisticsPurchaseView.AreaPurchases.Sum(p => p.Count),
                Other = statisticsPurchaseView.AreaPurchases.Sum(p => p.Other),
                Acaricide = statisticsPurchaseView.AreaPurchases.Sum(p => p.Acaricide),
                Fungicide = statisticsPurchaseView.AreaPurchases.Sum(p => p.Fungicide),
                Herbicide = statisticsPurchaseView.AreaPurchases.Sum(p => p.Herbicide),
                HygienicInsecticide = statisticsPurchaseView.AreaPurchases.Sum(p => p.HygienicInsecticide),
                Insecticide = statisticsPurchaseView.AreaPurchases.Sum(p => p.Insecticide),
                PlantGrowthRegulator = statisticsPurchaseView.AreaPurchases.Sum(p => p.PlantGrowthRegulator)
            };
            return statisticsPurchaseView;
        }

        /// <summary>
        /// 获取销售单数
        /// </summary>
        /// <param name="statisticsPurchaseCounts"></param>
        /// <param name="level"></param>
        /// <param name="areaId"></param>
        /// <returns></returns>
        private int GetStatisticsPurchaseCount(List<StatisticsPurchaseCount> statisticsPurchaseCounts, string level, long areaId)
        {
            if (statisticsPurchaseCounts == null)
            {
                return 0;
            }
            List<StatisticsPurchaseCount> statisticsPurchase = null;
            switch (level)
            {
                case AreaLevelEnum.Province:
                    statisticsPurchase = statisticsPurchaseCounts.Where(p => p.Province == areaId).ToList();
                    break;
                case AreaLevelEnum.City:
                    statisticsPurchase = statisticsPurchaseCounts.Where(p => p.City == areaId).ToList();
                    break;
                case AreaLevelEnum.District:
                    statisticsPurchase = statisticsPurchaseCounts.Where(p => p.District == areaId).ToList();
                    break;
                case AreaLevelEnum.Street:
                    statisticsPurchase = statisticsPurchaseCounts.Where(p => p.Street == areaId).ToList();
                    break;
                default:
                    statisticsPurchase = statisticsPurchaseCounts.Where(p => p.Street == areaId).ToList();
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
        /// <param name="areaId"></param>
        /// <param name="statisticsPurchase"></param>
        /// <param name="categoryName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private decimal GetGoodsCategoryNameWeight(long areaId, List<DbStatisticsPurchaseView> statisticsPurchase, string categoryName,
            int type)
        {
            if (statisticsPurchase == null || !statisticsPurchase.Any())
            {
                return 0;
            }
            List<DbStatisticsPurchaseView> statistics = null;
            switch (categoryName)
            {
                case StatisticsCategoryName.Herbicide:
                    statistics = statisticsPurchase.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.Herbicide).ToList();
                    break;
                case StatisticsCategoryName.Fungicide:
                    statistics = statisticsPurchase.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.Fungicide).ToList();
                    break;
                case StatisticsCategoryName.Insecticide:
                    statistics = statisticsPurchase.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.Insecticide).ToList();
                    break;
                case StatisticsCategoryName.Acaricide:
                    statistics = statisticsPurchase.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.Acaricide).ToList();
                    break;
                case StatisticsCategoryName.PlantGrowthRegulator:
                    statistics = statisticsPurchase.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.PlantGrowthRegulator).ToList();
                    break;
                case StatisticsCategoryName.HygienicInsecticide:
                    statistics = statisticsPurchase.Where(p =>
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
