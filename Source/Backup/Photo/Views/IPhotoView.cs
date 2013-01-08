///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Photo.Views.IPhotoView.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the viewing a Photo. 
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Photo.Views
{
    public interface IPhotoView
    {
        List<CommentTributeAdministrator> Comments { set;}
        Photos PhotoDetails { set; }
        string RecordCount { set;}
        string DrawPaging { set;}
        string SetRecordCount { set;}
        int NextCount { set; }
        int PrevCount { set;}
        string XmlFilePath { set;}
        int RecordNumber{set;}

        int CurrentPage { get; }
        int PhotoId { get;}
        int UserId { get;}
        int TributeId { get;}
        string TributeName { get; }
        string TributeType { get; }
        string TributeUrl { get; }

        int PageSize { get; set;}
        int CommentCount { set; get; }
    }
}




