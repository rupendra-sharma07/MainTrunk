GO
/****** Object:  StoredProcedure [dbo].[usp_SaveUserPersonalAccount]    Script Date: 02/09/2011 14:04:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[usp_SaveUserPersonalAccount]
(
 @UserName varchar(100),
 @Password varchar(50),
 @FirstName varchar(255),
 @LastName varchar(255),
 @Email varchar(255),
 @VerificationCode varchar(100),   
 @AllowIncomingMsg bit,  
 @City varchar(50),
 @State int,
 @Country int,
 @UserImage  varchar(250),
 @UserType int,
 @FacebookUid bigint
)
AS
BEGIN

SET NOCOUNT ON	
	
BEGIN
 TRY
    DECLARE @IsLocationHide bit
    SET @IsLocationHide = 0
    if @Country is null or @Country=-1
      SET @IsLocationHide = 1

    DECLARE @Username_base varchar(100)
    SET @Username_base = @UserName

    DECLARE @nameVersion int
    SET @nameVersion = 0

	IF @UserName != ''
	Begin
		WHILE (select COUNT(*) from USERS where [UserName]=@UserName)>0
		BEGIN
		  SET @nameVersion = @nameVersion+1
		  SET @UserName = @Username_base+'_'+cast(@nameVersion as varchar(100))
		END
	End

    INSERT INTO [dbo].[USERS] (
      [UserName],[Password],
      [FirstName],[LastName],
      [Email],[VerificationCode],[AllowIncomingMsg],
      [City],[State],[Country],
      [UserImage],
      [UserType], [FacebookUid], [IsLocationHide]
    )
    VALUES ( @UserName, @Password,
	@FirstName, @LastName,
	@Email, @VerificationCode, @AllowIncomingMsg,
	@City, @State, @Country,
        @UserImage, --'images/bg_ProfilePhoto.gif'
        @UserType, @FacebookUid, @IsLocationHide )

      DECLARE @new_id int      
      SELECT @new_id = @@IDENTITY

      If @Email='' AND (@FacebookUid IS NOT NULL) AND @FacebookUid>0
	insert into EMAILNOTIFICATION(UserId,[StoryNotify], [NotesNotify], [EventsNotify],
	  [GuestBookNotify], [PhotoAlbumNotify], [PhotosNotify], [VideosNotify],
          [CommentsNotify], [MessagesNotify],	NewsLetterNotify) 
	values(@new_id, 0, 0, 0, 
          0, 0, 0, 0, 
          0, 0,	0)
      Else	
        insert into EMAILNOTIFICATION(UserId,NewsLetterNotify) values(@new_id,@AllowIncomingMsg)

       SELECT USERS.UserId, USERS.UserName, dbo.USERS.Password, 
              CASE USERS.Usertype
                when 1 then USERS.FirstName else  UB.CompanyName
	      END as FirstName,   
              USERS.LastName, USERS.Email, USERS.FacebookUid, USERS.UserType, USERS.UserImage,
              USERS.IsUsernameVisiable,  PARAMETERSTYPESCODES.TypeDescription,   
              USERS.IsActive, USERS.IsDeleted    
       FROM USERS INNER JOIN PARAMETERSTYPESCODES      
           ON (USERS.UserType = PARAMETERSTYPESCODES.TypeCode  AND 
	       PARAMETERSTYPESCODES.ParameterType = 'USER_ROLE') 
         left join UserBusiness UB on USERS.Userid = UB.userid  
       WHERE USERS.UserId = @new_id		

  END TRY

    BEGIN CATCH
		EXEC RethrowError;
	END CATCH
    
    SET NOCOUNT OFF
END

--==============================================

/******LHK;(1:18 PM 2/9/2011)Obituary changes*********/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
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
@Date2 as datetime,
@PostMessage text, 
@MessageWithoutHtml text

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
                        CreatedBy, CreatedDate,PostMessage,MessageWithoutHtml)
				values( @UserTributeId, @tributeName,
                        @tributeType, @welcomeMessage,
                        @tributeImage, @tributeUrl, @themeId, 
                        @city,  @state, @country, @IsPrivate,@IsOrderDVDChecked,@IsMemTributeBoxChecked,
                        @Date1,@Date2,
                        @UserTributeId, getdate(),@PostMessage,@MessageWithoutHtml)
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
--------------------------------------
--LHK:story update for Obituary

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/*  Get Story - This procedure will get the Tribute Details, Story and List of topic in more about section.
	This will also find that user is admin or not for the this tribute
*/

ALTER Procedure [dbo].[usp_GetStory]

	-- Input Parameters for the stored procedure 
	@TributeId	int,
	@UserId		int
