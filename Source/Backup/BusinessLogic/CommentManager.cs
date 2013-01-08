///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.CommentManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the methods associated with comments
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.ResourceAccess;
//using System.Data;

namespace TributesPortal.BusinessLogic
{
    public class CommentManager
    {
        #region CLASS VARIABLES
        List<Comments> comments;
        private string moduleName=string.Empty;
        #endregion
        public void SaveComment(Comments Comment)
        {
            CommentResources objCommRes = new CommentResources();
            TributeResource objTributeResource = new TributeResource();
            object[] param ={ Comment };
            objCommRes.InsertRecord(param);
            //to send email notofication
            Tributes objTributeId = new Tributes();
            objTributeId.TributeId = Comment.CommentTypeId;
            objTributeId.TypeDescription = Comment.CodeTypeName;
            object[] paramMail = { objTributeId };
            List<UserInfo> objUser = objTributeResource.GetTributeAdministrators(paramMail);
            if (objUser.Count > 0)
            {
                SendEmail(objUser, Comment);
            }

            //Function to send the mail to the list of users who have added the Tribute in their list of favourites 
            List<UserInfo> objUserFav = objTributeResource.GetFavouriteTributeUsers(paramMail);
            if (objUserFav.Count > 0)
            {
                //objNote.NotesId = int.Parse(noteId.ToString());
                //As per discussion with Rupendra: will send the mail in "To" field. 
                //ie a comma separated list of users in the "to" field
                SendEmail(objUserFav, Comment);
            }

            /*
            Tributes objTributeId = new Tributes();
            objTributeId.TributeId = Comment.CommentTypeId;
            objTributeId.TypeDescription = "Guestbook";
            SendEmail(GetTributeOwner(objTributeId));*/

        }

        public void SaveComment(Comments Comment, string topUrl)
        {
            CommentResources objCommRes = new CommentResources();
            TributeResource objTributeResource = new TributeResource();
            object[] param = { Comment };
            objCommRes.InsertRecord(param);
            //to send email notofication
            Tributes objTributeId = new Tributes();
            objTributeId.TributeId = Comment.CommentTypeId;
            objTributeId.TypeDescription = Comment.CodeTypeName;
            object[] paramMail = { objTributeId };
            List<UserInfo> objUser = objTributeResource.GetTributeAdministrators(paramMail);
            if (objUser.Count > 0)
            {
                SendEmail(objUser, Comment, topUrl);
            }

            //Function to send the mail to the list of users who have added the Tribute in their list of favourites 
            List<UserInfo> objUserFav = objTributeResource.GetFavouriteTributeUsers(paramMail);
            if (objUserFav.Count > 0)
            {
                //objNote.NotesId = int.Parse(noteId.ToString());
                //As per discussion with Rupendra: will send the mail in "To" field. 
                //ie a comma separated list of users in the "to" field
                SendEmail(objUserFav, Comment, topUrl);
            }

            /*
            Tributes objTributeId = new Tributes();
            objTributeId.TributeId = Comment.CommentTypeId;
            objTributeId.TypeDescription = "Guestbook";
            SendEmail(GetTributeOwner(objTributeId));*/

        }

        public void DeleteComment(Comments Comment)
        {
            CommentResources objCommRes = new CommentResources();
            object[] param ={ Comment };
            objCommRes.Delete(param);

        }

        public List<CommentTributeAdministrator> CommentList(CommentTributeAdministrator objSession)
        {
            CommentResources objComRes = new CommentResources();
            object[] param ={ objSession };
            return objComRes.CommentList(param);
        }

        public int RecordCount(CommentTributeAdministrator objSesion)
        {
            CommentResources objComRes = new CommentResources();
            object[] param ={ objSesion };
            return objComRes.RecordCount(param);
        }

        public UserInfo GetTributeOwner(Tributes objTributeId)
        {
            TributeResource objTribute = new TributeResource();
            object[] obj = { objTributeId };
            return objTribute.GetTributeOwner(obj);
        }

        /// <summary>
        /// Method to send email to a user
        /// </summary>
        /// <param name="objUsers">User Info entity containing User name and email address</param>
        public void SendEmail(UserInfo objUsers)
        {
            EmailMessages objEmail = EmailMessages.Instance;
            //bool val = objEmail.SendMessages("" + WebConfig.NotificationEmail + "", objUsers.UserEmail, "New comment received", GetEmailBody(), EmailMessages.TextFormat.Html.ToString());
        }

