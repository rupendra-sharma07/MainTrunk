///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Event.Views.IEventFullView.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Event Full View.
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

#endregion

namespace TributesPortal.Event.Views
{
    public interface IEventFullView
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
        string EventCreatedBy { set;}
        string UserName { get;}
        string EventImage { set;}
        string Location { set;}
        string Address { set;}
        string City {set;}
        string State {set;}
        string EventName { set;}
        string EventDesc {set;}
        System.DateTime EventDate { set; }
        string EventTime { set; }
        string Country { set;}
        string HostName { set;}
        string PhoneNumber { set;}
        string EmailId { set;}
        string Invited { set; get;}

        //IList<GuestList> AttendingList { set;}
        //IList<GuestList> MaybeAttendingList { set;}
        //IList<GuestList> NotAttendingList { set; }
        //IList<GuestList> AwaitingList { set; }

        int AttendingCount { set; }
        int MaybeAttendingCount { set; }
        int NotAttendingCount { set; }
        int AwaitingCount { set; }

        bool IsAdmin { get; set; }

        IList<CompleteGuestList> CompleteGuestList { get; set; }
        string Hashcode { set; get; }
        string MealOptions { set; }
        bool IsAskForMeal { get; set; }
        bool AllowAdditionalPeople { set; }
        bool ShowRsvpStatistics { set; }
        bool IsPrivate { get; set; }

        #endregion
    }
}




