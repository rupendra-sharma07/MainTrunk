///Copyright       : Copyright (c) Optimus Information
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Event.GuestList.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the Guest details of the event
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

#endregion

public partial class Event_GuestList : PageBase, IGuestList
{

    #region CLASS VARIABLES

    private GuestListPresenter _presenter;

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
    protected string _EventName;
    protected IList<CompleteGuestList> _CompleteGuestList;
    protected string _MealOptions = string.Empty;
    protected bool _IsAskForMeal;
    public string TributeTypeName;

    #endregion


    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetValuesFromSession();
            TributeTypeName = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/";

            SetControlsValue();
            aTributeHome.HRef = Session["APP_PATH"] + _TributeUrl + "/";
            aEventNameHome.HRef = Session["APP_PATH"] + _TributeUrl + "/event.aspx?EventID=" + _EventId + "&TributeID=" + _TributeId;
            
                      
            if (!this.IsPostBack)
            {
                this._presenter.OnViewInitialized();
                _EventName = lblEventName.Text;
                //Start - Modification on 9-Dec-09 for the enhancement 3 of the Phase 1
                if (_TributeName != null) Page.Title = _TributeName + " | " + _EventName;
                //End
                if (Request.QueryString["TributeId"] != null)
                {
                    if (Session["TributeSession"] == null)
                        CreateTributeSession(); //to create the tribute session values if user comest o this page from link or from favorites list.
                }

                //StateManager objStateManager = StateManager.Instance;
                //SessionValue objSessionvalue1 = (SessionValue)stateManager1.Get("objSessionvalue", StateManager.State.Session);
                //objStateManager.Add("IsAskForMeal", IsAskForMeal, StateManager.State.Session);


                //Session["IsAskForMeal"] = IsAskForMeal;
                
                // This method will add the event info in the session
                AddEventInfoInSession();
            }
            
            this._presenter.OnViewLoaded();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnAddRsvp_Click(object sender, EventArgs e)
    {
         bool chkMail = false;
         string strEmailValue="";
         try
         {
             foreach (RepeaterItem objRsvp in repRSVP.Items)
             {
                 HtmlTableCell  tdEmail = (HtmlTableCell)objRsvp.FindControl("tdEmailID");

                 strEmailValue = tdEmail.InnerText.ToString();
                 if (strEmailValue.Trim() == txtEmail.Text.Trim() && txtEmail.Text.Trim().Length > 0)
                 {
                     chkMail = true;
                     break;
                 }
             }
             if (chkMail == false)
             {
                 this._presenter.AddRsvp();
                 this._presenter.OnViewInitialized();
                 
                 StateManager objStateManager = StateManager.Instance;
                 objStateManager.Add("CompleteGuestList", _CompleteGuestList, StateManager.State.Session);

                 _EventName = lblEventName.Text;
                 txtFirstName.Text = "";
                 txtLastName.Text = "";
                 txtPhoneNumber.Text = "";
                 txtEmail.Text = "";
                 txtComment.Text = "";
                 
                 if(IsAskForMeal)
                    ddlMealOption.SelectedIndex = 0;

                 rdoEventRSVPAttending.Checked = false;
                 rdoEventRSVPMaybe.Checked = false;
                 rdoEventRSVPNot.Checked = false;
                 lblErrMsg.Visible = false;
             }
             else
             {
                 lblErrMsg.InnerHtml = SetHeaderMessage("The person with the email address " + txtEmail.Text + " has already been invited to this event.", valsError.HeaderText);
                 lblErrMsg.Visible = true;
             }
         }

         catch (Exception ex)
         {
             throw ex;
         }

    }

    protected void lbtnInviteMoreGuest_Click(object sender, EventArgs e)
    {
        // Redirect to the Invite Guest Page
        string queryString = "?EventID=" + _EventId + "&TributeID=" + _TributeId;

        if (WebConfig.ApplicationMode.Equals("local"))
        {
            Response.Redirect("~/" + Session["TributeURL"] + "/inviteguest.aspx" + queryString, false);
        }
        else
        {
            //Comment the line above and uncomment the line below for server.
            Response.Redirect("http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + Session["TributeURL"] + "/inviteguest.aspx" + queryString, false);
        }
        //Redirect.RedirectToPage(Redirect.PageList.InviteGuest.ToString())
    }

    protected void lbtnExportToCsv_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=GuestList.csv");
        HttpContext.Current.Response.ContentType = "text/csv";
        HttpContext.Current.Response.AddHeader("Pragma", "public");

        HttpContext.Current.Response.Write("RsvpDate,FirstName,LastName,PhoneNumber,Email,MealOption,RsvpStatus,Comment");
        HttpContext.Current.Response.Write(Environment.NewLine);
    
        foreach (CompleteGuestList Guest in CompleteGuestList)
        {
            HttpContext.Current.Response.Write(Guest.RsvpDate + ",");
            HttpContext.Current.Response.Write(Guest.FirstName + ",");
            HttpContext.Current.Response.Write(Guest.LastName + ",");
            HttpContext.Current.Response.Write(Guest.PhoneNumber + ",");
            HttpContext.Current.Response.Write(Guest.Email + ",");
            HttpContext.Current.Response.Write(Guest.MealOption + ",");
            HttpContext.Current.Response.Write(Guest.RsvpStatus + ",");
            HttpContext.Current.Response.Write(Guest.Comment);
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        HttpContext.Current.Response.End();
    }

    #endregion


    #region PROPERTIES

    [CreateNew]
    public GuestListPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    #region IGuestList Members

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
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
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

    public string EventName
    {
        set
        {
            lblEventName.Text = value;
            _EventName = value;
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

                divRsvpResonse.Visible = true;
            }
            else
            {
                divRsvpResonse.Visible = false;
            }
        }
        get
        {
            return _CompleteGuestList;
        }
    }

