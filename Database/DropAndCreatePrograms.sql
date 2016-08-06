USE [NewhlDB]
GO

/****** Object:  Table [dbo].[Programs]    Script Date: 08/03/2016 12:39:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Programs]') AND type in (N'U'))
DROP TABLE [dbo].[Programs]
GO

USE [NewhlDB]
GO

/****** Object:  Table [dbo].[Programs]    Script Date: 08/03/2016 12:39:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[Programs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DayOfWeek] [nvarchar](30) NOT NULL,
	[StartTime] [nvarchar](30) NOT NULL,
	[Price] DECIMAL(19,4) NOT NULL,
	[StartDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Programs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO	