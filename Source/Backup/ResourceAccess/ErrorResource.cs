///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.ErrorResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Errors
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;

namespace TributesPortal.ResourceAccess
{
    public class ErrorResource : PortalResourceAccess
    {
        public  void  SaveError(string[] strParam, DbType[] enumDbType, object[] objValue)
        {

            System.Net.IPAddress[] strClientIP = Dns.GetHostAddresses(Dns.GetHostName());

            InsertRecord("WriteErrorLog", strParam, enumDbType, objValue);
        }
        
    }
}
