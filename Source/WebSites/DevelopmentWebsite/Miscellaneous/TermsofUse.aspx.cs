///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Miscellaneous.TermsofUse.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page informs the users about the Terms of Use to be followed when using the website
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
//using Facebook;
//using Facebook.Web;
//using Facebook.Session;

public partial class Miscellaneous_TermsofUse : PageBase, ITermsofUse
{
    private TermsofUsePresenter _presenter;
    //protected string _userName;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
            {
                divYM.Visible = true;
            }
        }
    }
    //    ConnectSession sess = new ConnectSession(
    //        ConfigurationManager.AppSettings["APIKey"],
    //        ConfigurationManager.AppSettings["Secret"]);

    //    if (!this.IsPostBack)
    //    {
    //        StateManager stateManager = StateManager.Instance;
    //        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);

    //        if (!Equals(objSessionvalue, null))
    //        {
    //            if (sess.IsConnected())
    //            {
    //                try
    //                {
    //                    Facebook.Rest.Api api = new Facebook.Rest.Api(sess);                        //Display user data captured from the Facebook API.  
    //                    Facebook.Schema.user user = api.Users.GetInfo();

    //                    spanLogout.InnerHtml = "<a href=\"#\" onclick=\"FB.Connect.logoutAndRedirect('" + Session["APP_BASE_DOMAIN"] + "Logout.aspx')\">" +
    //                      "   <img id=\"fb_logout_image\" src=\"http://static.ak.fbcdn.net/images/fbconnect/logout-buttons/logout_small.gif\" alt=\"Connect\"/>" +
    //                      "</a>";
    //                    if (objSessionvalue.UserName == null ||
    //                        string.Empty.Equals(objSessionvalue.UserName))
    //                    {
    //                        objSessionvalue.UserName = user.first_name + " " + user.last_name;
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    killFacebookCookies();
    //                    ShowMessage("Your Facebook session has timed out. Please clear private data in browser and log in again");
    //                    spanLogout.InnerHtml = "<a href='" + Session["APP_BASE_DOMAIN"] + "Logout.aspx'>Log out</a>";
    //                }
    //            }
    //            else
    //            {
    //                spanLogout.InnerHtml = "<a href='" + Session["APP_BASE_DOMAIN"] + "Logout.aspx'>Log out</a>";
    //            }
    //            _userName = objSessionvalue.UserName;
    //            myprofile.Visible = true;
    //            //myprofile.HRef = Session["APP_BASE_DOMAIN"] + "adminmytributeshome.aspx";
    //            myprofile.HRef = Session["APP_BASE_DOMAIN"].ToString() + "tributes.aspx";
    //        }
    //        else
    //        {
    //            spanLogout.InnerHtml = "<a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>Log in</a>";
    //            myprofile.Visible = false;
    //        }
    //        if (!sess.IsConnected())
    //        {
    //            spanLogout.InnerHtml = "<fb:login-button onlogin=\"window.location='" + Session["APP_BASE_DOMAIN"] + "log_in.aspx?location='+encodeURIComponent(location.href)\" v=\"2\"></fb:login-button>" + spanLogout.InnerHtml;
    //        }
    //        //this._presenter.OnViewInitialized();
    //    }
    //}

    [CreateNew]
    public TermsofUsePresenter Presenter
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


