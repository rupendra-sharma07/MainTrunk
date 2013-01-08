///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Miscellaneous.Views.PricingPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Pricing.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;

namespace TributesPortal.Miscellaneous.Views
{
    public class PricingPresenter : Presenter<IPricing>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        // private MiscellaneousController _controller;
        // public PricingPresenter([CreateNew] MiscellaneousController controller)
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




