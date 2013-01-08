///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.PaymentReceipt.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about Payment Receipts
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class PaymentReceipt
    {
        /// <summary>
        /// Default Contructor
        /// <summary>

        public enum PaymentEnum
        {
            Tributename,
            TypeDescription,
            Packagename,
            Enddate,
            CardholdersName,
            Address,
            City,
            State,
            Country,
            Zip,
            Telephone,
            StartDate,
            CreditCardType,
            CreditCardNo,
            AmountPaid,
            IsAutomaticRenew
        }

        public PaymentReceipt()
        { }


        public System.String Tributename
        {
            get { return _Tributename; }
            set { _Tributename = value; }
        }
        private System.String _Tributename;


        public System.String TypeDescription
        {
            get { return _TypeDescription; }
            set { _TypeDescription = value; }
        }
        private System.String _TypeDescription;


        public System.String Packagename
        {
            get { return _Packagename; }
            set { _Packagename = value; }
        }
        private System.String _Packagename;


        public string Enddate
        {
            get { return _Enddate; }
            set { _Enddate = value; }
        }
        private string _Enddate;


        public System.String CardholdersName
        {
            get { return _CardholdersName; }
            set { _CardholdersName = value; }
        }
        private System.String _CardholdersName;


        public System.String Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private System.String _Address;


        public System.String City
        {
            get { return _City; }
            set { _City = value; }
        }
        private System.String _City;


        public System.String State
        {
            get { return _State; }
            set { _State = value; }
        }
        private System.String _State;


        public System.String Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        private System.String _Country;


        public System.String Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }
        private System.String _Zip;


        public System.String Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }
        private System.String _Telephone;


        public System.DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private System.DateTime _StartDate;


        public System.String CreditCardType
        {
            get { return _CreditCardType; }
            set { _CreditCardType = value; }
        }
        private System.String _CreditCardType;


        public System.String CreditCardNo
        {
            get { return _CreditCardNo; }
            set { _CreditCardNo = value; }
        }
        private System.String _CreditCardNo;


        public int AmountPaid
        {
            get { return _AmountPaid; }
            set { _AmountPaid = value; }
        }
        private int _AmountPaid;

        public int TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }
        private int _TransactionId;

        public string SponsorEmailAddress
        {
            get { return _SponsorEmailAddress; }
            set { _SponsorEmailAddress = value; }
        }
        private string _SponsorEmailAddress;

        public bool IsAutomaticRenew
        {
            get { return _IsAutomaticRenew; }
            set { _IsAutomaticRenew = value; }
        }
        private bool _IsAutomaticRenew;


        /// <summary>
        /// User defined Contructor
        /// <summary>
        public PaymentReceipt(System.String Tributename,
            System.String TypeDescription,
            System.String Packagename,
            string Enddate,
            System.String CardholdersName,
            System.String Address,
            System.String City,
            System.String State,
            System.String Country,
            System.String Zip,
            System.String Telephone,
            System.DateTime StartDate,
            System.String CreditCardType,
            System.String CreditCardNo,
            int AmountPaid)
        {
            this._Tributename = Tributename;
            this._TypeDescription = TypeDescription;
            this._Packagename = Packagename;
            this._Enddate = Enddate;
            this._CardholdersName = CardholdersName;
            this._Address = Address;
            this._City = City;
            this._State = State;
            this._Country = Country;
            this._Zip = Zip;
            this._Telephone = Telephone;
            this._StartDate = StartDate;
            this._CreditCardType = CreditCardType;
            this._CreditCardNo = CreditCardNo;
            this._AmountPaid = AmountPaid;
        }

        public PaymentReceipt(int TransactionId, string Tributename,
            string TypeDescription, string Packagename, string Enddate,
            string CardholdersName, string Address, string City, string State,
            string Country, string Zip, string Telephone, DateTime StartDate,
            string CreditCardType, string CreditCardNo, int AmountPaid,
            string SponsorEmailAddress, bool IsAutomaticRenew)
        {
            this._TransactionId = TransactionId; 
            this._Tributename = Tributename;
            this._TypeDescription = TypeDescription;
            this._Packagename = Packagename;
            this._Enddate = Enddate;
            this._CardholdersName = CardholdersName;
            this._Address = Address;
            this._City = City;
            this._State = State;
            this._Country = Country;
            this._Zip = Zip;
            this._Telephone = Telephone;
            this._StartDate = StartDate;
            this._CreditCardType = CreditCardType;
            this._CreditCardNo = CreditCardNo;
            this._AmountPaid = AmountPaid;
            this._SponsorEmailAddress = SponsorEmailAddress;
            this._IsAutomaticRenew = IsAutomaticRenew;
        }

    }
}
