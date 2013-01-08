///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.IAdvanceSearch.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Advance Search Result.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

#endregion

namespace TributesPortal.Tribute.Views
{
    public interface IAdvanceSearch
    {
        #region PROPERTIES

        IList<Locations> Country { set; }
        IList<Locations> State { set; }
        IList<Tributes> TributeTypeList { set; }

        #endregion
    }
}




