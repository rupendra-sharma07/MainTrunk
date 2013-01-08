///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.popupCancel.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the popup Cancel page
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

public partial class Tribute_popupCancel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnDonotCancel.Attributes.Add("onClick", "javascript:window.close();");
    }
}
