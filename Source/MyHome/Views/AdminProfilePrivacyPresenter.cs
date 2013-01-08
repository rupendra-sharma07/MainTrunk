///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.AdminProfilePrivacyPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Admin Profile Privacy Settings.
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
//using TributesPortal.Users;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
//using TributesPortal.ResourceAccess;
//using System.Data;
using TributesPortal.MultipleLangSupport;
namespace TributesPortal.MyHome.Views
{
    public class AdminProfilePrivacyPresenter : Presenter<IAdminProfilePrivacy>
    {
        private MyHomeController _controller;
        public AdminProfilePrivacyPresenter([CreateNew] MyHomeController controller)
        {
            _controller = controller;
        }
        public void GetUserPrivacy()
        {
            UserRegistration _objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = View.UserId;
            _objUserReg.Users = objUsers;
            _controller.GetUserDetails(_objUserReg);

            if (_objUserReg.Users != null)
            {
                View.IsLocationHide = _objUserReg.Users.IsLocationHide;
                View.IsUsernameVisiable = _objUserReg.Users.IsUsernameVisiable;
                View.AllowIncomingMsg = _objUserReg.Users.AllowIncomingMsg;
                View.IsVisitCountHide = _objUserReg.Users.IsVisitCountHide;
            }
        }
        public void UpdatePrivacySettings()
        {
            UserRegistration objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = View.UserId;
            objUsers.IsUsernameVisiable = View.IsUsernameVisiable;
            objUsers.IsLocationHide = View.IsLocationHide;
            objUsers.AllowIncomingMsg = View.AllowIncomingMsg;
            objUsers.IsVisitCountHide = View.IsVisitCountHide;
            objUserReg.Users = objUsers;
            _controller.UpdatePrivacySettings(objUserReg);
        }


      
    }
}
