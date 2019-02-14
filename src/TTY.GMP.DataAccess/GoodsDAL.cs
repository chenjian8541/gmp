using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.View;
using TTY.GMP.Entity.Web.Goods.Request;
using TTY.GMP.IDataAccess;
using System.Linq;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 商品数据访问
    /// </summary>
    public class GoodsDAL : IGoodsDAL, IAdoContract, IDbContext<ErpDbContext>
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private readonly ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public GoodsDAL(ISqlHelper sqlHelper)
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
        /// 获取数据访问上下文
        /// </summary>
        /// <returns></returns>
        public ErpDbContext GetDbContext()
        {
            return new ErpDbContext();
        }

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public async Task<Goods> GetGoods(long goodsId)
        {
            return await this.Find<ErpDbContext, Goods>(goodsId);
        }

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public async Task<GoodsView> GetGoodsView(long goodsId)
        {
            var result = await this.ExecuteObjectAsync<GoodsView>($"SELECT a.*,b.toxicity_grade_name FROM sys_goods a LEFT JOIN base_toxicity_grade b on a.toxicity_grade_id = b.toxicity_grade_id where a.goods_id = {goodsId}");
            return result.FirstOrDefault();
        }

        /// <summary>
        /// 查询销售商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<SaleGoodsView>, int>> GetSaleGoodsPage(GetSaleGoodsPageRequest request)
        {
            return await this.ExecutePageAsync<SaleGoodsView>(request.PageCurrent, request.PageSize, "a.goods_id AS GoodsId,b.goods_category_name as CategoryName,c.brand_name as BrandName,a.goods_name as GoodsName,a.goods_spec as GoodsSpec,a.registration_number as RegistrationNumber,a.registration_holder as RegistrationHolder,u.unit_name as UnitName,0 as Qty",
                $"sys_goods  a INNER JOIN sys_goods_category b on a.goods_class_id=b.goods_category_id inner join base_brand c  on a.brand_id=c.brand_id LEFT join  sys_unit u on a.base_unit_id = u.unit_id ", request.GetQuerySql(), "a.created_time desc");
        }

        /// <summary>
        /// 查询商品销售记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<SaleGoodsLogView>, int>> GetSaleGoodsLogPage(GetSaleGoodsLogPageRequest request)
        {
            return await this.ExecutePageAsync<SaleGoodsLogView>(request.PageCurrent, request.PageSize, "d.shop_name as ShopName,e.goods_name as GoodsName,e.goods_spec as GoodsSpec,e.registration_number as RegistrationNumber, b.qty as Qty,c.member_name as MemberName,c.member_tel as MemberTel,a.bill_date as BillDate,a.bill_code as BillCode,c.identification as Identification,f.goods_category_name as GoodsCategoryName,g.unit_name as UnitName",
                "so_retail a INNER JOIN so_retail_detail b on a.retail_id=b.bill_id INNER join cm_shop d on a.shop_id=d.shop_id INNER JOIN sys_goods e on b.goods_id=e.goods_id inner join sys_goods_category f on e.goods_class_id = f.goods_category_id inner join sys_unit g on b.unit_id = g.unit_id LEFT JOIN  mb_member_info c on a.member_id=c.member_id",
                request.GetQuerySql(), "a.bill_date desc");
        }

        /// <summary>
        /// 查询商品采购记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<ReceiptInLogView>, int>> GetReceiptInLogPage(ReceiptInLogPageRequest request)
        {
            return await this.ExecutePageAsync<ReceiptInLogView>(request.PageCurrent, request.PageSize, "h.shop_name as ShopName,g.supplier_name as SupplierName,c.brand_name as BrandName,b.goods_name as GoodsName,b.goods_spec as GoodsSpec,d.goods_category_name as GoodsCategoryName,a.qty as Qty,e.unit_name as UnitName,f.in_date as InDate,f.in_code as InCode"
                , "po_in_receipt_detail a inner join sys_goods b on a.goods_id = b.goods_id inner join base_brand c on b.brand_id = c.brand_id inner join sys_goods_category d on b.goods_class_id = d.goods_category_id inner join sys_unit e on a.unit_id = e.unit_id inner join po_in_receipt f on a.in_id = f.in_id inner join base_supplier g on f.in_offer_id = g.supplier_id inner join cm_shop h on f.user_shop_id = h.shop_id",
                request.GetQuerySql(), " f.in_date desc");
        }

        /// <summary>
        /// 获取仓库下多个商品的库存数量
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StStockQtyView>> GetStStockQtyView(List<long> goodsId, long stockId)
        {
            return await this.ExecuteObjectAsync<StStockQtyView>($"select goods_id as GoodsId,IFNULL(int_qty, 0) AS Qty from st_stock_qty where  stock_id = {stockId} and goods_id in ({string.Join(",", goodsId)})");
        }

        /// <summary>
        /// 获取库存信息
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="stockIds"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StStockQtyView>> GetStStockQtyView(List<long> goodsId, List<long> stockIds)
        {
            return await this.ExecuteObjectAsync<StStockQtyView>($"select stock_id as StockId,goods_id as GoodsId,IFNULL(int_qty, 0) AS Qty from st_stock_qty where  stock_id in ({string.Join(",", stockIds)}) and goods_id in ({string.Join(",", goodsId)})");
        }

        /// <summary>
        /// 通过ID获取商品类别信息
        /// </summary>
        /// <param name="goodsCategoryId"></param>
        /// <returns></returns>
        public async Task<List<GoodsCategory>> GetGoodsCategory(List<long> goodsCategoryId)
        {
            return await this.FindList<ErpDbContext, GoodsCategory>(p => goodsCategoryId.Contains(p.GoodsCategoryId));
        }

        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        public async Task<List<SysUnit>> GetUnits(List<long> units)
        {
            return await this.FindList<ErpDbContext, SysUnit>(p => units.Contains(p.UnitId));
        }

        /// <summary>
        /// 获取品牌信息
        /// </summary>
        /// <param name="brandIds"></param>
        /// <returns></returns>
        public async Task<List<GoodsBrand>> GetGoodsBrands(List<long> brandIds)
        {
            return await this.FindList<ErpDbContext, GoodsBrand>(p => brandIds.Contains(p.BrandId));
        }
    }
}
