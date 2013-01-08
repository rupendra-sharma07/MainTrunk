///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.UserControl.Footer.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page defines the items to be displayed on the footer all the pages on the site
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Configuration;
using System.Xml;
using System.IO;
using TributesPortal.Utilities;
using System.Net;
using System.Text;
using PerceptiveMCAPI.Types;
using PerceptiveMCAPI.Methods;
using System.Linq;

public partial class UserControl_FooterHome : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            yourmomentsFooter.Visible = true;
            yourtributeFooter.Visible = false;
            copyRightYM.Visible = true;
            copyRight.Visible = false;
            copyRight.Visible = false;
        }    
        if (!Page.IsPostBack)
        {
            btnSubscribe.Attributes.Add("onclick", "return ValidateForm();");
        }
        if (!WebConfig.ApplicationMode.Equals("local"))
        {
            string strXmlPath = AppDomain.CurrentDomain.BaseDirectory + "Common\\XML\\SSLPage.xml";
            FileStream docIn = new FileStream(strXmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            XmlDocument pageDoc = new XmlDocument();
            //Load the Xml Document
            pageDoc.Load(docIn);
            XmlNodeList XNodeList = pageDoc.SelectNodes("//Pages/page");
            string requestedURL = Request.Url.ToString().ToUpper();
            string[] rawURL = Request.RawUrl.Split("/".ToCharArray());
            int length = rawURL.Length;
            string redirectedPageName = rawURL[length - 1].ToString();
            if (requestedURL.Contains(@"HTTP://"))
            {
                foreach (XmlElement XElement in XNodeList)
                {
                    string pagename = XElement["pagename"].InnerText.ToString().ToUpper();
                    if (requestedURL.Contains(pagename))
                    {
                        Response.Redirect(@"https://www." + WebConfig.TopLevelDomain + "/" + redirectedPageName);
                    }
                }
            }
            else
            {
                bool isPageSecure = false;
                foreach (XmlElement XElement in XNodeList)
                {
                    string pagename = XElement["pagename"].InnerText.ToString().ToUpper();
                    if (requestedURL.Contains(pagename))
                    {
                        isPageSecure = true;
                    }
                }
                if (!isPageSecure)
                    Response.Redirect(@"http://www." + WebConfig.TopLevelDomain + "/" + redirectedPageName);
            }
        }
        lblCopyRight.Text = System.DateTime.Now.Year.ToString();
    }

    /// <summary>
    /// This methods saves the email address to newsletter
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubscribe_Click(object sender, EventArgs e)
    {
        lblResult.Text = string.Empty;
        //AddSiteVisitor();
        AddMailChimpSubscriber();
        txtEmailAddress.Text = string.Empty;

    }

    private void AddMailChimpSubscriber()
    {
        listSubscribeInput input = new listSubscribeInput();
        input.api_AccessType = PerceptiveMCAPI.EnumValues.AccessType.Serial;
        input.api_CustomErrorMessages = true;
        input.parms.email_address = txtEmailAddress.Text.ToString();
        input.parms.send_welcome = true;
        input.parms.update_existing = false;
        input.parms.replace_interests = true;
        input.parms.double_optin = false;
        input.parms.merge_vars.Add("EMAIL", txtEmailAddress.Text.ToString());

        input.api_MethodType = PerceptiveMCAPI.EnumValues.MethodType.POST;
        input.api_Validate = true;
        input.api_OutputType = PerceptiveMCAPI.EnumValues.OutputType.XML;
        input.parms.apikey = WebConfig.MailChimpApiKey;
        input.parms.id = WebConfig.NewsLettersListID;
        listSubscribe Subscribe = new listSubscribe();
        listSubscribeOutput output = Subscribe.Execute(input);
        if (output.api_ErrorMessages.Count > 0)
        {
            string ErrorCode = output.api_ErrorMessages.FirstOrDefault().code;
            string Error = "Error occurred. " + output.api_ErrorMessages.FirstOrDefault().error;
            lblResult.Text = ErrorCode + " - " + Error;
            //return false;
        }
        else
        {
            if (output.result)
                lblResult.Text = "Thank You for Subscribing.";
        }
        txtEmailAddress.Text = "Enter your email address";
    }

    protected string HomeUrl()
    {
        string homeUrl = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"];
        return homeUrl;
    }
    protected string LogoTitle()
    {
        string title = "Return to Your " + ConfigurationManager.AppSettings["ApplicationType"] + " Home Page";
        return title;
    }

}//end class
