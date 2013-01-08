///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Miscellaneous.Views.IChangeSiteTheme.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Change Site Theme settings.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Miscellaneous.Views
{
    public interface IChangeSiteTheme
    {
        List<Templates> ThemesList { set;}
        int ExistingTheme { set;}
        int TributeId { get;}
        int ThemeId { get;}
        int ModifiedBy { get;}
        string ThemeType { get;}
        DateTime ModifiedDate { get;}
    }
}




