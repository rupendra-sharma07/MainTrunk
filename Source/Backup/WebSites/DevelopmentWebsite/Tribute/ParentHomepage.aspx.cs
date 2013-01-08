///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.ParentHomepage.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the parent home page
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Tribute.Views;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TributesPortal.MultipleLangSupport;
using System.Web.SessionState;

public partial class Tribute_ParentHomepage : System.Web.UI.Page
{
    protected String _userName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (!Equals(objSessionvalue, null))
            {
                spanLogout.InnerHtml = "<a href='Logout.aspx'>Log out</a>";
                _userName = objSessionvalue.UserName;
                myprofile.Visible = true;
                lnRegistration.Visible = false;
            }
            else
            {
                lnRegistration.Visible = true;
                spanLogout.InnerHtml = "<a href='../Users/log_in.aspx'>Log in</a>";
                myprofile.Visible = false;

            }
        }
    }
}
