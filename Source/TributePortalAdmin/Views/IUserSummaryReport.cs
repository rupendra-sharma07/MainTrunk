///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.TributePortalAdmin.Views.IUserSummaryReport.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the User Summary Report
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.TributePortalAdmin.Views
{
    public interface IUserSummaryReport
    {
        IList<Locations> Locations { set; }
        IList<Locations> States { set; }
        List<Users> UsersList { set;}
    }
}




