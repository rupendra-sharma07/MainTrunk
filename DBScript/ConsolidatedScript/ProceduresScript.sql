
GO
/***************************** 
usp_InsertCreditPointTransDetails   
******************************/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_InsertCreditPointTransDetails]
(
@UserId int,
@NetCreditPoints numeric,
@PurchasedDate datetime,
@CreditPackageId int,
@AmountPaid int ,
@CreatedDate Datetime,
@ModifiedDate datetime,
@CreditCardId int,
@IsDeleted bit,
@CouponId int,
@ConfirmationNo int

)
as
INSERT INTO [dbo].[CreditPointTransaction]
           (
UserId,
NetCreditPoints,
PurchasedDate,
CreditPackageId,
AmountPaid,
CreatedDate,
ModifiedDate,
CreditCardId,
IsDeleted,
CouponId,
ConfirmationNo
           )
     VALUES
(
@UserId,
@NetCreditPoints,
@PurchasedDate,
@CreditPackageId,
@AmountPaid,
@CreatedDate,
@ModifiedDate,
@CreditCardId,
@IsDeleted,
@CouponId,
@ConfirmationNo

)
GO
/******************************
usp_GetCreditCount
*******************************/
Create PROCEDURE [dbo].[usp_GetCreditCount]  
  
 -- Input Parameters for the stored procedure   
 @userId int
AS
Begin
if((Select COUNT(*) from CreditPointTransaction   where UserId=@UserId)>0)    
     BEGIN   
     SELECT top 1 * from CreditPointTransaction   where UserId= @UserId order by TransactionId desc 
End
Else 
begin
 Select * from CreditPointTransaction   where UserId=@UserId
End
End

GO
/******************************
usp_GetCreditCostMappingDetails
*******************************/
     
Create PROCEDURE [dbo].[usp_GetCreditCostMappingDetails]  
  AS
Begin
Select CreditPoints,
	   Convert(decimal(10,2),CostPerCredit) as CostPerCredit,
       TributeType 
from CreditPackageCostMaster

End
GO

/******************************
usp_InsertCurrentCreditPoint
*******************************/
     
Create PROCEDURE [dbo].[usp_InsertCurrentCreditPoints]        
 @UserId  int,        
 @NetCreditPoints int,        
 @AmountPaid int,        
 @CreditPackageId int,        
 @PurchasedDate DateTime,        
 @IsDeleted bit,        
 @ModifiedDate DateTime,        
 @CouponId int,        
 @CreditCardId int,        
 @CreatedDate DateTime,        
 @ConfirmationNo varchar(20)        
AS        
BEGIN        
SET NOCOUNT ON; 
Declare @TransactionId int      
        
INSERT INTO [dbo].[CreditPointTransaction]        
(        
  UserId,        
  NetCreditPoints,         
  AmountPaid,         
  CreditPackageId,         
  PurchasedDate,         
  IsDeleted,        
  ModifiedDate,        
  CouponId,         
  CreditCardId,        
  CreatedDate,        
  ConfirmationNo   
        
)        
       VALUES        
(        
  @UserId,        
  @NetCreditPoints,         
  @AmountPaid,         
  @CreditPackageId,         
  @PurchasedDate,         
  @IsDeleted,        
  @ModifiedDate,        
  @CouponId,         
  @CreditCardId,        
  @CreatedDate,        
  @ConfirmationNo
      
)     
      
Select @TransactionId = @@IDENTITY       
Select @TransactionId  
End

GO
/******************************
usp_GetVideoTributeTransactionDetail
*******************************/

GO
Create Procedure [dbo].[usp_GetVideoTributeTransactionDetails]  
@TributePackageId int  
AS  
BEGIN  
 select top 1 dbo.ISNULLorEmpty(cpt.Transactionid)AS 'TransactionId',tp.TributePackageId,tp.IsAutomaticRenew, tb.TributeId, tb.Tributename,ptc.TypeDescription,pk.Packagename,tp.Enddate,ucd.CardholdersName,ucd.Address,  
  ucd.City,lc.LocationName as State,(select LocationName from LOCATIONS lts where lts.Locationid=ucd.Country)as Country ,  
  ucd.Zip,ucd.Telephone,tp.StartDate,ucd.CreditCardType,ucd.CreditCardNo,tp.AmountPaid, tp.CreditCardId, Tb.TributeId,  
  ucd.SponsorEmailAddress  
 from TributePackage tp  
  Left Outer Join Tributes tb ON tb.TributeId = tp.UserTributeId  
  Left Outer Join Package pk ON pk.PackageId = tp.PackageId  
 Left Outer Join CreditPointTransaction cpt ON cpt.UserId = tp.UserId 
  Left Outer Join USERCREDITCARDDETAILS ucd ON ucd.UserId = tp.UserId  
  Left Outer Join Locations lc ON lc.LocationId = ucd.State  
  Left Outer Join PARAMETERSTYPESCODES ptc ON ptc.ParameterType='TRIBUTE_TYPE' and ptc.TypeCode=tb.TributeType  
 Where tp.TributePackageId = @TributePackageId order by ucd.createddate desc
