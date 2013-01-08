///Copyright       : Copyright (c) Optimus Information Inc. 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.EventInvitationCategory.cs
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

// This is the EventInvitationCategory Entity class for the Event. it contain all the Invitation Categories.
/// </summary>
/// 

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class EventInvitationCategory
    {
        #region ENUM

        public enum EventInvitationCategoryEnum
        {
            InvitationCategoryID,
            InvitationCategoryName,
            TributeType
        }
        #endregion

        #region FIELDS

        private int _InvitationCategoryID;
        private string _InvitationCategoryName;
        private int _TributeType;
                
        #endregion

        #region PROPERTIES
        
        public int InvitationCategoryID
        {
            get { return _InvitationCategoryID; }
            set { _InvitationCategoryID = value; }
        }
        public string InvitationCategoryName
        {
            get { return _InvitationCategoryName; }
            set { _InvitationCategoryName = value; }
        }
        public int TributeType
        {
            get { return _TributeType; }
            set { _TributeType = value; }
        }        
        #endregion

        #region CONSTRUCTOR

        /// <summary>
	    /// Default Contructor
	    /// <summary>
	    public EventInvitationCategory()
	    {}

        /// <summary>
	    /// User defined Contructor
	    /// <summary>
        public EventInvitationCategory(int InvitationCategoryID,
            string InvitationCategoryName,
            int TributeType)
	    {
            this._InvitationCategoryID = InvitationCategoryID;
            this._InvitationCategoryName = InvitationCategoryName;
            this._TributeType = TributeType;
        
        }//end user defined constructor

        #endregion

    }//end class
}//end namespace
