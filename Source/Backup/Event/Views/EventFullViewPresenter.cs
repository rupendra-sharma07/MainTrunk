///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Event.Views.EventFullViewPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Event Full View.
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using System.Reflection;
using System.ComponentModel;

#endregion


namespace TributesPortal.Event.Views
{
    public class EventFullViewPresenter : Presenter<IEventFullView>
    {

        #region CLASS VARIABLES

        private EventController _controller;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="controller">A EventController object to call the method of controller</param>
        public EventFullViewPresenter([CreateNew] EventController controller)
        {
            _controller = controller;
        }

        #endregion


        #region METHODS

        #region PUBLIC METHODS

        /// <summary>
        /// This Method will called every time the view loads
        /// </summary>
        public override void OnViewLoaded()
        {
            UserIsAdmin();
        }

        /// <summary>
        /// This method will call the first time the view loads
        /// </summary>
        public override void OnViewInitialized()
        {
            try
            {
                GetFullEvent();
                GetCompleteGuestList(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GetCompleteGuestList(bool isCreator)
        {
            try
            {
                View.CompleteGuestList = _controller.GetCompleteGuestList(View.EventID, View.Hashcode, isCreator);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This method will call the Event Controller class method to get the event detail in the database
        /// </summary>
        public void GetFullEvent()
        {
            try
            {
                Events objEvent = new Events();

                objEvent.EventID = View.EventID;
                objEvent.TributeType = View.TributeType;
                objEvent.UserId = View.UserID;
                objEvent.TributeId = View.TributeID;
                
                // Get the Event Detail for the selected event
                Events eventDetail = _controller.GetFullEvent(objEvent);

                View.IsAdmin = objEvent.IsAdmin;
                
                View.TributeType = objEvent.TributeType;
                View.TributeName = objEvent.TributeName;
                View.TributeUrl = objEvent.TributeURL;
                View.IsAskForMeal = objEvent.IsAskForMeal;
                PopulateValueInControl(eventDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will call the Event controller method to save the RSVPs for a Guest of the event
        /// </summary>
        //public void SaveRsvp(int counter, IList<CompleteGuestList> lstCompleteGuestList, int countRSVP)
        //{
        //    try
        //    {
        //        _controller.SaveRsvp(View.CompleteGuestList, View.EventID,counter,lstCompleteGuestList,countRSVP);
        //        GetFullEvent();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
 
        //}

        /// <summary>
        /// This method will call the Event controller method to save the RSVPs for a Guest of the event
        /// </summary>
        public void SaveRsvp()
        {
            try
            {
                View .CompleteGuestList = _controller.SaveRsvp(View.CompleteGuestList, View.EventID);
                GetFullEvent();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// This method will populate the values in the controls
        /// </summary>
        /// <param name="objEvent">A Events object which contain all the values</param>
        private void PopulateValueInControl(Events objEvent)
        {
            try
            {
                if (objEvent.IsActive == false)
                {
                    View.EventID = 0;
                }
                else
                {

                    View.EventUserID = objEvent.UserId;
                    View.Location = objEvent.Location;
                    View.Address = objEvent.Address;
                    View.City = objEvent.City;
                    View.State = objEvent.State;
                    View.PhoneNumber = objEvent.PhoneNumber;
                    View.Country = objEvent.Country;
                    View.EmailId = objEvent.EmailId;
                    View.EventDate = objEvent.EventDate;
                    View.EventDesc = objEvent.EventDesc.Replace("\n", "<br />");
                    View.EventImage = objEvent.EventImage;
                    View.EventName = objEvent.EventName;
                    View.EventTime = objEvent.EventStartTime;
                    View.HostName = objEvent.HostName;
                    View.EventCreatedBy = objEvent.UserName;
                    View.EventTypeName = objEvent.EventTypeName;
                    View.Invited = objEvent.EventRsvp;

                   
                    //View.AttendingList = objEvent.EventAttending;
                    //View.AwaitingList = objEvent.EventAwaiting;
                    //View.NotAttendingList = objEvent.EventNotAttending;
                    //View.MaybeAttendingList = objEvent.EventMaybeAttending;

                    View.AttendingCount = objEvent.EventAttending == null ? 0 : objEvent.EventAttending.Count;
                    View.AwaitingCount = objEvent.EventAwaiting == null ? 0 : objEvent.EventAwaiting.Count;
                    View.NotAttendingCount = objEvent.EventNotAttending == null ? 0 : objEvent.EventNotAttending.Count;
                    View.MaybeAttendingCount = objEvent.EventMaybeAttending == null ? 0 : objEvent.EventMaybeAttending.Count;
                    View.IsAskForMeal = objEvent.IsAskForMeal;
                    View.MealOptions = objEvent.MealOptions;
                    View.AllowAdditionalPeople = objEvent.AllowAdditionalPeople;
                    View.ShowRsvpStatistics = objEvent.ShowRsvpStatistics;
                    View.IsPrivate = objEvent.IsPrivate;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Method to set user admin info in the session
        /// </summary>
        private void UserIsAdmin()
        {
            try
            {
                UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
                objUserInfo.UserId = View.UserID;
                objUserInfo.TributeId = View.TributeID;
                objUserInfo.TypeName = PortalEnums.TributeContentEnum.EventFullView.ToString();

                objUserInfo.IsAdmin = View.IsAdmin;

                StateManager objStateManager = StateManager.Instance;
                objStateManager.Add(PortalEnums.AdminInfoEnum.UserAdminInfo_EventFullView.ToString(), objUserInfo, StateManager.State.Session);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        public IList<CompleteGuestList> GetEmailIdsForEvent(int guestId)
        {
            IList<CompleteGuestList> lstCompleteGuestList = new List<CompleteGuestList>();
            try
            {
                //lstCompleteGuestList = _controller.GetCompleteGuestList(View.EventID, View.Hashcode, false);
                lstCompleteGuestList = _controller.GetEmailIdsForEvent(guestId);
                //View.CompleteGuestList = lstCompleteGuestList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstCompleteGuestList;
        }        
        public bool GetCustomHeaderDetail(int tributeId)
        {
            return _controller.GetCustomHeaderDetail(tributeId);
        }

        public string GetTributeEndDate(int tributeId)
        {
            return _controller.GetTributeEndDate(tributeId);
        }
    }
}




