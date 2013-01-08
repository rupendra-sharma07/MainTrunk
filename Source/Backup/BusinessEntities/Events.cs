///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Events.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the entity to work with the events object
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

#endregion


/// <summary>
///Tribute Portal-Events Entity Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the Events Entity class for the Event. it contain all the variable required for the Events
/// </summary>
/// 

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class Events
    {

        #region ENUM

        public enum EventsEnum
        {
            EventID,
            TributeId,
	        UserId,
            EventImage,
            EventRsvp,
	        Location,
            Address,
            City,
            State,
            EventName,
	        EventDesc,
            EventTypeId,
            EventDate,
            EventStartTime,
            EventEndTime,
            TimeZoneID,
            Country,
	        HostName,
            PhoneNumber,
            EventTypeName,
            EmailId,
            CreatedBy,
            IsPrivate,
            CreatedDate,
            ModifiedBy,
            ModifiedDate,
            IsActive,
            IsDeleted,
            AllowAdditionalPeople,
            SendEmailReminder,
            ShowRsvpStatistics,
            MealOptions,
            EventMessage,
            EventThemeID,
            IsAskForMeal,
            FirstName,
            LastName
        }

        

        public enum EventMaintainState
        {
            Event_Admin,
            Event_State
        }

        #endregion


        #region FIELDS

        private int _EventID;
        private int _TributeId;
        private string _TributeType;
        private string _TributeName;
        private string _TributeURL;
	    private int _UserId;
        private string _UserName;
        private string _EventImage;
	    private string _Location;
        private string _Address;
        private string _City;
        private string _State;
        private string _Country;
        private string _EventPlace;
        private string _EventName;
	    private string _EventDesc;
        private int _EventTypeId;
        private string _EventTypeName;
        private System.DateTime _EventDate;
        private string _EventDateAndTime;
        private string _EventStartTime;
        private string _EventEndTime;
        private int _TimeZoneID;
	    private string _HostName;
        private string _PhoneNumber;
        private string _EmailId;
        private int _CreatedBy;
        private bool _IsPrivate;
        private System.DateTime _CreatedDate;
        private int _ModifiedBy;
        private System.DateTime _ModifiedDate;
        private bool _IsAdmin;
        private bool _IsActive;
        private bool _IsDeleted;
        private string _RSVP;
        private string _ServerURL;
        private string _InviteGuestURL;
        private string _FirstName;
        private string _LastName;
        private bool _AllowAdditionalPeople;
        private bool _SendEmailReminder;
        private bool _ShowRsvpStatistics;
        private string _MealOptions;
        private string _EventMessage;
        private int _EventThemeID;
        private bool _IsAskForMeal;
      

        IList<GuestList> _EventAwaiting;
        IList<GuestList> _EventAttending;
        IList<GuestList> _EventMaybeAttending;
        IList<GuestList> _EventNotAttending;

        IList<GiftImage> _ImageList;
        IList<string> _EventTypeList;
        IList<Locations> _CountryList;
        IList<Locations> _StateList;

        private Errors _CustomError;
        
        #endregion


        #region PROPERTIES

        public string EventRsvp
        {
            get { return _RSVP; }
            set { _RSVP = value; }
        }

        public int EventID
	    { 
		    get { return _EventID; }
		    set { _EventID = value; }
	    }

	    public int TributeId
	    { 
		    get { return _TributeId; }
		    set { _TributeId = value; }
	    }

	    public int UserId
	    { 
		    get { return _UserId; }
		    set { _UserId = value; }
	    }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public string TributeType
        {
            get { return _TributeType; }
            set { _TributeType = value; }
        }

        public string TributeName
        {
            get { return _TributeName; }
            set { _TributeName = value; }
        }

        public string TributeURL
        {
            get { return _TributeURL; }
            set { _TributeURL = value; }
        }

	    public string EventName
	    { 
		    get { return _EventName; }
		    set { _EventName = value; }
	    }

     

        public string EventTypeName
        {
            get { return _EventTypeName; }
            set { _EventTypeName = value; }
        }

	    public string EventDesc
	    { 
		    get { return _EventDesc; }
		    set { _EventDesc = value; }
	    }

        public string InviteGuestURL
	    {
            get { return _InviteGuestURL; }
            set { _InviteGuestURL = value; }
	    }

	    public int EventTypeId
	    { 
		    get { return _EventTypeId; }
		    set { _EventTypeId = value; }
	    }

	    public System.DateTime EventDate
	    { 
		    get { return _EventDate; }
		    set { _EventDate = value; }
	    }

        public string EventDateAndTime
        {
            get { return _EventDateAndTime; }
            set { _EventDateAndTime = value; }
        }

	    public string EventStartTime
	    { 
		    get { return _EventStartTime; }
		    set { _EventStartTime = value; }
	    }
	    
	    public string EventEndTime
	    { 
		    get { return _EventEndTime; }
		    set { _EventEndTime = value; }
	    }
	    
	    public int TimeZoneID
	    { 
		    get { return _TimeZoneID; }
		    set { _TimeZoneID = value; }
	    }
	    
	    public string EventImage
	    { 
		    get { return _EventImage; }
		    set { _EventImage = value; }
	    }
	    
	    public string Location
	    { 
		    get { return _Location; }
		    set { _Location = value; }
	    }

        public string Address
	    {
            get { return _Address; }
            set { _Address = value; }
	    }
	    
	    public string City
	    { 
		    get { return _City; }
		    set { _City = value; }
	    }

        public string State
	    { 
		    get { return _State; }
		    set { _State = value; }
	    }

        public string Country
	    { 
		    get { return _Country; }
		    set { _Country = value; }
	    }

        public string EventPlace
	    {
            get { return _EventPlace; }
            set { _EventPlace = value; }
	    }
        
	    public string HostName
	    { 
		    get { return _HostName; }
		    set { _HostName = value; }
	    }
        
	    public string PhoneNumber
	    { 
		    get { return _PhoneNumber; }
		    set { _PhoneNumber = value; }
	    }
	    
	    public string EmailId
	    { 
		    get { return _EmailId; }
		    set { _EmailId = value; }
	    }

        public string ServerURL
        {
            get { return _ServerURL; }
            set { _ServerURL = value; }
        }

	    public bool IsPrivate
	    { 
		    get { return _IsPrivate; }
		    set { _IsPrivate = value; }
	    }
	    
	    public int CreatedBy
	    { 
		    get { return _CreatedBy; }
		    set { _CreatedBy = value; }
	    }
	    
	    public System.DateTime CreatedDate
	    { 
		    get { return _CreatedDate; }
		    set { _CreatedDate = value; }
	    }
	    
	    public int ModifiedBy
	    { 
		    get { return _ModifiedBy; }
		    set { _ModifiedBy = value; }
	    }
	    
	    public System.DateTime ModifiedDate
	    { 
		    get { return _ModifiedDate; }
		    set { _ModifiedDate = value; }
	    }

        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set { _IsAdmin = value; }
        }

	    public bool IsActive
	    { 
		    get { return _IsActive; }
		    set { _IsActive = value; }
	    }

	    public bool IsDeleted
	    { 
		    get { return _IsDeleted; }
		    set { _IsDeleted = value; }
	    }
        public bool AllowAdditionalPeople
        {
            get { return _AllowAdditionalPeople; }
            set { _AllowAdditionalPeople = value; }
        }

        public bool SendEmailReminder
        {
            get { return _SendEmailReminder; }
            set { _SendEmailReminder = value; }
        }
        public bool ShowRsvpStatistics
        {
            get { return _ShowRsvpStatistics; }
            set { _ShowRsvpStatistics = value; }
        }
        public string MealOptions
        {
            get { return _MealOptions; }
            set { _MealOptions = value; }
        }
        public string EventMessage
        {
            get { return _EventMessage; }
            set { _EventMessage = value; }
        }

        public int EventThemeID
        {
            get { return _EventThemeID; }
            set { _EventThemeID = value; }
        }

        public IList<GiftImage> ImageList
        {
            get { return _ImageList; }
            set { _ImageList = value; }
        }

        public IList<string> EventTypeList
        {
            get { return _EventTypeList; }
            set { _EventTypeList = value; }
        }

        public IList<Locations> CountryList
        {
            get { return _CountryList; }
            set { _CountryList = value; }
        }

        public IList<Locations> StateList
        {
            get { return _StateList; }
            set { _StateList = value; }
        }

        public IList<GuestList> EventAttending
        {
            get { return _EventAttending; }
            set { _EventAttending = value; }
        }

        public IList<GuestList> EventAwaiting
        {
            get { return _EventAwaiting; }
            set { _EventAwaiting = value; }
        }

        public IList<GuestList> EventMaybeAttending
        {
            get { return _EventMaybeAttending; }
            set { _EventMaybeAttending = value; }
        }

        public IList<GuestList> EventNotAttending
        {
            get { return _EventNotAttending; }
            set { _EventNotAttending = value; }
        }

        public Errors CustomError
        {
            get { return _CustomError; }
            set { _CustomError = value; }
        }

        public bool IsAskForMeal
        {
            get { return _IsAskForMeal; }
            set { _IsAskForMeal = value; }
        }


        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        #endregion


        #region CONSTRUCTOR

        /// <summary>
	    /// Default Contructor
	    /// <summary>
	    public Events()
	    {}

        /// <summary>
	    /// User defined Contructor
	    /// <summary>
        public Events(int EventID, 
		    int TributeId, 
		    int UserId, 
		    string EventName, 
		    string EventDesc, 
		    int EventTypeId, 
		    System.DateTime EventDate, 
		    string EventStartTime, 
		    string EventEndTime, 
		    int TimeZoneID, 
		    string EventImage, 
		    string Location,
            string Address, 
		    string City,
            string State,
            string Country, 
		    string HostName, 
		    string PhoneNumber, 
		    string EmailId, 
		    bool IsPrivate, 
		    int CreatedBy, 
		    System.DateTime CreatedDate, 
		    int ModifiedBy, 
		    System.DateTime ModifiedDate, 
		    bool IsActive,
            bool IsDeleted,
            bool AllowAdditionalPeople,
            bool SendEmailReminder,
            bool ShowRsvpStatistics,
            string MealOptions,
            string EventMessage,
            int EventThemeID,
            bool IsAskForMeal,
            string FirstName,
            string LastName
            )
	    {
		    this._EventID = EventID;
		    this._TributeId = TributeId;
		    this._UserId = UserId;
		    this._EventName = EventName;
		    this._EventDesc = EventDesc;
		    this._EventTypeId = EventTypeId;
		    this._EventDate = EventDate;
		    this._EventStartTime = EventStartTime;
		    this._EventEndTime = EventEndTime;
		    this._TimeZoneID = TimeZoneID;
		    this._EventImage = EventImage;
		    this._Location = Location;
            this._Address = Address;
		    this._City = City;
		    this._State = State;
		    this._Country = Country;
		    this._HostName = HostName;
		    this._PhoneNumber = PhoneNumber;
		    this._EmailId = EmailId;
		    this._IsPrivate = IsPrivate;
		    this._CreatedBy = CreatedBy;
		    this._CreatedDate = CreatedDate;
		    this._ModifiedBy = ModifiedBy;
		    this._ModifiedDate = ModifiedDate;
		    this._IsActive = IsActive;
            this._IsDeleted = IsDeleted;
            this._AllowAdditionalPeople = AllowAdditionalPeople;
            this._SendEmailReminder = SendEmailReminder;
            this._ShowRsvpStatistics = ShowRsvpStatistics;
            this._MealOptions = MealOptions;
            this._EventMessage = EventMessage;
            this._EventThemeID = EventThemeID;
            this._IsAskForMeal = IsAskForMeal;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }

        #endregion
    }
}

