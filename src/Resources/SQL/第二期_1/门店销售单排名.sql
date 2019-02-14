-- ----------------------------
-- Table structure for shopretailrank
-- ----------------------------
DROP TABLE IF EXISTS `shopretailrank`;
CREATE TABLE `shopretailrank` (
  `ShopId` bigint(20) NOT NULL,
  `Type` int(4) NOT NULL,
  `Rank` int(8) NOT NULL,
  `ShopLinkMan` varchar(128) DEFAULT NULL,
  `ShopTelphone` varchar(24) DEFAULT NULL,
  `ShopAddress` varchar(200) DEFAULT NULL,
  `BillCount` int(8) NOT NULL,
  `OrgId` bigint(20) NOT NULL,
  `OrgNo` varchar(128) NOT NULL,
  `ShopName` varchar(128) NOT NULL,
  `StartTime` datetime NOT NULL,
  `EndTime` datetime NOT NULL,
  `IsLimit` bit(1) NOT NULL,
  `IsLast` bit(1) NOT NULL,
  `ProvinceName` varchar(128) DEFAULT NULL,
  `CityName` varchar(128) DEFAULT NULL,
  `DistrictName` varchar(128) DEFAULT NULL,
  `RecommendName` varchar(128) DEFAULT NULL,
  KEY `IX_ShopRetailRank_ShopId_EndTime` (`ShopId`,`EndTime`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for shopretailranklimit
-- ----------------------------
DROP TABLE IF EXISTS `shopretailranklimit`;
CREATE TABLE `shopretailranklimit` (
  `ShopId` bigint(20) NOT NULL,
  `Type` int(4) NOT NULL,
  PRIMARY KEY (`ShopId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;