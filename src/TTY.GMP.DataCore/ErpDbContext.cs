using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Config;
using TTY.GMP.Entity.Database;
using TTY.GMP.IOC;

namespace TTY.GMP.DataCore
{
    /// <summary>
    /// EF MySqlDbContext(Erp)
    /// </summary>
    public class ErpDbContext : DbContext
    {
        /// <summary>
        /// EF配置
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(CustomServiceLocator.GetInstance<IAppConfigurtaionServices>().AppSettings.MySqlErpConnectionString);
        }

        /// <summary>
        /// 门店信息
        /// </summary>
        public DbSet<Shop> Shops { get; set; }

        /// <summary>
        /// 地区信息
        /// </summary>
        public DbSet<Area> Areas { get; set; }

        /// <summary>
        /// 零售订单
        /// </summary>
        public DbSet<SoRetail> SoRetails { get; set; }

        /// <summary>
        /// 零售订单详情
        /// </summary>
        public DbSet<SoRetailDetail> SoRetailDetails { get; set; }

        /// <summary>
        /// 零售订单信息
        /// </summary>
        public DbSet<SoRetailInfo> SoRetailInfos { get; set; }

        /// <summary>
        /// 零售订单购买客户信息
        /// </summary>
        public DbSet<CmRetailCustomer> CmRetailCustomers { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public DbSet<Goods> Goodss { get; set; }

        /// <summary>
        /// 商品类别
        /// </summary>
        public DbSet<GoodsCategory> GoodsCategorys { get; set; }

        /// <summary>
        /// 商品品牌
        /// </summary>
        public DbSet<GoodsBrand> GoodsBrands { get; set; }

        /// <summary>
        /// 商品单位
        /// </summary>
        public DbSet<SysUnit> SysUnits { get; set; }

        /// <summary>
        /// 进货单
        /// </summary>
        public DbSet<PoInReceipt> PoInReceipts { get; set; }

        /// <summary>
        /// 进货单详情
        /// </summary>
        public DbSet<PoInReceiptDetail> PoInReceiptDetails { get; set; }

        /// <summary>
        /// 供应商信息
        /// </summary>
        public DbSet<BaseSupplier> BaseSuppliers { get; set; }

        /// <summary>
        /// 销售退货单
        /// </summary>
        public DbSet<SoRetailBack> SoRetailBacks { get; set; }

        /// <summary>
        /// 销售退货单明细
        /// </summary>
        public DbSet<SoRetailBackDetail> SoRetailBackDetails { get; set; }

        /// <summary>
        /// 采购退货单
        /// </summary>
        public DbSet<PoBackReceipt> PoBackReceipts { get; set; }

        /// <summary>
        /// 采购退货单明细
        /// </summary>
        public DbSet<PoBackReceiptDetail> PoBackReceiptDetails { get; set; }
    }
}
