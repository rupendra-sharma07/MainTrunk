///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Photo.Views.IPhotoAlbum.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Photo Album pages.
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Photo.Views
{
    public interface IPhotoAlbum
    {
        PhotoAlbum PhotoAlbumDetails {set;}
        List<Photos> PhotosList {set;}
        //List<Photos> PhotoImageList { set;}
        int TotalRecords { set; }
        string DrawPaging { set; }
        string XmlFilePath { set;}
        string RecordCount { set; }
        string TributeName { get; }
        string TributeType { get; }
        string TributeUrl { get; }
    }
}




