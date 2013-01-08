///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       :TributesPortal.Users.Views.HomePagePresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for viewing the home page.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;

namespace TributesPortal.Users.Views
{
    public class HomePagePresenter : Presenter<IHomePage>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        // private UsersController _controller;
        // public HomePagePresenter([CreateNew] UsersController controller)
        // {
        // 		_controller = controller;
        // }

        public override void OnViewLoaded()
        {
            // TODO: Implement code that will be executed every time the view loads
        }

        public override void OnViewInitialized()
        {
            // TODO: Implement code that will be executed the first time the view loads
        }

        // TODO: Handle other view events and set state in the view
    }
}




