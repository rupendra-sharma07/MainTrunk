using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.Coupons.Views
{
    public interface ICouponCreation
    {
        string CouponName { get;}
        decimal CouponDenomination { get;}
        bool IsPercentage { get;}
        System.DateTime ApplicableFromDate { get;}
        System.DateTime ExpiryDate { get;}
        int MaxNoOfUses { get;}
        int NoOfCoupons { get;}
        int CreatedBy { get;}
        string CouponCode { get;}
        int CouponPackage { get;}
        IList<ParameterTypesCodes> CouponUses { set;}
        IList<ParameterTypesCodes> CouponPackages { set;}
        
    }
}




