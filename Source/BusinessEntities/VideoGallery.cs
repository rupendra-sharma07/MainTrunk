///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.VideoGallery.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the class used to handle the Details about VideoGallery
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public partial class VideoGallery
    {
        #region FIELDS
        private Errors _objErrors;
        private Videos _objVideos;
        private string _userName;
        private string _createdDate;
        private string _idForDisplay;
        private string _nextPrev;
        private int _commentCount;
        private int _pageNumber;
        private int _pageSize;
        private int _totalRecords;
        private int _nextRecordCount;
        private int _prevRecordCount;
        private int _isAdmin;
        private int _recordNumber;
        #endregion

        #region PROPERTIES
        public Errors CustomError
        {
            get { return _objErrors; }
            set { _objErrors = value; }
        }

        public Videos Videos
        {
            get { return _objVideos; }
            set { _objVideos = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public string IdForDisplay
        {
            get { return _idForDisplay; }
            set { _idForDisplay = value; }
        }

        public string NextPrev
        {
            get { return _nextPrev; }
            set { _nextPrev = value; }
        }

        public int CommentCount
        {
            get { return _commentCount; }
            set { _commentCount = value; }
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
            get { return _totalRecords; }
            set { _totalRecords = value; }
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
        #endregion

    }
}
