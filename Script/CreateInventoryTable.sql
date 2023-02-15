
CREATE DATABASE `inventory` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_bin */;


CREATE TABLE `asset` (
  `CPU` varchar(300) COLLATE utf8mb4_bin DEFAULT NULL,
  `RAM` varchar(300) COLLATE utf8mb4_bin DEFAULT NULL,
  `DISK` varchar(300) COLLATE utf8mb4_bin DEFAULT NULL,
  `VideoController` varchar(300) COLLATE utf8mb4_bin DEFAULT NULL,
  `NetworkAdapter` varchar(300) COLLATE utf8mb4_bin DEFAULT NULL,
  `SoundCard` varchar(300) COLLATE utf8mb4_bin DEFAULT NULL,
  `Monitor` varchar(300) COLLATE utf8mb4_bin DEFAULT NULL,
  `SN` varchar(300) COLLATE utf8mb4_bin DEFAULT NULL,
  `Brand` varchar(300) COLLATE utf8mb4_bin DEFAULT NULL,
  `Model` varchar(300) COLLATE utf8mb4_bin DEFAULT NULL,
  `OS` varchar(300) COLLATE utf8mb4_bin DEFAULT NULL,
  `HostName` varchar(300) COLLATE utf8mb4_bin DEFAULT NULL,
  `IPAddr` varchar(300) COLLATE utf8mb4_bin DEFAULT NULL,
  `LastCheckin` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

