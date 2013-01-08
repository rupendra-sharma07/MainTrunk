///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Event.Views.ManageEventPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Event Add and edit Functionality.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Collections;
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
    public class ManageEventPresenter : Presenter<IManageEvent>
    {

        #region CLASS VARIABLES

        private EventController _controller;

        #endregion


        #region CONSTANT
        private const string DefaulImageUrl = "images/EventImages/Memorial/Memorial_Event8.jpg";
        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="controller">A EventController object to call the method of controller</param>
        public ManageEventPresenter([CreateNew] EventController controller)
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
                Events eventParam = new Events();
                
                eventParam.TributeType = View.TributeType;
                eventParam.EventID = View.EventID;
                eventParam.UserId = View.UserID;
                eventParam.TributeId = View.TributeID;
                
                // get the Image List, Event Type, Country List, and Event Detail
                Events objEvent = _controller.GetEventInfo(eventParam);

                // Populate the values in the controls
                PopulateValueInControl(objEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }

        /// <summary>
        /// This method will call the Event Controller class to get the State list for selected country
        /// and populate the state drop down list on UI
        /// </summary>
        public void GetStateList()
        {
            try
            {
                View.StateList = _controller.GetCountryList(Locationid(View.Country));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event Controller class method to Add the event in the database
        /// </summary>
        public string SaveEvent()
        {
            try
            {
                Events objEvent = CreateEventObject("Add");
                _controller.SaveEvent(objEvent);

                if (objEvent.CustomError != null)
                {
                    return objEvent.CustomError.ErrorMessage;
                }
                else
                {
                    View.EventID = objEvent.EventID;
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event Controller class method to Update the event in the database
        /// </summary>
        public string UpdateEvent()
        {
            try
            {
                Events objEvent = CreateEventObject("Update");
                _controller.UpdateEvent(objEvent);

                if (objEvent.CustomError != null)
                {
                    return objEvent.CustomError.ErrorMessage;
                }
                else
                {
                    View.EventID = objEvent.EventID;
                    return null;
                }
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
                View.ImageList = objEvent.ImageList;
                View.CountryList = objEvent.CountryList;
                View.StateList = objEvent.StateList;
                View.EventTypeList = objEvent.EventTypeList;

                //View.EventImage = DefaulImageUrl;
                string tributeType = View.TributeType.Replace("New Baby", "newbaby").ToLower();
                if (tributeType == "birthday")
                    View.EventImage = "images/EventImages/Birthday/birthday_event4.jpg";
                else if (tributeType == "wedding")
                    View.EventImage = "images/EventImages/Wedding/wedding_event4.jpg";
                else if (tributeType == "newbaby")
                    View.EventImage = "images/EventImages/New Baby/baby_event4.jpg";
                else if (tributeType == "memorial")
                    View.EventImage = "images/EventImages/Memorial/memorial_event9.jpg";
                else if (tributeType == "anniversary")
                    View.EventImage = "images/EventImages/Anniversary/anniversary_event7.jpg";
                else if (tributeType == "graduation")
                    View.EventImage = "images/EventImages/Graduation/grad_event3.jpg";


                View.IsAdmin = objEvent.IsAdmin;

                // If page is opened in the edit mode then popualte the event detail to edit
                if (View.EventID != 0)
                {
                    View.EventUserID = objEvent.UserId; 
                    View.Location = objEvent.Location;
                    View.Address = objEvent.Address;
                    View.City = objEvent.City;
                    View.Country = objEvent.Country;
                    View.StateList = _controller.GetCountryList(Locationid(View.Country));
                    View.State = objEvent.State;
                    View.PhoneNumber = objEvent.PhoneNumber;                    
                    View.EmailId = objEvent.EmailId;
                    View.Day = objEvent.EventDate.Day.ToString();
                    View.Month = objEvent.EventDate.Month.ToString();
                    View.Year = objEvent.EventDate.Year.ToString();
                    View.EventDesc = objEvent.EventDesc;
                    View.IsPrivate = objEvent.IsPrivate;
                    View.EventImage = objEvent.EventImage;
                    View.EventName = objEvent.EventName;
                    View.EventStartTime = objEvent.EventStartTime;
                    View.EventEndTime = objEvent.EventEndTime;
                    View.HostName = objEvent.HostName;
                    View.EventTypeId = objEvent.EventTypeName;
                    View.EventImagePrevURL = objEvent.EventImage;
                    View.AllowAdditionalPeople = objEvent.AllowAdditionalPeople;
                    View.SendEmailReminder = objEvent.SendEmailReminder;
                    View.ShowRsvpStatistics = objEvent.ShowRsvpStatistics;
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
        /// This method will Populate the Event Deatil object to update values in the database
        /// </summary>
        /// <returns></returns>
        private Events CreateEventObject(string operation)
        {
            Events objEvent = new Events();

            try
            {
                if (operation == "Update")
                {
                    objEvent.EventID = View.EventID;
                    objEvent.ModifiedBy = View.UserID;
                    objEvent.ModifiedDate = DateTime.Now;
                    objEvent.ServerURL = View.URL;
                }
                else
                {
                    objEvent.CreatedBy = View.UserID;
                    objEvent.CreatedDate = DateTime.Now;
                }

                objEvent.City = View.City;
                objEvent.Country = View.Country;
                objEvent.UserId = View.UserID;
                objEvent.EmailId = View.EmailId;

                if ((View.Day != "0") && (View.Month != "0") && (View.Year != ""))
                {
                    objEvent.EventDate = FormatDate(View.Day, View.Month, View.Year);
                }

                objEvent.EventDesc = View.EventDesc;
                objEvent.EventStartTime = View.EventStartTime;
                objEvent.EventEndTime = View.EventEndTime;
                objEvent.EventImage = View.EventImage;
                objEvent.EventName = View.EventName;
                objEvent.HostName = View.HostName;
                objEvent.IsPrivate = View.IsPrivate;
                objEvent.Location = View.Location;
                objEvent.PhoneNumber = View.PhoneNumber;
                objEvent.Address = View.Address;
                objEvent.IsPrivate = View.IsPrivate;
                objEvent.TributeId = View.TributeID;
                objEvent.EventTypeName = View.EventTypeId;
                objEvent.State = View.State == "" ? null : View.State;
                objEvent.FirstName = View.FirstName;
                objEvent.LastName = View.LastName;
                objEvent.ServerURL = View.URL;
                objEvent.InviteGuestURL = View.InviteGuestURL;
                objEvent.TributeType = View.TributeType;
                objEvent.TributeURL = View.TributeURL;
                objEvent.TributeName = View.TributeName;
                objEvent.AllowAdditionalPeople = View.AllowAdditionalPeople;
                objEvent.SendEmailReminder = View.SendEmailReminder;
                objEvent.ShowRsvpStatistics = View.ShowRsvpStatistics;
                objEvent.MealOptions = View.MealOptions;
                objEvent.IsAskForMeal = View.IsAskForMeal;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objEvent;
        }

        /// <summary>
        /// This method will format a Date object by passed date, month and year value
        /// </summary>
        /// <param name="day">A string object which contain the day</param>
        /// <param name="month">A string object which contain the month</param>
        /// <param name="year">A string object which contain the Year</param>
        /// <returns>This method will return the DateTime object</returns>
        private DateTime FormatDate(string day, string month, string year)
        {
            DateTime Date1;

            // Format the created after Date and time
            //string afterDate = month + "/" + day + "/" + year;
            string afterDate = day + "/" + month + "/" + year; //changed as the function was throwing error for above specified case.
            try
            {
                //DateTime.TryParse(afterDate.ToString(), out Date1);
                Date1 = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Date1;
        }

        /// <summary>
        /// This method will create a Location object 
        /// </summary>
        /// <param name="ID">A int variable which contain location ID</param>
        /// <returns>This method will return the Location object</returns>
        private Locations Locationid(string ID)
        {
            try
            {
                Locations objLocations = new Locations();

                if (ID != null)
                {
                    objLocations.LocationParentId = int.Parse(ID);
                }
                else
                {
                    objLocations.LocationParentId = 0;
                }

                return objLocations;
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
            UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
            objUserInfo.UserId = View.UserID;
            objUserInfo.TributeId = View.TributeID;
            objUserInfo.TypeName = PortalEnums.TributeContentEnum.ManageEvent.ToString();
            objUserInfo.IsAdmin = View.IsAdmin;

            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add(PortalEnums.AdminInfoEnum.UserAdminInfo_ManageEvent.ToString(), objUserInfo, StateManager.State.Session);
        }

        #endregion

        #endregion
    }
}




