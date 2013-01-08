
CREATE Procedure [dbo].[usp_GetEditTributeFieldDetails]    
    
 -- Input Parameters for the stored procedure     
 @TributeId int  

AS    
    
BEGIN    
    
 SET NOCOUNT ON;    
    
  -- Find that user is admin or not for this tribute    
 -- SELECT count(*) AS 'IsAdmin'    
  --FROM TRIBUTEADMINISTRATOR     
 -- WHERE UserId = @UserId and UserTributeId = @TributeId and IsDeleted = 0    
    
  -- Get Tribute Detail to display in Personal Detail Section    
  SELECT  TributeId, UserTributeId, TributeName, TributeImage, TributeUrl, Date1, Date2, City,     
     CreatedDate, State, Country,    
   ( SELECT TypeDescription     
     FROM PARAMETERSTYPESCODES     
     WHERE (ParameterType = 'TRIBUTE_TYPE') and (TypeCode = TributeType)    
   )AS 'TributeType',    
   ( SELECT LocationName    
     FROM Locations     
     WHERE LocationId = State and LocationParentId = Country    
   )AS 'StateName',    
   ( SELECT LocationName     
     FROM Locations     
     WHERE LocationId = Country    
   )AS 'CountryName'     
    
  FROM TRIBUTES    
  WHERE  TributeId = @TributeId    
    
      
END 