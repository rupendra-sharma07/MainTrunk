///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.VideoManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the methods associated with Videos
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.ResourceAccess;
#endregion

namespace TributesPortal.BusinessLogic
{
    public partial class VideoManager
    {
        #region METHODS
        /// <summary> 
        /// Method to send Video Tribute Headers details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        public UserBusiness GetHeaderDetailsOnUserId(int userId)
        {
            VideoResource objVideoResource = new VideoResource();
            return objVideoResource.GetHeaderDetailsOnUserId(userId);
        }

        /// <summary>
        /// Method to send the request to Video resource for the record insertion.
        /// </summary>
        /// <param name="objVid">Filled Video Entity</param>
        /// <returns>Object containing the Identity or Error Message</returns>
        public bool SaveVideo(Videos objVid, string type)
        {
            object objIdentity = null;
            bool videocaptionexist = false;
            try
            {
                VideoResource objVideo = new VideoResource();
                object[] param = { objVid };
                //objVideo.InsertRecord(param);
                object objVideoId = objVideo.InsertDataAndReturnId(param);
                if (objVideoId != null)
                {
                    objVid.VideoId = int.Parse(objVideoId.ToString());
                    //to send mail
                    TributeResource objTribute = new TributeResource();
                    Tributes objTrib = new Tributes();
                    objTrib.TributeId = int.Parse(objVid.UserTributeId.ToString());
                    objTrib.TypeDescription = objVid.ModuleTypeName;
                    object[] paramTrib = { objTrib };
                    List<UserInfo> objUser = objTribute.GetTributeAdministrators(paramTrib);
                    if (objUser.Count > 0)
                    {
                        SendEmail(objUser, objVid, type);
                    }

                    //Function to send the mail to the list of users who have added the Tribute in their list of favourites 
                    List<UserInfo> objUserFav = objTribute.GetFavouriteTributeUsers(paramTrib);
                    if (objUserFav.Count > 0)
                    {
                        //As per discussion with Rupendra: will send the mail in "To" field. 
                        //ie a comma separated list of users in the "to" field
                        SendEmail(objUserFav, objVid, type);
                    }

                }
                else
                    videocaptionexist = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return videocaptionexist;
        }


        public bool SaveVideoTributeAndSendEmail(Videos objVid, string type)
        {
            object objIdentity = null;
            bool videocaptionexist = false;
            try
            {
                VideoResource objVideo = new VideoResource();
                object[] param = { objVid };
                //objVideo.InsertRecord(param);
                object objVideoId = objVideo.InsertDataAndReturnId(param);
                if (objVideoId != null)
                {
                    objVid.VideoId = int.Parse(objVideoId.ToString());
                    //to send mail
                    TributeResource objTribute = new TributeResource();
                    Tributes objTrib = new Tributes();
                    objTrib.TributeId = int.Parse(objVid.UserTributeId.ToString());
                    objTrib.TypeDescription = objVid.ModuleTypeName;
                    object[] paramTrib = { objTrib };
                    List<UserInfo> objUser = objTribute.GetTributeAdministrators(paramTrib);
                    if (objUser.Count > 0)
                    {
                        SendEmailForNewVideoTribute(objUser, objVid, type);
                    }

                    //Function to send the mail to the list of users who have added the Tribute in their list of favourites 
                    List<UserInfo> objUserFav = objTribute.GetFavouriteTributeUsers(paramTrib);
                    if (objUserFav.Count > 0)
                    {
                        //As per discussion with Rupendra: will send the mail in "To" field. 
                        //ie a comma separated list of users in the "to" field
                        SendEmail(objUserFav, objVid, type);
                    }

                }
                else
                    videocaptionexist = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return videocaptionexist;
        }

        /// <summary>
        /// Method to send the request to Video Resource to get the list of videos for Video Tribute
        /// </summary>
        /// <param name="objVideoGallery">Video Gallery entity conatining UserTributeId, Page size and Page number</param>
        /// <returns>List of Videos</returns>
        public List<VideoGallery> GetVideoGalleryRecords(VideoGallery objVideoGallery)
        {
            VideoResource objVideo = new VideoResource();
            object[] param = { objVideoGallery };
            return objVideo.GetVideoGalleryRecords(param);
        }

        /// <summary>
        /// Method to send email to the list of users
        /// </summary>
        /// <param name="objUsers">User Info entity containing User name and email address</param>
        public void SendEmail(List<UserInfo> objUsers, Videos objVideo, string type)
        {
            EmailMessages objEmail = EmailMessages.Instance;
            foreach (UserInfo obj in objUsers)
            {
                StringBuilder sbToEmail = new StringBuilder();
                sbToEmail.Append(obj.UserEmail);
                sbToEmail.Append(",");
                bool val = objEmail.SendMessages("Your Tribute<" + WebConfig.NoreplyEmail + ">", sbToEmail.ToString(), GetEmailSubject(objVideo, type), GetEmailBody(obj, objVideo, type), EmailMessages.TextFormat.Html.ToString());
            }
        }

        public void SendEmailForNewVideoTribute(List<UserInfo> objUsers, Videos objVideo, string type)
        {
            EmailMessages objEmail = EmailMessages.Instance;
            foreach (UserInfo obj in objUsers)
            {
                StringBuilder sbToEmail = new StringBuilder();
                sbToEmail.Append(obj.UserEmail);
                sbToEmail.Append(",");
                bool val = objEmail.SendMessages("Your Tribute<" + WebConfig.NoreplyEmail + ">", sbToEmail.ToString(), GetEmailSubject(objVideo, type), GetEmailBodyForVideoTribute(obj, objVideo, type), EmailMessages.TextFormat.Html.ToString());
            }
        }

        /// <summary>
        /// Method to get the body part of email.
        /// </summary>
        /// <param name="objUserInfo">Filled User Info entity</param>
        /// <returns>HTML string of body part</returns>
        private string GetEmailBody(UserInfo objUserInfo, Videos objVideo, string type)
        {
            StringBuilder sbBody = new StringBuilder();
            string strLink = string.Empty;
            sbBody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + objVideo.UserName);
            if (!string.IsNullOrEmpty(type))
            {
                if (Equals(type, "VideoTribute"))
                    sbBody.Append(" added a new video tribute in the ");
                else if (Equals(type, "Video"))
                    sbBody.Append(" added a new video in the ");
            }
            sbBody.Append(objVideo.TributeName + " " + objVideo.TributeType + "  Tribute.</p>");
            if (!string.IsNullOrEmpty(type))
            {
                if (Equals(type, "VideoTribute"))
                    sbBody.Append("<p>To view the video tribute, follow the link below:");
                else if (Equals(type, "Video"))
                    sbBody.Append("<p>To view the video, follow the link below:");
            }
            sbBody.Append("<br/>");
            if (objVideo.TributeVideoId == string.Empty || objVideo.TributeVideoId == null)
                strLink = "http://" + objVideo.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objVideo.TributeUrl + "/video.aspx?mode=view&VideoId=" + objVideo.VideoId;
            else
                strLink = "http://" + objVideo.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objVideo.TributeUrl + "/video.aspx?VideoId=" + objVideo.VideoId + "&videoType=videotribute&mode=view&";
            //sbBody.Append(objVideo.PathToVisit + "/Video/ManageVideo.aspx" + "?VideoId=" + objVideo.VideoId + "&TributeId=" + objVideo.UserTributeId + "&TributeName=" + objVideo.TributeName + "&TributeType=" + objVideo.TributeType + "&TributeUrl=" + objVideo.TributeUrl + "&mode=link'>" + "Click here to visit the link</a>");
            sbBody.Append("<a href='" + strLink + "'>" + strLink + "</a></p>");
            if ((objVideo.TributeVideoId != string.Empty || objVideo.TributeVideoId != null) && Equals(type, "VideoTribute"))
            {
                sbBody.Append("<p>To add a link to the video tribute on your website, use the code below:<br/>");
                sbBody.Append("&lt;a href=&#34; <a href=\"http://" + objVideo.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objVideo.TributeUrl + "/video.aspx?VideoId=" + objVideo.VideoId + "&videoType=videotribute&mode=view\">http://" + objVideo.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objVideo.TributeUrl + "/video.aspx?VideoId=" + objVideo.VideoId + "&videoType=videotribute&mode=view&#34;</a>");
                sbBody.Append("&#34;&gt;Watch the Video Tribute &lt;/a &gt;</p>");
            }
            sbBody.Append("<p>---<br/>");
            sbBody.Append("Your Tribute Team</p>");

            return sbBody.ToString();
        }

        private string GetEmailBodyForVideoTribute(UserInfo objUserInfo, Videos objVideo, string type)
        {
            StringBuilder sbBody = new StringBuilder();
            string strLink = string.Empty;
            sbBody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + objVideo.UserName);
            if (!string.IsNullOrEmpty(type))
            {
                sbBody.Append(" created a new Video Tribute ");
            }
            if (!string.IsNullOrEmpty(type))
            {
                sbBody.Append("<p>To view the video tribute, follow the link below:");
            }
            sbBody.Append("<br/>");
            if (objVideo.TributeVideoId == string.Empty || objVideo.TributeVideoId == null)
                strLink = "http://video." + WebConfig.TopLevelDomain + "/video/videotribute.aspx?tributeId="+objVideo.UserTributeId;
               //sbBody.Append(objVideo.PathToVisit + "/Video/ManageVideo.aspx" + "?VideoId=" + objVideo.VideoId + "&TributeId=" + objVideo.UserTributeId + "&TributeName=" + objVideo.TributeName + "&TributeType=" + objVideo.TributeType + "&TributeUrl=" + objVideo.TributeUrl + "&mode=link'>" + "Click here to visit the link</a>");
            sbBody.Append("<a href='" + strLink + "'>" + strLink + "</a></p>");
            if ((objVideo.TributeVideoId != string.Empty || objVideo.TributeVideoId != null) && Equals(type, "VideoTribute"))
            {
                sbBody.Append("<p>To add a link to the video tribute on your website, use the code below:<br/>");
                sbBody.Append("<a href='http://video." + WebConfig.TopLevelDomain + "/video/videotribute.aspx?tributeId=" + objVideo.UserTributeId + "'>Watch the Video Tribute</a></p>");
                //sbBody.Append("&#34;&gt;Watch the Video Tribute &lt;/a &gt;</p>");
            }
            sbBody.Append("<p>---<br/>");
            sbBody.Append("Your Tribute Team</p>");

            return sbBody.ToString();
        }

