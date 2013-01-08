///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.TributeActivityManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the functions associated with tribute activity
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;
#endregion

namespace TributesPortal.BusinessLogic
{
    public partial class TributeActivityManager
    {
        /// <summary>
        /// Method to get the tribute activity list.
        /// </summary>
        /// <returns>Filled TributeActivityReport entity.</returns>
        public List<TributeActivityReport> GetTributeActivityList(string applicationType)
        {
            TributeActivityResource objTributeActivityResource = new TributeActivityResource();
            return objTributeActivityResource.GetTributeActivityReport(applicationType);
        }
    }
}
