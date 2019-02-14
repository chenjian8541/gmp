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
    /// 进货单数据访问
    /// </summary>
    public class InReceiptDAL : IInReceiptDAL, IDbContext<ErpDbContext>, IAdoContract
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public InReceiptDAL(ISqlHelper sqlHelper)
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
        /// 获取进货单信息
        /// </summary>
        /// <param name="inId"></param>
        /// <returns></returns>
        public async Task<PoInReceipt> GetPoInReceipt(long inId)
        {
            return await this.Find<ErpDbContext, PoInReceipt>(inId);
        }

        /// <summary>
        /// 获取进货单详情
        /// </summary>
        /// <param name="inId"></param>
        /// <returns></returns>
        public async Task<List<PoInReceiptDetail>> GetPoInReceiptDetail(long inId)
        {
            return await this.FindList<ErpDbContext, PoInReceiptDetail>(p => p.in_id == inId);
        }

        /// <summary>
        /// 通过单据ID获取单据详情信息
        /// </summary>
        /// <param name="inId"></param>
        /// <returns></returns>
        public async Task<List<PoInReceiptDetailView>> GetPoInReceiptDetailView(long inId)
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
                     	po_in_receipt_detail a
                     INNER JOIN sys_goods b ON a.goods_id = b.goods_id
                     INNER JOIN sys_unit d ON a.unit_id = d.unit_id
                     LEFT JOIN base_toxicity_grade c ON b.toxicity_grade_id = c.toxicity_grade_id
                     WHERE
                     	a.in_id = " + inId;
            return (await this.ExecuteObjectAsync<PoInReceiptDetailView>(sql)).ToList();
        }
    }
}
