///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.PortalController.Views.ICustomerList.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for viewing the customer list. 
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.PortalController.Views
{
    public interface ICustomerList
    {
        IList<Customer> Customers { set; }
        void checkRole();
    }
}




