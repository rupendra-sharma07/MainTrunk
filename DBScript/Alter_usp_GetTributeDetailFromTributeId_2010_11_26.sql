set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[usp_GetTributeDetailFromTributeId] 
(
@TributeID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT	dbo.TRIBUTES.TributeId,			
		dbo.TRIBUTES.TributeName, 		
		dbo.TRIBUTES.TributeUrl,	
		dbo.TRIBUTES.TributeImage,		
        dbo.PARAMETERSTYPESCODES.TypeDescription,
		dbo.TRIBUTES.ThemeId,
		dbo.TRIBUTES.CreatedDate,
		dbo.TRIBUTES.Date2,
		dbo.TRIBUTES.GoogleAdSense,
		dbo.TRIBUTES.IsActive,
dbo.TRIBUTES.IsOrderDVDChecked,
dbo.TRIBUTES.IsMemTributeBoxChecked,
dbo.TRIBUTES.City, 	dbo.TRIBUTES.State, dbo.TRIBUTES.Country	
FROM    dbo.TRIBUTES ,dbo.PARAMETERSTYPESCODES			
WHERE  dbo.TRIBUTES.TributeType=dbo.PARAMETERSTYPESCODES.TypeCode
and  dbo.TRIBUTES.TributeId = @TributeID and dbo.PARAMETERSTYPESCODES.ParameterType = 'TRIBUTE_TYPE'
END
