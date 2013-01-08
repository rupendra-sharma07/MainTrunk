///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Comments.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the Comment object
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class Comments
    {
        #region FIELDS
        private int _CommentId;
        private int _UserId;
        private int _TypeCodeId;
        private int _CommentTypeId;
        private int _TributeId;
        private string _Message;
        private int _CreatedBy;
        private int _ModifiedBy;
        private DateTime _CreatedDate;
        private DateTime _ModifiedDate;
        private Boolean _IsPrivate;
        private Boolean _IsActive;
        private Boolean _IsDeleted;
        private string _codeTypeName;
        private Errors objErr;
        private string _moduleFunctionalityName;
        private string _tributeName;
        private string _tributeType;
        private string _tributeUrl;
        private string _pathToVisit;
        private string _userName;

        // Added on 21 jun-2011 by rupendra
        private string _userType;
        private string _tableType;
        #endregion

        #region CONSTRUCTOR
        public Comments()
        {
        }
        public Comments(int CommentId, int UserId, int TypeCodeId, int CommentTypeId, string Message, int CreatedBy, DateTime CreatedDate)
        {
            this._CommentId = CommentId;
            this._UserId = UserId;
            this._TypeCodeId = TypeCodeId;
            this._CommentTypeId = CommentTypeId;
            this._Message = Message;
            this._CreatedBy = CreatedBy;
            this._CreatedDate = CreatedDate;
        }
        #endregion

        #region PROPERTIES
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
        public int CommentTypeId
        {
            get { return _CommentTypeId; }
            set { _CommentTypeId = value; }
        }
        public int TributeId
        {
            get { return _TributeId; }
            set { _TributeId = value; }
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
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public Boolean IsPrivate
        {
            get { return _IsPrivate; }
            set { _IsPrivate = value; }
        }
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }
        public Boolean IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        public Errors CustomError
        {
            get { return objErr; }
            set { objErr = value; }
        }
        public string CodeTypeName
        {
            get { return _codeTypeName; }
            set { _codeTypeName = value; }
        }
        public string ModuleFunctionalityName
        {
            get { return _moduleFunctionalityName; }
            set { _moduleFunctionalityName = value; }
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

        // Added by 21-june-2011
        public string UserType
        {
            get { return _userType; }
            set { _userType  = value; }
        }
        public string TableType
        {
            get { return _tableType; }
            set { _tableType = value; }
        }
        #endregion
    }
}
