
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.TributePortalAdmin.Views
{
    public interface IAddRemoveCredit
    {      
       
        AddRemoveCreditInfo AddRemoveEntries { set; }       
        double updatedCreditCount { set; }
    }
}