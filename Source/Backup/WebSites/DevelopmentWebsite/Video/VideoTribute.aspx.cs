///Copyright       : Copyright (c) Optimus Information
///Project         : Your Tributes
///File Name       : TributePortal.DevelopmentWebsite.Videos.VideoTribute.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page defines the items to be displayed on the video type tribute pages  on the site
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
using TributesPortal.Video.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using TributesPortal.MessagingSystem;
using System.Web.SessionState;
using TributesPortal.ResourceAccess;
using System.Globalization;
#endregion

public partial class Video_VideoTribute : PageBase, IVideoTribute
{
    #region CLASS VARIABLES
    private VideoTributePresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;

    private int _userId;
    private string _userName;

    private string _nextPrev = string.Empty;
    private string typeCodeName = PortalEnums.ModuleName.Video.ToString();
    private int _tributeId;
    private string _tributeName;
    private string _tributeType;
    private string _tributeUrl;
    Nullable<DateTime> dt1;
    private int _age;
    private bool _isPrivate;
    private string _city;
    private string _state;
    private string _country;
    private int? _linkMemTributeId;
    private bool _isMemTributeBoxChecked = false;
    private bool isAdmin = false;
    private bool _isUserLoggedIn = false;

    protected bool _isActive;
    private string profile_prefix = CommonUtilities.GetPath()[2].ToString();
    private string _TributePackageType;
    private DateTime _endDate;
    private string StoryImageURL = "";

    private int VideoTributeType;

    //private int _createdBy;
    private int? _videoId;
    private int? _videoUserId;
    private int _userTributeId;
    private string _tributeUserEmail;
    private string _tributeVideoId;
    //private string _videoUrl;
    private string _userEmail = "";
    private string VideoExpiryDate;

    //Header properties variables
    //private string strBusinessAddress;
    //private string strPhone;
    //private string strHeaderBGColor;
    //private string strHeaderLogo;
    //private string strWebsite;
    //private bool boolIsAddressOn;
    //private bool boolIsPhoneon;
    //private bool boolIsWebSiteon;
    private bool boolCustomHeaderOn;
    //private bool boolObituaryLinkOn;
    //private string strObituaryLink;
    //private DateTime _endDate;
    protected string URL;
    private string _emailID;
    private string _typeName;
   // private string _userBussCity;      // commented by Ud to remove warning
    //private string _userBussState;           // commented by Ud to remove warning
    //private bool isTributeIdNumeric;               // commented by Ud to remove warning
    bool IsCstmHeaderOn = true;

    #endregion

    #region CONSTANT
    private const string TYPE_NAME_TO_SAVE = "Video";
    private const string TYPE_NAME = "VideoTribute";
    private const string TYPE_NAME_TRIBUTE = "DisplayVideoTribute";
    private const string DefaultPath = "~/assets/";

    #endregion

    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            Response.Cache.SetExpires(DateTime.Now);
            StateManager objStateManager = StateManager.Instance;
            //fetching values from session
            GetValuesFromSession();
            Tributes objTrb = new Tributes();
            Tributes objTrbForLinkMem = new Tributes();
            Videos objVideos = new Videos();
            HeaderHome.Visible = false;
            divEditButton.Visible = false;
            dvUploadPhoto.Visible = false;
            divLearnMore.Visible = false;
            divViewMemorialTribute.Visible = false;
            divOrderDVD.Visible = false;

            divHeaderUC.Visible = false;
            lblDate1.Visible = false;
            lblAge.Visible = false;
            SetControlsValue();