        /// <summary>
        /// Method to send email to the list of users
        /// </summary>
        /// <param name="objUsers">User Info entity containing User name and email address</param>
        public void SendEmail(List<UserInfo> objUsers, Comments objComment)
        {
            EmailMessages objEmail = EmailMessages.Instance;
            foreach (UserInfo obj in objUsers)
            {
                bool val = objEmail.SendMessages("Your "+WebConfig.ApplicationWord+"<" + WebConfig.NoreplyEmail + ">", obj.UserEmail, GetEmailSubject(objComment), GetEmailBody(objComment), EmailMessages.TextFormat.Html.ToString());
            }
        }

        public void SendEmail(List<UserInfo> objUsers, Comments objComment, string topUrl)
        {
            EmailMessages objEmail = EmailMessages.Instance;
            foreach (UserInfo obj in objUsers)
            {
                bool val = objEmail.SendMessages("Your " + WebConfig.ApplicationWord + "<" + WebConfig.NoreplyEmail + ">", obj.UserEmail, GetEmailSubject(objComment), GetEmailBody(objComment, topUrl), EmailMessages.TextFormat.Html.ToString());
            }
        }

        /// <summary>
        /// Method to get the body part of email.
        /// </summary>
        /// <returns>HTML string of body part</returns>
        private string GetEmailBody(Comments objComment)
        {
            StringBuilder sbBody = new StringBuilder();
            string strPath;
            string strReadComment = "To read the comment, follow the link below:";
            sbBody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + objComment.UserName);

            if (moduleName == "Notes")
            {
                sbBody.Append(" commented on a note in the ");
                string strLink = "http://" + objComment.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/note.aspx?noteId=" + objComment.CommentTypeId;
                strPath = "<a href='" + strLink + "'>" + strLink + "</a>"; //"<a href='http://" + objComment.TributeType + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/note.aspx?noteId=" + objComment.CommentTypeId + "'>" + "Click here to visit the link</a>";
                //strPath = "<a href='http://" + objComment.PathToVisit + "/Notes/NoteFullView.aspx" + "?noteId=" + objComment.CommentTypeId + "&TributeId=" + objComment.TributeId + "&TributeName=" + objComment.TributeName + "&TributeType=" + objComment.TributeType + "&TributeUrl=" + objComment.TributeUrl + "&mode=link'>" + "Click here to visit the link</a>";
            }
            else if (moduleName == "Video")
            {
                sbBody.Append(" commented on a video in the ");
                string strLink = "http://" + objComment.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/video.aspx?mode=view&VideoId=" + objComment.CommentTypeId;
                strPath = "<a href='" + strLink + "'>" + strLink + "</a>"; // "<a href='http://" + objComment.TributeType + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/managevideo.aspx?VideoId=" + objComment.CommentTypeId + "'>" + "Click here to visit the link</a>";
                //strPath = "<a href='http://" + objComment.PathToVisit + "/Video/ManageVideo.aspx" + "?VideoId=" + objComment.CommentTypeId + "&TributeId=" + objComment.TributeId + "&TributeName=" + objComment.TributeName + "&TributeType=" + objComment.TributeType + "&TributeUrl=" + objComment.TributeUrl + "&mode=link'>" + "Click here to visit the link</a>";
            }
            else if (moduleName == "Photo")
            {
                sbBody.Append(" commented on a photo in the ");
                string strLink = "http://" + objComment.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/photo.aspx?PhotoId=" + objComment.CommentTypeId;
                strPath = "<a href='" + strLink + "'>" + strLink + "</a>"; // "<a href='http://" + objComment.TributeType + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/photo.aspx?PhotoId=" + objComment.CommentTypeId + "'>" + "Click here to visit the link</a>";
                //strPath = "<a href='http://" + objComment.PathToVisit + "/Photo/PhotoView.aspx" + "?PhotoId=" + objComment.CommentTypeId + "&TributeId=" + objComment.TributeId + "&TributeName=" + objComment.TributeName + "&TributeType=" + objComment.TributeType + "&TributeUrl=" + objComment.TributeUrl + "&mode=link'>" + "Click here to visit the link</a>";
            }
            else
            {
                sbBody.Append(" added a new guestbook entry in the ");
                strReadComment = "<p>To read the guestbook entry, follow the link below:";
                string strLink = "http://" + objComment.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/guestbook.aspx";
                strPath = "<a href='" + strLink + "'>" + strLink + "</a></p>"; // "<a href='http://" + objComment.TributeType + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/guestbook.aspx'>" + "Click here to visit the link</a>";
                //strPath = "<a href='http://" + objComment.PathToVisit + "/Guestbook/Guestbook.aspx" + "?TributeId=" + objComment.CommentTypeId + "&TributeName=" + objComment.TributeName + "&TributeType=" + objComment.TributeType + "&TributeUrl=" + objComment.TributeUrl + "&mode=link'>" + "Click here to visit the link</a>";
            }

