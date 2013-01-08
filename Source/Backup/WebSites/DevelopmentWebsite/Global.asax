<%@ Application Language="C#" Inherits="Microsoft.Practices.CompositeWeb.WebClientApplication" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="TributesPortal.MultipleLangSupport" %>
<%@ Import Namespace="TributesPortal.Utilities" %>
<%@ Import Namespace="TributesPortal.BusinessEntities" %>
<%@ Import Namespace="TributesPortal.BusinessLogic" %>
<%@ Import Namespace="TributesPortal.Users" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="TributesPortal.ResourceAccess" %>
<%@ Import Namespace="System.Reflection" %>

<script RunAt="server">    
  
    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-EN");
        
              
        
        //other values that can be used
        //the culture name can be stored in the Web.config file and retrieved from there...
        //fr-FR
        //en-EN
        //ro-RO
        
        // check the query string and cookies agains XSS and SQL Injection
        // this doesn't work on the live web site
        //HttpRequest Request = (sender as HttpApplication).Context.Request;
        //string strErrorMessage = string.Empty;

        //foreach (string key in Request.QueryString)
        //{
        //    if (!TributesPortal.ResourceAccess.IOVS.Sanitise(Request.QueryString[key], ref strErrorMessage))
        //    {
        //        throw new IovsException(strErrorMessage);
        //    }
        //}

        //foreach (string key in Request.Cookies)
        //{
        //    if (!TributesPortal.ResourceAccess.IOVS.Sanitise(Request.Cookies[key].Value, ref strErrorMessage))
        //    {
        //        throw new IovsException(strErrorMessage);
        //    }
        //}
    }

    protected void Application_EndRequest(Object sender, EventArgs e)
    {

    }

    void Application_End(object sender, EventArgs e)
    {
        Session.Abandon();
    }

    void Application_Error(object sender, EventArgs e)
    {
       try
       {
           if ((Server.GetLastError().InnerException is IovsException) || 
               (HttpContext.Current != null && HttpContext.Current.Server.GetLastError() is IovsException))
        {
                Response.Redirect("~/Errors/IovsErrorPage.htm");
        }
           
        //Logging Entry
        if ((Server.GetLastError().InnerException is IovsException) || (HttpContext.Current.Server.GetLastError() is IovsException))
        {
            ErrorsHandling.Error(MethodBase.GetCurrentMethod(), Server.GetLastError(), Server.GetLastError().InnerException, 
                "\r\n----------------------------------------------" + Request.Url.AbsoluteUri);
        }
       }
       catch { ; }
           
    }

    void Session_Start(object sender, EventArgs e)
    {
        
        string strSessionMode = "";
        string strAppPath = "";
        string strBaseDomain = "";
        string strScriptpath = "";
        strSessionMode = WebConfig.ApplicationMode.ToLower();

        if (strSessionMode == "local")
        {
            /*
            strAppPath = "http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.Url.Port.ToString() + "/DevelopmentWebsite/";
            strBaseDomain = "http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.Url.Port.ToString() + "/DevelopmentWebsite/";
            strScriptpath = "http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.Url.Port.ToString() + "/DevelopmentWebsite/";
             */
            strAppPath = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"].ToString();
            strBaseDomain = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"].ToString();
            strScriptpath = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"].ToString();
        }
        else
        {
            strAppPath = "http://" + Request.ServerVariables["SERVER_NAME"] + "/";
            strBaseDomain = "http://www." + WebConfig.TopLevelDomain + "/";
            strScriptpath = "../";
        }
        Session.Add("APP_PATH", strAppPath);
        Session.Add("APP_BASE_DOMAIN", strBaseDomain);
        Session.Add("APP_SCRIPT_PATH", strScriptpath);

        //if (strSessionMode != "local")
        //    GetSessionKeyValues(); //COMDIFFRES: why is this uncommented on .com

        //SESSION_TIMEOUT_ISSUE
        ////set this to indicate anonymous user
        //HttpCookie cooky = new HttpCookie("logintracker");
        //cooky.Domain = "." + WebConfig.TopLevelDomain;
        //cooky.Expires = DateTime.Now.AddYears(-1);
        //Response.Cookies.Add(cooky);
        //UrlRedirection();
    }

    public void GetSessionKeyValues()
    {
        UsersController _controller1 = new UsersController();
        //List<SessionValue> _objSession = new List<SessionValue>();
        List<SessionValue> _objSession = _controller1.GetSessionValuesDetails(Session.SessionID);
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;

        SessionValue objVal = new SessionValue();
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
        }
        stateManager.Add("objSessionValue", objVal, StateManager.State.Session);
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }
    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        if (HttpContext.Current.User != null)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.Identity is FormsIdentity)
                {
                    FormsIdentity id =
                        (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;

                    // Get the stored user-data, in this case, our roles
                    string userData = ticket.UserData;
                    string[] roles = userData.Split(',');
                    HttpContext.Current.User = new GenericPrincipal(id, roles);
                }
            }
        }
    }
//    #region <<redirection>>
//    protected void UrlRedirection()
//{
//        var sUrl = HttpContext.Current.Request.Url.ToString().ToLower();
//        string[] urlArr = sUrl.Split(".".ToCharArray());
//        string[] sUrlType = urlArr[urlArr.Length - 1].Contains("aspx") ? urlArr[urlArr.Length - 2].Split("/".ToCharArray()) : urlArr[urlArr.Length - 1].Split("/".ToCharArray());
//        //string[] sUrlType = urlArr[urlArr.Length - 2].Split("/".ToCharArray());
//        if (sUrl.Contains("memorial.yourtribute") && sUrlType[0].Contains("in") && sUrlType.Length<=2)
//        {
//            Response.Redirect("http://yourtribute.in/home.aspx");
//        }
//        else if (sUrl.Contains("memorial.yourtribute") && sUrlType[0].Contains("com") && sUrlType.Length <= 2)
//        {
//            Response.Redirect("http://yourtribute.com/home.aspx");
//        }
//    }
//    #endregion
       
</script>

