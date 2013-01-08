///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.AdminTributeUpdate.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about Video Tokens
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public partial class AdminTributeUpdate
    {
        public enum AdminTributeUpdateEnum
        {

            TributeUpdateId,
            TributeId,
            ChangeType,
            ModifiedDate,
            OldValue,
            NewValue
        }

        #region FIELDS
        private int _tributeUpdateId;
        private int _tributeId;
        private string _changeType;
        private DateTime _modifiedDate;
        private string _oldValue;
        private string _newValue;
        #endregion

        #region PROPERTIES
        public int TributeUpdateId
        {
            get { return _tributeUpdateId; }
            set { _tributeUpdateId = value; }
        }
        public int TributeId
        {
            get { return _tributeId; }
            set { _tributeId = value; }
        }
        public string ChangeType
        {
            get { return _changeType; }
            set { _changeType = value; }
        }
        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }
        public string OldValue
        {
            get { return _oldValue; }
            set { _oldValue = value; }
        }
        public string NewValue
        {
            get { return _newValue; }
            set { _newValue = value; }
        }

        public AdminTributeUpdate()
        { }

        public AdminTributeUpdate(int tributeUpdateId, int tributeId, string changeType, DateTime modifiedDate, string oldValue, string newValue)
        {
            this.TributeUpdateId = tributeUpdateId;
            this.TributeId = tributeId;
            this.ChangeType = changeType;
            this.ModifiedDate = modifiedDate;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
        #endregion
    }
}
