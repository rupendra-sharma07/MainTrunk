create table DefaultTheme(DefaultThemeId int primary key identity(1,1),UserId int not null ,TributeType int null  ,ThemeId int null)
GO

ALTER TABLE LatestSummary
DROP CONSTRAINT fk_Comments_Createdby

GO
CREATE TABLE [dbo].[tblCOMMENTS_New](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[UserName] [varchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserType] [smallint] NULL,
	[TypeCodeId] [int] NOT NULL,
	[CommentTypeId] [int] NOT NULL,
	[Message] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsPrivate] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_tblCOMMENTS_New] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Statistic [_dta_stat_1920218091_1_4_3_12_11]    Script Date: 07/04/2011 19:06:17 ******/
CREATE STATISTICS [_dta_stat_1920218091_1_4_3_12_11] ON [dbo].[tblCOMMENTS_New]([CommentId], [CommentTypeId], [TypeCodeId], [IsDeleted], [IsActive])
GO
/****** Object:  Statistic [_dta_stat_1920218091_12_4]    Script Date: 07/04/2011 19:06:17 ******/
CREATE STATISTICS [_dta_stat_1920218091_12_4] ON [dbo].[tblCOMMENTS_New]([IsDeleted], [CommentTypeId])
GO
/****** Object:  Statistic [_dta_stat_1920218091_2_4_11_12]    Script Date: 07/04/2011 19:06:17 ******/
CREATE STATISTICS [_dta_stat_1920218091_2_4_11_12] ON [dbo].[tblCOMMENTS_New]([UserId], [CommentTypeId], [IsActive], [IsDeleted])
GO
/****** Object:  Statistic [_dta_stat_1920218091_2_4_12_3_1]    Script Date: 07/04/2011 19:06:17 ******/
CREATE STATISTICS [_dta_stat_1920218091_2_4_12_3_1] ON [dbo].[tblCOMMENTS_New]([UserId], [CommentTypeId], [IsDeleted], [TypeCodeId], [CommentId])
GO
/****** Object:  Statistic [_dta_stat_1920218091_2_4_3_12]    Script Date: 07/04/2011 19:06:17 ******/
CREATE STATISTICS [_dta_stat_1920218091_2_4_3_12] ON [dbo].[tblCOMMENTS_New]([UserId], [CommentTypeId], [TypeCodeId], [IsDeleted])
GO



Create procedure [dbo].[usp_GetFolderName]        
        
 (    
-- Input Parameters for the stored procedure         
 @ThemeId int

       ) 
AS        
        
BEGIN       
        
select FolderName from Themes where ThemeId =@ThemeId   
END 

Go

Create PROCEDURE [dbo].[usp_DeleteThemeCategory]  
  
 -- Input Parameters for the stored procedure   
 @themeId  int   
AS  
  
BEGIN   
  delete from themes where themeid=@themeId
END  
  GO
Create procedure [dbo].[usp_InsertDefaultTheme]
@UserId int,
@ThemeId int,
@TributeType varchar(100)
as

if exists(select * from DefaultTheme where UserId=@UserId and TributeType=dbo.ufn_GetParameterTypeCode(@TributeType,'TRIBUTE_TYPE'))
BEGIN
      update DefaultTheme set ThemeId=@ThemeId where UserId=@UserId and TributeType=dbo.ufn_GetParameterTypeCode(@TributeType,'TRIBUTE_TYPE')       
END
ELSE
BEGIN
       insert into DefaultTheme(UserId,TributeType,ThemeId) values(@UserId,dbo.ufn_GetParameterTypeCode(@TributeType,'TRIBUTE_TYPE'),@ThemeId)
END

GO

Create procedure [dbo].[usp_GetDefaultTheme] 
@UserId int,
@TributeType varchar(100)
as 
select * from DefaultTheme where UserId=@UserId and TributeType=dbo.ufn_GetParameterTypeCode(@TributeType,'TRIBUTE_TYPE') 

GO

Create Procedure [usp_CheckPhotoAlbum]  
@UserTributeId int,   
@PhotoAlbumCaption varchar(100)   
 
AS  
BEGIN  
 Declare @AlbumCount int  
  
 Select @AlbumCount = Count(*) from PhotoAlbum   
  Where UserTributeId = @UserTributeId and PhotoAlbumCaption = @PhotoAlbumCaption and IsDeleted = 0 and IsActive = 1  
   
 if (@AlbumCount <> 0)  
 
  Select -1  
 
  
   
END  

GO


