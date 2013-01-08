///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Photo.Views.IPhotoGallery.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Photo Gallery pages.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Photo.Views
{
    public interface IPhotoGallery
    {
        List<PhotoAlbum> PhotoAlbumList { set; }
        int TotalRecords { set; }
        string DrawPaging { set; }
        string RecordCount { set; }
        string TributeName { get; }
        string TributeType { get; }
        string TributeUrl { get; }
    }
}




