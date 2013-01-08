set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO



ALTER Procedure [dbo].[usp_GetTributeUrlOnOldTributeUrl]
@TributeUrl varchar(200),
@TributeType varchar(100)
--SP to get the tribute details based on the TributeUrl and TributeType
--exec usp_GetTributeDetails '22', 'Graduation'
AS
BEGIN
  IF @TributeType='NewBaby' SET @TributeType='New Baby'
  
  DECLARE @cnt int
  select @cnt = count(*) 
  from TRIBUTES
  where TributeUrl = @TributeUrl AND IsDeleted = 0
    And TributeType in 
      (Select TypeCode 
       from PARAMETERSTYPESCODES 
       where ParameterType = 'TRIBUTE_TYPE' And TypeDescription = @TributeType)

  IF @cnt = 1 
    SELECT TributeURL,TypeDescription from Tributes,PARAMETERSTYPESCODES where OldTributeURL=@TributeURL
and  ParameterType='TRIBUTE_TYPE' and TypeCode=TributeType and TypeDescription = @TributeType
    and IsDeleted=0
  ELSE 
    -- then try to find by TributeUrl only
    BEGIN
      SELECT TributeURL,TypeDescription from Tributes,PARAMETERSTYPESCODES where OldTributeURL=@TributeURL
and  ParameterType='TRIBUTE_TYPE' and TypeCode=TributeType
    and IsDeleted=0
    END
END
