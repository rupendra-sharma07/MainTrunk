GO
/****** Object:  StoredProcedure [dbo].[usp_GetYourTributesFeed]    Script Date: 01/04/2012 10:38:36 ******/
--created by- Rajat agarwal 28/12/2011
--Purpose - Get XML feed 
--exec dbo.usp_GetYourTributesFeed @BusinessUserId=26881,@CurrentPage=1,@PageSize=10

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_GetYourTributesFeed]    ---8962,1,1,100
  -- Add the parameters for the stored procedure here    
@BusinessUserId int,
--@TributeTypeId int,    
@CurrentPage As int,    
@PageSize As int    
    
AS    
    
-- Declare variables.    
Declare @FirstRec int    
Declare @LastRec int    
-- Initialize variables.    
Set @FirstRec = @CurrentPage; 
Set @LastRec = (@CurrentPage + @PageSize);       
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    

BEGIN    
With TempCommentTable1 AS    
(    
select    
    ROW_NUMBER() OVER (ORDER BY trb.CreatedDate desc) AS 'RowNumber',    
 trb.TributeId,
 trb.UserTributeId,   
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
  trb.TributeUrl 
  from tributes trb,dbo.PARAMETERSTYPESCODES ptc,dbo.TRIBUTEPACKAGE tpg, dbo.USERS usr    
where
 --trb.TributeId=@CurrentPage   
--in (select UserTributeId from dbo.TRIBUTEADMINISTRATOR where isdeleted=0)    
 ptc.TypeCode=trb.TributeType    
and ptc.ParameterType='TRIBUTE_TYPE'   
and ptc.TypeCode=7   
and usr.UserType=2
and usr.UserId=@BusinessUserId
and tpg.UserId=@BusinessUserId
and trb.UserTributeId=@BusinessUserId   
and tpg.UserTributeId=trb.TributeId    
and trb.IsActive=1 and 
trb.TributeType=7 
and trb.IsDeleted=0    
)    
Select   
Distinct(TributeId),
 UserTributeId,     
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
 --EmailAlert,    
  TributeUrl
  --PackageId            
 from TempCommentTable1    
 where RowNumber >= @FirstRec and RowNumber < @LastRec 
 order by StartDate desc    
END    

END  

