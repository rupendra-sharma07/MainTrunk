using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.TributePortalAdmin.Views
{
    public interface IUpdateTribute
    {
        //TributeId,
        //    TributeName,
        //    TributeType,
        //    TributeUrl,
        //    CreatedBy,
        //    PackageId,
        //    EndDate

        int TributeId { get; set; }
        //string TributeName { get; set; }
        //int TributeType { get; set; }
        //string TributeUrl { get; set; }
        //int CreatedBy { get; set; }
        //Nullable<DateTime> EndDate { get; set; }

        AdminTributeUpdate objAdminTributeUpdate { get; set; }
        IList<AdminTributeUpdate> objAdminTributeUpdateList { get; set; }
        string ExpiryDate { get; set; }
        UpdateTribute objUpdateTribute { get; set; }
        string ChangeType { get; set; }
        DateTime ModifiedDate { get; set; }
        string OldValue { get; set; }
        string NewValue { get; set; }
        bool UpdateStatus { get; set; }

    }
}
