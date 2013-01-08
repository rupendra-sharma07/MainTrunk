
Go
/********************************
dbo.CreditPackageMaster
*********************************/
INSERT INTO [CreditPackageMaster] ([credit_package_name],[credit_points],[created_date],[tribute_type],[deleted_date])VALUES('Life Time',3,'Oct 26 2010  1:53:09:217PM',7,NULL)
GO
INSERT INTO [CreditPackageMaster] ([credit_package_name],[credit_points],[created_date],[tribute_type],[deleted_date])VALUES('1 Year',1,'Oct 26 2010  1:53:09:217PM',7,NULL)
GO 
/********************************
dbo.CreditPackageCostMaster
*********************************/
INSERT INTO [CreditPackageCostMaster] ([CreditPoints],[CostPerCredit],[TributeType],[IsActive])VALUES(1,12.00,8,NULL)
GO
INSERT INTO [CreditPackageCostMaster] ([CreditPoints],[CostPerCredit],[TributeType],[IsActive])VALUES(10,11.40,8,NULL)
GO
INSERT INTO [CreditPackageCostMaster] ([CreditPoints],[CostPerCredit],[TributeType],[IsActive])VALUES(20,10.80,8,NULL)
GO
INSERT INTO [CreditPackageCostMaster] ([CreditPoints],[CostPerCredit],[TributeType],[IsActive])VALUES(50,9.60,8,NULL)
GO
INSERT INTO [CreditPackageCostMaster] ([CreditPoints],[CostPerCredit],[TributeType],[IsActive])VALUES(100,7.85,8,NULL)
GO
INSERT INTO [CreditPackageCostMaster] ([CreditPoints],[CostPerCredit],[TributeType],[IsActive])VALUES(250,6.00,8,NULL)
/********************************
dbo.Themes
*********************************/

INSERT INTO [Themes] ([ThemeName],[ThemePath],[Tributetype],[ThemeClassId],[ThemeValue],[IsActive])VALUES('Legacy','http://172.26.176.214/DevelopmentWebsite/assets/images/Default Video.JPG','Video','yt-MemorialDefault','MemorialDefault',1)
GO
INSERT INTO [Themes] ([ThemeName],[ThemePath],[Tributetype],[ThemeClassId],[ThemeValue],[IsActive])VALUES('Flowers','http://172.26.176.214/DevelopmentWebsite/assets/images/Default Video.JPG','Video','yt-MemorialFlowers','MemorialFlowers',1)
GO
INSERT INTO [Themes] ([ThemeName],[ThemePath],[Tributetype],[ThemeClassId],[ThemeValue],[IsActive])VALUES('Patriotic','http://172.26.176.214/DevelopmentWebsite/assets/images/Default Video.JPG','Video','yt-MemorialPatriotic','MemorialPatriotic',1)
GO
INSERT INTO [Themes] ([ThemeName],[ThemePath],[Tributetype],[ThemeClassId],[ThemeValue],[IsActive])VALUES('Religious','http://172.26.176.214/DevelopmentWebsite/assets/images/Default Video.JPG','Video','yt-MemorialReligious','MemorialReligious',1)
GO
INSERT INTO [Themes] ([ThemeName],[ThemePath],[Tributetype],[ThemeClassId],[ThemeValue],[IsActive])VALUES('Celestial','http://172.26.176.214/DevelopmentWebsite/assets/images/Default Video.JPG','Video','yt-MemorialReligiousII','MemorialReligiousII',1)
GO
INSERT INTO [Themes] ([ThemeName],[ThemePath],[Tributetype],[ThemeClassId],[ThemeValue],[IsActive])VALUES('Sky','http://172.26.176.214/DevelopmentWebsite/assets/images/Default Video.JPG','Video','yt-MemorialSky','MemorialSky',1);
GO
/********************************
dbo.PARAMETERSTYPESCODES
*********************************/

INSERT INTO [PARAMETERSTYPESCODES] ([ParameterType],[TypeCode],[TypeDescription],[IsActive])VALUES('TRIBUTE_TYPE',8,'Video',1)

GO
/********************************
dbo.Themes
*********************************/
GO
update themes set subcategory='VideoDefault' where tributetype='Video';
GO
update themes set foldername=tributetype +'_'+subcategory+'_'+themevalue where tributetype='Video';
GO
update themes set subcategory='MemorialDefault' where tributetype='memorial';
GO
update themes set foldername='Mem_'+subcategory+'_'+themevalue where tributetype='memorial';
GO
update themes set subcategory='AnniversaryDefault' where tributetype='anniversary';
GO
update themes set foldername='Ann_'+subcategory+'_'+themevalue where tributetype='anniversary';
GO
update themes set subcategory='BirthdayDefault' where tributetype='birthday';
GO
update themes set foldername='Birth_'+subcategory+'_'+themevalue where tributetype='birthday';
GO
update themes set subcategory='GraduationDefault' where tributetype='graduation';
GO
update themes set foldername='Grad_'+subcategory+'_'+themevalue where tributetype='graduation';
GO
update themes set subcategory='BabyDefault' where tributetype='new baby';
GO
update themes set foldername='Baby_'+subcategory+'_'+themevalue where tributetype='new baby';
GO
update themes set subcategory='WeddingDefault' where tributetype='wedding';
GO
update themes set foldername='Wed_'+subcategory+'_'+themevalue where tributetype='wedding';
GO


