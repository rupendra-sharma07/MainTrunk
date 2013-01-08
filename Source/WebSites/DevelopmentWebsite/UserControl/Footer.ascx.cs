///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.UserControl.Footer.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page defines the items to be displayed on the footer all the pages on the site
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Xml;
using System.IO;
using TributesPortal.Utilities;
public partial class UserControl_Footer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
        lblCopyRight.Text = DateTime.Now.Year.ToString();
        lblCopyRightyt.Text = DateTime.Now.Year.ToString();
    }


}
