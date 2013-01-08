///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       :TributesPortal.Users.Views.UserLoginPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for allowing the User to Login.
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

namespace TributesPortal.Users.Views
{
    public class UserLoginPresenter : Presenter<IUserLogin>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        private UsersController _controller;
        public UserLoginPresenter([CreateNew] UsersController controller)
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


        public void OnLogin(GenralUserInfo _objGenralUserInfo)
        {
            _controller.ValidateLogin(_objGenralUserInfo);
            
        }
        public void CheckNResetPassword(GenralUserInfo _objGenralUserInfo)
        {

        }

        public void GetTributeOnId(TributesUserInfo _objTributeUserinfo)
        {
            _controller.GetTributeOnId(_objTributeUserinfo);
        }
        /*public string GetEventName(int _EventId)
        {
            return _controller.GetEventName(_EventId);
        }*/
    }
}




