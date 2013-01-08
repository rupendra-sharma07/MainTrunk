///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Videos.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the partial class used to handle the Details about Videos
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TributesPortal.BusinessEntities
{
    /// <summary>
    /// Class for the Video Entity
    /// </summary>
    [Serializable]
    public partial class Videos
    {
        #region ENUM
        public enum VideoEnum
        {
            UserId,
            UserTributeId,
            CreatedBy,
            ModifiedBy,
            VideoId,
            VideoTypeId,
            TributeVideoId,
            VideoCaption,
            VideoDesc,
            VideoUrl,
            CreatedDate,
            ModifiedDate,
            IsActive,
            IsDeleted,
            Error
        }
        #endregion

        #region CONSTRUCTOR
        public Videos()
        {
        }
        #endregion

        #region FIELDS
        private Nullable<int> _createdBy;
        private Nullable<int> _userId;
        private Nullable<int> _userTributeId;
        private Nullable<int> _modifiedBy;
        private Nullable<int> _videoId;
        private string _videoTypeId;
        private string _tributeVideoId;
        private string _videoCaption;
        private string _videoDesc;
        private string _videoUrl;
        private Nullable<DateTime> _createdDate;
        private Nullable<DateTime> _modifiedDate;
        private bool _isActive;
        private bool _isDeleted;
        private Errors objErr;
        private string _moduleTypeName;
        private string _tributeName;
        private string _tributeType;
        private string _tributeUrl;
        private string _pathToVisit;
        private string _userName;
        private bool _isTributeActive;
        #endregion

        #region PROPERTIES
        public Nullable<int> CreatedBy
        {
            get { return this._createdBy; }
            set { this._createdBy = value; }
        }
        
        public Nullable<int> ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }

        public string TributeVideoId
        {
            get { return this._tributeVideoId; }
            set { this._tributeVideoId = value; }
        }
        
        public Nullable<int> UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        public Nullable<int> VideoId
        {
            get { return this._videoId; }
            set { this._videoId = value; }
        }

        public string VideoTypeId
        {
            get { return this._videoTypeId; }
            set { this._videoTypeId = value; }
        }

        public Nullable<int> UserTributeId
        {
            get { return this._userTributeId; }
            set { this._userTributeId = value; }
        }

        public string VideoCaption
        {
            get { return this._videoCaption; }
            set { this._videoCaption = value; }
        }
        
        public string VideoDesc
        {
            get { return this._videoDesc; }
            set { this._videoDesc = value; }
        }

        public string VideoUrl
        {
            get { return this._videoUrl; }
            set { this._videoUrl = value; }
        }

        public Nullable<DateTime> CreatedDate
        {
            get { return this._createdDate; }
            set { this._createdDate = value; }
        }

        public Nullable<DateTime> ModifiedDate
        {
            get { return this._modifiedDate; }
            set { this._modifiedDate = value; }
        }

        public bool IsActive
        {
            get { return this._isActive; }
            set { this._isActive = value; }
        }

        public bool IsDeleted
        {
            get { return this._isDeleted; }
            set { this._isDeleted = value; }
        }

        public Errors CustomError
        {
            get { return objErr; }
            set { objErr = value; }
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
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public bool IsTributeActive
        {
            get { return this._isTributeActive; }
            set { this._isTributeActive = value; }
        }

        #endregion

    }
}
