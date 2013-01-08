///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.IAdminMytributesHome.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Admin My tributes Home pages.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.MyHome.Views
{
    public interface IAdminMytributesHome
    {
        int UserId { get;}
        IList<GetMyTributes> Mytributes { set;}
        IList<GetMyTributes> Mytributes_ { set;}
        IList<ParameterTypesCodes> TributeTypes { set; }
        bool myfavourite { set;}
        int TotalCount { set;}
    }
}
