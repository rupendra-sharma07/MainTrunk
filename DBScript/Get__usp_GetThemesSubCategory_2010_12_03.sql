create procedure [dbo].[usp_GetThemesSubCategory]      
      
 (  
-- Input Parameters for the stored procedure       
 @CategoryName  varchar(150)    
)  
      
AS      
      
BEGIN     
      
select  distinct SubCategory as SubCategory from Themes where TributeType=@CategoryName  
       
END    