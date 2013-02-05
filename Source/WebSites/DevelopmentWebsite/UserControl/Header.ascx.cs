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

public partial class UserControl_Header : System.Web.UI.UserControl
{
    protected string _userName = null;
    protected string _section = HeaderSecionEnum.home.ToString();
    protected string _tributeType = string.Empty;
    public string AppCurrentDomain = string.Empty;
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
        fbWebContext = FacebookWebContext.Current;        
        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionvalue, null) && !_section.Equals(HeaderSecionEnum.registration.ToString()))
        {
            spanLogout.InnerHtml = "<a id='header_logout' href='Logout.aspx'>Log out</a>";
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
        if (fbWebContext.Session== null || objSessionvalue == null)
        {
            StringBuilder sbl = new StringBuilder();
            sbl.Append("<fb:login-button size=\"small\"");

            if (Equals(objSessionvalue, null))
            {
                sbl.Append("\" onlogin=\"doAjaxLogin();\" v=\"2\"><fb:intl>");
                sbl.Append("Log in with Facebook");
            }
            else
            {
                sbl.Append("\" onlogin=\"doAjaxConnect();\" v=\"2\"><fb:intl>");
                sbl.Append("Connect");
            }

            sbl.Append("</fb:intl></fb:login-button>");
            sbl.Append(spanLogout.InnerHtml);

            spanLogout.InnerHtml = sbl.ToString();
            //////"<fb:login-button size=\"small\" +length=\""+
            //////(Equals(objSessionvalue, null) ? "long" : "short") + 
            //////"\" onlogin=\"window.location='" + Session["APP_BASE_DOMAIN"] + 
            //////"log_in.aspx?location='+encodeURIComponent(location.href)\" v=\"2\"></fb:login-button>" + 
            //////spanLogout.InnerHtml;
        }

        lnkAdvanceSearch.NavigateUrl = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "advancedsearch.aspx";
        // Set the controls value
        SetControlsValue();
    }

    private string GetTributeType()
    {
        string tributeType = "";

        if (rdoSearch_All.Checked == true)
        {
            tributeType = lblSearch_All.InnerText;
        }
        else if (rdoSearch_Anniversary.Checked == true)
        {
            tributeType = lblSearch_Anniversary.InnerText;
        }
        else if (rdoSearch_Birthday.Checked == true)
        {
            tributeType = lblSearch_Birthday.InnerText;
        }
        else if (rdoSearch_Graduation.Checked == true)
        {
            tributeType = lblSearch_Graduation.InnerText;
        }
        else if (rdoSearch_Memorial.Checked == true)
        {
            tributeType = lblSearch_Memorial.InnerText;
        }
        else if (rdoSearch_NewBaby.Checked == true)
        {
            tributeType = lblSearch_NewBaby.InnerText;
        }
        else if (rdoSearch_Wedding.Checked == true)
        {
            tributeType = lblSearch_Wedding.InnerText;
        }

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
            objSearchTribute.SearchString = txtSearchKeyword.Text.ToString();
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
            //Text for labels from the resource file
            lblFindTribute.Text = ResourceText.GetString("lblFindTribute_MP");                      // Find a Tribute
            lblSearchFor.InnerText = ResourceText.GetString("lblSearchFor_MP");                     // Search for:
            //txtSearchKeyword.Text = ResourceText.GetString("txtSearchKeyword_MP");                  // Enter the name of a Tribute
            lblSearch_All.InnerText = ResourceText.GetString("lblSearch_All_MP");                   // All Tributes
            lblSearch_Anniversary.InnerText = ResourceText.GetString("lblSearch_Anniversary_MP");   // Anniversary Tributes
            lblSearch_Birthday.InnerText = ResourceText.GetString("lblSearch_Birthday_MP");         // Birthday Tribute
            lblSearch_Graduation.InnerText = ResourceText.GetString("lblSearch_Graduation_MP");     // Graduation Tributes
            lblSearch_Memorial.InnerText = ResourceText.GetString("lblSearch_Memorial_MP");         // Memorial Tributes
            lblSearch_NewBaby.InnerText = ResourceText.GetString("lblSearch_NewBaby_MP");           // New Baby Tributes
            lblSearch_Wedding.InnerText = ResourceText.GetString("lblSearch_Wedding_MP");           // Wedding Tributes
            lnkAdvanceSearch.Text = ResourceText.GetString("lnkAdvanceSearch_MP");                  // Advanced Search
            lnkClose.InnerText = ResourceText.GetString("lnkClose_MP");                             // Close

            txtSearchKeyword.Attributes.Add("onclick", "this.select();");
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
}
