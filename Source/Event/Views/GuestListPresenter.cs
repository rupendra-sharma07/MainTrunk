///Copyright       : Copyright (c) Optimus Information
///Project         : Timeless Tributes
///File Name       : TributesPortal.Event.Views.GuestListPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Complete Guest List for an Event.
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
    public class GuestListPresenter : Presenter<IGuestList>
    {

        #region CLASS VARIABLES

        private EventController _controller;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="controller">A EventController object to call the method of controller</param>
        public GuestListPresenter([CreateNew] EventController controller)
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
                GetRsvpStatistics();
                GetCompleteGuestList(false);
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
                View.CompleteGuestList = _controller.GetCompleteGuestList(View.EventID, null, isCreator);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This method will call the Event Controller class method to get the event detail in the database
        /// </summary>
        public void GetRsvpStatistics()
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

                //View.IsAdmin = eventDetail.IsAdmin;
                //View.TributeType = eventDetail.TributeType;
                //View.TributeName = eventDetail.TributeName;
                //View.TributeUrl = eventDetail.TributeURL;
                View.IsAdmin = objEvent.IsAdmin;
                View.TributeType = objEvent.TributeType;
                View.TributeName = objEvent.TributeName;
                View.TributeUrl = objEvent.TributeURL;

                PopulateValueInControl(eventDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// This method will call the Event controller method to set the RSVP status for a Guest of the event
        /// </summary>
        public void AddRsvp()
        {
            try
            {
                CompleteGuestList objGuestToAdd = new CompleteGuestList();

                objGuestToAdd = View.GuestToAdd;

                _controller.AddRsvp(objGuestToAdd, View.EventID, 0);
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
                    View.EventName = objEvent.EventName;
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

                    View.MealOptions = objEvent.MealOptions;
                    View.IsAskForMeal = objEvent.IsAskForMeal;
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
    }
}




