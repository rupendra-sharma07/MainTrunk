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
else
--  Delete for Photo comments By Ashu---
UPDATE COMMENTS SET MODIFIEDDATE=GETDATE(),MODIFIEDBY=@USERID,ISDELETED=1  
WHERE COMMENTID=@COMMENTID  
  
  
--SELECT * FROM USERS  
--SELECT * FROM COMMENTS  
--SELECT * FROM PARAMETERSTYPESCODES  
--  
--UPDATE COMMENTS SET typecodeid=3  