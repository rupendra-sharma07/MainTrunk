///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Users.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about Users
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class Users
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        /// 

        public enum UserEnum
        { 
            UserId,
            UserName,
            Password,
            FirstName,
            LastName,
            Email,
            VerificationCode,
            UserType,
            UserImage,
            IsUsernameVisiable,
            AllowIncomingMsg,
            IsLocationHide,
            IsVisitCountHide,
            Status,
            City,
            State,
            Country,
            IsActive,
            IsDeleted,
            FacebookUid,
            ApplicationType,
            IsMobileViewOn
        }

        public Users()
        { }


        private int _UserId;
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }


        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }


        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private Nullable<Int64> _FacebookUid;
        public Nullable<Int64> FacebookUid
        {
            get { return _FacebookUid; }
            set { _FacebookUid = value; }
        }

        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }


        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }


        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }


        private string _VerificationCode;
        public string VerificationCode
        {
            get { return _VerificationCode; }
            set { _VerificationCode = value; }
        }


        private int _UserType;
        public int UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }


        private string _UserImage;
        public string UserImage
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }


        private bool _IsUsernameVisiable;
        public bool IsUsernameVisiable
        {
            get { return _IsUsernameVisiable; }
            set { _IsUsernameVisiable = value; }
        }


        private bool _AllowIncomingMsg;
        public bool AllowIncomingMsg
        {
            get { return _AllowIncomingMsg; }
            set { _AllowIncomingMsg = value; }
        }


        private bool _IsLocationHide;
        public bool IsLocationHide
        {
            get { return _IsLocationHide; }
            set { _IsLocationHide = value; }
        }


        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }


        private string _City;
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }


        private Nullable<int> _State;
        public Nullable<int> State
        {
            get { return _State; }
            set { _State = value; }
        }


        private Nullable<int> _Country;
        public Nullable<int> Country
        {
            get { return _Country; }
            set { _Country = value; }
        }


        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }


        private bool _IsDeleted;
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        private string _accountType;
        public string AccountType
        {
            get { return _accountType; }
            set { _accountType = value; }
        }

        private Nullable<DateTime> _createdAfter;
        public Nullable<DateTime> CreatedAfter
        {
            get { return _createdAfter; }
            set { _createdAfter = value; }
        }

        private Nullable<DateTime> _createdBefore;
        public Nullable<DateTime> CreatedBefore
        {
            get { return _createdBefore; }
            set { _createdBefore = value; }
        }
        private DateTime _createdOn;
        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }
        private string _creationDate;
        public string CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }
        private string _stateName;
        public string StateName
        {
            get { return _stateName; }
            set { _stateName = value; }
        }


        private string _countryName;
        public string CountryName
        {
            get { return _countryName; }
            set { _countryName = value; }
        }
        private string _searchUserId;
        public string SearchUserId
        {
            get { return _searchUserId; }
            set { _searchUserId = value; }
        }

        private bool _AtomEnabled;
        public bool AtomEnabled
        {
            get { return _AtomEnabled; }
            set { _AtomEnabled = value; }
        }

        private bool _EnableXMLFeed;
        public bool EnableXMLFeed
        {
            get { return _EnableXMLFeed; }
            set { _EnableXMLFeed = value; }
        }

        /// <summary>
        /// User defined Contructor
        /// <summary>

        public Users(string UserName)
        {
            this._UserName = UserName;
        }

        public Users(int UserID)
        {
            this._UserId = UserID;
        }
        public Users(string email, string password)
        {
            this._Email = email;
            this._Password = password;
        }
        // 

        private bool _IsVisitCountHide;
        public bool IsVisitCountHide
        {
            get { return _IsVisitCountHide; }
            set { _IsVisitCountHide = value; }
        }
        

        private bool _IsMobileViewOn;
        public bool IsMobileViewOn
        {
            get { return _IsMobileViewOn; }
            set { _IsMobileViewOn = value; }
        }


        //  objUsers.UserName = txtUsername.Text.ToString();
        //objUsers.Password = txtPassword.Text.ToString();
        //objUsers.FirstName = txtFirstName.Text.ToString();
        //objUsers.LastName = txtLastName.Text.ToString();
        //objUsers.Email = txtEmail.Text.ToString();
        //objUsers.VerificationCode = txtVerification.Text.ToString();
        //objUsers.AllowIncomingMsg = chkAgreeReceiveNewsletters.Checked;
        //objUsers.City = txtCity.Text.ToString();
        //objUsers.State = int.Parse(ddlStateProvince.SelectedValue.ToString());
        //objUsers.Country = int.Parse(ddlCountry.SelectedValue.ToString());




        public Users( 
            string UserName,
            string Password,
            string FirstName,
            string LastName,
            string Email,
            string VerificationCode,                                    
            bool AllowIncomingMsg,   
            string City,
            Nullable<int> State,
            Nullable<int> Country
            )
        {
            this._UserName = UserName;
            this._Password = Password;
            this._FirstName = FirstName;
            this._LastName = LastName;
            this._Email = Email;
            this._VerificationCode = VerificationCode;            
            this._AllowIncomingMsg = AllowIncomingMsg;
            this._City = City;
            this._State = State;
            this._Country = Country;
            this._FacebookUid = null;
        }

        // added by ud for sign up through modal pop up
        public Users(
          string UserName,
          string Password,
          string FirstName,
          string LastName,
          string Email,
          bool AllowIncomingMsg,
          Nullable<int> Country,
          int UserType,
          Nullable<Int64> FacebookUid
          )
        {
            this._UserName = UserName;
            this._Password = Password;
            this._FirstName = FirstName;
            this._LastName = LastName;
            this._Email = Email;
            this._AllowIncomingMsg = AllowIncomingMsg;            
            this._Country = Country;
            this._UserType = UserType;
            this._FacebookUid = null;
        }




        public Users(
          string UserName,
          string Password,
          string FirstName,
          string LastName,
          string Email,
          string VerificationCode,
          bool AllowIncomingMsg,
          string City,
          Nullable<int> State,
          Nullable<int> Country,
            int UserType
          )
        {
            this._UserName = UserName;
            this._Password = Password;
            this._FirstName = FirstName;
            this._LastName = LastName;
            this._Email = Email;
            this._VerificationCode = VerificationCode;
            this._AllowIncomingMsg = AllowIncomingMsg;            
            this._City = City;
            this._State = State;
            this._Country = Country;
            this._UserType = UserType;
            this._FacebookUid = null;
        }

        public Users(
          string UserName,
          string Password,
          string FirstName,
          string LastName,
          string Email,
          string VerificationCode,
          bool AllowIncomingMsg,
          string City,
          Nullable<int> State,
          Nullable<int> Country,
          int UserType, 
          Nullable<Int64> FacebookUid
          )
        {
            this._UserName = UserName;
            this._Password = Password;
            this._FirstName = FirstName;
            this._LastName = LastName;
            this._Email = Email;
            this._VerificationCode = VerificationCode;
            this._AllowIncomingMsg = AllowIncomingMsg;            
            this._City = City;
            this._State = State;
            this._Country = Country;
            this._UserType = UserType;
            this._FacebookUid = FacebookUid;
        }

        public Users(int UserId,
            string UserName,
            string Password,
            string FirstName,
            string LastName,
            string Email,
            string VerificationCode,
            int UserType,
            string UserImage,
            bool IsUsernameVisiable,
            bool AllowIncomingMsg,
            bool IsLocationHide,
            bool IsVisitCountHide,
            int Status,
            string City,
            Nullable<int> State,
            Nullable<int> Country,
            bool IsActive,
            bool IsDeleted)
        {
            this._UserId = UserId;
            this._UserName = UserName;
            this._Password = Password;
            this._FirstName = FirstName;
            this._LastName = LastName;
            this._Email = Email;
            this._VerificationCode = VerificationCode;
            this._UserType = UserType;
            this._UserImage = UserImage;
            this._IsUsernameVisiable = IsUsernameVisiable;
            this._AllowIncomingMsg = AllowIncomingMsg;
            this._IsLocationHide = IsLocationHide;
            this._IsVisitCountHide = IsVisitCountHide;
            this._Status = Status;
            this._City = City;
            this._State = State;
            this._Country = Country;
            this._IsActive = IsActive;
            this._IsDeleted = IsDeleted;
            this._FacebookUid = null;
        }

        /*Added by deepak Nagar*/
        /// <summary>
        /// This Constructor will be called to get User Information.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Email"></param>
        /// <param name="VerificationCode"></param>
        /// <param name="UserType"></param>
        /// <param name="UserImage"></param>
        /// <param name="Status"></param>
        /// <param name="City"></param>
        /// <param name="State"></param>
        /// <param name="Country"></param>
        public Users(
         int UserId,
         string UserName,         
         string FirstName,
         string LastName,
         string Email,                  
         string UserImage,         
         int Status,
         string City,
         Nullable<int> State,
         Nullable<int> Country,
         bool IsUsernameVisiable,
         bool AllowIncomingMsg,
         bool IsLocationHide,
            bool IsVisitCountHide
        )
        {
            this._UserId = UserId;
            this._UserName = UserName;
            this._Password = Password;
            this._FirstName = FirstName;
            this._LastName = LastName;
            this._Email = Email;
            this._UserImage = UserImage;            
            this._Status = Status;
            this._City = City;
            this._State = State;
            this._Country = Country;
            this._IsUsernameVisiable = IsUsernameVisiable;
            this._AllowIncomingMsg = AllowIncomingMsg;
            this._IsLocationHide = IsLocationHide;
            this._IsVisitCountHide = IsVisitCountHide;
            this._FacebookUid = null;
        }

        private EmailNotification _ObjEmailNotification;
        public EmailNotification EmailNotification
        {
            get { return _ObjEmailNotification; }
            set { _ObjEmailNotification = value; }
        }
        // Added by udham on Oct 3, 2011 for YM changes ( To identify the Application)
        private string _ApplicationType;
        public string ApplicationType
        {
            get { return _ApplicationType; }
            set { _ApplicationType = value; }
        }

        // added by udham on Oct 3, 2011 for YM changes(To identify application)
        public Users(
          string UserName,
          string Password,
          string FirstName,
          string LastName,
          string Email,
          string VerificationCode,
          bool AllowIncomingMsg,
          string City,
          Nullable<int> State,
          Nullable<int> Country,
          int UserType,
          Nullable<Int64> FacebookUid,
            string ApplicationType
          )
        {
            this._UserName = UserName;
            this._Password = Password;
            this._FirstName = FirstName;
            this._LastName = LastName;
            this._Email = Email;
            this._VerificationCode = VerificationCode;
            this._AllowIncomingMsg = AllowIncomingMsg;
            this._City = City;
            this._State = State;
            this._Country = Country;
            this._UserType = UserType;
            this._FacebookUid = FacebookUid;
            this._ApplicationType = ApplicationType;
        }
        // added by LHK on Jan 8, 2013 for IsMobileViewOn property
        public Users(
          string UserName,
          string Password,
          string FirstName,
          string LastName,
          string Email,
          string VerificationCode,
          bool AllowIncomingMsg,
          string City,
          Nullable<int> State,
          Nullable<int> Country,
          int UserType,
          Nullable<Int64> FacebookUid,
            string ApplicationType,
            bool IsMobileViewOn
          )
        {
            this._UserName = UserName;
            this._Password = Password;
            this._FirstName = FirstName;
            this._LastName = LastName;
            this._Email = Email;
            this._VerificationCode = VerificationCode;
            this._AllowIncomingMsg = AllowIncomingMsg;
            this._City = City;
            this._State = State;
            this._Country = Country;
            this._UserType = UserType;
            this._FacebookUid = FacebookUid;
            this._ApplicationType = ApplicationType;
            this._IsMobileViewOn = IsMobileViewOn;
        }
        
    }
}
