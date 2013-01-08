///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Photo.PhotoGallery.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the list of the photo albums created by the user
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
using TributesPortal.Photo.Views;
using TributesPortal.Utilities;
using TributesPortal.BusinessLogic;
using System.IO;
#endregion

public partial class Photo_PhotoGallery : PageBase, IPhotoGallery
{
    #region CLASS VARIABLES
    private PhotoGalleryPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    private int _userId;
    private string _userName;
    private int _tributeId;
    protected string _tributeName;
    protected string _tributeType;
    private string _tributeUrl;
    private DateTime _endDate;
    private int currentPage;
    private int pageSize;
    private bool isAdmin;
    protected bool _isActive;
    private int _photoAlbumID;
    //AG:Addd for Expiry Notice
    private string _TributePackageType;

    string tributeEndDate = string.Empty;
    string appDomian = string.Empty;
    int topHeight = 0;

    protected string strCommentsCount = ResourceText.GetString("lblComments_PG");
    protected string strPhotoAlbumHeader = ResourceText.GetString("lblPhotoAlbumHeader_PG");
    protected string strPhotosCount = ResourceText.GetString("lblPhotos_PG");
    protected string strCreated = ResourceText.GetString("lblCreated_PG");
    protected string strUpdated = ResourceText.GetString("lblUpdated_PG");
    protected string strCreatedBy = ResourceText.GetString("lblCreatedBy_PG");
    #endregion
    protected void page_Prerender(object sender, EventArgs e)
    {
     
    }

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {

                // Added by rupendra on June 20, 2011 to redirect on album aftwer updating the page
                int photoAlbumId = 0;
                string url;
                if ((Request.QueryString["TributeUrl"] != null))
                {
                    _tributeUrl = Request.QueryString["TributeUrl"].ToString();
                    StateManager stateManager = StateManager.Instance;
                    Tributes objTrib = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
                    if (objTrib != null)
                    {
                        if (objTrib.TributeId <= 0 && Session["PhotoAlbumTributeSession"] != null)
                        {
                            objTrib = Session["PhotoAlbumTributeSession"] as Tributes;
                        }
                    }
                    else if (Session["PhotoAlbumTributeSession"] != null)
                    {
                        objTrib = Session["PhotoAlbumTributeSession"] as Tributes;
                    }
                    if (objTrib != null)
                    {
                        if (Session["PhotoAlbumID2"] != null && objTrib.TributeUrl != null && !string.IsNullOrEmpty(objTrib.TributeUrl.ToString()))
                        {
                            int.TryParse(Session["PhotoAlbumID2"].ToString(), out photoAlbumId);
                            Session.Remove("PhotoAlbumID2");
                            Session["PhotoAlbumID2"] = null;
                            if (photoAlbumId > 0)
                            {
                                Session.Remove("PhotoAlbumID2");
                                Tributes objTrb = new Tributes();
                                if (WebConfig.ApplicationMode.Equals("local"))
                                {
                                    url = WebConfig.AppBaseDomain + objTrib.TributeUrl.ToString() + "/photoalbum.aspx?PhotoAlbumID=" + photoAlbumId.ToString();
                                }
                                else
                                {
                                    url = "http://" + objTrib.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrib.TributeUrl.ToString() + "/photoalbum.aspx?PhotoAlbumID=" + photoAlbumId.ToString();


                                }
                                Response.Redirect(url);
                            }
                        }
                    }
                }

                //making session null not to get these values while creating
                HttpContext.Current.Session["AlbumName"] = null;
                HttpContext.Current.Session["AlbumDesc"] = null;
            }
            try
            {
                //to get values from session and querystring.
                GetValuesFromSession();

                appDomian = string.Empty;
                if (WebConfig.ApplicationMode.Equals("local"))
                {
                    appDomian = WebConfig.AppBaseDomain.ToString();
                }
                else
                {
                    appDomian = "http://" + _tributeType.ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";
                }

                //Start - Modification on 9-Dec-09 for the enhancement 3 of the Phase 1
                if (_tributeName != null) Page.Title = _tributeName + " | Photos";
                //End

                UserIsAdmin();
                if (_userId == 0)
                {
                    divLogin.Visible = true;
                    divLogin.InnerHtml = ResourceText.GetString("strLoginMsg_PG") + " " + "<a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>" + ResourceText.GetString("strLogin_PG") + "</a>" + " " + ResourceText.GetString("strOr_PG") + " " + "<a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>" + ResourceText.GetString("strSignUp_PG") + "</a>";
                }
                else
                {
                    divLogin.Visible = false;
                }
                topHeight = 165;
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
                tributeEndDate = _presenter.GetTributeEndDate(_tributeId);
                //MG:Expiry Notice
                DateTime dt = new DateTime();
                if (!tributeEndDate.Equals("Never"))
                {
                    if (tributeEndDate.Contains("/"))
                    {
                        string[] date = tributeEndDate.Split('/');
                        DateTime date2 = new DateTime(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1]));
                        if (date2 < DateTime.Now)
                        {
                            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "fnExpiryNoticePopupClose();", true);

                            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "fnExpiryNoticePopup('location.href','document.title','NonMemo','" + _tributeId + "','" + appDomian + "','" + topHeight + "');", true);

                        }
                    }
                }

                if (!this.IsPostBack)
                {
                    AlbumPhotoAdditionFacebookWallPost();
                    ClearManagePhotoSession();
                    StateManager stateManager = StateManager.Instance;
                    Tributes objTrib = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);

                    if (Session["Result_photo_upload"] != null && int.Parse(Session["Result_photo_upload"].ToString()) < 0)
                    {

                        int result = int.Parse(Session["Result_photo_upload"].ToString());
                        string redirect = string.Empty;
                        if (WebConfig.ApplicationMode.Equals("local"))
                            redirect = "~/";
                        else
                            redirect = "http://" + _tributeType.ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";

                        redirect += Session["TributeURL"] + "/managephotoalbum.aspx" + "?result=" + int.Parse(Session["Result_photo_upload"].ToString()) + "&photoAlbumId=" + _photoAlbumID.ToString();
                        if (result == -6)
                            redirect += "&mode=addphotos";

                        Response.Redirect(redirect, false);

                        //lblErrMsg.InnerHtml = "Please provide valid data.";
                        //Session["Result_photo_upload"] = null;
                    }
                    else
                        this._presenter.GetPhotoGallery(GetPhotoAlbumObject());
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex.StackTrace);
                Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/404Error.htm");
                // throw ex;
            }
        }
        catch (Exception ex)
        {
            LogError(ex.Message, ex.StackTrace);
           // throw ex;
            Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/404Error.htm");
        }
    }
    private void LogError(string errorMessage, string stackTrace)
    {
        string logPath = Server.MapPath("~/Logs/" + DateTime.Today.ToString("MM-dd-yy"));

        // Create the subfolder
        DirectoryInfo objDir = new DirectoryInfo(logPath);
        if (!(objDir.Exists))
            objDir.Create();

        // Combine the new file name with the path
        logPath = System.IO.Path.Combine(logPath, "PhotoAlbumError.log");

        // random file names.

        using (System.IO.StreamWriter w = System.IO.File.AppendText(logPath))
        {
            w.WriteLine("{0} Error: {1}",
                DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture),
                errorMessage);
            w.WriteLine(stackTrace);
            w.Flush();
            w.Close();
        }

    }

    protected void AlbumPhotoAdditionFacebookWallPost()
    {
        string tributeHome;
        string photoAlbumUrl;
        string photoUrlBase;
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
        photoAlbumUrl = tributeHome + "photoalbum.aspx";
        photoUrlBase = tributeHome + "photo.aspx";

        string query_string = string.Empty;
        if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
        {
            query_string = "?TributeType=" + HttpUtility.UrlEncode(_tributeType);
            photoAlbumUrl = photoAlbumUrl + query_string;
            tributeHome = tributeHome + query_string;
            photoUrlBase = photoUrlBase + query_string;
        }
        aTributeHome.HRef = tributeHome;

        StateManager objStateManager = StateManager.Instance;
        if (Request.QueryString["post_on_facebook"] != null)
        {
            if (Request.QueryString["post_on_facebook"].ToString().Equals("True"))
            {
                int _newPhotoAlbumID = _photoAlbumID;
                if (objStateManager.Get("NewPhotoAlbumId", StateManager.State.Session) != null)
                {
                    _newPhotoAlbumID = int.Parse(objStateManager.Get("NewPhotoAlbumId", StateManager.State.Session).ToString());
                }
                Photos objPhoto = new Photos();
                objPhoto.PhotoAlbumId = _newPhotoAlbumID;
                objPhoto.PageNumber = 1;
                objPhoto.PageSize = 3;
                objPhoto.SortOrder = "desc";

            PhotoAlbum objAlbumDetails = null;
            PhotoManager mgr = new PhotoManager();
            objAlbumDetails = mgr.GetPhotoAlbumDetail(objPhoto);

            string[] getPath = CommonUtilities.GetPath();
            List<Photos> objPhotoList = mgr.GetPhotos(objPhoto);

            photoAlbumUrl += (photoAlbumUrl.Contains("?") ? "&" : "?") + "PhotoAlbumId=" + _newPhotoAlbumID.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">\n");
            sb.Append("$(document).addEvent('fb_connected', function() {\n");
            sb.Append("    var attachments = {\n");
            sb.Append("        name: '");
            sb.Append(string.Format("{0} added photos to the: {1} {2} Tribute", _userName, _tributeName, _tributeType));
            sb.Append("',\n");
            sb.Append("        href: '");
            sb.Append(photoAlbumUrl);
            sb.Append("',\n");
            sb.Append("        caption: '<b>Website:</b> ");
            sb.Append(tributeHome);
            sb.Append("',\n");
            sb.Append("        description: '<b>Photo Album:</b> ");
            sb.Append(objAlbumDetails.PhotoAlbumCaption);
            sb.Append("',\n");
            sb.Append("        media: [\n");
            foreach (Photos obj in objPhotoList)
            {
                if (_tributeType != null)
                {
                    // in my Common\\XML\\PhotoConfiguration.xml file getPath[2] already ends with '/'
                    obj.PhotoImage = getPath[2] + getPath[3] + "/" + _tributeUrl.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_") + "/" + obj.PhotoImage;

                    string photoUrl = photoUrlBase + (photoUrlBase.Contains("?") ? "&" : "?") +
                        "PhotoId=" + obj.PhotoId.ToString();

                    sb.Append("{type: 'image', href: '");
                    sb.Append(photoUrl);
                    sb.Append("', src: '");
                    sb.Append(obj.PhotoImage);
                    sb.Append("'}");
                    sb.Append(",");
                }
            }
            if (objPhotoList.Count > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("               ]\n");
            sb.Append("    };\n");
            sb.Append("    var action_link = [{\n");
            sb.Append("        text: '");
            sb.Append(string.Format("Visit {0} Tribute", _tributeType));
            sb.Append("',\n");
            sb.Append("        href: '");
            sb.Append(photoAlbumUrl);
            sb.Append("'\n");
            sb.Append("    }]\n");
            sb.Append("    publish_stream('', attachments, action_link, null, null, function() {});");
            sb.Append("});\n");
            sb.Append("</script>");

                ClientScript.RegisterStartupScript(GetType(), "facebook_wall_post", sb.ToString());
            }
        }
    }

    protected void lbtnAddPhoto_Click(object sender, EventArgs e)
    {
        try
        {
            if ((Master._packageId == 6) || (Master._packageId == 7))
            {
                int CurrentAlbums = _presenter.GetCurrentPhotoAlbums(_tributeId);
                if (CurrentAlbums >= (int.Parse(WebConfig.PhotoAlbumLimit)))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "fnReachLimitExpiryPopup('location.href','document.title','Photos','" + _tributeUrl + "','" + _tributeId + "','" + appDomian + "','" + topHeight + "');", true);
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "ReachLimitModalPopup('location.href','document.title'" + "');", true);
                }
                else
                {
                    StateManager stateManager = StateManager.Instance;
                    Tributes objTrb = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
                    if (objTrb != null)
                    {
                        if (WebConfig.ApplicationMode.Equals("local"))
                        {
                            Response.Redirect("~/" + Session["TributeURL"] + "/managephotoalbum.aspx?albummode=Create&Type=" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") , false);

                            //appDomian = WebConfig.AppBaseDomain.ToString();
                        }
                        else
                        {
                            appDomian = "http://www." + WebConfig.TopLevelDomain + "/" + Session["TributeURL"] + "/managephotoalbum.aspx?albummode=Create&Type=" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby");
                            Response.Redirect(appDomian, false);
                        }

                        //Response.Redirect("~/" + Session["TributeURL"] + "/managephotoalbum.aspx", false);
                        //Redirect.RedirectToPage(Redirect.PageList.ManagePhotoAlbum.ToString())
                    }
                }
            }
            else
            {
                StateManager stateManager = StateManager.Instance;
                Tributes objTrb = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
                if (objTrb != null)
                {
                    if (WebConfig.ApplicationMode.Equals("local"))
                    {
                        Response.Redirect("~/" + Session["TributeURL"] + "/managephotoalbum.aspx?albummode=Create&Type=" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby"), false);

                        //appDomian = WebConfig.AppBaseDomain.ToString();
                    }
                    else
                    {
                        appDomian = "http://www." + WebConfig.TopLevelDomain + "/" + Session["TributeURL"] + "/managephotoalbum.aspx?albummode=Create&Type=" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby");
                        Response.Redirect(appDomian, false);
                    }
                    // Response.Redirect("~/" + Session["TributeURL"] + "/managephotoalbum.aspx", false);
                    //Redirect.RedirectToPage(Redirect.PageList.ManagePhotoAlbum.ToString())
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public PhotoGalleryPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    public List<PhotoAlbum> PhotoAlbumList
    {
        set
        {
            repPhotoAlbumList.DataSource = value;
            repPhotoAlbumList.DataBind();
        }
    }
    public int TotalRecords
    {
        set
        {
            if (value > 0)
            {
                divPagingTop.Visible = true;
                divPagingBottom.Visible = true;
                spPageTop.InnerText = ResourceText.GetString("lblPage_PG");
                spPageBottom.InnerText = ResourceText.GetString("lblPage_PG");
                pClick.Visible = false;
                pNoRecord.Visible = false;
                lbtnAddPhoto.Visible = false;
                divButton.Visible = false;
                divNoAlbum.Visible = false;
            }
            else
            {
                pNoRecord.Visible = true;
                if (_userId != 0)
                {
                    pClick.Visible = true;
                    lbtnAddPhoto.Visible = true;
                    divButton.Visible = true;
                }
                else
                {
                    pClick.Visible = false;
                    lbtnAddPhoto.Visible = false;
                    divButton.Visible = false;
                }
                divPagingTop.Visible = false;
                divPagingBottom.Visible = false;
                pNoRecord.InnerText = ResourceText.GetString("strNoAlbumMessage_PG");
                pClick.InnerText = ResourceText.GetString("txtClick_PG");
                lbtnAddPhoto.Text = ResourceText.GetString("lbtnCreateAlbum_PG");
                divNoAlbum.Visible = true;
            }
        }
    }
    public string DrawPaging
    {
        set
        {
            spPagingTop.InnerHtml = value;
            spPagingBottom.InnerHtml = value;
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
    public string TributeName
    {
        get
        {
            return _tributeName;
        }
    }
    public string TributeType
    {
        get
        {
            return _tributeType;

        }
    }
    public string TributeUrl
    {
        get
        {
            return _tributeUrl;
        }
    }
    #endregion

    #region METHODS
    /// <summary>
    /// Method to fill the PhotoAlbum object to get the list of photo albums
    /// </summary>
    /// <returns>Filled PhotoAlbum entity</returns>
    private PhotoAlbum GetPhotoAlbumObject()
    {
        PhotoAlbum objAlbum = new PhotoAlbum();
        objAlbum.UserTributeId = _tributeId;
        objAlbum.PageNumber = currentPage;
        objAlbum.PageSize = pageSize;

        return objAlbum;
    }

    /// <summary>
    /// Method to get the values from session and query string.
    /// </summary>
    private void GetValuesFromSession()
    {
        StateManager objStateManager = StateManager.Instance;
        //to get user id from session as user is logged in user
        objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionValue, null))
        {
            _userId = objSessionValue.UserId;
            _userName = objSessionValue.FirstName + " " + objSessionValue.LastName;
        }

        object albumSession = objStateManager.Get("PhotoAlbumId", StateManager.State.Session);
        if (!Equals(albumSession, null))
            _photoAlbumID = int.Parse(objStateManager.Get("PhotoAlbumId", StateManager.State.Session).ToString());

        objTribute = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);
        if (objTribute != null)
        {
            if (objTribute.TributeId <= 0 && Session["PhotoAlbumTributeSession"] != null)
            {
                objTribute = Session["PhotoAlbumTributeSession"] as Tributes;
            }
        }
        else if (Session["PhotoAlbumTributeSession"] != null)
        {
            objTribute = Session["PhotoAlbumTributeSession"] as Tributes;
        }
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
            //CreateTributeSession(); //to create the tribute session values if user comes o this page from link or from favorites list.
        }
        else if (!Equals(objTribute, null))
        {
            if (objTribute.TributeId > 0)
            {
                _tributeId = objTribute.TributeId;
                _tributeName = objTribute.TributeName;
                _tributeType = objTribute.TypeDescription;
                _tributeUrl = objTribute.TributeUrl;
                _isActive = objTribute.IsActive;
                _TributePackageType = objTribute.TributePackageType;
            }
            if (!objTribute.Date2.Equals(null))
            {
                _endDate = (DateTime)objTribute.Date2;
            }
          
        }
        else
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);

        if (Session["TributeSession"] == null)
            CreateTributeSession(); //to create the tribute session values if user comest o this page from link or from favorites list.

        //to get page size from config file
        pageSize = (int.Parse(WebConfig.Pagesize_PhotoGallery));

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

    /// <summary>
    /// Method to get user is admin or owner
    /// </summary>
    private bool UserIsAdmin()
    {
        UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
        objUserInfo.UserId = _userId;
        objUserInfo.TributeId = _tributeId;
        objUserInfo.TypeName = "PhotoGallery";

        if (_userId != 0)
        {
            if (IsUserAdmin(objUserInfo))
            {
                objUserInfo.IsAdmin = true;
                isAdmin = true;
            }
        }
        else
        {
            objUserInfo.IsAdmin = false;
            isAdmin = false;
        }
        StateManager objStateManager = StateManager.Instance;
        objStateManager.Add("UserAdminInfo_PhotoGallery", objUserInfo, StateManager.State.Session);
        return isAdmin;
    }


    //clear the session variables that might have been set in ManagePhoto
    private void ClearManagePhotoSession()
    {
        Session["PhotoCount"] = null;
        Session["NewPhotoCount"] = null;
        Session["NewPhotoID"] = null;
        StateManager objStateManager = StateManager.Instance;
        objStateManager.Remove("NewPhotoAlbumId", StateManager.State.Session);
    }
    #endregion

}