///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Event.Views.InviteGuestPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Event Invite Guest.
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
    public class InviteGuestPresenter : Presenter<IInviteGuest>
    {

        #region CLASS VARIABLES

        private EventController _controller;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="controller">A EventController object to call the method of controller</param>
        public InviteGuestPresenter([CreateNew] EventController controller)
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
            View.EventInvitationCategoryList = _controller.GetEventInvitationCategories(View.TributeType);           

            View.EventThemeList = _controller.GetEventThemeInfo(View.EventThemeID, View.TributeType);            
        }

        /// <summary>
        /// This method will call the Event controller method to set the RSVP status of the event
        /// </summary>
        public string InviteGuest(string Eventname)
        {
            Events objEvent = null;

            StateManager objStateManager = StateManager.Instance;
            objEvent = (Events) objStateManager.Get("EventInfo", StateManager.State.Session);

            if (objEvent == null)
            {
                objEvent = new Events();
            }

            objEvent.EventAwaiting = View.GuestList;
            objEvent.EventID = View.EventID;
            objEvent.UserId = View.UserID;
            objEvent.TributeId = View.TributeID;
            objEvent.ServerURL = View.URL;


            objEvent.FirstName = View.FirstName;
            objEvent.LastName = View.LastName;
            objEvent.TributeName = View.TributeName;
            objEvent.TributeType = View.TributeType;
            objEvent.TributeURL = View.TributeURL;
            objEvent.EventName = Eventname;
            objEvent.EventMessage = View.EventMessage;
            objEvent.EventThemeID = View.EventThemeID;

            

           // save the list of email in the database and also send the email to all
            if (objEvent.EventAwaiting.Count > 0)
            {
                //Added by Amit: update the event
                //_controller.UpdateEvent(objEvent);

                View.GuestCount = _controller.InviteGuest(objEvent);

                return null;
            }
            else
            {
                string str = "Please select a contact to invite.";
                return str;
            }
        }

        /// <summary>
        /// This methods calls the control menthod to load the EventTheme
        /// </summary>
        /// <param name="invitationCategoryID">Event Invitation Category ID</param>
        /// <param name="tributeType">TributeType i.e. Wedding, Memorial etc.</param>
        public void LoadEventThemes(int invitationCategoryID, string tributeType)
        {
            View.EventThemeList = _controller.GetEventThemeInfo(invitationCategoryID, tributeType);
        }

        /// <summary>
        /// This method calls the control menthod to load the EventTheme
        /// </summary>
        /// <param name="themeID">EventThemeID</param>
        public void LoadTheme(int themeID)
        {
            EventTheme objEventTheme = _controller.GetEventThemeInfoByID(themeID);

            View.EventThemePreview = objEventTheme.ThemePreviewImage;
        }

        #endregion


        #region PRIVATE METHODS
        /// <summary>
        /// Method to set user admin info in the session
        /// </summary>
        private void UserIsAdmin()
        {
            UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
            objUserInfo.UserId = View.UserID;
            objUserInfo.TributeId = View.TributeID;
            objUserInfo.TypeName = PortalEnums.TributeContentEnum.InviteGuest.ToString();
            objUserInfo.IsAdmin = View.IsAdmin;

            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add(PortalEnums.AdminInfoEnum.UserAdminInfo_InviteGuest.ToString(), objUserInfo, StateManager.State.Session);
        }

        #endregion

        #endregion
        
    }//end class
}//end namespace




