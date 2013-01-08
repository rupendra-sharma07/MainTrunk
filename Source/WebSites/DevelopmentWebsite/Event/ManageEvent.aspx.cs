///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Event.Event.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to add a new event or edit an existing event.
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
using TributesPortal.Event.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;

#endregion

/// <summary>
///Tribute Portal- Manage Event ( Add and Edit) UI Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the UI class Event_ManageEvent for the Event Add and Edit. This will implement the 
// All the Properties in the IManageEvent interface. and will extend PageBase class which provides 
// 1. Error Event Handler
// 2. Exception handling
/// </summary>
/// 
public partial class Event_ManageEvent : PageBase, IManageEvent
{

    #region CLASS VARIABLES

    private ManageEventPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;

    private int _UserId;
    protected int _TributeId;
    private int _EventId;
    private int _EventUserID;
    private bool _IsAdmin;
    protected string _TributeType;
    protected string _TributeName;
    protected string _UserName;
    private string _TributeURL;
    protected string _UserProfile;
    private string _FirstName = "";
    private string _LastName = "";

    #endregion


    #region CONSTANT

    private const string DefaultPath = "../assets/images/Event";
    protected string todayDay = DateTime.Now.Day.ToString();
    protected string todayMonth = DateTime.Now.Month.ToString();
    protected string todayYear = DateTime.Now.Year.ToString();

    #endregion


    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.Form.Action = Request.RawUrl;
            // This Method will get the Tribute detail and user detail from the session
            GetValuesFromSession();

            // This will set the values to the controls
            SetControlsValue();
            aTributeHome.HRef = Session["APP_PATH"] + _TributeURL + "/";
            if (!this.IsPostBack)
            {
                this._presenter.OnViewInitialized();

                // Set Visibility of control on the basis of user right
                SetControlsVisibility();
            }

            this._presenter.OnViewLoaded();

