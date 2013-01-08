using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.TributePortalAdmin.Views
{
    public interface IEnableRSSFeed
    {
        string UserName { get; set; }
        int UserId { get; set; }
        bool AtomEnabled { get; set; }
        int UpdateOutput { get; set; }
        bool EnableXMLFeed { get; set; }
        //AddRemoveCreditInfo AddRemoveEntries { set; }
        //int updatedCreditCount { set; }
    }
}
