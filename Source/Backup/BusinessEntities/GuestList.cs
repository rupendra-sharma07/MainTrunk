///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.GuestList.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about Guests
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

#endregion


/// <summary>
///Tribute Portal-Event Guest List Entity Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the GuestList Entity class for the Guest List. it contain all the variable required for the GuestList
/// </summary>
/// 

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class GuestList
    {
         #region ENUM

        public enum EventsEnum
        {
            EventID,
            TributeId,
	        UserId,
            RSVP,
	        UserName,
            IsActive,
            IsDeleted
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
	    private int _UserId;
        private string _UserName;
        private string _RSVP;
        private bool _IsActive;
        private bool _IsDeleted;
        
        #endregion


        #region PROPERTIES

        public string RSVP
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

        #endregion

    }
}
