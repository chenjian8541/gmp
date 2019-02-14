using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.Event.GMP
{
    /// <summary>
    /// 此特性用于消费者定义所属队列名称
    /// </summary>
    public class QueueConsumerAttribution : Attribute
    {
        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName
        {
            get
            {
                return _queueName;
            }
        }

        /// <summary>
        /// Queue
        /// </summary>
        private string _queueName { get; set; }

        /// <summary>
        /// 构造函数
        /// 初始化队列名称
        /// </summary>
        /// <param name="queueName"></param>
        public QueueConsumerAttribution(string queueName)
        {
            _queueName = queueName;
        }
    }
}
