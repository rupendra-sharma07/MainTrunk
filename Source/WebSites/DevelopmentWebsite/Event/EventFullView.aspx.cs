///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Event.EventFullView.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the details of the selected event
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
using TributesPortal.Event.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.BusinessLogic;

#endregion

/// <summary>
///Tribute Portal- Event Full View UI Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the UI class Event_EventFullView for the full view of the Event. This will implement the 
// All the Properties in the IEventFullView interface. and will extend PageBase class which provides 
// 1. Error Event Handler
// 2. Exception handling


public partial class Event_EventFullView : PageBase, IEventFullView
{

    #region CLASS VARIABLES

    private EventFullViewPresenter _presenter;

    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;

    private int _UserId;
    protected int _TributeId;
    private int _EventId;
    private int _EventUserID;
    private bool _IsAdmin;
    protected string _TributeType;
    protected string _TributeName;
    private string _TributeUrl;
    protected bool _isActive;
    protected string _UserName;
    protected string _ShowMapParam = "";
    protected string _EventName;
    protected IList<CompleteGuestList> _CompleteGuestList = new List<CompleteGuestList>();
    protected string _Hashcode;
    protected string _MealOptions;
    protected bool _AllowAdditionalPeople;
    protected bool _ShowRsvpStatistics;
    protected bool _IsAskForMeal;
    protected bool _isPrivate;
    public static int countRSVP;
    
    #endregion


    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetValuesFromSession();

            // lbtnSaveMyRsvp.Attributes.Add("onclick", "ShowRSVPStatus();");

            SetControlsValue();
            aTributeHome.HRef = Session["APP_PATH"] + _TributeUrl + "/" + this.Master.query_string;
            //lbtnSaveMyRsvp.Attributes.Add("onclick", "ShowRSVPStatus();");



            if (!this.IsPostBack)
            {
                //ViewState["Counter"] = 1;
                this._presenter.OnViewInitialized();

                if (Request.QueryString["TributeId"] != null || Request.QueryString["WebsiteURL"] != null)
                {
                    if (Session["TributeSession"] == null)
                        CreateTributeSession(); //to create the tribute session values if user comest o this page from link or from favorites list.
                }

                // Set Visibility of control on the basis of user right and total number of gifts
                SetControlsVisibility();

                // This method will add the event info in the session
                AddEventInfoInSession();

                //IList<CompleteGuestList> lstCompleteGuestList = _presenter.GetEmailIdsForEvent(_CompleteGuestList[0].GuestId);// eventMgr.GetEmailIdsForEvent(_CompleteGuestList[0].GuestId);
                //ViewState["countRSVP"] = lstCompleteGuestList.Count;

                ViewState["Counter"] = 0;
            }

            this._presenter.OnViewLoaded();

