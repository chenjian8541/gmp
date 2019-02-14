-- ----------------------------
-- Table structure for statisticspurchasecount
-- ----------------------------
DROP TABLE IF EXISTS `statisticspurchasecount`;
CREATE TABLE `statisticspurchasecount` (
  `StatisticsId` bigint(20) NOT NULL,
  `OrgId` bigint(20) NOT NULL,
  `ShopId` bigint(20) NOT NULL,
  `ShopName` varchar(200) NOT NULL,
  `Province` bigint(20) NOT NULL,
  `City` bigint(20) NOT NULL,
  `District` bigint(20) NOT NULL,
  `Street` bigint(20) NOT NULL,
  `StartTime` datetime(4) NOT NULL,
  `EndTime` datetime(4) NOT NULL,
  `BillCount` int(8) NOT NULL,
  PRIMARY KEY (`StatisticsId`),
  KEY `IX_StatisticsPurchaseCount_StartTime_EndTime` (`StartTime`,`EndTime`),
  KEY `IX_StatisticsPurchaseCount_Province` (`Province`),
  KEY `IX_StatisticsPurchaseCount_City` (`City`),
  KEY `IX_StatisticsPurchaseCount_District` (`District`),
  KEY `IX_StatisticsPurchaseCount_Street` (`Street`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


-- ----------------------------
-- Table structure for statisticspurchasegoods
-- ----------------------------
DROP TABLE IF EXISTS `statisticspurchasegoods`;
CREATE TABLE `statisticspurchasegoods` (
  `StatisticsId` bigint(20) NOT NULL,
  `OrgId` bigint(20) NOT NULL,
  `ShopId` bigint(20) NOT NULL,
  `GoodsId` bigint(20) NOT NULL,
  `GoodsCategoryId` bigint(20) NOT NULL,
  `Province` bigint(20) NOT NULL,
  `City` bigint(20) NOT NULL,
  `District` bigint(20) NOT NULL,
  `Street` bigint(20) NOT NULL,
  `StartTime` datetime(4) NOT NULL,
  `EndTime` datetime(4) NOT NULL,
  `ShopName` varchar(200) NOT NULL,
  `GoodsName` varchar(50) NOT NULL,
  `GoodsSpec` varchar(50) DEFAULT NULL,
  `GoodsContents` varchar(50) DEFAULT NULL,
  `GoodsCategoryName` varchar(50) NOT NULL,
  `TotalCount` decimal(24,8) NOT NULL,
  `TotalWeight` decimal(24,8) NOT NULL,
  `TotalContentsWeight` decimal(24,8) NOT NULL,
  PRIMARY KEY (`StatisticsId`),
  KEY `IX_StatisticsPurchaseGoods_StartTime_EndTime` (`StartTime`,`EndTime`),
  KEY `IX_StatisticsPurchaseGoods_Province` (`Province`),
  KEY `IX_StatisticsPurchaseGoods_City` (`City`),
  KEY `IX_StatisticsPurchaseGoods_District` (`District`),
  KEY `IX_StatisticsPurchaseGoods_Street` (`Street`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


-- ----------------------------
-- Table structure for statisticsretailcount
-- ----------------------------
DROP TABLE IF EXISTS `statisticsretailcount`;
CREATE TABLE `statisticsretailcount` (
  `StatisticsId` bigint(20) NOT NULL,
  `OrgId` bigint(20) NOT NULL,
  `ShopId` bigint(20) NOT NULL,
  `ShopName` varchar(200) NOT NULL,
  `Province` bigint(20) NOT NULL,
  `City` bigint(20) NOT NULL,
  `District` bigint(20) NOT NULL,
  `Street` bigint(20) NOT NULL,
  `StartTime` datetime(4) NOT NULL,
  `EndTime` datetime(4) NOT NULL,
  `BillCount` int(8) NOT NULL,
  PRIMARY KEY (`StatisticsId`),
  KEY `IX_StatisticsRetailCount_StartTime_EndTime` (`StartTime`,`EndTime`),
  KEY `IX_StatisticsRetailCount_Province` (`Province`),
  KEY `IX_StatisticsRetailCount_City` (`City`),
  KEY `IX_StatisticsRetailCount_District` (`District`),
  KEY `IX_StatisticsRetailCount_Street` (`Street`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


-- ----------------------------
-- Table structure for statisticsretailgoods
-- ----------------------------
DROP TABLE IF EXISTS `statisticsretailgoods`;
CREATE TABLE `statisticsretailgoods` (
  `StatisticsId` bigint(20) NOT NULL,
  `OrgId` bigint(20) NOT NULL,
  `ShopId` bigint(20) NOT NULL,
  `GoodsId` bigint(20) NOT NULL,
  `GoodsCategoryId` bigint(20) NOT NULL,
  `Province` bigint(20) NOT NULL,
  `City` bigint(20) NOT NULL,
  `District` bigint(20) NOT NULL,
  `Street` bigint(20) NOT NULL,
  `StartTime` datetime(4) NOT NULL,
  `EndTime` datetime(4) NOT NULL,
  `ShopName` varchar(200) NOT NULL,
  `GoodsName` varchar(50) NOT NULL,
  `GoodsSpec` varchar(50) DEFAULT NULL,
  `GoodsContents` varchar(50) DEFAULT NULL,
  `GoodsCategoryName` varchar(50) NOT NULL,
  `TotalCount` decimal(24,8) NOT NULL,
  `TotalWeight` decimal(24,8) NOT NULL,
  `TotalContentsWeight` decimal(24,8) NOT NULL,
  PRIMARY KEY (`StatisticsId`),
  KEY `IX_StatisticsRetailGoods_StartTime_EndTime` (`StartTime`,`EndTime`),
  KEY `IX_StatisticsRetailGoods_Province` (`Province`),
  KEY `IX_StatisticsRetailGoods_City` (`City`),
  KEY `IX_StatisticsRetailGoods_Street` (`Street`),
  KEY `IX_StatisticsRetailGoods_District` (`District`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


-- ----------------------------
-- Table structure for statisticsstockgoods
-- ----------------------------
DROP TABLE IF EXISTS `statisticsstockgoods`;
CREATE TABLE `statisticsstockgoods` (
  `StatisticsId` bigint(20) NOT NULL,
  `OrgId` bigint(20) NOT NULL,
  `ShopId` bigint(20) NOT NULL,
  `GoodsId` bigint(20) NOT NULL,
  `GoodsCategoryId` bigint(20) NOT NULL,
  `Province` bigint(20) NOT NULL,
  `City` bigint(20) NOT NULL,
  `District` bigint(20) NOT NULL,
  `Street` bigint(20) NOT NULL,
  `ShopName` varchar(200) NOT NULL,
  `GoodsName` varchar(50) NOT NULL,
  `GoodsSpec` varchar(50) DEFAULT NULL,
  `GoodsContents` varchar(50) DEFAULT NULL,
  `GoodsCategoryName` varchar(50) NOT NULL,
  `TotalCount` decimal(24,8) NOT NULL,
  `TotalWeight` decimal(24,8) NOT NULL,
  `TotalContentsWeight` decimal(24,8) NOT NULL,
  `UpdateTime` datetime(4) NOT NULL,
  PRIMARY KEY (`StatisticsId`),
  KEY `IX_StatisticsStockGoods_Province` (`Province`),
  KEY `IX_StatisticsStockGoods_City` (`City`),
  KEY `IX_StatisticsStockGoods_District` (`District`),
  KEY `IX_StatisticsStockGoods_Street` (`Street`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



