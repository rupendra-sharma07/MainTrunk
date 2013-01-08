///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Donation.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Donation Details to be sent to epartnersingiving
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{

    [Serializable]
    public class Donation
    {
        public enum DonationEnum
        {
            TributeId, 
            IsDonation, 
            TributeName, 
            TributeType, 
            TributeURL, 
            TributeCreatorEmail, 
            CharityName, 
            CharityCountry, 
            CharityState, 
            CharityCity, 
            CharityAddress, 
            DonationNotificationEmail, 
            DonationPageURL, 
            CreatedBy
        }
   
        public Donation()
        {

        }

        private string _tributeName;
        public string TributeName
        {
            get { return _tributeName; }
            set { _tributeName = value; }
        }

        private string _tributeType;
        public string TributeType
        {
            get { return _tributeType; }
            set { _tributeType = value; }
        }

        private string _tributeUrl;
        public string TributeUrl
        {
            get { return _tributeUrl; }
            set { _tributeUrl = value; }
        }

        private string _creatorMail;
        public string CreatorMail
        {
            get { return _creatorMail; }
            set { _creatorMail = value; }
        }

        private string _charityName;
        public string CharityName
        {
            get { return _charityName; }
            set { _charityName = value; }
        }

        private string _charityCountry;
        public string CharityCountry
        {
            get { return _charityCountry; }
            set { _charityCountry = value; }
        }

        private string _charityState;
        public string CharityState
        {
            get { return _charityState; }
            set { _charityState = value; }
        }

        private string _charityCity;
        public string CharityCity
        {
            get { return _charityCity; }
            set { _charityCity = value; }
        }
      
        private string _charityAddress;
        public string CharityAddress
        {
            get { return _charityAddress; }
            set { _charityAddress = value; }
        }

        private string _donationNotifyMail;
        public string DonationNotifyMail
        {
            get { return _donationNotifyMail; }
            set { _donationNotifyMail = value; }
        }

        //private bool _isDeleted;
        //public bool IsDeleted
        //{
        //    get { return _isDeleted; }
        //    set { _isDeleted = value; }
        //}

        private int _createdBy;
        public int CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        private DateTime _createdDate;
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        private int _modifiedBy;
        public int ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private DateTime _modifiedDate;
        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }

        private int _tributeID;
        public int TributeID
        {
            get { return _tributeID; }
            set { _tributeID = value; }
        }

        private string _donationUrl;
        public string DonationUrl
        {
            get { return _donationUrl; }
            set { _donationUrl = value; }
        }

        private Errors _error;
        public Errors CustomError
        {
            get { return _error; }
            set { _error = value; }
        }

        //public Donation(int tributeID, string tributeName, string tributeType, string welcomeMessage, string tributeImage, string tributeUrl, int themeId, string city, int state, int country, bool isPrivate, bool isActive, bool isDeleted, DateTime date1, DateTime date2, string attribute1, string attribute2, int createdBy, DateTime createdDate, int modifiedBy, DateTime modifiedDate, Errors customErrors)
        //{
        //    this.TributeId = tributeID;
        //    this.TributeName = tributeName;
        //    this.TributeType = tributeType;
        //    this.TributeUrl = tributeUrl;
        //    this.City = city;
        //    this.State = state;
        //    this.Country = country;
        //    this.IsPrivate = isPrivate;
        //    this.IsActive = isActive;
        //    this.IsDeleted = isDeleted;
        //    this.Date1 = date1;
        //    this.Date2 = date2;
        //    this.Attribute1 = attribute1;
        //    this.Attribute2 = attribute2;
        //    this.CreatedBy = createdBy;
        //    this.CreatedDate = createdDate;
        //    this.ModifiedBy = modifiedBy;
        //    this.ModifiedDate = modifiedDate;
        //    this.CustomError = customErrors;
        //}


    }
}
