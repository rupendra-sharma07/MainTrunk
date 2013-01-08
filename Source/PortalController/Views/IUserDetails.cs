///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.PortalController.Views.IUserDetails.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for viewing the user details. 
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.PortalController.Views
{
    public interface IUserDetails
    {
        IList<CustomerCountry> Country { set; }
        IList<CustomerState> State { set; }
        void checkRole();
    }
}




