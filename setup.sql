-- Database: chat-identity
DROP DATABASE IF EXISTS `chat-identity`;

CREATE DATABASE `chat-identity`;
use `chat-identity`;
set @client_host:="http://localhost:8080";
set @login_redirect_url:="http://localhost:5001/account/login";
-- MySQL dump 10.13  Distrib 8.0.15, for macos10.14 (x86_64)
--
-- Host: localhost    Database: chat-identity
-- ------------------------------------------------------
-- Server version	8.0.21

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `ApiClaims`
--

DROP TABLE IF EXISTS `ApiClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ApiClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Type` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ApiResourceId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ApiClaims_ApiResourceId` (`ApiResourceId`),
  CONSTRAINT `FK_ApiClaims_ApiResources_ApiResourceId` FOREIGN KEY (`ApiResourceId`) REFERENCES `ApiResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ApiResources`
--

DROP TABLE IF EXISTS `ApiResources`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ApiResources` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Enabled` tinyint(1) NOT NULL,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DisplayName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) DEFAULT NULL,
  `LastAccessed` datetime(6) DEFAULT NULL,
  `NonEditable` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_ApiResources_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ApiResources`
--

LOCK TABLES `ApiResources` WRITE;
/*!40000 ALTER TABLE `ApiResources` DISABLE KEYS */;
INSERT INTO `ApiResources` VALUES (1,1,'api1','My API',NULL,'2021-07-04 04:01:12.761837',NULL,NULL,0);
/*!40000 ALTER TABLE `ApiResources` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `ApiClaims`
--

LOCK TABLES `ApiClaims` WRITE;
/*!40000 ALTER TABLE `ApiClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `ApiClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ApiProperties`
--

DROP TABLE IF EXISTS `ApiProperties`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ApiProperties` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Key` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ApiResourceId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ApiProperties_ApiResourceId` (`ApiResourceId`),
  CONSTRAINT `FK_ApiProperties_ApiResources_ApiResourceId` FOREIGN KEY (`ApiResourceId`) REFERENCES `ApiResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ApiProperties`
--

LOCK TABLES `ApiProperties` WRITE;
/*!40000 ALTER TABLE `ApiProperties` DISABLE KEYS */;
/*!40000 ALTER TABLE `ApiProperties` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ApiScopeClaims`
--

DROP TABLE IF EXISTS `ApiScopeClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ApiScopeClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Type` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ApiScopeId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ApiScopeClaims_ApiScopeId` (`ApiScopeId`),
  CONSTRAINT `FK_ApiScopeClaims_ApiScopes_ApiScopeId` FOREIGN KEY (`ApiScopeId`) REFERENCES `ApiScopes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ApiScopeClaims`
--

LOCK TABLES `ApiScopeClaims` WRITE;
/*!40000 ALTER TABLE `ApiScopeClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `ApiScopeClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ApiScopes`
--

DROP TABLE IF EXISTS `ApiScopes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ApiScopes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DisplayName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Required` tinyint(1) NOT NULL,
  `Emphasize` tinyint(1) NOT NULL,
  `ShowInDiscoveryDocument` tinyint(1) NOT NULL,
  `ApiResourceId` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_ApiScopes_Name` (`Name`),
  KEY `IX_ApiScopes_ApiResourceId` (`ApiResourceId`),
  CONSTRAINT `FK_ApiScopes_ApiResources_ApiResourceId` FOREIGN KEY (`ApiResourceId`) REFERENCES `ApiResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ApiScopes`
--

LOCK TABLES `ApiScopes` WRITE;
/*!40000 ALTER TABLE `ApiScopes` DISABLE KEYS */;
INSERT INTO `ApiScopes` VALUES (1,'api1','My API',NULL,0,0,1,1);
/*!40000 ALTER TABLE `ApiScopes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ApiSecrets`
--

DROP TABLE IF EXISTS `ApiSecrets`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ApiSecrets` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Expiration` datetime(6) DEFAULT NULL,
  `Type` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Created` datetime(6) NOT NULL,
  `ApiResourceId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ApiSecrets_ApiResourceId` (`ApiResourceId`),
  CONSTRAINT `FK_ApiSecrets_ApiResources_ApiResourceId` FOREIGN KEY (`ApiResourceId`) REFERENCES `ApiResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ApiSecrets`
--

LOCK TABLES `ApiSecrets` WRITE;
/*!40000 ALTER TABLE `ApiSecrets` DISABLE KEYS */;
/*!40000 ALTER TABLE `ApiSecrets` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetRoleClaims`
--

DROP TABLE IF EXISTS `AspNetRoleClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AspNetRoleClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetRoleClaims`
--

LOCK TABLES `AspNetRoleClaims` WRITE;
/*!40000 ALTER TABLE `AspNetRoleClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetRoleClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetRoles`
--

DROP TABLE IF EXISTS `AspNetRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AspNetRoles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetRoles`
--

LOCK TABLES `AspNetRoles` WRITE;
/*!40000 ALTER TABLE `AspNetRoles` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserClaims`
--

DROP TABLE IF EXISTS `AspNetUserClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AspNetUserClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserClaims`
--

LOCK TABLES `AspNetUserClaims` WRITE;
/*!40000 ALTER TABLE `AspNetUserClaims` DISABLE KEYS */;
INSERT INTO `AspNetUserClaims` VALUES (7,'b21f5495-25a3-4be0-aa3f-6522a4c6218b','identityId','b21f5495-25a3-4be0-aa3f-6522a4c6218b'),(8,'b21f5495-25a3-4be0-aa3f-6522a4c6218b','userName','james.bond@gmail.com'),(9,'b21f5495-25a3-4be0-aa3f-6522a4c6218b','chatUserId','656717b7-6dcb-452d-aa6f-4691b45dac1d'),(10,'c8b8c9fd-10eb-4088-85cb-b184b9839d02','identityId','c8b8c9fd-10eb-4088-85cb-b184b9839d02'),(11,'c8b8c9fd-10eb-4088-85cb-b184b9839d02','userName','john.wick@gmail.com'),(12,'c8b8c9fd-10eb-4088-85cb-b184b9839d02','chatUserId','24ce6482-6569-4c90-a938-fa8ac85606d7');
/*!40000 ALTER TABLE `AspNetUserClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserLogins`
--

DROP TABLE IF EXISTS `AspNetUserLogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserLogins`
--

LOCK TABLES `AspNetUserLogins` WRITE;
/*!40000 ALTER TABLE `AspNetUserLogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserLogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserRoles`
--

DROP TABLE IF EXISTS `AspNetUserRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserRoles`
--

LOCK TABLES `AspNetUserRoles` WRITE;
/*!40000 ALTER TABLE `AspNetUserRoles` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserTokens`
--

DROP TABLE IF EXISTS `AspNetUserTokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AspNetUserTokens` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserTokens`
--

LOCK TABLES `AspNetUserTokens` WRITE;
/*!40000 ALTER TABLE `AspNetUserTokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserTokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUsers`
--

DROP TABLE IF EXISTS `AspNetUsers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AspNetUsers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUsers`
--

LOCK TABLES `AspNetUsers` WRITE;
/*!40000 ALTER TABLE `AspNetUsers` DISABLE KEYS */;
INSERT INTO `AspNetUsers` VALUES ('b21f5495-25a3-4be0-aa3f-6522a4c6218b','james.bond@gmail.com','JAMES.BOND@GMAIL.COM','james.bond@gmail.com','JAMES.BOND@GMAIL.COM',0,'AQAAAAEAACcQAAAAEGIqTEiVKGTloIUDBE7RX4YqZMKHdcZScbf7LDfENAQMeNpezrWAdk5wBC6fsIYzQQ==','RCTTOQCUB4FUQZNZVOOCKIH4UKSK6J4Q','69bf26ba-0ebb-44fc-a6a5-c3eb9c40a7e9',NULL,0,0,NULL,1,0),('c8b8c9fd-10eb-4088-85cb-b184b9839d02','john.wick@gmail.com','JOHN.WICK@GMAIL.COM','john.wick@gmail.com','JOHN.WICK@GMAIL.COM',0,'AQAAAAEAACcQAAAAECXv9tNHq3h8yfta5Re7VNQv5gq9D3M+RE5zoshsDbjovUVOxK/2E1lZkQfsMCecxQ==','QNNJRHRQRPLC2GCWLM47JE3YUV5Q5TEM','52f902b3-32ee-4ec6-8876-141339feb16a',NULL,0,0,NULL,1,0);
/*!40000 ALTER TABLE `AspNetUsers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ClientClaims`
--

DROP TABLE IF EXISTS `ClientClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ClientClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Type` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientClaims_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientClaims_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ClientClaims`
--

LOCK TABLES `ClientClaims` WRITE;
/*!40000 ALTER TABLE `ClientClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `ClientClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ClientCorsOrigins`
--

DROP TABLE IF EXISTS `ClientCorsOrigins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ClientCorsOrigins` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Origin` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientCorsOrigins_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientCorsOrigins_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ClientCorsOrigins`
--

LOCK TABLES `ClientCorsOrigins` WRITE;
/*!40000 ALTER TABLE `ClientCorsOrigins` DISABLE KEYS */;
INSERT INTO `ClientCorsOrigins` VALUES (1,@client_host,1);
/*!40000 ALTER TABLE `ClientCorsOrigins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ClientGrantTypes`
--

DROP TABLE IF EXISTS `ClientGrantTypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ClientGrantTypes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `GrantType` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientGrantTypes_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientGrantTypes_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ClientGrantTypes`
--

LOCK TABLES `ClientGrantTypes` WRITE;
/*!40000 ALTER TABLE `ClientGrantTypes` DISABLE KEYS */;
INSERT INTO `ClientGrantTypes` VALUES (1,'implicit',1);
/*!40000 ALTER TABLE `ClientGrantTypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ClientIdPRestrictions`
--

DROP TABLE IF EXISTS `ClientIdPRestrictions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ClientIdPRestrictions` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Provider` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientIdPRestrictions_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientIdPRestrictions_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ClientIdPRestrictions`
--

LOCK TABLES `ClientIdPRestrictions` WRITE;
/*!40000 ALTER TABLE `ClientIdPRestrictions` DISABLE KEYS */;
/*!40000 ALTER TABLE `ClientIdPRestrictions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ClientPostLogoutRedirectUris`
--

DROP TABLE IF EXISTS `ClientPostLogoutRedirectUris`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ClientPostLogoutRedirectUris` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PostLogoutRedirectUri` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientPostLogoutRedirectUris_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientPostLogoutRedirectUris_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ClientPostLogoutRedirectUris`
--

LOCK TABLES `ClientPostLogoutRedirectUris` WRITE;
/*!40000 ALTER TABLE `ClientPostLogoutRedirectUris` DISABLE KEYS */;
INSERT INTO `ClientPostLogoutRedirectUris` VALUES (1,@login_redirect_url,1);
/*!40000 ALTER TABLE `ClientPostLogoutRedirectUris` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ClientProperties`
--

DROP TABLE IF EXISTS `ClientProperties`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ClientProperties` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Key` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientProperties_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientProperties_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ClientProperties`
--

LOCK TABLES `ClientProperties` WRITE;
/*!40000 ALTER TABLE `ClientProperties` DISABLE KEYS */;
/*!40000 ALTER TABLE `ClientProperties` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ClientRedirectUris`
--

DROP TABLE IF EXISTS `ClientRedirectUris`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ClientRedirectUris` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RedirectUri` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientRedirectUris_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientRedirectUris_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ClientRedirectUris`
--

LOCK TABLES `ClientRedirectUris` WRITE;
/*!40000 ALTER TABLE `ClientRedirectUris` DISABLE KEYS */;
INSERT INTO `ClientRedirectUris` VALUES (1, CONCAT(@client_host, '/callback') ,1);
/*!40000 ALTER TABLE `ClientRedirectUris` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ClientScopes`
--

DROP TABLE IF EXISTS `ClientScopes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ClientScopes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Scope` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientScopes_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientScopes_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ClientScopes`
--

LOCK TABLES `ClientScopes` WRITE;
/*!40000 ALTER TABLE `ClientScopes` DISABLE KEYS */;
INSERT INTO `ClientScopes` VALUES (1,'openid',1),(2,'profile',1),(3,'api1',1);
/*!40000 ALTER TABLE `ClientScopes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ClientSecrets`
--

DROP TABLE IF EXISTS `ClientSecrets`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ClientSecrets` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Description` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Expiration` datetime(6) DEFAULT NULL,
  `Type` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Created` datetime(6) NOT NULL,
  `ClientId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientSecrets_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientSecrets_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ClientSecrets`
--

LOCK TABLES `ClientSecrets` WRITE;
/*!40000 ALTER TABLE `ClientSecrets` DISABLE KEYS */;
INSERT INTO `ClientSecrets` VALUES (1,NULL,'K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=',NULL,'SharedSecret','2021-07-04 04:01:12.184858',1);
/*!40000 ALTER TABLE `ClientSecrets` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Clients`
--

DROP TABLE IF EXISTS `Clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Clients` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Enabled` tinyint(1) NOT NULL,
  `ClientId` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProtocolType` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RequireClientSecret` tinyint(1) NOT NULL,
  `ClientName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ClientUri` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `LogoUri` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `RequireConsent` tinyint(1) NOT NULL,
  `AllowRememberConsent` tinyint(1) NOT NULL,
  `AlwaysIncludeUserClaimsInIdToken` tinyint(1) NOT NULL,
  `RequirePkce` tinyint(1) NOT NULL,
  `AllowPlainTextPkce` tinyint(1) NOT NULL,
  `AllowAccessTokensViaBrowser` tinyint(1) NOT NULL,
  `FrontChannelLogoutUri` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `FrontChannelLogoutSessionRequired` tinyint(1) NOT NULL,
  `BackChannelLogoutUri` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `BackChannelLogoutSessionRequired` tinyint(1) NOT NULL,
  `AllowOfflineAccess` tinyint(1) NOT NULL,
  `IdentityTokenLifetime` int NOT NULL,
  `AccessTokenLifetime` int NOT NULL,
  `AuthorizationCodeLifetime` int NOT NULL,
  `ConsentLifetime` int DEFAULT NULL,
  `AbsoluteRefreshTokenLifetime` int NOT NULL,
  `SlidingRefreshTokenLifetime` int NOT NULL,
  `RefreshTokenUsage` int NOT NULL,
  `UpdateAccessTokenClaimsOnRefresh` tinyint(1) NOT NULL,
  `RefreshTokenExpiration` int NOT NULL,
  `AccessTokenType` int NOT NULL,
  `EnableLocalLogin` tinyint(1) NOT NULL,
  `IncludeJwtId` tinyint(1) NOT NULL,
  `AlwaysSendClientClaims` tinyint(1) NOT NULL,
  `ClientClaimsPrefix` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `PairWiseSubjectSalt` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) DEFAULT NULL,
  `LastAccessed` datetime(6) DEFAULT NULL,
  `UserSsoLifetime` int DEFAULT NULL,
  `UserCodeType` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `DeviceCodeLifetime` int NOT NULL,
  `NonEditable` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Clients_ClientId` (`ClientId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Clients`
--

LOCK TABLES `Clients` WRITE;
/*!40000 ALTER TABLE `Clients` DISABLE KEYS */;
INSERT INTO `Clients` VALUES (1,1,'spa','oidc',1,'SinglePage',NULL,NULL,NULL,0,1,0,0,0,1,NULL,1,NULL,1,0,300,3600,300,NULL,2592000,1296000,1,0,1,0,1,0,0,'client_',NULL,'2021-07-04 04:01:12.184476',NULL,NULL,NULL,NULL,300,0);
/*!40000 ALTER TABLE `Clients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `DataProtectionKeys`
--

DROP TABLE IF EXISTS `DataProtectionKeys`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `DataProtectionKeys` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FriendlyName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Xml` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `DataProtectionKeys`
--

LOCK TABLES `DataProtectionKeys` WRITE;
/*!40000 ALTER TABLE `DataProtectionKeys` DISABLE KEYS */;
INSERT INTO `DataProtectionKeys` VALUES (1,'key-c4ad4675-0bc7-4980-a50f-d71698b79013','<key id=\"c4ad4675-0bc7-4980-a50f-d71698b79013\" version=\"1\"><creationDate>2021-07-04T04:01:13.083377Z</creationDate><activationDate>2021-07-04T04:01:12.98008Z</activationDate><expirationDate>2021-10-02T04:01:12.98008Z</expirationDate><descriptor deserializerType=\"Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel.AuthenticatedEncryptorDescriptorDeserializer, Microsoft.AspNetCore.DataProtection, Version=3.1.6.0, Culture=neutral, PublicKeyToken=adb9793829ddae60\"><descriptor><encryption algorithm=\"AES_256_CBC\" /><validation algorithm=\"HMACSHA256\" /><masterKey p4:requiresEncryption=\"true\" xmlns:p4=\"http://schemas.asp.net/2015/03/dataProtection\"><!-- Warning: the key below is in an unencrypted form. --><value>pabmp8wIOJRWRYWbtVRZ5ooVM/I9mckLkFBwbR5RHGVrtcpEpZEqITC2YpjsJF1FZgHy5ER2LxbQzMZrZyi1Eg==</value></masterKey></descriptor></descriptor></key>');
/*!40000 ALTER TABLE `DataProtectionKeys` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `DeviceCodes`
--

DROP TABLE IF EXISTS `DeviceCodes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `DeviceCodes` (
  `UserCode` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DeviceCode` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SubjectId` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ClientId` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `Expiration` datetime(6) NOT NULL,
  `Data` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserCode`),
  UNIQUE KEY `IX_DeviceCodes_DeviceCode` (`DeviceCode`),
  KEY `IX_DeviceCodes_Expiration` (`Expiration`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `DeviceCodes`
--

LOCK TABLES `DeviceCodes` WRITE;
/*!40000 ALTER TABLE `DeviceCodes` DISABLE KEYS */;
/*!40000 ALTER TABLE `DeviceCodes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `IdentityClaims`
--

DROP TABLE IF EXISTS `IdentityClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `IdentityClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Type` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IdentityResourceId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_IdentityClaims_IdentityResourceId` (`IdentityResourceId`),
  CONSTRAINT `FK_IdentityClaims_IdentityResources_IdentityResourceId` FOREIGN KEY (`IdentityResourceId`) REFERENCES `IdentityResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `IdentityClaims`
--

LOCK TABLES `IdentityClaims` WRITE;
/*!40000 ALTER TABLE `IdentityClaims` DISABLE KEYS */;
INSERT INTO `IdentityClaims` VALUES (1,'preferred_username',1),(2,'nickname',1),(3,'middle_name',1),(4,'given_name',1),(5,'family_name',1),(6,'name',1),(7,'profile',1),(8,'picture',1),(9,'website',1),(10,'gender',1),(11,'birthdate',1),(12,'zoneinfo',1),(13,'locale',1),(14,'updated_at',1),(15,'sub',2);
/*!40000 ALTER TABLE `IdentityClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `IdentityProperties`
--

DROP TABLE IF EXISTS `IdentityProperties`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `IdentityProperties` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Key` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IdentityResourceId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_IdentityProperties_IdentityResourceId` (`IdentityResourceId`),
  CONSTRAINT `FK_IdentityProperties_IdentityResources_IdentityResourceId` FOREIGN KEY (`IdentityResourceId`) REFERENCES `IdentityResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `IdentityProperties`
--

LOCK TABLES `IdentityProperties` WRITE;
/*!40000 ALTER TABLE `IdentityProperties` DISABLE KEYS */;
/*!40000 ALTER TABLE `IdentityProperties` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `IdentityResources`
--

DROP TABLE IF EXISTS `IdentityResources`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `IdentityResources` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Enabled` tinyint(1) NOT NULL,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DisplayName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Required` tinyint(1) NOT NULL,
  `Emphasize` tinyint(1) NOT NULL,
  `ShowInDiscoveryDocument` tinyint(1) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) DEFAULT NULL,
  `NonEditable` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_IdentityResources_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `IdentityResources`
--

LOCK TABLES `IdentityResources` WRITE;
/*!40000 ALTER TABLE `IdentityResources` DISABLE KEYS */;
INSERT INTO `IdentityResources` VALUES (1,1,'profile','User profile','Your user profile information (first name, last name, etc.)',0,1,1,'2021-07-04 04:01:12.611429',NULL,0),(2,1,'openid','Your user identifier',NULL,1,0,1,'2021-07-04 04:01:12.585176',NULL,0);
/*!40000 ALTER TABLE `IdentityResources` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PersistedGrants`
--

DROP TABLE IF EXISTS `PersistedGrants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `PersistedGrants` (
  `Key` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SubjectId` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ClientId` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `Expiration` datetime(6) DEFAULT NULL,
  `Data` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Key`),
  KEY `IX_PersistedGrants_Expiration` (`Expiration`),
  KEY `IX_PersistedGrants_SubjectId_ClientId_Type` (`SubjectId`,`ClientId`,`Type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PersistedGrants`
--

LOCK TABLES `PersistedGrants` WRITE;
/*!40000 ALTER TABLE `PersistedGrants` DISABLE KEYS */;
/*!40000 ALTER TABLE `PersistedGrants` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20210628132243_InitialIdentityServerPersistedGrantDbMigration','3.1.1'),('20210628132252_InitialIdentityServerConfigurationDbMigration','3.1.1'),('20210628132411_InitialApplicationDb','3.1.1'),('20210628132454_InitialMyKeys','3.1.1');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;


-- Host: localhost    Database: chat-app
DROP DATABASE IF EXISTS `chat-app`;
CREATE DATABASE `chat-app`;
USE `chat-app`;

--
-- Table structure for table `Contacts`
--

DROP TABLE IF EXISTS `Contacts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Contacts` (
  `Id` char(36) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  `FirstName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` char(36) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Contacts`
--

LOCK TABLES `Contacts` WRITE;
/*!40000 ALTER TABLE `Contacts` DISABLE KEYS */;
INSERT INTO `Contacts` VALUES ('4316afa5-3482-4470-acc5-c0c488cefd74','2022-02-26 04:53:05.705406','0001-01-01 00:00:00.000000','john','wick','24ce6482-6569-4c90-a938-fa8ac85606d7'),('e51e1c91-a048-4add-890c-3853642e5a4f','2022-02-26 04:52:27.764458','0001-01-01 00:00:00.000000','james','bond','656717b7-6dcb-452d-aa6f-4691b45dac1d');
/*!40000 ALTER TABLE `Contacts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Conversations`
--

DROP TABLE IF EXISTS `Conversations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Conversations` (
  `Id` char(36) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  `Title` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `CreatorId` char(36) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Conversations_CreatorId` (`CreatorId`),
  CONSTRAINT `FK_Conversations_Users_CreatorId` FOREIGN KEY (`CreatorId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Conversations`
--

LOCK TABLES `Conversations` WRITE;
/*!40000 ALTER TABLE `Conversations` DISABLE KEYS */;
INSERT INTO `Conversations` VALUES ('692355f1-c3fa-4e2d-a39b-a07400638c20','2022-02-26 12:08:39.082669','0001-01-01 00:00:00.000000','john wick','656717b7-6dcb-452d-aa6f-4691b45dac1d');
/*!40000 ALTER TABLE `Conversations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `FileStorages`
--

DROP TABLE IF EXISTS `FileStorages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `FileStorages` (
  `Id` char(36) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ContentType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Data` longblob,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `FileStorages`
--

LOCK TABLES `FileStorages` WRITE;
/*!40000 ALTER TABLE `FileStorages` DISABLE KEYS */;
/*!40000 ALTER TABLE `FileStorages` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `FriendsRequests`
--

DROP TABLE IF EXISTS `FriendsRequests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `FriendsRequests` (
  `Id` char(36) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  `ReceiverId` char(36) NOT NULL,
  `RequesterId` char(36) NOT NULL,
  `Status` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `FriendsRequests`
--

LOCK TABLES `FriendsRequests` WRITE;
/*!40000 ALTER TABLE `FriendsRequests` DISABLE KEYS */;
INSERT INTO `FriendsRequests` VALUES ('9c933ea2-2f1d-49db-8726-6d829f4756e4','2022-02-26 04:53:20.151907','0001-01-01 00:00:00.000000','656717b7-6dcb-452d-aa6f-4691b45dac1d','24ce6482-6569-4c90-a938-fa8ac85606d7',1);
/*!40000 ALTER TABLE `FriendsRequests` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Members`
--

DROP TABLE IF EXISTS `Members`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Members` (
  `UserId` char(36) NOT NULL,
  `ConversationId` char(36) NOT NULL,
  PRIMARY KEY (`ConversationId`,`UserId`),
  CONSTRAINT `FK_Members_Conversations_ConversationId` FOREIGN KEY (`ConversationId`) REFERENCES `Conversations` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Members`
--

LOCK TABLES `Members` WRITE;
/*!40000 ALTER TABLE `Members` DISABLE KEYS */;
INSERT INTO `Members` VALUES ('24ce6482-6569-4c90-a938-fa8ac85606d7','692355f1-c3fa-4e2d-a39b-a07400638c20'),('656717b7-6dcb-452d-aa6f-4691b45dac1d','692355f1-c3fa-4e2d-a39b-a07400638c20');
/*!40000 ALTER TABLE `Members` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Messages`
--

DROP TABLE IF EXISTS `Messages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Messages` (
  `Id` char(36) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  `Text` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SenderId` char(36) NOT NULL,
  `ConversationId` char(36) NOT NULL,
  `AttachmentUrl` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `MessageType` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Messages_ConversationId` (`ConversationId`),
  KEY `IX_Messages_SenderId` (`SenderId`),
  CONSTRAINT `FK_Messages_Conversations_ConversationId` FOREIGN KEY (`ConversationId`) REFERENCES `Conversations` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Messages_Users_SenderId` FOREIGN KEY (`SenderId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Messages`
--

LOCK TABLES `Messages` WRITE;
/*!40000 ALTER TABLE `Messages` DISABLE KEYS */;
/*!40000 ALTER TABLE `Messages` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `NotificationTypes`
--

DROP TABLE IF EXISTS `NotificationTypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `NotificationTypes` (
  `Id` char(36) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Type` int NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `NotificationTypes`
--

LOCK TABLES `NotificationTypes` WRITE;
/*!40000 ALTER TABLE `NotificationTypes` DISABLE KEYS */;
/*!40000 ALTER TABLE `NotificationTypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Notifications`
--

DROP TABLE IF EXISTS `Notifications`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Notifications` (
  `Id` char(36) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  `RecipientId` char(36) NOT NULL,
  `SenderId` char(36) NOT NULL,
  `Read` tinyint(1) NOT NULL,
  `Content` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `TypeId` char(36) DEFAULT NULL,
  `NotificationTypeId` char(36) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Notifications_NotificationTypeId` (`NotificationTypeId`),
  KEY `IX_Notifications_RecipientId` (`RecipientId`),
  KEY `IX_Notifications_SenderId` (`SenderId`),
  KEY `IX_Notifications_TypeId` (`TypeId`),
  CONSTRAINT `FK_Notifications_NotificationTypes_NotificationTypeId` FOREIGN KEY (`NotificationTypeId`) REFERENCES `NotificationTypes` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_Notifications_NotificationTypes_TypeId` FOREIGN KEY (`TypeId`) REFERENCES `NotificationTypes` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_Notifications_Users_RecipientId` FOREIGN KEY (`RecipientId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Notifications_Users_SenderId` FOREIGN KEY (`SenderId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Notifications`
--

LOCK TABLES `Notifications` WRITE;
/*!40000 ALTER TABLE `Notifications` DISABLE KEYS */;
/*!40000 ALTER TABLE `Notifications` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Participants`
--

DROP TABLE IF EXISTS `Participants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Participants` (
  `Id` char(36) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  `ConversationId` char(36) NOT NULL,
  `UserId` char(36) NOT NULL,
  `Type` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Participants`
--

LOCK TABLES `Participants` WRITE;
/*!40000 ALTER TABLE `Participants` DISABLE KEYS */;
/*!40000 ALTER TABLE `Participants` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ReadReceipts`
--

DROP TABLE IF EXISTS `ReadReceipts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ReadReceipts` (
  `MessageId` char(36) NOT NULL,
  `SeenerId` char(36) NOT NULL,
  `ConversationId` char(36) NOT NULL,
  PRIMARY KEY (`MessageId`,`SeenerId`),
  CONSTRAINT `FK_ReadReceipts_Messages_MessageId` FOREIGN KEY (`MessageId`) REFERENCES `Messages` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ReadReceipts`
--

LOCK TABLES `ReadReceipts` WRITE;
/*!40000 ALTER TABLE `ReadReceipts` DISABLE KEYS */;
/*!40000 ALTER TABLE `ReadReceipts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `UserContacts`
--

DROP TABLE IF EXISTS `UserContacts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `UserContacts` (
  `UserId` char(36) NOT NULL,
  `ContactId` char(36) NOT NULL,
  `Id` char(36) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  PRIMARY KEY (`UserId`,`ContactId`),
  KEY `IX_UserContacts_ContactId` (`ContactId`),
  CONSTRAINT `FK_UserContacts_Contacts_ContactId` FOREIGN KEY (`ContactId`) REFERENCES `Contacts` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_UserContacts_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UserContacts`
--

LOCK TABLES `UserContacts` WRITE;
/*!40000 ALTER TABLE `UserContacts` DISABLE KEYS */;
INSERT INTO `UserContacts` VALUES ('24ce6482-6569-4c90-a938-fa8ac85606d7','e51e1c91-a048-4add-890c-3853642e5a4f','cf80ef25-811b-4012-a891-7891fd427ef1','2022-02-26 12:08:31.436751','0001-01-01 00:00:00.000000'),('656717b7-6dcb-452d-aa6f-4691b45dac1d','4316afa5-3482-4470-acc5-c0c488cefd74','aee59fcc-b2de-4221-8157-51e6eb3b38e5','2022-02-26 12:08:31.436447','0001-01-01 00:00:00.000000');
/*!40000 ALTER TABLE `UserContacts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Users` (
  `Id` char(36) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  `FirstName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES ('24ce6482-6569-4c90-a938-fa8ac85606d7','2022-02-26 04:53:05.705370','0001-01-01 00:00:00.000000','john','wick','john.wick@gmail.com',NULL),('656717b7-6dcb-452d-aa6f-4691b45dac1d','2022-02-26 04:52:27.764432','0001-01-01 00:00:00.000000','james','bond','james.bond@gmail.com',NULL);
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20210628131710_InitCreate','3.1.1');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-02-26 12:53:08
