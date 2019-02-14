using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.Database;
using TTY.GMP.IDataAccess;
using System.Linq;
using TTY.GMP.Entity.View;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 零售订单数据访问层
    /// </summary>
    public class RetailDAL : IRetailDAL, IDbContext<ErpDbContext>, IAdoContract
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public RetailDAL(ISqlHelper sqlHelper)
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
        /// 通过单据ID获取单据信息
        /// </summary>
        /// <param name="retailId"></param>
        /// <returns></returns>
        public async Task<SoRetail> GetSoRetail(long retailId)
        {
            return await this.Find<ErpDbContext, SoRetail>(retailId);
        }

        /// <summary>
        /// 通过单据ID获取单据详情信息
        /// </summary>
        /// <param name="retailId"></param>
        /// <returns></returns>
        public async Task<List<SoRetailDetailView>> GetSoRetailDetail(long retailId)
        {
            var sql = @"SELECT
                        	a.*, c.toxicity_grade_name,
                        	b.goods_name,
                        	b.goods_code,
                        	b.dosage_forms,
                        	b.registration_number,
                        	b.registration_holder,
                        	b.goods_product,
                        	b.goods_spec,
                        	b.goods_restrictive,
                        	d.unit_name
                        FROM
                        	so_retail_detail a
                        INNER JOIN sys_goods b ON a.goods_id = b.goods_id
                        INNER JOIN sys_unit d ON a.unit_id = d.unit_id
                        LEFT JOIN base_toxicity_grade c ON b.toxicity_grade_id = c.toxicity_grade_id
                        where bill_id = " + retailId;
            return (await this.ExecuteObjectAsync<SoRetailDetailView>(sql)).ToList();
        }

        /// <summary>
        /// 通过单据ID获取购买客户信息
        /// </summary>
        /// <param name="retailId"></param>
        /// <returns></returns>
        public async Task<CmRetailCustomer> GetCmRetailCustomer(long retailId)
        {
            var bindDetail = await this.Find<ErpDbContext, SoRetailInfo>(p => p.retail_id == retailId);
            if (bindDetail == null || bindDetail.retail_customer_id == null)
            {
                return null;
            }
            return await this.Find<ErpDbContext, CmRetailCustomer>(p => p.RetailCustomerId == bindDetail.retail_customer_id.Value);
        }
    }
}