            //url check
            if ((Request.QueryString["tributeId"] != null) && (int.TryParse(Request.QueryString["tributeId"].ToString(), out _tributeId)))
            {
                // For passing in Create Tribute Button of Modal Popup
                Session["TributeId"] = _tributeId.ToString();

                //LHK: Check for expiry message
                string tributeEndDate = _presenter.GetTributeEndDate(_tributeId);
                #region if loop
                if (tributeEndDate != "")
                {
                    if (!tributeEndDate.Equals("Never"))
                    {
                        DateTime dt1 =DateTime.Now;

                        DateTime.TryParseExact(tributeEndDate, "MM'/'dd'/'yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None, out dt1);
                        if (dt1 > DateTime.Now)
                        {
                            VideoExpiryDate = dt1.ToString("dd/MM/yyyy");
                            if (WebConfig.ApplicationMode.Equals("local"))
                            {
                                lblErrorMessage.Text = "<img src='../assets/images/warn.png'/> The video can be viewed until<b> " + dt1.ToString("MMMM dd, yyyy") + "</b>. To keep it online longer, <a href='" + Session["APP_PATH"].ToString() + "tribute/videotributesponsor.aspx?tributeId=" + _tributeId + "' style='font-weight:bold'>click here to upgrade</a>";
                            }
                            else
                            {
                                lblErrorMessage.Text = "<img src='../assets/images/warn.png'/> The video can be viewed until <b> " + dt1.ToString("MMMM dd, yyyy") + "</b>. To keep it online longer, <a href='http://video." + WebConfig.TopLevelDomain + "/tribute/videotributesponsor.aspx?tributeId=" + _tributeId + "' style='font-weight:bold'>click here to upgrade</a>";
                            }
                        }
                        else
                        {
                            VideoExpiryDate = dt1.ToString("dd/MM/yyyy");
                            if (WebConfig.ApplicationMode.Equals("local"))
                            {
                                lblErrorMessage.Text = "<img src='../assets/images/error_pic.png'/> This video has expired. To reactivate the video so that it can be viewed for life. <a href='" + Session["APP_PATH"].ToString() + "tribute/videotributesponsor.aspx?tributeId=" + _tributeId + "'style='color:Red;font-weight:bold' >click here to upgrade</a>";
                            }
                            else
                            {
                                lblErrorMessage.Text = "<img src='../assets/images/error_pic.png'/> This video has expired. To reactivate the video so that it can be viewed for life. <a href='http://video." + WebConfig.TopLevelDomain + "/tribute/videotributesponsor.aspx?tributeId=" + _tributeId + "'style='color:Red;font-weight:bold' >click here to upgrade</a>";
                            }
                        }
                    }
                    else
                    {
                        VideoExpiryDate = null;
                        lblErrorMessage.Text = "<img src='../assets/images/warn.png'/> This video has been upgraded and can be viewed forever!";
                    }
                }
                #endregion

                #region HeaderHome
                //HeaderHome visible for logged in user
                if (_userId > 0)
                {

                    HeaderHome.Visible = true;

                }
                #endregion
                //fetching Tribute Details on TributeId
                objTrb = _presenter.GetTributeFieldDetails(_tributeId);

                #region loop for video tribute
                if (objTrb != null)
                {
                    if (objTrb.TributeType == 8)
                    {
                        //fetching Video Details on tributeId
                        _tributeType = "Video";
                        objVideos = _presenter.GetVideoDetailsOnUserTributeId(_tributeId);

                        //for Link mem tribute id
                        _presenter.GetLinkVideoMemorialTribute(_userId, _tributeId);
                        //check VideoFile mapping wrt to tributeId. (video url contains fileName)
                        #region ifloopAdmin check
                        if (objVideos != null)
                        {
                            if (objVideos.TributeVideoId != null)
                            {

                                if (_userId == _videoUserId)
                                {
                                    isAdmin = true;
                                }
                                //objTrbForLinkMem = _presenter.GetDetailOfLinkedtribute(Convert.ToInt32(_linkMemTributeId.ToString()));
                                //Session["TributeURlofLinkMemTrb"] = objTrbForLinkMem.TributeUrl.ToString();
                            }
                        }
                        #endregion

                        if (_linkMemTributeId > 0)
                        {
                            objTrbForLinkMem = _presenter.GetDetailOfLinkedtribute(Convert.ToInt32(_linkMemTributeId.ToString()));
                            if (objTrbForLinkMem != null)
                                Session["TributeURlofLinkMemTrb"] = objTrbForLinkMem.TributeUrl.ToString();
                        }
                        //if (objTrb.TributeType.Equals(8))
                        //{                            
                        //fetching Video Details on tributeId
                        objVideos = _presenter.GetVideoDetailsOnUserTributeId(_tributeId);

                        //check VideoFile mapping wrt to tributeId. (video url contains fileName)
                        #region if loop3
                        if (objVideos != null)
                        {
                            if (objVideos.TributeVideoId != null)
                            {
                                if (_userId == 0)
                                {
                                    // TributeCustomHeader.Visible = false;
                                    divHeaderUC.Visible = false;
                                    _userId = _userTributeId;
                                }
                                lblMailforDVD.Text = "<a href='mailto:" + TributeUserEmail.ToString() + "?subject=Request to order Video Tribute on DVD'>Email funeral home to order a DVD</a> ";
                                //LHK: 7/1/2011 -check for private removed if (_isPrivate)

                                ShowVideo();

                                //AG: Added for visit count
                                if (_tributeId > 0)
                                {
                                    this._presenter.AddTributeCount(_tributeId);
                                }

                                Users objUsers = _presenter.GetUserNameOnUserId(_userId);
                                TributeCustomHeader.UserName = objUsers.UserName.ToString();

                                GetHeaderDetailsOnUserId(_userId);
                                if (IsCustomHeaderOn == true)
                                {
                                    TributeCustomHeader.Visible = true;
                                    //EmptyDivAboveMainPanel.Visible = false;
                                    //LHK:7/1/2011-revomed TributeCustomHeader
                                }
                                else
                                {
                                    TributeCustomHeader.Visible = false;
                                    divHeaderUC.Visible = false;
                                    if (!_isUserLoggedIn)
                                    {
                                        //LHK: to add a blank div for saparation between header and page body.
                                        //EmptyDivAboveMainPanel.Visible = true;
                                    }
                                }
                                if (!this.IsPostBack)
                                {
                                    try
                                    {
                                        this._presenter.OnViewInitialized();
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                                this._presenter.OnViewLoaded();
                                SetControlsVisibility();
                            }
                            else
                            {
                                Response.Redirect("~/Errors/Error404.aspx", false);
                            }
                        }
                        else
                        {
                            Response.Redirect("~/Errors/Error404.aspx", false);
                        }
                        #endregion
                    }
                }
                else
                {
                    Response.Redirect("~/Errors/Error404.aspx", false);

                }
                #endregion

                //AG: Added new folder name
                string folderName = this._presenter.GetExistingFolderName(_tributeId);
                //body.Attributes.Add("onload", "Themer('" + themeValue + "');");
                string appPath = string.Empty;
                if (WebConfig.ApplicationMode.ToLower().Equals("local"))
                {
                    appPath = WebConfig.AppBaseDomain;
                }
                else
                {
                    appPath = string.Format("{0}{1}{2}", "http://www.", WebConfig.TopLevelDomain, "/");
                }
                idSheet.Attributes.Add("href", appPath + "assets/themes/" + folderName + "/theme.css"); //to set the selected theme
            }
            else
            {
                Response.Redirect("~/Errors/Error404.aspx", false);
            }


            //LHK:EmptyDivAboveMainPanel
            StateManager stateMngr = StateManager.Instance;
            SessionValue objSessvalue = (SessionValue)stateMngr.Get("objSessionvalue", StateManager.State.Session);

            if (_tributeUrl != null)
            {
                GetCustomHeaderVisible(_tributeUrl, WebConfig.ApplicationType.ToString());
            }
            if (!(objSessvalue != null))
            {
                if (!IsCstmHeaderOn)
                {
                    EmptyDivAboveMainPanel.Visible = true;
                }
            }
            //LHK:EmptyDivAboveMainPanel
            if (_tributeName != null) Page.Title = _tributeName + " | " + _tributeType + " Tribute";
        }
        catch (Exception ex)
        {
            //throw ex;
            Response.Redirect("~/Errors/Error404.aspx", false);
        }
    }

    //protected void lbtnEditPersonalDetail_Click(object sender, EventArgs e)
    //{
    //    //Implement Open EditModelPopUp
    //    StringBuilder sbl = new StringBuilder();
    //    sbl.Append("<a class='yt-horizontalSpacer' href='");
    //    sbl.Append("log_in.aspx");
    //    sbl.Append("javascript: void(0);' onclick='UserLoginModalpopupFromSubDomain(location.href,document.title);");

    //    sbl.Append("'>Log in</a>");
    //    //spanLogout.InnerHtml = sbl.ToString();
    //}
    #endregion

    #region PROPERTIES
    [CreateNew]
    public VideoTributePresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    //from story
    public string TributeImage
    {
        set
        {
            string[] virtualDir = CommonUtilities.GetPath();
            if (virtualDir != null)
            {
                imgTributeImageView.ImageUrl = virtualDir[2] + value;
            }
            Session["TributeImage"] = value;
        }
    }
    public string TributeName
    {
        set
        {
            _tributeName = value;
            LblOwnwerName.Text = value;
            LblTributeNameLearnMoreBtn.Text = value;
            LblTributeNameViewTributeBtn.Text = value;
            imgTributeImageView.AlternateText = value;
            Session["TributeName"] = value;
        }

    }
    public DateTime? Date1
    {

        set
        {
            dt1 = value;
            if (dt1 != null)
            {
                lblDate1.Visible = true;
                lblOwnerDOB.Text = value.ToString();
                DateTime tmpDate = DateTime.Parse(lblOwnerDOB.Text);
                lblOwnerDOB.Text = dt1.Value.ToString("MMMM dd, yyyy");
            }
        }
    }
    public DateTime? Date2
    {
        set
        {
            Nullable<DateTime> dt2 = value;
            if (dt2 != null)
            {
                //hdnVideoExpiryDate.Value = Date2.Value;
                lblOwnerDOD.Text = value.ToString();
                DateTime tmpDate = DateTime.Parse(lblOwnerDOD.Text);
                lblOwnerDOD.Text = dt2.Value.ToString("MMMM dd, yyyy");
                if (dt1.Equals(null))
                {
                    lblDate1.Visible = true;
                    lblAge.Visible = true;
                    lblDate1.Text = "  ";
                    lblOwnerDOB.Text = "  ";
                    lblAge.Text = "  ";
                    lblOwnerAge.Text = "  ";
                }
            }
        }
    }

    public int Age
    {
        set
        {
            _age = value;
            //if (_age != null)   // commented by Ud to remove warning
            if (_age != 0)
                lblAge.Visible = true;
            if (_age < 1)
            {
                lblOwnerAge.Text = "<1";

            }
            else
            {
                lblOwnerAge.Text = _age.ToString();
            }
        }
    }
    public string City
    {
        get
        {
            return _city;
        }
        set
        {
            _city = value.ToString();

        }
    }
    public string State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
        }
    }
    public string Country
    {
        get
        {
            return _country;
        }
        set
        {
            _country = value;

        }
    }
    public bool IsOrderDVDChecked
    {
        set
        {
            _isMemTributeBoxChecked = value;
            divOrderDVD.Visible = value;

        }
    }
    public bool IsMemTributeBoxChecked
    {
        set
        {
            divIsMemTributeOn.Visible = value;
        }
    }
    public int TributeType
    {
        set
        {
            VideoTributeType = value;
        }
    }

    public string Location
    {
        set
        {
            lblLocation.Text = value;
        }
    }
    //for video

    //public int CreatedBy
    //{
    //    set
    //    {
    //        _createdBy = value;
    //    }
    //}
    public string TributeVideoId
    {
        set
        {
            _tributeVideoId = value;
        }
    }
    public int UserId
    {
        get
        {
            return _userId;
        }
        set
        {
            _userId = value;
        }
    }

    public string UserEmail
    {
        set
        {
            StateManager objStateManager = StateManager.Instance;
            objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
            if (!Equals(objSessionValue, null))
            {
                _userEmail = objSessionValue.UserEmail;
            }
        }
        get
        {
            StateManager objStateManager = StateManager.Instance;
            objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
            if (!Equals(objSessionValue, null))
            {
                return objSessionValue.UserEmail;
            }
            else
                return null;
        }
    }
    public int? VideoId
    {
        set
        {
            _videoId = value;
        }
    }
    public int? VideoUserId
    {
        get
        {
            return _videoUserId;
        }
        set
        {
            _videoUserId = value;
            Session["UserId"] = value;
        }
    }

    public int UserTributeId
    {
        get
        {
            return _userTributeId;
        }
        set
        {
            _userTributeId = value;
        }
    }
    public string TributeUserEmail
    {
        get
        {
            return _tributeUserEmail;
        }
        set
        {
            _tributeUserEmail = value;
        }
    }
    public bool IsPrivate
    {
        set
        {
            _isPrivate = value;
        }
    }

    //public string TributeType
    //{
    //    set
    //    {
    //        _tributeType = value;
    //    }
    //}
    public string TributeUrl
    {
        get
        {
            return _tributeUrl;
        }
        set
        {
            _tributeUrl = value;
            Session["TributeUrl"] =value;
           
        }
      
    }

    public Nullable<int> LinkMemoTributeId
    {
        set
        {
            _linkMemTributeId = value;
        }
    }

    //public string VideoUrl
    //{
    //    set
    //    {
    //        _videoUrl = value;
    //    }
    //}

    //Properties for tribute header

    //public string BusinessAddress
    //{
    //    get
    //    {
    //        return strBusinessAddress;

    //    }
    //    set
    //    {
    //        strBusinessAddress = value;
    //    }

    //}
    //public string Phone
    //{
    //    get
    //    {
    //        return strPhone;

    //    }
    //    set
    //    {
    //        strPhone = value;
    //    }

    //}

    //public string HeaderBGColor
    //{
    //    get
    //    {
    //        return strHeaderBGColor;
    //    }
    //    set
    //    {
    //        strHeaderBGColor = value;
    //    }

    //}

    //public string HeaderLogo
    //{
    //    get
    //    {
    //        return strHeaderLogo;

    //    }
    //    set
    //    {
    //        strHeaderLogo = value;
    //        //Session["HeaderLogo"] = value.ToString();
    //        StateManager objStateManager = StateManager.Instance;
    //        objStateManager.Add("HeaderLogo",value, StateManager.State.Session);
    //    }

    //}

    //public string WebSite
    //{
    //    get
    //    {
    //        return strWebsite;
    //    }
    //    set
    //    {
    //        strWebsite = value;

    //    }

    //}
    //public bool IsAddressOn
    //{
    //    get
    //    {
    //        return boolIsAddressOn;
    //    }
    //    set
    //    {
    //        boolIsAddressOn = Convert.ToBoolean(value);

    //    }
    //}
    //public bool IsPhoneOn
    //{
    //    get
    //    {
    //        return boolIsPhoneon;
    //    }
    //    set
    //    {
    //        boolIsPhoneon = Convert.ToBoolean(value);

    //    }
    //}

    //public bool IsWebSiteOn
    //{
    //    get
    //    {
    //        return boolIsWebSiteon;
    //    }
    //    set
    //    {
    //        boolIsWebSiteon = Convert.ToBoolean(value);

    //    }
    //}
    

    //public bool IsObituaryURLOn
    //{
    //    get
    //    {
    //        return boolObituaryLinkOn;
    //    }
    //    set
    //    {
    //        boolObituaryLinkOn = Convert.ToBoolean(value);

    //    }
    //}
    //public string ObituaryURL
    //{
    //    get
    //    {
    //        return strObituaryLink;
    //    }
    //    set
    //    {
    //        strObituaryLink = value;
    //    }
    //}
    //public string UserBussCity
    //{
    //    get
    //    {
    //        return _userBussCity;
    //    }
    //    set
    //    {
    //        _userBussCity = value;
    //    }
    //}
    //public string UserBussState
    //{
    //    get
    //    {
    //        return _userBussState;
    //    }
    //    set
    //    {
    //        _userBussState = value;
    //    }
    //}
    public bool IsCustomHeaderOn
    {
        get
        {
            return boolCustomHeaderOn;
        }
        set
        {
            boolCustomHeaderOn = Convert.ToBoolean(value);

        }
    }
    #endregion

    #region METHODS
    #region Tribute HeaderMethods
    private void GetHeaderDetailsOnUserId(int userId)
    {
        _presenter.GetHeaderDetailsOnUserId(userId);
    }
    #endregion


    private void GetValuesFromSession()
    {
        StateManager objStateManager = StateManager.Instance;

        //to get user id from session as user is logged in user
        objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionValue, null))
        {
            //Commented by LHK: as this is implemented in header now.EmptyDivAboveMainPanel.Visible = false;
            _isUserLoggedIn = true;
            _userId = objSessionValue.UserId;
            if (objSessionValue.UserType == 1)
            {
                if (objSessionValue.IsUsernameVisiable)
                {
                    _userName = objSessionValue.UserName;
                }
                else
                {
                    _userName = (objSessionValue.FirstName + " " + objSessionValue.LastName);
                }
            }
            else
            {
                _userName = objSessionValue.FirstName == string.Empty ? objSessionValue.UserName : (objSessionValue.FirstName + " " + objSessionValue.LastName);
            }
        }
        if (!(_userEmail == null || _userEmail == ""))
        {
            _userEmail = objSessionValue.UserEmail;
        }
        else
        {
            _userEmail = string.Empty;
        }
        objTribute = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);

        if (!Equals(objTribute, null))
        {
            _tributeId = objTribute.TributeId;
            _tributeName = objTribute.TributeName;
            _tributeType = objTribute.TypeDescription;
            _tributeUrl = objTribute.TributeUrl;
            _isActive = objTribute.IsActive;
            _TributePackageType = objTribute.TributePackageType;
            if (!objTribute.Date2.Equals(null))
            {
                _endDate = (DateTime)objTribute.Date2;
            }

        }

        if (Session["TributeSession"] == null)
            CreateTributeSession(); //to create the tribute session values if user comes to this page from link or from favorites list.

        else if (objStateManager.Get("VideoSession", StateManager.State.Session) != null)
        {
            _videoId = int.Parse(objStateManager.Get("VideoSession", StateManager.State.Session).ToString());
        }
    }
    private void CreateTributeSession()
    {
        Tributes objTribute = new Tributes();
        objTribute.TributeId = _tributeId;
        objTribute.TributeName = _tributeName;
        objTribute.TypeDescription = _tributeType;
        objTribute.TributeUrl = _tributeUrl;
        objTribute.IsActive = _isActive;
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add("TributeSession", objTribute, TributesPortal.Utilities.StateManager.State.Session);
    }
    private void SetControlsVisibility()
    {
        if (_linkMemTributeId > 0)
        {
            divLearnMore.Visible = false;
            divViewMemorialTribute.Visible = true;
        }
        else
        {
            divLearnMore.Visible = true;
            divViewMemorialTribute.Visible = false;
        }
        if (isAdmin)
        {
            //divHeaderUC.Visible = true;
            divEditButton.Visible = true;
            dvUploadPhoto.Visible = true;
        }
        //HeaderHome.Visible = _isUserLoggedIn;
    }


    private void SetTributeImageUrl()
    {
        if (StoryImageURL != string.Empty)
        {
            //hdnStoryImageURL.Value = StoryImageURL.ToString();
            //imgStoryImage.Src = StoryImageURL.ToString();
            //StoryImageURL = string.Empty;
        }
    }

    private void ShowVideo()
    {

        StringBuilder sb = new StringBuilder();
        string[] path = CommonUtilities.GetVideoTributePath();

        string swfFile = path[2] + _tributeUrl + "_" + _videoUserId + "_Video/" + _tributeVideoId + ".swf";
        string swfPlayer = string.Format("{0}{1}", path[2], "videoswfplayer.swf");
        if (_tributeVideoId != null)
        {

            if (VideoExpiryDate != null)
            {
                //sb.Append("<object height='480' width='720' wmode='transparent' type='application/x-shockwave-flash' allowScriptAccess='sameDomain' pluginspage='http://www.adobe.com/go/getflashplayer' allowFullScreen='false' bgcolor='ffffff' align='left' id='shell' name='shell' data='" + swfPlayer + "'><param name='menu' value='false'><param name='allowFullScreen' value='false' /><param name='bgcolor' value='#ffffff' /><param name='quality' value='high' /><param name='allowScriptAccess' value='sameDomain' /><param name='flashvars' value='MovieName=" + swfFile.Trim() + "&amp;ExpiryDate=" + VideoExpiryDate.Trim() + "&amp;UpgradeURL=" + Session["APP_PATH"].ToString() + "tribute/videotributesponsor.aspx?tributeId=" + _tributeId + "'></object>");
                mainBody.Attributes.Add("onload", "javascript:showvideo('" + swfPlayer + "','" + swfFile + "','" + VideoExpiryDate + "','" + Session["APP_PATH"].ToString() + "tribute/videotributesponsor.aspx?tributeId=" + _tributeId + "');");
            }
            else
            {
                mainBody.Attributes.Add("onload", "javascript:showvideo('" + swfPlayer + "','" + swfFile + "','null','" + Session["APP_PATH"].ToString() + "tribute/videotributesponsor.aspx?tributeId=" + _tributeId + "');");
                //sb.Append("<object height='480' width='720' wmode='transparent' type='application/x-shockwave-flash' allowScriptAccess='sameDomain' pluginspage='http://www.adobe.com/go/getflashplayer' allowFullScreen='false' bgcolor='ffffff' id='shell' align='left' name='shell' data='" + swfPlayer + "'><param name='menu' value='false'><param name='allowFullScreen' value='false' /><param name='bgcolor' value='#ffffff' /><param name='quality' value='high' /><param name='allowScriptAccess' value='sameDomain' /><param name='flashvars' value='MovieName=" + swfFile.Trim() + "&amp;ExpiryDate=null&amp;UpgradeURL=" + Session["APP_PATH"].ToString() + "tribute/videotributesponsor.aspx?tributeId=" + _tributeId + "'></object>");
            }

        }
        

    }


    protected void lbtnSubmit_Click(object sender, EventArgs e)
    {
        URL = hdnTypeToMail.Value;
        List<string> lstEmailAddresses = new List<string>();
        string[] emailAddresses = (txtEmailAddress.Text.Replace(",", ";")).Split(';');

        foreach (string strEmailAddress in emailAddresses)
        {
            lstEmailAddresses.Add(strEmailAddress);
        }
        SetValueForEmailInSession(_typeName);
        SendEmail(lstEmailAddresses);
    }

    private void SetValueForEmailInSession(string typeName)
    {
        StateManager stateManager = StateManager.Instance;
        EmailLink objEmail = new EmailLink();
        objEmail.EmailBody = "<font style='font-size: 12px; font-family:Lucida Sans;'>";
        string PageName = "";
        string ApplicationPath = "<a href='" + URL + "'>" + URL + "</a>";
        string UrlToEmail = ApplicationPath;
        objEmail.FromEmailAddress = _emailID;

        if (typeName == "GuestBook")
        {
            PageName = "Guestbook";
        }
        else if (typeName == PortalEnums.TributeContentEnum.Story.ToString())
        {
            PageName = PortalEnums.TributeContentEnum.Story.ToString();
        }
        else if (typeName == PortalEnums.TributeContentEnum.Gift.ToString())
        {
            PageName = PortalEnums.TributeContentEnum.Gift.ToString();
        }
        else if (typeName == PortalEnums.TributeContentEnum.Event.ToString())
        {
            PageName = PortalEnums.TributeContentEnum.Event.ToString();
        }
        else if (typeName == PortalEnums.TributeContentEnum.EventFullView.ToString())
        {
            PageName = PortalEnums.TributeContentEnum.Event.ToString();
        }
        else if (typeName == "TributeNotes")
        {
            PageName = PortalEnums.TributeContentEnum.Notes.ToString();
        }
        else if (typeName == "NoteFullView")
        {
            PageName = PortalEnums.TributeContentEnum.Notes.ToString();
        }
        else if (typeName == "VideoGallery")
        {
            PageName = "Videos";
        }
        else if (typeName == "ManageVideo")
        {
            PageName = "Video";
        }
        else if (typeName == "PhotoGallery")
        {
            PageName = "Photos";
        }
        else if (typeName == "PhotoAlbum")
        {
            PageName = "Photo Album";
        }
        else if (typeName == "PhotoFullView")
        {
            PageName = "Photo";
        }
        else if (typeName == "ManageVideoTribute")
        {
            PageName = "VideoTribute";
        }

        if (typeName == "TributeNotes")
            objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtReadEmailWThe_SM_Master") + " " + PageName + " " + ResourceText.GetString("txtOnYT_SM_Master");
        else if (typeName == "NoteFullView")
            objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtReadEmail_SM_Master") + " Note " + ResourceText.GetString("txtOnYT_SM_Master");
        else if (typeName == "Event")
            objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWTA_SM_Master") + " Events " + ResourceText.GetString("txtOnYT_SM_Master");
        else if (typeName == "EventFullView")
            objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtViewEmailEvent_SM_Master") + " Event " + ResourceText.GetString("txtOnYT_SM_Master");
        else if (typeName == "Gift")
            objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWTA_SM_Master") + " Gifts " + ResourceText.GetString("txtOnYT_SM_Master");
        else if (typeName == "GuestBook")
            objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtReadEmail_SM_Master") + " Guestbook " + ResourceText.GetString("txtOnYT_SM_Master");
        else if (typeName == "Story")
            objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtReadEmail_SM_Master") + " Story " + ResourceText.GetString("txtOnYT_SM_Master");
        else if (typeName == "VideoGallery")
            objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWTA_SM_Master") + " a Video " + ResourceText.GetString("txtOnYT_SM_Master");
        else if (typeName == "PhotoGallery")
            objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWTA_SM_Master") + " Photos " + ResourceText.GetString("txtOnYT_SM_Master");
        else if (typeName == "ManageVideoTribute")
            objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWTA_SM_Master") + " a Video Tribute " + ResourceText.GetString("txtOnYT_SM_Master");
        else
            objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtViewEmail_SM_Master") + " " + PageName + " " + ResourceText.GetString("txtOnYT_SM_Master");


        objEmail.TypeName = typeName;

        StringBuilder sbEmailbody = new StringBuilder();

        if (typeName == "TributeNotes")
            sbEmailbody.Append(txtUserName.Text + " " + ResourceText.GetString("txtReadEmailWThe_SM_Master") + " " + PageName + " in the " + _tributeName + " " + _tributeType + " Tribute.");
        else if (typeName == "NoteFullView")
            sbEmailbody.Append(txtUserName.Text + " " + ResourceText.GetString("txtReadEmail_SM_Master") + " Note in the " + _tributeName + " " + _tributeType + " Tribute.");
        else if (typeName == "Event")
            sbEmailbody.Append(txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWThe_SM_Master") + " Events in the " + _tributeName + " " + _tributeType + " Tribute.");
        else if (typeName == "EventFullView")
            sbEmailbody.Append(txtUserName.Text + " " + ResourceText.GetString("txtViewEmailEvent_SM_Master") + " Event in the " + _tributeName + " " + _tributeType + " Tribute.");
        else if (typeName == "Gift")
            sbEmailbody.Append(txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWThe_SM_Master") + " Gifts in the " + _tributeName + " " + _tributeType + " Tribute.");
        else if (typeName == "GuestBook")
            sbEmailbody.Append(txtUserName.Text + " " + ResourceText.GetString("txtReadEmailWThe_SM_Master") + " Guestbook in the " + _tributeName + " " + _tributeType + " Tribute.");
        else if (typeName == "Story")
            sbEmailbody.Append(txtUserName.Text + " " + ResourceText.GetString("txtReadEmail_SM_Master") + " Story in the " + _tributeName + " " + _tributeType + " Tribute.");
        else if (typeName == "VideoGallery")
            sbEmailbody.Append(txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWTA_SM_Master") + " a Video in the " + _tributeName + " " + _tributeType + " Tribute.");
        else if (typeName == "PhotoGallery")
            sbEmailbody.Append(txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWThe_SM_Master") + " Photos in the " + _tributeName + " " + _tributeType + " Tribute.");
        else if (typeName == "ManageVideoTribute")
            sbEmailbody.Append(txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWTA_SM_Master") + " a Video Tribute in the " + _tributeName + " " + _tributeType + " Tribute.");
        else
            sbEmailbody.Append(txtUserName.Text + " " + ResourceText.GetString("txtViewEmail_SM_Master") + " " + PageName + " in the " + _tributeName + " " + _tributeType + " Tribute.");

        sbEmailbody.Append("<br/>");
        sbEmailbody.Append("<br/>");
        if (typeName == "Event")
            sbEmailbody.Append(ResourceText.GetString("txtToView_SM_Master") + " Events" + ResourceText.GetString("txtFollowLink_SM_Master"));
        else if (typeName == "Gift")
            sbEmailbody.Append(ResourceText.GetString("txtToView_SM_Master") + " Gifts" + ResourceText.GetString("txtFollowLink_SM_Master"));
        else if (typeName == "NoteFullView")
            sbEmailbody.Append(ResourceText.GetString("txtToView_SM_Master") + " Note" + ResourceText.GetString("txtFollowLink_SM_Master"));
        else if (typeName == "VideoGallery")
            sbEmailbody.Append(ResourceText.GetString("txtToView_SM_Master") + " Video" + ResourceText.GetString("txtFollowLink_SM_Master"));
        else if (typeName == "ManageVideoTribute")
            sbEmailbody.Append(ResourceText.GetString("txtToView_SM_Master") + " Video Tribute" + ResourceText.GetString("txtFollowLink_SM_Master"));
        else
            sbEmailbody.Append(ResourceText.GetString("txtToView_SM_Master") + " " + PageName + ResourceText.GetString("txtFollowLink_SM_Master"));

        sbEmailbody.Append("<br/>");
        sbEmailbody.Append(UrlToEmail);
        sbEmailbody.Append("<br/>");
        sbEmailbody.Append("<br/>");
        sbEmailbody.Append("---");
        sbEmailbody.Append("<br/>");
        sbEmailbody.Append(ResourceText.GetString("txtFrom_SM_Master"));

        objEmail.EmailBody = objEmail.EmailBody + sbEmailbody.ToString();
        stateManager.Add(PortalEnums.SessionValueEnum.ShareTributeEmail.ToString(), objEmail, StateManager.State.Session);
    }

    public void SendEmail(List<string> lstEmailAdd)
    {
        StateManager stateManager = StateManager.Instance;
        EmailLink objEmail = (EmailLink)stateManager.Get(PortalEnums.SessionValueEnum.ShareTributeEmail.ToString(), StateManager.State.Session);
        string URLToEmail = URL;
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

    private void SetControlsValue()
    {
        try
        {
            ////Text for labels from the resource file
            //lblFindTribute.Text = ResourceText.GetString("lblFindTribute_MP");                      // Find a Tribute
            //lblSearchFor.InnerText = ResourceText.GetString("lblSearchFor_MP");                     // Search for:
            ////txtSearchKeyword.Text = ResourceText.GetString("txtSearchKeyword_MP");                  // Enter the name of a Tribute
            //lblSearch_All.InnerText = ResourceText.GetString("lblSearch_All_MP");                   // All Tributes
            //lblSearch_Anniversary.InnerText = ResourceText.GetString("lblSearch_Anniversary_MP");   // Anniversary Tributes
            //lblSearch_Birthday.InnerText = ResourceText.GetString("lblSearch_Birthday_MP");         // Birthday Tribute
            //lblSearch_Graduation.InnerText = ResourceText.GetString("lblSearch_Graduation_MP");     // Graduation Tributes
            //lblSearch_Memorial.InnerText = ResourceText.GetString("lblSearch_Memorial_MP");         // Memorial Tributes
            //lblSearch_NewBaby.InnerText = ResourceText.GetString("lblSearch_NewBaby_MP");           // New Baby Tributes
            //lblSearch_Wedding.InnerText = ResourceText.GetString("lblSearch_Wedding_MP");           // Wedding Tributes
            //lnkAdvanceSearch.Text = ResourceText.GetString("lnkAdvanceSearch_MP");                  // Advanced Search
            //lnkClose.InnerText = ResourceText.GetString("lnkClose_MP");                             // Close

            //txtSearchKeyword.Attributes.Add("onclick", "this.select();");

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

    private void GetCustomHeaderVisible(string _tributeUrl, string ApplicationType)
    {
        IsCustomHeaderOn = false;
        VideoResource objVideoResource = new VideoResource();
        IsCstmHeaderOn = objVideoResource.GetCustomHeaderVisible(_tributeUrl, ApplicationType);
    }
    #endregion


}
