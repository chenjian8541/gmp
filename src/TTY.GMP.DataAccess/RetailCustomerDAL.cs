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
    public class RetailCustomerDAL : IRetailCustomerDAL, IAdoContract
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public RetailCustomerDAL(ISqlHelper sqlHelper)
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
        /// 获取客户信息
        /// </summary>
        /// <param name="retailIds"></param>
        /// <returns></returns>
        public async Task<List<DbRetailCustomerView>> GetRetailCustomer(List<long> retailIds)
        {
            var sql = $@"SELECT
                             	j.retail_customer_name AS RetailCustomerName,
                            	j.retail_customer_tel AS RetailCustomerTel,
                            	j.identification AS Identification,
                            	g.retail_id AS RetailId
                            FROM
                            	so_retail_info g
                            INNER JOIN cm_retail_customer j ON g.retail_customer_id = j.retail_customer_id 
                            where g.retail_id in ({string.Join(",", retailIds)})";
            return (await this.ExecuteObjectAsync<DbRetailCustomerView>(sql)).ToList();
        }
    }
}
