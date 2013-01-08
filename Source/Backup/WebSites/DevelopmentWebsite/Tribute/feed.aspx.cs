using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.MyHome;
//using TributesPortal.ResourceAccess;
using TributesPortal.Tribute.Views;

public partial class Tribute_feed : System.Web.UI.Page, IFeed
{
    #region Private variables
    private FeedPresenter _presenter;
    string _tributeHomeUrl = string.Empty;
    string _tributeImageUrl = string.Empty;
    IList<GetTributesFeed> _objTributeList = null;
    static IList<ParameterTypesCodes> _TributeTypes;
    static string tributeType = string.Empty;
    DateTime FeedUpdatedDate;
    //string DOB;
    //string DOD;
    string strDates = string.Empty;
    int userId = 0;

    string _tributeName = string.Empty;
    private int _tributeTypeId = 7;
    private int _businessUserId = 0;
    private int _currentPage = 1;
    private int _pageSize = 10;
    private int _totalObituaries = 0;
    

    [CreateNew]
    public FeedPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    public IList<ParameterTypesCodes> TributeTypes
    {
        set
        {
            _TributeTypes = value;
        }
    }
    public IList<GetTributesFeed> ObjTributeList
    {
        get
        {
            return _objTributeList;
        }
        set
        {
            _objTributeList = value;
        }
    }
    public string TributeHomeUrl
    {
        get
        {
            return _tributeHomeUrl;
        }
        set
        {
            _tributeHomeUrl = value;
        }
    }

    public string TributeImageUrl
    {
        get
        {
            return _tributeImageUrl;
        }
        set
        {
            _tributeImageUrl = value;
        }
    }
    #endregion
    //private MyHomeController _controller;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        //UserInfoResource objUserInfo = new UserInfoResource();
        GetTributesFeed objtribute = new GetTributesFeed();
        Users objUser = new Users();
        
