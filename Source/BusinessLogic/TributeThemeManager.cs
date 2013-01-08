///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.UserManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the functions required to manage themes
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;
#endregion

namespace TributesPortal.BusinessLogic
{
    public partial class TributeThemeManager
    {
        /// <summary>
        /// Method to get the list of themes for a tribute
        /// </summary>
        /// <param name="objTributeType">Type of Tribute</param>
        public List<Templates> GetThemes(Templates objTributeType)
        {
            TemplateResource objTheme = new TemplateResource();
            return objTheme.GetTemplates(objTributeType.TributeType);
        }

        public List<Templates> GetThemesFolder(Templates objTributeType, string applicationType)
        {
            TemplateResource objTheme = new TemplateResource();
            return objTheme.GetThemesTemplates(objTributeType.TributeType, applicationType);
        }

        /// <summary>
        /// Method to update the tribute theme
        /// </summary>
        /// <param name="objTheme">Tribute entity containing Tribute Id, Theme Id</param>
        public void UpdateTributeTheme(Tributes objTheme)
        {
            TemplateResource objTemplate = new TemplateResource();
            object[] param = { objTheme };
            objTemplate.UpdateTributeTheme(param);
        }

        /// <summary>
        /// Method to get the theme for the tribute
        /// </summary>
        /// <param name="objTheme">Template Object</param>
        /// <returns></returns>
        public Templates GetThemeForTribute(Tributes objTheme)
        {
            TemplateResource objTemplate = new TemplateResource();
            object[] param = { objTheme };
            return objTemplate.GetThemeForTribute(param);
        }

        public Templates GetThemeFolderForTribute(Tributes objTheme)
        {
            TemplateResource objTemplate = new TemplateResource();
            object[] param = { objTheme };
            return objTemplate.GetThemeFolderForTribute(param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objTheme"></param>
        /// <returns></returns>
        public Templates GetExistingFolderName(Tributes objTheme)
        {
            TemplateResource objTemplate = new TemplateResource();
            object[] param = { objTheme };
            return objTemplate.GetExistingFolderName(param);
        }

        
    }
}