        /// <summary>
        /// Method to get subject for email.
        /// </summary>
        /// <param name="objComment"></param>
        /// <returns>Subject of email in string format.</returns>
        private string GetEmailSubject(Videos objVideo, string type)
        {
            string subject = string.Empty;
            if (!string.IsNullOrEmpty(type))
            {
                if (Equals(type, "VideoTribute"))
                    subject = objVideo.UserName + " added a new video tribute on Your Tribute...";
                else if (Equals(type, "Video"))
                    subject = objVideo.UserName + " added a new video on Your Tribute...";
            }

            return subject;

        }

        /// <summary>
        /// Method to get the details of video.
        /// </summary>
        /// <param name="objVideoGallery">Video Gallery entity containing User Tribute id, Page size and Page number</param>
        /// <returns>Filled VideoGallery entity</returns>
        public VideoGallery GetVideoDetails(VideoGallery objVideoGallery)
        {
            VideoResource objVideo = new VideoResource();
            object[] param = { objVideoGallery };
            return objVideo.GetVideoDetails(param);
        }

        /// <summary>
        /// Method to update the video details
        /// </summary>
        /// <param name="objVid">Filled Video entity</param>
        public object UpdateVideoDetails(Videos objVid)
        {
            VideoResource objVideo = new VideoResource();
            object[] param = { objVid };
            return objVideo.UpdateVideoDetails(param);
        }