            if (ddlEventType.SelectedItem.Text == "other")
            {
                txtOtherType.CssClass = "yt-Form-Input yt-MoreTypeText yt-Shown";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // Get the State list for selected country
            // if (ddlCountry.SelectedItem.Text == "United States")
            //   {
            this._presenter.GetStateList();
            //  }
            //    else
            //    {
            //    List<Locations> tmpLocation = new List<Locations>();
            //    StateList = tmpLocation;
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnCancelEvent_Click(object sender, EventArgs e)
    {
        try
        {
            // Redirect to the list of events
            //Response.Redirect("~/" + Session["TributeURL"] +"/event.aspx", false);
            Response.Redirect("~/" + Session["TributeURL"] + "/events.aspx", false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnSaveEvent_Click(object sender, EventArgs e)
    {
        try
        {
            string error = string.Empty;
            //bool pass1 = true;
            //DateTime objEventDate = new DateTime();
            Indecator1.Visible = false;
            int eventYear = 0;
            int.TryParse(txtYear.Text, out eventYear);

            bool passEventDate = false;
            if ((ddlDay.SelectedValue.Trim() != "") && (int.Parse(ddlDay.SelectedValue.Trim()) > (DateTime.Today.Day-1)))
                passEventDate = true;
            if ((ddlMonth.SelectedValue.Trim() != "") && (int.Parse(ddlMonth.SelectedValue.Trim()) > (DateTime.Today.Month - 1)))
                passEventDate = true;
            if ((eventYear > (DateTime.Today.Year - 1)) && (eventYear < 9999))
                passEventDate = true;
            else
                passEventDate = false;

            //DateTime.TryParse(ddlDay.SelectedValue + "/" + ddlMonth.SelectedValue + "/" + txtYear.Text, out objEventDate);
            if (passEventDate)
            {
                imgEventImage.Src = hdnEventImageURL.Value; 
                imgEventImage.Alt = _TributeName;
                ViewState["ImgURL"] = imgEventImage.Src;

                SaveThumbnailImage(false);
                // Save the image in default path for the event
                SaveImage(false);


                // Save the Event Details
                string errorMsg = _presenter.SaveEvent();
                Events objEvent = new Events();
                if (rbtnlstMealSelection.SelectedIndex == 0)
                    objEvent.IsAskForMeal = true;
                else
                    objEvent.IsAskForMeal = false;
                StateManager objStateManager = StateManager.Instance;
                objStateManager.Add("EventInfo", objEvent, StateManager.State.Session);

                // Show the error message - This will occur if Event Name with this tribute type already exist
                if ((errorMsg != null) && (errorMsg != ""))
                {
                    lblErrMsg.InnerHtml = ShowMessage(ResourceText.GetString("valHeader_ME"), errorMsg, 1);
                    lblErrMsg.Visible = true;
                    if (ViewState["ImgURL"] != null)
                    {
                        hdnEventImageURL.Value = ViewState["ImgURL"].ToString();
                        imgEventImage.Src = ViewState["ImgURL"].ToString();
                    }
                }
                else
                {
                    // Redirect to the Invite Guest Page
                    string queryString = string.Empty;
                    if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                        queryString = "?EventID=" + _EventId + "&WebsiteID=" + _TributeId + "&EventName=" + this._presenter.View.EventName;
                    else
                        queryString = "?EventID=" + _EventId + "&TributeID=" + _TributeId + "&EventName=" + this._presenter.View.EventName;

                    if (facebook_share.Checked == true)
                    {
                        queryString += "&post_on_facebook=" + facebook_share.Checked;
                    }

                    Response.Redirect("~/" + Session["TributeURL"] + "/inviteguest.aspx" + queryString, false);
                    //Redirect.RedirectToPage(Redirect.PageList.InviteGuest.ToString())
                }
            }
            else
            {
                Indecator1.Visible = true;
                lblErrMsg.InnerHtml = SetHeaderMessage("Please enter a valid Event date.", valsError.HeaderText);
                lblErrMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnEditEvent_Click(object sender, EventArgs e)
    {
        try
        {
            imgEventImage.Src = hdnEventImageURL.Value;
            imgEventImage.Alt = _TributeName;
            ViewState["ImgURL"] = imgEventImage.Src;

            SaveThumbnailImage(false);
            // Save the image in default path for the event
            SaveImage(false);


            // Save the Event Details
            string errorMsg = _presenter.UpdateEvent();

            // Show the error message - This will occur if Event Name with this tribute type already exist
            if ((errorMsg != null) && (errorMsg != ""))
            {
                lblErrMsg.InnerHtml = ShowMessage(ResourceText.GetString("valHeader_ME"), errorMsg, 1);
                lblErrMsg.Visible = true;
                if (ViewState["ImgURL"] != null)
                {
                    hdnEventImageURL.Value = ViewState["ImgURL"].ToString();
                    imgEventImage.Src = ViewState["ImgURL"].ToString();
                }
            }
            else
            {
                // Redirect to the list of events
                Response.Redirect("~/" + Session["TributeURL"] + "/events.aspx", false);
                //Redirect.RedirectToPage(Redirect.PageList.Event.ToString()
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void repImage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType != ListItemType.Item) && (e.Item.ItemType != ListItemType.AlternatingItem))
        {
            return;
        }
        else
        {
            Image tmpImageUrl = (Image)e.Item.FindControl("imgImageList");

            // When click on the image set the event image to selected image
            string function = "SetImageURL('" + tmpImageUrl.ImageUrl + "')";
            tmpImageUrl.Attributes.Add("onclick", function);
        }
    }
    /// <summary>
    /// This methods add meals value to list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnAddMealOption_Click(object sender, EventArgs e)
    {
        //if (txtMealOption.Text.Length > 0)
        //    lbMealOptions.Items.Add(txtMealOption.Text);
        //txtMealOption.Text = string.Empty;
        //lbMealOptions.Focus();

        SortedList objSortedList = new SortedList();
        int flg = 0;

        if (txtMealOption.Text.Length > 0)
        {
            foreach (ListItem lItem in lbMealOptions.Items)
            {
                if (txtMealOption.Text == lItem.Text)
                {
                    flg = 1;
                }
            }
            if (flg == 0)
            {
                lbMealOptions.Items.Add(txtMealOption.Text);
            }

            foreach (ListItem lItem in lbMealOptions.Items)
            {
                objSortedList.Add(lItem.Value, lItem);
            }

            lbMealOptions.Items.Clear();

            for (int i = 0; i < objSortedList.Count; i++)
            {
                lbMealOptions.Items.Add((ListItem)objSortedList[objSortedList.GetKey(i)]);
            }
        }
        txtMealOption.Text = string.Empty;

    }

    /// <summary>
    /// This method remove the meals from the list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnRemoveMealOption_Click(object sender, EventArgs e)
    {
        if (lbMealOptions.SelectedIndex > -1)
            lbMealOptions.Items.Remove(lbMealOptions.SelectedValue);
        //lbMealOptions.Focus();
    }

    /// <summary>
    /// This method show/hides the meal option
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbtnlstMealSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        fnCheckForMeal();
        //if (rbtnlstMealSelection.SelectedItem.Value.Equals("1"))
        //    divMealOptions.Style.Add(HtmlTextWriterStyle.Display, "block");
        //else
        //    divMealOptions.Style.Add(HtmlTextWriterStyle.Display, "none");

        //rbtnlstMealSelection.Focus();
        
    }

    #endregion


    #region PROPERTIES

    [CreateNew]
    public ManageEventPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }


    #region IManageEvent Members

    public int UserID
    {
        get
        {
            return _UserId;
        }
    }

    public int EventUserID
    {
        set
        {
            _EventUserID = value;
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
            return _EventId;
        }

        set
        {
            _EventId = value;
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

    public IList<string> EventTypeList
    {
        set
        {
            ddlEventType.DataSource = value;
            ddlEventType.DataBind();
        }
    }

    public IList<GiftImage> ImageList
    {
        set
        {
            repImage.DataSource = value;
            repImage.DataBind();
        }
    }

    public IList<Locations> CountryList
    {
        set
        {
            ddlCountry.DataSource = value;
            ddlCountry.DataTextField = Locations.Location.LocationName.ToString();
            ddlCountry.DataValueField = Locations.Location.LocationId.ToString();
            ddlCountry.DataBind();
        }
    }

    public IList<Locations> StateList
    {
        set
        {
            ddlState.Items.Clear();
            if (value.Count > 0)
            {
                ddlState.DataSource = value;
                ddlState.DataTextField = Locations.Location.LocationName.ToString();
                ddlState.DataValueField = Locations.Location.LocationId.ToString();
                ddlState.DataBind();
                ddlState.Enabled = true;
            }
            else
                ddlState.Enabled = false;

        }
    }

    public string UserName
    {
        set
        {
            _UserName = value;
        }
    }

    public string Location
    {
        set
        {
            txtLocation.Text = value;
        }
        get
        {
            return txtLocation.Text;
        }
    }

    public string City
    {
        set
        {
            txtCity.Text = value;
        }
        get
        {
            return txtCity.Text;
        }
    }

    public string URL
    {
        get
        {
            string EmailHref = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeURL + "/event.aspx?eventId=##" + "</a>";
            //string QueryString = "?TributeId=" + _TributeId + "&TributeName=" + _TributeName + "&TributeType=" + _TributeType + "&TributeUrl=" + _TributeURL + "&mode=emailPage";
            //string QueryString = "?mode=emailPage";
            //string ApplicationPath = "<a href='http://" + Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;
            string ApplicationPath = "<a href='http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeURL + "/event.aspx?eventId=##";

            //return ApplicationPath + "/Event/event.aspx" + QueryString + "'>" + EmailHref;
            return ApplicationPath + "'>" + EmailHref;
        }
    }

    public string InviteGuestURL
    {
        get
        {
            //return "<a href='http://" + Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath + "/event/eventfullview.aspx";
            return "<a href='http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeURL + "/event.aspx";
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

    public string EventName
    {
        set
        {
            txtEventName.Text = value;
        }
        get
        {
            return txtEventName.Text;
        }
    }

    //public string EventCreatedBy
    //{
    //    set
    //    {
    //        ddCreatedBy.InnerHtml = "<a id='lnkCreatedBy' runat='server' href='javascript:void(0);' onclick=\"UserProfileModal_1('" + _EventUserID + "');\">" + value + "</a>";
    //    }

    //    get
    //    {
    //        return "";
    //    }
    //}

    public string EventDesc
    {
        set
        {
            txtEventDesc.Text = value;
        }
        get
        {
            return txtEventDesc.Text;
        }
    }

    public string EventImage
    {
        get
        {
            return hdnEventImageURL.Value.ToString();
        }
        set
        {
            string[] virtualDir = CommonUtilities.GetPath();

            if (virtualDir != null)
            {
                hdnEventImageURL.Value = virtualDir[2] + value;
                imgEventImage.Src = virtualDir[2] + value;
                imgEventImage.Alt = _TributeName;
            }
        }
    }


    public string EventImagePrevURL
    {
        set
        {
            string[] virtualDir = CommonUtilities.GetPath();
            ViewState[PortalEnums.EventStateEnum.EventImageURL.ToString()] = virtualDir[2] + value;
        }
    }

    public string Address
    {
        get
        {
            return txtAddress.Text;
        }
        set
        {
            txtAddress.Text = value;
        }
    }
    public string EventTypeId
    {
        get
        {
            if (ddlEventType.SelectedValue == "other")
            {
                return txtOtherType.Text;
            }
            else
            {
                return ddlEventType.SelectedValue;
            }
        }
        set
        {
            ListItem tmpItem = ddlEventType.Items.FindByValue(value.ToString());
            if (tmpItem == null)
            {
                ddlEventType.SelectedValue = "other";
                txtOtherType.CssClass = "yt-Form-Input yt-MoreTypeText yt-Shown";
                txtOtherType.Text = value.ToString();
            }
            else
            {
                ddlEventType.SelectedValue = value.ToString();
                txtOtherType.CssClass = "yt-Form-Input yt-MoreTypeText";
            }

        }
    }

    public string Day
    {
        set
        {
            ddlDay.SelectedValue = value;
        }

        get
        {
            return ddlDay.SelectedValue;
        }
    }

    public string Month
    {
        set
        {
            ddlMonth.SelectedValue = value;
        }

        get
        {
            return ddlMonth.SelectedValue;
        }
    }

    public string Year
    {
        set
        {
            txtYear.Text = value;
        }

        get
        {
            return txtYear.Text;
        }
    }

    public string EventStartTime
    {
        get
        {
            return ddlHourStart.SelectedItem.Text + ":" + ddlMinuteStart.SelectedItem.Text + " " + ddlAMPMStart.SelectedItem.Text;
        }

        set
        {
            char[] sep = { ':', ' ' };
            string[] arr = value.Split(sep);

            if (arr != null)
            {
                ddlHourStart.SelectedValue = arr[0].ToString();
                ddlMinuteStart.SelectedValue = ddlMinuteStart.Items.IndexOf(ddlMinuteStart.Items.FindByText(arr[1].ToString())).ToString();
                ddlAMPMStart.SelectedValue = ddlAMPMStart.Items.IndexOf(ddlAMPMStart.Items.FindByText(arr[2].ToString())).ToString();
            }
        }
    }

    public string EventEndTime
    {
        get
        {
            return ddlHourEnd.SelectedItem.Text + ":" + ddlMinuteEnd.SelectedItem.Text + " " + ddlAMPMEnd.SelectedItem.Text;
        }

        set
        {
            char[] sep = { ':', ' ' };
            string[] arr = value.Split(sep);

            if (arr != null)
            {
                if (arr[0] != null)
                {
                    ddlHourEnd.SelectedValue = arr[0].ToString();
                }

                if (arr[1] != null)
                {
                    ddlMinuteEnd.SelectedValue = ddlMinuteEnd.Items.IndexOf(ddlMinuteEnd.Items.FindByText(arr[1].ToString())).ToString();
                }

                if (arr[2] != null)
                {
                    ddlAMPMEnd.SelectedValue = ddlAMPMEnd.Items.IndexOf(ddlAMPMEnd.Items.FindByText(arr[2].ToString())).ToString();
                }
            }
        }

    }

    public string State
    {
        set
        {
            ddlState.SelectedValue = value;
        }

        get
        {
            return ddlState.SelectedValue;
        }
    }

    public string Country
    {
        set
        {
            ddlCountry.SelectedValue = value;
        }

        get
        {
            return ddlCountry.SelectedValue;
        }
    }

    public string HostName
    {
        get
        {
            return txtHost.Text;
        }
        set
        {
            txtHost.Text = value;
        }
    }

    public string PhoneNumber
    {
        get
        {
            if ((txtPhoneNumber1.Text != "") && (txtPhoneNumber2.Text != "") && (txtPhoneNumber3.Text != ""))
            {
                return txtPhoneNumber1.Text + "-" + txtPhoneNumber2.Text + "-" + txtPhoneNumber3.Text;
            }
            else
            {
                return "";
            }
        }
        set
        {
            if (value != "")
            {
                char sep = '-';
                string[] arr = value.Split(sep);

                if (arr != null)
                {
                    txtPhoneNumber1.Text = arr[0].ToString();
                    txtPhoneNumber2.Text = arr[1].ToString();
                    txtPhoneNumber3.Text = arr[2].ToString();
                }
            }
        }
    }

    public string EmailId
    {
        get
        {
            return txtEmail.Text;
        }
        set
        {
            txtEmail.Text = value;
        }
    }

    public bool IsPrivate
    {
        get
        {
            return rdoPrivate.Checked;
        }
        set
        {
            rdoPrivate.Checked = value;
            rdoPublic.Checked = !value;
        }
    }

    public bool IsAskForMeal
    {
       
        get
        {
            if (rbtnlstMealSelection.SelectedItem.Value.ToString() == "1")
                return true ;
            else
                return false;
        }
        set
        {            
            int count = rbtnlstMealSelection.Items.Count;
            if (value == true)
            {
                rbtnlstMealSelection.SelectedIndex = 0;
                
//                rbtnlstMealSelection_SelectedIndexChanged (obj , args);
                fnCheckForMeal();
            }
            else
            {
                rbtnlstMealSelection.SelectedIndex = 1;
                //rbtnlstMealSelection_SelectedIndexChanged(obj, args);
                fnCheckForMeal();
            }
        }
    }

    public bool IsAdmin
    {
        get
        {
            // Get the admin value from the view state
            _IsAdmin = (bool)ViewState[PortalEnums.EventStateEnum.Event_Admin.ToString()];
            return _IsAdmin;
        }
        set
        {
            _IsAdmin = value;

            // Add the admin value in View State
            ViewState.Add(PortalEnums.EventStateEnum.Event_Admin.ToString(), _IsAdmin);
        }
    }
    public bool AllowAdditionalPeople
    {
        get
        {
            return chkAllowAdditionalPeople.Checked;
        }
        set
        {
            chkAllowAdditionalPeople.Checked = value;
        }

    }
    public bool SendEmailReminder
    {
        get
        {
            return chkSendEmailReminder.Checked;
        }
        set
        {
            chkSendEmailReminder.Checked = value;
        }

    }
    public bool ShowRsvpStatistics
    {
        get
        {
            return chkShowRsvpStatistics.Checked;
        }
        set
        {
            chkShowRsvpStatistics.Checked = value;
        }

    }
    //Using '#' as seperator for different meal options
    public string MealOptions
    {
        get
        {
            string mealOptions = string.Empty;
            foreach (ListItem item in lbMealOptions.Items)
            {
                if (mealOptions.Length > 0)
                    mealOptions = string.Format("{0}#{1}", mealOptions, item);
                else
                    mealOptions = item.Value;
            }
            return mealOptions;
        }
        set
        {
            if (value != null && value.Length > 0)
            {
                string[] mealOptions = value.Split('#');
                foreach (string item in mealOptions)
                {
                    lbMealOptions.Items.Add(item);
                }
                this.rbtnlstMealSelection.Items.FindByValue("1").Selected = true;
                divMealOptions.Style.Add(HtmlTextWriterStyle.Display, "block");
            }
            else
            {
                this.rbtnlstMealSelection.Items.FindByValue("0").Selected = true;
                divMealOptions.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
        }
    } 

    #endregion

    #endregion


    #region METHODS


    private void fnCheckForMeal()
    {
        try
        {
            if (rbtnlstMealSelection.SelectedItem.Value.Equals("1"))
                divMealOptions.Style.Add(HtmlTextWriterStyle.Display, "block");
            else
                divMealOptions.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// This function will get the values (User Id and Tribute Detail) from the session
    /// </summary>
    private void GetValuesFromSession()
    {
        try
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            // Get values from session
            StateManager objStateManager = StateManager.Instance;

            // To get user id from session as user is logged in user
            objSessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);
            if (objSessionValue != null)
            {
                _UserId = objSessionValue.UserId;
                if (objSessionValue.UserType == 2)
                {
                    _FirstName = objSessionValue.FirstName;
                }
                else
                {
                    _FirstName = objSessionValue.FirstName;
                    _LastName = objSessionValue.LastName;
                }
                _EventUserID = objSessionValue.UserId;
            }
            else
            {
                _IsAdmin = false;
                _UserId = 0;

                ViewState.Add(PortalEnums.EventStateEnum.Event_Admin.ToString(), false);
            }

            // To get tribute detail from session
            objTribute = (Tributes)objStateManager.Get(PortalEnums.SessionValueEnum.TributeSession.ToString(), StateManager.State.Session);
            if (objTribute != null)
            {
                _TributeId = objTribute.TributeId;
                _TributeName = objTribute.TributeName;
                _TributeType = objTribute.TypeDescription;
                _TributeURL = objTribute.TributeUrl;
            }

            if (Request.QueryString["EventId"] != null)
            {
                _EventId = int.Parse(Request.QueryString["EventId"].ToString());
            }

            // If user and Tribute id is blank then redirect to the login page
            if ((_TributeId == 0) || (_UserId == 0))
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
    /// This Function will set the value of the control and error messages from the resource File
    /// </summary>
    private void SetControlsValue()
    {
        try
        {
            //Text for labels from the resource file
            lblEventName.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblEventName_ME");
            lblEventType.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblEventType_ME");
            lblEventDate.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblEventDate_ME");
            lblMonth.InnerText = ResourceText.GetString("lblMonth_ME");
            lblDay.InnerText = ResourceText.GetString("lblDay_ME");
            lblYear.InnerText = ResourceText.GetString("lblYear_ME");
            lblEventTime.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblEventTime_ME");
            lblCity.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblCity_ME");
            lblLocation.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblLocation_ME");
            lblState.InnerHtml = ResourceText.GetString("lblState_ME");
            lblCountry.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblCountry_ME");
            lblAddress.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblAddress_ME");

            lblHost.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblHost_ME");
            lblPhone.InnerText = ResourceText.GetString("lblPhone_ME");
            lblEmail.InnerText = ResourceText.GetString("lblEmail_ME");
            lblHead.Text = ResourceText.GetString("lblHead_ME");
            lblEventinfo.Text = ResourceText.GetString("lblEventinfo_ME");
            lblTime.Text = ResourceText.GetString("lblTime_ME");

            lblPublic.InnerText = ResourceText.GetString("lblPublic_ME");
            lblPriavte.InnerText = ResourceText.GetString("lblPriavte_ME");
            lblPrivacy.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblPrivacy_ME");
            lblEventDesc.InnerHtml = ResourceText.GetString("lblEventDesc_ME");
            lblEventHost.Text = ResourceText.GetString("lblEventHost_ME");
            //lblCreatedBy.Text = ResourceText.GetString("lblCreatedBy_ME");
            lblRequiredFields.InnerHtml = ResourceText.GetString("lblRequiredFields_ST") + "<em class='required'>* </em>";

            if (ddlMonth.Items.Count <= 0)
            {
                int i = 0;
                ListItem item = new ListItem("", i.ToString());

                ddlMonth.Items.Add(item);
                for (i = 1; i <= 12; i++)
                {
                    string month = "Month" + i + "_ST";
                    item = new ListItem(ResourceText.GetString(month), i.ToString());
                    ddlMonth.Items.Add(item);
                }
            }

            lbtnCancelEvent.Text = ResourceText.GetString("lbtnCancelEvent_ME");
            lbtnSaveEvent.Text = ResourceText.GetString("lbtnSaveEvent_ME");
            lbtnEditEvent.Text = ResourceText.GetString("lbtnEditEvent_ME");

            //Text for error messages from the resource file
            valEventName.ErrorMessage = ResourceText.GetString("valEventName_ME");
            valAddOtherTpe.ErrorMessage = ResourceText.GetString("valAddOtherTpe_ME");
            valRequireDate.ErrorMessage = ResourceText.GetString("valRequireDate_ME");
            valValidDate.ErrorMessage = ResourceText.GetString("valValidDate_ME");
            valCheckDate.ErrorMessage = ResourceText.GetString("valCheckDate_ME");
            valCheckTime.ErrorMessage = ResourceText.GetString("valCheckTime_ME");
            valRequireLocation.ErrorMessage = ResourceText.GetString("valRequireLocation_ME");
            valLocation.ErrorMessage = ResourceText.GetString("valLocation_ME");
            valRequireAddress.ErrorMessage = ResourceText.GetString("valRequireAddress_ME");
            valRequireCity.ErrorMessage = ResourceText.GetString("valRequireCity_ME");
            valCity.ErrorMessage = ResourceText.GetString("valCity_ME");
            valHost.ErrorMessage = ResourceText.GetString("valHost_ME");
            valPhoneNumber.ErrorMessage = ResourceText.GetString("valPhoneNumber_ME");
            valPrivacy.ErrorMessage = ResourceText.GetString("valPrivacy_ME");

            ddlEventType.Attributes.Add("onchange", "return typeMoreSelect()");
            txtEventDesc.Attributes.Add("onkeyup", "CheckDescLength();");

           // txtEventName.Focus();

            //Text for meal seletion
            lblMealSelections.InnerText = ResourceText.GetString("lblMealSelections_ME");
            lblMealOption.InnerText = ResourceText.GetString("lblMealOption_ME");
            lblAdditionalOptions.InnerText = ResourceText.GetString("lblAdditionalOptions_ME");
            chkAllowAdditionalPeople.Text = ResourceText.GetString("chkAllowAdditionalPeople_ME");
            chkSendEmailReminder.Text = ResourceText.GetString("chkSendEmailReminder_ME");
            chkShowRsvpStatistics.Text = ResourceText.GetString("chkShowRsvpStatistics_ME");
            lbtnRemoveMealOption.Text = ResourceText.GetString("lbtnRemoveMealOption_ME");
            lbtnAddMealOption.Text = ResourceText.GetString("lbtnAddMealOption_ME");

            if (rbtnlstMealSelection.Items.Count <= 0)
            {
                rbtnlstMealSelection.Items.Add(new ListItem(ResourceText.GetString("lstAskMeal_ME"), "1"));
                rbtnlstMealSelection.Items.Add(new ListItem(ResourceText.GetString("lstDontAskMeal_ME"), "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Set Visibility of control on the basis of user right
    /// </summary>
    private void SetControlsVisibility()
    {
        try
        {
            //to set visibility of edit and add button

            // If page is in Add mode then dispaly Add button
            if (_EventId == 0)
            {
                lbtnEditEvent.Visible = false;
                lbtnSaveEvent.Visible = true;
                //By default "don't ask" option will be selected and meal options will be hidden 
                this.rbtnlstMealSelection.Items.FindByValue("0").Selected = true;
                divMealOptions.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
            // If page is in Edit mode then dispaly Edit button
            else if (_EventId >= 1)
            {
                lbtnEditEvent.Visible = true;
                lbtnSaveEvent.Visible = false;
            }

            // if user is not admin then don't dispaly the edit and add button
            if (_IsAdmin == false)
            {
                lbtnSaveEvent.Visible = false;
                lbtnEditEvent.Visible = false;
            }

            // If Other is selected in the dropdown then display the text box otherwise not
            if (ddlEventType.SelectedItem != null)
            {
                if (ddlEventType.SelectedItem.Text == "other")
                {
                    txtOtherType.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// This method will save the final image in the default path and set the URL to that path
    /// </summary>
    /// <param name="isEdit">A bool variable which contains whether user wants to Edit or wants to add new one</param>
    private void SaveImage(bool isEdit)
    {

        // path of the previous image file ( in case of edit )
        string prevImage = (string)ViewState[PortalEnums.EventStateEnum.EventImageURL.ToString()];

        // in case of edit if image path is not changed then exit from the function
        if (prevImage != hdnEventImageURL.Value)
        {
            string fileName = Path.GetFileName(hdnEventImageURL.Value);

            //Path where you want to upload the file.
            string[] eventPath = CommonUtilities.GetPath();

            if (eventPath == null)
            {
                return;
            }

            // Destination Path = Drive + TributePhotos Folder + TributeURL_TributeType Folder + Event Folder
            string destPath = eventPath[0] + "/" + eventPath[1] + "/" + _TributeURL.Replace(" ", "_") + "_" + _TributeType.Replace(" ", "_") + "/" + eventPath[5];

            // Source Path = Drive + TributePhotos Folder + Temp Folder + File Name
            string srcPath = eventPath[0] + "/" + eventPath[1] + "/" + hdnEventImageURL.Value.Substring(hdnEventImageURL.Value.IndexOf(eventPath[2]) + eventPath[2].Length, hdnEventImageURL.Value.Length - eventPath[2].Length); ;

            DirectoryInfo dirObj = new DirectoryInfo(destPath);

            //if directory does not exists creates a directory
            if (!dirObj.Exists)
            {
                dirObj.Create();
            }

            // Check to see if a file already exists with the same name as the file to upload.  

            string tempfileName = fileName;
            int counter = 1;

            while (File.Exists(destPath + "/" + tempfileName))
            {
                // If a file with this name already exists, add a number in the filename.
                tempfileName = counter.ToString() + fileName;
                counter = counter + 1;
            }

            fileName = tempfileName;

            // if is in edit mode then delete the previous file
            if (isEdit)
            {
                if (prevImage != "")
                {
                    string newPrevImage = destPath + "/" + Path.GetFileName(prevImage.ToString());
                    if (File.Exists(newPrevImage))
                    {
                        File.Delete(newPrevImage);
                    }

                    ViewState[PortalEnums.EventStateEnum.EventImageURL.ToString()] = "";
                }
            }

            // Copy the file from the temp location to final destination folder
            if (File.Exists(srcPath))
            {
                File.Copy(srcPath, destPath + "/" + fileName);

                string newFilename = _TributeURL.Replace(" ", "_") + "_" + _TributeType.Replace(" ", "_") + "/" + eventPath[5] + "/" + fileName;

                hdnEventImageURL.Value = newFilename;
                imgEventImage.Src = newFilename;
            }
        }
        else
        {
            //Path where you want to upload the file.
            string[] eventPath = CommonUtilities.GetPath();

            string newFilename = hdnEventImageURL.Value.Substring(hdnEventImageURL.Value.IndexOf(eventPath[2]) + eventPath[2].Length, hdnEventImageURL.Value.Length - eventPath[2].Length); ;

            hdnEventImageURL.Value = newFilename;
            imgEventImage.Src = newFilename;
        }

    }
    private void SaveThumbnailImage(bool isEdit)
    {

        // path of the previous image file ( in case of edit )
        string prevImage = (string)ViewState[PortalEnums.EventStateEnum.EventImageURL.ToString()];

        // in case of edit if image path is not changed then exit from the function
        if (prevImage != hdnEventImageURL.Value)
        {
            string fileName = Path.GetFileName(hdnEventImageURL.Value);

            //Path where you want to upload the file.
            string[] eventPath = CommonUtilities.GetPath();
            string strSourceSourceRowPath = string.Empty;
            if (hdnEventImageURL.Value.Contains("EventImages"))
            {
                string[] arrEventPath = hdnEventImageURL.Value.Split('/');
                strSourceSourceRowPath = arrEventPath[0] + "//" + arrEventPath[2] + "/" + arrEventPath[3] + "/" + arrEventPath[4] + "/" + arrEventPath[5] + "/" + arrEventPath[6] + "/Thumbnails/" + arrEventPath[7];

            }
            else
            {
                strSourceSourceRowPath = hdnEventImageURL.Value;
            }

            if (eventPath == null)
            {
                return;
            }

            // Destination Path = Drive + TributePhotos Folder + TributeURL_TributeType Folder + Event Folder
            string destPath = eventPath[0] + "/" + eventPath[1] + "/" + _TributeURL.Replace(" ", "_") + "_" + _TributeType.Replace(" ", "_") + "/" + eventPath[5] + "/" + eventPath[3];

            // Source Path = Drive + TributePhotos Folder + Temp Folder + File Name
            string srcPath = eventPath[0] + "/" + eventPath[1] + "/" + strSourceSourceRowPath.Substring(strSourceSourceRowPath.IndexOf(eventPath[2]) + eventPath[2].Length, strSourceSourceRowPath.Length - eventPath[2].Length); ;

            DirectoryInfo dirObj = new DirectoryInfo(destPath);

            //if directory does not exists creates a directory
            if (!dirObj.Exists)
            {
                dirObj.Create();
            }

            // Check to see if a file already exists with the same name as the file to upload.  

            string tempfileName = fileName;
            int counter = 1;

            while (File.Exists(destPath + "/" + tempfileName))
            {
                // If a file with this name already exists, add a number in the filename.
                tempfileName = counter.ToString() + fileName;
                counter = counter + 1;
            }

            fileName = tempfileName;

            // if is in edit mode then delete the previous file
            if (isEdit)
            {
                if (prevImage != "")
                {
                    string newPrevImage = destPath + "/" + Path.GetFileName(prevImage.ToString());
                    if (File.Exists(newPrevImage))
                    {
                        File.Delete(newPrevImage);
                    }

                    ViewState[PortalEnums.EventStateEnum.EventImageURL.ToString()] = "";
                }
            }

            // Copy the file from the temp location to final destination folder
            if (File.Exists(srcPath))
            {
                File.Copy(srcPath, destPath + "/" + fileName);

            }
        }
        else
        {
            //Path where you want to upload the file.
            string[] eventPath = CommonUtilities.GetPath();

        }

    }

    #endregion

}


