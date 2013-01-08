///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.SearchResult.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the search results for tributes
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO; 
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Tribute.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;
using TributePortalSecurity;
#endregion

/// <summary>
///Tribute Portal-SearchResult UI Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the UI class Tribute_SearchResult for showing the Search result ( Advance and Basic Search). This will implement the 
// All the Properties in the ISearchResult interface. and will extend PageBase class which provides 
// 1. Error Event Handler
// 2. Exception handling
/// </summary>
/// 
public partial class Tribute_SearchResult : PageBase, ISearchResult
{

    #region CLASS VARIABLES

    private SearchResultPresenter _presenter;

    private SessionValue _SessionValue = null;
    private SearchTribute _SearchParam = null;

    private int _PageSize = 10;
    private int _CurrentPage = 1;
    private int _TotalRecordCount;
    private int _UserId;
    private string _Message = "";

    #endregion


    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // get the User detail and search Parameter from the Session
            GetValuesFromSession();

            // Set the controls value
            SetControlsValue();

            if (!this.IsPostBack)
            {
                //this._presenter.OnViewInitialized();
                this._presenter.GetTributeTypeList(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower());

                // Search for tribute
                this._presenter.Search(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower());

                SetSelection();

            }

            // Set controls visibility on the basis of search result count
            SetControlsVisibility();

