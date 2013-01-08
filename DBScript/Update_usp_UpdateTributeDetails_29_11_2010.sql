

CREATE PROCEDURE [dbo].[usp_UpdateTributeDetail]  
 @TributeId  int,  
 @TributeName varchar(250),  
 @TributeImage varchar(250),  
 @City   varchar(50),  
 @State   int,  
 @Country  int,  
 @Date1   datetime = NULL,  
 @Date2   datetime = NULL,  
 @ModifiedBy  int,  
 @ModifiedDate datetime   
  
AS  
BEGIN  
 UPDATE TRIBUTES   
  
 SET  TributeName = @TributeName,  
   TributeImage = @TributeImage,  
   City = @City,  
   State = @State,  
   Country = @Country,  
   Date1 = @Date1,  
   Date2 = @Date2,  
   ModifiedBy = @ModifiedBy,   
   ModifiedDate = @ModifiedDate  
  
 WHERE TributeId = @TributeId   
END  