END

/******************************
usp_CreateTribute
*******************************/

GO
ALTER procedure [dbo].[usp_CreateTribute]
@UserTributeId as int,
@tributeName as varchar(250),
@tributeType as int,
@welcomeMessage as varchar(1000),
@tributeImage as varchar(250)=null,
@tributeUrl as varchar(250),
@themeId as int,
@city as varchar(50),
@state as int,
@country as int,
@IsPrivate as bit,
@IsOrderDVDChecked as bit,
@IsMemTributeBoxChecked  as bit,
@Date1 as datetime,
@Date2 as datetime
as
Begin
Declare @_Email varchar(250)
Set NoCount On

Begin Transaction
Insert into Tributes (UserTributeId, TributeName,
  						TributeType, WelcomeMessage,
                        TributeImage,TributeUrl, 
                        ThemeId, City, [State],	
                       	Country, IsPrivate,IsOrderDVDChecked,IsMemTributeBoxChecked,
                        Date1,Date2,
                        CreatedBy, CreatedDate)
				values( @UserTributeId, @tributeName,
                        @tributeType, @welcomeMessage,
                        @tributeImage, @tributeUrl, @themeId, 
                        @city,  @state, @country, @IsPrivate,@IsOrderDVDChecked,@IsMemTributeBoxChecked,
                        @Date1,@Date2,
                        @UserTributeId, getdate())
Select @@IDENTITY

Select @_Email=Email from dbo.USERS where UserID=@UserTributeId;

EXEC usp_SaveAdminConfirm @@IDENTITY,@UserTributeId,@_Email,1
---- @UserTributeId  int,
----      @UserId int,
----      @Email varchar(250),
----      @IsOwner bit  
If @@Error<>0
Begin
Rollback transaction
Return -1
End
Commit Transaction
		
Set NoCount Off
End
Go
GO
/*****************************
usp_GetVideoDetailsOnUserTributeId
******************************/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[usp_GetVideoDetailsOnUserTributeId]
@tributeId int
AS
	BEGIN
			SELECT VideoId ,UserId, TributeVideoId, UserTributeId
			FROM  dbo.VIDEOS 
			WHERE UserTributeId=@tributeId			
	END

GO
/*****************************
usp_LinkVideoTribute
******************************/
CREATE PROCEDURE [dbo].[usp_LinkVideoTribute]
	-- Input Parameters for the stored procedure 
	@videoTributeId		int,
	@tributeId			int

AS
BEGIN
	DECLARE @TributeType  int

	SELECT @TributeType = TributeType
	FROM Tributes where TributeId = @tributeId;

		IF (@TributeType = 7)
		BEGIN
			UPDATE Tributes SET LinkMemTributeId = @tributeId WHERE TributeId = @videoTributeId;
		END
END

GO
/*****************************
usp_GetEditTributeFieldDetails
******************************/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[usp_GetEditTributeFieldDetails] 
 -- Input Parameters for the stored procedure     
 @TributeId int
AS  
BEGIN
 SET NOCOUNT ON;  
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

/********************
usp_UpdateTributeDetail
********************/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[usp_UpdateTributeDetail]  
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
GO
/********************
usp_GetUserDetails
********************/
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
	,usrBus.[ObituaryLinkPage]
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

