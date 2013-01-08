///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.PortalController.Views.LoginPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for User Login.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;

namespace TributesPortal.PortalController.Views
{
    public class LoginPresenter : Presenter<ILogin>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        private PortalControllerController _controller;
        public LoginPresenter([CreateNew] PortalControllerController controller)
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

        public void OnLogin(GeneralUser _GeneralUser)
        {
            _controller.OnLogin(_GeneralUser);
        }
        // TODO: Handle other view events and set state in the view

        public void OnValidateUserRole(UserRole roles)
        {
             _controller.OnValidateUserRole(roles);
        }
    }
}




