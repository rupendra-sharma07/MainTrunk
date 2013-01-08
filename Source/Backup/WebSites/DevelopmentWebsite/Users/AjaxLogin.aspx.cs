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
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.Miscellaneous;
using TributesPortal.Utilities;
using TributesPortal.Users;
using Facebook;
using Facebook.Web;
using System.Collections.Generic;
using TributesPortal.BusinessLogic;

/*
 * Handles the login via facebook connect Ajaxy with json.
 * 
 * 
 */

public partial class Users_AjaxLogin : System.Web.UI.Page
{
    
    private Nullable<Int64> _FacebookUid;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AddHeader("Pragma", "no-cache");

        string _action = string.Empty;
        if (Request.QueryString["action"] != null)
        {
            _action = Request.QueryString["action"].ToString();
        }

        FacebookWebClient fbclient = new FacebookWebClient(ConfigurationManager.AppSettings["APIKey"], ConfigurationManager.AppSettings["Secret"]);
        var fbWebContext = FacebookWebContext.Current;
        if (fbWebContext.Session != null) // check session
        {
            _FacebookUid = fbWebContext.UserId;
            try
            {
                if(_action.Equals("facebookLogin")) 
                {
                    doFacebookLogin();
                } else if(_action.Equals("facebookSignup"))
                {
                    doFacebookSignup();
                }
                else if (_action.Equals("facebookConnect"))
                {
                    doFacebookConnect();
                }
                else if (_action.Equals("facebookDisconnect"))
                {
                    doFacebookDisconnect();
                } 
            }
            catch (Exception ex)
            {
                messageText.Text = "<h2>An error had occurred.</h2><h3>Please  <a href=\"#\" onclick=\"fb_err_logout(); return false;\">" +
                          "   <img id=\"fb_logout_image\" src=\"http://static.ak.fbcdn.net/images/fbconnect/logout-buttons/logout_small.gif\" alt=\"Connect\"/>" +
                          "</a> and try again.</h3>"+ex.ToString();
            }
        }
        
