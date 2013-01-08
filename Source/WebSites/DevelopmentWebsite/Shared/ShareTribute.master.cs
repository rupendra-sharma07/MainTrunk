///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Shared.GuestBook.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page is used as a general purpose master pages
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

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
using TributesPortal.Users;
using TributesPortal.Utilities;
using TributesPortal.Miscellaneous;
using TributesPortal.MultipleLangSupport;
using TributesPortal.ResourceAccess;
//using Facebook;
//using Facebook.Web;
//using Facebook.Session;
#endregion


public partial class Shared_ShareTribute : System.Web.UI.MasterPage
{

    #region CLASS VARIABLES

    protected string _userName;
    private int _userId;
    public int _packageId;
   // private string _typeName;    // commented by Ud for warnings 
    protected int _tributeId;
    protected string _tributeType;
    protected string _tributeName;
    protected string _tributeUrl;
    protected bool _isActive;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    private bool IsCustomHeaderOn = false;
    #endregion


    #region EVENTS
    protected void Page_Init(object sender, EventArgs e)
    {
        
        StateManager objStateManager = StateManager.Instance;
        MiscellaneousController objMisc = new MiscellaneousController();
        objTribute = new Tributes();

        if ((Request.QueryString["TributeUrl"] != null) && (Request.QueryString["TributeType"] != null))
        {
            objTribute.TributeUrl = Request.QueryString["TributeUrl"].ToString();
            objTribute.TypeDescription = Request.QueryString["TributeType"].ToString();
            objStateManager.Add("TributeSession", objMisc.GetTributeSessionForUrlAndType(objTribute,WebConfig.ApplicationType.ToString()), TributesPortal.Utilities.StateManager.State.Session);
        }
        else if (Request.QueryString["TributeUrl"] != null)
        {
            objTribute.TributeUrl = Request.QueryString["TributeUrl"].ToString();
            objTribute.TypeDescription = null;
            objStateManager.Add("TributeSession", objMisc.GetTributeSessionForUrlAndType(objTribute,WebConfig.ApplicationType.ToString()), TributesPortal.Utilities.StateManager.State.Session);
        }

        TributePackage objpackage = new TributePackage();
        objpackage.UserTributeId = objTribute.TributeId;
        object[] param = { objpackage };
        objMisc.TriputePackageInfo(param);
        if (objpackage.CustomError == null)
            _packageId = objpackage.PackageId;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //LHK:EmptyDivAboveMainPanel
        StateManager stateTribute = StateManager.Instance;
        SessionValue objSessvalue = (SessionValue)stateTribute.Get("objSessionvalue", StateManager.State.Session);

        if ((Request.QueryString["TributeUrl"] != null))
        {
            string _trbUrl = Request.QueryString["TributeUrl"].ToString();
            GetCustomHeaderVisible(_trbUrl, WebConfig.ApplicationType.ToString());
        }
        if (!(objSessvalue != null))
        {
            if (!IsCustomHeaderOn)
            {
                EmptyDivAboveMainPanel.Visible = true;
            }
        }
        //LHK:EmptyDivAboveMainPanel
        try
        {
            // get the Tribute and User detail from the Session
            GetValuesFromSession();

            //LHK; events tab not selected issue -22/11/2011
            if (Page.GetType().Name.ToLower() == "event_inviteguest_aspx")
                liEvents.Attributes.Add("class", "yt-Events selected");

            //to load existing theme.
            string appPath = string.Empty;
            if (WebConfig.ApplicationMode.ToLower().Equals("local"))
            {
                appPath = WebConfig.AppBaseDomain;
            }
            else
            {
                appPath = string.Format("{0}{1}{2}", "http://www.", WebConfig.TopLevelDomain, "/");
            }
            idSheet.Href = appPath + "assets/themes/" + GetExistingTheme().FolderName + "/theme.css";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnMyProfile_Click(object sender, EventArgs e)
    {

    }

    protected void popuplbtnSendemail_Click(object sender, EventArgs e)
    {
        try
        {
            GenralUserInfo objGenralUserInfo = new GenralUserInfo();
            UserInfo objUserInfo = new UserInfo();
            objUserInfo.UserEmail = txtLoginEmail1.Text;
            objGenralUserInfo.RecentUsers = objUserInfo;
            UsersController objUsersController = new UsersController();
            objUsersController.CheckAndSendPassword(objGenralUserInfo, false);
            txtLoginEmail1.Text = string.Empty;
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion


    #region METHODS


    /// <summary>
    /// This function will get the values (User Id and Tribute Detail) from the session
    /// </summary>
    private void GetValuesFromSession()
    {
        try
        {
            StateManager objStateManager = StateManager.Instance;

            //to get logged in user name from session as user is logged in user
            objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);

            objTribute = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);
            if (Request.QueryString["fbmode"] != null)
            {
                if (Request.QueryString["fbmode"] == "facebook")
                {
                    _tributeId = int.Parse(Request.QueryString["TributeId"].ToString());
                    _tributeType = Request.QueryString["TributeType"].ToString();
                    _tributeName = Request.QueryString["TributeName"].ToString();
                    _tributeUrl = Request.QueryString["TributeUrl"].ToString();
                }
                else
                    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
            }
            else if (!Equals(objTribute, null))
            {
                _tributeId = objTribute.TributeId;
                _tributeType = objTribute.TypeDescription;
                _tributeName = objTribute.TributeName;
                _tributeUrl = objTribute.TributeUrl;
                _isActive = objTribute.IsActive;
            }
            //else
            //{
            //    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
            //}

            //to display login and logout option based on the Session value for the user.
            if (!Equals(objSessionValue, null))
            {
                _userId = objSessionValue.UserId;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Method to create the tribute session values if user comes o this page from link or from favorites list.
    /// </summary>
    private void CreateTributeSession()
    {
        Tributes objTribute = new Tributes();
        objTribute.TributeId = _tributeId;
        objTribute.TributeName = _tributeName;
        objTribute.TypeDescription = _tributeType;
        objTribute.TributeUrl = _tributeUrl;
        objTribute.IsActive = _isActive;
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add("TributeSession", objTribute, TributesPortal.Utilities.StateManager.State.Session);
    }

    /// <summary>
    /// Method to get the theme for tribute
    /// </summary>
    public Templates GetExistingTheme()
    {
        Tributes objTribute = new Tributes();
        MiscellaneousController _controller = new MiscellaneousController();
        objTribute.TributeId = _tributeId;
        return _controller.GetThemeFolderForTribute(objTribute);
    }
    private void GetCustomHeaderVisible(string _tributeUrl, string ApplicationType)
    {
        IsCustomHeaderOn = false;
        VideoResource objVideoResource = new VideoResource();
        IsCustomHeaderOn = objVideoResource.GetCustomHeaderVisible(_tributeUrl, ApplicationType);
    }

    #endregion

}