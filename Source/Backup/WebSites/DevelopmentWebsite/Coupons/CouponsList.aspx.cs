///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Coupons.CouponsList.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the admin user to view the list of discount coupons
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
public partial class Coupons_CouponsList : System.Web.UI.Page, ICouponsList
{
    private CouponsListPresenter _presenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this._presenter.OnViewInitialized();
            this._presenter.GetCoupondetails(5);//Change with UserId
        }
        this._presenter.OnViewLoaded();
    }

    [CreateNew]
    public CouponsListPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }




    #region ICouponsList Members

    public System.Collections.Generic.IList<TributesPortal.BusinessEntities.GetCoupondetails> GetCoupondetails
    {
        set {

            gvMyCoupons.DataSource = value;
            gvMyCoupons.DataBind();
        
        }
    }

    #endregion
    protected void gvMyCoupons_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton txtCoupoName = (LinkButton)e.Row.FindControl("txtCoupoName");
            txtCoupoName.CommandArgument = e.Row.RowIndex.ToString();
        }
    }
    protected void gvMyCoupons_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Remove"))
        {
             int index = int.Parse(e.CommandArgument.ToString());
             Label txtPrimaryCouponID = (Label)gvMyCoupons.Rows[index].FindControl("txtPrimaryCouponID");

             this._presenter.DeleteCoupons(5, int.Parse(txtPrimaryCouponID.Text));//Change with UserId
             this._presenter.GetCoupondetails(5);//Change with UserId
            
        }
        else if (e.CommandName.Equals("SelectCoupon"))
        {
            int index = int.Parse(e.CommandArgument.ToString());
            //Couponname
            LinkButton txtCoupoName = (LinkButton)gvMyCoupons.Rows[index].FindControl("txtCoupoName");
            //Couponid
            Label txtPrimaryCouponID = (Label)gvMyCoupons.Rows[index].FindControl("txtPrimaryCouponID");
            //Coupons Count
            Label lblCouponsCount = (Label)gvMyCoupons.Rows[index].FindControl("lblCouponsCount");
            //Coupon Denomination
            Label txtCouponDenomination = (Label)gvMyCoupons.Rows[index].FindControl("txtCouponDenomination");
            //Coupon Denomination
            Label txtPackage = (Label)gvMyCoupons.Rows[index].FindControl("txtPackage");
            //Expires
            Label txtExpires = (Label)gvMyCoupons.Rows[index].FindControl("txtExpires");
            //Usage
            Label txtUsage = (Label)gvMyCoupons.Rows[index].FindControl("txtUsage");
            
            string Querystring="Couponid="+txtPrimaryCouponID.Text+"&CouponName="+txtCoupoName.Text+"&Count="+lblCouponsCount.Text+"&Denom="+txtCouponDenomination.Text+"&Package="+txtPackage.Text+"&Expiry="+txtExpires.Text+"&Usg="+txtUsage.Text;

            Response.Redirect("~/Coupons/CouponDetail.aspx?" + Querystring);
        }
    }
    protected void gvMyCoupons_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton LinkButtonDelete = e.Row.FindControl("LinkButtonDelete") as LinkButton;
            LinkButtonDelete.Attributes.Add("onClick", "javascript:return dispconfirm();");
        }
    }
}