        /// <summary>
        /// Method to delete video
        /// </summary>
        /// <param name="objVid">Filled Video Entity</param>
        public void DeleteVideo(Videos objVid)
        {
            VideoResource objVideo = new VideoResource();
            object[] param = { objVid };
            objVideo.DeleteVideo(param);
        }

        /// <summary>
        /// Method to get the details of video tribute.
        /// </summary>
        /// <param name="objVideoGallery">Video Gallery entity containing User Tribute id</param>
        /// <returns>Filled Video entity</returns>
        public VideoGallery GetVideoTributeDetails(Videos objTributeId)
        {
            VideoResource objVideo = new VideoResource();
            object[] param = { objTributeId };
            return objVideo.GetVideoTributeDetails(param);
        }

        /// <summary>
        /// Method to get the list of comments module wise.
        /// </summary>
        /// <param name="objSession"></param>
        /// <returns></returns>
        public List<CommentTributeAdministrator> GetModuleComments(CommentTributeAdministrator objComment)
        {
            CommentResources objCommentResource = new CommentResources();
            object[] param = { objComment };
            return objCommentResource.GetModuleComments(param);
        }

        /// <summary>
        /// Method to update the video tribute details
        /// </summary>
        /// <param name="objVid">Filled Video entity</param>
        public object UpdateVideoTributeDetails(Videos objVid)
        {
            VideoResource objVideo = new VideoResource();
            object[] param = { objVid };
            return objVideo.UpdateVideoTributeDetails(param);
        }

