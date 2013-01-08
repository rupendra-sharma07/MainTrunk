///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.UserInfo.cs
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
    public class UserInfo
    {
        private int _UserId;
        private Nullable<Int64> _FacebookUid;
        private string _UserName;
        private string _UserPassword;
        private string _FirstName;
        private string _LastName;
        private string _UserType;
        private bool _IsUsernameVisiable;
        private string _UserTypeDescription;
        private string _UserEmail;
        private string _UserImage;
        private bool _IsOwner;
        private string _ApplicationType;

        public int UserID
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        
        public Nullable<Int64> FacebookUid
        {
            get { return _FacebookUid; }
            set { _FacebookUid = value; }
        }

        public string UserName
        {
            get { return _UserName;}
            set { _UserName = value; }
        }

        public string UserPassword
        {
            get { return _UserPassword; }
            set { _UserPassword = value; }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }

        public bool IsUsernameVisiable
        {
            get { return _IsUsernameVisiable; }
            set { _IsUsernameVisiable = value; }
        }

        public string UserTypeDescription
        {
            get { return _UserTypeDescription; }
            set { _UserTypeDescription = value; }
        }
        public string UserEmail
        {
            get { return _UserEmail; }
            set { _UserEmail = value; }
        }

        public string UserImage
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }

        public bool IsOwner
        {
            get { return _IsOwner; }
            set { _IsOwner = value; }
        }
        // Added by ashu on sept 30, 2011 for YM changes ( To identify the Application)
        public string ApplicationType
        {
            get { return _ApplicationType; }
            set { _ApplicationType = value; }
        }

        
    }
}
