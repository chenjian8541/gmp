using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Config;

namespace TTY.GMP.Entity.Database
{
    [Table("sysuser")]
    public class SysUser : ICacheDataContract
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        public long UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long UserRoleId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户帐号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 数据权限类型
        /// <see cref="TTY.GMP.Entity.Enum.DataLimitTypeEnum"/>
        /// </summary>
        public int DataLimitType { get; set; }

        /// <summary>
        /// 数据权限（地区）
        /// 各地区之间以“,”隔开
        /// 地区ID和地区level以“|”隔开
        /// </summary>
        public string DataLimitArea { get; set; }

        /// <summary>
        /// 数据权限（门店）
        /// 各门店之间以“,”隔开
        /// </summary>
        public string DataLimitShop { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        public string AuthorityValue { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 用户状态
        /// <see cref="TTY.GMP.Entity.Enum.UserStatusFlagEnum"/>
        /// </summary>
        public int StatusFlag { get; set; }

        /// <summary>
        /// 数据状态
        /// <see cref="TTY.GMP.Entity.Enum.DataFlagEnum"/>
        /// </summary>
        public int DataFlag { get; set; }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public string GetKeyFormat(params object[] parms)
        {
            return string.Format("SysUser_{0}", parms);
        }
    }
}
