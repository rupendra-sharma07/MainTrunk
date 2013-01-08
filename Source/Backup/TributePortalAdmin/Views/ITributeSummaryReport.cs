///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.TributePortalAdmin.Views.ITributeSummaryReport.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Tribute Summary Report
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.TributePortalAdmin.Views
{
    public interface ITributeSummaryReport
    {
        IList<Locations> Locations { set; }
        IList<Locations> States { set; }
        List<TributeSearch> TributeList { set;}
    }
}




