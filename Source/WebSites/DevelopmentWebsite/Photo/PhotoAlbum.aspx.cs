///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Photo.PhotoAlbum.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the list of photos that are a part of the selected photo album
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Web.UI;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Photo.Views;
using TributesPortal.Utilities;
using System.Text.RegularExpressions;
using TributesPortal.Miscellaneous;
#endregion

public partial class Photo_PhotoAlbum : PageBase, IPhotoAlbum
{
    #region CLASS VARIABLES
    private PhotoAlbumPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    private int photoAlbumId;
    private int _userId;
    //private int _count;
    private int _tributeId;
    protected string _tributeName;
    private string _tributeType;
    private string _tributeUrl;
    private int currentPage;
    private int pageSize;
    private bool isAdmin;
    protected bool _isActive;
    private string _TributePackageType;

    protected string photoAlbumCaption = string.Empty;
    protected string strPhotoHeader = ResourceText.GetString("lblPhotos_PA");
    protected string strCommentsCount = ResourceText.GetString("lblComments_PA");
    private DateTime _endDate;
    public int _packageId = 8;
    string appDomian = string.Empty;
    int topHeight;
    bool IsCustomHeaderOn = false;
    public int DownloadPhotoAlbumId = 0;

    string url = string.Empty;
    //protected string _xmlFilePath = string.Empty;
    #endregion

    #region CONSTANTS
    private const string TYPE_NAME = "PhotoAlbum";
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetValuesFromSession();
            topHeight = 165;

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

            if (_tributeName != null)
                Page.Title = _tributeName + " | Photo Album";
            //End

            UserIsAdminOrOwner();
            aTributeHome.HRef = Session["APP_PATH"] + _tributeUrl + "/";

            string tributeEndDate = _presenter.GetTributeEndDate(_tributeId);

            if (Request.QueryString["PhotoAlbumId"] != null)
            {
                int.TryParse(Request.QueryString["PhotoAlbumId"], out photoAlbumId);
                if (photoAlbumId > 0)
                    Session["PhotoAlbumId"] = photoAlbumId.ToString();
            }

