using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.CacheBucket;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.View;
using TTY.GMP.ICache;
using TTY.GMP.IDataAccess;
using TTY.GMP.Utility;
using System.Linq;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 门店统计数据访问
    /// </summary>
    public class ShopStatisticsDAL : BaseCacheDAL<ShopStatisticsBucket>, IShopStatisticsDAL, IAdoContract
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheProvider"></param>
        /// <param name="sqlHelper"></param>
        public ShopStatisticsDAL(ICacheProvider cacheProvider, ISqlHelper sqlHelper) : base(cacheProvider)
        {
            this._sqlHelper = sqlHelper;
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="sqlHelper"></param>
        /// <returns></returns>
        public DbConnection GetDbConnection(out ISqlHelper sqlHelper)
        {
            sqlHelper = _sqlHelper;
            return sqlHelper.GetErpSqlConnection();
        }

        /// <summary>
        /// 从数据库中获取数据
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected override async Task<ShopStatisticsBucket> GetDb(params object[] keys)
        {
            var type = keys[0].ToLong();
            var strSql = new StringBuilder();
            if (type != 0)
            {
                strSql.AppendFormat(@"SELECT
	                                        b.area_id AS Province,
	                                        b.area_code AS ProvinceNumber,
	                                        b.area_name AS ProvinceName,
	                                        c.area_id as City,
	                                        c.area_code as CityNumber,
	                                        c.area_name as CityName,
	                                        SUM(a.count) AS ShopCount,
	                                        CASE
                                                WHEN (SELECT SUM(i.count) FROM (SELECT province,city,district,1 as count from cm_shop 
											            UNION ALL
                                                    SELECT province,city,district,count from cm_shop_count) i WHERE province != 0) = 0 THEN 0
                                                ELSE ROUND(
		                                                SUM(a.count) / (SELECT SUM(u.count) FROM (SELECT province,city,district,1 as count from cm_shop 
													                                            UNION ALL 
												                                            SELECT province,city,district,count from cm_shop_count) u WHERE province != 0) * 100, 4)
                                                END
                                            AS ShopRatio
                                        FROM
	                                        ( SELECT province,city,district,1 as count from cm_shop
			                                    UNION ALL 
	                                            SELECT province,city,district,count from cm_shop_count ) a
                                        LEFT JOIN base_area b ON a.province = b.area_id 
                                        LEFT JOIN base_area c ON a.city = c.area_id
                                        WHERE 
	                                        province != 0
                                        GROUP BY
	                                        b.area_id,c.area_id
                                        ORDER BY
	                                        ShopRatio DESC");
            }
            else
            {
                strSql.AppendFormat(@"SELECT
	                                        b.area_id AS Province,
	                                        b.area_code AS ProvinceNumber,
	                                        b.area_name AS ProvinceName,
	                                        SUM(a.count) AS ShopCount,
                                            0 as City,
	                                        0 as CityNumber,
	                                        '' as CityName,
	                                        CASE
                                                WHEN (SELECT SUM(i.count) FROM (SELECT province,city,district,1 as count from cm_shop 
												        UNION ALL
											        SELECT province,city,district,count from cm_shop_count) i WHERE province != 0) = 0 THEN 0
	                                            ELSE ROUND(
			                                        SUM(a.count) / (SELECT SUM(u.count) FROM (SELECT province,city,district,1 as count from cm_shop 
                                                                            UNION ALL 
												                        SELECT province,city,district,count from cm_shop_count) u WHERE province != 0) * 100, 4)
	                                            END
                                            AS ShopRatio
                                        FROM 
	                                        ( SELECT province,city,district,1 as count from cm_shop
			                                    UNION ALL 
	                                         SELECT province,city,district,count from cm_shop_count ) a
                                        RIGHT JOIN base_area b ON a.province = b.area_id 
                                        LEFT JOIN base_area c ON a.city = c.area_id
                                        WHERE
	                                        a.province != 0 and b.`level` = 'province'
                                        GROUP BY
	                                        b.area_id
                                        ORDER BY
	                                        ShopRatio DESC");
            }
            var result = await this.ExecuteObjectAsync<ShopStatisticsView>(strSql.ToString());
            var shopStatistics = result.ToList();
            if (shopStatistics != null && shopStatistics.Count > 0)
            {
                return new ShopStatisticsBucket() { ShopStatistics = shopStatistics };
            }
            return null;
        }

        /// <summary>
        /// 获取门店统计信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<ShopStatisticsView>> GetShopStatistics(ShopStatisticsTypeEnum type)
        {
            var buckt = await GetCache((int)type);
            if (buckt != null)
            {
                return buckt.ShopStatistics;
            }
            return null;
        }

        /// <summary>
        /// 更新全国门店统计信息
        /// </summary>
        /// <param name="type"></param>
        public async Task UpdateShopStatistics(ShopStatisticsTypeEnum type)
        {
            await UpdateCache((int)type);
        }
    }
}
