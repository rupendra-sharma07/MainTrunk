///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.ModelPopup.SendMessage.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to send a message to some other users
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

public partial class ModelPopup_SendMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Focus();

    }
    protected void lbtnSendMessages_Click(object sender, EventArgs e)
    {

        string AdministratorMail = Request.QueryString["emailId"].ToString(); //"" + WebConfig.InfoEmail + "";
            //WebConfig.AdministratorMail"].ToString();
        EmailMessages objEmail = EmailMessages.Instance;
      /*  if(ddlReason.SelectedItem.Value == "needtechsupport") 
            AdministratorMail = "" + WebConfig.SupportEmail + "";
        else if(ddlReason.SelectedItem.Value == "generalinquiry") 
            AdministratorMail = "" + WebConfig.InfoEmail + "";
        else if(ddlReason.SelectedItem.Value == "billingquestion") 
            AdministratorMail = "" + WebConfig.BillingEmail + "";
        else if(ddlReason.SelectedItem.Value == "newfeature") 
            AdministratorMail = "" + WebConfig.FeedbackEmail + "";
        else if(ddlReason.SelectedItem.Value == "inappropriate") 
            AdministratorMail = "" + WebConfig.SupportEmail + ""; */
     

        //objEmail.SendMessages(AdministratorMail, txtEmail.Text, ddlReason.Text, txtarEmailMessage.Text, EmailMessages.TextFormat.Html.ToString());
        objEmail.SendMessages(txtName.Text+"<"+txtEmail.Text+">", AdministratorMail, ddlReason.Text, txtarEmailMessage.Text, EmailMessages.TextFormat.Html.ToString());
        StringBuilder objstrb = new StringBuilder();
        objstrb.Append("<script language=javascript>");
        objstrb.Append("parent.modalClose();");
        objstrb.Append("</script>");

        Response.Write(objstrb.ToString());
        

    }
}
