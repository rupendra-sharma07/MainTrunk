///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Photo.Views.PhotoUploadPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for uploading a photo.
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
    public class PhotoUploadPresenter : Presenter<IPhotoUpload>
    {
        #region CLASS VARIABLES
        private PhotoController _controller;
        #endregion

        #region CONSTRUCTOR
        public PhotoUploadPresenter([CreateNew] PhotoController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS

        public void CreateAlbumAndSavePhoto()
        {
            PhotoAlbum objPhotoAlbum = this.View.PhotoAlbumDetails;
            List<Photos> objListPhotos = this.View.PhotoList;
            int result;
            //empty photo album name
            if (string.IsNullOrEmpty(objPhotoAlbum.PhotoAlbumCaption))
            {
                this.View.Result = -2;
                this.View.PhotoAlbumID = 0;
            }
            //length of photo album name
            if (!string.IsNullOrEmpty(objPhotoAlbum.PhotoAlbumCaption) && objPhotoAlbum.PhotoAlbumCaption.Length > 100)
            {
                this.View.Result = -3;
                this.View.PhotoAlbumID = 0;
            }
            //length of photo album description
            if (!string.IsNullOrEmpty(objPhotoAlbum.PhotoAlbumDesc) && objPhotoAlbum.PhotoAlbumDesc.Length > 1000)
            {
                this.View.Result = -4;
                this.View.PhotoAlbumID = 0;
            }
            //check if the PhotoCount is not > 60
            else if (objPhotoAlbum.PhotoCount + objListPhotos.Count > Convert.ToInt32(Utilities.WebConfig.MaxPhotosInAlbum_PhotoAlbum))
            {
                this.View.Result = -5;
                this.View.PhotoAlbumID = 0;
            }
            else
            {
                if (objPhotoAlbum.PhotoAlbumId == 0) //when user is creating a new album
                {
                    if (objPhotoAlbum.PhotoCount == 0)
                    {
                        result = _controller.CreateAlbumAndSavePhotos(objPhotoAlbum, objListPhotos);
                        this.View.Result = result;
                        // it is a new PhotoAlbum, so let's pass it back to the view
                        this.View.PhotoAlbumID = result;
                    }
                }
                else //when user is adding photos to an existing album
                {
                    if (this.View.PhotoAlbumID > 0)
                    {
                        foreach (Photos obj in objListPhotos)
                        {
                            obj.PhotoAlbumId = objPhotoAlbum.PhotoAlbumId;
                        }
                        result = _controller.SavePhotos(objListPhotos);
                        this.View.Result = result;

                        if (result > 0)
                            //store the last photo ID
                            this.View.UploadedPhotoID = result;
                    }
                }
            }

            // no errors so far
            if (this.View.Result > 0)
            {
                try
                {
                    // we can save the files on the server
                    this.View.SaveFilesOnStorage(objListPhotos);
                    // increase UploadedPhotoCount 
                    this.View.UploadedPhotoCount = this.View.UploadedPhotoCount + 1;

                    //send appropriate emails
                    //this is an existing photo album; send an email for the first image added 
                    // TO DO : how send email for the last image in the batch of photos uploaded?
                    if (this.View.ExistingPhotoCount != 0 && this.View.UploadedPhotoCount == 1)
                    {
                        // retrieve the updated photo list
                        objListPhotos = this.View.PhotoList;
                        _controller.SendPhotoEmail(objListPhotos);
                    }
                    // this is a new photo album; send an email for the first image added
                    if (this.View.ExistingPhotoCount == 0 && this.View.UploadedPhotoCount == 1)
                    {
                        // retrieve the updated album                       
                        objPhotoAlbum = this.View.PhotoAlbumDetails;
                        _controller.SendPhotoAlbumEmail(objPhotoAlbum);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

    }
}




