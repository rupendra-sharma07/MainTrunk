
///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.TributeHomePage.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the tribute home page with links to tribute sub pages
///Audit Trail     : Date of Modification  Modified By         Description

#region NameSpaces
using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Tribute.Views;
using TributesPortal.Utilities;
using TributesPortal.Miscellaneous;
using Facebook.Web;
using TributesPortal.ResourceAccess;
#endregion NameSpaces

public partial class Tribute_TributeHomePage_ : PageBase, ITributeHomePage
{
    #region Variables
    private TributeHomePagePresenter _presenter;
    protected string _tributeName = string.Empty;
    private int _UserID = 0;
    protected int _packageId;
    private int _TribureId;
    public int pack_type = 0;
    protected string _tributeUrl;
    protected string _userName;
    private SessionValue objSessionValue = null;
    private string _firstName = "";
    private string _lastName = "";
    public string _emailId = "";
    private bool _isAutoRenew = false;
    protected string _themeName;
    protected int amount = 0;
    public string _tributeTypeName = string.Empty;
    public string divWecome = string.Empty;
    protected string _query_string = string.Empty; // to add to urls parameter TributeType...
    public string AppCurrentDomain = string.Empty;
    bool IsCustomHeaderOn = false;
    private string _postMessage;
    private string _messageWithoutHtml;
    protected string _typeDescription = string.Empty;
    private string _initialUrl = string.Empty;
    private string _redirectionUrl = string.Empty;
    bool IsVisitCountOn = false;
    private int typeCodeId = 3;
    private string _typeName = "GuestBook";
    protected string URL;
    public string gbLink = string.Empty;
    public string giftLink = string.Empty;
    public string albumLink = string.Empty;
    public string videoLink = string.Empty;
    private string mainDomainUrl = "www." + WebConfig.TopLevelDomain;

    private static int _maxStoryActivity = int.Parse(WebConfig.MaxStoryLimit);
    private static int _maxNotesActivity = int.Parse(WebConfig.MaxNotesLimit);
    private static int _maxGuestbookActivity = int.Parse(WebConfig.MaxGuestbookLimit);
    private static int _maxGiftsActivity = int.Parse(WebConfig.MaxGiftsLimit);
    private static int _maxEventsActivity = int.Parse(WebConfig.MaxEventsLimit);
    private static int _maxPhotosActivity = int.Parse(WebConfig.MaxPhotosLimit);
    private static int _maxVideosActivity = int.Parse(WebConfig.MaxVideosLimit);
    private static int _maxVideoTributeActivity = int.Parse(WebConfig.MaxVideoTributeLimit);
    private static int _maxCommentsActivity = int.Parse(WebConfig.MaxCommentsLimit);

    private static int _maxNoteCommentsActivity = int.Parse(WebConfig.MaxNoteCommentsLimit);
    private static int _maxPhotoCommentsActivity = int.Parse(WebConfig.MaxPhotoCommentsLimit);
    private static int _maxVideoCommentsActivity = int.Parse(WebConfig.MaxVideoCommentsLimit);
    private static int _maxVideoTributeCommentsActivity = int.Parse(WebConfig.MaxVideoTributeCommentsLimit);

    private int _storyActivity = 0;
    private int _notesActivity = 0;
    private int _guestbookActivity = 0;
    private int _giftsActivity = 0;
    private int _eventsActivity = 0;
    private int _photosActivity = 0;
    private int _videosActivity = 0;
    private int _videoTributeActivity = 0;
    private int _commentsActivity = 0;

    private int _noteCommentsActivity = 0;
    private int _photoCommentsActivity = 0;
    private int _videoCommentsActivity = 0;
    private int _videoTributeCommentsActivity = 0;

