///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Gift.Views.IGift.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Gift.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

#endregion

namespace TributesPortal.Gift.Views
{
    public interface IGift
    {
        #region PROPERTIES

        int UserID { get; set; }
        int TributeID { get; set; }
        int GiftId { get; set;}

        string TributeName { get; set;}
        string TributeType { get; set;}
        string GiftMessage { get; set;}
        string GiftSentBy { get; set;}
        string ImageUrl { get; set; }

        int ImageId { get; set;}
        IList<Gifts> GiftList { set; }
        IList<GiftImage> ImageList { set;}

        bool IsAdmin { get; set;}
        string RecordCount { set;}
        string DrawPaging { set;}

        int PageSize { get; set;}
        int CurrentPage { get; set;}
        int TotalRecordCount { get; set;}

        string FirstName { get;}
        string LastName { get;}
        string UrlToEmail { get;}
        string UserName { get;}
        #endregion
    }
}




