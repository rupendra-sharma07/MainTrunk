///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Note.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about Notes
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DURECTIVES
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public partial class Note
    {
        #region ENUM
        public enum NotesEnum
        {
            NotesId,
            UserTributeId,
            UserId,
            Title,
            PostMessage,
            MessageWithoutHtml,
            CreatedBy,
            CreatedDate,
            ModifiedBy,
            ModifiedDate,
            IsDeleted
        }
        #endregion

        #region FIELDS
        private Nullable<int> _notesId;
        private Nullable<int> _userTributeId;
        private Nullable<int> _userId;
        private string _title;
        private string _postMessage;
        private string _messageWithoutHtml;
        private Nullable<int> _createdBy;
        private Nullable<DateTime> _createdDate;
        private Nullable<int> _modifiedBy;
        private Nullable<DateTime> _modifiedDate;
        private bool _isDeleted;
        private bool _isLocationHide;
        private string _userName;
        private int _currentPage;
        private int _pageSize;
        private int _totalRecords;
        private string _creationDate; //for displaying date in the required format
        private string _creationTime; //for displaying time in the required format
        //private Users _userDetails;
        private string _city;
        private string _state;
        private string _country;
        private string _location;
        private int _commentCount;
        private string _userImage;
        private string _moduleTypeName;
        private string _tributeName;
        private string _tributeType;
        private string _tributeUrl;
        private string _pathToVisit;
        private Nullable<Int64> _FacebookUid;
        #endregion

        #region PROPERTIES
        public Nullable<int> NotesId
        {
            get { return _notesId; }
            set { _notesId = value; }
        }
        public Nullable<int> UserTributeId
        {
            get { return _userTributeId; }
            set { _userTributeId = value; }
        }
        public Nullable<int> UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public string PostMessage
        {
            get { return _postMessage; }
            set { _postMessage = value; }
        }
        public string MessageWithoutHtml
        {
            get { return _messageWithoutHtml; }
            set { _messageWithoutHtml = value; }
        }
        public Nullable<int> CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }
        public Nullable<DateTime> CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
        public Nullable<int> ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }
        public Nullable<DateTime> ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
        public bool IsLocationHide
        {
            get { return _isLocationHide; }
            set { _isLocationHide = value; }
        }
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; }
        }
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        public int TotalRecords
        {
            get { return _totalRecords; }
            set { _totalRecords = value; }
        }
        public string CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }
        public string CreationTime
        {
            get { return _creationTime; }
            set { _creationTime = value; }
        }
        //private Users UserDetails
        //{
        //    get { return _userDetails; }
        //    set { _userDetails = value; }
        //}
        public int CommentCount
        {
            get { return _commentCount; }
            set { _commentCount = value; }
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
        public string UserImage
        {
            get { return _userImage; }
            set { _userImage = value; }
        }
        public Nullable<Int64> FacebookUid
        {
            get { return _FacebookUid; }
            set { _FacebookUid = value; }
        }
        public string ModuleTypeName
        {
            get { return _moduleTypeName; }
            set { _moduleTypeName = value; }
        }
        public string TributeName
        {
            get { return _tributeName; }
            set { _tributeName = value; }
        }
        public string TributeType
        {
            get { return _tributeType; }
            set { _tributeType = value; }
        }
        public string TributeUrl
        {
            get { return _tributeUrl; }
            set { _tributeUrl = value; }
        }
        public string PathToVisit
        {
            get { return _pathToVisit; }
            set { _pathToVisit = value; }
        }
        #endregion
    }
}
