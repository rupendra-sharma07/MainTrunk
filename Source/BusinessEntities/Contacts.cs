///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Contacts.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the Contacts object
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class Contacts
    {

        #region ENUM

        public enum ContactsEnum
        {
            ConatctName,
            Email
        }

        #endregion


        #region FIELDS

        string _ConatctName;
        string _Email;

        #endregion


        #region PROPERTIES

        public string ConatctName
        {
            get { return _ConatctName; }
            set { _ConatctName = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        #endregion
    }
}
