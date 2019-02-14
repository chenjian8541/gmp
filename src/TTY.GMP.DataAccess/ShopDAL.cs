using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.DataCore;
using TTY.GMP.IDataAccess;
using System.Linq;
using TTY.GMP.ICache;
using TTY.GMP.Entity.Database;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Web.Shop.Request;
using TTY.GMP.Utility;
using System.Data.Common;
using System.Threading.Tasks;
using TTY.GMP.Entity.View;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 门店数据访问
    /// </summary>
    public class ShopDAL : IShopDAL, IDbContext<ErpDbContext>, IAdoContract
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public ShopDAL(ISqlHelper sqlHelper)
        {
            this._sqlHelper = sqlHelper;
        }

        /// <summary>
        /// 获取数据访问上下文
        /// </summary>
        /// <returns></returns>
        public ErpDbContext GetDbContext()
        {
            return new ErpDbContext();
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="sqlHelper"></param>
        /// <returns></returns>
        public DbConnection GetDbConnection(out ISqlHelper sqlHelper)
        {
            sqlHelper = _sqlHelper;
            return _sqlHelper.GetErpSqlConnection();
        }

        /// <summary>
        /// 查询门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<Shop>, int>> GetShopPage(GetShopPageRequest request)
        {
            return await this.ExecutePageAsync<Shop>(request.PageCurrent, request.PageSize,
                "shop_linkMan as ShopLinkMan,shop_id as ShopId,shop_code as ShopCode,shop_name as ShopName,stock_id as StockId,org_id as OrgId,cash_id as CashId,bank_id as BankId,member_pay_id as MemberPayId,status as Status,pycode as Pycode,shop_telphone as ShopTelphone,shop_address as ShopAddress,creater,createdtime,modifier,modifiedtime,province,city,district,street",
                "cm_shop", request.GetQuerySql(), "createdtime desc");
        }

        /// <summary>
        /// 通过门店id集合查询门店
        /// 各id之间以“,”隔开
        /// </summary>
        /// <param name="strIds"></param>
        /// <returns></returns>
        public async Task<List<Shop>> GetShopByIds(string strIds)
        {
            if (string.IsNullOrEmpty(strIds))
            {
                return new List<Shop>();
            }
            var ids = strIds.Split(',').Select(p => p.ToLong());
            return await this.FindList<ErpDbContext, Shop>(p => ids.Contains(p.ShopId));
        }

        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<Shop> GetShop(long shopId)
        {
            return await this.Find<ErpDbContext, Shop>(shopId);
        }

        /// <summary>
        /// 获取门店零售单排名信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<ShopRetailRankView>> GetShopRetailRank(long startTime, long endTime)
        {
            var sql = string.Format(@"SELECT
                s.shop_id as ShopId,
	            s.shop_linkMan as ShopLinkMan,
	            s.shop_telphone as ShopTelphone,
                s.shop_address as ShopAddress,
	            a.bill_count as BillCount,
	            a.org_id as OrgId,
	            s.shop_name as ShopName,
	            r1.area_name AS ProvinceName,
	            r2.area_name AS CityName,
	            r3.area_name AS DistrictName
            FROM
	            (
		            SELECT
			            r.shop_id,
			            r.org_id,
			            count(a.bill_id) AS bill_count
		            FROM
			            (
				            SELECT DISTINCT
					            (d.bill_id) AS bill_id
				            FROM
					            so_retail_detail d
				            INNER JOIN sys_goods g ON d.goods_id = g.goods_id
				            AND g.goods_is_service = 0
			            ) a
		            INNER JOIN so_retail r ON a.bill_id = r.retail_id
		            WHERE
			            r.created_date >= {0} and r.created_date <= {1}
		            GROUP BY
			            r.shop_id,
			            r.org_id
		            ORDER BY
			            count(a.bill_id) DESC
	            ) a
            INNER JOIN cm_shop s ON a.shop_id = s.shop_id 
            LEFT JOIN base_area r1 ON s.province = r1.area_id
            LEFT JOIN base_area r2 ON s.city = r2.area_id
            LEFT JOIN base_area r3 ON s.district = r3.area_id", startTime, endTime);
            var result = await this.ExecuteObjectAsync<ShopRetailRankView>(sql);
            return result.ToList();
        }
    }
}
