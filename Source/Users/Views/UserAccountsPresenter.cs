///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       :TributesPortal.Users.Views.UserAccountsPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for viewing the user account information.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;

namespace TributesPortal.Users.Views
{
    public class UserAccountsPresenter : Presenter<IUserAccounts>
    {

        
        
         private UsersController _controller;
         public UserAccountsPresenter([CreateNew] UsersController controller)
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
        public void GetAllTributeList()
        {
            View.SearchTributesList = _controller.GetAllTributeList();
        }

        // TODO: Handle other view events and set state in the view
    }
}




