set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[usp_GetSubCategoryForTributeType]
(
@TributeType varchar(50)
)
AS

BEGIN

	SET NOCOUNT ON;		
		SELECT TributeType,
				SubCategory 
		FROM themes
		WHERE TributeType=@TributeType
		GROUP BY TributeType,SubCategory
		ORDER BY TributeType,SubCategory
END