            sbBody.Append(objComment.TributeName + " " + objComment.TributeType + "  " + WebConfig.ApplicationWordForInternalUse + ".</p>");
            sbBody.Append(strReadComment);
            //sbBody.Append("To read the guestbook entry, follow the link below:");
            sbBody.Append("<br/>");
            sbBody.Append(strPath);
            //sbBody.Append(objComment.PathToVisit + "/Guestbook/Guestbook.aspx" + "?TributeId=" + objComment.CommentTypeId + "&TributeName=" + objComment.TributeName + "&TributeType=" + objComment.TributeType + "&TributeUrl=" + objComment.TributeUrl + "&mode=link'>" + "Click here to visit the link</a>");

            sbBody.Append("<p>---<br/>");
            sbBody.Append("Your " + WebConfig.ApplicationWord + " Team</p>");

            return sbBody.ToString();
        }

        private string GetEmailBody(Comments objComment, string topUrl)
        {
            StringBuilder sbBody = new StringBuilder();
            string strPath;
            string strReadComment = "To read the comment, follow the link below:";
            sbBody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + objComment.UserName);

            if (moduleName == "Notes")
            {
                sbBody.Append(" commented on a note in the ");
                string strLink = topUrl + "?" + "http://" + objComment.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/note.aspx?noteId=" + objComment.CommentTypeId;
                strPath = "<a href='" + strLink + "'>" + strLink + "</a>"; //"<a href='http://" + objComment.TributeType + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/note.aspx?noteId=" + objComment.CommentTypeId + "'>" + "Click here to visit the link</a>";
                //strPath = "<a href='http://" + objComment.PathToVisit + "/Notes/NoteFullView.aspx" + "?noteId=" + objComment.CommentTypeId + "&TributeId=" + objComment.TributeId + "&TributeName=" + objComment.TributeName + "&TributeType=" + objComment.TributeType + "&TributeUrl=" + objComment.TributeUrl + "&mode=link'>" + "Click here to visit the link</a>";
            }
            else if (moduleName == "Video")
            {
                sbBody.Append(" commented on a video in the ");
                string strLink = topUrl + "?" + "http://" + objComment.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/video.aspx?mode=view&VideoId=" + objComment.CommentTypeId;
                strPath = "<a href='" + strLink + "'>" + strLink + "</a>"; // "<a href='http://" + objComment.TributeType + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/managevideo.aspx?VideoId=" + objComment.CommentTypeId + "'>" + "Click here to visit the link</a>";
                //strPath = "<a href='http://" + objComment.PathToVisit + "/Video/ManageVideo.aspx" + "?VideoId=" + objComment.CommentTypeId + "&TributeId=" + objComment.TributeId + "&TributeName=" + objComment.TributeName + "&TributeType=" + objComment.TributeType + "&TributeUrl=" + objComment.TributeUrl + "&mode=link'>" + "Click here to visit the link</a>";
            }
            else if (moduleName == "Photo")
            {
                sbBody.Append(" commented on a photo in the ");
                string strLink = topUrl + "?" + "http://" + objComment.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/photo.aspx?PhotoId=" + objComment.CommentTypeId;
                strPath = "<a href='" + strLink + "'>" + strLink + "</a>"; // "<a href='http://" + objComment.TributeType + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/photo.aspx?PhotoId=" + objComment.CommentTypeId + "'>" + "Click here to visit the link</a>";
                //strPath = "<a href='http://" + objComment.PathToVisit + "/Photo/PhotoView.aspx" + "?PhotoId=" + objComment.CommentTypeId + "&TributeId=" + objComment.TributeId + "&TributeName=" + objComment.TributeName + "&TributeType=" + objComment.TributeType + "&TributeUrl=" + objComment.TributeUrl + "&mode=link'>" + "Click here to visit the link</a>";
            }
            else
            {
                sbBody.Append(" added a new guestbook entry in the ");
                strReadComment = "<p>To read the guestbook entry, follow the link below:";
                //string strLink = "http://" + objComment.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/guestbook.aspx?iframeurl=" + topUrl;
                string strLink = topUrl + "?http://" + objComment.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/guestbook.aspx";
                strPath = "<a href='" + strLink + "'>" + strLink + "</a></p>"; // "<a href='http://" + objComment.TributeType + "." + WebConfig.TopLevelDomain + "/" + objComment.TributeUrl + "/guestbook.aspx'>" + "Click here to visit the link</a>";
                //strPath = "<a href='http://" + objComment.PathToVisit + "/Guestbook/Guestbook.aspx" + "?TributeId=" + objComment.CommentTypeId + "&TributeName=" + objComment.TributeName + "&TributeType=" + objComment.TributeType + "&TributeUrl=" + objComment.TributeUrl + "&mode=link'>" + "Click here to visit the link</a>";
            }

