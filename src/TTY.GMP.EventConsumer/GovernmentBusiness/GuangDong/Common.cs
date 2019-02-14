using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Government.GuangDong.Request;
using TTY.GMP.IBusiness;
using System.Linq;
using Newtonsoft.Json;
using TTY.GMP.Entity.Config;

namespace TTY.GMP.EventConsumer.GovernmentBusiness.GuangDong
{
    /// <summary>
    /// 公共处理类
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 创建经销商信息
        /// </summary>
        /// <param name="config"></param>
        /// <param name="agencyBll"></param>
        /// <param name="governmentGuangDongBll"></param>
        /// <param name="shop"></param>
        /// <param name="areas"></param>
        /// <returns></returns>
        public static async Task<GdCommonRequest> CreateAgency(GovernmentApiConfig config, IAgencyBLL agencyBll, IGovernmentGuangDongBLL governmentGuangDongBll, Shop shop, List<Area> areas)
        {
            var agency = await agencyBll.GetAgency(shop.ShopId);
            if (agency != null)
            {
                return new GdCommonRequest(agency.Id, agency.Code, agency.Name);
            }
            Area province = null;
            if (shop.Province != null)
            {
                province = areas.FirstOrDefault(p => p.AreaId == shop.Province.Value);
            }
            Area city = null;
            if (shop.City != null)
            {
                city = areas.FirstOrDefault(p => p.AreaId == shop.City.Value);
            }
            Area district = null;
            if (shop.District != null)
            {
                district = areas.FirstOrDefault(p => p.AreaId == shop.District.Value);
            }
            var addShopRequest = new GdAddShopRequest()
            {
                name = shop.ShopName,
                jyxkz = string.Empty,
                date = string.Empty,
                enddate = string.Empty,
                jyxkz_pic = string.Empty,
                headman = shop.ShopLinkMan,
                headphone = shop.ShopTelphone,
                linkman = shop.ShopLinkMan,
                linkphone = shop.ShopTelphone,
                address = shop.ShopAddress,
                yjdz = shop.ShopAddress,
                postcode = string.Empty,
                issd = 0,
                dpmj = new Random().Next(30, 100).ToString(),
                jyfw = string.Empty,
                isxzshop = 0,
                fzdw = string.Empty,
                account = string.Empty,
                password = string.Empty,
                province = province?.AreaName,
                city = city?.AreaName,
                country = district?.AreaName,
                town = string.Empty,
                provincecode = province?.AreaCode,
                citycode = city?.AreaCode,
                countrycode = district?.AreaCode,
                towncode = string.Empty,
                ckmj = new Random().Next(100, 150).ToString(),
                jsry = shop.ShopLinkMan,
                jsryphone = shop.ShopTelphone
            };
            var result = await governmentGuangDongBll.AddShopInfo(config.GuangDong.ApiAddress, config.GuangDong.QyCode, addShopRequest);
            if (string.IsNullOrEmpty(result.Shop.id))
            {
                throw new Exception($"经销商数据上传失败,参数{JsonConvert.SerializeObject(addShopRequest)}");
            }
            await agencyBll.AddAgency(new Agency()
            {
                ShopId = shop.ShopId,
                Id = result.Shop.id,
                Code = result.Shop.code,
                Name = result.Shop.name
            });
            return new GdCommonRequest(result.Shop.id, result.Shop.code, result.Shop.name);
        }
    }
}
