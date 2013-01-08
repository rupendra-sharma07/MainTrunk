///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Users.Views.IUserAccounts.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the User Accounts
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.Users.Views
{
    public interface IUserAccounts
    {
        IList<SearchTribute> SearchTributesList { set; }
    }
}




