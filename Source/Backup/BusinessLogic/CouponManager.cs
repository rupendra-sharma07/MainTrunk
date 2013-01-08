///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.CouponManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the methods associated with coupons
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.ResourceAccess;
//using System.Data;
using System.Configuration;
using System.Transactions;

namespace TributesPortal.BusinessLogic
{
   public class CouponManager
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="objParam"></param>
       public void CreateCouponDetails(object[] objParam)
       {
           CouponResource objResource = new CouponResource();
           using (TransactionScope trans = new TransactionScope())
           {
               objResource.CreateCouponDetails(objParam);
               //Transaction Commited
               trans.Complete();
           }
       }

       public List<ParameterTypesCodes> CouponPackages()
       { 
           ParameterResource objresource=new ParameterResource();
           return objresource.CouponPackages();
       }

       public List<ParameterTypesCodes> CouponUses()
       {
           ParameterResource objresource = new ParameterResource();
           return objresource.CouponUses();
       }

       public List<GetCoupondetails> GetCoupondetails(int userID)
       {
           CouponResource objresource = new CouponResource();
           return objresource.GetCoupondetails(userID);
       }

       public List<CouponsAvailable> GetCouponbyId(int Couponid)
       {
           CouponResource objresource = new CouponResource();
           return objresource.GetCouponbyId(Couponid);
       }
       public void DeleteCoupons(object[] objCoupons)
       {
           CouponResource objresource = new CouponResource();
           using (TransactionScope trans = new TransactionScope())
           {
               objresource.DeleteCoupons(objCoupons);
               //Transaction Commited
               trans.Complete();
           }
       }
        
       //ParameterResource.cs
    }
}