            //MG:Expiry Notice
            //AG(20-jan-11): Added topheight for expiry page
            bool isCustomeHeaderOn = _presenter.GetCustomHeaderDetail(_tributeId);
            if (Equals(objSessionValue, null))//when not logged in
            {
                if (isCustomeHeaderOn)
                    topHeight = 197;
                else
                    topHeight = 91;
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
                this._presenter.GetPhotoAlbumData(GetPhotoObject());
            }
            bool isAllowedPhotoCheck = false;
            MiscellaneousController objMisc = new MiscellaneousController();
            DateTime date2 = new DateTime();
            //MG:Expiry Notice
            DateTime dt = new DateTime();
            if (!tributeEndDate.Equals("Never"))
            {
                if (tributeEndDate.Contains("/"))
                {
                    string[] date = tributeEndDate.Split('/');
                    date2 = new DateTime(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1]));

                }
            }

            _packageId = _presenter.GetPackIdonPhotoAlbumId(photoAlbumId);

            isAllowedPhotoCheck = objMisc.IsAllowedPhotoCheck(photoAlbumId);

            if (((_packageId == 5) && !isAllowedPhotoCheck && (date2 < DateTime.Now)))
            {
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "fnExpiryNoticePopupClose();", true);

                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "fnExpiryNoticePopup('location.href','document.title','NonMemo','" + _tributeId + "','" + appDomian + "','" + topHeight + "');", true);

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "fnReachLimitExpiryPopup('location.href','document.title','UpgradeAlbum','" + _tributeUrl + "','" + _tributeId + "','" + appDomian + "','" + topHeight + "');", true);
            }

        }
        catch (Exception ex)
        {
            Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/Error404.aspx");
        }
    }
    protected void lbtnCreateAlbum_Click(object sender, EventArgs e)
    {
        try
        {
             StateManager stateManager = StateManager.Instance;
                Tributes objTrb = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
                if (objTrb != null)
                {
                    if (WebConfig.ApplicationMode.Equals("local"))
                    {

                        Response.Redirect("~/" + Session["TributeURL"] + "/managephotoalbum.aspx" + "?mode=addphotos&photoAlbumId=" + photoAlbumId + "&Type=" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby"), false);

                        //appDomian = WebConfig.AppBaseDomain.ToString();
                    }
                    else
                    {

                        appDomian = "http://www." + WebConfig.TopLevelDomain + "/" + Session["TributeURL"] + "/managephotoalbum.aspx" + "?mode=addphotos&photoAlbumId=" + photoAlbumId + "&Type=" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby");
                        Response.Redirect(appDomian, false);
                    }
                }
           // Response.Redirect("~/" + Session["TributeURL"] + "/managephotoalbum.aspx" + "?mode=addphotos&photoAlbumId=" + photoAlbumId, false);
            //Redirect.RedirectToPage(Redirect.PageList.ManagePhotoAlbum.ToString())
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void lbtnImage_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["PhotoAlbumId"] != null)
        {
            if (int.TryParse(Request.QueryString["PhotoAlbumId"], out photoAlbumId))
            {
                StateManager stateManager = StateManager.Instance;
                DownloadPhotoAlbumId = photoAlbumId;
                Session["PhotoAlbumId"] = photoAlbumId.ToString();
                string imagePath = string.Empty;
                Tributes objTributes = objTribute = (Tributes)stateManager.Get(PortalEnums.SessionValueEnum.TributeSession.ToString(), StateManager.State.Session);
                if ((_packageId == 6) || (_packageId == 7) || (_packageId == 8))
                {
                    if (Equals(objSessionValue, null))//when not logged in
                    {
                        if (IsCustomHeaderOn)
                            topHeight = 198;
                        else
                            topHeight = 81;
                    }
                    else
                    {
                        if (IsCustomHeaderOn)
                            topHeight = 261;
                        else
                            topHeight = 133;
                    }
                    if (Request.QueryString["PhotoAlbumId"] != null)
                    {
                        if (photoAlbumId > 0)
                            Session["PhotoAlbumId"] = photoAlbumId.ToString();
                    }
                    if (WebConfig.ApplicationMode.Equals("local"))
                    {
                        appDomian = WebConfig.AppBaseDomain.ToString();
                    }
                    else
                    {
                        Tributes objTrib = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
                        appDomian = "http://" + objTrib.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";
                    }
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "fnReachLimitExpiryPopup('location.href','document.title','UpgradeAlbum','" + _tributeUrl + "','" + _tributeId + "','" + appDomian + "','" + topHeight + "');", true);
                }
            }
        }
    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public PhotoAlbumPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    public PhotoAlbum PhotoAlbumDetails
    {
        set
        {
            if (value.PhotoAlbumCaption.Length != 0 && value.PhotoAlbumCaption.Length > 25)
                photoAlbumCaption = value.PhotoAlbumCaption.Remove(25) + "...";
            else
                photoAlbumCaption = value.PhotoAlbumCaption; //to display album name in navigation bar

            Session["PhotoCount"] = value.PhotoCount;
            ClearManagePhotoSession();
            hAlbumCaption.InnerHtml = value.PhotoAlbumCaption;
            pAlbumDesc.InnerHtml = value.PhotoAlbumDesc.Replace("\n", "</br>");
            if (!string.IsNullOrEmpty(StripHtml(value.PhotoAlbumDesc)))
            { Master.fbDescription = StripHtml(value.PhotoAlbumDesc); }
            pCreatedBy.InnerHtml = ResourceText.GetString("lblUploadedBy_PA") + " " + "<a href='javascript:void(0);' onclick=\"UserProfileModal_1('" + value.CreatedBy + "');\" class='yt-Comments'>" + value.UserName + "</a> on " + value.CreatedDate.ToString("MMMM dd, yyyy");

            //assign photoalbumid
            Master.DownloadPhotoAlbumId = value.PhotoAlbumId;

            //to add caption in session for displaying title in the navigation of AddPhotos page
            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add("PhotoAlbumName", value.PhotoAlbumCaption, StateManager.State.Session);
        }
    }
    public List<Photos> PhotosList
    {
        set
        {
            if (value.Count > 0)
            {
                rptPhotoList.DataSource = value;
                if (!string.IsNullOrEmpty(value[0].PhotoImage.ToString()))
                { Master.fbThumbnail = value[0].PhotoImage.ToString(); }
                rptPhotoList.DataBind();
                Master.PhotosList = value;
            }
            else
            {
                Response.Redirect("~/" + Session["TributeURL"] + "/photos.aspx", false);
                //Redirect.RedirectToPage(Redirect.PageList.PhotoGallery.ToString())
            }
        }
    }
    public int TotalRecords
    {
        set
        {
            if (value > 0)
            {
                //divNoRecord.Visible = false;
                ////divPhoto.Visible = true;
                divPagingTop.Visible = true;
                divPagingBottom.Visible = true;
                spPageTop.InnerText = ResourceText.GetString("txtPage_PA");
                spPageBottom.InnerText = ResourceText.GetString("txtPage_PA");
                hAlbumCaption.Visible = true;
                pAlbumDesc.Visible = true;
                pCreatedBy.Visible = true;
                pClick.Visible = false;
                pNoRecord.Visible = false;
                lbtnCreateAlbum.Visible = false;
                divCreateAlbum.Visible = false;
            }
            else
            {
                //divNoRecord.Visible = true;
                //divPhoto.Visible = false;
                pClick.Visible = true;
                pNoRecord.Visible = true;
                lbtnCreateAlbum.Visible = true;
                divCreateAlbum.Visible = true;
                divPagingTop.Visible = false;
                divPagingBottom.Visible = false;
                hAlbumCaption.Visible = false;
                pAlbumDesc.Visible = false;
                pCreatedBy.Visible = false;
                pNoRecord.InnerText = ResourceText.GetString("txtNoPhoto_PA");
                pClick.InnerText = ResourceText.GetString("txtClick_PA");
                lbtnCreateAlbum.Text = ResourceText.GetString("lbtnAddPhoto_PA");
                //divNoRecord.InnerText = ResourceText.GetString("txtNoPhoto_PA");
            }
            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add("TotalPhotosInAlbum", value, StateManager.State.Session);
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
    public string XmlFilePath
    {
        set
        {
            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add("XMLFilePath", value, StateManager.State.Session);
            objStateManager.Add("SlideShowStartPhoto", "0", StateManager.State.Session);
        }
    }
    #endregion

    #region METHODS

    public string StripHtml(string htmlString)
    {
        Regex regex = new Regex("</?(.*)>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        string finalString = Regex.Replace(htmlString, @"<(.|\n)*?>", string.Empty);  //regex.Replace(htmlString, regex, string.Empty);
        return finalString;
        //return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
    }


    /// <summary>
    /// Method to fill the Photos object to get the details of photo album
    /// </summary>
    /// <returns>Filled Photos entity</returns>
    private Photos GetPhotoObject()
    {
        Photos objPhoto = new Photos();
        objPhoto.PhotoAlbumId = photoAlbumId;
        objPhoto.PageNumber = currentPage;
        objPhoto.PageSize = pageSize;
        objPhoto.SortOrder = "asc";
        return objPhoto;
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
            _userId = objSessionValue.UserId;

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
        //to get photo album id from querystring
        object albumSession = objStateManager.Get("PhotoAlbumId", StateManager.State.Session);
        if (Request.QueryString["PhotoAlbumId"] != null)
        {
            photoAlbumId = int.Parse(Request.QueryString["PhotoAlbumId"].ToString());
            objStateManager.Add("PhotoAlbumId", photoAlbumId, StateManager.State.Session);
        }
        else if (!Equals(albumSession, null))
        {
            photoAlbumId = int.Parse(albumSession.ToString());
        }
        else
            photoAlbumId = 0;

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
        pageSize = (int.Parse(WebConfig.Pagesize_PhotoAlbum));

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
    private bool UserIsAdminOrOwner()
    {
        UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
        objUserInfo.UserId = _userId;
        objUserInfo.TributeId = _tributeId;
        objUserInfo.TypeId = photoAlbumId;
        objUserInfo.TypeName = TYPE_NAME;

        if (_userId != 0)
        {
            bool isUserAdmin = IsUserAdmin(objUserInfo);
            bool isUserOwner = IsUserOwner(objUserInfo);

            objUserInfo.IsAdmin = isUserAdmin;
            objUserInfo.IsOwner = isUserOwner;
            isAdmin = isUserAdmin;
        }
        else
        {
            objUserInfo.IsAdmin = false;
            isAdmin = false;
        }
        StateManager objStateManager = StateManager.Instance;
        objStateManager.Add("UserAdminInfo_PhotoAlbum", objUserInfo, StateManager.State.Session);
        return isAdmin;
    }

    //clear the session variables thet might have been set in ManagePhoto
    private void ClearManagePhotoSession()
    {
        Session["NewPhotoCount"] = null;
        Session["NewPhotoID"] = null;
        StateManager objStateManager = StateManager.Instance;
        objStateManager.Remove("NewPhotoAlbumId", StateManager.State.Session);
    }
    #endregion

}