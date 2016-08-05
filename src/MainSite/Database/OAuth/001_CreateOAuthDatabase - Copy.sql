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
	PRIMARY KEY (`Id`),
	UNIQUE INDEX `IX_AMFUsers_Id` (`Id` ASC));

CREATE UNIQUE INDEX `IX_AMFUsers_Email` ON `AMFUsers` 
(
	`Email` ASC
);


String FirstName, String LastName, String Email, String confirm, String USAHockeyNum, DateTime DOB, String Address1, String Address2, String City, String State, String Zip, String Phone1, String Phone2, String Emergency1, String Emergency2, int YearsExp, String Internet, String Referral, String Tournament, String Other, String LTP, String Tues, String Wed, String Stickhandling, String Games)
