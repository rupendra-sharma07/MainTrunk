///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.MailMessage.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about Mail Messages
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class MailMessage
    {


        public MailMessage()
        { }


        //private int _MessageId;
        //public int MessageId
        //{ 
        //    get { return _MessageId; }
        //    set { _MessageId = value; }
        //}



        private string _SendByUser;
        public string SendByUser
        {
            get { return _SendByUser; }
            set { _SendByUser = value; }
        }

        private string _UserImage;
        public string UserImage
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }


        private int _MessageId;
        public int MessageId
        {
            get { return _MessageId; }
            set { _MessageId = value; }
        }

        private int _SendByUserId;
        public int SendByUserId
        {
            get { return _SendByUserId; }
            set { _SendByUserId = value; }
        }


        private int _ParantMsgId;
        public int ParantMsgId
        {
            get { return _ParantMsgId; }
            set { _ParantMsgId = value; }
        }

        private string _Subject;
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }


        private string _Body;
        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }


        private int _SendToUserId;
        public int SendToUserId
        {
            get { return _SendToUserId; }
            set { _SendToUserId = value; }
        }


        private System.DateTime _SendDate;
        public System.DateTime SendDate
        {
            get { return _SendDate; }
            set { _SendDate = value; }
        }


        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }


        private System.DateTime _RecievedDate;
        public System.DateTime RecievedDate
        {
            get { return _RecievedDate; }
            set { _RecievedDate = value; }
        }


        private int _CreatedBy;
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }


        private System.DateTime _CreatedDate;
        public System.DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }


        private int _ModifiedBy;
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }


        private System.DateTime _ModifiedDate;
        public System.DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }


        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }


        private bool _IsDeleted;
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        private Errors _ObjErrors;
        public Errors CustomError
        {
            get { return _ObjErrors; }
            set { _ObjErrors = value; }
        }

        /// <summary>
        /// User defined Contructor
        /// <summary>
        public MailMessage(//int MessageId, 
        int SendByUserId,
        string Subject,
        string Body,
        int SendToUserId,
        System.DateTime SendDate,
        int Status,
        System.DateTime RecievedDate,
        int CreatedBy,
        System.DateTime CreatedDate,
        int ModifiedBy,
        System.DateTime ModifiedDate,
        bool IsActive,
        bool IsDeleted)
        {
            //this._MessageId = MessageId;
            this._SendByUserId = SendByUserId;
            this._Subject = Subject;
            this._Body = Body;
            this._SendToUserId = SendToUserId;
            this._SendDate = SendDate;
            this._Status = Status;
            this._RecievedDate = RecievedDate;
            this._CreatedBy = CreatedBy;
            this._CreatedDate = CreatedDate;
            this._ModifiedBy = ModifiedBy;
            this._ModifiedDate = ModifiedDate;
            this._IsActive = IsActive;
            this._IsDeleted = IsDeleted;
        }
    }
}
