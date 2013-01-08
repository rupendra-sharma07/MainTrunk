///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Themes.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about themes
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class Themes
    {

        public enum ThemesEnum
        {
            ThemeId,
            ThemeName,
            ThemePath,
            Tributetype,
            ThemeCssClass,
            ThemeValue,
            IsActive,
            SubCategory,
            FolderName,
            ApplicationType
        }

        /// <summary>
        /// Default Contructor
        /// <summary>
        public Themes()
        { }


        public int ThemeId
        {
            get { return _ThemeId; }
            set { _ThemeId = value; }
        }
        private int _ThemeId;


        public string ThemeName
        {
            get { return _ThemeName; }
            set { _ThemeName = value; }
        }
        private string _ThemeName;


        public string ThemePath
        {
            get { return _ThemePath; }
            set { _ThemePath = value; }
        }
        private string _ThemePath;


        public string Tributetype
        {
            get { return _Tributetype; }
            set { _Tributetype = value; }
        }
        private string _Tributetype;

    
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

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        private bool _IsActive;

        private string _SubCategory;
        public string SubCategory
        {
            get { return _SubCategory; }
            set { _SubCategory = value; }
        }

        private string _FolderName;
        public string FolderName
        {
            get { return _FolderName; }
            set { _FolderName = value; }
        }
        private string _applicationType;
        public string ApplicationType
        {
            get { return _applicationType; }
            set { _applicationType = value; }
        }

        /// <summary>
        /// User defined Contructor
        /// <summary>
        public Themes(int ThemeId,
            string ThemeName,
            string ThemePath,
            string Tributetype,
            bool IsActive)
        {
            this._ThemeId = ThemeId;
            this._ThemeName = ThemeName;
            this._ThemePath = ThemePath;
            this._Tributetype = Tributetype;
            this._IsActive = IsActive;
        }

        public Themes(int ThemeId, string ThemeName, string ThemePath, string Tributetype, string ThemeCssClass, string ThemeValue, string SubCategory, string FolderName)
        {
            this._ThemeId = ThemeId;
            this._ThemeName = ThemeName;
            this._ThemePath = ThemePath;
            this._Tributetype = Tributetype;
            this._themeCssClass = ThemeCssClass;
            this._themeValue = ThemeValue;
            this._SubCategory = SubCategory;
            this._FolderName = FolderName;

        }
        public Themes(int ThemeId, string ThemeName, string ThemePath, string Tributetype, string ThemeCssClass, string ThemeValue, string SubCategory, string FolderName,string ApplicationType)
        {
            this._ThemeId = ThemeId;
            this._ThemeName = ThemeName;
            this._ThemePath = ThemePath;
            this._Tributetype = Tributetype;
            this._themeCssClass = ThemeCssClass;
            this._themeValue = ThemeValue;
            this._SubCategory = SubCategory;
            this._FolderName = FolderName;
            this._applicationType = ApplicationType;

        }

        public Themes(int ThemeId, string ThemeName, string ThemePath, string Tributetype, string ThemeCssClass, string ThemeValue)
        {
            this._ThemeId = ThemeId;
            this._ThemeName = ThemeName;
            this._ThemePath = ThemePath;
            this._Tributetype = Tributetype;
            this._themeCssClass = ThemeCssClass;
            this._themeValue = ThemeValue;

        }

    }




}
