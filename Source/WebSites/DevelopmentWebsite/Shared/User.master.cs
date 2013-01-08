///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Shared.User.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page provides the default layout for the user profile pages
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Web.UI;
using TributesPortal.BusinessEntities;
using TributesPortal.Users;

public partial class Shared_User : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            txtLoginUsername1.Attributes.Add("onkeydown", "setpopdefault();");
            txtLoginPassword1.Attributes.Add("onkeydown", "setpopdefault2();");
            txtLoginEmail1.Attributes.Add("onkeydown", "setpopemail();");
            popuplbtnSendemail.Attributes.Add("onclick", "SetEmailGroup();");
            popuplbtnLogin.Attributes.Add("onclick", "SetUserGroup();");            
            
            //
        }
    }
    private void CheckAvailability()
    {
        string[] UsersName = UserHiddenField.Value.ToString().Split(',');
        if (UsersName.Length == 2)
        {
                GenralUserInfo _objGenralUserInfo = new GenralUserInfo();
                UserInfo objUserInfo = new UserInfo();
                objUserInfo.UserName = UsersName[0];
                objUserInfo.UserPassword = UsersName[1];
                _objGenralUserInfo.RecentUsers = objUserInfo;
                UsersController objUsersController = new UsersController();
                objUsersController.ValidateLogin(_objGenralUserInfo);
                if (_objGenralUserInfo.CustomError == null)
                {
                    HiddenFieldAvailability.Value = "true";
                }
                else
                {
                    HiddenFieldAvailability.Value = "false";
                }
        }    
    }

    protected void lnkpopSendemail_Click(object sender, ImageClickEventArgs e)
    {
        string[] UsersName = UserHiddenField.Value.ToString().Split(',');

    }
   
    
    protected void popuplbtnSendemail_Click(object sender, EventArgs e)
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
    protected void txtLoginEmail_TextChanged(object sender, EventArgs e)
    {

    }
}
