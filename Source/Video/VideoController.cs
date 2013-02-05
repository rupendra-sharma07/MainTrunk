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
///File Name       : TributesPortal.Video.VideoController.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Controller class for the files under Videos.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;
#endregion

namespace TributesPortal.Video
{
    public class VideoController
    {
        public VideoController()
        {
        }

        /// <summary>
        /// Method to update video tribute details
        /// </summary>
        /// <param name="objVideoDetails">Filled Video entity</param>
        public object UpdateVideoTributeDetails(Videos objVideoDetails)
        {
            return FacadeManager.VideoManager.UpdateVideoTributeDetails(objVideoDetails);
        }
        //Agnesh Arya--To get Tributes personal details through edit modal popup

        public Tributes GetEditTributeFieldDetails(int tributeId)
        {
            return FacadeManager.VideoManager.GetEditTributeFieldDetails(tributeId);
        }

        /// <summary>
        /// This method will call the Tribute Manager class to get the Country list and State list
        /// </summary>
        /// 
        /// <param name="countries">This will pass the parent location (country) for the state and null for the country
        /// </param>
        /// <returns>This method will return the list of location(state, country)</returns>
        public IList<Locations> GetCountryList(Locations countries)
        {
            try
            {
                return FacadeManager.TributeManager.GetCountryList(countries);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method to get Video tribute header details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        public UserBusiness GetHeaderDetailsOnUserId(int userId)
        {
            return FacadeManager.VideoManager.GetHeaderDetailsOnUserId(userId);
        }

        /// <summary>
        /// Method to save Video
        /// </summary>
        /// <param name="objVideo">Filled Video Entity</param>
        /// <returns>Video Id</returns>
        public bool SaveVideo(Videos objVideo, string type)
        {
            //if(Equals(type,"VideoTribute"))
            return FacadeManager.VideoManager.SaveVideo(objVideo, type);
        }

        /// <summary>
        /// Method to get the list of videos for the selected tribute
        /// </summary>
        /// <param name="objVideoGallery">Filled Video Gallery Entity</param>
        /// <returns>List of videos in the gallery</returns>
        public List<VideoGallery> GetVideosOfTribute(VideoGallery objVideoGallery)
        {
            return FacadeManager.VideoManager.GetVideoGalleryRecords(objVideoGallery);
        }

        /// <summary>
        /// Method to get the details of selected video
        /// </summary>
        /// <param name="objVideoGallery">Video Id</param>
        /// <returns>Filled Video Gallery entity</returns>
        public VideoGallery GetVideoDetails(VideoGallery objVideoGallery)
        {
            return FacadeManager.VideoManager.GetVideoDetails(objVideoGallery);
        }

        /// <summary>
        /// Method to update video details
        /// </summary>
        /// <param name="objVideoDetails">Filled Video entity</param>
        public object UpdateVideoDetails(Videos objVideoDetails)
        {
            return FacadeManager.VideoManager.UpdateVideoDetails(objVideoDetails);
        }

        /// <summary>
        /// Method to delete video
        /// </summary>
        /// <param name="objVideo">Video entity containing Video Id</param>
        public void DeleteVideo(Videos objVideo)
        {
            FacadeManager.VideoManager.DeleteVideo(objVideo);
        }

        /// <summary>
        /// Method to get the video tribute details
        /// </summary>
        /// <param name="objVideo">Videos entity containing video id</param>
        public VideoGallery GetVideoTributeDetails(Videos objVideo)
        {
            return FacadeManager.VideoManager.GetVideoTributeDetails(objVideo);
        }

        /// <summary>
        /// Method to get the list of commnets for the selected video
        /// </summary>
        /// <param name="objComment">Comment entity containing videoid</param>
        /// <returns>List of comments</returns>
        public List<CommentTributeAdministrator> GetModuleComments(CommentTributeAdministrator objComment)
        {
            return FacadeManager.VideoManager.GetModuleComments(objComment);
        }

        /// <summary>
        /// Method to save comment for video
        /// </summary>
        /// <param name="Comment">Filled Comment Entity.</param>
        public void SaveComment(Comments objComment)
        {
            FacadeManager.CommentMgr.InsertModuleComment(objComment);
        }

        /// <summary>
        /// Method to save comment for video
        /// </summary>
        /// <param name="Comment">Filled Comment Entity.</param>
        public void SaveComment(Comments objComment, string _topUrl)
        {
            FacadeManager.CommentMgr.InsertModuleComment(objComment, _topUrl);
        }

        /// <summary>
        /// Method to delete comment for video
        /// </summary>
        /// <param name="objComment"></param>
        public void DeleteComment(Comments objComment)
        {
            FacadeManager.CommentMgr.DeleteComment(objComment);
        }

        /// <summary>
        /// This method will call the Story Mananger class method to update the tribute detail
        /// </summary>
        /// 
        /// <param name="objTribute">Stories object which contain the Tribute detail which user want to update
        /// </param>
        public void UpdateTributeDetail(Tributes objTribute)
        {
            try
            {
                FacadeManager.VideoManager.UpdateTributeDetail(objTribute);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateVideoTributeDetail(Tributes objTribute)
        {
            try
            {
                FacadeManager.VideoManager.UpdateVideoTributeDetail(objTribute);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Method to get the list of tributes to add video tributes
        /// </summary>
        /// <param name="objTribute">Tribute entity containing user id</param>
        /// <returns>List of tributes</returns>
        public List<Tributes> GetListOfTributesForVideoTribute(Tributes objTribute)
        {
            return FacadeManager.VideoManager.GetListOfTributesForVideoTribute(objTribute);
        }

        /// <summary>
        /// Method to get token details
        /// </summary>
        /// <param name="objTokenId">Video Token entity containing token id </param>
        /// <returns>Filled VideoToken entity.</returns>
        public VideoToken GetTokenDetails(VideoToken objTokenId)
        {
            return FacadeManager.VideoManager.GetTokenDetails(objTokenId);
        }

        /// <summary>
        /// Method to get User Details
        /// </summary>
        /// <param name="objUserId">UserRegistration entity containing UserId.</param>
        public void GetUserInfo(UserRegistration objUserId)
        {
            FacadeManager.UserInfoManager.GetUserDetails(objUserId);
        }


        //Mohit Gupta for tribute upgrade 31 Jan 2011 Phase 2

        public void GetUserDetails(UserRegistration objUserId)
        {
            FacadeManager.UserInfoManager.GetUserDetails(objUserId);
        }

        /// <summary>
        /// Method to delete video tribute based on tribute id.
        /// </summary>
        /// <param name="objVideo">Video entity containing tribute Id</param>
        public void DeleteVideoTribute(Videos objVideo)
        {
            FacadeManager.VideoManager.DeleteVideoTribute(objVideo);
        }
        /// <summary>
        /// LHK: to get end date of a tribute
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns>date in string format</returns>
        public string GetTributeEndDate(int tributeId)
        {
            return FacadeManager.VideoManager.GetTributeEndDate(tributeId);
        }
        /// <summary>
        /// LHK: To get tribute details for videoTribute
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns>tribute type of object</returns>
        public Tributes GetTributeFieldDetails(int tributeId)
        {
            return FacadeManager.VideoManager.GetTributeFieldDetails(tributeId);
        }

        /// <summary>
        /// LHK: fetch video details for video display
        /// </summary>
        /// <param name="_tributeId"></param>
        /// <returns>object of videos type</returns>
        public Videos GetVideoDetailsOnUserTributeId(int tributeId)
        {
            return FacadeManager.VideoManager.GetVideoDetailsOnUserTributeId(tributeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserTributeId"></param>
        /// <returns></returns>
        public int? GetLinkVideoMemorialTribute(int? UserId, int UserTributeId)
        {
            return FacadeManager.VideoManager.GetLinkVideoMemorialTribute(UserId, UserTributeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objTribute"></param>
        /// <returns></returns>
        public Templates GetExistingFolderName(Tributes objTribute)
        {
            return FacadeManager.TributeThemeManager.GetExistingFolderName(objTribute);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns></returns>
        public Users GetUserNameOnUserId(int _userId)
        {
            return FacadeManager.VideoManager.GetUserNameOnUserId(_userId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        public void AddTributeCount(int tributeId)
        {
            FacadeManager.TributeManager.AddTributeCount(tributeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool GetCustomHeaderDetail(int tributeId)
        {
            return FacadeManager.VideoManager.GetCustomHeaderDetail(tributeId);
        }


        internal int GetCurrentVideos(int _tributeId)
        {
            return FacadeManager.VideoManager.GetCurrentVideos(_tributeId);
        }

        /// <summary>
        /// Addded to copy folder from new location
        /// </summary>
        /// <param name="_tributeId"></param>
        /// <returns></returns>
        internal string GetOldTributeUrlOnTributeId(int _tributeId)
        {
            return FacadeManager.VideoManager.GetOldTributeUrlOnTributeId(_tributeId);
        }

        internal void SendMailToAdmin(string link, string videolink, string NewtributeURL, string tributeType)
        {
            FacadeManager.VideoManager.SendMailToAdmin(link, videolink, NewtributeURL, tributeType);
        }
        internal void SendMailToAdmin(string NewtributeURL, string errorString)
        {
            FacadeManager.VideoManager.SendMailToAdmin(NewtributeURL, errorString);
        }

        internal int GetPackage(int? nullableId)
        {
            int tributeId = 0;
            if (nullableId != null)
            {
                int.TryParse(nullableId.ToString(), out tributeId); 
            }
            return FacadeManager.TributeManager.GetTrbDetailOnTributeId(tributeId);
        }



        internal int GetTributePackageId(int _tributeId)
        {
            return FacadeManager.BillingManager.TributePackageId(_tributeId);
        }
    }//end class
}//end namespace
