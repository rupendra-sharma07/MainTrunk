set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[usp_CreateThemesOnCategory]
(
 @ThemeName   varchar(100),  
 @Tributetype varchar(100),  
 @ThemeValue varchar(200), 
 @IsActive bit, 
 @SubCategory  varchar(250),  
 @FolderName  varchar(250)
 
)
AS   
BEGIN  
INSERT INTO [dbo].[THEMES]
           ([ThemeName]           
           ,[ThemePath]
		   ,[Tributetype]             
           ,[ThemeValue]
		   ,[IsActive]           
           ,[SubCategory]
           ,[FolderName])
     VALUES
           (@ThemeName 
			,'http://172.26.176.214/DevelopmentWebsite/assets/images/Default Anniversary.JPG'          
           ,@Tributetype
		   ,@ThemeValue
           ,@IsActive
           ,@SubCategory
           ,@FolderName)

SELECT @@Identity  
 END 
