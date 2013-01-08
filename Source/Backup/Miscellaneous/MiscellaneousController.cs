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
///File Name       : TributesPortal.Miscellaneous.MiscellaneousController.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Controller class for the Miscellaneouss.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;
#endregion

namespace TributesPortal.Miscellaneous
{
    public class MiscellaneousController
    {
        public MiscellaneousController()
        {
        }

        /// <summary>
        /// Method to get the list of Themes based on Tribute Type
        /// </summary>
        /// <param name="objTributeType">Type of tribute in Template entity</param>
        /// <returns>List of Themes</returns>
        public List<Templates> GetThemesList(Templates objTributeType)
        {
            return FacadeManager.TributeThemeManager.GetThemes(objTributeType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objTributeType"></param>
        /// <returns></returns>
        public List<Templates> GetThemesFolderList(Templates objTributeType,string applicationType)
        {
            return FacadeManager.TributeThemeManager.GetThemesFolder(objTributeType, applicationType);
        }

        /// <summary>
        /// Method to update tribute theme.
        /// </summary>
        /// <param name="objTheme">Filled tribute entity.</param>
        public void UpdateTributeTheme(Tributes objTheme)
        {
            FacadeManager.TributeThemeManager.UpdateTributeTheme(objTheme);
        }

        /// <summary>
        /// Method to get the theme for the tribute
        /// </summary>
        /// <param name="objTribute">Filled tribute entity</param>
        /// <returns>Filled Template entity.</returns>
        public Templates GetThemeForTribute(Tributes objTribute)
        {
            return FacadeManager.TributeThemeManager.GetThemeForTribute(objTribute);
        }

        public Templates GetThemeFolderForTribute(Tributes objTribute)
        {
            return FacadeManager.TributeThemeManager.GetThemeFolderForTribute(objTribute);
        }

        /// <summary>
        /// Method to add tribute to favorite list.
        /// </summary>
        /// <param name="objFavorite">Filled AddToFavorite entity.</param>
        /// <returns>0 if value added else > 0 (already in favorite list)</returns>
        public int AddToFavorites(AddToFavorite objFavorite)
        {
            return FacadeManager.TributeManager.AddToFavourites(objFavorite);
        }

        /// <summary>
        /// Method to get count for Tribute is in User favorite list or not.
        /// </summary>
        /// <param name="objFavorite">AddToFavorite entity containing TributeId and UserId</param>
        /// <returns>Count if > 0 already in list else not in list</returns>
        public int GetUserTributeFavorites(AddToFavorite objFavorite)
        {
            return FacadeManager.TributeManager.GetUserTributeFavorites(objFavorite);
        }
        
        /// <summary>
        /// Method to remove favorite from list.
        /// </summary>
        /// <param name="objFavorite">Filled AddToFavorite entity.</param>
        public void RemoveFromFavotire(AddToFavorite objFavorite)
        {
            FacadeManager.TributeManager.RemoveFromFavotire(objFavorite);
        }

        /// <summary>
        /// Method to get Tribute Details for session based on Tribute Url and TributeType.
        /// </summary>
        /// <param name="objTribute">Tribute entity containing Tribute Url and Tribute Type.</param>
        /// <returns>Filled Tributes entity.</returns>
        public Tributes GetTributeSessionForUrlAndType(Tributes objTribute, string ApplicationType)
        {
            return FacadeManager.TributeManager.GetTributeSessionForUrlAndType(objTribute, ApplicationType);
        }

        public void TriputePackageInfo(object[] param)
        {
            FacadeManager.BillingManager.TriputePackageInfo(param);
        }

        /// <summary>
        /// for notes limit
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public int GetCurrentNotes(int tributeId)
        {
            return FacadeManager.NotesManager.GetCurrentNotes(tributeId);
        }

        /// <summary>
        /// to GetCurrentEvents 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public int GetCurrentEvents(int tributeId)
        {
            return FacadeManager.NotesManager.GetCurrentEvents(tributeId);
        }

        /// <summary>
        /// to GetCurrentPhotoAlbums
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public int GetCurrentPhotoAlbums(int tributeId)
        {
            return FacadeManager.PhotoManager.GetCurrentPhotoAlbums(tributeId);
        }

        /// <summary>
        /// to GetCurrentVideos
        /// </summary>
        /// <param name="_tributeId"></param>
        /// <returns></returns>
        public int GetCurrentVideos(int _tributeId)
        {
            return FacadeManager.VideoManager.GetCurrentVideos(_tributeId);
        }

        /// <summary>
        /// GetTributeUrlOnOldTributeUrl
        /// </summary>
        /// <param name="_tributeUrl"></param>
        /// <returns></returns>
        public Tributes GetTributeUrlOnOldTributeUrl(Tributes objtrb,string ApplicationType)
        {
            return FacadeManager.TributeManager.GetTributeUrlOnOldTributeUrl(objtrb, ApplicationType);
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

        public List<Photos> GetPhotoImagesList(Photos objPhoto)
        {
            return FacadeManager.PhotoManager.GetPhotoImagesList(objPhoto);
        }

        public int GetTributePackageId(int _tributeId)
        {
            return FacadeManager.BillingManager.TributePackageId(_tributeId);
        }

        public string GetTributeEndDate(int _tributeId)
        {
            return FacadeManager.PhotoManager.GetTributeEndDate(_tributeId);
        }

        public void GetTributeByID(TributesUserInfo _objTributeUserinfo)
        {
            FacadeManager.UserInfoManager.GetTributeByID(_objTributeUserinfo);
        }

        public bool IsAllowedPhotoCheck(int _photoAlbumId)
        {
            return FacadeManager.TributeManager.IsAllowedPhotoCheck(_photoAlbumId);
        }

        public bool IsAllowedPhotoCheckonPhotoId(int PhotoId)
        {
            return FacadeManager.TributeManager.IsAllowedPhotoCheckonPhotoId(PhotoId);
        }

        public int GetPackIdonPhotoId(int PhotoId)
        {
            return FacadeManager.BillingManager.GetPackIdonPhotoId(PhotoId);
        }
    }
}
