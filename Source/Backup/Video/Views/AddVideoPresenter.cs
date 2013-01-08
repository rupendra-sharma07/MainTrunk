///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Video.Views.AddVideoPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for adding a video  to the tribute.
///Audit Trail     : Date of Modification  Modified By         Description
#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Video.Views
{
    public class AddVideoPresenter : Presenter<IAddVideo>
    {
        #region CLASS VARIABLES
        private VideoController _controller;
        #endregion

        #region CONSTRUCTOR
        public AddVideoPresenter([CreateNew] VideoController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Method to save video
        /// </summary>
        /// <param name="objVideo">Filled Video entity</param>
        /// <returns>Object containing video id</returns>
        public bool SaveVideo(Videos objVideo)
        {
           return _controller.SaveVideo(objVideo,"Video");
        }

        /// <summary>
        /// Method to get the video details
        /// </summary>
        /// <param name="objVideo">Video Gallery entity conatining video id</param>
        public void GetVideoDetails()
        {
            VideoGallery objVideoDetails = _controller.GetVideoDetails(this.View.VideoDetails);
            if (objVideoDetails.Videos.TributeVideoId != string.Empty)
                this.View.IsVideoTribute = true;
            else
                this.View.IsVideoTribute = false;

            this.View.VideoDetails = objVideoDetails;
        }

        /// <summary>
        /// Method to update the video details 
        /// </summary>
        /// <param name="objVideo">Filled Video entity</param>
        public object UpdateVideoDetails(Videos objVideo)
        {
           return _controller.UpdateVideoDetails(objVideo);
        }

        /// <summary>
        /// Method to update the video tribute details 
        /// </summary>
        /// <param name="objVideo">Filled Video entity</param>
        public object UpdateVideoTributeDetails(Videos objVideo)
        {
            return _controller.UpdateVideoTributeDetails(objVideo);
        }

        /// <summary>
        /// Method to delete video
        /// </summary>
        /// <param name="objVideo">Video entity containing video id</param>
        public void DeleteVideo(Videos objVideo)
        {
            _controller.DeleteVideo(objVideo);
        }
        #endregion
    }
}




