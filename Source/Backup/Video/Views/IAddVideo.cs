///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Video.Views.IAddVideo.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Add Video
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Video.Views
{
    public interface IAddVideo
    {
        VideoGallery VideoDetails { set; get;}
        bool IsVideoTribute { set;}
    }
}




