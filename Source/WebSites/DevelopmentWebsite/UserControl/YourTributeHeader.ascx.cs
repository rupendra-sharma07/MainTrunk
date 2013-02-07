///Copyright       : Copyright (c) Optimus Information
///Project         : Your Tributes
///File Name       : TributePortal.DevelopmentWebsite.UserControl.YourTributeHeader.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page defines the items to be displayed on the Video tribute page 
///Audit Trail     : Date of Modification     Modified By         Description


using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Text;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using Facebook.Web;
using TributesPortal.ResourceAccess;


public partial class UserControl_YourTributeHeader : System.Web.UI.UserControl
{
    protected string _userName = null;
    protected string _section = HeaderSecionEnum.home.ToString();
    protected string _tributeType = string.Empty;
    public string HomeNavValue = string.Empty;
    public string TourNavValue = string.Empty;
    public string FeaturesNavValue = string.Empty;
    public string ExamplesNavValue = string.Empty;
    public string PricingNavValue = string.Empty;
    public string LogoUrl = string.Empty;
    
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

    public string HomeUrl()
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
               // logo.Attributes.Add("href", ConfigurationManager.AppSettings["APP_BASE_DOMAIN"].ToString() + "channelhomepage.aspx?Type=" + HttpUtility.UrlEncode(_tributeType) + "&Theme=" + theme[_tributeType]);
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
        logo.Attributes.Add("href", homeUrl);
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
        logo.Attributes.Add("title", title);
        return title;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            topNavigationYM.Visible = true;
            topNavigationYT.Visible = false;
        }                
        
        var fbWebContext = FacebookWebContext.Current;        // get facebook session
        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionvalue, null) && !_section.Equals(HeaderSecionEnum.registration.ToString()))
        {
            spanLogout.InnerHtml = "<a class='logoutLink' id='header_logout' href='Logout.aspx?session=Logout'>Log out</a>";
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
            //spanSignUp.Visible = false;
        }
        else if (!_section.Equals(HeaderSecionEnum.inner.ToString()))
        {
            StringBuilder sbl = new StringBuilder();

            sbl.Append("<a class='yt-horizontalSpacer' href='");
                sbl.Append("log_in.aspx");
            sbl.Append("' >Log in</a>");
            spanLogout.InnerHtml = sbl.ToString();
            divProfile.Visible = false;
        }
        if (fbWebContext.Session == null && objSessionvalue != null)
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
        //Added to get home url and titleof logo
        string x = HomeUrl();
        x = LogoTitle();
    }//end page_load

    public void SetGlobalList(string className)
    {
        //yt-globalMenuItem1
    }
    
}//End class
