using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TTY.GMP.EventConsumer
{
    /// <summary>
    /// 规范消费者消费动作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventConsumer<in T> : IConsumer where T : TTY.Event.GMP.Event
    {
        /// <summary>
        /// 消费时执行的方法
        /// </summary>
        /// <param name="eEvent"></param>
        /// <returns></returns>
        Task Consume(T eEvent);
    }
}
