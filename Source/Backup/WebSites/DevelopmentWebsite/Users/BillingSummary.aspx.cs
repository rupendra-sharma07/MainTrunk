///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Users.BillingSummary.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to view his/her billing summary
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
using TributesPortal.Users.Views;

public partial class Users_BillingSummary : System.Web.UI.Page, IBillingSummary
{
    private BillingSummaryPresenter _presenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //this._presenter.OnViewInitialized(int);
            this._presenter.Onload(int.Parse(Session["Tributeid"].ToString()));            
        }
        this._presenter.OnViewLoaded();
    }

    [CreateNew]
    public BillingSummaryPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    // TODO: Forward events to the presenter and show state to the user.
    // For examples of this, see the View-Presenter (with Application Controller) QuickStart:
    //		ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.wcsf.2007jun/wcsf/html/02-480-ViewPresenter.htm


    #region IBillingSummary Members

    public System.Collections.Generic.IList<TributesPortal.BusinessEntities.PaymentReceipt> PaymentReceipt
    {
        set
        {
            FormView1.DataSource = value;
            FormView1.DataBind();        
        }
    }

    #endregion
}


