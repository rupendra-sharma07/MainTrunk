///Copyright       : Copyright (c) optimus info India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.GetMyTributesFeed.cs
///Author          : LHK
///Creation Date   : 6:05 PM 3/30/2011
///Description     : This file defines the attributes of the object used to get tributes for a particular user
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
     [Serializable]
    public class GetTributesFeed
    {/// <summary>
        /// Default Contructor
        /// <summary>
        public GetTributesFeed()
        { }

        public Errors CustomError
        {
            get { return _ObjErrors; }
            set { _ObjErrors = value; }
        }
        private Errors _ObjErrors;

        public System.Int32 UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private System.Int32 _UserId;

        public System.Int32 TributeId
        {
            get { return _TributeId; }
            set { _TributeId = value; }
        }
        private System.Int32 _TributeId;


        public System.String TributeName
        {
            get { return _TributeName; }
            set { _TributeName = value; }
        }
        private System.String _TributeName;

        public System.String TributeImage
        {
            get { return _TributeImage; }
            set { _TributeImage = value; }
        }
        private System.String _TributeImage;

        public System.String TypeDescription
        {
            get { return _TypeDescription; }
            set { _TypeDescription = value; }
        }
        private System.String _TypeDescription;

        public System.String DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        private System.String _DOB;
        public System.String DOD
        {
            get { return _DOD; }
            set { _DOD = value; }
        }
        private System.String _DOD;

        private DateTime _modifiedDate;

        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }

        public System.String MessageWithoutHtml
        {
            get { return _messageWithoutHtml; }
            set { _messageWithoutHtml = value; }
        }
        private System.String _messageWithoutHtml;

        public System.String TributeUrl
        {
            get { return _TributeUrl; }
            set { _TributeUrl = value; }
        }
        private System.String _TributeUrl;

        public System.DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private System.DateTime _StartDate;


        public DateTime Renewaldate
        {
            get { return _Renewaldate; }
            set { _Renewaldate = value; }
        }
        private DateTime _Renewaldate;

        public System.String Enddate
        {
            get { return _Enddate; }
            set { _Enddate = value; }
        }
        private System.String _Enddate;


        public System.Int32 Visit
        {
            get { return _Visit; }
            set { _Visit = value; }
        }
        private System.Int32 _Visit;


        public System.Boolean EmailAlert
        {
            get { return _EmailAlert; }
            set { _EmailAlert = value; }
        }
        private System.Boolean _EmailAlert;

        public string ExpiredOn
        {
            get { return _expiredOn; }
            set { _expiredOn = value; }
        }
        private string _expiredOn;

        /// <summary>
        /// User defined Contructor
        /// <summary>
        public GetTributesFeed(System.Int32 TributeId,
            System.String TributeName,
            System.String TypeDescription,
            System.DateTime StartDate,
            System.String Enddate,
            System.Int32 Visit,
            System.Boolean EmailAlert)
        {
            this._TributeId = TributeId;
            this._TributeName = TributeName;
            this._TypeDescription = TypeDescription;
            this._StartDate = StartDate;
            this._Enddate = Enddate;
            this._Visit = Visit;
            this._EmailAlert = EmailAlert;
        }

    }
}
