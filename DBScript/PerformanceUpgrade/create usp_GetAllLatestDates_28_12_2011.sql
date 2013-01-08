GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllLatestDates]    Script Date: 12/28/2011 10:35:39 ******/
--created by- LHK 28/12/2011
--Purpose - Get latest dates
--execute dbo.usp_GetAllLatestDates @Todaydate='12/28/2011',@Seconddate='12/28/2010'

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

ALTER PROCEDURE [dbo].[usp_GetAllLatestDates]  
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
		 CONVERT(VARCHAR(10),ISNULL(vd.ModifiedDate,vd.CreatedDate),111)  AS CreatedDate
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
		 CONVERT(VARCHAR(10),ISNULL(cmt.ModifiedDate,cmt.CreatedDate),111) AS CreatedDate
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
  SELECT  cmt.Userid,
		 CONVERT(VARCHAR(10),ISNULL(cmt.ModifiedDate,cmt.CreatedDate),111) AS CreatedDate
 FROM dbo.tblCOMMENTS_New cmt --,dbo.USERS usr   
 WHERE cmt.CommentTypeId=@TributeId    
 AND cmt.TypeCodeId=dbo.ufn_GetParameterTypeCode('Guestbook','APP_SECTION')  
 AND cmt.IsActive=1  
 AND cmt.IsDeleted=0  
 AND  ISNULL(CONVERT(VARCHAR(10),cmt.ModifiedDate,111),CONVERT(VARCHAR(10),cmt.CreatedDate,111))  
   BETWEEN CONVERT(VARCHAR(10),@Seconddate,111) AND CONVERT(VARCHAR(10),@Todaydate,111)  
--- End
UNION  
-- to get the values for NOTES 
 SELECT  usr.Userid,   
		 CONVERT(VARCHAR(10),ISNULL(NT.ModifiedDate,NT.CreatedDate),111) AS CreatedDate
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
		 CONVERT(VARCHAR(10),ISNULL(cmt.ModifiedDate,cmt.CreatedDate),111) AS CreatedDate
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
		 CONVERT(VARCHAR(10),ISNULL(cmt.ModifiedDate,cmt.CreatedDate),111) AS CreatedDate
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
		 CONVERT(VARCHAR(10),ISNULL(PA.ModifiedDate,PA.CreatedDate),111) AS CreatedDate
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
		 CONVERT(VARCHAR(10),ISNULL(cmt.ModifiedDate,cmt.CreatedDate),111) AS CreatedDate
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
		 CONVERT(VARCHAR(10),ISNULL(evt.ModifiedDate,evt.CreatedDate),111)   AS CreatedDate   
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
		 CONVERT(VARCHAR(10),ISNULL(SD.ModifiedDate,SD.CreatedDate),111) AS CreatedDate  		 
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
		 CONVERT(VARCHAR(10),ISNULL(gft.ModifiedDate,gft.CreatedDate),111) AS CreatedDate
 FROM  dbo.IMAGE img, dbo.GIFT  gft   
 WHERE  img.ImageId = gft.ImageId  
 AND	gft.IsActive=1  
 AND	gft.IsDeleted=0  
 AND	gft.tributeId=@TributeId  
 AND    ISNULL(CONVERT(VARCHAR(10),gft.ModifiedDate,111),CONVERT(VARCHAR(10),gft.CreatedDate,111))  
   between CONVERT(VARCHAR(10),@Seconddate,111) AND CONVERT(VARCHAR(10),@Todaydate,111) 
  
  
)  
  
SELECT CONVERT(VARCHAR(10),CreatedDate,111)as CreatedDate
 FROM TempCommentTable 
group by  CreatedDate
order by CreatedDate desc  

  
END  