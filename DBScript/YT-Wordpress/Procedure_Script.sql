set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

    
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    

ALTER PROCEDURE [dbo].[usp_GetTributesFeed]    ---8962,1,1,100
 -- Add the parameters for the stored procedure here    
@Userid int,    
@TributeTypeId int,    
@CurrentPage As int,    
@PageSize As int    
    
AS    
    
-- Declare variables.    
Declare @FirstRec int    
Declare @LastRec int    
-- Initialize variables.    
Set @FirstRec = (@CurrentPage - 1) * @PageSize    
Set @LastRec = (@CurrentPage * @PageSize + 1);    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
if(@TributeTypeId=0 OR @TributeTypeId=1)        
BEGIN    
With TempCommentTable AS    
(    
select    
ROW_NUMBER() OVER (ORDER BY trb.CreatedDate desc) AS 'RowNumber',    
 trb.TributeId,    
 trb.TributeName, 
 trb.TributeImage,
 trb.Date1,
 trb.Date2,
 trb.MessageWithoutHtml,
 ptc.TypeDescription,    
 trb.CreatedDate as StartDate,    
 tpg.StartDate as Renewaldate,    
    --(SELECT MAX(Enddate)  FROM TRIBUTEPACKAGE where UserTributeId=trb.TributeId and IsDeleted=0 )as Enddate,    
 ISNULL(convert(varchar,tpg.EndDate,101),'Never') as Enddate,    
 (select count(*)  from dbo.WEBSTATISTICS wst where wst.SectionTypeId=trb.TributeId) as Visit,    
 trb.EmailAlert,    
trb.TributeUrl,
tpg.PackageId        
from tributes trb,dbo.PARAMETERSTYPESCODES ptc,dbo.TRIBUTEPACKAGE tpg    
where trb.TributeID    
in (select UserTributeId from dbo.TRIBUTEADMINISTRATOR where UserId=@Userid 
--and isactive=1 
and isdeleted=0)    
and ptc.TypeCode=trb.TributeType    
and ptc.ParameterType='TRIBUTE_TYPE'    
--and tpg.UserId=trb.UserTributeId    
and tpg.UserTributeId=trb.TributeId    
--and trb.IsActive=1
and trb.TributeType=7      
and trb.IsDeleted=0    
and tpg.CreatedDate=(SELECT MAX(CreatedDate)  FROM TRIBUTEPACKAGE where UserTributeId=trb.TributeId and IsDeleted=0 )    
    
)    
Select     
Distinct(TributeId),    
 TributeName,
 TributeImage,
 Date1,
 Date2,
 MessageWithoutHtml,
 TypeDescription,    
  StartDate,    
  Renewaldate,    
  Enddate,    
 Visit,    
 EmailAlert,    
   TributeUrl,
	PackageId            
 from TempCommentTable    
  where RowNumber > @FirstRec and RowNumber < @LastRec    
order by StartDate desc    
END    
ELSE    
BEGIN    
With TempCommentTable1 AS    
(    
select    
    ROW_NUMBER() OVER (ORDER BY trb.CreatedDate desc) AS 'RowNumber',    
 trb.TributeId,    
 trb.TributeName,
 trb.TributeImage,
 trb.Date1,
 trb.Date2,
trb.MessageWithoutHtml ,
 ptc.TypeDescription,    
 trb.CreatedDate as StartDate,    
 tpg.StartDate as Renewaldate,    
 ISNULL(convert(varchar,tpg.EndDate,101),'Never') as Enddate,     
 (select count(*)  from dbo.WEBSTATISTICS wst where wst.SectionTypeId=trb.TributeId) as Visit,    
 trb.EmailAlert,    
trb.TributeUrl,
tpg.PackageId
from tributes trb,dbo.PARAMETERSTYPESCODES ptc,dbo.TRIBUTEPACKAGE tpg    
where trb.TributeID    
in (select UserTributeId from dbo.TRIBUTEADMINISTRATOR where UserId=@Userid 
--and isactive=1 
and isdeleted=0)    
and ptc.TypeCode=trb.TributeType    
and ptc.ParameterType='TRIBUTE_TYPE'    
and ptc.TypeCode=@TributeTypeId    
--and tpg.UserId=trb.UserTributeId    
and tpg.UserTributeId=trb.TributeId    
--and trb.IsActive=1 
and trb.TributeType=7 
and trb.IsDeleted=0    
and tpg.CreatedDate=(SELECT MAX(CreatedDate)  FROM TRIBUTEPACKAGE where UserTributeId=trb.TributeId and IsDeleted=0 )    
)    
Select   
Distinct(TributeId),    
 TributeName,
 TributeImage, 
 Date1,
 Date2,
