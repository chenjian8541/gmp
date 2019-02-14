using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.CacheBucket;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.WebApi.Controllers.User;
using Xunit;

namespace TTY.GMP.UnitTest.WebApi.User
{
    /// <summary>
    /// 用户登录单元测试
    /// </summary>
    public class LoginActionTest
    {
        private readonly Mock<ISysUserBLL> _sysUserBll;

        private readonly Mock<ISysUserRoleBLL> _sysUserRoleBll;

        private readonly Mock<ISysUserLogBLL> _sysUserLoginBll;

        private readonly Mock<IAreaBLL> _areaBll;

        private readonly LoginAction _action;

        public LoginActionTest()
        {
            _sysUserBll = new Mock<ISysUserBLL>();
            _sysUserRoleBll = new Mock<ISysUserRoleBLL>();
            _sysUserLoginBll = new Mock<ISysUserLogBLL>();
            _areaBll = new Mock<IAreaBLL>();
            _action = new LoginAction(_sysUserBll.Object, _sysUserRoleBll.Object, _sysUserLoginBll.Object, _areaBll.Object);
        }

        /// <summary>
        /// 超过错误次数
        /// </summary>
        [Fact]
        public void ProcessAction_More_Than_Login_Failed_Record()
        {
            _sysUserBll.Setup(p => p.GetUserLoginFailedRecord(It.IsAny<string>())).Returns(Task.FromResult(new UserLoginFailedBucket()
            {
                ExpireAtTime = DateTime.Now.AddMinutes(10),
                FailedCount = 6
            }));
            var result = _action.ProcessAction(null, new LoginRequest() { UserAccount = "admin", UserPassword = "123456" }).Result;
            Assert.Equal(result.code, StatusCode.Login20003);
        }

        /// <summary>
        /// 用户名或密码为空
        /// </summary>
        [Fact]
        public void ProcessAction_UserAccount_Or_UserPassword_Is_Empty()
        {
            _sysUserBll.Setup(p => p.GetUserLoginFailedRecord(It.IsAny<string>())).Returns(Task.FromResult<UserLoginFailedBucket>(null));
            var result = _action.ProcessAction(null, new LoginRequest() { UserAccount = "admin", UserPassword = null }).Result;
            Assert.Equal(result.code, StatusCode.BadRequest);
        }

        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        [Fact]
        public void ProcessAction_UserAccount_Or_UserPassword_Is_Wrong()
        {
            _sysUserBll.Setup(p => p.GetUserLoginFailedRecord(It.IsAny<string>())).Returns(Task.FromResult<UserLoginFailedBucket>(null));
            _sysUserBll.Setup(p => p.GetSysUser(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<SysUser>(null));
            var result = _action.ProcessAction(null, new LoginRequest() { UserAccount = "admin", UserPassword = "123456" }).Result;
            Assert.Equal(result.code, StatusCode.Login20001);
        }

        /// <summary>
        /// 用户帐号被禁用
        /// </summary>
        [Fact]
        public void ProcessAction_UserAccount_Is_Disable()
        {
            _sysUserBll.Setup(p => p.GetUserLoginFailedRecord(It.IsAny<string>())).Returns(Task.FromResult(new UserLoginFailedBucket()
            {
                ExpireAtTime = DateTime.Now.AddMinutes(-10),
                FailedCount = 6
            }));
            _sysUserBll.Setup(p => p.GetSysUser(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new SysUser()
            {
                UserId = 2,
                StatusFlag = (int)UserStatusFlagEnum.Disable
            }));
            var result = _action.ProcessAction(null, new LoginRequest() { UserAccount = "admin", UserPassword = "123456" }).Result;
            Assert.Equal(result.code, StatusCode.Login20002);
        }
    }
}
