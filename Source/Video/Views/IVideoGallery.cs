///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Video.Views.IVideoGallery.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Video Gallery
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Video.Views
{
    public interface IVideoGallery
    {
        IList<VideoGallery> VideoGalleryVideos { set; }
        VideoGallery VideoTributeDetails { set;}
        //bool IsHavingRecords { set; }
        int TotalRecords { set;}
        string RecordCount { set;}
        string DrawPaging { set; }
        int PackageId { set; }
    }
}




