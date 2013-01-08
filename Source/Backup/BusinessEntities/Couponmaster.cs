///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Couponmaster.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the Coupon master object
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class Couponmaster
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public Couponmaster()
        { }


        public int PrimaryCouponID
        {
            get { return _PrimaryCouponID; }
            set { _PrimaryCouponID = value; }
        }
        private int _PrimaryCouponID;

        public int CouponPackage
        {
            get { return _CouponPackage; }
            set { _CouponPackage = value; }
        }
        private int _CouponPackage;


        public string CouponName
        {
            get { return _CouponName; }
            set { _CouponName = value; }
        }
        private string _CouponName;


        public decimal CouponDenomination
        {
            get { return _CouponDenomination; }
            set { _CouponDenomination = value; }
        }
        private decimal _CouponDenomination;


        public bool IsPercentage
        {
            get { return _IsPercentage; }
            set { _IsPercentage = value; }
        }
        private bool _IsPercentage;


        public System.DateTime ApplicableFromDate
        {
            get { return _ApplicableFromDate; }
            set { _ApplicableFromDate = value; }
        }
        private System.DateTime _ApplicableFromDate;


        public System.DateTime ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        private System.DateTime _ExpiryDate;


        public int MaxNoOfUses
        {
            get { return _MaxNoOfUses; }
            set { _MaxNoOfUses = value; }
        }
        private int _MaxNoOfUses;


        public int NoOfCoupons
        {
            get { return _NoOfCoupons; }
            set { _NoOfCoupons = value; }
        }
        private int _NoOfCoupons;


        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        private int _CreatedBy;


        public System.DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        private System.DateTime _CreatedDate;


        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        private int _ModifiedBy;


        public System.DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }
        private System.DateTime _ModifiedDate;


        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        private bool _IsActive;


        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        private bool _IsDeleted;

        /// <summary>
        /// User defined Contructor
        /// <summary>
        public Couponmaster(int PrimaryCouponID,
            string CouponName,
            decimal CouponDenomination,
            bool IsPercentage,
            System.DateTime ApplicableFromDate,
            System.DateTime ExpiryDate,
            int MaxNoOfUses,
            int NoOfCoupons,
            int CreatedBy,
            System.DateTime CreatedDate,
            int ModifiedBy,
            System.DateTime ModifiedDate,
            bool IsActive,
            bool IsDeleted)
        {
            this._PrimaryCouponID = PrimaryCouponID;
            this._CouponName = CouponName;
            this._CouponDenomination = CouponDenomination;
            this._IsPercentage = IsPercentage;
            this._ApplicableFromDate = ApplicableFromDate;
            this._ExpiryDate = ExpiryDate;
            this._MaxNoOfUses = MaxNoOfUses;
            this._NoOfCoupons = NoOfCoupons;
            this._CreatedBy = CreatedBy;
            this._CreatedDate = CreatedDate;
            this._ModifiedBy = ModifiedBy;
            this._ModifiedDate = ModifiedDate;
            this._IsActive = IsActive;
            this._IsDeleted = IsDeleted;
        }

    }
}
