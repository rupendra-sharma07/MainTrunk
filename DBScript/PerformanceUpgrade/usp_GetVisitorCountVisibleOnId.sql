CREATE PROCEDURE [dbo].[usp_GetVisitorCountVisibleOnId]

	-- Input Parameters for the stored procedure 
	@TributeId	int
AS

BEGIN
	SELECT dbo.USERS.IsVisitCountHide
	FROM dbo.USERS,dbo.TRIBUTES
	WHERE dbo.USERS.IsActive = 1 
and dbo.TRIBUTES.TributeId =  @TributeId
and dbo.TRIBUTES.UserTributeId = dbo.USERS.UserId
END





-- usp_GetVisitorCountVisibleOnId 20908