///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.SearchTribute.cs
///Author          : 
///Creation Date   : 
///Description     : This is the SearchTribute Entity class for the Searching of the tribute. 
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections;

#endregion

/// <summary>
///Tribute Portal-Search Tributes Entity Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the SearchTribute Entity class for the Searching the tribute. 
/// </summary>
/// 

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class SearchTribute
    {   

        #region FIELDS

        private int _TributeID;
        private int _UserTributeID;
        private string _TributeName;
        private string _SearchString;
        private string _ChangeSearchString;
        private string _SearchType;
        private string _TributeType;
        private string _TributeImage;
        private string _tributeUrl;
        private string _Date1;
        private string _City;
        private int _State;
        private int _Country;
        private string _StateName;
        private string _CountryName;
        private string _Location;
        private DateTime _DateForSorting;
        private string _CreatedDate;
        private Nullable<DateTime> _CreatedAfterDate;
        private Nullable<DateTime> _CreatedBeforeDate;
        private string _Hits;
        private string _CommonString;
        private int _PageNumber;
        private int _PageSize;
        private int _TotalRecords;
        private Errors _CustomError;
        private string _typeDescription;
        private string _SortOrder;
        private string _CreatedBy;
        private string _UserName;
        private UserBusiness _UserBusinessInfo;
        private string _videoTributeId;
        private string _ApplicationType;
        #endregion


        #region PROPERTIES

        public int TributeID
        {
            get { return _TributeID; }
            set { _TributeID = value; }
        }

        public int UserTributeID
        {
            get { return _UserTributeID; }
            set { _UserTributeID = value; }
        }

        public string TributeName
        {
            get { return _TributeName; }
            set { _TributeName = value; }
        }

        public string TributeType
        {
            get { return _TributeType; }
            set { _TributeType = value; }
        }

        public string TributeUrl
        {
            get { return _tributeUrl; }
            set { _tributeUrl = value; }
        }

        public string SearchString
        {
            get { return _SearchString; }
            set { _SearchString = value; }
        }

        public string ChangeSearchString
        {
            get { return _ChangeSearchString; }
            set { _ChangeSearchString = value; }
        }

        public string SearchType
        {
            get { return _SearchType; }
            set { _SearchType = value; }
        }

        public string TributeImage
        {
            get { return _TributeImage; }
            set { _TributeImage = value; }
        }

        public string Date1
        {
            get { return _Date1; }
            set { _Date1 = value; }
        }

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        public int State
        {
            get { return _State; }
            set { _State = value; }
        }

        public int Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        public string StateName
        {
            get { return _StateName; }
            set { _StateName = value; }
        }

        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }

        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public DateTime DateForSorting
        {
            get { return _DateForSorting; }
            set { _DateForSorting = value; }
        }

        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        public Nullable<DateTime> CreatedAfterDate
        {
            get { return _CreatedAfterDate; }
            set { _CreatedAfterDate = value; }
        }

        public Nullable<DateTime> CreatedBeforeDate
        {
            get { return _CreatedBeforeDate; }
            set { _CreatedBeforeDate = value; }
        }

        public string Hits
        {
            get { return _Hits; }
            set { _Hits = value; }
        }

        public string CommonString
        {
            get { return _CommonString; }
            set { _CommonString = value; }
        }

        public int PageNumber
        {
            get { return _PageNumber; }
            set { _PageNumber = value; }
        }

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }

        public int TotalRecords
        {
            get { return _TotalRecords; }
            set { _TotalRecords = value; }
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public Errors CustomError
        {
            get { return _CustomError; }
            set { _CustomError = value; }
        }

        public string TypeDescription
        {
            get { return _typeDescription; }
            set { _typeDescription = value; }

        }

        public string SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }

        }

        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        public UserBusiness UserBusinessInfo
        {
            get { return _UserBusinessInfo; }
            set { _UserBusinessInfo = value; }
        }

        public string VideoTributeId
        {
            get { return _videoTributeId; }
            set { _videoTributeId = value; }
        }
        public string ApplicationType
        {
            get { return _ApplicationType; }
            set { _ApplicationType = value; }
        }
        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Default Contructor
        /// <summary>
        public SearchTribute()
        { }

        
        /// <summary>
        /// User defined Contructor
        /// <summary>
        public SearchTribute(int TributeID, int UserTributeID, string TributeName, string TributeType, string TributeImage,
            string Date1, string Location, DateTime CreatedDate, string Hits, int TotalRecords, string SearchString,
            string City, int State, int Country, int PageNumber, int PageSize)
        {
            this._TributeID = TributeID;
            this._UserTributeID = UserTributeID;
            this._TributeName = TributeName;
            this._TributeImage = TributeImage;
            this._Date1 = Date1;
            this._DateForSorting = CreatedDate;
            this._Location = Location;
            this._Hits = Hits;
            this._SearchString = SearchString;
            this._City = City;
            this._State = State;
            this._Country = Country;
            this._PageNumber = PageNumber;
            this._PageSize = PageSize;
            this._TotalRecords = TotalRecords;
        }
        /// <summary>
        /// Constructor added bu Udham
        /// For searching website wise
        /// </summary>
        public SearchTribute(int TributeID, int UserTributeID, string TributeName, string TributeType, string TributeImage,
            string Date1, string Location, DateTime CreatedDate, string Hits, int TotalRecords, string SearchString,
            string City, int State, int Country, int PageNumber, int PageSize,string ApplicationType)
        {
            this._TributeID = TributeID;
            this._UserTributeID = UserTributeID;
            this._TributeName = TributeName;
            this._TributeImage = TributeImage;
            this._Date1 = Date1;
            this._DateForSorting = CreatedDate;
            this._Location = Location;
            this._Hits = Hits;
            this._SearchString = SearchString;
            this._City = City;
            this._State = State;
            this._Country = Country;
            this._PageNumber = PageNumber;
            this._PageSize = PageSize;
            this._TotalRecords = TotalRecords;
            this._ApplicationType = ApplicationType;
        }

        #endregion


        #region SORTING METHOD

        public class AscendingSorter : IComparer
        {
            public int Compare(Object x, Object y)
            {
                SearchTribute p1 = (SearchTribute)x;
                IComparable ic1 = (IComparable)p1.DateForSorting;

                SearchTribute p2 = (SearchTribute)y;
                IComparable ic2 = (IComparable)p2.DateForSorting;

                return ic1.CompareTo(ic2);
            }
        }

        public  class DescendingSorter : IComparer
        {
            public int Compare(Object x, Object y)
            {
                SearchTribute p1 = (SearchTribute)x;
                IComparable ic1 = (IComparable)p1.DateForSorting;

                SearchTribute p2 = (SearchTribute)y;
                IComparable ic2 = (IComparable)p2.DateForSorting;

                return ic2.CompareTo(ic1);
            }
        }

        #endregion
    }


}
