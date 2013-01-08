///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.ISearchResult.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Search Result.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

#endregion

namespace TributesPortal.Tribute.Views
{
    public interface ISearchResult
    {
        #region PROPERTIES

        string RecordCount { set;}
        string DrawPaging { set;}

        int PageSize { get; set;}
        int CurrentPage { get; set;}
        int TotalRecordCount { get; set;}

        string SearchType { get;}
        string Message { set;}
        
        IList<SearchTribute> SearchTributesList { set; }
        IList<Tributes> TributeTypeList { set; }

        SearchTribute SearchParameter { get;}

        #endregion
    }
}




