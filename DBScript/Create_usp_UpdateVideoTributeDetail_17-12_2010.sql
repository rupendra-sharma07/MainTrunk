set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[usp_UpdateVideoTributeDetail]  
 @TributeId  int,  
 @TributeName varchar(250),   
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
   City = @City,  
   State = @State,  
   Country = @Country,  
   Date1 = @Date1,  
   Date2 = @Date2,  
   ModifiedBy = @ModifiedBy,   
   ModifiedDate = @ModifiedDate  
  
 WHERE TributeId = @TributeId   
END  