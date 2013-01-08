///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Myhome.AdminProfilePrivacy.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays and allows the user to modify the privacy settings for his/her account.
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
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.MyHome.Views;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using System.IO;
using System.Collections.Generic;

public partial class MyHome_AdminProfilePrivacy : PageBase, IAdminProfilePrivacy
{
    #region private variables
    private AdminProfilePrivacyPresenter _presenter;
    int UserID;
    string bannerMessage = string.Empty;
    //protected string _userName;
    #endregion private variables

    #region Public methods
    [CreateNew]
    public AdminProfilePrivacyPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    #endregion Public methods


    protected void Page_Load(object sender, EventArgs e)
    {
        // Code to implement SSL Functionality.
        //if (!WebConfig.ApplicationMode.Equals("local"))
        //if (Request.Url.ToString().Contains(@"http://"))
        //    Response.Redirect(@"https://www." + WebConfig.TopLevelDomain + @"/adminprofileprivacy.aspx");
        this.Master.NavPrimary = Shared_InnerSecure.AdminNavPrimaryEnum.myprofile.ToString();
        this.Master.NavSecondary = Shared_InnerSecure.AdminNavSecondaryEnum.privacy.ToString();
        this.Master.PageTitle = "Privacy Settings";

        if (!this.IsPostBack)
        {
            StateManager stateManagerP = StateManager.Instance;
            string PageName = "AdminProfilePrivacy";
            stateManagerP.Add(PortalEnums.SessionValueEnum.SearchPageName.ToString(), PageName, StateManager.State.Session);

            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                UserID = objSessionvalue.UserId;

                this._presenter.GetUserPrivacy();
                btnSaveChanges.Attributes.Add("onclick", "HideIndicator();");
                setDefault();
            }
            else
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }
        }  
    }

    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        try
        {
            this._presenter.UpdatePrivacySettings();
            setmessage("<h2>Privacy Settings Updated</h2><P>Your privacy setting updated successfully.</P>", 2);            
        }
        catch(Exception ex)
        {
            setmessage("<h2>Oops - there is a problem with profile privacy.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + "</li></ul>", 1);
        }
    }

    private void setmessage(string msg, int type)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");

        if (type == 1)
            errormsg.Attributes.Add("class", "yt-Error");
        else
            errormsg.Attributes.Add("class", "yt-Notice");

        errormsg.InnerHtml = msg;
        errormsg.Visible = true;
    }

    private void setDefault()
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        errormsg.InnerHtml = "";
        errormsg.Visible = false;
    }

    #region IAdminProfilePrivacy Members

    public string BannerMessage
    {
        get
        {
            return bannerMessage;
        }
        set
        {
            bannerMessage = value;
        }
    }
    public int UserId
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
                UserID = objSessionvalue.UserId;
            return UserID;
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }


    #region Privacy Settings
    public bool IsUsernameVisiable
    {
        get
        {
            if (rdbDisplayMyUsername.Checked == true)
            {
                return rdbDisplayMyUsername.Checked;
            }
            else
            {
                return false;
            }
        }
        set
        {
            rdbDisplayMyUsername.Checked = value;
            rdbDisplayMyFullName.Checked = !value;
        }
    }

    public bool IsLocationHide
    {
        get
        {
            if (rdbHideMyLocation.Checked == true)
            {
                return rdbHideMyLocation.Checked;
            }
            else
            {
                return false;
            }
        }
        set
        {

            rdbHideMyLocation.Checked = value;
            rdbDisplayMyLocation.Checked = !value;

        }
    }

    private string LoggedInUserName
    {
        set
        {
            ViewState["LoggedInUserName"] = value;
        }
        get
        {
            if (ViewState["LoggedInUserName"] == null)
                return null;
            else
                return ViewState["LoggedInUserName"].ToString();
        }
    }
    public bool AllowIncomingMsg
    {
        get
        {
            if (rdbAllowUsers.Checked == true)
            {
                return rdbAllowUsers.Checked;
            }
            else
            {
                return false;
            }
        }
        set
        {
            rdbAllowUsers.Checked = value;
            rdbDoNotAllow.Checked = !value;
        }
    }
    public bool IsVisitCountHide
    {
        get
        {
            if (rdbDisplayVisitCount.Checked == true)
            {
                return rdbDisplayVisitCount.Checked;
            }
            else
            {
                return false;
            }
        }
        set
        {
            rdbDisplayVisitCount.Checked = value;
            rdbHideVisitCount.Checked = !value;
        }
    }
    #endregion Privacy Settings

    #endregion
}
