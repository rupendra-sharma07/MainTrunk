///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.App_Code.Redirect.cs
///Author          : 
///Creation Date   : 
///Description     : All Web forms will extend this PageBase class which provides the 
///                   following capabailities to all inheriting classes 
///                   1. Error Event Handler
///                   2. Exception handling
///                   3. Authorization
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Resources;
using System.Globalization;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;
using TributesPortal.Users;
using System.Reflection;
//using System.Text;

public class
    PageBase : System.Web.UI.Page
{

    /// <summary>
    /// 
    /// </summary>

    public PageBase()
    {
        this.Error += new EventHandler(PageBase_Error);
    }


    /// <summary>
    ///  Function Name     : OnPreInit
    ///  Purpose           : will set Theme of page.
    ///  Input             : 
    ///  return output     : void
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreInit(EventArgs e)
    {
        //SESSION_TIMEOUT_ISSUE
        //OnSessionTimeout();

        Response.Cookies["ASP.NET_SessionId"].Value = Session.SessionID;
        Response.Cookies["ASP.NET_SessionId"].Domain = "." + WebConfig.TopLevelDomain + "";
       

        //Page.RegisterStartupScript("PostBackFix","<script>document.forms[0].action='';</script>");
        StateManager obj = StateManager.Instance;
        obj.Add("CLientIP", Request.UserHostAddress.ToString(), StateManager.State.ViewState);
        
        GetSessionKeyValues();//Remove comment for server

        //string themeName = GetExistingTheme();
        //if (themeName != string.Empty)
        //{
        //    this.Theme = themeName;
        //}
        //else
        //{
        //    this.Theme = "Default";
        //}

        //this.Theme = "";

        //string strClientIP = Request.UserHostAddress;
        //string theme = "";
        //if (Page.Request.Form.Count > 0)
        //{
        //    string[] keys = Page.Request.Form.AllKeys;
        //    for (int i = 0; i < keys.Length; i++)
        //    {
        //        // TO DO : Change the name of control development. 
        //        if (keys[i] == "ctl00$Themes")
        //        {
        //            theme = Page.Request["ctl00$Themes"].ToString();
        //            if (theme == "Default")
        //            {
        //                theme = "";
        //            }
        //        }                
        //    }

        //}
        //this.Theme = theme;

        string servername = Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add("SERVERNAME", servername, TributesPortal.Utilities.StateManager.State.Session);
    }

    private void OnSessionTimeout()
    {
        //It appears from testing that the Request and Response both share the 
        // same cookie collection.  If I set a cookie myself in the Reponse, it is 
        // also immediately visible to the Request collection.  This just means that 
        // since the ASP.Net_SessionID is set in the Session HTTPModule (which 
        // has already run), thatwe can't use our own code to see if the cookie was 
        // actually sent by the agent with the request using the collection. Check if 
        // the given page supports session or not (this tested as reliable indicator 
        // if EnableSessionState is true), should not care about a page that does 
        // not need session
        if (Context.Session != null)
        {
            //Tested and the IsNewSession is more advanced then simply checking if 
            // a cookie is present, it does take into account a session timeout, because 
            // I tested a timeout and it did show as a new session
            if (Session.IsNewSession)
            {
                // If it says it is a new session, but an existing cookie exists, then it must 
                // have timed out (can't use the cookie collection because even on first 
                // request it already contains the cookie (request and response
                // seem to share the collection)
                string szCookieHeader = Request.Headers["Cookie"];
                if ((null != szCookieHeader) && (szCookieHeader.IndexOf("ASP.NET_SessionId") >= 0)
                    && (szCookieHeader.IndexOf("logintracker") >= 0))
                {
                    Logout();
                    if (WebConfig.ApplicationMode == "local")
                        Response.Redirect("/DevelopmentWebsite/log_in.aspx");
                    else
                        Response.Redirect("/log_in.aspx");
                }
            }
        }
    }

    private void Logout()
    {
        //code copy from logout.aspx. the coder must be very hyper about clearing session.
        Session.RemoveAll();
        UsersController _controller = new UsersController();
        StateManager stateManager = StateManager.Instance;
        _controller.DeleteSessionDetails(Session.SessionID);
        stateManager.Remove("objSessionvalue", StateManager.State.Session);
        Session.Clear();
        Session.Abandon();
        //StateManager objStateManager = StateManager.Instance;
        //objStateManager.Add("LoginChk", null, StateManager.State.Session);
        Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddYears(-30);
        Response.Cookies["ASP.NET_SessionId"].Domain = "." + WebConfig.TopLevelDomain;
        HttpCookie cooky = new HttpCookie("logintracker");
        cooky.Domain = "." + WebConfig.TopLevelDomain;
        cooky.Expires = DateTime.Now.AddYears(-1);
        Response.Cookies.Add(cooky);
    }

    private void PageBase_Error(object sender, EventArgs e)
    {
        
    }

    ///<summary>
    ///Redirect user to login page 
    ///</summary>
    protected void RedirectToLoginPage()
    {
        Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
    }

    /// <summary>
    /// This method will show error message in Validation summery control.
    /// </summary>
    /// <param name="strErrorMeassage">
    /// The message you wnat to show.
    ///</param>

    public void ShowMessage(string strErrorMeassage)
    {
        //ClientScript.RegisterHiddenField("ctl00_DefaultContent_ValidationSummary1", strErrorMeassage);
        string headertext = " <h2>Oops - there is a problem.</h2>                                                             <h3>Please correct the errors below:</h3>";
        ScriptManager.RegisterHiddenField(Page, "ctl00_DefaultContent_ValidationSummary1", strErrorMeassage);
        ClientScript.RegisterHiddenField("ctl00_DefaultContent_ValidationSummary1", strErrorMeassage);
        string fontString = @"if(typeof(Page_ValidationSummaries) != ""undefined"") ";
        fontString += @"{";
        fontString += @" var summary ;";
        fontString += @" var txtmsg='" + strErrorMeassage + "';";
        fontString += @" summary = Page_ValidationSummaries[0];";
        fontString += @"if( summary.style.display == ""none"" )";
        fontString += @"{";
        fontString += @"var s = """" ;";
        fontString += @"s = s + ""<ul>"" ;";
        fontString += @"s = s + ""  <li>"" ;";
        fontString += @"s = s + txtmsg ;";
        fontString += @"s = s + ""  </li>"" ;";
        fontString += @"s = s + ""</ul>"" ;";
        fontString += @"summary.style.display = """" ;";
        fontString += @" summary.innerHTML ='" + headertext + "'+ s ;";
        fontString += @"  summary.focus() ;";
        fontString += @"}";
        fontString += @"}";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ErrProcessor", fontString, true);

    }
    public string ShowMessage(string strErrorMeassage, int intMessage)
    {
        if (intMessage == 0)
        {
            string headertext = "<ul>";
            headertext += "<li>";
            headertext += strErrorMeassage;
            headertext += "</li>";
            return headertext += "</ul>";
        }
        else
        {
            string headertext = " <h2>Oops - there is a problem.</h2>                                                        <h3>Please correct the errors below:</h3>";
            headertext += "<ul>";
            headertext += "<li>";
            headertext += strErrorMeassage;
            headertext += "</li>";
            return headertext += "</ul>";
        }

    }

    public string ShowMessage(string headerText, string strErrorMeassage, int intMessage)
    {
        if (intMessage == 0)
        {
            string headertext = "<ul>";
            headertext += "<li>";
            headertext += strErrorMeassage;
            headertext += "</li>";
            return headertext += "</ul>";
        }
        else
        {
            string headertext = headerText + "<h3>Please correct the errors below:</h3>";
            headertext += "<ul>";
            headertext += "<li>";
            headertext += strErrorMeassage;
            headertext += "</li>";
            return headertext += "</ul>";
        }

    }

    public void ShowMessage(string headertext, string strErrorMeassage, bool msgtype)
    {
        //ClientScript.RegisterHiddenField("ctl00_DefaultContent_ValidationSummary1", strErrorMeassage);

        ScriptManager.RegisterHiddenField(Page, "ctl00_DefaultContent_ValidationSummary1", strErrorMeassage);
        ClientScript.RegisterHiddenField("ctl00_DefaultContent_ValidationSummary1", strErrorMeassage);
        string fontString = @"if(typeof(Page_ValidationSummaries) != ""undefined"") ";
        fontString += @"{";
        fontString += @" var summary ;";
        fontString += @" var txtmsg='" + strErrorMeassage + "';";
        fontString += @" summary = Page_ValidationSummaries[0];";
        fontString += @"if( summary.style.display == ""none"" )";
        fontString += @"{";
        fontString += @"var s = """" ;";
        fontString += @"s = s + ""<ul>"" ;";
        fontString += @"s = s + ""  <li>"" ;";
        fontString += @"s = s + txtmsg ;";
        fontString += @"s = s + ""  </li>"" ;";
        fontString += @"s = s + ""</ul>"" ;";
        fontString += @"summary.style.display = """" ;";
        if (msgtype.Equals(true))
            fontString += @" summary.innerHTML ='" + headertext + "'+ s ;";
        else
            fontString += @" summary.innerHTML = s ;";
        fontString += @"  summary.focus() ;";
        fontString += @"}";
        fontString += @"}";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ErrProcessor", fontString, true);

    }

    public void killFacebookCookies()
    {
        string ApplicationKey = ConfigurationManager.AppSettings["APIKey"];
        string[] fb_cookies = { "user", "session_key", "expires", "ss" };
        foreach (string cookieName in fb_cookies)
        {
            string fullCookieName = string.Format("{0}_{1}", ApplicationKey, cookieName);
            deleteCookie(fullCookieName);
        }
        deleteCookie(ApplicationKey);
    }

    private void deleteCookie(string cookieName)
    {
        if (HttpContext.Current != null
                && HttpContext.Current.Request != null
                && HttpContext.Current.Request.Cookies != null
                && HttpContext.Current.Request.Cookies[cookieName] != null)
        {

            HttpCookie cookie = new HttpCookie(cookieName, string.Empty);
            cookie.Expires = DateTime.Now.AddDays(-1);
            // cookie.Domain = "." + WebConfig.TopLevelDomain;
            Response.Cookies.Add(cookie);
        }
    }


    /// <summary>
    /// 
    /// ' Function Name     : IsPublicUserAuthorized
    ///' Purpose           : This method checks authentication for the Public users
    ///' Input             : userId as String
    ///' return output     : True if authorized user else False
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public bool IsUserAuthorized(string userId)
    {

        /* Disable client-side caching of web pages such that Browser Back/Forward button usage does
        ' not cause any issues*/
        bool continueFlag = false;

        DisableBrowserBack();
        if ((userId == "") || (userId == "0"))
        {

            Response.Redirect("home.aspx");
        }
        else
        {
            continueFlag = true;
        }



        return continueFlag;

    }

    public void DisableBrowserBack()
    {
        Response.Write("<script>history.forward();</script>");
    }


    /// <summary>
    /// This procedure Set focus to control passed to it.
    /// </summary>
    /// <param name="ctrl"></param>
    public void SetFocus(Control ctrl)
    {
        string focusString;

        if (ctrl.ClientID != null)
        {
            focusString = "<SCRIPT language='javascript'>document.getElementById('ctl00_ContentPlaceHolder1_" + ctrl.ID + "').focus() </SCRIPT>";
            RegisterStartupScript("focus", focusString);
        }
    }

    /// <summary>
    /// Method to check if user is Admin of Tribute
    /// </summary>
    /// <param name="objUserInfo">UserAdminOwnerInfo entity containing UserId and TributeId</param>
    /// <returns>True/False</returns>
    public bool IsUserAdmin(UserAdminOwnerInfo objUserInfo)
    {
        UserInfoManager objUser = new UserInfoManager();
        return bool.Parse(objUser.IsUserAdmin(objUserInfo).ToString());
    }

    /// <summary>
    /// Method to check if user is Owner of type(Photo, Video etc.)
    /// </summary>
    /// <param name="objUserInfo">UserAdminOwnerInfo entity containing UserId, TypeId and TypeName</param>
    /// <returns>True/False</returns>
    public bool IsUserOwner(UserAdminOwnerInfo objUserInfo)
    {
        UserInfoManager objUser = new UserInfoManager();
        return bool.Parse(objUser.IsUserOwner(objUserInfo).ToString());
    }

    /// <summary>
    /// Function to get the theme for selected tribute
    /// </summary>
    /// <returns>Theme Name</returns>
    private string GetExistingTheme()
    {
        Tributes objTribute = new Tributes();
        Tributes objTributeSession = null;
        //Templates objTheme = null;
        string themeName = string.Empty;
        //to get tribute id from session
        StateManager objStateManager = StateManager.Instance;
        objTributeSession = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);

        if (!Equals(objTributeSession, null))
        {
            objTribute.TributeId = objTributeSession.TributeId;
            TributeThemeManager objThemeManager = new TributeThemeManager();
            themeName = objThemeManager.GetThemeForTribute(objTribute).ThemeValue;
        }
        return themeName;
    }

    public string SetHeaderMessage(string message, string header)
    {
        string headertext = string.Empty;
        if (!(string.Equals(header, null)))
            headertext = header;

        headertext += "<ul>";
        headertext += "<li>";
        headertext += message;
        headertext += "</li>";
        headertext += "</ul>";
        return headertext;

    }

    #region << Storing Session Values >>
    protected void Page_Unload(object sender, EventArgs e)
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
    public void GetSessionKeyValues()
    {
        UsersController _controller1 = new UsersController();
        //List<SessionValue> _objSession = new List<SessionValue>();
        List<SessionValue> _objSession = _controller1.GetSessionValuesDetails(Session.SessionID);
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;

        SessionValue objVal = new SessionValue();


        if (_objSession.Count > 0)
        {
            foreach (SessionValue obj in _objSession)
            {
                if (obj.SessionKey == "UserId")
                    objVal.UserId = Convert.ToInt32(obj.SessionValues);

                if (obj.SessionKey == "UserName")
                    objVal.UserName = obj.SessionValues;

                if (obj.SessionKey == "Email")
                    objVal.UserEmail = obj.SessionValues;

                if (obj.SessionKey == "FirstName")
                    objVal.FirstName = obj.SessionValues;

                if (obj.SessionKey == "LastName")
                    objVal.LastName = obj.SessionValues;

                if (obj.SessionKey == "UserType")
                    objVal.UserType = Convert.ToInt32(obj.SessionValues);

                if (obj.SessionKey == "UserTypeDescription")
                    objVal.UserTypeDescription = obj.SessionValues;

                if (obj.SessionKey == "IsUsernameVisiable")
                    objVal.IsUsernameVisiable = Convert.ToBoolean(obj.SessionValues);

                // Added by Rupendra to get User image

                if (obj.SessionKey == "UserImage")
                    objVal.UserImage = obj.SessionValues;
            }

            stateManager.Add("objSessionValue", objVal, StateManager.State.Session);
        }
    }
    #endregion << Storing Session Values >>
}
