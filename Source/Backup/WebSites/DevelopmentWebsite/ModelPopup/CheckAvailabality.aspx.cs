///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.ModelPopup.CheckAvailabality.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the availability of username to a user.
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
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;
using TributesPortal.Utilities;

public partial class ModelPopup_CheckAvailabality : System.Web.UI.Page
{
    public string _UserDetails = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
     //   if (!IsPostBack)
       // {
            if ((Request.QueryString["User"] != null) && (Request.QueryString["Pass"] != null))
            {
                string UserName = Request.QueryString["User"].ToString();
                string Password = Request.QueryString["Pass"].ToString();
                CheckAvailablity(UserName, Password);
            }
            if (Request.QueryString["UserId"] != null)
            {
                string UserId = Request.QueryString["UserId"].ToString();
                //Response.Write("true");
                //Response.Write("false");
                UserDetails(int.Parse(UserId));
            }
            if (Request.QueryString["Couponid"] != null)
            {
                string UserId = Request.QueryString["Couponid"].ToString();
                Response.Write("1");               
            }
        
        //}
       

        //ClientScriptManager manager = Page.ClientScript;
        //manager.RegisterStartupScript(this.GetType(), "CallSomething", "Cookiecreate('hi');", true);
       // this.Page.Attributes.Add
    }
    private void CheckAvailablity(string UserName, string Password)
    {
        string uName = string.Empty;
        HiddenField hf = (HiddenField)this.FindControl("ctl00$HiddenFieldAvailability");
        UserInfoManager objUserInfoManager = new UserInfoManager();        
        GenralUserInfo _objGenralUserInfo = new GenralUserInfo();
        UserInfo objUserInfo = new UserInfo();
        objUserInfo.UserName = UserName;
        objUserInfo.UserPassword = Password.ToLower().ToString();;
        _objGenralUserInfo.RecentUsers = objUserInfo;
        objUserInfoManager.UserLogin(_objGenralUserInfo);        
        if (_objGenralUserInfo.CustomError == null)
        {
             SetSessionValue(_objGenralUserInfo);           
            Response.Write("true");
        }
        else
        {
            Response.Write("false");
        }
        
    }
    private void UserDetails(int UserId)
    {
        UserInfoManager objUserInfoManager = new UserInfoManager();
        UserProfile objregist = new UserProfile();
        objregist.UserId = UserId;
        object[] param={objregist};        
        objUserInfoManager.GetUserProfile(param);

        _UserDetails = objregist.FirstName;
        _UserDetails += "`" + objregist.UserName;
        _UserDetails += "`" + objregist.CreatedOn.ToString("MMMM dd,yyyy");
        _UserDetails += "`" + objregist.City + "=" + objregist.State.ToString() + "=" + objregist.Country.ToString();        
        _UserDetails += "`" + objregist.Website;
        _UserDetails += "`" + objregist.IsLocationHide;
        _UserDetails += "`" + objregist.IsUsernameVisiable;
        _UserDetails += "`" + objregist.UserImage;
        _UserDetails += "`" + objregist.AllowIncomingMsg;

        Response.Write(_UserDetails);
        // TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;
        //stateManager.Add("Userdetails", objregist, StateManager.State.Session);

    }

    private void SetSessionValue(GenralUserInfo _objGenralUserInfo)
    {
        SessionValue _objSessionValue = new SessionValue(_objGenralUserInfo.RecentUsers.UserID,
                                                               _objGenralUserInfo.RecentUsers.UserName,
                                                               _objGenralUserInfo.RecentUsers.FirstName,
                                                               _objGenralUserInfo.RecentUsers.LastName,
                                                               _objGenralUserInfo.RecentUsers.UserEmail,
                                                               int.Parse(_objGenralUserInfo.RecentUsers.UserType),
                                                               _objGenralUserInfo.RecentUsers.UserTypeDescription,
                                                               _objGenralUserInfo.RecentUsers.IsUsernameVisiable
                                                                //Added by Rupendra to get User Image
                                                                   , _objGenralUserInfo.RecentUsers.UserImage
                                                               );
        TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;
        stateManager.Add("objSessionvalue", _objSessionValue, StateManager.State.Session);
    }
}
