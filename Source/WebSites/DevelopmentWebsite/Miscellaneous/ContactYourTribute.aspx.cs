///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Miscellaneous.ContactYourTribute.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page informs the users how they can contact the YourTribute
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

public partial class Miscellaneous_ContactYourTribute : System.Web.UI.Page, IContactYourTribute
{
    private ContactYourTributePresenter _presenter;
    protected string _userName;
    private SessionValue objSessionValue = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "Contact Your " + ConfigurationManager.AppSettings["ApplicationWord"].ToString();
        if (!this.IsPostBack)
        {
            //code for YT Mobile redirections
            string redirctMobileUrl = string.Empty;

            DeviceManager deviceManager = new DeviceManager
            {
                UserAgent = Request.UserAgent,
                IsMobileBrowser = Request.Browser.IsMobileDevice
            };

            // Added by Varun Goel on 25 Jan 2013 for NoRedirection functionality
            TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;

            objSessionValue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionValue == null || objSessionValue.NoRedirection == null || objSessionValue.NoRedirection == false)
            {
                if (deviceManager.IsMobileDevice())
                {
                    // Redirection URL
                    redirctMobileUrl = string.Format("{0}{1}{2}", "https://www.", WebConfig.TopLevelDomain, "/mobile/ContactUs.html");
                    Response.Redirect(redirctMobileUrl, false);
                }
            }
        }
    }
    //    if (!this.IsPostBack)
    //    {
    //        StateManager stateManager = StateManager.Instance;
    //        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);

    //        if (!Equals(objSessionvalue, null))
    //        {
    //            _userName = objSessionvalue.UserName;
    //            spanLogout.InnerHtml = "<a href='Logout.aspx'>Log out</a>";
    //            myprofile.Visible = true;
    //            //myprofile.HRef = Session["APP_BASE_DOMAIN"] + "adminmytributeshome.aspx";
    //            myprofile.HRef = Session["APP_BASE_DOMAIN"].ToString() + "tributes.aspx";
    //        }
    //        else
    //        {
    //            spanLogout.InnerHtml = "<a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>Log in</a>";
    //            myprofile.Visible = false;
    //        }
    //        //this._presenter.OnViewInitialized();
    //    }
    //}

    [CreateNew]
    public ContactYourTributePresenter Presenter
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


