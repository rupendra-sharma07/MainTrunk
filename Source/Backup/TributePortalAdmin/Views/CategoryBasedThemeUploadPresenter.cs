#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.TributePortalAdmin.Views;

#endregion

namespace TributesPortal.TributePortalAdmin.Views
{
    public class CategoryBasedThemeUploadPresenter : Presenter<ICategoryBasedThemeUpload>
    {
        #region CLASS VARIABLES
        private TributePortalAdminController _controller;
        #endregion

        #region CONSTRUCTOR
        public CategoryBasedThemeUploadPresenter([CreateNew] TributePortalAdminController controller)
        {
            _controller = controller;
        }
        #endregion

        #region Methods
        /// <summary>
        /// To Get Category list 
        /// </summary>
        public void LoadCategory(string applicationType)
        {
            View.CategoryList = _controller.GetCategoryList(applicationType);
        }
        /// <summary>
        /// To get Sub Category List based on Category name
        /// </summary>
        /// <param name="strCategoryName"></param>

        public void LoadSubCategory(string strCategoryName)
        {
            View.SubCategoryList = _controller.GetSubCategoryList(strCategoryName);
        }

        /// <summary>
        // Get Sub Categories for Delete theme in Admin portal By Ashu
        /// </summary>
        /// <param name="strCategoryName"></param>
        public void LoadDeleteSubCategory(string strCategoryName)
        {
            View.SubCategoryDeleteList = _controller.GetSubCategoryList(strCategoryName);
        }
        /// <summary>
        /// Get Sub Categories for Update theme in Admin portal By Ashu
        /// </summary>
        /// <param name="strCategoryName"></param>
        public void LoadUpdateSubCategory(string strCategoryName)
        {
            View.SubCategoryUpdateList = _controller.GetSubCategoryList(strCategoryName);
        }
        /// <summary>
        ///  Get Themes for  delete theme in Admin portal By Ashu
        /// </summary>
        /// <param name="strCategoryName"></param>
        /// <param name="strSubCategoryName"></param>
        public void LoadThemes(string strCategoryName, string strSubCategoryName, string applicationType)
        {
            View.ThemeDeleteList = _controller.GetThemesList(strCategoryName, strSubCategoryName, applicationType);

        }
        /// <summary>
        /// Get Themes for Update theme in Admin portal By Ashu
        /// </summary>
        /// <param name="strCategoryName"></param>
        /// <param name="strSubCategoryName"></param>
        public void LoadUpdateThemes(string strCategoryName, string strSubCategoryName, string applicationType)
        {
            View.ThemeUpdateList = _controller.GetThemesList(strCategoryName, strSubCategoryName, applicationType);

        }
        /// <summary>
        ///  Get Folder Name for Update and delete theme in Admin portal By Ashu
        /// </summary>
        /// <param name="themeId"></param>
        public void GetThemeFolderName(int themeId)
        {
            View.FolderName = _controller.GetFoldername(themeId);

        }
        /// <summary>
        /// To delete theme from database By Ashu
        /// </summary>
        /// <param name="themeId"></param>
        public void DeleteTheme(int themeId)
        {
            _controller.DeleteBasedTheme(themeId);
        }

        public void SaveTheme(string applicationType)
        {
            try
            {
                Themes objThemes = CreateThemesObject(applicationType);
                _controller.SaveCategoryBasedTheme(objThemes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Themes CreateThemesObject(string applicationType)
        {
            Themes objTheme = new Themes();

            objTheme.Tributetype = View.CategoryName;
            objTheme.SubCategory = View.SubCategoryName;
            objTheme.ThemeName = View.ThemeName;
            objTheme.ThemeValue = View.ThemeValue;
            objTheme.IsActive = true;
            objTheme.FolderName = View.FolderName;
            objTheme.ApplicationType = applicationType;
            return objTheme;
        }

        #endregion

    }


}
