///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.GetMyTributes.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to get tributes for a particular user
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class GetMyTributes
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public GetMyTributes()
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


        public System.String TypeDescription
        {
            get { return _TypeDescription; }
            set { _TypeDescription = value; }
        }
        private System.String _TypeDescription;


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

        //LHK; for speed up issue
        public string TributeHomeUrl
        {
            get { return _TributeHomeUrl; }
            set { _TributeHomeUrl = value; }
        }
        private string _TributeHomeUrl;

        /// <summary>
        /// User defined Contructor
        /// <summary>
		public GetMyTributes(System.Int32 TributeId,
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
		public GetMyTributes(System.Int32 TributeId,
            System.String TributeName,
            System.String TypeDescription,
            System.DateTime StartDate,
            System.String Enddate,
            System.Int32 Visit,
            System.Boolean EmailAlert,
            string TributeHomeUrl)
        {
            this._TributeId = TributeId;
            this._TributeName = TributeName;
            this._TypeDescription = TypeDescription;
            this._StartDate = StartDate;
            this._Enddate = Enddate;
            this._Visit = Visit;
            this._EmailAlert = EmailAlert;
            this._TributeHomeUrl = TributeHomeUrl;
        }
        
    }
}
