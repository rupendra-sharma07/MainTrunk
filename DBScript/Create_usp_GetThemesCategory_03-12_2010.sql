

CREATE procedure [dbo].[usp_GetThemesCategory]        
        
   -- Input Parameters for the stored procedure       
 @IsActive  bit    
        
AS        
        
BEGIN       
        
select distinct TributeType as TributeType from Themes  where IsActive =@IsActive  
         
END 