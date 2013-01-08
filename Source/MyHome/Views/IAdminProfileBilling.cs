///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.IAdminProfileBilling.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Admin Profile Billing pages.
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.MyHome.Views
{
    public interface IAdminProfileBilling
    {
        IList<BillingHistory> BillingInformation { set; }
        string BannerMessage { get;set;}
        int UserId { get;set;}

        //CC details
        IList<ParameterTypesCodes> PaymentModes { set;}
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
        int CreditCardId { get;set;}
        int Visibility { set; }
       //Receipt
        IList<PaymentReceipt> PaymentReceipt { set; }

        //
        //CC details Popup
        IList<ParameterTypesCodes> PaymentModes_ { set;}
        IList<Locations> CCCountryList_ { set; }
        IList<Locations> CCStateList_ { set; }
        string SelectedCCCountry_ { get;}
        int SelectedCCState_ { get;}
        string SelectedCCCity_ { get;}
        string CreditCardNo_ { get;}
        string CVC_ { get;}
        string CardholdersName_ { get;}
        DateTime ExpirationDate_ { get; }
        string Telephone_ { get;}
        string PaymentMethod_ { get;}
        string Address_ { get;}
        string ZipCode_ { get;}
        bool NotifyBeforeRenew_ { get;}
        bool IsCardDetailsReusable_ { get;}
        int PackageId_ { get;}

        bool IsPercentage { get;set;}
        string Denomination { get;set;}
    }
}




