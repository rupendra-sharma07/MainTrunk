///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.PortalController.Views.UserDetailsPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for User Details.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;

namespace TributesPortal.PortalController.Views
{
    public class UserDetailsPresenter : Presenter<IUserDetails>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
         private PortalControllerController _controller;
         public UserDetailsPresenter([CreateNew] PortalControllerController controller)
         {
         		_controller = controller;
         }

        public override void OnViewLoaded()
        {
            // TODO: Implement code that will be executed every time the view loads
        }

        public override void OnViewInitialized()
        {
            View.Country = _controller.CountryList();
            //View.State = _controller.StateList(CountryID);
            // TODO: Implement code that will be executed the first time the view loads
        }
        public  void OnCountrySelect(string CountryID)
        {
            View.State = _controller.StateList(CountryID);
            // TODO: Implement code that will be executed the first time the view loads
        }
        public void OnSaveCustomer(Customer customer)
        {
            _controller.SaveCustomer(customer);
        }

        public void OnUpdateCustomer(Customer customer)
        {
            _controller.UpdateCustomer(customer);
        }

        // TODO: Handle other view events and set state in the view
    }
}




