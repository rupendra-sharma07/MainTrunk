///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Photo.Views.EditPhotoAlbumPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for editing a photo album.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Photo.Views
{
    public class EditPhotoAlbumPresenter : Presenter<IEditPhotoAlbum>
    {
        #region CLASS VARIABLES
        private PhotoController _controller;
        #endregion

        #region CONSTRUCTOR
        public EditPhotoAlbumPresenter([CreateNew] PhotoController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Method to get the photo album details.
        /// </summary>
        public void GetPhotoAlbumDetails()
        {
            this.View.PhotoAlbumDetails = this._controller.GetPhotoAlbumDetail(GetPhotoObject());
        }

        /// <summary>
        /// Method to update photo album details
        /// </summary>
        public string UpdatePhotoAlbumDetails()
        {
            object objReturn = this._controller.UpdatePhotoAlbum(GetPhotoAlbumToUpdate());
            if (!Equals(objReturn, null))
            {
                Errors objError = (Errors)objReturn;
                if (objError.ErrorMessage == "Failure")
                {
                    return objError.ErrorMessage;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Method to delete photo album.
        /// </summary>
        public void DeletePhotoAlbum()
        {
            this._controller.DeletePhotoAlbum(GetPhotoAlbumToDelete());
        }
        /// <summary>
        /// Method to get object with photo album id to get the details of photo album.
        /// </summary>
        /// <returns>PhotoAlbum entity containing photoalbumid.</returns>
        public Photos GetPhotoObject()
        {
            Photos objPhotoAlbum = new Photos();
            objPhotoAlbum.PhotoAlbumId = this.View.PhotoAlbumId;
            return objPhotoAlbum;
        }

        /// <summary>
        /// Method to get the list of photo albums in tribute.
        /// </summary>
        public void GetPhotoAlbumList()
        {
            PhotoAlbum objAlbum = new PhotoAlbum();
            objAlbum.UserTributeId = this.View.TributeId;
            List<PhotoAlbum> objList = this._controller.GetPhotoAlbumList(objAlbum);
            StringBuilder sb = new StringBuilder();
            foreach (PhotoAlbum obj in objList)
            {
                sb.Append(obj.PhotoAlbumCaption);
                sb.Append(";");
            }
            this.View.PhotoAlbumList = sb.ToString();
        }

        /// <summary>
        /// Method to get object to be updated
        /// </summary>
        /// <returns>Filled PhotoAlbum entity</returns>
        private PhotoAlbum GetPhotoAlbumToUpdate()
        {
            PhotoAlbum objPhotoAlbumUpdate = new PhotoAlbum();
            objPhotoAlbumUpdate.PhotoAlbumId = this.View.PhotoAlbumId;
            objPhotoAlbumUpdate.PhotoAlbumCaption = this.View.PhotoAlbumCaption;
            objPhotoAlbumUpdate.PhotoAlbumDesc = this.View.PhotoAlbumDesc;
            objPhotoAlbumUpdate.UserId = this.View.UserId;
            objPhotoAlbumUpdate.ModifiedBy = this.View.UserId;
            objPhotoAlbumUpdate.ModifiedDate = DateTime.Now;

            return objPhotoAlbumUpdate;
        }

        /// <summary>
        /// Method to get object for delettion
        /// </summary>
        /// <returns>Filled PhotoAlbum entity</returns>
        private PhotoAlbum GetPhotoAlbumToDelete()
        {
            PhotoAlbum objDeletePhotoAlbum = new PhotoAlbum();
            objDeletePhotoAlbum.PhotoAlbumId = this.View.PhotoAlbumId;
            objDeletePhotoAlbum.UserId = this.View.UserId;
            objDeletePhotoAlbum.IsDeleted = true;
            return objDeletePhotoAlbum;
        }
        #endregion
    }
}




