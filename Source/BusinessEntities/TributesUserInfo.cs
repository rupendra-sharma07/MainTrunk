///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.TributesUserInfo.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details 
///                  of the users related to tributes
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class TributesUserInfo
    {
        private Tributes _objTributes;
        private UserInfo _objUserInfo;
        private Errors _objErrors;
        private TributeAdministrator _TributeAdministrator;

        public TributeAdministrator TributeAdministrator
        {
            get { return _TributeAdministrator; }
            set { _TributeAdministrator = value; }
        }

        public Tributes Tributes
        {
            get { return _objTributes; }
            set { _objTributes = value; }
        }
        public UserInfo Requester
        {
            get { return _objUserInfo; }
            set { _objUserInfo = value; }
        }
        public Errors CustomError
        {
            get { return _objErrors; }
            set { _objErrors = value; }
        }
    }
}
