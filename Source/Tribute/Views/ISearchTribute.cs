///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.ISearchTribute.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for search of a Tribute.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.Tribute.Views
{
    public interface ISearchTribute
    {
        IList<SearchTribute> SearchTributesList { set; }
        IList<Tributes> TributeTypeList { set; }
        int TotalRecords { set;}
    }
}




