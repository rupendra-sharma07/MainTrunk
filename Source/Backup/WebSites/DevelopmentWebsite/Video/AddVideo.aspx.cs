///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.Video.AddVideo.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to add videos(YouTube) to the selected tribute
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using TributesPortal.Video.Views;
#endregion

public partial class Video_AddVideo : PageBase, IAddVideo
{
    #region CLASS VARIABLES
    private AddVideoPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    private int _userId = 0;
    private int _tributeId;
    private string _tributeName;
    private string mode;
    private int _videoId;
    private bool isVideoTribute;
    private bool isUserAdmin;
    private bool isUserOwner;
    private string _userName;
    private string _tributeType;
    private string _tributeUrl;
    #endregion

    #region CONSTANTS
    private const string ADD_TYPE_NAME = "AddVideo";
    private const string EDIT_TYPE_NAME = "EditVideo";
    private const string MODULE_TYPE_NAME = "Video";
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.Now);
        GetSessionAndQSValues(); //to get the values from query string and session
        lbtnDeleteVideo.Attributes.Add("onclick", "if(confirm('" + ResourceText.GetString("msgDelete_NFV") + "')){}else{return false}");
        aTributeHome.HRef = Session["APP_PATH"] + _tributeUrl + "/";
        lbtnAddVideo.Attributes.Add("onclick", "hidesummery();");
        
        try
        {
            if (!this.IsPostBack)
            {
                //this._presenter.OnViewInitialized();
                if (!Equals(mode, string.Empty))
                {
                    if (Equals(mode, "edit"))
                    {
                        _presenter.GetVideoDetails();
                    }
                }
                Page.SetFocus(txtVideoName);
            }
            //this._presenter.OnViewLoaded();
            SetValuesToControls();
            UserIsAdminOrOwner(); //to set the visibility of options in side menu.
            SetControlsVisibilty();
            UserAccess();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnAddVideo_Click(object sender, EventArgs e)
    {
        try
        {
            //Check for valid data in Note Title
            string error = string.Empty;
            string errorVideoName = string.Empty;
            string errorVideoDesc = string.Empty;
            TributesPortal.ResourceAccess.IOVS.Sanitise(txtVideoName.Text, ref errorVideoName);
            TributesPortal.ResourceAccess.IOVS.Sanitise(txtVideoDesc.Text, ref errorVideoDesc);
            //txtVideoDesc
            if ((!string.IsNullOrEmpty(errorVideoName)) || (!string.IsNullOrEmpty(errorVideoDesc)))
            {
                //Display the error message
                if (!string.IsNullOrEmpty(errorVideoName))
                {
                    error = "Please enter valid characters in video name";
                    //revVideoNameSpecialchar.ErrorMessage = "Please enter valid characters in video name";
                }
                if (!string.IsNullOrEmpty(error))
                {
                    error = error + "<br>" + "Please enter valid characters in video description";

                }
                else
                    error = "Please enter valid characters in video description";

                //vsErrorSummary.inn
                //lblErrMsg.InnerHtml = ShowErrorMessage(error);
                //lblErrMsg.Visible = true;
                //spnMessage.Visible = true;

                revVideoNameSpecialchar.ErrorMessage = error;
                return;
            }
           


            if (Equals(mode, "edit")) //if user comes to this page for editing video
            {
                if (isVideoTribute) //if the edit mode is for Video Tribute
                {
                    object objReturn = new object();
                    objReturn = _presenter.UpdateVideoTributeDetails(SetObjectForVideoTribute());
                    if (objReturn == null)
                    Response.Redirect("~/" + Session["TributeURL"] + "/video.aspx?videoId=" + _videoId + "&mode=view&videoType=videotribute");
                else
                {
                    lblErrMsg.InnerHtml = SetHeaderMessage("Video name already exists, please try again.", vsErrorSummary.HeaderText);
                    lblErrMsg.Visible = true;
                }
                    //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.ManageVideo.ToString()) + "?videoId=" + _videoId + "&mode=view&videoType=videotribute");
                }
                else
                {
                    object objReturn = new object();
                    Videos objVideo = SetObject();
                    objReturn = _presenter.UpdateVideoDetails(objVideo);
                if(objReturn==null)
                    Response.Redirect("~/" + Session["TributeURL"] + "/video.aspx?videoId=" + _videoId);
                else
                {
                    lblErrMsg.InnerHtml = SetHeaderMessage("Video name already exists, please try again.", vsErrorSummary.HeaderText);
                    lblErrMsg.Visible = true;
                }
                    //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.ManageVideo.ToString()) + "?videoId=" + _videoId);
                }
            }
            else //this else part is for add video
            {
                Videos objVideo = SetObject();
                if (!_presenter.SaveVideo(objVideo))
                    if (facebook_share.Checked == true)
                    {
                        //may also need to add noteId as well.
                        Response.Redirect("~/" + Session["TributeURL"] + 
                            "/videos.aspx?post_on_facebook=" + facebook_share.Checked +
                            "&videoId=" + objVideo.VideoId.ToString(), false);
                    }
                    else
                    {
                        Response.Redirect("~/" + Session["TributeURL"] + "/videos.aspx", false);
                    }
                else
                {
                    lblErrMsg.InnerHtml = SetHeaderMessage("Video name already exists, please try again.", vsErrorSummary.HeaderText);
                    lblErrMsg.Visible = true;
                }
                //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.VideoGallery.ToString()), false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
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

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        if (Equals(mode, "edit")) //if user comes to this page for editing video
        {
            if (isVideoTribute) //if the edit mode is for Video Tribute
            {
                Response.Redirect("~/" + Session["TributeURL"] + "/video.aspx?videoId=" + _videoId + "&mode=view&videoType=videotribute");
                //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.ManageVideo.ToString()) + "?videoId=" + _videoId + "&mode=view&videoType=videotribute");
            }
            else
            {
                Response.Redirect("~/" + Session["TributeURL"] + "/video.aspx?videoId=" + _videoId);
                //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.ManageVideo.ToString()) + "?videoId=" + _videoId);
            }
        }
        else //this else part is for add video
        {
            Response.Redirect("~/" + Session["TributeURL"] + "/videos.aspx");
            //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.VideoGallery.ToString()));
        }
    }

    protected void lbtnDeleteVideo_Click(object sender, EventArgs e)
    {
        Videos objVideo = new Videos();
        objVideo.VideoId = _videoId;
        objVideo.UserId = _userId;
        objVideo.IsDeleted = true;

        _presenter.DeleteVideo(objVideo);
        Response.Redirect("~/" + Session["TributeURL"] + "/videos.aspx");
        //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.VideoGallery.ToString()));
    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public AddVideoPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    /// <summary>
    /// To bind the data to controls
    /// </summary>
    public VideoGallery VideoDetails
    {
        set
        {
            txtVideoName.Text = value.Videos.VideoCaption;
            txtVideoDesc.Text = value.Videos.VideoDesc;
            txtVideoId.Text = value.Videos.VideoTypeId.ToString();
            txtVideoUrl.Text = value.Videos.VideoUrl;
        }
        get
        {
            VideoGallery objVideoGal = new VideoGallery();
            Videos objVideo = new Videos();
            objVideo.VideoId = _videoId;
            objVideoGal.Videos = objVideo;
            return objVideoGal;
        }
    }

    /// <summary>
    /// To set the visibility based on the type of video. If Video is a VideoTribute, system hides the controls for
    /// VideoId & Video Url
    /// </summary>
    public bool IsVideoTribute
    {
        set
        {
            isVideoTribute = value;
            hAdd.Visible = !value;
            ulAddVideo.Visible = !value;
            divVideoUrl.Visible = !value;
            stgOr.Visible = !value;
            divVideoId.Visible = !value;

            ViewState["IsVideoTribute"] = value;
            //objStateManager.Add("IsVideoTribute", value, StateManager.State.ViewState);
        }
    }
    #endregion

    #region METHODS
    /// <summary>
    /// Function to set the values to Video Entity with the data entered 
    /// in the controls for video not for video tribute.
    /// </summary>
    /// <returns>Filled Video Entity</returns>
    private Videos SetObject()
    {
        Videos objVideo = new Videos();
        objVideo.UserTributeId = _tributeId;
        objVideo.UserId = _userId;
        objVideo.VideoCaption = txtVideoName.Text.Trim();
        objVideo.VideoDesc = txtVideoDesc.Text.Trim();
        objVideo.VideoUrl = txtVideoUrl.Text.Trim() != String.Empty ? txtVideoUrl.Text.Trim() : null;
        objVideo.VideoTypeId = txtVideoId.Text.Trim() != string.Empty ? txtVideoId.Text.Trim() : null;
        objVideo.TributeVideoId = null; //it is to be blank as we can't create any video tribute from this UI.
        objVideo.ModuleTypeName = MODULE_TYPE_NAME;
        if (mode == "edit") //for edit mode
        {
            objVideo.VideoId = _videoId;
            objVideo.CreatedBy = null;
            objVideo.CreatedDate = null;
            objVideo.ModifiedBy = _userId;
            objVideo.ModifiedDate = DateTime.Now;
        }
        else
        {
            objVideo.CreatedBy = _userId;
            objVideo.CreatedDate = DateTime.Now;
            objVideo.ModifiedBy = null;
            objVideo.ModifiedDate = null;
        }
        objVideo.IsActive = true;
        objVideo.IsDeleted = false;

        objVideo.UserName = _userName;
        objVideo.TributeName = _tributeName;
        objVideo.TributeType = _tributeType;
        objVideo.TributeUrl = _tributeUrl;
        objVideo.PathToVisit = "<a href='http://" + Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;
        return objVideo;
    }

    /// <summary>
    /// Method to set the Video Entity for VideoTribute.
    /// </summary>
    /// <returns>Filled Video Entity.</returns>
    private Videos SetObjectForVideoTribute()
    {
        Videos objVideo = new Videos();
        objVideo.VideoId = _videoId;
        objVideo.VideoCaption = txtVideoName.Text.Trim();
        objVideo.VideoDesc = txtVideoDesc.Text.Trim();
        objVideo.ModifiedBy = _userId;
        objVideo.ModifiedDate = DateTime.Now;

        return objVideo;
    }

    /// <summary>
    /// Function to set the values to labels from the resource file
    /// </summary>
    private void SetValuesToControls()
    {
        //Text for labels from the resource file
        spPageMode.InnerText = ResourceText.GetString("txtNavigator_AV");
        //hAddVideo.InnerText = ResourceText.GetString("lblAddVideo_AV");
        stgRequired.InnerHtml = ResourceText.GetString("lblRequired_AV") + "<em class='required'>* </em>";
        lblVideoName.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblVideoName_AV");
        lblVideoDescription.InnerText = ResourceText.GetString("lblVideoDesc_AV");
        //hAdd.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblUploadVideo_AV");
        liText1.InnerHtml = ResourceText.GetString("txtInfo1_AV") + "<a href='http://www.youtube.com/' target=\"_blank\">YouTube</a>.";
        liText2.InnerHtml = ResourceText.GetString("txtInfo2_AV");
        liText3.InnerHtml = ResourceText.GetString("txtInfo3_AV") + "<a href='http://help.youtube.com/support/youtube/bin/topic.py?topic=10524' target=\"_blank\">" + ResourceText.GetString("txtInfo3_1_AV") + "</a>.";
        liText4.InnerHtml = "<strong>" + ResourceText.GetString("txtInfo4_AV") + "</strong>";
        lblVideoUrl.InnerText = ResourceText.GetString("lblVideoUrl_AV");
        lblVideoId.InnerText = ResourceText.GetString("lblVideoId_AV");
        lbtnCancel.Text = ResourceText.GetString("btnCancel_AV");

        lbtnDeleteVideo.Text = ResourceText.GetString("btnDeleteVideo_AV");
        stgOr.InnerText = ResourceText.GetString("lblOr_AV");

        if (Equals(mode, "edit"))
        {
            lbtnAddVideo.Text = ResourceText.GetString("lbtnEditVideo_AV");
            spPageMode.InnerText = ResourceText.GetString("nvgEditVideo_AV");
            hAddVideo.InnerText = ResourceText.GetString("lblEditVideo_AV");
            hAdd.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblEditUploadVideo_AV");
        }
        else
        {
            lbtnAddVideo.Text = ResourceText.GetString("btnAddVideo_AV");
            spPageMode.InnerText = ResourceText.GetString("nvgAddVideo_AV");
            hAddVideo.InnerText = ResourceText.GetString("lblAddVideo_AV");
            hAdd.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblUploadVideo_AV");
        }

        if (isVideoTribute)
        {
            lbtnAddVideo.Text = ResourceText.GetString("lbtnSaveVideoTribute_AV");
            lbtnDeleteVideo.Text = ResourceText.GetString("lbtnDeleteVideoTribute_AV");
        }

        //Text for error messages from the resource file
        rfvVideoName.ErrorMessage = ResourceText.GetString("errVideoName_AV");
        cvYouTubeVideo.ErrorMessage = ResourceText.GetString("errVideoUrlAndId_AV");
        cvVideoDesc.ErrorMessage = ResourceText.GetString("errMaxLength_AV");
        reValVideoUrl.ErrorMessage = ResourceText.GetString("errVideoUrl_AV");

        revVideoNameSpecialchar.ErrorMessage =ResourceText.GetString("errVideoNameSpecialchar_AV");
        revVideoDescSpecialchar.ErrorMessage = ResourceText.GetString("errVideoDescSpecialchar_AV");
        revVideoIdSpecialchar.ErrorMessage = ResourceText.GetString("errVideoIdSpecialchar_AV");
    }

    /// <summary>
    /// Method to get user is admin or owner
    /// </summary>
    private void UserIsAdminOrOwner()
    {
        UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
        objUserInfo.UserId = _userId;
        objUserInfo.TributeId = _tributeId;
        objUserInfo.TypeName = "AddVideo";

        if (IsUserAdmin(objUserInfo))
            objUserInfo.IsAdmin = true;
        else
            objUserInfo.IsAdmin = false;

        isUserAdmin = objUserInfo.IsAdmin;

        //to check if user is qowner of video
        UserAdminOwnerInfo objUserOwner = new UserAdminOwnerInfo();
        objUserOwner.UserId = _userId;
        objUserOwner.TypeId = _videoId;
        objUserOwner.TributeId = _tributeId; //_userTributeId;
        objUserOwner.TypeName = "AddVideo";

        isUserOwner = IsUserOwner(objUserOwner);

        if (isUserOwner)
            objUserInfo.IsOwner = true;
        else
            objUserInfo.IsOwner = false;

        StateManager objStateManager = StateManager.Instance;
        objStateManager.Add("UserAdminOwnerInfo_AddVideo", objUserInfo, StateManager.State.Session);
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
            _userName = objSessionValue.FirstName == string.Empty ? objSessionValue.UserName : (objSessionValue.FirstName + " " + objSessionValue.LastName);
        }

        //to get tribute id and name from session
        objTribute = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);
        if (!Equals(objTribute, null))
        {
            _tributeId = objTribute.TributeId;
            _tributeName = objTribute.TributeName;
            _tributeType = objTribute.TypeDescription;
            _tributeUrl = objTribute.TributeUrl;
        }

        if (!Equals(Request.QueryString["mode"], null))
        {
            mode = Request.QueryString["mode"].ToString();
            if (objStateManager.Get("VideoSession", StateManager.State.Session) != null)
                _videoId = int.Parse(objStateManager.Get("VideoSession", StateManager.State.Session).ToString());
        }

        //to check if user is loggedin
        if (_userId == 0) //if user is not a logged in user redirect to login page
        {
            //Response.Redirect("log_in.aspx");
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
        }

        if (ViewState["IsVideoTribute"] != null)
            isVideoTribute = bool.Parse(ViewState["IsVideoTribute"].ToString());
        //isVideoTribute = bool.Parse(objStateManager.Get("IsVideoTribute", StateManager.State.ViewState).ToString());
    }

    /// <summary>
    /// Method to set visibility of controls
    /// </summary>
    private void SetControlsVisibilty()
    {
        if (Equals(mode, "edit"))
        {
            if (isVideoTribute)
            {
                if (isUserAdmin)
                    lbtnDeleteVideo.Visible = true;
                else
                    lbtnDeleteVideo.Visible = false;
                instructions.Visible = false;
                idinstructions.Visible = false;
            }
            else
            {
                lbtnDeleteVideo.Visible = true;
                instructions.Visible = true;
                idinstructions.Visible = true;
            }
        }
        else
        {
            lbtnDeleteVideo.Visible = false;
            instructions.Visible = true;
            idinstructions.Visible = true;
        }
    }

    /// <summary>
    /// Method to check accessibility for user
    /// </summary>
    private void UserAccess()
    {
        if (_userId != 0)
        {
            if (isVideoTribute) //if video is a VideoTribute
            {
                if (!isUserAdmin) //if user is admin only then user can edit a video tribute
                {
                    Response.Redirect("~/" + Session["TributeURL"] + "/videos.aspx");
                    //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.VideoGallery.ToString()));
                }
            }
            else //if video is not a video tribute
            {
                if (mode == "edit")
                    if (!isUserAdmin) //to edit user is to be Admin or Owner of the video
                        if (!isUserOwner)
                        {
                            Response.Redirect("~/" + Session["TributeURL"] + "/videos.aspx");
                            //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.VideoGallery.ToString()));
                        }
            }
        }
        else
        {
            //Response.Redirect("log_in.aspx");
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
        }
    }
    #endregion
}