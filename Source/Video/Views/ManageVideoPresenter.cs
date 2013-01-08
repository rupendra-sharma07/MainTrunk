///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Video.Views.ManageVideoPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for managing the videos/video tributes.
///Audit Trail     : Date of Modification  Modified By         Description
#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
#endregion

namespace TributesPortal.Video.Views
{
    public class ManageVideoPresenter : Presenter<IManageVideo>
    {
        #region CLASS VARIABLES
        private VideoController _controller;
        #endregion

        #region CONSTRUCTOR
        public ManageVideoPresenter([CreateNew] VideoController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        
        /// <summary>
        /// Method to get the Video details
        /// </summary>
        public void GetVideoDetails(CommentTributeAdministrator objComment)
        {
            //set input values for SP
            Videos objVideo = new Videos();
            objVideo.VideoId = this.View.VideoId;
            objVideo.UserId = this.View.UserId;
            VideoGallery objVideoGallery = new VideoGallery();
            objVideoGallery.NextPrev = this.View.NextPrev;
            objVideoGallery.Videos = objVideo;//this.View.UserTributeId;

            //get value from SP
            objVideoGallery = _controller.GetVideoDetails(objVideoGallery);

            if (objVideoGallery.CreatedDate != null)
            {
                //fetch values to display on page
                List<VideoGallery> objVideoDetails = new List<VideoGallery>();
                objVideoDetails.Add(objVideoGallery);
                this.View.VideoId = (int)objVideoGallery.Videos.VideoId;
                //this.View.VideoCaption = objVideoGallery.Videos.VideoCaption; //to set value to Video Caption in View mode
                this.View.VideoDetails = objVideoDetails; //to set values to data list in View Mode
                //this.View.TotalRecords = objVideoGallery.TotalRecords;
                //this.View.IsAdmin = objVideoGallery.IsAdmin;
                //this.View.CreatedBy = (int)objVideoGallery.Videos.CreatedBy;
                this.View.NextCount = objVideoGallery.NextRecordCount;
                this.View.PrevCount = objVideoGallery.PrevRecordCount;
                this.View.SetRecordCount = ResourceText.GetString("lblVideo_MV") + " " + (objVideoGallery.TotalRecords - objVideoGallery.RecordNumber + 1) + " " + ResourceText.GetString("lblOf_MV") + " " + objVideoGallery.TotalRecords;

                this.View.CommentCount = objVideoGallery.CommentCount;

                this.View.UserTributeId = objVideoGallery.Videos.UserTributeId;
                LoadComments(objComment);
            }
            else
                this.View.VideoDetails = null;
        }

        /// <summary>
        /// Method to update the video details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateVideo(object sender, EventArgs e)
        {
            //Videos objVideoDetails = new Videos();
            //objVideoDetails.VideoId = this.View.VideoId;
            //objVideoDetails.VideoCaption = this.View.VideoCaption;
            //objVideoDetails.VideoDesc = this.View.VideoDesc;
            //objVideoDetails.VideoUrl = this.View.VideoUrl;
            //objVideoDetails.VideoTypeId = this.View.VideoTypeId;
            //objVideoDetails.ModifiedBy = this.View.ModifiedBy;
            //objVideoDetails.ModifiedDate = this.View.ModifiedDate;
            //_controller.UpdateVideoDetails(objVideoDetails);
        }

        /// <summary>
        /// Method to delete video.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeleteVideo(object sender, EventArgs e)
        {
            Videos objVideo = new Videos();
            objVideo.VideoId = this.View.VideoId;
            objVideo.IsDeleted = true;
            _controller.DeleteVideo(objVideo);
        }

        public void LoadComments(CommentTributeAdministrator objNoteComment)
        {
            //to get the comments list
            List<CommentTributeAdministrator> objComments = _controller.GetModuleComments(objNoteComment); // _guestBookController.CommentList(objNoteComment);

            //to change the \n from message to </br>
            List<CommentTributeAdministrator> objUpdatedComments = new List<CommentTributeAdministrator>();
            foreach (CommentTributeAdministrator obj in objComments)
            {
                CommentTributeAdministrator objComment = new CommentTributeAdministrator();
                objComment.City = obj.City;
                objComment.CommentId = obj.CommentId;
                objComment.CommentTypeId = obj.CommentTypeId;
                objComment.Country = obj.Country;
                objComment.CreatedBy = obj.CreatedBy;
                objComment.CreatedDate = obj.CreatedDate;
                objComment.CurrentPage = obj.CurrentPage;
                objComment.IsAdmin = obj.IsAdmin;
                objComment.IsPrivate = obj.IsPrivate;
                objComment.Message = obj.Message.Replace("\n", "</br>");
                objComment.PageSize = obj.PageSize;
                objComment.State = obj.State;
                objComment.TotalRecords = obj.TotalRecords;
                objComment.TributeId = obj.TributeId;
                objComment.TypeCodeId = obj.TypeCodeId;
                objComment.UserId = obj.UserId;
                if ((obj.UserImage.StartsWith("http://") || obj.UserImage.StartsWith("https://")))
                {
                    objComment.UserImage = obj.UserImage;
                }
                else
                {
                    objComment.UserImage = CommonUtilities.GetPath()[2].ToString() + obj.UserImage;
                }
                objComment.FacebookUid = obj.FacebookUid;
                objComment.UserName = obj.UserName;
                objComment.UserType = obj.UserType;

                if (obj.IsLocationHide)
                {
                    objComment.Location = string.Empty;
                }
                else
                {

                    var sLocation = string.Empty;
                    sLocation += " (";
                    sLocation += !string.IsNullOrEmpty(obj.City)
                                     ? ((obj.City) + ", ")
                                     : "";
                    sLocation += !string.IsNullOrEmpty(obj.State)
                                     ? ((obj.State) + ", ")
                                     : "";
                    sLocation += !string.IsNullOrEmpty(obj.Country)
                                    ? (obj.Country)
                                    : "";
                    sLocation = sLocation.TrimEnd(',',' ');
                    sLocation += ")";

                    objComment.Location = sLocation;

                    //if (obj.City == string.Empty && obj.State == string.Empty)
                    //    objComment.Location = "(" + obj.Country + ")";
                    //else
                    //    if (obj.City == string.Empty && obj.State != string.Empty)
                    //        objComment.Location = "(" + obj.State + ", " + obj.Country + ")";
                    //    else if (obj.City != string.Empty && obj.State == string.Empty)
                    //        objComment.Location = "(" + obj.City + ", " + obj.Country + ")";
                    //    else
                    //        objComment.Location = "(" + obj.City + ", " + obj.State + ", " + obj.Country + ")";


                }

                objUpdatedComments.Add(objComment);
            }

            this.View.Comments = objUpdatedComments;

            //to display the Message count
            this.View.RecordCount = GetMessageCount(objNoteComment.CurrentPage, objNoteComment.PageSize, objComments.Count, this.View.CommentCount);

            //to display the Paging
            this.View.DrawPaging = GetPaging(this.View.CommentCount, objNoteComment.PageSize, objNoteComment.CurrentPage);
        }

        /// <summary>
        /// Method to save comment on video
        /// </summary>
        /// <param name="objComment">Filled Comment entity</param>
        public void SaveComment(Comments objComment)
        {
            _controller.SaveComment(objComment);
        }

        /// <summary>
        /// Method to save comment on video
        /// </summary>
        /// <param name="objComment">Filled Comment entity</param>
        public void SaveComment(Comments objComment, string _topUrl)
        {
            _controller.SaveComment(objComment, _topUrl);
        }

        /// <summary>
        /// Method to delete comment on note
        /// </summary>
        /// <param name="objComment">Filled Comment entity</param>
        public void DeleteComment(Comments objComment)
        {
            _controller.DeleteComment(objComment);
        }

        /// <summary>
        /// Method to create string for paging
        /// </summary>
        /// <param name="totalRecords">Total number of records</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="currentPage">Current size</param>
        /// <returns>string containing paging</returns>
        private string GetPaging(int totalRecords, int pageSize, int currentPage)
        {
            int pageCount = 0;
            if (totalRecords % pageSize == 0)
                pageCount = totalRecords / pageSize;
            else
                pageCount = (totalRecords / pageSize) + 1;

            CommonUtilities objUtilities = new CommonUtilities();
            return objUtilities.DrawPaging(currentPage, pageCount, "video.aspx");

            //return DrawPaging(currentPage, pageCount);
        }

        /// <summary>
        /// Method to get the method count text
        /// </summary>
        /// <param name="currentPage">Current page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="recordCount">Number of records to be displayed</param>
        /// <param name="totalRecords">Total number of records</param>
        /// <returns>String containing text to display</returns>
        private string GetMessageCount(int currentPage, int pageSize, int recordCount, int totalRecords)
        {
            int endCount = 0;
            string strMessage;
            int startCount = currentPage == 1 ? 1 : currentPage * pageSize - (pageSize - 1);

            if (recordCount < pageSize)
                endCount = currentPage * pageSize - (pageSize - recordCount);
            else
                endCount = currentPage * pageSize;

            if (recordCount > 1)
                strMessage = ResourceText.GetString("strMessages_GB") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_GB") + " " + endCount.ToString() + " " + ResourceText.GetString("strOf_GB") + " " + totalRecords;
            else
                strMessage = ResourceText.GetString("strMessage_GB") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_GB") + " " + endCount.ToString() + " " + ResourceText.GetString("strOf_GB") + " " + totalRecords;

            return strMessage;
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public string GetTributeEndDate( int tributeId)
        {
            return _controller.GetTributeEndDate(tributeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool GetCustomHeaderDetail(int tributeId)
        {
            return _controller.GetCustomHeaderDetail(tributeId);
        }

        /// <summary>
        /// Addded to copy folder from new location
        /// </summary>
        /// <param name="_tributeId"></param>
        /// <returns></returns>
        public string GetOldTributeUrlOnTributeId(int _tributeId)
        {
            return _controller.GetOldTributeUrlOnTributeId(_tributeId);
        }

        /// <summary>
        /// SendMailToAdmin for directory copy.
        /// </summary>
        /// <param name="OldTributeURL"></param>
        /// <param name="NewtributeURL"></param>
        /// <param name="tributeType"></param>
        public void SendMailToAdmin(string link,string Videolink, string NewtributeURL, string tributeType)
        {
            _controller.SendMailToAdmin(link, Videolink,NewtributeURL, tributeType);
        }

        /// <summary>
        /// error reporting SendMailToAdmin for directory copy.
        /// </summary>
        /// <param name="OldTributeURL"></param>
        /// <param name="NewtributeURL"></param>
        /// <param name="tributeType"></param>
        public void SendMailToAdmin(string NewtributeURL, string errorString)
        {
            _controller.SendMailToAdmin(NewtributeURL, errorString);
        }
        #endregion



        public void GetPackage()
        {
            this.View.PackageId =  _controller.GetPackage(this.View.UserTributeId);
        }
    }//end class
}//end namespace