GO
/********************************
usp_GetTributeDetailFromTributeId
*********************************/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetTributeDetailFromTributeId] 
(
@TributeID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT	dbo.TRIBUTES.TributeId,			
		dbo.TRIBUTES.TributeName, 		
		dbo.TRIBUTES.TributeUrl,	
		dbo.TRIBUTES.TributeImage,		
        dbo.PARAMETERSTYPESCODES.TypeDescription,
		dbo.TRIBUTES.ThemeId,
		dbo.TRIBUTES.CreatedDate,
		dbo.TRIBUTES.Date2,
		dbo.TRIBUTES.Date1,
		dbo.TRIBUTES.GoogleAdSense,
		dbo.TRIBUTES.IsActive,
		dbo.TRIBUTES.IsOrderDVDChecked,
		dbo.TRIBUTES.IsMemTributeBoxChecked,
		dbo.TRIBUTES.City, 	
		dbo.TRIBUTES.State, 
		dbo.TRIBUTES.Country,
		dbo.TRIBUTES.State AS StateName,
		dbo.TRIBUTES.Country AS CountryName
		--dbo.ufn_GetCountryStateName(dbo.TRIBUTES.State) AS StateName,
		--dbo.ufn_GetCountryStateName(dbo.TRIBUTES.Country) AS CountryName	
FROM    dbo.TRIBUTES ,dbo.PARAMETERSTYPESCODES			
WHERE  dbo.TRIBUTES.TributeType=dbo.PARAMETERSTYPESCODES.TypeCode
and  dbo.TRIBUTES.TributeId = @TributeID and dbo.PARAMETERSTYPESCODES.ParameterType = 'TRIBUTE_TYPE'
END

GO
/*****************************
usp_UpdateBusinessDetails
*****************************/

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

GO
/********************************
usp_GetTributeHeaderDetail
*********************************/
CREATE Procedure [dbo].[usp_GetTributeHeaderDetails]            
@UserId int            
AS         
BEGIN    
SELECT 
BusinessAddress,
dbo.USERS.City,
dbo.ufn_GetCountryStateName(dbo.USERS.State) AS State,
Phone,
HeaderBGColor,
WebSite,
HeaderLogo,
IsAddressOn,
IsPhoneNoOn,
IsWebAddressOn,
DisplayCustomHeader, 
IsObUrlLinkOn,
ObituaryLinkPage
FROM dbo.userbusiness,dbo.USERS
WHERE dbo.userbusiness.userid = dbo.USERS.UserId
and dbo.USERS.IsActive=1
and dbo.USERS.userid=@UserId
END

GO
/************************************
usp_GetTributeOnTributeId
*************************************/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[usp_GetTributeOnTributeId]
@TributeId int
--SP to get the tribute details based on the TributeUrl and TributeType
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
	USERS.Email,
    IsPrivate
	

FROM   dbo.TRIBUTES ,dbo.PARAMETERSTYPESCODES, dbo.TRIBUTEPACKAGE  ,dbo.USERS
			WHERE  dbo.TRIBUTES.TributeType=dbo.PARAMETERSTYPESCODES.TypeCode
			and dbo.TRIBUTEPACKAGE.UserTributeId = dbo.TRIBUTES.TributeId
			and TRIBUTES.UserTributeId = dbo.USERS.UserId
			and  dbo.TRIBUTES.TributeId =@TributeId and dbo.PARAMETERSTYPESCODES.ParameterType = 'TRIBUTE_TYPE'
			and dbo.TRIBUTES.IsDeleted=0
END


GO
/********************************
usp_UpdateVideoTributeOwnerCredit
*********************************/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UpdateVideoTributeOwnerCredit]
	-- Input Parameters for the stored procedure 
	@videoTributeUserId		int	

AS
BEGIN
Update dbo.CreditPointTransaction Set NetCreditPoints= NetCreditPoints +1,ModifiedDate= getdate() where transactionid in (select top 1 transactionid from dbo.CreditPointTransaction where UserId = @videoTributeUserId order by CreatedDate desc)

	
END

GO
/********************************
usp_GetLinkedVideoTributeId
*********************************/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[usp_GetLinkedVideoTributeId]  
  
 -- Input Parameters for the stored procedure   
 @tributeId int,
 @userId int
AS
Begin
if((Select COUNT(*) from LinkVideoMemorialTribute   where MemTributeId =@tributeId and UserId =@userId ) >0)
select videotributeid from LinkVideoMemorialTribute where  MemTributeId =  @tributeId and UserId =@userId
else
Select COUNT(*) videotributeid from LinkVideoMemorialTribute   where MemTributeId =@tributeId and UserId =@userId
    
End
GO
/********************************
usp_InsertLinkVideoMemTribute
*********************************/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_InsertLinkVideoMemTribute]
(
@userId int,
@videoTributeId int,
@memTributeId int
)
as
BEGIN
	DECLARE @TributeType  int

	SELECT @TributeType = TributeType
	FROM Tributes where TributeId = @memTributeId;

		IF (@TributeType = 7)
		BEGIN
			INSERT INTO [dbo].[LinkVideoMemorialTribute]
           (
			[UserId]
           ,[VideoTributeId]
           ,[MemTributeId]
           )
     VALUES
		(
           @userId ,
           @videoTributeId ,
           @memTributeId 

           );
		END
END

GO

/********************************
usp_UpdateVideoTributeImage
*********************************/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[usp_UpdateVideoTributeImage]
	@TributeId			int,
	@TributeImage	varchar(255)

