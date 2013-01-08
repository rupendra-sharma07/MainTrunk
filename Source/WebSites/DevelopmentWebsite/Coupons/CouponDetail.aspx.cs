///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Coupons.CouponDetail.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the admin user to view the discount coupons' details
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Coupons.Views;
using TributesPortal.Utilities;
public partial class Coupons_CouponDetail : System.Web.UI.Page, ICouponDetail
{
    private CouponDetailPresenter _presenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            SetData();                   
           
        }
       
    }

    [CreateNew]
    public CouponDetailPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    private void SetData()
    {
        if (Request.QueryString["Couponid"] != null)
        {
            int couponid = int.Parse(Request.QueryString["Couponid"].ToString());
            this._presenter.OnLoaded(couponid);  
        }
        if (Request.QueryString["CouponName"] != null)
        {
            lblCouponname.InnerHtml = Request.QueryString["CouponName"].ToString();
        }
        if (Request.QueryString["Count"] != null)
        {
            lblCouponNumbers.InnerHtml = Request.QueryString["Count"].ToString();
        }
        if (Request.QueryString["Denom"] != null)
        {
            lblDiscount.InnerHtml = Request.QueryString["Denom"].ToString();
        }
        if (Request.QueryString["Package"] != null)
        {
            lblPackage.InnerHtml = Request.QueryString["Package"].ToString();
        }
        if (Request.QueryString["Expiry"] != null)
        {
            lblExpiryDate.InnerHtml = Request.QueryString["Expiry"].ToString();
        }
        if (Request.QueryString["Usg"] != null)
        {
            lblUsage.InnerHtml = Request.QueryString["Usg"].ToString();
        }
    
    }


    // TODO: Forward events to the presenter and show state to the user.
    // For examples of this, see the View-Presenter (with Application Controller) QuickStart:
    //		ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.wcsf.2007jun/wcsf/html/02-480-ViewPresenter.htm


    #region ICouponDetail Members

    public System.Collections.Generic.IList<TributesPortal.BusinessEntities.CouponsAvailable> CouponsDetail
    {
        set {
            rptrCoupons.DataSource = value;
            rptrCoupons.DataBind();

        }
    }

    #endregion
}