        /// <summary>
        /// Method to get the list of tributes to add video tributes
        /// </summary>
        /// <param name="objUserId">Tribute entity containing user id</param>
        /// <returns>List of tributes</returns>
        public List<Tributes> GetListOfTributesForVideoTribute(Tributes objUserId)
        {
            VideoResource objVideoResource = new VideoResource();
            object[] param = { objUserId };
            return objVideoResource.GetListOfTributesForVideoTribute(param);
        }

        /// <summary>
        /// Method to get the Token Details.
        /// </summary>
        /// <param name="objTokenId">Video Token entity containing Token Id</param>
        /// <returns>Filled VideoToken entity.</returns>
        public VideoToken GetTokenDetails(VideoToken objTokenId)
        {
            VideoResource objVideo = new VideoResource();
            object[] param = { objTokenId };
            return objVideo.GetTokenDetails(param);
        }

        /// <summary>
        /// Method to delete video tribute based on TributeId
        /// </summary>
        /// <param name="objVid"></param>
        public void DeleteVideoTribute(Videos objVid)
        {
            VideoResource objVideo = new VideoResource();
            object[] param = { objVid };
            objVideo.DeleteVideoTribute(param);
        }

        public Videos GetVideoTributeDetails(int videoId)
        {
            Videos objVideos;
            VideoResource objVideoResource = new VideoResource();
            objVideos = objVideoResource.GetTributeDetails(videoId);
            return objVideos;
        }

        /// <summary>
        /// LHK:to get end date of the tribute.
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns>Date in string format</returns>

        public string GetTributeEndDate(int tributeId)
        {
            VideoResource objVideoResource = new VideoResource();
            return objVideoResource.GetTributeEndDate(tributeId);
        }

        /// <summary>
        /// LHK:to get the vt left panel details 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns>object of tributes type</returns>
        public Tributes GetTributeFieldDetails(int tributeId)
        {
            VideoResource objVideoResource = new VideoResource();
            return objVideoResource.GetTributeFieldDetails(tributeId);

        }

        /// <summary>
        /// LHK:to get the vt video details
        /// </summary>
        /// <param name="_tributeId"></param>
        /// <returns>object of videos type</returns>
        public Videos GetVideoDetailsOnUserTributeId(int _tributeId)
        {
            VideoResource objVideoResource = new VideoResource();
            return objVideoResource.GetVideoDetailsOnUserTributeId(_tributeId);
        }



        public Tributes GetEditTributeFieldDetails(int tributeId)
        {
            VideoResource objVideoResource = new VideoResource();
            return objVideoResource.GetEditTributeFieldDetails(tributeId);

        }
        //Agnesh Arya--To get trdibute details over edit modal popup

