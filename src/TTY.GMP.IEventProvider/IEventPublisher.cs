using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.IEventProvider
{
    /// <summary>
    /// 定义发布消息规范
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        void Publish<T>(T message) where T : TTY.Event.GMP.Event;
    }
}
