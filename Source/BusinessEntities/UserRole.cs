///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.UserRole.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about User Roless
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class UserRole
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public UserRole()
        { }


        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private string _UserId;
      


        public string roles
        {
            get { return _roles; }
            set { _roles = value; }
        }
        private string _roles;

        /// <summary>
        /// User defined Contructor
        /// <summary>
        public UserRole(string UserId,            
            string roles)
        {
            this._UserId = UserId;            
            this._roles = roles;
        }

    }
}
