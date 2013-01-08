using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;

namespace TributesPortal.Coupons.Views
{
    public class CouponDetailPresenter : Presenter<ICouponDetail>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        private CouponsController _controller;
        public CouponDetailPresenter([CreateNew] CouponsController controller)
        {
            _controller = controller;
        }

        public override void OnViewLoaded()
        {
          
        }

        public override void OnViewInitialized()
        {
            // TODO: Implement code that will be executed the first time the view loads
        }

        public void OnLoaded(int couponid)
        {
            View.CouponsDetail = _controller.GetCouponbyId(couponid);
        }
        // TODO: Handle other view events and set state in the view
    }
}




