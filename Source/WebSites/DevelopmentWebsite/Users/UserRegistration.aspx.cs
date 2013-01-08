///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Users.UserRegistration.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows a user to register to the site
///Audit Trail     : Date of Modification  Modified By         Description

#region Refrences
using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Users.Views;
using TributesPortal.Utilities;
using Facebook.Web;
using PerceptiveMCAPI.Types;
using PerceptiveMCAPI.Methods;
using System.Linq;
using System.Text;
using System.Configuration;
using Facebook;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using PerceptiveMCAPI;

#endregion Refrences
/// <summary>
///Tribute Portal-Channel Home Timeless Software
//============================================, IUserRegistration
// Copyright © Timeless Software  All rights reserved.
/// </summary>

public partial class Users_UserRegistration : PageBase, IUserRegistration
{
    #region Variables
    private UserRegistrationPresenter _presenter;
    
    private string UserIsAvailable;
    protected string CityReq = "";
    private bool location_changed = false;
    const string headertext = " <h2>Oops - there was a problem with your sign up.</h2>";
    private string Email;
    #endregion Variables

    #region events

    protected override void OnPreRender(EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (FacebookWebContext.Current.Session == null)
        {
            SignUpOptions.Visible = true;
            RecaptchaPanel.Visible = true;
            RecaptchaControl1.Enabled = true;
        }
        else
        {
            SignUpOptions.Visible = false;
            RecaptchaPanel.Visible = false;
            RecaptchaControl1.SkipRecaptcha = true;
            RecaptchaControl1.Enabled = false;
        }

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AddHeader("Pragma", "no-cache");

        if (!this.IsPostBack)
        {
            ImgBtnSignMe.Attributes.Add("onclick", "HideIndecator();");
            txtEmail.Focus();
            this._presenter.OnCountryLoad(Locationid(null));
            this._presenter.OnStateLoad(Locationid(ddlCountry.SelectedValue));
            this._presenter.BusinessTypes();
            rdoPersonalAccount.Checked = true;
            pnlAccount.Visible = true;
            SetPanelsVisibility(false);

            SetControlText();

            // Added For Event Handling - Parul
            if (Request.QueryString["EventID"] != null)
            {
                Email = Request.QueryString["Email"];

                txtEmail.Text = Email;
            }
            else if (Request.QueryString["TributeUrl"] != null)
            {
                if (Request.QueryString["Email"] != null)
                    txtEmail.Text = Request.QueryString["Email"];
            }
            else if (Request.QueryString["TributeID"] != null)
            {
                if (Request.QueryString["Email"] != null)
                    txtEmail.Text = Request.QueryString["Email"];
            }
        }
        this._presenter.OnViewLoaded();
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.OnStateLoad(Locationid(ddlCountry.SelectedValue));
        location_changed = true;
    }

