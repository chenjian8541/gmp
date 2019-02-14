using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Utility
{
    /// <summary>
    /// 主键帮助类
    /// </summary>
    public class PrimaryKeyHelper
    {
        /// <summary>
        /// 默认构造函数
        /// 外部无法实例化，只能通过单例访问
        /// </summary>
        private PrimaryKeyHelper()
        {
            var workerId = 0;
            var datacenterId = 0;
            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException(string.Format("worker Id 必须大于0，且不能大于MaxWorkerId： {0}", MaxWorkerId));
            }
            if (datacenterId > MaxDatacenterId || datacenterId < 0)
            {
                throw new ArgumentException(string.Format("region Id 必须大于0，且不能大于MaxWorkerId： {0}", MaxDatacenterId));
            }
        }

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object LockObj = new object();

        /// <summary>
        /// PrimaryKeyHelper实例
        /// </summary>
        private static PrimaryKeyHelper _instance;

        /// <summary>
        /// PrimaryKeyHelper实例（单例模式）
        /// </summary>
        public static PrimaryKeyHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new PrimaryKeyHelper();
                        }
                        return _instance;
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 锁
        /// </summary>
        private readonly object _lock = new Object();

        /// <summary>
        /// 序列
        /// </summary>
        private long _sequence = 0L;

        /// <summary>
        /// 基准时间
        /// </summary>
        public const long Twepoch = 1474992000000L;

        /// <summary>
        /// 机器标识位数
        /// </summary>
        const int WorkerIdBits = 5;

        /// <summary>
        /// 数据标志位数
        /// </summary>
        const int DatacenterIdBits = 5;

        /// <summary>
        /// 序列号识位数
        /// </summary>
        const int SequenceBits = 12;

        /// <summary>
        /// 机器ID最大值
        /// </summary>
        const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);

        /// <summary>
        /// 数据标志ID最大值
        /// </summary>
        const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);

        /// <summary>
        /// 序列号ID最大值
        /// </summary>
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);

        /// <summary>
        /// 机器ID偏左移12位
        /// </summary>
        private const int WorkerIdShift = SequenceBits;

        /// <summary>
        /// 数据ID偏左移17位
        /// </summary>
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;

        /// <summary>
        /// 时间毫秒左移22位
        /// </summary>
        public const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;

        /// <summary>
        /// lastTimestamp
        /// </summary>
        private long _lastTimestamp = -1L;

        /// <summary>
        /// WorkerId
        /// </summary>
        public long WorkerId { get; private set; }

        /// <summary>
        /// DatacenterId
        /// </summary>
        public long DatacenterId { get; private set; }

        /// <summary>
        /// 防止产生的时间比之前的时间还要小（由于NTP回拨等问题）,保持增量的趋势.
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        private long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        /// <summary>
        /// 获取当前的时间戳
        /// </summary>
        /// <returns></returns>
        private long TimeGen()
        {
            return TimeExtensions.CurrentTimeMillis();
        }

        /// <summary>
        /// 生成一个主键
        /// </summary>
        /// <returns></returns>
        public long CreateID()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();
                if (timestamp < _lastTimestamp)
                {
                    throw new Exception(string.Format("时间戳必须大于上一次生成ID的时间戳.  拒绝为{0}毫秒生成id", _lastTimestamp - timestamp));
                }
                //如果上次生成时间和当前时间相同,在同一毫秒内
                if (_lastTimestamp == timestamp)
                {
                    //sequence自增，和sequenceMask相与一下，去掉高位
                    _sequence = (_sequence + 1) & SequenceMask;
                    //判断是否溢出,也就是每毫秒内超过1024，当为1024时，与sequenceMask相与，sequence就等于0
                    if (_sequence == 0)
                    {
                        //等待到下一毫秒
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    //如果和上次生成时间不同,重置sequence，就是下一毫秒开始，sequence计数重新从0开始累加,
                    //为了保证尾数随机性更大一些,最后一位可以设置一个随机数
                    _sequence = 0;//new Random().Next(10);
                }

                _lastTimestamp = timestamp;
                return ((timestamp - Twepoch) << TimestampLeftShift) | (DatacenterId << DatacenterIdShift) | (WorkerId << WorkerIdShift) | _sequence;
            }
        }
    }

    /// <summary>
    /// 时间扩展类
    /// </summary>
    public static class TimeExtensions
    {

        public static Func<long> CurrentTimeFunc = InternalCurrentTimeMillis;

        public static long CurrentTimeMillis()
        {
            return CurrentTimeFunc();
        }

        public static IDisposable StubCurrentTime(Func<long> func)
        {
            CurrentTimeFunc = func;
            return new DisposableAction(() =>
            {
                CurrentTimeFunc = InternalCurrentTimeMillis;
            });
        }

        public static IDisposable StubCurrentTime(long millis)
        {
            CurrentTimeFunc = () => millis;
            return new DisposableAction(() =>
            {
                CurrentTimeFunc = InternalCurrentTimeMillis;
            });
        }

        private static readonly DateTime Jan1st1970 = new DateTime
           (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static long InternalCurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }
    }

    /// <summary>
    ///  释放
    /// </summary>
    public class DisposableAction : IDisposable
    {
        readonly Action _action;

        public DisposableAction(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            _action = action;
        }

        public void Dispose()
        {
            _action();
        }
    }
}
