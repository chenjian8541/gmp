using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TTY.GMP.LOG;

namespace TTY.GMP.EventConsumer
{
    /// <summary>
    /// 消费者基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ConsumerBase<T> : IEventConsumer<T> where T : TTY.Event.GMP.Event
    {
        /// <summary>
        /// 执行消息消费的方法
        /// </summary>
        /// <param name="eEvent">当前队列的消息</param>
        public async Task Consume(T eEvent)
        {
            try
            {
                await Receive(eEvent);
            }
            catch (Exception ex)
            {
                Log.Error($"【RabbitMq】处理消息失败,参数:{JsonConvert.SerializeObject(eEvent)}", ex, this.GetType());
            }
        }

        /// <summary>
        /// 消费者接收消息
        /// </summary>
        /// <param name="eEvent">消息体</param>
        protected abstract Task Receive(T eEvent);
    }
}
