///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.IAdminProfileSettings.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Admin Profile Settings.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.MyHome.Views
{
    public interface IAdminProfileSettings
    {
        IList<Locations> Locations { set; }
        IList<Locations> States { set; }
        IList<ParameterTypesCodes> Business { set; }
        string BannerMessage { get;set;}
        int UserId { get;set;}
        string UserName { get;set;}
        Nullable<Int64> FacebookUid {get;set;}
        string FirstName { get;set;}
        string LastName { get;set;}
        string UserImage { get;set;}
        string City { get;set;}
        Nullable<int> State { get;set;}
        Nullable<int> Country { get;set;}
        string Website { get;set;}
        string CompanyName { get;set;}
        int BusinessType { get;set;}
        string BusinessAddress { get;set;}
        string Phone { get;set;}
        string ZipCode { get; set; }
        //LHK-26Oct-CheckBox for AdminHeaderCreation Functionality.
        string HeaderBGColor { get;set; }
        bool IsAddressOn { get; set; }
        bool IsWebAddressOn { get; set; }
        bool IsObUrlLinkOn { get; set; }
        bool IsPhoneNoOn  { get; set; }
        bool DisplayCustomHeader { get; set; }
        string HeaderLogo { get; set; }
        string ObituaryLinkPage { get; set; }
        //Till Here
        bool PanelVisibility { get;set;}
        bool IsUsernameVisiable { get;set;}
        bool AllowIncomingMsg { get;set;}
        bool IsLocationHide { get;set;}
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
        

        IList<GiftImage> ImageList { set;}
    }

}
