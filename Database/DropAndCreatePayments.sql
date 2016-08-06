USE [NewhlDB]
GO

/****** Object:  Table [dbo].[Payments]    Script Date: 08/03/2016 12:42:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Payments]') AND type in (N'U'))
DROP TABLE [dbo].[Payments]
GO

USE [NewhlDB]
GO

/****** Object:  Table [dbo].[Payments]    Script Date: 08/03/2016 12:42:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Payments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PlayerID] [bigint] NOT NULL,
	[ProgramDays] [nvarchar](128) NOT NULL,
	[PaymentTotalDue] [float] NOT NULL,
	[PaymentMethod] [nvarchar](15) NOT NULL,
	[CheckNumber] [nvarchar](15) NULL,
	[PaymentAmount] [float] NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PAYMENTS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


