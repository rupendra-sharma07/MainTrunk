///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       :TributesPortal.Users.Views.ChangeEmailPasswordPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for allowing the user to Change hie/her Email and Password.
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
    public class ChangeEmailPasswordPresenter : Presenter<IChangeEmailPassword>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        private UsersController _controller;
        public ChangeEmailPasswordPresenter([CreateNew] UsersController controller)
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

        public void OnChangeEmailPassword(GenralUserInfo _objGenralUserInfo)
        {
            _controller.OnChangeEmailPassword(_objGenralUserInfo);
        }

        public void GetUserData(GenralUserInfo _objGenralUserInfo)
        {
            _controller.GetUserData(_objGenralUserInfo);
        }

        // TODO: Handle other view events and set state in the view
    }
}




