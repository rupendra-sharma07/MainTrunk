///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Photo.Views.ManagePhotoAlbumPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for managing photo album.
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
    public class ManagePhotoAlbumPresenter : Presenter<IManagePhotoAlbum>
    {
        #region CLASS VARIABLES
        private PhotoController _controller;
        #endregion

        #region CONSTRUCTOR
        public ManagePhotoAlbumPresenter([CreateNew] PhotoController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Method to get the count for number of photos in the selected album 
        /// </summary>
        public void GetPhotoCount()
        {
            PhotoAlbum objPhotoAlbum = new PhotoAlbum();
            objPhotoAlbum.PhotoAlbumId = this.View.PhotoAlbumId;
            this.View.PhotoCount = (int)_controller.GetPhotoCount(objPhotoAlbum);
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

        public void CreateAlbum()
        {
            PhotoAlbum objPhotoAlbum = this.View.PhotoAlbumDetails;

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

            else
            {
                if (objPhotoAlbum.PhotoAlbumId == 0) //when user is creating a new album
                {
                    //result = _controller.CreateAlbumAndSavePhotos(objPhotoAlbum, objListPhotos);

                    result = _controller.CreatePhotoAlbum(objPhotoAlbum);
                    this.View.Result = result;
                    //Send mail for success creation of new album
                    if (result > 0)
                    {
                        objPhotoAlbum.PhotoAlbumId = result;
                        _controller.SendPhotoAlbumEmail(objPhotoAlbum);
                    }
                    // it is a new PhotoAlbum, so let's pass it back to the view
                    this.View.PhotoAlbumID = result;

                }
            }
        }


        

        public void SaveImageList(Photos objPhotos, bool sendAddPhotosEmail)
        {
            int result = 0;
            if (this.View.PhotoAlbumID > 0)
            {
                result = _controller.SavePhotos(objPhotos);
                this.View.Result = result;

                if (result > 0)
                    //store the last photo ID
                    this.View.UploadedPhotoID = result;
                objPhotos.PhotoId = result;
                if ((sendAddPhotosEmail) && (result > 0))
                {
                    IList<Photos> objLPhoto = new List<Photos>();
                    objLPhoto.Add(objPhotos);
                    _controller.SendPhotoEmail(objLPhoto);
                }
            }

        }

        

        public Tributes GetTributeSessionForUrlAndType(string tributeUrl, string tributeType)
        {
            Tributes objTribute = new Tributes();
            objTribute = _controller.GetTributeSessionForUrlAndType(GetTributeObject(tributeUrl, tributeType));
            return objTribute;
        }
        public Tributes GetTributeSessionForUrlAndType(string tributeUrl)
        {
            Tributes objTribute = new Tributes();
            objTribute = _controller.GetTributeSessionForUrlAndType(GetTributeObject(tributeUrl, null));
            return objTribute;
        }

        public Tributes GetTributeObject(string tributeUrl, string tributeType)
        {
            Tributes objTribute = new Tributes();
            objTribute.TributeUrl = tributeUrl;
            objTribute.TypeDescription = tributeType;
            return objTribute;
        }
      
        /// <summary>
        /// Method to get the photo album details.
        /// </summary>
        public string  GetPhotoAlbumDetails(Photos objPhotoAlbumId)
        {
            PhotoAlbum objPhotoalbum = new PhotoAlbum();
            objPhotoalbum = this._controller.GetPhotoAlbumDetail(objPhotoAlbumId);
            if (objPhotoalbum != null)
                return objPhotoalbum.PhotoAlbumCaption;
            else
               return string.Empty;
        }

       
       
        #endregion
    }
}




