using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Enum
{
    /// <summary>
    /// 用户操作日志类型
    /// </summary>
    public enum UserLogEnum
    {
        /// <summary>
        /// 登录
        /// </summary>
        Login = 0,

        /// <summary>
        /// 智控中心
        /// </summary>
        Map = 1,

        /// <summary>
        /// 销售商品
        /// </summary>
        SaleGoods = 2,

        /// <summary>
        /// 销售监管
        /// </summary>
        SaleGoodsLog = 3,

        /// <summary>
        /// 采购监管
        /// </summary>
        ReceiptInLog = 4,

        /// <summary>
        /// 零售商管理
        /// </summary>
        Shop = 5,

        /// <summary>
        /// 角色管理
        /// </summary>
        RoleMgr = 6,

        /// <summary>
        /// 编辑角色
        /// </summary>
        RoleEdit = 7,

        /// <summary>
        /// 删除角色
        /// </summary>
        RoleDel = 8,

        /// <summary>
        /// 查看角色
        /// </summary>
        RoleGet = 9,

        /// <summary>
        /// 编辑用户
        /// </summary>
        UserEdit = 10,

        /// <summary>
        /// 重置密码
        /// </summary>
        UserResetPassword = 11,

        /// <summary>
        /// 修改密码
        /// </summary>
        UserChangePassword = 12,

        /// <summary>
        /// 修改状态
        /// </summary>
        UserChangeStatusFlag = 13,

        /// <summary>
        /// 设置用户数据权限
        /// </summary>
        UserDataLimitSave = 14,

        /// <summary>
        /// 用户管理
        /// </summary>
        UserMgr = 15,

        /// <summary>
        /// 查看用户数据权限
        /// </summary>
        UserDataLimitGet = 16,

        /// <summary>
        /// 查看用户
        /// </summary>
        UserGet = 17,

        /// <summary>
        /// 区域农药销售统计
        /// </summary>
        GetStatisticsRetail = 18,

        /// <summary>
        /// 区域农药采购统计
        /// </summary>
        GetStatisticsPurchase = 19,

        /// <summary>
        /// 区域农药库存统计
        /// </summary>
        GetStatisticsStock = 20,

        /// <summary>
        /// 区域门店销售看板
        /// </summary>
        GetStatisticsRetailShopPage = 21,

        /// <summary>
        /// 区域门店采购看板
        /// </summary>
        GetStatisticsPurchaseShopPage = 22,

        /// <summary>
        /// 区域门店库存看板
        /// </summary>
        GetStatisticsStockShopPage = 23,

        /// <summary>
        /// [开放接口]获取商品和库存信息
        /// </summary>
        GetGoodsPaging = 24,

        /// <summary>
        /// [开放接口]获取门店信息
        /// </summary>
        GetShopPaging = 25,

        /// <summary>
        /// [开放接口]获取采购信息
        /// </summary>
        GetPurchasePaging = 26,

        /// <summary>
        /// [开放接口]获取采购退货信息
        /// </summary>
        GetPurchaseBackPaging = 27,

        /// <summary>
        /// [开放接口]获取销售商品信息
        /// </summary>
        GetSaleGoodsPaging = 28,

        /// <summary>
        /// [开放接口]获取销售退货信息
        /// </summary>
        GetSaleGoodsBackPaging = 29
    }
}
