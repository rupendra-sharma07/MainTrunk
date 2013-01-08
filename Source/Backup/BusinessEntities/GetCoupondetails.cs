///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.GetCoupondetails.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the entity to be used when getting the details of a coupon
///Audit Trail     : Date of Modification  Modified By         Description

using System;


namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class GetCoupondetails
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public GetCoupondetails()
        { }


        public System.Int32 PrimaryCouponID
        {
            get { return _PrimaryCouponID; }
            set { _PrimaryCouponID = value; }
        }
        private System.Int32 _PrimaryCouponID;


        public System.String CouponName
        {
            get { return _CouponName; }
            set { _CouponName = value; }
        }
        private System.String _CouponName;


        public System.Int32 CouponCount
        {
            get { return _CouponCount; }
            set { _CouponCount = value; }
        }
        private System.Int32 _CouponCount;


        public string CouponDenomination
        {
            get { return _CouponDenomination; }
            set { _CouponDenomination = value; }
        }
        private string _CouponDenomination;


        public System.String Package
        {
            get { return _Package; }
            set { _Package = value; }
        }
        private System.String _Package;


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


        public System.String Usage
        {
            get { return _Usage; }
            set { _Usage = value; }
        }
        private System.String _Usage;

        /// <summary>
        /// User defined Contructor
        /// <summary>
        public GetCoupondetails(System.Int32 PrimaryCouponID,
            System.String CouponName,
            System.Int32 CouponCount,
            System.String CouponDenomination,
            System.String Package,
            System.DateTime ApplicableFromDate,
            System.DateTime ExpiryDate,
            System.String Usage)
        {
            this._PrimaryCouponID = PrimaryCouponID;
            this._CouponName = CouponName;
            this._CouponCount = CouponCount;
            this._CouponDenomination = CouponDenomination;
            this._Package = Package;
            this._ApplicableFromDate = ApplicableFromDate;
            this._ExpiryDate = ExpiryDate;
            this._Usage = Usage;
        }

    }
}
