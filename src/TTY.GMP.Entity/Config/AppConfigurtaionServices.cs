using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Config
{
    /// <summary>
    /// 应用程序配置管理类
    /// </summary>
    public class AppConfigurtaionServices : IAppConfigurtaionServices
    {
        /// <summary>
        /// AppSettings配置
        /// </summary>
        private AppSettings _appSettings;

        /// <summary>
        /// 构造函数，注入配置信息
        /// </summary>
        /// <param name="appConfiguration"></param>
        public AppConfigurtaionServices(IOptions<AppSettings> appConfiguration)
        {
            if (appConfiguration != null)
            {
                _appSettings = appConfiguration.Value;
            }
        }

        /// <summary>
        /// AppSettings配置
        /// </summary>
        public AppSettings AppSettings
        {
            get
            {
                return _appSettings;
            }
            set
            {
                _appSettings = value;
            }
        }
    }
}
