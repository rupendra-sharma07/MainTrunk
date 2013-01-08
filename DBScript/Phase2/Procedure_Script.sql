set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[usp_GetTributeThemeFolder]
@TributeId int
AS
BEGIN
	--Stored Procedure to get the existing theme id for the selected tribute
	Select Tributes.ThemeId, ThemeName, ThemeValue, FolderName from Tributes 
			Inner Join Themes ON Themes.ThemeId = Tributes.ThemeId
			where Tributes.TributeId = @TributeId and Tributes.IsDeleted = 0
END

GO
-------------------------------------------------------
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetThemesTemplates] 
@ThemeName as varchar(250)
AS 
if @ThemeName='All'
SELECT * FROM THEMES 
else
SELECT * FROM THEMES where Tributetype=@ThemeName

GO
-------------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetCustomHeaderDetailOnTributeId] 
(
@TributeID int
)
AS
BEGIN
SET NOCOUNT ON;
	DECLARE @UserType int

	SELECT @UserType = dbo.USERS.usertype from dbo.TRIBUTES,dbo.USERS
	WHERE dbo.TRIBUTES.usertributeid = dbo.USERS.userid And dbo.TRIBUTES.tributeid=@TributeID	

	IF (@UserType = 1)  
		BEGIN
			SELECT	'FALSE' as DisplayCustomHeader
		END
	ELSE
		BEGIN
			SELECT	case isnull(dbo.Userbusiness.DisplayCustomHeader,0) when 0 then 'FALSE' else 'TRUE' end as DisplayCustomHeader
			FROM    dbo.Userbusiness,dbo.TRIBUTES ,dbo.PARAMETERSTYPESCODES, dbo.TRIBUTEPACKAGE   
			WHERE  dbo.TRIBUTES.TributeType=dbo.PARAMETERSTYPESCODES.TypeCode
			and dbo.TRIBUTEPACKAGE.UserTributeId = dbo.TRIBUTES.TributeId
			and  dbo.TRIBUTES.UserTributeId = dbo.Userbusiness.UserId 
			and  dbo.TRIBUTES.TributeId = @TributeID and dbo.PARAMETERSTYPESCODES.ParameterType = 'TRIBUTE_TYPE'			
		END
	
	SET NOCOUNT OFF;
END

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[usp_GetTributeIdOnTributeUrl]
	@TributeUrl varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select statements for procedure here
	SELECT TributeId from Tributes where TributeUrl=@TributeUrl;
END

-------------------
GO

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[usp_GetTributeHeaderDetails]            
@UserId int            
AS
BEGIN           
           
SELECT 
BusinessAddress,
dbo.USERS.City AS City,
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
and dbo.USERS.UserType=2
END


-- Admin Portal add remove credit 

GO
/****** Object:  StoredProcedure [dbo].[usp_AddOrDebitCredits]    Script Date: 01/28/2011 15:55:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[usp_AddOrDebitCredits]
(
@UserId int,
@UserName varchar(255),
@UserIdOptedOruserName int,
@CreditOrDebit int,
@CreditCount int
)
as

Begin 

DECLARE @NetCredits	int
DECLARE @DummyNetCredits int

if (@CreditOrDebit = 1)
Begin

if (@UserIdOptedOruserName = 1)
Begin
Select @DummyNetCredits = NetCreditPoints from CreditPointTransaction where UserId= @UserId
Set @NetCredits= @CreditCount + @DummyNetCredits
End
else
Begin 
Select @DummyNetCredits= NetCreditPoints from CreditPointTransaction where UserId= (Select UserId from Users where Username= @username)  and transactionid in (select max(transactionid) from CreditPointTransaction)  order by createddate desc
Set @NetCredits= @CreditCount + @DummyNetCredits
Select  @UserId=UserId from Users where Username= @username
End


if(@NetCredits>=0)
Begin
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
@NetCredits,
getdate(),
0,
0,
getdate(),
getdate(),
111,
0,
111,
111

)

Select top 1 NetCreditPoints from CreditPointTransaction where UserId= @UserId order by CreatedDate
 desc
end

else
Begin
Select -1 as NetCreditPoints
End

End


if (@CreditOrDebit = 2)
Begin

if (@UserIdOptedOruserName = 1)
Begin
Select @DummyNetCredits = NetCreditPoints from CreditPointTransaction where UserId= @UserId
Set @NetCredits= @DummyNetCredits - @CreditCount 
End
else
Begin 
Select @DummyNetCredits= NetCreditPoints from CreditPointTransaction where UserId= (Select UserId from Users where Username= @username)  and transactionid in (select max(transactionid) from CreditPointTransaction)  order by createddate desc
Set @NetCredits= @DummyNetCredits - @CreditCount 
Select  @UserId=UserId from Users where Username= @username
End


if(@NetCredits> =0)
Begin
INSERT INTO [dbo].[CreditPointTransaction]
(UserId,
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
@NetCredits,
getdate(),
0,
0,
getdate(),
getdate(),
111,
0,
111,
111

)
Select top 1 NetCreditPoints from CreditPointTransaction where UserId= @UserId order by CreatedDate desc
end

else

Begin
Select -1 as NetCreditPoints
End
 

End

End

=======
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/*  LHK:Get custom header visibility
*/

Create PROCEDURE [dbo].[usp_GetCustomHeaderVisible]

	-- Input Parameters for the stored procedure 
	@TributeUrl	varchar(max)
AS

BEGIN
	SELECT dbo.USERBUSINESS.DisplayCustomHeader
	FROM dbo.USERBUSINESS,dbo.TRIBUTES
	WHERE dbo.USERBUSINESS.IsActive = 1 
and dbo.TRIBUTES.TributeUrl =  @TributeUrl 
and dbo.TRIBUTES.UserTributeId = dbo.USERBUSINESS.UserId
END


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/*  LHK:Get custom header UserName
*/


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[usp_GetTributeHeaderDetails]            
@UserId int            
AS
BEGIN           
           
SELECT 
dbo.USERS.UserName as UserName,
BusinessAddress,
dbo.USERS.City AS City,
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
and dbo.USERS.UserType=2
END

--====================================

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,LHK>
-- Create date: <6:03 PM 1/27/2011>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[usp_GetTributeUrlOnTributeId]
	@TributeId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select statements for procedure here
	SELECT TributeUrl from Tributes where TributeId=@TributeId;
END

-- =============================================
-- Author:		<Author,,LHK>
-- Create date: <1:14 AM 2/3/2011>
-- Description:	<Description,,>
-- =============================================

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/*  Get Image - This procedure will get the predefined custom header visiblity
*/

Create PROCEDURE [dbo].[usp_GetCustomHeaderVisibleOnId]

	-- Input Parameters for the stored procedure 
	@TributeId	int
AS

BEGIN
	SELECT dbo.USERBUSINESS.DisplayCustomHeader
	FROM dbo.USERBUSINESS,dbo.TRIBUTES
	WHERE dbo.USERBUSINESS.IsActive = 1 
and dbo.TRIBUTES.TributeId =  @TributeId 
and dbo.TRIBUTES.UserTributeId = dbo.USERBUSINESS.UserId
END


