///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.AdminConfirmationPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for confirmation of a user to be the admin of a tribute.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;

namespace TributesPortal.Tribute.Views
{
    public class AdminConfirmationPresenter : Presenter<IAdminConfirmation>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        // private TributeController _controller;
        // public AdminConfirmationPresenter([CreateNew] TributeController controller)
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




