///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.Video.ManageVideo.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to view the uploaded video
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Video.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using System.IO;
#endregion

public partial class Video_ManageVideo : PageBase, IManageVideo
{
    #region CLASS VARIABLES
    private ManageVideoPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    List<VideoGallery> objVideoDetails;
    private int _userId = 0;
    private string _userName;
    private int _videoId;
    private string _nextPrev = string.Empty;
    private string typeCodeName = PortalEnums.ModuleName.Video.ToString();
    private int currentPage;
    private int _tributeId;
    private string _tributeName;
    private string _tributeType;
    private string _tributeUrl;
    private int pageSize;
    private bool isAdmin;
    private int typeCodeId = 4; //it's for video
    private int totalComments;
    private string _videoType;
    protected bool _isActive;
    private string profile_prefix = CommonUtilities.GetPath()[2].ToString();
    private string _TributePackageType;
    private DateTime _endDate;

    //LHK: WordPress Integration
    private string _TopUrl = string.Empty;
    private bool isInIframe = false;

    private int? _userTributeId;
    private int _packageId;
    #endregion

    #region CONSTANT
    private const string TYPE_NAME_TO_SAVE = "Video";
    private const string TYPE_NAME = "ManageVideo";
    private const string TYPE_NAME_TRIBUTE = "ManageVideoTribute";
    private const string MODULE_FUNCTIONALITY_NAME = "Comment";
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
     {
        //LHK: 3:59 PM 9/5/2011 - Wordpress topURL
        if (Request.QueryString["topurl"] != null)
        {
            _TopUrl = Request.QueryString["topurl"].ToString();
            Response.Cookies["topurl"].Value = _TopUrl;
            Response.Cookies["topurl"].Domain = "." + WebConfig.TopLevelDomain;
            Response.Cookies["topurl"].Expires = DateTime.Now.AddHours(4);
        }

        int topHeight = 165;
        Response.Cache.SetExpires(DateTime.Now);
        StateManager objStateManager = StateManager.Instance;
        GetValuesFromSession();
        objVideoDetails = (List<VideoGallery>)objStateManager.Get("VideoDetails", StateManager.State.Session);

        //DJ: Added code for expiry message
        string appDomian = string.Empty;
        if (WebConfig.ApplicationMode.Equals("local"))
        {
            appDomian = WebConfig.AppBaseDomain.ToString();
        }
        else
        {
            appDomian = "http://" + _tributeType.ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";
        }
        string strVideoType = "";
        if (Request.QueryString["videoType"] != null)
            strVideoType = Request.QueryString["videoType"].ToString();
        else
            strVideoType = "";

        string tributeEndDate = _presenter.GetTributeEndDate(_tributeId);
        //MG:Expiry Notice
        //AG(20-jan-11): Added topheight for expiry page
        bool isCustomeHeaderOn = _presenter.GetCustomHeaderDetail(_tributeId);
        if (Equals(objSessionValue, null))//when not logged in
        {
            if (isCustomeHeaderOn)
                topHeight = 197;
            else
                topHeight = 74;
        }
        else
        {
            if (isCustomeHeaderOn)
                topHeight = 258;
            else
                topHeight = 131;
        }
 
        if (!this.IsPostBack)
        {
            try
            {
                //LHK: for Wordpress integration
                Ajax.Utility.RegisterTypeForAjax(typeof(Video_ManageVideo));
                lbtnPost.Attributes.Add("onclick", "setIsInTopurl();");

                this._presenter.OnViewInitialized();
                UserIsAdminOrOwner(""); //to set the visibility of options in side menu.
                SetControlsVisibility();
                this._presenter.GetVideoDetails(SetCommentObject(currentPage));
                Page.SetFocus(txtVideoComment);
                _presenter.GetPackage();
            }
            catch (Exception ex)
            {
                Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/Error404.aspx");
            }
        }
        if (!tributeEndDate.Equals("Never"))
        {
            string[] date = tributeEndDate.Split('/');
            if (!(string.IsNullOrEmpty(date[0])))
            {
                DateTime date2 = new DateTime(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1]));
                if ((date2 < DateTime.Now) && (strVideoType.ToLower() == "videotribute") && (_packageId == 3 || _packageId == 5 || _packageId == 7 || _packageId == 8))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "fnExpiryNoticePopupClose();", true);

