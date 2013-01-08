#pragma warning disable 1587
///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Shared.Story.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page provides the default layout for the sub pages for a tribute
///Audit Trail     : Date of Modification  Modified By         Description
#pragma warning restore 1587

#region USING DIRECTIVES

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using TributesPortal.BusinessEntities;
using TributesPortal.MessagingSystem;
using TributesPortal.Miscellaneous;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using System.IO;
using Facebook.Web;
using TributesPortal.ResourceAccess;
using ICSharpCode.SharpZipLib.Zip;
#endregion

public partial class Shared_Story : System.Web.UI.MasterPage
{
    #region CLASS VARIABLES

    protected string _userName;
    private int _userId;
    private string _typeName;
    private string _firstName;
    private string _lastName;
    private string _emailID;
    protected int _tributeId;
    protected string _tributeType;
    protected string _tributeName;
    protected string _tributeUrl;
    protected string _themeName; //for aspx variable
    protected string _themeValue; //for setting the selected theme
    protected bool _isActive;
    private DateTime _endDate;
    private DateTime _createdDate;
    private int currentPage;
    private bool _isAdmin;
    private bool _isOwner;
    private int _noteId;
    private int _videoId;
    private int _photoId;
    private int _photoAlbumId;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    private int _totalPhotos = 0;
    private int isInFavorite = 0;
    protected string adSenseCode = string.Empty;
    protected string adSenseComment = string.Empty;
    protected string _xmlFilePath = string.Empty;
    protected int _recordNumber = 0;
    protected string toFav = string.Empty;
    protected string toFavText = string.Empty;
    protected string URL;
    protected string _tributeTypeName;
    public int _packageId;
    protected string _query_string = string.Empty; // to add to urls parameter TributeType...
    public int pack_type = 0;
    private bool IsCustomHeaderOn = false;
    public string _OriginalImageUrl;
    public string _BigImageAlbumUrl;
    public int DownloadPhotoAlbumId = 0;

    string appDomian = string.Empty;
    public int topHeight = 0;
    string url = string.Empty;
    public List<Photos> PhotosList;
    //LHK: WordPress Integration
    public  bool isInIframe = false;
    private string _TopUrl = string.Empty;
    public string PageUrl = string.Empty;
    public string PageTitle = string.Empty;
    public string app_domin = string.Empty;
    public string codedURL = string.Empty;
    public string codedtitle = string.Empty;

    public string fbDescription = string.Empty;
    public string fbThumbnail = string.Empty;

    #endregion

    #region Properties

    public virtual String query_string
    {
        get { return _query_string; }
    }

    #endregion

    #region EVENTS

