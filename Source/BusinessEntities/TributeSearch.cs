///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.TributeSearch.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the search for a tribute
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public partial class TributeSearch
    {
        #region FIEDLS
        //private Tributes _tributes;
        private int _tributeId;
        private int _userTributeId;
        private string _tributeName;
        private int _tributeType;
        private string _city;
        private Nullable<int> _state;
        private DateTime _createdDate;
        private string _creationDate;
        private string _typeDescription;
        private int _country;
        private bool _isActive;
        private bool _isDeleted;
        private string _searchTributeId;
        private string _searchUserId;
        private string _userName;
        private string _tributeStatus;
        private string _stateName;
        private string _countryName;
        private Nullable<DateTime> _createdAfter;
        private Nullable<DateTime> _createdBefore;
        private Nullable<DateTime> _purchasedAfter;
        private Nullable<DateTime> _purchasedBefore;
        private string _endDate;
        private string _tributeUrl; // Added by Rahul 6-Feb-2009
        private string _applicationType; // Added by udham 7-nov-2011
        #endregion

        #region PROPERTIES
        //public Tributes Tributes
        //{
        //    get { return _tributes; }
        //    set { _tributes = value; }
        //}

        public int TributeId
        {
            get { return _tributeId; }
            set { _tributeId = value; }
        }
        public int UserTributeId
        {
            get { return _userTributeId; }
            set { _userTributeId = value; }
        }
        public string TributeName
        {
            get { return _tributeName; }
            set { _tributeName = value; }
        }
        public int TributeType
        {
            get { return _tributeType; }
            set { _tributeType = value; }
        }
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public Nullable<int> State
        {
            get { return _state; }
            set { _state = value; }
        }
        public int Country
        {
            get { return _country; }
            set { _country = value; }
        }
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
        public string TypeDescription
        {
            get { return _typeDescription; }
            set { _typeDescription = value; }
        }
        public string SearchTributeId
        {
            get { return _searchTributeId; }
            set { _searchTributeId = value; }
        }
        public string SearchUserId
        {
            get { return _searchUserId; }
            set { _searchUserId = value; }
        }
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public string TributeStatus
        {
            get { return _tributeStatus; }
            set { _tributeStatus = value; }
        }
        public string StateName
        {
            get { return _stateName; }
            set { _stateName = value; }
        }
        public string CountryName
        {
            get { return _countryName; }
            set { _countryName = value; }
        }
        public Nullable<DateTime> CreatedAfter
        {
            get { return _createdAfter; }
            set { _createdAfter = value; }
        }
        public Nullable<DateTime> CreatedBefore
        {
            get { return _createdBefore; }
            set { _createdBefore = value; }
        }
        public Nullable<DateTime> PurchasedAfter
        {
            get { return _purchasedAfter; }
            set { _purchasedAfter = value; }
        }
        public Nullable<DateTime> PurchasedBefore
        {
            get { return _purchasedBefore; }
            set { _purchasedBefore = value; }
        }
        public string EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        public string CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }
        public string TributeUrl
        {
            get { return _tributeUrl; }
            set { _tributeUrl = value; }
        }
        public string ApplicationType
        {
            get { return _applicationType; }
            set { _applicationType = value; }
        }
        #endregion
    }
}