MessageWithoutHtml,
 TypeDescription,    
  StartDate,    
  Renewaldate,    
  Enddate,    
 Visit,    
 EmailAlert,    
  TributeUrl,
  PackageId            
 from TempCommentTable1    
 where RowNumber > @FirstRec and RowNumber < @LastRec    
 order by StartDate desc    
END    

END  

-----------------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[usp_AtomEnableOnUserIdOrName]
(
@UserId int,
@UserName varchar(255),
@AtomEnabled bit
)
as
Begin
SET NOCOUNT ON;
Declare @cnt int
if (@UserId > 0)
	Begin
		Select @cnt= 	Count(UserId) 
				from	dbo.USERS 
		where	UserId = @UserId and UserType = 2 and IsDeleted = 0
	If @cnt > 0
		Begin
			Update USERS
			Set AtomEnabled = @AtomEnabled
			where UserId=@UserId
			Select 1 as UpdateOutput, @UserId as UserId 
		End
else
Select 0 as UpdateOutput, 0 as UserId
	End	
Else
	Begin
		Select @cnt= Count(UserId) 
				from	dbo.USERS 
		where	UserName = @UserName and UserType = 2 and IsDeleted = 0
	If @cnt > 0
		Begin
			Update USERS
			Set AtomEnabled = @AtomEnabled
			where UserName = @UserName
			Select 1 as UpdateOutput, UserId from USERS where UserName = @UserName
		End
else
Select 0 as UpdateOutput, 0 as UserId
	End				

END


------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
create Procedure [dbo].[usp_GetUserDetailsOnUserId]
@UserId int
AS
BEGIN
	Select 
	UserName,
	UserType,
	AtomEnabled
	from USERS
		Where @UserId=UserId and UserType = 2 and IsDeleted = 0
END

---------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

    
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    

ALTER PROCEDURE [dbo].[usp_GetTributesFeed]    ---8962,1,1,100
 -- Add the parameters for the stored procedure here    
@Userid int,    
@TributeTypeId int,    
@CurrentPage As int,    
@PageSize As int    
    
AS    
    
-- Declare variables.    
Declare @FirstRec int    
Declare @LastRec int    
-- Initialize variables.    
Set @FirstRec = (@CurrentPage - 1) * @PageSize    
Set @LastRec = (@CurrentPage * @PageSize + 1);    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
if(@TributeTypeId=0 OR @TributeTypeId=1)        
BEGIN    
With TempCommentTable AS    
(    
select    
ROW_NUMBER() OVER (ORDER BY trb.CreatedDate desc) AS 'RowNumber',    
 trb.TributeId,    
 trb.TributeName, 
 trb.TributeImage,
 trb.Date1,
 trb.Date2,
 trb.MessageWithoutHtml,
trb.ModifiedDate,
 trb.countryname,
 ptc.TypeDescription,    
 trb.CreatedDate as StartDate,    
 tpg.StartDate as Renewaldate,    
    --(SELECT MAX(Enddate)  FROM TRIBUTEPACKAGE where UserTributeId=trb.TributeId and IsDeleted=0 )as Enddate,    
 ISNULL(convert(varchar,tpg.EndDate,101),'Never') as Enddate,    
 (select count(*)  from dbo.WEBSTATISTICS wst where wst.SectionTypeId=trb.TributeId) as Visit,    
 trb.EmailAlert,    
trb.TributeUrl,
tpg.PackageId        
from tributes trb,dbo.PARAMETERSTYPESCODES ptc,dbo.TRIBUTEPACKAGE tpg    
where trb.TributeID    
in (select UserTributeId from dbo.TRIBUTEADMINISTRATOR where UserId=@Userid 
--and isactive=1 
and isdeleted=0)    
and ptc.TypeCode=trb.TributeType    
and ptc.ParameterType='TRIBUTE_TYPE'    
--and tpg.UserId=trb.UserTributeId    
and tpg.UserTributeId=trb.TributeId    
--and trb.IsActive=1
and trb.TributeType=7      
and trb.IsDeleted=0    
and tpg.CreatedDate=(SELECT MAX(CreatedDate)  FROM TRIBUTEPACKAGE where UserTributeId=trb.TributeId and IsDeleted=0 )    
    
)    
Select     
Distinct(TributeId),    
 TributeName,
 TributeImage,
 Date1,
 Date2,
 MessageWithoutHtml,
ModifiedDate,
countryname,
 TypeDescription,    
  StartDate,    
  Renewaldate,    
  Enddate,    
 Visit,    
 EmailAlert,    
   TributeUrl,
	PackageId            
 from TempCommentTable    
  where RowNumber > @FirstRec and RowNumber < @LastRec    
