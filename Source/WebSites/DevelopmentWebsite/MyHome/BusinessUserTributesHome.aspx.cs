///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Myhome.BusinessUserHome.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the tributes by a particular business user
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
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.MyHome.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Users;

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

public partial class MyHome_BusinessUserTributesHome : PageBase, IBusinessUserHome
{
    #region CLASS VARIABLES

    private BusinessUserHomePresenter _presenter;
    private SessionValue _SessionValue = null;

    private int _PageSize = 10;
    private int _CurrentPage = 1;
    private int _TotalRecordCount;
    private int _UserId;
    private int _UserType;
    protected string _UserName;
    protected string _BusinessUserName;
    private string _Message = "";
    protected string _ShowMapParam = "";
    protected string companyname = "";
    protected string _emailId = "";
    //private string _videoTributeId = string.Empty;
    #endregion


    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Get the User detail
            GetValuesFromSession();

            // Set the controls value
            SetControlsValue();

            if (!this.IsPostBack)
            {
                this._presenter.OnViewInitialized();

                // Get tribute listing
                this._presenter.GetBusinessUserTributeList();
                if (_UserType != 2)
                {
                    SetUserControlsVisibility();
                    return;
                }
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
            // Get tribute listing
            _CurrentPage = 1;
            this._presenter.GetBusinessUserTributeList();

            SetControlsVisibility();
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
            if (repSearchTribute.Items.Count > 0)
            {
                // Get tribute listing
                this._presenter.GetBusinessUserTributeList();

                SetControlsVisibility();
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
            HiddenField hdnVideoTributeId = (HiddenField)e.Item.FindControl("hdnVideoTributeId");

            Tributes objTribute = new Tributes();

            objTribute.TributeName = lnkTributeName.Text;
            objTribute.TypeDescription = hdnTributeType.Value;
            objTribute.TributeId = int.Parse(e.CommandArgument.ToString());

            StateManager stateManager = StateManager.Instance;
            stateManager.Add(PortalEnums.SessionValueEnum.TributeSession.ToString(), objTribute, StateManager.State.Session);


            if (e.CommandName.Equals("TributeHome"))
            {
                if (WebConfig.ApplicationMode.Equals("local"))
                {
                    // Redirect to the Tribute Home Page
                    //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Login2HomePage.ToString()));
                    Response.Redirect(Session["APP_PATH"].ToString() + hdnTributeUrl.Value + "/"); //TributeHomePage.aspx");
                }
                else
                {
                    //Use this line for server and comment the line written above
                    Response.Redirect("http://" + hdnTributeType.Value.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + hdnTributeUrl.Value);
                }
            }
            else if (e.CommandName.Equals("WatchVideo"))
            {
                //ie. a video tribute id exists
                if (!string.IsNullOrEmpty(hdnVideoTributeId.Value))
                {
                    //redirect to video page
                    if (WebConfig.ApplicationMode.Equals("local"))
                    {
                        Response.Redirect(Session["APP_PATH"].ToString() + hdnTributeUrl.Value + "/video.aspx?mode=view&videoType=videotribute&videoId=" + hdnVideoTributeId.Value);
                    }
                    else
                    {
                        //Use this line for server and comment the line written above
                        Response.Redirect("http://" + hdnTributeType.Value.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + hdnTributeUrl.Value + "/video.aspx?mode=view&videoType=videotribute&videoId=" + hdnVideoTributeId.Value);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void repSearchTribute_ItemCommand(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lnkVideoTribute = (LinkButton)e.Item.FindControl("lbtnVideoTribute");
        HiddenField hdnVideoTributeId = (HiddenField)e.Item.FindControl("hdnVideoTributeId");

        if (hdnVideoTributeId.Value == string.Empty || Convert.ToInt32(hdnVideoTributeId.Value) == 0)
            lnkVideoTribute.Visible = false;
        else
            lnkVideoTribute.Visible = true;
    }
    protected void btnSearchSubmit_Click(object sender, ImageClickEventArgs e)
    {
        // Get tribute listing
        _CurrentPage = 1;
        this._presenter.GetBusinessUserTributeList();
        SetControlsVisibility();
    }

    protected void lbtnSendemail_Click(object sender, EventArgs e)
    {
        //GenralUserInfo objGenralUserInfo = new GenralUserInfo();
        //UserInfo objUserInfo = new UserInfo();

        //objUserInfo.UserEmail = txtLoginEmail1.Text;
        //objGenralUserInfo.RecentUsers = objUserInfo;

        //UsersController objUsersController = new UsersController();
        //objUsersController.CheckAndSendPassword(objGenralUserInfo, false);
        //txtLoginEmail1.Text = string.Empty;
        //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
    }

    protected void lbtnCancelMessage_Click(object sender, EventArgs e)
    {
        //divView.Visible = true;
        //divEdit.Visible = false;

        //txtWelcomeMessage.Text = lblWelcomeMessage.Text;
    }
    protected void lbtnSaveMessage_Click(object sender, EventArgs e)
    {
        this._presenter.SaveMessage();

        //lblWelcomeMessage.Text = txtWelcomeMessage.Text;

        //divView.Visible = true;
        //divEdit.Visible = false;
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
       // divView.Visible = false;
       // divEdit.Visible = true;

        //txtWelcomeMessage.Text = lblWelcomeMessage.Text;
    }

    #endregion


    #region PROPERTIES

    [CreateNew]
    public BusinessUserHomePresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    #region IBusinessUserHome Members

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

    public int SortOrder
    {
        get
        {
            return ddlSort.SelectedIndex;
        }
    }

    public string TributeType
    {
        get
        {
            return ddlTributeType.SelectedItem.Text;
        }
    }

    public string SearchString
    {
        get
        {
            //return txtSearchKeyword.Text;
            return string.Empty;
        }
    }

    public string UserName
    {
        get
        {
            return _BusinessUserName;
        }
    }

    public int UserID
    {
        get
        {
            return _UserId;
        }
    }

    public string ImageuRL
    {
        get
        {
            //return imgLogo.Src;
            return string.Empty;
        }
        set
        {

            if (value.Length > 0)
            {
                //imgLogo.Src = value;
            }
        }
    }

    public string WelcomeMessage
    {
        get
        {
            //return txtWelcomeMessage.Text;
            return string.Empty;
        }
        set
        {
            //txtWelcomeMessage.Text = value;
            //lblWelcomeMessage.Text = value;
        }
    }

    public string CompanyName
    {
        set
        {
            companyname = value;
            this.Page.Title = companyname;
            //lblOrg.Text = value;
        }
    }

    public string ZipCode
    {
        set
        {
            //lblPostalCode.Text = value;
        }
    }

    public string StreetAddress
    {
        set
        {
            //lblStreetAddress.Text = value;
        }
    }

    public string Locality
    {
        set
        {
            //lblLocality.Text = value;

            if (value != "")
            {
                //lblLocality.Text += ",";
            }
        }
    }

    public string Region
    {
        set
        {
            //lblRegion.Text = value;

            if (value != "")
            {
                //lblRegion.Text += ",";
            }
        }
    }

    public string Country
    {
        set
        {
            //lblCountry.Text = value;
        }
    }

    public string PostalCode
    {
        set
        {
            //lblPostalCode.Text = value;
        }
    }

    public string Telephone
    {
        set
        {
            //if (value != string.Empty)
            //    lblTelephone.Text = "(" + value.ToString().Substring(0, 3) + ") " + value.Substring(3, 3) + "-" + value.Substring(6, 4);
        }
    }

    public string WebsiteAddress
    {
        set
        {
           // lnkURL.InnerText = value;
            //string url = "http://" + value;
           // lnkURL.Attributes.Add("onclick", "OpenNewWindow('" + url + "');");
        }
    }

    public bool isEdit
    {
        set
        {
            //lbtnEdit.Visible = value;
            //lbtnUpload.Visible = value;
            //divView.Visible = true;
            //divEdit.Visible = false;
        }
    }
    public string BusinessPageName
    {
        get
        {
            // Added by Ashu on Oct 4, 2011 for rewrite URL 
            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                return "BusinessUserMomentsHome.aspx";
            else
                return "BusinessUserTributesHome.aspx";
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

    public string EmailId
    {
        set
        {
           // _emailId = value;
           // lnkEmail.Attributes.Add("onclick", "OpenSendMessage('" + value + "');");
        }
    }
    public int UserType
    {
        get
        {
            return _UserType;
        }
        set
        {
            _UserType = value;
        }
    }
    //public string VideoTributeId
    //{
    //    get { return "Hello"; }
    //    set { _videoTributeId = value; }
    //}
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
            //lblSearchResult.Text = ResourceText.GetString("lblSearchResult_SR");    // Search Results:
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
                _UserName = _SessionValue.UserName;
                //myprofile.Visible = true;
                // To display login and logout option based on the Session value for the user.
                //spanLogout.InnerHtml = "<a href='Logout.aspx'>Log out</a>";
            }
            else
            {
                //myprofile.Visible = false;
                //spanLogout.InnerHtml = "<a href='javascript: void(0);' onclick='doModalLogin();'>Log in</a>";
                //spanLogout.InnerHtml = "<a href='javascript: void(0);' onclick='UserLoginModalpopupFromSubDomain(location.href,document.title);' >Log in</a>";
            }

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

            if (Request.QueryString["username"] != null)
            {
                _BusinessUserName = Request.QueryString["username"].ToString();
                objStateManager.Add("BusinessUserName", _BusinessUserName, StateManager.State.Session);
                //maindiv.Visible = true;
                //Div1.Visible = false;
                //  SpanTribute.Visible = true;
                //this.Page.Title = "Your Tribute - {" + _BusinessUserName + "}'s Tributes";

            }
            else if (Session["BusinessUserName"] != null)
            {
                _BusinessUserName = Session["BusinessUserName"].ToString();
                //maindiv.Visible = true;
                //Div1.Visible = false;
            }
            else
            {
                SetUserControlsVisibility();
            }


            // To get page size from config file
            _PageSize = (int.Parse(WebConfig.Pagesize_Gift));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SetUserControlsVisibility()
    {
        //maindiv.Visible = false;
        //Div1.InnerText = "You are not authorised user.";
       // Div1.Visible = true;
        //  SpanTribute.Visible = false;
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

               // lblNotice.Attributes.Add("class", "yt-Error");

                //maindiv.Visible = true;
                PageFoot.Visible = false;
                PageHead.Visible = false;
                divMessage.InnerHtml = "No tributes found.";
                divMessage.Visible = true;
                //COMDIFFRES: (this function is called from page_load) why the following function is commented? it is not commented in .com version
                //SetUserControlsVisibility();
            }
            else
            {
                divTributeList.Visible = true;
                divMessage.Visible = false;
               // lblNotice.Attributes.Add("class", "yt-Notice");

                PageFoot.Visible = true;
                PageHead.Visible = true;
            }

            if (_Message != "")
            {
               // lblNotice.InnerHtml = _Message;
               // lblNotice.Visible = true;
            }
            else
            {
               // lblNotice.Visible = false;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}
