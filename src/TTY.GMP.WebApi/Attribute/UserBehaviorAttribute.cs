using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Enum;

namespace TTY.GMP.WebApi.Attribute
{
    /// <summary>
    /// 用户行为特性
    /// </summary>
    public class UserBehaviorAttribute : System.Attribute
    {
        /// <summary>
        /// 用户行为
        /// </summary>
        /// <param name="type"></param>
        /// <param name="describe"></param>
        public UserBehaviorAttribute(UserLogEnum type, string describe)
        {
            this.Type = type;
            this.Describe = describe;
        }

        /// <summary>
        /// 行为类型
        /// </summary>
        public UserLogEnum Type { get; set; }

        /// <summary>
        /// 行为描述
        /// </summary>
        public string Describe { get; set; }
    }
}
