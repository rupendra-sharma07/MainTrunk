///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.PhotoManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the basic methods for photos
///Audit Trail     : Date of Modification  Modified By         Description



#region USING DIRECTIVES
using System;
using System.Collections.Generic;
//using System.Data;
using System.IO;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;
using TributesPortal.Utilities;
using System.Transactions;
#endregion

namespace TributesPortal.BusinessLogic
{
    public partial class PhotoManager
    {
        /// <summary>
        /// Method to create photo album
        /// </summary>
        /// <param name="objPhotoAlbum">Filled PhotoAlbum entity</param>
        /// <param name="objPhotoList">List of filled Photo entity</param>
        public int CreatePhotoAlbum(PhotoAlbum objPhotoAlbum, List<Photos> objPhotoList)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            object photoAlbumId = 0;
            object[] param = { objPhotoAlbum };
            using (TransactionScope trans = new TransactionScope())
            {
                photoAlbumId = objPhotoResource.AddPhotoAlbum(param);

                if (!Equals(photoAlbumId, null))
                {
                    if (int.Parse(photoAlbumId.ToString()) > 0)
                        AddPhotos(objPhotoList, int.Parse(photoAlbumId.ToString()));
                    else
                        return int.Parse(photoAlbumId.ToString());
                }
                //Transaction Commited
                trans.Complete();
            }
                     
            return int.Parse(photoAlbumId.ToString());
        }

        /// <summary>
        /// Method to add Photos
        /// </summary>
        /// <param name="objPhotoList">List of filled Photo entity</param>
        /// <param name="photoAlbumId">PhotoAlbumId</param>
        public void AddPhotos(List<Photos> objPhotoList, int photoAlbumId)
        {
            foreach (Photos objPhoto in objPhotoList)
            {
                objPhoto.PhotoAlbumId = photoAlbumId;
            }
            PhotoResource objPhotoAlbumResource = new PhotoResource();
            object[] param = { objPhotoList };
            objPhotoAlbumResource.AddPhotoToAlbum(param);
        }

        /// <summary>
        /// Method to add photos
        /// </summary>
        /// <param name="objPhotoList">List of filled photos entity.</param>
        public int AddPhotos(List<Photos> objPhotoList)
        {
            PhotoResource objPhotoAlbumResource = new PhotoResource();
            object[] param = { objPhotoList };

            object objLastPhotoId = new object();
            using (TransactionScope trans = new TransactionScope())
            {
                objLastPhotoId = objPhotoAlbumResource.AddPhotoToAlbum(param);
                //Transaction Commited
                trans.Complete();
            }
            return int.Parse(objLastPhotoId.ToString());
        }

