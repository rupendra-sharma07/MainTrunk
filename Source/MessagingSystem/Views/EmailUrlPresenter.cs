///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MessagingSystem.Views.EmailUrlPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Messaging System
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;
using TributesPortal.Utilities;
#endregion

namespace TributesPortal.MessagingSystem.Views
{
    public class EmailUrlPresenter : Presenter<IEmailUrl>
    {
        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        private MessagingSystemController _controller;
        public EmailUrlPresenter([CreateNew] MessagingSystemController controller)
        {
            _controller = controller;
        }

        public override void OnViewLoaded()
        {
            // TODO: Implement code that will be executed every time the view loads
        }

        public override void OnViewInitialized()
        {
            // TODO: Implement code that will be executed the first time the view loads
        }

        // TODO: Handle other view events and set state in the view

        /// <summary>
        /// Method to load data for logged in User name and User Email address to controls.
        /// </summary>
        public void LoadControlsData()
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionValue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionValue != null)
            {
                this.View.FromUserName = objSessionValue.UserName;
                this.View.FromEmailAddress = objSessionValue.UserEmail;
            }
        }

        /// <summary>
        /// Method to send email to the receipents.
        /// </summary>
        public void SendEmail()
        {
            StateManager stateManager = StateManager.Instance;
            //EmailLink objEmail = (EmailLink)stateManager.Get("objEmailLink_GuestBook", StateManager.State.Session);
            EmailLink objEmail = (EmailLink)stateManager.Get("objEmailLink", StateManager.State.Session);

            EmailLink objEmailLink = new EmailLink();
            objEmailLink.FromEmailAddress = this.View.FromEmailAddress;
            objEmailLink.FromUserName = this.View.FromUserName;
            objEmailLink.EmailTo = this.View.Receipients;
            objEmailLink.UrlToEmail = objEmail.UrlToEmail;
            objEmailLink.TypeName = objEmail.TypeName;
            this.View.TypeName = objEmail.TypeName;
            _controller.SendEmail(objEmailLink);
        }
    }
}