AS

BEGIN

	SET NOCOUNT ON;

		-- Find that user is admin or not for this tribute
		SELECT count(*) AS 'IsAdmin'
		FROM TRIBUTEADMINISTRATOR 
		WHERE UserId = @UserId and UserTributeId = @TributeId and IsDeleted = 0

		-- Get Tribute Detail to display in Personal Detail Section
		SELECT  TributeId, UserTributeId, TributeName, TributeImage, TributeUrl, Date1, Date2, City, 
					CreatedDate, State, Country,PostMessage,MessageWithoutHtml,
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

		-- Get Story and topic list in the more about section
		SELECT ST.SectionId, ST.TributeId, ST.UserId, ST.PrimaryTitle, ST.SecondaryTitle, SD.UserBiographyId, SD.SectionAnswer
		FROM STORIES ST
		INNER JOIN STORIESDETAILS SD
		ON ST.SectionId = SD.SectionId and ST.TributeId = @TributeId and SD.IsDeleted = 0

END

--LHK: Update obituary
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[usp_UpdateObituaryDetail]
	@TributeId			int,
	@PostMessage	   text,
	@MessageWithoutHtml text

AS
BEGIN

	SET NOCOUNT ON;
		UPDATE	TRIBUTES

		SET		PostMessage = @PostMessage,
				MessageWithoutHtml = @MessageWithoutHtml

		WHERE	TributeId = @TributeId
END


ALTER PROCEDURE [dbo].[usp_GetSequenceTribute]
	-- Add the parameters for the stored procedure here
@TributeName VARCHAR(1000),
@TributeType VARCHAR(1000)
AS

-- Declare variables.
Declare @Record int
Declare @SequenceTributeName VARCHAR(1000)
SET @Record=1
SET @SequenceTributeName=@TributeName
BEGIN
	
	WHILE @Record<50
	BEGIN
		IF (SELECT 	COUNT(*) FROM Tributes, PARAMETERSTYPESCODES
			 Where (TributeUrl=@SequenceTributeName or OldTributeUrl=@SequenceTributeName  ) AND							TypeDescription=@TributeType AND ParameterType='TRIBUTE_TYPE')<1
			BEGIN
				SET @SequenceTributeName= @TributeName + CONVERT(VARCHAR,@Record)
				BREAK
			END
		ELSE
			BEGIN
				SET @Record=@Record+1
				SET @SequenceTributeName= @TributeName + CONVERT(VARCHAR,@Record)
				CONTINUE
				
			END
	END
	SELECT @SequenceTributeName

END

--LHK: GetCurrentNotes
--6:10 PM 2/16/2011
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[usp_GetCurrentNotes]
@tributeId int
AS
BEGIN

	Select Count(NotesId)
         from NOTES
         Where UserTributeId = @tributeId and IsDeleted = 0
END

--LHK: GetCurrentEvents
--6:10 PM 2/16/2011
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[usp_GetCurrentEvents]
@tributeId int
AS
BEGIN

	Select Count(EventID) as TotalEvents
         from EVENT
         Where TributeId = @tributeId and IsDeleted = 0
END


--LHK: GetCurrentPhotoAlbum
--6:10 PM 2/16/2011

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[usp_GetCurrentPhotoAlbum]
@tributeId int
AS
BEGIN

	Select Count(PhotoAlbumId) as TotalPhotoAlbum
         from PHOTOALBUM
         Where UserTributeId = @tributeId and IsDeleted = 0
END
--LHK: GetCurrentVideos
--6:10 PM 2/16/2011

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[usp_GetCurrentVideos]
@tributeId int
AS
BEGIN

	Select Count(VideoId) as TotalVideos
         from VIDEOS
         Where UserTributeId = @tributeId and IsDeleted = 0
END

--Moihit's change.
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
 
/*  This procedure will return all administrators whether email alerts is true or false   
*/  


ALTER Procedure [dbo].[usp_GetAdministrators]  
@intUserTributeId int
AS
BEGIN
	select TA.UserId, TA.Email,   
	Case   
		(Select COUNT(*) from UserBusiness where UserId = TA.UserId)  
	 when 1 Then   
	 (  
	  Select Case usr1.IsUserNameVisiable  
	  When 1 then   
	   usr1.UserName  
	  else  
	   urb.CompanyName  
	  end  
	  from   
	   USERS usr1,UserBusiness urb where  
	   usr1.Userid=urb.Userid  
	   and usr1.Userid=TA.UserId  
	 )  
	Else   
	 Case US.IsUserNameVisiable  
	 when 1 Then US.UserName  
	  Else US.FirstName  + Char(9) + US.LastName  
	 end  
	end  
	as FirstName,  
	 US.LastName,TA.IsOwner,us.UserType  
	   from TRIBUTEADMINISTRATOR TA  
	   Inner Join Users US ON US.UserId = TA.UserId  
	   Where TA.UserTributeId = @intUserTributeId and TA.IsActive = 1 and TA.IsDeleted=0  
