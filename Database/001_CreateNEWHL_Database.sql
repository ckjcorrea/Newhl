USE [NewhlDb]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 07/31/2016 00:06:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[LoginAttemps]    Script Date: 03/12/2015 12:16:42 ******/
CREATE TABLE [dbo].[LoginAttempts](
	[Id] BIGINT  IDENTITY(1,1) NOT NULL,
	[WasSuccessfull] bit NOT NULL,
	[AttemptDate] datetime NOT NULL,
	[Source] nvarchar(255) NOT NULL,
	[UserName] nvarchar(255) NOT NULL,
CONSTRAINT [PK_LoginAttemps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]



USE [NewhlDb]
GO

/****** Object:  Table [dbo].[AMFUsers]    Script Date: 07/31/2016 21:44:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AMFUsers](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[USAHockeyNum] [nvarchar](30) NOT NULL,
	[DOB] [datetime] NOT NULL,
	[Address1] [nvarchar](255) NOT NULL,
	[Address2] [nvarchar](255) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](15) NOT NULL,
	[Zip] [nvarchar](10) NOT NULL,
	[Phone1] [nvarchar](12) NOT NULL,
	[Phone2] [nvarchar](12) NULL,
	[Emergency1] [nvarchar](255) NOT NULL,
	[Emergency2] [nvarchar](255) NOT NULL,
	[YearsExp] [int] NOT NULL,
	[Level] [nvarchar](5) NOT NULL,
	[Internet] [nvarchar](5) NULL,
	[Referral] [nvarchar](5) NULL,
	[Tournament] [nvarchar](5) NULL,
	[LTP] [nvarchar](5) NULL,
	[Tuesday] [nvarchar](5) NULL,
	[Wednesday] [nvarchar](5) NULL,
	[Stickhandling] [nvarchar](5) NULL,
	[Somerville] [nvarchar](5) NULL,
	[Games] [nvarchar](5) NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserStatus] [int] NULL,
	[Role] [int]  NULL,
	[PasswordSalt] [nchar](50) NULL,
	[PasswordHash] [nchar](50) NULL,
	[PasswordHint] [nvarchar](50) NULL,
 CONSTRAINT [PK_AMFUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[Programs]    Script Date: 07/30/2016 12:16:42 ******/
CREATE TABLE [dbo].[Programs](
	[Id] BIGINT  IDENTITY(1,1) NOT NULL,
	[ProgramName] nvarchar(30) NOT NULL,
	[ProgramDay] nvarchar(15) NOT NULL,
	[Location] nvarchar(15) NOT NULL,
	[Price] float NOT NULL,
	[StartDate] datetime  NOT NULL,
CONSTRAINT [PK_PROGRAMS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[ProgramRegistrations]    Script Date: 07/30/2016 12:16:42 ******/
CREATE TABLE [dbo].[ProgramRegistrations](
	[Id] BIGINT  IDENTITY(1,1) NOT NULL,
	[PlayerID] BIGINT  NOT NULL,
	[ProgramID] BIGINT NOT NULL,
	[Season] nvarchar(15) NOT NULL,
CONSTRAINT [PK_PROGRAMREGISTRATIONS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[Payments]    Script Date: 07/30/2016 12:16:42 ******/
CREATE TABLE [dbo].[Payments](
	[Id] BIGINT  IDENTITY(1,1) NOT NULL,
	[PlayerID] BIGINT NOT NULL,
	[ProgramDays] nvarchar(128)  NOT NULL,
	[PaymentTotalDue] float NOT NULL,
	[PaymentMethod] nvarchar(15) NOT NULL,
	[CheckNumber] nvarchar(15) NULL,
	[PaymentAmount] float NOT NULL,
	[PaymentDate] datetime NOT NULL,
CONSTRAINT [PK_PAYMENTS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
