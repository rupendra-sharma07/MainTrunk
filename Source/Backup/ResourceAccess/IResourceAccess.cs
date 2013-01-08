///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.IResourceAccess.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the resource access interface to be extended by the all
///                     resource access classes
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TributesPortal.ResourceAccess
{
    public interface IResourceAccess
    {
       void InsertRecord(object[] Params);
       void GetData(object[] Params);
       void UpdateRecord(object[] Params);
       void Delete(object[] Params);
       object InsertDataAndReturnId(object[] Params);
    }
}
