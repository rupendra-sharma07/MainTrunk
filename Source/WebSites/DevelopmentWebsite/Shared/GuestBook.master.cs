///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Shared.GuestBook.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page is used as a general purpose master pages
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.MessagingSystem;
using TributesPortal.Miscellaneous;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Users;
using TributesPortal.Utilities;
#endregion

public partial class Shared_GuestBook : System.Web.UI.MasterPage
{
    #region CLASS VARIABLES
    protected string _userName;
    private int _userId;
    private string _typeName;
    protected int _tributeId;
    protected string _tributeType;
    protected string _tributeName;
    protected string _tributeUrl;
    private string _firstName;
    private string _lastName;
    private string _emailID;
    private int currentPage;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    private int isInFavorite = 0;
    protected string adSenseCode = string.Empty;
    protected string adSenseComment = string.Empty;
    protected string toFav = string.Empty;
    protected string toFavText = string.Empty;
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HtmlGenericControl body = (HtmlGenericControl)Page.Master.FindControl("masterBody");
            
            StateManager objStateManager = StateManager.Instance;
            //to get logged in user name from session as user is logged in user
            objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);

            objTribute = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);
            if (Request.QueryString["fbmode"] != null || Request.QueryString["mode"] != null)
            {
                if (Request.QueryString["fbmode"] == "facebook" || Request.QueryString["mode"] == "link")
                {
                    _tributeId = int.Parse(Request.QueryString["TributeId"].ToString());
                    _tributeType = Request.QueryString["TributeType"].ToString();
                    _tributeName = Request.QueryString["TributeName"].ToString();
                    _tributeUrl = Request.QueryString["TributeUrl"].ToString();
                }
                else
                    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
            }
            else if (!Equals(objTribute, null))
            {
                _tributeId = objTribute.TributeId;
                _tributeType = objTribute.TypeDescription;
                _tributeName = objTribute.TributeName;
                _tributeUrl = objTribute.TributeUrl;
            }
            else
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
            }

            if (!Equals(objSessionValue, null))
            {
                _userId = objSessionValue.UserId;
                _firstName = objSessionValue.FirstName;
                _lastName = objSessionValue.LastName;
                _userName = objSessionValue.UserName;
                _emailID = objSessionValue.UserEmail;
                //Added by deepak nagar.
                myprofile.Visible = true;
            }
            else
            {
                myprofile.Visible = false;
            }
            SetMenuOptions();
            // This Function will load themes in the left panel and loads the selected theme for the tribute.
            LoadThemes();
            if (hdnSelectedThemeValue.Value != string.Empty)
            {
                body.Attributes.Add("onload", "Themer('" + hdnSelectedThemeValue.Value + "');");
            }
            else
            {
                string themeValue = GetExistingTheme().ThemeValue;
                body.Attributes.Add("onload", "Themer('" + themeValue + "');");
            }
            //Method to check if tribute already in favorite list.
            CheckForFavorite();

            //GoogleAdSense(_tributeType);

            //to set affiliate link.
            AffiliateLinks(_tributeType);

            //to get current page number, if user clicks on page number in paging it gets tha page number from query string
            //else page number is 1
            if (Request.QueryString["PageNo"] != null)
                currentPage = int.Parse(Request.QueryString["PageNo"].ToString());
            else
                currentPage = 1;

            //hdnUrl.Value = Request.Url.ToString() + "&mode=fav";
            hdnUrl.Value = "http://" + Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath + "/GuestBook/GuestBook.aspx?TributeId=" + objTribute.TributeId + "&TributeName=" + _tributeName + "&TributeType=" + _tributeType + "&PageNo=" + currentPage + "&mode=fav";

            //to display login and logout option based on the Session value for the user.
            if (!Equals(objSessionValue, null))
                spanLogout.InnerHtml = "<a href='Logout.aspx'>Log out</a>";
            else
                spanLogout.InnerHtml = "<a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>Log in</a>";

            // Set the controls value
            SetControlsValue();

            //to set values to hidden variables for facebook
            hdnTributeId.Value = _tributeId.ToString();
            hdnTributeName.Value = _tributeName;
            hdnTributeType.Value = _tributeType;
            hdnTributeUrl.Value = _tributeUrl;

            //if (hdnTypeToMail.Value == "Tribute")
            //    SetValueForEmailInSession("Tribute");
            //else
                SetValueForEmailInSession(_typeName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnMyProfile_Click(object sender, EventArgs e)
    {

    }
    protected void lbtnEmail_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.UnderConstruction.ToString()), false);
        
        //Code is to be uncommented for use, as it's a working code
        /*SetValueForEmailInSession(_typeName);
        Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.EmailUrl.ToString()), false);*/
    }
    protected void lbtnEmailTribute_Click(object sender, EventArgs e)
    {

        StateManager stateManager = StateManager.Instance;
        EmailLink objEmail = new EmailLink();

        string EmailHref = "http://" + _tributeType + "." + WebConfig.TopLevelDomain + "/" + _tributeUrl + "</a>";
        string QueryString = "?TributeId=" + _tributeId + "&TributeName=" + _tributeName + "&TributeType=" + _tributeType + "&TributeUrl=" + _tributeUrl + "&mode=emailPage";
        string ApplicationPath = "<a href='http://" + Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;
        string UrlToEmail = ApplicationPath + "/Tribute/TributeHomePage.aspx" + QueryString + "'>" + EmailHref + "</a>";

        objEmail.EmailSubject = _firstName + " " + _lastName + " wants you to view a " + _tributeType + " on Your Tribute...";
        objEmail.TypeName = _typeName;
        objEmail.FromEmailAddress = _emailID;
        objEmail.EmailBody = _firstName + " " + _lastName + " wants you to view the " + _tributeName + " Tribute.To view the tribute, follow the link below: " + "<br/> <br/>" + UrlToEmail + "<br/> <br/>";

        stateManager.Add(PortalEnums.SessionValueEnum.ShareTributeEmail.ToString(), objEmail, StateManager.State.Session);
        Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.ShareTribute.ToString()), false);
    }
    //protected void lbtnShareOnFacebook_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.UnderConstruction.ToString()), false);
    //}
    protected void lbtnSaveTheme_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnSelectedTheme.Value != string.Empty) // || hdnSelectedTheme.Value != null)
            {
                Tributes objTribute = new Tributes();
                MiscellaneousController _controller = new MiscellaneousController();
                objTribute.TributeId = _tributeId;
                objTribute.ThemeId = int.Parse(hdnSelectedTheme.Value);
                objTribute.ModifiedBy = _userId;
                objTribute.ModifiedDate = DateTime.Now;

                _controller.UpdateTributeTheme(objTribute);
                //LoadThemes();
                
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void popuplbtnSendemail_Click(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnAddToFavorite_Click(object sender, EventArgs e)
    {
        try
        {
            AddToFavorite objFavorite = new AddToFavorite();
            int returnVal = 0;
            objFavorite.UserId = _userId;
            objFavorite.TributeId = _tributeId;
            if (isInFavorite > 0)
            {
                objFavorite.ModifiedBy = _userId;
                objFavorite.ModifiedDate = DateTime.Now;
                objFavorite.EmailAlert = chkFavoritesEmailNotifications.Checked;
                objFavorite.IsActive = true;
                objFavorite.IsDeleted = true;
            }
            else
            {
                objFavorite.CreatedBy = _userId;
                objFavorite.CreatedDate = DateTime.Now;
                objFavorite.EmailAlert = chkFavoritesEmailNotifications.Checked;
                objFavorite.IsActive = true;
                objFavorite.IsDeleted = false;
            }

            MiscellaneousController _controller = new MiscellaneousController();
            if (isInFavorite > 0)
                _controller.RemoveFromFavotire(objFavorite);
            else
                returnVal = _controller.AddToFavorites(objFavorite);

            if (returnVal > 0) //already in favorite list
            {
                lblErrMsg.InnerHtml = ShowErrorMessage(ResourceText.GetString("errFavorite_Master"));
                lblErrMsg.Visible = true;
            }
            else
            {
                lblErrMsg.Visible = false;
            }
            CheckForFavorite();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            // Create SearchTribute object
            SearchTribute objSearchTribute = new SearchTribute();

            // Assign the search parameter to this object
            objSearchTribute.TributeType = GetTributeType();
            objSearchTribute.SearchString = txtSearchKeyword.Text.ToString();
            objSearchTribute.SearchType = PortalEnums.SearchEnum.Basic.ToString();
            objSearchTribute.SortOrder = "DESC";

            // Create StateManager object and add search paramter in the session
            StateManager objStateMgr = StateManager.Instance;
            objStateMgr.Add(PortalEnums.SearchEnum.Search.ToString(), objSearchTribute, StateManager.State.Session);

            // Redirect to the Search Result page
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.SearchResult.ToString()), false);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    protected void lbtnSubmit_Click(object sender, EventArgs e)
    {
        List<string> lstEmailAddresses = new List<string>();
        string[] emailAddresses = (txtEmailAddress.Text.Replace(",", ";")).Split(';');

        foreach (string strEmailAddress in emailAddresses)
        {
            lstEmailAddresses.Add(strEmailAddress);
        }

        SendEmail(lstEmailAddresses);
    }

    
    #endregion
    
    #region METHODS
    /// <summary>
    /// Function to set left menu
    /// </summary>
    private void SetMenuOptions()
    {
        StateManager objStateManager = StateManager.Instance;

        if (Page.GetType().Name.ToLower() == "guestbook_guestbook_aspx")
        {
            UserAdminOwnerInfo objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminInfo_GuestBook", StateManager.State.Session);
            SetMenuItemsVisibility(objUserInfo.TypeName, objUserInfo.IsAdmin, objUserInfo.IsOwner, objUserInfo.UserId, objUserInfo.Mode);
            SetMenuItemText(objUserInfo.TypeName);
            _typeName = objUserInfo.TypeName;
            _tributeId = objUserInfo.TributeId;
            //_tributeType = objUserInfo.TypeName;
            objStateManager.Add("TypeName_GuestBook", objUserInfo.TypeName, StateManager.State.ViewState);
        }
    }

    /// <summary>
    /// Method to set visibility of menu options
    /// </summary>
    /// <param name="typeName">Type Name</param>
    /// <param name="isUserAdmin">Is user admin</param>
    /// <param name="isUserOwner">is user owner</param>
    /// <param name="userId">User Id</param>
    /// <param name="mode">Mode of page</param>
    private void SetMenuItemsVisibility(string typeName, bool isUserAdmin, bool isUserOwner, int userId, string mode)
    {
        if (typeName == "GuestBook")
        {
            if (isUserAdmin)
            {
                liEmailPage.Visible = true;
                liEmailTribute.Visible = true;
                liShareOnFacebook.Visible = true;
                liChangeSiteTheme.Visible = true;
                liManageTribute.Visible = true;
                liAddToFavorite.Visible = false;
                pLogin.Visible = false;
                divProfile.Visible = true;
            }
            else if (userId != 0)
            {
                liEmailPage.Visible = true;
                liEmailTribute.Visible = true;
                liShareOnFacebook.Visible = true;
                liChangeSiteTheme.Visible = false;
                liManageTribute.Visible = false;
                liAddToFavorite.Visible = true;
                pLogin.Visible = false;
                divProfile.Visible = true;
            }
            else
            {
                liEmailPage.Visible = true;
                liEmailTribute.Visible = true;
                liShareOnFacebook.Visible = true;
                liChangeSiteTheme.Visible = false;
                liManageTribute.Visible = false;
                liAddToFavorite.Visible = false;
                pLogin.Visible = true;
                divProfile.Visible = false;
            }
        }
    }

    /// <summary>
    /// Method to set text to menu options
    /// </summary>
    /// <param name="typeName">TypeName</param>
    private void SetMenuItemText(string typeName)
    {
        if (typeName == "GuestBook")
        {
            //AG: Chnage popup login function
            //pLogin.InnerHtml = ResourceText.GetString("lblloginmsg_GB_Master") + "<a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'> Log in</a> or <a href='../Users/UserRegistration.aspx'>Sign up</a>"; // ResourceText.GetString("lblloginmsg_GB_Master");
            pLogin.InnerHtml = ResourceText.GetString("lblloginmsg_GB_Master") + "<a href='javascript: void(0);' onclick='UserLoginModalpopupFromSubDomain(location.href,document.title);'> Log in</a> or <a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>Sign up</a>"; // ResourceText.GetString("lblloginmsg_GB_Master");
            //lbtnEmail.Text = ResourceText.GetString("lbtnEmailGuestbook_GB_Master"); // "Email this page";
            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
            {
                lbtnEmailTribute.Text = ResourceText.GetString("lbtnEmailTribute_GB_Master1");
                liManageTribute.InnerHtml = "<a href='../Miscellaneous/UnderConstruction.aspx'>" + ResourceText.GetString("lbtnManageTributeSite_GB_Master1") + "</a>";
            }
            else
            {
                lbtnEmailTribute.Text = ResourceText.GetString("lbtnEmailTribute_GB_Master");
                liManageTribute.InnerHtml = "<a href='../Miscellaneous/UnderConstruction.aspx'>" + ResourceText.GetString("lbtnManageTributeSite_GB_Master") + "</a>";
            }
            //liShareOnFacebook.InnerHtml = "<a href='../Miscellaneous/UnderConstruction.aspx'>" + ResourceText.GetString("lbtnShareOnFaceBook_GB_Master") + "</a>";
            liShareOnFacebook.InnerHtml = "<a href='http://www.facebook.com/share.php?u=<url>'  onclick='return fbs_click()'>" + ResourceText.GetString("lbtnShareOnFaceBook_GB_Master") + "</a>";
            //liAddToFavorite.InnerHtml = "<a href='#' onclick='return CreateBookmarkLink();'>" + ResourceText.GetString("lbtnAddtoMyFavourate_GB_Master") + "</a>";
            //liChangeSiteTheme.InnerHtml = "<a href='#' class='yt-Tool-ChangeTheme-Link'>" + ResourceText.GetString("lbtnChangeSiteTheme_GB_Master") + "</a>";
        }
    }

    /// <summary>
    /// Method to set the url to be mailed to user.
    /// </summary>
    private void SetValueForEmailInSession(string typeName)
    {
        StateManager stateManager = StateManager.Instance;
        EmailLink objEmail = new EmailLink();
        string strQueryString = "TributeId=" + _tributeId + "&TributeName=" + _tributeName + "&TributeType=" + _tributeType + "&TributeUrl=" + _tributeUrl + "&mode=link";
        objEmail.EmailSubject = txtUserName.Text + " wants you to read a guestbook on Your Tribute...";
        objEmail.TypeName = typeName;
        //objEmail.EmailBody = txtUserName.Text + " wants you to view a guestbook in the " + _tributeName + "Tribute.To view the " + PageName + ", follow the link below: " + "<br/> <br/>" + UrlToEmail + "<br/> <br/>" + "----" + "<br/>" + "Your Tribute Team";
        StringBuilder sbEmailbody = new StringBuilder();
        sbEmailbody.Append(txtUserName.Text + " wants you to view a guestbook in the " + _tributeName + " Tribute.");
        sbEmailbody.Append("<br/>");
        sbEmailbody.Append("<br/>");
        sbEmailbody.Append("To view the guestbook, follow the link below:");
        sbEmailbody.Append("<br/>");
        sbEmailbody.Append("<a href='http://" + Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath + "/GuestBook/GuestBook.aspx?" + strQueryString + "'>Click here to visit the link.</a>");
        sbEmailbody.Append("<br/>");
        sbEmailbody.Append("<br/>");
        sbEmailbody.Append("---");
        sbEmailbody.Append("<br/>");
        sbEmailbody.Append("Your Tribute Team");

        objEmail.EmailBody = sbEmailbody.ToString();
        stateManager.Add("objEmailLink", objEmail, StateManager.State.Session);
    }

    /// <summary>
    /// Method to load themes in the left panel
    /// </summary>
    private void LoadThemes()
    {
        Templates objTributeType = new Templates();
        objTributeType.TributeType = _tributeType; // "Wedding";

        int existingTheme = GetExistingTheme().TemplateID;
        //hdnSelectedTheme.Value = existingTheme.ToString();
        MiscellaneousController _controller = new MiscellaneousController();
        List<Templates> lstThemes = _controller.GetThemesList(objTributeType);
        StringBuilder sbChangeSiteTheme = new StringBuilder();
        foreach (Templates objThemes in lstThemes)
        {
            sbChangeSiteTheme.Append("<div class='yt-Form-Field yt-Form-Field-Radio' id='" + objThemes.ThemeCssClass + "'>"); // + objThemes.TemplateName.Remove(objThemes.TemplateName.IndexOf(" "), 1) + "'>");
            //sbChangeSiteTheme.Append("<asp:RadioButton ID=" + objThemes.TemplateID + " runat='server' GroupName='Theme' />" ; //<input name='rdoTheme' type='radio' id='rdo" + objThemes.TemplateID + "' value='" + objThemes.TemplateName.Remove(objThemes.TemplateName.IndexOf(" "), 1) + "' />");
            sbChangeSiteTheme.Append("<input name='rdoTheme' type='radio' runat='server' id='rdo" + objThemes.TemplateID + "' onclick='javascript:Themer(\"" + objThemes.ThemeValue + "\");GetSelectedTheme(" + objThemes.TemplateID + ",\"" + objThemes.ThemeValue +  "\");' value='" + objThemes.ThemeValue + "'"); //objThemes.TemplateName.Replace(" ", "") + "'"); //.Remove(objThemes.TemplateName.IndexOf(" "), 1) + "'");
            if (hdnSelectedTheme.Value != string.Empty)
            {
                if (int.Parse(hdnSelectedTheme.Value) == objThemes.TemplateID)
                    sbChangeSiteTheme.Append(" Checked='Checked' />");
                else
                    sbChangeSiteTheme.Append("  />");
            }
            else
            {
                if (existingTheme == objThemes.TemplateID)
                    sbChangeSiteTheme.Append(" Checked='Checked' />");
                else
                    sbChangeSiteTheme.Append("  />");
            }
            sbChangeSiteTheme.Append("<label for='rdo" + objThemes.TemplateID + "'>"); //rdo" + objThemes.TemplateName + "'>");
            sbChangeSiteTheme.Append(objThemes.TemplateName + " <span class='yt-ThemeColorPrimary'></span><span class='yt-ThemeColorSecondary'></span></label>");
            sbChangeSiteTheme.Append("</div>");
        }
        litThemes.Text = sbChangeSiteTheme.ToString();

        StateManager stateManager = StateManager.Instance;
        stateManager.Add("ThemeOnMaster", lstThemes, StateManager.State.Session);

    }

    /// <summary>
    /// Method to get the theme for tribute
    /// </summary>
    public Templates GetExistingTheme()
    {
        Tributes objTribute = new Tributes();
        MiscellaneousController _controller = new MiscellaneousController();
        objTribute.TributeId = _tributeId;
        return _controller.GetThemeForTribute(objTribute);
        //return _controller.GetThemeForTribute(objTribute).TemplateID;
    }

    /// <summary>
    /// Method to show error message
    /// </summary>
    /// <param name="strErrorMessage">Error message</param>
    /// <returns>Html for error message</returns>
    public string ShowErrorMessage(string strErrorMessage)
    {
        string headertext = " <h2>Oops - there is a problem.</h2><br/>";
        headertext += "<ul>";
        headertext += "<li>";
        headertext += strErrorMessage;
        headertext += "</li>";
        return headertext += "</ul>";
    }

    /// <summary>
    /// Method to get the code of Google Adsense.
    /// </summary>
    /// <param name="tributeType">Tribute type.</param>
    private void GoogleAdSense(string tributeType)
    {
        if (tributeType == "Anniversary")
        {
            adSenseCode = "5118276450";
            adSenseComment = "/* Anniversary, 160x600 Verticle */";
        }
        else if (tributeType == "Birthday")
        {
            adSenseCode = "0604124633";
            adSenseComment = "/* Birthday, 160x600 Verticle */";
        }
        else if (tributeType == "Graduation")
        {
            adSenseCode = "1565407601";
            adSenseComment = "/* Graduation, 160x600 Verticle */";
        }
        else if (tributeType == "Memorial")
        {
            adSenseCode = "9980482471";
            adSenseComment = "/* Memorial, 160x600 Verticle */";
        }
        else if (tributeType == "New Baby")
        {
            adSenseCode = "1694060658";
            adSenseComment = "/* New Baby, 160x600 Verticle */";
        }
        else if (tributeType == "Wedding")
        {
            adSenseCode = "1176450699";
            adSenseComment = "/* Wedding, 160x600 Verticle */";
        }
    }

    /// <summary>
    /// Method to send email to the receipents.
    /// </summary>
    public void SendEmail(List<string> lstEmailAdd)
    {
        StateManager stateManager = StateManager.Instance;
        EmailLink objEmail = (EmailLink)stateManager.Get("objEmailLink", StateManager.State.Session);

        EmailLink objEmailLink = new EmailLink();
        objEmailLink.FromEmailAddress = txtUserEmailAddress.Text;
        objEmailLink.FromUserName = txtUserName.Text;
        objEmailLink.EmailTo = lstEmailAdd;
        objEmailLink.UrlToEmail = objEmail.UrlToEmail;
        objEmailLink.TypeName = objEmail.TypeName;
        objEmailLink.EmailBody = objEmail.EmailBody;
        objEmailLink.EmailSubject = objEmail.EmailSubject;

        MessagingSystemController objMsg = new MessagingSystemController();
        objMsg.SendEmail(objEmailLink);
    }

    /// <summary>
    /// This Function will set the value of the control and error messages from the resource File
    /// </summary>
    private void SetControlsValue()
    {
        try
        {
            //Text for labels from the resource file
            lblFindTribute.Text = ResourceText.GetString("lblFindTribute_MP");                      // Find a Tribute
            lblSearchFor.InnerText = ResourceText.GetString("lblSearchFor_MP");                     // Search for:
            //txtSearchKeyword.Text = ResourceText.GetString("txtSearchKeyword_MP");                  // Enter the name of a Tribute
            lblSearch_All.InnerText = ResourceText.GetString("lblSearch_All_MP");                   // All Tributes
            lblSearch_Anniversary.InnerText = ResourceText.GetString("lblSearch_Anniversary_MP");   // Anniversary Tributes
            lblSearch_Birthday.InnerText = ResourceText.GetString("lblSearch_Birthday_MP");         // Birthday Tribute
            lblSearch_Graduation.InnerText = ResourceText.GetString("lblSearch_Graduation_MP");     // Graduation Tributes
            lblSearch_Memorial.InnerText = ResourceText.GetString("lblSearch_Memorial_MP");         // Memorial Tributes
            lblSearch_NewBaby.InnerText = ResourceText.GetString("lblSearch_NewBaby_MP");           // New Baby Tributes
            lblSearch_Wedding.InnerText = ResourceText.GetString("lblSearch_Wedding_MP");           // Wedding Tributes
            lnkAdvanceSearch.Text = ResourceText.GetString("lnkAdvanceSearch_MP");                  // Advanced Search
            lnkClose.InnerText = ResourceText.GetString("lnkClose_MP");                             // Close

            txtSearchKeyword.Attributes.Add("onclick", "this.select();");

            //for popup window
            hEmailPage.InnerText = ResourceText.GetString("hEmailPage_EU");
            pRequired.InnerHtml = "<strong>" + ResourceText.GetString("lblRequired_EU") + "<em class=\"required\">*</em></strong>";
            lblUserName.InnerHtml = "<em class=\"required\">* </em>" + ResourceText.GetString("lblUserName_EU");
            lblUserEmailAddress.InnerHtml = "<em class=\"required\">* </em>" + ResourceText.GetString("lblUserEmailAddress_EU");
            lblEmailAddress.InnerText = ResourceText.GetString("lblEmailAddress_EU");
            rfvUserName.ErrorMessage = ResourceText.GetString("errUserName_EU");
            rfvUserEmailAddress.ErrorMessage = ResourceText.GetString("errUserEmailAddress_EU");
            revFromEmailAddress.ErrorMessage = ResourceText.GetString("errUserEmailAddress_EU");
            cvCheckValidEmail.ErrorMessage = ResourceText.GetString("errUserEmailAddress_EU");
            lbtnSubmit.Text = ResourceText.GetString("btnSubmit_EU");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// This method will return the selected tribute type
    /// </summary>
    /// <returns>A string which contain the tribute type</returns>
    private string GetTributeType()
    {
        string tributeType = "";

        if (rdoSearch_All.Checked == true)
        {
            tributeType = lblSearch_All.InnerText;
        }
        else if (rdoSearch_Anniversary.Checked == true)
        {
            tributeType = lblSearch_Anniversary.InnerText;
        }
        else if (rdoSearch_Birthday.Checked == true)
        {
            tributeType = lblSearch_Birthday.InnerText;
        }
        else if (rdoSearch_Graduation.Checked == true)
        {
            tributeType = lblSearch_Graduation.InnerText;
        }
        else if (rdoSearch_Memorial.Checked == true)
        {
            tributeType = lblSearch_Memorial.InnerText;
        }
        else if (rdoSearch_NewBaby.Checked == true)
        {
            tributeType = lblSearch_NewBaby.InnerText;
        }
        else if (rdoSearch_Wedding.Checked == true)
        {
            tributeType = lblSearch_Wedding.InnerText;
        }

        return tributeType;
    }

    /// <summary>
    /// Method to check if tribute in user favorite list and set btn string.
    /// </summary>
    private void CheckForFavorite()
    {
        MiscellaneousController _favController = new MiscellaneousController();
        AddToFavorite objFavorite = new AddToFavorite();
        objFavorite.TributeId = _tributeId;
        objFavorite.UserId = _userId;
        isInFavorite = _favController.GetUserTributeFavorites(objFavorite);
        if (isInFavorite > 0)
        {
            lbtnAddToFavorite.Text = ResourceText.GetString("lbtnRemoveFromFavourate_GB_Master");
            toFav = ResourceText.GetString("txtRemoveFromFavorite_GB_Master");
            toFavText = ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments" ? ResourceText.GetString("txtRemoveFavorite_GB_Master1") : ResourceText.GetString("txtRemoveFavorite_GB_Master");
        }
        else
        {
            lbtnAddToFavorite.Text = ResourceText.GetString("lbtnAddFav_GB_Master");
            toFav = ResourceText.GetString("txtAddToFavorite_GB_Master");
            toFavText = ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments" ? ResourceText.GetString("txtAddFavorite_GB_Master1") : ResourceText.GetString("txtAddFavorite_GB_Master");
        }
    }

    /// <summary>
    /// Method to get Html for Affiliate links
    /// </summary>
    /// <param name="tributeType">Tribute Type</param>
    public void AffiliateLinks(string tributeType)
    {
        CommonUtilities objUtil = new CommonUtilities();
        divAddSense.InnerHtml = objUtil.AffiliateLinks(tributeType);
    }
    #endregion    
}
