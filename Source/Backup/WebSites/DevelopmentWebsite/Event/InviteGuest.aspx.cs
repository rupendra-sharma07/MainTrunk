///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Event.Event.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to invite people to his/her event that is being organized.
///Audit Trail     : Date of Modification  Modified By         Description
///
#region USING DIRECTIVES

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Text;
using System.Xml;
using System.IO;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Event.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;
using Improsys.ContactImporter;
using Facebook;
using Facebook.Web;
#endregion

/// <summary>
///Tribute Portal- Invite Guest UI Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the UI class Event_InviteGuest for inviting the guest. This will implement the 
// All the Properties in the IManageEvent interface. and will extend PageBase class which provides 
// 1. Error Event Handler
// 2. Exception handling
/// </summary>
/// 

public partial class Event_InviteGuest : PageBase, IInviteGuest
{
    #region CLASS VARIABLES

    private InviteGuestPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    FacebookWebContext fbwebContext;
    private int _UserId;
    protected int _TributeId;
    private int _EventId;
    private bool _IsAdmin;
    protected string _TributeType;
    protected string _TributeName;
    protected string _TributeURL;
    protected string _UserName;
    protected string _FbReqAction = "";
    protected string _FbReqContent = "";
    private ArrayList _EmailAdd;
    private string _FirstName = "";
    private string _LastName = "";
    private IList<EventInvitationCategory> _eventInvitationCategoryList = null;
    private IList<EventTheme> _eventThemeList = null;
    public string _EventName = string.Empty;
    protected bool _IsAskForMeal;
    #endregion

    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "")
        {
            lblShareMsg2.Text = @"Choose a design of an invitation to email to your guests and include a personal message. Next, enter your guests email address manually or import them from another website. Your email invitation will also include a summary of event details and a link to your moments website.";
        }
        fbwebContext = FacebookWebContext.Current;
        try
        {
            StateManager objStateManager = StateManager.Instance;
            _EventName = (string)objStateManager.Get("EventName", StateManager.State.Session);

           //--Resolved Breadcrumbs issue by Ashu -----//
            if (_EventName == null && Request.QueryString["EventName"] != null)
            {
                _EventName = Request.QueryString["EventName"].ToString();
            }
          
            lbtnInviteGuest.Attributes.Add("onclick", "ShowGuestCount();");
            // This Method will get the Tribute detail and user detail from the session
            GetValuesFromSession();

            if(_TributeType!=null)
                Session["WebsiteTributeType"] = _TributeType.ToLower().Replace("new baby", "newbaby");

            aEventNameHome.HRef = Session["APP_PATH"] + _TributeURL + "/event.aspx?EventID=" + _EventId + "&TributeID=" + _TributeId;
            
            //Start - 
            if (_TributeName != null) Page.Title = _TributeName + " | " + _EventName;
            //End
 
            // This will set the values to the controls
            SetControlsValue();

            aTributeHome.HRef = Session["APP_PATH"] + _TributeURL + "/";

            if (!this.IsPostBack)
            {
                this._presenter.OnViewInitialized();

                multiViewContact.SetActiveView(viewEmailContact);
                limyfavourite.Attributes.Remove("class");
                liFacebookImport.Attributes.Remove("class");
                limytribute.Attributes.Add("class", "yt-Selected");
                
                //Load categories on the basis of Tribute Type
                LoadCategory();

                LoadThemes();

                DefaultPreviewTheme();

                // Find that user is admin or not
                UserIsAdmin();                
            }

            this._presenter.OnViewLoaded();

            // Set Visibility of control on the basis of record
            SetControlsVisibility();
            SetFacebookStuffAndLinks();

            //if (_connectSession.IsConnected())
            if (FacebookWebContext.Current.Session != null) // check facebook session
            {
                SetFacebookRequest();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SetFacebookStuffAndLinks()
    {
        // I want to bypass "presenter" because who knows, maybe proper place
        // for that piece is "manageevent" page. It is here just 
        // because it it "redirection target" after event creation.
        EventManager event_mgr = new EventManager();
        
        Events eventParam = new Events();
        eventParam.TributeType = _TributeType;
        eventParam.EventID = _EventId;
        eventParam.UserId = _UserId;
        eventParam.TributeId = _TributeId;

        // get the Image List, Event Type, Country List, and Event Detail
        Events objEvent = event_mgr.GetEventInfo(eventParam);

        string tributeHome;
        string eventUrl;
        if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
        {
            tributeHome = Session["APP_PATH"] + _TributeURL;
        }
        else
        {
            tributeHome = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." +
                WebConfig.TopLevelDomain + "/" + _TributeURL;
        }
        tributeHome += "/";
        eventUrl = tributeHome + "event.aspx";

        string query_string = string.Empty;
        if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
        {
            query_string = "?TributeType=" + HttpUtility.UrlEncode(_TributeType);
            eventUrl = eventUrl + query_string;
            tributeHome = tributeHome + query_string;
        }
        eventUrl += (eventUrl.Contains("?") ? "&" : "?") + "EventID=" + _EventId.ToString()+
            "&TributeID=" + _TributeId.ToString();

        aTributeHome.HRef = tributeHome;
        eventWallTributeHome.Text = tributeHome;

        eventWallPostSubject.Text = string.Format("{0} created the event: {1} {2} {3}",
            _FirstName, _TributeName, _TributeType, objEvent.EventName);
        eventWallLink.Text = eventUrl;
        eventWallLink1.Text = eventUrl;
        eventWallLink2.Text = eventUrl;
        eventWallWhen.Text = DateTime.Parse(objEvent.EventDate.ToString()).ToString("MMMM dd, yyyy") + ", " + objEvent.EventStartTime + " - " + objEvent.EventEndTime;
        eventWallWhere.Text = objEvent.EventPlace;
        eventWallImage.Text = CommonUtilities.GetPath()[2].ToString()+objEvent.EventImage;
        //ScriptManager.RegisterClientScriptBlock
    }

    protected void SetFacebookRequest(){
        string Eventname = string.Empty;
        if (Request.QueryString["EventName"] != null)
        {
            Eventname = Request.QueryString["EventName"].ToString();
        }
        string GuestPassUrl = string.Empty;

        StringBuilder sb = new StringBuilder();
        StringBuilder gp = new StringBuilder();
        
        if (WebConfig.ApplicationMode.Equals("local"))
        {
            sb.Append(Session["APP_BASE_DOMAIN"]);
        }
        else
        {
            sb.Append("http://");
            sb.Append(_TributeType.Replace("New Baby", "newbaby").ToLower());
            sb.Append(".");
            sb.Append(WebConfig.TopLevelDomain);
            sb.Append("/");

            gp.Append(sb.ToString());
        }
        gp.Append(sb.ToString());

        sb.Append(_TributeURL);
        sb.Append("/event.aspx");
        sb.Append("?EventID=");
        sb.Append(_EventId);
        if (WebConfig.ApplicationMode.Equals("local"))
        {
            sb.Append("&TributeType=");
            sb.Append(HttpUtility.UrlEncode(_TributeType));
        }
        _FbReqAction = sb.ToString();

        gp.Append("log_in.aspx?EventID=");
        gp.Append(_EventId);
        gp.Append("&TributeUrl=");
        gp.Append(_TributeURL);
        gp.Append("&TributeType=");
        gp.Append(HttpUtility.UrlEncode(_TributeType));
        GuestPassUrl = gp.ToString();

        sb.Remove(0, sb.Length);
        sb.Append("<fb:name uid=\"");
        //sb.Append(_connectSession.UserId);
        sb.Append(fbwebContext.UserId);
        sb.Append("\" />  invited you to the event \"");
        sb.Append(Eventname);
        sb.Append("\". To RSVP and see more details, follow the link ");
        sb.Append("<a href='");
        sb.Append(GuestPassUrl);
        sb.Append("'>");
        sb.Append(_FbReqAction);
        sb.Append("</a>.<fb:req-choice url=\"");
        sb.Append(GuestPassUrl);
        sb.Append("\" label=\"RSVP Event ");
        sb.Append(Eventname);
        sb.Append("\"/>");

        _FbReqContent = Server.HtmlEncode(sb.ToString());
    }

    protected void rdoHotmail_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            // Visible Login controls if hotmail is clicked
            if (rdoHotmail.Checked == true)
            {
                lblEmailExtension.Text = "@hotmail.com";
                divContactImportLogin.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void rdoYahoo_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            // Visible Login controls if Yahoo is clicked
            if (rdoYahoo.Checked == true)
            {
                lblEmailExtension.Text = "@yahoo.com";
                divContactImportLogin.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void rdoGmail_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            // Visible Login controls if Gmail is clicked
            if (rdoGmail.Checked == true)
            {
                lblEmailExtension.Text = "@gmail.com";
                divContactImportLogin.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void rdoAOL_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            // Visible Login controls if AOL is clicked
            if (rdoAOL.Checked == true)
            {
                lblEmailExtension.Text = "@aol.com";
                divContactImportLogin.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnAddContact_Click(object sender, EventArgs e)
    {
        try
        {
            bool duplicate = false;

            // check if user enetrs some email address
            if (txtEmailAddresses.Text != "")
            {
                // split the email addresses
                char[] sep = { ',', ';' };
                string[] arr = txtEmailAddresses.Text.Split(sep);

                // Get the Address List from View State
                _EmailAdd = (ArrayList)ViewState["Event_AddressList"];

                // If no email address no exist in the view state then allocate teh memory to the email
                if (_EmailAdd == null)
                {
                    _EmailAdd = new ArrayList();
                }

                // Now match if entered email address already exist in the repeater.
                // If yes then remove dupliacte entries
                if (_EmailAdd != null)
                {
                    if (arr.Length > 0)
                    {
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if ((arr[i].Trim() != "") && (!(_EmailAdd.Contains(arr[i].Trim()))))
                            {
                                _EmailAdd.Add(arr[i].Trim());
                            }
                            else
                            {
                                duplicate = true;
                            }
                        }
                    }
                }

                // bind the repaeter with the unique email address list
                repAddressList.DataSource = _EmailAdd;
                repAddressList.DataBind();

                // Enable the inviteGuest and remove button
                lbtnInviteGuest.Enabled = true;
                lbtnRemove.Enabled = true;

                // Add the Address List in View State
                ViewState.Add("Event_AddressList", _EmailAdd);

                txtEmailAddresses.Text = "";

                // if entries are duplicate then show the message
                if (duplicate)
                {
                    lblErrMsg.InnerHtml = ShowMessage(ResourceText.GetString("valHeader_IC"), ResourceText.GetString("valDuplicate_ME"), 0);
                    lblErrMsg.Visible = true;
                }
                else
                {
                    lblErrMsg.InnerHtml = "";
                    lblErrMsg.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // TBD - need to make changes after contact importer implemented
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            bool duplicate = false;

            // check if user enetrs some email address
            if (repContactList.Items.Count > 0)
            {
                // Get the Address List from View State
                _EmailAdd = (ArrayList)ViewState["Event_AddressList"];

                // If no email address no exist in the view state then allocate the memory to the email
                if (_EmailAdd == null)
                {
                    _EmailAdd = new ArrayList();
                }

                // Now match if entered email address already exist in the repeater.
                // If yes then remove dupliacte entries
                if (_EmailAdd != null)
                {
                    for (int i = 0; i <= repContactList.Items.Count - 1; i++)
                    {
                        Label lblContactEmail = (Label)repContactList.Items[i].FindControl("lblContactEmail");
                        CheckBox chkAdd = (CheckBox)repContactList.Items[i].FindControl("chkContact");

                        if ((lblContactEmail != null) && (chkAdd != null))
                        {
                            if ((chkAdd.Checked == true) && (lblContactEmail.Text != ""))
                            {
                                if (!(_EmailAdd.Contains(lblContactEmail.Text)))
                                {
                                    _EmailAdd.Add(lblContactEmail.Text);
                                }
                                else
                                {
                                    duplicate = true;
                                }
                            }
                        }
                    }
                }

                // bind the repaeter with the unique email address list
                repAddressList.DataSource = _EmailAdd;
                repAddressList.DataBind();

                lbtnInviteGuest.Enabled = true;
                lbtnRemove.Enabled = true;

                // Add the Address List in View State
                ViewState.Add("Event_AddressList", _EmailAdd);

                txtEmailAddresses.Text = "";

                // close the Imported contact list pop up
                ScriptManager.RegisterClientScriptBlock(lbtnAdd, GetType(), "CloseContactPopup", "closeContactPopup();", true);

                // if entries are duplicate then show the message
                if (duplicate)
                {
                    lblErrMsg.InnerHtml = ShowMessage(ResourceText.GetString("valHeader_IC"), ResourceText.GetString("valDuplicate_ME"), 0);
                    lblErrMsg.Visible = true;
                }
                else
                {
                    lblErrMsg.InnerHtml = "";
                    lblErrMsg.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            // Redirect to the list of events
            Response.Redirect("~/" + Session["TributeURL"] + "/events.aspx", false);
            //Redirect.RedirectToPage(Redirect.PageList.Event.ToString())
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnInviteGuest_Click(object sender, EventArgs e)
    {
        try
        {
            if (repAddressList.Items.Count > 0)
            {
                String Eventname = string.Empty;
                if (Request.QueryString["EventName"] != null)
                {
                    Eventname = Request.QueryString["EventName"].ToString();
                }

                string errorMsg = this._presenter.InviteGuest(Eventname);

                if ((errorMsg != null) && (errorMsg != ""))
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.InnerHtml = ShowMessage(ResourceText.GetString("valHeader_IC"), errorMsg, 1);
                }
                else
                {
                    lblErrMsg.InnerHtml = "";
                    lblErrMsg.Visible = false;

                    // Display the popup which dispalys the status that how many guests are invited
                    //ScriptManager.RegisterClientScriptBlock(lbtnInviteGuest, GetType(), "GuestPopup", "doContactSend();", true);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string[] nameArray;
            string[] emailArray;
            string emailsite = GetEmailService();

            ContactImporter importer = new ContactImporter(txtUserName.Text, txtPassword.Text, emailsite);
            importer.login();

            bool isLogin = importer.logged_in;

            if (isLogin)
            {
                importer.getcontacts();

                lblErrMsg.InnerHtml = "";
                lblErrMsg.Visible = false;
            }
            else
            {
                lblErrMsg.InnerHtml = ShowMessage("<h2>Oops - there was a problem with your login detail..</h2>", "Invalid Login detail", 1);
                lblErrMsg.Visible = true;
                return;
            }

            nameArray = importer.nameArray;
            emailArray = importer.emailArray;

            if ((emailArray.Length > 0) && (nameArray.Length > 0))
            {
                IList<Contacts> lstContacts = new List<Contacts>();

                for (int i = 0; i < emailArray.Length; i++)
                {
                    Contacts tmpContacts = new Contacts();

                    tmpContacts.ConatctName = nameArray[i];
                    tmpContacts.Email = emailArray[i];

                    lstContacts.Add(tmpContacts);
                    tmpContacts = null;
                }

                lbtnInviteGuest.Enabled = true;
                lbtnRemove.Enabled = true;

                // close the popup
                ScriptManager.RegisterClientScriptBlock(lbtnLogin, GetType(), "ClosePopup", "modalClose();", true);

                repContactList.DataSource = lstContacts;
                repContactList.DataBind();

                lblContactMsg1.Text = emailArray.Length + " contacts have been imported from " + emailsite.Substring(0, emailsite.IndexOf('.'));

                // open the contact list popup
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "ContactPopup", "doModalContact();", true);
            }
            else
            {
                // close the popup
                ScriptManager.RegisterClientScriptBlock(lbtnLogin, GetType(), "ClosePopup", "modalClose();", true);

                lblErrMsg.InnerHtml = ShowMessage("No contacts found", 0);
                lblErrMsg.Visible = true;
            }
        }
        catch //(Exception ex) // by Ud for warning
        {
            // close the popup
            ScriptManager.RegisterClientScriptBlock(lbtnLogin, GetType(), "ClosePopup", "modalClose();", true);

            lblErrMsg.InnerHtml = ShowMessage("<h2>Oops - there was a problem with your login detail..</h2>", "can't connect to server", 1);
            lblErrMsg.Visible = true;
        }
        finally
        {
            // close the popup
            ScriptManager.RegisterClientScriptBlock(lbtnLogin, GetType(), "ClosePopup", "modalClose();", true);
        }
    }

    protected void lbtnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            // Get the Address List from View State
            _EmailAdd = (ArrayList)ViewState["Event_AddressList"];

            if (_EmailAdd == null)
            {
                return;
            }
            bool check = false;
            for (int i = 0; i <= repAddressList.Items.Count - 1; i++)
            {
                Label lblemail = (Label)repAddressList.Items[i].FindControl("lblEmail");
                CheckBox chkemail = (CheckBox)repAddressList.Items[i].FindControl("chkAddress");
                if (chkemail != null)
                {
                    if (chkemail.Checked == true)
                    {
                        _EmailAdd.Remove(lblemail.Text);
                        check = true;
                    }
                }
            }
            if (check == false)
            {
                lblErrMsg.InnerHtml = ShowMessage("<h2>Oops - there was a problem with your login detail..</h2>", ResourceText.GetString("valEmailNotSelected_Share"), 0);
                lblErrMsg.Visible = true;
            }
            else
            {
                lblErrMsg.Visible = false;
            }

            // bind the repaeter with the unique email address list
            repAddressList.DataSource = _EmailAdd;
            repAddressList.DataBind();
            //set the control (check box ) checked porperty
            for (int i = 0; i <= repAddressList.Items.Count - 1; i++)
            {
                Label lblemail = (Label)repAddressList.Items[i].FindControl("lblEmail");
                CheckBox chkemail = (CheckBox)repAddressList.Items[i].FindControl("chkAddress");
                if (chkemail != null)
                {
                    chkemail.Checked = false;
                }
            }
            // Add the Address List in View State
            ViewState.Add("Event_AddressList", _EmailAdd);

            // Count the Guest
            ScriptManager.RegisterClientScriptBlock(lbtnAdd, GetType(), "countGuest", "countGuests();", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dlDesign_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("DesignClick"))
        {
            int themeID = (int)dlDesign.DataKeys[(int)e.Item.ItemIndex];

            this._presenter.LoadTheme(themeID);
            ViewState["EventThemeID"] = themeID;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEventCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.LoadEventThemes(InvitationCategoryID, _TributeType);
        
            this.LoadThemes();
            if (_eventThemeList.Count > 0)
            {
                DefaultPreviewTheme();
            }
            else
            {
                EventThemePreview = "";
            }
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbEnterEmail_Click(object sender, EventArgs e)
    {
        multiViewContact.SetActiveView(viewEmailContact);
        limyfavourite.Attributes.Remove("class");
        liFacebookImport.Attributes.Remove("class");
        limytribute.Attributes.Add("class", "yt-Selected");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbImportEmails_Click(object sender, EventArgs e)
    {
        multiViewContact.SetActiveView(viewImportContact);
        limytribute.Attributes.Remove("class");
        liFacebookImport.Attributes.Remove("class");
        limyfavourite.Attributes.Add("class", "yt-Selected");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbImportfacebookFriends_Click(object sender, EventArgs e)
    {
        multiViewContact.SetActiveView(viewFacebookContact);
        limytribute.Attributes.Remove("class");
        limyfavourite.Attributes.Remove("class");
        liFacebookImport.Attributes.Add("class", "yt-Selected");
    }

    protected void dlDesign_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.SelectedItem)
        {
            ImageButton imgDesign = (ImageButton)e.Item.FindControl("imgDesign");
            imgDesign.ImageUrl = GetImageURL(imgDesign.ImageUrl);
        }
    }
    #endregion

    #region PROPERTIES

    [CreateNew]
    public InviteGuestPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    #region IInviteGuest Members

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

    public bool IsAdmin
    {
        get
        {
            // Get the admin value from the view state
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

    public IList<GuestList> GuestList
    {
        get
        {
            List<GuestList> lstGuest = new List<GuestList>();
            foreach (RepeaterItem dataItem in repAddressList.Items)
            {
                CheckBox chkAdd = (CheckBox)dataItem.FindControl("chkAddress");
                if (chkAdd.Checked == true)
                {
                    Label lblAdd = (Label)dataItem.FindControl("lblEmail");
                    if (lblAdd.Text != "")
                    {
                        GuestList guest = new GuestList();
                        guest.UserName = lblAdd.Text;
                        lstGuest.Add(guest);
                        guest = null;
                    }
                }
            }

            return lstGuest;
        }
    }

    public IList<EventInvitationCategory> EventInvitationCategoryList
    {        
        set
        {
            _eventInvitationCategoryList = value;
        }
    }

    public IList<EventTheme> EventThemeList
    {
        set
        {
            _eventThemeList = value;
        }
    }

    public int EventID
    {
        get
        {
            return _EventId;
        }
    }

    public int GuestCount
    {
        set
        {
            if (value == repAddressList.Items.Count)
            {
                //lblGuestNotice.Text = value + " " + ResourceText.GetString("valGuestInvite_IG");
            }
            else
            {
                //lblGuestNotice.Text = value + " " + ResourceText.GetString("valGuestInvite_IG") + "<br />" + ResourceText.GetString("valDuplicate_IG");
            }
        }
    }

    public string URL
    {
        get
        {
            string QueryString = "?EventID=" + _EventId + "&TributeURL=" + _TributeURL + "&TributeType=" + _TributeType;
            string ApplicationPath = "<a href='"+ Session["APP_BASE_DOMAIN"].ToString()+"/log_in.aspx" + QueryString+"'>";
            return ApplicationPath;
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

    public int InvitationCategoryID
    {
        get
        {
            return int.Parse(ddlEventCategories.SelectedItem.Value);
        }
    }

    public string EventThemePreview
    {
        set
        {
            imgPreviewImage.ImageUrl = GetImageURL(value);
        }
    }

    public string EventMessage
    {
        get
        {
            return txtMessage.Text;
        }
    }

    public int EventThemeID
    {
        get
        {
            int id = 0;
            if(ViewState["EventThemeID"]!=null)
            {
                id = int.Parse(ViewState["EventThemeID"].ToString());
            }
            return id;
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
                _FirstName = objSessionValue.FirstName;
                _LastName = objSessionValue.LastName;                
            }
            else
            {
                _IsAdmin = false;
            }

            // to get tribute detail from session
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
            if ((_TributeId == 0) || (_UserId == 0) || (_EventId == 0))
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

            lblShare.Text = ResourceText.GetString("lblInvite_IG");              //Invite Guests
            lblShareMsg1.Text = ResourceText.GetString("lblShareMsg1_IG");      //You have chosen to share this Tribute with others!
            lblShareMsg2.Text = ResourceText.GetString("lblShareMsg2_IG");      //Using this tool you can email the web address (and a quick message if you would like) to people who you’d like to share the site with. If the choose to sign-up, they can add content to the tribute!
            lblEmail.Text = ResourceText.GetString("lblEmail_IG");              //Select an email service to import contacts from:
            lblUserName.Text = ResourceText.GetString("lblUserName_IG");        //Username:
            lblPassword.Text = ResourceText.GetString("lblPassword_IG");        //Password:
            lbtnLogin.Text = ResourceText.GetString("lbtnLogin_IG");            //Log in
            lblEmailHead.Text = ResourceText.GetString("lblEmailHead_IG");            //Enter Email Addresses
            lblEmailAddress.Text = ResourceText.GetString("lblEmailAddress_IG");   // Enter multiple email addresses (separate with a , or ;):
            //Removed for Phase2 Change
            //lblContactHead.Text = lblContact.Text = ResourceText.GetString("lblContact_IG");          //Import Contacts

            lbtnAddContact.Text = lbtnAdd.Text = ResourceText.GetString("lbtnAdd_IG");    //Add
            lbtnCancel.Text = ResourceText.GetString("lbtnSkip_IG");            //Cancel
            lbtnInviteGuest.Text = ResourceText.GetString("lbtnInviteGuest_IG");  //Invite Guest
            lblGuestInvite.Text = ResourceText.GetString("lblGuestInvite_IG");    //Guests to Invite

            lblContactMsg1.Text = ResourceText.GetString("lblContactMsg1_IG");  //20 contacts have been imported from Hotmail.
            lblContactMsg2.Text = ResourceText.GetString("lblContactMsg2_IG");  //Please select who you would like to invite from the list below, then click the ‘Add’ button to continue.
            //lbtnSelectAllGuest.Text = lbtnSelectAll.Text = ResourceText.GetString("lbtnSelectAll_IG");    //Select All
            // lbtnDeselectAllGuest.Text = lbtnDeselectAll.Text = ResourceText.GetString("lbtnDeselectAll_IG");//Deselect All

            valCheckValidEmail.ErrorMessage = ResourceText.GetString("valCheckValidEmail_ME");
            valEmailAddress.ErrorMessage = ResourceText.GetString("valEmailAddress_ME");
            valUserName.ErrorMessage = ResourceText.GetString("valUserName_ME");
            valPassword.ErrorMessage = ResourceText.GetString("valPassword_ME");

            //Added for phase 2
            lblChooseDesign.InnerText = ResourceText.GetString("lblChooseDesign_IG");
            lblEnterMessage.InnerText = ResourceText.GetString("lblEnterMessage_IG");
            lblInvitationPreview.InnerText = ResourceText.GetString("lblInvitationPreview_IG");
            imgPreviewImage.AlternateText = ResourceText.GetString("imgPreviewImage_IG");
            lbEnterEmail.Text = ResourceText.GetString("lbEnterEmail_IG");
            lbImportEmails.Text = ResourceText.GetString("lbImportEmails_IG");
            lbImportfacebookFriends.Text = ResourceText.GetString("lbImportfacebookFriends_IG");
            lblSampleEmailAddress.Text = ResourceText.GetString("lblSampleEmailAddress_IG");
            //lblMessage.Text = ResourceText.GetString("lblMessage_IG");
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
            if (repAddressList.Items.Count == 0)
            {
                lbtnInviteGuest.Enabled = false;
                lbtnRemove.Enabled = false;
            }
            else
            {
                lbtnInviteGuest.Enabled = true;
                lbtnRemove.Enabled = true;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Find that user is admin or not and add teh value in view state
    /// </summary>
    private void UserIsAdmin()
    {
        UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
        objUserInfo.UserId = _UserId;
        objUserInfo.TributeId = _TributeId;
        _IsAdmin = IsUserAdmin(objUserInfo);

        // Add the admin value in View State
        object[] objVal = { _IsAdmin };
        ViewState.Add("Event_Admin", objVal);

        txtEmailAddresses.Text = "";
        repAddressList.DataSource = null;
        repAddressList.DataBind();

        lbtnLogin.Attributes.Add("onclick", "SetValidationGroup('LoginGroup')");
        lbtnAddContact.Attributes.Add("onclick", "SetValidationGroup('EmailGroup')");
    }

    /// <summary>
    /// This method will get the Which email service is selected and create a string according to that
    /// </summary>
    /// <returns>Returns a string which contains the value according to email service </returns>
    /// 
    private string GetEmailService()
    {
        string emailService = "";

        if (rdoAOL.Checked == true)
        {
            emailService = "aol.com";
        }
        else if (rdoHotmail.Checked == true)
        {
            emailService = "hotmail.com";
        }
        else if (rdoYahoo.Checked == true)
        {
            emailService = "yahoo.com";
        }
        else if (rdoGmail.Checked == true)
        {
            emailService = "gmail.com";
        }

        return emailService;
    }

    /// <summary>
    /// Load the event themes
    /// </summary>
    private void LoadThemes()
    {
        dlDesign.DataSource = _eventThemeList;
        dlDesign.DataKeyField = "EventThemeID";
        dlDesign.DataBind();
        dlDesign.SelectedIndex = 0;
    }

    /// <summary>
    /// load the event invitation categories
    /// </summary>
    private void LoadCategory()
    {
        ddlEventCategories.DataSource = _eventInvitationCategoryList;
        ddlEventCategories.DataTextField = "InvitationCategoryName";
        ddlEventCategories.DataValueField = "InvitationCategoryID";
        ddlEventCategories.DataBind();

        ddlEventCategories.Items.Insert(0, new ListItem("All " + _TributeType + " Invitations", "0"));

    }

    /// <summary>
    /// 
    /// </summary>
    private void DefaultPreviewTheme()
    {
        if (dlDesign.Items.Count > 0)
        {
            int themeID = (int)dlDesign.DataKeys[0];
            this._presenter.LoadTheme(themeID);
            ViewState["EventThemeID"] = themeID;
        }
    }
    private string GetImageURL(string imageURL)
    {
        string retImageURL = string.Empty;
        return retImageURL = imageURL.Replace("../", Session["APP_BASE_DOMAIN"].ToString());

    }
    #endregion

      
}//end class
