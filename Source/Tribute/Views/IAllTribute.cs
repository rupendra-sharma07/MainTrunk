///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.IAllTribute.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the All Tribute.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

#endregion

namespace TributesPortal.Tribute.Views
{
    public interface IAllTribute
    {
        #region PROPERTIES

        IList<SearchTribute> SearchTributesList { set; }

        #endregion
    }
}




