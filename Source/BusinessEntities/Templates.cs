///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Templates.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about Templates
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
   public partial class Templates
    {
       public Templates()
       {

       }
       private int _templateID;

       public int TemplateID
       {
           get { return _templateID; }
           set { _templateID = value; }
       }
       private string _templateName;

       public string TemplateName
       {
           get { return _templateName; }
           set { _templateName = value; }
       }
       private string _templatePath;

       public string TemplatePath
       {
           get { return _templatePath; }
           set { _templatePath = value; }
       }

       //To get the themes based on tribute type
       //Added by: Gaurav Puri 
       private string _tributeType; 
       public string TributeType
       {
           get { return _tributeType; }
           set { _tributeType = value; }
       }

       private string _themeCssClass;
       public string ThemeCssClass
       {
           get { return _themeCssClass; }
           set { _themeCssClass = value; }
       }

       private string _themeValue;
       public string ThemeValue
       {
           get { return _themeValue; }
           set { _themeValue = value; }
       }

       private string _FolderName;
       public string FolderName
       {
           get { return _FolderName; }
           set { _FolderName = value; }
       }

    }//end class
}//end namespace
