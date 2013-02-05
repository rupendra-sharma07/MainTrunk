using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class CreditPointTransaction
    {
        public enum CreditPointDetails
        {

            TransactionId,
            UserId,
            NetCreditPoints,
            AmountPaid,
            CreditPackageId,
            PurchasedDate,
            IsDeleted,
            ModifiedDate,
            CouponId,
            CreditCardId,
            CreatedDate,
            ConfirmationNo
        }

        public CreditPointTransaction()
        { }

        public int TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }
        private int _TransactionId;


        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        private int _userId;


        public Double NetCreditPoints
        {
            get { return _NetCreditPoints; }
            set { _NetCreditPoints = value; }
        }
        private Double _NetCreditPoints;


        public int AmountPaid
        {
            get { return _AmountPaid; }
            set { _AmountPaid = value; }
        }
        private int _AmountPaid;


        public int CreditPackageId
        {
            get { return _CreditPackageId; }
            set { _CreditPackageId = value; }
        }
        private int _CreditPackageId;

        public DateTime PurchasedDate
        {
            get { return _PurchasedDate; }
            set { _PurchasedDate = value; }
        }
        private DateTime _PurchasedDate;


        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        private bool _IsDeleted;


        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }
        private DateTime _ModifiedDate;

        public int CouponId
        {
            get { return _CouponId; }
            set { _CouponId = value; }
        }
        private int _CouponId;


        public int CreditCardId
        {
            get { return _CreditCardId; }
            set { _CreditCardId = value; }
        }
        private int _CreditCardId;


        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        private DateTime _CreatedDate;


        public int ConfirmationNo
        {
            get { return _ConfirmationNo; }
            set { _ConfirmationNo = value; }
        }
        private int _ConfirmationNo;
    }

    }

