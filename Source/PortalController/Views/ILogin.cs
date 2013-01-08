///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.PortalController.Views.ILogin.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Logging in of a user. 
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.PortalController.Views
{
    public interface ILogin
    {
        string GetUserRole(string UserID, string Password);
        bool GetValidUser(string UserID);
    }
}




