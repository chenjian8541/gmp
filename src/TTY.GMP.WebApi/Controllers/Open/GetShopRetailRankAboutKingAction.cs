using System;
using System.Collections.Generic;
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
    /// 获取门店销售单排名信息(订单王)
    /// </summary>
    public class GetShopRetailRankAboutKingAction
    {
        /// <summary>
        /// 门店销售单排名业务
        /// </summary>
        private readonly IShopRetailRankBLL _shopRetailRankBll;

        /// <summary>
        /// 门店排名配置
        /// </summary>
        private readonly ShopRetailRankConfig _shopRetailRankConfig;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopRetailRankBll"></param>
        public GetShopRetailRankAboutKingAction(IShopRetailRankBLL shopRetailRankBll)
        {
            this._shopRetailRankBll = shopRetailRankBll;
            _shopRetailRankConfig = CustomServiceLocator.GetInstance<IAppConfigurtaionServices>().AppSettings.ShopRetailRankConfig;
        }

        /// <summary>
        /// 获取订单王数据
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(long shopId)
        {
            var ranksAll = await _shopRetailRankBll.GetLastShopRetailRank((int)ShopRetailRankTypeEnum.King);
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
            if (myrank == null)
            {
                responseView.Type = (int)GetShopRetailRankAboutKingTypeEnum.Zero;
                return ResponseBase.Success(responseView);
            }
            if (myrank.BillCount < _shopRetailRankConfig.MinBillCountKing)
            {
                responseView.Type = (int)GetShopRetailRankAboutKingTypeEnum.NotEnough;
                responseView.NeedBillCount = _shopRetailRankConfig.MinBillCountKing - myrank.BillCount;
                return ResponseBase.Success(responseView);
            }
            responseView.Type = (int)GetShopRetailRankAboutKingTypeEnum.Enough;
            return ResponseBase.Success(responseView);
        }

        /// <summary>
        /// 获取基本的返回信息
        /// </summary>
        /// <param name="myrank"></param>
        /// <param name="ranksAll"></param>
        /// <param name="ranksValid"></param>
        /// <returns></returns>
        private GetShopRetailRankAboutKingView GetResponseView(ShopRetailRank myrank, List<ShopRetailRank> ranksAll, List<ShopRetailRank> ranksValid)
        {
            var respnseView = new GetShopRetailRankAboutKingView()
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
            return respnseView;
        }
    }
}
