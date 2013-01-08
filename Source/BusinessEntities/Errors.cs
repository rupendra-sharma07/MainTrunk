///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Errors.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the entity to work with the Errors object
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class Errors
    {
        string _ErrMessage;
        public string ErrorMessage
        {
            get { return _ErrMessage; }
            set { _ErrMessage = value; }
        }
    }
}
