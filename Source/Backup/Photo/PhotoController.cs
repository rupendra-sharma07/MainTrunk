//===============================================================================
// Microsoft patterns & practices
// Web Client Software Factory
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================


///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Photo.PhotoController.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Controller class for the files under Photos.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;
using TributesPortal.Utilities;
#endregion

namespace TributesPortal.Photo
{
    public class PhotoController
    {
        #region CONSTRUCTOR
        public PhotoController()
        {
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Method to create photo album and add photos to it. 
        /// </summary>
        /// <param name="objPhotoAlbum">Filled PhotoAlbum entity.</param>
        /// <param name="objPhotoList">Filled list of Photos.</param>
        public int CreateAlbumAndSavePhotos(PhotoAlbum objPhotoAlbum, List<Photos> objPhotoList)
        {
            return FacadeManager.PhotoManager.CreatePhotoAlbum(objPhotoAlbum, objPhotoList);
        }

        /// <summary>
        /// Method to add photos (called in case of adding more photos to album)
        /// </summary>
        /// <param name="objPhotoList">list of filled photos entity.</param>
        public int SavePhotos(List<Photos> objPhotoList)
        {
           return FacadeManager.PhotoManager.AddPhotos(objPhotoList);
        }

        /// <summary>
        /// Method to get the list of photo albums for the tribute.
        /// </summary>
        /// <param name="objPhotoAlbum">PhotoAlbum entity containing tribute id, page number and page size.</param>
        /// <returns>List of photo albums.</returns>
        public List<PhotoAlbum> GetPhotoGalleryRecords(PhotoAlbum objPhotoAlbum)
        {
            return FacadeManager.PhotoManager.GetPhotoGalleryRecords(objPhotoAlbum);
        }

        /// <summary>
        /// Method to get the list of photos.
        /// </summary>
        /// <param name="objPhotoAlbum">Photo entity containing PhotoAlbumId, page number and page size.</param>
        /// <returns>List of photos.</returns>
        public List<Photos> GetPhotos(Photos objPhoto)
        {
            return FacadeManager.PhotoManager.GetPhotos(objPhoto);
        }

        /// <summary>
        /// Method to get photo album detail.
        /// </summary>
        /// <param name="objPhotoAlbum">Photo entity containing PhotoAlbumId.</param>
        /// <returns>Photo album detail.</returns>
        public PhotoAlbum GetPhotoAlbumDetail(Photos objPhoto)
        {
            return FacadeManager.PhotoManager.GetPhotoAlbumDetail(objPhoto);
        }

        public int GetCurrentPhotoAlbums(int tributeId)
        {
            return FacadeManager.PhotoManager.GetCurrentPhotoAlbums(tributeId);
        }
        

        /// <summary>
        /// Method to get photo detail.
        /// </summary>
        /// <param name="objPhotoAlbum">Photo entity containing PhotoId.</param>
        /// <returns>Photo detail.</returns>
        public Photos GetPhotoDetail(Photos objPhoto)
        {
            return FacadeManager.PhotoManager.GetPhotoDetail(objPhoto);
        }

        /// <summary>
        /// Method to get the list of commnets for the selected photo
        /// </summary>
        /// <param name="objComment">Comment entity containing photoid</param>
        /// <returns>List of comments</returns>
        public List<CommentTributeAdministrator> GetModuleComments(CommentTributeAdministrator objComment)
        {
            return FacadeManager.PhotoManager.GetModuleComments(objComment);
        }

        /// <summary>
        /// Method to save comment for photo.
        /// </summary>
        /// <param name="Comment">Filled Comment Entity.</param>
        public void SaveComment(Comments objComment)
        {
            FacadeManager.CommentMgr.InsertModuleComment(objComment);
        }

        /// <summary>
        /// Method to save comment for photo.
        /// </summary>
        /// <param name="Comment">Filled Comment Entity.</param>
        public void SaveComment(Comments objComment, string _topUrl)
        {
            FacadeManager.CommentMgr.InsertModuleComment(objComment, _topUrl);
        }

        /// <summary>
        /// Method to delete comment for photo.
        /// </summary>
        /// <param name="objComment"></param>
        public void DeleteComment(Comments objComment)
        {
            FacadeManager.CommentMgr.DeleteComment(objComment);
        }

        /// <summary>
        /// Method to update photo details.
        /// </summary>
        /// <param name="objPhoto">Filled photos entity.</param>
        public void UpdatePhoto(Photos objPhoto)
        {
            FacadeManager.PhotoManager.UpdatePhotoDetails(objPhoto);
        }

        /// <summary>
        /// Method to delete photo.
        /// </summary>
        /// <param name="objPhoto">Filled photos entity.</param>
        public void DeletePhoto(Photos objPhoto)
        {
            FacadeManager.PhotoManager.DeletePhoto(objPhoto);
        }

        /// <summary>
        /// Method to get the count for number of photos in the selected album
        /// </summary>
        /// <param name="objPhotoAlbumId">PhotoAlbum entity containing PhotoAlbumId</param>
        /// <returns>Object containing count for number of photos in album.</returns>
        public object GetPhotoCount(PhotoAlbum objPhotoAlbumId)
        {
            return FacadeManager.PhotoManager.GetPhotoCount(objPhotoAlbumId);
        }

        /// <summary>
        /// Method to update photo album details.
        /// </summary>
        /// <param name="objPhoto">Filled photo album entity.</param>
        public object UpdatePhotoAlbum(PhotoAlbum objPhotoAlbum)
        {
            return FacadeManager.PhotoManager.UpdatePhotoAlbumDetails(objPhotoAlbum);
        }

        /// <summary>
        /// Method to delete photo album.
        /// </summary>
        /// <param name="objPhoto">Filled photo album entity.</param>
        public void DeletePhotoAlbum(PhotoAlbum objPhotoAlbum)
        {
            FacadeManager.PhotoManager.DeletePhotoAlbum(objPhotoAlbum);
        }

        /// <summary>
        /// Method to get the list of photoimagess.
        /// </summary>
        /// <param name="objPhotoAlbum">Photo entity containing PhotoAlbumId.</param>
        /// <returns>List of PhotoImages.</returns>
        public List<Photos> GetPhotoImagesList(Photos objPhoto)
        {
            return FacadeManager.PhotoManager.GetPhotoImagesList(objPhoto);
        }

        /// <summary>
        /// Method to get the list of photoalbums.
        /// </summary>
        /// <param name="objPhotoAlbum">PhotoAlbum entity containing TributeId.</param>
        /// <returns>List of PhotoAlbums.</returns>
        public List<PhotoAlbum> GetPhotoAlbumList(PhotoAlbum objPhotoAlbum)
        {
            return FacadeManager.PhotoManager.GetPhotoAlbumList(objPhotoAlbum);
        }

        /// <summary>
        /// Sends email when a new photo album is created
        /// </summary>
        /// <param name="objPhotoAlbum"></param>
        public void SendPhotoAlbumEmail(PhotoAlbum objPhotoAlbum)
        {
            FacadeManager.PhotoManager.SendPhotoAlbumEmail(objPhotoAlbum);
        }

        /// <summary>
        /// Sends email when photos are added to an album
        /// </summary>
        /// <param name="objPhotoList"></param>
        public void SendPhotoEmail(List<Photos> objPhotoList)
        {
            FacadeManager.PhotoManager.SendPhotoEmail(objPhotoList);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tributeId"></param>
        /// <returns></returns>
        public string GetTributeEndDate(int _tributeId)
        {
            return FacadeManager.PhotoManager.GetTributeEndDate(_tributeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool GetCustomHeaderDetail(int tributeId)
        {
            return FacadeManager.PhotoManager.GetCustomHeaderDetail(tributeId);
        }

        public int CreatePhotoAlbum(PhotoAlbum objPhotoAlbum)
        {
            return FacadeManager.PhotoManager.CreatePhotoAlbum(objPhotoAlbum);
        }

        public int SavePhotos(Photos objPhoto)
        {
            return FacadeManager.PhotoManager.AddPhotos(objPhoto);
        }


        public void SendPhotoEmail(IList<Photos> objLPhoto)
        {
            List<Photos> objPhotoList = (List<Photos>)objLPhoto;
            FacadeManager.PhotoManager.SendPhotoEmail(objPhotoList);
        }

        public Tributes GetTributeSessionForUrlAndType(Tributes objTribute)
        {
            return FacadeManager.TributeManager.GetTributeSessionForUrlAndType(objTribute, WebConfig.ApplicationType.ToString());
        }

        internal int TributePackageId(int tributeId)
        {
            return FacadeManager.BillingManager.TributePackageId(tributeId);
        }
        #endregion



        internal int GetPackIdonPhotoAlbumId(int photoAlbumId)
        {
            return FacadeManager.TributeManager.GetPackIdonPhotoAlbumId(photoAlbumId);
        }
    }//end class
}//end namespace
