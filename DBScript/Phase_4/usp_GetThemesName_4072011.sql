Create procedure [dbo].[usp_GetThemesName]        
        
 (    
-- Input Parameters for the stored procedure         
 @CategoryName  varchar(150),   
 @SubCategoryName  varchar(150)      

       ) 
AS        
        
BEGIN       
        
select Themeid, ThemeName from Themes where TributeType=@CategoryName and   SubCategory= @SubCategoryName          
END 

