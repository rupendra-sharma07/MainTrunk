///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.UserControl.TributeBlog.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page defines blog control to be used on the profile page
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
using System.Text;
using System.IO;
using System.Xml;


public partial class UserControl_TributeBlog : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetBlogs();
    }

    private void SetBlogs()
    {
        string BlogUrl = TributesPortal.Utilities.WebConfig.BlogUrl;
        DateTime defaultDate = DateTime.Parse("1/1/001");
        try
        {
        
        // Create a new XmlTextReader from the specified URL (RSS feed)
        XmlTextReader rssReader = new XmlTextReader(BlogUrl);
        XmlDocument rssDoc = new XmlDocument();
        // Load the XML content into a XmlDocument
        rssDoc.Load(rssReader);



        int nodeCount = 0;

            // Loop for the <rss> tag
            //XmlNode nodeRss;
            StringBuilder strlbul = new StringBuilder();
            strlbul.Append("<ul>");
            for (int i = 0; i < rssDoc.ChildNodes.Count; i++)
            {

                // If it is the rss tag
                if (rssDoc.ChildNodes[i].Name == "rss")
                {
                    //        // <rss> tag found
                    XmlNode nodeRss = rssDoc.ChildNodes[i];
                    for (int j = 0; j < nodeRss.ChildNodes.Count; j++)
                    {

                        //    // If it is the channel tag
                        if (nodeRss.ChildNodes[j].Name == "channel")
                        {
                            // <channel> tag found
                            XmlNode nodeChannel = nodeRss.ChildNodes[j];
                            for (int k = 0; k < nodeChannel.ChildNodes.Count; k++)
                            {
                                XmlNode nodeItem = nodeChannel.ChildNodes[k];
                                // If it is the item tag, then it has children tags which we will add as items to the ListView
                                if (nodeItem.Name == "item")
                                {
                                    if (nodeCount >= 5)
                                    {
                                        strlbul.Append("</ul>");
                                        ltrlBlogs.Text = strlbul.ToString();
                                        return;
                                    }

                                    strlbul.Append("<li>");

                                    string title = nodeItem.ChildNodes[0].InnerText;
                                    //Used the new position of link.
                                    //string link = nodeItem.ChildNodes[1].InnerText;
                                    string link = nodeItem.ChildNodes[4].InnerText;
                                    DateTime _blogdate;
                                    DateTime.TryParse(nodeItem.ChildNodes[3].InnerText, out _blogdate);
                                    if (_blogdate.Equals(defaultDate))
                                        DateTime.TryParse(nodeItem.ChildNodes[4].InnerText, out _blogdate);
                                    strlbul.Append("<a href='" + link + "' target='_blank'>" + title + "</a><br />" + _blogdate.ToString("MMMM dd, yyyy"));
                                    strlbul.Append("</li>");

                                    nodeCount++;
                                }

                            }


                        }
                    }
                }
            }

            strlbul.Append("</ul>");
            ltrlBlogs.Text = strlbul.ToString();
        }
        catch (Exception ex)
        { 
        
        }
    }
}
