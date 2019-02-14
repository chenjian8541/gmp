using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.View;
using TTY.GMP.ICache;
using TTY.GMP.IDataAccess;
using System.Linq;

namespace TTY.GMP.DataAccess
{
    public class StockInOutRecordDAL : IStockInOutRecordDAL, IAdoContract
    {

        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public StockInOutRecordDAL(ISqlHelper sqlHelper)
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
        /// 获取商品出入库统计
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="billTypes"></param>
        /// <returns></returns>
        public async Task<List<StockInOutView>> GetStockInOut(long startTime, long endTime, string billTypes)
        {
            string sql = $@"SELECT
                            	a.org_id AS OrgId,
                            	d.shop_id AS ShopId,
                            	a.goods_id AS GoodsId,
                            	c.goods_category_id AS GoodsCategoryId,
                            	d.province AS Province,
                            	d.city AS City,
                            	d.district AS District,
                            	d.street AS Street,
                            	d.shop_name AS ShopName,
                            	b.goods_name AS GoodsName,
                            	b.goods_spec AS GoodsSpec,
                            	b.contents AS GoodsContents,
                            	c.goods_category_name AS GoodsCategoryName,
                            	a.sum AS TotalCount
                            FROM
                            	(
                            		SELECT
                            			stock_id,
                            			goods_id,
                            			org_id,
                            			sum(qty) AS sum
                            		FROM
                            			st_in_out_stock_record
                            		WHERE
                            			bill_type IN ({billTypes}) and out_in_date >={startTime} and out_in_date <={endTime}
                            		GROUP BY
                            			stock_id,
                            			goods_id,
                            			org_id
                            	) AS a
                            INNER JOIN sys_goods b ON a.goods_id = b.goods_id
                            INNER JOIN sys_goods_category c ON b.goods_class_id = c.goods_category_id
                            INNER JOIN cm_shop d ON a.stock_id = d.stock_id";
            var result = await this.ExecuteObjectAsync<StockInOutView>(sql);
            if (result != null && result.Any())
            {
                return result.ToList();
            }
            return null;
        }
    }
}
