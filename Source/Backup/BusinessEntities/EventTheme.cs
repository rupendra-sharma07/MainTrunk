///Copyright       : Copyright (c) Optimus Information Inc. 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.EventTheme.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the entity to work with the events object
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

#endregion

/// <summary>
///Tribute Portal-Events Entity Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the EventTheme Entity class for the Event. it contain all the themes.
/// </summary>
/// 

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class EventTheme
    {
        #region ENUM

        public enum EventThemeEnum
        {
            EventThemeID,
            EventThemeName,
            InvitationCategoryID,
            ThemeThumbnailImage,
            ThemePreviewImage,
            ThemeFullSizeImage,
            ThemeBackgroundColor,
            SubCategory,
            CategoryName,
            ThemeValue,
            FolderName
        }
        
        #endregion

        #region FIELDS

        private int _EventThemeID;
        private string _EventThemeName;
        private int _InvitationCategoryID;
        private string _ThemeThumbnailImage;
        private string _ThemePreviewImage;
        private string _ThemeFullSizeImage;
        private string _ThemeBackgroundColor;
        private string _SubCategory;
        private string _Category;
        private string _ThemeValue;
        private string _ThemeName;
        #endregion

        #region PROPERTIES

        public int EventThemeID
        {
            get { return _EventThemeID; }
            set { _EventThemeID = value; }
        }
        public string EventThemeName
        {
            get { return _EventThemeName; }
            set { _EventThemeName = value; }
        }
        public int InvitationCategoryID
        {
            get { return _InvitationCategoryID; }
            set { _InvitationCategoryID = value; }
        }
        public string ThemeThumbnailImage
        {
            get { return _ThemeThumbnailImage; }
            set { _ThemeThumbnailImage = value; }
        }
        public string ThemePreviewImage
        {
            get { return _ThemePreviewImage; }
            set { _ThemePreviewImage = value; }
        }
        public string ThemeFullSizeImage
        {
            get { return _ThemeFullSizeImage; }
            set { _ThemeFullSizeImage = value; }
        }
        public string ThemeBackgroundColor
        {
            get { return _ThemeBackgroundColor; }
            set { _ThemeBackgroundColor = value; }
        }

        public string SubCategory
        {
            get { return _SubCategory; }
            set { _SubCategory = value; }
        }
        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }
        public string ThemeValue
        {
            get { return _ThemeValue; }
            set { _ThemeValue = value; }
        }
        public string ThemeName
        {
            get { return _ThemeName; }
            set { _ThemeName = value; }
        }
        #endregion

        #region CONSTRUCTOR

        /// <summary>
	    /// Default Contructor
	    /// <summary>
	    public EventTheme()
	    {}

        /// <summary>
	    /// User defined Contructor
	    /// <summary>
        public EventTheme(int EventThemeID,
            string EventThemeName,
            int InvitationCategoryID,
            string ThemeThumbnailImage,
            string ThemePreviewImage,
            string ThemeFullSizeImage)
	    {
            this._EventThemeID = EventThemeID;
            this._EventThemeName = EventThemeName;
            this._InvitationCategoryID = InvitationCategoryID;
            this._ThemeThumbnailImage = ThemeThumbnailImage;
            this._ThemePreviewImage = ThemePreviewImage;
            this._ThemeFullSizeImage = ThemeFullSizeImage;
            this._ThemeValue = ThemeValue;
            this.Category = Category;
            this.SubCategory = SubCategory;
            this.ThemeName = ThemeName;

	    }//end user defined constructor

        #endregion
    
    }//end namespace
}//end class
