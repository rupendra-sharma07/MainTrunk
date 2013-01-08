///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Gifts.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about Gifts
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;

#endregion

/// <summary>
///Tribute Portal-Gifts Entity Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the Gifts Entity class for the Gift. it contain all the variable required for the Gift Module
/// </summary>
/// 

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class Gifts
    {
        #region ENUM

        public enum GiftEnum
        {
            GiftId,
            TributeId,
            TributeType,
            UserId,
            ImageId,
            UserName,
            ImageUrl,
            TributeName,
            GiftMessage,
            GiftSentBy,
            CreatedBy,
            ModifiedBy,
            CreatedDate,
            ModifiedDate,
            IsActive,
            IsDeleted,
            Error
        }

        public enum GiftMaintainState
        {
            Gift_Admin,
        }

        #endregion


        #region FIELDS

        private int _GiftId;
        private int _TributeId;
        private Nullable<int> _UserId;
        private int _ImageId;
        private string _UserName;
        private string _ImageUrl;

        private string _TributeName;
        private string _TributeType;
        private string _TributeURL;
        private string _GiftImage;
        private string _GiftMessage;
        private string _GiftSentBy;
        private string _Location;
        private int _PageSize;
        private int _CurrentPage;
        private int _TotalRecordCount;
        
        private int _CreatedBy;
        private string _CreatedDate;
        private int _ModifiedBy;
        private Nullable<DateTime> _ModifiedDate;

        private bool _IsAdmin;
        private Errors _CustomError;
        private string _FirstName;
        private string _LastName;
        private string _UrlToEmail;

        #endregion


        #region PROPERTIES

        public int GiftId
        {
            get { return _GiftId; }
            set { _GiftId = value; }
        }

        public int TributeId
        {
            get { return _TributeId; }
            set { _TributeId = value; }
        }

        public Nullable<int> UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public int ImageId
        {
            get { return _ImageId; }
            set { _ImageId = value; }
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public string ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
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

        public string TributeURL
        {
            get { return _TributeURL; }
            set { _TributeURL = value; }
        }

        public string GiftImage
        {
            get { return _GiftImage; }
            set { _GiftImage = value; }
        }

        public string GiftMessage
        {
            get { return _GiftMessage; }
            set { _GiftMessage = value; }
        }

        public string GiftSentBy
        {
            get { return _GiftSentBy; }
            set { _GiftSentBy = value; }
        }

        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
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

        public string UrlToEmail
        {
            get { return _UrlToEmail; }
            set { _UrlToEmail = value; }
        }

        public int CurrentPage
        {
            get { return _CurrentPage; }
            set { _CurrentPage = value; }
        }

        public int TotalRecordCount
        {
            get { return _TotalRecordCount; }
            set { _TotalRecordCount = value; }
        }

        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set { _IsAdmin = value; }
        }

        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        public Nullable<DateTime> ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        public Errors CustomError
        {
            get { return _CustomError; }
            set { _CustomError = value; }
        }

        #endregion
    }
}
