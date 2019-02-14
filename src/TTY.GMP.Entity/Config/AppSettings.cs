using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Config
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// (ERP)MySql数据库连接
        /// </summary>
        public string MySqlErpConnectionString { get; set; }

        /// <summary>
        /// (GMP)MySql数据库连接
        /// </summary>
        public string MySqlGmpConnectionString { get; set; }

        /// <summary>
        /// Redis配置
        /// </summary>
        public RedisConfig RedisConfig { get; set; }

        /// <summary>
        /// 平台接口信息
        /// </summary>
        public PlatformConfig PlatformConfig { get; set; }

        /// <summary>
        /// RabbitMq 配置
        /// </summary>
        public RabbitMqConfig RabbitMqConfig { get; set; }

        /// <summary>
        /// 任务调度配置
        /// </summary>
        public QuartzConfig QuartzConfig { get; set; }

        /// <summary>
        /// 邮箱配置
        /// </summary>
        public MailConfig MailConfig { get; set; }

        /// <summary>
        /// 定时生成门店零售单排名功能配置
        /// </summary>
        public ShopRetailRankConfig ShopRetailRankConfig { get; set; }

        /// <summary>
        /// 政府对接API配置
        /// </summary>
        public GovernmentApiConfig GovernmentApiConfig { get; set; }
    }

    /// <summary>
    /// Redis配置
    /// </summary>
    public class RedisConfig
    {
        /// <summary>
        /// 可写服务器
        /// </summary>
        public string ServerList { get; set; }

        /// <summary>
        /// 默认库ID
        /// </summary>
        public int DefaultDb { get; set; }
    }

    /// <summary>
    /// 平台配置
    /// </summary>
    public class PlatformConfig
    {
        /// <summary>
        /// 平台请求地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 安全key
        /// </summary>
        public string ApiKey { get; set; }
    }

    /// <summary>
    /// RabbitMq配置
    /// </summary>
    public class RabbitMqConfig
    {
        /// <summary>
        /// RabbitMq服务器
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 是否启用RabbitMq功能
        /// </summary>
        public bool IsOpen { get; set; }
    }

    /// <summary>
    /// Quartz任务调度配置
    /// </summary>
    public class QuartzConfig
    {
        /// <summary>
        /// 端口
        /// </summary>
        public string QuartzExporterPort { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string QuartzExporterBindName { get; set; }

        /// <summary>
        /// 执行线程数量
        /// </summary>
        public int QuartzThreadCount { get; set; }

        /// <summary>
        /// 数据库连接语句
        /// </summary>
        public string QuartzConnectionString { get; set; }
    }

    /// <summary>
    /// 邮箱配置
    /// </summary>
    public class MailConfig
    {
        /// <summary>
        /// 电子邮箱地址(发件人)
        /// </summary>
        public string SenderAddress { get; set; }

        /// <summary>
        /// 显示名称(发件人)
        /// </summary>
        public string SenderDisplayName { get; set; }

        /// <summary>
        /// 用户名(发件人)
        /// </summary>
        public string SenderUserName { get; set; }

        /// <summary>
        /// 密码(发件人)
        /// </summary>
        public string SenderPassword { get; set; }

        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        public string MailHost { get; set; }

        /// <summary>
        /// 服务器端口
        /// </summary>
        public string MailPort { get; set; }
    }

    /// <summary>
    /// 定时生成门店零售单排名功能配置
    /// </summary>
    public class ShopRetailRankConfig
    {
        /// <summary>
        /// 订单王(统计开始时间)
        /// </summary>
        public DateTime KingStartTime { get; set; }

        /// <summary>
        /// 附件生成地址
        /// </summary>
        public string MailAttachmentAddress { get; set; }

        /// <summary>
        /// 收件人(各收件人之间以";"隔开)
        /// </summary>
        public string MailRecipients { get; set; }

        /// <summary>
        /// 获取满足的最小订单数 (成长之星)
        /// </summary>
        public int MinBillCountGrowth { get; set; }

        /// <summary>
        /// 排名限制数(排名在此之前的才能获奖)
        /// </summary>
        public int RankLimitCount { get; set; }

        /// <summary>
        /// 展示排名数
        /// </summary>
        public int ShowCount { get; set; }

        /// <summary>
        /// 获取满足的最小订单数 （订单王）
        /// </summary>
        public int MinBillCountKing { get; set; }

        /// <summary>
        /// 是否测试
        /// </summary>
        public bool IsTest { get; set; }
    }

    /// <summary>
    /// 政府对接API配置
    /// </summary>
    public class GovernmentApiConfig
    {
        /// <summary>
        /// 广东省农药经营管理系统平台通讯接口
        /// </summary>
        public GuangDong GuangDong { get; set; }
    }

    /// <summary>
    /// 广东省农药经营管理系统平台通讯接口
    /// </summary>
    public class GuangDong
    {
        /// <summary>
        /// 通讯编码
        /// </summary>
        public string QyCode { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        public string ApiAddress { get; set; }
    }
}
