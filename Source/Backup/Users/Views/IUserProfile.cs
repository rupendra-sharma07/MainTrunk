///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Users.Views.IUserProfile.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the User Profile
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using TributesPortal.BusinessEntities;
namespace TributesPortal.Users.Views
{
    public interface IUserProfile
    {
        IList<Locations> Locations { set; }
        IList<Locations> States { set; }
        IList<ParameterTypesCodes> Business { set; }
        IList<BillingHistory> BillingInformation { set; }
        IList<ParameterTypesCodes> PaymentModes { set; }

        //IList<ParameterTypesCodes> TributeNotification { set;get; }
        //IList<ParameterTypesCodes> GeneralNotification { set;get; }
        string BannerMessage { get;set;}
        int UserId { get;set;}
        string UserName { get;set;}
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
        string HeaderBGColor { get; set; }
        string ZipCode { get;set;}

        //LHK: Added for Video tribute - Start
        bool IsAddressOn { get; set; }
        bool IsWebAddressOn { get; set; }
        bool IsObUrlLinkOn { get; set; }
        bool IsPhoneNoOn { get; set; }
        bool DisplayCustomHeader { get; set; }
        string HeaderLogo { get; set; }
        string ObituaryLinkPage { get; set; }

        //-end 

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
        // Change eMail/Password
        string Email {get;}
        string Password{get;}      
       // Billing informaion
        string CardNumber { get;set;}
        string CardName { get;set;}
        DateTime ExpiryDate { get;set; }
        string Telephone{get;set;}
        string PaymentMethod { get;set;}
    }
}
