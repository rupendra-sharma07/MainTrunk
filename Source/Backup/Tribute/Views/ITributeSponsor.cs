///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.ITributeSponsor.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the sponsoring a tribute.
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using System.Collections;

namespace TributesPortal.Tribute.Views
{
    public interface ITributeSponsor
    {
        IList<Locations> CCCountryList { set; }
        IList<Locations> CCStateList { set; }
        IList<Locations> CountryList { set; }
        IList<Locations> StateList { set; }
        IList<ParameterTypesCodes> PaymentModes { set; }
       
        // CCinformation
        string SelectedCCCountry { get;set;}
        string SelectedCCState { get;set;}

        string SelectedCountry { get; set; }
        string SelectedState { get; set; }       

        int SelectedCCState_ { get;set;}
        string SelectedCCCity { get;set;}
        string CreditCardNo { get;set;}
        string CardholdersName { get;set;}
        DateTime ExpirationDate { get;set;}
        string Telephone { get;set;}
        string PaymentMethod { get;set;}
        string Address { get;set;}
        string ZipCode { get;set;}
        bool NotifyBeforeRenew { get;}
        bool IsCardDetailsReusable { get;}
        int getPackageId { get;}
        bool IsSponserHide { get;}
        bool IsSponsor { get; }
        bool IsPercentage { get;set;}
        string Denomination { get;set;}
        int TributeId { get;}
        int UserID { get;}
        int PackageId { get; set;}
        string TributeType { get; set;}
        string SubDomain { get; set;}
        string TributeURL { get; set;}
        Nullable<DateTime> EndDate { get;set;}
        Tributes GetTributeSession { set;}
        int CreditCardId { get;set;}
        string CVC { get;set;}
        Decimal AmountPaid { get; }

        TributePackage TributePackageDetails { get; set;}
        bool IsUserAdmin { set;}

        string SponsorEmailAddress { get;}
        int TributePackageId { get;set;}
        int TransactionId { get;set;}
        int AdminOwnerId { get; set; }
        string AdminOwner { get; set; }
        IList<UserInfo> OtherAdmins { get; set; }
        UserRegistration UserDetails { get; set; }
        string UserEmail{ get; }
        int TributeAdminCount { get; set; }
        string chkAvailability { set; }
        string ApplicationType { get; }
        bool IsContainsVideo { get; set; }

    }
}


