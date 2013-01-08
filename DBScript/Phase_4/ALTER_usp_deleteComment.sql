set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
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
