///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.SessionValue.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about Sessions
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class SessionValue
    {
        public SessionValue()
        {

        }
        private int _UserId;
        private string _UserName;
        private string _FirstName;
        private string _LastName;
        private string _UserEmail;
        private int _UserType;
        private string _UserTypeDescription;
        private bool _IsUsernameVisiable;

        // Added by Varun to get No Redirection boolean on 25-Jan-2013
        private bool _NoRedirection = false;

        // Added by rupendra to get user image on 24-june -2011
        private string _UserImage;

        #region<< Storing Session Values >>

        public SessionValue(int Id, string SessionKey, string SessionValue)
        {
            this._ID = Id;
            this._SessionKey = SessionKey;
            this._SessionValues = SessionValue;
        }

        private string _SessionKey;
        private string _SessionValues;
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string SessionKey
        {
            get { return _SessionKey; }
            set { _SessionKey = value; }
        }

        public string SessionValues
        {
            get { return _SessionValues; }
            set { _SessionValues = value; }
        }

        #endregion<<Storing Session Values>>



        public int UserId 
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
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
        public string UserEmail
        {
            get { return _UserEmail; }
            set { _UserEmail = value; }
        }
        public int UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }
        public string UserTypeDescription
        {
            get { return _UserTypeDescription; }
            set { _UserTypeDescription = value; }
        }
        public bool IsUsernameVisiable
        {
            get { return _IsUsernameVisiable; }
            set { _IsUsernameVisiable = value; }
        }

        //// Added by rupendra to get user image on 24-june -2011
        public string UserImage
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }
        // Added by Varun to get No Redirection boolean on 25-Jan-2013
        public bool NoRedirection
        {
            get { return _NoRedirection; }
            set { _NoRedirection = value; }
        }

        //Overloaded constructor
        public SessionValue(int _UserId,
        string _UserName,
        string _FirstName,
        string _LastName,
        string _UserEmail,
        int _UserType,
        string _UserTypeDescription,
        bool _IsUsernameVisiable
            //// Added by rupendra to get user image on 24-june -2011
            ,string _sUserImage
            )
        {
            this._UserId = _UserId;
            this._UserName = _UserName;
            this._FirstName = _FirstName;
            this._LastName = _LastName;
            this._UserEmail = _UserEmail;
            this._UserType = _UserType;
            this._UserTypeDescription = _UserTypeDescription;
            this._IsUsernameVisiable = _IsUsernameVisiable;
            this.UserImage = _sUserImage;            
        }

        //Overloaded constructor -- Added by Varun on 25 Jan 2013
        public SessionValue(
            // Added by Varun to get No Redirection boolean on 25-Jan-2013
            bool _noRedirection
            )
        {            
            this.NoRedirection = _noRedirection;
        }

        public SessionValue(int _UserId,
        string _UserName,
        string _FirstName,
        string _LastName,
        string _UserEmail,
        int _UserType,
        string _UserTypeDescription,
        bool _IsUsernameVisiable
          
            )
        {
            this._UserId = _UserId;
            this._UserName = _UserName;
            this._FirstName = _FirstName;
            this._LastName = _LastName;
            this._UserEmail = _UserEmail;
            this._UserType = _UserType;
            this._UserTypeDescription = _UserTypeDescription;
            this._IsUsernameVisiable = _IsUsernameVisiable;
           
        }
    }
}
