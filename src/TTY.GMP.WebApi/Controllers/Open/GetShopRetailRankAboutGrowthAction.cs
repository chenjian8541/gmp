using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Config;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Web.Open.Response;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;

namespace TTY.GMP.WebApi.Controllers.Open
{
    /// <summary>
    /// 获取门店销售单排名信息
    /// </summary>
    public class GetShopRetailRankAboutGrowthAction
    {
        /// <summary>
        /// 门店销售单排名业务
        /// </summary>
        private readonly IShopRetailRankBLL _shopRetailRankBll;

        /// <summary>
        /// 门店销售单排名限制名单
        /// </summary>
        private readonly IShopRetailRankLimitBLL _shopRetailRankLimitBll;

        /// <summary>
        /// 门店排名配置
        /// </summary>
        private readonly ShopRetailRankConfig _shopRetailRankConfig;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopRetailRankBll"></param>
        /// <param name="shopRetailRankLimitBll"></param>
        public GetShopRetailRankAboutGrowthAction(IShopRetailRankBLL shopRetailRankBll, IShopRetailRankLimitBLL shopRetailRankLimitBll)
        {
            this._shopRetailRankBll = shopRetailRankBll;
            this._shopRetailRankLimitBll = shopRetailRankLimitBll;
            _shopRetailRankConfig = CustomServiceLocator.GetInstance<IAppConfigurtaionServices>().AppSettings.ShopRetailRankConfig;
        }

        /// <summary>
        /// 获取门店销售单排名信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(long shopId)
        {
            var ranksAll = await _shopRetailRankBll.GetLastShopRetailRank((int)ShopRetailRankTypeEnum.Growth);
            if (!ranksAll.Any())
            {
                return new ResponsePagingBase().GetResponseError(StatusCode.ShopRetailRank60001, "未查询到排名信息");
            }
            var ranksValid = ranksAll.Where(p => p.IsLimit == false).OrderBy(p => p.Rank).ToList();
            var myrank = ranksAll.FirstOrDefault(p => p.ShopId == shopId);
            if (ranksValid.Count > _shopRetailRankConfig.ShowCount)
            {
                ranksValid = ranksValid.GetRange(0, _shopRetailRankConfig.ShowCount);
            }
            var responseView = GetResponseView(myrank, ranksAll, ranksValid);
            var limitShops = await _shopRetailRankLimitBll.GetShopRetailRankWinner(shopId);
            if (limitShops != null)
            {
                return GetIsWinnerResponse(responseView);
            }
            if (myrank == null)
            {
                return GetBillCountZero(responseView);
            }
            if (myrank.BillCount >= _shopRetailRankConfig.MinBillCountGrowth)
            {
                return GetBillCountEnough(responseView);
            }
            return GetBillCountNotEnough(responseView);
        }

        /// <summary>
        /// 获取基本的返回信息
        /// </summary>
        /// <param name="myrank"></param>
        /// <param name="ranksAll"></param>
        /// <param name="ranksValid"></param>
        /// <returns></returns>
        private GetShopRetailRankAboutGrowthView GetResponseView(ShopRetailRank myrank, List<ShopRetailRank> ranksAll, List<ShopRetailRank> ranksValid)
        {
            var respnseView = new GetShopRetailRankAboutGrowthView()
            {
                BillCount = myrank == null ? 0 : myrank.BillCount,
                StartTime = ranksAll[0].StartTime,
                EndTime = ranksAll[0].EndTime,
                Rank = myrank == null ? 0 : myrank.Rank
            };
            respnseView.ShopRetailRankViews = ranksValid.Select(p => new ShopRetailRankView()
            {
                BillCount = p.BillCount,
                Rank = p.Rank,
                ShopLinkMan = p.ShopLinkMan,
                ShopTelphone = p.ShopTelphone,
                ProvinceName = p.ProvinceName,
                CityName = p.CityName,
                DistrictName = p.DistrictName
            }).ToList();
            var gc = new GregorianCalendar();
            var weekOfYear1 = gc.GetWeekOfYear(_shopRetailRankConfig.KingStartTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var weekOfYear2 = gc.GetWeekOfYear(ranksAll[0].StartTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            respnseView.Week = weekOfYear2 - weekOfYear1 + 1;
            return respnseView;
        }

        /// <summary>
        /// 已获得成长之星
        /// </summary>
        /// <returns></returns>
        private ResponseBase GetIsWinnerResponse(GetShopRetailRankAboutGrowthView responseView)
        {
            responseView.Type = (int)GetShopRetailRankAboutGrowthTypeEnum.IsWinner;
            return ResponseBase.Success(responseView);
        }

        /// <summary>
        /// 返回订单数为0的情况
        /// </summary>
        /// <param name="responseView"></param>
        /// <returns></returns>
        private ResponseBase GetBillCountZero(GetShopRetailRankAboutGrowthView responseView)
        {
            responseView.Type = (int)GetShopRetailRankAboutGrowthTypeEnum.Zero;
            return ResponseBase.Success(responseView);
        }

        /// <summary>
        /// 订单数足够
        /// </summary>
        /// <param name="responseView"></param>
        /// <returns></returns>
        private ResponseBase GetBillCountEnough(GetShopRetailRankAboutGrowthView responseView)
        {
            responseView.Type = (int)GetShopRetailRankAboutGrowthTypeEnum.Enough;
            return ResponseBase.Success(responseView);
        }

        /// <summary>
        /// 订单数不够
        /// </summary>
        /// <param name="responseView"></param>
        /// <returns></returns>
        private ResponseBase GetBillCountNotEnough(GetShopRetailRankAboutGrowthView responseView)
        {
            responseView.Type = (int)GetShopRetailRankAboutGrowthTypeEnum.NotEnough;
            responseView.NeedBillCount = _shopRetailRankConfig.MinBillCountGrowth - responseView.BillCount;
            return ResponseBase.Success(responseView);
        }
    }
}
