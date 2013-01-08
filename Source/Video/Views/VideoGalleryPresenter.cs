///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Video.Views.VideoGalleryPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for viewing all the uploaded videos in the video gallery.
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
    public class VideoGalleryPresenter : Presenter<IVideoGallery>
    {
        #region CLASS VARIABLES
        private VideoController _controller;
        #endregion

        #region CONSTRUCTOR
        public VideoGalleryPresenter([CreateNew] VideoController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Method to get the list of videos in the Tribute
        /// </summary>
        /// <param name="objVideoGallery">Video Gallery entity containing UserTributeId</param>
        public void GetVideosOfGallery(VideoGallery objVideoGallery)
        {
            List<VideoGallery> objVideos = _controller.GetVideosOfTribute(objVideoGallery);
            //int endCount = 0;
            if (objVideos.Count > 0)
            {
                this.View.TotalRecords = objVideos[0].TotalRecords;
                
                //this.View.IsHavingRecords = true;
                this.View.VideoGalleryVideos = objVideos;  //_controller.GetVideosOfTribute(objVideoGallery);
                this.View.DrawPaging = GetPaging(objVideos[0].TotalRecords, objVideoGallery.PageSize, objVideoGallery.PageNumber);
                this.View.RecordCount = GetNotesCount(objVideoGallery.PageNumber, objVideoGallery.PageSize, objVideos.Count, objVideos[0].TotalRecords);
            }
            else
            {
                //this.View.IsHavingRecords = false;
                this.View.VideoGalleryVideos = null;
                this.View.TotalRecords = 0;
            }
        }

        public void GetVideoTributeDetails(Videos objVideo)
        {
            VideoGallery objVideoTribute = _controller.GetVideoTributeDetails(objVideo);
            if (!Equals(objVideoTribute, null))
            {
                this.View.VideoTributeDetails = objVideoTribute;
                this.View.PackageId = _controller.GetPackage(objVideoTribute.Videos.UserTributeId);
            }
            else
                this.View.VideoTributeDetails = null;
        }

        // to get data about one video by VideoId
        public VideoGallery GetVideoDetails(VideoGallery objVideo)
        {
            VideoGallery objVideoGallery = _controller.GetVideoDetails(objVideo);
            return objVideoGallery;
        }

        /// <summary>
        /// Method to get the method count text
        /// </summary>
        /// <param name="currentPage">Current page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="recordCount">Number of records to be displayed</param>
        /// <param name="totalRecords">Total number of records</param>
        /// <returns>String containing text to display</returns>
        private string GetNotesCount(int currentPage, int pageSize, int recordCount, int totalRecords)
        {
            int endCount = 0;
            string strMessage;
            int startCount = currentPage == 1 ? 1 : currentPage * pageSize - (pageSize - 1);

            if (recordCount < pageSize)
                endCount = currentPage * pageSize - (pageSize - recordCount);
            else
                endCount = currentPage * pageSize;

            if (recordCount > 1)
                strMessage = ResourceText.GetString("lblVideoHeader_VG") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_TN") + " " + endCount.ToString() + " " + ResourceText.GetString("strOf_TN") + " " + totalRecords;
            else
                strMessage = ResourceText.GetString("lblVideoPage_VG") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_TN") + " " + endCount.ToString() + " " + ResourceText.GetString("strOf_TN") + " " + totalRecords;

            return strMessage;
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
            return objUtilities.DrawPaging(currentPage, pageCount, "videos.aspx");
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public string GetTributeEndDate(int tributeId)
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

        public int GetCurrentVideos(int _tributeId)
        {
            return _controller.GetCurrentVideos(_tributeId);
        }

        #endregion


        
    }//end class
}//end namespace




