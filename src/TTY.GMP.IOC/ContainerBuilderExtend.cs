using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac.Extras.DynamicProxy;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace TTY.GMP.IOC
{
    /// <summary>
    /// 对ContainerBuilder扩展
    /// </summary>
    internal static class ContainerBuilderExtend
    {
        /// <summary>
        /// 通过扫描bin目录所有依赖的dll进行批量注入
        /// </summary>
        /// <param name="this"></param>
        internal static void Initialize(this ContainerBuilder @this)
        {
            var assemblyNames = GetAllAssemblyFiles();
            var limitNames = new string[] { "Model", "PrecompiledViews", "Authority", "Cache.Redis", "ICache", "Http" , "Throttle" };
            foreach (var name in assemblyNames)
            {
                if (IsLimitProject(limitNames, name))
                {
                    continue;
                }
                var referencedAssembly = Assembly.Load(name);
                @this.RegisterAssemblyTypes(referencedAssembly).AsImplementedInterfaces().EnableInterfaceInterceptors();
            }
        }

        /// <summary>
        /// 判断是否为限制注入的项目
        /// </summary>
        /// <param name="limitNames"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static bool IsLimitProject(string[] limitNames, string name)
        {
            foreach (var limitName in limitNames)
            {
                if (name.Contains(limitName))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 扫码bin目录获取所有依赖的dll
        /// </summary>
        /// <returns>所有依赖的类库信息</returns>
        private static List<string> GetAllAssemblyFiles()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            return Directory.GetFiles(path, "*.dll").Select(Path.GetFileNameWithoutExtension).Where(a => Regex.IsMatch(a, "TTY.GMP")).ToList();
        }
    }
}