ALTER PROCEDURE [dbo].[usp_GetcommentListPageWise]
@userId as int,
@TypeCodeId as int,
@CommentTypeId as int,
@TributeId as int,
@CurrentPage As tinyint,
@PageSize As tinyint
AS
BEGIN  
  --DECLARE @USEROLE AS INT
  --select @USEROLE=UserType from users where userid=@userId

  Declare @FirstRec int
  Declare @LastRec int
  Declare @BusinessCount int
  Declare @UserName varchar(200)
  -- Initialize variables.
  Set @FirstRec = (@CurrentPage - 1) * @PageSize
  Set @LastRec = (@CurrentPage * @PageSize + 1);

  With TempCommentTable AS
  (SELECT ROW_NUMBER() OVER (ORDER BY Comments.CreatedDate desc) AS 'RowNumber',
	  CommentId,comments.UserId,TypeCodeId,CommentTypeId,Message,
	  IsPrivate,CreatedBy,(CreatedDate),ModifiedBy,ModifiedDate,
	  comments.IsActive,comments.IsDeleted,
	  users.UserType, users.UserImage, users.FacebookUid,
	  0 as IsAdmin,--isnull(TRIBUTEADMINISTRATOR.UserId,0) as IsAdmin,
	  Case (Select COUNT(*) from UserBusiness where UserId = users.userid)
	  When 1 Then 
	       Case USERS.IsUserNameVisiable
	       When 1 Then USERS.UserName
	       Else (Select CompanyName from UserBusiness where UserId = users.userid)
	       End
	  Else 
	       Case USERS.IsUserNameVisiable
	       when 1 Then USERS.UserName
	       Else 
			CASE USERS.LastName
				WHEN '' THEN USERS.FirstName
				Else	USERS.FirstName + Char(9) + USERS.LastName
			End
			end
	  END as 'UserName'
	-- Added on 22-june-2011
--     ,'1' "UserType"
	,users.City, LOC1.LocationName AS 'State', LOC2.LocationName AS 'Country', users.IsLocationHide
	
--Added on 22-june-2011
   ,'1' "tableType"   --- 1 for this data is comming from Comments table


   FROM COMMENTS inner join users on users.userid=comments.userid  --TO CHECK WHETHTER LOGIN USER IS ADMIN OR NOT
			  --left join TRIBUTEADMINISTRATOR on TRIBUTEADMINISTRATOR.UserTributeId=@TributeId --and TRIBUTEADMINISTRATOR.userid=@userId
	Left Outer Join Locations LOC1 ON LOC1.LocationId = Users.State and LOC1.LocationParentId = Users.Country -- For State
	Left Outer Join Locations LOC2 ON LOC2.LocationId = Users.Country --For Country
   WHERE comments.IsDeleted =0 and comments.IsActive=1      --Isdeleted=1 means comment has been deleted 
     AND TypeCodeId=@TypeCodeId AND CommentTypeId=@CommentTypeId

	--- Added new on 22-june-2011 by rupendra

	Union all

	SELECT ROW_NUMBER() OVER (ORDER BY Comments.CreatedDate desc) AS 'RowNumber',
	  CommentId,comments.UserId,TypeCodeId,CommentTypeId,Message,
	  IsPrivate,CreatedBy,(CreatedDate),ModifiedBy,ModifiedDate,
	  comments.IsActive,comments.IsDeleted
	,comments.UserType
	, users.UserImage, users.FacebookUid,
	  0 as IsAdmin   
	 ,comments.UserName
		-- New Added field on 22-june-2011
     --,comments.UserType
     ,users.City, LOC1.LocationName AS 'State', LOC2.LocationName AS 'Country', users.IsLocationHide
    
     ,'2' "tableType"   --- 2 for this data is comming from tblComments_New table
	FROM tblComments_New Comments
 
	left outer join users on users.userid=comments.userid  --TO CHECK WHETHTER LOGIN USER IS ADMIN OR NOT
	 --left join TRIBUTEADMINISTRATOR on TRIBUTEADMINISTRATOR.UserTributeId=@TributeId --and TRIBUTEADMINISTRATOR.userid=@userId
	Left Outer Join Locations LOC1 ON LOC1.LocationId = Users.State and LOC1.LocationParentId = Users.Country -- For State
	Left Outer Join Locations LOC2 ON LOC2.LocationId = Users.Country --For Country
   WHERE comments.IsDeleted =0 and comments.IsActive=1      --Isdeleted=1 means comment has been deleted 
     AND TypeCodeId=@TypeCodeId AND CommentTypeId=@CommentTypeId


  )
  Select CommentId, UserId, TypeCodeId, CommentTypeId, Message, IsPrivate, CreatedBy, CreatedDate, ModifiedBy,
	 ModifiedDate, IsActive, IsDeleted, UserType, UserImage, IsAdmin, UserName, FacebookUid,
	 City, State, Country, IsLocationHide
     -- Added new
    ,UserType,tableType
  from TempCommentTable
  where RowNumber > @FirstRec and RowNumber < @LastRec

END

GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_AddComments]   
@UserId as int,  
@TypeCodeId as int,  
@CommentTypeId as varchar(100),  
@Message as varchar (3000), --should be text  
@IsPrivate as bit,  
@CreatedBy as int,  
 
@IsActive as bit,  
@IsDeleted as bit ,  

--- Added by rupendra for YT phase 4 Enhancement on 21 jun 2011
@UserName varchar(200)='',
@UserType smallint='0'

