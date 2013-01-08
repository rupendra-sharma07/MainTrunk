CREATE PROCEDURE [dbo].[usp_GetVisitorCountVisible]

	-- Input Parameters for the stored procedure 
	@TributeUrl	varchar(max)
AS

BEGIN
	SELECT dbo.USERS.IsVisitCountHide
	FROM dbo.USERS,dbo.TRIBUTES
	WHERE dbo.USERS.IsActive = 1 
and dbo.TRIBUTES.TributeUrl =  @TributeUrl 
and dbo.TRIBUTES.UserTributeId = dbo.USERS.UserId
END





-- usp_GetVisitorCountVisible 'Stella-Archuleta'