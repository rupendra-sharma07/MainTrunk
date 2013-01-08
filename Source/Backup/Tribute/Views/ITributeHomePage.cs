///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.ITributeHomePage.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Tribute home page.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using System.Collections;

namespace TributesPortal.Tribute.Views
{
    public interface ITributeHomePage
    {
        IList<UserInfo> OtherAdmins { set;}
        IList<TributeLatest> TodayVideos { set;}
        IList<TributeLatest> YesterdayLatest { set;}
        IList<TributeLatest> ThirdLatest { set;}
        string AdminOwner { get; set;}
        string AdminOwnerId { get; set;}
        int TributeId { get;}
        int UserID { get;}

        //Start - Modification on 7-Dec-09 for the enhancement 5 of the Phase 1
        string TributeCreatedByOrProvidedBy { set; }
        //End

        //Start - Modification on 17-Dec-09 for the enhancement 6 of the Phase 1
        string CompanyLogo { set; }
        //End

        string TributeName { set; }
        string TributeMessage { get; set;}
        string TributeType { get; set;}
        Nullable<DateTime> Date1 { set;}
        Nullable<DateTime> Date2 { set;}
        string City { set;}
        string TributeImage { get; set;}
        int PackageId { get; set;}
        Nullable<DateTime> EndDate { set;}
        string NotePostMessage { set; }
        Nullable<DateTime> NoteCreatedDate { set;}
        string NoteTitle { set; }
        string NotesId { set; }
        bool isStory { set;}
        int VisitCount { set;}
        bool IsStoryAdded { set;}
        bool IsStoryHide { set;}

        string PostMessage { get;}
        int ToUserId { get; }
        string Subject { get;}

        string TypeDescription { get;set; }

        // CCinformation
        //IList<ParameterTypesCodes> PaymentModes { set; }
        //IList<Locations> CCCountryList { set; }
        //IList<Locations> CCStateList { set; }
        //string SelectedCCCountry { get;}
        //string SelectedCCState { get;}
        //string SelectedCCCity { get;}
        //string CreditCardNo { get;}
        //string CardholdersName { get;}
        //DateTime ExpirationDate { get; }
        //string Telephone { get;}
        //string PaymentMethod { get;}
        //string Address { get;}
        //string ZipCode { get;}
        //bool NotifyBeforeRenew { get; set;}
        //bool IsCardDetailsReusable { get;}
        //int getPackageId { get;}
        //bool IsSponserHide { get;}
        IList<Photos> TodayAlbumPhotos { set; }
        IList<Photos> YesterdayAlbumPhotos { set; }
        IList<Photos> ThirdAlbumPhotos { set; }
        //bool IsPercentage { get;set;}
        //string Denomination { get;set;}

        Tributes GetTributeSession { set;}
        Donation DonationCharity { set;}

        //LHK:(3:49 PM 2/8/2011) for Obituary Note for TributeType=Memorial
        string ObPostMessage { get; set; }
        string ObMessageWithoutHtml { get; set; }
    }
}




