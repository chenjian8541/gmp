using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.View;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 库存数据统计
    /// </summary>
    public class StockQtyDAL : IStockQtyDAL, IAdoContract
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public StockQtyDAL(ISqlHelper sqlHelper)
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
        /// 获取最近修改的库存信息
        /// </summary>
        /// <param name="lastUpdateTime"></param>
        /// <returns></returns>
        public async Task<List<DbStockGoodsView>> GetRecentModifyStockGoods(long lastUpdateTime)
        {
            var sql = $@"SELECT
                        	a.org_id AS OrgId,
                        	b.shop_id AS ShopId,
                        	a.goods_id AS GoodsId,
                        	d.goods_category_id AS GoodsCategoryId,
                        	b.province AS Province,
                        	b.city AS City,
                        	b.district AS District,
                        	b.street AS Street,
                        	b.shop_name AS ShopName,
                        	c.goods_name AS GoodsName,
                        	c.goods_spec AS GoodsSpec,
                        	c.contents AS GoodsContents,
                        	d.goods_category_name AS GoodsCategoryName,
                        	a.int_qty AS TotalCount
                        FROM
                        	st_stock_qty a
                        INNER JOIN cm_shop b ON a.stock_id = b.stock_id
                        INNER JOIN sys_goods c ON a.goods_id = c.goods_id
                        INNER JOIN sys_goods_category d ON c.goods_class_id = d.goods_category_id
                        WHERE
                        	a.created_time > {lastUpdateTime}
                        OR a.modified_time > {lastUpdateTime}";
            var result = await this.ExecuteObjectAsync<DbStockGoodsView>(sql);
            if (result != null && result.Any())
            {
                return result.ToList();
            }
            return null;
        }
    }
}
