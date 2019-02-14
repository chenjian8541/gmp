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
    /// EF MySqlDbContext(Gmp)
    /// </summary>
    public class GmpDbContext : DbContext
    {
        /// <summary>
        /// EF配置
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(CustomServiceLocator.GetInstance<IAppConfigurtaionServices>().AppSettings.MySqlGmpConnectionString);
        }

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<SysUser> SysUsers { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public DbSet<SysUserRole> SysUserRoles { get; set; }

        /// <summary>
        /// 用户操作日志
        /// </summary>
        public DbSet<SysUserLog> SysUserLogs { get; set; }

        /// <summary>
        /// 门店销售单排名
        /// </summary>
        public DbSet<ShopRetailRank> ShopRetailRanks { get; set; }

        /// <summary>
        /// 门店销售单获奖者
        /// </summary>
        public DbSet<ShopRetailRankLimit> ShopRetailRankWinners { get; set; }

        /// <summary>
        /// 经销商信息
        /// </summary>
        public DbSet<Agency> Agencys { get; set; }

        /// <summary>
        /// 商品库存统计
        /// </summary>
        public DbSet<StatisticsStockGoods> StatisticsStockGoods { get; set; }

        /// <summary>
        /// 商品采购统计
        /// </summary>
        public DbSet<StatisticsPurchaseGoods> StatisticsPurchaseGoods { get; set; }

        /// <summary>
        /// 商品零售统计
        /// </summary>
        public DbSet<StatisticsRetailGoods> StatisticsRetailGoods { get; set; }

        /// <summary>
        /// 商品销售单统计
        /// </summary>
        public DbSet<StatisticsRetailCount> StatisticsRetailCount { get; set; }

        /// <summary>
        /// 商品采购单统计
        /// </summary>
        public DbSet<StatisticsPurchaseCount> StatisticsPurchaseCount { get; set; }
    }
}
