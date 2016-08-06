USE [NewhlDb]
GO

ALTER TABLE [dbo].[AMFUsers] ADD Other nvarchar(5) NULL;
GO

ALTER TABLE [dbo].[AMFUsers] ALTER COLUMN Address2 nvarchar(255) NULL;
GO

DROP Table [dbo].[Payments]
GO

/****** Object:  Table [dbo].[Payments]    Script Date: 08/03/2016 12:42:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Payments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PlayerId] [bigint] NOT NULL,
	[Amount] float NOT NULL,
	[PaymentMethod] int NOT NULL,
	[DateSubmitted] datetime not NULL,
	[DateVerified] datetime NULL,
	[VerificationIdentifier] nvarchar(255) NULL,
	[AdditionalDetails] nvarchar(512) NULL
 CONSTRAINT [PK_PAYMENTS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  ForeignKey [FK_Payments_Users]    Script Date: 09/15/2014 08:05:13 ******/
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Users] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[AMFUsers] ([Id])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Users]
GO