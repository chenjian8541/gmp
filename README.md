## 什么是 GMP
GMP 是一个政府数据监控平台，使用.net core技术，里边使用了Redis、rabbitmq、autofac、Throttle（开源的限流框架）、log4net、EF、Dapper、Quartz等技术

### 1. rabbitmq消费逻辑
```csharp
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
```
### 2. 仓储层多数据库操作
```csharp
     public class SysUserDAL : BaseCacheDAL<SysUser>, ISysUserDAL, IDbContext<GmpDbContext>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheProvider"></param>
        public SysUserDAL(ICacheProvider cacheProvider) : base(cacheProvider)
        { }

        /// <summary>
        /// 获取数据访问上下文
        /// </summary>
        /// <returns></returns>
        public GmpDbContext GetDbContext()
        {
            return new GmpDbContext();
        }

        /// <summary>
        /// 从数据库中获取需要缓存的信息
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected override async Task<SysUser> GetDb(params object[] keys)
        {
            return await this.Find<GmpDbContext, SysUser>(p => p.UserId == keys[0].ToLong() && p.DataFlag == (int)DataFlagEnum.Normal);
        }

        /// <summary>
        /// 通过ID获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<SysUser> GetSysUser(long userId)
        {
            return await base.GetCache(userId);
        }
       }
    ```
       
