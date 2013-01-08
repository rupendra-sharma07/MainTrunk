///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.ChannelHome .aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the channel home page
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Tribute.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;


public partial class Tribute_ChannelHome : System.Web.UI.Page, IChannelHome
{
    private ChannelHomePresenter _presenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this._presenter.OnViewInitialized();
        }
        this._presenter.OnViewLoaded();
    }

    [CreateNew]
    public ChannelHomePresenter Presenter
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

    //To bind the repeater control for featured tributes.
    public IList<FeaturedTribute> FeaturedTributes
    {
        set
        {
            if (value.Count > 0)
            {
                rptFeaturedTributes.Visible = true;
                lblNoRecord.Visible = false;
                rptFeaturedTributes.DataSource = value;
                rptFeaturedTributes.DataBind();
            }
            else
            {
                rptFeaturedTributes.Visible = false;
                lblNoRecord.Visible = true;
            }
        }
    }
}


