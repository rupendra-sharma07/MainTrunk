--7:33 PM 1/27/2011 LHK:ObituaryNote Message
ALTER TABLE dbo.TRIBUTES
ADD PostMessage text,MessageWithoutHtml text

--Mohit
-- TributeURL for Tribute creation and upgrade

Alter table tributes add OldTributeURL varchar(max)

Go
update tributes set oldtributeURl=tributeurl

Go 
ALTER procedure [dbo].[usp_TributeDomainAvaibality]
(
@TributeUrl varchar(250),
@TributeType int
)
as
BEGIN
select count(TributeUrl) from dbo.TRIBUTES where (TributeUrl=@TributeUrl
and TributeType=@TributeType) or (OldTributeUrl=@TributeUrl and TributeType=@TributeType)
END


alter table dbo.PACKAGE alter column Price numeric(18,2)

INSERT dbo.PACKAGE VALUES ( 'Tribute LifeTime', 99.95, 1, 1, 0, NULL, '20110118 00:00:00:000', NULL, NULL, 1, 0)
INSERT dbo.PACKAGE VALUES ( 'Tribute Yearly', 39.95, 1, 1, 1, NULL, '20110118 00:00:00:000', NULL, NULL, 1, 0)
INSERT dbo.PACKAGE VALUES ( 'Photo LifeTime', 49.95, 1, 1, 1, NULL, '20110118 00:00:00:000', NULL, NULL, 1, 0)
INSERT dbo.PACKAGE VALUES ( 'Photo Yearly', 19.95, 0, 1, 1, NULL, '20110118 00:00:00:000', NULL, NULL, 1, 0)
INSERT dbo.PACKAGE VALUES ( 'Announcement', 0.00, 0, 1, 1, NULL, '20110118 00:00:00:000', NULL, NULL, 1, 0)