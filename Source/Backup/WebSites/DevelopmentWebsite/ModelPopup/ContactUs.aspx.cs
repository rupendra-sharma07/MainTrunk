///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.ModelPopup.ContactUs.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays a form that the user can use to contact YourTribute
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
using TributesPortal.Utilities;
using System.Text;

public partial class ModelPopup_ContactUs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Focus();

    }
    protected void lbtnSendMessages_Click(object sender, EventArgs e)
    {
        string fromEmail = string.Empty;

        string AdministratorMail = "" + WebConfig.InfoEmail + "";
        //WebConfig.AdministratorMail;
        EmailMessages objEmail = EmailMessages.Instance;
        if (ddlReason.SelectedItem.Value == "needtechsupport")
            AdministratorMail = "" + WebConfig.SupportEmail + "";
        else if (ddlReason.SelectedItem.Value == "generalinquiry")
            AdministratorMail = "" + WebConfig.InfoEmail + "";
        else if (ddlReason.SelectedItem.Value == "billingquestion")
            AdministratorMail = "" + WebConfig.BillingEmail + "";
        else if (ddlReason.SelectedItem.Value == "newfeature")
            AdministratorMail = "" + WebConfig.FeedbackEmail + "";
        else if (ddlReason.SelectedItem.Value == "inappropriate")
            AdministratorMail = "" + WebConfig.SupportEmail + "";

        fromEmail = txtName.Text + " <" + txtEmail.Text + ">";

        objEmail.SendMessages(fromEmail, AdministratorMail, ddlReason.SelectedItem.Text, txtarEmailMessage.Text, EmailMessages.TextFormat.Html.ToString());
        StringBuilder objstrb = new StringBuilder();
        objstrb.Append("<script language=javascript>");
        objstrb.Append("parent.modalClose();");
        objstrb.Append("</script>");

        Response.Write(objstrb.ToString());


    }
}
