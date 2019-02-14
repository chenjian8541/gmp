using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.IOC
{
    /// <summary>
    /// 自定义服务定位器
    /// 通过此定位器获取依赖关系
    /// </summary>
    public class CustomServiceLocator
    {
        /// <summary>
        /// 服务定位器
        /// </summary>
        private static IServiceProvider _serviceProvider;

        /// <summary>
        /// 初始化内部维护的“服务定位器”
        /// </summary>
        /// <param name="serviceProvider"></param>
        internal static void InitCustomServiceLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 获取类型实例
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService GetInstance<TService>()
        {
            return (TService)_serviceProvider.GetService(typeof(TService));
        }
    }
}
