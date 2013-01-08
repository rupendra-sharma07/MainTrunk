///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Event.Views.IManageEvent.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Event manager.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

#endregion

namespace TributesPortal.Event.Views
{
    public interface IManageEvent
    {
        #region PROPERTIES

        int UserID { get; }
        int EventUserID { set; }
        int TributeID { get; }
        int EventID { get; set; }
        string TributeName { get; }
        string TributeType { get; }
        string TributeURL { get; }

        string UserName { set;}
        string EventImage { get; set;}
        string Location { get; set;}
        string Address { get; set;}
        string City { get; set;}
        string State { get; set;}
        string EventName { get; set;}
        //string EventCreatedBy { get; set;}
        string EventDesc { get; set;}
        string EventTypeId { get; set;}
        string Day { get; set;}
        string Month { get; set;}
        string Year { get; set;}
        string EventStartTime { set; get; }
        string EventEndTime { set; get; }
        string Country { get; set;}
        string HostName { get; set;}
        string PhoneNumber { get; set;}
        string EmailId { get; set;}
        bool IsPrivate { get; set;}
        string URL { get;}
        string InviteGuestURL { get;}
        string FirstName { get;}
        string LastName { get;}
        string EventImagePrevURL { set; }

        bool IsAdmin { get; set;}

        bool AllowAdditionalPeople { get; set; }
        bool SendEmailReminder { get; set; }
        bool ShowRsvpStatistics { get; set; }
        string MealOptions { get; set; }
        bool IsAskForMeal { get; set; }
        IList<GiftImage> ImageList { set;}
        IList<string> EventTypeList { set;}
        IList<Locations> CountryList { set; }
        IList<Locations> StateList { set; }

        #endregion
    }
}




