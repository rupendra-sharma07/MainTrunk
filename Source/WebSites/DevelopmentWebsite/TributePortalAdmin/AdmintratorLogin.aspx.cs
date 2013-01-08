///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.TributePortalAdmin.AdmintratorLogin.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to login as the administrator
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
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.TributePortalAdmin.Views;
using TributesPortal.Utilities;
using System.Xml;
using System.IO;

public partial class TributePortalAdmin_AdmintratorLogin : System.Web.UI.Page, IAdmintratorLogin
{
    private AdmintratorLoginPresenter _presenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this._presenter.OnViewInitialized();
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
                        //string redirectUrl = Request.Url.ToString().Replace(@"http://", @"https://");
                        //Response.Redirect(redirectUrl);
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
        this._presenter.OnViewLoaded();
    }

    [CreateNew]
    public AdmintratorLoginPresenter Presenter
    {
        set
        {
           
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    public string UserName
    {
        get { return txtUsername.Text; }
    }

    public string Password
    {
        get
        {
            return txtPassword.Text.ToString(); //Removed ToLower() to maintain case sensitivity 05-Feb-09 --ANKI
        }
    }
    public string Message
    {
        set { lblErrorMsg.Text = value; ; }
    }


    protected void btnLogin_Click(object sender, EventArgs e)
    {
        _presenter.OnAdminLogin();
        if (lblErrorMsg.Text.ToString() == "")
            Response.Redirect("PortalSummaryReport.Aspx");
    }
}