--- end here

AS  
--first time entry would be Active (1)  
set @IsActive=1     
--first time entry would be Active (1)  
set @IsDeleted=0  
 



--- end
Declare @CommentId int  


--- previous Query
--		INSERT INTO Comments  
--					(
--						UserId,TypeCodeId,CommentTypeId,Message,IsPrivate,CreatedBy,CreatedDate,IsActive,IsDeleted
--						
--					)  
--			VALUES   
--				(
--					@UserId,@TypeCodeId,@CommentTypeId,@Message,@IsPrivate,@CreatedBy,GETDATE(),@IsActive,@IsDeleted
--					
--				)  

--- End
   

-- here @UserType ---->  0 -- guest user , 1---> YT user , 2 ---> facebook user
INSERT INTO [tblCOMMENTS_New]
					(
						UserId,TypeCodeId,CommentTypeId,Message,IsPrivate,CreatedBy,CreatedDate,IsActive,IsDeleted
						--- Added by rupendra for YT phase 4 Enhancement on 21 jun 2011
						,UserName,UserType
					)  
VALUES   
	(
		@UserId,@TypeCodeId,@CommentTypeId,@Message,@IsPrivate,@CreatedBy,GETDATE(),@IsActive,@IsDeleted
		--- Added by rupendra for YT phase 4 Enhancement on 21 jun 2011
						,@UserName,@UserType
	)  
  
 Set @CommentId = @@Identity  
  
 Declare @CreateDate datetime  
 Set @CreateDate = GetDate()  
 --to insert data in Latest Summary Table  
 Exec usp_InsertLatestSummary 'Guestbook', @CommentId, @CreatedBy, @CommentTypeId, @CommentTypeId, 0, @CreateDate  
 
 



GO

---------------------------------------------------------
-- Updated by : Rupendra sharma
-- updated on : 23-jun-2011
-- purpose :    contains query to delete comments from tblComments_New , Comments
-- RunAS :     usp_deleteComment] @USERID='10',@COMMENTID='1345',

---------------------------------------------------------
ALTER PROCEDURE [dbo].[usp_deleteComment]
@COMMENTID AS INT,
@USERID AS INT,
@tableType varchar(1)='0'   -- this is to find out from wich table (tblComments_New or Comments) the comment exists ...... corrosponding values are 2, 1


AS

if @tableType='1'
begin
	UPDATE COMMENTS SET MODIFIEDDATE=GETDATE(),MODIFIEDBY=@USERID,ISDELETED=1
	WHERE COMMENTID=@COMMENTID
END
else if @tableType='2'
begin
	UPDATE tblComments_New SET MODIFIEDDATE=GETDATE(),MODIFIEDBY=@USERID,ISDELETED=1
	WHERE COMMENTID=@COMMENTID
END


GO



ALTER PROCEDURE [dbo].[usp_GetLatestVideo]  
@TributeId int,  
@Todaydate datetime ,  
@Seconddate datetime  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets FROM  
 -- interfering with SELECT statements.   
 SET NOCOUNT ON;  
  
