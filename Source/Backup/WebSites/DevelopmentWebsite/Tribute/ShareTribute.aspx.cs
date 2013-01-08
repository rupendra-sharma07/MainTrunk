///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.ShareTribute.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to share the tribute with other people 
///Audit Trail     : Date of Modification  Modified By         Description


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
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;
using Improsys.ContactImporter;
using TributesPortal.Tribute.Views;

#endregion

/// <summary>
///Tribute Portal- Share Tribute UI Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the UI class Tribute_ShareTribute for sharing the tribute. This will implement the 
// All the Properties in the IShareTribute interface. and will extend PageBase class which provides 
// 1. Error Event Handler
// 2. Exception handling
/// </summary>
/// 


public partial class Tribute_ShareTribute : PageBase, IShareTribute
{

    #region CLASS VARIABLES

    private ShareTributePresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    private EmailLink objEmail = null;

    private int _UserId;
    protected int _TributeId;
    private bool _IsAdmin;
    protected string _TributeType;
    protected string _TributeName;
    protected string _TributeUrl;
    protected string _UserName;
    private ArrayList _EmailAdd;
    private string _EmailBody;
    private string _EmailSubject;
    private string _EmailFrom;
    protected string _PageName;
    public string ReferrerURL
    {
        get
        {
            if (ViewState["url"] != null)
                return ViewState["url"].ToString();
            else
                return null;
        }
        set
        {
            ViewState["url"] = value;
        }

    }

    private IList<EventInvitationCategory> _eventInvitationCategoryList = null;
    private IList<EventTheme> _eventThemeList = null;
    #endregion

    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (ReferrerURL == null && Request.UrlReferrer != null)
                ReferrerURL = Request.UrlReferrer.ToString();
            lbtnInviteGuest.Attributes.Add("onclick", "ShowGuestCount();");
            // This Method will get the Tribute detail and user detail from the session
            GetValuesFromSession();

            // This will set the values to the controls
            SetControlsValue();

            if (!this.IsPostBack)
            {
                this._presenter.OnViewInitialized();

                //----------Sart: Added for Phase3-----------
                multiViewContact.SetActiveView(viewEmailContact);
                limyfavourite.Attributes.Remove("class");
                liFacebookImport.Attributes.Remove("class");
                limytribute.Attributes.Add("class", "yt-Selected");

                //Load categories on the basis of Tribute Type
                LoadCategory();

                LoadThemes();

                DefaultPreviewTheme();
                //----------End: Added for Phase3-----------

                // Find that user is admin or not
                UserIsAdmin();
            }

            this._presenter.OnViewLoaded();

            // Set Visibility of control on the basis of record
            SetControlsVisibility();
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
            Response.Redirect(ReferrerURL, false);
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
                string errorMsg = this._presenter.InviteGuest();

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
                lblErrMsg.InnerHtml = ShowMessage("<h2>Oops - there was a problem with your login detail..</h2>", "Invalid Login", 1);
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
        catch //(Exception ex) // commented by Ud to remove warning
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

    #endregion

    #region PROPERTIES

