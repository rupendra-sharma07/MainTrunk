///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.IAdminProfileEmail.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Admin Profile Email pages.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.MyHome.Views
{
    public interface IAdminProfileEmail
    {
        string BannerMessage { get;set;}
        int UserId { get;set;}
        bool StoryNotify { get;set;}
        bool NotesNotify { get;set;}
        bool EventsNotify { get;set;}
        bool GuestBookNotify { get;set;}
        bool GiftsNotify { get;set;}
        bool PhotoAlbumNotify { get;set;}
        bool PhotosNotify { get;set;}
        bool VideosNotify { get;set;}
        bool CommentsNotify { get;set;}
        bool MessagesNotify { get;set;}
        bool NewsLetterNotify { get;set;}
    }
}