        public void UpdateTributeDetail(Tributes objStory)
        {
            try
            {
                VideoResource objStoryRes = new VideoResource();
                objStoryRes.UpdateTributeDetail(objStory);
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateVideoTributeDetail(Tributes objStory)
        {
            try
            {
                VideoResource objStoryRes = new VideoResource();
                objStoryRes.UpdateVideoTributeDetail(objStory);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? GetLinkVideoMemorialTribute(int? UserId, int UserTributeId)
        {
            VideoResource objVideoResource = new VideoResource();
            return objVideoResource.GetLinkVideoMemorialTribute(UserId, UserTributeId);
        }
        public Users GetUserNameOnUserId(int _userId)
        {
            VideoResource objVideoResource = new VideoResource();
            return objVideoResource.GetUserNameOnUserId(_userId);
        }


        public void UpdateVideoTributeImage(int _tributeId, string _videoTributeImage)
        {
            VideoResource objVideoResource = new VideoResource();
            objVideoResource.UpdateVideoTributeImage(_tributeId, _videoTributeImage);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool GetCustomHeaderDetail(int tributeId)
        {
            VideoResource objVideoResource = new VideoResource();
            return objVideoResource.GetCustomHeaderDetail(tributeId);
        }

        public int GetCurrentVideos(int _tributeId)
        {
            VideoResource objVideoResource = new VideoResource();
            return objVideoResource.GetCurrentVideos(_tributeId);
        }
        /// <summary>
        /// Added to get old trburl to copy directory
        /// </summary>
        /// <param name="_tributeId"></param>
        /// <returns></returns>
        public string GetOldTributeUrlOnTributeId(int _tributeId)
        {
            VideoResource objVideoResource = new VideoResource();
            return objVideoResource.GetOldTributeUrlOnTributeId(_tributeId);
        }
        #endregion



        public void SendMailToAdmin(string strlink, string VideoLink, string NewtributeURL, string tributeType)
        {
            EmailMessages objEmail = EmailMessages.Instance;

            string testFrom = WebConfig.NoreplyEmail;
            string AdminEmail = string.Empty;
            //remove dev email after test.
            //testFrom = "smtpuser@yourtribute.com";
            if (!(string.IsNullOrEmpty(WebConfig.DevAdminEmail.ToString())))
            {
                AdminEmail = WebConfig.DevAdminEmail.ToString();
            }
            //bool val = objEmail.SendMessages("Your Tribute<" + testFrom + ">", WebConfig.AdministratorMail + "," + AdminEmail, "Files Copied to Upgraded TributeURL for TributeUrl = " + NewtributeURL, "Files Copied for tributeUrl = " + NewtributeURL + " of Type = " + tributeType + " on " + DateTime.Now.ToString(), EmailMessages.TextFormat.Html.ToString());
            bool val = objEmail.SendMessages("Your Tribute<" + testFrom + ">", WebConfig.AdministratorMail + "," + AdminEmail, "Files Copied to Upgraded TributeURL for TributeUrl = " + NewtributeURL,
                "Hello Administrator<br/><br/>The Video files for the following tribute are re-copied on "+DateTime.Now.ToString()+"<br/>To view the Tribute, follow the link below:<br/><a href='" + strlink.ToString() + "'>" +
                strlink.ToString() + "</a><br/>To view the Video recopied, follow the link below:<br/><a href='" +
                VideoLink.ToString() + "'>" + VideoLink.ToString() + "</a><br/><br/>---<br/>Thanks<br/>Your Tribute Development Team" , EmailMessages.TextFormat.Html.ToString());

        }

        public void SendMailToAdmin( string NewtributeURL, string errorString)
        {
            EmailMessages objEmail = EmailMessages.Instance;

            string testFrom = WebConfig.NoreplyEmail;
            string AdminMail = string.Empty; ;
            //remove dev email after test.
            //testFrom = "smtpuser@yourtribute.com";
            //string testDev = "lhari.k@optimusinfo.com";
            if (string.IsNullOrEmpty(WebConfig.DevAdminEmail.ToString()))
            {
                AdminMail = WebConfig.DevAdminEmail.ToString();
            }
            bool val = objEmail.SendMessages("Your Tribute<" + testFrom + ">", WebConfig.AdministratorMail + "," + AdminMail, "Error while copy for TributeUrl = " + NewtributeURL, "Error Sting = " + errorString + " on " + DateTime.Now.ToString(), EmailMessages.TextFormat.Html.ToString());

        }
    }//end class
}//end namespace
