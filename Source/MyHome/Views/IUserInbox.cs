///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.IUserInbox.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the User Inbox pages.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.MyHome.Views
{
    public interface IUserInbox
    {
        string PostMessage { get;}
        int ToUserId { get; }
        string Subject1 { get;}
        int UserId { get;}
        int SendbyUserId { get;}
        string Subject { get;}
        string EmailBody { get;}        
        IList<MailMessage> UserInbox { set;}
        IList<MailMessage> UserSentItem { set;}
        bool mytribute { set;}
        bool myfavourite { set;}
        int TotalCount { set;}
        int TotalCount_ { set;}
        string UserAddress { set; }
        
    }
}