    [CreateNew]
    public ShareTributePresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }


    #region IShareTribute Members

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

    public string EmailBody
    {
        get
        {
            return _EmailBody;
        }
    }

    public string EmailSubject
    {
        get
        {
            return _EmailSubject;
        }
    }

    public string EmailFrom
    {
        get
        {
            return _EmailFrom;
        }
    }

    public string PersonalMessage
    {
        get
        {
            return txtMessage.Text;
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

    public int GuestCount
    {
        set
        {
            if (value == repAddressList.Items.Count)
            {
                lblGuestNotice.Text = value + " " + ResourceText.GetString("valGuestInvite_SHT");
            }
            else
            {
                lblGuestNotice.Text = value + " " + ResourceText.GetString("valGuestInvite_SHT") + "<br />" + ResourceText.GetString("valDuplicate_IG");
            }
        }
    }

    public string URL
    {
        get
        {
            return "http://" + Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath + "/Users/log_in.aspx";
        }
    }

    public string TributeType
    {
        get
        {
            return _TributeType;
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

    public int EventThemeID
    {
        get
        {
            int id = 0;
            if (ViewState["EventThemeID"] != null)
            {
                id = int.Parse(ViewState["EventThemeID"].ToString());
            }
            return id;
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
            Response.Cache.SetExpires(DateTime.Now);

            // Get values from session
            StateManager objStateManager = StateManager.Instance;

            // To get user id from session as user is logged in user
            objSessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);
            if (objSessionValue != null)
            {
                _UserId = objSessionValue.UserId;
                _UserName = objSessionValue.UserName;
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
                _TributeUrl = objTribute.TributeUrl;
            }

            // to get tribute detail from session
            objEmail = (EmailLink)objStateManager.Get(PortalEnums.SessionValueEnum.ShareTributeEmail.ToString(), StateManager.State.Session);
            if (objEmail != null)
            {
                _EmailBody = objEmail.EmailBody;
                _EmailSubject = objEmail.EmailSubject;

                if (objEmail.FromEmailAddress != null && objEmail.FromEmailAddress.Length != 0)
                    _EmailFrom = objEmail.FromEmailAddress;
                else
                {
                    _EmailSubject = "Someone" + objEmail.EmailSubject;
                    //_EmailBody = "Someone" + objEmail.EmailBody;
                    //{
                    //    if (_EmailBody.Contains("<p>"))
                    //        _EmailBody = _EmailBody.Remove(_EmailBody.IndexOf("<p>"), 3);
                    //}
                    _EmailFrom = "Your Tribute <" + WebConfig.ForgetPassAdmin + ">";
                }
                SetPageName(objEmail.TypeName);
            }

            // If user and Tribute id is blank then redirect to the login page
            if ((_TributeId == 0) || (objEmail == null))
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

            lblShare.Text = ResourceText.GetString("lblShare_IG");              //Share Tribute
            //lblShareMsg1.Text = ResourceText.GetString("lblShareMsg1_IG");      //You have chosen to share this Tribute with others!
            //lblShareMsg2.Text = ResourceText.GetString("lblShareMsg2_IG");      //Using this tool you can email the web address (and a quick message if you would like) to people who you’d like to share the site with. If the choose to sign-up, they can add content to the tribute!
            lblEmail.Text = ResourceText.GetString("lblEmail_IG");              //Select an email service to import contacts from:
            lblUserName.Text = ResourceText.GetString("lblUserName_IG");        //Username:
            lblPassword.Text = ResourceText.GetString("lblPassword_IG");        //Password:
            lbtnLogin.Text = ResourceText.GetString("lbtnLogin_IG");            //Log in
            lblEmailHead.Text = ResourceText.GetString("lblEmailHead_IG");            //Enter Email Addresses
            //lblEmailAddress.Text = ResourceText.GetString("lblEmailAddress_IG");   // Enter multiple email addresses (separate with a , or ;):
            //lblContactHead.Text = lblContact.Text = ResourceText.GetString("lblContact_IG");          //Import Contacts

            lbtnAddContact.Text = lbtnAdd.Text = ResourceText.GetString("lbtnAdd_IG");    //Add
            lbtnCancel.Text = ResourceText.GetString("lbtnCancel_IG");            //Cancel
            lbtnInviteGuest.Text = ResourceText.GetString("lbtnShare_IG1");  //Invite Guest
            lblGuestInvite.Text = ResourceText.GetString("lblShareList_IG");    // Share List

            //lblPersonalMessage.Text = ResourceText.GetString("lblPersonalMessage_IG");    //Add a Personal Message:
            lblContactMsg1.Text = ResourceText.GetString("lblContactMsg1_IG");  //20 contacts have been imported from Hotmail.
            lblContactMsg2.Text = ResourceText.GetString("lblContactMsg2_IG");  //Please select who you would like to invite from the list below, then click the ‘Add’ button to continue.
            //lbtnSelectAllGuest.Text = lbtnSelectAll.Text = ResourceText.GetString("lbtnSelectAll_IG");    //Select All
            //lbtnDeselectAllGuest.Text = lbtnDeselectAll.Text = ResourceText.GetString("lbtnDeselectAll_IG");//Deselect All

            valCheckValidEmail.ErrorMessage = ResourceText.GetString("valCheckValidEmail_ME");
            valEmailAddress.ErrorMessage = ResourceText.GetString("valEmailAddress_ME");
            valUserName.ErrorMessage = ResourceText.GetString("valUserName_ME");
            valPassword.ErrorMessage = ResourceText.GetString("valPassword_ME");
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
    /// Find that user is admin or not and add the value in view state
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
    /// This method is to set the previos page name to move to back
    /// </summary>
    /// <param name="typeName"></param>
    private void SetPageName(string typeName)
    {
        string tributeHome;
        if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
        {
            tributeHome = Session["APP_PATH"] + _TributeUrl;
        }
        else
        {
            tributeHome = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." +
                WebConfig.TopLevelDomain + "/" + _TributeUrl;
        }
        tributeHome += "/";
        if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
        {
            string query_string = "?TributeType=" + HttpUtility.UrlEncode(_TributeType);
            tributeHome = tributeHome + query_string;
        }
        if (ReferrerURL == null)
        {
            ReferrerURL = tributeHome;
        }

        if (typeName == PortalEnums.TributeContentEnum.Story.ToString())
        {
            lnkBreadcrumbs.InnerHtml = "<a href='../" + _TributeUrl + "'>Tribute Home</a>";
            lnkBreadcrumbs.InnerHtml += "<a href='../" + _TributeUrl + "/story.aspx'>Story</a>";
            lnkBreadcrumbs.InnerHtml += "<span class='selected'>Share Tribute </span>";
            lbtnPreviousPage.HRef = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeUrl + "/story.aspx"; //".." + PortalConstants.STORY_PAGE;
        }
        else if (typeName == PortalEnums.TributeContentEnum.Gift.ToString())
        {
            lnkBreadcrumbs.InnerHtml = "<a href='../" + _TributeUrl + "'>Tribute Home</a>";
            lnkBreadcrumbs.InnerHtml += "<a href='../" + _TributeUrl + "/Gift.aspx'>Gift</a>";
            lnkBreadcrumbs.InnerHtml += "<span class='selected'>Share Tribute </span>";
            lbtnPreviousPage.HRef = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeUrl + "/Gift.aspx"; //".." + PortalConstants.GIFT_PAGE;
        }
        else if (typeName == PortalEnums.TributeContentEnum.Event.ToString())
        {
            lnkBreadcrumbs.InnerHtml = "<a href='../" + _TributeUrl + "'>Tribute Home</a>";
            lnkBreadcrumbs.InnerHtml += "<a href='../" + _TributeUrl + "/events.aspx'>Events</a>";
            lnkBreadcrumbs.InnerHtml += "<span class='selected'>Share Tribute </span>";
            lbtnPreviousPage.HRef = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeUrl + "/events.aspx"; // ".." + PortalConstants.EVENT_PAGE;
        }
        else if (typeName == "TributeHomePage")
        {
            lnkBreadcrumbs.InnerHtml = "<a href='../" + _TributeUrl + "'>Tribute Home</a>";
            lnkBreadcrumbs.InnerHtml += "<span class='selected'>Share Tribute </span>";
            // Added by Ashu on Oct 4, 2011 for rewrite URL 
            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                lbtnPreviousPage.HRef = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeUrl + "/MomentsHomePage.aspx";
            else
                lbtnPreviousPage.HRef = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeUrl + "/tributehomepage.aspx";

        }
        else if (typeName.ToLower() == "tributenotes")
        {
            lnkBreadcrumbs.InnerHtml = "<a href='../" + _TributeUrl + "'>Tribute Home</a>";
            lnkBreadcrumbs.InnerHtml += "<a href='notes.aspx'>Notes</a>";
            lnkBreadcrumbs.InnerHtml += "<span class='selected'>Share Tribute </span>";
            lbtnPreviousPage.HRef = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeUrl + "/notes.aspx";
        }
        else if (typeName.ToLower() == "videogallery")
        {
            lnkBreadcrumbs.InnerHtml = "<a href='../" + _TributeUrl + "'>Tribute Home</a>";
            lnkBreadcrumbs.InnerHtml += "<a href='../" + _TributeUrl + "/videos.aspx'>Videos</a>";
            lnkBreadcrumbs.InnerHtml += "<span class='selected'>Share Tribute </span>";
            lbtnPreviousPage.HRef = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeUrl + "/videos.aspx";
        }
        else if (typeName.ToLower() == "photogallery")
        {
            lnkBreadcrumbs.InnerHtml = "<a href='../" + _TributeUrl + "'>Tribute Home</a>";
            lnkBreadcrumbs.InnerHtml += "<a href='../" + _TributeUrl + "/photos.aspx'>Photo</a>";
            lnkBreadcrumbs.InnerHtml += "<span class='selected'>Share Tribute </span>";
            lbtnPreviousPage.HRef = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeUrl + "/photos.aspx";
        }
        else if (typeName.ToLower() == "guestbook")
        {
            lnkBreadcrumbs.InnerHtml = "<a href='../" + _TributeUrl + "'>Tribute Home</a>";
            lnkBreadcrumbs.InnerHtml += "<a href='../" + _TributeUrl + "/guestbook.aspx'>Guestbook</a>";
            lnkBreadcrumbs.InnerHtml += "<span class='selected'>Share Tribute </span>";
            lbtnPreviousPage.HRef = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeUrl + "/guestbook.aspx";
        }
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

        ddlEventCategories.Items.Insert(0, new ListItem("All " + _TributeType + " eCards", "0"));

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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="imageURL"></param>
    /// <returns></returns>
    private string GetImageURL(string imageURL)
    {
        string retImageURL = string.Empty;
        return retImageURL = imageURL.Replace("../", Session["APP_BASE_DOMAIN"].ToString());

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dlDesign_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.SelectedItem)
        {
            ImageButton imgDesign = (ImageButton)e.Item.FindControl("imgDesign");
            imgDesign.ImageUrl = GetImageURL(imgDesign.ImageUrl);
        }
    }

    #endregion

    
}//end class