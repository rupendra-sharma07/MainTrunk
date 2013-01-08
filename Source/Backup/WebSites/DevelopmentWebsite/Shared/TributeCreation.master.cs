///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Shared.TributeCreation.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page provides the default layout for the tribute creation pages
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Web.UI;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;
using Facebook.Web;

public partial class Shared_TributeCreation : System.Web.UI.MasterPage
{
    #region CLASS VARIABLES

    protected string _UserName;
//    private int _UserID;
 //   private ConnectSession _connectSession;
//    private FacebookClient fbclient;
    private FacebookWebContext fbWebContext;
    #endregion


    public string CreditLinkButton
    {
        get
        {
            return string.Empty;
        }
        set
        {           
            ytHeader.CreditLink = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //_connectSession = new ConnectSession(ConfigurationManager.AppSettings["APIKey"], ConfigurationManager.AppSettings["Secret"]);
        //fbclient = new FacebookClient(ConfigurationManager.AppSettings["APIKey"], ConfigurationManager.AppSettings["Secret"]);
        fbWebContext = FacebookWebContext.Current;  
        if (!IsPostBack)
        {
            StateManager objStateManager = StateManager.Instance;
            SessionValue objSessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);
            //to get user id from session as user is logged in user
            objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
            if (!Equals(objSessionValue, null))
            {
                if (Request.QueryString["Type"] != null)
                {
                    ytHeader.TributeType = Request.QueryString["Type"].ToString();
                    int NetCreditPoints;
                    StateManager stateManager = StateManager.Instance;
                    SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
                    UserRegistration _objUserReg = new UserRegistration();
                    TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
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
                    ytHeader.CreditLink = "Credits(" + NetCreditPoints.ToString() + ")";
                }

            }
            else
            {
                string Querystring = "?PageName=" + "TributeCreation";
                //AG(25-Nov-2010): Added for tribute creation from video tribute display page

                if (Request.QueryString["Type"] != null)
                {
                    Querystring += "&Type=" + Request.QueryString["Type"].ToString();
                } 
                if (Request.QueryString["VideoTributeId"] != null)
                {
                    Querystring += "&VideoTributeId=" + Request.QueryString["VideoTributeId"].ToString();
                }
                //LHK(12-Jan-2011):added for tribute creation from pricing and signup page
                if (Request.QueryString["AccountType"] != null)
                {
                    Querystring += "&AccountType=" + Request.QueryString["AccountType"].ToString();
                }

                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()) + Querystring, false);
            }

            //StateManager objStateManager = StateManager.Instance;

            //to get logged in user name from session as user is logged in user
            //SessionValue objSessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);

            //if (!Equals(objSessionValue, null))
            //{                
            //    if (_connectSession.IsConnected())
            //    {
            //        try
            //        {
            //            Facebook.Rest.Api api = new Facebook.Rest.Api(_connectSession);                        //Display user data captured from the Facebook API.  
            //            Facebook.Schema.user user = api.Users.GetInfo();
            //            if (objSessionValue.UserName == null ||
            //                string.Empty.Equals(objSessionValue.UserName))
            //            {
            //                objSessionValue.UserName = user.first_name + " " + user.last_name;
            //            }
            //            spanLogout.InnerHtml = "<a href=\"#\" onclick=\"FB.Connect.logoutAndRedirect('" + Session["APP_BASE_DOMAIN"] + "Logout.aspx')\">" +
            //              "   <img id=\"fb_logout_image\" src=\"http://static.ak.fbcdn.net/images/fbconnect/logout-buttons/logout_small.gif\" alt=\"Connect\"/>" +
            //              "</a>";
            //        }
            //        catch (Exception ex)
            //        {
            //            //killFacebookCookies();
            //            //ShowMessage(ex.ToString() + "Your Facebook session has timed out. Please clear private data in browser and log in again");
            //        }
            //    }
            //    else
            //    {
            //        spanLogout.InnerHtml = "<a href='Logout.aspx'>Log out</a>";
            //    }
            //    _UserID = objSessionValue.UserId;
            //    _UserName = objSessionValue.UserName;
            //}
        }
    }

    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.ClearHeaders();
        Response.ExpiresAbsolute = DateTime.Now.AddHours(-1);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AddHeader("Pragma", "no-cache");

        //        ClientScriptManager.RegisterStartupScript(this.GetType(), "DisableBack", strDisableBackButton);


        //Response.ClearHeaders();
        //Response.ExpiresAbsolute = DateTime.Now.AddHours(-5);
        //Response.Cache.SetExpires(DateTime.Now.AddDays(-5));
        //Response.CacheControl = "no-cache";
        //Response.AddHeader("Pragma", "no-cache");
        //Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        //string strDisableBackButton = "window.history.forward(1)";
        string strDisableBackButton = "window.history.go(1)";

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "DisableBack", strDisableBackButton, true);
        //Response.Redirect("../Users/log_in.aspx");
        Response.Redirect(Session["APP_BASE_DOMAIN"] + "logout.aspx",false);
    }

}
