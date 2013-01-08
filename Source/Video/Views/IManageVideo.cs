///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Video.Views.IManageVideo.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Manage Video
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Video.Views
{
    public interface IManageVideo
    {
        //int ModifiedBy { get;}
        int UserId { get;}
        //string VideoDesc { get;}
        //string VideoUrl { get;}
        //string VideoTypeId { get;}
        //DateTime ModifiedDate { get;}
        string NextPrev { get;}

        //int CreatedBy { set;}
        //int TotalRecords { set; }
        int NextCount { set; }
        int PrevCount { set;}
        //int IsAdmin { set;}
        string SetRecordCount { set;}
        List<VideoGallery> VideoDetails { set;}
        List<CommentTributeAdministrator> Comments { set;}
        string RecordCount { set;}
        string DrawPaging { set;}

        int VideoId { get; set;}
        //string VideoCaption { set; get;}
        int CommentCount { set; get; }
        int? UserTributeId { get; set; }
        int PackageId { set; }

    }
}




