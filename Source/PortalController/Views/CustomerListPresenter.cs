///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.PortalController.Views.CustomerListPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for viewing a Customer List.
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;

namespace TributesPortal.PortalController.Views
{
    public class CustomerListPresenter : Presenter<ICustomerList>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
         private PortalControllerController _controller;
         public CustomerListPresenter([CreateNew] PortalControllerController controller)
         {
         		_controller = controller;
         }

        public override void OnViewLoaded()
        {
           
            // TODO: Implement code that will be executed every time the view loads
        }

        public override void OnViewInitialized()
        {
            View.Customers = _controller.CustomerList();
            // TODO: Implement code that will be executed the first time the view loads
        }


        // TODO: Handle other view events and set state in the view
    }
}




