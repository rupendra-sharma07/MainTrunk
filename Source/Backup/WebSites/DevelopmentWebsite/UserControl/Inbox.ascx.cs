///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.UserControl.Inbox.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page describes the functioning of the inbox provided by the site to the users
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
using TributesPortal.BusinessLogic;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;

public partial class UserControl_Inbox : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
        if (objSessionvalue != null)
        {
            object[] objValue ={ objSessionvalue.UserId };
            UserInfoManager objinbox = new UserInfoManager();
            int _inboxcount = objinbox.UserInboxCount(objValue);
            lbtnInbox.Text = "Inbox (" + _inboxcount.ToString() + ")";
            lbtnInbox.Visible = true;
        }
        else
        {
            lbtnInbox.Visible = false;
        }
    }
    protected void lbtnInbox_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + "inbox.aspx");
    }
}
