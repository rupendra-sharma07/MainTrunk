///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.ITributeCreation.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Tribute Creation pages.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using System.Collections;
namespace TributesPortal.Tribute.Views
{
    public interface ITributeCreation
    {
        IList<ParameterTypesCodes> TributeTypes { set; }
        IList<Themes> ThemeNames { set; }
        IList<Themes> ThemeNames_ { set; }
        IList<Locations> CountryList { set; }
        IList<Locations> StateList { set; }
        IList<Locations> CCCountryList { set; }
        IList<Locations> CCStateList { set; }
        IList<ParameterTypesCodes> PaymentModes { set; }

        IList<Locations> DonationCountryList { set; }
        IList<Locations> DonationStateList { set; }

        string EditTheme { get; }
        int UserId { get; }
        string UserMail { get; }
        //Url Availability
        string chkAvailability { set; }

        // Step1
        string TributeFor { get; }
        string TributeType { get; }
        string SelectedTheme { get; }
        string TributeUrl { get; set; }

        //Step2

        Nullable<DateTime> date1 { get; set; }
        Nullable<DateTime> date2 { get; set; }
        string SelectedCountry { get; set; }
        string SelectedState { get; set; }
        string SelectedCity { get; set; }
        int Age { get; }
        string TributeImage { get; }
        string WelcomeMsg { get; }

        //Step3
        bool IsPrivate { get; }
        bool IsOrderDVDChecked { get; }
        bool IsMemTributeBoxChecked { get; }

        ArrayList AdminEmailLists { get; }

        //Donation
        bool IsDonationActive { get; }
        string CharityName { get; }
        string DonationEmail { get; }
        string SelectedDonationCountry { get; }
        string SelectedDonationCountryText { get; }
        string SelectedDonationState { get; }
        string DonationCity { get; }
        string DonationAddress { get; }


        // Billing Info

        string SelectedCCCountry { get; }
        string SelectedCCState { get; }
        string SelectedCCCity { get; }
        string CreditCardNo { get; }
        string CVC { get; }
        string CardholdersName { get; }
        DateTime ExpirationDate { get; }
        string Telephone { get; }
        string PaymentMethod { get; }
        string Address { get; }
        string ZipCode { get; }
        bool NotifyBeforeRenew { get; }
        bool IsCardDetailsReusable { get; }
        int PackageId { get; set; }
        Decimal AmountPaid { get; }
        int TransactionId { get; set; }
        int TributePackageId { get; set; }
        Nullable<DateTime> EndDate { get; set; }
        //PaymentReceipt TransactionDetails { set;}

        //Coupon
        bool IsPercentage { get; set; }
        string Denomination { get; set; }

        //For Video Tribute
        int TributeId { get; }
        string TributeTypeDescription { get; }
        string TributeName { get; }
        string CreatedTributeUrl { get; }
        string VideoCaption { get; }
        string VideoDesc { get; }
        string VideoTributeId { get; }
        string UserName { get; }

        //For Credit Card Details
        UserRegistration CreditCardDetails { set; }
        IList<ParameterTypesCodes> GetSelectedPaymentMode { set; }
        string SelectedPaymentMode { set; }
        double NetCreditPoints { set; get; }
        IList<CreditCostMapping> CreditCostMappingList { set; }

        //LHK:(3:49 PM 2/8/2011) for Obituary Note for TributeType=Memorial
        string ObPostMessage { get; set; }
        string ObMessageWithoutHtml { get; set; }


        // Ud: (5:51 PM 6/22/2011) for Default Theme for Business users
        int DefaultTheme { get; set; }
        // added by udham for checking application type
        string ApplicationType { get; }

        //YT enhancement phase 1 :LHK
        string TributeFirstName { get; }
        string TributeLastName { get; }
    }
}