        /// <summary>
        /// Method to get the list of photo albums
        /// </summary>
        /// <param name="objPhotoAlbum">PhotoAlbum entity containing TributeId, pagesize and page number</param>
        /// <returns>List of filled PhotoAlbum entity</returns>
        public List<PhotoAlbum> GetPhotoGalleryRecords(PhotoAlbum objPhotoAlbum)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            object[] param = { objPhotoAlbum };
            return objPhotoResource.GetPhotoGalleryRecords(param);
        }

        /// <summary>
        /// Method to get the list of photos
        /// </summary>
        /// <param name="objPhotoAlbum">Photo entity containing PhotoAlbumId, pagesize and page number</param>
        /// <returns>List of filled Photo entity</returns>
        public List<Photos> GetPhotos(Photos objPhoto)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            object[] param = { objPhoto };
            return objPhotoResource.GetPhotos(param);
        }

        /// <summary>
        /// Method to get the list of photos date wise
        /// </summary>
        /// <param name="objPhotoAlbum">Photo entity containing PhotoAlbumId, pagesize and page number</param>
        /// <returns>List of filled Photo entity</returns>
        public List<Photos> GetPhotosDateWise(Photos objPhoto)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            object[] param = { objPhoto };
            return objPhotoResource.GetPhotosDateWise(param);
        }

        /// <summary>
        /// Method to get photo album details.
        /// </summary>
        /// <param name="objPhotoAlbum">Photo entity containing PhotoAlbumId</param>
        /// <returns>Filled PhotoAlbum entity</returns>
        public PhotoAlbum GetPhotoAlbumDetail(Photos objPhoto)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            object[] param = { objPhoto };
            return objPhotoResource.GetPhotoAlbumDetail(param);
        }

        public int GetCurrentPhotoAlbums(int tributeId)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            return objPhotoResource.GetCurrentPhotoAlbums(tributeId);
        }

        /// <summary>
        /// Method to get photo details.
        /// </summary>
        /// <param name="objPhotoAlbum">Photo entity containing PhotoId</param>
        /// <returns>Filled Photo entity</returns>
        public Photos GetPhotoDetail(Photos objPhoto)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            object[] param = { objPhoto };
            return objPhotoResource.GetPhotoDetail(param);
        }

        /// <summary>
        /// Method to get the list of comments for selected photo
        /// </summary>
        /// <param name="objComment">Object containing PhotoId</param>
        /// <returns>List of comments</returns>
        public List<CommentTributeAdministrator> GetModuleComments(CommentTributeAdministrator objComment)
        {
            CommentResources objCommentResource = new CommentResources();
            object[] param ={ objComment };
            return objCommentResource.GetModuleComments(param);
        }

        /// <summary>
        /// Method to update photo details
        /// </summary>
        /// <param name="objVid">Filled photo entity</param>
        public void UpdatePhotoDetails(Photos objPhotos)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            object[] param = { objPhotos };

            using (TransactionScope trans = new TransactionScope())
            {
                objPhotoResource.UpdatePhotoDetails(param);
                //Transation Commited
                trans.Complete();
            }
        }

        /// <summary>
        /// Method to delete photo
        /// </summary>
        /// <param name="objVid">Filled photo Entity</param>
        public void DeletePhoto(Photos objPhotos)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            object[] param = { objPhotos };

            using (TransactionScope trans = new TransactionScope())
            {
                objPhotoResource.DeletePhoto(param);
                //Transation Commited
                trans.Complete();
            }
        }

        /// <summary>
        /// Method to get the count for number of photos in the selected album.
        /// </summary>
        /// <param name="objPhotoAlbumId">PhotoAlbum entity containing PhotoAlbumId</param>
        /// <returns>Object containing count for number of photos in album.</returns>
        public object GetPhotoCount(PhotoAlbum objPhotoAlbumId)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            object[] param = { objPhotoAlbumId };
            return objPhotoResource.GetPhotoCountForAlbum(param);
        }

        /// <summary>
        /// Method to update photo album details
        /// </summary>
        /// <param name="objVid">Filled photo album entity</param>
        public object UpdatePhotoAlbumDetails(PhotoAlbum objPhotoAlbum)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            object[] param = { objPhotoAlbum };

            using (TransactionScope trans = new TransactionScope())
            {
                //return objPhotoResource.UpdatePhotoAlbumDetails(param);
                object objReturn = objPhotoResource.UpdatePhotoAlbumDetails(param);
                //Transation Commited
                trans.Complete();
                return objReturn;
            }
        }

        /// <summary>
        /// Method to delete photo album
        /// </summary>
        /// <param name="objVid">Filled photo album Entity</param>
        public void DeletePhotoAlbum(PhotoAlbum objPhotoAlbum)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            object[] param = { objPhotoAlbum };
            using (TransactionScope trans = new TransactionScope())
            {
                objPhotoResource.DeletePhotoAlbum(param);
                //Transaction Commited
                trans.Complete();
            }
        }

        /// <summary>
        /// Method to get the list of photoImagess
        /// </summary>
        /// <param name="objPhotoAlbum">Photo entity containing PhotoAlbumId</param>
        /// <returns>List of filled Photo entity</returns>
        public List<Photos> GetPhotoImagesList(Photos objPhoto)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            object[] param = { objPhoto };
            return objPhotoResource.GetPhotoImagesList(param);
        }

        /// <summary>
        /// Method to send email to the list of users
        /// </summary>
        /// <param name="objUsers">User Info entity containing User name and email address</param>
        public void SendEmail(List<UserInfo> objUsers, string strAdded)
        {
            EmailMessages objEmail = EmailMessages.Instance;
            foreach (UserInfo obj in objUsers)
            {
                StringBuilder sbToEmail = new StringBuilder();
                sbToEmail.Append(obj.UserEmail);
                sbToEmail.Append(",");
                //if (strAdded == "PhotoAlbum")
                //    objEmail.SendMessages("" + WebConfig.NotificationEmail + "", sbToEmail.ToString(), "New PhotoAlbum Added", GetEmailBody(obj, strAdded), EmailMessages.TextFormat.Html.ToString());
                //else
                objEmail.SendMessages("Your Tribute<" + WebConfig.NoreplyEmail + ">", sbToEmail.ToString(), "New Photo(s)", GetEmailBody(obj, strAdded), EmailMessages.TextFormat.Html.ToString());
            }
        }

        /// <summary>
        /// Method to send email to the list of users on adding photos only.
        /// </summary>
        /// <param name="objUsers">User Info entity containing User name and email address</param>
        public void SendEmail(List<UserInfo> objUsers, string strAdded, Photos objPhoto)
        {
            EmailMessages objEmail = EmailMessages.Instance;
            foreach (UserInfo obj in objUsers)
            {
                StringBuilder sbToEmail = new StringBuilder();
                sbToEmail.Append(obj.UserEmail);
                sbToEmail.Append(",");
                objEmail.SendMessages("Your Tribute<" + WebConfig.NoreplyEmail + ">", sbToEmail.ToString(), GetEmailSubject(objPhoto), GetEmailBody(obj, strAdded, objPhoto), EmailMessages.TextFormat.Html.ToString());
            }
        }

        /// <summary>
        /// Method to send email to the list of users on adding photo album.
        /// </summary>
        /// <param name="objUsers">User Info entity containing User name and email address</param>
        public void SendEmail(List<UserInfo> objUsers, string strAdded, PhotoAlbum objPhotoAlbum)
        {
            EmailMessages objEmail = EmailMessages.Instance;
            foreach (UserInfo obj in objUsers)
            {
                StringBuilder sbToEmail = new StringBuilder();
                sbToEmail.Append(obj.UserEmail);
                sbToEmail.Append(",");
                //if (strAdded == "PhotoAlbum")
                objEmail.SendMessages("Your Tribute<" + WebConfig.NoreplyEmail + ">", sbToEmail.ToString(), GetEmailSubject(objPhotoAlbum), GetEmailBody(obj, strAdded, objPhotoAlbum), EmailMessages.TextFormat.Html.ToString());
                //else
                //    objEmail.SendMessages("" + WebConfig.NotificationEmail + "", sbToEmail.ToString(), "New Photo(s)", GetEmailBody(obj, strAdded), EmailMessages.TextFormat.Html.ToString());
            }
        }

        /// <summary>
        /// Method to get the body part of email on adding photos only.
        /// </summary>
        /// <param name="objUserInfo">Filled User Info entity</param>
        /// <returns>HTML string of body part</returns>
        private string GetEmailBody(UserInfo objUserInfo, string strAdded, Photos objPhoto)
        {
            StringBuilder sbBody = new StringBuilder();
            sbBody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>" +objPhoto.UserName);
            sbBody.Append(" added new photos in the ");
            sbBody.Append(objPhoto.TributeName + " " + objPhoto.TributeType + "  Tribute.</p>");
            sbBody.Append("<p>To view the photos, follow the link below:");
            sbBody.Append("<br/>");
            string strLink = "http://" + objPhoto.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objPhoto.TributeUrl + "/Photo.aspx" + "?PhotoId=" + objPhoto.PhotoId;
            //sbBody.Append("<a href='http://" + objPhoto.PathToVisit + "/Photo/PhotoView.aspx" + "?PhotoId=" + objPhoto.PhotoId + "&TributeId=" + objPhoto.UserTributeId + "&TributeName=" + objPhoto.TributeName + "&TributeType=" + objPhoto.TributeType + "&TributeUrl=" + objPhoto.TributeUrl + "&mode=link'>" + "Click here to visit the link</a>");
            //sbBody.Append("<a href='http://" + objPhoto.TributeType + "." + WebConfig.TopLevelDomain + "/" + objPhoto.TributeUrl + "/Photo.aspx" + "?PhotoId=" + objPhoto.PhotoId + "'>" + "Click here to visit the link</a>");
            sbBody.Append("<a href='" + strLink + "'>" + strLink + "</a></p>");
            sbBody.Append("<p>---<br/>");
            sbBody.Append("Your Tribute Team</p></font>");

            return sbBody.ToString();
        }

        /// <summary>
        /// Method to get the body part of email on adding photos only.
        /// </summary>
        /// <param name="objUserInfo">Filled User Info entity</param>
        /// <returns>HTML string of body part</returns>
        private string GetEmailBody(UserInfo objUserInfo, string strAdded, PhotoAlbum objAlbum)
        {
            StringBuilder sbBody = new StringBuilder();
            sbBody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + objAlbum.UserName);
            sbBody.Append(" added a new photo album in the ");
            sbBody.Append(objAlbum.TributeName + " " + objAlbum.TributeType + "  Tribute.</p>");
            sbBody.Append("<p>To view the photo album, follow the link below:");
            sbBody.Append("<br/>");
            string strLink = "http://" + objAlbum.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objAlbum.TributeUrl + "/photoalbum.aspx" + "?PhotoAlbumId=" + objAlbum.PhotoAlbumId;
            //sbBody.Append("<a href='http://" + objAlbum.PathToVisit + "/Photo/PhotoAlbum.aspx" + "?PhotoAlbumId=" + objAlbum.PhotoAlbumId + "&TributeId=" + objAlbum.UserTributeId + "&TributeName=" + objAlbum.TributeName + "&TributeType=" + objAlbum.TributeType + "&TributeUrl=" + objAlbum.TributeUrl + "&mode=link'>" + "Click here to visit the link</a>");
            //sbBody.Append("<a href='http://" + objAlbum.TributeType + "." + WebConfig.TopLevelDomain + "/" + objAlbum.TributeUrl  + "/photoalbum.aspx" + "?PhotoAlbumId=" + objAlbum.PhotoAlbumId + "'>" + "Click here to visit the link</a>");
            sbBody.Append("<a href='" + strLink + "'>" + strLink + "</a></p>");
            sbBody.Append("<p>---<br/>");
            sbBody.Append("Your Tribute Team</p></font>");

            return sbBody.ToString();
        }

        /// <summary>
        /// Method to get the body part of email.
        /// </summary>
        /// <param name="objUserInfo">Filled User Info entity</param>
        /// <returns>HTML string of body part</returns>
        private string GetEmailBody(UserInfo objUserInfo, string strAdded)
        {
            StringBuilder sbBody = new StringBuilder();
            sbBody.Append(" added a new photo in the tribute ");
            sbBody.Append("<br/>");
            sbBody.Append("<br/>");
            sbBody.Append("<br/>");
            sbBody.Append("---");
            sbBody.Append("<br/>");
            sbBody.Append("Your Tribute Team");

            return sbBody.ToString();
        }

        /// <summary>
        /// Method to get subject for email on adding album.
        /// </summary>
        /// <param name="objComment"></param>
        /// <returns>Subject of email in string format.</returns>
        private string GetEmailSubject(PhotoAlbum objAlbum)
        {
            return objAlbum.UserName + " added a new photo album on Your Tribute...";
        }

        /// <summary>
        /// Method to get subject for email on adding photos only.
        /// </summary>
        /// <param name="objComment"></param>
        /// <returns>Subject of email in string format.</returns>
        private string GetEmailSubject(Photos objPhoto)
        {
            return objPhoto.UserName + " added new photos on Your Tribute...";
        }

        /// <summary>
        /// Method to get the list of photo albums
        /// </summary>
        /// <param name="objPhotoAlbum">PhotoAlbum entity containing TributeId</param>
        /// <returns>List of filled PhotoAlbum entity</returns>
        public List<PhotoAlbum> GetPhotoAlbumList(PhotoAlbum objPhotoAlbum)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            object[] param = { objPhotoAlbum };
            return objPhotoResource.GetPhotoAlbumList(param);
        }


        public void SendPhotoAlbumEmail(PhotoAlbum objPhotoAlbum)
        {
            //to send mail
            TributeResource objTribute = new TributeResource();
            Tributes objTrib = new Tributes();
            objTrib.TributeId = int.Parse(objPhotoAlbum.UserTributeId.ToString());
            objTrib.TypeDescription = objPhotoAlbum.ModuleTypeName;
            object[] paramTrib = { objTrib };
            List<UserInfo> objUser = objTribute.GetTributeAdministrators(paramTrib);
            if (objUser.Count > 0)
            {
                SendEmail(objUser, "PhotoAlbum", objPhotoAlbum);
            }

            //Function to send the mail to the list of users who have added the Tribute in their list of favourites 
            List<UserInfo> objUserFav = objTribute.GetFavouriteTributeUsers(paramTrib);
            if (objUserFav.Count > 0)
            {
                //As per discussion with Rupendra: will send the mail in "To" field. 
                //ie a comma separated list of users in the "to" field
                SendEmail(objUserFav, "PhotoAlbum", objPhotoAlbum);
            }
        }

        public void SendPhotoEmail(List<Photos> objPhotoList)
        {
            //to send mail
            TributeResource objTribute = new TributeResource();
            Tributes objTrib = new Tributes();
            objTrib.TributeId = int.Parse(objPhotoList[0].UserTributeId.ToString());
            objTrib.TypeDescription = objPhotoList[0].ModuleTypeName;
            object[] paramTrib = { objTrib };
            List<UserInfo> objUser = objTribute.GetTributeAdministrators(paramTrib);
            if (objUser.Count > 0)
            {
                Photos objPhoto = objPhotoList[objPhotoList.Count - 1];
                SendEmail(objUser, "Photo", objPhoto);
            }

            //Function to send the mail to the list of users who have added the Tribute in their list of favourites 
            List<UserInfo> objUserFav = objTribute.GetFavouriteTributeUsers(paramTrib);
            if (objUserFav.Count > 0)
            {
                Photos objPhoto = objPhotoList[objPhotoList.Count - 1];
                //As per discussion with Rupendra: will send the mail in "To" field. 
                //ie a comma separated list of users in the "to" field
                SendEmail(objUserFav, "Photo", objPhoto);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tributeId"></param>
        /// <returns></returns>
        public string GetTributeEndDate(int _tributeId)
        {
            PhotoResource objPhotoResource = new PhotoResource();

            return objPhotoResource.GetTributeEndDate(_tributeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool GetCustomHeaderDetail(int tributeId)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            return objPhotoResource.GetCustomHeaderDetail(tributeId);
        }
        public int CreatePhotoAlbum(PhotoAlbum objPhotoAlbum)
        {
            PhotoResource objPhotoResource = new PhotoResource();
            object photoAlbumId = 0;
            object[] param = { objPhotoAlbum };
            using (TransactionScope trans = new TransactionScope())
            {
                photoAlbumId = objPhotoResource.AddPhotoAlbum(param);
                //Transaction Commited
                trans.Complete();
            }

            return int.Parse(photoAlbumId.ToString());
        }
        public int AddPhotos(Photos objPhoto)
        {
            //Cannot convert type 'TributesPortal.BusinessEntities.Photos' to 'System.Collections.Generic.List<TributesPortal.BusinessE
            PhotoResource objPhotoAlbumResource = new PhotoResource();
            IList<Photos> objLPhoto = new List<Photos>();
            objLPhoto.Add(objPhoto);
            //IList<Photos> objListPhoto = (IList<Photos>)objLPhoto[0];
            object[] param = { objLPhoto };

            object objLastPhotoId = new object();
            using (TransactionScope trans = new TransactionScope())
            {
                objLastPhotoId = objPhotoAlbumResource.AddPhotoToAlbum(param);
                //Transaction Commited
                trans.Complete();
            }
            return int.Parse(objLastPhotoId.ToString());
        }
    }//end class
}//end namespace
