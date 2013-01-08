///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Photo.Views.ManagePhotoPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for managing photos.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
#endregion

namespace TributesPortal.Photo.Views
{
    public class ManagePhotoPresenter : Presenter<IManagePhoto>
    {
        #region CLASS VARIABLES
        private PhotoController _controller;
        #endregion

        #region CONSTRUCTOR
        public ManagePhotoPresenter([CreateNew] PhotoController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Method to get details of photo.
        /// </summary>
        public void GetPhotoDetails()
        {
            string[] getPath = CommonUtilities.GetPath();
            Photos objPhoto = this._controller.GetPhotoDetail(GetPhotoObject());
            objPhoto.PhotoDesc = objPhoto.PhotoDesc; //.Replace("\n", "</br>");
            //objPhoto.PhotoImage = getPath[2] + "/" + this.View.TributeName.Replace(" ", "_") + "_" + this.View.TributeType.Replace(" ", "_") + "/" + objPhoto.PhotoImage;
            objPhoto.PhotoImage = getPath[2] + "/" + this.View.TributeUrl.Replace(" ", "_") + "_" + this.View.TributeType.Replace(" ", "_") + "/" + objPhoto.PhotoImage;
            this.View.PhotoDetails = objPhoto;
        }

        /// <summary>
        /// Method to update photo
        /// </summary>
        public void UpdatePhoto()
        {
            this._controller.UpdatePhoto(GetPhotoToUpdate());
        }

        /// <summary>
        /// Method to delete photo
        /// </summary>
        public void DeletePhoto()
        {
            this._controller.DeletePhoto(GetPhotoToDelete());
        }

        /// <summary>
        /// Method to get object with photo id to get the details of photo for that photo id.
        /// </summary>
        /// <returns>Photos entity containing photo id.</returns>
        public Photos GetPhotoObject()
        {
            Photos objPhoto = new Photos();
            objPhoto.PhotoId = this.View.PhotoId;
            return objPhoto;
        }

        /// <summary>
        /// Method to get object to be updated
        /// </summary>
        /// <returns>Filled photos entity</returns>
        private Photos GetPhotoToUpdate()
        {
            Photos objPhotoUpdate = new Photos();
            objPhotoUpdate.PhotoId = this.View.PhotoId;
            objPhotoUpdate.PhotoCaption = this.View.PhotoCaption;
            objPhotoUpdate.PhotoDesc = this.View.PhotoDesc;
            objPhotoUpdate.UserId = this.View.UserId;
            objPhotoUpdate.ModifiedBy = this.View.UserId;
            objPhotoUpdate.ModifiedDate = DateTime.Now;

            return objPhotoUpdate;
        }

        /// <summary>
        /// Method to get object for delettion
        /// </summary>
        /// <returns>Filled photos entity</returns>
        private Photos GetPhotoToDelete()
        {
            Photos objDeletePhoto = new Photos();
            objDeletePhoto.PhotoId = this.View.PhotoId;
            objDeletePhoto.UserId = this.View.UserId;
            objDeletePhoto.IsDeleted = true;
            objDeletePhoto.ModifiedDate = DateTime.Now;
            return objDeletePhoto;
        }

        
        #endregion
    }
}




