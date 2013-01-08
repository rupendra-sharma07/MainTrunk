create  procedure usp_CreateThemesOnCategory
(
@EventThemeName   varchar(50),  
 @ThemeCategory varchar(100),  
 @ThemeValue varchar(100),  
 @SubCategory  varchar(250),  
 @FolderName  varchar(250)
 
)
AS  
  
BEGIN  
INSERT INTO [DotComDB01072010].[dbo].[THEMES]
           ([ThemeName]
           
           ,[Tributetype]
           
           ,[ThemeValue]
           
           ,[SubCategory]
           ,[FolderName])
     VALUES
           (@ThemeName 
           
           ,@ThemeCategory
			@ThemeValue

           ,@SubCategory
           ,@FolderName
)
SELECT @@Identity  
 END 