    protected void ddlStateProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        location_changed = true;
    }

    protected void rdoPersonalAccount_CheckedChanged(object sender, EventArgs e)
    {

        errorVerification.Visible = false;
        lblCity.Text = "<label>" + ResourceText.GetString("lblCity_UR") + "</label>";
        pnlAccount.Visible = true;
        SetPanelsVisibility(false);
        lblErrMsg.InnerHtml = "";
        lblErrMsg.Visible = false;
        txtPassword.Attributes.Add("value", "");
        txtConfirmPassword.Attributes.Add("value", "");
        chkAgreeReceiveNewsletters.Checked = false;
        if (Request.QueryString["EventID"] != null)
        {
            if (Request.QueryString["Email"] != null)
            {
                Email = Request.QueryString["Email"];
                txtEmail.Text = Request.QueryString["Email"];
            }
        }
        else if (Request.QueryString["TributeUrl"] != null)
        {
            if (Request.QueryString["Email"] != null)
                txtEmail.Text = Request.QueryString["Email"];
        }
        else if (Request.QueryString["TributeID"] != null)
        {
            if (Request.QueryString["Email"] != null)
                txtEmail.Text = Request.QueryString["Email"];
        }
    }


    protected void rdoBusinessAccount_CheckedChanged(object sender, EventArgs e)
    {
        errorVerification.Visible = false;
        lblCity.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblCity_UR") + "</label>";
        pnlAccount.Visible = true;
        SetPanelsVisibility(true);
        lblErrMsg.InnerHtml = "";
        lblErrMsg.Visible = false;
        txtPassword.Attributes.Add("value", "");
        txtConfirmPassword.Attributes.Add("value", "");
        chkAgreeReceiveNewsletters.Checked = true;
        if (Request.QueryString["EventID"] != null)
        {
            if (Request.QueryString["Email"] != null)
            {
                Email = Request.QueryString["Email"];
                txtEmail.Text = Request.QueryString["Email"];
            }
        }
        else if (Request.QueryString["TributeUrl"] != null)
        {
            if (Request.QueryString["Email"] != null)
                txtEmail.Text = Request.QueryString["Email"];
        }
        else if (Request.QueryString["TributeID"] != null)
        {
            if (Request.QueryString["Email"] != null)
                txtEmail.Text = Request.QueryString["Email"];
        }
        
    }
    protected void ImgBtnSignMe_Click(object sender, EventArgs e)
    {
        lblErrMsg.InnerHtml = "";
        lblErrMsg.Visible = false;
        try
        {
            if (!Page.IsValid)
            {
                return;
            }
            if (txtPassword.Text.Length >= 4)
            {
                SignMe();
            }
            else
            {
                lblErrMsg.InnerHtml = ShowMessage(headertext, ResourceText.GetString("ErrMsgPassWord"), 1);
                lblErrMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
        txtPassword.Attributes.Add("value", txtPassword.Text);
        txtConfirmPassword.Attributes.Add("value", txtPassword.Text);
    }

    protected void ImgBtnChkAvailabalaty_Click(object sender, EventArgs e)
    {

        if (txtUsername.Text.Length > 0)
        {
            bool avability = CheckAvailablity();
        }
        else
        {
            lblErrMsg.InnerHtml = ShowMessage(headertext, ResourceText.GetString("MsgErrChkAvaibality_UR"), 1);
            lblErrMsg.Visible = true;
        }
    }

    #endregion events

    #region Properties
    [CreateNew]
    public UserRegistrationPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    private Locations Locationid(string ID)
    {
        Locations objLocations = new Locations();
        if (ID != null)
        {
            objLocations.LocationParentId = int.Parse(ID);
        }
        else
        {
            objLocations.LocationParentId = 0;
        }
        return objLocations;

    }
    public IList<Locations> Locations
    {
        set
        {
            ddlCountry.DataSource = value;
            ddlCountry.DataTextField = "LocationName";
            ddlCountry.DataValueField = "LocationId";
            ddlCountry.DataBind();
            ddlCountry.SelectedIndex = 0;
        }
    }
    public IList<ParameterTypesCodes> BusinessType
    {
        set
        {
            if (value.Count > 0)
            {
                ddlBusinessType.DataSource = value;
                ddlBusinessType.DataTextField = ParameterTypesCodes.Parameters.TypeDescription.ToString();
                ddlBusinessType.DataValueField = ParameterTypesCodes.Parameters.TypeCode.ToString();
                ddlBusinessType.DataBind();
            }
        }
    }
    public IList<Locations> States
    {
        set
        {
            ddlStateProvince.Items.Clear();
            if (value.Count > 0)
            {
                ddlStateProvince.DataSource = value;
                ddlStateProvince.DataTextField = "LocationName";
                ddlStateProvince.DataValueField = "LocationId";
                ddlStateProvince.DataBind();
                ddlStateProvince.SelectedIndex = 0;
                ddlStateProvince.Enabled = true;
            }
            else
            {
                ddlStateProvince.Enabled = false;
            }
        }
    }

    private void setLocationFromFacebookLocation(DropDownList ddl, string loc_name){
        ddl.ClearSelection();
        foreach (ListItem item in ddl.Items)
        {
            if(item.Text.Equals(loc_name)){
                item.Selected = true;
                break;
            }
        }
    }

    #endregion Properties

    #region Methods
    /// <summary>
    /// MaqilChimp Integartion
    /// </summary>
    /// <param name="userType"></param>
    /// <returns></returns>
    private bool AddMailChimpSubscriber(int userType)
    {
        bool returnVal = false;
        try
        {
            if (chkAgreeReceiveNewsletters.Checked == true)
            {
                listSubscribeInput input = new listSubscribeInput();
                listSubscribe Subscribe = new listSubscribe();
                input.api_AccessType = PerceptiveMCAPI.EnumValues.AccessType.Serial;
                input.api_CustomErrorMessages = true;
                input.api_MethodType = PerceptiveMCAPI.EnumValues.MethodType.POST;
                input.api_Validate = true;
                input.api_OutputType = PerceptiveMCAPI.EnumValues.OutputType.XML;
                input.parms.email_address = txtEmail.Text;
                input.parms.send_welcome = true;
                input.parms.update_existing = true;
                input.parms.replace_interests = true;
                input.parms.double_optin = false;
                
                input.parms.apikey = WebConfig.MailChimpApiKeyNew;
                input.parms.id = WebConfig.UserNewsLetterListID;

                input.parms.merge_vars.Add("EMAIL", txtEmail.Text);
                // ------------------------------ address

                List<interestGroupings> groupings = new List<interestGroupings>();
                interestGroupings ig = new interestGroupings { name = "Account Status", groups = new List<string> { "New User Personal" } };
                if (userType == 2)
                {
                    ig = new interestGroupings { name = "Account Status", groups = new List<string> { "New User Business" } };
                }
                groupings.Add(ig);

                input.parms.merge_vars.Add("groupings", groupings);

                // execution

                listSubscribeOutput output = Subscribe.Execute(input);
                //phase-1 enhancement


                if ((output != null) && (output.api_ErrorMessages.Count > 0))
                {
                    string ErrorCode = output.api_ErrorMessages.FirstOrDefault().code;
                    string Error = "Error occured. " + output.api_ErrorMessages.FirstOrDefault().error;

                    lblErrMsg.InnerHtml = ShowMessage(headertext, ErrorCode + "</br>" + Error, 1);
                    lblErrMsg.Visible = true;
                    returnVal = false;
                }
                else
                {
                    if (output.result == true)
                    {
                        returnVal = true;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblErrMsg.InnerHtml = ShowMessage(headertext, ex.Message, 1);
            lblErrMsg.Visible = true;
            returnVal = false;
        }
        return returnVal;
    }


    public string UserAvailablity
    {
        set
        {
            UserIsAvailable = value;
        }
    }
    private void SetControlText()
    {
        /* Set CallOut Box Messages */
        // CBCity.Text = ResourceText.GetString("txtCity_UR");
        CBCompanyName.Text = ResourceText.GetString("txtBusinessName_UR");
        if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourmoments")
        {
        CBEmail.Text = ResourceText.GetString("txtEmail_UR1");
        // CBFirstName.Text = ResourceText.GetString("txtFirstName_UR");
        CBUsername.Text = ResourceText.GetString("txtUsername_UR1");
        }
        else
        {
            CBEmail.Text = ResourceText.GetString("txtEmail_UR");
        // CBFirstName.Text = ResourceText.GetString("txtFirstName_UR");
        CBUsername.Text = ResourceText.GetString("txtUsername_UR");
        }

        //  CBLastName.Text = ResourceText.GetString("txtLastName_UR");
        CBPassword.Text = ResourceText.GetString("txtPassword_UR");
        // CBStreetAddress.Text = ResourceText.GetString("txtStreetAddress_UR");
        CBWebsiteAddress.Text = ResourceText.GetString("txtWebsiteAddress_UR");
        // CBZipCode.Text = ResourceText.GetString("txtZipCode_UR");
        // CBVerification.Text = ResourceText.GetString("txtVerification_UR");
        CBConfirmPassword.Text = ResourceText.GetString("txtConfirmPassword_UR");
        /* Set Validator Messages */
        rEvEmail.ErrorMessage = ResourceText.GetString("rEvEmail_UR");
        rfvEmail.ErrorMessage = ResourceText.GetString("rfvEmail_UR");
        rfvFirstName.ErrorMessage = ResourceText.GetString("rfvFirstName_UR");
        rfvPassword.ErrorMessage = ResourceText.GetString("rfvPassword_UR");
        rfvUsername.ErrorMessage = ResourceText.GetString("rfvUsername_UR");
        // rfvVerification.ErrorMessage = ResourceText.GetString("rfvVerification_UR");
        rfvZipCode.ErrorMessage = ResourceText.GetString("rfvZipCode_UR");
        cvAcceptPolicies.ErrorMessage = ResourceText.GetString("cvAcceptPolicies_UR");


        ///* Set Control Text */
        // rdoBusinessAccount.Text = ResourceText.GetString("rdoBusinessAccount_UR");
        // rdoPersonalAccount.Text = ResourceText.GetString("rdoPersonalAccount_UR");

        /*Set Labels Text*/
        // lblSignUp.Text = ResourceText.GetString("lblSignUp_UR");
        //  lblPersonalAcctDesc.Text = ResourceText.GetString("lblPersonalAcctDesc_UR");
        // lblBusinessAcctDesc.Text = ResourceText.GetString("lblBusinessAcctDesc_UR");
        // lblPerRequiredFields.Text = ResourceText.GetString("lblPerRequiredFields_UR");

        lblEmail.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblEmail_UR") + "</label>";
        lblWebsiteAddress.Text = "<label>" + ResourceText.GetString("lblWebsiteAddress_UR") + "</label>";
        lblUsername.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblUsername_UR") + "</label>";
        lblPassword.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblPassword_UR") + "</label>";
        lblConfirmPassword.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblConfirmPassword_UR") + "</label>";
        lblBusinessName.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblBusinessName_UR") + "</label>";
        lblFirstName.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblFirstName_UR") + "</label>";
        lblLastName.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblLastName_UR") + "</label>";
        lblBusinessType.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblBusinessType_UR") + "</label>";
        lblStreetAddress.Text = "<label>" + ResourceText.GetString("lblStreetAddress_UR") + "</label>";
        lblCountry.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblCountry_UR") + "</label>";
        lblStateProvince.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblStateProvince_UR") + "</label>";
        lblCity.Text = "<label>" + ResourceText.GetString("lblCity_UR") + "</label>";
        lblPhoneNumber.Text = "<label>Phone Number:</label>";
        lblZipCode.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblZipCode_UR") + "</label>";
        // lblVerification.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblVerification_UR") + "</label>";
        //lblVerifyMsg.Text = ResourceText.GetString("lblVerifyMsg_UR");
        lblPersonalRecaptcha.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblPersonalRecaptcha") + "</label>";
        
    }
    private  UserRegistration SaveAccount()
    {
         
        int usertype = 1;
        if (rdoBusinessAccount.Checked)
            usertype = 2;

        UserRegistration objUserReg = new UserRegistration();
        int state = -1;
        if (ddlStateProvince.SelectedValue != "")
        {
            state = int.Parse(ddlStateProvince.SelectedValue);
        }
        string _Pass = TributePortalSecurity.Security.EncryptSymmetric(txtPassword.Text.ToLower());
        string _UserImage = "images/bg_ProfilePhoto.gif";
        Nullable<Int64> facebookUid = null;             
        
        var fbWebContext = FacebookWebContext.Current;
        if (FacebookWebContext.Current.Session != null)
            {
                facebookUid = fbWebContext.UserId; 
                try 
                {
                    var fbwc = new FacebookWebClient(FacebookWebContext.Current.AccessToken);
                    string fql = "Select pic_square from user where uid = " + fbWebContext.UserId;
                    JsonArray me2 = (JsonArray)fbwc.Query(fql);

                    var mm = (IDictionary<string, object>)me2[0];
                    if (!string.IsNullOrEmpty((string)mm["pic_square"])) 
                {
                    _UserImage = (string)mm["pic_square"]; // get user image
                }               
            }
            catch //(Exception ex) // commented by Ud to remove warning
            {
            }
        }

        Users objUsers = new Users(
                                    txtUsername.Text.Trim(),
                                    _Pass,
                                    txtFirstName.Text,
                                    txtLastName.Text,
                                    txtEmail.Text,
                                    "",
                                    chkAgreeReceiveNewsletters.Checked,
                                    txtCity.Text,
                                    state,
                                    int.Parse(ddlCountry.SelectedValue),
                                    usertype,facebookUid,ApplicationType
                                      );
        objUsers.UserImage = _UserImage; 
        UserBusiness objUserBus = new UserBusiness();
        //Check for Personal Account.

        //Check for Business Account.
        if (rdoBusinessAccount.Checked)
        {
            objUserBus.Website = txtWebsiteAddress.Text;
            objUserBus.CompanyName = txtBusinessName.Text;
            objUserBus.BusinessType = int.Parse(ddlBusinessType.SelectedValue);
            objUserBus.BusinessAddress = txtStreetAddress.Text;
            objUserBus.ZipCode = txtZipCode.Text;

            objUserBus.Phone = txtPhoneNumber1.Text + txtPhoneNumber2.Text + txtPhoneNumber3.Text;

            objUserReg.UserBusiness = objUserBus;
        }
        objUserReg.Users = objUsers;
        return objUserReg;
    }



    private void SignMe()
    {
        try
        {
                errorVerification.Visible = false;
                bool avability = CheckAvailablity();
                if (avability != true)
                {
                    int email = _presenter.EmailAvailable();
                    if (email == 0)
                    {
                        UserRegistration objUserReg = SaveAccount();
                        _presenter.SavePersonalAccount(objUserReg);
                        if (objUserReg.CustomError != null)
                        {
                            if (objUserReg.CustomError.ErrorMessage.Contains("Facebook"))
                            {
                                objUserReg.CustomError.ErrorMessage = objUserReg.CustomError.ErrorMessage +
                          " Please <a href=\"#\" onclick=\"fb_logout(); return false;\">" +
                          "   <img id=\"fb_logout_image\" src=\"http://static.ak.fbcdn.net/images/fbconnect/logout-buttons/logout_small.gif\" alt=\"Connect\"/>" +
                          "</a> and try again.";
                            }
                            lblErrMsg.InnerHtml = ShowMessage(headertext, objUserReg.CustomError.ErrorMessage, 1);
                            lblErrMsg.Visible = true;
                        }
                        else
                        {
                            SessionValue objSessionValue = new SessionValue(objUserReg.Users.UserId,
                                                                             objUserReg.Users.UserName,
                                                                             objUserReg.Users.FirstName,
                                                                             objUserReg.Users.LastName,
                                                                             objUserReg.Users.Email,
                                                                             objUserReg.UserBusiness == null ? 1 : 2, "Basic", objUserReg.Users.IsUsernameVisiable);
                            StateManager stateManager = StateManager.Instance;
                            stateManager.Add("objSessionvalue", objSessionValue, StateManager.State.Session);

                            if (chkAgreeReceiveNewsletters.Checked)
                            {
                                bool retval = AddMailChimpSubscriber(objUserReg.Users.UserType);
                            }

                            //Add Personal User to MailChimpLists


                            // Added For Event Handling - Parul
                            if (Request.QueryString["EventID"] != null)
                            {
                                string EventID = Request.QueryString["EventID"];
                                string TributeUrl = Request.QueryString["TributeUrl"];
                                string EmailID = Request.QueryString["Email"];
                                if (EmailID == txtEmail.Text.Trim())
                                {
                                    string queryString = "?EventID=" + EventID;
                                    if (WebConfig.ApplicationMode.Equals("local"))
                                    {
                                        Response.Redirect(Session["APP_BASE_DOMAIN"] + TributeUrl + "/event.aspx" + queryString, false);
                                    }
                                    else
                                    {
                                        Response.Redirect("http://" + Request.QueryString["TributeType"] + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/event.aspx" + queryString, false);
                                    }
                                }
                                else
                                { 
                                    // Added by Ashu on Oct 3, 2011 for rewrite URL 
                                    if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourmoments")
                                        Response.Redirect(Session["APP_BASE_DOMAIN"] + "moments.aspx", false);
                                    else
                                        Response.Redirect(Session["APP_BASE_DOMAIN"] + "tributes.aspx", false);
                                }
                            }
                            else if (Request.QueryString["TributeUrl"] != null)
                            {
                                string emailId = Request.QueryString["Email"];

                                if (emailId == txtEmail.Text.Trim())
                                {
                                    if (WebConfig.ApplicationMode.Equals("local"))
                                    {
                                        Response.Redirect(Session["APP_BASE_DOMAIN"] + Request.QueryString["TributeUrl"] + "/inviteadminconfirmation.aspx", false);
                                    }
                                    else
                                    {
                                        Response.Redirect("http://" + Request.QueryString["TributeType"].Replace("New Baby", "newbaby").ToLower() + "." + TributesPortal.Utilities.WebConfig.TopLevelDomain + "/" + Request.QueryString["TributeUrl"] + "/inviteadminconfirmation.aspx");
                                    }
                                }
                                else
                                {
                                    if (WebConfig.ApplicationMode.Equals("local"))
                                    {
                                        Response.Redirect(Session["APP_BASE_DOMAIN"] + Request.QueryString["TributeUrl"], false);
                                    }
                                    else
                                    {
                                        Response.Redirect("http://" + Request.QueryString["TributeType"].Replace("New Baby", "newbaby").ToLower() + "." + TributesPortal.Utilities.WebConfig.TopLevelDomain + "/" + Request.QueryString["TributeUrl"], false);
                                    }
                                    
                                }
                            }
                            else if (Request.QueryString["TributeID"] != null)
                            {
                                string tributeName = Request.QueryString["TributeName"];
                                string tributeId = Request.QueryString["TributeID"];
                                string emailId = Request.QueryString["Email"];

                                if (emailId == txtEmail.Text.Trim())
                                {
                                    Tributes objTribute = new Tributes();
                                    objTribute.TributeId = int.Parse(tributeId);
                                    objTribute.TributeName = tributeName;
                                    TributesPortal.Utilities.StateManager stateTributes = TributesPortal.Utilities.StateManager.Instance;
                                    stateTributes.Add("TributeSession", objTribute, TributesPortal.Utilities.StateManager.State.Session);
                                    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.UserLogin2AdminConformation.ToString()));
                                }
                                else
                                {
                                    if (WebConfig.ApplicationMode.Equals("local"))
                                    {
                                        Response.Redirect(Session["APP_BASE_DOMAIN"] + Request.QueryString["TributeUrl"], false);
                                    }
                                    else
                                    {
                                        //Uncomment the line below and comment the line above for server.
                                        Response.Redirect("http://" + Request.QueryString["TributeType"].Replace("New Baby", "newbaby").ToLower() + "." + TributesPortal.Utilities.WebConfig.TopLevelDomain + "/" + Request.QueryString["TributeUrl"], false);
                                    }
                                }
                            }
                            else if (Request.QueryString["PageName"] != null)
                            {
                                string pageName = Request.QueryString["PageName"];

                                if (pageName == "TributeCreation")
                                {
                                    string querystring = string.Empty;
                                    int accountType = 0;
                                    int.TryParse(Request.QueryString["AccountType"], out accountType);
                                    if (accountType > 0)
                                    {
                                        if (Request.QueryString["Type"] != null)
                                        {
                                            querystring = "?Type=" + Request.QueryString["Type"];
                                        }
                                        if (Request.QueryString["TributeType"] != null)
                                        {
                                            querystring = "TributeType=" + Request.QueryString["TributeType"];
                                        }
                                        if (Request.QueryString["AccountType"] != null)
                                        {
                                            if (string.IsNullOrEmpty(querystring))
                                                querystring += "AccountType=" + Request.QueryString["AccountType"];
                                            else
                                                querystring += "&AccountType=" + Request.QueryString["AccountType"];
                                        }
                                    }
                                    Response.Redirect(Session["APP_BASE_DOMAIN"] + "create.aspx" + "?" + querystring, false);
                                }
                            }

                            else
                            {
                                string str = Redirect.RedirectToPage(Redirect.PageList.UserAccounts.ToString());
                                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", Session.SessionID));
                                Response.Cookies["ASP.NET_SessionId"].Domain = "." + WebConfig.TopLevelDomain;
                                Session["mytribute"] = false;
                                // Added by Ashu on Oct 3, 2011 for rewrite URL 
                                if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourmoments")
                                    Response.Redirect(Session["APP_BASE_DOMAIN"] + "moments.aspx", false);
                                else
                                    Response.Redirect(Session["APP_BASE_DOMAIN"] + "tributes.aspx", false);
                            }
                        }
                    }
                    else
                    {
                        lblErrMsg.InnerHtml = ShowMessage(headertext, "User already exists for this email: " + txtEmail.Text, 1);//COMDIFFRES: is this message correct?
                        lblErrMsg.Visible = true;
                    }
                }
        }
        catch (Exception ex)
        {
            lblErrMsg.InnerHtml = ShowMessage(headertext, ex.Message, 1);
            lblErrMsg.Visible = true;
        }
    }

    private bool CheckAvailablity()
    {
        bool avability = false;
        UserRegistration objUserReg = new UserRegistration();
        if(string.IsNullOrEmpty(txtUsername.Text))
        {
            txtUsername.Text = GenerateUserName(txtFirstName.Text, txtLastName.Text);
        }
        Users objuser = new Users(txtUsername.Text);
        objuser.ApplicationType = this.ApplicationType;
        objUserReg.Users = objuser;
        this._presenter.UserAvailability(objUserReg);
        if (UserIsAvailable != "0")
        {
            Imgability.Attributes.Add("class", "availabilityNotice-Unavailable");
            Imgability.InnerHtml = "Unavailable";
            txtUsername.Text = "";
            avability = true;
            lblErrMsg.InnerHtml = ShowMessage(headertext, ResourceText.GetString("MsgErrNotAvailable_UR"), 1);
            lblErrMsg.Visible = true;

        }
        else
        {
            Imgability.Attributes.Add("class", "availabilityNotice-Available");
            Imgability.InnerHtml = "Available!";
            lblErrMsg.InnerHtml = "";
            lblErrMsg.Visible = false;
        }
        return avability;
    }

    private void SetPanelsVisibility(bool val)
    {
        if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourtribute")
         {
             PanelUserName.Visible = val;
             UpdatePanel1.Visible = val;
             CityPanel.Visible = val;

             if (val == false)
             {
                 PanelPassword.Attributes.Remove("class");
                 PanelPassword.CssClass = "yt-signup-Password";
                 PanelConfirmPassword.CssClass = "yt-signup-ConfirmPassword";
                 SignupNewsLetter.InnerHtml = "I would like to receive periodic newsletters from Your Tribute, including company news and special offers.";
             }
             else
             {
                 PanelPassword.CssClass = "signup-Password";
                 PanelConfirmPassword.CssClass = "signup-Password";
                 SignupNewsLetter.InnerHtml = "I would like to receive periodic newsletters, including tips and tricks when creating tributes,from Your Tribute.";
             }
         }
        else
        {
            SignupNewsLetter.InnerHtml = "I would like to receive periodic newsletters, including tips and tricks when creating websites,from Your Moments.";
        }

        txtEmail.Focus();
        lblBusinessAcctDesc.Visible = val;
        BusinessTypePanel.Visible = val;
        WebsitePanel.Visible = val;
        StreetAddressPanel.Visible = val;
        CompanyNamePanel.Visible = val;
        ZipCodePanel.Visible = val;
        PanelPhone.Visible = val;
        FirstNamePanel.Visible = !val;
        LastNamePanel.Visible = !val;
        lblPersonalAcctDesc.Visible = !val;
        txtWebsiteAddress.Text = "";
        txtEmail.Text = "";

        // Added For Event Handling - Parul
        if (Request.QueryString["EventID"] != null)
        {
            string email = Request.QueryString["Email"];

            txtEmail.Text = email;
        }
        else if (Request.QueryString["TributeID"] != null)
        {
            if (Request.QueryString["Email"] != null)
                txtEmail.Text = Request.QueryString["Email"];
        }

        txtUsername.Text = "";
        txtPassword.Text = "";
        txtConfirmPassword.Text = "";
        txtBusinessName.Text = "";
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtStreetAddress.Text = "";
        txtCity.Text = "";
        txtPhoneNumber1.Text = "";
        txtPhoneNumber2.Text = "";
        txtPhoneNumber3.Text = "";
        txtZipCode.Text = "";
        rfvtxtCity.Visible = val;
        ddlBusinessType.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
        this._presenter.OnStateLoad(Locationid(ddlCountry.SelectedValue));
        chkAgreeTermsUse.Checked = false;

        if (FacebookWebContext.Current.Session != null)
        {
            //Display user data captured from the Facebook API.  
            try
            {
                var fbwc = new FacebookWebClient(FacebookWebContext.Current.AccessToken);
                var me = (IDictionary<string, object>)fbwc.Get("me");
                string fql = "Select current_location, email from user where uid = " + (string)me["id"];
                JsonArray me2 = (JsonArray)fbwc.Query(fql);
                var mm = (IDictionary<string, object>)me2[0];

                txtFirstName.Text = (string)me["first_name"];
                txtLastName.Text = (string)me["last_name"];

                JsonObject hl = (JsonObject)mm["current_location"];

                if ((string)hl[0] != null)
                {
                    txtCity.Text = (string)hl[0];
                    setLocationFromFacebookLocation(ddlCountry, (string)hl[2]);
                    this._presenter.OnStateLoad(Locationid(ddlCountry.SelectedValue));
                    setLocationFromFacebookLocation(ddlStateProvince, (string)hl[1]);
                }

                string email_ = string.Empty; // user.proxied_email;  
                string result = (string)mm["email"];      
                if (!string.IsNullOrEmpty(result))
                {
                     email_ = result;  
                }
                txtEmail.Text = email_;
            }
            catch (Exception ex)
            {
                if (!PortalValidationSummary.Visible)
                {
                    string msg =
                        "Problems with Facebook session. Please <a href=\"#\" onclick=\"fb_logout(); return false;\">" +
                        "   <img id=\"fb_logout_image\" src=\"http://static.ak.fbcdn.net/images/fbconnect/logout-buttons/logout_small.gif\" alt=\"Connect\"/>" +
                        "</a> and try again." + ex;
                    lblErrMsg.InnerHtml = ShowMessage(headertext, msg, 0);
                    lblErrMsg.Visible = true;
                }
            }
        }
    }


    private static string Encrypt(string strText)
    {
        string key = "&%#@?,:*";
        TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        des.IV = new byte[8];
        PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, new byte[-1 + 1]);
        des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);
        MemoryStream ms = new MemoryStream((strText.Length * 2) - 1);
        CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        byte[] plainBytes = Encoding.UTF8.GetBytes(strText);
        encStream.Write(plainBytes, 0, plainBytes.Length);
        encStream.FlushFinalBlock();
        byte[] encryptedBytes = new byte[(int)ms.Length - 1 + 1];
        ms.Position = 0;
        ms.Read(encryptedBytes, 0, (int)ms.Length);
        encStream.Close();
        return Convert.ToBase64String(encryptedBytes);
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        // recaptcha.InnerHtml = GetRandomCouponNumber(6);
    }

    /// <summary>
    /// Auto generated numbers.
    /// </summary>
    /// <param name="numChars"></param>
    /// <param name="seed"></param>
    /// <returns></returns>
    public string GetRandomCouponNumber(int numChars)
    {
        string[] chars = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S",
                        "T", "U", "V", "W", "X", "Y", "Z", "2", "3", "4", "5", "6", "7", "8", "9" };

        Random rnd = new Random();
        string random = string.Empty;
        for (int i = 0; i < numChars; i++)
        {
            random += chars[rnd.Next(0, 33)];
        }
        return random;
    }
    protected void txtPassword_PreRender(object sender, EventArgs e)
    {
        txtPassword.Attributes.Add("value", txtPassword.Text);
        txtConfirmPassword.Attributes["value"] = txtConfirmPassword.Text;
    }

    private string GenerateUserName(string firstname, string lastName)
    {
        string userName = "";
        Random random = new Random();
        userName = firstname + lastName + random.Next(1, 9999);
        return userName;
    }
    #endregion Methods

    #region IUserRegistration Members


    public string UserEmail
    {
        get { return txtEmail.Text; }
    }
    public string ApplicationType
    {
        get { return ConfigurationManager.AppSettings["ApplicationType"]; }
    }

    #endregion

}


