using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.IOC
{
    /// <summary>
    /// 依赖注入程序入口
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        /// 容器
        /// </summary>
        private static ContainerBuilder builder;

        /// <summary>
        /// 依赖注入程序入口
        /// </summary>
        /// <param name="contractProcess"></param>
        /// <returns></returns>
        public static IServiceProvider Bootstrap(Action<ContainerBuilder> contractProcess = null)
        {
            builder = new ContainerBuilder();
            builder.Initialize();
            contractProcess?.Invoke(builder);
            var container = builder.Build();
            var provider = new AutofacServiceProvider(container);
            CustomServiceLocator.InitCustomServiceLocator(provider);
            return provider;
        }
    }
}
