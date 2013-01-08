///Copyright       : Copyright (c) Optimus Information India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.TributePortalAdmin.Views.AddRemoveCreditPresenter.cs
///Author          : Mohit Gupta
///Creation Date   : 27 Jan 2011


using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;

namespace TributesPortal.TributePortalAdmin.Views
{
    public class AddRemoveCreditPresenter : Presenter<IAddRemoveCredit>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller

        private TributePortalAdminController _controller;
        public AddRemoveCreditPresenter([CreateNew] TributePortalAdminController controller)
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

      
      

        /// <summary>
        /// Method to add or Delete credits  by Mohit Gupta
        /// </summary>
        /// <param name="objUser">Filled Users entity.</param>
        public void AddOrDebitCredits(AddRemoveCreditInfo objUser)
        {
            AddRemoveCreditInfo objAddRemoveCreditInfo = _controller.AddOrDebitCredits(objUser);
            this.View.updatedCreditCount = objAddRemoveCreditInfo.CreditCount;

        }

      
       
    }
}
