-- ----------------------------
-- Table structure for agency
-- ----------------------------
DROP TABLE IF EXISTS `agency`;
CREATE TABLE `agency` (
  `ShopId` bigint(20) NOT NULL,
  `Id` varchar(128) DEFAULT NULL,
  `Code` varchar(128) DEFAULT NULL,
  `Name` varchar(128) DEFAULT NULL,
  UNIQUE KEY `IX_Agency_ShopId` (`ShopId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