WITH TempCommentTable AS  
( 
-- to get the values for videos 
 SELECT  usr.Userid,  
		 CASE ( SELECT COUNT(*) FROM UserBusiness WHERE UserId = ISNULL(vd.ModifiedBy,vd.UserId) )  
				WHEN 1 THEN   
					( 
						SELECT CASE usr1.IsUserNameVisiable  
							WHEN 1 THEN   usr1.UserName  
							ELSE urb.CompanyName  
							END  
							FROM USERS usr1,UserBusiness urb 
							WHERE usr1.Userid=urb.Userid  
							AND usr1.Userid=ISNULL(vd.ModifiedBy,vd.UserId)   
					)  
				ELSE   
						CASE usr.IsUserNameVisiable  
							WHEN 1 THEN usr.UserName  
							ELSE usr.FirstName + CHAR(9) + usr.LastName  
						END  
		 END   AS FirstName,  
		 vd.VideoCaption AS VideoCaption,  
		 vd.VideoDesc AS Message ,  
		 vd.VideoUrl AS VideoUrl,  
		 vd.VideoTypeId AS VideoTypeId,  
		 vd.VideoId AS ID, 
		 ISNULL(TributeVideoId,'') AS 'TributeVideoId',  
		 'Video' AS Type,  
		 ISNULL(vd.ModifiedDate,vd.CreatedDate)  AS CreatedDate,  
		 CASE ISNULL(CONVERT(VARCHAR(10),vd.ModifiedDate,111),'A')  
				WHEN 'A' THEN  'A'  
				ELSE 'U'  
		 END  AS MODE  
 FROM Videos vd,Users usr 
 WHERE vd.IsDeleted=0  
 AND vd.isactive=1
 AND vd.UserTributeId=@TributeId  
 AND usr.UserId=ISNULL(vd.ModifiedBy,vd.UserId)  
 AND usr.IsActive=1  
 AND usr.IsDeleted=0  
 AND ISNULL(CONVERT(VARCHAR(10),vd.ModifiedDate,111),CONVERT(VARCHAR(10),vd.CreatedDate,111))  
   BETWEEN CONVERT(VARCHAR(10),@Seconddate,111) AND CONVERT(VARCHAR(10),@Todaydate,111)     
  
UNION  
-- to get the values for guestbook entries 
  SELECT  usr.Userid,  
		 CASE  ( SELECT COUNT(*) FROM UserBusiness WHERE UserId = ISNULL(cmt.ModifiedBy,cmt.UserId))  
			WHEN 1 THEN   
			(  
				SELECT CASE usr1.IsUserNameVisiable  
					WHEN 1 THEN usr1.UserName  
					ELSE urb.CompanyName  
				END  
				FROM  USERS usr1,UserBusiness urb 
				WHERE usr1.Userid=urb.Userid  
				AND usr1.Userid=ISNULL(cmt.ModifiedBy,cmt.UserId)  
			)  
		 ELSE   
			CASE usr.IsUserNameVisiable  
				WHEN 1 THEN usr.UserName  
				ELSE usr.FirstName + CHAR(9) + usr.LastName  
			END  
		 END AS FirstName,  
		 '' AS VideoCaption,  
		 cmt.Message AS Message,  
		 '' AS VideoUrl,  
		 '' AS VideoTypeId,  
		 cmt.CommentId AS ID, '' AS 'TributeVideoId',  
		 'Guestbook' AS Type,
		 --cmt.CreatedDate AS CreatedDate   
		 ISNULL(cmt.ModifiedDate,cmt.CreatedDate) AS CreatedDate,  
	     'A' AS MODE  
 FROM dbo.COMMENTS cmt,dbo.USERS usr   
 WHERE cmt.CommentTypeId=@TributeId    
 AND cmt.TypeCodeId=dbo.ufn_GetParameterTypeCode('Guestbook','APP_SECTION')  
 AND cmt.IsActive=1  
 AND cmt.IsDeleted=0  
 AND usr.IsActive=1  
 AND usr.IsDeleted=0  
 AND usr.UserId=ISNULL(cmt.ModifiedBy,cmt.UserId)
 AND  ISNULL(CONVERT(VARCHAR(10),cmt.ModifiedDate,111),CONVERT(VARCHAR(10),cmt.CreatedDate,111))  
   BETWEEN CONVERT(VARCHAR(10),@Seconddate,111) AND CONVERT(VARCHAR(10),@Todaydate,111)  
  
-- My comment Rupendra 28 july2011
UNION
-- to get the values for guestbook entries 
  SELECT  cmt.Userid,  cmt.UserName 'FirstName',
--		 CASE  ( SELECT COUNT(*) FROM UserBusiness WHERE UserId = ISNULL(cmt.ModifiedBy,cmt.UserId))  
--			WHEN 1 THEN   
--			(  
--				SELECT CASE usr1.IsUserNameVisiable  
--					WHEN 1 THEN usr1.UserName  
--					ELSE urb.CompanyName  
--				END  
--				FROM  USERS usr1,UserBusiness urb 
--				WHERE usr1.Userid=urb.Userid  
--				AND usr1.Userid=ISNULL(cmt.ModifiedBy,cmt.UserId)  
--			)  
--		 ELSE   
--			CASE usr.IsUserNameVisiable  
--				WHEN 1 THEN usr.UserName  
--				ELSE usr.FirstName + CHAR(9) + usr.LastName  
--			END  
--		 END AS FirstName,  
		 '' AS VideoCaption,  
		 cmt.Message AS Message,  
		 '' AS VideoUrl,  
		 '' AS VideoTypeId,  
		 cmt.CommentId AS ID, '' AS 'TributeVideoId',  
		 'Guestbook' AS Type,
		 --cmt.CreatedDate AS CreatedDate   
		 ISNULL(cmt.ModifiedDate,cmt.CreatedDate) AS CreatedDate,  
	     'A' AS MODE  
 FROM dbo.tblCOMMENTS_New cmt --,dbo.USERS usr   
 WHERE cmt.CommentTypeId=@TributeId    
 AND cmt.TypeCodeId=dbo.ufn_GetParameterTypeCode('Guestbook','APP_SECTION')  
 AND cmt.IsActive=1  
 AND cmt.IsDeleted=0  
-- AND usr.IsActive=1  
-- AND usr.IsDeleted=0  
-- AND usr.UserId=ISNULL(cmt.ModifiedBy,cmt.UserId)
 AND  ISNULL(CONVERT(VARCHAR(10),cmt.ModifiedDate,111),CONVERT(VARCHAR(10),cmt.CreatedDate,111))  
   BETWEEN CONVERT(VARCHAR(10),@Seconddate,111) AND CONVERT(VARCHAR(10),@Todaydate,111)  



--- End
UNION  
-- to get the values for NOTES 
 SELECT  usr.Userid,  
		 --usr.FirstName+' '+usr.LastName AS FirstName ,  
		 CASE (SELECT COUNT(*) FROM UserBusiness WHERE UserId = ISNULL(NT.ModifiedBy,NT.UserId))  
			WHEN 1 THEN   
				(  
					SELECT CASE usr1.IsUserNameVisiable  
						WHEN 1 THEN  usr1.UserName  
						ELSE urb.CompanyName  
					END  
					FROM USERS usr1,UserBusiness urb 
					WHERE usr1.Userid=urb.Userid  
					AND usr1.Userid=ISNULL(NT.ModifiedBy,NT.UserId)  
				)  
			ELSE   
				CASE usr.IsUserNameVisiable  
					WHEN 1 THEN usr.UserName  
					ELSE usr.FirstName + CHAR(9) + usr.LastName  
				END  
		 END AS FirstName,  
		 NT.Title AS VideoCaption,  
		 CONVERT(VARCHAR(1000),NT.PostMessage,111) AS Message ,  
		 '' AS VideoUrl,  
		 '' AS VideoTypeId,  
		 NT.NotesId  AS ID, 
		 '' AS 'TributeVideoId',  
		 'Notes' AS Type,  
		 ISNULL(NT.ModifiedDate,NT.CreatedDate) AS CreatedDate,  
		 CASE ISNULL(CONVERT(VARCHAR(10),NT.ModifiedDate,111),'A')  
			WHEN 'A' THEN 'A'  
			ELSE  'U'  
		 END AS MODE  
 FROM NOTES NT,Users usr 
 WHERE NT.UserTributeId = @TributeId  
 AND   NT.IsDeleted = 0  
 AND   usr.UserId=ISNULL(NT.ModifiedBy,NT.UserId)
 AND   usr.IsActive=1  
 AND   usr.IsDeleted=0  
AND  ISNULL(CONVERT(VARCHAR(10),NT.ModifiedDate,111),CONVERT(VARCHAR(10),NT.CreatedDate,111))  
   BETWEEN CONVERT(VARCHAR(10),@Seconddate,111) AND CONVERT(VARCHAR(10),@Todaydate,111)  

UNION  
-- to get the values for Video Comment
 SELECT   usr.Userid,   
		 CASE (SELECT COUNT(*) FROM UserBusiness WHERE UserId = ISNULL(CMT.ModifiedBy,CMT.UserId))  
			 WHEN 1 THEN   
				(  
					SELECT CASE usr1.IsUserNameVisiable  
					WHEN 1 THEN usr1.UserName  
					ELSE  urb.CompanyName  
					END  
					FROM  USERS usr1,UserBusiness urb 
					WHERE	usr1.Userid=urb.Userid  
					AND usr1.Userid=ISNULL(CMT.ModifiedBy,CMT.UserId)  
				)  
			ELSE   
				CASE usr.IsUserNameVisiable  
				WHEN 1 THEN usr.UserName  
				ELSE usr.FirstName + CHAR(9) + usr.LastName  
			END  
		 END  AS FirstName,  
		 '' AS VideoCaption,  
		 cmt.Message AS Message,  
		 vd.VideoUrl AS VideoUrl,  
		 vd.VideoTypeId AS VideoTypeId,   
		 vd.VideoId AS ID, 
		 ISNULL(vd.TributeVideoId, '') AS 'TributeVideoId',  
		 'VideoComment' AS Type,   
		 ISNULL(cmt.ModifiedDate,cmt.CreatedDate) AS CreatedDate,  
		 'A' AS MODE  
 FROM dbo.COMMENTS CMT,Users usr,VIDEOS vd 
 WHERE	CMT.CommentTypeID=vd.VideoId  
 AND	CMT.TypeCodeId=4  
 AND	CMT.IsDeleted=0  
 AND    vd.UserTributeId=@TributeId  
 AND	vd.IsActive=1   
 AND    vd.IsDeleted=0 
 AND	usr.UserId=ISNULL(CMT.ModifiedBy,CMT.UserId)
 AND	usr.IsActive=1  
 AND	usr.IsDeleted=0  
 AND    ISNULL(CONVERT(VARCHAR(10),CMT.ModifiedDate,111),CONVERT(VARCHAR(10),CMT.CreatedDate,111))  
		BETWEEN CONVERT(VARCHAR(10),@Seconddate,111) AND CONVERT(VARCHAR(10),@Todaydate,111)   
 
UNION  
-- to get the values for Note Comments
 SELECT   usr.Userid,  
		 CASE (SELECT COUNT(*) FROM UserBusiness WHERE UserId = usr.userid)  
			WHEN 1 THEN   
			(  
				SELECT CASE usr1.IsUserNameVisiable  
					WHEN 1 THEN  usr1.UserName  
					ELSE urb.CompanyName  
				END  
				FROM  USERS usr1,UserBusiness urb 
				WHERE usr1.Userid=urb.Userid  
				AND usr1.Userid=usr.userid  
			)  
			ELSE   
				CASE usr.IsUserNameVisiable  
					WHEN 1 THEN usr.UserName  
					ELSE usr.FirstName + CHAR(9) + usr.LastName  
					END  
				END  AS FirstName,  
		 note.Title AS VideoCaption,  
		 cmt.Message AS Message,  
		 '' AS VideoUrl,  
		 CONVERT(VARCHAR(10),note.NotesId,111)AS VideoTypeId,  
		 cmt.CommentId  AS ID, 
		 '' AS 'TributeVideoId',  
		 'NoteComment' AS Type,   
		 ISNULL(cmt.ModifiedDate,cmt.CreatedDate) AS CreatedDate,  
		 'A' AS MODE  
 FROM dbo.COMMENTS cmt,Users usr,NOTES note 
 WHERE	CommentTypeID in (  SELECT NotesId FROM NOTES nt WHERE nt.UserTributeId=@TributeId  )  
 AND	cmt.TypeCodeId=dbo.ufn_GetParameterTypeCode('Notes','APP_SECTION')   
 AND	cmt.IsDeleted=0 
 AND	cmt.CommentTypeID=note.NotesId   
 AND	usr.UserId=cmt.UserId  
 AND	usr.IsActive=1  
 AND	usr.IsDeleted=0  
 AND	ISNULL(CONVERT(VARCHAR(10),cmt.ModifiedDate,111),CONVERT(VARCHAR(10),cmt.CreatedDate,111))  
		BETWEEN CONVERT(VARCHAR(10),@Seconddate,111) AND CONVERT(VARCHAR(10),@Todaydate,111)   

UNION  
--to get the values of Photo Albums
SELECT   ISNULL(PA.ModifiedBy,PA.UserId) AS Userid,  
		 CASE  (SELECT COUNT(*) FROM UserBusiness WHERE UserId = users.userid)  
			WHEN 1 THEN   
				(  
					SELECT CASE usr1.IsUserNameVisiable  
					WHEN 1 THEN usr1.UserName  
					ELSE urb.CompanyName  
					END  
					FROM  USERS usr1,UserBusiness urb 
					WHERE  usr1.Userid=urb.Userid  
					AND usr1.Userid=users.userid  
				)  
			ELSE   
				CASE users.IsUserNameVisiable  
					WHEN 1 THEN users.UserName  
					ELSE users.FirstName + CHAR(9) + users.LastName  
				END  
			END  AS FirstName,  
		 PA.PhotoAlbumCaption AS VideoCaption,  
		 '' AS Message,  
		 '' AS VideoUrl,  
		 '' AS VideoTypeId,  
		 PA.PhotoAlbumId AS ID, 
		 '' AS 'TributeVideoId',  
		 'Album' AS Type,  
		 ISNULL(PA.ModifiedDate,PA.CreatedDate) AS CreatedDate,  
		 'A' AS MODE  
 FROM PhotoAlbum PA  Inner Join Users 
 on Users.UserId=ISNULL(PA.ModifiedBy,PA.UserId)  
 WHERE	 PA.UserTributeId = @TributeId 
 AND	 PA.IsDeleted = 0 
 AND	 PA.IsActive = 1 
 AND	 ISNULL(CONVERT(VARCHAR(10),PA.ModifiedDate,111),CONVERT(VARCHAR(10),PA.CreatedDate,111))  
		 BETWEEN CONVERT(VARCHAR(10),@Seconddate,111) AND CONVERT(VARCHAR(10),@Todaydate,111)    

UNION  
 -- to get the values of Photo Comment
 SELECT   usr.Userid,  
		 CASE  (SELECT COUNT(*) FROM UserBusiness WHERE UserId = ISNULL(cmt.ModifiedBy,cmt.UserId))  
			WHEN 1 THEN   
				(  
					SELECT CASE usr1.IsUserNameVisiable  
						WHEN 1 THEN   usr1.UserName  
						ELSE  urb.CompanyName  
					END  
					FROM USERS usr1,UserBusiness urb 
					WHERE usr1.Userid=urb.Userid  
					AND usr1.Userid=ISNULL(cmt.ModifiedBy,cmt.UserId)  
				)  
			ELSE   
				CASE usr.IsUserNameVisiable  
					WHEN 1 THEN usr.UserName  
					ELSE usr.FirstName + CHAR(9) + usr.LastName  
				END  
			END AS FirstName,   
		 PH.PhotoCaption AS VideoCaption,  
		 cmt.Message AS Message,  
		 PH.PhotoImage AS VideoUrl,     
		 CONVERT(VARCHAR(10),PH.UserPhotoId,111)AS VideoTypeId,  
		 cmt.CommentId  AS ID, '' AS 'TributeVideoId',  
		 'PhotoComment' AS Type,     
		 ISNULL(cmt.ModifiedDate,cmt.CreatedDate) AS CreatedDate,
		 'A' AS MODE  
FROM dbo.COMMENTS cmt,Users usr,PHOTO PH 
WHERE CommentTypeID in   (  
							SELECT UserPhotoId FROM dbo.PHOTO WHERE PhotoAlbumId in   
							(
								SELECT PhotoAlbumId FROM dbo.PHOTOALBUM WHERE UserTributeId=@TributeId 
							)   
						 )  
AND		cmt.TypeCodeId=dbo.ufn_GetParameterTypeCode('Photo','APP_SECTION')   
AND		cmt.IsDeleted=0  
AND		usr.UserId=ISNULL(cmt.ModifiedBy,cmt.UserId) 
AND		usr.IsActive=1  
AND		usr.IsDeleted=0  
AND		cmt.CommentTypeID=PH.UserPhotoId  
AND		ISNULL(CONVERT(VARCHAR(10),cmt.ModifiedDate,111),CONVERT(VARCHAR(10),cmt.CreatedDate,111))  
		BETWEEN CONVERT(VARCHAR(10),@Seconddate,111) AND CONVERT(VARCHAR(10),@Todaydate,111)   
  
UNION 
 --to get the values for events
 SELECT	 evt.UserId,  
		 CASE (SELECT COUNT(*) FROM UserBusiness WHERE UserId = ISNULL(EVT.ModifiedBy,EVT.UserId) )  
				WHEN 1 THEN   
				(  
					SELECT CASE usr1.IsUserNameVisiable  
						WHEN 1 THEN  usr1.UserName  
						ELSE  urb.CompanyName  
					END  
					FROM   USERS usr1,UserBusiness urb 
					WHERE  usr1.Userid=urb.Userid  
					AND usr1.Userid=ISNULL(EVT.ModifiedBy,EVT.UserId)   
				)  
				ELSE   
					CASE usr.IsUserNameVisiable  
						WHEN 1 THEN usr.UserName  
						ELSE usr.FirstName + CHAR(9) + usr.LastName  
					END  
		 END   AS FirstName,  
		 evt.EventName AS VideoCaption,  
		 evt.EventDesc AS Message,  
		 evt.EventImage AS VideoUrl,  
		 '' AS VideoTypeId,  
		 evt.EventID AS ID, '' AS 'TributeVideoId',  
		 'Event' AS Type,  
		 ISNULL(evt.ModifiedDate,evt.CreatedDate)   AS CreatedDate,  
		 CASE ISNULL(CONVERT(VARCHAR(10),evt.ModifiedDate,111),'A')  
				WHEN 'A' THEN  'A'  
				ELSE  'U'  
		 END  AS MODE  
 FROM    dbo.EVENT EVT,Users usr 
 WHERE   Tributeid=@TributeId  
 AND IsPrivate=0  
 AND usr.UserId=ISNULL(EVT.ModifiedBy,EVT.UserId) 
 AND usr.IsActive=1  
 AND usr.IsDeleted=0  
 AND evt.IsActive=1  
 AND evt.IsDeleted=0  
 AND  ISNULL(CONVERT(VARCHAR(10),evt.ModifiedDate,111),CONVERT(VARCHAR(10),evt.CreatedDate,111))  
   BETWEEN CONVERT(VARCHAR(10),@Seconddate,111) AND CONVERT(VARCHAR(10),@Todaydate,111)    
  
  
UNION  
  -- to get the value of story
SELECT  ST.UserId,  
		 CASE (SELECT COUNT(*) FROM UserBusiness WHERE UserId = ISNULL(SD.ModifiedBy,ST.UserId))  
			WHEN 1 THEN   
				(  
					SELECT CASE usr1.IsUserNameVisiable  
							WHEN 1 THEN usr1.UserName  
							ELSE urb.CompanyName  
							END  
					FROM   USERS usr1,UserBusiness urb 
					WHERE  usr1.Userid=urb.Userid  
					AND usr1.Userid=ISNULL(SD.ModifiedBy,ST.UserId)
				)  
		    ELSE 
				  CASE usr.IsUserNameVisiable  
					WHEN 1 THEN usr.UserName  
					ELSE usr.FirstName + CHAR(9) + usr.LastName  
				  END  
		 END AS FirstName,
		 ST.PrimaryTitle AS VideoCaption,   
		 CONVERT(VARCHAR(1000),SD.SectionAnswer,111) AS Message,  
		 ST.SecondaryTitle AS VideoUrl,  
		 '' AS VideoTypeId,  
		 SD.UserBiographyId  AS ID, '' AS 'TributeVideoId',  
		 'Story' AS Type,   
		 ISNULL(SD.ModifiedDate,SD.CreatedDate) AS CreatedDate,  
		 CASE ISNULL(CONVERT(VARCHAR(10),SD.ModifiedDate,111),'A')  
			WHEN 'A' THEN  'A'  
			ELSE 'U'  
		 END AS MODE 		 
 FROM	Users usr,   STORIES ST ,   STORIESDETAILS SD  
 WHERE  ST.SectionId = SD.SectionId 
 AND    ST.TributeId = @TributeId  
 AND	SD.IsActive = 1   
 AND    SD.IsDeleted = 0  
 AND	usr.UserId=ISNULL(SD.ModifiedBy,ST.UserId) 
 AND	usr.IsActive=1  
 AND	usr.IsDeleted=0  
 AND    ISNULL(CONVERT(VARCHAR(10),SD.ModifiedDate,111),CONVERT(VARCHAR(10),SD.CreatedDate,111))  
		BETWEEN CONVERT(VARCHAR(10),@Seconddate,111) AND CONVERT(VARCHAR(10),@Todaydate,111)  
    
UNION  
 --to get the gift values
SELECT   ISNULL(gft.Userid,0) AS Userid,  
		 Case (ISNULL(gft.Userid,0))  
			when 0 Then  gft.GiftSentBy  
			Else  
				(  
					SELECT Case (SELECT COUNT(*) FROM UserBusiness WHERE UserId = ISNULL(gft.ModifiedBy,gft.UserId))  
						when 1 Then (  
									SELECT Case usr.IsUserNameVisiable  
										When 1 then   usr.UserName  
										else urb.CompanyName  
									end  
									FROM USERS usr,UserBusiness urb 
									WHERE  usr.Userid=urb.Userid  
									AND usr.Userid=gft.UserID)  
						Else   
							Case usr.IsUserNameVisiable  
								when 1 Then usr.UserName  
								Else usr.FirstName + Char(9) + usr.LastName  
							end  
					end  
					FROM Users usr WHERE UserId = ISNULL(gft.ModifiedBy,gft.UserId)  
				)  
			END  AS FirstName,  
		 '' AS VideoCaption,  
		 gft.GiftMessage AS Message,  
		 img.ImageUrl AS VideoUrl,  
		 '' AS VideoTypeId,  
		 gft.GiftId  AS ID, 
		 '' AS 'TributeVideoId',  
		 'Gift' AS Type,   
		 ISNULL(gft.ModifiedDate,gft.CreatedDate) AS CreatedDate,
		 'A' AS MODE  
 FROM  dbo.IMAGE img, dbo.GIFT  gft   
 WHERE  img.ImageId = gft.ImageId  
 AND	gft.IsActive=1  
 AND	gft.IsDeleted=0  
 AND	gft.tributeId=@TributeId  
 AND    ISNULL(CONVERT(VARCHAR(10),gft.ModifiedDate,111),CONVERT(VARCHAR(10),gft.CreatedDate,111))  
   between CONVERT(VARCHAR(10),@Seconddate,111) AND CONVERT(VARCHAR(10),@Todaydate,111) 
  
  
)  
  
