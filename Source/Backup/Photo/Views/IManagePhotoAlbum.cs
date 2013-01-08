///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Photo.Views.IManagePhotoAlbum.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the manage Photo Album pages. 
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Photo.Views
{
    public interface IManagePhotoAlbum
    {
        int PhotoAlbumId { get; }
        int TributeId { get; }

        int PhotoCount { set; }
        string PhotoAlbumList { set; }

        PhotoAlbum PhotoAlbumDetails { set; get; }
        List<Photos> PhotoList { set; get; }

        int Result { set; get; }
        void SaveFilesOnStorage(List<Photos> objPhotoList);

        int PhotoAlbumID { set; get; }
        int ExistingPhotoCount { get; }
        int UploadedPhotoCount { set; get; }
        int UploadedPhotoID { set; get; }
    }
}




