--LHK: YT-Wordpress changes (5:25 PM 3/29/2011)

alter table Users
add AtomEnabled bit Default 'False'

update Users set AtomEnabled='False'


--LHK for getting obituary message in feed.
ALTER TABLE TRIBUTES ALTER COLUMN MessageWithoutHtml VARCHAR(MAX)


/******LHK: table to mapp wordpress site on UserId****/
GO
/****** Object:  Table [dbo].[WordPressSite]    Script Date: 08/18/2011 10:09:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WordPressSite](
	[WordPressSiteId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[WordPressSite] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_WordPressSite] PRIMARY KEY CLUSTERED 
(
	[WordPressSiteId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF