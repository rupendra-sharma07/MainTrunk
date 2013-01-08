///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.AdminProfileEmailpasswordPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Admin Profile Email password Settings.
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
    public class AdminProfileEmailpasswordPresenter : Presenter<IAdminProfileEmailpassword>
    {

           private MyHomeController _controller;
      //  private IList<ParameterTypesCodes> entities = null;


        public AdminProfileEmailpasswordPresenter([CreateNew] MyHomeController controller)
        {
            _controller = controller;
        }

       public void OnChangeEmailPassword()
       {
           GenralUserInfo objGenralUserInfo = new GenralUserInfo();
           UserInfo objUserInfo = new UserInfo();
           objUserInfo.UserID = View.UserId;
           objUserInfo.UserEmail = View.Email.Trim();
           objUserInfo.UserPassword = View.Password;
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                objUserInfo.UserName = objSessionvalue.UserName;
                if (objSessionvalue.UserEmail.Trim() == View.Email.Trim())
                    objUserInfo.UserEmail = string.Empty;
            }
           objGenralUserInfo.RecentUsers = objUserInfo;

           if (!(string.IsNullOrEmpty(objUserInfo.UserEmail) && string.IsNullOrEmpty(objUserInfo.UserPassword)))
                _controller.OnChangeEmailPassword(objGenralUserInfo);

           if (objGenralUserInfo.CustomError == null)
           {
               View.BannerMessage = ResourceText.GetString("msgSuccess_UP");
           }
           else
           {
               View.BannerMessage = objGenralUserInfo.CustomError.ErrorMessage.ToString();
           }

       }
        // modified by udham for checking email available for both your moments and yourtribute.
        public int EmailAvailable(string Email, string ApplicationType)
        {
            return _controller.EmailAvailable(Email, ApplicationType);
        }
    }
}