            sbBody.Append(objComment.TributeName + " " + objComment.TributeType + "  " + WebConfig.ApplicationWordForInternalUse + ".</p>");
            sbBody.Append(strReadComment);
            //sbBody.Append("To read the guestbook entry, follow the link below:");
            sbBody.Append("<br/>");
            sbBody.Append(strPath);
            //sbBody.Append(objComment.PathToVisit + "/Guestbook/Guestbook.aspx" + "?TributeId=" + objComment.CommentTypeId + "&TributeName=" + objComment.TributeName + "&TributeType=" + objComment.TributeType + "&TributeUrl=" + objComment.TributeUrl + "&mode=link'>" + "Click here to visit the link</a>");

            sbBody.Append("<p>---<br/>");
            sbBody.Append("Your " + WebConfig.ApplicationWord + " Team</p>");

            return sbBody.ToString();
        }

        /// <summary>
        /// Method to get subject for email.
        /// </summary>
        /// <param name="objComment"></param>
        /// <returns>Subject of email in string format.</returns>
        private string GetEmailSubject(Comments objComment)
        {
            if (moduleName == "Notes")
                return objComment.UserName + " commented on a note on Your " + WebConfig.ApplicationWord + "...";
            else if (moduleName == "Video")
                return objComment.UserName + " commented on a video on Your " + WebConfig.ApplicationWord + "...";
            else if (moduleName == "Photo")
                return objComment.UserName + " commented on a photo on Your " + WebConfig.ApplicationWord + "...";
            else
                return objComment.UserName + " added a new guestbook entry on Your " + WebConfig.ApplicationWord + "...";
        }

        /// <summary>
        /// Method to get the list of comments module wise.
        /// </summary>
        /// <param name="objSession"></param>
        /// <returns></returns>
        public List<CommentTributeAdministrator> GetModuleComments(CommentTributeAdministrator objSession)
        {
            CommentResources objCommentResource = new CommentResources();
            object[] param ={ objSession };
            return objCommentResource.GetModuleComments(param);
        }

        /// <summary>
        /// Method to insert comments on mudule
        /// </summary>
        /// <param name="Comment">Filled comment entity</param>
        public void InsertModuleComment(Comments Comment)
        {
            CommentResources objCommRes = new CommentResources();
            TributeResource objTributeResource = new TributeResource();
            moduleName = Comment.CodeTypeName;
            object[] param ={ Comment };
            objCommRes.InsertModuleComment(param);
            //to send email
            Tributes objTributeId = new Tributes();
            objTributeId.TributeId = Comment.TributeId;
            objTributeId.TypeDescription = Comment.ModuleFunctionalityName;
            object[] paramMail = { objTributeId };
            List<UserInfo> objUser = objTributeResource.GetTributeAdministrators(paramMail);
            if (objUser.Count > 0)
            {
                SendEmail(objUser, Comment);
            }

            //Function to send the mail to the list of users who have added the Tribute in their list of favourites 
            List<UserInfo> objUserFav = objTributeResource.GetFavouriteTributeUsers(paramMail);
            if (objUserFav.Count > 0)
            {
                //objNote.NotesId = int.Parse(noteId.ToString());
                //As per discussion with Rupendra: will send the mail in "To" field. 
                //ie a comma separated list of users in the "to" field
                SendEmail(objUserFav, Comment);
            }
        }

        /// <summary>
        /// Method to insert comments on mudule
        /// </summary>
        /// <param name="Comment">Filled comment entity</param>
        public void InsertModuleComment(Comments Comment, string _topUrl)
        {
            CommentResources objCommRes = new CommentResources();
            TributeResource objTributeResource = new TributeResource();
            moduleName = Comment.CodeTypeName;
            object[] param = { Comment };
            objCommRes.InsertModuleComment(param);
            //to send email
            Tributes objTributeId = new Tributes();
            objTributeId.TributeId = Comment.TributeId;
            objTributeId.TypeDescription = Comment.ModuleFunctionalityName;
            object[] paramMail = { objTributeId };
            List<UserInfo> objUser = objTributeResource.GetTributeAdministrators(paramMail);
            if (objUser.Count > 0)
            {
                SendEmail(objUser, Comment, _topUrl);
            }

            //Function to send the mail to the list of users who have added the Tribute in their list of favourites 
            List<UserInfo> objUserFav = objTributeResource.GetFavouriteTributeUsers(paramMail);
            if (objUserFav.Count > 0)
            {
                //objNote.NotesId = int.Parse(noteId.ToString());
                //As per discussion with Rupendra: will send the mail in "To" field. 
                //ie a comma separated list of users in the "to" field
                SendEmail(objUserFav, Comment, _topUrl);
            }
        }

    }
}
