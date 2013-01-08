using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using System.Data;



namespace TributesPortal.Coupons.Views
{
    public class CouponCreationPresenter : Presenter<ICouponCreation>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        private CouponsController _controller;
        public CouponCreationPresenter([CreateNew] CouponsController controller)
        {
            _controller = controller;
        }

        public override void OnViewLoaded()
        {
           
        }

        public override void OnViewInitialized()
        {
            SetCouponUses();
            SetCouponPackages();
            // TODO: Implement code that will be executed the first time the view loads
        }

        public void SetCouponUses()
        {
            View.CouponUses = this._controller.GetCouponUses();
        }

        public void SetCouponPackages()
        {
            View.CouponPackages = this._controller.GetCouponPackages();
        }

        public void CreateCoupon()
        {
            Couponmaster objmaster = new Couponmaster();
            objmaster.CouponName = View.CouponName;
            objmaster.CouponDenomination = View.CouponDenomination;
            objmaster.ApplicableFromDate = View.ApplicableFromDate;
            objmaster.IsPercentage = View.IsPercentage;
            objmaster.ExpiryDate = View.ExpiryDate;
            objmaster.MaxNoOfUses = View.MaxNoOfUses;
            objmaster.NoOfCoupons = View.NoOfCoupons;
            objmaster.CreatedBy = View.CreatedBy;
            objmaster.CouponPackage = View.CouponPackage;


            CouponsAvailable objCouponavilable = new CouponsAvailable();
            objCouponavilable.CouponCode = View.CouponCode;

            TributesPortal.BusinessEntities.Coupons objCoupons= new TributesPortal.BusinessEntities.Coupons();
            objCoupons.Couponsavailable = objCouponavilable;
            objCoupons.CouponMaster = objmaster;
            object[] param ={ objCoupons };
            this._controller.CreateCoupon(param);        
        }

       

        // TODO: Handle other view events and set state in the view
    }
}




