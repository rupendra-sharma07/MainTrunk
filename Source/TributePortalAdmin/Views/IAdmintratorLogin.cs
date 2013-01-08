///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.TributePortalAdmin.Views.IAdmintratorLogin.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Admintrator Login
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.TributePortalAdmin.Views
{
    public interface IAdmintratorLogin
    {
       string UserName { get;}
       string Password { get;}
       string Message { set; }
          
    }
}