                    if (_tributeType.ToLower().Equals("memorial") && strVideoType == "videotribute")
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "fnExpiryNoticePopup('location.href','document.title','Memo','" + _tributeId + "','" + appDomian + "','" + topHeight + "');", true);
                    else
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "fnExpiryNoticePopup('location.href','document.title','NonMemo','" + _tributeId + "','" + appDomian + "','" + topHeight + "');", true);


                }
            }
        }

        SetValuesToLabels();

        this._presenter.OnViewLoaded();
    }

    protected void lbtnEditVideo_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnDeleteVideo_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
    }

    protected void dlindex_ItemCommand(object source, DataListCommandEventArgs e)
    {
    }

    protected void lbtnPre_Click(object sender, EventArgs e)
    {
        VideoGallery objValues = objVideoDetails[0];
        _videoId = (int)objValues.Videos.VideoId;
        _nextPrev = "Prev";
        _videoId = int.Parse(objVideoDetails[0].PrevRecordCount.ToString()); //to get the previous video id to be displayed
        Response.Redirect("~/" + Session["TributeURL"] + "/video.aspx?mode=view&videoId=" + _videoId, false);
    }

    protected void lbtnNext_Click(object sender, EventArgs e)
    {
        VideoGallery objValues = objVideoDetails[0];
        _videoId = (int)objValues.Videos.VideoId;
        _nextPrev = "Next";
        _videoId = int.Parse(objVideoDetails[0].NextRecordCount.ToString()); //to get the next video id to be displayed
        Response.Redirect("~/" + Session["TributeURL"] + "/video.aspx?mode=view&videoId=" + _videoId, false);
    }

    protected void dlComments_DeleteCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            Comments objComment = new Comments();
            objComment.CommentId = int.Parse(e.CommandArgument.ToString());
            objComment.UserId = _userId;
            this._presenter.DeleteComment(objComment);
            this._presenter.GetVideoDetails(SetCommentObject(currentPage));
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    protected void dlComments_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
            btnDelete.Text = ResourceText.GetString("btnDelete_NFV");
            btnDelete.Attributes.Add("onclick", "if(confirm('" + ResourceText.GetString("msgDelete_NFV") + "')){}else{return false}");

            HiddenField hdnUserID = (HiddenField)e.Item.FindControl("hdnUserId");
            int UserId = int.Parse(hdnUserID.Value.ToString());

            if (isAdmin)
            {
                btnDelete.Visible = true;
            }
            else if (_userId == UserId)
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }

            CommentTributeAdministrator comment = (CommentTributeAdministrator)e.Item.DataItem;
            StringBuilder html = new StringBuilder();
            HtmlGenericControl itemProfilePicture = (HtmlGenericControl)e.Item.FindControl("itemProfilePicSpn");
            HtmlImage itemprofilepic = (HtmlImage)e.Item.FindControl("itemProfilePicImg");

            if (comment.FacebookUid != null)
            {
                if (Facebook.Web.FacebookWebContext.Current.Session != null)
                {
                    html.Append("<span style='border-bottom:solid 1px white ;border-right:solid 1px white ; width:58px;height:58px; '>");
                    html.Append("<fb:profile-pic uid=\"");
                    html.Append(comment.FacebookUid.ToString());
                    html.Append("\" size=\"square\" facebook-logo=\"true\"></fb:profile-pic></span>");
                    itemProfilePicture.InnerHtml = html.ToString();
                    itemprofilepic.Visible = false;
                }
                else
                {
                    itemprofilepic.Src = "http://graph.facebook.com/" + comment.FacebookUid.ToString() + "/picture?type=square";
                    itemProfilePicture.Visible = false;
                }
            }
            else
            {
                html.Append("<img style='border-bottom:solid 1px white ;border-right:solid 1px white ; width:48px; ' src='");
                html.Append(comment.UserImage.ToString());
                html.Append("' alt='Photo of ");
                html.Append(comment.UserName.ToString());
                html.Append("'  height='48' />");
                itemProfilePicture.InnerHtml = html.ToString();
                itemprofilepic.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    //for wordpress 
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SetSessionTopurl(string svalue)
    {
        Session["isInIframe"] = svalue;
    }

    protected void lbtnPost_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["topurl"] != null)
            {
                _TopUrl = Request.Cookies["topurl"].Value.ToString().Trim().ToLower();
            }
            if (Session["isInIframe"] != null)
            {
                isInIframe = bool.Parse(Session["isInIframe"].ToString());
                Session["isInIframe"] = null;
            }
            if (!(string.IsNullOrEmpty(_TopUrl)) && isInIframe)
            {
                _TopUrl = _TopUrl.Replace("topurl=", "");
                this._presenter.SaveComment(GetCommentDataToSave(), _TopUrl);
            }
            else
            {
                this._presenter.SaveComment(GetCommentDataToSave());
            }
            string queryString = string.Empty;
            if (Request.QueryString["videoType"] != null)
                queryString = "?PageNo=1&mode=view&videoType=videotribute&videoId=" + _videoId;
            else
                queryString = "?PageNo=1&mode=view&videoId=" + _videoId; //"?TributeId=" + _tributeId + "&TributeName=" + _tributeName + "&TributeType=" + _tributeType + "&PageNo=1";

            Response.Redirect(Context.Request.RawUrl.Split("?".ToCharArray())[0] + queryString, false);
        }
        catch (Exception ex)
        {
            throw (ex);
        }

    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public ManageVideoPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    public List<VideoGallery> VideoDetails
    {
        set
        {
            StringBuilder sb = new StringBuilder();
            if (value != null)
            {
                if ((_videoType == "videotribute") && !(string.IsNullOrEmpty(_tributeType)))
                {
                    //Start - Modification on 9-Dec-09 for the enhancement 3 of the Phase 1
                    if (_tributeName != null) Page.Title = _tributeName + " | Video Tribute";
                    //Page.Title = "Video Tribute";
                    //End
                    hVideoView.InnerText = "Video Tribute"; 
                    string[] path = CommonUtilities.GetVideoTributePath();
                    string PathVideoFile = path[1] + _tributeUrl + "_" + _tributeType.Replace(" ","_") + "/" + value[0].Videos.TributeVideoId + ".swf";
                    string linkVideoFile = path[2] + "/" + _tributeUrl + "_" + _tributeType.Replace(" ", "_") + "/" + value[0].Videos.TributeVideoId + ".swf";

                    if (!string.IsNullOrEmpty(value[0].Videos.TributeVideoId.ToString()))
                        Master.fbThumbnail = path[2] + "/" + _tributeUrl + "_" + _tributeType.Replace(" ", "_") + "/" + value[0].Videos.TributeVideoId + ".jpg";

                    if (!(File.Exists(PathVideoFile)))
                    {
                        string OldTributeUrl = _presenter.GetOldTributeUrlOnTributeId(_tributeId);
                        if (!(_tributeUrl.Equals(OldTributeUrl)))
                        {
                            //Copy file code
                            CopyVideo(OldTributeUrl,_tributeUrl,_tributeType);
                            //send mail code
                            string strlink = "http://" + _tributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _tributeUrl + "/";
                            string videolink = Request.RawUrl.ToString();
                            _presenter.SendMailToAdmin(strlink, videolink, _tributeUrl, _tributeType);
                        }
                    }

                    sb.Append("<a href='javascript:void(0);' onclick=" + "\"" + "PopupVideoForFlashPlayer('" + path[2] + "','" + linkVideoFile + "');" + "\"");

                    sb.Append("class='yt-TributeVideoThumb yt-VideoLink'>");
                    sb.Append("<img src='" + path[2] + "/" + _tributeUrl + "_" + _tributeType.Replace(" ","_") + "/" + value[0].Videos.TributeVideoId + "_big.jpg' alt='' height = '320px' width = '480px' />");
                    sb.Append("<span class='yt-Click'>Click to play full size video tribute</span>");
                    sb.Append("</a>"); 

                    if (Session["tributeEndDate"] != null)
                    {
                        // Added by Ashu on Oct 4, 2011 for rewrite URL 
                        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                            divExpired.InnerHtml = "<br/><p> This video can be viewed until <b>" + Convert.ToDateTime(Session["tributeEndDate"]).ToString("MMMM dd, yyyy") + "</b>.</p>" + "<p> If you would like to keep it online longer, <a href=" + Session["APP_BASE_DOMAIN"] + "MomentsSponsor.aspx?TributeURL=" + _tributeUrl + "&TributeType=" + _tributeType.Replace("New Baby", "newbaby") + "><b>click here to sponsor this " + _tributeType.ToLower() + " tribute.</b></a></p>";
                        else
                            divExpired.InnerHtml = "<br/><p> This video can be viewed until <b>" + Convert.ToDateTime(Session["tributeEndDate"]).ToString("MMMM dd, yyyy") + "</b>.</p>" + "<p> If you would like to keep it online longer, <a href=" + Session["APP_BASE_DOMAIN"] + "TributeSponsor.aspx?TributeURL=" + _tributeUrl + "&TributeType=" + _tributeType.Replace("New Baby", "newbaby") + "><b>click here to sponsor this " + _tributeType.ToLower() + " tribute.</b></a></p>";
                        divExpired.Visible = true;
                    }
                    else
                    {
                        divExpired.InnerHtml = "<br/><p> This " + _tributeType.ToLower() + " tribute has been sponsored so that this video can be viewed forever.</p>";
                        divExpired.Visible = true;
                    }
                }
                else
                {
                    //Start - Modification on 9-Dec-09 for the enhancement 3 of the Phase 1
                    if (_tributeName != null) Page.Title = _tributeName + " | Video";
                    //End
                    hVideoView.InnerText = ResourceText.GetString("lblVideoView_MV");
                    sb.Append("<div id='yt-flashcontent'>Youtube Video -- Flash is required to view this video or you have an old version of Adobe's Flash Player. <a href='http://www.macromedia.com/go/getflashplayer'>Get the latest Flash player.</a></div>");
                    sb.Append("<script type='text/javascript'>EmbedVideo('http://www.youtube.com/v/" + value[0].IdForDisplay + "'); </script>");
                }

                spPageMode.InnerText = value[0].Videos.VideoCaption;
                hVideoName.InnerText = value[0].Videos.VideoCaption;

                divVideoDisplay.InnerHtml = sb.ToString();
                divDesc.InnerHtml = "<p>" + value[0].Videos.VideoDesc + "</p>" + " <p>Uploaded by <a href='javascript:void(0);' onclick=\"UserProfileModal_1('" + value[0].Videos.UserId + "');\">" + value[0].UserName + "</a> on " + value[0].CreatedDate + "</p>";

                if (!string.IsNullOrEmpty(value[0].Videos.VideoDesc.ToString()))
                    Master.fbDescription = value[0].Videos.VideoDesc.ToString();
                

                //save the Video details in Session for Edit.
                StateManager objStateManager = StateManager.Instance;
                objStateManager.Add("VideoDetails", value, StateManager.State.Session);
            }
            else
            {
                Response.Redirect("~/" + "Errors/Error404.aspx", false);
            }
        }

    }

    public List<CommentTributeAdministrator> Comments
    {
        set
        {
            if (value.Count == 0 && currentPage > 1)
            {
                string queryString = "?PageNo=" + (currentPage - 1) + "&videoId=" + _videoId;
                Response.Redirect("~/" + Session["TributeURL"] + "/video.aspx" + queryString, false);
            }
            dlComments.DataSource = value;
            dlComments.DataBind();
        }
    }

    public int? UserTributeId
    {
        get { return _userTributeId; }
        set { _userTributeId = value; }
    }

    public int PackageId
    {
        set { _packageId = value; }
    }
    public string DrawPaging
    {
        set
        {
            spanPagingTop.InnerHtml = value;
            spanPagingBottom.InnerHtml = value;
        }
    }
    public string RecordCount
    {
        set
        {
            spanRecordCountBottom.InnerText = value;
            spanRecordCountTop.InnerText = value;
        }
    }
    public int CommentCount
    {
        set
        {
            totalComments = value;
            //to set the visibility of paging and message if no note exists for the tribute.
            if (value > 0)
            {
                divPagingTop.Visible = true;
                divCommentsList.Visible = true;
                divPagingBottom.Visible = true;
            }
            else
            {
                divPagingTop.Visible = false;
                divCommentsList.Visible = false;
                divPagingBottom.Visible = false;
            }
        }
        get
        {
            return totalComments;
        }
    }
    
    public int NextCount
    {
        set
        {
            if (value > 0)
            {
                lbtnNext.Enabled = true;
            }
            else
            {
                lbtnNext.Enabled = false;
            }
        }
    }
    public int PrevCount
    {
        set
        {
            if (value > 0)
            {
                lbtnPre.Enabled = true;
            }
            else
            {
                lbtnPre.Enabled = false;
            }
        }
    }
    public string SetRecordCount
    {
        set
        {
            spRecordCount.InnerText = value;
        }
    }
    public string NextPrev
    {
        get
        {
            return _nextPrev;
        }
    }
    public int UserId
    {
        get
        {
            return _userId;
        }
    }
    public int VideoId
    {
        get { return _videoId; }
        set
        {
            _videoId = value;
        }
    }
    
    #endregion

    #region METHODS
    /// <summary>
    /// Method to set values to labels in case of Edit Video.
    /// </summary>
    private void SetValuesToLabels()
    {
        lbtnPre.Text = ResourceText.GetString("lbtnPrevious_MV");
        lbtnNext.Text = ResourceText.GetString("lbtnNext_MV");
        lbtnPost.Text = ResourceText.GetString("lbtnPostComment_MV");
        spanPageTop.InnerText = ResourceText.GetString("lblPage_MV");
        spanPageBottom.InnerText = ResourceText.GetString("lblPage_MV");

        if (_videoType == "videotribute")
        {
            lblToComment.InnerText = ResourceText.GetString("lblCommentVideoTribute_MV");
            divLogin.InnerHtml = ResourceText.GetString("lblLoginVideoTribute_MV") + " " + "<a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>Log in</a>" + " " + ResourceText.GetString("lblOr_MV") + " " + "<a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>Sign up</a>";
        }
        else
        {
            lblToComment.InnerText = ResourceText.GetString("lblCommentHeader_MV");
            divLogin.InnerHtml = ResourceText.GetString("lblLoginMessage_MV") + " " + "<a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>Log in</a>" + " " + ResourceText.GetString("lblOr_MV") + " " + "<a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>Sign up</a>";
        }


        //Text for error messages from the resource file
        rfvMessage.ErrorMessage = ResourceText.GetString("errMessage_MV");
        cvMessage.ErrorMessage = ResourceText.GetString("errMessageLength_MV");
        string tributeHome;
        string videoUrl;
        if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
        {
            tributeHome = Session["APP_PATH"] + _tributeUrl;
        }
        else
        {
            tributeHome = "http://" + _tributeType.Replace("New Baby", "newbaby").ToLower() + "." +
                WebConfig.TopLevelDomain + "/" + _tributeUrl;
        }
        tributeHome += "/";
        videoUrl = tributeHome + "Video.aspx";

        string query_string = string.Empty;
        if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
        {
            query_string = "?TributeType=" + HttpUtility.UrlEncode(_tributeType);
            videoUrl = videoUrl + query_string;
            tributeHome = tributeHome + query_string;
        }
        videoUrl += (videoUrl.Contains("?") ? "&" : "?") + "mode=view&videoId=" + _videoId.ToString();

        aTributeHome.HRef = tributeHome;
        videoWallTributeHome.Text = tributeHome;
        videoWallTributeHome1.Text = tributeHome;


        videoWallPostSubject.Text = string.Format("{0} commented on a video on the: {1} {2} Tribute", _userName, _tributeName, _tributeType);
        videoWallLink.Text = videoUrl;
        videoWallLink1.Text = videoUrl;
        videoWallTributeType.Text = _tributeType;
        if (objVideoDetails != null)
        {
            videoWallThumbnail.Text = "http://img.youtube.com/vi/" + objVideoDetails[0].IdForDisplay + "/default.jpg";
        }
    }

    /// <summary>
    /// Method to get user is admin or owner
    /// </summary>
    private void UserIsAdminOrOwner(string mode)
    {
        UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
        StateManager objStateManager = StateManager.Instance;
        if (mode == "edit")
        {
            objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminOwnerInfo", StateManager.State.Session);
            objUserInfo.Mode = "edit";
        }
        else
        {
            objUserInfo.UserId = _userId;
            objUserInfo.TributeId = _tributeId; //TO DO: to be picked dynamically
            objUserInfo.TypeId = _videoId;
            if (_videoType == "videotribute")
                objUserInfo.TypeName = TYPE_NAME_TRIBUTE;
            else
                objUserInfo.TypeName = TYPE_NAME;
            objUserInfo.Mode = "view";
            bool isUserAdmin = IsUserAdmin(objUserInfo);
            bool isUserOwner = IsUserOwner(objUserInfo);

            if (isUserAdmin)
            {
                objUserInfo.IsAdmin = isUserAdmin;
                objUserInfo.IsOwner = false;
                isAdmin = true;
            }
            else if (isUserOwner)
            {
                objUserInfo.IsAdmin = false;
                objUserInfo.IsOwner = isUserOwner;
                isAdmin = false;
            }
        }
        objStateManager.Add("UserAdminOwnerInfo_ManageVideo", objUserInfo, StateManager.State.Session);
    }

    /// <summary>
    /// Method to set values to the comment object
    /// </summary>
    /// <param name="CurrentPage">Current page number</param>
    /// <returns>Filled CommentTributeAdministrator entity.</returns>
    public CommentTributeAdministrator SetCommentObject(int CurrentPage)
    {
        CommentTributeAdministrator objComAdmin = new CommentTributeAdministrator();
        objComAdmin.UserId = _userId;
        objComAdmin.TypeCodeName = typeCodeName;
        objComAdmin.CommentTypeId = _videoId;
        objComAdmin.TributeId = _tributeId;
        objComAdmin.CurrentPage = CurrentPage;
        objComAdmin.PageSize = pageSize;
        return objComAdmin;
    }

    /// <summary>
    /// Method to get the data from session for Logged in user and selected tribute details.
    /// </summary>
    private void GetValuesFromSession()
    {
        StateManager objStateManager = StateManager.Instance;

        //to get user id from session as user is logged in user
        objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionValue, null))
        {
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
        else if (Request.QueryString["mode"] != null || Request.QueryString["fbmode"] != null) //if user is coming through link
        {
            if (Request.QueryString["TributeId"] != null)
                _tributeId = int.Parse(Request.QueryString["TributeId"].ToString());

            if (Request.QueryString["TributeName"] != null)
                _tributeName = Request.QueryString["TributeName"].ToString();

            if (Request.QueryString["TributeType"] != null)
                _tributeType = Request.QueryString["TributeType"].ToString();

            if (Request.QueryString["TributeUrl"] != null)
                _tributeUrl = Request.QueryString["TributeUrl"].ToString();

        }
        else
        {
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
        }

        if (Session["TributeSession"] == null)
            CreateTributeSession(); //to create the tribute session values if user comest o this page from link or from favorites list.

        //to get page size from config file
        pageSize = (int.Parse(WebConfig.Pagesize_Videos_Comments));

        //to get current page number, if user clicks on page number in paging it gets tha page number from query string
        //else page number is 1
        if (Request.QueryString["PageNo"] != null)
            currentPage = int.Parse(Request.QueryString["PageNo"].ToString());
        else
            currentPage = 1;

        //to get video id from querystring
        if (Request.QueryString["videoId"] != null)
        {
            _videoId = int.Parse(Request.QueryString["videoId"].ToString());
            objStateManager.Add("VideoSession", _videoId, StateManager.State.Session);
        }
        else if (objStateManager.Get("VideoSession", StateManager.State.Session) != null)
        {
            _videoId = int.Parse(objStateManager.Get("VideoSession", StateManager.State.Session).ToString());
        }

        //to get value if full view is for video tribute
        if (Request.QueryString["videoType"] != null)
            _videoType = Request.QueryString["videoType"].ToString();
    }

    /// <summary>
    /// Method to create the tribute session values if user comes o this page from link or from favorites list.
    /// </summary>
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

    /// <summary>
    /// Method to return the filled comment object to save
    /// </summary>
    /// <returns>Filled Comments entity</returns>
    public Comments GetCommentDataToSave()
    {
        Comments objComment = new Comments();
        objComment.UserId = _userId;
        objComment.CodeTypeName = TYPE_NAME_TO_SAVE;
        objComment.ModuleFunctionalityName = MODULE_FUNCTIONALITY_NAME;
        objComment.TypeCodeId = typeCodeId;
        objComment.TributeId = _tributeId;
        objComment.CommentTypeId = _videoId;
        objComment.CreatedBy = _userId;
        objComment.CreatedDate = DateTime.Now;
        objComment.IsActive = true;
        objComment.IsDeleted = false;
        objComment.Message = txtVideoComment.Text.ToString().Trim();
        objComment.UserName = _userName;
        objComment.TributeName = _tributeName;
        objComment.TributeType = _tributeType;
        objComment.TributeUrl = _tributeUrl;
        objComment.PathToVisit = Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;
        return objComment;
    }

    /// <summary>
    /// Method to set the control visibility
    /// </summary>
    private void SetControlsVisibility()
    {
        if (!Equals(_userId, 0))
        {
            divPostComment.Visible = true;
            divLogin.Visible = false;
        }
        else
        {
            divPostComment.Visible = false;
            divLogin.Visible = true;
        }

        if (_videoType == "videotribute")
            divRecordCount.Visible = false;
    }
    #endregion

    //For TributeVideo
    public void CopyVideo(string OldTributeURL, string NewtributeURL, string tributeType)
    {

        string[] paths = CommonUtilities.GetVideoTributePath();
        string srcPath = paths[1] + OldTributeURL + "_" + tributeType;
        string destPath = paths[1] + NewtributeURL + "_" + tributeType;
        try
        {
            CopyOldURlFolderToNewURLFolder(srcPath, destPath);
        }
        catch (Exception a)
        {
            throw a;
        }
    }

    public void CopyOldURlFolderToNewURLFolder(string sourceFolder, string destFolder)
    {
        int CopyIteration = 0;
        int MaxCopyIteration = 0; //= 5;
        int.TryParse(WebConfig.VideoFileCopyIteration.ToString(), out MaxCopyIteration);
        if (Directory.Exists(sourceFolder))
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                CopyIteration = 0;
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);

                //LHK : Added check MaxCopyIteration times if Video Copied successfully.
                try
                {
                    while (CopyIteration < MaxCopyIteration)
                    {
                        if (!(File.Exists(dest)) && (File.Exists(file)))
                        {
                            File.Copy(file, dest);
                        }
                        else
                        {
                            CopyIteration = 5;
                        }
                        CopyIteration++;

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyOldURlFolderToNewURLFolder(folder, dest);
            }
        }
    }

}