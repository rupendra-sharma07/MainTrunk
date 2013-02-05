///Copyright       : Copyright (c) Optimus Information
///Project         : Your Tributes
///File Name       : TributePortal.DevelopmentWebsite.UserControl.TributeHeader.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page defines the items to be displayed on the Video tribute page 
///Audit Trail     : Date of Modification     Modified By         Description


using System;
using System.Configuration;
using System.Web;
using System.Text;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using Facebook.Web;
using TributesPortal.ResourceAccess;
using TributesPortal.Users;

public partial class UserControl_TributeHeader : System.Web.UI.UserControl
{
    protected string _userName = null;
    protected string _section = HeaderSecionEnum.home.ToString();
    protected string _tributeType = string.Empty;
    public string AppCurrentDomain = string.Empty;
    PageBase pagebase = new PageBase();
    public enum HeaderSecionEnum
    {
        tribute,
        home,
        channelHome,
        inner,
        login,
        registration
    }

    public string Section
    {
        get
        {
            return _section;
        }
        set
        {
            _section = value;
        }
    }
    public string TributeType
    {
        get
        {
            return _tributeType;
        }
        set
        {
            _tributeType = value;
        }
    }
    public string CreditLink
    {
        set
        {
            //lbtnCreditCount.Text = "Credits (" + value + ")";
            lnCreditCount.InnerHtml = "Credits (" + value + ")";
        }
    }

    protected string HomeUrl()
    {
        string homeUrl = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"]; 
        return homeUrl;
    }

    protected string LogoTitle()
    {
        string title = "Return to Your " + ConfigurationManager.AppSettings["ApplicationType"] + " Home Page";
        return title;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(UserControl_TributeHeader));
        var fbWebContext = FacebookWebContext.Current.Session;
             
         StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionvalue, null) && !_section.Equals(HeaderSecionEnum.registration.ToString()))
        {
            spanLogout.InnerHtml = "<a id='header_logout' style='cursor:pointer; text-decoration:underline;' onclick='javascript:LogOut();'>Log out</a>";
            int intUserType = objSessionvalue.UserType;
            if (intUserType == 1)
            {
                // _userName = objSessionvalue.FirstName + " " + objSessionvalue.LastName;
                _userName = objSessionvalue.FirstName;
                lnCreditCount.Visible = false;
            }
            else if (intUserType == 2)
            {
                _userName = objSessionvalue.UserName;
                double NetCreditPoints;
                UserRegistration _objUserReg = new UserRegistration();
                Users objUsers = new Users();
                objUsers.UserId = objSessionvalue.UserId;
                _objUserReg.Users = objUsers;
                object[] param = { _objUserReg };
                BillingResource objBillingResource = new BillingResource();
                objBillingResource.GetCreditPointCount(param);
                UserRegistration objDetails = (UserRegistration)param[0];
                if (objDetails.CreditPointTransaction == null)
                {
                    NetCreditPoints = 0;
                }
                else
                {
                    NetCreditPoints = objDetails.CreditPointTransaction.NetCreditPoints;
                }
                //lbtnCreditCount.Text = "Credits (" + NetCreditPoints.ToString() + ")";
                lnCreditCount.InnerHtml = "Credits (" + NetCreditPoints.ToString() + ")";
                Session["_userId"] = objSessionvalue.UserId.ToString();

            }
            // Added by Ashu on Oct 3, 2011 for rewrite URL 
            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                myprofile.HRef = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"].ToString() + "moments.aspx";//Session["APP_BASE_DOMAIN"].ToString() + "moments.aspx";
            else
                myprofile.HRef = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"].ToString() + "tributes.aspx";//Session["APP_BASE_DOMAIN"].ToString() + "tributes.aspx";

            divProfile.Visible = true;
            spanSignUp.Visible = false;
        }
        else if (!_section.Equals(HeaderSecionEnum.inner.ToString()))
        {
            StringBuilder sbl = new StringBuilder();
            sbl.Append("<a href='");
            if (_section.Equals(HeaderSecionEnum.home.ToString()))
            {
                sbl.Append("log_in.aspx");
            }
            else
            {
                sbl.Append("javascript: void(0);' onclick='UserLoginModalpopupFromSubDomain(location.href,document.title);");
            }
            sbl.Append("'>Log in</a>");

            spanSignUp.Visible = !(_section.Equals(HeaderSecionEnum.registration.ToString()));
            lnRegistration.HRef = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"].ToString() +
                "UserRegistration.aspx";

            spanLogout.InnerHtml = sbl.ToString();
            divProfile.Visible = false;
        }

        if (FacebookWebContext.Current.Session == null || objSessionvalue == null)
        {
            StringBuilder sbl = new StringBuilder();
            sbl.Append("<fb:login-button size=\"small\"");

            if (Equals(objSessionvalue, null))
            {
                //LHK: for non logged in user Do not show Facebook 
                //sbl.Append("\" onlogin=\"doAjaxLogin();\" v=\"2\"><fb:intl>");
                //sbl.Append("Log in with Facebook");
            }
            else
            {
                sbl.Append("\" onlogin=\"doAjaxConnect();\" v=\"2\"><fb:intl>");
                sbl.Append("Connect");
            }

            sbl.Append("</fb:intl></fb:login-button>");
            sbl.Append(spanLogout.InnerHtml);

            spanLogout.InnerHtml = sbl.ToString();
            
        }
        // Set the controls value
        SetControlsValue();
    }


    protected void lnkLogout_click(object sender, EventArgs e)
    {
          PageBase pb = new PageBase();
        Response.Cookies.Clear();
        var fbWebcontext = FacebookWebContext.Current;
        if (fbWebcontext.Session!=null)
        {
            fbWebcontext.DeleteAuthCookie();
            // just to be sure...
            //pagebase.killFacebookCookies();
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

    private string GetTributeType()
    {
        string tributeType = "";
        return tributeType;
    }


    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            // Create SearchTribute object
            SearchTribute objSearchTribute = new SearchTribute();

            // Assign the search parameter to this object
            objSearchTribute.TributeType = GetTributeType();
            //objSearchTribute.SearchString = txtSearchKeyword.Text.ToString();
            objSearchTribute.SearchType = PortalEnums.SearchEnum.Basic.ToString();
            objSearchTribute.SortOrder = "DESC";

            // Create StateManager object and add search paramter in the session
            StateManager objStateMgr = StateManager.Instance;
            objStateMgr.Add(PortalEnums.SearchEnum.Search.ToString(), objSearchTribute, StateManager.State.Session);

            // Redirect to the Search Result page
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.SearchResult.ToString()));
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }
    /// <summary>
    /// This Function will set the value of the control and error messages from the resource File
    /// Added By Parul
    /// </summary>
    private void SetControlsValue()
    {
        try
        {
           if(string.IsNullOrEmpty(_userName))
           {
               TributeMainHeader.Visible = false;
           }
           else
           {
               TributeMainHeader.Visible = true;
           }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void UserLogOut()
    {
        UserInfoResource UIResource = new UserInfoResource();
        var fbWebcontext = FacebookWebContext.Current;
        HttpContext.Current.Session.Clear();
        pagebase.killFacebookCookies();
        
        try
        {
            if (fbWebcontext.Session != null)
            {                
                fbWebcontext.DeleteAuthCookie();
            }
        }
        catch
        { }
        
        HttpContext.Current.Session.RemoveAll();
        StateManager stateManager = StateManager.Instance;
        UIResource.DeleteSession(HttpContext.Current.Session.SessionID);
        stateManager.Remove("objSessionvalue", StateManager.State.Session);
        HttpContext.Current.Session.Clear();
        HttpContext.Current.Session.Abandon();
        
    }
   
}
