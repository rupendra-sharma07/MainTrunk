///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.TributeLatest.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about the 
///                   latest tributes
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class TributeLatest
    {
       
        private Errors _ObjErrors;
        private string _FirstName;
        private string _VideoCaption;
        private string _VideoDesc;
        private string _VideoUrl;
        private string _VideoTypeId;
        private string _CommentMessage;
        private string _Title;
        private Nullable<DateTime> _CreationDate;
        private Nullable<DateTime> _SecondDate;
        private int _TributeId;
        private string _Type;
        private int _ID;
        private int _UserId;
        private string _videoTributeUrl; //added by Gaurav Puri on 16-May-2008

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        private string _Mode;

        public string Type_
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public int TributeId
        {
            get { return _TributeId; }
            set { _TributeId = value; }        
        }

        public Errors CustomError
        {
            get { return _ObjErrors; }
            set { _ObjErrors = value; }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string VideoCaption
        {
            get { return _VideoCaption; }
            set { _VideoCaption = value; }
        }
        public string VideoDesc
        {
            get { return _VideoDesc; }
            set { _VideoDesc = value; }
        }
        public string VideoUrl
        {
            get { return _VideoUrl; }
            set { _VideoUrl = value; }
        }
        public string VideoTypeId
        {
            get { return _VideoTypeId; }
            set { _VideoTypeId = value; }
        }
        public string CommentMessage
        {
            get { return _CommentMessage; }
            set { _CommentMessage = value; }
        }
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public Nullable<DateTime> CreationDate
        {
            get { return _CreationDate; }
            set { _CreationDate = value; }
        }
        public Nullable<DateTime> SecondDate
        {
            get { return _SecondDate; }
            set { _SecondDate = value; }
        }

        //Added by Gaurav Puri on 16-May-2008
        public string VideoTributeUrl
        {
            get { return _videoTributeUrl; }
            set { _videoTributeUrl = value; }
        }
       
    }
}