            this._presenter.OnViewLoaded();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlTributeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            StateManager stateManager = StateManager.Instance;
            SearchTribute objSerachParam = (SearchTribute)stateManager.Get(PortalEnums.SearchEnum.Search.ToString(), StateManager.State.Session);
            _CurrentPage = 1;
            if (objSerachParam != null)
            {
                objSerachParam.TributeType = ddlTributeType.SelectedItem.Text;
                stateManager.Add(PortalEnums.SearchEnum.Search.ToString(), objSerachParam, StateManager.State.Session);

                this._presenter.Search(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower());

                SetControlsVisibility();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            StateManager stateManager = StateManager.Instance;
            SearchTribute objSerachParam = (SearchTribute)stateManager.Get(PortalEnums.SearchEnum.Search.ToString(), StateManager.State.Session);
            _CurrentPage = 1;
            if (objSerachParam != null)
            {
                if (ddlSort.SelectedIndex == 0)
                {
                    objSerachParam.SortOrder = PortalEnums.Sorting.DESC.ToString();
                }
                else if (ddlSort.SelectedIndex == 1)
                {
                    objSerachParam.SortOrder = PortalEnums.Sorting.ASC.ToString();
                }

                stateManager.Add(PortalEnums.SearchEnum.Search.ToString(), objSerachParam, StateManager.State.Session);
            }

            if (repSearchTribute.Items.Count > 0)
            {
                this._presenter.Search(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void repSearchTribute_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            LinkButton lnkTributeName = (LinkButton)e.Item.FindControl("lbtnTributeName");
            HiddenField hdnTributeType = (HiddenField)e.Item.FindControl("hdnTributeType");
            HiddenField hdnTributeUrl = (HiddenField)e.Item.FindControl("hdnTributeUrl");

            Tributes objTribute = new Tributes();

            objTribute.TributeName = lnkTributeName.Text;
            objTribute.TypeDescription = hdnTributeType.Value;
            objTribute.TributeId = int.Parse(e.CommandArgument.ToString());

            StateManager stateManager = StateManager.Instance;
            stateManager.Add(PortalEnums.SessionValueEnum.TributeSession.ToString(), objTribute, StateManager.State.Session);

            if (WebConfig.ApplicationMode.Equals("local"))
            {
                //Redirect to the Tribute Home Page
                Response.Redirect(Session["APP_PATH"].ToString() + hdnTributeUrl.Value + "/", false);
            }
            else
            {
                if (objTribute.TypeDescription.Equals("Video"))
                {
                    Response.Redirect("http://" + hdnTributeType.Value + "." + WebConfig.TopLevelDomain + "/video/videotribute.aspx?tributeId=" + objTribute.TributeId);
                }
                else
                {
                    //Use this line for server and comment the line written above
                    Response.Redirect("http://" + hdnTributeType.Value.Replace("New Baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + hdnTributeUrl.Value + "/");
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion


    #region PROPERTIES

    [CreateNew]
    public SearchResultPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    #region ISearchResult Members

    public int TotalRecordCount
    {
        get
        {
            return _TotalRecordCount;
        }

        set
        {
            _TotalRecordCount = value;
        }
    }

    public int PageSize
    {
        get
        {
            return _PageSize;
        }

        set
        {
            _PageSize = value;
        }
    }

    public int CurrentPage
    {
        get
        {
            return _CurrentPage;
        }

        set
        {
            _CurrentPage = value;
        }
    }

    public string RecordCount
    {
        set
        {
            spanHeadRecordCount.InnerText = value;
            spanFootRecordCount.InnerText = value;
        }
    }

    public string DrawPaging
    {
        set
        {
            spanPagingHead.InnerHtml = value;
            spanPagingFoot.InnerHtml = value;
        }
    }

    public string Message
    {
        set
        {
            _Message = value;   
        }
    }

    public SearchTribute SearchParameter 
    {
        get
        {
            return _SearchParam;
        }
    }

    public string SearchType
    {
        get
        {
            return _SearchParam.SearchType;
        }
    }

    public IList<SearchTribute> SearchTributesList
    {
        set
        {
            if (value.Count > 0)
            {
                repSearchTribute.Visible = true;
                repSearchTribute.DataSource = value;
                repSearchTribute.DataBind();
            }
            else
            {
                repSearchTribute.Visible = false;
                repSearchTribute.DataSource = null;
                repSearchTribute.DataBind();
            }
        }
    }

    public IList<Tributes> TributeTypeList
    {
        set
        {
            ddlTributeType.DataSource = value;
            ddlTributeType.DataTextField = "TributeName";
            ddlTributeType.DataValueField = "TributeID";
            ddlTributeType.DataBind();
            ddlTributeType.SelectedIndex = 0;
        }
    }

    #endregion

    #endregion


    #region METHODS

    /// <summary>
    /// This Function will set the value of the control and error messages from the resource File
    /// </summary>
    private void SetControlsValue()
    {
        try
        {
            //Text for labels from the resource file
            lblSearchResult.Text = ResourceText.GetString("lblSearchResult_SR");    // Search Results:
            lblShowMe.Text = ResourceText.GetString("lblShowMe_SR");                // Show Me:
            lblSortBy.Text = ResourceText.GetString("lblSortBy_SR");                // Sort By:

            if (ddlSort.Items.Count == 0)
            {
                ddlSort.Items.Add("Created: Newest"); // ResourceText.GetString("ddlSort_Newest");
                ddlSort.Items.Add("Created: Oldest"); // ResourceText.GetString("ddlSort_Oldest");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// This method will set the selection on the Tribute and sorting dropdown
    /// </summary>
    private void SetSelection()
    {
        if (_SearchParam != null)
        {
            string type = _SearchParam.TributeType + " Tributes";
            ListItem tmpItem = ddlTributeType.Items.FindByText(type);
            if (tmpItem != null)
            {
                ddlTributeType.SelectedValue = (ddlTributeType.Items.IndexOf(tmpItem) + 1).ToString();
            }

            if ((_SearchParam.SortOrder != null) && (_SearchParam.SortOrder != ""))
            {
                if (_SearchParam.SortOrder == PortalEnums.Sorting.ASC.ToString())
                {
                    ddlSort.SelectedIndex = 1;
                }
                else if (_SearchParam.SortOrder == PortalEnums.Sorting.DESC.ToString())
                {
                    ddlSort.SelectedIndex = 0;
                }
            }
        }
    }


    private void WriteLog(string report)
    {
        string LogFile;

        LogFile = "c:\\MyTest.txt";



        FileStream fs = new FileStream(LogFile, FileMode.OpenOrCreate, FileAccess.Write);

        StreamWriter m_streamWriter = new StreamWriter(fs);
        m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);

        m_streamWriter.WriteLine(report + "-" + DateTime.Now.ToString());

        m_streamWriter.Flush();
        fs.Close();
    }

    /// <summary>
    /// This function will get the User Detail and Search Parameter from the session
    /// </summary>
    private void GetValuesFromSession()
    {
        try
        {
            Response.Cache.SetExpires(DateTime.Now);

            // Create State manager instance
            StateManager objStateManager = StateManager.Instance;

            // To get user id from session as user is logged in user
            _SessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);
            if (_SessionValue != null)
            {
                _UserId = _SessionValue.UserId;
            }

          //  string typeName = (string)objStateManager.Get(PortalEnums.SessionValueEnum.SearchPageName.ToString(), StateManager.State.Session);
          //  if ((typeName != null) || (typeName != ""))
           // {
               // SetPageName(typeName);
           // }

            // To get current page number, if user clicks on page number in paging it gets the page number from query string
            // else page number is 1
            if (Request.QueryString["PageNo"] != null)
            {
                _CurrentPage = int.Parse(Request.QueryString["PageNo"].ToString());
            }
            else
            {
                _CurrentPage = 1;
            }

            // To get page size from config file
            _PageSize = (int.Parse(WebConfig.Pagesize_Gift));

            // Get the search Parameter from the session/querystring
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"] != null)
                {
                    if (Request.QueryString["mode"] == "search")
                    {
                        _SearchParam = new SearchTribute();
                        _SearchParam.TributeType = Security.DecryptSymmetric(Request.QueryString["tributetype"].ToString());
                        if (Request.QueryString["searchstring"] != null)
                            _SearchParam.SearchString = Security.DecryptSymmetric(Request.QueryString["searchstring"].ToString());
                        else
                            _SearchParam.SearchString = string.Empty;
                        _SearchParam.SearchType = Security.DecryptSymmetric(Request.QueryString["searchtype"].ToString());
                        _SearchParam.SortOrder = Security.DecryptSymmetric(Request.QueryString["searchorder"].ToString());

                       //Adding Session for paging                        

                        // Create StateManager object and add search paramter in the session
                        //COMDIFFRES: (Search parameters are stored in the session. This might be done due to the security reason) following two lines are commented in .com file                       
                        objStateManager.Add(PortalEnums.SearchEnum.Search.ToString(), _SearchParam, StateManager.State.Session);
                    }
                }
                else
                {                   
                   // WriteLog(Request.Cookies["ASP.NET_SessionId"].Value);                    
                    _SearchParam = (SearchTribute)objStateManager.Get(PortalEnums.SearchEnum.Search.ToString(), StateManager.State.Session);                                     
                 }
            }
            else
                _SearchParam = (SearchTribute)objStateManager.Get(PortalEnums.SearchEnum.Search.ToString(), StateManager.State.Session);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Set Visibility of control on the basis of user right and total number of gifts
    /// </summary>
    private void SetControlsVisibility()
    {
        try
        {

            //to set visibility of comments list. if no record found displays message else displays list.
            if (repSearchTribute.Items.Count == 0)
            {
                divTributeList.Visible = false;

                lblNotice.Attributes.Add("class", "yt-Error");

                PageFoot.Visible = false;
                PageHead.Visible = false;
            }
            else
            {
                divTributeList.Visible = true;

                lblNotice.Attributes.Add("class", "yt-Notice");

                PageFoot.Visible = true;
                PageHead.Visible = true;
            }

            if (_Message != "")
            {
                lblNotice.InnerHtml = _Message;
                lblNotice.Visible = true;
            }
            else
            {
                lblNotice.Visible = false;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// This method is to set the previos page name to move to back
    /// </summary>
    /// <param name="typeName"></param>
    private void SetPageName(string typeName)
    {
       /* if (typeName == PortalEnums.TributeContentEnum.Story.ToString())
        {
            lnkBreadcrumbs.HRef = PortalConstants.STORY_PAGE;
            lnkBreadcrumbs.InnerText = PortalEnums.TributeContentEnum.Story.ToString();
        }
        else if (typeName == PortalEnums.TributeContentEnum.Gift.ToString())
        {
            lnkBreadcrumbs.HRef = PortalConstants.GIFT_PAGE;
            lnkBreadcrumbs.InnerText = PortalEnums.TributeContentEnum.Gift.ToString();
        }
        else if (typeName == PortalEnums.TributeContentEnum.Event.ToString())
        {
            lnkBreadcrumbs.HRef = PortalConstants.GIFT_PAGE;
            lnkBreadcrumbs.InnerText = PortalEnums.TributeContentEnum.Gift.ToString();

            lnkBreadcrumbs.InnerHtml = "<a href='../Tribute/TributeHomePage.aspx'>Tribute Home</a>";
            lnkBreadcrumbs.InnerHtml += "<a href='.." + PortalConstants.EVENT_PAGE + "'>Events</a>";
            lnkBreadcrumbs.InnerHtml += "<span class='selected'>Search Results </span>";
        }
        else if (typeName == "Notes")
        {
            lnkBreadcrumbs.HRef = PortalConstants.GIFT_PAGE;
            lnkBreadcrumbs.InnerText = PortalEnums.TributeContentEnum.Gift.ToString();

            lnkBreadcrumbs.InnerHtml = "<a href='../Tribute/TributeHomePage.aspx'>Tribute Home</a>";
            lnkBreadcrumbs.InnerHtml += "<a href='../Notes/TributeNotes.aspx'>Notes</a>";
            lnkBreadcrumbs.InnerHtml += "<span class='selected'>Search Results </span>";
        }
        else if (typeName == "Video")
        {
            lnkBreadcrumbs.HRef = PortalConstants.GIFT_PAGE;
            lnkBreadcrumbs.InnerText = PortalEnums.TributeContentEnum.Gift.ToString();

            lnkBreadcrumbs.InnerHtml = "<a href='../Tribute/TributeHomePage.aspx'>Tribute Home</a>";
            lnkBreadcrumbs.InnerHtml += "<a href='../Video/VideoGallery.aspx'>Video</a>";
            lnkBreadcrumbs.InnerHtml += "<span class='selected'>Search Results </span>";
        }
        else if (typeName == "Photo")
        {
            lnkBreadcrumbs.HRef = PortalConstants.GIFT_PAGE;
            lnkBreadcrumbs.InnerText = PortalEnums.TributeContentEnum.Gift.ToString();

            lnkBreadcrumbs.InnerHtml = "<a href='../Tribute/TributeHomePage.aspx'>Tribute Home</a>";
            lnkBreadcrumbs.InnerHtml += "<a href='../Photo/PhotoGallery.aspx'>Photo</a>";
            lnkBreadcrumbs.InnerHtml += "<span class='selected'>Search Results </span>";
        }
        else if (typeName == "Guestbook")
        {
            lnkBreadcrumbs.HRef = PortalConstants.GIFT_PAGE;
            lnkBreadcrumbs.InnerText = PortalEnums.TributeContentEnum.Gift.ToString();

            lnkBreadcrumbs.InnerHtml = "<a href='../Tribute/TributeHomePage.aspx'>Tribute Home</a>";
            lnkBreadcrumbs.InnerHtml += "<a href='../Guestbook/Guestbook.aspx'>Guestbook</a>";
            lnkBreadcrumbs.InnerHtml += "<span class='selected'>Search Results </span>";
        }
        else if (typeName == "TributeHomePage")
        {
            lnkBreadcrumbs.HRef = "../Tribute/TributeHomePage.aspx";
            lnkBreadcrumbs.InnerText = "Tribute Home";
        }
        else if (typeName == "AdminMyfavorites")
        {
            lnkBreadcrumbs.HRef = "../MyHome/AdminMyfavorites.aspx";
            lnkBreadcrumbs.InnerText = "My Favorites";
        }
        else if (typeName == "AdminMytributesHome")
        {
            lnkBreadcrumbs.HRef = "../MyHome/AdminMytributesHome.aspx";
            lnkBreadcrumbs.InnerText = "My Tributes";
        }
        else if (typeName == "AdminMytributesPrivacy")
        {
            lnkBreadcrumbs.HRef = "../MyHome/AdminMytributesPrivacy.aspx";
            lnkBreadcrumbs.InnerText = "Tributes Privacy Setting";
        }
        else if (typeName == "AdminProfileBilling")
        {
            lnkBreadcrumbs.HRef = "../MyHome/AdminProfileBilling.aspx";
            lnkBreadcrumbs.InnerText = "Billing";
        }
        else if (typeName == "UserInbox")
        {
            lnkBreadcrumbs.HRef = "../MyHome/UserInbox.aspx";
            lnkBreadcrumbs.InnerText = "Inbox";
        }
        else if (typeName == "UserEvents")
        {
            lnkBreadcrumbs.HRef = "../MyHome/UserEvents.aspx";
            lnkBreadcrumbs.InnerText = "My Events";
        }
        else if (typeName == "AdminProfileSettings")
        {
            lnkBreadcrumbs.HRef = "../MyHome/AdminProfileSettings.aspx";
            lnkBreadcrumbs.InnerText = "Profile Settings";
        }
        else if (typeName == "AdminProfilePrivacy")
        {
            lnkBreadcrumbs.HRef = "../MyHome/AdminProfilePrivacy.aspx";
            lnkBreadcrumbs.InnerText = "Profile Privacy";
        }
        else if (typeName == "AdminProfileEmailpassword")
        {
            lnkBreadcrumbs.HRef = "../MyHome/AdminProfileEmailpassword.aspx";
            lnkBreadcrumbs.InnerText = "Change Email/password";
        }
        else if (typeName == "AdminProfileEmail")
        {
            lnkBreadcrumbs.HRef = "../MyHome/AdminProfileEmail.aspx";
            lnkBreadcrumbs.InnerText = "Email Notification";
        } */
        
    }

    #endregion
    
}


