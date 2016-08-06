USE [NewhlDB]
GO

/****** Object:  Table [dbo].[AMFUsers]    Script Date: 08/02/2016 12:47:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AMFUsers]') AND type in (N'U'))
DROP TABLE [dbo].[AMFUsers]
GO

USE [NewhlDB]
GO

/****** Object:  Table [dbo].[AMFUsers]    Script Date: 08/02/2016 12:47:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AMFUsers](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[USAHockeyNum] [nvarchar](30) NOT NULL,
	[DOB] [datetime] NOT NULL,
	[Address1] [nvarchar](255) NOT NULL,
	[Address2] [nvarchar](255) NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
	[ZipCode] [nvarchar](10) NOT NULL,
	[Phone1] [nvarchar](20) NOT NULL,
	[Phone2] [nvarchar](20) NULL,
	[Emergency1] [nvarchar](50) NOT NULL,
	[Emergency2] [nvarchar](50) NOT NULL,
	[YearsExp] [int] NOT NULL,
	[Level] [nvarchar](5) NOT NULL,
	[Internet] [nvarchar](5) NULL,
	[Referral] [nvarchar](5) NULL,
	[Tournament] [nvarchar](5) NULL,
	[Other] [nvarchar](5) NULL,
	[LTP] [nvarchar](5) NULL,
	[Tuesday] [nvarchar](5) NULL,
	[Wednesday] [nvarchar](5) NULL,
	[Stickhandling] [nvarchar](5) NULL,
	[Somerville] [nvarchar](5) NULL,
	[Games] [nvarchar](5) NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserStatus] [int] NULL,
	[Role] [int] NULL,
	[PasswordSalt] [nchar](50) NULL,
	[PasswordHash] [nchar](50) NULL,
	[PasswordHint] [nvarchar](50) NULL,
 CONSTRAINT [PK_AMFUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


