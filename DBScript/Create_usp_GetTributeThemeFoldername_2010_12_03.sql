CREATE Procedure [dbo].[usp_GetTributeThemeFoldername]
@TributeId int
AS
BEGIN
	--Stored Procedure to get the existing theme id for the selected tribute
	Select Tributes.ThemeId, ThemeName, ThemeValue,FolderName from Tributes 
			Inner Join Themes ON Themes.ThemeId = Tributes.ThemeId
			where Tributes.TributeId = @TributeId and Tributes.IsDeleted = 0
END