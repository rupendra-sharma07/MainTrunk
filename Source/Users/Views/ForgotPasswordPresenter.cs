///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       :TributesPortal.Users.Views.ForgotPasswordPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for retreiving the password in case the user forgets his/her password.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.Users;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.BusinessLogic;
//using TributesPortal.ResourceAccess;

namespace TributesPortal.Users.Views
{
    public class ForgotPasswordPresenter : Presenter<IForgotPassword>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //

        public ForgotPasswordPresenter()
        { 
        
        }

        private UsersController _controller;
        public ForgotPasswordPresenter([CreateNew] UsersController controller)
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

        public void CheckAndSendPassword(GenralUserInfo _objGenralUserInfo, bool _Reset)
        {
            _controller.CheckAndSendPassword(_objGenralUserInfo, _Reset);
            //if (_objGenralUserInfo.CustomError == null)
            //{
            //    EmailMessages objEmail = EmailMessages.Instance;


            //    bool val = objEmail.SendMessages("mkumar@in.sopragroup.com", _objGenralUserInfo.RecentUsers.UserEmail, "Forgot password", GetEmailBody(_objGenralUserInfo.RecentUsers), EmailMessages.TextFormat.Html.ToString());
            //    if (val == true)
            //    {
            //        //Transfer Page to Common Message
            //    }
            //    else
            //    {
            //        // Show an error message
            //    }
            //}
        }

        //private string GetEmailBody(UserInfo objUserInfo)
        //{
        //    string _Emailbody=string.Empty;
        //    try
        //    {
        //        _Emailbody += "User Name:" + objUserInfo.UserName + "<br/>" ;
        //        _Emailbody += "Pssword:" + objUserInfo.UserPassword + "<br/>";
        //        _Emailbody += "First Name" + objUserInfo.FirstName + "<br/>";
        //        _Emailbody += "Last Name:" + objUserInfo.LastName + "<br/>";
        //        _Emailbody += "User Type:" + objUserInfo.UserType + "<br/>";
        //        _Emailbody += " <a href='http://localhost:4762/DevelopmentWebsite/Users/UserLogin.aspx'>Click here to go to Login Page</a>";
        //    }
        //    catch (Exception e1)
        //    { 

        //    }
        //    return _Emailbody;
        //}
    }
}




