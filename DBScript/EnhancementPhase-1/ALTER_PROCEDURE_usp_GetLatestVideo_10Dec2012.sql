USE [TributePortal_New_P1]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetLatestVideo]    Script Date: 12/10/2012 21:09:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
		 CONVERT(VARCHAR(1000),NT.MessageWithoutHtml,111) AS Message ,  
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
 UNION  
  -- to get the value of Obituary
SELECT  ISNULL(Trb.MessageCreatedBy,Trb.MessageModifiedBy) as UserId,  
		 CASE (SELECT COUNT(*) FROM UserBusiness WHERE UserId = ISNULL(Trb.MessageCreatedBy,Trb.MessageModifiedBy))
			WHEN 1 THEN   
				(  
					SELECT CASE usr1.IsUserNameVisiable  
							WHEN 1 THEN usr1.UserName  
							ELSE urb.CompanyName  
							END  
					FROM   USERS usr1,UserBusiness urb 
					WHERE  usr1.Userid=urb.Userid  
					AND usr1.Userid = ISNULL(Trb.MessageCreatedBy,Trb.MessageModifiedBy)
				)  
		    ELSE 
				  CASE usr.IsUserNameVisiable  
					WHEN 1 THEN usr.UserName  
					ELSE usr.FirstName + CHAR(9) + usr.LastName  
				  END  
		 END AS FirstName,
		 'Obituary' AS VideoCaption,   
		 CONVERT(VARCHAR(1000),Trb.MessageWithoutHtml,111) AS Message,  
		 '' AS VideoUrl,  
		 '' AS VideoTypeId,  
		 Trb.TributeId  AS ID, '' AS 'TributeVideoId',  
		 'Obituary' AS Type,   
		 ISNULL(Trb.MessageCreatedDate,Trb.MessageModifiedDate) AS CreatedDate,  
		 CASE ISNULL(CONVERT(VARCHAR(10),Trb.MessageModifiedDate,111),'A')  
			WHEN 'A' THEN  'A'  
			ELSE 'U'  
		 END AS MODE 		 
 FROM	Users usr,   TRIBUTES Trb
 WHERE  Trb.TributeId = @TributeId  
 AND	Trb.IsActive = 1   
 AND    Trb.IsDeleted = 0  
 AND	usr.UserId=ISNULL(Trb.MessageCreatedBy,Trb.MessageModifiedBy)
 AND	usr.IsActive=1  
 AND	usr.IsDeleted=0  
 AND    ISNULL(CONVERT(VARCHAR(10),Trb.MessageModifiedDate,111),CONVERT(VARCHAR(10),Trb.MessageModifiedDate,111))  
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


