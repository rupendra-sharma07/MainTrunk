///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       :TributesPortal.Users.Views.BillingSummaryPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for viewing the Billing Summary.
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;

namespace TributesPortal.Users.Views
{
    public class BillingSummaryPresenter : Presenter<IBillingSummary>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        private UsersController _controller;
        public BillingSummaryPresenter([CreateNew] UsersController controller)
        {
            _controller = controller;
        }

        public override void OnViewLoaded()
        {
            // TODO: Implement code that will be executed every time the view loads
        }

        public override void OnViewInitialized()
        {
         
        }
        public void Onload(int TributeId)
        {
            object[] param ={ TributeId };
            View.PaymentReceipt = _controller.PaymentReceipt(param);   
        }


        // TODO: Handle other view events and set state in the view
    }
}




