using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.IOC;
using TTY.GMP.EventConsumer;

namespace TTY.GMP.ServiceBus
{
    public class SubscriptionAdapt
    {
        /// <summary>
        /// 系统组建是否加载完毕
        /// 用于处理rabbitmq消费跑在了autofac注入前面，导致报错
        /// rabbitmq消费时判断系统初始化是否完毕，如果未完毕则等待1分钟执行消费
        /// </summary>
        public static bool IsSystemLoadingFinish = false;

        /// <summary>
        /// 需要发送Event时
        /// </summary>
        /// <param name="host">服务器IP</param>
        /// <param name="queue">消费queue</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        public IBusControl PublishAt(string host, string queue, string userName, string password)
        {
            var uri = new Uri($"rabbitmq://{host}");
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(uri, queue, x =>
                {
                    x.Username(userName);
                    x.Password(password);
                });
            });
            bus.Start();
            return bus;
        }

        /// <summary>
        /// 需要消费Event时
        /// </summary>
        /// <param name="host">服务器IP</param>
        /// <param name="queue">消费queue</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        public IBusControl SubscribeAt<T>(string host, string queue, string userName, string password)
            where T : TTY.Event.GMP.Event
        {
            var uri = new Uri($"rabbitmq://{host}");
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var sbcHost = sbc.Host(uri, h =>
                {
                    h.Username(userName);
                    h.Password(password);
                });
                sbc.ReceiveEndpoint(sbcHost, queue, x =>
                {
                    x.Handler<T>(msgBag =>
                    {
                        if (!IsSystemLoadingFinish)
                        {
                            System.Threading.Thread.Sleep(60 * 1000);
                        }
                        return CustomServiceLocator.GetInstance<IEventConsumer<T>>().Consume(msgBag.Message);
                    });
                });
            });
            bus.Start();
            return bus;
        }
    }
}
