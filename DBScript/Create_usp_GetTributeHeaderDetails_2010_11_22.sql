



CREATE Procedure [dbo].[usp_GetTributeHeaderDetails]        
@UserId int        
AS        
BEGIN  
     
SELECT BusinessAddress,Phone,HeaderBGColor,WebSite,HeaderLogo 
FROM userbusiness WHERE userId=@UserId  
        
 END      
    