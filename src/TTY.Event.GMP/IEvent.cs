using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.Event.GMP
{
    /// <summary>
    /// 规范事件消息的格式
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// 事件唯一码
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; }

        /// <summary>
        /// 发送事件的服务器名
        /// </summary>
        string FromApplicationMachine { get; }
    }
}
