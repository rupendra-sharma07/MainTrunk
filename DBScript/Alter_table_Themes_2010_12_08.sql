Alter table themes add SubCategory varchar(100) ,FolderName varchar(255);
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
