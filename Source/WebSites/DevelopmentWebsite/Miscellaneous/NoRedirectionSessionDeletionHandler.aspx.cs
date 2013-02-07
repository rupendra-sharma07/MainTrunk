using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook;
using Facebook.Web;
using TributesPortal.Users;
using TributesPortal.Utilities;

public partial class Miscellaneous_NoRedirectionSessionDeletionHandler : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cookies.Clear();
        var fbWebContext = FacebookWebContext.Current;
        if (FacebookWebContext.Current.Session != null)
        {
            FacebookWebContext.Current.DeleteAuthCookie();           
            killFacebookCookies();
        }

        Session.RemoveAll();
        UsersController _controller = new UsersController();
        StateManager stateManager = StateManager.Instance;
        _controller.DeleteSessionDetails(Session.SessionID);
        stateManager.Remove("objSessionvalue", StateManager.State.Session);
        Session.Clear();
        Session.Abandon();        
        
        HttpCookie cookie = new HttpCookie("ASP.NET_SessionId", string.Empty);
        cookie.Domain = "." + WebConfig.TopLevelDomain;
        Response.Cookies.Add(cookie);        
    }
}