USE [sample]
GO

/****** Object:  Table [dbo].[expense]    Script Date: 25-05-2023 18:29:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[expense](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[amount] [real] NOT NULL,
	[date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


