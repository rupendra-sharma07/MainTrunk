using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;

namespace TributesPortal.Coupons.Views
{
    public class CouponsListPresenter : Presenter<ICouponsList>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        private CouponsController _controller;
        public CouponsListPresenter([CreateNew] CouponsController controller)
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

        public void GetCoupondetails(int UserId)
        {
            View.GetCoupondetails =_controller.GetCoupondetails(UserId);
        }

        public void DeleteCoupons(int Userid,int Couponid)
        {
            Couponmaster objcou = new Couponmaster();
            objcou.ModifiedBy = Userid;
            objcou.PrimaryCouponID = Couponid;
             object[] objCoupons ={ objcou };
             _controller.DeleteCoupons(objCoupons);
        }

        //public List<GetCoupondetails> GetCoupondetails(int userID)

        // TODO: Handle other view events and set state in the view
    }
}




