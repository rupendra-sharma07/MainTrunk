///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.ModelPopup.UserProfile.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to view the User profile in a popup window
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
using System.Text;

public partial class ModelPopup_UserProfile : PageBase
{
    //private TributeHomePagePresenter _presenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Changed by rupendra to enable button postback as it was not working due to URL Rewritting 26-jul-2011
        //this.Page.Form.Action = Request.RawUrl;
        divResult.Visible = false;
        Response.Cache.SetExpires(DateTime.Now);
        if (Request.QueryString["userid"] != null)
        {
            if (!Page.IsPostBack)
            {
                string userid = Request.QueryString["userid"];
                UserDetails(int.Parse(userid));
            }
        }

    }

    private void UserDetails(int UserId)
    {

        UserInfoManager objUserInfoManager = new UserInfoManager();
        UserProfile objregist = new UserProfile();
        objregist.UserId = UserId;
        object[] param ={ objregist };
        objUserInfoManager.GetUserProfile(param);
        StringBuilder objstrb = new StringBuilder();

        if (objregist.IsUsernameVisiable.Equals(true))
        {
            objstrb.Append("<dt id='dtlblusrname' runat='server' >Username:</dt>");
            objstrb.Append("<dd id='ddtxtusrname' runat='server' class='nickname'>" + objregist.UserName + "</dd>");
        }
        else
        {
            objstrb.Append("<dt id='dtlblname' runat='server'>Name:</dt>");
            objstrb.Append("<dd id='ddtxtname' runat='server' class='fn'>" + objregist.FirstName + "</dd>");
        }
        objstrb.Append("<dt id='dtlblmember' runat='server'>Member Since:</dt>");
        objstrb.Append("<dd id='ddtxtmember' runat='server'>" + objregist.CreatedOn.ToString("MMMM dd, yyyy") + "</dd>");
        if (objregist.IsLocationHide.Equals(false))
        {
            objstrb.Append("<dt id='dtlbllocation' runat='server'>Location:</dt>");
            if (objregist.StreetAddress == "")
            {
                if (objregist.City == "")
                {
                    if (objregist.State == "")
                        objstrb.Append("<dd id='ddtxtlocation' runat='server' class='adr'>" + objregist.Country.ToString() + "</dd>");
                    else
                        objstrb.Append("<dd id='ddtxtlocation' runat='server' class='adr'>" + objregist.State.ToString() + ", " + objregist.Country.ToString() + "</dd>");
                }
                else
                {
                    if (objregist.State == "")
                        objstrb.Append("<dd id='ddtxtlocation' runat='server' class='adr'>" + objregist.City + " <br />" + objregist.Country.ToString() + "</dd>");
                    else
                        objstrb.Append("<dd id='ddtxtlocation' runat='server' class='adr'>" + objregist.City + " <br />" + objregist.State.ToString() + ", " + objregist.Country.ToString() + "</dd>");
                }
            }
            else
            {
                if (objregist.City == "")
                {
                    if (objregist.State == "")
                        objstrb.Append("<dd id='ddtxtlocation' runat='server' class='adr'>" + objregist.StreetAddress + "<br />" + objregist.Country.ToString() + "</dd>");
                    else
                        objstrb.Append("<dd id='ddtxtlocation' runat='server' class='adr'>" + objregist.StreetAddress + "<br />" + objregist.State.ToString() + ", " + objregist.Country.ToString() + "</dd>");
                }
                else
                {
                    if (objregist.State == "")
                        objstrb.Append("<dd id='ddtxtlocation' runat='server' class='adr'>" + objregist.StreetAddress + "<br />" + objregist.City + " <br />" + objregist.Country.ToString() + "</dd>");
                    else
                        objstrb.Append("<dd id='ddtxtlocation' runat='server' class='adr'>" + objregist.StreetAddress + "<br />" + objregist.City + ", " + objregist.State + " <br />" + objregist.Country + "</dd>");
                }
            }
        }
        if (objregist != null && objregist.PhoneNumber != null && (!objregist.PhoneNumber.Equals("")))
        {
            String strPhone = "(" + objregist.PhoneNumber.Substring(0, 3) + ") " + objregist.PhoneNumber.Substring(3, 3) + "-" + objregist.PhoneNumber.Substring(6);
            objstrb.Append("<dt id='dtlblphone' runat='server' >Phone Number:</dt>");
            objstrb.Append("<dd id='ddlblphone' runat='server' class='phone'>" + strPhone + "</dd>");
        }
        if (objregist != null && objregist.Website != null && (!objregist.Website.Equals("")))
        {
            objstrb.Append("<dt id='dtlblwebsite' runat='server' >Website:</dt>");
            objstrb.Append("<dd id='ddlblwebsite' runat='server' class='url'><a onclick=window.open('http://" + objregist.Website.Trim() + "') href='javascript:void(0);'>" + objregist.Website + "</a></dd>");
        }

        ltrlUserProfile.Text = objstrb.ToString();

        string profile_prefix = CommonUtilities.GetPath()[2].ToString();
        if (objregist.UserImage.StartsWith("http://") || objregist.UserImage.StartsWith("https://"))
        {
            UserProfileImage.Src = objregist.UserImage;
        }
        else
        {
            UserProfileImage.Src = profile_prefix + objregist.UserImage;
        }
        //if (objregist.FacebookUid != null &&
        //        (objregist.UserImage.EndsWith("images/bg_ProfilePhoto.gif") ||
        //         objregist.UserImage.StartsWith("http://") || objregist.UserImage.StartsWith("https://")))
        if (objregist.FacebookUid != null && Facebook.Web.FacebookWebContext.Current.Session != null)
        {
            aUserProfileImg.Visible = false;
            UserProfileImage.Visible = false;
            FacebookProfileImage.Visible = true;
            StringBuilder html = new StringBuilder();
            html.Append("<fb:profile-pic uid=\"");
            html.Append(objregist.FacebookUid.ToString());
            html.Append("\" size=\"small\" linked=\"false\" facebook-logo=\"true\"></fb:profile-pic>");
            FacebookProfileImage.InnerHtml = html.ToString();
            //FacebookProfileImage.InnerHtml = "<fb:profile-pic uid=\""+objregist.FacebookUid.ToString()+
            //    "\" facebook-logo=\"true\" linked=\"false\" size=\"square\"></fb:profile-pic>";

            //StringBuilder html = new StringBuilder();
            //html.Append("<fb:profile-pic uid=\"");
            //html.Append(FacebookWebContext.Current.UserId);
            //html.Append("\" size=\"square\"></fb:profile-pic>");
            //fbimage.InnerHtml = html.ToString();

        }

        if (objregist.AllowIncomingMsg.Equals(false))
            fldMessage.Visible = false;
        else
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                if (objSessionvalue.UserId > 0)
                {
                    divlogin.Visible = false;
                    fldMessage.Visible = true;
                }
                else
                {
                    divlogin.Visible = true;
                    fldMessage.Visible = false;
                }
            }
            else
            {
                divlogin.Visible = true;
                fldMessage.Visible = false;
            }

        }

        StateManager stateManager1 = StateManager.Instance;
        SessionValue objSessionvalue1 = (SessionValue)stateManager1.Get("objSessionvalue", StateManager.State.Session);
        if (objSessionvalue1 != null)
            divlogin.Visible = false;
        else
            divlogin.Visible = true;

    }
    protected void ltbnSendMessages_Click(object sender, EventArgs e)
    {
        string _EmailBody = string.Empty;
        string Subject = string.Empty;
        int SendToUserId = 0;
        try
        {
            if (Request.QueryString["userid"] != null)
            {
                string userid = Request.QueryString["userid"];
                SendToUserId = int.Parse(userid);
            }
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            int SendByUserId = 0;
            if (objSessionvalue != null)
            {
                SendByUserId = objSessionvalue.UserId;
            }


            Subject = txtUserProfileMsgSubj.Text;

            _EmailBody = txtarUserProfileMsg.Text;

            EmailManager objec = new EmailManager();
            objec.SendMail(SendByUserId, SendToUserId, Subject, _EmailBody);

            // Changed by rupendra to close the current model pop-up window
            //String strJScript;
            //strJScript = "<script language=javascript>";
            //strJScript += "parent.modalCloseLogin();";
            //strJScript += "</script>";
            //Response.Write(strJScript);

            divResult.Visible = true;
            txtUserProfileMsgSubj.Text = "";
            txtarUserProfileMsg.Text = "";

            ClientScript.RegisterClientScriptBlock(GetType(), "Close Profile popup", "parent.modalClose();", true);
            //ClientScript.RegisterClientScriptBlock(GetType(), "Close Profile popup", "window.parent.location.reload(true);", true);

        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "Close Profile popup", "parent.window.location.reload(true);", true);

        }
    }
}
