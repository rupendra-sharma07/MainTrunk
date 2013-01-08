///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.UserAdminOwnerInfo.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Admin User Details
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public partial class UserAdminOwnerInfo
    {
        #region FIELDS
        private int _userId;
        private int _typeId;
        private int _tributeId;
        private string _typeName;
        private string _mode;
        private bool _isAdmin;
        private bool _isOwner;
        #endregion

        #region PROPERTIES
        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
            }
        }
        public int TypeId
        {
            get { return _typeId; }
            set
            {
                _typeId = value;
            }
        }
        public int TributeId
        {
            get { return _tributeId; }
            set
            {
                _tributeId = value;
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
        public string Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
            }
        }
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
            }
        }
        public bool IsOwner
        {
            get { return _isOwner; }
            set
            {
                _isOwner = value;
            }
        }
        #endregion
    }
}
