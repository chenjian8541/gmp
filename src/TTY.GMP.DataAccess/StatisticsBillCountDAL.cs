using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.View;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 订单数统计
    /// </summary>
    public class StatisticsBillCountDAL : IStatisticsBillCountDAL, IAdoContract
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public StatisticsBillCountDAL(ISqlHelper sqlHelper)
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
        /// 零售单统计
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private async Task<List<DbStatisticsRetailBillCount>> GetRetailBillCount(long startTime, long endTime)
        {
            var sql = $@"SELECT
                            	c.org_id AS OrgId,
                            	c.shop_id AS ShopId,
                            	c.shop_name AS ShopName,
                            	c.province AS Province,
                            	c.city AS City,
                            	c.district AS District,
                            	c.street AS Street,
                            	b.BillCount
                            FROM
                            	(
                            		SELECT
                            			shop_id,
                            			count(0) AS BillCount
                            		FROM
                            			so_retail a
                            		WHERE
                            			a.bill_date >= {startTime}
                            		AND a.bill_date <= {endTime} AND a.red = 0 
                            		GROUP BY
                            			shop_id
                            	) AS b
                            INNER JOIN cm_shop c ON b.shop_id = c.shop_id";
            var result = await this.ExecuteObjectAsync<DbStatisticsRetailBillCount>(sql);
            if (result != null && result.Any())
            {
                return result.ToList();
            }
            return new List<DbStatisticsRetailBillCount>();
        }

        /// <summary>
        /// 零售退货单统计
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private async Task<List<DbStatisticsRetailBillCount>> GetRetailBackBillCount(long startTime, long endTime)
        {
            var sql = $@"SELECT
                            	c.org_id AS OrgId,
                            	c.shop_id AS ShopId,
                            	c.shop_name AS ShopName,
                            	c.province AS Province,
                            	c.city AS City,
                            	c.district AS District,
                            	c.street AS Street,
                            	b.BillCount
                            FROM
                            	(
                            		SELECT
                            			shop_id,
                            			count(0) AS BillCount
                            		FROM
                            			so_retail_back a
                            		WHERE
                            			a.bill_date >= {startTime}
                            		AND a.bill_date <= {endTime} and a.red = 0
                            		GROUP BY
                            			shop_id
                            	) AS b
                            INNER JOIN cm_shop c ON b.shop_id = c.shop_id";
            var result = await this.ExecuteObjectAsync<DbStatisticsRetailBillCount>(sql);
            if (result != null && result.Any())
            {
                return result.ToList();
            }
            return new List<DbStatisticsRetailBillCount>();
        }

        /// <summary>
        /// 获取零售单数统计(包括商品零售、零售退货)
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<DbStatisticsRetailBillCount>> GetAllRetailBillCount(long startTime, long endTime)
        {
            var retailBillCount = await GetRetailBillCount(startTime, endTime);
            var retailBackBill = await GetRetailBackBillCount(startTime, endTime);
            retailBillCount.AddRange(retailBackBill);
            var result = from a in retailBillCount
                         group a by new { a.OrgId, a.ShopId, a.ShopName, a.Province, a.City, a.District, a.Street }
                into g
                         select new DbStatisticsRetailBillCount()
                         {
                             BillCount = g.Sum(p => p.BillCount),
                             City = g.Key.City,
                             District = g.Key.District,
                             OrgId = g.Key.OrgId,
                             Province = g.Key.Province,
                             ShopId = g.Key.ShopId,
                             ShopName = g.Key.ShopName,
                             Street = g.Key.Street
                         };
            return result.ToList();
        }

        /// <summary>
        /// 采购订单数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private async Task<List<DbStatisticsPurchaseBillCount>> GetReceiptInBillCount(long startTime, long endTime)
        {
            var sql = $@"SELECT
                                	c.org_id AS OrgId,
                                	c.shop_id AS ShopId,
                                	c.shop_name AS ShopName,
                                	c.province AS Province,
                                	c.city AS City,
                                	c.district AS District,
                                	c.street AS Street,
                                	b.BillCount
                                FROM
                                	(
                                		SELECT
                                			stock_id,
                                			count(0) AS BillCount
                                		FROM
                                			po_in_receipt a
                                		WHERE
                                			a.in_date >= {startTime}
                                		AND a.in_date <= {endTime} and a.red = 0
                                		GROUP BY
                                			stock_id
                                	) AS b
                                INNER JOIN cm_shop c ON b.stock_id = c.stock_id";
            var result = await this.ExecuteObjectAsync<DbStatisticsPurchaseBillCount>(sql);
            if (result != null && result.Any())
            {
                return result.ToList();
            }
            return new List<DbStatisticsPurchaseBillCount>();
        }

        /// <summary>
        /// 采购退货订单数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private async Task<List<DbStatisticsPurchaseBillCount>> GetReceiptBackBillCount(long startTime, long endTime)
        {
            var sql = $@"SELECT
                                	c.org_id AS OrgId,
                                	c.shop_id AS ShopId,
                                	c.shop_name AS ShopName,
                                	c.province AS Province,
                                	c.city AS City,
                                	c.district AS District,
                                	c.street AS Street,
                                	b.BillCount
                                FROM
                                	(
                                		SELECT
                                			stock_id,
                                			count(0) AS BillCount
                                		FROM
                                			po_back_receipt a
                                		WHERE
                                			a.back_date >= {startTime}
                                		AND a.back_date <= {endTime} and a.receipt_back_red = 0
                                		GROUP BY
                                			stock_id
                                	) AS b
                                INNER JOIN cm_shop c ON b.stock_id = c.stock_id";
            var result = await this.ExecuteObjectAsync<DbStatisticsPurchaseBillCount>(sql);
            if (result != null && result.Any())
            {
                return result.ToList();
            }
            return new List<DbStatisticsPurchaseBillCount>();
        }

        /// <summary>
        /// 获取采购单数(采购、采购退货)
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<DbStatisticsPurchaseBillCount>> GetAllPurchaseBillCount(long startTime, long endTime)
        {
            var receiptInBillCount = await GetReceiptInBillCount(startTime, endTime);
            var receiptBackBillCount = await GetReceiptBackBillCount(startTime, endTime);
            receiptInBillCount.AddRange(receiptBackBillCount);
            var result = from a in receiptInBillCount
                         group a by new { a.OrgId, a.ShopId, a.ShopName, a.Province, a.City, a.District, a.Street }
                into g
                         select new DbStatisticsPurchaseBillCount()
                         {
                             BillCount = g.Sum(p => p.BillCount),
                             City = g.Key.City,
                             District = g.Key.District,
                             OrgId = g.Key.OrgId,
                             Province = g.Key.Province,
                             ShopId = g.Key.ShopId,
                             ShopName = g.Key.ShopName,
                             Street = g.Key.Street
                         };
            return result.ToList();
        }
    }
}
