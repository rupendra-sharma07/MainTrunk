///Copyright       : Copyright (c) Optimus Information Inc. 
///Project         : Timeless Tributes
///File Name       : TributesPortal.TributePortalAdmin.Views.AdminThemeUploadPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for viewing the summary reports by the portal admin.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.TributePortalAdmin.Views
{
    public class AdminThemeUploadPresenter : Presenter<IAdminThemeUpload>
    {
        #region CLASS VARIABLES
        private TributePortalAdminController _controller;
        #endregion

        #region CONSTRUCTOR
        public AdminThemeUploadPresenter([CreateNew] TributePortalAdminController controller)
        {
            _controller = controller;
        }
        #endregion

        public override void OnViewLoaded()
        {
            // TODO: Implement code that will be executed every time the view loads
        }

        public override void OnViewInitialized()
        {
            // TODO: Implement code that will be executed the first time the view loads
        }

        public void LoadInvitationCategory()
        {
            View.EventInvitationCategoryList = _controller.GetEventInvitationCategoryList(View.TributeType);
        }

        public void SaveInvitationCategory()
        {
            try
            {
                EventInvitationCategory objEventInvitationCategory = CreateEventInvitationCategoryObject();
                _controller.SaveInvitationCategory(objEventInvitationCategory);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private EventInvitationCategory CreateEventInvitationCategoryObject()
        {
            EventInvitationCategory objEventInvitationCategory = new EventInvitationCategory();

            objEventInvitationCategory.InvitationCategoryName = View.InvitationCategoryName;
            objEventInvitationCategory.TributeType = View.TributeTypeID;

            return objEventInvitationCategory;
        }

        public void SaveEventTheme()
        {
            try
            {
                EventTheme objEventTheme = CreateEventThemeObject();
                _controller.SaveEventTheme(objEventTheme);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private EventTheme CreateEventThemeObject()
        {
            EventTheme objEventTheme = new EventTheme();

            objEventTheme.EventThemeName = View.EventThemeName;
            objEventTheme.InvitationCategoryID = View.InvitationCategoryID;
            objEventTheme.ThemeThumbnailImage = View.ThumbnailImagePath;
            objEventTheme.ThemePreviewImage = View.PreviewImagePath;
            objEventTheme.ThemeFullSizeImage = View.FullSizeImagePath;
            objEventTheme.ThemeBackgroundColor = View.ThemeBackgroundColor;

            return objEventTheme;
        }
        public void Deletecategory(int rowId)
        {
            _controller.Deletecategory(rowId);
        }
        public void DeleteTheme(int rowId)

        {
            _controller.DeleteTheme(rowId);
        }
        public IList<EventInvitationCategory> GetEventInvitationCategories(string TributeType)
        {
            return _controller.GetEventInvitationCategoryList(View.TributeType);
        }

        public IList<EventTheme> GetEventThemeInfo(int invitationCategory, string tributeType)
        {
            return _controller.GetEventThemeInfo(invitationCategory, tributeType);
        }  
    }//end class
}//end namespace
