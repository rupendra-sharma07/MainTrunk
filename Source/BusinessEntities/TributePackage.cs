///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.TributePackage.cs
///Author          : LHK
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details of the
///                  tribute package   
///Audit Trail     : Date of Modification  Modified By         Description

using System;
namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class TributePackage
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public TributePackage()
        { }

        public enum PackageTribute
        {
            TributePackageId,
            UserId,
            UserTributeId,
            StartDate,
            EndDate,
            IsAutomaticRenew,
            AmountPaid,
            PackageId,
            CouponId,
            IsSponsor,
            IsSponserHide,
            CreditCardId,
            AutoRenewdate
        }

        private Errors _ObjErrors;

        public Errors CustomError
        {
            get { return _ObjErrors; }
            set { _ObjErrors = value; }
        }





        public int CreditCardId
        {
            get { return _CreditCardId; }
            set { _CreditCardId = value; }
        }
        private int _CreditCardId;

        public int TributePackageId
        {
            get { return _TributePackageId; }
            set { _TributePackageId = value; }
        }
        private int _TributePackageId;


        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private int _UserId;


        public int UserTributeId
        {
            get { return _UserTributeId; }
            set { _UserTributeId = value; }
        }
        private int _UserTributeId;


        public Nullable<DateTime> StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private Nullable<DateTime> _StartDate;


        public Nullable<DateTime> EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        private Nullable<DateTime> _EndDate;

        public Nullable<DateTime> AutoRenewdate
        {
            get { return _AutoRenewdate; }
            set { _AutoRenewdate = value; }
        }
        private Nullable<DateTime> _AutoRenewdate;


        public bool IsAutomaticRenew
        {
            get { return _IsAutomaticRenew; }
            set { _IsAutomaticRenew = value; }
        }
        private bool _IsAutomaticRenew;


        public decimal AmountPaid
        {
            get { return _AmountPaid; }
            set { _AmountPaid = value; }
        }
        private decimal _AmountPaid;


        public int PackageId
        {
            get { return _PackageId; }
            set { _PackageId = value; }
        }
        private int _PackageId;


        public Nullable<int> CouponId
        {
            get { return _CouponId; }
            set { _CouponId = value; }
        }
        private Nullable<int> _CouponId;


        public bool IsSponsor
        {
            get { return _IsSponsor; }
            set { _IsSponsor = value; }
        }
        private bool _IsSponsor;


        public bool IsSponserHide
        {
            get { return _IsSponserHide; }
            set { _IsSponserHide = value; }
        }
        private bool _IsSponserHide;

        /// <summary>
        /// User defined Contructor
        /// <summary>
        public TributePackage(int TributePackageId,
            int UserId,
            int UserTributeId,
            System.DateTime StartDate,
            System.DateTime EndDate,
            bool IsAutomaticRenew,
            decimal AmountPaid,
            int PackageId,
            int CouponId,
            bool IsSponsor,
            bool IsSponserHide)
        {
            this._TributePackageId = TributePackageId;
            this._UserId = UserId;
            this._UserTributeId = UserTributeId;
            this._StartDate = StartDate;
            this._EndDate = EndDate;
            this._IsAutomaticRenew = IsAutomaticRenew;
            this._AmountPaid = AmountPaid;
            this._PackageId = PackageId;
            this._CouponId = CouponId;
            this._IsSponsor = IsSponsor;
            this._IsSponserHide = IsSponserHide;
        }

    }
}
