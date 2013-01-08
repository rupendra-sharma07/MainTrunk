///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.ModelPopup.LoginPopup.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to log in to the site from a popup window.
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
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;
using TributesPortal.Utilities;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using Facebook;
using Facebook.Web;
using System.Collections.Generic;


public partial class ModelPopup_LoginPopup : System.Web.UI.Page
{

    FacebookWebContext fbwebContext;
    private static string BannerMessage = string.Empty;

    private Nullable<Int64> _FacebookUid;
    const string headertext = "<h2>Oops - there was a problem with your login.</h2><h3>Please correct the errors below:</h3>";
    protected string _showSignUpDialog = "true";

    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(ModelPopup_LoginPopup));

        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            lbllogin.Text = "Log in to Your Moments";
            lblemailinfo.Text = "Enter your Your Moments login info:";
        }
        
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AddHeader("Pragma", "no-cache");


        if (!Page.IsPostBack)
        {
            //Remote Code
            Response.Cookies["ASP.NET_SessionId"].Value = Session.SessionID;
            Response.Cookies["ASP.NET_SessionId"].Domain = "." + WebConfig.TopLevelDomain + "";
        }

        if (!WebConfig.ApplicationMode.Equals("local"))
        {
            string strXmlPath = AppDomain.CurrentDomain.BaseDirectory + "Common\\XML\\SSLPage.xml";
            FileStream docIn = new FileStream(strXmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            XmlDocument pageDoc = new XmlDocument();
            //Load the Xml Document
            pageDoc.Load(docIn);
            XmlNodeList XNodeList = pageDoc.SelectNodes("//Pages/page");
            string requestedURL = Request.Url.ToString().ToUpper();
            string[] rawURL = Request.RawUrl.Split("/".ToCharArray());
            int length = rawURL.Length;
            string redirectedPageName = rawURL[length - 1].ToString();
            if (requestedURL.Contains(@"HTTP://"))
            {
                foreach (XmlElement XElement in XNodeList)
                {
                    string pagename = XElement["pagename"].InnerText.ToString().ToUpper();
                    if (requestedURL.Contains(pagename))
                    {
                        Response.Redirect(@"https://www." + WebConfig.TopLevelDomain + "/" + redirectedPageName);

                    }
                }
            }
            else
            {
                bool isPageSecure = false;
                foreach (XmlElement XElement in XNodeList)
                {
                    string pagename = XElement["pagename"].InnerText.ToString().ToUpper();
                    if (requestedURL.Contains(pagename))
                    {
                        isPageSecure = true;
                    }
                }
                if (!isPageSecure)
                    Response.Redirect(@"http://www." + WebConfig.TopLevelDomain + "/" + redirectedPageName);
            }
        }
    }


    //[AjaxPro.AjaxMethod()]   
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void FacebookLogin()
    {
        fbwebContext = FacebookWebContext.Current;
        if (FacebookWebContext.Current.Session != null)
        {
            _FacebookUid = fbwebContext.UserId;
            try
            {
                var fbwc = new FacebookWebClient(FacebookWebContext.Current.AccessToken);
                var me = (IDictionary<string, object>)fbwc.Get("me");
                string fbName = (string)me["first_name"] + " " + (string)me["last_name"]; // get first name and last name
                if (OnFacebookLogin(fbName))
                {
                    SaveSessionInDB();
                    return;
                }
                else
                {
                    string notice = string.Format("<h2>Welcome, {0}.</h2>" +
                        "<h3>Login here to connect your Facebook login to your account<br/>" +
                        "Or sign-up to create full account, connected with your Facebook credentials</h3>", fbName);
                    if (Request.QueryString["ytfblink"] != null)
                    {

                    }
                    else
                    {

                    }
                }
            }
            catch (FacebookOAuthException ex)
            {

            }
        }
    }
    private void CheckUserAvailablity(string UserName, string Password)
    {

        errorPwd.Visible = false;
        string uName = string.Empty;
        HiddenField hf = (HiddenField)this.FindControl("ctl00$HiddenFieldAvailability");
        UserInfoManager objUserInfoManager = new UserInfoManager();
        GenralUserInfo _objGenralUserInfo = new GenralUserInfo();
        UserInfo objUserInfo = new UserInfo();
        objUserInfo.UserName = UserName;
        objUserInfo.UserPassword = Password.ToLower().ToString();
        objUserInfo.ApplicationType = ConfigurationManager.AppSettings["ApplicationType"].ToString();
        _objGenralUserInfo.RecentUsers = objUserInfo;

        objUserInfoManager.UserLogin(_objGenralUserInfo);
        if (_objGenralUserInfo.CustomError == null)
        {
            //added the below two lines to handle the session issue
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", Session.SessionID));
            Response.Cookies["ASP.NET_SessionId"].Domain = "." + WebConfig.TopLevelDomain;

            SetSessionValue(_objGenralUserInfo);
            RedirectPage();
        }
        else
        {
            errorPwd.Visible = true;
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", "setIndicatorPassword_();", true);
        }

    }

    private void SetSessionValue(GenralUserInfo _objGenralUserInfo)
    {
        SessionValue _objSessionValue = new SessionValue(_objGenralUserInfo.RecentUsers.UserID,
                                                               _objGenralUserInfo.RecentUsers.UserName,
                                                               _objGenralUserInfo.RecentUsers.FirstName,
                                                               _objGenralUserInfo.RecentUsers.LastName,
                                                               _objGenralUserInfo.RecentUsers.UserEmail,
                                                               int.Parse(_objGenralUserInfo.RecentUsers.UserType),
                                                               _objGenralUserInfo.RecentUsers.UserTypeDescription,
                                                               _objGenralUserInfo.RecentUsers.IsUsernameVisiable
                                                               , _objGenralUserInfo.RecentUsers.UserImage
                                                               );
        TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;
        stateManager.Add("objSessionvalue", _objSessionValue, StateManager.State.Session);
    }
    protected void popuplbtnSendemail_Click(object sender, EventArgs e)
    {
        GenralUserInfo objGenralUserInfo = new GenralUserInfo();
        UserInfo objUserInfo = new UserInfo();
        objUserInfo.UserEmail = txtLoginEmail1.Text;
        objGenralUserInfo.RecentUsers = objUserInfo;
        UserInfoManager objuse = new UserInfoManager();
        objuse.CheckAndSendPassword(objGenralUserInfo, false);
        if (objGenralUserInfo.CustomError != null)
        {
            string ErrorMsg = objGenralUserInfo.CustomError.ErrorMessage.ToString();
            if (ErrorMsg != "")
            {
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", "setForgotPassword_();", true);

            }
        }
        else
        {
            txtLoginEmail1.Text = string.Empty;

            StringBuilder strb = new StringBuilder();
            strb.Append("parent.modalClose_();");
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", strb.ToString(), true);
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

    }

    protected void popuplbtnSignup_Click(object sender, EventArgs e)
    {

    }
    protected void popuplbtnLogin_Click1(object sender, EventArgs e)
    {
        string Password = TributePortalSecurity.Security.EncryptSymmetric(txtLoginPassword1.Text.ToLower().ToString());
        CheckUserAvailablity(txtLoginUsername1.Text, Password);
        //SESSION_TIMEOUT_ISSUE
        ////set this to indicate some user logged in
        //HttpCookie cooky = new HttpCookie("logintracker");
        //cooky.Domain = "." + WebConfig.TopLevelDomain;
        //Response.Cookies.Add(cooky);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void popuplbtnSignup_Click1(object sender, EventArgs e)
    {
        string location = "../Users/userregistration.aspx";
        StringBuilder strb = new StringBuilder();
        strb.Append("parent.setLocation('" + location + "');");
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", strb.ToString(), true);

    }
    private void RedirectPage()
    {
        string location = Convert.ToString(Request.QueryString["location"]);

        if (Request.QueryString["title"] != null)
        {
            string title = Request.QueryString["title"].ToString();
            if (title.ToLower().Equals("sign up"))
            {
                string location_ = "../MyHome/AdminMytributesHome.aspx";
                StringBuilder strb = new StringBuilder();
                strb.Append("parent.setLocation(" + "'" + location_ + "'" + ");");
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", strb.ToString(), true);
            }
            else
            {
                StringBuilder Script = new StringBuilder();
                if (string.IsNullOrEmpty(location))
                {
                    location = "../MyHome/AdminMytributesHome.aspx";
                    Script.Append("parent.setLocation(" + "'" + location + "'" + ");");
                }
                else
                    Script.Append("chk(" + "'" + location + "'" + ");");
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", Script.ToString(), true);
            }
        }
        else
        {
            StringBuilder Script = new StringBuilder();
            if (string.IsNullOrEmpty(location))
            {
                location = "../MyHome/AdminMytributesHome.aspx";
                Script.Append("parent.setLocation(" + "'" + location + "'" + ");");
            }
            else
                Script.Append("chk(" + "'" + location + "'" + ");");
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", Script.ToString(), true);
        }
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
                FacadeManager.UserInfoManager.InsertSession(_objSessionValue, HttpContext.Current.Session.SessionID);

            }
        }
    }
    public bool OnFacebookLogin(string fbName)
    {
        GenralUserInfo _objGenralUserInfo = new GenralUserInfo();
        UserInfo objUserInfo = new UserInfo();
        objUserInfo.FacebookUid = _FacebookUid;
        objUserInfo.ApplicationType = ApplicationType;
        _objGenralUserInfo.RecentUsers = objUserInfo;
        if (objUserInfo.FacebookUid == null) return false;
        FacadeManager.UserInfoManager.CheckFacebookAccountAvailability(_objGenralUserInfo);
        //if (objGenralUserInfo.RecentUsers.UserID != null && objGenralUserInfo.RecentUsers.UserID > 0)  // commented by Ud to remove warning
        if (_objGenralUserInfo.RecentUsers.UserID > 0)
        {
            SetSessionValue(_objGenralUserInfo);
            return true;
        }
        else
        {
            //View.Message =
            //"If you are already a Your Tribute member, then please log in to associate your account with your Facebook credentials.";
            //;
        }
        return false;
    }
    public string ApplicationType
    {
        get {return ConfigurationManager.AppSettings["ApplicationType"].ToString(); }
    }
}
