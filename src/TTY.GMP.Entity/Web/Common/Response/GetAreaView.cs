using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.Common.Response
{
    /// <summary>
    /// 获取地区信息
    /// </summary>
    public class GetAreaView
    {
        /// <summary>
        /// 地区ID
        /// </summary>
        public long AreaId { get; set; }

        /// <summary>
        /// 父级主键
        /// </summary>	
        public long ParentId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>	
        public string AreaName { get; set; }

        /// <summary>
        /// 子地区
        /// </summary>
        public List<GetAreaView> Children { get; set; }
    }
}
