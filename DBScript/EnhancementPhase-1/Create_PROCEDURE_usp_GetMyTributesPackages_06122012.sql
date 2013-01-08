USE [TributePortal_New_P1]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetMyTributesPackages]    Script Date: 12/06/2012 11:54:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,LHK>    
-- Create date: <Create Date,06122012,>    
-- Description: <Description,to fetch packageId's,>    
-- =============================================    

Create PROCEDURE [dbo].[usp_GetMyTributesPackages]    ---8962
 -- Add the parameters for the stored procedure here    
@Userid int
    
AS    
   
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
         
BEGIN    
	With TempCommentTable AS    
		(    
			select    
				ROW_NUMBER() OVER (ORDER BY trb.CreatedDate desc) AS 'RowNumber',    
				 trb.TributeId,  
				 ptc.TypeDescription,
				tpg.PackageId        
			from tributes trb,dbo.PARAMETERSTYPESCODES ptc,dbo.TRIBUTEPACKAGE tpg    
			where trb.TributeID    
			in (select UserTributeId from dbo.TRIBUTEADMINISTRATOR where UserId=@Userid 
			and isdeleted=0)    
			and ptc.TypeCode=trb.TributeType    
			and ptc.ParameterType='TRIBUTE_TYPE'      
			and tpg.UserTributeId=trb.TributeId      
			and trb.IsDeleted=0    
			and tpg.CreatedDate=(SELECT MAX(CreatedDate)  FROM TRIBUTEPACKAGE where UserTributeId=trb.TributeId and IsDeleted=0 )    
		    
		)    
	Select          
		 --TypeDescription,    
		 PackageId            
	 from TempCommentTable    
		group by 
		--TypeDescription,
		PackageId  
		order by 
		--TypeDescription,
		PackageId 
	END 
End
