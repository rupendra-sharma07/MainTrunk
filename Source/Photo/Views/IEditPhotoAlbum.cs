///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Photo.Views.IEditPhotoAlbum.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the editing a Photo album. 
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Photo.Views
{
    public interface IEditPhotoAlbum
    {
        int PhotoAlbumId { get;}
        int UserId { get;}
        int TributeId { get; }
        string TributeName { get; }
        string TributeType { get; }
        string PhotoAlbumCaption { get;}
        string PhotoAlbumDesc { get;}

        PhotoAlbum PhotoAlbumDetails { set;}
        string PhotoAlbumList { set;}
        //string Failure { set;}
    }
}




