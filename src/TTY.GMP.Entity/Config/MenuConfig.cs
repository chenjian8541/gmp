using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Config
{
    /// <summary>
    /// 菜单配置
    /// </summary>
    public class MenuConfig
    {
        /// <summary>
        /// 权重Id（唯一）
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// 约定编码
        /// </summary>
        public string PerCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父Id (默认0)
        /// </summary>
        public int FatherId { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 菜单类型
        /// <see cref="TTY.GMP.Entity.Enum.MenuEnum"/>
        /// </summary>
        public int Type { get; set; }
    }
}
