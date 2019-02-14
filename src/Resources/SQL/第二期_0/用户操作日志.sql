DROP TABLE IF EXISTS `sysuserlog`;
CREATE TABLE `sysuserlog` (
  `LogId` bigint(20) NOT NULL,
  `UserId` bigint(20) NOT NULL,
  `Type` int(4) NOT NULL,
  `IpAddress` varchar(24) NOT NULL,
  `Describe` varchar(48) DEFAULT NULL,
  `Ot` datetime(4) NOT NULL,
  PRIMARY KEY (`LogId`),
  KEY `IX_UserLog_Type` (`Type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户操作日志';