    public string MealOptions
    {
        set
        {
            _MealOptions = value;
            if (_MealOptions.Length > 0)
            {
                trMealOption.Attributes.Add("style", "display:block");
                ddlMealOption.Items.Clear();
                foreach (string item in _MealOptions.Split('#'))
                {
                    ddlMealOption.Items.Add(item);
                }
                ddlMealOption.Items.Insert(0, new ListItem("Select a meal option:","0"));
                ViewState["MealOptions"] = _MealOptions;                    
            }
            else
            {
                trMealOption.Attributes.Add("style", "display:none");
            }
        }
        get
        {
            return _MealOptions;
        }

    }

    public bool IsAskForMeal
    {
        set
        {
            _IsAskForMeal = value;
           
            if (_IsAskForMeal == true)
            {
                ddlMealOption.Visible = true;
                lblMealOption.Visible = true;
               
            }
            else
            {
                ddlMealOption.Visible = false;
                lblMealOption.Visible = false;
               
            }
           
        }
        get
        {
            return _IsAskForMeal;
        }
    }

  

    public CompleteGuestList GuestToAdd
    {
        get
        {
            CompleteGuestList _GuestToAdd = new CompleteGuestList();

            _GuestToAdd.FirstName = txtFirstName.Text;
            _GuestToAdd.LastName = txtLastName.Text;
            _GuestToAdd.PhoneNumber = txtPhoneNumber.Text;
            _GuestToAdd.Email = txtEmail.Text;
            if (ViewState["MealOptions"] != null)
            {
                _MealOptions = ViewState["MealOptions"].ToString();
            }
            if(_MealOptions.Length >0)
            {
                if (!Equals(ddlMealOption.SelectedItem.Value, "0"))
                {
                    _GuestToAdd.MealOption = ddlMealOption.SelectedItem.Value;
                }
            }
            _GuestToAdd.Comment = txtComment.Text;

            if (rdoEventRSVPAttending.Checked)
                _GuestToAdd.RsvpStatus = "Attending";
            else if (rdoEventRSVPMaybe.Checked)
                _GuestToAdd.RsvpStatus = "Maybe Attending";
            else if (rdoEventRSVPNot.Checked)
                _GuestToAdd.RsvpStatus = "Not Attending";
            else
                _GuestToAdd.RsvpStatus = "Awaiting Response";

            return _GuestToAdd;
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
            }
            Session["TributeType"] = _TributeType;

            if (Request.QueryString["TributeId"] != null)
            {
                _TributeId = int.Parse(Request.QueryString["TributeId"].ToString());
            }
            if (Request.QueryString["EventId"] != null)
            {
                _EventId = int.Parse(Request.QueryString["EventId"].ToString());
                Session["EventIdForEdit"] = _EventId;
            }
            if ((_TributeId == 0) || ((_EventId == 0)))
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
            lblName.Text = ResourceText.GetString("lblName_EFV");               //Name:
            lblType.Text = ResourceText.GetString("lblType_EFV");               //Type:
            lblGuest.Text = ResourceText.GetString("lblGuest_EFV");             //Guests
            //lblRSVP.Text = ResourceText.GetString("lblRSVP_EFV");               //You still need to RSVP:

            //lblRSVPAttending.InnerText = ResourceText.GetString("lblRSVPAttending_EFV");  //Attending
            //lblRSVPMaybe.InnerText = ResourceText.GetString("lblRSVPMaybe_EFV");         //Maybe Attending
            //lblRSVPNot.InnerText = ResourceText.GetString("lblRSVPNot_EFV");             //Not Attending
            lbtnAddRsvp.Text = "ADD RSVP"; //ResourceText.GetString("lbtnRSVP_EFV");                 //SAVE MY RSVP
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
    /// This method will add the Event Information in the session
    /// </summary>
    private void AddEventInfoInSession()
    {
        Events objEvent = new Events();

        // Add values in session
        StateManager objStateManager = StateManager.Instance;
        objStateManager.Add("EventInfo", objEvent, StateManager.State.Session);
        objStateManager.Add("CompleteGuestList", _CompleteGuestList, StateManager.State.Session);
    }

    #endregion
}