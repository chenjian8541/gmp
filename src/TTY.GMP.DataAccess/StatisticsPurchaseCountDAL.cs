using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.Database;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 采购单据数数据访问
    /// </summary>
    public class StatisticsPurchaseCountDAL : IStatisticsPurchaseCountDAL, IDbContext<GmpDbContext>, IAdoContract
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public StatisticsPurchaseCountDAL(ISqlHelper sqlHelper)
        {
            this._sqlHelper = sqlHelper;
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
        /// 新增采购单 单据统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddStatisticsPurchaseCount(StatisticsPurchaseCount model)
        {
            await this.Insert(model);
        }

        /// <summary>
        /// 获取最近统计时间
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetStatisticsPurchaseCountMaxTime()
        {
            var result = await this.ExecuteScalarAsync("select max(StartTime) from StatisticsPurchaseCount");
            return Convert.ToDateTime(result);
        }

        /// <summary>
        /// 获取门店采购单数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<StatisticsPurchaseCount>> GetStatisticsPurchaseCount(string shopIds, DateTime startTime, DateTime endTime)
        {
            var sql = $"SELECT * from StatisticsPurchaseCount where ShopId in ({shopIds}) and StartTime >= '{startTime.ToString("yyyy-MM-dd HH:mm:ss")}' and EndTime <='{endTime.ToString("yyyy-MM-dd HH:mm:ss")}'";
            var result = await this.ExecuteObjectAsync<StatisticsPurchaseCount>(sql);
            if (result != null && result.Any())
            {
                return result.ToList();
            }
            return new List<StatisticsPurchaseCount>();
        }

        /// <summary>
        /// 获取地区采购单数量
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <param name="limitShops"></param>
        /// <returns></returns>
        public async Task<List<StatisticsPurchaseCount>> GetStatisticsPurchaseCount(string areaIds, DateTime startTime,
            DateTime endTime, string level, string limitShops)
        {
            var areaLevelName = CommonLib.GetAreaLevelFieldName(level);
            var sql = $"SELECT * from StatisticsPurchaseCount where {areaLevelName} in ({areaIds}) and StartTime >= '{startTime.ToString("yyyy-MM-dd HH:mm:ss")}' and EndTime <='{endTime.ToString("yyyy-MM-dd HH:mm:ss")}'";
            if (!string.IsNullOrEmpty(limitShops))
            {
                sql += $" and ShopId in {limitShops}";
            }
            var result = await this.ExecuteObjectAsync<StatisticsPurchaseCount>(sql);
            if (result != null && result.Any())
            {
                return result.ToList();
            }
            return new List<StatisticsPurchaseCount>();
        }
    }
}
