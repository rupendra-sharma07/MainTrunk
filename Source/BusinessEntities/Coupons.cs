///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Coupons.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the Coupons object
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class Coupons
    {
        private Errors _ObjErrors;
        private Couponmaster _ObjCouponmaster;
        private CouponsAvailable _ObjCouponsAvailable;        

        public Errors CustomError
        {
            get { return _ObjErrors; }
            set { _ObjErrors = value; }
        }

        public Couponmaster CouponMaster
        {
            get { return _ObjCouponmaster; }
            set { _ObjCouponmaster = value; }
        }

        public CouponsAvailable Couponsavailable
        {
            get { return _ObjCouponsAvailable; }
            set { _ObjCouponsAvailable = value; }
        }
      
    }
}