END


--LHK (1:20 AM 2/20/2011)
--6:10 PM 2/16/2011
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

Alter Procedure [dbo].[usp_GetCurrentNotes]
@tributeId int
AS
BEGIN

	Select Count(NotesId) as TotalNotes
         from NOTES
         Where UserTributeId = @tributeId and IsDeleted = 0
END

--LHK: GetCurrentEvents
--6:10 PM 2/16/2011
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO


[8:40:02 PM] Mohit Gupta: USE [DotComDB01072010]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserDetailsFromEmail]    Script Date: 02/23/2011 20:39:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[usp_GetUserDetailsFromEmail]    
@email varchar(max),
@password  varchar(max) 
AS    
BEGIN    
  SET NOCOUNT ON      
  BEGIN    
   TRY    
   if((Select COUNT(*) from USERBUSINESS where [UserId]=(Select Userid from Users US where US.email=@email and US.password=@password))>0)    
     BEGIN   
     SELECT users.[UserId]    
     ,users.[UserName]    
     ,users.[Password]
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
     users.[Email]=@email
      and 
      users.[Password]=@password   
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
      users.[Email]=@email
      and 
      users.[Password]=@password
      END     
   END TRY    
      BEGIN CATCH    
    EXEC RethrowError;    
   END CATCH    
    
        --END    
  SET NOCOUNT OFF    
        

END



Create PROCEDURE [dbo].[usp_UpdateTributeURL]
  @tributeId int,
  @tributeUrl varchar(250) 
  
AS  
BEGIN  

if (SELECT count(*) FROM TRIBUTES WHERE TributeUrl = @tributeUrl or oldtributeurl =@tributeUrl )=0
Begin
 UPDATE TRIBUTES  SET TributeURL=@tributeUrl WHERE tributeId =  @tributeId    
END  
End

END


--LHK :(1:04 AM 2/24/2011) GetTributeUrlOnOldTributeUrl
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[usp_GetTributeUrlOnOldTributeUrl]
	 @TributeURL varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select statements for procedure here
	SELECT TributeURL from Tributes where OldTributeURL=@TributeURL
    and IsDeleted=0;
END

--LHK:
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
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
@Date2 as datetime,
@PostMessage text, 
@MessageWithoutHtml text

as
Begin
Declare @_Email varchar(250)
Set NoCount On

Begin Transaction
Insert into Tributes (UserTributeId, TributeName,
  						TributeType, WelcomeMessage,
                        TributeImage,TributeUrl,OldTributeURL ,
                        ThemeId, City, [State],	
                       	Country, IsPrivate,IsOrderDVDChecked,IsMemTributeBoxChecked,
                        Date1,Date2,
                        CreatedBy, CreatedDate,PostMessage,MessageWithoutHtml)
				values( @UserTributeId, @tributeName,
                        @tributeType, @welcomeMessage,
                        @tributeImage, @tributeUrl,@tributeUrl, @themeId, 
                        @city,  @state, @country, @IsPrivate,@IsOrderDVDChecked,@IsMemTributeBoxChecked,
                        @Date1,@Date2,
                        @UserTributeId, getdate(),@PostMessage,@MessageWithoutHtml)
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

--LHK:(10:33 PM 2/25/2011)
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[usp_GetTributeIdOnTributeUrl]
	@TributeUrl varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select statements for procedure here
	SELECT TributeId from Tributes where TributeUrl=@TributeUrl or OldtributeUrl=@TributeUrl ;
END



----------------------------------------------
--LHK:
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[usp_GetTributeUrlOnOldTributeUrl]
	 @TributeURL varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select statements for procedure here
	SELECT TributeURL,TypeDescription from Tributes,PARAMETERSTYPESCODES where OldTributeURL=@TributeURL
and  ParameterType='TRIBUTE_TYPE' and TypeCode=TributeType
    and IsDeleted=0;
END

-----------------------------------------------
-- Alter usp for videoTribute Creation order reciept
--3:17 PM 3/22/2011
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[usp_GetVideoTributeTransactionDetails]  
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
 Where tp.TributePackageId = @TributePackageId order by ucd.createddate,transactionId desc
END