        if (Request.QueryString["userid"] != null)
        {
            #region Feed on UserId
            int.TryParse(Request.QueryString["userid"].Trim(), out userId);
            objtribute.UserId = userId;

            if (userId > 0)
            {
                objUser = _presenter.GetUserDetailsOnUserId(userId);
            }
            if ((objUser.UserType == 2) && (objUser.AtomEnabled.Equals(true)))
            {
                object[] param = { objtribute, 1, 1, 1000 };
                if (objtribute.CustomError == null)
                {
                    _presenter.GetTributesFeed(param);
                    foreach (GetTributesFeed tribute in _objTributeList)
                    {
                        //DateTime TempModifiedDate = new DateTime();
                        if (tribute.ModifiedDate != null)
                        {
                            if (tribute.ModifiedDate > FeedUpdatedDate)
                                FeedUpdatedDate = tribute.ModifiedDate;
                        }
                    }
                    //objTributeList = objUserInfo.GetMyTributes(param);
                }
                GenerateFeed(_businessUserId ,userId, ObjTributeList);
            }
            #endregion
        }
        else
        {
            // for YourTributeFeed
            #region YourTributeFeed
            //fetching TributeName from Query string for search
            if (Request.QueryString["Search"] != null)
            {
                //to send null in case of no bussiness user id in url.
                int? BussUserId = null;
                if (Request.QueryString["BusinessUserId"] != null)
                    int.TryParse(Request.QueryString["BusinessUserId"].ToString(), out _businessUserId);
                if (_businessUserId == 0 || _businessUserId < 0)
                    _businessUserId = 0;
                else
                    BussUserId = _businessUserId;
                //fetching CurrentPage from Query string for search
                if (Request.QueryString["Start"] != null)
                    int.TryParse(Request.QueryString["Start"].ToString(), out _currentPage);
                if (_currentPage < 0)
                    _currentPage = 0;

                //fetching PageSize from Query string for search
                if (Request.QueryString["PageSize"] != null)
                    int.TryParse(Request.QueryString["PageSize"].ToString(), out _pageSize);
                if (_pageSize < 0)
                    _pageSize = 0;

            //lhk: convert full text search into keyword search.
                _tributeName = "%" + Request.QueryString["Search"].ToString() + "%";
                object[] objprm = { _tributeName, _tributeTypeId, BussUserId, _currentPage, _pageSize };
                _presenter.GetYourTributeFeedOnTributeName(objprm);
                GenerateFeed(_businessUserId,userId, ObjTributeList);
            }
            else
            {

                //fetching BusinessUserId from Query string for search
                if (Request.QueryString["BusinessUserId"] != null)
                    int.TryParse(Request.QueryString["BusinessUserId"].ToString(), out _businessUserId);
                if (_businessUserId == 0 || _businessUserId<0 )
                    _businessUserId = 0;

                //fetching CurrentPage from Query string for search
                if (Request.QueryString["Start"] != null)
                    int.TryParse(Request.QueryString["Start"].ToString(), out _currentPage);
                //if (_currentPage == 0)
                //    _currentPage = 0;
                if (_currentPage < 0)
                    _currentPage = 0;

                //fetching PageSize from Query string for search
                if (Request.QueryString["PageSize"] != null)
                    int.TryParse(Request.QueryString["PageSize"].ToString(), out _pageSize);
                //if (_pageSize == 0 )
                //    _pageSize = 0;
                if ( _pageSize < 0 )
                    _pageSize = 0;

                object[] objparam = { _businessUserId, _currentPage, _pageSize };
                _presenter.GetYourTributesFeed(objparam);
                GenerateFeed(_businessUserId, userId, ObjTributeList);
            }

            #endregion
        }
    }

    private void GenerateFeed( int _businessUserId ,int userId, IList<GetTributesFeed> ObjTributeList)
    {
        string[] virtualDir = CommonUtilities.GetPath();
        DateTime DOB = new DateTime();
        DateTime DOD = new DateTime();

        Response.Clear();
        Response.ContentType = "text/xml";
        XmlTextWriter xmlTextObject = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
        xmlTextObject.WriteStartDocument();
        xmlTextObject.WriteStartElement("feed");
        #region Feed
        xmlTextObject.WriteAttributeString("xmlns", "http://www.w3.org/2005/Atom");
        xmlTextObject.WriteElementString("title", "Your Tribute feed");

        xmlTextObject.WriteStartElement("link");
        xmlTextObject.WriteAttributeString("rel", "self");
        xmlTextObject.WriteAttributeString("type", "application/atom+xml");
        xmlTextObject.WriteAttributeString("href", "http://www.yourtribute.com/tribute/atom.xml");
        xmlTextObject.WriteEndElement();

        xmlTextObject.WriteStartElement("link");
        xmlTextObject.WriteAttributeString("rel", "alternate");
        xmlTextObject.WriteAttributeString("type", "text/html");
        xmlTextObject.WriteAttributeString("href", "http://www.yourtribute.com/tribute/");
        xmlTextObject.WriteEndElement();

        xmlTextObject.WriteElementString("id", "http://www.yourtribute.com");

        xmlTextObject.WriteElementString("published", DateTime.Now.ToString());

        xmlTextObject.WriteElementString("updated", FeedUpdatedDate.ToString());

        xmlTextObject.WriteStartElement("author");
        xmlTextObject.WriteElementString("name", "YourTribute");
        xmlTextObject.WriteEndElement();

        if ((_businessUserId > 0) && (Request.QueryString["Search"] != null))
        {
            _tributeName = "%" + Request.QueryString["Search"].ToString() + "%";
            object[] objprm = { _tributeName, _tributeTypeId, _businessUserId };
            _totalObituaries = _presenter.GetTotalActiveObituariesOnTributeName(objprm);
            xmlTextObject.WriteElementString("TotalActiveObitCount", _totalObituaries.ToString());
        }
        else if (_businessUserId > 0)
        {
            _totalObituaries = _presenter.GetTotalActiveObituaries(_businessUserId);
            xmlTextObject.WriteElementString("TotalActiveObitCount", _totalObituaries.ToString());
        }
        else if (Request.QueryString["Search"] != null)
        {
            _tributeName = "%" + Request.QueryString["Search"].ToString() + "%";
            object[] objprm = { _tributeName, _tributeTypeId, null };
            _totalObituaries = _presenter.GetTotalActiveObituariesOnTributeName(objprm);
            xmlTextObject.WriteElementString("TotalActiveObitCount", _totalObituaries.ToString());
        }

        xmlTextObject.WriteElementString("refreshRate", "1000");
        if (userId > 0)
        {
            #region loop for multiple entry
            foreach (GetTributesFeed tribute in _objTributeList)
            {
                strDates = string.Empty;
                string strObituaryText = string.Empty;
                //_tributeURL = "http://" + objVal.TypeDescription.ToLower() + "." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl;
                //_tributeHomeUrl = WebConfig.AppBaseDomain + tribute.TributeUrl + "/";
                _tributeHomeUrl = "http://" + tribute.TypeDescription.ToLower() + "." + WebConfig.TopLevelDomain + "/" + tribute.TributeUrl + "/";
                _tributeImageUrl = virtualDir[2] + tribute.TributeImage.ToString();
                if (!(string.IsNullOrEmpty(tribute.DOB)))
                {
                    DOB = DateTime.Parse(tribute.DOB.ToString());
                    strDates = DOB.ToShortDateString() + " - ";
                }
                if (!(string.IsNullOrEmpty(tribute.DOD)))
                {
                    DOD = DateTime.Parse(tribute.DOD);
                    strDates = strDates + DOD.ToShortDateString();
                    //strDates = strDates + DOD.ToString("dd MMMM yyyy");
                }
                strObituaryText = CleanMessage(tribute.MessageWithoutHtml.ToString());
                if (strObituaryText.Length > 250)
                {
                    strObituaryText = strObituaryText.Substring(0, 250) + "...";
                }

                #region item entry tag
                xmlTextObject.WriteStartElement("entry");
                xmlTextObject.WriteElementString("title", tribute.TributeName);
                xmlTextObject.WriteElementString("published", tribute.DOD.ToString());
                if (tribute.ModifiedDate != null)
                {
                    xmlTextObject.WriteElementString("updated", tribute.ModifiedDate.ToString());
                }
                else
                {
                    xmlTextObject.WriteElementString("updated", tribute.StartDate.ToString());
                }

                xmlTextObject.WriteStartElement("catagory");
                xmlTextObject.WriteAttributeString("term", "Obituaries");
                xmlTextObject.WriteEndElement();

                xmlTextObject.WriteElementString("id", tribute.TributeId.ToString());

                xmlTextObject.WriteStartElement("summary");
                //xmlTextObject.WriteStartElement("summary", "<![CDATA[<div><img width='120' height='120' src=" + _tributeImageUrl + " alt='memorial_TributePhoto.jpg' title='memorial_TributePhoto.jpg' style='float:left;'/><span style='margin-left:150px;'><p>" + strDates + "</p>" + strObituaryText);
                xmlTextObject.WriteAttributeString("type", "html");
                xmlTextObject.WriteCData("<img height='120' src='" + _tributeImageUrl + "' alt='memorial_TributePhoto.jpg' title='memorial_TributePhoto.jpg' style='float:left; margin-right:20px;'/><span style='margin-left:150px;'><p>" + strDates + "</p>" + strObituaryText);
                xmlTextObject.WriteEndElement();

                xmlTextObject.WriteStartElement("content");
                //xmlTextObject.WriteElementString("content", "<![CDATA[<p><iframe src=" + _tributeHomeUrl + " name='frame1' scrolling='auto' frameborder='no' align='center' height = '1100px' width = '962px'><br /></iframe></p>]]>");
                xmlTextObject.WriteCData("<p><center><iframe src=" + _tributeHomeUrl + " name='frame1' scrolling='auto' frameborder='no' align='center' height = '1100px' width = '962px'><br /></iframe></center></p>");
                xmlTextObject.WriteEndElement();

                xmlTextObject.WriteEndElement();
                #endregion
                //}
            }
            #endregion
        }
        else
        {
            # region for each entry
            foreach (GetTributesFeed tribute in _objTributeList)
            {
                strDates = string.Empty;
                string strObituaryText = string.Empty;
                _tributeHomeUrl = "http://" + tribute.TypeDescription.ToLower() + "." + WebConfig.TopLevelDomain + "/" + tribute.TributeUrl + "/";
                _tributeImageUrl = virtualDir[2] + tribute.TributeImage.ToString();
                if (!(string.IsNullOrEmpty(tribute.DOB)))
                {
                    DOB = DateTime.Parse(tribute.DOB.ToString());
                    strDates = DOB.ToShortDateString() + " - ";
                }
                if (!(string.IsNullOrEmpty(tribute.DOD)))
                {
                    DOD = DateTime.Parse(tribute.DOD);
                    strDates = strDates + DOD.ToShortDateString();
                    //strDates = strDates + DOD.ToString("dd MMMM yyyy");
                }
                strObituaryText = CleanMessage(tribute.MessageWithoutHtml.ToString());

                #region item entry tag
                xmlTextObject.WriteStartElement("entry");
                xmlTextObject.WriteElementString("PersonsName", tribute.TributeName);
                xmlTextObject.WriteElementString("PersonsPhoto", _tributeImageUrl);
                xmlTextObject.WriteElementString("ObituaryText", strObituaryText);
                xmlTextObject.WriteElementString("DeathDate", tribute.DOD.ToString());
                xmlTextObject.WriteElementString("TributeLink", _tributeHomeUrl);
                xmlTextObject.WriteElementString("FuneralHomeIdentifier", tribute.UserId.ToString());
                xmlTextObject.WriteEndElement();
                #endregion

            }
            #endregion
        }
        #endregion
        xmlTextObject.WriteEndElement();
        //xmlTextObject.WriteEndElement();
        xmlTextObject.WriteEndDocument();
        xmlTextObject.Flush();
        xmlTextObject.Close();
        Response.End();
    } 
    #endregion

    #region methods
    private string CleanMessage(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            if (message.Contains("&nbsp;"))
                message = message.Replace("&nbsp;", " ");
            if (message.Contains("&quot;"))
                message = message.Replace("&quot;", "\'");
            if (message.Contains("&amp;"))
                message = message.Replace("&amp;", "&");
            if (message.Contains("&gt;"))
                message = message.Replace("&gt;", ">");
            if (message.Contains("&lt;"))
                message = message.Replace("&lt;", "<");
            if (message.Contains("&#33;"))
                message = message.Replace("&#33;", "!");
            if (message.Contains("&#34;"))
                message = message.Replace("&lt;", "\"");
            if (message.Contains("&#35;"))
                message = message.Replace("&#35;", "#");
            if (message.Contains("&#36;"))
                message = message.Replace("&#36;", "$");
            if (message.Contains("&#37;"))
                message = message.Replace("&#37;", "%");
            if (message.Contains("&#40;"))
                message = message.Replace("&#40;", "(");
            if (message.Contains("&#41;"))
                message = message.Replace("&#41;", ")");

            while (message.Contains("&nbsp;&nbsp;"))
            {
                message = message.Replace("&nbsp;&nbsp;", "&nbsp;");
                message = message.Replace("&nbsp; &nbsp;", "&nbsp;");
            }

            if (message.Contains("\r"))
                message = message.Replace("\r", " ");
            if (message.Contains("\n"))
                message = message.Replace("\n", " ");

            while (message.Contains("  "))
            {
                message = message.Replace("  ", " ");
            }
        }
        return message;
    } 
	#endregion
}
