///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Users.Logout.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page deletes the user session when the user logs out of the site and redirects to 
///                  the log in page
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
using TributesPortal.Utilities;
using TributesPortal.Users;
using Facebook;
using Facebook.Web;


public partial class Users_Logout : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cookies.Clear();

        //FacebookClient fbapp = new FacebookClient();
        //ConnectSession sess = new ConnectSession(
        //    ConfigurationManager.AppSettings["APIKey"],
        //    ConfigurationManager.AppSettings["Secret"]);
        var fbWebContext = FacebookWebContext.Current;
        if (FacebookWebContext.Current.Session != null)
        {
            FacebookWebContext.Current.DeleteAuthCookie();
            //sess.Logout();
            // just to be sure...
            killFacebookCookies();
        }

        Session.RemoveAll();
        UsersController _controller = new UsersController();
        StateManager stateManager = StateManager.Instance;
        _controller.DeleteSessionDetails(Session.SessionID);
        stateManager.Remove("objSessionvalue", StateManager.State.Session);
        Session.Clear();
        Session.Abandon();
        //StateManager objStateManager = StateManager.Instance;
        //objStateManager.Add("LoginChk", null, StateManager.State.Session);
        //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        HttpCookie cookie = new HttpCookie("ASP.NET_SessionId", string.Empty);
        cookie.Domain = "." + WebConfig.TopLevelDomain;
        Response.Cookies.Add(cookie);
        //Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddYears(-30);        
        //Response.Cookies["ASP.NET_SessionId"].Domain = "." + WebConfig.TopLevelDomain;
        Response.Redirect("~/log_in.aspx");
        //Redirect.PageList.Inner2LoginPage.ToString()));
    }
}
