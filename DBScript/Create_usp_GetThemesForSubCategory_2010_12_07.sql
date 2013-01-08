set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetThemesForSubCategory] 
@ThemeName as varchar(250),
@SubCategory as varchar(250)
AS 
BEGIN
	if @SubCategory='All'
		SELECT * FROM THEMES where Tributetype=@ThemeName
	else
		SELECT * FROM THEMES where Tributetype=@ThemeName and Subcategory=@SubCategory
END