SELECT UserId, FirstName,  
 VideoCaption, Message,  
VideoUrl,VideoTypeId,ID,   
 TributeVideoId,   
 Type,CreatedDate,MODE  
 FROM TempCommentTable  
order by CreatedDate desc  
  
  
  
END  

GO

ALTER PROCEDURE [dbo].[usp_GetcommentsCount]
@userId as int,
@TypeCodeId as int,
@CommentTypeId as int,
@UserTributeId as int
AS


--DECLARE @USEROLE AS INT
--select @USEROLE=UserType from users where userid=@userId
--
--IF @USEROLE=2 OR @USEROLE=3 
--	BEGIN
      
	declare @Total_Records int
	select @Total_Records='0'
     --- New query added by rupendra to display the New Comments Table record count

		select @Total_Records = @Total_Records + count (*) 
		--FROM COMMENTS inner join users on users.userid=comments.userid  --TO CHECK WHETHTER LOGIN USER IS ADMIN OR NOT
		FROM COMMENTS left outer join users on users.userid=comments.userid  --TO CHECK WHETHTER LOGIN USER IS ADMIN OR NOT
		WHERE comments.IsDeleted <>1 and comments.IsActive=1      --Isdeleted=1 means comment has been deleted 
		AND TypeCodeId=@TypeCodeId
		AND CommentTypeId=@CommentTypeId
		
		select @Total_Records = @Total_Records + count (*) 
		--FROM COMMENTS inner join users on users.userid=comments.userid  --TO CHECK WHETHTER LOGIN USER IS ADMIN OR NOT
		FROM tblCOMMENTS_New COMMENTS left outer join users on users.userid=comments.userid  --TO CHECK WHETHTER LOGIN USER IS ADMIN OR NOT
		WHERE comments.IsDeleted <>1 and comments.IsActive=1      --Isdeleted=1 means comment has been deleted 
		AND TypeCodeId=@TypeCodeId
		AND CommentTypeId=@CommentTypeId

		Select @Total_Records "RecordCount"


GO

Create procedure [dbo].[usp_GetThemesName]        
        
 (    
-- Input Parameters for the stored procedure         
 @CategoryName  varchar(150),   
 @SubCategoryName  varchar(150)      

       ) 
AS        
        
BEGIN       
        
select Themeid, ThemeName from Themes where TributeType=@CategoryName and   SubCategory= @SubCategoryName          
END 
