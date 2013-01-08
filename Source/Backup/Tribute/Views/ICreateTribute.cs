///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.ICreateTribute.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Tribute Creation page.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.Tribute.Views
{
    public interface ICreateTribute
    {
        IList<Tributes> Tributes { set;}
        IList<Templates> Templates { set;}
        IList<Locations> Countries { set;}
        IList<Locations> States { set;}
        string strUrl { set;}
        IList<Templates> ThemeNames { set;}

    }
}




