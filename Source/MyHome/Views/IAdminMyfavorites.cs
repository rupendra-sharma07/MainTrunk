///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.IAdminMyfavorites.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Admin My favorites pages.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.MyHome.Views
{
    public interface IAdminMyfavorites
    {
        int UserId { get;}
        IList<GetMyTributes> MyFavourites { set;}
        IList<GetMyTributes> MyFavourites_ { set;}
        IList<ParameterTypesCodes> TributeTypes2 { set; }
        string PostMessage { get;}
        int ToUserId { get; }
        string Subject { get;}
        bool mytribute { set;}
        int TotalCount { set;}
    }
}
