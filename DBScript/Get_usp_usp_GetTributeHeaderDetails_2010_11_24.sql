
CREATE Procedure [dbo].[usp_GetTributeHeaderDetails]          
@UserId int          
AS          
    
       
select BusinessAddress,Phone,HeaderBGColor,WebSite,HeaderLogo,IsAddressOn,IsPhoneNoOn,IsWebAddressOn from userbusiness where userId=@UserId and DisplayCustomHeader=1   
          
          
  