///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.TributeAdminPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for displaying the admin invite/confirmation for a tribute.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;

namespace TributesPortal.Tribute.Views
{
    public class TributeAdminPresenter : Presenter<ITributeAdmin>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
         private TributeController _controller;
         public TributeAdminPresenter([CreateNew] TributeController controller)
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

        public void OnLogin(GenralUserInfo _objGenralUserInfo)
        {
            _controller.ValidateLogin(_objGenralUserInfo);

        }

        // TODO: Handle other view events and set state in the view
    }
}