AS
BEGIN
		UPDATE	TRIBUTES 
		SET		TributeImage = @TributeImage
		WHERE	TributeId = @TributeId 

END
GO
/********************************
usp_UpdateVideoTributeDetail
*********************************/
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
GO

/********************************
usp_CreateThemesOnCategory
*********************************/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_CreateThemesOnCategory]
(
 @ThemeName   varchar(100),  
 @Tributetype varchar(100),  
 @ThemeValue varchar(200), 
 @IsActive bit, 
 @SubCategory  varchar(250),  
 @FolderName  varchar(250)
 
)
AS   
BEGIN  
INSERT INTO [dbo].[THEMES]
           ([ThemeName]           
           ,[ThemePath]
		   ,[Tributetype]             
           ,[ThemeValue]
		   ,[IsActive]           
           ,[SubCategory]
           ,[FolderName])
     VALUES
           (@ThemeName 
			,'http://172.26.176.214/DevelopmentWebsite/assets/images/Default Anniversary.JPG'          
           ,@Tributetype
		   ,@ThemeValue
           ,@IsActive
           ,@SubCategory
           ,@FolderName)

SELECT @@Identity  
 END 

GO

/********************************
dbo.usp_GetThemesSubCategory
*********************************/
GO
create procedure [dbo].[usp_GetThemesSubCategory]      
      
 (  
-- Input Parameters for the stored procedure       
 @CategoryName  varchar(150)    
)  
      
AS      
      
BEGIN     
      
select  distinct SubCategory as SubCategory from Themes where TributeType=@CategoryName  
       
END 
GO

/********************************
dbo.usp_GetThemesCategory
*********************************/
GO
CREATE procedure [dbo].[usp_GetThemesCategory]        
        
   -- Input Parameters for the stored procedure       
 @IsActive  bit    
        
AS        
        
BEGIN       
        
select distinct TributeType as TributeType from Themes  where IsActive =@IsActive  
         
END

GO

/********************************
dbo.usp_GetThemesForSubCategory
*********************************/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetThemesForSubCategory] 
@ThemeName as varchar(250),
@SubCategory as varchar(250)
AS 
BEGIN
	if @SubCategory='All'
		SELECT * FROM THEMES where Tributetype=@ThemeName
	else
		SELECT * FROM THEMES where Tributetype=@ThemeName and Subcategory=@SubCategory
END

GO

/********************************
dbo.usp_GetTributeThemeFoldername
*********************************/
GO
CREATE Procedure [dbo].[usp_GetTributeThemeFoldername]
@TributeId int
AS
BEGIN
	--Stored Procedure to get the existing theme id for the selected tribute
	Select Tributes.ThemeId, ThemeName, ThemeValue,FolderName from Tributes 
			Inner Join Themes ON Themes.ThemeId = Tributes.ThemeId
			where Tributes.TributeId = @TributeId and Tributes.IsDeleted = 0
END


GO

/************************************
dbo.usp_GetSubCategoryForTributeType
*************************************/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[usp_GetSubCategoryForTributeType]
(
@TributeType varchar(50)
)
AS

BEGIN

	SET NOCOUNT ON;		
		SELECT TributeType,
				SubCategory 
		FROM themes
		WHERE TributeType=@TributeType
		GROUP BY TributeType,SubCategory
		ORDER BY TributeType,SubCategory
END

GO
/************************************
dbo.usp_GetLinkVideoMemorialTribute
*************************************/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

create Procedure [dbo].[usp_GetLinkVideoMemorialTribute]
@UserId int,
@VideoTributeId int

as
	Begin
			select MemTributeId
			from  dbo.LinkVideoMemorialTribute
 where 
VideoTributeId = @VideoTributeId
and UserId = @UserId
End

Go
/************************************
usp_GetCreditPtPurchaseDetails
*************************************/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[usp_GetCreditPtPurchaseDetails]  
@TransactionId int  
AS  
BEGIN  
 select dbo.ISNULLorEmpty(cpt.CreditCardId)AS 'TransactionId',ucd.CardholdersName,ucd.Address,  
  ucd.City,lc.LocationName as State,(select LocationName from LOCATIONS lts where lts.Locationid=ucd.Country)as Country ,  
  ucd.Zip,ucd.Telephone,ucd.CreditCardType,ucd.CreditCardNo,cpt.CreatedDate,cpt.AmountPaid,null as Endate,
  ucd.SponsorEmailAddress  
 from CreditPointTransaction cpt 
  Left Outer Join USERCREDITCARDDETAILS ucd ON ucd.CreditCardId = cpt.CreditCardId  
  Left Outer Join Locations lc ON lc.LocationId = ucd.State  
  
 Where cpt.TransactionId = @TransactionId  
END 