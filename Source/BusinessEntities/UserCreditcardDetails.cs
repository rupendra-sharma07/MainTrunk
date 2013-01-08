///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.UserCreditcardDetails.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the User Credit card Details
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class UserCreditcardDetails
    {
        /// <summary>
        /// Default Contructor
        /// <summary>

        public enum CardDetais
        {

            CreditCardId,
            UserId,
            CardholdersName,
            CreditCardType,
            CreditCardNo,
            ExpirationDate,
            Address,
            City,
            Zip,
            State,
            Country,
            Telephone,
            IsCardDetailsReusable,
            NotifyBeforeRenew,
            CreatedBy,
            CreatedDate,
            ModifiedBy,
            ModifiedDate,
            IsActive,
            IsDeleted,
            SponsorEmailAddress
        }

        public UserCreditcardDetails()
        { }



        public string CVC
        {
            get { return _cvc; }
            set { _cvc = value; }
        }
        private string _cvc;

        public bool SaveCCInfo
        {
            get { return _SaveCCInfo; }
            set { _SaveCCInfo = value; }
        }
        private bool _SaveCCInfo;


        public int CreditCardId
        {
            get { return _CreditCardId; }
            set { _CreditCardId = value; }
        }
        private int _CreditCardId;


        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private int _UserId;


        public string CardholdersName
        {
            get { return _CardholdersName; }
            set { _CardholdersName = value; }
        }
        private string _CardholdersName;


        public string CreditCardType
        {
            get { return _CreditCardType; }
            set { _CreditCardType = value; }
        }
        private string _CreditCardType;


        public string CreditCardNo
        {
            get { return _CreditCardNo; }
            set { _CreditCardNo = value; }
        }
        private string _CreditCardNo;


        public System.DateTime ExpirationDate
        {
            get { return _ExpirationDate; }
            set { _ExpirationDate = value; }
        }
        private System.DateTime _ExpirationDate;


        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private string _Address;


        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _City;


        public string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }
        private string _Zip;


        public Nullable<int> State
        {
            get { return _State; }
            set { _State = value; }
        }
        private Nullable<int> _State;


        public Nullable<int> Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        private Nullable<int> _Country;


        public string Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }
        private string _Telephone;


        public bool IsCardDetailsReusable
        {
            get { return _IsCardDetailsReusable; }
            set { _IsCardDetailsReusable = value; }
        }
        private bool _IsCardDetailsReusable;

        public bool NotifyBeforeRenew
        {
            get { return _NotifyBeforeRenew; }
            set { _NotifyBeforeRenew = value; }
        }
        private bool _NotifyBeforeRenew;


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

        public string SponsorEmailAddress
        {
            get { return _SponsorEmailAddress; }
            set { _SponsorEmailAddress = value; }
        }
        private string _SponsorEmailAddress;


        /// <summary>
        /// User defined Contructor
        /// <summary>
        public UserCreditcardDetails(int CreditCardId,
            int UserId,
            string CardholdersName,
            string CreditCardType,
            string CreditCardNo,
            System.DateTime ExpirationDate,
            string Address,
            string City,
            string Zip,
            int State,
            int Country,
            string Telephone,
            bool IsCardDetailsReusable,
            int CreatedBy,
            System.DateTime CreatedDate,
            int ModifiedBy,
            System.DateTime ModifiedDate,
            bool IsActive,
            bool IsDeleted)
        {
            this._CreditCardId = CreditCardId;
            this._UserId = UserId;
            this._CardholdersName = CardholdersName;
            this._CreditCardType = CreditCardType;
            this._CreditCardNo = CreditCardNo;
            this._ExpirationDate = ExpirationDate;
            this._Address = Address;
            this._City = City;
            this._Zip = Zip;
            this._State = State;
            this._Country = Country;
            this._Telephone = Telephone;
            this._IsCardDetailsReusable = IsCardDetailsReusable;
            this._CreatedBy = CreatedBy;
            this._CreatedDate = CreatedDate;
            this._ModifiedBy = ModifiedBy;
            this._ModifiedDate = ModifiedDate;
            this._IsActive = IsActive;
            this._IsDeleted = IsDeleted;
        }

        public UserCreditcardDetails(
           int UserId,
           string CreditCardType,
           string CreditCardNo,
           string CardholdersName,
           System.DateTime ExpirationDate,
           string Address,
           string City,
           string Zip,
           int State,
           int Country,
           string Telephone
           )
        {
            this._UserId = UserId;
            this._CreditCardType = CreditCardType;
            this._CreditCardNo = CreditCardNo;
            this._CardholdersName = CardholdersName;
            this._ExpirationDate = ExpirationDate;
            this._Address = Address;
            this._City = City;
            this._Zip = Zip;
            this._State = State;
            this._Country = Country;
            this._Telephone = Telephone;
        }

    }
}
