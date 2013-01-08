using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.TributePortalAdmin.Views
{
    public interface ICategoryBasedThemeUpload
    {  
        string CategoryName { get; set; }
        string SubCategoryName { get; set; }
        IList<Themes> CategoryList {  set; }
        IList<Themes> SubCategoryList {  set; }
        IList<Themes> SubCategoryDeleteList { set; }
        IList<Themes> SubCategoryUpdateList { set; }
        IList<Themes> ThemeDeleteList { set; }
        IList<Themes> ThemeUpdateList { set; }
        string ThemeName { get;  }
        string ThemeValue { get; }
        string FolderName { get; set; }      
    }
}
