using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.Event.GMP
{
    /// <summary>
    /// 事件消息
    /// </summary>
    public class Event : IEvent
    {
        /// <summary>
        /// 默认程序名称
        /// </summary>
        public const string DefaultApplicationName = "TTY.GMP";

        /// <summary>
        /// 发送者的服务器名称
        /// </summary>
        public static readonly string DefaultMachineName = Environment.MachineName;

        /// <summary>
        /// 构造函数
        /// 初始化事件默认信息
        /// </summary>
        protected Event()
        {
            Id = Guid.NewGuid();
            CreateTime = DateTime.Now;
            FromApplicationMachine = DefaultApplicationName;
        }

        /// <summary>
        /// 事件唯一码
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 发送事件的服务器名
        /// </summary>
        public string FromApplicationMachine { get; set; }
    }
}
