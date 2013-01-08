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