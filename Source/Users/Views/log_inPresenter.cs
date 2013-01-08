///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       :TributesPortal.Users.Views.log_inPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for logging in to the site.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.Users;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
//using TributesPortal.ResourceAccess;
//using System.Data;
using TributesPortal.MultipleLangSupport;

namespace TributesPortal.Users.Views
{
    public class log_inPresenter : Presenter<Ilog_in>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //

     
        private UsersController _controller;
        public log_inPresenter([CreateNew] UsersController controller)
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

       

        private void SetSessionValue(GenralUserInfo _objGenralUserInfo)
        {
            SessionValue _objSessionValue = new SessionValue(_objGenralUserInfo.RecentUsers.UserID,
                                                                   _objGenralUserInfo.RecentUsers.UserName,
                                                                   _objGenralUserInfo.RecentUsers.FirstName,
                                                                   _objGenralUserInfo.RecentUsers.LastName,
                                                                   _objGenralUserInfo.RecentUsers.UserEmail,
                                                                   int.Parse(_objGenralUserInfo.RecentUsers.UserType),
                                                                   _objGenralUserInfo.RecentUsers.UserTypeDescription,
                                                                   _objGenralUserInfo.RecentUsers.IsUsernameVisiable,
                                                                   // Added by rupendra
                                                                   _objGenralUserInfo.RecentUsers.UserImage
                                                                 
                                                                   );
            TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;
            stateManager.Add("objSessionvalue", _objSessionValue, StateManager.State.Session);
        }

        public object DoShortFacebookSignup(UserRegistration objUserRegistration)
        {
            return _controller.DoShortFacebookSignup(objUserRegistration);
        }

        public bool OnFacebookLogin(string fbName)
        {
            GenralUserInfo _objGenralUserInfo = new GenralUserInfo();
            UserInfo objUserInfo = new UserInfo();
            objUserInfo.FacebookUid = View.FacebookUid;
            objUserInfo.ApplicationType = View.ApplicationType;
            _objGenralUserInfo.RecentUsers = objUserInfo;
            if (objUserInfo.FacebookUid == null) return false;
            if (_controller.FacebookAccountIsAvailable(_objGenralUserInfo))
            {
                SetSessionValue(_objGenralUserInfo);
                View.Message = "";
                return true;
            }
            else
            {
                View.Message = 
                "If you are already a Your Tribute member, then please log in to associate your account with your Facebook credentials.";
;
            }
            return false;
        }

        public void OnLogin()
        {
            GenralUserInfo _objGenralUserInfo = new GenralUserInfo();
            UserInfo objUserInfo = new UserInfo();
            objUserInfo.UserName = View.UserName;
            objUserInfo.UserPassword = View.Password;
            objUserInfo.FacebookUid = View.FacebookUid;
 // Added by ashu on sept 30, 2011 for YM changes (To identify the Application)
            objUserInfo.ApplicationType = View.ApplicationType;
            _objGenralUserInfo.RecentUsers = objUserInfo;
            
            _controller.ValidateLogin(_objGenralUserInfo);
            View.Email = _objGenralUserInfo.RecentUsers.UserEmail;

            if (_objGenralUserInfo.CustomError == null)
            {
                SetSessionValue(_objGenralUserInfo);  
                View.Message = "";
            }
            else
            {
                View.Message = ResourceText.GetString(_objGenralUserInfo.CustomError.ErrorMessage.ToString());
               // ShowMessage(ResourceText.GetString(objGenralUserInfo.CustomError.ErrorMessage.ToString())); //objGenralUserInfo.CustomError.ErrorMessage.ToString();
            }

        }

        public void GetEventName()
        {
            if (View.EventID != 0)
            {
                Events objEvent = _controller.GetEventName(View.EventID);
                View.EventName = objEvent.EventName;
            }
        }

        public int GetFacebookStateId(string countryName, string stateName)
        {
            object[] param = { countryName,stateName };
            return  _controller.GetstateIdByName(param);
            
        }
        public int GetFacebookCountryId(string countryName)
        {
           return  _controller.GetCountryIdByName(countryName);
        }
        /// <summary>
        /// Checking Email by Udham on 18 Nov 2011
        /// </summary>
        /// <returns></returns>
        public int EmailAvailable()
        {
            return _controller.EmailAvailable(View.FBEmail, View.ApplicationType);
        }
        
    }
}




