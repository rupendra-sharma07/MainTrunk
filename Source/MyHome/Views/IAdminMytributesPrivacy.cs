///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.IAdminMytributesPrivacy.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Admin My tributes Privacy pages.
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.MyHome.Views
{
    public interface IAdminMytributesPrivacy
    {
        int SponserId { get;set;}
        int TributePackageId { get;set;}        
        int UserId { get;}
        int TributeId { get;set;}
        IList<UserInfo> TributeAdministrators { set;}
        string TributeOwner { set; }
        string TributeName { set; get;}
        bool IsPrivate { set; get;}
        bool IsAutomaticRenew { set; get;}
        bool IsSponsor { set; get;}
        bool IsSponserHide { set; get;}
        string Sponsorname { set; get;}
        bool GoogleAdSense { set; get;} 
        //CC details
        IList<Locations> CCCountryList { set; }
        IList<Locations> CCStateList { set; }
        string SelectedCCCountry { get;set;}        
        int SelectedCCState { get;set;}
        string SelectedCCCity { get;set;}
        string CreditCardNo { get;set;}
        string CVC { get;set;}
        string CardholdersName { get;set;}
        DateTime ExpirationDate { get;set; }
        string Telephone { get;set;}
        string PaymentMethod { get;set;}
        string Address { get;set;}
        string ZipCode { get;set;}
        bool NotifyBeforeRenew { get;}
        bool IsCardDetailsReusable { get;}
        int PackageId { get;set;}
        int ACT { get;}
        DateTime NewStartedDate { get; }
        String NewExpiryDate { set; }
        IList<GetMyTributes> Mytribute { set;}
        bool IsPercentage { get;set;}
        string Denomination { get;set;}
       //added for donation project
        Donation DonationCharity { get;set;}
        bool IsDonation { get;}
    }
}
