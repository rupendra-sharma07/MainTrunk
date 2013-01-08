///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.IUserEvents.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the User Events.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.MyHome.Views
{
    public interface IUserEvents
    {
        int UserId { get;}        
        IList<Events> MyEvents { set;}
        int TotalCount { set;}
        IList<GetMyTributes> TributeDetail {set;}
    }
}




