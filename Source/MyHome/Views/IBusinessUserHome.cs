///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.IBusinessUserHome.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Business User Home.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

#endregion

namespace TributesPortal.MyHome.Views
{
    public interface IBusinessUserHome
    {
        #region PROPERTIES

        string RecordCount { set;}
        string DrawPaging { set;}

        int PageSize { get; set;}
        int CurrentPage { get; set;}
        int TotalRecordCount { get; set;}

        int SortOrder { get; }
        int UserID { get; }
        int UserType { get; set; }
        string SearchString { get; }
        string UserName { get; }
        string TributeType { get; }
        string ImageuRL { get; set; }
        string WelcomeMessage { get; set; }
        string CompanyName { set; }
        string StreetAddress { set; }
        string Locality { set; }
        string Region { set; }
        string Country { set; }
        string PostalCode { set; }
        string Telephone { set; }
        string WebsiteAddress { set; }
        string ZipCode { set; }
        string EmailId { set;}
        bool isEdit { set; }

        IList<SearchTribute> SearchTributesList { set; }
        IList<Tributes> TributeTypeList { set; }

        //AG: Added for checking page name
        string BusinessPageName { get; }


        #endregion
    }
}




