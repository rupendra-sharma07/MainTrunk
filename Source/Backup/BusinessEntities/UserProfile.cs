///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.UserProfile.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about UserProfile
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class UserProfile
    {

        public UserProfile()
        { }

        public UserProfile(
         int UserId,
         string UserName,         
         string FirstName,         
         string City,
         string State,
         string Country,
         string StreetAddress,
        string PhoneNumber,
        bool IsUsernameVisiable,
        bool AllowIncomingMsg,
        bool IsLocationHide,

         DateTime CreatedOn
        )
        {
            this._UserId = UserId;
            this._UserName = UserName;            
            this._FirstName = FirstName;            
            this._City = City;
            this._State = State;
            this._Country = Country;
            this.StreetAddress = StreetAddress;
            this._PhoneNumber = PhoneNumber;
            this._IsUsernameVisiable = IsUsernameVisiable;
            this._AllowIncomingMsg = AllowIncomingMsg;
            this._IsLocationHide = IsLocationHide;
            this._CreatedOn = CreatedOn;
        }

        

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

        private string _City;
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }


        private string _State;
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }


        private string _Country;
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        private string _StreetAddress;
        public string StreetAddress
        {
            get { return _StreetAddress; }
            set { _StreetAddress = value; }
        }


        private string _PhoneNumber;
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }


        private string _Website;
        public string Website
        {
            get { return _Website; }
            set { _Website = value; }
        }

        private DateTime _CreatedOn;
        public DateTime CreatedOn
        {
            get { return _CreatedOn; }
            set { _CreatedOn = value; }
        }

        private string _UserImage;
        public string UserImage
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }

    }
}
