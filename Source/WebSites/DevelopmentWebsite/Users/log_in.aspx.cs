///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Users.log_in.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to log into the site
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
using System.Threading;
using System.Xml;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Users.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Miscellaneous;
using TributesPortal.Users;
using TributesPortal.Utilities;
using Facebook;
using Facebook.Web;
using System.Collections.Generic;
using TributesPortal.ResourceAccess;

public partial class Users_log_in : PageBase, Ilog_in
{
    
    private log_inPresenter _presenter;
    private static string BannerMessage = string.Empty;
    private string _Email;
    private string _FBEmail;
    private Nullable<Int64> _FacebookUid;
    int _EventID;
    string _EventName;
    const string headertext = "<h2>Oops - there was a problem with your login.</h2><h3>Please correct the errors below:</h3>";
    protected string _showSignUpDialog = "true";
    private bool m_bIsTerminating = false;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(Users_log_in));
        //this.Form.Action = Request.RawUrl;
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AddHeader("Pragma", "no-cache");
        showFbDialog.Text = "false";
        //Page.Title = "Log In - Your Tribute";
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            signUpYm.Visible = true;
            signUpYt.Visible = false;
            //Page.Title = "Log In - Your Moments";
            ytExamplesBlock.Visible = false;
        } 
             
        if (!this.IsPostBack)
        {            
            var fbWebContext = FacebookWebContext.Current;
            if (FacebookWebContext.Current.Session != null)
            {
                if (FacebookWebContext.Current.AccessToken != null)
                {
                    var fbwc = new FacebookWebClient(FacebookWebContext.Current.AccessToken);
                    var me = (IDictionary<string, object>)fbwc.Get("me");   // get own information
                    _FacebookUid = fbWebContext.UserId;                // get own user id
                    try
                    {

                        string fbName = (string)me["first_name"] + " " + (string)me["last_name"]; // get first name and last name
                        if (this._presenter.OnFacebookLogin(fbName))
                        {
                            _showSignUpDialog = "false";
                            showFbDialog.Text = "false";
                            SaveSessionInDB();
                            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", Session.SessionID));
                            Response.Cookies["ASP.NET_SessionId"].Domain = "." + WebConfig.TopLevelDomain;
                            RedirectPage();
                            return;
                        }
                        else
                        {
                            string notice = string.Format("<h2>Welcome, {0}.</h2>" +
                                "<h3>Login here to connect your Facebook login to your account<br/>" +
                                "Or sign-up to create full account, connected with your Facebook credentials</h3>", fbName);
                            Notice.InnerHtml = notice;
                            Notice.Visible = true;
                            lbtnLogin.Text = "Connect";
                            if (Request.QueryString["ytfblink"] != null)
                            {
                                showFbDialog.Text = "false";
                            }
                            else
                            {
                                showFbDialog.Text = "true";
                            }
                        }
                    }
                    catch (FacebookApiException ex)
                    {
                        _showSignUpDialog = "false";
                        ShowMessage("Something unexpected went wrong. Sorry. Please <a href=\"#\" onclick=\"fb_logout(); return false;\">" +
                              "   <img id=\"fb_logout_image\" src=\"http://static.ak.fbcdn.net/images/fbconnect/logout-buttons/logout_small.gif\" alt=\"Connect\"/>" +
                              "</a> and try again." + ex.ToString());
                        doFbLogout.Text = "true";
                    }
                }
            }

            Page.Form.DefaultButton = this.FindControl(lbtnLogin.ClientID).UniqueID;
            Page.Form.DefaultFocus = this.FindControl(txtLoginUsername.ClientID).UniqueID;
            // Page.Form.DefaultButton = this.FindControl(txtLoginUsername.ClientID).UniqueID;
            lbtnLogin.Attributes.Add("onclick", "JavaScript:HideIndecator();");
            txtLoginUsername.Attributes.Add("onkeydown", "JavaScript:if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + lbtnLogin.UniqueID + "').click();return false;}} else {return true}; ");
            txtLoginPassword.Attributes.Add("onkeydown", "JavaScript:if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + lbtnLogin.UniqueID + "').click();return false;}} else {return true}; ");
            this._presenter.OnViewInitialized();
            if (Request.QueryString["PageName"] != null)
            {
                string PageName = Request.QueryString["PageName"].ToString();
                if (PageName == "TributeCreation")
                {
                    //LHK: this is the maessage that appears in the background of Login label.
                    lblMessage.InnerText = "In order to create and contribute, you first need to login to Your Tribute.";
                    lblMessage.Visible = true;
                }
            }
            // Added For Event Handling - Parul
            else if (Request.QueryString["EventID"] != null)
            {
                _EventID = int.Parse(Request.QueryString["EventID"].ToString());
                String TributeUrl = Request.QueryString["TributeUrl"].ToString();
                String TypeDescription = Request.QueryString["TributeType"].ToString();

                _presenter.GetEventName();

                if (_EventName != "")
                {
                    string href = "http://" + TypeDescription.Replace("New Baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/event.aspx?EventID=" + EventID;// +"&TributeID=" + TributeID;
                    lblMessage.InnerHtml = "You have been invited to the event " + "<a href='" + href + "'>" + _EventName + "</a>" + ". Please Log in or Sign up to RSVP to the event. <BR>To view the event details and RSVP later, " + "<a href='" + href + "'>" + "Click Here" + "</a>" + ".";
                    lblMessage.Visible = true;
                }
            }
            else if (Request.QueryString["TributeName"] != null)
            {
                string TributeName = Request.QueryString["TributeName"].ToString();
                string TributeId = Request.QueryString["Tributeid"].ToString();
                SetNoticeHeader(TributeName, TributeId);

            }
            else if (Request.QueryString["TributeUrl"] != null)
            {
                Tributes objTribute = new Tributes();
                objTribute.TributeUrl = Request.QueryString["TributeUrl"].ToString();
                objTribute.TypeDescription = Request.QueryString["TributeType"].ToString();

                MiscellaneousController objMisc = new MiscellaneousController();
                SetNoticeHeader(objMisc.GetTributeSessionForUrlAndType(objTribute,WebConfig.ApplicationType.ToString()));

            }
            else
            {
                lblMessage.Visible = false;
            }

            if (lblMessage.Visible == false && Session["IsTributeDeleted"] != null && Convert.ToBoolean(Session["IsTributeDeleted"]) == true)
            {
                ShowMessage("<h2>Oops - there is a problem with tribute.</h2> <h3>Please correct the errors below:</h3>", "Tribute has been deleted by administrator.", true);
                Session["IsTributeDeleted"] = null;
                _showSignUpDialog = "false";
            }
        }

        this._presenter.OnViewLoaded();
    }

    [CreateNew]
    public log_inPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    private void SetNoticeHeader(string tributename, string tributeid)
    {
        Tributes objTribute = new Tributes();
        objTribute.TributeId = int.Parse(tributeid);
        objTribute.TributeName = tributename;
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add("TributeSession", objTribute, TributesPortal.Utilities.StateManager.State.Session);
        String strbul = "";
        // Added by Ashu on Oct 4, 2011 for rewrite URL 
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
            strbul = "<h2>You have been selected to be an administrator of the Tribute: <a href='MomentsHomePage.aspx'>" + tributename + "</a></h2>";
        else if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourtribute")
            strbul = "<h2>You have been selected to be an administrator of the Tribute: <a href='tributehomepage.aspx'>" + tributename + "</a></h2>";
        strbul += " <h3>Please log in or sign up to continue.</h3>";
        Notice.InnerHtml = strbul;
        Notice.Visible = true;
    }

    private void SetNoticeHeader(Tributes objTribute)
    {
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add("TributeSession", objTribute, TributesPortal.Utilities.StateManager.State.Session);
        String strbul = "<h2>You have been selected to be an administrator of the Tribute: <a href='http://" + objTribute.TypeDescription + "." + WebConfig.TopLevelDomain + "/" + objTribute.TributeUrl + "'>" + objTribute.TributeName + "</a></h2>";
        strbul += " <h3>Please log in or sign up to continue.</h3>";
        Notice.InnerHtml = strbul;
        Notice.Visible = true;
    }


    protected void lbtnLogin_Click(object sender, EventArgs e)
    {

        try
        {
            _showSignUpDialog = "false";
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", Session.SessionID));
            Response.Cookies["ASP.NET_SessionId"].Domain = "." + WebConfig.TopLevelDomain;
            this._presenter.OnLogin();
            SaveSessionInDB();

            if (BannerMessage == "")
            {
                errorUserName.Visible = false;
                errorPwd.Visible = false;
                //SESSION_TIMEOUT_ISSUE
                ////set this to indicate some user logged in
                //HttpCookie cooky = new HttpCookie("logintracker");
                //cooky.Domain = "." + WebConfig.TopLevelDomain;
                //Response.Cookies.Add(cooky);
                RedirectPage();
            }
            ShowMessage(headertext, BannerMessage, true);
            errorUserName.Visible = true;
            errorPwd.Visible = true;
            Page.SetFocus(txtLoginPassword);
        }
        catch (Exception ex)
        {
            ShowMessage(headertext, ex.Message, true);
            errorUserName.Visible = true;
            errorPwd.Visible = true;
            _showSignUpDialog = "false";
        }

    }

    private void RedirectPage()
    {
        string destination_url = "";

        if (Request.QueryString["location"] != null)
        {
            destination_url = Request.QueryString["location"].ToString();
            //Response.Redirect(Request.QueryString["location"].ToString(), false);
        } 
        else if (Request.QueryString["EventId"] != null)
        {
            string EventID = Request.QueryString["EventID"].ToString();
            String TributeUrl = Request.QueryString["TributeUrl"].ToString();
            String TypeDescription = Request.QueryString["TributeType"].ToString();
            string EmailID = Request.QueryString["Email"].ToString();

            if (EmailID == _Email)
            {
                //string queryString = "http://" + TypeDescription.Replace("New Baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/event.aspx?EventID=" + EventID;// +"&TributeID=" + TributeID;
                string queryString = "?EventID=" + EventID;// +"&TributeID=" + TributeID;

                //  string queryString = "?EventID=" + EventID + "&TributeID=" + TributeID;
                //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.EventFullView.ToString()) + queryString, false);

                if (WebConfig.ApplicationMode.Equals("local"))
                {
                    //uncomment the below line and comment the line below for server
                    destination_url = Session["APP_BASE_DOMAIN"].ToString() + TributeUrl + "/event.aspx" + queryString;
                    //Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + TributeUrl + "/event.aspx" + queryString, false);
                }
                else
                {
                    destination_url = "http://" + TypeDescription.Replace("New Baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/event.aspx" + queryString;
                    //Response.Redirect("http://" + TypeDescription.Replace("New Baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/event.aspx" + queryString, false);
                }
            }
            else
            {
                //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.UserAccounts.ToString()));
                //Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + "tributes.aspx", false);
                //if (txtLoginUsername.Text.Contains("@"))
                //{
                //login with the incorrect email address (not the email that the admin request was sent to)
                RemoveSession();
                BannerMessage = "You did not login with the same username that the event request was sent to? Please try again.";
                ShowMessage(headertext, BannerMessage, true);
                errorUserName.Visible = true;
                errorPwd.Visible = true;
                //txtLoginUsername.Text = "";
                Page.SetFocus(txtLoginPassword);
                _showSignUpDialog = "false";
            }
        }
        else if (Request.QueryString["PageName"] != null)
        {
            string PageName = Request.QueryString["PageName"].ToString();

            if (PageName == "TributeCreation")
            {
                string Querystring = string.Empty;
                if (Request.QueryString["Type"] != null)
                {
                    if (string.IsNullOrEmpty(Querystring))
                        Querystring = "Type=" + Request.QueryString["Type"].ToString();
                    else
                        Querystring = "&Type=" + Request.QueryString["Type"].ToString();
                }
                if (Request.QueryString["VideoTributeId"] != null)
                {
                    if (string.IsNullOrEmpty(Querystring))
                        Querystring += "VideoTributeId=" + Request.QueryString["VideoTributeId"].ToString();
                    else
                        Querystring += "&VideoTributeId=" + Request.QueryString["VideoTributeId"].ToString();
                }
                if (Request.QueryString["AccountType"] != null)
                {
                    if(string.IsNullOrEmpty(Querystring))
                        Querystring += "AccountType=" + Request.QueryString["AccountType"].ToString();
                    else
                        Querystring += "&AccountType=" + Request.QueryString["AccountType"].ToString();
                }

                destination_url = Session["APP_BASE_DOMAIN"].ToString() + "create.aspx" + "?" + Querystring;
                //Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + "create.aspx" + "?" + Querystring, false);
            }
            else if (PageName == "myprofile")
            {
                destination_url = Session["APP_BASE_DOMAIN"].ToString() + "AdminProfileSettings.aspx";
                //Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + "AdminProfileSettings.aspx", false);
            }
        }
        else if (Request.QueryString["TributeUrl"] != null)
        {
            //TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            //stateManager.Get("TributeSession", TributesPortal.Utilities.StateManager.State.Session);

            if (Request.QueryString["Isowner"] != null)
            {
                // Added by Ashu on Oct 4 2011 for rewrite URL 
                if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                    destination_url = "MomentsHomePage.aspx";
                else
                    destination_url = "tributehomepage.aspx";
                //Response.Redirect("tributehomepage.aspx");
            }
            else
            {
                if (Request.QueryString["Email"] != null)
                {
                    if (((SessionValue)Session["objSessionvalue"]).UserEmail == Request.QueryString["Email"].ToString())
                    {
                        Session["Email"] = Request.QueryString["Email"];

                        if (WebConfig.ApplicationMode.Equals("local"))
                        {
                            destination_url = Session["APP_BASE_DOMAIN"] + Request.QueryString["TributeUrl"].ToString() + "/inviteadminconfirmation.aspx";
                            //Response.Redirect(Session["APP_BASE_DOMAIN"] + Request.QueryString["TributeUrl"].ToString() + "/inviteadminconfirmation.aspx", false);
                        }
                        else
                        {
                            destination_url = "http://" + Request.QueryString["TributeType"].ToString().Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + Request.QueryString["TributeUrl"].ToString() + "/inviteadminconfirmation.aspx";
                            //Response.Redirect("http://" + Request.QueryString["TributeType"].ToString().Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + Request.QueryString["TributeUrl"].ToString() + "/inviteadminconfirmation.aspx");
                        }
                    }
                    else
                    {
                        //if (txtLoginUsername.Text.Contains("@"))
                        //{
                        //login with the incorrect email address (not the email that the admin request was sent to)
                        RemoveSession();
                        BannerMessage = "You did not login with the same username that the admin request was sent to? Please try again.";
                        ShowMessage(headertext, BannerMessage, true);
                        errorUserName.Visible = true;
                        errorPwd.Visible = true;
                        //txtLoginUsername.Text = "";
                        Page.SetFocus(txtLoginPassword);
                        _showSignUpDialog = "false";
                        //}
                        //else
                        //    Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + "tributes.aspx", false);
                    }
                }
            }
        }
        else if (Request.QueryString["mode"] != null)
        {
            if (Request.QueryString["mode"].ToString() == "inbox")
            {
                if (WebConfig.ApplicationMode.Equals("local"))
                {
                    destination_url = Session["APP_BASE_DOMAIN"] + "inbox.aspx";
                    //Response.Redirect(Session["APP_BASE_DOMAIN"] + "inbox.aspx", false);
                }
                else
                {
                    destination_url = "http://www." + WebConfig.TopLevelDomain + "/inbox.aspx";
                    //Response.Redirect("http://www." + WebConfig.TopLevelDomain + "/inbox.aspx", false);
                }
            }
        }
        else
        {
            // Added by Ashu on Oct 3, 2011 for rewrite URL 
            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                destination_url = Session["APP_BASE_DOMAIN"].ToString() + "moments.aspx";
            else
                destination_url = Session["APP_BASE_DOMAIN"].ToString() + "tributes.aspx";
            //Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + "tributes.aspx", false);
        }
        if ((Request.QueryString["Url"] != null) && (Request.QueryString["Type"] != null))
        {
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                destination_url = Session["APP_BASE_DOMAIN"] + Request.QueryString["Url"].ToString() + "/";
                //Response.Redirect(Session["APP_BASE_DOMAIN"] + Request.QueryString["TributeUrl"].ToString() + "/inviteadminconfirmation.aspx", false);
            }
            else
            {
                destination_url = "http://" + Request.QueryString["Type"].ToString().Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + Request.QueryString["Url"].ToString() + "/";
            }
        }

        if (!destination_url.Equals(string.Empty))
        {
           string  strResponse = "<html><head><title>Wait...</title></head>"+
            "<body><div align='center'><center>"+
            "<img src='assets/images/ajax-loader.gif'/>" +
            "</center></div></body></html>";

           try
           {
               Response.Clear();
               Response.Write(strResponse);

               Response.Redirect(destination_url, false); // without false parameter?
               HttpContext.Current.ApplicationInstance.CompleteRequest();
               m_bIsTerminating = true;
           }
           catch (ThreadAbortException ) { } // remove e after exception  by Ud
        }
    }

    protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
    {
        if (m_bIsTerminating == false)
            base.RaisePostBackEvent(sourceControl, eventArgument);
    }

    protected override void Render(HtmlTextWriter writer)
    {
        if (m_bIsTerminating == false)
            base.Render(writer);
    } 
    
    private void SavetimeStamps()
    {
        DateTime dtm = DateTime.Now;
        ViewState.Add("Timestamp", dtm);
        Session.Add("Portal_Timestamp", dtm);
    }
    private bool IsExpired()
    {
        if (Session["Portal_Timestamp"] == null)
            return false;
        else if (ViewState["Timestamp"] == null)
            return false;
        else if (Session["Portal_Timestamp"].ToString() == ViewState["Timestamp"].ToString())
            return false;
        else
            return true;

    }

    #region Ilog_in Members

    public string UserName
    {
        get { return txtLoginUsername.Text; }
    }

    public string Password
    {
        get
        {
            return TributePortalSecurity.Security.EncryptSymmetric(txtLoginPassword.Text.ToString());
            // return txtLoginPassword.Text.ToLower().ToString();
        }
    }
    //public int FacebookCountryId
    //{
    //    get
    //    {
    //        return  _FacebookCountryId;
    //    }
    //    set
    //    {
    //        _FacebookCountryId = value;
    //    }
    //}
    //public int FacebookStateId
    //{
    //    get
    //    {
    //        return _FacebookStateId;
    //    }
    //    set
    //    {
    //        _FacebookStateId = value;
    //    }
    //}
    public string Email
    {
        set { _Email = value; }
    }

    public Nullable<Int64> FacebookUid
    {
        get { return _FacebookUid; }
        set { _FacebookUid = value; }
    }

    public int EventID
    {
        get
        {
            if (Request.QueryString["EventID"] != null)
            {
                return int.Parse(Request.QueryString["EventID"].ToString());
            }
            else
            {
                return 0;
            }
        }
    }

    public string EventName
    {
        set { _EventName = value; }
    }

    public string Message
    {
        set { BannerMessage = value; ; }
    }
    public string ApplicationType
    {
        get { return ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower();}
    }

    #endregion
    protected void lbtnFacebookSignup_Click(object sender, EventArgs e)
    {
        var fbWebContext = FacebookWebContext.Current;
        if (FacebookWebContext.Current.Session != null)
        {
            var fbwc = new FacebookWebClient(FacebookWebContext.Current.AccessToken);
            var me = (IDictionary<string, object>)fbwc.Get("me");
            _FacebookUid = fbWebContext.UserId;
            try
            {
                string fbName = (string)me["first_name"] + " " + (string)me["last_name"];

                string UserName = fbName.ToLower().Replace(" ", "_").Replace("'", "");/*+
                    "_"+_FacebookUid.ToString()*/

                Nullable<int> state = null;
                Nullable<int> country = null;
                string _UserImage = "images/bg_ProfilePhoto.gif";
                //if (!string.IsNullOrEmpty(user.pic_square))
                string fql = "Select current_location,pic_square,email from user where uid = " + fbWebContext.UserId;
                JsonArray me2 = (JsonArray)fbwc.Query(fql);
                var mm = (IDictionary<string, object>)me2[0];

                if (!string.IsNullOrEmpty((string)mm["pic_square"]))
                {
                    _UserImage = (string)mm["pic_square"]; // get user image
                }

                string city = "";
                if ((JsonObject)mm["current_location"] != null)
                {
                    JsonObject hl = (JsonObject)mm["current_location"];
                    city = (string)hl[0];

                    if (_presenter.GetFacebookStateId((string)hl[2], (string)hl[1]) > 0)
                    {
                        state = _presenter.GetFacebookStateId((string)hl[2], (string)hl[1]);
                    }
                    if (_presenter.GetFacebookCountryId((string)hl[2]) > 0)
                    {
                        country = _presenter.GetFacebookCountryId((string)hl[2]);
                    }

                }

                string password_ = string.Empty;
                _FBEmail = string.Empty;  //user.proxied_email;

                string result = (string)mm["email"];
                if (!string.IsNullOrEmpty(result))
                {

                    _FBEmail = result;
                    password_ = RandomPassword.Generate(8, 10);
                    password_ = TributePortalSecurity.Security.EncryptSymmetric(password_);

                }

                int _email = _presenter.EmailAvailable();
                if (_email == 0)
                {

                    UserRegistration objUserReg = new UserRegistration();
                    TributesPortal.BusinessEntities.Users objUsers =
                        new TributesPortal.BusinessEntities.Users(
                         UserName, password_,
                         (string)me["first_name"], (string)me["last_name"], _FBEmail,
                         "", false,
                         city, state, country, 1, _FacebookUid, ApplicationType);
                    objUsers.UserImage = _UserImage;
                    // objUsers.ApplicationType = ApplicationType;
                    objUserReg.Users = objUsers;

                    /*System.Decimal identity = (System.Decimal)*/
                    _presenter.DoShortFacebookSignup(objUserReg);

                    if (objUserReg.CustomError != null)
                    {
                        ShowMessage(string.Format("<h2>Sorry, {0}.</h2>" +
                            "<h3>Those Facebook credentials are already used in some other Your Tribute Account</h3>", fbName), "", true);
                    }
                    else
                    {
                        SessionValue _objSessionValue = new SessionValue(objUserReg.Users.UserId,
                                                                         objUserReg.Users.UserName,
                                                                         objUserReg.Users.FirstName,
                                                                         objUserReg.Users.LastName,
                                                                         objUserReg.Users.Email,
                                                                         objUserReg.UserBusiness == null ? 1 : 2,
                                                                         "Basic",
                                                                         objUserReg.Users.IsUsernameVisiable);
                        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
                        stateManager.Add("objSessionvalue", _objSessionValue, TributesPortal.Utilities.StateManager.State.Session);

                        SaveSessionInDB();
                        Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", Session.SessionID));
                        Response.Cookies["ASP.NET_SessionId"].Domain = "." + WebConfig.TopLevelDomain;
                        RedirectPage();
                        return;
                    }
                }
                else
                {
                    ShowMessage(headertext, "User already exists for this email: " + _FBEmail, true);//COMDIFFRES: is this message correct?
                    _showSignUpDialog = "false";
                }

            }
            catch (Exception ex)
            {
                ShowMessage(headertext, ex.Message, true);
                _showSignUpDialog = "false";
                // killFacebookCookies();
                // ShowMessage("Your Facebook session has timed out. Please logout and try again");
            }

        }
    }

    protected void lbtnSignup_Click(object sender, EventArgs e)
    {
        string str = this.Page.Title.ToString();

        // Added For Event Handling - Parul
        if (Request.QueryString["EventID"] != null)
        {
            string EventID = Request.QueryString["EventID"].ToString();
            String TributeUrl = Request.QueryString["TributeUrl"].ToString();
            String TypeDescription = Request.QueryString["TributeType"].ToString().Replace("New Baby", "newbaby");
            string EmailID = Request.QueryString["Email"].ToString();

            string queryString = "?EventID=" + EventID + "&TributeUrl=" + TributeUrl + "&TributeType=" + TypeDescription + "&Email=" + EmailID;
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Admin2UserRegistration.ToString()) + queryString, false);
        }
        else if (Request.QueryString["TributeUrl"] != null)
        {
            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            Tributes objTributeDetails = (Tributes)stateManager.Get("TributeSession", TributesPortal.Utilities.StateManager.State.Session);
            string EmailID = Request.QueryString["Email"].ToString();

            string queryString = "?TributeUrl=" + objTributeDetails.TributeUrl + "&TributeType=" + Request.QueryString["TributeType"].ToString().Replace("New Baby", "newbaby").ToLower() + "&Email=" + EmailID;
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Admin2UserRegistration.ToString()) + queryString, false);
        }
        else if (Request.QueryString["Tributeid"] != null)
        {
            string TributeName = Request.QueryString["TributeName"].ToString();
            string TributeID = Request.QueryString["Tributeid"].ToString();
            string EmailID = Request.QueryString["Email"].ToString();

            string queryString = "?TributeName=" + TributeName + "&TributeID=" + TributeID + "&Email=" + EmailID;
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Admin2UserRegistration.ToString()) + queryString, false);
        }
        else if (Request.QueryString["PageName"] != null)
        {
            string PageName = Request.QueryString["PageName"].ToString();

            if (PageName == "TributeCreation")
            {
                string Type;
                string queryString = string.Empty;
                if (Request.QueryString["AccountType"] != null)
                {
                    queryString = "?AccountType=" + Request.QueryString["AccountType"].ToString();
                }
                if (Request.QueryString["Type"] != null)
                {
                    Type = Request.QueryString["Type"].ToString();
                    if(string.IsNullOrEmpty(queryString))
                        queryString += "?PageName=" + PageName + "&Type=" + Type;
                    else
                        queryString += "&PageName=" + PageName + "&Type=" + Type;
                    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Admin2UserRegistration.ToString()) + queryString, false);
                }
                else
                {
                    if (string.IsNullOrEmpty(queryString))
                        queryString += "?PageName=" + PageName;
                    else
                        queryString += "&PageName=" + PageName;
                    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Admin2UserRegistration.ToString()) + queryString, false);
                }
            }
        }
        else
        {
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Admin2UserRegistration.ToString()), false);
        }
    }

    protected void lbtnforgetpassword_Click(object sender, EventArgs e)
    {

    }

    private void SaveSessionInDB()
    {
        SessionValue _objSessionValue = null;
        HttpContext.Current.Request.Path.ToString();
        TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;
        _objSessionValue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);

        if (!Equals(_objSessionValue, null))
        {

            if (_objSessionValue.UserId > 0)
            {
                UsersController _controller = new UsersController();
                _controller.SessionStore(_objSessionValue, HttpContext.Current.Session.SessionID);

            }
        }
    }

    /// <summary>
    /// Remove session from the database
    /// </summary>
    private void RemoveSession()
    {
        Session.RemoveAll();
        UsersController _controller = new UsersController();
        _controller.DeleteSessionDetails(Session.SessionID);
        Session.Abandon();
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void UserLogOut()
    {
        //HttpContext.Current.Response.Cookies.Clear();
        UserInfoResource UIResource = new UserInfoResource();
        PageBase pb = new PageBase();
        var fbWebcontext = FacebookWebContext.Current;

        HttpContext.Current.Session.Clear();
        pb.killFacebookCookies();

        try
        {
            if (fbWebcontext.Session != null)
            {
                fbWebcontext.DeleteAuthCookie();
                // just to be sure...                
            }
        }
        catch
        { }

        HttpContext.Current.Session.RemoveAll();
        UsersController _controller = new UsersController();
        StateManager stateManager = StateManager.Instance;
        UIResource.DeleteSession(HttpContext.Current.Session.SessionID);
        stateManager.Remove("objSessionvalue", StateManager.State.Session);
        HttpContext.Current.Session.Clear();
        HttpContext.Current.Session.Abandon();

    }

    #region Ilog_in Members


    public string FBEmail
    {
        get
        {
            return _FBEmail;
        }
        set
        {
            _FBEmail = value;
        }
    }

    #endregion
}


