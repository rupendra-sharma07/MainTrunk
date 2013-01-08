///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Event.Views.IEvent.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Event Listing.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

#endregion

namespace TributesPortal.Event.Views
{
    public interface IEvent
    {
        #region PROPERTIES

        int UserID { get; }
        int TributeID { get; }
        int EventID { get; }
        string TributeName { get; }
        string TributeType { get; }
        string TributeURL { get; }
        bool IsAdmin { get; set;}
        IList<Events> EventList { set; }
        string FirstName { get;}
        string LastName { get;}
        string EventName { get;}

        //string RecordCount { set;}
        //string DrawPaging { set;}

        #endregion
    }
}




