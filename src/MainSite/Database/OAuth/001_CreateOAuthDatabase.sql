/****** Object:  Table [AMFOAuth].[AMFOAuth]    Script Date: 03/12/2015 12:16:42 ******/
CREATE TABLE `LoginAttempts`(
	`Id` BIGINT NOT NULL AUTO_INCREMENT,
	`WasSuccessfull` bit NOT NULL,
	`AttemptDate` datetime NOT NULL,
	`Source` nvarchar(255) NOT NULL,
	`UserName` nvarchar(255) NOT NULL,
	PRIMARY KEY (`Id`),
	UNIQUE INDEX `IX_LoginAttempts_Id` (`Id` ASC));

CREATE INDEX `IX_LoginAttempts_UserName` ON `LoginAttempts` 
(
	`UserName` ASC
);

/****** Object:  Table [dbo].[Consumers]    Script Date: 03/12/2015 12:16:42 ******/
CREATE TABLE `Consumers`(
	`ConsumerKey` nvarchar(36) NOT NULL,
	`ConsumerSecret` nvarchar(36) NOT NULL,
	`PublicKey` nvarchar(255) NOT NULL,
	`Name` nvarchar(100) NOT NULL,
	`ContactEmail` nvarchar(255) NOT NULL,
	`AutoGrant` bit NOT NULL,
	`AccessTokenLifetime` int NOT NULL,
	PRIMARY KEY (`ConsumerKey`),
	UNIQUE INDEX `IX_Consumers_ConsumerKey` (`ConsumerKey` ASC));

CREATE TABLE `ConsumerNonce`(
	`Nonce` nvarchar(36) NOT NULL,
	`ConsumerKey` nvarchar(36) NOT NULL,
	`Timestamp` datetime NOT NULL,
	PRIMARY KEY (`Nonce`),
	UNIQUE INDEX `IX_ConsumerNonce_Nonce` (`Nonce` ASC));

/****** Object:  Table [dbo].[AMFUsers]    Script Date: 03/12/2015 12:16:42 ******/
CREATE TABLE `AMFUsers`(
	`Id` BIGINT NOT NULL AUTO_INCREMENT,
	`Email` nvarchar(255) NOT NULL,
	`FirstName` nvarchar(30) NOT NULL,
	`LastName` nvarchar(30) NOT NULL,
	`PasswordSalt` nvarchar(50) NOT NULL,
	`PasswordHash` nvarchar(50) NOT NULL,
	`PasswordHint` nvarchar(255) NULL,
	`DateCreated` datetime NOT NULL,
	`UserStatus` int NOT NULL,
	`Role` int NOT NULL,
	`ResetToken` nvarchar(36) NULL,
	PRIMARY KEY (`Id`),
	UNIQUE INDEX `IX_AMFUsers_Id` (`Id` ASC));

CREATE UNIQUE INDEX `IX_AMFUsers_Email` ON `AMFUsers` 
(
	`Email` ASC
);

CREATE TABLE `AccessTokens`(
	`Id` BIGINT NOT NULL AUTO_INCREMENT,
	`ConsumerKey` nvarchar(36) NOT NULL,
	`ExpirationDate` datetime NOT NULL,
	`Realm` nvarchar(512) NOT NULL,
	`Token` nvarchar(36) NOT NULL,
	`TokenSecret` nvarchar(36) NOT NULL,
	`UserName` nvarchar(100) NOT NULL,
	`UserId` bigint NOT NULL,
	`DateGranted` datetime NOT NULL,
	`DateCreated` datetime NOT NULL,
	PRIMARY KEY (`Id`),
	UNIQUE INDEX `IX_AccessTokens_Id` (`Id` ASC));

CREATE UNIQUE INDEX `IX_AccessTokens_Token` ON `AccessTokens` 
(
	`Token` ASC
);

CREATE TABLE `RequestTokens`(
	`Id` BIGINT NOT NULL AUTO_INCREMENT,
	`ConsumerKey` nvarchar(36) NOT NULL,
	`Realm` nvarchar(512) NULL,
	`Token` nvarchar(36) NOT NULL,
	`TokenSecret` nvarchar(36) NOT NULL,
	`ExpirationDate` datetime NOT NULL,
	`CallbackUrl` nvarchar(1024) NOT NULL,
	`State` int NOT NULL,
	`UserId` bigint NULL,
	`UserName` nvarchar(255) NULL,
	`VerifierCode` nvarchar(36) NULL,
	`DateAuthorized` datetime NULL,
	`AccessTokenId` bigint NULL,
	`DateCreated` datetime NOT NULL,
	PRIMARY KEY (`Id`),
	UNIQUE INDEX `IX_RequestTokens_Id` (`Id` ASC));

CREATE UNIQUE INDEX `IX_RequestTokens_Token` ON `RequestTokens` 
(
	`Token` ASC
);

/****** Object:  ForeignKey [FK_RequestTokens_AccessTokens]    Script Date: 03/12/2015 12:16:42 ******/
ALTER TABLE `RequestTokens`  ADD  CONSTRAINT `FK_RequestTokens_AccessTokens` FOREIGN KEY(`AccessTokenId`)
REFERENCES `AccessTokens` (`Id`);
