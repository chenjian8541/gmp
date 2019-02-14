DROP TABLE IF EXISTS `sysuser`;
CREATE TABLE `sysuser` (
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  `UserRoleId` bigint(20) NOT NULL COMMENT '角色ID',
  `NickName` varchar(48) NOT NULL COMMENT '昵称',
  `Account` varchar(48) NOT NULL COMMENT '帐号',
  `Pwd` varchar(240) NOT NULL COMMENT '密码',
  `DataLimitType` int(4) NOT NULL DEFAULT '1',
  `DataLimitArea` longtext NOT NULL COMMENT '数据权限（地区）',
  `DataLimitShop` longtext NOT NULL COMMENT '数据权限（门店）',
  `AuthorityValue` text COMMENT '权限值',
  `LastLoginTime` datetime(4) DEFAULT NULL COMMENT '最后登录时间',
  `StatusFlag` int(4) NOT NULL COMMENT '用户状态',
  `DataFlag` int(4) NOT NULL COMMENT '数据 状态',
  PRIMARY KEY (`UserId`),
  KEY `IX_User_NickName` (`NickName`),
  UNIQUE KEY `IX_User_Account` (`Account`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户表';

DROP TABLE IF EXISTS `sysuserrole`;
CREATE TABLE `sysuserrole` (
  `UserRoleId` bigint(20) NOT NULL COMMENT '角色ID',
  `Name` varchar(128) NOT NULL COMMENT '角色名称',
  `AuthorityValue` text NOT NULL COMMENT '权限值',
  `Remark` varchar(128) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`UserRoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户角色表';

-- ----------------------------
-- 初始化角色和用户数据
-- ----------------------------
INSERT INTO `sysuserrole` VALUES ('1', '超级管理员', '16646142', '');
INSERT INTO `sysuser` VALUES ('1', '1', 'administrator', 'admin', 'LwDOoiuDgd+8uU5qtaqWFw==', '0', '', '', '0', '2018-05-28 10:35:42.0180', '1', '1');
