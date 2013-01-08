///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Users.ForgotPassword.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to recover his lost password
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

public partial class Users_ForgotPassword : PageBase, IForgotPassword
{
    private ForgotPasswordPresenter _presenter;

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
    public ForgotPasswordPresenter Presenter
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

    private void SetText()
    {
        lblForgotPassword.Text = ResourceText.GetString("lblForgotPassword_FYP");
        lblNote.Text = ResourceText.GetString("lblNote_FYP");
        lblEmail.Text = ResourceText.GetString("lblEmail_FYP");
        revEmail.ErrorMessage= ResourceText.GetString("revEmail_FYP");
        rfvEmail.ErrorMessage = ResourceText.GetString("rfvEmail_FYP");
        btnSubmit.Text = ResourceText.GetString("btnSubmit_FYP");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GenralUserInfo objGenralUserInfo = new GenralUserInfo();
            UserInfo objUserInfo = new UserInfo();
            objUserInfo.UserEmail = txtEmailId.Text.ToString();
            objGenralUserInfo.RecentUsers = objUserInfo;
            _presenter.CheckAndSendPassword(objGenralUserInfo, false);
            if (objGenralUserInfo.CustomError == null)
            {
                ShowMessage("Password Information send successfully");
            }
            else
            {
                lblMsg.Text = objGenralUserInfo.CustomError.ErrorMessage.ToString();
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = e1.Message;//objGenralUserInfo.CustomError.ErrorMessage.ToString();
        }
    }
}