        else
        {
            messageText.Text = "<h2>You are not Facebook Connected.</h2>";
        }
    }

    private void doFacebookConnect()
    {
        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
        if (objSessionvalue == null)
        {
            messageText.Text = "Your Tribute session had expired. Please log in.";
            refreshPage.Text = Request.QueryString["source"].ToString().Equals("headerLogin") 
                ? "false" : "true";
        }
        else
        {
            UserRegistration objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = objSessionvalue.UserId;
            objUsers.FacebookUid = _FacebookUid;
            objUserReg.Users = objUsers;

            UserInfoManager umgr = new UserInfoManager();
            umgr.UpdateFacebookAssociation(objUserReg);
            StringBuilder sbr = new StringBuilder();
            if (objUserReg.CustomError != null)
            {

                var fbwc = new FacebookWebClient(FacebookWebContext.Current.AccessToken);
                var me = (IDictionary<string, object>)fbwc.Get("me");
                string fbName = (string)me["first_name"] + " " + (string)me["last_name"];

                sbr.Append(string.Format("<div class=\"yt-Error\"><h3>Sorry, {0}.</h3>", fbName));
                sbr.Append(string.Format(
                    "The Facebook account named {0} is aleady used by some other Your Tribute Account.", 
                    fbName));
                sbr.Append("Would you like to:");
                sbr.Append("<ul>");
                sbr.Append("<li><a href=\"#\" onclick=\"fb_logout(); return false;\">");
                sbr.Append("   <img id=\"fb_logout_image\" ");
                sbr.Append("src=\"http://static.ak.fbcdn.net/images/fbconnect/logout-buttons/logout_small.gif\"");
                sbr.Append(" alt=\"Logout from Facebook and Your Tribute\"/></a> of both Your Tribute and Facebook Connect ");
                sbr.Append("and switch to the other account using your Facebook email address and password.</li>");
                sbr.Append("<li><a href=\"#\" onclick=\"fb_err_logout(); return false;\">");
                sbr.Append("<img id=\"fb_logout_image\" ");
                sbr.Append("src=\"http://static.ak.fbcdn.net/images/fbconnect/logout-buttons/logout_small.gif\"");
                sbr.Append(" alt=\"Disconnect from Facebook\"/></a> of Facebook Connect and try again.</li>");
                sbr.Append("<li><a href=\"javascript:void(0);\" onclick=\"OpenContactUS();\">Contact us</a> to discuss the situation further if you are confused.</li>");
                sbr.Append("</div>");

                messageText.Text = sbr.ToString();
                refreshPage.Text = "false";
            }
            else
            {
                if(Request.QueryString["source"].ToString().Equals("headerLogin"))
                {
                  refreshPage.Text = "true";
                } else {
                    messageText.Text = "<div class=\"yt-Notice\">Facebook Connection was added to your account.</div>";
                    refreshPage.Text = "false";
                }
            }
        }
    }

    private void doFacebookDisconnect()
    {
        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
        StringBuilder sbr = new StringBuilder();
        if (objSessionvalue == null)
        {
            messageText.Text = "Your Tribute session had expired. Please log in.";
            refreshPage.Text = Request.QueryString["source"].ToString().Equals("headerLogin")
               ? "false" : "true";
        }
        else
        {
            UserRegistration objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = objSessionvalue.UserId;
            objUserReg.Users = objUsers;

            UserInfoManager umgr = new UserInfoManager();
            umgr.RemoveFacebookAssociation(objUserReg);
            HttpContext.Current.Session.Clear();
            if (string.IsNullOrEmpty(objSessionvalue.UserEmail))
            {
                sbr.Append("<div class=\"yt-Error\"><h3>Urgent: You must <a href=\"");
                sbr.Append(Session["APP_BASE_DOMAIN"].ToString());
                sbr.Append("adminprofileemailpassword.aspx\">set up an email address and password</a>!</h3>");
                sbr.Append("Your account was disconnected from Facebook, but you do not have an ");
                sbr.Append("email address and password on file. If you do not create a password ");
                sbr.Append("then you will not be able to login later.</div>");
                messageText.Text = sbr.ToString();
            }
            else
            {
                messageText.Text = "<div class=\"yt-Notice\">Facebook was disconnected from your account.</div>";
            }
        }
    }

    private void doFacebookLogin()
    {
        var fbwc = new FacebookWebClient(FacebookWebContext.Current.AccessToken);
        var me = (IDictionary<string, object>)fbwc.Get("me");
        string fbName = (string)me["first_name"] + " " + (string)me["last_name"];
        if (OnFacebookLogin(fbName))
        {
            SaveSessionInDB();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", Session.SessionID));
            Response.Cookies["ASP.NET_SessionId"].Domain = "." + WebConfig.TopLevelDomain;

            showDialog.Text = "false";
            refreshPage.Text = "true";
        }
        else
        {
            //user may not have account with this Facebook connection
            showDialog.Text = "true";
            refreshPage.Text = "false";
        }
    }

    protected void doFacebookSignup()
    {

        var fbwc = new FacebookWebClient(FacebookWebContext.Current.AccessToken);
        var me = (IDictionary<string, object>)fbwc.Get("me");
        string fbName = (string)me["first_name"] + " " + (string)me["last_name"];
        string UserName = fbName.ToLower().Replace(" ", "_").Replace("'", "");/*+
            "_"+_FacebookUid.ToString()*/
        
        string fql = "Select current_location,pic_square,email from user where uid = " + (string)me["id"];
        JsonArray me2 = (JsonArray)fbwc.Query(fql);
        var mm = (IDictionary<string, object>)me2[0];

        Nullable<int> state = null;
        Nullable<int> country = null;
        string _UserImage = "images/bg_ProfilePhoto.gif";

        if (!string.IsNullOrEmpty((string)mm["pic_square"]))
        {
            _UserImage = (string)mm["pic_square"]; // get user image
        }
        string city = "";
        JsonObject hl = (JsonObject)mm["current_location"];
        if ((string)hl[0] != null)
        {
            city = (string)hl[0];
            UserManager usrmngr = new UserManager();
            object[] param = { (string)hl[2],(string)hl[1] };
            if (usrmngr.GetstateIdByName(param) > 0)
            {
                state = usrmngr.GetstateIdByName(param);
            }
            if (usrmngr.GetCountryIdByName((string)hl[2]) > 0)
            {
                country = usrmngr.GetCountryIdByName((string)hl[2]);
            }

        }


        string password_ = string.Empty;
        string email_ = string.Empty;  //user.proxied_email;
        string result = (string)mm["email"];
        if (!string.IsNullOrEmpty(result))
        {
            email_ = result;
            password_ = RandomPassword.Generate(8, 10);
            password_ = TributePortalSecurity.Security.EncryptSymmetric(password_);

        }
        UserRegistration objUserReg = new UserRegistration();
        TributesPortal.BusinessEntities.Users objUsers =
            new TributesPortal.BusinessEntities.Users(
             UserName, password_,
             (string)me["first_name"], (string)me["last_name"], email_, 
             "", false,
             city, state, country, 1, _FacebookUid);
        objUsers.UserImage = _UserImage;
        objUserReg.Users = objUsers;

        /*System.Decimal identity = (System.Decimal)*/
        UserInfoManager umgr = new UserInfoManager();
        umgr.SavePersonalAccount(objUserReg);

        if (objUserReg.CustomError != null)
        {
            messageText.Text=string.Format("<h2>Sorry, {0}.</h2>" +
                "<h3>Those Facebook credentials are already used in some other Your Tribute Account</h3>", 
                fbName);
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
            
            showDialog.Text = "false";
            refreshPage.Text = "true";
        }
    }

    private bool OnFacebookLogin(string fbName)
    {
        GenralUserInfo _objGenralUserInfo = new GenralUserInfo();
        UserInfo objUserInfo = new UserInfo();
        objUserInfo.FacebookUid = _FacebookUid;
        objUserInfo.ApplicationType = ApplicationType;
        _objGenralUserInfo.RecentUsers = objUserInfo;
        if (objUserInfo.FacebookUid == null) return false;

        UserInfoManager umgr = new UserInfoManager();
        umgr.CheckFacebookAccountAvailability(_objGenralUserInfo);
        if (_objGenralUserInfo.RecentUsers.UserID != null && _objGenralUserInfo.RecentUsers.UserID > 0)
        {
            SetSessionValue(_objGenralUserInfo);
            return true;
        }
        return false;
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
                                                               );
        TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;
        stateManager.Add("objSessionvalue", _objSessionValue, StateManager.State.Session);
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
    public string ApplicationType
    {
        get{return  ConfigurationManager.AppSettings["ApplicationType"].ToString();}
    }
}
