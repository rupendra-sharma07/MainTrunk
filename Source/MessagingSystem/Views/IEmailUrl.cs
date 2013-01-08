///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MessagingSystem.Views.IEmailUrl.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Email URL.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TributesPortal.MessagingSystem.Views
{
    public interface IEmailUrl
    {
        string FromUserName { set; get;}
        string FromEmailAddress { set; get;}
        string TypeName { set;}
        //string UrlToEmail { get;}
        List<string> Receipients { get;}

        
    }
}




