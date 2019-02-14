using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.Database;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 门店销售单排名数据访问
    /// </summary>
    public class ShopRetailRankDAL : IShopRetailRankDAL, IDbContext<GmpDbContext>, IAdoContract
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public ShopRetailRankDAL(ISqlHelper sqlHelper)
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
            return sqlHelper.GetGmpConnection();
        }

        /// <summary>
        /// 获取数据访问上下文
        /// </summary>
        /// <returns></returns>
        public GmpDbContext GetDbContext()
        {
            return new GmpDbContext();
        }

        /// <summary>
        /// 批量新增门店销售单排名数据
        /// </summary>
        /// <param name="shopRetailRanks"></param>
        /// <returns></returns>
        public async Task AddShopRetailRank(List<ShopRetailRank> shopRetailRanks)
        {
            await this.InsertRange(shopRetailRanks);
        }

        /// <summary>
        /// 删除此时间之前的排名数据
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task DelShopRetailRank(DateTime time)
        {
            await this.ExecuteAsync($"DELETE FROM shopretailrank WHERE EndTime <= '{time.ToString("yyyy-MM-dd HH:mm:ss")}'");
        }

        /// <summary>
        /// 标记之前的数据
        /// </summary>
        /// <returns></returns>
        public async Task MarkShopRetailRankOld()
        {
            await this.ExecuteAsync("UPDATE shopretailrank set IsLast = 0 where IsLast = 1");
        }

        /// <summary>
        /// 获取上一次排名情况
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ShopRetailRank>> GetLastShopRetailRank(int type)
        {
            return await this.ExecuteObjectAsync<ShopRetailRank>($"SELECT * from shopretailrank where IsLast = 1 and  Type = {type}");
        }
    }
}
