///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Utilities.CommonUtilities.cs
///Author          : 
///Creation Date   : 
///Description     : This file is used to declare methods common to the entire application
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Collections;
using System.Web;
//using System.Web.SessionState;
//using System.Web.Security;
//using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using System.Xml;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Utilities
{
    public class CommonUtilities
    {
        #region PAGING METHODS
        /// <summary>
        /// Method to draw paging
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageCount">Total number of pages</param>
        /// <returns>string containing paging</returns>
        public string DrawPaging(int pageNumber, int pageCount)
        {
            StringBuilder sb = new StringBuilder();
            int x, y, z, pageEnd, pageStart;
            int pagingSize = int.Parse(WebConfig.PagingSize_guestBook);
            if (pageCount == 1)
            {
                sb.Append("<strong>&nbsp;&nbsp;&nbsp;");
                sb.Append(pageCount);
                sb.Append("</strong>&nbsp;&nbsp;");
            }
            if (pageCount > 1)
            {
                if (pageNumber % pagingSize == 0)
                {
                    pageStart = pageNumber - (pagingSize - 1);
                }
                else
                {
                    y = pageNumber.ToString().Length;
                    if (y == 1)
                    {
                        pageStart = 1;
                    }
                    else
                    {
                        z = int.Parse(pageNumber.ToString().Substring(0, y - 1));
                        pageStart = (z * pagingSize) + 1;
                    }
                }
                if (pageStart + (pagingSize - 1) > pageCount)
                {
                    pageEnd = pageCount;
                }
                else
                {
                    pageEnd = pageStart + (pagingSize - 1);
                }

                if (pageStart > pagingSize)
                {
                    sb.Append("<a href=");
                    sb.Append(DrawLink(pageStart - 1));
                    sb.Append(">Previous </a>&nbsp;&nbsp;&nbsp;&nbsp;");
                    //sb.Append(">Previous 10</a>&nbsp;&nbsp;&nbsp;&nbsp;");
                }

                if (pageStart != 1)
                {
                    //sb.Append("<a href=");
                    //sb.Append(DrawLink(pageNumber - 1));
                    //sb.Append("><<</a>&nbsp;&nbsp;");
                }
                else
                {
                    sb.Append("&nbsp;&nbsp;");
                }

                for (x = pageStart; x <= pageEnd; x++)
                {
                    if (x == pageNumber)
                    {
                        //sb.Append("<strong>[");
                        sb.Append("<strong>");
                        sb.Append(x);
                        sb.Append("</strong>&nbsp;&nbsp;");
                        //sb.Append("]</strong>&nbsp;&nbsp;");
                    }
                    else
                    {
                        sb.Append("<a href=");
                        sb.Append(DrawLink(x));
                        sb.Append(">");
                        sb.Append(x);
                        sb.Append("</a>&nbsp;&nbsp;");
                    }
                }
                if (pageNumber < pageCount)
                {
                    //sb.Append("<a href=");
                    //sb.Append(DrawLink(pageNumber + 1));
                    //sb.Append(">></a>");
                }
                else
                {
                    sb.Append("&nbsp;&nbsp;");
                }
                if (pageEnd < pageCount)
                {
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;<a href=");
                    sb.Append(DrawLink(pageEnd + 1));
                    sb.Append(">Next</a>");
                    //sb.Append(">Next 10</a>");
                }

                //sb.Append("<small><br>Total Page Count: ");
                //sb.Append(pageCount);
                //sb.Append("</small>");
                //sb.Append("</CENTER>");
                return sb.ToString();
            }
            return sb.ToString();
        }

        public string DrawPaging(int pageNumber, int pageCount, string pagePath)
        {
            StringBuilder sb = new StringBuilder();
            int x, y, z, pageEnd, pageStart;
            int pagingSize = int.Parse(WebConfig.PagingSize_guestBook);
            if (pageCount == 1)
            {
                sb.Append("<strong>&nbsp;&nbsp;&nbsp;");
                sb.Append(pageCount);
                sb.Append("</strong>&nbsp;&nbsp;");
            }
            if (pageCount > 1)
            {
                if (pageNumber % pagingSize == 0)
                {
                    pageStart = pageNumber - (pagingSize - 1);
                }
                else
                {
                    y = pageNumber.ToString().Length;
                    if (y == 1)
                    {
                        pageStart = 1;
                    }
                    else
                    {
                        z = int.Parse(pageNumber.ToString().Substring(0, y - 1));
                        pageStart = (z * pagingSize) + 1;
                    }
                }
                if (pageStart + (pagingSize - 1) > pageCount)
                {
                    pageEnd = pageCount;
                }
                else
                {
                    pageEnd = pageStart + (pagingSize - 1);
                }

                if (pageStart > pagingSize)
                {
                    sb.Append("<a href=");
                    sb.Append(DrawLink(pageStart - 1, pagePath));
                    sb.Append(">Previous </a>&nbsp;&nbsp;&nbsp;&nbsp;");
                    //sb.Append(">Previous 10</a>&nbsp;&nbsp;&nbsp;&nbsp;");
                }

                if (pageStart != 1)
                {
                    //sb.Append("<a href=");
                    //sb.Append(DrawLink(pageNumber - 1));
                    //sb.Append("><<</a>&nbsp;&nbsp;");
                }
                else
                {
                    sb.Append("&nbsp;&nbsp;");
                }

                for (x = pageStart; x <= pageEnd; x++)
                {
                    if (x == pageNumber)
                    {
                        //sb.Append("<strong>[");
                        sb.Append("<strong>");
                        sb.Append(x);
                        sb.Append("</strong>&nbsp;&nbsp;");
                        //sb.Append("]</strong>&nbsp;&nbsp;");
                    }
                    else
                    {
                        sb.Append("<a href=");
                        sb.Append(DrawLink(x, pagePath));
                        sb.Append(">");
                        sb.Append(x);
                        sb.Append("</a>&nbsp;&nbsp;");
                    }
                }
                if (pageNumber < pageCount)
                {
                    //sb.Append("<a href=");
                    //sb.Append(DrawLink(pageNumber + 1));
                    //sb.Append(">></a>");
                }
                else
                {
                    sb.Append("&nbsp;&nbsp;");
                }
                if (pageEnd < pageCount)
                {
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;<a href=");
                    sb.Append(DrawLink(pageEnd + 1, pagePath));
                    sb.Append(">Next</a>");
                    //sb.Append(">Next 10</a>");
                }

                //sb.Append("<small><br>Total Page Count: ");
                //sb.Append(pageCount);
                //sb.Append("</small>");
                //sb.Append("</CENTER>");
                return sb.ToString();
            }
            return sb.ToString();
        }

        /// <summary>
        /// Method to add query string in paging links
        /// </summary>
        /// <param name="pageNumber">Current Page number</param>
        /// <returns>string querystring</returns>
        private string DrawLink(int pageNumber)
        {
            return "?PageNo=" + pageNumber;
        }

        private string DrawLink(int pageNumber, string pagePath)
        {
            return "\"" + pagePath + "?PageNo=" + pageNumber + "\"";
        }
        #endregion


        /// <summary>
        /// This function is used to get the path for photos to get and save.
        /// </summary>
        /// <returns>Array of string containing drive name, root folder name and photo virtual directory name.</returns>
        public static string[] GetPath()
        {
            string strXmlPath = AppDomain.CurrentDomain.BaseDirectory + "Common\\XML\\PhotoConfiguration.xml";

            FileStream docIn = new FileStream(strXmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            XmlDocument contactDoc = new XmlDocument();
            //Load the Xml Document
            contactDoc.Load(docIn);

            //Get a node
            XmlNodeList driveName = contactDoc.GetElementsByTagName("DriveNameOnServiceServer");
            XmlNodeList rootFolderName = contactDoc.GetElementsByTagName("PhotoRootFolder");
            XmlNodeList photoVirtualDirectory = contactDoc.GetElementsByTagName("PhotoVirtualDirectory");
            XmlNodeList photoThumbnailDirectory = contactDoc.GetElementsByTagName("ThumbnailFolder");
            XmlNodeList photoXmlFileDirectory = contactDoc.GetElementsByTagName("XMLFileFolder");
            XmlNodeList EventFolder = contactDoc.GetElementsByTagName("EventFolder");
            XmlNodeList StoryFolder = contactDoc.GetElementsByTagName("StoryFolder");
            XmlNodeList TempFolder = contactDoc.GetElementsByTagName("TempFolder");
            XmlNodeList CompanyFolder = contactDoc.GetElementsByTagName("CompanyLogoFolder");
            XmlNodeList SSLPhotoVirtualDirectory = contactDoc.GetElementsByTagName("SSLPhotoVirtualDirectory");
            XmlNodeList HeaderLogo = contactDoc.GetElementsByTagName("HeaderLogoFolder");


            string[] getPath = { driveName.Item(0).InnerText, rootFolderName.Item(0).InnerText, photoVirtualDirectory.Item(0).InnerText, 
                photoThumbnailDirectory.Item(0).InnerText, photoXmlFileDirectory.Item(0).InnerText, EventFolder.Item(0).InnerText, 
                TempFolder.Item(0).InnerText, StoryFolder.Item(0).InnerText, CompanyFolder.Item(0).InnerText,SSLPhotoVirtualDirectory.Item(0).InnerText,HeaderLogo.Item(0).InnerText };

            //get the value
            return getPath;
        }

        /// <summary>
        /// Method to get the paths for video tribute
        /// </summary>
        /// <returns>string array containing data of all nodes in video configuration.</returns>
        public static string[] GetVideoTributePath()
        {
            string strXmlPath = AppDomain.CurrentDomain.BaseDirectory + "Common\\XML\\VideoConfiguration.xml";

            FileStream docIn = new FileStream(strXmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            XmlDocument contactDoc = new XmlDocument();
            //Load the Xml Document
            contactDoc.Load(docIn);

            //Get a node
            XmlNodeList srcPath = contactDoc.GetElementsByTagName("SourceVideoTributePath");
            XmlNodeList destPath = contactDoc.GetElementsByTagName("DestinationVideoTributePath");
            XmlNodeList virtualPath = contactDoc.GetElementsByTagName("VideoVirtualDirectory");
            string[] getPath = { srcPath.Item(0).InnerText, destPath.Item(0).InnerText, virtualPath.Item(0).InnerText };

            //get the value
            return getPath;
        }

        /// <summary>
        /// Method to create the xml data for xml file.
        /// </summary>
        /// <param name="objImageList">List of photo images</param>
        /// <param name="path">path from where the photos to be picked</param>
        /// <param name="albumCaption">Album caption</param>
        /// <param name="albumDesc">Album Description</param>
        /// <returns>Xml data in string format.</returns>
        public string GetXmlData(List<Photos> objImageList, string path, string albumCaption, string albumDesc)
        {
            string[] getPath = CommonUtilities.GetPath();

            StringBuilder sb = new StringBuilder();

            sb.Append("<?xml version='1.0' encoding='UTF-8' ?>");
            sb.Append("<gallery>");
            //sb.Append("<album title='" + albumCaption + "' description='" + albumDesc + "' ");
            sb.Append("<album title='' description='' ");
            sb.Append("lgpath='" + path);
            foreach (Photos obj in objImageList)
            {
                sb.Append("<img src='" + obj.PhotoImage + "' />");
            }
            sb.Append("</album>");
            sb.Append("</gallery>");

            return sb.ToString();
        }

        /// <summary>
        /// Method to get Html for Affiliate links
        /// </summary>
        /// <param name="tributeType">Tribute Type</param>
        /// <returns>Html in string format</returns>
        public string AffiliateLinks(string tributeType)
        {
            StringBuilder sbLink = new StringBuilder();
            sbLink.Append("<dl class=\"yt-LinkList\">");
            sbLink.Append("<dt>Flowers</dt>");
            sbLink.Append("<dd><a href=\"http://click.linksynergy.com/fs-bin/click?id=jDnbmfmVD24&offerid=100462.10000286&type=3&subid=0\">www.1800flowers.com</a></dd>");
            sbLink.Append("<dt>Gift Baskets</dt>");
            sbLink.Append("<dd><a href=\"http://click.linksynergy.com/fs-bin/click?id=jDnbmfmVD24&offerid=99970.10000075&type=3&subid=0\">www.giftbaskets.com</a></dd>");
            sbLink.Append("<dt>Personalized Gifts</dt>");
            sbLink.Append("<dd><a href=\"http://click.linksynergy.com/fs-bin/click?id=jDnbmfmVD24&offerid=130019.10000050&type=3&subid=0\">www.giftsforyounow.com</a></dd>");
            sbLink.Append("</dl>");

            //sbLink.Append("<a href=\"http://click.linksynergy.com/fs-bin/click?id=jDnbmfmVD24&offerid=100462.10000286&type=3&subid=0\" target=\"new\">1-800-Flowers.com</a><IMG border=0 width=1 height=1 src=\"http://ad.linksynergy.com/fs-bin/show?id=jDnbmfmVD24&bids=100462.10000286&type=3&subid=0\">");
            //sbLink.Append("<a href=\"http://click.linksynergy.com/fs-bin/click?id=jDnbmfmVD24&offerid=99970.10000075&type=3&subid=0\" target=\"new\" >GiftBaskets.com</a><IMG border=0 width=1 height=1 src=\"http://ad.linksynergy.com/fs-bin/show?id=jDnbmfmVD24&bids=99970.10000075&type=3&subid=0\" >");
            //sbLink.Append("<a href=\"http://click.linksynergy.com/fs-bin/click?id=jDnbmfmVD24&offerid=130019.10000050&type=3&subid=0\" target=\"new\" >GiftsForYouNow.com</a><IMG border=0 width=1 height=1 src=\"http://ad.linksynergy.com/fs-bin/show?id=jDnbmfmVD24&bids=130019.10000050&type=3&subid=0\" >");

            return sbLink.ToString();
        }

        /// <summary>
        /// to create cookies once the Welcome message is displayed and closed
        /// </summary>
        /// <param name="userId"></param>
        public void CreateCookie(int userId)
        {
            string cookieName = string.Empty;
            if (userId != 0)
                cookieName = userId.ToString() + "cookie";
            else
                cookieName = "Timeless" + "cookie";

            //set to true to indicate the Welcome message modal has been displayed once
            HttpCookie aCookie = new HttpCookie(cookieName);
            aCookie.Value = "true";
            aCookie.Expires = DateTime.Now.AddDays(180);
            aCookie.Domain = "." + WebConfig.TopLevelDomain;

            HttpContext.Current.Response.Cookies.Add(aCookie);

        }

        /// <summary>
        /// to read a cookie and check if the cookie exists already.
        /// if the cookie exists then the modal message has been displayed once
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>bool=true when message displayed once 
        /// bool=false when message has not been displayed at all</returns>
        public bool ReadCookie(int userId)
        {
            string cookieName = string.Empty;
            bool result = false;
            if (userId != 0)
            {
                cookieName = userId.ToString() + "cookie";
                HttpCookie Cookie = HttpContext.Current.Request.Cookies[cookieName];
                if (Cookie != null || HttpContext.Current.Request.Cookies["Timeless" + "cookie"] != null)
                {
                    if (Cookie != null)
                        result = Convert.ToBoolean(Cookie.Value);
                    else
                        result = Convert.ToBoolean(HttpContext.Current.Request.Cookies["Timeless" + "cookie"].Value);
                }

            }
            else
            {
                cookieName = "Timeless" + "cookie";
                HttpCookie Cookie = HttpContext.Current.Request.Cookies[cookieName];
                if (Cookie != null)
                {
                    result = Convert.ToBoolean(Cookie.Value);
                }
            }
            return result;
        }
    }
}

