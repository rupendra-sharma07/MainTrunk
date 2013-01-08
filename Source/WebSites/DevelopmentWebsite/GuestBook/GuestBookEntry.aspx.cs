///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.GuestBook.GuestBookEntry.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays and allows users to add entries to the guestbook for a tribute
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
using TributesPortal.MultipleLangSupport;

public partial class GuestBook_GuestBookEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetControlText();
        }
    }

    private void SetControlText()
    {
        lblMessage_GB.Text = ResourceText.GetString("lblMessage_GB");
    }
}
