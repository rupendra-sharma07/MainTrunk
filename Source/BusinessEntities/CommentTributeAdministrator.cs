///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.CommentTributeAdministrator.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the Comment Tribute Administrator object
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class CommentTributeAdministrator
    {
        #region FIELDS
        private int _CommentId;
        private int _UserId;
        private Nullable<Int64> _FacebookUid;
        private int _TypeCodeId;
        private int _CommentTypeId;
        private int _CreatedBy;
        private int _UserType;
        private int _IsAdmin;
        private int _CurrentPage;
        private int _PageSize;
        private int _TributeId;
        private int _totalRecords;
        private string _Message;
        private string _UserImage;
        private string _UserName;
        private Boolean _IsPrivate;
        private bool _isLocationHide;
        private string _CreatedDate;
        private string _city;
        private string _state;
        private string _country;
        private string _location; //for changing location
        private string _typeCodeName;

        //Added new on 22 jun 2011 by rupendra to get the Table type from wich the comments are fetched
        // Here 1 --> Comments , 2--> tblComments_New
        private string _tableType;
        #endregion

        #region PROPERTIES
        public int TributeId
        {
            get { return _TributeId; }
            set { _TributeId = value; }
        }
        public int CommentId
        {
            get { return _CommentId; }
            set { _CommentId = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public Nullable<Int64> FacebookUid
        {
            get { return _FacebookUid; }
            set { _FacebookUid = value; }
        }
        public int CommentTypeId
        {
            get { return _CommentTypeId; }
            set { _CommentTypeId = value; }
        }
        public int TypeCodeId
        {
            get { return _TypeCodeId; }
            set { _TypeCodeId = value; }
        }
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public int UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }
        public int CurrentPage
        {
            get { return _CurrentPage; }
            set { _CurrentPage = value; }
        }
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        public int IsAdmin
        {
            get { return _IsAdmin; }
            set { _IsAdmin = value; }
        }
        public int TotalRecords
        {
            get { return _totalRecords; }
            set { _totalRecords = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public string UserImage
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public Boolean IsPrivate
        {
            get { return _IsPrivate; }
            set { _IsPrivate = value; }
        }
        public Boolean IsLocationHide
        {
            get { return _isLocationHide; }
            set { _isLocationHide = value; }
        }
        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }
        public string TypeCodeName
        {
            get { return _typeCodeName; }
            set { _typeCodeName = value; }
        }

        public string TableType
        {
            get { return _tableType; }
            set { _tableType = value; }
        }
        #endregion

        #region CONSTRUCTOR
        public CommentTributeAdministrator()
        {
        }

        public CommentTributeAdministrator(int CommentId, int UserId, int TypeCodeId, 
            int CommentTypeId, string Message, int CreatedBy, string CreatedDate, 
            int UserType, string userImage, int IsAdmin, string UserName, 
            string City, string State, string Country, bool IsLocationHide,
            Nullable<Int64> FacebookUid
            //Added new on 22 jun 2011 by rupendra to get the Table type from wich the comments are fetched
            // Here 1 --> Comments , 2--> tblComments_New
            ,string sTableType
            )
        {


            _CommentId = CommentId;
            _UserId = UserId;
            _TypeCodeId = TypeCodeId;
            _CommentTypeId = CommentTypeId;
            _Message = Message;
            _CreatedBy = CreatedBy;
            _CreatedDate = CreatedDate;
            _UserType = UserType;
            _UserImage = userImage;
            _IsAdmin = IsAdmin;
            _UserName = UserName;
            _city = City;
            _state = State;
            _country = Country;
            _isLocationHide = IsLocationHide;
            _FacebookUid = FacebookUid;
            //Added new on 22 jun 2011 by rupendra to get the Table type from wich the comments are fetched
            // Here 1 --> Comments , 2--> tblComments_New
            _tableType = sTableType;
        }


        // Overloaded 
        public CommentTributeAdministrator(int CommentId, int UserId, int TypeCodeId,
           int CommentTypeId, string Message, int CreatedBy, string CreatedDate,
           int UserType, string userImage, int IsAdmin, string UserName,
           string City, string State, string Country, bool IsLocationHide,
           Nullable<Int64> FacebookUid
           
           )
        {


            _CommentId = CommentId;
            _UserId = UserId;
            _TypeCodeId = TypeCodeId;
            _CommentTypeId = CommentTypeId;
            _Message = Message;
            _CreatedBy = CreatedBy;
            _CreatedDate = CreatedDate;
            _UserType = UserType;
            _UserImage = userImage;
            _IsAdmin = IsAdmin;
            _UserName = UserName;
            _city = City;
            _state = State;
            _country = Country;
            _isLocationHide = IsLocationHide;
            _FacebookUid = FacebookUid;
           
        }
        #endregion
    }
}