    protected void Page_Init(object sender, EventArgs e)
    {
        //Page.RegisterStartupScript("ScriptDescription", "<script type=\"text/javascript\">    alert('" + Request.Url.ToString() + "');</script>");
        try
        {
            StateManager stateManager = StateManager.Instance;
            MiscellaneousController objMisc = new MiscellaneousController();

          //-- Ashu(June2,2011) : Changes for jquery conflict issues & Javascript exception

        if (!(Request.Url.AbsolutePath.ToLower().EndsWith("managephotoalbum.aspx")))
        {
            DivScript.Visible = true;
            divModalboxscript.Visible = true;
        }
        else
        {
            DivScript.Visible = false;
            divModalboxscript.Visible = false;
        }

        app_domin = WebConfig.AppBaseDomain;
        // to avoid redirection for create photo album page
        if (!(Request.Url.AbsolutePath.ToLower().EndsWith("managephotoalbum.aspx")))
        {
            //if Tribute Type and Tribute Url are in querystring        
            #region LHK:Redirection to upgradedUrl

            if (!this.IsPostBack)
            {
                Tributes objTrb = new Tributes();
                //GetUpgradedUrl
                if ((Request.QueryString["TributeUrl"] != null))
                {
                    _tributeUrl = Request.QueryString["TributeUrl"].ToString();
                    objTrb.TributeUrl = _tributeUrl = Request.QueryString["TributeUrl"].ToString();

                    if (Request.QueryString["TributeType"] != null)
                    {
                        objTrb.TypeDescription = Request.QueryString["TributeType"].ToString();
                    }                    
                    else if (Session["PhotoAlbumTributeSession"] != null)
                    {
                        objTrb = Session["PhotoAlbumTributeSession"] as Tributes;
                        if(objTrb != null)
                            if(string.IsNullOrEmpty(objTrb.TypeDescription))
                                Session["TributeType"] = objTrb.TypeDescription;
                    }
                    objTrb = objMisc.GetTributeUrlOnOldTributeUrl(objTrb, WebConfig.ApplicationType.ToString());
                 
                    if (Request.QueryString["TributeType"] != null)
                    {
                        objTrb.TypeDescription = Request.QueryString["TributeType"].ToString();
                    }
                   
                    if (objTrb != null)
                    {
                        if (objTrb.TributeUrl != null)
                        {
                            if (!(string.IsNullOrEmpty(objTrb.TributeUrl.ToString())) && (!(_tributeUrl.Equals(objTrb.TributeUrl.ToString()))))
                            {
                                url = GetRedirectUrl();
                                Response.Redirect(url, true);

                            }
                        }
                    }
                }
            }
            #endregion

            #region For Image uploader redirection
            //LHK: redirection from main domain to subdomain- for image uploader
            if (!(WebConfig.ApplicationMode.Equals("local")))
            {

                if (Request.QueryString["Type"] != null && Request.QueryString["TributeUrl"] != null)
                {
                    RediectUsingQueryString();
                }
                else
                {
                    RedirectUsingSession();
                }
            }
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                if (Request.QueryString["mode"] != null && Request.QueryString["TributeUrl"] != null)
                {
                    if (Request.QueryString["mode"].ToString() == "Create")
                    {
                        if (FacebookWebContext.Current.Session != null)
                        {
                            url = WebConfig.AppBaseDomain + Request.QueryString["TributeUrl"].ToString() + "/photos.aspx?post_on_facebook=True";
                        }
                        else
                        {
                            url = WebConfig.AppBaseDomain + Request.QueryString["TributeUrl"].ToString() + "/photos.aspx";

                        }
                    }
                    else if (Request.QueryString["mode"].ToString() == "AddPhotos")
                    {
                        url = WebConfig.AppBaseDomain + Request.QueryString["TributeUrl"].ToString() + "/photoalbum.aspx?photoAlbumId=" + Request.QueryString["AlbumId"].ToString();
                    }
                    if(! string.IsNullOrEmpty(url))
                        Response.Redirect(url, false);
                }
            }
                #endregion

        }
        Tributes objTribute = new Tributes();

        if ((Request.QueryString["TributeUrl"] != null) && (Request.QueryString["TributeType"] != null))
        {
            objTribute.TributeUrl = Request.QueryString["TributeUrl"].ToString();
            objTribute.TypeDescription = Request.QueryString["TributeType"].ToString();
            stateManager.Add("TributeSession", objMisc.GetTributeSessionForUrlAndType(objTribute, WebConfig.ApplicationType.ToString()), TributesPortal.Utilities.StateManager.State.Session);
        }

        else if (Request.QueryString["TributeUrl"] != null)
        {
            objTribute.TributeUrl = Request.QueryString["TributeUrl"].ToString();

            if (Session["TributeType"] != null)
                objTribute.TypeDescription = Session["TributeType"].ToString();
            else
                objTribute.TypeDescription = null;

            stateManager.Add("TributeSession", objMisc.GetTributeSessionForUrlAndType(objTribute, WebConfig.ApplicationType.ToString()), TributesPortal.Utilities.StateManager.State.Session);

        }
        Tributes objTrib = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
        if (objTrib != null)
        {
            if (objTrib.CreatedDate != null)
                Session["TributeCreatedDate"] = objTrib.CreatedDate;
            if (objTrib.TributeUrl != null)
                Session["TributeURL"] = objTrib.TributeUrl;
        }
           
            TributePackage objpackage = new TributePackage();
            objpackage.UserTributeId = objTribute.TributeId;

            object[] param = { objpackage };
            objMisc.TriputePackageInfo(param);
            if (objpackage.CustomError == null)
            {
                _packageId = objpackage.PackageId;
                if (_packageId != 1 && objpackage.EndDate != null)
                {
                    _endDate = (DateTime)objpackage.EndDate;
                    Session["tributeEndDate"] = _endDate;
                }
            }
        }
        catch (Exception ex)
        {
           throw ex;
        }
       
    }

    public void RediectUsingQueryString()
    {
        string TypeDesc = Request.QueryString["Type"].ToString().ToLower().Replace("new baby", "newbaby");
        string Tributeurl = Request.QueryString["TributeUrl"].ToString();

        url = Request.Url.ToString().ToLower();
        string startUrl = Request.QueryString["Type"].ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain;

        if (!(url.Contains(startUrl)))
        {
            Session["PhotoAlbumTributeSession"] = null;

            if (url.Contains("story.aspx"))
                url = "http://" + TypeDesc + "." + WebConfig.TopLevelDomain + "/" + Tributeurl + "/story.aspx";
            else if (url.Contains("notes.aspx"))
                url = "http://" + TypeDesc + "." + WebConfig.TopLevelDomain + "/" + Tributeurl + "/notes.aspx";
            else if (url.Contains("event.aspx"))
                url = "http://" + TypeDesc + "." + WebConfig.TopLevelDomain + "/" + Tributeurl + "/events.aspx";
            else if (url.Contains("guestbook.aspx"))
                url = "http://" + TypeDesc + "." + WebConfig.TopLevelDomain + "/" + Tributeurl + "/guestbook.aspx";
            else if (url.Contains("gift.aspx"))
                url = "http://" + TypeDesc + "." + WebConfig.TopLevelDomain + "/" + Tributeurl + "/Gift.aspx";
            else if (url.Contains("photogallery.aspx"))
            {
                if (Request.QueryString["mode"] != null)
                {
                    if (Request.QueryString["mode"].ToString() == "Create")
                    {
                        if (FacebookWebContext.Current.Session != null)
                        {
                            url = "http://" + TypeDesc + "." + WebConfig.TopLevelDomain + "/" + Tributeurl + "/photos.aspx?post_on_facebook=True";
                        }
                        else
                        {
                            url = "http://" + TypeDesc + "." + WebConfig.TopLevelDomain + "/" + Tributeurl + "/photos.aspx";
                        }
                    }

                    else if (Request.QueryString["mode"].ToString() == "AddPhotos" && Request.QueryString["AlbumId"] != null)
                    {
                        url = "http://" + TypeDesc + "." + WebConfig.TopLevelDomain + "/" + Tributeurl + "/photoalbum.aspx?photoAlbumId=" + Request.QueryString["AlbumId"].ToString();
                    }
                }
            }

            else if (url.Contains("videogallery.aspx"))
                url = "http://" + TypeDesc + "." + WebConfig.TopLevelDomain + "/" + Tributeurl + "/videos.aspx";

            if(string.IsNullOrEmpty(url))
                Response.Redirect(url, false);
        }
    }

    public void RedirectUsingSession()
    {
        StateManager stateManager = StateManager.Instance;
        Tributes objTrb = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
        if (objTrb != null)
        {
            if (objTrb.TypeDescription == null)
            {
                if (Session["PhotoAlbumTributeSession"] != null)
                {
                    objTrb = Session["PhotoAlbumTributeSession"] as Tributes;
                }
            }
        }
        else if (Session["PhotoAlbumTributeSession"] != null)
        {
            objTrb = Session["PhotoAlbumTributeSession"] as Tributes;
        }
        if (objTrb != null)
        {
            if (objTrb.TypeDescription != null)
            {
                url = Request.Url.ToString().ToLower();
                string startUrl = objTrb.TypeDescription.ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain;

                if (!(url.Contains(startUrl)))
                {
                    Session["PhotoAlbumTributeSession"] = null;

                    if (url.Contains("story.aspx"))
                        url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/story.aspx";
                    else if (url.Contains("notes.aspx"))
                        url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/notes.aspx";
                    else if (url.Contains("event.aspx"))
                        url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/events.aspx";
                    else if (url.Contains("guestbook.aspx"))
                        url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/guestbook.aspx";
                    else if (url.Contains("gift.aspx"))
                        url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/Gift.aspx";
                    else if (url.Contains("photogallery.aspx"))
                        url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/photos.aspx";
                    else if (url.Contains("videogallery.aspx"))
                        url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/videos.aspx";

                    if(string.IsNullOrEmpty(url))
                        Response.Redirect(url, false);
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(Shared_Story));
        //code for YT MObile redirections
        string redirctMobileUrl = string.Empty;

        MiscellaneousController objMisc = new MiscellaneousController();
        if (!IsPostBack)
        {
            DeviceManager deviceManager = new DeviceManager
            {
                UserAgent = Request.UserAgent,
                IsMobileBrowser = Request.Browser.IsMobileDevice
            };
            if (deviceManager.IsMobileDevice())
            {
                #region MObile Redirect

                Tributes obTrb = new Tributes();
                string mTrbUrl = string.Empty;
                string mTrbType = string.Empty;
                string strStartUrl = string.Empty;
                int mTabId = 0;
                if (Request.QueryString["TributeUrl"] != null)
                {
                    obTrb.TributeUrl = mTrbUrl = Request.QueryString["TributeUrl"].ToString();
                }
                if (Request.QueryString["TributeType"] != null)
                {
                    obTrb.TypeDescription = mTrbType = Request.QueryString["TributeType"].ToString();
                }
                bool IsMobileViewOn = objMisc.GetIsMobileViewOn(obTrb);

                bool IsMobileRedirectOn = false;
                bool.TryParse(WebConfig.IsMobileRedirectOn, out IsMobileRedirectOn);

                if (!(string.IsNullOrEmpty(mTrbUrl)) && !(string.IsNullOrEmpty(mTrbType)) && IsMobileViewOn && IsMobileRedirectOn)
                {
                    obTrb = objMisc.GetTributeUrlOnOldTributeUrl(obTrb, WebConfig.ApplicationType.ToString());

                    strStartUrl = string.Format("{0}{1}{2}{3}{4}{5}", "https://www.", WebConfig.TopLevelDomain, "/mobile/index.html?tributeurl=", obTrb.TributeUrl, "&tributetype=", mTrbType);

                    string url = Request.Url.ToString().ToLower();

                    #region story
                    if (url.Contains("story.aspx"))
                        redirctMobileUrl = strStartUrl + "&page=story"; 
                    #endregion
                    #region notes
                    else if (url.Contains("notes.aspx"))
                        redirctMobileUrl = strStartUrl + "&page=notes"; 
                    #endregion
                    #region note
                    else if (url.Contains("notefullview.aspx"))
                    {
                        if ((Request.QueryString["noteId"] != null))
                        {
                            if (int.TryParse(Request.QueryString["noteId"], out mTabId))
                            {
                                redirctMobileUrl = strStartUrl + "&page=note&id=" + mTabId.ToString();
                            }
                            else
                            {
                                redirctMobileUrl = strStartUrl + "&page=notes";
                            }
                        }
                        else
                        {
                            redirctMobileUrl = strStartUrl + "&page=notes";
                        }
                    } 
                    #endregion
                    #region events
                    else if (url.Contains("event.aspx"))
                        redirctMobileUrl = strStartUrl + "&page=events"; 
                    #endregion
                    #region event
                    else if (url.Contains("eventfullview.aspx"))
                    {
                        if ((Request.QueryString["EventID"] != null))
                        {
                            if (int.TryParse(Request.QueryString["EventID"], out mTabId))
                            {
                                redirctMobileUrl = strStartUrl + "&page=event&id=" + mTabId.ToString();
                            }
                            else
                            {
                                redirctMobileUrl = strStartUrl + "&page=events";
                            }
                        }
                        else
                        {
                            redirctMobileUrl = strStartUrl + "&page=events";
                        }
                    } 
                    #endregion
                    #region guestbook
                    else if (url.Contains("guestbook.aspx"))
                        redirctMobileUrl = strStartUrl + "&page=guestbook"; 
                    #endregion
                    #region gift
                    else if (url.Contains("gift.aspx"))
                        redirctMobileUrl = strStartUrl + "&page=memorials"; 
                    #endregion
                    #region photogallery
                    else if (url.Contains("photogallery.aspx"))
                        redirctMobileUrl = strStartUrl + "&page=gallery"; 
                    #endregion
                    #region photoalbum
                    else if (url.Contains("photoalbum.aspx"))
                    {
                        if ((Request.QueryString["PhotoAlbumId"] != null))
                        {
                            if (int.TryParse(Request.QueryString["PhotoAlbumId"], out mTabId))
                            {
                                redirctMobileUrl = strStartUrl + "&page=photoalbum&id=" + mTabId.ToString();
                            }
                            else
                            {
                                redirctMobileUrl = strStartUrl + "&page=gallery";
                            }
                        }
                        else
                        {
                            redirctMobileUrl = strStartUrl + "&page=gallery";
                        }
                    } 
                    #endregion
                    #region photoview
                    else if (url.Contains("photoview.aspx"))
                    {
                        if ((Request.QueryString["PhotoId"] != null))
                        {
                            if (int.TryParse(Request.QueryString["PhotoId"], out mTabId))
                            {
                                redirctMobileUrl = strStartUrl + "&page=photo&id=" + mTabId.ToString();
                            }
                            else
                            {
                                redirctMobileUrl = strStartUrl + "&page=gallery";
                            }
                        }
                        else
                        {
                            redirctMobileUrl = strStartUrl + "&page=gallery";
                        }
                    } 
                    #endregion
                    #region photoview
                    else if (url.Contains("photoview.aspx"))
                        redirctMobileUrl = strStartUrl + "&page=gallery"; 
                    #endregion
                    #region managevideo
                    else if (url.ToLower().Contains("managevideo.aspx"))
                    {
                        if ((Request.QueryString["videoId"] != null))
                        {
                            if (int.TryParse(Request.QueryString["videoId"], out mTabId))
                            {
                                redirctMobileUrl = strStartUrl + "&page=video&id=" + mTabId.ToString();
                            }
                            else
                            {
                                redirctMobileUrl = strStartUrl + "&page=gallery";
                            }
                        }
                        else
                        {
                            redirctMobileUrl = strStartUrl + "&page=gallery";
                        }
                    } 
                    #endregion
                    #region videos
                    else if (url.ToLower().Contains("videogallery.aspx"))
                    {
                        redirctMobileUrl = strStartUrl + "&page=gallery";
                    }
                    #endregion
                    else
                    {
                        redirctMobileUrl = strStartUrl + "&page=home";
                    }
                }
                #endregion
            }
        }
        if (string.IsNullOrEmpty(redirctMobileUrl))
        {
        try
        {

            PageUrl = GetRedirectUrl();
            PageTitle = Page.Title.ToString();
            codedURL = HttpUtility.UrlEncode(PageUrl).ToString();
            codedtitle = HttpUtility.UrlEncode(PageTitle).ToString();

            if (Request.QueryString["topurl"] != null)
            {
                _TopUrl = Request.QueryString["topurl"].ToString();
                Response.Cookies["topurl"].Value = _TopUrl;
                Response.Cookies["topurl"].Domain = _TopUrl;
                Response.Cookies["topurl"].Expires = DateTime.Now.AddHours(4);
            }

            if (Request.Cookies["topurl"] != null)
            {
                hdnTopUrl.Value = Request.Cookies["topurl"].Value.ToString();
            }
            if (Session["isInIframe"] != null)
            {
                isInIframe = bool.Parse(Session["isInIframe"].ToString());
            }

            // New code added on 07 june 2011 by rupendra to handle Apple safari problem
            if (Request.Browser.Browser.ToString().Trim().Equals("AppleMAC-Safari"))
            {
            }

            //LHK:EmptyDivAboveMainPanel
            StateManager stateTribute = StateManager.Instance;
            SessionValue objSessvalue = (SessionValue)stateTribute.Get("objSessionvalue", StateManager.State.Session);
            if ((Request.QueryString["TributeUrl"] != null))
            {
                string _trbUrl = Request.QueryString["TributeUrl"].ToString();
                GetCustomHeaderVisible(_trbUrl, WebConfig.ApplicationType.ToString());
            }
            if (!(objSessvalue != null))
            {
                if (!IsCustomHeaderOn)
                {
                    EmptyDivAboveMainPanel.Visible = true;
                }
            }
            //LHK:EmptyDivAboveMainPanel

            if (WebConfig.ApplicationMode.Equals("local"))
            {
                appDomian = WebConfig.AppBaseDomain.ToString();
            }
            else
            {
                StateManager stateManager = StateManager.Instance;
                Tributes objTrib = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
                //Ashu 18 aug for session null
                if (objTrib != null)
                {
                    if (objTrib.TypeDescription != null)
                    {
                        appDomian = "http://" + objTrib.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";
                    }
                    else if (Session["PhotoAlbumTributeSession"] != null)
                    {
                        objTrib = Session["PhotoAlbumTributeSession"] as Tributes;
                        if (objTrib.TypeDescription != null)
                            appDomian = "http://" + objTrib.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";
                    }
                }
                else if (Session["PhotoAlbumTributeSession"] != null)
                {
                    objTrib = Session["PhotoAlbumTributeSession"] as Tributes;
                    if (objTrib != null)
                    {
                        if (objTrib.TypeDescription != null)
                            appDomian = "http://" + objTrib.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";
                    }
                }
            }


            try
            {
                if (Request.QueryString["TributeType"] != null)
                {
                    ytHeader.TributeType = Request.QueryString["TributeType"].ToString();
                }

                // get the Tribute and User detail from the Session           
                GetValuesFromSession();
                // Set the Class of the div according the Page Name
                SetClass();
                SetTabClass();
                // This function will set the Left Menu
                SetMenuOptions();
                SetMenuItemClass();
                // This Function will load themes in the left panel and loads the selected theme for the tribute.
                LoadThemes();
                //Method to check if tribute already in favorite list.
                CheckForFavorite();
                //to set affiliate link.
                AffiliateLinks(_tributeType);
                StateManager objStateManager = StateManager.Instance;
                if (objStateManager.Get("NoteSession", StateManager.State.Session) != null)
                    _noteId = int.Parse(objStateManager.Get("NoteSession", StateManager.State.Session).ToString());
                if (objStateManager.Get("VideoSession", StateManager.State.Session) != null)
                    _videoId = int.Parse(objStateManager.Get("VideoSession", StateManager.State.Session).ToString());
                if (objStateManager.Get("PhotoViewSession", StateManager.State.Session) != null)
                    _photoId = int.Parse(objStateManager.Get("PhotoViewSession", StateManager.State.Session).ToString());
                if (objStateManager.Get("PhotoAlbumId", StateManager.State.Session) != null)
                    _photoAlbumId = int.Parse(objStateManager.Get("PhotoAlbumId", StateManager.State.Session).ToString());
                if (objStateManager.Get("XmlFilePath", StateManager.State.Session) != null) //to get xml file name for slideshow
                    _xmlFilePath = objStateManager.Get("XmlFilePath", StateManager.State.Session).ToString();
                //to get photo number from where to start slideshow.
                if (objStateManager.Get("SlideShowStartPhoto", StateManager.State.Session) != null) //to get start photo number in slideshow.
                    _recordNumber = int.Parse(objStateManager.Get("SlideShowStartPhoto", StateManager.State.Session).ToString());
                else
                    _recordNumber = 0;

                // Set the controls value
                SetControlsValue();



                SetPageNameInSession(_typeName);
                if (!(string.IsNullOrEmpty(_tributeType)))
                {
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
                }

                //LHK: for photo upgrade changes
                if ((liAdd.Visible == false) && (liEdit.Visible == false) && (liView.Visible == false) && (liDownloadalbum.Visible == false) && (liVieFullPhoto.Visible == false))
                    divSubTool.Visible = false;
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex.StackTrace);
                throw ex;
            }
        }
        catch (Exception ex)
        {
            LogError(ex.Message, ex.StackTrace);
            throw ex;
        }
        }
        else
        {
            Response.Redirect(redirctMobileUrl, false);
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


    protected void lbtnMyProfile_Click(object sender, EventArgs e)
    {

    }

    protected void lbtnEmailTribute_Click(object sender, EventArgs e)
    {
        StateManager stateManager = StateManager.Instance;
        EmailLink objEmail = new EmailLink();
        string EmailHref = string.Empty;
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
            EmailHref = _TopUrl + "?http://" + _tributeType.ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + _tributeUrl;
        }
        else
        {
            EmailHref = "http://" + _tributeType.ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + _tributeUrl;
        }
        string UrlToEmail = "<a href='" + EmailHref + "'>" + EmailHref + "</a>";

        if (_firstName == string.Empty)
        {
            objEmail.EmailSubject = _userName + " wants you to view a " + _tributeType + " Tribute on Your Tribute...";
            objEmail.EmailBody = "<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + _userName + " wants you to view the " + _tributeName + " Tribute.</p> <p>To view the tribute, follow the link below: " + "<br/>" + UrlToEmail + "</p></font>";
        }
        else
        {
            objEmail.EmailSubject = _firstName + " " + _lastName + " wants you to view a " + _tributeType + " Tribute on Your Tribute...";
            objEmail.EmailBody = "<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + _firstName + " " + _lastName + " wants you to view the " + _tributeName + " Tribute.</p> <p>To view the tribute, follow the link below: " + "<br/>" + UrlToEmail + "</p>";
        }
        objEmail.TypeName = _typeName;
        objEmail.FromEmailAddress = _emailID;

        stateManager.Add(PortalEnums.SessionValueEnum.ShareTributeEmail.ToString(), objEmail, StateManager.State.Session);
        // Added by Ashu on Oct 4, 2011 for rewrite URL 
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
            Response.Redirect("~/" + Session["TributeURL"] + "/shareMoments.aspx", false);
        else
            Response.Redirect("~/" + Session["TributeURL"] + "/sharetribute.aspx", false);

    }

    protected void lbtnShareOnFacebook_Click(object sender, EventArgs e)
    {
        Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.UnderConstruction.ToString()), false);
    }
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
            }
        }
        catch (Exception ex)
        {
           throw ex;
        }
    }

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            MiscellaneousController objMisc = new MiscellaneousController();
            StateManager stateTribute = StateManager.Instance;
            SessionValue objSessvalue = (SessionValue)stateTribute.Get("objSessionvalue", StateManager.State.Session);
            #region TributeNotes
            if (_typeName == "TributeNotes")
            {
                    Response.Redirect("~/" + Session["TributeURL"] + "/managenote.aspx", false);
                
            }
            #endregion
            #region VideoGallery
            else if (_typeName == "VideoGallery")
            {

                    Response.Redirect("~/" + Session["TributeURL"] + "/addvideo.aspx", false);
                
            }
            #endregion
            #region Event
            else if (_typeName == PortalEnums.TributeContentEnum.Event.ToString())
            {
                
                    Response.Redirect("~/" + Session["TributeURL"] + "/manageevent.aspx", false);
                
            }
            #endregion
            #region EventFullView
            else if (_typeName == PortalEnums.TributeContentEnum.EventFullView.ToString())
            {
 
                    Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + Session["TributeURL"].ToString() + "/manageevent.aspx" + "?EventID=" + Session["EventIdForEdit"].ToString());
                

            }
            #endregion
            #region PhotoGallery
            else if (_typeName == "PhotoGallery")
            {
                if (Equals(objSessionValue, null))//when not logged in
                {
                    if (IsCustomHeaderOn)
                        topHeight = 197;
                    else
                        topHeight = 78;
                }
                else
                {
                    if (IsCustomHeaderOn)
                        topHeight = 258;
                    else
                        topHeight = 131;
                }

                bool isAllowedPhotoCheck = false;
                string tributeEndDate = objMisc.GetTributeEndDate(_tributeId);
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
                isAllowedPhotoCheck = objMisc.IsAllowedPhotoCheck(_photoAlbumId);



                if ((_packageId == 6) || (_packageId == 7) || (_packageId == 3) || (_packageId == 8) | ((_packageId == 5) && !isAllowedPhotoCheck && (date2 < DateTime.Now)))
                {
                    int CurrentAlbums = objMisc.GetCurrentPhotoAlbums(_tributeId);
                    if (CurrentAlbums >= (int.Parse(WebConfig.PhotoAlbumLimit)))
                    {
                        if (Equals(objSessionValue, null))//when not logged in
                        {
                            if (IsCustomHeaderOn)
                                topHeight = 197;
                            else
                                topHeight = 78;
                        }
                        else
                        {
                            if (IsCustomHeaderOn)
                                topHeight = 258;
                            else
                                topHeight = 131;
                        }

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "fnReachLimitExpiryPopup('location.href','document.title','Photos','" + _tributeUrl + "','" + _tributeId + "','" + appDomian + "','" + topHeight + "');", true);
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

                            }
                            else
                            {
                                appDomian = "http://www." + WebConfig.TopLevelDomain + "/" + Session["TributeURL"] + "/managephotoalbum.aspx?albummode=Create&Type=" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby");
                                Response.Redirect(appDomian, false);

                            }

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

                        }
                        else
                        {
                            appDomian = "http://www." + WebConfig.TopLevelDomain + "/" + Session["TributeURL"] + "/managephotoalbum.aspx?albummode=Create&Type=" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby");
                            Response.Redirect(appDomian, false);
                        }
                    }
                }
            } 
            #endregion
            #region PhotoAlbum
            else if (_typeName == "PhotoAlbum")
            {
                StateManager stateManager = StateManager.Instance;
                Tributes objTrb = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
                if (objTrb != null)
                {
                    if (WebConfig.ApplicationMode.Equals("local"))
                    {
                        Response.Redirect("~/" + Session["TributeURL"] + "/managephotoalbum.aspx" + "?mode=addphotos&photoAlbumId=" + _photoAlbumId + "&Type=" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby"), false);

                    }
                    else
                    {
                        appDomian = "http://www." + WebConfig.TopLevelDomain + "/" + Session["TributeURL"] + "/managephotoalbum.aspx" + "?mode=addphotos&photoAlbumId=" + _photoAlbumId + "&Type=" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby");
                        Response.Redirect(appDomian, false);
                    }
                }
            } 
            #endregion
        }
        catch (Exception ex)
        {
           throw ex;
        }
    }
    protected void lbtnBackTo_Click(object sender, EventArgs e)
    {
        string redirecturl = "";
        if (Session["PhotoAlbumTributeSession"] != null)
        {
             objTribute = Session["PhotoAlbumTributeSession"] as Tributes;
            _tributeId = objTribute.TributeId;
            _tributeType = objTribute.TypeDescription;
            _tributeTypeName = objTribute.TypeDescription.ToLower().Replace("new baby", "newbaby");
            _tributeName = objTribute.TributeName;
            _createdDate = objTribute.CreatedDate;
            _tributeUrl = objTribute.TributeUrl;
            _isActive = objTribute.IsActive;
        }
        if (WebConfig.ApplicationMode.Equals("local"))
        {
            redirecturl = WebConfig.AppBaseDomain;
        }
        else
        {
            redirecturl = "http://" + _tributeType.ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";


        }
        try
        {
            if (_tributeUrl != null)
            {
                if (_typeName == "AddNote" || _typeName == "EditNote")
                    Response.Redirect(redirecturl + _tributeUrl + "/notes.aspx", false);
                else if (_typeName == "AddVideo" || _typeName == "EditVideo") //for back to video
                {
                    Response.Redirect(redirecturl + _tributeUrl + "/videos.aspx", false);
                    //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.VideoGallery.ToString()), false);
                }
                else if (_typeName == PortalEnums.TributeContentEnum.ManageEvent.ToString())
                    Response.Redirect(redirecturl + _tributeUrl + "/events.aspx", false);
                else if (_typeName == PortalEnums.TributeContentEnum.InviteGuest.ToString())
                    Response.Redirect(redirecturl + _tributeUrl + "/events.aspx", false);
                else if (_typeName == "AddPhotoAlbum")
                    Response.Redirect(redirecturl + _tributeUrl + "/photos.aspx", false);
                else if (_typeName == "ManagePhoto")
                    Response.Redirect(redirecturl + _tributeUrl + "/photo.aspx", false);
                else if (_typeName == "AddPhotosToAlbum")
                    Response.Redirect(redirecturl + _tributeUrl + "/photos.aspx", false);
                else if (_typeName == "EditPhotoAlbum")
                    Response.Redirect(redirecturl + _tributeUrl + "/photoalbum.aspx", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            if (_typeName == "NoteFullView")
                Response.Redirect("~/" + Session["TributeURL"] + "/managenote.aspx" + "?mode=edit" + "&noteId=" + _noteId, false);
            else if (_typeName == "ManageVideo" || _typeName == "ManageVideoTribute")
            {
                Response.Redirect("~/" + Session["TributeURL"] + "/addvideo.aspx?mode=edit" + "&videoId=" + _videoId, false);
            }
            else if (_typeName == "PhotoFullView")
                Response.Redirect("~/" + Session["TributeURL"] + "/managephoto.aspx" + "?&PhotoId=" + _photoId, false);
            else if (_typeName == "PhotoAlbum")
                Response.Redirect("~/" + Session["TributeURL"] + "/editphotoalbum.aspx" + "?&photoAlbumId=" + _photoAlbumId, false);
            else if (_typeName == PortalEnums.TributeContentEnum.EventFullView.ToString())
            {
                Response.Redirect("~/" + Session["TributeURL"].ToString() + "/manageevent.aspx" + "?EventID=" + Session["EventIdForEdit"].ToString());
            }
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
                lblErrMsg.InnerHtml = ShowErrorMessage(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments" ? ResourceText.GetString("errFavorite_Master1") : ResourceText.GetString("errFavorite_Master"));
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

    protected void lBtnDownloadAlbum_Click(object sender, EventArgs e)
    {

        MiscellaneousController objMisc = new MiscellaneousController();
        string[] getPath = CommonUtilities.GetPath();

        StateManager objStateManager = StateManager.Instance;
        //to get logged in user name from session as user is logged in user
        objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
        if (Request.QueryString["PhotoAlbumId"] != null)
        {
            if (int.TryParse(Request.QueryString["PhotoAlbumId"], out _photoAlbumId))
            {
                DownloadPhotoAlbumId = _photoAlbumId;
                Session["PhotoAlbumId"] = _photoAlbumId.ToString();
                string imagePath = string.Empty;
                Tributes objTributes = objTribute = (Tributes)objStateManager.Get(PortalEnums.SessionValueEnum.TributeSession.ToString(), StateManager.State.Session);
                if (objTributes != null)
                {
                    if (string.IsNullOrEmpty(objTributes.TributePackageType))
                    {
                        _packageId = objMisc.GetTributePackageId(_tributeId);
                    }
                }
                bool isAllowedPhotoCheck = false;
                string tributeEndDate = objMisc.GetTributeEndDate(_tributeId);
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
                isAllowedPhotoCheck = objMisc.IsAllowedPhotoCheck(_photoAlbumId);

                if (((_packageId == 3) || (_packageId == 6) || (_packageId == 7) || (_packageId == 8)) ||((_packageId == 5) && !isAllowedPhotoCheck && (date2 < DateTime.Now)))
                {
                    #region popup
		
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
                        if (_photoAlbumId > 0)
                            Session["PhotoAlbumId"] = _photoAlbumId.ToString();
                    }
                    if (WebConfig.ApplicationMode.Equals("local"))
                    {
                        appDomian = WebConfig.AppBaseDomain.ToString();
                    }
                    else
                    {
                        StateManager stateManager = StateManager.Instance;
                        Tributes objTrib = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
                        appDomian = "http://" + objTrib.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";
                    }
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "fnReachLimitExpiryPopup('location.href','document.title','UpgradeAlbum','" + _tributeUrl + "','" + _tributeId + "','" + appDomian + "','" + topHeight + "');", true); 
	#endregion
                }
                else
                {
                    #region allowFunctionality
                    List<Photos> objListPhotos = new List<Photos>();
                    Photos objPhotos = new Photos();
                    if (Request.QueryString["PhotoAlbumId"] != null)
                    {
                        int.TryParse(Request.QueryString["PhotoAlbumId"], out DownloadPhotoAlbumId);
                        objPhotos.PhotoAlbumId = DownloadPhotoAlbumId;
                    }

                    objListPhotos = objMisc.GetPhotoImagesList(objPhotos);

                    if ((DownloadPhotoAlbumId > 0) && (objListPhotos.Count > 0))
                    {

                        // zip up the files
                        try
                        {
                            string sTargetFolderPath = getPath[0] + "/" + getPath[1] + "/" + _tributeUrl.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_");

                            //to create directory for image.
                            string galleryPath = getPath[0] + "/" + getPath[1] + "/" + getPath[6];
                            string sZipFileName = "Album_" + DownloadPhotoAlbumId.ToString();
                            string[] filenames = Directory.GetFiles(sTargetFolderPath);
                            // Zip up the files - From SharpZipLib Demo Code
                            using (ZipOutputStream s = new ZipOutputStream(File.Create(galleryPath + "\\" + sZipFileName + ".zip")))
                            {
                                s.SetLevel(9); // 0-9, 9 being the highest level of compression

                                byte[] buffer = new byte[4096];
                                foreach (Photos objPhoto in objListPhotos)
                                {
                                    bool Foundflag = true;
                                    string ImageFile = string.Empty;
                                    string smallFile = string.Empty;
                                    ImageFile = sTargetFolderPath + "\\" + "/Big_" + objPhoto.PhotoImage;
                                    smallFile = sTargetFolderPath + "\\" + objPhoto.PhotoImage;
                                    foreach (string file in filenames)
                                    {
                                        if ((file.EndsWith("Big_" + objPhoto.PhotoImage)) && (File.Exists(ImageFile)))
                                        {
                                            Foundflag = false; //FlagsAttribute set false for small image
                                            //Code to zip 
                                            ZipEntry entry = new ZipEntry(Path.GetFileName(ImageFile));

                                            entry.DateTime = DateTime.Now;
                                            s.PutNextEntry(entry);

                                            using (FileStream fs = File.OpenRead(ImageFile))
                                            {
                                                int sourceBytes;
                                                do
                                                {
                                                    sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                                    s.Write(buffer, 0, sourceBytes);

                                                } while (sourceBytes > 0);
                                            }
                                            //Code to zip till here 
                                        }
                                    }
                                    if (Foundflag) // if big image is not found.
                                    {
                                        foreach (string file in filenames)
                                        {
                                            if ((file.EndsWith(objPhoto.PhotoImage)) && (File.Exists(smallFile)) && (!(file.EndsWith("Big_" + objPhoto.PhotoImage))))
                                            //(File.Exists(smallFile))
                                            {
                                                ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                                                entry.DateTime = DateTime.Now;
                                                s.PutNextEntry(entry);

                                                using (FileStream fs = File.OpenRead(file))
                                                {
                                                    int sourceBytes;
                                                    do
                                                    {
                                                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                                        s.Write(buffer, 0, sourceBytes);

                                                    } while (sourceBytes > 0);
                                                }
                                            }
                                        }
                                    }
                                }
                                s.Finish();
                                s.Close();
                            }
                            Response.ContentType = "zip";

                            string sfile = sZipFileName + ".zip";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + sfile);

                            Response.TransmitFile(galleryPath + "\\" + sfile);

                            Response.End();

                        }
                        catch  //Exception ex) //  by Ud  
                        {

                        }
                    } 
                    #endregion
                }
            }
        }
    }
    protected void lBtnVieFullPhoto_Click(object sender, EventArgs e)
    {
        MiscellaneousController objMisc = new MiscellaneousController();
        Photos objPhotos = new Photos();
        string[] getPath = CommonUtilities.GetPath();

        StateManager objStateManager = StateManager.Instance;
        objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
        if (Request.QueryString["PhotoId"] != null)
        {
            if (int.TryParse(Request.QueryString["PhotoId"], out _photoId))
            {
                string strOriginalImage = string.Empty;
                string DirBigImage = string.Empty;
                string imagePath = string.Empty;
                objPhotos.PhotoId = _photoId;
                objPhotos = objMisc.GetPhotoDetail(objPhotos);
                Tributes objTributes = objTribute = (Tributes)objStateManager.Get(PortalEnums.SessionValueEnum.TributeSession.ToString(), StateManager.State.Session);
                if (objTributes != null)
                {
                    if (string.IsNullOrEmpty(objTributes.TributePackageType))
                    {
                        _packageId = objMisc.GetTributePackageId(_tributeId);
                    }
                }
                bool isAllowedPhotoCheck = false;
                string tributeEndDate = objMisc.GetTributeEndDate(_tributeId);
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
                isAllowedPhotoCheck = objMisc.IsAllowedPhotoCheck(_photoAlbumId);

                if (((_packageId == 3) || (_packageId == 6) || (_packageId == 7) ||(_packageId == 8)) || ((_packageId == 5) && !isAllowedPhotoCheck && (date2 < DateTime.Now)))
                {
                    if (Equals(objSessionValue, null))//when not logged in
                    {
                        if (IsCustomHeaderOn)
                            topHeight = 198;
                        else
                            topHeight = 88;
                    }
                    else
                    {
                        if (IsCustomHeaderOn)
                            topHeight = 261;
                        else
                            topHeight = 133;
                    }
                    if (Request.QueryString["PhotoId"] != null)
                    {
                        if (int.TryParse(Request.QueryString["PhotoId"], out _photoId))
                            Session["PhotoId"] = _photoId.ToString();
                    }
                    if (WebConfig.ApplicationMode.Equals("local"))
                    {
                        appDomian = WebConfig.AppBaseDomain.ToString();
                    }
                    else
                    {
                        StateManager stateManager = StateManager.Instance;
                        Tributes objTrib = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
                        appDomian = "http://" + objTrib.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";
                    }
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "fnReachLimitExpiryPopup('location.href','document.title','UpgradePhoto','" + _tributeUrl + "','" + _tributeId + "','" + appDomian + "','" + topHeight + "');", true);
                }
                else if (objTributes != null)
                {
                    if (Request.QueryString["TributeUrl"] != null)
                        _tributeUrl = Request.QueryString["TributeUrl"].ToString();

                    string DefaultPath = getPath[0] + "/" + getPath[1] + "/" + _tributeUrl + "_" + objTributes.TypeDescription;
                    //DirectoryInfo objDir = new DirectoryInfo(DirBigImage);
                    DirBigImage = "Big_" + objPhotos.PhotoImage;
                    if (!File.Exists(Path.Combine(DefaultPath, DirBigImage)))
                    {
                        //show big image
                        imagePath = getPath[2] + "/" + _tributeUrl + "_" + objTributes.TypeDescription + "/" + objPhotos.PhotoImage;

                        Page.ClientScript.RegisterStartupScript(GetType(), "open window", "function f(){ window.open('" + imagePath + "'); return false; } f();", true);

                    }
                    else
                     {
                        //show small image

                        imagePath = getPath[2] + "/" + _tributeUrl + "_" + objTributes.TypeDescription + "/" + DirBigImage;

                        Page.ClientScript.RegisterStartupScript(GetType(), "open window", "function f(){ window.open('" + imagePath + "'); return false; } f();", true);
                    }

                }
            }
        }
    }

    #endregion

    #region METHODS

    /// <summary>
    /// Function to set left menu
    /// </summary>
    private void SetMenuOptions()
    {
        try
        {
            UserAdminOwnerInfo objUserInfo = null;
            StateManager objStateManager = StateManager.Instance;

            if (Page.GetType().Name.ToLower() == "guestbook_guestbook_aspx")
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminInfo_GuestBook", StateManager.State.Session);
                objStateManager.Add("TypeName_GuestBook", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.story_story_aspx.ToString())
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get(PortalEnums.AdminInfoEnum.UserAdminInfo_Story.ToString(), StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_Story", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.gift_gift_aspx.ToString())
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get(PortalEnums.AdminInfoEnum.UserAdminInfo_Gift.ToString(), StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_Gift", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.event_manageevent_aspx.ToString())
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get(PortalEnums.AdminInfoEnum.UserAdminInfo_ManageEvent.ToString(), StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_ManageEvent", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.event_event_aspx.ToString())
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get(PortalEnums.AdminInfoEnum.UserAdminInfo_Event.ToString(), StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_Event", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.event_eventfullview_aspx.ToString())
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get(PortalEnums.AdminInfoEnum.UserAdminInfo_EventFullView.ToString(), StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_EventFullView", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.event_inviteguest_aspx.ToString())
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get(PortalEnums.AdminInfoEnum.UserAdminInfo_InviteGuest.ToString(), StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_InviteGuest", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == "notes_tributenotes_aspx")
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminInfo_TributeNote", StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_TributeNotes", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == "notes_managenote_aspx")
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminInfo_ManageNote", StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_AddNote", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == "notes_notefullview_aspx")
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminInfo_NoteFullView", StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_NoteFullView", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == "video_addvideo_aspx")
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminOwnerInfo_AddVideo", StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_AddVideo", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == "video_videogallery_aspx")
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminOwnerInfo_VideoGallery", StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_VideoGallery", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == "video_managevideo_aspx")
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminOwnerInfo_ManageVideo", StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_ManageVideo", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == "photo_managephotoalbum_aspx")
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminOwnerInfo_AddPhotoAlbum", StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_AddPhotoAlbum", objUserInfo.TypeName, StateManager.State.ViewState);
                else if (Session["PhotoAlbumobjUserInfo"] != null)
                {
                    objUserInfo = Session["PhotoAlbumobjUserInfo"] as UserAdminOwnerInfo;
                    if (objUserInfo != null)
                        objStateManager.Add("TypeName_AddPhotoAlbum", objUserInfo.TypeName, StateManager.State.ViewState);
                }

            }
            else if (Page.GetType().Name.ToLower() == "photo_photogallery_aspx")
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminInfo_PhotoGallery", StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_PhotoGallery", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == "photo_photoalbum_aspx")
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminInfo_PhotoAlbum", StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_PhotoAlbum", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == "photo_photoview_aspx")
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminInfo_PhotoFullView", StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_PhotoFullView", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == "photo_managephoto_aspx")
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminOwnerInfo_ManagePhoto", StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_ManagePhoto", objUserInfo.TypeName, StateManager.State.ViewState);
            }
            else if (Page.GetType().Name.ToLower() == "photo_editphotoalbum_aspx")
            {
                objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminOwnerInfo_EditPhotoAlbum", StateManager.State.Session);
                if (objUserInfo != null)
                    objStateManager.Add("TypeName_EditPhotoAlbum", objUserInfo.TypeName, StateManager.State.ViewState);
            }

            if (objUserInfo != null)
            {
                _typeName = objUserInfo.TypeName;
                _tributeId = objUserInfo.TributeId;
                _isAdmin = objUserInfo.IsAdmin;
                _isOwner = objUserInfo.IsOwner;
            }

            SetMenuItemsVisibility();
            SetMenuItemText();
        }
        catch (Exception ex)
        {
             throw ex;
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
    private void SetMenuItemsVisibility()
    {
        try
        {
            liDownloadalbum.Visible = false;
            liVieFullPhoto.Visible = false;

            DivInternal.Visible = true;
            //to get total number of photos in the selected album to disable the Add photos option in number of photos is 60.
            StateManager objStateManager = StateManager.Instance;
            if (objStateManager.Get("TotalPhotosInAlbum", StateManager.State.Session) != null) //to get start photo number in slideshow.
                _totalPhotos = int.Parse(objStateManager.Get("TotalPhotosInAlbum", StateManager.State.Session).ToString());
            else
                _totalPhotos = 0;

            if (_isAdmin)
            {
                divYTMangeTools.Visible = true;
                liChangeSiteTheme.Visible = true;
                liManageTribute.Visible = true;
                liAddToFavorite.Visible = false;
                pLogin.Visible = false;
                liView.Visible = false;
                if (_typeName == "TributeNotes" || _typeName == "VideoGallery")
                {
                    liAdd.Visible = true;
                    liBackTo.Visible = false;
                    liEdit.Visible = false;
                }
                else if (_typeName == "AddNote" || _typeName == "EditNote" || _typeName == "AddVideo" || _typeName == "EditVideo"
                            || _typeName == "AddPhotoAlbum" || _typeName == "AddPhotosToAlbum" || _typeName == "EditPhotoAlbum")
                {
                    liBackTo.Visible = true;
                    DivInternal.Visible = false;
                    liAdd.Visible = false;
                    liEdit.Visible = false;
                }
                else if (_typeName == "NoteFullView" || _typeName == "ManageVideo" || _typeName == "ManageVideoTribute") //for Note full view and Video full view
                {
                    liEdit.Visible = true;
                    liAdd.Visible = false;
                    liBackTo.Visible = false;
                }
                else if (_typeName == PortalEnums.TributeContentEnum.Event.ToString())
                {
                    liEdit.Visible = false;
                    liAdd.Visible = true;
                    liBackTo.Visible = false;
                }
                else if (_typeName == PortalEnums.TributeContentEnum.ManageEvent.ToString())
                {
                    liBackTo.Visible = true;
                    DivInternal.Visible = false;
                    liAdd.Visible = false;
                    liEdit.Visible = false;
                }
                else if (_typeName == PortalEnums.TributeContentEnum.EventFullView.ToString())
                {
                    liBackTo.Visible = false;
                    liAdd.Visible = false;
                    liEdit.Visible = true;
                }
                else if (_typeName == PortalEnums.TributeContentEnum.InviteGuest.ToString())
                {
                    liBackTo.Visible = true;
                    DivInternal.Visible = false;
                    liAdd.Visible = false;
                    liEdit.Visible = false;
                }
                else if (_typeName == "PhotoGallery")
                {
                    liAdd.Visible = true;
                }
                else if (_typeName == "PhotoAlbum")
                {
                    liAdd.Visible = true;
                    if (_totalPhotos >= (int.Parse(WebConfig.MaxPhotosInAlbum_PhotoAlbum)))
                        lbtnAdd.Enabled = false;
                    else
                        lbtnAdd.Enabled = true;
                    liEdit.Visible = true;
                    liView.Visible = true;
                    _photoAlbumId = 0;
                    if (Request.QueryString["PhotoAlbumId"] != null)
                        int.TryParse(Request.QueryString["PhotoAlbumId"].ToString(), out _photoAlbumId);
                    if ((_totalPhotos > 0) && (_photoAlbumId > 0))
                    {
                        liDownloadalbum.Visible = true;
                        liVieFullPhoto.Visible = false;
                    }
                }
                else if (_typeName == "PhotoFullView")
                {
                    liAdd.Visible = false;
                    liEdit.Visible = true;
                    liView.Visible = true;
                    _photoId = 0;
                    if (Request.QueryString["PhotoId"] != null)
                        int.TryParse(Request.QueryString["PhotoId"].ToString(), out _photoId);
                    if ((_totalPhotos > 0) && (_photoId > 0))
                    {
                        liVieFullPhoto.Visible = true;
                        liDownloadalbum.Visible = false;

                    }
                }
                else if (_typeName == "ManagePhoto")
                {
                    liBackTo.Visible = true;
                    liEdit.Visible = false;
                    liAdd.Visible = false;
                    DivInternal.Visible = false;
                    
                }
                
            }
            else if (_userId != 0)
            {
                divYTMangeTools.Visible = false;
                
                liChangeSiteTheme.Visible = false;
                liManageTribute.Visible = false;
                liAddToFavorite.Visible = true;
                pLogin.Visible = false;
                liAdd.Visible = false;
                liBackTo.Visible = false;
                liEdit.Visible = false;
                //divProfile.Visible = true;
                liView.Visible = false;
                if (_typeName == "AddVideo")
                {
                    liBackTo.Visible = true;
                    DivInternal.Visible = false;
                    
                }
                else if (_typeName == "VideoGallery")
                {
                    liAdd.Visible = true;
                }
                else if (_typeName == "ManageVideo")
                {
                    if (_isOwner)
                        liEdit.Visible = true;
                }
                else if (_typeName == "ManageVideoTribute")
                {
                    liEdit.Visible = false;
                }
                else if (_typeName == "AddPhotoAlbum" || _typeName == "AddPhotosToAlbum" || _typeName == "EditPhotoAlbum")
                {
                    liBackTo.Visible = true;
                    DivInternal.Visible = false;
                    DivInternal.Visible = false;
                    
                }
                else if (_typeName == "PhotoGallery")
                {
                    liAdd.Visible = true;
                }
                else if (_typeName == "PhotoAlbum")
                {
                    if (_isOwner)
                    {
                        liAdd.Visible = true;
                        liEdit.Visible = true;
                        liView.Visible = true;
                    }
                    else
                    {
                        liAdd.Visible = true;
                        liEdit.Visible = false;
                        liView.Visible = true;
                    }
                    if (_totalPhotos >= (int.Parse(WebConfig.MaxPhotosInAlbum_PhotoAlbum)))
                        lbtnAdd.Enabled = false;
                    else
                        lbtnAdd.Enabled = true;
                    if (_totalPhotos > 0)
                        liDownloadalbum.Visible = true;
                }
                else if (_typeName == "PhotoFullView")
                {
                    if (_isOwner)
                    {
                        liEdit.Visible = true;
                        liView.Visible = true;
                    }
                    else
                    {
                        liEdit.Visible = false;
                        liView.Visible = true;
                    }
                    if (_totalPhotos > 0)
                    {
                        liVieFullPhoto.Visible = true;
                    }
                }
                else if (_typeName == "ManagePhoto")
                {
                    if (_isOwner)
                    {
                        liBackTo.Visible = true;
                        liEdit.Visible = false;
                        liAdd.Visible = false;
                        DivInternal.Visible = false;
                       
                    }
                }
            }
            else
            {
                divYTMangeTools.Visible = false;
                
                liChangeSiteTheme.Visible = false;
                liManageTribute.Visible = false;
                liAddToFavorite.Visible = false;
               
                liView.Visible = false;
                if (_typeName == "TributeNotes" || _typeName == "NoteFullView" || _typeName == PortalEnums.TributeContentEnum.Story.ToString())
                    pLogin.Visible = false;
                else
                    pLogin.Visible = true;

                liAdd.Visible = false;
                liBackTo.Visible = false;
                liEdit.Visible = false;

                if ((_typeName == PortalEnums.TributeContentEnum.Event.ToString()) ||
                    (_typeName == PortalEnums.TributeContentEnum.ManageEvent.ToString()) ||
                    (_typeName == PortalEnums.TributeContentEnum.EventFullView.ToString()) ||
                    (_typeName == PortalEnums.TributeContentEnum.InviteGuest.ToString()))
                {
                    pLogin.Visible = false;
                }
                if (_typeName == "PhotoAlbum" || _typeName == "PhotoFullView")
                    liView.Visible = true;
                //LHK:(2:33 PM 4/14/2011) added for aurigma upload enhancement 
                if (Request.QueryString["PhotoAlbumId"] != null)
                    int.TryParse(Request.QueryString["PhotoAlbumId"].ToString(), out _photoAlbumId);
                if (Request.QueryString["PhotoId"] != null)
                    int.TryParse(Request.QueryString["PhotoId"].ToString(), out _photoId);

                if ((_totalPhotos > 0) || (_photoAlbumId > 0) || (_photoId > 0))
                {
                    //liVieFullPhoto.Visible = true;
                    if (_typeName == "PhotoAlbum")
                    {
                        liDownloadalbum.Visible = true;
                        liVieFullPhoto.Visible = false;
                    }
                    if (_typeName == "PhotoFullView")
                    {
                        liVieFullPhoto.Visible = true;
                        liDownloadalbum.Visible = false;

                    }
                }
                
                

            }
            //LHK: enhancement internal pages- 16 feb2012
            if (_typeName.ToLower().Contains("guestbook"))
            {
                liCondolences.Visible = false;
                liGifts.Visible = true;
                LiAlbum.Visible = true;
                liVideo.Visible = true;
            }
            if (_typeName.ToLower().Contains("gift"))
            {
                liGuestBook.Visible = true;
                livirtualGift.Visible = false;
                LiAlbum.Visible = true;
                liVideo.Visible = true;
            }
            if (_typeName.ToLower().Contains("photo"))
            {
                liGuestBook.Visible = true;
                liGifts.Visible = true;
                LiAlbum.Visible = false;
                liVideo.Visible = true;
            }
            if (_typeName.ToLower().Contains("video"))
            {
                liGuestBook.Visible = true;
                liGifts.Visible = true;
                LiAlbum.Visible = true;
                liVideo.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method to set text to menu options
    /// </summary>
    /// <param name="typeName">TypeName</param>
    private void SetMenuItemText()
    {
        fbTitle.Content = this.Page.Title.ToString();
        StateManager stateManager = StateManager.Instance;
        string[] virtualDir = CommonUtilities.GetPath();
        MiscellaneousController objMisc = new MiscellaneousController();
        TributesUserInfo _objTributeUserInfo = new TributesUserInfo();
        Tributes objTributes = new Tributes();
        objTributes.TributeId = _tributeId;
        if (_tributeId > 0)
        {
            _objTributeUserInfo.Tributes = objTributes;
            objMisc.GetTributeByID(_objTributeUserInfo);

            Tributes objTrib = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
            try
            {
                if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                {
                    lblGuestBook.Text = "Leave a message";
                    liManageTribute.InnerHtml = "<a href='" + Session["APP_BASE_DOMAIN"] + "AdminMyMomentsPrivacy.aspx?tributeid=" + _tributeId + "&sby=manage'> Manage Website </a>";
                }
                else
                {
                    if (_tributeType != null)
                    {
                        if (_tributeType.ToLower().Equals("memorial"))
                            lblGuestBook.Text = "Leave condolences";
                        else
                            lblGuestBook.Text = "Leave a message";
                    }
                    liManageTribute.InnerHtml = "<a href='" + Session["APP_BASE_DOMAIN"] + "adminmytributesprivacy.aspx?tributeid=" + _tributeId + "&sby=manage'>" + ResourceText.GetString("lbtnManageTributeSite_GB_Master") + "</a>";
                }
                if (_typeName == "GuestBook")
                {
                    pLogin.InnerHtml = ResourceText.GetString("lblloginmsg_GB_Master") + " <a href='javascript: void(0);' onclick='UserLoginModalpopupFromSubDomain(location.href,document.title);'>Log in</a> or <a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>Sign up</a>";
                }
                else if (_typeName == PortalEnums.TributeContentEnum.Story.ToString())
                {
                    pLogin.InnerHtml = ResourceText.GetString("lblloginmsg_ST_Master") + " <a href='javascript: void(0);' onclick='UserLoginModalpopupFromSubDomain(location.href,document.title);'>Log in</a> or <a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>Sign up</a>";

                    if (!string.IsNullOrEmpty(_objTributeUserInfo.Tributes.MessageWithoutHtml.ToString()))
                    {
                        fbDescription = _objTributeUserInfo.Tributes.MessageWithoutHtml.ToString();
                    }
                }
                if (_typeName == PortalEnums.TributeContentEnum.Gift.ToString())
                {
                    pLogin.InnerHtml = ResourceText.GetString("lblloginmsg_GT_Master") + " <a href='javascript: void(0);' onclick='UserLoginModalpopupFromSubDomain(location.href,document.title);'>Log in</a> or <a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>Sign up</a>";
                }
                if (_typeName == PortalEnums.TributeContentEnum.Event.ToString())
                {
                    lbtnAdd.Text = "Add an event";
                }
                if (_typeName == PortalEnums.TributeContentEnum.ManageEvent.ToString())
                {
                    lbtnBackTo.Text = "Back To Event";
                }
                if (_typeName == PortalEnums.TributeContentEnum.EventFullView.ToString())
                {
                    lbtnEdit.Text = "Edit event";
                }
                if (_typeName == PortalEnums.TributeContentEnum.InviteGuest.ToString())
                {
                    lbtnBackTo.Text = "Back To Event";
                }
                #region FOR NOTES PAGES
                else if (_typeName == "TributeNotes")
                {
                    lbtnAdd.Text = ResourceText.GetString("lbtnAddNote_Note_Master");
                }
                else if (_typeName == "AddNote" || _typeName == "EditNote")
                {
                    lbtnBackTo.Text = ResourceText.GetString("lbtnBackToNotes_Note_Master");
                }
                else if (_typeName == "NoteFullView")
                {
                    lbtnEdit.Text = ResourceText.GetString("lbtnEditNote_Note_Master");
                }
                #endregion
                #region FOR VIDEO PAGES
                else if (_typeName == "AddVideo" || _typeName == "EditVideo")
                {
                    lbtnBackTo.Text = ResourceText.GetString("lbtnBackToVideos_Video_Master");
                                    }
                else if (_typeName == "VideoGallery")
                {
                    pLogin.InnerHtml = ResourceText.GetString("lblLogin_VideoGallery_Master") + " <a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>Log in</a>" + ResourceText.GetString("lblOr_ST_Master") + "<a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>Sign up</a>";
                    lbtnAdd.Text = ResourceText.GetString("lbtnAddVideo_Video_Master");
                                    }
                else if (_typeName == "ManageVideo")
                {
                    pLogin.InnerHtml = ResourceText.GetString("lblLoginComment_ManageVideo_Master") + " <a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>Log in</a>" + ResourceText.GetString("lblOr_ST_Master") + "<a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>Sign up</a>";
                    lbtnEdit.Text = ResourceText.GetString("lbtnEditVideo_Video_Master");
                                    }
                else if (_typeName == "ManageVideoTribute")
                {

                    pLogin.InnerHtml = ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments" ? ResourceText.GetString("lblLoginComment_VideoTribute_Master1") : ResourceText.GetString("lblLoginComment_VideoTribute_Master") + " <a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>Log in</a>" + ResourceText.GetString("lblOr_ST_Master") + "<a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>Sign up</a>";
                    lbtnEdit.Text = ResourceText.GetString("lbtnEditVideoTribute_MV");
                                    }
                #endregion
                #region FOR PHOTO PAGES
                else if (_typeName == "AddPhotoAlbum")
                {
                    lbtnBackTo.Text = ResourceText.GetString("lbtnBackToPhotos_Photo_Master");
                }
                else if (_typeName == "AddPhotosToAlbum")
                {
                    lbtnBackTo.Text = "Back to photo album"; //ResourceText.GetString("lbtnBackToPhotos_Photo_Master");
                }
                else if (_typeName == "EditPhotoAlbum")
                {
                    lbtnBackTo.Text = ResourceText.GetString("lbtnBackToPhotos_EditPhoto_Master");
                }
                else if (_typeName == "PhotoGallery")
                {
                    pLogin.InnerHtml = ResourceText.GetString("lblLogin_PhotoGallery_Master") + " <a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>Log in</a>" + ResourceText.GetString("lblOr_ST_Master") + "<a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>Sign up</a>";
                    lbtnAdd.Text = ResourceText.GetString("lbtnCreateAlbum_Photo_Master");
                }
                else if (_typeName == "PhotoAlbum")
                {
                    pLogin.InnerHtml = ResourceText.GetString("txtLogin_PA_Master") + " <a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>Log in</a>" + ResourceText.GetString("lblOr_ST_Master") + "<a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>Sign up</a>";
                    lbtnAdd.Text = ResourceText.GetString("lbtnAddPhotos_PA_Master");
                    lbtnEdit.Text = ResourceText.GetString("lbtnEditAlbum_PA_Master");
                }
                else if (_typeName == "PhotoFullView")
                {
                    pLogin.InnerHtml = ResourceText.GetString("txtLogin_PV_Master") + " <a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>" + ResourceText.GetString("lnkLogin_Master") + "</a>" + ResourceText.GetString("lblOr_ST_Master") + "<a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>" + ResourceText.GetString("lnkSignup_Master") + "</a>";
                    lbtnEdit.Text = ResourceText.GetString("lbtnEditPhoto_PV_Master");
                }
                else if (_typeName == "ManagePhoto")
                {
                    lbtnBackTo.Text = ResourceText.GetString("lbtnBackToPhotos_MP_Master");
                }

                if (objTrib != null)
                {
                    if (string.IsNullOrEmpty(fbDescription))
                        fbDescription = _objTributeUserInfo.Tributes.WelcomeMessage.ToString();
                    if (string.IsNullOrEmpty(fbThumbnail))
                        fbThumbnail = virtualDir[2] + _objTributeUserInfo.Tributes.TributeImage.ToString();
                }
                fbDesc.Content = fbDescription;
                fbThumb.Content = fbThumbnail;
                PageDesc.Content = fbDescription;
                PageThumb.Href = fbThumbnail;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

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


            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
            {
                if (typeName == "TributeNotes")
                    objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtReadEmailWThe_SM_Master") + " " + PageName + " " + ResourceText.GetString("txtOnYT_SM_Master1");
                else if (typeName == "NoteFullView")
                    objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtReadEmail_SM_Master") + " Note " + ResourceText.GetString("txtOnYT_SM_Master1");
                else if (typeName == "Event")
                    objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWTA_SM_Master") + " Events " + ResourceText.GetString("txtOnYT_SM_Master1");
                else if (typeName == "EventFullView")
                    objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtViewEmailEvent_SM_Master") + " Event " + ResourceText.GetString("txtOnYT_SM_Master1");
                else if (typeName == "Gift")
                    objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWTA_SM_Master") + " Gifts " + ResourceText.GetString("txtOnYT_SM_Master1");
                else if (typeName == "GuestBook")
                    objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtReadEmail_SM_Master") + " Guestbook " + ResourceText.GetString("txtOnYT_SM_Master1");
                else if (typeName == "Story")
                    objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtReadEmail_SM_Master") + " Story " + ResourceText.GetString("txtOnYT_SM_Master1");
                else if (typeName == "VideoGallery")
                    objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWTA_SM_Master") + " a Video " + ResourceText.GetString("txtOnYT_SM_Master1");
                else if (typeName == "PhotoGallery")
                    objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWTA_SM_Master") + " Photos " + ResourceText.GetString("txtOnYT_SM_Master1");
                else if (typeName == "ManageVideoTribute")
                    objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtViewEmailWTA_SM_Master") + " a Video Tribute " + ResourceText.GetString("txtOnYT_SM_Master1");
                else
                    objEmail.EmailSubject = txtUserName.Text + " " + ResourceText.GetString("txtViewEmail_SM_Master") + " " + PageName + " " + ResourceText.GetString("txtOnYT_SM_Master1");
            
            }
            else
            {
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
            }

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
            sbEmailbody.Append(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments" ? ResourceText.GetString("txtFrom_SM_Master1") : ResourceText.GetString("txtFrom_SM_Master"));

            objEmail.EmailBody = objEmail.EmailBody + sbEmailbody.ToString();
            stateManager.Add(PortalEnums.SessionValueEnum.ShareTributeEmail.ToString(), objEmail, StateManager.State.Session);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method to set the url to be mailed to user.
    /// </summary>
    private void SetPageNameInSession(string typeName)
    {
        try
        {
            StateManager stateManager = StateManager.Instance;
            string PageName = "";

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
                PageName = PortalEnums.TributeContentEnum.Video.ToString();
            }
            else if (typeName == "ManageVideo" || typeName == "ManageVideoTribute")
            {
                PageName = PortalEnums.TributeContentEnum.Video.ToString();
            }

            stateManager.Add(PortalEnums.SessionValueEnum.SearchPageName.ToString(), PageName, StateManager.State.Session);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method to load themes in the left panel
    /// </summary>
    private void LoadThemes()
    {
        try
        {
            StateManager stateManager = StateManager.Instance;
             MiscellaneousController objMisc = new MiscellaneousController();
            string strPath = "";
            if (Request.RawUrl.Contains("/"))
            {
                strPath = Request.RawUrl.ToString().Substring(0, Request.RawUrl.ToString().LastIndexOf('/'));
                strPath = strPath.Substring(strPath.LastIndexOf('/') + 1);
            }
            if ((strPath != "") && (Request.QueryString["Type"] != null))
            {
                objTribute.TributeUrl = strPath;
                objTribute.TypeDescription = Request.QueryString["Type"].ToString().ToLower().Replace("newbaby", "new baby");
                objTribute = objMisc.GetTributeSessionForUrlAndType(objTribute, WebConfig.ApplicationType.ToString());
            }

            if (objTribute != null)
            {
                if (objTribute.TributeId > 0)
                {
                    _tributeId = objTribute.TributeId;
                    _tributeName = objTribute.TributeName;
                    _tributeType = objTribute.TypeDescription;
                    _tributeUrl = objTribute.TributeUrl;
                }
            }
            Templates objTributeType = new Templates();
            objTributeType.TributeType = _tributeType; // "Wedding";

            int existingTheme = GetExistingTheme().TemplateID;
            MiscellaneousController _controller = new MiscellaneousController();
            List<Templates> lstThemes = _controller.GetThemesFolderList(objTributeType,WebConfig.ApplicationType);

            StringBuilder sbChangeSiteTheme = new StringBuilder();
            foreach (Templates objThemes in lstThemes)
            {
                sbChangeSiteTheme.Append("<div class='yt-Form-Field yt-Form-Field-Radio' id='" + objThemes.ThemeCssClass + "'>"); // + objThemes.TemplateName.Remove(objThemes.TemplateName.IndexOf(" "), 1) + "'>");
                                sbChangeSiteTheme.Append("<input name='rdoTheme' type='radio' runat='server' id='rdo" + objThemes.TemplateID + "' onclick='javascript:Themer(\"" + objThemes.ThemeValue + "\");GetSelectedTheme(" + objThemes.TemplateID + ",\"" + objThemes.ThemeValue + "\");' value='" + objThemes.ThemeValue + "'");
                string appPath = string.Empty;
                if (WebConfig.ApplicationMode.ToLower().Equals("local"))
                {
                    appPath = WebConfig.AppBaseDomain;
                }
                else
                {
                    appPath = string.Format("{0}{1}{2}", "http://www.", WebConfig.TopLevelDomain, "/");
                }
                if (hdnSelectedTheme.Value != string.Empty)
                {
                    if (int.Parse(hdnSelectedTheme.Value) == objThemes.TemplateID)
                    {
                        sbChangeSiteTheme.Append(" Checked='Checked' />");
                        idSheet.Href = appPath + "assets/themes/" + objThemes.FolderName + "/theme.css"; //to set the selected theme
                    }
                    else
                        sbChangeSiteTheme.Append("  />");
                }
                else
                {
                    if (existingTheme == objThemes.TemplateID)
                    {
                        sbChangeSiteTheme.Append(" Checked='Checked' />");
                        idSheet.Href = appPath + "assets/themes/" + objThemes.FolderName + "/theme.css"; //to set the selected theme
                    }
                    else
                        sbChangeSiteTheme.Append("  />");
                }
                sbChangeSiteTheme.Append("<label for='rdo" + objThemes.TemplateID + "'>"); //rdo" + objThemes.TemplateName + "'>");
                sbChangeSiteTheme.Append(objThemes.TemplateName + " <span class='yt-ThemeColorPrimary'></span><span class='yt-ThemeColorSecondary'></span></label>");
                sbChangeSiteTheme.Append("</div>");
            }
            litThemes.Text = sbChangeSiteTheme.ToString();

            stateManager.Add("ThemeOnMaster", lstThemes, StateManager.State.Session);
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    /// <summary>
    /// Method to get the theme for tribute
    /// </summary>
    public Templates GetExistingTheme()
    {
        Tributes objTribute = new Tributes();
        MiscellaneousController _controller = new MiscellaneousController();
        try
        {
            objTribute.TributeId = _tributeId;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _controller.GetThemeForTribute(objTribute);
    }

    /// <summary>
    /// This function will get the values (User Id and Tribute Detail) from the session
    /// </summary>
    private void GetValuesFromSession()
    {
        try
        {
            StateManager objStateManager = StateManager.Instance;
            //to get logged in user name from session as user is logged in user
            objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);

            //LHK:for empty div above body
            if (TributeCustomHeader.Visible)
            {
                if (objSessionValue == null)
                    ytHeader.Visible = false;
            }
            //LHK: till here

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
            if (Request.QueryString["fbmode"] != null)
            {
                if (Request.QueryString["fbmode"] == "facebook")
                {
                    _tributeId = int.Parse(Request.QueryString["TributeId"].ToString());
                    _tributeType = Request.QueryString["TributeType"].ToString();
                    _tributeTypeName = Request.QueryString["TributeType"].ToString().ToLower().Replace("new baby", "newbaby");
                    _tributeName = Request.QueryString["TributeName"].ToString();
                    _tributeUrl = Request.QueryString["TributeUrl"].ToString();
                }
                else
                    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
            }
            else if ((Request.QueryString["videoType"] != null) && (Request.QueryString["videoId"] != null))
            {
                int videoId = int.Parse(Request.QueryString["videoId"].ToString());
                TributesPortal.BusinessLogic.VideoManager videoManager = new TributesPortal.BusinessLogic.VideoManager();
                Videos videos = videoManager.GetVideoTributeDetails(videoId);
                if (!(string.IsNullOrEmpty(objTribute.TypeDescription)))
                {
                    objTribute.TributeName = videos.TributeName;
                    _tributeName = objTribute.TributeName;
                    _isActive = videos.IsTributeActive;
                    _tributeType = objTribute.TypeDescription;
                    _tributeTypeName = objTribute.TypeDescription.ToLower().Replace("new baby", "newbaby");
                    _tributeUrl = Request.QueryString["TributeUrl"].ToString();
                }
            }
            else if (!Equals(objTribute, null))
            {
                if (objTribute.TributeId > 0)
                {

                    _tributeId = objTribute.TributeId;
                    _tributeType = objTribute.TypeDescription;
                    _tributeTypeName = objTribute.TypeDescription.ToLower().Replace("new baby", "newbaby");
                    _tributeName = objTribute.TributeName;
                    _createdDate = objTribute.CreatedDate;
                    _tributeUrl = objTribute.TributeUrl;
                    _isActive = objTribute.IsActive;
                }

                if (Session["tributeEndDate"] != null)
                    _endDate = (DateTime)Session["tributeEndDate"];

            }
            else
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
            }

                    if (Session["TributeCreatedDate"] != null && (Convert.ToDateTime(Session["TributeCreatedDate"])) < Convert.ToDateTime(WebConfig.Launch_Day) && DateTime.Today <= Convert.ToDateTime(WebConfig.Launch_Day).AddDays(180))
            {
                CommonUtilities utility = new CommonUtilities();
                if (objSessionValue != null && objSessionValue.UserId > 0)
                {
                    if (!utility.ReadCookie(objSessionValue.UserId))
                    {
                        utility.CreateCookie(objSessionValue.UserId);
                    }
                }
                else if (objSessionValue == null)
                {
                    if (!utility.ReadCookie(0))
                    {
                        
                        utility.CreateCookie(-1);
                    }
                }
            }


            if (!Equals(objSessionValue, null))
            {
                

                _userId = objSessionValue.UserId;
                _userName = objSessionValue.UserName;
                _firstName = objSessionValue.FirstName;
                _lastName = objSessionValue.LastName;
                _emailID = objSessionValue.UserEmail;
            }
            else
            {
                if (_isActive.Equals(false))
                {
                    SetExpiry();
                }


            }
            //else page number is 1
            if (Request.QueryString["PageNo"] != null)
                currentPage = int.Parse(Request.QueryString["PageNo"].ToString());
            else
                currentPage = 1;

            //to set values to hidden variables for facebook
            hdnTributeId.Value = _tributeId.ToString();
            hdnTributeName.Value = _tributeName;
            hdnTributeType.Value = _tributeType;
            hdnTributeUrl.Value = _tributeUrl;

            if (Session["TributeSession"] == null)
                CreateTributeSession(); //to create the tribute session values if user comest o this page from link or from favorites list.


            if (_packageId != 1)
            {
                if (_isActive.Equals(false))
                {
                    SetExpiry();
                    if (_endDate != null && _endDate < DateTime.Today)
                    {
                        TimeSpan diff = DateTime.Now.Subtract(DateTime.Parse(_endDate.ToString()));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// This method will set the class name of the div on the basis of the Page Name
    /// </summary>
    private void SetClass()
    {

        try
        {
            string className = "";
            if (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.story_story_aspx.ToString())
                className = "yt-Container yt-Channel-Memorial yt-Story yt-Tribute";
            else if (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.gift_gift_aspx.ToString())
                className = "yt-Container yt-Channel-Memorial yt-Gift";
            else if (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.event_manageevent_aspx.ToString())
                className = "yt-Container yt-Event yt-Channel-Memorial";
            else if ((Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.event_event_aspx.ToString()) ||
                      (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.event_eventfullview_aspx.ToString()) ||
                      (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.event_inviteguest_aspx.ToString())
                    )
                className = "yt-Container yt-Event yt-Channel-Memorial";
            else if (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.tribute_sharetribute_aspx.ToString())
                className = "yt-Container yt-ShareTribute";
            else if (Page.GetType().Name.ToLower() == "guestbook_guestbook_aspx")
                className = "yt-Container yt-Channel-Memorial yt-Guest";
            else if (Page.GetType().Name.ToLower() == "notes_tributenotes_aspx")
                className = "yt-Container yt-Channel-Memorial yt-Note";
            else if (Page.GetType().Name.ToLower() == "notes_managenote_aspx")
                className = "yt-Container yt-Note yt-Channel-Memorial";
            else if (Page.GetType().Name.ToLower() == "notes_notefullview_aspx")
                className = "yt-Container yt-Channel-Memorial yt-Note";
            else if (Page.GetType().Name.ToLower() == "video_addvideo_aspx")
                className = "yt-Container yt-Video yt-Channel-Memorial";
            else if (Page.GetType().Name.ToLower() == "video_videogallery_aspx")
                className = "yt-Container yt-Channel-Memorial yt-Video";
            else if (Page.GetType().Name.ToLower() == "video_managevideo_aspx")
                className = "yt-Container yt-Channel-Memorial yt-Video";
            else if (Page.GetType().Name.ToLower() == "photo_managephotoalbum_aspx")
                className = "yt-Container yt-Video yt-Channel-Memorial";
            else if (Page.GetType().Name.ToLower() == "photo_photogallery_aspx")
                className = "yt-Container yt-Channel-Memorial yt-Photo";
            else if (Page.GetType().Name.ToLower() == "photo_photoalbum_aspx")
                className = "yt-Container yt-Channel-Memorial yt-Photo";
            else if (Page.GetType().Name.ToLower() == "photo_photoview_aspx")
                className = "yt-Container yt-Channel-Memorial yt-Video";
            else if (Page.GetType().Name.ToLower() == "photo_managephoto_aspx"
                   || Page.GetType().Name.ToLower() == "photo_editphotoalbum_aspx")
                className = "yt-Container yt-Channel-Memorial yt-Video";

            if (_isActive.Equals(false))
                className += " yt-tribute yt-Expired";
            divMain.Attributes.Add("class", className);

            if ((Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.tribute_sharetribute_aspx.ToString()) ||
                (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.event_inviteguest_aspx.ToString()))
            {
                className = "yt-ContentContainerInner yt-ContentFull";
            }
            else
            {
                className = "yt-ContentContainerInner";
            }
            if (_isActive.Equals(false))
                className += " yt-Expired";
            divContentContainer.Attributes.Add("class", className);
        }
        catch (Exception ex)
        {
         throw ex;
        }
    }
    private void SetExpiry()
    {
        #region commented patch2
        
        #endregion


    }

    /// <summary>
    /// Method to set the class for tabs based on the page
    /// </summary>
    private void SetTabClass()
    {
        try
        {
            if (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.tribute_sharetribute_aspx.ToString())
            {
                divContentSecondary.Visible = false;
            }
            else
            {
                divContentSecondary.Visible = true;
            }

            if (Page.GetType().Name.ToLower() == "story_story_aspx")
                liStory.Attributes.Add("class", "yt-Story selected");
            else if (Page.GetType().Name.ToLower() == "gift_gift_aspx")
                liGifts.Attributes.Add("class", "yt-Gifts selected");
            else if (Page.GetType().Name.ToLower() == "guestbook_guestbook_aspx")
                liGuestBook.Attributes.Add("class", "yt-GuestBook selected");
            else if (Page.GetType().Name.ToLower() == "notes_tributenotes_aspx" || Page.GetType().Name.ToLower() == "notes_managenote_aspx"
                           || Page.GetType().Name.ToLower() == "notes_notefullview_aspx")
            {
                liNotes.Attributes.Add("class", "yt-Notes selected");
                //liNotes2 .Attributes.Add("class", "yt-Notes selected");
            }
            else if (Page.GetType().Name.ToLower() == "video_addvideo_aspx" || Page.GetType().Name.ToLower() == "video_videogallery_aspx"
                            || Page.GetType().Name.ToLower() == "video_managevideo_aspx")
            {
                liVideos.Attributes.Add("class", "yt-Videos selected");

            }
            else if (Page.GetType().Name.ToLower() == "photo_managephotoalbum_aspx" || Page.GetType().Name.ToLower() == "photo_photogallery_aspx"
                        || Page.GetType().Name.ToLower() == "photo_photoalbum_aspx" || Page.GetType().Name.ToLower() == "photo_photoview_aspx"
                   || Page.GetType().Name.ToLower() == "photo_managephoto_aspx" || Page.GetType().Name.ToLower() == "photo_editphotoalbum_aspx")
                liPhotos.Attributes.Add("class", "yt-Photos selected");
            else if ((Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.event_event_aspx.ToString()) ||
                      (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.event_eventfullview_aspx.ToString()) ||
                      (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.event_inviteguest_aspx.ToString()) ||
                      (Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.event_manageevent_aspx.ToString()))
                liEvents.Attributes.Add("class", "yt-Events selected");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method to set the class for left menu items based on page
    /// </summary>
    private void SetMenuItemClass()
    {
        try
        {
            if (Page.GetType().Name.ToLower() == "notes_tributenotes_aspx")
                liAdd.Attributes.Add("class", "yt-Tool-AddNote");
            else if (Page.GetType().Name.ToLower() == "notes_managenote_aspx")
                liBackTo.Attributes.Add("class", "yt-Tool-BackToNotes");
            else if (Page.GetType().Name.ToLower() == "notes_notefullview_aspx")
                liEdit.Attributes.Add("class", "yt-Tool-EditNote");
            else if (Page.GetType().Name.ToLower() == "video_addvideo_aspx")
                liBackTo.Attributes.Add("class", "yt-Tool-BackToVideos");
            else if (Page.GetType().Name.ToLower() == "video_videogallery_aspx")
                liAdd.Attributes.Add("class", "yt-Tool-AddVideo");
            else if (Page.GetType().Name.ToLower() == "video_managevideo_aspx")
                liEdit.Attributes.Add("class", "yt-Tool-EditVideo");
            else if (Page.GetType().Name.ToLower() == "photo_managephotoalbum_aspx")
                liBackTo.Attributes.Add("class", "yt-Tool-BackToPhotos");
            else if (Page.GetType().Name.ToLower() == "photo_photogallery_aspx")
                liAdd.Attributes.Add("class", "yt-Tool-AddAlbum");
            else if (Page.GetType().Name.ToLower() == "photo_photoalbum_aspx")
            {
                liAdd.Attributes.Add("class", "yt-Tool-AddPhoto");
                liEdit.Attributes.Add("class", "yt-Tool-EditAlbum");
            }
            else if (Page.GetType().Name.ToLower() == "photo_photoview_aspx")
                liEdit.Attributes.Add("class", "yt-Tool-EditPhoto");
            else if (Page.GetType().Name.ToLower() == "photo_managephoto_aspx" ||
                        Page.GetType().Name.ToLower() == "photo_editphotoalbum_aspx")
                liBackTo.Attributes.Add("class", "yt-Tool-BackToPhotos");
            else if (Page.GetType().Name.ToLower() == "event_event_aspx")
                liAdd.Attributes.Add("class", "yt-Tool-EditEvent");
            else if (Page.GetType().Name.ToLower() == "event_eventfullview_aspx")
                liEdit.Attributes.Add("class", "yt-Tool-AddEvent");
            else if (Page.GetType().Name.ToLower() == "event_manageevent_aspx")
                liBackTo.Attributes.Add("class", "yt-Tool-BackToEvent");
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
    /// This Function will set the value of the control and error messages from the resource File
    /// </summary>
    private void SetControlsValue()
    {
        try
        {
            
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
    /// Method to check if tribute in user favorite list and set btn string.
    /// </summary>
    private void CheckForFavorite()
    {
        try
        {
            MiscellaneousController _favController = new MiscellaneousController();
            AddToFavorite objFavorite = new AddToFavorite();
            objFavorite.TributeId = _tributeId;
            objFavorite.UserId = _userId;
            isInFavorite = _favController.GetUserTributeFavorites(objFavorite);
            if (isInFavorite > 0)
            {
                lbtnAddToFavorite.Text = ResourceText.GetString("lbtnRemoveFromFavourate_ST_Master");
                toFav = ResourceText.GetString("txtRemoveFromFavorite_ST_Master");                 
                toFavText = ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments" ? ResourceText.GetString("txtRemoveFavorite_ST_Master1"):ResourceText.GetString("txtRemoveFavorite_ST_Master");
            }
            else
            {
                lbtnAddToFavorite.Text = ResourceText.GetString("lbtnAddFav_ST_Master");
                toFav = ResourceText.GetString("txtAddToFavorite_ST_Master");
                toFavText = ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments" ? ResourceText.GetString("txtAddFavorite_ST_Master1") : ResourceText.GetString("txtAddFavorite_ST_Master");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
        objTribute.CreatedDate = _createdDate;
        objTribute.IsActive = _isActive;
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add("TributeSession", objTribute, TributesPortal.Utilities.StateManager.State.Session);
    }

    /// <summary>
    /// Method to get Html for Affiliate links
    /// </summary>
    /// <param name="tributeType">Tribute Type</param>
    public void AffiliateLinks(string tributeType)
    {
        CommonUtilities objUtil = new CommonUtilities();
        //divAddSense.InnerHtml = objUtil.AffiliateLinks(tributeType);
    }

    /// <summary>
    /// Method to send email to the receipents.
    /// </summary>
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

    protected void killFacebookCookies()
    {
        string ApplicationKey = ConfigurationManager.AppSettings["APIKey"];
        string[] fb_cookies = { "user", "session_key", "expires", "ss" };
        foreach (string cookieName in fb_cookies)
        {
            string fullCookieName = string.Format("{0}_{1}", ApplicationKey, cookieName);
            deleteCookie(fullCookieName);
        }
        deleteCookie(ApplicationKey);
    }

    private void deleteCookie(string cookieName)
    {
        if (HttpContext.Current != null
                && HttpContext.Current.Request != null
                && HttpContext.Current.Request.Cookies != null
                && HttpContext.Current.Request.Cookies[cookieName] != null)
        {

            HttpCookie cookie = new HttpCookie(cookieName, string.Empty);
            cookie.Expires = DateTime.Now.AddDays(-1);
            // cookie.Domain = "." + WebConfig.TopLevelDomain;
            Response.Cookies.Add(cookie);
        }
    }
    private void GetCustomHeaderVisible(string _tributeUrl, string ApplicationType)
    {
        IsCustomHeaderOn = false;
        VideoResource objVideoResource = new VideoResource();
        IsCustomHeaderOn = objVideoResource.GetCustomHeaderVisible(_tributeUrl, ApplicationType);
    }

    /// <summary>
    /// Created to construct a application specific url
    /// </summary>
    /// <returns> url </returns>
    private string GetRedirectUrl()
    {
        StateManager stateManager = StateManager.Instance;
        Tributes objTrb = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
        string url = Request.Url.ToString().ToLower();
        if (objTrb != null)
        {
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                if (url.Contains("story.aspx"))
                    url = WebConfig.AppBaseDomain + objTrb.TributeUrl.ToString() + "/story.aspx";
                if (url.Contains("notes.aspx"))
                    url = WebConfig.AppBaseDomain + objTrb.TributeUrl.ToString() + "/notes.aspx";
                if (url.Contains("event.aspx"))
                    url = WebConfig.AppBaseDomain + objTrb.TributeUrl.ToString() + "/events.aspx";
                if (url.Contains("guestbook.aspx"))
                    url = WebConfig.AppBaseDomain + objTrb.TributeUrl.ToString() + "/guestbook.aspx";
                if (url.Contains("gift.aspx"))
                    url = WebConfig.AppBaseDomain + objTrb.TributeUrl.ToString() + "/Gift.aspx";
                if (url.Contains("photogallery.aspx"))
                    url = WebConfig.AppBaseDomain + objTrb.TributeUrl.ToString() + "/photos.aspx";
                if (url.Contains("videogallery.aspx"))
                    url = WebConfig.AppBaseDomain + objTrb.TributeUrl.ToString() + "/videos.aspx";
                //LHK: 4:34 PM 9/19/2011 : for fixing video not redirecting to upgraded url.
                if (url.ToLower().Contains("managevideo.aspx"))
                {
                    string queryStr = Request.QueryString.ToString().Replace("TributeUrl=" + _tributeUrl + "&", "");
                    url = WebConfig.AppBaseDomain + objTrb.TributeUrl.ToString() + "/video.aspx?" + queryStr;//Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf('?')) + "?" + queryStr;
                }
            }
            else
            {
                if (url.Contains("story.aspx"))
                    url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/story.aspx";
                else if (url.Contains("notes.aspx"))
                    url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/notes.aspx";
                else if (url.Contains("event.aspx"))
                    url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/events.aspx";
                else if (url.Contains("guestbook.aspx"))
                    url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/guestbook.aspx";
                else if (url.Contains("gift.aspx"))
                    url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/Gift.aspx";
                else if (url.Contains("photogallery.aspx"))
                    url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/photos.aspx";
                else if (url.Contains("videogallery.aspx"))
                    url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/videos.aspx";
                //LHK: 4:34 PM 9/19/2011 : for fixing video not redirecting to upgraded url.
                else if (url.ToLower().Contains("managevideo.aspx"))
                {
                    string queryStr = Request.QueryString.ToString().Replace("TributeUrl=" + _tributeUrl + "&", "");
                    url = "http://" + objTrb.TypeDescription.ToString().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objTrb.TributeUrl.ToString() + "/video.aspx?" + queryStr;//Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf('?')) + "?" + queryStr;
                }
            }
        }

        return url;

    }


    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void setIsInTopurl(bool inIframe)
    {
      HttpContext.Current.Session["isInIframe"] = inIframe;
    }
    #endregion

}