order by StartDate desc    
END    
ELSE    
BEGIN    
With TempCommentTable1 AS    
(    
select    
    ROW_NUMBER() OVER (ORDER BY trb.CreatedDate desc) AS 'RowNumber',    
 trb.TributeId,    
 trb.TributeName,
 trb.TributeImage,
 trb.Date1,
 trb.Date2,
trb.MessageWithoutHtml ,
trb.ModifiedDate,
 ptc.TypeDescription,    
 trb.CreatedDate as StartDate,    
 tpg.StartDate as Renewaldate,    
 ISNULL(convert(varchar,tpg.EndDate,101),'Never') as Enddate,     
 (select count(*)  from dbo.WEBSTATISTICS wst where wst.SectionTypeId=trb.TributeId) as Visit,    
 trb.EmailAlert,    
trb.TributeUrl,
tpg.PackageId
from tributes trb,dbo.PARAMETERSTYPESCODES ptc,dbo.TRIBUTEPACKAGE tpg    
where trb.TributeID    
in (select UserTributeId from dbo.TRIBUTEADMINISTRATOR where UserId=@Userid 
--and isactive=1 
and isdeleted=0)    
and ptc.TypeCode=trb.TributeType    
and ptc.ParameterType='TRIBUTE_TYPE'    
and ptc.TypeCode=@TributeTypeId    
--and tpg.UserId=trb.UserTributeId    
and tpg.UserTributeId=trb.TributeId    
--and trb.IsActive=1 
and trb.TributeType=7 
and trb.IsDeleted=0    
and tpg.CreatedDate=(SELECT MAX(CreatedDate)  FROM TRIBUTEPACKAGE where UserTributeId=trb.TributeId and IsDeleted=0 )    
)    
Select   
Distinct(TributeId),    
 TributeName,
 TributeImage, 
 Date1,
 Date2,
MessageWithoutHtml,
ModifiedDate,
 TypeDescription,    
  StartDate,    
  Renewaldate,    
  Enddate,    
 Visit,    
 EmailAlert,    
  TributeUrl,
  PackageId            
 from TempCommentTable1    
 where RowNumber > @FirstRec and RowNumber < @LastRec    
 order by StartDate desc    
END    

END  


-------------------------------------------------------------
USE [TimelessTributePortal]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetTributesFeed]    Script Date: 04/27/2011 21:32:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

    
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    

Alter PROCEDURE [dbo].[usp_GetTributesFeed]    ---8962,1,1,100
 -- Add the parameters for the stored procedure here    
@Userid int,    
@TributeTypeId int,    
@CurrentPage As int,    
@PageSize As int    
    
AS    
    
-- Declare variables.    
Declare @FirstRec int    
Declare @LastRec int    
-- Initialize variables.    
Set @FirstRec = (@CurrentPage - 1) * @PageSize    
Set @LastRec = (@CurrentPage * @PageSize + 1);    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
if(@TributeTypeId=0 OR @TributeTypeId=1)        
BEGIN    
With TempCommentTable AS    
(    
select    
ROW_NUMBER() OVER (ORDER BY trb.CreatedDate desc) AS 'RowNumber',    
 trb.TributeId,    
 trb.TributeName, 
 trb.TributeImage,
 trb.Date1,
 trb.Date2,
 trb.MessageWithoutHtml,
trb.ModifiedDate,
 trb.countryname,
 ptc.TypeDescription,    
 trb.CreatedDate as StartDate,    
 tpg.StartDate as Renewaldate,    
    --(SELECT MAX(Enddate)  FROM TRIBUTEPACKAGE where UserTributeId=trb.TributeId and IsDeleted=0 )as Enddate,    
 ISNULL(convert(varchar,tpg.EndDate,101),'Never') as Enddate,    
 (select count(*)  from dbo.WEBSTATISTICS wst where wst.SectionTypeId=trb.TributeId) as Visit,    
 trb.EmailAlert,    
trb.TributeUrl,
tpg.PackageId        
from tributes trb,dbo.PARAMETERSTYPESCODES ptc,dbo.TRIBUTEPACKAGE tpg    
where trb.TributeID    
in (select UserTributeId from dbo.TRIBUTEADMINISTRATOR where UserId=@Userid 
--and isactive=1 
and isdeleted=0)    
and ptc.TypeCode=trb.TributeType    
and ptc.ParameterType='TRIBUTE_TYPE'    
--and tpg.UserId=trb.UserTributeId    
and tpg.UserTributeId=trb.TributeId    
--and trb.IsActive=1
and trb.TributeType=7      
and trb.IsDeleted=0    
and tpg.CreatedDate=(SELECT MAX(CreatedDate)  FROM TRIBUTEPACKAGE where UserTributeId=trb.TributeId and IsDeleted=0 )    
    
)    
Select     
Distinct(TributeId),    
 TributeName,
 TributeImage,
 Date1,
 Date2,
 MessageWithoutHtml,
