///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.Video.VideoGallery.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page is displays the list of video's and the video 
///                  tribute added to the selected tribute 
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using TributesPortal.BusinessLogic;
using TributesPortal.Video.Views;
#endregion

public partial class Video_VideoGallery : PageBase, IVideoGallery
{
    #region CLASS VARIABLES
    private VideoGalleryPresenter _presenter;
    private int _userId = 0;
    private string _userName;
    private int pageSize;
    private int currentPage;
    private int _tributeId;
    private string _tributeName;
    private string _tributeType;
    private string _tributeUrl;
    protected bool _isActive;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    protected string txtComment;
    protected string videoTributeId;
    protected string videoTributeName;
    protected string videoTributeImage;
    protected string videoTributeCreatedOn;
    protected string videoTributeCreatedBy;
    protected string videoTributeComments;
    protected string strCreatedOn;
    protected string strCreatedBy;
    protected string CreatorId;
    private int _videoId;
    string tributeEndDate = string.Empty;
    string appDomian = string.Empty;
    int topHeight = 0;
    //AG:Addd for Expiry Notice
    private string _TributePackageType;
    private DateTime _endDate;
    private int _packageId = 0;
    #endregion   

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.Cache.SetExpires(DateTime.Now);
            GetSessionAndQSValues();
            topHeight = 165;
            appDomian = string.Empty;
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                appDomian = WebConfig.AppBaseDomain.ToString();
            }
            else
            {
                appDomian = "http://" + _tributeType.ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";
            }

            tributeEndDate = _presenter.GetTributeEndDate(_tributeId);

            //MG:Expiry Notice
            //AG(20-jan-11): Added topheight for expiry page
            bool isCustomeHeaderOn = _presenter.GetCustomHeaderDetail(_tributeId);
            if (Equals(objSessionValue, null))//when not logged in
            {
                if (isCustomeHeaderOn)
                    topHeight = 197;
                else
                    topHeight = 88;
            }
            else
            {
                if (isCustomeHeaderOn)
                    topHeight = 258;
                else
                    topHeight = 131;
            }
            
            

            //Start - Modification on 9-Dec-09 for the enhancement 3 of the Phase 1
            if (_tributeName != null) Page.Title = _tributeName + " | Videos";
            //End

            aTributeHome.HRef = Session["APP_PATH"] + _tributeUrl + "/";
            if (!this.IsPostBack)
            {
                _presenter.GetVideoTributeDetails(SetVideoTributeEntityObject());
                _presenter.GetVideosOfGallery(SetVideoEntityObject(currentPage));
                SetValuesToControls();
                VideoCreatioFacebookWallPost();
                //to get video id from querystring            
            }
            UserIsAdminOrOwner(); //to set the visibility of options in side menu.

            if (!tributeEndDate.Equals("Never"))
            {
                string[] date = tributeEndDate.Split('/');
                DateTime date2 = new DateTime(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1]));
                if ((date2 < DateTime.Now) && (_packageId == 3))
                {
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "fnExpiryNoticePopupClose();", true);

                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "fnExpiryNoticePopup('location.href','document.title','NonMemo','" + _tributeId + "','" + appDomian + "','" + topHeight + "');", true);

                }
            }

        }
        catch (Exception ex)
        {
            Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/Error404.aspx");
        }
    }

    protected void VideoCreatioFacebookWallPost()
    {
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
        aTributeHome.HRef = tributeHome;

        StateManager objStateManager = StateManager.Instance;
        if (Request.QueryString["post_on_facebook"] != null && 
            Request.QueryString["post_on_facebook"].ToString().Equals("True"))
        {
            if (Request.QueryString["videoId"] != null)
            {
                _videoId = int.Parse(Request.QueryString["videoId"].ToString());
                objStateManager.Add("VideoSession", _videoId, StateManager.State.Session);
            }
            else if (objStateManager.Get("VideoSession", StateManager.State.Session) != null)
            {
                _videoId = int.Parse(objStateManager.Get("VideoSession", StateManager.State.Session).ToString());
            }
            Videos objVideo = new Videos();
            objVideo.VideoId = _videoId;
            objVideo.UserId = _userId;
            VideoGallery objVideoGallery = new VideoGallery();
            objVideoGallery.Videos = objVideo;//this.View.UserTributeId
            VideoManager mgr = new VideoManager();
            objVideoGallery = mgr.GetVideoDetails(objVideoGallery);

            videoUrl += (videoUrl.Contains("?") ? "&" : "?") + "mode=view&videoId=" + _videoId.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">\n");
            sb.Append("$(document).addEvent('fb_connected', function() {\n");
            sb.Append("    var attachments = {\n");
            sb.Append("        name: '");
            sb.Append(string.Format("{0} added a video on the: {1} {2} Tribute", _userName, _tributeName, _tributeType));
            sb.Append("',\n");
            sb.Append("        href: '");
            sb.Append(videoUrl);
            sb.Append("',\n");
            sb.Append("        caption: '<b>Website:</b> ");
            sb.Append(tributeHome);
            sb.Append("',\n");
            sb.Append("        description: '<b>Video:</b> ");
            sb.Append(objVideoGallery.Videos.VideoCaption);
            sb.Append("',\n");
            sb.Append("        media: [{\n");
            sb.Append("          type: 'image',\n");
            sb.Append("          src:'");
            sb.Append(string.Format("http://img.youtube.com/vi/{0}/default.jpg", objVideoGallery.IdForDisplay));
            sb.Append("',\n");
            sb.Append("          href: '");
            sb.Append(videoUrl);
            sb.Append("'\n");
            sb.Append("               }]\n");
            sb.Append("    };\n");
            sb.Append("    var action_link = [{\n");
            sb.Append("        text: '");
            sb.Append(string.Format("Visit {0} Tribute", _tributeType));
            sb.Append("',\n");
            sb.Append("        href: '");
            sb.Append(tributeHome);
            sb.Append("'\n");
            sb.Append("    }]\n");
            sb.Append("    publish_stream('', attachments, action_link, null, null, function() {});");
            sb.Append("});\n");
            sb.Append("</script>");

            ClientScript.RegisterStartupScript(GetType(), "facebook_wall_post", sb.ToString());
        }
    }

    protected void lbtnAddVideo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/" + Session["TributeURL"] + "/addvideo.aspx", false);
    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public VideoGalleryPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    public int TotalRecords
    {
        set
        {
            if (value == 0)
            {
                divPagingTop.Visible = false;
                divPagingBottom.Visible = false;
                divNoRecord.Visible = true;
                if (_userId == 0)
                    divAddVideo.Visible = false;
                else
                    divAddVideo.Visible = true;
            }
            else
            {
                divNoRecord.Visible = false;
                divAddVideo.Visible = false;
            }
        }
    }

    public string RecordCount
    {
        set
        {
            spRecordCountTop.InnerText = value;
            spRecordCountBottom.InnerText = value;
        }
    }

    public string DrawPaging
    {
        set
        {
            spPageTop.InnerText = ResourceText.GetString("lblPage_VG");
            spPageBottom.InnerText = ResourceText.GetString("lblPage_VG");
            spPagingTop.InnerHtml = value;
            spPagingBottom.InnerHtml = value;
        }
    }

    /// <summary>
    /// to bind the data list control to display the videos in the video gallery.
    /// </summary>
    public IList<VideoGallery> VideoGalleryVideos
    {
        set
        {
            dlVideos.DataSource = value;
            dlVideos.DataBind();
        }
    }

    public VideoGallery VideoTributeDetails
    {
        set
        {
            if (!Equals(value, null))
            {
                string[] path = CommonUtilities.GetVideoTributePath();
                hVideoTribute.Visible = true;
                divVideoTribute.Visible = true;
                hrLine.Visible = true;
                videoTributeId = value.Videos.VideoId.ToString();
                videoTributeName = value.Videos.VideoCaption;
                videoTributeImage = path[2] + "/" + _tributeUrl + "_" + _tributeType.Replace(" ","_") + "/" + value.Videos.TributeVideoId + "_big.jpg";  //value.IdForDisplay;
                videoTributeCreatedBy = value.UserName;
                videoTributeCreatedOn = value.CreatedDate;
                videoTributeComments = value.CommentCount.ToString();
                CreatorId = value.Videos.UserId.ToString();
            }
            else
            {
                hVideoTribute.Visible = false;
                divVideoTribute.Visible = false;
                hrLine.Visible = false;
            }
        }
    }

    public int PackageId
    {
        set { _packageId = value; }
    }
    #endregion

    #region METHODS
    /// <summary>
    /// Function to set the values to labels from the resource file
    /// </summary>
    private void SetValuesToControls()
    {
        hVideo.InnerText = ResourceText.GetString("lblVideoHeader_VG");
        hVideoTribute.InnerText = ResourceText.GetString("lblVideoTribute_VG");
        hVideos.InnerText = ResourceText.GetString("lblVideo_VG");
        divNoRecord.InnerText = ResourceText.GetString("lblNoVideo_VG");
        txtComment = ResourceText.GetString("txtComments_VG");
        strCreatedBy = ResourceText.GetString("txtCreatedBy_VG");
        strCreatedOn = ResourceText.GetString("txtCreatedOn_VG");
        lbtnAddVideo.Text = ResourceText.GetString("btnAddVideo_VG");
        if (_userId == 0)
        {
            divLogin.Visible = true;
            divLogin.InnerHtml = ResourceText.GetString("lblLogin_VG") + " " + "<a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>Log in</a>" + " " + ResourceText.GetString("lblOr_VG") + " " + "<a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>Sign up</a>";
        }
        else
        {
            divLogin.Visible = false;
        }

    }

    /// <summary>
    /// Method to set the User tribute id to get the list of Videos for tribute
    /// </summary>
    /// <returns>Filled Video Gallery entity with UserTributeId</returns>
    private VideoGallery SetVideoEntityObject(int currentPage)
    {
        VideoGallery objVideoGallery = new VideoGallery();
        Videos objVideo = new Videos();
        objVideo.UserTributeId = _tributeId; // _userTributeId;
        objVideoGallery.Videos = objVideo; //TO DO: to be picked dynamically
        objVideoGallery.PageNumber = currentPage;
        objVideoGallery.PageSize = pageSize;
        return objVideoGallery;
    }

    /// <summary>
    /// Method to get the video entity to get the video tribute
    /// </summary>
    /// <returns>Video entity containing tributeid</returns>
    private Videos SetVideoTributeEntityObject()
    {
        Videos objVideo = new Videos();
        objVideo.UserTributeId = _tributeId;
        return objVideo;
    }

    /// <summary>
    /// Method to get user is admin or owner
    /// </summary>
    private void UserIsAdminOrOwner()
    {
        UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
        objUserInfo.UserId = _userId;
        objUserInfo.TributeId = _tributeId; // _userTributeId;
        objUserInfo.TypeName = "VideoGallery";

        if (IsUserAdmin(objUserInfo))
            objUserInfo.IsAdmin = true;
        else
            objUserInfo.IsAdmin = false;

        StateManager objStateManager = StateManager.Instance;
        objStateManager.Add("UserAdminOwnerInfo_VideoGallery", objUserInfo, StateManager.State.Session);
    }

    /// <summary>
    /// Method to get values from querystring and session variables
    /// </summary>
    private void GetSessionAndQSValues()
    {
        StateManager objStateManager = StateManager.Instance;
        //to get user id from session as user is logged in user
        objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionValue, null))
        {
            _userId = objSessionValue.UserId;
            _userName = objSessionValue.FirstName + " " + objSessionValue.LastName;
        }

        //to get tribute id and name from session
        objTribute = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);
        if (Request.QueryString["mode"] != null || Request.QueryString["fbmode"] != null) //if user is coming through link
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
        else if (!Equals(objTribute, null))
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
        else
        {
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
        }

        if (Session["TributeSession"] == null)
            CreateTributeSession(); //to create the tribute session values if user comest o this page from link or from favorites list.
                
        //to get page size from config file
        pageSize = (int.Parse(WebConfig.Pagesize_VideoGallery));

        //to get current page number, if user clicks on page number in paging it gets tha page number from query string
        //else page number is 1
        if (Request.QueryString["PageNo"] != null)
            currentPage = int.Parse(Request.QueryString["PageNo"].ToString());
        else
            currentPage = 1;
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
    #endregion
}