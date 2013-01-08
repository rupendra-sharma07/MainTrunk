///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Users.Views.Ilog_in.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the User Log in
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.Users.Views
{
    public interface Ilog_in
    {
        string UserName { get;}
        string Password { get;}
        Nullable<Int64> FacebookUid { get; set; }
        string Message { set; }
        string Email { set; }
        int EventID { get; }
        string EventName { set; }
        // Added by ashu on sept 30, 2011 for YM changes ( To identify the Application)
        string ApplicationType { get; }
        // Added by Udham on NOv 18, 2011 for Checking Email
        string FBEmail { get; set; }
    }
}




