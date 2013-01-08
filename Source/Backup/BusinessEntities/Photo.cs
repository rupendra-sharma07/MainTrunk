///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Photos.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about Photos
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public partial class Photos
    {
        #region ENUM
        public enum PhotoEnum
        {
            PhotoId,
            PhotoAlbumId,
            UserTributeId,
            UserId,
            CreatedBy,
            ModifiedBy,
            PhotoCaption,
            PhotoDesc,
            PhotoImage,
            CreatedDate,
            ModifiedDate,
            IsActive,
            IsDeleted
        }
        #endregion

        #region FIELDS
        private int _photoId;
        private int _photoAlbumId;
        private int _userTributeId;
        private int _userId;
        private int _createdBy;
        private int _modifiedBy;
        private int _commentCount;
        private int _pageNumber;
        private int _pageSize;
        private string _sortOrder = "asc";  // possible values: asc, desc
        private int _totalRecords;
        private int _nextRecordCount;
        private int _prevRecordCount;
        private int _isAdmin;
        private int _recordNumber;
        private string _photoCaption;
        private string _photoAlbumCaption;
        private string _photoDesc;
        private string _photoImage;
        private string _userName;
        private string _nextPrev;
        private string _creationDate;
        private string _modificationDate;
        private string _moduleTypeName;
        private DateTime _createdDate;
        private DateTime _modifiedDate;
        private bool _isActive;
        private bool _isDeleted;
        private string _tributeName;
        private string _tributeType;
        private string _tributeUrl;
        private string _pathToVisit;
        #endregion

        #region PROPERTIES
        public int PhotoId
        {
            get
            {
                return _photoId;
            }
            set
            {
                _photoId = value;
            }
        }
        public int PhotoAlbumId
        {
            get
            {
                return _photoAlbumId;
            }
            set
            {
                _photoAlbumId = value;
            }
        }
        public int UserTributeId
        {
            get
            {
                return _userTributeId;
            }
            set
            {
                _userTributeId = value;
            }
        }
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }
        public int CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                _createdBy = value;
            }
        }
        public int ModifiedBy
        {
            get
            {
                return _modifiedBy;
            }
            set
            {
                _modifiedBy = value;
            }
        }
        public int CommentCount
        {
            get
            {
                return _commentCount;
            }
            set
            {
                _commentCount = value;
            }
        }
        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value; }
        }
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        public int TotalRecords
        {
            get
            {
                return _totalRecords;
            }
            set
            {
                _totalRecords = value;
            }
        }
        public string SortOrder
        {
            get
            {
                return _sortOrder;
            }
            set
            {
                _sortOrder = value;
            }
        }
        public int NextRecordCount
        {
            get { return _nextRecordCount; }
            set { _nextRecordCount = value; }
        }

        public int PrevRecordCount
        {
            get { return _prevRecordCount; }
            set { _prevRecordCount = value; }
        }

        public int IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }
        public int RecordNumber
        {
            get { return _recordNumber; }
            set { _recordNumber = value; }
        }
        public string PhotoCaption
        {
            get
            {
                return _photoCaption;
            }
            set
            {
                _photoCaption = value;
            }
        }
        public string PhotoAlbumCaption
        {
            get
            {
                return _photoAlbumCaption;
            }
            set
            {
                _photoAlbumCaption = value;
            }
        }
        public string PhotoDesc
        {
            get
            {
                return _photoDesc;
            }
            set
            {
                _photoDesc = value;
            }
        }
        public string PhotoImage
        {
            get 
            {
                return _photoImage;
            }
            set
            {
                _photoImage = value;
            }
        }
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public string NextPrev
        {
            get { return _nextPrev; }
            set { _nextPrev = value; }
        }
        public string CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }
        public string ModificationDate
        {
            get { return _modificationDate; }
            set { _modificationDate = value; }
        }
        public string ModuleTypeName
        {
            get { return _moduleTypeName; }
            set { _moduleTypeName = value; }
        }
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
            set
            {
                _createdDate = value;
            }
        }
        public DateTime ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }
            set
            {
                _modifiedDate = value;
            }
        }
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }
        public bool IsDeleted
        {
            get
            {
                return _isDeleted;
            }
            set
            {
                _isDeleted = value;
            }
        }

        public DateTime ToDate
        {
            get
            {
                return _createdDate;
            }
            set
            {
                _createdDate = value;
            }
        }
        public DateTime FromDate
        {
            get
            {
                return _createdDate;
            }
            set
            {
                _createdDate = value;
            }
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
