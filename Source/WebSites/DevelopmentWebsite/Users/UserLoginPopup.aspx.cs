///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Users.UserLoginPopup.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the login page in a popup
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
using TributesPortal.Users.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;

public partial class Users_UserLoginPopup :PageBase, IUserLogin
{
    private UserLoginPresenter _presenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this._presenter.OnViewInitialized();
            SetText();
        }
        this._presenter.OnViewLoaded();
    }

    [CreateNew]
    public UserLoginPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    // TODO: Forward events to the presenter and show state to the user.
    // For examples of this, see the View-Presenter (with Application Controller) QuickStart:
    //		ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.wcsf.2007jun/wcsf/html/02-480-ViewPresenter.htm

    protected void btnSignin_Click(object sender, EventArgs e)
    {
        try
        {
            string _UserType = string.Empty;

            _UserType = ValidateUser(txtUserName.Text, txtPassword.Text);
            FormsAuthentication.Initialize();

            if (_UserType != string.Empty)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1, // Ticket version
                    txtUserName.Text, // Username associated with ticket
                    DateTime.Now, // Date/time issued
                    DateTime.Now.AddMinutes(30), // Date/time to expire
                    true, // "true" for a persistent user cookie
                    _UserType, // User-data, in this case the roles             
                    FormsAuthentication.FormsCookiePath);// Path cookie valid for

                // Encrypt the cookie using the machine key for secure transport
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(
                   FormsAuthentication.FormsCookieName, // Name of auth cookie
                   hash); // Hashed ticket

                // Set the cookie's expiration time to the tickets expiration time
                if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

                cookie.Domain = "." + WebConfig.TopLevelDomain;

                // Add the cookie to the list for outgoing response
                Response.Cookies.Add(cookie);
                Session["UserName"] = txtUserName.Text;
                //e.Authenticated = true;
                //Transfer page will goes here
                Response.Redirect("CustomerList.aspx");
            }
            else
            {
               // e.Authenticated = false;
                return;
            }

        }
        catch (Exception e1)
        {
            throw e1;
        }
        finally
        {

        }
    }
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
// code to send mail
    }

    public string ValidateUser(string objUserName, string objUserPasss)
    {
        string _UserType = string.Empty;
        try
        {
            GenralUserInfo objGenralUserInfo = new GenralUserInfo();
            UserInfo objUserInfo = new UserInfo();

            objUserInfo.UserName = objUserName;
            objUserInfo.UserPassword = objUserPasss;
            objGenralUserInfo.RecentUsers = objUserInfo;
            _presenter.OnLogin(objGenralUserInfo);

            if (objGenralUserInfo.CustomError == null)
            {
                //ShowMessage(objGenralUserInfo.CustomError.ErrorMessage.ToString());
                _UserType = objGenralUserInfo.RecentUsers.UserType.ToString();
                TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;

                stateManager.Add("UserId", objGenralUserInfo.RecentUsers.UserID.ToString(), StateManager.State.Session);
                stateManager.Add("UserName", objGenralUserInfo.RecentUsers.UserName.ToString(), StateManager.State.Session);
                stateManager.Add("FirstName", objGenralUserInfo.RecentUsers.FirstName.ToString(), StateManager.State.Session);
                stateManager.Add("LastName", objGenralUserInfo.RecentUsers.LastName.ToString(), StateManager.State.Session);
                stateManager.Add("UserType", objGenralUserInfo.RecentUsers.UserType.ToString(), StateManager.State.Session);
             //   stateManager.Add("UserTypeDescription", objGenralUserInfo.RecentUsers.UserTypeDescription.ToString(), StateManager.State.Session);
                stateManager.Add("UserImagePath", objGenralUserInfo.RecentUsers.UserImage.ToString(), StateManager.State.Session);
            }
            else
            {
                lblFailureText.Text = objGenralUserInfo.CustomError.ErrorMessage.ToString();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

        return _UserType;
    }

    private void SetText()
    {
        lblUserName.Text = ResourceText.GetString("lblUserName_ULP");
        lblPassword.Text = ResourceText.GetString("lblPassword_ULP");
        lblNote.Text = ResourceText.GetString("lblNote_ULP");
        lblEmail.Text = ResourceText.GetString("lblEmail_ULP");
        lblForgetPass.Text = ResourceText.GetString("lblForgetPass_ULP");
        btnSendMail.Text = ResourceText.GetString("btnSendMail_ULP");
        btnSignin.Text = ResourceText.GetString("btnSignin_ULP");
       
    }
}


