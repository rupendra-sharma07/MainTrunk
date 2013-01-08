///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.UserRegistration.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the 
///                     Details about User Registration
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class UserRegistration
    {
        private Errors _ObjErrors;
        private Users _ObjUsers;
        private UserBusiness _ObjUserBusiness;
        private EmailNotification _ObjEmailNotification;
        private UserCreditcardDetails _ObjUserCreditcardDetails;
        private CreditPointTransaction _objCreditPointTransaction;
        private CreditCostMapping _objCreditCostMapping;
       

        public Errors CustomError
        {
            get { return _ObjErrors; }
            set { _ObjErrors = value; }
        }

        public Users Users
        {
            get { return _ObjUsers; }
            set { _ObjUsers = value; }
        }

        public UserBusiness UserBusiness
        {
            get { return _ObjUserBusiness; }
            set { _ObjUserBusiness = value; }
        }

        public EmailNotification EmailNotification
        {
            get { return _ObjEmailNotification; }
            set { _ObjEmailNotification = value; }
        }
        public UserCreditcardDetails UserCreditcardDetails
        {
            get { return _ObjUserCreditcardDetails; }
            set { _ObjUserCreditcardDetails = value; }
        }

        public CreditPointTransaction CreditPointTransaction
        {
            get { return _objCreditPointTransaction; }
            set { _objCreditPointTransaction = value; }
        }

        public CreditCostMapping CreditCostMapping
        {
            get { return _objCreditCostMapping; }
            set { _objCreditCostMapping = value; }
        }
    }
}
