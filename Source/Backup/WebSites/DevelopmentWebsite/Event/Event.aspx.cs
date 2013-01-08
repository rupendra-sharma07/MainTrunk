///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Event.Event.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the list of events organized for the selected tribute.
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Event.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;

#endregion

/// <summary>
///Tribute Portal- Event UI Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the UI class IEvent for the listing of the all the Event in the tribute. This will implement the 
// All the Properties in the IEvent interface. and will extend PageBase class which provides 
// 1. Error Event Handler
// 2. Exception handling
/// </summary>
/// 


public partial class Event_Event : PageBase, IEvent
{

    #region CLASS VARIABLES

    private EventPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;

    private int _UserId;
    protected int _TributeId;
    private int _EventID;
    private string _EventName;
    private bool _IsAdmin;
    protected string _TributeType;
    protected string _TributeName;
    protected bool _isActive;
    protected string _UserName;
    private string _TributeURL;
    private string _FirstName = "";
    private string _LastName = "";

    private int intPageSize;
    //protected int _tributeId = 0;
    private int currentPage;
    private int totalRecordCount;   //Get value from store procedure
    string tributeEndDate = string.Empty;
    string appDomian = string.Empty;
    int topHeight = 0;
    //AG:Addd for Expiry Notice
    private string _TributePackageType;

    #endregion


    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        topHeight = 140;
        try
        {
            // Get the User and Tribute Detail from session
            GetValuesFromSession();

            //AG: Added code for expiry message
            if (!Equals(_TributePackageType,null))
            {
                if (_TributePackageType.Contains("Announce"))
                {           
                   // ScriptManager.RegisterStartupScript(Page,this.GetType(), "a", "fnExpiryNoticePopupClose();", true);                    
                }
            }

            //Start - Modification on 9-Dec-09 for the enhancement 3 of the Phase 1
            if (_TributeName != null) Page.Title = _TributeName + " | Events";
            //End

            //to get current page number, if user clicks on page number in paging it gets tha page number from query string
            //else page number is 1
            if (Request.QueryString["PageNo"] != null)
                currentPage = int.Parse(Request.QueryString["PageNo"].ToString());
            else
                currentPage = 1;

            //to get page size from config file
            intPageSize = (int.Parse(WebConfig.Pagesize_myEvents));

            // set text of the  controls
            SetControlsValue();
            aTributeHome.HRef = Session["APP_PATH"] + _TributeURL + "/";
            if (!this.IsPostBack)
            {
                this._presenter.OnViewInitialized();
                // Set Visibility of control on the basis of user right and total number of gifts
                SetControlsVisibility();
            }
            
            this._presenter.OnViewLoaded();
            tributeEndDate = _presenter.GetTributeEndDate(_TributeId);
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                appDomian = WebConfig.AppBaseDomain.ToString();
            }
            else
            {
                appDomian = "http://" + _TributeType.ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";
            }

