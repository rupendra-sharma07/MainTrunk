///Copyright       : Copyright (c) Optimus Information
///Project         : Timeless Tributes
///File Name       : TributesPortal.Event.Views.IGuestList.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Complete Guest List for an Event.
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

#endregion

namespace TributesPortal.Event.Views
{
    public interface IGuestList
    {
        #region PROPERTIES

        int UserID { get; }
        int EventUserID { set; }
        int TributeID { get; }
        int EventID { get; set; }
        string EventTypeName { set; }
        string TributeName { get; set;}
        string TributeUrl { get; set;}
        string TributeType { get; set; }
        string UserName { get;}
        string EventName { set;}
        string Invited { set; get;}

        int AttendingCount { set; }
        int MaybeAttendingCount { set; }
        int NotAttendingCount { set; }
        int AwaitingCount { set; }

        bool IsAdmin { get; set; }

        IList<CompleteGuestList> CompleteGuestList { get; set; }
        string MealOptions { set; }
        bool IsAskForMeal { set; get; }
        CompleteGuestList GuestToAdd { get; }

        #endregion
    }
}




