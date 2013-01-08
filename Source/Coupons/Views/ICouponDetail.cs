using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.Coupons.Views
{
    public interface ICouponDetail
    {
        IList<CouponsAvailable> CouponsDetail { set;}
    }
}