            //lnkEditEvent.NavigateUrl = Redirect.RedirectToPage(Redirect.PageList.ManageEvent.ToString()) + "?EventID=" + _EventId;
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                lnkEditEvent.NavigateUrl = Session["APP_BASE_DOMAIN"] + TributeUrl + "/manageevent.aspx" +
                    this.Master.query_string + (string.Empty.Equals(this.Master.query_string) ? "?" : "&") +
                    "EventID=" + _EventId;
            }
            else
            {
                //Uncomment the line below and comment the line above for server.
                lnkEditEvent.NavigateUrl = "http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/manageevent.aspx" + "?EventID=" + _EventId;
            }

            // Show the Map
            ShowGoogleMap();

            _EventName = lblEventName.Text;
            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add("EventName", lblEventName.Text, StateManager.State.Session);

            //Start - Modification on 9-Dec-09 for the enhancement 3 of the Phase 1
            if (_TributeName != null) Page.Title = _TributeName + " | " + _EventName;
            //End

            _IsAskForMeal = (bool)objStateManager.Get("IsAskForMeal", StateManager.State.Session);
            foreach (RepeaterItem objRsvp in repRSVP.Items)
            {
                DropDownList ddlRSVPMeal = (DropDownList)objRsvp.FindControl("ddlMealOption");
                Label lblRSVPMeal = (Label)objRsvp.FindControl("lblMealOption");
                if (_IsAskForMeal == true)
                {
                    ddlRSVPMeal.Visible = true;
                    lblRSVPMeal.Visible = true;
                }
                else
                {
                    ddlRSVPMeal.Visible = false;
                    lblRSVPMeal.Visible = false;
                }
            }

            int topHeight = 0;
            string tributeEndDate;
            string appDomian;
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
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "ReachLimitModalPopup('location.href','document.title'" + "');", true);

                }
            }

        }
        catch (Exception ex)
        {
            Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/Error404.aspx");
        }
    }

    protected void lbtnSaveMyRsvp_Click(object sender, EventArgs e)
    {
        try
        {
            PopulateCompleteGuestListFromRepeater();
            string[] objstrarr = new string[15];
            bool isDuplicateEmail = false;
            string duplicateEmail = string.Empty;
            int i = -1;
            foreach (RepeaterItem objRsvp in repRSVP.Items)
            {
                if (objRsvp.ItemIndex > -1)
                {
                    i++;
                    TextBox txtEmail = (TextBox)objRsvp.FindControl("txtEmail");

                    objstrarr[i] = txtEmail.Text.Trim();
                }
            }
            for (int count = 0; count < objstrarr.Length; count++)
            {
                if (isDuplicateEmail == false)
                {
                    for (int counter = 0; counter < objstrarr.Length; counter++)
                    {
                        if (count != counter)
                        {
                            if (objstrarr[counter] != null && objstrarr[counter].Length > 0)
                                if (objstrarr[count] == objstrarr[counter])
                                {
                                    duplicateEmail = objstrarr[count];
                                    isDuplicateEmail = true;
                                    break;
                                }
                        }
                    }
                }
            }

            IList<CompleteGuestList> lstCompleteGuestList = _presenter.GetEmailIdsForEvent(_CompleteGuestList[0].GuestId);// eventMgr.GetEmailIdsForEvent(_CompleteGuestList[0].GuestId);
            

           //s this._presenter.OnViewInitialized();


            for (int count = Convert.ToInt32(ViewState["Counter"].ToString()) + 1; count < objstrarr.Length; count++)
            {
                if (objstrarr[count] != null)
                {
                    for (int j = 0; j < lstCompleteGuestList.Count; j++)
                    {
                        if (isDuplicateEmail == false)
                        {

                            if ((lstCompleteGuestList[j].Email == objstrarr[count] && lstCompleteGuestList[j].GuestId != 0) && objstrarr[count].ToString().Length > 0)
                            {
                                duplicateEmail = objstrarr[count];
                                isDuplicateEmail = true;
                                break;
                            }

                        }
                        else
                            break;
                    }
                }
            }



            

            if (isDuplicateEmail == true)
            {
                lblErrMsg.InnerHtml = SetHeaderMessage("The person with the email address " + duplicateEmail + " has already been invited to this event.", valsError.HeaderText);
                lblErrMsg.Visible = true;
                //ViewState["Counter"] = 0;
            }
            else
            {

                lblErrMsg.Visible = false;
                //this._presenter.SaveRsvp(Convert.ToInt32(ViewState["Counter"].ToString()) + 1, lstCompleteGuestList,Convert .ToInt32(ViewState["countRSVP"].ToString ()));
                //_presenter.SaveRsvp2(lstCompleteGuestList,_EventId);
               this._presenter.SaveRsvp();
               
                ViewState["Counter"] = ddlTotalGuests.SelectedIndex;
                

            }
            isDuplicateEmail = false;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void lbtnInviteMoreGuest_Click(object sender, EventArgs e)
    //{
    //    // Redirect to the Invite Guest Page
    //    string queryString = "?EventID=" + _EventId + "&TributeID=" + _TributeId;

    //    if (WebConfig.ApplicationMode.Equals("local"))
    //    {
    //        Response.Redirect("~/" + Session["TributeURL"] + "/inviteguest.aspx" + queryString, false);
    //    }
    //    else
    //    {
    //        //Comment the line above and uncomment the line below for server.
    //        Response.Redirect("http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + Session["TributeURL"] + "/inviteguest.aspx" + queryString, false);
    //    }
    //    //Redirect.RedirectToPage(Redirect.PageList.InviteGuest.ToString())
    //}

    protected void ddlTotalGuests_SelectedIndexChanged(object sender, EventArgs e)
    {
       // ViewState["Counter"] = ddlTotalGuests.SelectedIndex;
        PopulateCompleteGuestListFromRepeater();

        int OldTotalGuestCount = _CompleteGuestList.Count;
        int NewTotalGuestCount = int.Parse(ddlTotalGuests.SelectedValue);

        if (NewTotalGuestCount > OldTotalGuestCount)
        {
            for (int i = OldTotalGuestCount; i < NewTotalGuestCount; i++)
                _CompleteGuestList.Add(null);
        }
        else if (NewTotalGuestCount < OldTotalGuestCount)
        {
            for (int i = OldTotalGuestCount - 1; i >= NewTotalGuestCount; i--)
                _CompleteGuestList.RemoveAt(i);
        }

        repRSVP.DataSource = _CompleteGuestList;
        repRSVP.DataBind();
    }

    protected void repRSVP_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList ddlMealOption = (DropDownList)e.Item.FindControl("ddlMealOption");
            ddlMealOption.Items.Clear();
            if (_MealOptions != null && _MealOptions.Length > 0)
            {
                foreach (string item in _MealOptions.Split('#'))
                {
                    ddlMealOption.Items.Add(item);
                }
                ddlMealOption.Items.Insert(0, new ListItem("Select a meal option:", "0"));
            }
            else
                e.Item.FindControl("trMealOption").Visible = false;

            CompleteGuestList CompleteGuestList = (CompleteGuestList)e.Item.DataItem;

            StateManager objStateManager = StateManager.Instance;
            objSessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);
            if (CompleteGuestList != null)
            {
                ((TextBox)e.Item.FindControl("txtFirstName")).Text = CompleteGuestList.FirstName;
                ((TextBox)e.Item.FindControl("txtLastName")).Text = CompleteGuestList.LastName;
                ((TextBox)e.Item.FindControl("txtPhoneNumber")).Text = CompleteGuestList.PhoneNumber;
                ((TextBox)e.Item.FindControl("txtEmail")).Text = CompleteGuestList.Email;

                if (ddlMealOption.Items.Count > 0)
                {
                    if (ddlMealOption.Items.FindByText(CompleteGuestList.MealOption) != null)
                        ddlMealOption.Items.FindByText(CompleteGuestList.MealOption).Selected = true;
                }

                if (CompleteGuestList.RsvpStatus == "Attending")
                    ((RadioButtonList)e.Item.FindControl("rblRSVP")).SelectedIndex = 0;
                if (CompleteGuestList.RsvpStatus == "Maybe Attending")
                    ((RadioButtonList)e.Item.FindControl("rblRSVP")).SelectedIndex = 1;
                if (CompleteGuestList.RsvpStatus == "Not Attending")
                    ((RadioButtonList)e.Item.FindControl("rblRSVP")).SelectedIndex = 1;

                txtComment.Text = CompleteGuestList.Comment;
            }
        }
    }

    #endregion


    #region PROPERTIES

    [CreateNew]
    public EventFullViewPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    #region IEventFullView Members

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
            if (value == 0)
            {
                //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
                Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/Error404.aspx");
            }
        }
    }

  

    public string TributeName
    {
        get
        {
            return _TributeName;
        }

        set
        {
            _TributeName = value;
        }
    }

    public string TributeUrl
    {
        get
        {
            return _TributeUrl;
        }

        set
        {
            _TributeUrl = value;
        }
    }

    public string TributeType
    {
        get
        {
            return _TributeType;
        }

        set
        {
            _TributeType = value;
        }
    }

    public string EventCreatedBy
    {
        set
        {
            ddCreatedBy.InnerHtml = "<a id='lnkCreatedBy' runat='server' href='javascript:void(0);' onclick=\"UserProfileModal_1('" + _EventUserID + "');\">" + value + "</a>";
        }

        get
        {
            return "";
        }
    }

    public string UserName
    {
        get
        {
            return _UserName;
        }
    }

    public string EventTypeName
    {
        set
        {
            lblEventType.Text = value;
        }
    }

    public string Location
    {
        set
        {
            lblEventLocation.Text = value;
        }
    }

    public string City
    {
        set
        {
            lblEventCity.Text = value;
        }
    }

    public string EventName
    {
        set
        {
            lblEventName.Text = value;
            _EventName = value;
        }
    }

    public string EventDesc
    {
        set
        {
            if (value == "")
            {
                divDesc.Visible = false;
            }
            else
            {
                lblEventDesc.Text = value;
                if (!string.IsNullOrEmpty(value))
                    Master.fbDescription = value;
            }
        }
    }

    public string EventImage
    {
        set
        {
            string[] virtualDir = CommonUtilities.GetPath();
            imgEventImage.Src = virtualDir[2] + value;
            imgEventImage.Alt = _TributeName;
            if(!string.IsNullOrEmpty(value))
                Master.fbThumbnail = virtualDir[2] + value;
        }
    }

    public string Address
    {

        set
        {
            lblEventAddress.Text = value;
        }
    }

    public DateTime EventDate
    {

        set
        {
            lblEventDate.Text = value.ToString("MMMM dd, yyyy");
        }
    }

    public string EventTime
    {
        set
        {
            lblEventTime.Text = value;
        }
    }

    public string State
    {
        set
        {
            if (value == "")
            {
                value = "None";
            }

            lblEventState.Text = value;
        }
    }

    public string Country
    {
        set
        {
            lblEventCountry.Text = value;
        }
    }

    public string HostName
    {
        set
        {
            lblEventHostName.Text = value;
        }
    }

    public string PhoneNumber
    {
        set
        {
            if (value == "")
            {
                value = "None";
            }

            lblEventPhone.Text = value;
        }
    }

    public string EmailId
    {
        set
        {
            if (value == "")
            {
                value = "None";
            }

            lblEmailHost.Text = value;
        }
    }

    public int AttendingCount
    {
        set
        {
            lblAttendingCount.Text = value.ToString().Trim();
        }
    }

    public int MaybeAttendingCount
    {
        set
        {
            lblMaybeAttendingCount.Text = value.ToString().Trim();
        }
    }

    public int NotAttendingCount
    {
        set
        {
            lblNotAttendingCount.Text = value.ToString().Trim();
        }
    }

    public int AwaitingCount
    {
        set
        {
            lblAwaitingCount.Text = value.ToString().Trim();
        }
    }

    public string Invited
    {
        get
        {
            string eventStatus = "";

            return eventStatus;
        }
        set
        {

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

    public IList<CompleteGuestList> CompleteGuestList
    {
        set
        {
            if (value.Count > 0)
            {
                _CompleteGuestList = value;
                repRSVP.DataSource = value;
                repRSVP.DataBind();
                ddlTotalGuests.Items.FindByText(value .Count.ToString()).Selected = true; 
                //ddlTotalGuests.SelectedIndex = ddlTotalGuests.SelectedIndex;
                //ddlTotalGuests.SelectedIndex = Convert.ToInt32(ViewState["Counter"].ToString());
                //ddlTotalGuests.Items.FindByText(ddlTotalGuests.SelectedIndex.ToString()).Selected = true;
            }
        }
        get
        {
            return _CompleteGuestList;
        }
    }

    public string Hashcode
    {
        get
        {
            return _Hashcode;
        }

        set
        {
            _Hashcode = value;
        }
    }

    public string MealOptions
    {
        set
        {
            _MealOptions = value;
        }
    }

    public bool IsAskForMeal
    {
        set
        {
            _IsAskForMeal = value;


        }
        get
        {
            return _IsAskForMeal;
        }
    }

    public bool AllowAdditionalPeople
    {
        set
        {
            _AllowAdditionalPeople = value;
        }
    }

    public bool ShowRsvpStatistics
    {
        set
        {
            _ShowRsvpStatistics = value;
        }
    }
    public bool IsPrivate
    {
        get
        {
            return _isPrivate;
        }
        set
        {
            _isPrivate = value;
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

            _CompleteGuestList = (IList<CompleteGuestList>)objStateManager.Get("CompleteGuestList", StateManager.State.Session);
            _MealOptions = (string)objStateManager.Get("MealOptions", StateManager.State.Session);

            //to get user id from session as user is logged in user
            objSessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);
            if (objSessionValue != null)
            {
                _UserId = objSessionValue.UserId;
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

                if (Request.QueryString["TributeURL"] != null)
                    _TributeUrl = Request.QueryString["TributeURL"].ToString();

                if (Session["TributeSession"] == null)
                    CreateTributeSession(); //to create the tribute session values if user comest o this page from link or from favorites list.
            }
            else if (objTribute != null)
            {
                _TributeId = objTribute.TributeId;
                _TributeName = objTribute.TributeName;
                _TributeType = objTribute.TypeDescription;
                _TributeUrl = objTribute.TributeUrl;
                _isActive = objTribute.IsActive;
                //_isPrivate = objTribute.IsPrivate;
                //_IsAskForMeal = objTribute .
            }

            if (Request.QueryString["TributeId"] != null)
            {
                _TributeId = int.Parse(Request.QueryString["TributeId"].ToString());
            }
            if (Request.QueryString["EventId"] != null)
            {
                _EventId = int.Parse(Request.QueryString["EventId"].ToString());
                Session["EventIdForEdit"] = _EventId;
            }
            if (Request.QueryString["Hashcode"] != null)
            {
                _Hashcode = Request.QueryString["Hashcode"].ToString();
            }

            if ((_TributeId == 0) || ((_EventId == 0)))
            {
                //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
                Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/Error404.aspx");
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
        objTribute.TributeUrl = _TributeUrl;
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
            lblEventinfo.Text = ResourceText.GetString("lblEventinfo_EFV");     //Event Information
            lnkEditEvent.Text = ResourceText.GetString("lnkEditEvent_EFV");     //Edit Event
            lblName.Text = ResourceText.GetString("lblName_EFV");               //Name:
            lblType.Text = ResourceText.GetString("lblType_EFV");               //Type:
            lblCreatedBy.Text = ResourceText.GetString("lblCreatedBy_EFV");     //Created By:
            lblTimePlace.Text = ResourceText.GetString("lblTimePlace_EFV");     //Time and Place
            lblDate.Text = ResourceText.GetString("lblDate_EFV");               //Date:
            lblTime.Text = ResourceText.GetString("lblTime_EFV");               //Time:
            lblLocation.Text = ResourceText.GetString("lblLocation_EFV");       //Location:
            lblAddress.Text = ResourceText.GetString("lblAddress_EFV");         //Address:
            lblCity.Text = ResourceText.GetString("lblCity_EFV");               //City:
            lblState.Text = ResourceText.GetString("lblState_EFV");             //State:
            lblCountry.Text = ResourceText.GetString("lblCountry_EFV");         //Country:
            lblEventHost.Text = ResourceText.GetString("lblEventHost_EFV");     //Event Host
            lblHost.Text = ResourceText.GetString("lblHost_EFV");               //Host:
            lblPhone.Text = ResourceText.GetString("lblPhone_EFV");             //Phone:
            lblEmail.Text = ResourceText.GetString("lblEmail_EFV");             //Email:
            lblDesc.Text = ResourceText.GetString("lblDesc_EFV");               //Event Description
            lblGuest.Text = ResourceText.GetString("lblGuest_EFV");             //Guests
            //lblRSVP.Text = ResourceText.GetString("lblRSVP_EFV");               //You still need to RSVP:

            //lblRSVPAttending.InnerText = ResourceText.GetString("lblRSVPAttending_EFV");  //Attending
            //lblRSVPMaybe.InnerText = ResourceText.GetString("lblRSVPMaybe_EFV");         //Maybe Attending
            //lblRSVPNot.InnerText = ResourceText.GetString("lblRSVPNot_EFV");             //Not Attending
            lbtnSaveMyRsvp.Text = ResourceText.GetString("lbtnRSVP_EFV");                 //SAVE MY RSVP
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
            //to set visibility of Edit button

            Nullable<DateTime> eventDate = null;
            if (lblEventDate.Text != "")
            {
                eventDate = DateTime.Parse(lblEventDate.Text);
            }
            valFirstNameSelfRSVP.EnableClientScript = false;
            valLastNameSelfRSVP.EnableClientScript = false;
            valFirstNameSelfRSVP.Visible = false;
            valLastNameSelfRSVP.Visible = false;

            lnkEditEvent.Visible = false;
            //lbtnInviteMoreGuest.Visible = false;
            fsSelfRSVP.Style.Add(HtmlTextWriterStyle.Display, "none");
            ((HtmlGenericControl)this.Master.FindControl("liViewEventGuestList")).Visible = false;
            ((HtmlGenericControl)this.Master.FindControl("liInviteMoreGuests")).Visible = false;
            if (_IsAdmin)
            {
                if (eventDate != null)
                {
                    if (eventDate > DateTime.Now)
                    {
                        lnkEditEvent.Visible = true;
                        //lbtnInviteMoreGuest.Visible = true;

                        if (WebConfig.ApplicationMode.Equals("local"))
                        {
                            ((HtmlGenericControl)this.Master.FindControl("liViewEventGuestList")).InnerHtml = "<a href='" + Session["APP_BASE_DOMAIN"] + Session["TributeURL"] + "/guestlist.aspx?EventId=" + _EventId + "&TributeID=" + _TributeId + "&EventName=" + _EventName + "'>" + "View Event Guest List" + "</a>";
                            ((HtmlGenericControl)this.Master.FindControl("liViewEventGuestList")).Visible = true;
                            ((HtmlGenericControl)this.Master.FindControl("liInviteMoreGuests")).InnerHtml = "<a href='" + Session["APP_BASE_DOMAIN"] + Session["TributeURL"] + "/inviteguest.aspx?EventId=" + _EventId + "&TributeID=" + _TributeId + "&EventName=" + _EventName + "'>" + "Invite More Guests" + "</a>";
                            ((HtmlGenericControl)this.Master.FindControl("liInviteMoreGuests")).Visible = true;
                        }
                        else
                        {
                            ((HtmlGenericControl)this.Master.FindControl("liViewEventGuestList")).InnerHtml = "<a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/guestlist.aspx?EventId=" + _EventId + "&TributeID=" + _TributeId + "&EventName=" + _EventName + "'>" + "View Event Guest List" + "</a>";
                            ((HtmlGenericControl)this.Master.FindControl("liViewEventGuestList")).Visible = true;
                            ((HtmlGenericControl)this.Master.FindControl("liInviteMoreGuests")).InnerHtml = "<a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/inviteguest.aspx?EventId=" + _EventId + "&TributeID=" + _TributeId + "&EventName=" + _EventName + "'>" + "Invite More Guests" + "</a>";
                            ((HtmlGenericControl)this.Master.FindControl("liInviteMoreGuests")).Visible = true;
                        }

                    }
                }
                else
                {
                    lnkEditEvent.Visible = true;
                    //lbtnInviteMoreGuest.Visible = true;
                    if (WebConfig.ApplicationMode.Equals("local"))
                    {
                        ((HtmlGenericControl)this.Master.FindControl("liViewEventGuestList")).InnerHtml = "<a href='" + Session["APP_BASE_DOMAIN"] + Session["TributeURL"] + "/guestlist.aspx?EventId=" + _EventId + "&TributeID=" + _TributeId + "&EventName=" + _EventName + "'>" + "View Event Guest List" + "</a>";
                        ((HtmlGenericControl)this.Master.FindControl("liViewEventGuestList")).Visible = true;
                        ((HtmlGenericControl)this.Master.FindControl("liInviteMoreGuests")).InnerHtml = "<a href='" + Session["APP_BASE_DOMAIN"] + Session["TributeURL"] + "/inviteguest.aspx?EventId=" + _EventId + "&TributeID=" + _TributeId + "&EventName=" + _EventName + "'>" + "Invite More Guests" + "</a>";
                        ((HtmlGenericControl)this.Master.FindControl("liInviteMoreGuests")).Visible = true;
                    }
                    else
                    {
                        ((HtmlGenericControl)this.Master.FindControl("liViewEventGuestList")).InnerHtml = "<a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/guestlist.aspx?EventId=" + _EventId + "&TributeID=" + _TributeId + "&EventName=" + _EventName + "'>" + "View Event Guest List" + "</a>";
                        ((HtmlGenericControl)this.Master.FindControl("liViewEventGuestList")).Visible = true;
                        ((HtmlGenericControl)this.Master.FindControl("liInviteMoreGuests")).InnerHtml = "<a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/inviteguest.aspx?EventId=" + _EventId + "&TributeID=" + _TributeId + "&EventName=" + _EventName + "'>" + "Invite More Guests" + "</a>";
                        ((HtmlGenericControl)this.Master.FindControl("liInviteMoreGuests")).Visible = true;
                    }
                }
            }

            if (_UserId > 0)
            {
                divRsvpStatistics.Visible = true;
                fsTotalGuests.Visible = true;
            }
            else
            {
                divRsvpStatistics.Visible = _ShowRsvpStatistics;
                fsTotalGuests.Visible = _AllowAdditionalPeople;
            }

            if (Equals(_IsAdmin, false))
            {
                if (_Hashcode == null)
                {
                    //fsTotalGuests.Style.Add(HtmlTextWriterStyle.Display, "none");
                    //repRSVP.Visible = false;
                    //fsSelfRSVP.Style.Add(HtmlTextWriterStyle.Display, "block");
                    fsSelfRSVP.Style.Add(HtmlTextWriterStyle.Display, "none");

                    //valFirstNameSelfRSVP.Visible = true;
                    //valLastNameSelfRSVP.Visible = true;
                    //valFirstNameSelfRSVP.EnableClientScript = true;
                    //valLastNameSelfRSVP.EnableClientScript = true;
                    txtComment.Text = string.Empty;

                    //if (_IsAskForMeal)
                    //{
                    //    trMealOptionSelfRSVP.Style.Add(HtmlTextWriterStyle.Display, "block");
                    //    ddlMealOptionSelfRSVP.Items.Clear();
                    //    if (_MealOptions != null && _MealOptions.Length > 0)
                    //    {
                    //        foreach (string item in _MealOptions.Split('#'))
                    //            ddlMealOptionSelfRSVP.Items.Add(item);
                    //    }

                    //}
                    //else
                    //    trMealOptionSelfRSVP.Style.Add(HtmlTextWriterStyle.Display, "none");

                    repRSVP.Visible = true;
                    divRsvpStatistics.Visible = _ShowRsvpStatistics;
                    fsTotalGuests.Visible = _AllowAdditionalPeople;
                    _CompleteGuestList = new List<CompleteGuestList>();
                    _CompleteGuestList.Add(null);
                    repRSVP.DataSource = _CompleteGuestList;
                    repRSVP.DataBind();
                }
                else
                {
                    repRSVP.Visible = true;
                    fsSelfRSVP.Style.Add(HtmlTextWriterStyle.Display, "none");

                    divRsvpStatistics.Visible = _ShowRsvpStatistics;
                    fsTotalGuests.Visible = _AllowAdditionalPeople;
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// This method will display the Google map
    /// </summary>
    private void ShowGoogleMap()
    {
        _ShowMapParam = "'" + lblEventAddress.Text + "', '" + lblEventCity.Text + "', '" + lblEventState.Text + "', '" + lblEventCountry.Text + "'";
    }

    /// <summary>
    /// This method will add the Event Information in the session
    /// </summary>
    private void AddEventInfoInSession()
    {
        Events objEvent = new Events();

        objEvent.EventStartTime = "When: " + lblEventDate.Text + "-" + lblEventTime.Text;
        objEvent.EventPlace = "Where: " + lblEventLocation.Text + "<br/> " + lblEventAddress.Text + "<br/> " + lblEventCity.Text + ", " + lblEventState.Text + "<br/>" + lblEventCountry.Text;

        // Add values in session
        StateManager objStateManager = StateManager.Instance;
        objStateManager.Add("EventInfo", objEvent, StateManager.State.Session);
        objStateManager.Add("CompleteGuestList", _CompleteGuestList, StateManager.State.Session);
        objStateManager.Add("MealOptions", _MealOptions, StateManager.State.Session);
        objStateManager.Add("IsAskForMeal", _IsAskForMeal, StateManager.State.Session);
    }

    private void PopulateCompleteGuestListFromRepeater()
    {
        int i = -1;
        foreach (RepeaterItem objRsvp in repRSVP.Items)
        {
            if (objRsvp.ItemIndex > -1)
            {
                i++;
                if (_CompleteGuestList[i] == null) _CompleteGuestList[i] = new CompleteGuestList();
                if (_CompleteGuestList[i] != null)
                {
                    TextBox txtFirstName = (TextBox)objRsvp.FindControl("txtFirstName");
                    _CompleteGuestList[i].FirstName = txtFirstName != null ? txtFirstName.Text : "";
                    TextBox txtLastName = (TextBox)objRsvp.FindControl("txtLastName");
                    _CompleteGuestList[i].LastName = txtLastName != null ? txtLastName.Text : "";
                    TextBox txtPhoneNumber = (TextBox)objRsvp.FindControl("txtPhoneNumber");
                    _CompleteGuestList[i].PhoneNumber = txtPhoneNumber != null ? txtPhoneNumber.Text : "";
                    TextBox txtEmail = (TextBox)objRsvp.FindControl("txtEmail");
                    _CompleteGuestList[i].Email = txtEmail != null ? txtEmail.Text : "";

                    DropDownList ddlMealOption = (DropDownList)objRsvp.FindControl("ddlMealOption");
                    if (ddlMealOption != null && !Equals(ddlMealOption.Text, "0"))
                    {
                        _CompleteGuestList[i].MealOption = ddlMealOption.Text;
                    }

                    //RadioButton rdoEventRSVPAttending = (RadioButton)objRsvp.FindControl("rdoEventRSVPAttending");
                    //RadioButton rdoEventRSVPMaybe = (RadioButton)objRsvp.FindControl("rdoEventRSVPMaybe");
                    //RadioButton rdoEventRSVPNot = (RadioButton)objRsvp.FindControl("rdoEventRSVPNot");
                    RadioButtonList rblRSVP = ((RadioButtonList)objRsvp.FindControl("rblRSVP"));
                    if (rblRSVP.SelectedIndex == 0)
                        _CompleteGuestList[i].RsvpStatus = "Attending";
                    else if (rblRSVP.SelectedIndex == 1)
                        _CompleteGuestList[i].RsvpStatus = "Maybe Attending";
                    else if (rblRSVP.SelectedIndex == 2)
                        _CompleteGuestList[i].RsvpStatus = "Not Attending";
                    else
                        _CompleteGuestList[i].RsvpStatus = "Awaiting Response";


                    _CompleteGuestList[i].Comment = txtComment.Text;
                }
            }
        }

        //StateManager objStateManager = StateManager.Instance;
        //objStateManager.Add("CompleteGuestList", _CompleteGuestList, StateManager.State.Session);
    }

    #endregion
}