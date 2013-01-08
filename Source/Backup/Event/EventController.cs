//===============================================================================
// Microsoft patterns & practices
// Web Client Software Factory
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;

#endregion

///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Event.EventController.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Controller class for the Event.
///Audit Trail     : Date of Modification  Modified By         Description

namespace TributesPortal.Event
{
    public class EventController
    {

        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the EventController class
        /// </summary>
        public EventController()
        {
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// This method will call the Tribute Manager class to get the Country list and State list
        /// </summary>
        /// 
        /// <param name="countries">This will pass the parent location (country) for the state and null for the country
        /// </param>
        /// <returns>This method will return the list of location(state, country)</returns>
        public IList<Locations> GetCountryList(Locations countries)
        {
            try
            {
                return FacadeManager.TributeManager.GetCountryList(countries);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event Manager class method to get the 
        /// Image List, Event Type, Country List, and Event Detail
        /// </summary>
        /// <param name="objEvent">An Event Object which contain Tribute and Event information</param>
        /// <returns>This method will return the Events object which contain the Event information</returns>
        public Events GetEventInfo(Events objEvent)
        {
            try
            {
                return FacadeManager.EventManager.GetEventInfo(objEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event Manager class method to Add the event in the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain Event information</param>
        public void SaveEvent(Events objEvent)
        {
            try
            {
                FacadeManager.EventManager.SaveEvent(objEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event Manager class method to Update the event in the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain updated Event information</param>
        public void UpdateEvent(Events objEvent)
        {
            try
            {
                FacadeManager.EventManager.UpdateEvent(objEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event Manager class method to get the event list from the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain Tribute Id for which wants to get the event list</param>
        /// <returns>This method will return the list of Events object which contain the Event list</returns>
        public IList<Events> GetEventList(Events objEvent)
        {
            try
            {
                return FacadeManager.EventManager.GetEventList(objEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event Manager class method to get the event detail in the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain Event Id for which wants to get the detail of the event</param>
        /// <returns>This method will return the Events object which contain the Event information</returns>
        public Events GetFullEvent(Events objEvent)
        {
            try
            {
                return FacadeManager.EventManager.GetFullEvent(objEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CompleteGuestList> GetCompleteGuestList(int EventId, string Hashcode, bool isCreator)
        {
            try
            {
                return FacadeManager.EventManager.GetCompleteGuestList(EventId, Hashcode, isCreator);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event Manager class method to Delete the event in the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain event id which wants to delete</param>
        public void DeleteEvent(Events objEvent)
        {
            try
            {
                FacadeManager.EventManager.DeleteEvent(objEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event Manager method to set the RSVP status for a Guest of the event
        /// </summary>
        /// <param name="objGuestToAdd">An CompleteGuestList Object which contain Guest and it's RSVP status</param>
        /// <param name="objEvent">An Events Object which contain event details</param>
        public void AddRsvp(CompleteGuestList objGuestToAdd, int EventId, int UserId)
        {
            try
            {
                FacadeManager.EventManager.AddRsvp(objGuestToAdd, EventId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This method will call the Event Manager method to save the RSVPs for a Guest of the event
        /// </summary>
        /// <param name="objGuestToAdd">An CompleteGuestList Object which contain Guest and it's RSVP status</param>
        //public void SaveRsvp(IList<CompleteGuestList> objGuestToAdd, int EventId, int counter, IList<CompleteGuestList> lstCompleteGuestList, int countRSVP)
        //{
        //    try
        //    {
        //        FacadeManager.EventManager.SaveRsvp(objGuestToAdd, EventId, counter, lstCompleteGuestList,countRSVP);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}


        /// <summary>
        /// This method will call the Event Manager method to save the RSVPs for a Guest of the event
        /// </summary>
        /// <param name="objGuestToAdd"></param>
        /// <param name="EventId"></param>
        /// <returns></returns>
        public  IList<CompleteGuestList> SaveRsvp(IList<CompleteGuestList> objGuestToAdd, int EventId)
        {
            IList<CompleteGuestList> objCompleteGuestList = new List<CompleteGuestList>();
            try
            {
                objCompleteGuestList = FacadeManager.EventManager.SaveRsvp(objGuestToAdd, EventId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objCompleteGuestList;

        }


        /// <summary>
        /// This method will call the Event Manager method to invite the guest for the event
        /// </summary>
        /// <param name="objEvent">n Event Object which contain event id and Guest List</param>
        public int InviteGuest(Events objEvent)
        {
            try
            {
                return FacadeManager.EventManager.InviteGuest(objEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call EventManger class to get the 
        /// EventInvitationcategories for the passed TributeType 
        /// </summary>
        /// <param name="tributeType">TributeType</param>
        /// <returns>List of EventInvitationCategory object</returns>
        public IList<EventInvitationCategory> GetEventInvitationCategories(string tributeType)
        {
            try
            {
                return FacadeManager.EventManager.EventInvitationCategories(tributeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call EventManger class to get the EventTheme Information 
        /// for the passed EventInvitationCategoryID and TributeType
        /// </summary>
        /// <param name="eventInvitationCategoryID">EventInvitationCategoryID</param>
        /// <param name="tributeType">TributeType</param>
        /// <returns>List of EventTheme object</returns>
        public IList<EventTheme> GetEventThemeInfo(int eventInvitationCategoryID, string tributeType)
        {
            try
            {
                return FacadeManager.EventManager.EventThemeInfo(eventInvitationCategoryID, tributeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call EventManger class to get the EventTheme Information for the passed EventThemeID
        /// </summary>
        /// <param name="eventThemeID"></param>
        /// <returns>EventTheme object</returns>
        public EventTheme GetEventThemeInfoByID(int eventThemeID)
        {
            try
            {
                return FacadeManager.EventManager.GetEventThemeByID(eventThemeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guestId"></param>
        /// <returns></returns>
        public IList<CompleteGuestList> GetEmailIdsForEvent(int guestId)
        {
            try
            {
                return FacadeManager.EventManager.GetEmailIdsForEvent(guestId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetTributeEndDate(int tributeId)
        {
            return FacadeManager.NotesManager.GetTributeEndDate(tributeId);
        }
        public int GetCurrentEvents(int tributeId)
        {
            return FacadeManager.NotesManager.GetCurrentEvents(tributeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool GetCustomHeaderDetail(int tributeId)
        {
            return FacadeManager.NotesManager.GetCustomHeaderDetail(tributeId);
        }

        #endregion

       

    }//end class
}//end namespace
