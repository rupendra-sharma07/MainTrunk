///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Users.ChangeEmailPassword.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to change his/her email or password of the account
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
using TributesPortal.Users;

public partial class Users_ChangeEmailPassword : PageBase, IChangeEmailPassword
{
    private ChangeEmailPasswordPresenter _presenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;
        if (stateManager.Get("UserId", StateManager.State.Session) == null)
        {
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.UserReg2AdminConf.ToString()));
        }
        if (!this.IsPostBack)
        {

            this._presenter.OnViewInitialized();
            LoadUserData();
            SetText();
        }
        this._presenter.OnViewLoaded();
    }

    [CreateNew]
    public ChangeEmailPasswordPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    private void SetText()
    {
        lblEmail.Text = ResourceText.GetString("lblEmailTitle_CEP");
        lblEmailTitle.Text = ResourceText.GetString("lblEmail_CEP");
        lblPasswordTitle.Text = ResourceText.GetString("lblPasswordTitle_CEP");
        lblPassword.Text = ResourceText.GetString("lblPassword_CEP");
        lblConformPassword.Text = ResourceText.GetString("lblConformPassword_CEP");
        btnSubmit.Text = ResourceText.GetString("btnSubmit_CEP");
        revEmail.ErrorMessage = ResourceText.GetString("revEmail_CEP");
        rfvEmail.ErrorMessage = ResourceText.GetString("rfvEmail_CEP");
        rfvPassword.ErrorMessage = ResourceText.GetString("rfvPassword_CEP");
        rfvConformPass.ErrorMessage = ResourceText.GetString("rfvConformPass_CEP");
        cvConformPassword.ErrorMessage = ResourceText.GetString("cvConformPassword_CEP");
    }
    private void LoadUserData()
    {
        //lblMsg.Text = "";
        GenralUserInfo objGenralUserInfo = new GenralUserInfo();
        UserInfo objUserInfo = new UserInfo();

        TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;

        //stateManager.Add("UserId", objGenralUserInfo.RecentUsers.UserID.ToString(), StateManager.State.Session);
        objUserInfo.UserID = int.Parse(stateManager.Get("UserId", StateManager.State.Session).ToString());
        objGenralUserInfo.RecentUsers = objUserInfo;
        _presenter.GetUserData(objGenralUserInfo);
        if (objGenralUserInfo.CustomError == null)
        {
            txtEmail.Text = objGenralUserInfo.RecentUsers.UserEmail;
            txtPassword.Text = objGenralUserInfo.RecentUsers.UserPassword;
            txtConformPassword.Text = objGenralUserInfo.RecentUsers.UserPassword;
        }
        else
        {
            lblMessage.Text = objGenralUserInfo.CustomError.ErrorMessage.ToString();
        }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GenralUserInfo objGenralUserInfo = new GenralUserInfo();
        UserInfo objUserInfo = new UserInfo();

        TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;

        //stateManager.Add("UserId", objGenralUserInfo.RecentUsers.UserID.ToString(), StateManager.State.Session);
        objUserInfo.UserID = int.Parse(stateManager.Get("UserId", StateManager.State.Session).ToString());
        objUserInfo.UserEmail = txtEmail.Text.Trim();//int.Parse(stateManager.Get("UserId", StateManager.State.Session).ToString());
        objUserInfo.UserPassword = txtPassword.Text.Trim();
        objGenralUserInfo.RecentUsers = objUserInfo;
        _presenter.OnChangeEmailPassword(objGenralUserInfo);
        if (objGenralUserInfo.CustomError == null)
        {
            txtEmail.Text = objGenralUserInfo.RecentUsers.UserEmail;
            txtPassword.Text = objGenralUserInfo.RecentUsers.UserPassword;
            txtConformPassword.Text = objGenralUserInfo.RecentUsers.UserPassword;
            lblMessage.Text = ResourceText.GetString("msgSuccess_CEP");
        }
        else
        {
            lblMessage.Text = objGenralUserInfo.CustomError.ErrorMessage.ToString();
        }
    }
	
}


