///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Miscellaneous.AboutFeatures.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page informs about the features of the site to the site visitors
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
using TributesPortal.Miscellaneous.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;


public partial class Miscellaneous_features : PageBase, IAboutFeatures
{
   
    #region Variables
    protected string _userName;
    private AboutFeaturesPresenter _presenter;
    #endregion Variables
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            yourtribute.Visible = false;
            Yourmoments.Visible = true;
            featuresTitle.InnerHtml = @"Features - Learn what features are included with Your Moments";
            YT11.InnerHtml = @"Create an online website for free!";
        }
        else if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourtribute")
        {
            yourtribute.Visible = true;
            Yourmoments.Visible = false;
        }
    }

    [CreateNew]
    public AboutFeaturesPresenter Presenter
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

}


