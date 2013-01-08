///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.GenralUserInfo.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the entity to be used when getting the Genral User Info
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class GenralUserInfo
    {
        private Errors _ObjErrors;
        private UserInfo _ObjUser;
        
        public Errors CustomError
        {
            get { return _ObjErrors; }
            set { _ObjErrors = value; }
        }

        public UserInfo RecentUsers
        {
            get { return _ObjUser; }
            set { _ObjUser = value; }
        }
        public GenralUserInfo()
        {

        }
        public GenralUserInfo(string _UserName,string _Password)
        {
            _ObjErrors = new Errors();
            _ObjUser = new UserInfo();
            _ObjUser.UserName = _UserName;
            _ObjUser.UserPassword = _Password;
        }
    }
}
