/* Made By Ashu(24th June,2011) */

using System;
using System.Text;
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
using System.Xml.Linq;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.BusinessLogic;
using Facebook;
using Facebook.Web;
using System.Collections.Generic;

public partial class UserControl_GiveGift : System.Web.UI.UserControl
{
    public string _tabName;
    public string _upgradeUrl = string.Empty;
    public string _tributeUrl;
    public string location = string.Empty;
    FacebookWebContext fbWebContext;
    private Nullable<Int64> _FacebookUid;

    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(UserControl_GiveGift));
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            Label2.Text = "Create a Your Moments account to get the most out of this website.";
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
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void FacebookLogin()
    {
        fbWebContext = FacebookWebContext.Current; //get Facebook Session
        if (FacebookWebContext.Current.Session != null)
        {            
            var app = new FacebookWebClient();
            var me = (IDictionary<string, object>)app.Get("me");   // get own information
            _FacebookUid = fbWebContext.UserId;                // get own user id
            try
            {
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
                    //Notice.InnerHtml = notice;
                    //Notice.Visible = true;
                    //lbtnLogin.Text = "Connect";

                }
            }
            catch (Exception ex)
            {
                
            }
        }         
    }



    protected void lbtnGift_Click(object sender, EventArgs e)
    {
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        Tributes objVal = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
        if (objVal.TypeDescription != null)
        {
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                location = WebConfig.AppBaseDomain + objVal.TributeUrl + "/"  + "Gift.aspx" + "?PageNo=1&gift_without_login=true";
            }
            else
            {
                
                    if (objVal.TypeDescription.Equals("New Baby"))
                        location = "http://newbaby." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl + "/" + "Gift.aspx" + "?PageNo=1&gift_without_login=true";
                    else if (objVal.TypeDescription.Equals("Wedding"))
                        location = "http://wedding." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl + "/" + "Gift.aspx" + "?PageNo=1&gift_without_login=true";
                    else if (objVal.TypeDescription.Equals("Memorial"))
                        location = "http://memorial." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl + "/" + "Gift.aspx" + "?PageNo=1&gift_without_login=true";
                    else if (objVal.TypeDescription.Equals("Graduation"))
                        location = "http://graduation." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl + "/" + "Gift.aspx" + "?PageNo=1&gift_without_login=true";
                    else if (objVal.TypeDescription.Equals("Anniversary"))
                        location = "http://anniversary." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl + "/" + "Gift.aspx" + "?PageNo=1&gift_without_login=true";
                    else if (objVal.TypeDescription.Equals("Birthday"))
                        location = "http://birthday." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl + "/" + "Gift.aspx" + "?PageNo=1&gift_without_login=true";
                    else
                        location = "http://" + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl + "/" + "Gift.aspx" + "?PageNo=1&gift_without_login=true";
                }
            }
        
        StringBuilder sb = new StringBuilder();
        sb.Append("parent.setLocation('" + location + "');");
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "HidePanel", sb.ToString(), true);                

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
    public string ApplicationType
    {
        get { return ConfigurationManager.AppSettings["ApplicationType"].ToString(); }
    }
}
