///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Myhome.AdminProfileEmailpassword.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to modify his/her email id and password.
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

public partial class MyHome_AdminProfileEmailpassword : PageBase, IAdminProfileEmailpassword
{
    #region private variables
    private AdminProfileEmailpasswordPresenter _presenter;
    int UserID;
    string bannerMessage = string.Empty;
    #endregion private variables

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

    protected void Page_Load(object sender, EventArgs e)
    {
        // Code to implement SSL Functionality.
        //if (!WebConfig.ApplicationMode.Equals("local"))
        //if (Request.Url.ToString().Contains(@"http://"))
        //    Response.Redirect(@"https://www." + WebConfig.TopLevelDomain + @"/adminprofileemailpassword.aspx");
        this.Master.NavPrimary = Shared_InnerSecure.AdminNavPrimaryEnum.myprofile.ToString();
        this.Master.NavSecondary = Shared_InnerSecure.AdminNavSecondaryEnum.emailpassword.ToString();
        this.Master.PageTitle = "Change Email/Password";

        if (!this.IsPostBack)
        {
            //lnkAdvanceSearch.NavigateUrl = Session["APP_BASE_DOMAIN"] + "advancedsearch.aspx";
            StateManager stateManagerP = StateManager.Instance;
            string PageName = "AdminProfileEmailpassword";
            stateManagerP.Add(PortalEnums.SessionValueEnum.SearchPageName.ToString(), PageName, StateManager.State.Session);

            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                UserID = objSessionvalue.UserId;
                LoggedInUserName = objSessionvalue.UserName;
                txtEmail.Text = objSessionvalue.UserEmail;
                lbtnSaveChanges.Attributes.Add("onclick", "HideIndicator();");
            }
            else
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }
        }
        setDefault();
    }

    protected void lbtnSaveChanges_Click(object sender, EventArgs e)
    {
        try
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);

            bool isEmailSame = false;
            int emailcheck = 0;
            if (objSessionvalue.UserEmail.Trim() != txtEmail.Text.Trim())
                emailcheck = _presenter.EmailAvailable(txtEmail.Text,ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower());
            else
                isEmailSame = true;

            //int emailcheck = _presenter.EmailAvailable(txtEmail.Text);
            if (emailcheck.Equals(0))
            {
                string message = string.Empty;
                this._presenter.OnChangeEmailPassword();

                //Set the new email in session
                if (!string.IsNullOrEmpty(txtEmail.Text.Trim()) && !isEmailSame)
                {
                    objSessionvalue.UserEmail = txtEmail.Text.Trim();
                    stateManager.Add("objSessionvalue", objSessionvalue, StateManager.State.Session);
                }

                if (txtEmail.Text.Length > 0 && txtPassword.Text.Length > 0 && !isEmailSame)
                    setmessage("<h2>Email & Password Updated</h2><P>Your email & password are updated successfully.</P>", 2);
                else if (txtPassword.Text.Length > 0)
                    setmessage("<h2>Password Updated</h2><P>Your password is updated successfully.</P>", 2);
                else if (!isEmailSame)
                    setmessage("<h2>Email Updated</h2><P>Your email is updated successfully.</P>", 2);
                else
                    setmessage("<h2>Oops - there is a problem in your submission.</h2> <h3>Please correct the errors below:</h3><ul><li>New Email address is simliar to the Old Email Address.</li></ul>", 1);

                txtPassword.Text = txtConfirmPassword.Text = string.Empty;
            }
            else
            {
                setmessage("<h2>Oops - there is a problem in your submission.</h2> <h3>Please correct the errors below:</h3><ul><li>Email address already exists.</li></ul>", 1);
            }

        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem in your submission.</h2> <h3>Please correct the errors below:</h3><ul><li>"+ex.Message+"</li></ul>", 1);
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

    #region Public methods
    [CreateNew]
    public AdminProfileEmailpasswordPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    #endregion Public methods

    #region IAdminProfileEmailpassword Members

    public string Email
    {
        get {return txtEmail.Text.ToString(); }
    }

    public string Password
    {
        get {
           // return TributePortalSecurity.Security.EncryptSymmetric(txtPassword.Text.ToString());
            return txtPassword.Text.ToString(); 
        }
    }
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


    #endregion
    
}
