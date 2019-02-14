using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.WebApi.Core;
using Microsoft.AspNetCore.Http;
using TTY.GMP.Entity.Enum;
using System.Text;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.View;
using TTY.GMP.IBusiness;

namespace TTY.GMP.WebApi.Common
{
    /// <summary>
    /// 公共的一些操作
    /// </summary>
    public class ComLib
    {
        /// <summary>
        /// 处理查询条件
        /// 把数据权限限制条件带入
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns>是否允许查看数据</returns>
        public static bool HandleRequest(HttpContext httpContext, IDataLimitRequest request)
        {
            request.LimitShops = request.LimitProvince = request.LimitCity = request.LimitDistrict = string.Empty;
            var ticket = AppTicket.GetAppTicket(httpContext);
            switch (ticket.DataLimitType)
            {
                case (int)DataLimitTypeEnum.All:
                    return true;
                case (int)DataLimitTypeEnum.Area:
                    if (string.IsNullOrEmpty(ticket.DataLimitArea))
                    {
                        return false;
                    }
                    HandleRequestByArea(ticket.DataLimitArea, request);
                    return true;
                case (int)DataLimitTypeEnum.Shop:
                    if (string.IsNullOrEmpty(ticket.DataLimitShop))
                    {
                        return false;
                    }
                    HandleRequestByShop(ticket.DataLimitShop, request);
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 按照地区数据权限设置
        /// </summary>
        /// <param name="dataLimitArea"></param>
        /// <param name="request"></param>
        private static void HandleRequestByArea(string dataLimitArea, IDataLimitRequest request)
        {
            var limitAreas = dataLimitArea.Split(',');
            var limitProvince = new StringBuilder();
            var limitCity = new StringBuilder();
            var limitDistrict = new StringBuilder();
            foreach (var limitArea in limitAreas)
            {
                if (string.IsNullOrEmpty(limitArea))
                {
                    continue;
                }
                var temp = limitArea.Split('|');
                switch (temp[1])
                {
                    case AreaLevelEnum.Province:
                        limitProvince.AppendFormat("{0},", temp[0]);
                        break;
                    case AreaLevelEnum.City:
                        limitCity.AppendFormat("{0},", temp[0]);
                        break;
                    case AreaLevelEnum.District:
                        limitDistrict.AppendFormat("{0},", temp[0]);
                        break;
                }
            }
            request.LimitProvince = limitProvince.ToString().TrimEnd(',');
            request.LimitCity = limitCity.ToString().TrimEnd(',');
            request.LimitDistrict = limitDistrict.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 按照门店数据权限设置
        /// </summary>
        /// <param name="dataLimitShop"></param>
        /// <param name="request"></param>
        private static void HandleRequestByShop(string dataLimitShop, IDataLimitRequest request)
        {
            request.LimitShops = dataLimitShop;
        }

        /// <summary>
        /// 处理区域报表查询的开始时间和结束时间
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static Tuple<DateTime, DateTime> GetAreaStatisticsTime(DateTime? startTime, DateTime? endTime)
        {
            DateTime newStartTime;
            DateTime newEndTime;
            if (startTime == null || endTime == null)
            {
                /*
                 *默认时间
                 *1、如果是1号，则默认日期为上个月;
                 *2、不是1号，则默认日期为当月1号到当前日期
                 */
                var now = DateTime.Now;
                if (now.Day == 1)
                {
                    var tempTime = now.AddMonths(-1);
                    newStartTime = new DateTime(tempTime.Year, tempTime.Month, 1);
                    newEndTime = newStartTime.AddMonths(1).AddSeconds(-1);
                }
                else
                {
                    newStartTime = new DateTime(now.Year, now.Month, 1);
                    newEndTime = now;
                }
                return Tuple.Create(newStartTime, newEndTime);
            }
            newStartTime = startTime.Value.Date;
            newEndTime = endTime.Value.Date.AddDays(1).AddSeconds(-1);
            return Tuple.Create(newStartTime, newEndTime);
        }

        /// <summary>
        /// 根据用户的地区权限，获取用户的默认地区数据
        /// </summary>
        /// <param name="areaBll"></param>
        /// <param name="dataLimitArea"></param>
        /// <returns></returns>
        public static async Task<Tuple<string, string>> GetGetAreaStatisticsAreaId(IAreaBLL areaBll, string dataLimitArea)
        {
            var temp = dataLimitArea.Split(',');
            var provinceArea = new List<long>();
            var cityArea = new List<long>();
            var districtArea = new List<long>();
            var streetArea = new List<long>();
            foreach (var s in temp)
            {
                var area = s.Split('|');
                switch (area[1])
                {
                    case AreaLevelEnum.Province:
                        provinceArea.Add(Convert.ToInt64(area[0]));
                        break;
                    case AreaLevelEnum.City:
                        cityArea.Add(Convert.ToInt64(area[0]));
                        break;
                    case AreaLevelEnum.District:
                        districtArea.Add(Convert.ToInt64(area[0]));
                        break;
                    case AreaLevelEnum.Street:
                        streetArea.Add(Convert.ToInt64(area[0]));
                        break;
                }
            }
            if (provinceArea.Any())
            {
                return await GetProvinceArea(provinceArea, areaBll);
            }
            if (cityArea.Any())
            {
                return await GetCityArea(cityArea, areaBll);
            }
            if (districtArea.Any())
            {
                return await GetDistrictArea(districtArea, areaBll);
            }
            return GetStreetArea(streetArea);
        }

        /// <summary>
        /// 通过省获取地区
        /// </summary>
        /// <param name="provinceArea"></param>
        /// <param name="areaBll"></param>
        /// <returns></returns>
        private static async Task<Tuple<string, string>> GetProvinceArea(List<long> provinceArea, IAreaBLL areaBll)
        {
            if (provinceArea.Count > 1)
            {
                return Tuple.Create(string.Join(',', provinceArea), AreaLevelEnum.Province);
            }
            var citys = await areaBll.GetCity(provinceArea[0]);
            return Tuple.Create(string.Join(',', citys.Select(p => p.AreaId)), AreaLevelEnum.City);
        }

        /// <summary>
        /// 通过市获取地区
        /// </summary>
        /// <param name="cityArea"></param>
        /// <param name="areaBll"></param>
        /// <returns></returns>
        private static async Task<Tuple<string, string>> GetCityArea(List<long> cityArea, IAreaBLL areaBll)
        {
            if (cityArea.Count > 1)
            {
                return Tuple.Create(string.Join(',', cityArea), AreaLevelEnum.City);
            }
            var districts = await areaBll.GetDistrict(cityArea[0]);
            return Tuple.Create(string.Join(',', districts.Select(p => p.AreaId)), AreaLevelEnum.District);
        }

        /// <summary>
        /// 通过县获取地区
        /// </summary>
        /// <param name="districtArea"></param>
        /// <param name="areaBll"></param>
        /// <returns></returns>
        private static async Task<Tuple<string, string>> GetDistrictArea(List<long> districtArea, IAreaBLL areaBll)
        {
            if (districtArea.Count > 1)
            {
                return Tuple.Create(string.Join(',', districtArea), AreaLevelEnum.District);
            }
            var streets = await areaBll.GetStreet(districtArea[0]);
            return Tuple.Create(string.Join(',', streets.Select(p => p.AreaId)), AreaLevelEnum.Street);
        }

        /// <summary>
        /// 通过乡镇/街道获取地区
        /// </summary>
        /// <param name="streetArea"></param>
        /// <returns></returns>
        private static Tuple<string, string> GetStreetArea(List<long> streetArea)
        {
            return Tuple.Create(string.Join(',', streetArea), AreaLevelEnum.Street);
        }

        /// <summary>
        /// 获取品牌信息
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="goodsBrands"></param>
        /// <returns></returns>
        public static string GetBrand(long brandId, List<GoodsBrand> goodsBrands)
        {
            return goodsBrands.FirstOrDefault(p => p.BrandId == brandId)?.BrandName;
        }

        /// <summary>
        /// 获取库存信息
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="stockId"></param>
        /// <param name="stockQtys"></param>
        /// <returns></returns>
        public static decimal GetRepertoryQty(long goodsId, long stockId, IEnumerable<StStockQtyView> stockQtys)
        {
            var qty = stockQtys.FirstOrDefault(p => p.GoodsId == goodsId && p.StockId == stockId);
            if (qty == null)
            {
                return 0;
            }
            return qty.Qty;
        }

        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="sysUnits"></param>
        /// <returns></returns>
        public static string GetUnitNmae(long unitId, List<SysUnit> sysUnits)
        {
            return sysUnits.FirstOrDefault(p => p.UnitId == unitId)?.UnitName;
        }

        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="goodsCategories"></param>
        /// <returns></returns>
        public static string GetCategoryName(long categoryId, List<GoodsCategory> goodsCategories)
        {
            return goodsCategories.FirstOrDefault(p => p.GoodsCategoryId == categoryId)?.GoodsCategoryName;
        }

        /// <summary>
        /// 获取地区信息
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="areas"></param>
        /// <returns></returns>
        public static string GetAreaName(long areaId, List<Area> areas)
        {
            return areas.FirstOrDefault(p => p.AreaId == areaId)?.AreaName;
        }

        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="baseSuppliers"></param>
        /// <returns></returns>
        public static string GetSupplierName(long supplierId, List<BaseSupplier> baseSuppliers)
        {
            return baseSuppliers.FirstOrDefault(p => p.supplier_id == supplierId)?.supplier_name;
        }
    }
}
