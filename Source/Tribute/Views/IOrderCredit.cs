using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using System.Collections;

namespace TributesPortal.Tribute.Views
{
    public interface IOrderCredit
    {
        IList<Locations> CCCountryList { set; }
        IList<Locations> CCStateList { set; }
        IList<ParameterTypesCodes> PaymentModes { set; }

        // CCinformation
        string SelectedCCCountry { get; set; }
        string SelectedCCState { get; set; }
        int SelectedCCState_ { get; set; }
        string SelectedCCCity { get; set; }
        string CreditCardNo { get; set; }
        string CardholdersName { get; set; }
        DateTime ExpirationDate { get; set; }
        string Telephone { get; set; }
        string PaymentMethod { get; set; }
        string Address { get; set; }
        string ZipCode { get; set; }
        bool NotifyBeforeRenew { get; }
        bool IsCardDetailsReusable { get; }
        int getPackageId { get; }
        bool IsSponserHide { get; }
        bool IsPercentage { get; set; }
        string Denomination { get; set; }
        int TributeId { get; }
        int UserID { get; }
        int PackageId { get; set; }
        string TributeType { get; set; }
        string SubDomain { get; set; }
        string TributeURL { get; set; }
        Nullable<DateTime> EndDate { get; set; }
        Tributes GetTributeSession { set; }
        int CreditCardId { get; set; }
        string CVC { get; set; }
        Decimal AmountPaid { get; }

        TributePackage TributePackageDetails { get; set; }
        bool IsUserAdmin { set; }

        string SponsorEmailAddress { get; }
        int TributePackageId { get; set; }
        int TransactionId { get; set; }
        double NetCreditPoints { set; get; }
        IList<CreditCostMapping> CreditCostMappingList { set; }
    }
}
