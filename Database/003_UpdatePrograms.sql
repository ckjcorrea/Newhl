USE [NewhlDb]
GO

/****** Object:  Table [dbo].[Payments]    Script Date: 08/03/2016 12:42:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Seasons](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[StartDate] date NOT NULL,
	[EndDate] date NOT NULL,
	[DateCreated] datetime not NULL,
	[IsActive] [bit] NOT NULL
 CONSTRAINT [PK_Seasons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Programs] ADD SeasonId bigint NOT NULL;
GO

/****** Object:  ForeignKey [FK_Programs_Seasons]    Script Date: 09/15/2014 08:05:13 ******/
ALTER TABLE [dbo].[Programs]  WITH CHECK ADD  CONSTRAINT [FK_Programs_Seasons] FOREIGN KEY([SeasonId])
REFERENCES [dbo].[Seasons] ([Id])
GO
ALTER TABLE [dbo].[Programs] CHECK CONSTRAINT [FK_Programs_Sessions]
GO
ALTER Table [dbo].[Programs] ADD Location nvarchar(512) NOT NULL Default 'Location'
GO
ALTER Table [dbo].[Programs] DROP COLUMN StartDate;
GO
ALTER Table [dbo].[Programs] DROP COLUMN EndDate;
GO
ALTER Table [dbo].[Programs] Add DateCreated datetime default '1/1/2016'
GO
ALTER Table [dbo].[Programs] DROP Column DayOfWeek;
GO
ALTER Table [dbo].[Programs] Add DayOfWeek Integer default 1
GO

CREATE TABLE [dbo].[PlayerSeasons](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SeasonId] [bigint] NOT NULL,
	[PlayerId] [bigint] NOT NULL
 CONSTRAINT [PK_PlayerSeasons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PlayerSeasons]  WITH CHECK ADD  CONSTRAINT [FK_PlayerSeasons_Season] FOREIGN KEY([SeasonId])
REFERENCES [dbo].[Seasons] ([Id])
GO
ALTER TABLE [dbo].[PlayerSeasons]  WITH CHECK ADD  CONSTRAINT [FK_PlayerSeasons_Players] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[AMFUsers] ([Id])
GO
CREATE TABLE [dbo].[PlayerSeasonPrograms](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PlayerSeasonId] [bigint] NOT NULL,
	[ProgramId] [bigint] NOT NULL
 CONSTRAINT [PK_PlayerSeasonPrograms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PlayerSeasonPrograms]  WITH CHECK ADD  CONSTRAINT [FK_PlayerSeasonPrograms_PlayerSeason] FOREIGN KEY([PlayerSeasonId])
REFERENCES [dbo].[PlayerSeasons] ([Id])
GO 
DROP Table [dbo].[Payments]
GO

CREATE TABLE [dbo].[Payments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PlayerSeasonId] [bigint] NOT NULL,
	[Amount] float NOT NULL,
	[PaymentMethod] nvarchar(20) NOT NULL,
	[DateSubmitted] datetime not NULL,
	[DateVerified] datetime NULL,
	[VerificationIdentifier] nvarchar(255) NULL,
	[AdditionalDetails] nvarchar(512) NULL,
	[PaymentState] int NOT NULL,
	[TransactionId] nvarchar(36) NOT NULL
 CONSTRAINT [PK_PAYMENTS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  ForeignKey [FK_Payments_PlayerSeasons]    Script Date: 09/15/2014 08:05:13 ******/
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_PlayerSeasons] FOREIGN KEY([PlayerSeasonId])
REFERENCES [dbo].[PlayerSeasons] ([Id])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_PlayerSeasons]
GO
ALTER Table [dbo].[AMFUsers] DROP Column LTP
GO
ALTER Table [dbo].[AMFUsers] DROP Column Tuesday
GO
ALTER Table [dbo].[AMFUsers] DROP Column Wednesday
GO
ALTER Table [dbo].[AMFUsers] DROP Column Stickhandling
GO
ALTER Table [dbo].[AMFUsers] DROP Column Somerville
GO
ALTER Table [dbo].[AMFUsers] DROP Column Games
GO

