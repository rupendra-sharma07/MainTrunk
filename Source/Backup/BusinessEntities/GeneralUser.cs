///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.GeneralUser.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the entity to be used when dealing with the General User
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class GeneralUser
    {
        private Errors _ObjErrors;
        private Users _ObjUsers;
        private UserRole _ObjUserRole;

        public Errors CustomError
        {
            get { return _ObjErrors; }
            set { _ObjErrors = value; }
        }

        public Users CustomUsers
        {
            get { return _ObjUsers; }
            set { _ObjUsers = value; }
        }

        public UserRole CustomUserRole
        {
            get { return _ObjUserRole; }
            set { _ObjUserRole = value; }
        }
    }
}
