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
			INSERT INTO [dbo].[LinkVdeoMemorialTribute]
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

