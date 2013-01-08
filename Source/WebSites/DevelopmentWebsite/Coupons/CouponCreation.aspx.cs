///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Coupons.CouponCreation.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the admin user to create discount coupons
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
using System.Text;
using TributesPortal.Utilities;

public partial class Coupons_CouponCreation : PageBase, ICouponCreation
{
    private CouponCreationPresenter _presenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            lbtnSaveChanges.Attributes.Add("onclick", "HideIndicator();");
            this._presenter.OnViewInitialized();
            FillDays(ddlDay);
            SetBlank();
        }
        this._presenter.OnViewLoaded();
    }

    [CreateNew]
    public CouponCreationPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }




    private void FillDays(DropDownList ddldays)
    {
        for (int i = 0; i <= 31; i++)
        {
            if (i == 0)
            {
                ddldays.Items.Insert(i, " ");
            }
            else
            {
                ddldays.Items.Insert(i, i.ToString());
            }
        }
    }
    protected void lbtnSaveChanges_Click(object sender, EventArgs e)
    {
        try
        {
            this._presenter.CreateCoupon();
            setmessage("<h2>Coupon(s) created</h2><P>Coupon(s) created sussfully.</P>", 2);
            SetBlank();
            Response.Redirect("~/Coupons/CouponsList.aspx");
        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem in inbox.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + "</li></ul>", 1);
        }
    }

    private void SetBlank()
    {
        txtCCYear.Text = txtCouponName.Text = txtDolar.Text = txtPercentage.Text = string.Empty;
        txtCouponnumbers.Text = "1";
        ddlCCMonth.SelectedIndex = 0;
        ddlDay.SelectedIndex = 0;
        ddlPackageType.SelectedIndex = 0;
        ddlusage.SelectedIndex = 0;
    }

    private void setmessage(string msg, int type)
    {

        if (type == 1)
            errormsg.Attributes.Add("class", "yt-Error");
        else
            errormsg.Attributes.Add("class", "yt-Notice");

        errormsg.InnerHtml = msg;
        errormsg.Visible = true;
    }



    /// <summary>
    /// Auto generated numbers.
    /// </summary>
    /// <param name="numChars"></param>
    /// <param name="seed"></param>
    /// <returns></returns>
    public string GetRandomCouponNumber(int numChars)
    {
        string[] chars = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S",
                        "T", "U", "V", "W", "X", "Y", "Z", "2", "3", "4", "5", "6", "7", "8", "9" };

        Random rnd = new Random();
        string random = string.Empty;
        for (int i = 0; i < numChars; i++)
        {
            random += chars[rnd.Next(0, 33)];
        }
        return random;
    }

    #region ICouponCreation Members

    public string CouponName
    {
        get
        {
            return txtCouponName.Text;
        }
    }

    public decimal CouponDenomination
    {
        get
        {
            if (txtDolar.Text.Length > 0)
                return Decimal.Parse(txtDolar.Text);
            else
                return Decimal.Parse(txtPercentage.Text);

        }
    }

    public bool IsPercentage
    {
        get
        {
            if (txtPercentage.Text.Length > 0)
                return true;
            else
                return false;
        }
    }

    public DateTime ApplicableFromDate
    {
        get
        {
            return DateTime.Now;
        }
    }

    public DateTime ExpiryDate
    {
        get
        {
            DateTime epirydate = new DateTime(int.Parse(txtCCYear.Text), int.Parse(ddlCCMonth.SelectedValue), int.Parse(ddlDay.SelectedValue));
            return epirydate;
        }
    }

    public int MaxNoOfUses
    {
        get
        {
            return int.Parse(ddlusage.SelectedValue);
        }
    }

    public int NoOfCoupons
    {
        get
        {
            return int.Parse(txtCouponnumbers.Text);
        }
    }

    public int CreatedBy
    {
        get
        {
            return 5;
        }
    }

    public string CouponCode
    {
        get
        {
            StringBuilder objCoupons = new StringBuilder();
            int totalcoupons = int.Parse(txtCouponnumbers.Text);
            for (int count = 0; count < totalcoupons; count++)
            {
                string coupon = GetRandomCouponNumber(10);
                if (objCoupons.Length == 0)
                {
                   
                    objCoupons.Append(coupon+count.ToString());
                }
                else
                {                    
                    objCoupons.Append(";" + coupon + count.ToString());
                }

            }
            return objCoupons.ToString();
        }
    }


    public System.Collections.Generic.IList<TributesPortal.BusinessEntities.ParameterTypesCodes> CouponUses
    {
        set
        {
            ddlusage.DataSource = value;
            ddlusage.DataTextField = "TypeDescription";
            ddlusage.DataValueField = "TypeCode";
            ddlusage.DataBind();
        }
    }

    public System.Collections.Generic.IList<TributesPortal.BusinessEntities.ParameterTypesCodes> CouponPackages
    {
        set
        {
            ddlPackageType.DataSource = value;
            ddlPackageType.DataTextField = "TypeDescription";
            ddlPackageType.DataValueField = "TypeCode";
            ddlPackageType.DataBind();
        }
    }

    #endregion

    #region ICouponCreation Members


    public int CouponPackage
    {
        get {

            return int.Parse(ddlPackageType.SelectedValue);
        }
    }

    #endregion
}


