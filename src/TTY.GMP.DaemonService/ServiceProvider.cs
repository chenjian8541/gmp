using System;
using System.Reflection;
using System.Linq;
using TTY.Event.GMP;
using TTY.GMP.IOC;
using TTY.GMP.ServiceBus;
using Autofac;
using MassTransit;
using TTY.GMP.EventConsumer;
using System.IO;
using Newtonsoft.Json;
using TTY.GMP.Entity.Config;
using TTY.GMP.IBusiness;
using TTY.GMP.Entity.Enum;
using System.Threading;
using TTY.GMP.LOG;
using StackExchange.Redis;
using TTY.GMP.Cache.Redis;
using TTY.GMP.ICache;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Quartz.Impl;
using Quartz;
using System.Text;
using Microsoft.AspNetCore.Http;
using TTY.GMP.Http;
using TTY.GMP.Jobs;
using TTY.GMP.Entity.Government.GuangDong.Request;
using TTY.GMP.IEventProvider;

namespace TTY.GMP.DaemonService
{
    /// <summary>
    /// 服务处理类
    /// </summary>
    public class ServiceProvider
    {
        /// <summary>
        /// 处理服务业务
        /// </summary>
        public static void Process()
        {
            AppSettings appSettings = null;
            Bootstrapper.Bootstrap(p =>
            {
                appSettings = InitCustomIoc(p);
                InitRabbitMq(p, appSettings.RabbitMqConfig);
                InitScheduler(appSettings.QuartzConfig);
            });
            SubscriptionAdapt.IsSystemLoadingFinish = true;
        }

        /// <summary>
        /// 自定义一些注入规则
        /// </summary>
        /// <param name="container"></param>
        private static AppSettings InitCustomIoc(ContainerBuilder container)
        {
            //appsettings
            var appsettingsJson = File.ReadAllText("appsettings.json");
            var appSettings = JsonConvert.DeserializeObject<AppSettings>(appsettingsJson);
            var appConfigurtaionServices = new AppConfigurtaionServices(null) { AppSettings = appSettings };
            container.RegisterInstance(appConfigurtaionServices).As<IAppConfigurtaionServices>();
            //RedisConfig
            var configuration = ConfigurationOptions.Parse(appSettings.RedisConfig.ServerList, true);
            configuration.ResolveDns = true;
            var redis = ConnectionMultiplexer.Connect(configuration);
            container.RegisterInstance(redis).As<ConnectionMultiplexer>();
            container.RegisterType<RedisProvider>().As<ICacheProvider>();
            //IHttpClient
            container.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            container.RegisterType<StandardHttpClient>().As<IHttpClient>().SingleInstance();
            return appSettings;
        }

        /// <summary>
        /// 初始化rabbitmq
        /// </summary>
        /// <param name="container"></param>
        /// <param name="config"></param>
        private static void InitRabbitMq(ContainerBuilder container, RabbitMqConfig config)
        {
            if (!config.IsOpen)
            {
                return;
            }
            var subscriptionAdapt = new SubscriptionAdapt();
            var consumers = Assembly.Load("TTY.GMP.EventConsumer").GetTypes();
            foreach (var consumer in consumers)
            {
                var attributions = consumer.GetCustomAttributes(typeof(QueueConsumerAttribution), false);
                if (attributions.Length > 0)
                {
                    var consumerQueue = ((QueueConsumerAttribution)attributions[0]).QueueName;
                    var consumerSuperClass = consumer.GetInterfaces().FirstOrDefault(d => d.IsGenericType && d.GetGenericTypeDefinition() == typeof(IEventConsumer<>));
                    if (consumerSuperClass == null)
                    {
                        continue;
                    }
                    container.RegisterType(consumer).As(consumerSuperClass);
                    var consumerType = consumerSuperClass.GetGenericArguments().Single();
                    var methodInfo = typeof(SubscriptionAdapt).GetMethod("SubscribeAt").MakeGenericMethod(new Type[] { consumerType });
                    var busControl = (IBusControl)methodInfo.Invoke(subscriptionAdapt, new object[] { config.Host, consumerQueue, config.UserName, config.Password });
                    var publisher = new EventPublisher(busControl);
                    container.RegisterInstance(publisher).As<IEventPublisher>();
                }
            }
            Console.WriteLine("RabbitMq订阅成功");
        }

        /// <summary>
        /// 配置任务调度
        /// </summary>
        /// <param name="quartzConfig"></param>
        private static void InitScheduler(QuartzConfig quartzConfig)
        {
            var properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "TTYScheduler";
            properties["quartz.scheduler.instanceId"] = "instance_tty";
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = quartzConfig.QuartzThreadCount.ToString();
            properties["quartz.threadPool.threadPriority"] = "Normal";
            properties["quartz.jobStore.misfireThreshold"] = "60000";
            //properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            properties["quartz.scheduler.exporter.channelType"] = "tcp";
            properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            properties["quartz.jobStore.lockHandler.type"] = "Quartz.Impl.AdoJobStore.UpdateLockRowSemaphore, Quartz";
            properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.MySQLDelegate, Quartz";
            properties["quartz.jobStore.useProperties"] = "false";
            properties["quartz.jobStore.dataSource"] = "ttyscheduler";
            properties["quartz.jobStore.tablePrefix"] = "qrtz_";
            properties["quartz.jobStore.clustered"] = "true";
            properties["quartz.dataSource.ttyscheduler.provider"] = "MySql";
            properties["quartz.scheduler.exporter.port"] = quartzConfig.QuartzExporterPort;
            properties["quartz.scheduler.exporter.bindName"] = quartzConfig.QuartzExporterBindName;
            properties["quartz.dataSource.ttyscheduler.connectionString"] = quartzConfig.QuartzConnectionString;
            properties["quartz.serializer.type"] = "binary";
            var schedulerFactory = new StdSchedulerFactory(properties);
            var scheduler = schedulerFactory.GetScheduler().Result;
            scheduler.Start();
            Console.WriteLine("Quartz启动成功");
        }
    }
}
