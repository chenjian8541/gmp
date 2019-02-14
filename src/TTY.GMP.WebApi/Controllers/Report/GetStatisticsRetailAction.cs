using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    /// 区域农药销售统计
    /// </summary>
    public class GetStatisticsRetailAction
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
        public GetStatisticsRetailAction(IAreaBLL areaBll, IReportBLL reportBll)
        {
            this._areaBll = areaBll;
            this._reportBll = reportBll;
        }

        /// <summary>
        /// 区域农药销售统计
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(HttpContext httpContext, GetStatisticsRetailRequest request)
        {
            _appTicket = AppTicket.GetAppTicket(httpContext);
            if (request.AreaId == 0 || _appTicket.DataLimitType == (int)DataLimitTypeEnum.Shop)
            {
                return await GetStatisticsRetailDefault(request);
            }
            return await GetStatisticsRetailLevel(request);
        }

        /// <summary>
        /// 获取默认数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponseBase> GetStatisticsRetailDefault(GetStatisticsRetailRequest request)
        {
            List<DbStatisticsRetailView> statisticsRetail = null;
            var statisticsTime = ComLib.GetAreaStatisticsTime(request.StartTime, request.EndTime);
            var limitShops = string.Empty;
            var arealevel = string.Empty;
            switch (_appTicket.DataLimitType)
            {
                case (int)DataLimitTypeEnum.All:
                    var province = await _areaBll.GetProvince();
                    statisticsRetail = await _reportBll.GetStatisticsRetail(string.Join(',', province.Select(p => p.AreaId)),
                        statisticsTime.Item1, statisticsTime.Item2, AreaLevelEnum.Province);
                    arealevel = AreaLevelEnum.Province;
                    break;
                case (int)DataLimitTypeEnum.Area:
                    if (!string.IsNullOrEmpty(_appTicket.DataLimitArea))
                    {
                        var areaInfo = await ComLib.GetGetAreaStatisticsAreaId(_areaBll, _appTicket.DataLimitArea);
                        statisticsRetail = await _reportBll.GetStatisticsRetail(areaInfo.Item1, statisticsTime.Item1,
                            statisticsTime.Item2, areaInfo.Item2);
                        arealevel = areaInfo.Item2;
                    }
                    break;
                case (int)DataLimitTypeEnum.Shop:
                    limitShops = _appTicket.DataLimitShop;
                    if (!string.IsNullOrEmpty(_appTicket.DataLimitShop))
                    {
                        statisticsRetail = await _reportBll.GetStatisticsRetailByShop(_appTicket.DataLimitShop,
                            statisticsTime.Item1, statisticsTime.Item2);
                        arealevel = AreaLevelEnum.Street;
                    }
                    break;
            }
            var result = await GetStatisticsRetailView(statisticsTime.Item1, statisticsTime.Item2, statisticsRetail, request.Type, limitShops, arealevel);
            return ResponseBase.Success(result);
        }

        /// <summary>
        /// 获取选择的地区下一级数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponseBase> GetStatisticsRetailLevel(GetStatisticsRetailRequest request)
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
            var statisticsRetail = await _reportBll.GetStatisticsRetail(string.Join(',', areas.Select(p => p.AreaId)),
                statisticsTime.Item1, statisticsTime.Item2, newLevel);
            var result = await GetStatisticsRetailView(statisticsTime.Item1, statisticsTime.Item2, statisticsRetail, request.Type, string.Empty, newLevel);
            return ResponseBase.Success(result);
        }

        /// <summary>
        /// 获取空数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private StatisticsRetailView GetEmptyData(DateTime startTime, DateTime endTime)
        {
            var statisticsRetailView = new StatisticsRetailView()
            {
                AreaRetails = new List<AreaRetailView>(),
                AreaRetailSum = new AreaRetailSumView(),
                StartTime = startTime,
                EndTime = endTime
            };
            return statisticsRetailView;
        }

        /// <summary>
        /// 获取销售统计信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="statisticsRetail"></param>
        /// <param name="type"></param>
        /// <param name="limitShops"></param>
        /// <param name="areaLevel"></param>
        /// <returns></returns>
        private async Task<StatisticsRetailView> GetStatisticsRetailView(DateTime startTime, DateTime endTime, List<DbStatisticsRetailView> statisticsRetail, int type, string
             limitShops, string areaLevel)
        {
            var statisticsRetailView = new StatisticsRetailView()
            {
                AreaRetails = new List<AreaRetailView>(),
                AreaRetailSum = new AreaRetailSumView(),
                StartTime = startTime,
                EndTime = endTime
            };
            if (statisticsRetail == null || !statisticsRetail.Any())
            {
                return statisticsRetailView;
            }
            var areaIds = statisticsRetail.Select(p => p.AreaId).Distinct().ToList();
            var areas = await _areaBll.GetArea(areaIds);
            List<StatisticsRetailCount> statisticsRetailCounts;
            if (string.IsNullOrEmpty(limitShops))
            {
                statisticsRetailCounts = await _reportBll.GetStatisticsRetailCount(string.Join(',', areaIds), startTime, endTime,
                    areaLevel, string.Empty);
            }
            else
            {
                statisticsRetailCounts = await _reportBll.GetStatisticsRetailCount(limitShops, startTime, endTime);
            }
            foreach (var areaId in areaIds)
            {
                var area = areas.FirstOrDefault(p => p.AreaId == areaId);
                var areaRetailView = new AreaRetailView()
                {
                    AreaId = areaId,
                    AreaLevel = area?.Level,
                    AreaName = area?.AreaName,
                    Herbicide = GetGoodsCategoryNameWeight(areaId, statisticsRetail, StatisticsCategoryName.Herbicide, type),
                    Fungicide = GetGoodsCategoryNameWeight(areaId, statisticsRetail, StatisticsCategoryName.Fungicide, type),
                    Insecticide = GetGoodsCategoryNameWeight(areaId, statisticsRetail, StatisticsCategoryName.Insecticide, type),
                    Acaricide = GetGoodsCategoryNameWeight(areaId, statisticsRetail, StatisticsCategoryName.Acaricide, type),
                    PlantGrowthRegulator = GetGoodsCategoryNameWeight(areaId, statisticsRetail, StatisticsCategoryName.PlantGrowthRegulator, type),
                    HygienicInsecticide = GetGoodsCategoryNameWeight(areaId, statisticsRetail, StatisticsCategoryName.HygienicInsecticide, type)
                };
                areaRetailView.Sum = statisticsRetail.Where(p => p.AreaId == areaId).Sum(c => type == (int)StatisticsTypeEnum.ContentsWeight
                       ? c.TotalContentsWeight
                       : c.TotalWeight);
                areaRetailView.Other = areaRetailView.Sum - areaRetailView.Herbicide - areaRetailView.Fungicide -
                                       areaRetailView.Insecticide - areaRetailView.Acaricide
                                       - areaRetailView.PlantGrowthRegulator - areaRetailView.HygienicInsecticide;
                areaRetailView.Count = GetStatisticsRetailCount(statisticsRetailCounts, areaLevel, areaId);
                statisticsRetailView.AreaRetails.Add(areaRetailView);
            }
            statisticsRetailView.AreaRetailSum = new AreaRetailSumView()
            {
                Sum = statisticsRetailView.AreaRetails.Sum(p => p.Sum),
                Count = statisticsRetailView.AreaRetails.Sum(p => p.Count),
                Other = statisticsRetailView.AreaRetails.Sum(p => p.Other),
                Acaricide = statisticsRetailView.AreaRetails.Sum(p => p.Acaricide),
                Fungicide = statisticsRetailView.AreaRetails.Sum(p => p.Fungicide),
                Herbicide = statisticsRetailView.AreaRetails.Sum(p => p.Herbicide),
                HygienicInsecticide = statisticsRetailView.AreaRetails.Sum(p => p.HygienicInsecticide),
                Insecticide = statisticsRetailView.AreaRetails.Sum(p => p.Insecticide),
                PlantGrowthRegulator = statisticsRetailView.AreaRetails.Sum(p => p.PlantGrowthRegulator)
            };
            return statisticsRetailView;
        }

        /// <summary>
        /// 获取销售单数
        /// </summary>
        /// <param name="statisticsRetailCounts"></param>
        /// <param name="level"></param>
        /// <param name="areaId"></param>
        /// <returns></returns>
        private int GetStatisticsRetailCount(List<StatisticsRetailCount> statisticsRetailCounts, string level, long areaId)
        {
            if (statisticsRetailCounts == null)
            {
                return 0;
            }
            List<StatisticsRetailCount> statisticsRetail = null;
            switch (level)
            {
                case AreaLevelEnum.Province:
                    statisticsRetail = statisticsRetailCounts.Where(p => p.Province == areaId).ToList();
                    break;
                case AreaLevelEnum.City:
                    statisticsRetail = statisticsRetailCounts.Where(p => p.City == areaId).ToList();
                    break;
                case AreaLevelEnum.District:
                    statisticsRetail = statisticsRetailCounts.Where(p => p.District == areaId).ToList();
                    break;
                case AreaLevelEnum.Street:
                    statisticsRetail = statisticsRetailCounts.Where(p => p.Street == areaId).ToList();
                    break;
                default:
                    statisticsRetail = statisticsRetailCounts.Where(p => p.Street == areaId).ToList();
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
        /// <param name="statisticsRetail"></param>
        /// <param name="categoryName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private decimal GetGoodsCategoryNameWeight(long areaId, List<DbStatisticsRetailView> statisticsRetail, string categoryName,
            int type)
        {
            if (statisticsRetail == null || !statisticsRetail.Any())
            {
                return 0;
            }
            List<DbStatisticsRetailView> statistics = null;
            switch (categoryName)
            {
                case StatisticsCategoryName.Herbicide:
                    statistics = statisticsRetail.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.Herbicide).ToList();
                    break;
                case StatisticsCategoryName.Fungicide:
                    statistics = statisticsRetail.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.Fungicide).ToList();
                    break;
                case StatisticsCategoryName.Insecticide:
                    statistics = statisticsRetail.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.Insecticide).ToList();
                    break;
                case StatisticsCategoryName.Acaricide:
                    statistics = statisticsRetail.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.Acaricide).ToList();
                    break;
                case StatisticsCategoryName.PlantGrowthRegulator:
                    statistics = statisticsRetail.Where(p =>
                        p.AreaId == areaId && p.GoodsCategoryName == StatisticsCategoryName.PlantGrowthRegulator).ToList();
                    break;
                case StatisticsCategoryName.HygienicInsecticide:
                    statistics = statisticsRetail.Where(p =>
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
