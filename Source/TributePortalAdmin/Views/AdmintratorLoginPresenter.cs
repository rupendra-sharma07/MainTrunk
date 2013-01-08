///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.TributePortalAdmin.Views.AdmintratorLoginPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for logging in of the portal admin.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;

namespace TributesPortal.TributePortalAdmin.Views
{
    public class AdmintratorLoginPresenter : Presenter<IAdmintratorLogin>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        private TributePortalAdminController _controller;
        public AdmintratorLoginPresenter([CreateNew] TributePortalAdminController controller)
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

        public void OnAdminLogin()
        {
            GenralUserInfo _objGenralUserInfo = new GenralUserInfo();
            UserInfo objUserInfo = new UserInfo();
            objUserInfo.UserName = View.UserName;
            objUserInfo.UserPassword = View.Password;
            _objGenralUserInfo.RecentUsers = objUserInfo;
             _controller.ValidateLogin(_objGenralUserInfo);
            

            if (_objGenralUserInfo.CustomError == null)
            {
                SetSessionValue(_objGenralUserInfo);
                View.Message = "";
            }
            else
            {
                View.Message = "Please enter correct user name & password"; //changed The message text - 05-Feb-09 ANKI
                // ShowMessage(ResourceText.GetString(objGenralUserInfo.CustomError.ErrorMessage.ToString())); //objGenralUserInfo.CustomError.ErrorMessage.ToString();
            }

        }
        private void SetSessionValue(GenralUserInfo _objGenralUserInfo)
        {
            SessionValue _objSessionValue = new SessionValue(_objGenralUserInfo.RecentUsers.UserID,
                                                                   _objGenralUserInfo.RecentUsers.UserName,
                                                                   _objGenralUserInfo.RecentUsers.FirstName,
                                                                   _objGenralUserInfo.RecentUsers.LastName,"",1,"",
                                                                   _objGenralUserInfo.RecentUsers.IsUsernameVisiable
                                                                   );
            TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;
            stateManager.Add("objSessionvalueAdmin", _objSessionValue, StateManager.State.Session);
        }
    }
}




