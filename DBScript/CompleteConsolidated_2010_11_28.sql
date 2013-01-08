
Alter table tributes add IsOrderDVDChecked bit ,IsMemTributeBoxChecked bit
Alter table tributes add LinkMemTributeId int

alter table dbo.USERBUSINESS
add ObituaryLinkPage varchar(100)







-------------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[usp_GetTributeOnTributeId]
@TributeId int
--SP to get the tribute details based on the TributeUrl and TributeType
--exec usp_GetTributeDetails '22', 'Graduation'
AS
BEGIN
SELECT
	TributeImage,
	TributeName,
    TributeURL,
	TributeType,
	TRIBUTES.UserTributeId,
    Date1,
	Date2,
	dbo.TRIBUTES.City,
	dbo.ufn_GetCountryStateName(dbo.TRIBUTES.State) AS State,
	dbo.ufn_GetCountryStateName(dbo.TRIBUTES.Country) AS Country,
    IsOrderDVDChecked,
    IsMemTributeBoxChecked,
    LinkMemTributeId,
	USERS.Email
	

FROM   dbo.TRIBUTES ,dbo.PARAMETERSTYPESCODES, dbo.TRIBUTEPACKAGE  ,dbo.USERS
			WHERE  dbo.TRIBUTES.TributeType=dbo.PARAMETERSTYPESCODES.TypeCode
			and dbo.TRIBUTEPACKAGE.UserTributeId = dbo.TRIBUTES.TributeId
			and TRIBUTES.UserTributeId = dbo.USERS.UserId
			and  dbo.TRIBUTES.TributeId =@TributeId and dbo.PARAMETERSTYPESCODES.ParameterType = 'TRIBUTE_TYPE'
END
------------------------------------------------------

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

ALTER Procedure [dbo].[usp_GetVideoDetailsOnUserTributeId]
@tributeId int
as
	Begin
			select VideoId ,UserId, TributeVideoId, UserTributeId
			from  dbo.VIDEOS where UserTributeId=@tributeId			
	End

------------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

ALTER Procedure [dbo].[usp_GetUserDetails]    
@UserId int    
AS    
BEGIN    
  SET NOCOUNT ON      
  BEGIN    
   TRY    
   if((Select COUNT(*) from USERBUSINESS where [UserId]=@UserId)>0)    
     BEGIN   
     SELECT users.[UserId]    
     ,users.[UserName]    
     ,users.[Password]                                            
    -- ,users.[FirstName]  
  ,usrBus.[CompanyName] as FirstName  
     ,users.[LastName]    
     ,users.[Email]    
     ,users.[VerificationCode]    
     ,users.[UserType]    
     ,users.[UserImage]    
     ,users.[IsUsernameVisiable]    
     ,users.[AllowIncomingMsg]    
     ,users.[IsLocationHide]    
     ,users.[Status]    
     ,users.[City]    
     ,users.[State]    
     ,users.[Country]     
     ,usrBus.[Website]    
     ,usrBus.[CompanyName]  
     ,usrBus.[BusinessType]    
     ,usrBus.[BusinessAddress]    
     ,usrBus.[ZipCode]         
     ,usrBus.[Phone]    
     ,usrBus.[CompanyLogo]
	 ,usrBus.[HeaderBGColor]
	,usrBus.[HeaderLogo]
	,usrBus.[IsAddressOn]
	,usrBus.[IsWebAddressOn]
	,usrBus.[IsObUrlLinkOn]
	,usrBus.[IsPhoneNoOn]
,usrBus.[DisplayCustomHeader]
     FROM [dbo].[USERS] users INNER JOIN [USERBUSINESS]AS usrBus    
     ON  users.[UserId]=usrBus.[UserId]    
     where     
     users.[IsActive]=1    
     and    
     users.[IsDeleted]=0    
     and    
     usrBus.[IsActive]=1    
     and     
     users.[UserId]=@UserId    
     END    
                ELSE    
                    BEGIN    
      SELECT users.[UserId]    
      ,users.[UserName]    
      ,users.[Password]    
      ,users.[FirstName]    
      ,users.[LastName]    
      ,users.[Email]    
      ,users.[VerificationCode]    
      ,users.[UserType]    
      ,users.[UserImage]    
      ,users.[IsUsernameVisiable]    
      ,users.[AllowIncomingMsg]    
      ,users.[IsLocationHide]    
      ,users.[Status]    
      ,users.[City]    
      ,users.[State]    
      ,users.[Country]          
      FROM [dbo].[USERS] users    
      where                
      users.[IsActive]=1    
      and    
      users.[IsDeleted]=0    
      and          
      users.[UserId]=@UserId     
                    END     
   END TRY    
      BEGIN CATCH    
    EXEC RethrowError;    
   END CATCH    
    
        --END    
  SET NOCOUNT OFF    
        
END 
-------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[usp_UpdateBusinessDetails]  
@UserId int,  
@Website varchar(100),  
@CompanyName varchar(255),  
@BusinessType int,  
@BusinessAddress varchar(100),  
@Phone varchar(20),  
@ZipCode varchar(20),
@HeaderBGColor varchar(10),
@HeaderLogo varchar(255),
@ObituaryLinkPage varchar(100),
@IsAddressOn bit,
@IsWebAddressOn bit,
@IsObUrlLinkOn bit,
@IsPhoneNoOn bit,
@DisplayCustomHeader bit
        
AS  
BEGIN  
  SET NOCOUNT ON    
  BEGIN  
     
    UPDATE [dbo].[USERBUSINESS]  
    SET      [Website] =@Website,  
       [CompanyName] = @CompanyName,  
       [BusinessType] = @BusinessType,  
       [BusinessAddress] = @BusinessAddress,  
       [Phone] = @Phone,  
       [ZipCode] = @ZipCode,
	   [HeaderBGColor] = @HeaderBGColor ,
	   [HeaderLogo] = @HeaderLogo,
	   [ObituaryLinkPage] = @ObituaryLinkPage,
	   [IsAddressOn]=@IsAddressOn ,
	   [IsWebAddressOn]=@IsWebAddressOn ,
   	   [IsObUrlLinkOn]=@IsObUrlLinkOn ,
	   [IsPhoneNoOn]=@IsPhoneNoOn,
		[DisplayCustomHeader]=@DisplayCustomHeader
     where        
     [IsActive]=1 and [UserId]=@UserId  
  
        END  
  SET NOCOUNT OFF  
      
END  

------------------------------

Alter table [dbo].[USERBUSINESS]
Add HeaderBGColor Varchar(10)

Alter table [dbo].[USERBUSINESS]
Add IsAddressOn bit

Alter table [dbo].[USERBUSINESS]
Add IsWebAddressOn bit

Alter table [dbo].[USERBUSINESS]
Add IsObUrlLinkOn bit

Alter table [dbo].[USERBUSINESS]
Add IsPhoneNoOn bit

Alter table [dbo].[USERBUSINESS]
Add DisplayCustomHeader bit

Alter table [dbo].[USERBUSINESS]
Add HeaderLogo varchar(255)


--only to be rum for the first time


Update [dbo].[USERBUSINESS]
Set IsAddressOn = 'false'

Update [dbo].[USERBUSINESS]
Set IsWebAddressOn = 'false'

Update [dbo].[USERBUSINESS]
Set IsObUrlLinkOn = 'false'

Update [dbo].[USERBUSINESS]
Set IsPhoneNoOn = 'false'

Update [dbo].[USERBUSINESS]
Set DisplayCustomHeader = 'false'



