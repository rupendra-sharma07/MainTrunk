///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.TributeAdmin.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the admin rights to the user
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
using TributesPortal.Tribute.Views;
using TributesPortal.BusinessEntities;

public partial class Tribute_TributeAdmin : PageBase, ITributeAdmin
{
    private TributeAdminPresenter _presenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this._presenter.OnViewInitialized();
         //   btnSignIn.Attributes.Add("onclick",);
        }
        this._presenter.OnViewLoaded();
    }

    [CreateNew]
    public TributeAdminPresenter Presenter
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        //this.Page.d
        ValidateUser(txtUserName.Text.ToString(),txtPassword.Text.ToString() );
    }


    public string ValidateUser(string UserName, string Passsword)
    {
        string _UserType = string.Empty;
        try
        {
            GenralUserInfo objGenralUserInfo = new GenralUserInfo();
            UserInfo objUserInfo = new UserInfo();

            objUserInfo.UserName = UserName;
            objUserInfo.UserPassword = Passsword;
            objGenralUserInfo.RecentUsers = objUserInfo;
            _presenter.OnLogin(objGenralUserInfo);

            if (objGenralUserInfo.CustomError == null)
            {
                //ShowMessage(objGenralUserInfo.CustomError.ErrorMessage.ToString());
                _UserType = objGenralUserInfo.RecentUsers.UserType.ToString();
            }
            else
            {
                ShowMessage(objGenralUserInfo.CustomError.ErrorMessage.ToString());
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

        return _UserType;
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {   
        Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Admin2UserRegistration.ToString()));
    }
}


