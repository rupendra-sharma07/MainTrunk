///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.CouponsAvailable.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the Coupons Availability object
///Audit Trail     : Date of Modification  Modified By         Description

using System;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class CouponsAvailable
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public CouponsAvailable()
        { }


        public int CouponID
        {
            get { return _CouponID; }
            set { _CouponID = value; }
        }
        private int _CouponID;


        public int PrimaryCouponID
        {
            get { return _PrimaryCouponID; }
            set { _PrimaryCouponID = value; }
        }
        private int _PrimaryCouponID;


        public decimal SerialNumber
        {
            get { return _SerialNumber; }
            set { _SerialNumber = value; }
        }
        private decimal _SerialNumber;


        public string CouponCode
        {
            get { return _CouponCode; }
            set { _CouponCode = value; }
        }
        private string _CouponCode;


        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private int _Status;


        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        private bool _IsActive;


        public bool IsDelete
        {
            get { return _IsDelete; }
            set { _IsDelete = value; }
        }
        private bool _IsDelete;

        /// <summary>
        /// User defined Contructor
        /// <summary>
        public CouponsAvailable(int CouponID,
            int PrimaryCouponID,
            decimal SerialNumber,
            string CouponCode,
            int Status,
            bool IsActive,
            bool IsDelete)
        {
            this._CouponID = CouponID;
            this._PrimaryCouponID = PrimaryCouponID;
            this._SerialNumber = SerialNumber;
            this._CouponCode = CouponCode;
            this._Status = Status;
            this._IsActive = IsActive;
            this._IsDelete = IsDelete;
        }

    }
}
