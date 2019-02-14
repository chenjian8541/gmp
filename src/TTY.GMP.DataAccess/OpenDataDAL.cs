using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Open.database;
using TTY.GMP.Entity.Open.View;
using TTY.GMP.Entity.View;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 开放数据 数据访问层
    /// </summary>
    public class OpenDataDAL : IOpenDataDAL, IAdoContract
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public OpenDataDAL(ISqlHelper sqlHelper)
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
        /// 分页获取商品及库存信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<DbGoodsView>, int>> GetGoodsPaging<T>(T request) where T : RequestPagingBase, IQuerySql
        {
            return await this.ExecutePageAsync<DbGoodsView>(request.PageCurrent, request.PageSize, "a.brand_id as BrandId,a.base_unit_id as BaseUnitId,c.stock_id as StockId,a.goods_id as GoodsId,a.goods_class_id as GoodsCategoryId,a.goods_name as GoodsName,a.goods_code as GoodsCode,a.goods_short_name as GoodsShortName,a.goods_spec as GoodsSpec,a.goods_origin as GoodsOrigin,a.goods_ingredient as GoodsIngredient,a.dosage_forms as DosageForms,a.contents as Contents,a.`comment` as `Comment`,a.registration_holder as RegistrationHolder,a.registration_number as RegistrationNumber,a.goods_product as GoodsProduct,a.goods_restrictive as GoodsRestrictive,c.shop_name as ShopName,c.province,c.city,c.district,c.street ",
                "sys_goods a inner join cm_shop c on c.org_id = a.org_id", request.GetQuerySql(), "a.created_time desc");
        }

        /// <summary>
        /// 分页获取商品采购信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<InReceiptDetail>, int>> GetInReceiptDetail<T>(T request) where T : RequestPagingBase, IQuerySql
        {
            return await this.ExecutePageAsync<InReceiptDetail>(request.PageCurrent, request.PageSize, "a.goods_id AS GoodsId,a.qty AS Qty,f.in_code AS InCode,f.in_date AS InDate,h.shop_id AS ShopId,h.shop_name AS ShopName,h.province AS Province,h.city AS City,h.district AS District,h.street AS Street,f.in_offer_id AS InOfferId,a.unit_id AS UnitId,b.goods_name AS GoodsName,b.goods_class_id AS GoodsCategoryId,b.brand_id AS BrandId,b.goods_spec AS GoodsSpec",
                "po_in_receipt_detail a INNER JOIN sys_goods b ON a.goods_id = b.goods_id INNER JOIN po_in_receipt f ON a.in_id = f.in_id INNER JOIN cm_shop h ON f.user_shop_id = h.shop_id", request.GetQuerySql(), "f.in_date desc");
        }

        /// <summary>
        /// 分页获取商品采购退货信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<BackReceiptDetail>, int>> GetBackReceiptDetail<T>(T request) where T : RequestPagingBase, IQuerySql
        {
            return await this.ExecutePageAsync<BackReceiptDetail>(request.PageCurrent, request.PageSize, "a.goods_id AS GoodsId,a.qty AS Qty,f.back_code AS BackCode,f.back_date AS BackDate,h.shop_id AS ShopId,h.shop_name AS ShopName,h.province AS Province,h.city AS City,h.district AS District,h.street AS Street,f.back_offer_id AS BackOfferId,a.unit_id AS UnitId,b.goods_name AS GoodsName,b.goods_class_id AS GoodsCategoryId,b.brand_id AS BrandId,b.goods_spec AS GoodsSpec",
                "po_back_receipt_detail a INNER JOIN sys_goods b ON a.goods_id = b.goods_id INNER JOIN po_back_receipt f ON a.back_id = f.back_id INNER JOIN cm_shop h ON f.user_shop_id = h.shop_id", request.GetQuerySql(), "f.back_date desc");
        }

        /// <summary>
        /// 分页获取商品销售信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<RetailDetail>, int>> GetRetailDetail<T>(T request) where T : RequestPagingBase, IQuerySql
        {
            return await this.ExecutePageAsync<RetailDetail>(request.PageCurrent, request.PageSize, "c.retail_id as RetailId,c.bill_code AS BillCode,c.bill_date AS BillDate,a.qty AS Qty,a.unit_id AS UnitId,b.goods_id AS GoodsId,b.goods_class_id AS GoodsCategoryId,b.goods_spec AS GoodsSpec,b.goods_name AS GoodsName,b.brand_id AS BrandId,d.shop_name AS ShopName,d.shop_id AS ShopId,d.province AS Province,d.city AS City,d.district AS District,d.street AS Street",
                "so_retail_detail a INNER JOIN sys_goods b ON a.goods_id = b.goods_id INNER JOIN so_retail c ON a.bill_id = c.retail_id INNER JOIN cm_shop d ON c.shop_id = d.shop_id", request.GetQuerySql(), "c.bill_date desc");
        }

        /// <summary>
        /// 分页获取商品销售退货信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<RetailBackDetail>, int>> GetRetailBackDetail<T>(T request) where T : RequestPagingBase, IQuerySql
        {
            return await this.ExecutePageAsync<RetailBackDetail>(request.PageCurrent, request.PageSize, "c.retail_back_id AS RetailBackId,c.`code` AS BillCode,c.bill_date AS BillDate,a.back_qty AS Qty,a.unit_id AS UnitId,b.goods_id AS GoodsId,b.goods_class_id AS GoodsCategoryId,b.goods_spec AS GoodsSpec,b.goods_name AS GoodsName,b.brand_id AS BrandId,d.shop_name AS ShopName,d.shop_id AS ShopId,d.province AS Province,d.city AS City,d.district AS District,d.street AS Street",
                "so_retail_back_detail a INNER JOIN sys_goods b ON a.goods_id = b.goods_id INNER JOIN so_retail_back c ON a.retail_back_id = c.retail_back_id INNER JOIN cm_shop d ON c.shop_id = d.shop_id", request.GetQuerySql(), "c.bill_date desc");
        }
    }
}