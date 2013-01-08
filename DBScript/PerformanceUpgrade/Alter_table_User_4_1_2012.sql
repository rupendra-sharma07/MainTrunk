ALTER TABLE USERS
Add [EnableXMLFeed] [bit] NULL DEFAULT ('False')

update users set [EnableXMLFeed]='False'