using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Text;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.BusinessLogic;
using Facebook;
using Facebook.Web;
using TributesPortal.ResourceAccess;

public partial class UserControl_HeaderHome : System.Web.UI.UserControl
{
    protected string _userName = null;
    protected string _section = HeaderSecionEnum.home.ToString();
    protected string _tributeType = string.Empty;
    public string HomeNavValue = string.Empty;
    public string TourNavValue = string.Empty;
    public string FeaturesNavValue = string.Empty;
    public string ExamplesNavValue = string.Empty;
    public string PricingNavValue = string.Empty;
    FacebookWebContext fbWebContext;

    public enum HeaderSecionEnum
    {
        tribute,
        home,
        channelHome,
        inner,
        login,
        registration
    }
    public string NavigationName { get; set; }

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
        string homeUrl = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"].ToString(); //Session["APP_BASE_DOMAIN"] +"Home.aspx";
        if (!string.IsNullOrEmpty(_tributeType) &&
            (_section.Equals(HeaderSecionEnum.tribute.ToString()) ||
             _section.Equals(HeaderSecionEnum.channelHome.ToString())))
        {
            Hashtable theme = new Hashtable();
            theme.Add("New Baby", "BabyDefault");
            theme.Add("Birthday", "BirthdayDefault");
            theme.Add("Graduation", "GraduationDefault");
            theme.Add("Wedding", "WeddingDefault");
            theme.Add("Anniversary", "AnniversaryDefault");
            theme.Add("Memorial", "MemorialDefault");

            StringBuilder sbq = new StringBuilder();

            if (WebConfig.ApplicationMode.Equals("local"))
            {
                sbq.Append(ConfigurationManager.AppSettings["APP_BASE_DOMAIN"].ToString());
                sbq.Append("channelhomepage.aspx?Type=");
                sbq.Append(HttpUtility.UrlEncode(_tributeType));
                sbq.Append("&Theme='");
                sbq.Append(theme[_tributeType]);
                sbq.Append("'");
            }
            else
            {
                sbq.Append("http://");
                sbq.Append(_tributeType.ToLower().Replace("New Baby", "newbaby"));
                sbq.Append(".");
                sbq.Append(WebConfig.TopLevelDomain);
            }
            homeUrl = sbq.ToString();
        }
        return homeUrl;
    }

    protected string LogoTitle()
    {
        string title = "Return to Your Tribute Home Page";
        if (!string.IsNullOrEmpty(_tributeType) &&
            (_section.Equals(HeaderSecionEnum.tribute.ToString()) ||
             _section.Equals(HeaderSecionEnum.channelHome.ToString())))
        {
            title = "Return to Channel Home Page";
        }
        return title;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            topNavigationYM.Visible = true;
            topNavigationYT.Visible = false;
        }

        fbWebContext = FacebookWebContext.Current;  // get current facebook session
        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionvalue, null) && !_section.Equals(HeaderSecionEnum.registration.ToString()))
        {
            spanLogout.InnerHtml = "<a class='logoutLink' id='header_logout' href='Logout.aspx'>Log out</a>";
            int intUserType = objSessionvalue.UserType;
            if (intUserType == 1)
            {
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

            sbl.Append("<a class='yt-horizontalSpacer' href='");
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
            spanLogout.InnerHtml = sbl.ToString();
            divProfile.Visible = false;
        }
        if (fbWebContext.Session == null || objSessionvalue == null)
        {
            StringBuilder sbl = new StringBuilder();
            sbl.Append("<fb:login-button size=\"small\"");
            sbl.Append("\" onlogin=\"doAjaxLogin();\" v=\"2\"><fb:intl>");
            sbl.Append((Equals(objSessionvalue, null) ? "Log in with Facebook" : "Connect"));
            sbl.Append("</fb:intl></fb:login-button>");
            sbl.Append(spanLogout.InnerHtml);

            spanLogout.InnerHtml = sbl.ToString();
        }

        HomeNavValue = "non-current";
        TourNavValue = "non-current";
        FeaturesNavValue = "non-current";
        ExamplesNavValue = "non-current";
        PricingNavValue = "non-current";

        switch (NavigationName)
        {
            case "Home":
                HomeNavValue = "current";
                break;
            case "Tour":
                TourNavValue = "current";
                break;
            case "Features":
                FeaturesNavValue = "current";
                break;
            case "Examples":
                ExamplesNavValue = "current";
                break;
            case "Pricing":
                PricingNavValue = "current";
                break;
            default:
                break;
        }

        // 25 Mar, 2010
        //string strURL = Request.Url.ToString();
        //if (strURL.Contains("AboutFeatures"))
        //{
        //    strURL = strURL.Replace("AboutFeatures", "features");
        //    Context.RewritePath(strURL);

        //}
        // match found - do any replacement needed
        //string sendToUrl = RewriterUtils.ResolveUrl(app.Context.Request.ApplicationPath, re.Replace(requestedPath, rules[i].SendTo));         


        //// Rewrite the URL
        //RewriterUtils.RewriteUrl(app.Context, sendToUrl);


    }//end page_load

    public void SetGlobalList(string className)
    {
        //yt-globalMenuItem1
    }

    //protected void lbtnCreditCount_Click(object sender, EventArgs e)
    //{
    //    StateManager stateManager = StateManager.Instance;
    //    SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
    //    if (!Equals(objSessionvalue, null))
    //    {
    //        Response.Redirect(Session["APP_BASE_DOMAIN"] + "Tribute/OrderCredit.aspx");
    //    }
    //    else
    //    {
    //        Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
    //    }
    //}
}//End class
