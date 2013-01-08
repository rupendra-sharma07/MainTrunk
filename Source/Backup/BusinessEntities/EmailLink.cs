///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.EmailLink.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the Email Link object
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public partial class EmailLink
    {
        #region FIELDS
        private string _emailSubject;
        private string _emailBody;
        private string _fromUserName;
        private string _fromEmailAddress;
        private string _urlToEmail;
        private string _typeName;
        private List<string> _emailTo;
        private int _EventID;
        private string _EventName;
        #endregion

        #region PROPERTIES
        public string EmailSubject
        {
            get { return _emailSubject; }
            set 
            {
                _emailSubject = value;
            }
        }
        public string EmailBody
        {
            get { return _emailBody; }
            set
            {
                _emailBody = value;
            }
        }
        public string FromUserName
        {
            get { return _fromUserName; }
            set
            {
                _fromUserName = value;
            }
        }
        public string FromEmailAddress
        {
            get { return _fromEmailAddress; }
            set
            {
                _fromEmailAddress = value;
            }
        }
        public string UrlToEmail
        {
            get { return _urlToEmail; }
            set
            {
                _urlToEmail = value;
            }
        }
        public string TypeName
        {
            get { return _typeName; }
            set
            {
                _typeName = value;
            }
        }
        public List<string> EmailTo
        {
            get { return _emailTo; }
            set
            {
                _emailTo = value;
            }
        }

        public int EventID
        {
            get { return _EventID; }
            set
            {
                _EventID = value;
            }
        }

        public string EventName
        {
            get { return _EventName; }
            set
            {
                _EventName = value;
            }
        }

        #endregion
    }
}
