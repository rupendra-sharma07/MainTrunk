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