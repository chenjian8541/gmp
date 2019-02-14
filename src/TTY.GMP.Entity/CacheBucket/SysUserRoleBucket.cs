using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Config;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.Entity.CacheBucket
{
    /// <summary>
    /// 缓存所有的用户角色信息
    /// </summary>
    public class SysUserRoleBucket : ICacheDataContract
    {
        /// <summary>
        /// 用户角色信息
        /// </summary>
        public List<SysUserRole> SysUserRoles { get; set; }

        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public string GetKeyFormat(params object[] parms)
        {
            return "SysUserRoleBucket";
        }
    }
}