            bool isCustomeHeaderOn = _presenter.GetCustomHeaderDetail(_TributeId);
            if (Equals(objSessionValue, null))//when not logged in
            {
                if (isCustomeHeaderOn)
                    topHeight = 196;
                else
                    topHeight = 89;
            }
            else
            {
                if (isCustomeHeaderOn)
                    topHeight = 258;
                else
                    topHeight = 130;
            }
           // DateTime dt = new DateTime();  by Ud for warning
            if (!tributeEndDate.Equals("Never"))
            {
                string[] date = tributeEndDate.Split('/');
                DateTime date2 = new DateTime(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1]));
                if (date2 < DateTime.Now)
                {
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "fnExpiryNoticePopupClose();", true);
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "fnExpiryNoticePopup('location.href','document.title','NonMemo','" + _TributeId + "','" + appDomian + "','" + topHeight + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/Error404.aspx");
        }
    }

    protected void lnkAddEvent_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session["APP_PATH"].ToString() + Session["TributeURL"].ToString() + "/manageevent.aspx", false);
    }

    protected void repEventList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                _EventID = int.Parse(((HiddenField)e.Item.FindControl("hdnEventId")).Value.ToString());
                _EventName = ((Label)e.Item.FindControl("lblEventName")).Text;
                this._presenter.DeleteEvent();

                Image imgEvent = (Image)e.Item.FindControl("imgEventImage");

                //Path where you want to upload the file.
                string[] eventPath = CommonUtilities.GetPath();

                if (eventPath == null)
                {
                    return;
                }
                string srcPath = eventPath[0] + "/" + eventPath[1] + "/" + _TributeURL.Replace(" ", "_") + "_" + _TributeType.Replace(" ", "_") + "/" + eventPath[7];
                srcPath += srcPath + "/" + Path.GetFileName(imgEvent.ImageUrl);
                if (File.Exists(srcPath))
                {
                    File.Delete(srcPath);
                }
                string queryString = "?TributeId=" + _TributeId + "&TributeName=" + _TributeName + "&TributeType=" + _TributeType;
                if (WebConfig.ApplicationMode.Equals("local"))
                {
                    Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + Session["TributeURL"].ToString() + "/events.aspx", false);
                }
                else
                {
                    //Uncomment the line below and comment the line above for server
                    Response.Redirect("http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + Session["TributeURL"].ToString() + "/events.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    protected void repEventList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lblDate = (Label)e.Item.FindControl("lblDate");
        lblDate.Text = ResourceText.GetString("lblDate_EV");  // When:

        Label lblPlace = (Label)e.Item.FindControl("lblPlace");
        lblPlace.Text = ResourceText.GetString("lblPlace_EV");  // Where:

        Label lblCreatedBy = (Label)e.Item.FindControl("lblCreatedBy");
        lblCreatedBy.Text = ResourceText.GetString("lblCreatedBy_EV");  // Created By:

        HyperLink lbtnFullEvent = (HyperLink)e.Item.FindControl("lbtnFullEvent");
        lbtnFullEvent.Text = ResourceText.GetString("lbtnFullEvent_EV");  // Click for full event details...

        int ID = int.Parse(((HiddenField)e.Item.FindControl("hdnEventId")).Value.ToString());
        if(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        lbtnFullEvent.NavigateUrl = Session["APP_PATH"] + _TributeURL + "/event.aspx" + "?EventID=" + ID + "&WebsiteID=" + _TributeId;
        else
            lbtnFullEvent.NavigateUrl = Session["APP_PATH"] + _TributeURL + "/event.aspx" + "?EventID=" + ID + "&TributeID=" + _TributeId;

        LinkButton btnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
        btnDelete.Text = ResourceText.GetString("lbtnDelete_EV");
        btnDelete.Attributes.Add("onclick", "if(confirm('" + ResourceText.GetString("msgDelete_EV") + "')){}else{return false}");

        Label lblEventDate = (Label)e.Item.FindControl("lblEventDate");

        Nullable<DateTime> eventDate = null;
        if (lblEventDate.Text != "")
        {
            string tmp = lblEventDate.Text.Substring(0, lblEventDate.Text.IndexOf("from") - 1);
            if (tmp != "")
            {
                eventDate = DateTime.Parse(tmp);
            }
        }

        btnDelete.Visible = false;

        if (_IsAdmin)
        {
            if (eventDate != null)
            {
                if (eventDate > DateTime.Now)
                {
                    btnDelete.Visible = true;
                }
            }
            else
            {
                btnDelete.Visible = true;
            }
        }

    }

    protected void lbtnDeleteEvent_Click(object sender, EventArgs e)
    {
        this._presenter.DeleteEvent();

        string queryString = "?TributeId=" + _TributeId + "&TributeName=" + _TributeName + "&TributeType=" + _TributeType;
        Response.Redirect("~/" + Session["TributeURL"] + "/events.aspx" + queryString, false);
        //
    }

    #endregion


    #region PROPERTIES

    [CreateNew]
    public EventPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    #region IEvent Members

    public int UserID
    {
        get
        {
            return _UserId;
        }
    }

    public int TributeID
    {
        get
        {
            return _TributeId;
        }
    }

    public int EventID
    {
        get
        {
            return _EventID;
        }
    }
    public string EventName
    {
        get
        {
            return _EventName;
        }
    }
    public string TributeName
    {
        get
        {
            return _TributeName;
        }
    }

    public string TributeType
    {
        get
        {
            return _TributeType;
        }
    }

    public string TributeURL
    {
        get
        {
            return _TributeURL;
        }
    }

    public IList<Events> EventList
    {
        set
        {
            repEventList.DataSource = value;
            repEventList.DataBind();
        }
    }

    public bool IsAdmin
    {
        get
        {
            object[] objVal = (object[])ViewState["Event_Admin"];
            if (objVal != null)
            {
                _IsAdmin = bool.Parse(objVal[0].ToString());
            }
            return _IsAdmin;
        }
        set
        {
            _IsAdmin = value;

            // Add the admin value in View State
            object[] objVal = { _IsAdmin };
            ViewState.Add("Event_Admin", objVal);
        }
    }

    public string FirstName
    {
        get
        {
            return _FirstName;
        }
    }

    public string LastName
    {
        get
        {
            return _LastName;
        }
    }

    #endregion

    #endregion


    #region METHODS

    /// <summary>
    /// This function will get the values (User Id and Tribute Detail) from the session
    /// </summary>
    private void GetValuesFromSession()
    {
        try
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            // get values from session
            StateManager objStateManager = StateManager.Instance;

            //to get user id from session as user is logged in user
            objSessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);
            if (objSessionValue != null)
            {
                _UserId = objSessionValue.UserId;
                if (objSessionValue.FirstName == string.Empty)
                    _FirstName = objSessionValue.UserName;
                else
                {
                    _FirstName = objSessionValue.FirstName;
                    _LastName = objSessionValue.LastName;
                }
            }
            else
            {
                _IsAdmin = false;
            }

            // to get tribute detail from session
            objTribute = (Tributes)objStateManager.Get(PortalEnums.SessionValueEnum.TributeSession.ToString(), StateManager.State.Session);

            if (Request.QueryString["mode"] != null) //if user is coming through link
            {
                if (Request.QueryString["TributeId"] != null)
                    _TributeId = int.Parse(Request.QueryString["TributeId"].ToString());

                if (Request.QueryString["TributeName"] != null)
                    _TributeName = Request.QueryString["TributeName"].ToString();

                if (Request.QueryString["TributeType"] != null)
                    _TributeType = Request.QueryString["TributeType"].ToString();

                if (Session["TributeSession"] == null)
                    CreateTributeSession(); //to create the tribute session values if user comest o this page from link or from favorites list.

            }
            else if (objTribute != null)
            {
                _TributeId = objTribute.TributeId;
                _TributeName = objTribute.TributeName;
                _TributeType = objTribute.TypeDescription;
                _TributeURL = objTribute.TributeUrl;
                _isActive = objTribute.IsActive;
                _TributePackageType = objTribute.TributePackageType;
            }

            if (_TributeId == 0)
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Method to create the tribute session values if user comes to this page from link or from favorites list.
    /// </summary>
    private void CreateTributeSession()
    {
        Tributes objTribute = new Tributes();
        objTribute.TributeId = _TributeId;
        objTribute.TributeName = _TributeName;
        objTribute.TypeDescription = _TributeType;
        objTribute.TributeUrl = _TributeURL;
        objTribute.IsActive = _isActive;
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add(PortalEnums.SessionValueEnum.TributeSession.ToString(), objTribute, TributesPortal.Utilities.StateManager.State.Session);
    }


    /// <summary>
    /// This Function will set the value of the control and error messages from the resource File
    /// </summary>
    private void SetControlsValue()
    {
        try
        {
            //Text for labels from the resource file
            lblHead.Text = ResourceText.GetString("lblHead_EV");  //Events
            lnkAddEvent.Text = ResourceText.GetString("lnkAddEvent_EV");  //Add Event
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// This method will save the object in the View State
    /// </summary>
    /// <param name="objValue">A object which contain value which want to add in view state</param>
    /// <param name="key">A string varaible which is key for setting the values from view state</param>
    private void AddValuesInViewState(object[] objValue, string key)
    {
        ViewState.Add(key, objValue);
    }


    /// <summary>
    /// This method will get the Values in the View State for the passed key
    /// </summary>
    /// <param name="key">A string varaible which is key for getting the values from view state</param>
    /// <returns>This method will return a object which conatin View State value for the passed key</returns>
    private object[] GetValuesFromViewState(string key)
    {
        object[] objValue = (object[])ViewState[key];

        return objValue;
    }


    /// <summary>
    /// Set Visibility of control on the basis of user right 
    /// </summary>
    private void SetControlsVisibility()
    {
        try
        {
            //to set visibility of add button
            if (_IsAdmin == true)
            {
                lnkAddEvent.Visible = true;
                divButton.Visible = true;
            }
            else
            {
                lnkAddEvent.Visible = false;
                divButton.Visible = false;
            }

            if (repEventList.Items.Count > 0)
            {
                divMessage.Visible = false;
            }
            else
            {
                divMessage.Visible = true;
                divMessage.InnerText = ResourceText.GetString("strNoMessage_ME");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public CommentTributeAdministrator GetSessionObject(int CurrentPage)
    {
        try
        {
            CommentTributeAdministrator objComAdmin = new CommentTributeAdministrator();
            objComAdmin.CurrentPage = (int)CurrentPage;
            objComAdmin.PageSize = (int)intPageSize;
            objComAdmin.TotalRecords = totalRecordCount;

            return objComAdmin;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

}