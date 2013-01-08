///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Tributes.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the tribute Details
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{

    [Serializable]
    public partial class Tributes
    {
        public enum TributeEnum
        {
            TributeId,
            UserTributeId,
            TributeName,
            TributeType,
            WelcomeMessage,
            TributeImage,
            TributeUrl,
            ThemeId,
            City,
            State,
            Country,
            IsPrivate,
            IsOrderDVDChecked,
            IsMemTributeBoxChecked,
            IsActive,
            IsDeleted,
            Date1,
            Date2,
            Attribute1,
            Attribute2,
            CreatedBy,
            CreatedDate,
            ModifiedBy,
            ModifiedDate,
            CustomError,
            TributePackageType,
            LinkMemTributeId,  //Added for linked memorial tribute for video tribute
            PostMessage, //For Obituary note
            MessageWithoutHtml, //For Obituary note
            TributeFirstName,//YT enhancement pahse 1 26/11/2012
            TributeLastName,//YT enhancement pahse 1 26/11/2012
            MessageCreatedDate,//YT enhancement pahse 1 26/11/2012
            MessageModifiedDate,//YT enhancement pahse 1 26/11/2012
            MessageAddedModifiedBy
        }
        public Tributes()
        {

        }
        private int _tributeCode;

        public int TributeId
        {
            get { return _tributeCode; }
            set { _tributeCode = value; }
        }
        private int _userTributeId;

        public int UserTributeId
        {
            get { return _userTributeId; }
            set { _userTributeId = value; }
        }

        private string _tributeName;

        public string TributeName
        {
            get { return _tributeName; }
            set { _tributeName = value; }
        }
        private int _tributeType;

        public int TributeType
        {
            get { return _tributeType; }
            set { _tributeType = value; }
        }
        private string _message;

        public string WelcomeMessage
        {
            get { return _message; }
            set { _message = value; }
        }
        private string _tributeImage;

        public string TributeImage
        {
            get { return _tributeImage; }
            set { _tributeImage = value; }
        }
        private string _tributeUrl;

        public string TributeUrl
        {
            get { return _tributeUrl; }
            set { _tributeUrl = value; }
        }
        private int _themeId;

        public int ThemeId
        {
            get { return _themeId; }
            set { _themeId = value; }
        }
        private string _city;

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        private Nullable<int> _state;

        public Nullable<int> State
        {
            get { return _state; }
            set { _state = value; }
        }
        private int _country;

        public int Country
        {
            get { return _country; }
            set { _country = value; }
        }
        private bool _isPrivate;

        public bool IsPrivate
        {
            get { return _isPrivate; }
            set { _isPrivate = value; }
        }

        private bool _GoogleAdSense;
        public bool GoogleAdSense
        {
            get { return _GoogleAdSense; }
            set { _GoogleAdSense = value; }
        }

        private Boolean _isActive;

        public Boolean IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        private bool _isDeleted;

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }

        //added to maintain the status of donation w.r.t. a tribute
        private bool _isDonation;
        public bool IsDonation
        {
            get { return _isDonation; }
            set { _isDonation = value; }
        }

        private Nullable<DateTime> _date1;

        public Nullable<DateTime> Date1
        {
            get { return _date1; }
            set { _date1 = value; }
        }
        private Nullable<DateTime> _date2;

        public Nullable<DateTime> Date2
        {
            get { return _date2; }
            set { _date2 = value; }
        }
        private string _attribute1;

        public string Attribute1
        {
            get { return _attribute1; }
            set { _attribute1 = value; }
        }
        private string _attribute2;

        public string Attribute2
        {
            get { return _attribute2; }
            set { _attribute2 = value; }
        }

        private int _createdBy;

        public int CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }
        private DateTime _createdDate;

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
        private int _modifiedBy;

        public int ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }
        private DateTime _modifiedDate;

        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }
        private Errors _error;

        public Errors CustomError
        {
            get { return _error; }
            set { _error = value; }
        }

        private string _typeDescription;
        public string TypeDescription
        {
            get { return _typeDescription; }
            set { _typeDescription = value; }
        }

        private int _havingVideoTribute;
        public int HavingVideoTribute
        {
            get { return _havingVideoTribute; }
            set { _havingVideoTribute = value; }
        }

        private string _tributePackageType;
        public string TributePackageType
        {
            get { return _tributePackageType; }
            set { _tributePackageType = value; }
        }

        private bool _isOrderDVDChecked;

        public bool IsOrderDVDChecked
        {
            get { return _isOrderDVDChecked; }
            set { _isOrderDVDChecked = value; }
        }

        private bool _isMemTributeBoxChecked;

        public bool IsMemTributeBoxChecked
        {
            get { return _isMemTributeBoxChecked; }
            set { _isMemTributeBoxChecked = value; }
        }

        //Added for linked memorial tribute for video tribute
        private int? _linkMemTributeId;

        public Nullable<int> LinkMemTributeId
        {
            get { return _linkMemTributeId; }
            set { _linkMemTributeId = value; }
        }
        private string _postMessage;

        //Added for ObNote
        public string PostMessage
        {
            get { return _postMessage; }
            set { _postMessage = value;}
        }

        private string _messageWithoutHtml;

        public string MessageWithoutHtml
        {
            get { return _messageWithoutHtml; }
            set { _messageWithoutHtml = value; }
        }
        private string _tributeFirstName;
        public string TributeFirstName
        {
            get { return _tributeFirstName; }
            set { _tributeFirstName = value; }
        }
        private string _tributeLastName;
        public string TributeLastName
        {
            get { return _tributeLastName; }
            set { _tributeLastName = value; }
        }

        //--------------------------------
        private DateTime? _messageCreatedDate;
        public DateTime? MessageCreatedDate
        {
            get { return _messageCreatedDate; }
            set { _messageCreatedDate = value; }
        }

        private DateTime? _messageModifiedDate;
        public DateTime? MessageModifiedDate
        {
            get { return _messageModifiedDate; }
            set { _messageModifiedDate = value; }
        }

        private int _messageAddedModifiedBy;
        public int MessageAddedModifiedBy
        {
            get { return _messageAddedModifiedBy; }
            set { _messageAddedModifiedBy = value; }
        }
        


        public Tributes(int tributeID, string tributeName, int tributeType, string welcomeMessage, string tributeImage, string tributeUrl, int themeId, string city, int state, int country, bool isPrivate, bool isOrderDVDChecked, bool isMemTributeBoxChecked, bool isActive, bool isDeleted, DateTime date1, DateTime date2, string attribute1, string attribute2, int createdBy, DateTime createdDate, int modifiedBy, DateTime modifiedDate, Errors customErrors, string tributePackageType, int linkMemTributeId)
        {
            this.TributeId = tributeID;
            this.TributeName = tributeName;
            this.TributeType = tributeType;
            this.WelcomeMessage = welcomeMessage;
            this.TributeImage = tributeImage;
            this.TributeUrl = tributeUrl;
            this.ThemeId = themeId;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.IsPrivate = isPrivate;
            this.IsOrderDVDChecked = isOrderDVDChecked;
            this.IsMemTributeBoxChecked = isMemTributeBoxChecked;
            this.IsActive = isActive;
            this.IsDeleted = isDeleted;
            this.Date1 = date1;
            this.Date2 = date2;
            this.Attribute1 = attribute1;
            this.Attribute2 = attribute2;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.ModifiedBy = modifiedBy;
            this.ModifiedDate = modifiedDate;
            this.CustomError = customErrors;
            this.TributePackageType = tributePackageType;

            //Added for linked memorial tribute for video tribute
            this.LinkMemTributeId = linkMemTributeId;
        }

        public Tributes(int tributeID, string tributeName, int tributeType, string welcomeMessage, string tributeImage, string tributeUrl, int themeId, string city, int state, int country, bool isPrivate, bool isOrderDVDChecked, bool isMemTributeBoxChecked, bool isActive, bool isDeleted, DateTime date1, DateTime date2, string attribute1, string attribute2, int createdBy, DateTime createdDate, int modifiedBy, DateTime modifiedDate, Errors customErrors, string tributePackageType, int linkMemTributeId, string PostMessage, string MessageWithoutHtml, string TributeFirstName, string TributeLastName)
        {
            this.TributeId = tributeID;
            this.TributeName = tributeName;
            this.TributeType = tributeType;
            this.WelcomeMessage = welcomeMessage;
            this.TributeImage = tributeImage;
            this.TributeUrl = tributeUrl;
            this.ThemeId = themeId;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.IsPrivate = isPrivate;
            this.IsOrderDVDChecked = isOrderDVDChecked;
            this.IsMemTributeBoxChecked = isMemTributeBoxChecked;
            this.IsActive = isActive;
            this.IsDeleted = isDeleted;
            this.Date1 = date1;
            this.Date2 = date2;
            this.Attribute1 = attribute1;
            this.Attribute2 = attribute2;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.ModifiedBy = modifiedBy;
            this.ModifiedDate = modifiedDate;
            this.CustomError = customErrors;
            this.TributePackageType = tributePackageType;

            //Added for linked memorial tribute for video tribute
            this.LinkMemTributeId = linkMemTributeId;
            this.PostMessage = PostMessage;
            this.MessageWithoutHtml = MessageWithoutHtml;

            //YT enhancement phase 1: 26-11-2012

            this.TributeFirstName = TributeFirstName;
            this.TributeLastName = TributeLastName;
        }

        public Tributes(int tributeID, string tributeName, int tributeType, string welcomeMessage, string tributeImage, string tributeUrl, int themeId, string city, int state, int country, bool isPrivate, bool isOrderDVDChecked, bool isMemTributeBoxChecked, bool isActive, bool isDeleted, DateTime date1, DateTime date2, string attribute1, string attribute2, int createdBy, DateTime createdDate, int modifiedBy, DateTime modifiedDate, Errors customErrors, string tributePackageType, int linkMemTributeId, string PostMessage, string MessageWithoutHtml, string TributeFirstName, string TributeLastName, DateTime? MessageCreatedDate, DateTime? MessageModifiedDate, int MessageAddedModifiedBy)
        {
            this.TributeId = tributeID;
            this.TributeName = tributeName;
            this.TributeType = tributeType;
            this.WelcomeMessage = welcomeMessage;
            this.TributeImage = tributeImage;
            this.TributeUrl = tributeUrl;
            this.ThemeId = themeId;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.IsPrivate = isPrivate;
            this.IsOrderDVDChecked = isOrderDVDChecked;
            this.IsMemTributeBoxChecked = isMemTributeBoxChecked;
            this.IsActive = isActive;
            this.IsDeleted = isDeleted;
            this.Date1 = date1;
            this.Date2 = date2;
            this.Attribute1 = attribute1;
            this.Attribute2 = attribute2;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.ModifiedBy = modifiedBy;
            this.ModifiedDate = modifiedDate;
            this.CustomError = customErrors;
            this.TributePackageType = tributePackageType;

            //Added for linked memorial tribute for video tribute
            this.LinkMemTributeId = linkMemTributeId;
            this.PostMessage = PostMessage;
            this.MessageWithoutHtml = MessageWithoutHtml;

            //YT enhancement phase 1: 26-11-2012

            this.TributeFirstName = TributeFirstName;
            this.TributeLastName = TributeLastName;

            //YT enhancement phase 1: 26-11-2012
            this.MessageCreatedDate = MessageCreatedDate; 
            this.MessageModifiedDate = MessageModifiedDate; 
            this.MessageAddedModifiedBy = MessageAddedModifiedBy;

        }
    }
}
