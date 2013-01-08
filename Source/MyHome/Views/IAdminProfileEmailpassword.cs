///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.IAdminProfileEmailpassword.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Admin Profile Email password.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.MyHome.Views
{
   public interface IAdminProfileEmailpassword
    {
       string BannerMessage { get;set;}

        // Change eMail/Password
       int UserId { get;set;}
        string Email { get;}
        string Password { get;}  
    }
}