ModifiedDate,
countryname,
 TypeDescription,    
  StartDate,    
  Renewaldate,    
  Enddate,    
 Visit,    
 EmailAlert,    
   TributeUrl,
	PackageId            
 from TempCommentTable    
  where RowNumber > @FirstRec and RowNumber < @LastRec    
order by StartDate desc    
END    
ELSE    
BEGIN    
With TempCommentTable1 AS    
(    
select    
    ROW_NUMBER() OVER (ORDER BY trb.CreatedDate desc) AS 'RowNumber',    
 trb.TributeId,    
 trb.TributeName,
 trb.TributeImage,
 trb.Date1,
 trb.Date2,
trb.MessageWithoutHtml ,
trb.ModifiedDate,
 ptc.TypeDescription,    
 trb.CreatedDate as StartDate,    
 tpg.StartDate as Renewaldate,    
 ISNULL(convert(varchar,tpg.EndDate,101),'Never') as Enddate,     
 (select count(*)  from dbo.WEBSTATISTICS wst where wst.SectionTypeId=trb.TributeId) as Visit,    
 trb.EmailAlert,    
trb.TributeUrl,
tpg.PackageId
from tributes trb,dbo.PARAMETERSTYPESCODES ptc,dbo.TRIBUTEPACKAGE tpg    
where trb.TributeID    
in (select UserTributeId from dbo.TRIBUTEADMINISTRATOR where UserId=@Userid 
--and isactive=1 
and isdeleted=0)    
and ptc.TypeCode=trb.TributeType    
and ptc.ParameterType='TRIBUTE_TYPE'    
and ptc.TypeCode=@TributeTypeId    
--and tpg.UserId=trb.UserTributeId    
and tpg.UserTributeId=trb.TributeId    
--and trb.IsActive=1 
and trb.TributeType=7 
and trb.IsDeleted=0    
and tpg.CreatedDate=(SELECT MAX(CreatedDate)  FROM TRIBUTEPACKAGE where UserTributeId=trb.TributeId and IsDeleted=0 )    
)    
Select   
Distinct(TributeId),    
 TributeName,
 TributeImage, 
 Date1,
 Date2,
MessageWithoutHtml,
ModifiedDate,
 TypeDescription,    
  StartDate,    
  Renewaldate,    
  Enddate,    
 Visit,    
 EmailAlert,    
  TributeUrl,
  PackageId            
 from TempCommentTable1    
 where RowNumber > @FirstRec and RowNumber < @LastRec    
 order by StartDate desc    
END    

END  


------------------------------------------------------
USE [TimelessTributePortal]
GO
/****** Object:  StoredProcedure [dbo].[usp_AtomEnableOnUserIdOrName]    Script Date: 04/27/2011 21:33:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter procedure [dbo].[usp_AtomEnableOnUserIdOrName]
(
@UserId int,
@UserName varchar(255),
@AtomEnabled bit
)
as
Begin
SET NOCOUNT ON;
Declare @cnt int
if (@UserId > 0)
	Begin
		Select @cnt= 	Count(UserId) 
				from	dbo.USERS 
		where	UserId = @UserId and UserType = 2 and IsDeleted = 0
	If @cnt > 0
		Begin
			Update USERS
			Set AtomEnabled = @AtomEnabled
			where UserId=@UserId
			Select 1 as UpdateOutput, @UserId as UserId 
		End
else
Select 0 as UpdateOutput, 0 as UserId
	End	
Else
	Begin
		Select @cnt= Count(UserId) 
				from	dbo.USERS 
		where	UserName = @UserName and UserType = 2 and IsDeleted = 0
	If @cnt > 0
		Begin
			Update USERS
			Set AtomEnabled = @AtomEnabled
			where UserName = @UserName
			Select 1 as UpdateOutput, UserId from USERS where UserName = @UserName
		End
else
Select 0 as UpdateOutput, 0 as UserId
	End				

END
---------------------------------------------------------
USE [TimelessTributePortal]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserDetailsOnUserId]    Script Date: 04/27/2011 21:34:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter Procedure [dbo].[usp_GetUserDetailsOnUserId]
@UserId int
AS
BEGIN
	Select 
	UserName,
	UserType,
	AtomEnabled
	from USERS
		Where @UserId=UserId and UserType = 2 and IsDeleted = 0
END