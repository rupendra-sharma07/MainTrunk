//===============================================================================
// Microsoft patterns & practices
// Web Client Software Factory
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;


namespace TributesPortal.Coupons
{
    public class CouponsController
    {
        public CouponsController()
        {

        }

        #region  public methods
        public void CreateCoupon(object[] objParam)
        {
            FacadeManager.CouponManager.CreateCouponDetails(objParam);
        }

        public List<ParameterTypesCodes> GetCouponUses()
        {
            return FacadeManager.CouponManager.CouponUses();
        }

        public List<ParameterTypesCodes> GetCouponPackages()
        {
            return FacadeManager.CouponManager.CouponPackages();
        }

        public List<GetCoupondetails> GetCoupondetails(int userID)
        {
            return FacadeManager.CouponManager.GetCoupondetails(userID);            
        }

        public List<CouponsAvailable> GetCouponbyId(int Couponid)
        {
            return FacadeManager.CouponManager.GetCouponbyId(Couponid);            
        }

        public void DeleteCoupons(object[] objCoupons)
        {            
            FacadeManager.CouponManager.DeleteCoupons(objCoupons);
        }

        #endregion  public methods
    }
}
