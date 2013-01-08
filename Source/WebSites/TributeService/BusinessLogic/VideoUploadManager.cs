#region USING DIRECTIVES
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TributeService.BusinessEntities;
using TributeService.ResourceAccessLayer;
#endregion

namespace TributeService.BusinessLogic
{
    public class VideoUploadManager
    {
        public string UpdateVideoStatus(VideoStatus objStatus)
        {
            VideoUploadResource objVideoUpload = new VideoUploadResource();
            return objVideoUpload.UpdateVideoStatus(objStatus);
        }

    }
}