    #endregion Variables



    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(Tribute_TributeHomePage_));
        //code for YT MObile redirections
        string redirctMobileUrl = string.Empty;
        string mTributeType = null;
        if (!IsPostBack)
        {
            DeviceManager deviceManager = new DeviceManager
                                              {
                                                  UserAgent = Request.UserAgent,
                                                  IsMobileBrowser = Request.Browser.IsMobileDevice
                                              };
            // Added by Varun Goel on 25 Jan 2013 for NoRedirection functionality
            TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;
            objSessionValue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionValue == null || objSessionValue.NoRedirection == null || objSessionValue.NoRedirection == false)
            {
                if (deviceManager.IsMobileDevice())
                {
                    Tributes oTribute = new Tributes();
                    if ((Request.QueryString["TributeUrl"] != null) && (Request.QueryString["TributeType"] != null))
                    {
                        oTribute.TributeUrl = Request.QueryString["TributeUrl"];
                        oTribute.TypeDescription = mTributeType = Request.QueryString["TributeType"];

                        oTribute = _presenter.GetTributeUrlOnOldTributeUrl(oTribute,
                                                                           WebConfig.ApplicationType.ToString());

                        bool isMobileViewOn = _presenter.GetIsMobileViewOn(oTribute);
                        bool IsMobileRedirectOn = false;
                        bool.TryParse(WebConfig.IsMobileRedirectOn, out IsMobileRedirectOn);
                        bool IsInIframe = false;
                        if (HttpContext.Current.Session["isInIframe"] != null)
                        {
                            bool.TryParse(HttpContext.Current.Session["isInIframe"].ToString(), out IsInIframe);
                        }

                        if (isMobileViewOn && IsMobileRedirectOn && (mTributeType.ToLower().Equals("memorial")) && (!IsInIframe))
                        {
                            redirctMobileUrl = string.Format("{0}{1}{2}{3}{4}{5}", "https://www.", WebConfig.TopLevelDomain, "/mobile/index.html?tributeurl=", oTribute.TributeUrl, "&tributetype=", Request.QueryString["TributeType"], "&page=home");
                        }
                    }
                    else
                    {
                        // Redirection URL
                        redirctMobileUrl = string.Format("{0}{1}{2}", "https://www.", WebConfig.TopLevelDomain, "/mobile/Search.html");
                        Response.Redirect(redirctMobileUrl, false);
                    }
                }
            }
        }
        if (string.IsNullOrEmpty(redirctMobileUrl))
        {

            Tributes objTrb = new Tributes();
            try
            {
                //LHK: 3:59 PM 9/5/2011 - Wordpress topURL
                if (Request.QueryString["topurl"] != null)
                {
                    Response.Cookies["topurl"].Value = Request.QueryString["topurl"].ToString();
                    Response.Cookies["topurl"].Domain = "." + WebConfig.TopLevelDomain;
                    Response.Cookies["topurl"].Expires = DateTime.Now.AddHours(4);
                }

                SubDomainCheck();

                //LHK:EmptyDivAboveMainPanel
                StateManager stateTribute = StateManager.Instance;
                SessionValue objSessvalue =
                    (SessionValue)stateTribute.Get("objSessionvalue", StateManager.State.Session);

                if ((Request.QueryString["TributeUrl"] != null))
                {
                    _tributeUrl = Request.QueryString["TributeUrl"].ToString();
                    GetCustomHeaderVisible(_tributeUrl, WebConfig.ApplicationType.ToString());
                    GetVisitorCountVisible(_tributeUrl, WebConfig.ApplicationType.ToString());
                }
                if ((Request.QueryString["Id"] != null))
                {
                    int _trbId;
                    int.TryParse(Request.QueryString["Id"].ToString(), out _trbId);
                    GetCustomHeaderVisible(_trbId);
                    GetVisitorCountVisible(_trbId);
                }
                if (!(objSessvalue != null))
                {
                    if (!IsCustomHeaderOn)
                    {
                        EmptyDivAboveMainPanel.Visible = true;
                    }
                }

                // to Hide Visitor count :Ud
                if (!IsVisitCountOn)
                {
                    Visit.Visible = false;
                }
                //LHK:EmptyDivAboveMainPanel

                //StateManager stateManager = StateManager.Instance;
                SessionValue objSessionvalue =
                    (SessionValue)stateTribute.Get("objSessionvalue", StateManager.State.Session);

                // Set the value of the seraching control from the resource access class
                if (Request.QueryString["TributeType"] != null)
                {
                    ytHeader.TributeType = Request.QueryString["TributeType"].ToString();
                }

                _presenter.GetTributeUrlOnTributeId(_TribureId);

                if (Request.Browser.Version.Equals("6.0"))
                {
                    divWecome = "yt-Welcome1";
                }
                else
                {
                    divWecome = "yt-Welcome";
                }
                if (Request.QueryString["Id"] != null)
                {
                    int.TryParse(Request.QueryString["Id"].ToString(), out _TribureId);
                    _tributeUrl = _presenter.GetTributeUrlOnTributeId(_TribureId);
                }

                spanErrorMessage.InnerHtml = string.Empty;

                if (!this.IsPostBack)
                {
                    #region MyRegion OLD !this.IsPostBack

                    StateManager stateManagerP = StateManager.Instance;
                    string PageName = "TributeHomePage";
                    stateManagerP.Add(PortalEnums.SessionValueEnum.SearchPageName.ToString(), PageName,
                                      StateManager.State.Session);

                    Announcement.Visible = false;
                    SaveMsg.Visible = false;

                    if (Request.QueryString["Tributeid"] != null)
                    {
                        StateManager stateTribureqs = StateManager.Instance;
                        Tributes objTributeqs = new Tributes();
                        objTributeqs.TributeId = int.Parse(Request.QueryString["Tributeid"].ToString());
                        stateTribureqs.Add("TributeSession", objTributeqs, StateManager.State.Session);
                    }
                    //if Tribute Type and Tribute Url are in querystring

                    if ((Request.QueryString["TributeUrl"] != null) && (Request.QueryString["TributeType"] != null))
                    {
                        _presenter.GetTributeSessionForUrlAndType(Request.QueryString["TributeUrl"].ToString(),
                                                                  Request.QueryString["TributeType"].ToString(),
                                                                  WebConfig.ApplicationType.ToString());
                    }
                    else if (Request.QueryString["TributeUrl"] != null)
                    {
                        _presenter.GetTributeSessionForUrlAndType(Request.QueryString["TributeUrl"].ToString(), null,
                                                                  WebConfig.ApplicationType.ToString());
                    }
                    else if (Request.QueryString["Id"] != null)
                    {
                        int.TryParse(Request.QueryString["Id"].ToString(), out _TribureId);
                        _presenter.GetTributeSessionForTributeId(_TribureId);
                    }

                    StateManager stateTribure = StateManager.Instance;
                    Tributes objTribute = (Tributes)stateTribure.Get("TributeSession", StateManager.State.Session);
                    if (!Equals(objTribute, null))
                    {
                        _TribureId = objTribute.TributeId;
                        _tributeUrl = objTribute.TributeUrl;
                        _typeDescription = objTribute.TypeDescription;
                        Session["TributeURL"] = _tributeUrl;
                        Session["isActive"] = objTribute.IsActive;
                        SaveValues(_TribureId);

                        HttpSessionState st = HttpContext.Current.Session;
                        if (Session["Visit_"] == null)
                        {
                            Session["Visit_"] = st.SessionID;
                            this._presenter.AddTributeCount();
                        }
                        else
                        {
                            if (Session["Visit_"].ToString() == st.SessionID)
                            {
                                Session["Visit_"] = st.SessionID;
                                if (Session["Visit"] == null)
                                {
                                    Session["Visit"] = _TribureId;
                                    this._presenter.AddTributeCount();
                                }
                                else
                                {
                                    if (int.Parse(Session["Visit"].ToString()) == _TribureId)
                                    {
                                        Session["Visit"] = _TribureId;
                                    }
                                    else
                                    {
                                        this._presenter.AddTributeCount();
                                        Session["Visit"] = _TribureId;
                                    }
                                }

                            }
                            else
                            {
                                Session["Visit_"] = st.SessionID;
                                this._presenter.AddTributeCount();
                            }
                        }

                        StateManager stateTribure_ = StateManager.Instance;
                        Tributes objTribute_ =
                            (Tributes)stateTribure_.Get("TributeSession", StateManager.State.Session);
                        if (!Equals(objTribute_.TributeName, null))
                        {
                            if (objTribute_.TributeId > 0)
                            {
                                _TribureId = objTribute_.TributeId;
                                _tributeUrl = objTribute_.TributeUrl;
                                //_query_string = _tributeUrl + "/";
                                _tributeName = objTribute_.TributeName;
                                this.lTributeName1.Text = _tributeName;
                                this.lTributeName2.Text = _tributeName;
                                _tributeType = objTribute_.TypeDescription;
                                if (!(string.IsNullOrEmpty(_tributeType)))
                                {
                                    if (!(_tributeType.ToLower().Equals("memorial")))
                                    {
                                        obituaryBlock.Visible = false;
                                    }
                                }
                                _tributeTypeName = objTribute_.TypeDescription.ToLower().Replace("new baby",
                                                                                                 "newbaby");
                                string folderName = this._presenter.GetExistingFolderName(objTribute_.TributeId);
                                string appPath = string.Empty;
                                if (WebConfig.ApplicationMode.ToLower().Equals("local"))
                                {
                                    appPath = WebConfig.AppBaseDomain;
                                }
                                else
                                {
                                    appPath = string.Format("{0}{1}{2}", "http://www.", WebConfig.TopLevelDomain,
                                                            "/");
                                }
                                idSheet.Attributes.Add("href",
                                                       appPath + "assets/themes/" + folderName + "/theme.css");
                                //to set the selected theme
                                Session["TributeTypeName"] = _tributeTypeName;

                                //AG:18/03/10: Set package id value
                                MiscellaneousController objMisc = new MiscellaneousController();
                                TributePackage objpackage = new TributePackage();
                                objpackage.UserTributeId = _TribureId;
                                object[] param = { objpackage };
                                objMisc.TriputePackageInfo(param);
                                if (objpackage.CustomError == null)
                                {
                                    _packageId = objpackage.PackageId;
                                    Session["UserPackageID"] = _packageId;
                                }
                            }


                            this._presenter.GetTributeCount();
                            this._presenter.GetTributeAdminis();
                            this._presenter.GetMaymentModes();
                            this._presenter.GetCCCountryList();
                            this._presenter.GetCCStateList();
                            this._presenter.GetStoryDetail();
                            //to get the details for the tributes' donation details
                            this._presenter.GetDonationDetails();

                            //Start - Modification on 7-Dec-09 for the enhancement 5 of the Phase 1
                            //SetTributeCreatedByOrProvidedBy function sets the TributeCreatedByOrProvidedBy value as per the user type
                            this._presenter.SetTributeCreatedByOrProvidedBy();
                            lblTributeCreatedByOrProvidedBy.InnerText = _TributeCreatedByOrProvidedBy;
                            //End

                            //Start - Modification on 17-Dec-09 for the enhancement 6 of the Phase 1
                            this._presenter.SetCompanyLogo();
                            SetControlsValue();
                            //End

                            //check the creation date of the tribute.
                            // if the date is greater than the lanch date then donot show message box, else show the message modal
                            //show this message only till 6 months after the launch of the site
                            if ((objTribute.CreatedDate < Convert.ToDateTime(WebConfig.Launch_Day.ToString())) &&
                                (DateTime.Today <= Convert.ToDateTime(WebConfig.Launch_Day.ToString()).AddDays(180)))
                            {
                                CommonUtilities utility = new CommonUtilities();
                                //if (_UserID > 0 && _TribureId > 0)
                                //{
                                if (!utility.ReadCookie(_UserID))
                                {
                                    body.Attributes.Add("onload", "setIsInTopurl();");
                                    utility.CreateCookie(_UserID);
                                }
                            }
                            //LHK: GuestBook entry
                            StringBuilder html = new StringBuilder();
                            txtMessage.Attributes.Add("onkeyup", "CheckGuestBookCommentLength();");

                            if (_UserID > 0)
                            {
                                btnPost.Attributes.Add("onClick", "return validateInput(); return false;");

                                //LHK: for login| Signup button visibility
                                divLogin.Attributes.Add("class", "hideBlock");

                                divHomeAuthUser.Style.Add(HtmlTextWriterStyle.Display, "inline");
                                divUnAuthUser.Style.Add(HtmlTextWriterStyle.Display, "none");
                                StateManager objStateManager = StateManager.Instance;
                                objSessionValue =
                                    (SessionValue)
                                    objStateManager.Get("objSessionvalue", StateManager.State.Session);
                                if (!Equals(objSessionValue, null))
                                {
                                    _userName = objSessionValue.FirstName == string.Empty
                                                    ? objSessionValue.UserName
                                                    : (objSessionValue.FirstName + " " + objSessionValue.LastName);
                                }

                                FacebookWebContext fbwebcon = new FacebookWebContext();

                                if (fbwebcon.Session != null)
                                {
                                    imgAppLogo.Src = ResolveUrl("~/assets/images/icon_Facebook.gif");
                                    lblUserName.InnerHtml = "Logged in as " + _userName;
                                }
                                else
                                {
                                    imgAppLogo.Src = ResolveUrl("~/assets/images/favicon.ico");
                                    lblUserName.InnerHtml = "Logged in as " + _userName;
                                }
                            }
                            else
                            {
                                btnPost.Attributes.Add("onClick", "return setSessionMsg(); return false;");
                                divUnAuthUser.Style.Add(HtmlTextWriterStyle.Display, "inline");
                                divHomeAuthUser.Style.Add(HtmlTextWriterStyle.Display, "none");
                            }
                            //GuestBook till here
                            UserIsAdmin();
                            SetLiteralHtml_();
                            SetUrltoLinkButtons();

                            if (_tributeType == "Anniversary")
                                _themeName = "AnniversaryDefault";
                            else if (_tributeType == "Birthday")
                                _themeName = "BirthdayDefault";
                            else if (_tributeType == "Graduation")
                                _themeName = "GraduationDefault";
                            else if (_tributeType == "Memorial")
                                _themeName = "MemorialDefault";
                            else if (_tributeType == "New Baby")
                                _themeName = "BabyDefault";
                            else if (_tributeType == "Wedding")
                                _themeName = "WeddingDefault";
                            ytHeader.TributeType = _tributeType;

                            Session["TributeType"] = _typeDescription;
                        }
                        else
                        {
                            Response.Redirect("~/Errors/Error404.aspx", false);
                        }
                    }

                    #endregion

                    lblPlusone.InnerHtml = "<a class='addthis_button_google_plusone' g:plusone:size='medium'></a>";

                    #region LHK: for GuestBook entry

                    if (_UserID == 0)
                    {
                        btnPost.Attributes.Add("onClick", "return setSessionMsg(); return false;");
                    }
                    else
                    {
                        btnPost.Attributes.Add("onClick", "return validateInput(); return false;");
                    }

                    #endregion

                }


                if (objSessionvalue != null)
                {
                    _UserID = objSessionvalue.UserId;
                    _firstName = objSessionvalue.FirstName;
                    _lastName = objSessionvalue.LastName;
                    _emailId = objSessionvalue.UserEmail;
                }
                else
                {
                    _UserID = 0;
                }

                //Start - Modification on 9-Dec-09 for the enhancement 3 of the Phase 1
                if (_tributeName != null)
                    Page.Title = _tributeName + " | " + _tributeType + " " +
                                 ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString();

                if (!this.IsPostBack)
                {

                    #region !post Back code

                    string nonLoggedIn = Request.QueryString["GuestBook_without_login"];
                    if (Session["CommentsSession"] != null &&
                        !string.IsNullOrEmpty(Session["CommentsSession"].ToString()))
                    {

                        // code here for save
                        ArrayList _arrNew = (ArrayList)Session["CommentsSession"];
                        if (_arrNew != null && _arrNew.Count == 2)
                        {
                            txtMessage.Text = _arrNew[1].ToString();
                            txtUserName.Text = _arrNew[0].ToString();
                        }

                        if ((nonLoggedIn == "true") || (_UserID > 0))
                        {
                            if (!txtMessage.Text.ToLower().Trim().Equals("message") &&
                                !txtUserName.Text.ToLower().Equals("name") && !txtMessage.Text.Trim().Equals("") &&
                                !txtUserName.Text.Trim().Equals("")) //
                            {
                                BtnClick_deligate _objBtnClickDeligate = new BtnClick_deligate(btnPost_Click);
                                object o = new object();
                                EventArgs obje = new EventArgs();
                                _objBtnClickDeligate(o, obje);
                            }
                        }

                        Session.Remove("CommentsSession");
                        txtMessage.Text = "Message";
                        txtUserName.Text = "Name";
                    }

                    fbDesc.Content = TributeMessage.ToString();
                    fbThumb.Content = TributeImage.ToString();

                    PageDesc.Content = TributeMessage.ToString();
                    PageThumb.Href = TributeImage.ToString();

                    #endregion

                    #region GetUpgradedUrl

                    //GetUpgradedUrl
                    if ((Request.QueryString["TributeUrl"] != null))
                    {

                        objTrb.TributeUrl = Request.QueryString["TributeUrl"].ToString();
                        if (Request.QueryString["TributeType"] != null)
                        {
                            objTrb.TypeDescription = Request.QueryString["TributeType"].ToString();
                        }
                        objTrb = _presenter.GetTributeUrlOnOldTributeUrl(objTrb,
                                                                         WebConfig.ApplicationType.ToString());

                        if (objTrb.TributeUrl != null)
                        {
                            _initialUrl = objTrb.TributeUrl.ToString();
                            _tributeType = objTrb.TypeDescription.ToString();
                            if (Request.QueryString["TributeType"] != null)
                            {
                                _tributeType = Request.QueryString["TributeType"].ToString();
                            }
                            if (!(string.IsNullOrEmpty(_initialUrl)) && (!(_tributeUrl.Equals(_initialUrl))))
                            {
                                if (WebConfig.ApplicationMode.Equals("local"))
                                {
                                    Response.Redirect(WebConfig.AppBaseDomain + _initialUrl + "/", false);
                                }
                                else
                                {
                                    Response.Redirect(
                                        "http://" + _tributeType.ToLower().Replace("new baby", "newbaby") + "." +
                                        WebConfig.TopLevelDomain + "/" + _initialUrl + "/", false);
                                }
                            }
                        }
                    }

                    #endregion

                }
            }

            catch (Exception ex)
            {
                Response.Redirect("../Errors/Error404.aspx");
            }

        }
        else
        {
            Response.Redirect(redirctMobileUrl, false);
        }
    }


    private void SubDomainCheck()
    {
        //LHK: top level domain check LH # 277.	Subdomain Issue 
        if (Request.Url.ToString().ToLower().Contains(mainDomainUrl.ToLower()))
        {
            throw new NotImplementedException();
        }
    }

    private void SetUrltoLinkButtons()
    {
        //Added to convert link button rediredt through javascript to direct links.
        StateManager stateTribure_ = StateManager.Instance;
        Tributes objTribute_ = (Tributes)stateTribure_.Get("TributeSession", StateManager.State.Session);
        if (objTribute_.TypeDescription != null)
        {
            gbLink = "http://" + objTribute_.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTribute_.TributeUrl.ToString() + "/guestbook.aspx";
            giftLink = "http://" + objTribute_.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTribute_.TributeUrl.ToString() + "/gift.aspx";
            albumLink = "http://" + objTribute_.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTribute_.TributeUrl.ToString() + "/photos.aspx";
            videoLink = "http://" + objTribute_.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTribute_.TributeUrl.ToString() + "/videos.aspx";
        }

    }

    private void SetControlsValue()
    {

        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            lblGuestBook.Text = "Leave a message";
            lblGbHeader.Text = "To leave a message, enter your message then click \"Post message\".";
            linkLongCondolences.Text = "Click here to view all messages in the Guestbook";
            lblMessageInvite.Text = "Invite friends and family to share their memories and contribute content to this website.";
        }
        else
        {
            if (_tributeType.ToLower().Equals("memorial"))
            {
                lblGuestBook.Text = "Leave condolences";
                lblGbHeader.Text = "To leave a condolence, enter your message then click \"Post message\".";
                linkLongCondolences.Text = "Click here to view all condolences in the Guestbook";
                lblMessageInvite.Text = "Invite friends and family to share their memories and contribute content to this memorial.";
            }
            else
            {
                lblGuestBook.Text = "Leave a message";
                lblGbHeader.Text = "To leave a message, enter your message then click \"Post message\".";
                linkLongCondolences.Text = "Click here to view all messages in the Guestbook";
                lblMessageInvite.Text = "Invite friends and family to share their memories and contribute content to this website.";
            }
        }
    }

    #region Events

    protected void liCondolences_Click(object sender, EventArgs e)
    {
        StateManager stateTribure_ = StateManager.Instance;
        Tributes objTribute_ = (Tributes)stateTribure_.Get("TributeSession", StateManager.State.Session);
        if (objTribute_.TypeDescription != null)
            Response.Redirect("http://" + objTribute_.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTribute_.TributeUrl.ToString() + "/guestbook.aspx");
    }

    private void GetVisitorCountVisible(string _tributeUrl, string ApplicationType)
    {
        IsVisitCountOn = true;
        TributeResource objTrbResource = new TributeResource();
        IsVisitCountOn = objTrbResource.GetVisitorCountVisible(_tributeUrl, ApplicationType);

    }
    private void GetVisitorCountVisible(int _trbId)
    {
        IsVisitCountOn = true;
        TributeResource objTrbResource = new TributeResource();
        IsVisitCountOn = objTrbResource.GetVisitorCountVisible(_trbId);
    }


    private void GetCustomHeaderVisible(string _tributeUrl, string ApplicationType)
    {
        IsCustomHeaderOn = false;
        VideoResource objVideoResource = new VideoResource();
        IsCustomHeaderOn = objVideoResource.GetCustomHeaderVisible(_tributeUrl, ApplicationType);
    }
    private void GetCustomHeaderVisible(int _trbId)
    {
        IsCustomHeaderOn = false;
        VideoResource objVideoResource = new VideoResource();
        IsCustomHeaderOn = objVideoResource.GetCustomHeaderVisible(_trbId);
    }

    private void SetExpiry()
    {
        StateManager stateTribure_ = StateManager.Instance;
        Tributes objTribute_ = (Tributes)stateTribure_.Get("TributeSession", StateManager.State.Session);
        if (!Equals(objTribute_, null))
        {

            string themeValue = this._presenter.GetExistingTheme(objTribute_.TributeId);
            //body.Attributes.Add("onload", "Themer_('" + themeValue + "');");

        }
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        editMsg.Visible = false;
        SaveMsg.Visible = true;
        _presenter.GetWelcomeMessage();

        if (hdnFullmessage.Value != null)
        {
            if (hdnFullmessage.Value.ToString().Length > 0)
            {
                string msg = GetMessagetoSave(hdnFullmessage.Value.ToString());
                while (msg.Contains("``"))
                    msg = msg.Replace("``", "`");
                msg = msg.Replace("`", "\r\n");
                txtarWelcome.Text = msg;
            }
        }
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        editMsg.Visible = true;
        string WholeString = string.Empty;
        SaveMsg.Visible = false;
        string st = string.Empty;
        string actualstr = string.Empty;
        if (txtarWelcome.Text.Trim().Length > 0)
        {
            actualstr = GetMessagetoSave(txtarWelcome.Text.ToString().Trim());
            hdnFullmessage.Value = actualstr;
        }
        else
        {
            hdnFullmessage.Value = WholeString = ResourceText.GetString(this._presenter.View.TypeDescription).Replace("~", _tributeName);//
        }
        _presenter.UpdateTributeMessage(GetMessagetoSave(actualstr));
        actualstr = GetMessagetoShow(actualstr);
        SetMessageText(actualstr);
    }

    private string GetMessagetoShow(string actualstr)
    {
        string str = string.Empty;
        if (actualstr.Length > 0)
        {
            actualstr = actualstr.Replace("\r\n", "`");
            while (actualstr.Contains("\n\n"))
                actualstr = actualstr.Replace("\n\n", "\n");
            actualstr = actualstr.Replace("\n", "`");
            while (actualstr.Contains("``"))
                actualstr = actualstr.Replace("``", "`");
            while (actualstr.Contains("<br/><br/>"))
                actualstr = actualstr.Replace("<br/><br/>", "<br/>");
            actualstr = actualstr.Replace("<br/>", "`");

            string[] Entercount = actualstr.Split('`');
            if (Entercount.Length > 6)
            {
                StringBuilder secstr = new StringBuilder();
                for (int i = 0; i < 6; i++)
                {
                    secstr.Append(Entercount[i].ToString() + "<br/>");
                }
                str = secstr.ToString();
            }
            else
            {
                str = actualstr.Replace("\n", "<br/>");
                str = str.Replace("`", "<br/>");
            }
        }
        else
        {
            hdnFullmessage.Value = str = ResourceText.GetString(this._presenter.View.TypeDescription).Replace("~", _tributeName);//
        }
        return str;
    }

    private string GetMessagetoSave(string str)
    {
        if (str.Length >= 0)
        {
            while (str.Contains("<br/><br/>"))
                str = str.Replace("<br/><br/>", "<br/>");
            str = str.Replace("<br/>", "`");
            while (str.Contains("\r\n\r\n"))
                str = str.Replace("\r\n\r\n", "\r\n");
            str = str.Replace("\r\n", "`");
            while (str.Contains("\n\n"))
                str = str.Replace("\n\n", "\n");
            str = str.Replace("\n", "`");
            while (str.Contains("``"))
                str = str.Replace("``", "`");
            str = str.Replace("`", "\r\n");
        }
        else
        {
            str = ResourceText.GetString(this._presenter.View.TypeDescription).Replace("~", _tributeName);//
        }
        return str;
    }

    protected void lbtnReadNode_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/" + Session["TributeURL"] + "/note.aspx?noteId=" + _noteid);
    }

    protected void popuplbtnSendemail_Click(object sender, EventArgs e)
    {

    }

    protected void lnkbCancel_Click(object sender, EventArgs e)
    {
        editMsg.Visible = true;
        SaveMsg.Visible = false;
    }

    protected void ddlCCCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.GetCCStateList();
    }

    protected void lbtnSendMessage_Click(object sender, EventArgs e)
    {

    }


    protected void lbtnSubmit_Click(object sender, EventArgs e)
    {

    }
    #endregion Events

    #region Methods

    /// <summary>
    /// Method to set the url to be mailed to user.
    /// </summary>
    private void SetValueForEmailInSession(string typeName)
    {
        try
        {
            StateManager stateManager = StateManager.Instance;
            EmailLink objEmail = new EmailLink();
            objEmail.EmailBody = "<font style='font-size: 12px; font-family:Lucida Sans;'>";
            string PageName = "TributeHome";
            typeName = "TributeHome";
            string ApplicationPath = "<a href='" + URL + "'>" + URL + "</a>";
            string UrlToEmail = ApplicationPath;
            objEmail.FromEmailAddress = _emailId;



            objEmail.TypeName = typeName;

            StringBuilder sbEmailbody = new StringBuilder();


            sbEmailbody.Append("<br/>");
            sbEmailbody.Append("<br/>");

            sbEmailbody.Append(ResourceText.GetString("txtToView_SM_Master") + " " + PageName + ResourceText.GetString("txtFollowLink_SM_Master"));

            sbEmailbody.Append("<br/>");
            sbEmailbody.Append(UrlToEmail);
            sbEmailbody.Append("<br/>");
            sbEmailbody.Append("<br/>");
            sbEmailbody.Append("---");
            sbEmailbody.Append("<br/>");
            sbEmailbody.Append(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments" ? ResourceText.GetString("txtFrom_SM_Master1") : ResourceText.GetString("txtFrom_SM_Master"));

            objEmail.EmailBody = objEmail.EmailBody + sbEmailbody.ToString();
            stateManager.Add(PortalEnums.SessionValueEnum.ShareTributeEmail.ToString(), objEmail, StateManager.State.Session);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void UserIsAdmin()
    {
        UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
        if (objSessionvalue != null)
        {
            objUserInfo.UserId = objSessionvalue.UserId;
        }
        StateManager stateTribure1 = StateManager.Instance;
        Tributes objTribute = (Tributes)stateTribure1.Get("TributeSession", StateManager.State.Session);
        if (!Equals(objTribute, null))
        {
            objUserInfo.TributeId = objTribute.TributeId;
        }
        if (objUserInfo.UserId != 0)
        {
            if (IsUserAdmin(objUserInfo))
            {
                objUserInfo.IsAdmin = true;
                lbtnEdit.Visible = true;
                addstory.Visible = true;
                this._presenter.IsStoryAdded();
            }
            else
            {
                lbtnEdit.Visible = false;
                addstory.Visible = false;
            }
        }
        else
        {
            objUserInfo.IsAdmin = false;
            lbtnEdit.Visible = false;
            addstory.Visible = false;
        }



    }
    private void SaveValues(int TributeId)
    {
        Tributes objTribute = new Tributes();
        objTribute.TributeId = TributeId;
        _presenter.GetTributeSession(objTribute);
        _tributeName = objTribute.TributeName;
        _tributeType = objTribute.TypeDescription;
        if (objTribute.TypeDescription != null)
        {
            _tributeTypeName = objTribute.TypeDescription.ToLower().Replace("new baby", "newbaby");
        }

        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add("TributeSession", objTribute, TributesPortal.Utilities.StateManager.State.Session);

    }

    //LHK: Removed commented code on 11:30 AM 9/23/2011
    private void SetDefault()
    {


    }

    private void SetMessageText(string _WholeString)
    {
        if (_WholeString.Length <= 256)
        {
            lblWelcomeMsg.InnerHtml = _WholeString;
        }
        else
        {
            lblWelcomeMsg.InnerHtml = _WholeString.Substring(0, 256).ToString() + "....";
        }

    }
    #endregion Methods

    #region ITributeHomePage Members

    [CreateNew]
    public TributeHomePagePresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    public System.Collections.Generic.IList<TributesPortal.BusinessEntities.UserInfo> OtherAdmins
    {
        set
        {
            if (value.Count > 0)
            {
                StringBuilder adminHtml = new StringBuilder();
                for (int i = 0; i < value.Count; i++)
                {
                    string param2 = value[i].UserID.ToString();
                    string param1 = "";
                    adminHtml.Append("<a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 + ");' >" + value[i].FirstName.ToString() + "</a>" + " ");
                }
                rtrOtherAdminist.InnerHtml = adminHtml.ToString();
                lblOtherAdminist.Visible = true;
            }
            else
            {
                lblOtherAdminist.Visible = false;
            }
        }
    }

    public int TributeId
    {
        get
        {
            StateManager stateTribure1 = StateManager.Instance;
            Tributes objTribute = (Tributes)stateTribure1.Get("TributeSession", StateManager.State.Session);
            if (!Equals(objTribute, null))
            {
                _TribureId = objTribute.TributeId;
            }
            return _TribureId;
        }
    }

    public string TributeName
    {
        set
        {
            _tributeName = value.ToString();
        }
    }

    public string TributeMessage
    {
        get
        {
            return lblWelcomeMsg.InnerText;
        }
        set
        {
            if (value != null)
            {
                if (value.Length > 0)
                {
                    string actualstr = GetMessagetoShow(value);
                    SetMessageText(actualstr);
                    hdnFullmessage.Value = value;
                }

            }
        }
    }

    DateTime? ITributeHomePage.Date1
    {
        set
        {
            Nullable<DateTime> dt1 = value;
            if (dt1 != null)
            {
                Session["DOB"] = dt1.Value.ToString("MMMM dd, yyyy");

                if (_presenter.View.TributeType.Equals("Memorial"))
                {

                    txtDate1.InnerText = dt1.Value.ToString("MMMM dd, yyyy");
                    lblDate1.InnerText = "Date of Birth:";
                }
                else if (_presenter.View.TributeType.Equals("New Baby"))
                {
                    txtDate1.InnerText = dt1.Value.ToString("MMMM dd, yyyy");
                    lblDate1.InnerText = "Date of Birth:";
                }
                else if (_presenter.View.TributeType.Equals("Birthday"))
                {

                    lblDate1.InnerText = "Date of Birth:";
                    if (dt1.Value.Year == 1780)
                        txtDate1.InnerText = dt1.Value.ToString("MMMM dd");
                    else
                        txtDate1.InnerText = dt1.Value.ToString("MMMM dd, yyyy");


                }
                else if (_presenter.View.TributeType.Equals("Graduation"))
                {

                    txtDate1.InnerText = dt1.Value.ToString("MMMM dd, yyyy");
                    lblDate1.InnerText = "Date of Graduation:";
                }
                else if (_presenter.View.TributeType.Equals("Wedding"))
                {

                    txtDate1.InnerText = dt1.Value.ToString("MMMM dd, yyyy");
                    lblDate1.InnerText = "Date of Wedding:";
                }
                else if (_presenter.View.TributeType.Equals("Anniversary"))
                {

                    txtDate1.InnerText = dt1.Value.ToString("MMMM dd, yyyy");
                    lblDate1.InnerText = "Date of Anniversary:";
                }
            }
        }
    }

    DateTime? ITributeHomePage.Date2
    {
        set
        {
            Nullable<DateTime> dt2 = value;
            if (dt2 != null)
            {
                Session["DOD"] = dt2.Value.ToString("MMMM dd, yyyy");
                if (_presenter.View.TributeType.Equals("Memorial"))
                {
                    lblDate2.InnerText = "Date of Death:";
                    txtDate2.InnerText = dt2.Value.ToString("MMMM dd, yyyy");
                    string[] years = txtDate1.InnerText.Split(',');
                    if (years.Length >= 2)
                    {
                        int Age = dt2.Value.Year - int.Parse(years[1].ToString());

                        if (dt2.HasValue)
                        {
                            Nullable<DateTime> dob = DateTime.Parse(txtDate1.InnerText);
                            //if not had a birthday this year, then subtract the year
                            if (dob.HasValue && (dt2.Value.DayOfYear < dob.Value.DayOfYear))
                                Age--;
                        }

                        string Age1 = Age.ToString();
                        lblAge1.InnerText = "Age:";

                        if (Age1 != "0")
                            txtAge1.InnerText = Age1;
                        else
                        { txtAge1.InnerText = "<1"; }
                    }
                }
                else if (_presenter.View.TributeType.Equals("New Baby"))
                {
                    lblDate2.InnerText = "Due Date:";
                    txtDate2.InnerText = dt2.Value.ToString("MMMM dd, yyyy");
                    txtAge1.Visible = false;
                    lblAge1.Visible = false;
                }
            }
        }
    }

    public string City
    {
        set
        {
            txtLocation.InnerText = value.ToString();
            Session["Location"] = txtLocation.InnerText;
        }
    }


    protected string _tributeType;
    string ITributeHomePage.TributeType
    {
        get
        {
            return _tributeType;
        }
        set
        {
            _tributeType = value.ToString();
        }
    }

    public string TributeImage
    {
        get
        {
            return imgTributeImage.ImageUrl;
        }
        set
        {
            string[] virtualDir = CommonUtilities.GetPath();

            if (virtualDir != null)
            {
                imgTributeImage.ImageUrl = virtualDir[2] + value;


                Session["ImagePath"] = imgTributeImage.ImageUrl;
            }
        }
    }

    public string AdminOwner
    {
        get
        {
            return lbtnCreatedby.InnerText;
        }
        set
        {
            if (value.Length > 0)
            {
                string param2 = this._presenter.View.AdminOwnerId;
                lbtnCreatedby.InnerHtml = "<a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 + ");' >" + value.ToString() + "</a>";
            }


        }
    }

    protected void lbtnRemoveMessage_Click(object sender, EventArgs e)
    {
        addstory.Visible = false;
        _presenter.SetStoryVisibility();
    }


    private int _PackageId;
    public int PackageId
    {
        get
        {
            return _PackageId;

        }
        set
        {
            _PackageId = value;
        }
    }

    public DateTime? EndDate
    {

        set
        {
            Session["tributeEndDate"] = value;
            Nullable<DateTime> dt1 = value;
            if (dt1 != null)
            {
                if (this._presenter.View.PackageId == 2)
                {
                    lblAccountExpire.InnerText = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " account type:";
                    txtAccountExpire.InnerText = "Memorial Tribute (" + dt1.Value.ToString("MM/dd/yyyy") + ")";//"Tribute (1 Year)"; updated phase 1
                    btnsponcer.Visible = true; //false;

                    if (dt1 < DateTime.Now)
                    {
                        TimeSpan diff = DateTime.Now.Subtract(DateTime.Parse(dt1.ToString()));
                        lblAccountExpire.InnerText = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " account type:";
                        txtAccountExpire.InnerText = "Memorial Tribute (Expired)";// "Tribute (Expired)"; updated for phase 1
                    }
                }
                else if (this._presenter.View.PackageId == 3)
                {
                    lblAccountExpire.InnerText = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " account type:";
                    txtAccountExpire.InnerText = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " (Free Trial)";//Obituary (Lifetime)"; // updated for phase 1
                    btnsponcer.Visible = true;
                    if (dt1 < DateTime.Now)
                    {
                        TimeSpan diff = DateTime.Now.Subtract(DateTime.Parse(dt1.ToString()));
                        lblAccountExpire.InnerText = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " account type:";
                        txtAccountExpire.InnerText = "Announce (Free)"; //updated for phase 1
                    }
                }
                //LHK:(6:37 PM 2/4/2011)
                else if (this._presenter.View.PackageId == 5)
                {
                    lblAccountExpire.InnerText = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " account type:";
                    txtAccountExpire.InnerText = "Memorial Tribute (" + dt1.Value.ToString("MM/dd/yyyy") + ")";//"Tribute (1 Year)"; updated phase 1
                    if (dt1 < DateTime.Now)
                    {
                        txtAccountExpire.InnerText = "Memorial Tribute (Expired)";// "Tribute (Expired)"; updated for phase 1
                    }
                    btnsponcer.Visible = true;
                }
                else if (this._presenter.View.PackageId == 7)
                {
                    lblAccountExpire.InnerText = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " account type:";
                    txtAccountExpire.InnerText = "Premium Obituary (" + dt1.Value.ToString("MM/dd/yyyy") + ")"; //updated Photo Tribute (1 year) to Premium Obituary (XX/XX/XXXX) 
                    if (dt1 < DateTime.Now)
                    {
                        txtAccountExpire.InnerText = "Premium Obituary (Expired)";//"Photo Tribute (Expired)"; updated for Phase 1
                    }
                    btnsponcer.Visible = true; //false;
                }
                else if (this._presenter.View.PackageId == 8)
                {

                    lblAccountExpire.InnerText = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " account type:";
                    txtAccountExpire.InnerText = "Obituary (Lifetime)"; //updated Tribute (Free Trial) to Obituary (Lifetime) for phase 1
                    btnsponcer.Visible = true;
                    if (dt1 < DateTime.Now)
                    {
                        txtAccountExpire.InnerText = "Obituary (Lifetime)";// "Announce (Free)"; updated for phase 1
                    }
                }
            }
            else
            {
                if (this._presenter.View.PackageId == 1)
                {
                    lblAccountExpire.InnerText = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " account type:";
                    txtAccountExpire.InnerText = "Memorial Tribute (Lifetime)";
                    btnsponcer.Visible = false;
                }
                else if (this._presenter.View.PackageId == 4)
                {
                    lblAccountExpire.InnerText = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " account type:";
                    txtAccountExpire.InnerText = "Memorial Tribute (Lifetime)";
                    btnsponcer.Visible = false;
                }

                else if (this._presenter.View.PackageId == 6)
                {
                    lblAccountExpire.InnerText = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " account type:";

                    txtAccountExpire.InnerText = "Premium Obituary (Lifetime)";//"Photo Tribute (Lifetime)"; updated for Phase 1

                    btnsponcer.Visible = true;
                }
                else if (this._presenter.View.PackageId == 8)
                {
                    lblAccountExpire.InnerText = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " account type:";

                    txtAccountExpire.InnerText = "Obituary (Lifetime)";//"Photo Tribute (Lifetime)"; updated for Phase 1

                    btnsponcer.Visible = true;
                }
            }
        }
    }

    public string NotePostMessage
    {
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                string stripped = Regex.Replace(value.ToString(), @"<(.|\n)*?>", " ");

                stripped = CleanMessage(stripped);

                if (stripped.Length > 255)
                {
                    NoteMessage.InnerText = stripped.Substring(0, 255) + "....";

                }
                else
                    NoteMessage.InnerText = stripped;
                Announcement.Visible = true;
            }
            else
            {
                Announcement.Visible = false;
            }
        }
    }

    public string _NoteDate;
    public DateTime? NoteCreatedDate
    {
        set
        {
            Nullable<DateTime> dt1 = value;
            _NoteDate = dt1.Value.ToString("MMMM dd, yyyy");
            Application["_NoteDate"] = _NoteDate;
        }
    }

    public string NoteTitle
    {
        set
        {
            Announcement.Visible = true;
            title.InnerHtml = "<span class='yt-Date'>" + Application["_NoteDate"].ToString() + "</span>" + value.ToString();
            //title.InnerText = ;
        }
    }

    public int UserID
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                _UserID = objSessionvalue.UserId;
            }
            return _UserID;
        }
    }

    //Start - Modification on 7-Dec-09 for the enhancement 5 of the Phase 1
    private string _TributeCreatedByOrProvidedBy;
    public string TributeCreatedByOrProvidedBy
    {
        set
        {
            _TributeCreatedByOrProvidedBy = value;
        }
    }
    //End

    //Start - Modification on 17-Dec-09 for the enhancement 6 of the Phase 1
    public string CompanyLogo
    {
        set
        {
            if (value.Length > 0)
            {
                string[] virtualDir = CommonUtilities.GetPath();
                if (virtualDir != null)
                {
                    imgCompanyLogo.Src = virtualDir[2] + value;
                    divCompanyLogo.Visible = true;
                }

            }
            else
            {
                divCompanyLogo.Visible = false;
            }
        }
    }

    public bool isStory
    {
        set
        {
            addstory.Visible = value;
        }
    }

    public int VisitCount
    {
        set
        {
            Visit.InnerText = "Visits to this site: " + value.ToString();
        }
    }
    public System.Collections.Generic.IList<TributeLatest> TodayVideos
    {
        set
        {
            IList<TributeLatest> Latest = value;
        }
    }

    public IList<TributeLatest> YesterdayLatest
    {
        set
        {
            IList<TributeLatest> Latest = value;
        }
    }
    public IList<TributeLatest> ThirdLatest
    {
        set
        {
            IList<TributeLatest> Latest = value;
            if (Latest.Count > 0)
            {
            }
        }
    }

    public bool IsStoryAdded
    {
        set
        {
            addstory.Visible = value;
            if (value.Equals(true))
            {
                this._presenter.IsStoryHide();
            }
        }
    }

    public string PostMessage
    {
        get
        {
            return "<P>";
        }
    }

    public int ToUserId
    {
        get
        {
            return int.Parse(hfToUserid.Value);
        }
    }
    public string Subject
    {
        get
        {
            return "No Subject.";
        }
    }

    public int getPackageId
    {
        get
        {
            HtmlInputRadioButton rdoMembershipYearly = (HtmlInputRadioButton)this.FindControl("rdoMembershipYearly");
            HtmlInputRadioButton rdoMembershipLifetime = (HtmlInputRadioButton)this.FindControl("rdoMembershipLifetime");
            int _packageid = 0;
            if (rdoMembershipYearly.Checked)
            {
                _packageid = 2;
            }
            if (rdoMembershipLifetime.Checked)
            {
                _packageid = 1;
            }
            return _packageid;
        }
    }


    public IList<ParameterTypesCodes> PaymentModes
    {
        set
        {

        }
    }

    public bool IsSponserHide
    {

        get
        {
            bool _IsSponserHide = false;
            HtmlInputRadioButton rdoInformYes = (HtmlInputRadioButton)this.FindControl("rdoInformYes");
            if (rdoInformYes.Checked)
                _IsSponserHide = true;
            return _IsSponserHide;
        }
    }

    string _AdminOwnerId;
    public string AdminOwnerId
    {
        get
        {
            return _AdminOwnerId;
        }
        set
        {
            _AdminOwnerId = value;
        }
    }


    string tributetype_;
    public string TypeDescription
    {
        get
        {
            StateManager stateTribure = StateManager.Instance;
            Tributes objTribute = (Tributes)stateTribure.Get("TributeSession", StateManager.State.Session);
            return objTribute.TypeDescription;
        }
        set
        {
            tributetype_ = value;
        }
    }

    static int _noteid;
    public string NotesId
    {
        set
        {
            _noteid = int.Parse(value);
        }
    }


    static IList<Photos> _TodayAlbumPhotos;
    public IList<Photos> TodayAlbumPhotos
    {
        set
        {
            StateManager stateTribure = StateManager.Instance;
            Tributes objTribute = (Tributes)stateTribure.Get("TributeSession", StateManager.State.Session);
            if (!Equals(objTribute, null))
            {
                _tributeUrl = objTribute.TributeUrl;
                tributetype_ = objTribute.TypeDescription;
            }


            string[] getPath = CommonUtilities.GetPath();
            _TodayAlbumPhotos = value;
            for (int counter = 0; counter < value.Count; counter++)
            {
                _TodayAlbumPhotos[counter].PhotoImage = getPath[2] + "/" + getPath[3] + "/" + _tributeUrl.Replace(" ", "_") + "_" + tributetype_.Replace(" ", "_") + "/" + _TodayAlbumPhotos[counter].PhotoImage;
            }
        }
    }

    public IList<Photos> YesterdayAlbumPhotos
    {
        set { throw new Exception("The method or operation is not implemented."); }
    }

    public IList<Photos> ThirdAlbumPhotos
    {
        set { throw new Exception("The method or operation is not implemented."); }
    }

    public Tributes GetTributeSession
    {
        set
        {
            StateManager stateTributes = StateManager.Instance;
            stateTributes.Add("TributeSession", value, StateManager.State.Session);
        }
    }
    public string ObPostMessage
    {
        get
        {
            return _postMessage;
        }
        set
        {
            _postMessage = value;
            if (_postMessage.Length > 0)
            {
                //to shrink the spaces.
                while (_postMessage.Contains("&nbsp;&nbsp;"))
                {
                    _postMessage = _postMessage.Replace("&nbsp;&nbsp;", "&nbsp;");
                    _postMessage = _postMessage.Replace("&nbsp; &nbsp;", "&nbsp;");
                }
                while (_postMessage.Contains("  "))
                {
                    _postMessage = _postMessage.Replace("  ", " ");
                }
                Literal2.Text = _postMessage;
            }
        }
    }
    public string ObMessageWithoutHtml
    {
        get
        {
            return _messageWithoutHtml;
        }
        set
        {
            _messageWithoutHtml = value;
            if (_messageWithoutHtml.Length == 0)
            {
                obituaryBlock.Visible = false;
            }
        }
    }

    #endregion


    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtMessage.Text.ToString().Trim().ToLower().Equals("message"))
            {
                spanErrorMessage.InnerHtml = "Please enter a message.";
            }
            else
            {
                this._presenter.OnSaveComments(SaveData());
                txtMessage.Text = "Message";
                txtUserName.Text = "Name";
                Session.Remove("CommentsSession");
                if (WebConfig.ApplicationMode.Equals("local"))
                {
                    Response.Redirect(WebConfig.AppBaseDomain + _tributeUrl + "/guestbook.aspx");
                }
                else
                {
                    StateManager stateTribure_ = StateManager.Instance;
                    Tributes objTribute_ = (Tributes)stateTribure_.Get("TributeSession", StateManager.State.Session);
                    if (objTribute_.TypeDescription != null)
                        Response.Redirect("http://" + objTribute_.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTribute_.TributeUrl.ToString() + "/guestbook.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    /// <summary>
    /// Method to return the filled comment object to save
    /// </summary>
    /// <returns>Filled Comments entity</returns>
    public Comments SaveData()
    {
        try
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            Tributes objTribute = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);


            Comments objComment = new Comments();
            objComment.UserId = _UserID;
            objComment.TypeCodeId = typeCodeId;
            objComment.CommentTypeId = objTribute.TributeId;
            if (objSessionvalue != null)
                objComment.CreatedBy = objSessionvalue.UserId;
            else
                objComment.CreatedBy = 0;
            objComment.CodeTypeName = _typeName;

            // changed on 21-june -2011 by rupendra for YT phase 4
            if (_UserID > 0)
            {
                objComment.UserName = objSessionvalue.FirstName == string.Empty ? objSessionvalue.UserName : (objSessionvalue.FirstName + " " + objSessionvalue.LastName);
            }
            else
                objComment.UserName = txtUserName.Text.Trim();

            //New added to check wather user is connected with facebook
            // UserType   0---> Not loggedin,  1--> YT logged in , 2--> FB logeed in              
            if (FacebookWebContext.Current.Session != null)
            {
                objComment.UserType = "2";
            }
            else if (_UserID > 0)
            {
                objComment.UserType = "1";
            }
            else objComment.UserType = "0";
            // End




            objComment.TributeName = objTribute.TributeName.ToString();
            objComment.TributeType = objTribute.TypeDescription.ToString();
            objComment.TributeUrl = objTribute.TributeUrl.ToString();

            objComment.Message = txtMessage.Text.ToString().Trim();
            return objComment;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnValidateCoupon_Click(object sender, EventArgs e)
    {
    }

    #region ITributeHomePage Members

    bool _IsPercentage = false;
    public bool IsPercentage
    {
        get
        {
            return _IsPercentage;
        }
        set
        {
            _IsPercentage = value;
        }
    }

    string _Denom = string.Empty;
    public string Denomination
    {
        get
        {
            return _Denom;
        }
        set
        {
            _Denom = value;
        }
    }

    public Donation DonationCharity
    {
        set
        {
            //set the charity name in case charity Name and URL to epartners - both - exist
            if (value.CharityName != null && value.CharityName.Length != 0 && value.DonationUrl != null && value.DonationUrl.Length != 0)
            {

            }
            //set the default text as charity name in case only URL to epartners exist
            else if (value.DonationUrl != null && value.DonationUrl.Length != 0)
            {

            }
        }
    }

    #endregion

    // Added By Parul Jain
    protected void lbtnShareTribute_Click(object sender, EventArgs e)
    {
        StateManager stateTribure_ = StateManager.Instance;
        Tributes objTribute_ = (Tributes)stateTribure_.Get("TributeSession", StateManager.State.Session);
        if (objTribute_.TypeDescription != null)
            Response.Redirect(WebConfig.AppBaseDomain + "log_in.aspx?Url=" + _tributeUrl + "&Type=" + objTribute_.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "&session=logout");
    }

    #region ITributeHomePage Members


    public bool IsStoryHide
    {
        set
        {
            addstory.Visible = !value;
        }
    }

    #endregion

    private void SetLiteralHtml_()
    {
        CountMax = 23;
        StringBuilder objStrb = new StringBuilder();
        List<DateTime> objListLatestDate = new List<DateTime>();

        //LHK: code changes to reduce page load time
        objListLatestDate = _presenter.GetAllLatestDates();
        if (objListLatestDate != null)
        {
            foreach (DateTime objdate in objListLatestDate)
            {
                IList<TributeLatest> objLat = _presenter.GetAllLatest(objdate);
                if (objLat.Count > 0)
                {
                    string date = string.Empty;
                    string innerHtmlText = SetHtml(objLat);
                    if (!string.IsNullOrEmpty(innerHtmlText))
                    {
                        if (objdate.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                            date = "Today";
                        else if (objdate.ToShortDateString().Equals(DateTime.Now.AddDays(-1).ToShortDateString()))
                            date = "Yesterday";
                        else
                            date = objdate.ToString("MMMM dd, yyyy");
                        objStrb.Append("<H3>" + date + "</H3>");
                        objStrb.Append("<dl>" + innerHtmlText + "</dl>");
                    }


                }
            }
        }

        if (objStrb.ToString().Length == 0)
        {
            objStrb.Append("<h3>No Latest " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " Activity</h3>");
        }
        Literal1.Text = objStrb.ToString();
    }

    //LHK: Removed commented code on 11:30 AM 9/23/2011

    static int CountMax = 23;
    private string SetHtml(IList<TributeLatest> Latest)
    {


        StringBuilder sbChangeSiteTheme = new StringBuilder();
        if ((Latest.Count > 0) && (CountMax > 0))
        {
            int counter = 0;
            counter = Latest.Count;
            //if (CountMax >= Latest.Count)
            //{
            //    counter = Latest.Count;
            //}
            //else
            //{
            //    counter = CountMax;
            //}
            //CountMax = CountMax - Latest.Count;
            for (int i = 0; i < counter; i++)
            {

                if ((Latest[i].Type_.ToString() == "Obituary") && (_storyActivity < _maxStoryActivity))
                {
                    #region ObituaryActivity
                    _storyActivity++;
                    CountMax--;
                    string stripped = Regex.Replace(Latest[i].VideoDesc, @"<(.|\n)*?>", string.Empty);
                    string param2 = Latest[i].UserId.ToString();

                    if (Latest[i].VideoCaption == "Obituary")
                    {
                        string mode = string.Empty;
                        if (Latest[i].Mode == "A")
                        {
                            mode = "added an";
                        }
                        else
                        {
                            mode = "updated the";
                        }
                        sbChangeSiteTheme.Append("<dt><a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 + ");'  >" + Latest[i].FirstName + "</a> " + mode + "  <a href='story.aspx'> " + Latest[i].VideoCaption.ToLower() + "</a>: </dt>");
                        sbChangeSiteTheme.Append("<dd class='yt-Feed-Message'>");
                        if (stripped.Length < 50)
                            sbChangeSiteTheme.Append("&#8220;" + stripped + "&#8221;");
                        else
                        {
                            string stripped1 = stripped.Substring(0, 49).ToString() + "...";
                            sbChangeSiteTheme.Append("&#8220;" + stripped1 + "...&#8221;");
                        }
                        sbChangeSiteTheme.Append("&nbsp;<br/><a href='story.aspx'>Read the full Obituary...</a>");
                        sbChangeSiteTheme.Append("</dd>");
                    }
                    if (Latest[i].VideoCaption == "More About")
                    {
                        string mode_ = string.Empty;
                        if (Latest[i].Mode == "A")
                        {
                            mode_ = " added ";
                        }
                        else
                        {
                            mode_ = " updated ";
                        }

                        sbChangeSiteTheme.Append("<dt><a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 + ");'  >" + Latest[i].FirstName + "</a>" + mode_ + "&#8220;<a href='story.aspx'>" + Latest[i].VideoUrl + "</a>&#8221;" + " in the story:" + "</dt>");

                        sbChangeSiteTheme.Append("<dd class='yt-Feed-Message'>");
                        if (stripped.Length < 50)
                            sbChangeSiteTheme.Append("&#8220;" + stripped + "&#8221;");
                        else
                        {
                            string stripped1 = stripped.Substring(0, 49).ToString() + "...";
                            sbChangeSiteTheme.Append("&#8220;" + stripped1 + "...&#8221;");
                        }
                        sbChangeSiteTheme.Append("&nbsp;<br/><a href='story.aspx'>Read the full story...</a>");
                        sbChangeSiteTheme.Append("</dd>");
                    }
                    //More About 
                    #endregion
                }

                if ((Latest[i].Type_.ToString() == "Story") && (_storyActivity < _maxStoryActivity))
                {
                    #region storyActivity
                    _storyActivity++;
                    CountMax--;
                    string stripped = Regex.Replace(Latest[i].VideoDesc, @"<(.|\n)*?>", string.Empty);
                    string param2 = Latest[i].UserId.ToString();

                    if (Latest[i].VideoCaption == "Story")
                    {
                        string mode = string.Empty;
                        if (Latest[i].Mode == "A")
                        {
                            mode = "added a";
                        }
                        else
                        {
                            mode = "updated the";
                        }
                        sbChangeSiteTheme.Append("<dt><a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 + ");'  >" + Latest[i].FirstName + "</a> " + mode + "  <a href='story.aspx'> " + Latest[i].VideoCaption.ToLower() + "</a>: </dt>");
                        sbChangeSiteTheme.Append("<dd class='yt-Feed-Message'>");
                        if (stripped.Length < 50)
                            sbChangeSiteTheme.Append("&#8220;" + stripped + "&#8221;");
                        else
                        {
                            string stripped1 = stripped.Substring(0, 49).ToString() + "...";
                            sbChangeSiteTheme.Append("&#8220;" + stripped1 + "...&#8221;");
                        }
                        sbChangeSiteTheme.Append("&nbsp;<br/><a href='story.aspx'>Read the full story...</a>");
                        sbChangeSiteTheme.Append("</dd>");
                    }
                    if (Latest[i].VideoCaption == "More About")
                    {
                        string mode_ = string.Empty;
                        if (Latest[i].Mode == "A")
                        {
                            mode_ = " added ";
                        }
                        else
                        {
                            mode_ = " updated ";
                        }

                        sbChangeSiteTheme.Append("<dt><a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 + ");'  >" + Latest[i].FirstName + "</a>" + mode_ + "&#8220;<a href='story.aspx'>" + Latest[i].VideoUrl + "</a>&#8221;" + " in the story:" + "</dt>");

                        sbChangeSiteTheme.Append("<dd class='yt-Feed-Message'>");
                        if (stripped.Length < 50)
                            sbChangeSiteTheme.Append("&#8220;" + stripped + "&#8221;");
                        else
                        {
                            string stripped1 = stripped.Substring(0, 49).ToString() + "...";
                            sbChangeSiteTheme.Append("&#8220;" + stripped1 + "...&#8221;");
                        }
                        sbChangeSiteTheme.Append("&nbsp;<br/><a href='story.aspx'>Read the full story...</a>");
                        sbChangeSiteTheme.Append("</dd>");
                    }
                    //More About 
                    #endregion
                }
                else if ((Latest[i].Type_.ToString() == "Notes") && (_notesActivity < _maxNotesActivity))
                {
                    #region notesActivity
                    _notesActivity++;
                    CountMax--;
                    string mode = string.Empty;
                    if (Latest[i].Mode == "A")
                    {
                        mode = " added the note ";
                    }
                    else
                    {
                        mode = " updated the note ";
                    }

                    string stripped = Regex.Replace(Latest[i].VideoDesc, @"<(.|\n)*?>", string.Empty);
                    string param2 = Latest[i].UserId.ToString();
                    sbChangeSiteTheme.Append("<dt><a href='javascript:void(0);'  onclick='UserProfileModal_1(" + param2 + ");' >" + Latest[i].FirstName + "</a>" + mode + "&#8220;<a href='note.aspx?noteId=" + Latest[i].ID + "'>" + Latest[i].VideoCaption + "</a>&#8221;: </dt>");
                    sbChangeSiteTheme.Append("<dd class='yt-Feed-Message'>");

                    if (stripped.Length < 50)
                        sbChangeSiteTheme.Append("&#8220;" + stripped + "&#8221;");
                    else
                    {
                        string stripped1 = stripped.Substring(0, 49).ToString();// +"...";
                        sbChangeSiteTheme.Append("&#8220;" + stripped1 + "...&#8221;");
                    }

                    sbChangeSiteTheme.Append("&nbsp;<br/><a href='note.aspx?noteId=" + Latest[i].ID + "'>Read the full note...</a>");
                    sbChangeSiteTheme.Append("</dd>");
                    #endregion
                }
                else if ((Latest[i].Type_.ToString() == "Guestbook") && (_guestbookActivity < _maxGuestbookActivity))
                {
                    #region guestbookActivity
                    _guestbookActivity++;
                    CountMax--;
                    string param2 = Latest[i].UserId.ToString();
                    //Changes by Rupendra on 28july2011
                    if (Latest[i].UserId > 0)
                        sbChangeSiteTheme.Append("<dt><a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 + ");' >" + Latest[i].FirstName + "</a> signed the <a href='guestbook.aspx'>guestbook</a>:</dt>");
                    else
                        sbChangeSiteTheme.Append("<dt><a href='javascript:void(0);' style='cursor:default;text-decoration:none;' >" + Latest[i].FirstName + "</a> signed the <a href='guestbook.aspx'>guestbook</a>:</dt>");
                    sbChangeSiteTheme.Append("<dd class='yt-Feed-Message'>");
                    //LHK: for roland tribute comments breakline removal.
                    if ((Latest[i].VideoDesc.Contains("<p>")) || (Latest[i].VideoDesc.Contains("</p>")))
                    {
                        Latest[i].VideoDesc = Latest[i].VideoDesc.Replace("<p>", "");
                        Latest[i].VideoDesc = Latest[i].VideoDesc.Replace("</p>", "");
                    }
                    if (Latest[i].VideoDesc.Length < 50)
                        sbChangeSiteTheme.Append("&#8220;" + Latest[i].VideoDesc + "&#8221;");
                    else
                        sbChangeSiteTheme.Append("&#8220;" + Latest[i].VideoDesc.Substring(0, 50) + "...&#8221;");

                    sbChangeSiteTheme.Append("&nbsp;<br/><a href='guestbook.aspx'>Read the full message...</a>");
                    sbChangeSiteTheme.Append("</dd>");
                    #endregion
                }
                else if ((Latest[i].Type_.ToString() == "Gift") && (_giftsActivity < _maxGiftsActivity))
                {
                    #region giftsActivity
                    _giftsActivity++;
                    CountMax--;
                    string giftimage = string.Empty;
                    string[] virtualDir = CommonUtilities.GetPath();
                    if (virtualDir != null)
                    {
                        giftimage = virtualDir[2] + Latest[i].VideoUrl;
                    }
                    string stripped = Regex.Replace(Latest[i].VideoDesc, @"<(.|\n)*?>", string.Empty);
                    string param2 = Latest[i].UserId.ToString();
                    if (Latest[i].UserId != 0)
                        sbChangeSiteTheme.Append("<dt><a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 + ");' >" + Latest[i].FirstName + "</a> gave a <a href='gift.aspx'> gift</a>:</dt>");
                    else
                    {
                        string Name = Latest[i].FirstName;
                        if (Name.Length == 0)
                        {
                            Name = "Unregistered User";
                        }
                        sbChangeSiteTheme.Append("<dt>" + Name + " gave a <a href='gift.aspx'> gift: </a></dt>");
                    }

                    sbChangeSiteTheme.Append("<dd class='yt-Feed-Gift'>");
                    sbChangeSiteTheme.Append(" <a href='gift.aspx' class='yt-Thumb'>");
                    sbChangeSiteTheme.Append("<img src='" + giftimage.Replace("New Baby", "New%20Baby") + "' class='yt-Event-Photo' alt='' />");

                    if (Latest[i].VideoDesc.Length < 50)
                    {
                        if (Latest[i].VideoDesc.Length > 0)
                        {
                            sbChangeSiteTheme.Append("</a>&#8220;" + Latest[i].VideoDesc + "&#8221;");
                            sbChangeSiteTheme.Append("&nbsp;<br/><a href='gift.aspx'>Read the full message...</a> </dd>");
                        }
                        else
                            sbChangeSiteTheme.Append("</a></dd>");

                    }
                    else
                    {
                        sbChangeSiteTheme.Append("</a>&#8220;" + Latest[i].VideoDesc.Substring(0, 49) + "...&#8221;");
                        sbChangeSiteTheme.Append("&nbsp;<br/><a href='gift.aspx'>Read the full message...</a> </dd> ");
                    }

                    #endregion
                }
                else if ((Latest[i].Type_.ToString() == "Event") && (_eventsActivity < _maxEventsActivity))
                {
                    #region eventsActivity
                    _eventsActivity++;
                    CountMax--;
                    string eventimage = string.Empty;
                    string[] virtualDir = CommonUtilities.GetPath();
                    if (virtualDir != null)
                    {
                        string[] arrEventPath = Latest[i].VideoUrl.Split('/');
                        //eventimage = virtualDir[2] + Latest[i].VideoUrl.Split('/');
                        eventimage = virtualDir[2] + arrEventPath[0] + "/" + arrEventPath[1] + "/" + virtualDir[3] + "/" + arrEventPath[2];

                    }
                    string stripped = Regex.Replace(Latest[i].VideoDesc, @"<(.|\n)*?>", string.Empty);
                    string param2 = Latest[i].UserId.ToString();
                    string mode = string.Empty;
                    if (Latest[i].Mode == "A")
                    {
                        mode = " added the event ";
                    }
                    else
                    {
                        mode = " updated the event ";
                    }


                    sbChangeSiteTheme.Append("<dt><a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 + ");'  >" + Latest[i].FirstName + "</a>" + mode);
                    sbChangeSiteTheme.Append("&#8220;<a href='event.aspx?EventId=" + Latest[i].ID + "&TributeID=" + _TribureId + "'>" + Latest[i].VideoCaption + "</a>&#8221;:");
                    sbChangeSiteTheme.Append("</dt>");
                    sbChangeSiteTheme.Append("<dd class='yt-Feed-Thumbs'>");
                    sbChangeSiteTheme.Append("<a href='event.aspx?EventId=" + Latest[i].ID + "&TributeID=" + _TribureId + "' class='yt-Thumb' ><img src='" + eventimage + "' width='75' height='75' alt='' /></a>");

                    if (stripped.Length > 0)
                    {
                        if (stripped.Length < 50)
                            sbChangeSiteTheme.Append("&#8220;" + stripped + "&#8221;");
                        else
                        {
                            string stripped1 = stripped.Substring(0, 49).ToString(); //+ "...";
                            sbChangeSiteTheme.Append("&#8220;" + stripped1 + "...&#8221;");
                        }
                        //sbChangeSiteTheme.Append("&nbsp;<br/><a href='event.aspx?EventId=" + Latest[i].ID + "&TributeID=" + _TribureId + "'>Click for full event details...</a>");
                    }
                    sbChangeSiteTheme.Append("&nbsp;<br/><a href='event.aspx?EventId=" + Latest[i].ID + "&TributeID=" + _TribureId + "'>Click for full event details...</a>");
                    sbChangeSiteTheme.Append("</dd>");
                    #endregion
                }
                else if ((Latest[i].Type_.ToString() == "Album") && (_photosActivity < _maxPhotosActivity))
                {
                    #region photosActivity
                    _photosActivity++;
                    CountMax--;
                    string param2 = Latest[i].UserId.ToString();
                    sbChangeSiteTheme.Append("<dt>");
                    sbChangeSiteTheme.Append("<a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 + ");' >" + Latest[i].FirstName + "</a>");
                    sbChangeSiteTheme.Append(" added new photos to the album");
                    sbChangeSiteTheme.Append("&nbsp;&#8220;<a href='photoalbum.aspx?PhotoAlbumId=" + Latest[i].ID + "'>" + Latest[i].VideoCaption + "</a>&#8221;:");
                    sbChangeSiteTheme.Append("</dt>");
                    sbChangeSiteTheme.Append("<dd class='yt-Feed-Thumbs'>");
                    this._presenter.GetPhotosDatewise(Latest[i].ID);
                    if (_TodayAlbumPhotos.Count > 0)
                    {
                        for (int j = 0; j < _TodayAlbumPhotos.Count; j++)
                        {
                            sbChangeSiteTheme.Append("<a href='photo.aspx?PhotoId=" + _TodayAlbumPhotos[j].PhotoId + "' class='yt-Thumb' ><img src='" + _TodayAlbumPhotos[j].PhotoImage + "'  alt='' /></a>");
                        }
                    }
                    sbChangeSiteTheme.Append("</dd>");
                    #endregion
                }
                else if ((Latest[i].Type_.ToString() == "Video") && ((_videosActivity < _maxVideosActivity) || (_videoTributeActivity < _maxVideoTributeActivity)))
                {
                    string param2 = Latest[i].UserId.ToString();
                    string Videos = Latest[i].VideoUrl.ToString().Replace("v=", "`");
                    string VideoPicId = Latest[i].VideoTypeId.ToString();
                    string[] Videoid = Videos.Split('`');
                    string mode = string.Empty;



                    #region videoTributeActivity

                    if (Latest[i].VideoTributeUrl.ToString().Length > 0 &&
                        (_videoTributeActivity < _maxVideoTributeActivity))
                    {
                        sbChangeSiteTheme.Append("<dt>");
                        sbChangeSiteTheme.Append("<a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 +
                                                 ");' >" + Latest[i].FirstName + "</a>");



                        if (Latest[i].Mode == "A")
                        {
                            mode = " added the video";
                        }
                        else
                        {
                            mode = " updated the video";
                        }

                        if (Latest[i].VideoTributeUrl.ToString().Length > 0)

                            _videoTributeActivity++;
                        CountMax--;
                        sbChangeSiteTheme.Append(mode + " tribute");
                        sbChangeSiteTheme.Append(
                            " &#8220;<a href='video.aspx?mode=view&videoType=videotribute&videoId=" + Latest[i].ID +
                            "'>" + Latest[i].VideoCaption + "</a>&#8221;:");

                        sbChangeSiteTheme.Append("</dt>");
                        sbChangeSiteTheme.Append(" <dd class='yt-Feed-Thumbs'>");

                        if (Latest[i].VideoTributeUrl.ToString().Length > 0)
                        //if video is video tribute Added by Gaurav Puri on 16-May-2008
                        {
                            sbChangeSiteTheme.Append("<a href='video.aspx?mode=view&videoType=videotribute&videoId=" +
                                                     Latest[i].ID + "' class='yt-VideoLink yt-Thumb'>");
                            sbChangeSiteTheme.Append("<img src=' " + CommonUtilities.GetVideoTributePath()[2] +
                                                     _tributeUrl + "_" + _tributeType.Replace(" ", "_") + "/" +
                                                     Latest[i].VideoTributeUrl.ToString() + "_big.jpg" +
                                                     "' width=\"130\" height=\"97\" alt='Click to view' />");
                        }
                        else
                        {
                            sbChangeSiteTheme.Append("<a href='video.aspx?mode=view&videoType=video&videoId=" +
                                                     Latest[i].ID + "' class='yt-VideoLink yt-Thumb'>");

                            if (Videoid.Length > 1)
                                sbChangeSiteTheme.Append("<img src='http://img.youtube.com/vi/" + Videoid[1].ToString() +
                                                         "/default.jpg' alt='Click to view' />");
                            else if (VideoPicId.Length != 0)
                                sbChangeSiteTheme.Append("<img src='http://img.youtube.com/vi/" + VideoPicId.ToString() +
                                                         "/default.jpg' alt='Click to view' />");
                            else
                                sbChangeSiteTheme.Append("<img src='" + Session["APP_PATH"] +
                                                         "assets/images/sample_video.jpg' alt='Click to view' />");
                        }
                        sbChangeSiteTheme.Append("</a></dd>");
                    }

                    #endregion



                    #region videosActivity

                    else if (_videosActivity < _maxVideosActivity)
                    {
                        sbChangeSiteTheme.Append("<dt>");
                        sbChangeSiteTheme.Append("<a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 +
                                                 ");' >" + Latest[i].FirstName + "</a>");


                        if (Latest[i].Mode == "A")
                        {
                            mode = " added the video";
                        }
                        else
                        {
                            mode = " updated the video";
                        }

                        _videosActivity++;
                        CountMax--;
                        sbChangeSiteTheme.Append(mode);
                        sbChangeSiteTheme.Append(" &#8220;<a href='video.aspx?mode=view&videoType=video&videoId=" +
                                                 Latest[i].ID + "'>" + Latest[i].VideoCaption + "</a>&#8221;:");

                        sbChangeSiteTheme.Append("</dt>");
                        sbChangeSiteTheme.Append(" <dd class='yt-Feed-Thumbs'>");

                        if (Latest[i].VideoTributeUrl.ToString().Length > 0)
                        //if video is video tribute Added by Gaurav Puri on 16-May-2008
                        {
                            sbChangeSiteTheme.Append("<a href='video.aspx?mode=view&videoType=videotribute&videoId=" +
                                                     Latest[i].ID + "' class='yt-VideoLink yt-Thumb'>");
                            sbChangeSiteTheme.Append("<img src=' " + CommonUtilities.GetVideoTributePath()[2] +
                                                     _tributeUrl + "_" + _tributeType.Replace(" ", "_") + "/" +
                                                     Latest[i].VideoTributeUrl.ToString() + "_big.jpg" +
                                                     "' width=\"130\" height=\"97\" alt='Click to view' />");
                        }
                        else
                        {
                            sbChangeSiteTheme.Append("<a href='video.aspx?mode=view&videoType=video&videoId=" +
                                                     Latest[i].ID + "' class='yt-VideoLink yt-Thumb'>");

                            if (Videoid.Length > 1)
                                sbChangeSiteTheme.Append("<img src='http://img.youtube.com/vi/" + Videoid[1].ToString() +
                                                         "/default.jpg' alt='Click to view' />");
                            else if (VideoPicId.Length != 0)
                                sbChangeSiteTheme.Append("<img src='http://img.youtube.com/vi/" + VideoPicId.ToString() +
                                                         "/default.jpg' alt='Click to view' />");
                            else
                                sbChangeSiteTheme.Append("<img src='" + Session["APP_PATH"] +
                                                         "assets/images/sample_video.jpg' alt='Click to view' />");
                        }
                        sbChangeSiteTheme.Append("</a></dd>");
                    }

                    #endregion



                }
                else if ((Latest[i].Type_.ToString() == "NoteComment") && (_commentsActivity < _maxCommentsActivity))
                {
                    #region NoteComment
                    _noteCommentsActivity++;
                    _commentsActivity++;
                    CountMax--;
                    string stripped = Regex.Replace(Latest[i].VideoDesc, @"<(.|\n)*?>", string.Empty);
                    string param2 = Latest[i].UserId.ToString();
                    sbChangeSiteTheme.Append("<dt><a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 + ");'  >" + Latest[i].FirstName + "</a> commented on the note &#8220;<a href='note.aspx?noteId=" + Latest[i].VideoTypeId + "'>" + Latest[i].VideoCaption + "</a>&#8221;: </dt>");
                    sbChangeSiteTheme.Append("<dd class='yt-Feed-Message'>");
                    if (stripped.Length < 50)
                        sbChangeSiteTheme.Append("&#8220;" + stripped + "&#8221;");
                    else
                    {
                        string stripped1 = stripped.Substring(0, 49).ToString();
                        sbChangeSiteTheme.Append("&#8220;" + stripped1 + "...&#8221;");
                    }
                    sbChangeSiteTheme.Append("&nbsp;<br/><a href='note.aspx?noteId=" + Latest[i].VideoTypeId + "'>Read the full comment...</a>");
                    sbChangeSiteTheme.Append("</dd>");
                    #endregion
                }
                else if ((Latest[i].Type_.ToString() == "PhotoComment") && (_commentsActivity < _maxCommentsActivity))
                {
                    #region PhotoComment
                    _photoCommentsActivity++;
                    _commentsActivity++;
                    CountMax--;
                    StateManager stateTribure = StateManager.Instance;
                    Tributes objTribute = (Tributes)stateTribure.Get("TributeSession", StateManager.State.Session);
                    if (!Equals(objTribute, null))
                    {
                        _tributeUrl = objTribute.TributeUrl;
                        tributetype_ = objTribute.TypeDescription;
                    }

                    string stripped = Regex.Replace(Latest[i].VideoDesc, @"<(.|\n)*?>", string.Empty);
                    string[] getPath = CommonUtilities.GetPath();
                    string param2 = Latest[i].UserId.ToString();
                    string Videos = Latest[i].VideoUrl.ToString();
                    string VideoUrl = getPath[2] + "/" + getPath[3] + "/" + _tributeUrl.Replace(" ", "_") + "_" + tributetype_.Replace(" ", "_") + "/" + Videos;
                    string[] Videoid = Videos.Split('`');

                    sbChangeSiteTheme.Append("<dt><a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 + ");'  >" + Latest[i].FirstName + "</a> commented on a <a href='photo.aspx?PhotoId=" + Latest[i].VideoTypeId + "'> photo</a>:</dt>");
                    sbChangeSiteTheme.Append("<dd class='yt-Feed-Thumbs'>");
                    sbChangeSiteTheme.Append("<a href='photo.aspx?PhotoId=" + Latest[i].VideoTypeId + "' class='yt-Thumb' ><img src='" + VideoUrl + "'  alt='' /></a>");

                    if (stripped.Length < 50)
                        sbChangeSiteTheme.Append("&#8220;" + stripped + "&#8221;");
                    else
                    {
                        string stripped1 = stripped.Substring(0, 49).ToString() + "...";
                        sbChangeSiteTheme.Append("&#8220;" + stripped1 + "...&#8221;");
                    }
                    sbChangeSiteTheme.Append("&nbsp;<br/><a href='photo.aspx?PhotoId=" + Latest[i].VideoTypeId + "'>Read the full comment...</a>");
                    sbChangeSiteTheme.Append("</dd>");
                    #endregion
                }
                else if ((Latest[i].Type_.ToString() == "VideoComment") && (_commentsActivity < _maxCommentsActivity))
                {
                    #region VideoComment
                    string stripped = Regex.Replace(Latest[i].VideoDesc, @"<(.|\n)*?>", string.Empty);
                    string param2 = Latest[i].UserId.ToString();
                    string Videos = Latest[i].VideoUrl.ToString().Replace("v=", "`");
                    string VideoPicId = Latest[i].VideoTypeId.ToString();
                    string[] Videoid = Videos.Split('`');


                    if (Latest[i].VideoTributeUrl.ToString().Length > 0) //if video is video tribute Added by Gaurav Puri on 16-May-2008
                        sbChangeSiteTheme.Append("<dt><a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 + ");'  >" + Latest[i].FirstName + "</a> commented on a <a href='video.aspx?mode=view&videoType=videotribute&videoId=" + Latest[i].ID + "'> video tribute</a>: </dt>");
                    else
                        sbChangeSiteTheme.Append("<dt><a href='javascript:void(0);' onclick='UserProfileModal_1(" + param2 + ");'  >" + Latest[i].FirstName + "</a> commented on a <a href='video.aspx?mode=view&videoType=video&videoId=" + Latest[i].ID + "'> video</a>: </dt>");

                    sbChangeSiteTheme.Append("<dd>");


                    if (Latest[i].VideoTributeUrl.ToString().Length > 0) //if video is video tribute Added by Gaurav Puri on 16-May-2008
                    {
                        _videoTributeCommentsActivity++;
                        _commentsActivity++;
                        CountMax--;
                        sbChangeSiteTheme.Append("<a href='video.aspx?mode=view&videoType=videotribute&videoId=" + Latest[i].ID + "' class='yt-VideoLink yt-Thumb'>");
                        sbChangeSiteTheme.Append("<img src=' " + CommonUtilities.GetVideoTributePath()[2] + "/" + _tributeUrl + "_" + _tributeType + "/" + Latest[i].VideoTributeUrl.ToString() + "_big.jpg" + "' width=\"130\" height=\"97\" alt='Click to view' />");
                    }
                    else
                    {
                        _videoCommentsActivity++;
                        _commentsActivity++;
                        CountMax--;
                        sbChangeSiteTheme.Append("<a href='video.aspx?mode=view&videoType=video&videoId=" + Latest[i].ID + "' class='yt-VideoLink yt-Thumb'>");
                        if (Videoid.Length > 1)
                            sbChangeSiteTheme.Append("<img src='http://img.youtube.com/vi/" + Videoid[1].ToString() + "/default.jpg' alt='Click to view' />");
                        else if (VideoPicId.Length != 0)
                            sbChangeSiteTheme.Append("<img src='http://img.youtube.com/vi/" + VideoPicId.ToString() + "/default.jpg' alt='Click to view' />");
                        else
                            sbChangeSiteTheme.Append("<img src='" + Session["APP_PATH"] + "assets/images/sample_video.jpg' alt='Click to view' />");
                    }

                    sbChangeSiteTheme.Append("</a>");
                    if (stripped.Length < 50)
                        sbChangeSiteTheme.Append("&#8220;" + stripped + "&#8221;");
                    else
                    {
                        string stripped1 = stripped.Substring(0, 49).ToString() + "...";
                        sbChangeSiteTheme.Append("&#8220;" + stripped1 + "...&#8221;");
                    }

                    if (Latest[i].VideoTributeUrl.ToString().Length > 0) //if video is video tribute Added by Gaurav Puri on 16-May-2008
                        sbChangeSiteTheme.Append("&nbsp;<br/><a href='video.aspx?mode=view&videoType=videotribute&videoId=" + Latest[i].ID + "'>Read the comment...</a>");
                    else
                        sbChangeSiteTheme.Append("&nbsp;<br/><a href='video.aspx?mode=view&videoType=video&videoId=" + Latest[i].ID + "'>Read the full comment...</a>");

                    sbChangeSiteTheme.Append("</dd>");
                    #endregion
                }
            }
        }
        return sbChangeSiteTheme.ToString();
    }

    protected void lnkSponsor_Click(object sender, EventArgs e)
    {
        StateManager stateTribure_ = StateManager.Instance;
        Tributes objTribute_ = (Tributes)stateTribure_.Get("TributeSession", StateManager.State.Session);
        if ((objTribute_ != null) || (objTribute_.TributeId > 0))
        {
            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                Response.Redirect(Session["APP_BASE_DOMAIN"] + "MomentsSponsor.aspx?WebsiteURL=" + objTribute_.TributeUrl + "&WebsiteType=" + objTribute_.TypeDescription.ToLower().Replace("new baby", "newbaby"));
            else
                Response.Redirect(Session["APP_BASE_DOMAIN"] + "TributeSponsor.aspx?TributeURL=" + objTribute_.TributeUrl + "&TributeType=" + objTribute_.TypeDescription.ToLower().Replace("new baby", "newbaby"));
        }
        else
        {
            // Added by Ashu on Oct 4, 2011 for rewrite URL 
            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                Response.Redirect(Session["APP_BASE_DOMAIN"] + "MomentsSponsor.aspx?WebsiteURL=" + Request.QueryString["TributeUrl"] + "&WebsiteType=" + Request.QueryString["TributeType"]);
            else
                Response.Redirect(Session["APP_BASE_DOMAIN"] + "TributeSponsor.aspx?TributeURL=" + Request.QueryString["TributeUrl"] + "&TributeType=" + Request.QueryString["TributeType"]);
        }
    }

    public void lnkLoginBUtton_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session["APP_BASE_DOMAIN"] + "/log_in.aspx", false);
    }

    /// <summary>
    /// method to remove the HTML symbols for special characters before displaying on screen
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private string CleanMessage(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            if (message.Contains("&nbsp;"))
                message = message.Replace("&nbsp;", " ");
            if (message.Contains("&quot;"))
                message = message.Replace("&quot;", "\'");
            if (message.Contains("&amp;"))
                message = message.Replace("&amp;", "&");
            if (message.Contains("&gt;"))
                message = message.Replace("&gt;", ">");
            if (message.Contains("&lt;"))
                message = message.Replace("&lt;", "<");
            if (message.Contains("&#33;"))
                message = message.Replace("&#33;", "!");
            if (message.Contains("&#34;"))
                message = message.Replace("&lt;", "\"");
            if (message.Contains("&#35;"))
                message = message.Replace("&#35;", "#");
            if (message.Contains("&#36;"))
                message = message.Replace("&#36;", "$");
            if (message.Contains("&#37;"))
                message = message.Replace("&#37;", "%");
            if (message.Contains("&#40;"))
                message = message.Replace("&#40;", "(");
            if (message.Contains("&#41;"))
                message = message.Replace("&#41;", ")");
        }
        return message;
    }
    public string StripHtml(string htmlString)
    {
        Regex regex = new Regex("</?(.*)>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        string finalString = Regex.Replace(htmlString, @"<(.|\n)*?>", string.Empty);  //regex.Replace(htmlString, regex, string.Empty);
        return finalString;
    }

    //deligate added for firing button click event 
    public delegate void BtnClick_deligate(object sender, EventArgs e);

    //to save guestbook comment in a session.
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SetSessionValues(string sname, string smessg)
    {
        ArrayList _arrComent = new ArrayList();
        _arrComent.Insert(0, sname);
        _arrComent.Insert(1, smessg);
        HttpContext.Current.Session["CommentsSession"] = _arrComent;
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void setIsInTopurl(bool inIframe)
    {
        HttpContext.Current.Session["isInIframe"] = inIframe;
    }
}