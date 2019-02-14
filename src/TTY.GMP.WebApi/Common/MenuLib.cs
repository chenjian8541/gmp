using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Config;

namespace TTY.GMP.WebApi.Common
{
    /// <summary>
    /// 菜单管理类
    /// </summary>
    public class MenuLib
    {
        /// <summary>
        /// MenuConfig配置
        /// </summary>
        private static IList<MenuConfig> _menuConfigs;

        /// <summary>
        /// 防止多线程同步读取本地menu.json文件
        /// </summary>
        private static object LockObj = new object();

        /// <summary>
        /// 获取菜单配置信息
        /// </summary>
        public static IList<MenuConfig> MenuConfigs
        {
            get
            {
                if (_menuConfigs == null)
                {
                    lock (LockObj)
                    {
                        if (_menuConfigs == null)
                        {
                            var json = File.ReadAllText("menu.json");
                            _menuConfigs = JsonConvert.DeserializeObject<List<MenuConfig>>(json);
                            return _menuConfigs;
                        }
                    }
                }
                return _menuConfigs;
            }
        }
    }
}
