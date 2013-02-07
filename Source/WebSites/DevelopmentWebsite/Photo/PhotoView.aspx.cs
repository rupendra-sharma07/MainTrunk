///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Photo.PhotoView.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the selected photo to the user and 
///                  also allows to add comment or view as a slideshow
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Photo.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using System.IO;
using System.Text;
using TributesPortal.Miscellaneous;
#endregion

public partial class Photo_PhotoView : PageBase, IPhotoView
{
    #region CLASS VARIABLES
    private PhotoViewPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    public Photos objPhotoDetails = null;
    private int _userId;
    private string _userName;
    private int _tributeId;
    private string _tributeType;
    private string _tributeUrl;
    private int pageSize;
    private int currentPage;
    //private string typeCodeName = "Photo"; //PortalEnums.ModuleName.Notes.ToString();
    private int totalComments;
    private bool isAdmin;
    protected bool _isActive;
    protected int _photoId;
    protected int recordNumber;
    protected string _tributeName;
    protected string photoAlbumId;
    protected string photoAlbumCaption;
    protected string photoCaption;
    protected string xmlPath = string.Empty;

    string[] getPath = CommonUtilities.GetPath();
    string strBigImage = string.Empty;
    string SmallImage = string.Empty;
    string strOldOriginalImage = string.Empty;
    string DirBigImage = string.Empty;
    public int _packageId = 8;
    string appDomian = string.Empty;
    int topHeight;
    bool IsCustomHeaderOn = false;
    private string profile_prefix = CommonUtilities.GetPath()[2].ToString();
    //LHK: WordPress Integration
    private string _TopUrl=string.Empty;
    private bool isInIframe = false;
    public int _tableType = 1;

    #endregion

    #region CONSTANTS
    private const string TYPE_NAME_TO_SAVE = "Photo";
    private const string TYPE_NAME = "PhotoFullView";
    private const string MODULE_FUNCTIONALITY_NAME = "Comment";
    #endregion
    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(Photo_PhotoView));
        this.Form.Action = Request.RawUrl;
        try
        {
            //LHK: 3:59 PM 9/5/2011 - Wordpress topURL
            if (Request.QueryString["topurl"] != null)
            {
                _TopUrl = Request.QueryString["topurl"].ToString();
                Response.Cookies["topurl"].Value = _TopUrl;
                Response.Cookies["topurl"].Domain = _TopUrl;
                Response.Cookies["topurl"].Expires = DateTime.Now.AddHours(4);
            }
            lbtnPost.Attributes.Add("onclick", "setIsInTopurl();");

            GetValuesFromSession(); //to get values of logged in user and selected tribute from session.
            UserIsAdmin();
            SetValuesToControls();
            SetControlsVisibility();

            //Start - Modification on 9-Dec-09 for the enhancement 3 of the Phase 1
            if (_tributeName != null) Page.Title = _tributeName + " | Photo";
            //End

            if (!this.IsPostBack)
            {
                this._presenter.GetPhotoDetails();
                //Page.SetFocus(txtPhotoComment);
            }

            if (Request.QueryString["PhotoId"] != null)
            {
                if (int.TryParse(Request.QueryString["PhotoId"], out _photoId))
                    Session["PhotoId"] = _photoId.ToString();
            }
            MiscellaneousController objMisc = new MiscellaneousController();
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
            isAllowedPhotoCheck = objMisc.IsAllowedPhotoCheckonPhotoId(PhotoId);
            _packageId = objMisc.GetPackIdonPhotoId(PhotoId);
            StateManager objStateManager = StateManager.Instance;
            IsCustomHeaderOn = _presenter.GetCustomHeaderDetail(_tributeId);
            //to get user id from session as user is logged in user
            objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
            if (((_packageId == 5) && !isAllowedPhotoCheck && (date2 < DateTime.Now)))
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


            if (Request.QueryString["View"] != null)
            {

                TributePackage objpackage = new TributePackage();
                Tributes objTributes = objTribute = (Tributes)objStateManager.Get(PortalEnums.SessionValueEnum.TributeSession.ToString(), StateManager.State.Session);
                if (objTributes != null)
                {
                    if (string.IsNullOrEmpty(objTributes.TributePackageType))
                    {
                        _packageId = _presenter.GetTributePackageId(_tributeId);
                    }
                    else
                    {
                        if (objTributes.TributePackageType.Equals("Tribute (Never)"))
                            _packageId = 4;
                        else if (objTributes.TributePackageType.StartsWith("Tribute ("))
                            _packageId = 5;
                    }
                }
                if (!this.IsPostBack)
                {
                   
                    if (WebConfig.ApplicationMode.Equals("local"))
                    {
                        appDomian = WebConfig.AppBaseDomain.ToString();
                    }
                    else
                    {
                        appDomian = "http://" + _tributeType.ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";
                    }
                    IsCustomHeaderOn = _presenter.GetCustomHeaderDetail(_tributeId);
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
                    else if ((objTributes.TributePackageType.Equals("Tribute (Never)")) || (objTributes.TributePackageType.StartsWith("Tribute (")))
                    {
                        if (objTributes != null)
                        {

                            if (Request.QueryString["TributeUrl"] != null)
                                _tributeUrl = Request.QueryString["TributeUrl"].ToString();

                            if (!File.Exists(strBigImage))
                            {
                                //show big image
                                string redirectScript = "<script>window.open('" + strBigImage + "');</script>";
                                Response.Write(redirectScript);

                            }
                            else if (!File.Exists(SmallImage))
                            {
                                //show small image
                                string redirectScript = "<script>window.open('" + SmallImage + "');</script>";
                                Response.Write(redirectScript);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/Error404.aspx");
        }
    }
    protected void rptComments_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            CommentTributeAdministrator drv = (CommentTributeAdministrator)e.Item.DataItem;

            LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
            btnDelete.Text = "Delete"; // ResourceText.GetString("btnDelete_NFV");
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

            StringBuilder html = new StringBuilder();

            HtmlGenericControl itemProfilePicture = (HtmlGenericControl)e.Item.FindControl("itemProfilePicSpn");
            HtmlImage itemprofilepic = (HtmlImage)e.Item.FindControl("itemProfilePicImg");

            if (drv.FacebookUid != null)
            {
                if (Facebook.Web.FacebookWebContext.Current.Session != null)
                {
                    html.Append("<span style='border-bottom:solid 1px white ;border-right:solid 1px white ; width:58px;height:58px; '>");
                    html.Append("<fb:profile-pic uid=\"");
                    html.Append(drv.FacebookUid.ToString());
                    html.Append("\" size=\"square\" facebook-logo=\"true\"></fb:profile-pic></span>");
                    itemProfilePicture.InnerHtml = html.ToString();
                    itemprofilepic.Visible = false;
                }
                else
                {
                    itemprofilepic.Src = "http://graph.facebook.com/" + drv.FacebookUid.ToString() + "/picture?type=square";
                    itemProfilePicture.Visible = false;
                }
            }
            else 
            {
                string imgsrc = "";
                html.Append("<img style='border-bottom:solid 1px white ;border-right:solid 1px white ; width:48px; ' src='");
                if (drv.UserImage.StartsWith("http://") || drv.UserImage.StartsWith("https://"))
                {
                    imgsrc = drv.UserImage.ToString();
                }
                else
                {
                    imgsrc = CommonUtilities.GetPath()[2].ToString() + drv.UserImage.ToString();
                }
                html.Append(imgsrc);
                html.Append("' alt='Photo of ");
                html.Append(drv.UserName.ToString());
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

    protected void rptComments_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            Comments objComment = new Comments();
            objComment.CommentId = int.Parse(e.CommandArgument.ToString());
            objComment.UserId = _userId;
            objComment.TableType = _tableType.ToString();
            this._presenter.DeleteComment(objComment);
            //SetControlsVisibiltiy();
            this._presenter.GetPhotoDetails();
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    //for wordpress 
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SetSessionTopurl(bool inIframe)
    {
        Session["isInIframe"] = inIframe;
    }

    protected void lbtnPost_Click(object sender, EventArgs e)
    {
        try
        {
            //LHK: 3:59 PM 9/5/2011 - Wordpress topURL
            if (Request.QueryString["topurl"] != null)
            {
                _TopUrl = Request.QueryString["topurl"].ToString();
            }
            if (Session["isInIframe"] != null)
            {
                isInIframe = bool.Parse(Session["isInIframe"].ToString());
                Session["isInIframe"] = null;
            }
            if (!(string.IsNullOrEmpty(_TopUrl)) && isInIframe)
            {
                this._presenter.SaveComment(GetCommentDataToSave(), _TopUrl);
            }
            else
            {
                this._presenter.SaveComment(GetCommentDataToSave());
            }
            
            string queryString = "?PageNo=1&PhotoId=" + _photoId; //"?TributeId=" + _tributeId + "&TributeName=" + _tributeName + "&TributeType=" + _tributeType + "&PageNo=1";
            //Response.Redirect("photo.aspx" + queryString, false);
            Response.Redirect(Context.Request.RawUrl.Split("?".ToCharArray())[0] + queryString, false);
            //Redirect.RedirectToPage(Redirect.PageList.PhotoView.ToString())
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }
    protected void lbtnPre_Click(object sender, EventArgs e)
    {
        try
        {
            StateManager stateManager = StateManager.Instance;
            objPhotoDetails = (Photos)stateManager.Get("PhotoDetails", StateManager.State.Session);
            if (objPhotoDetails == null)
            {
                this._presenter.GetPhotoDetails();
                objPhotoDetails = (Photos)stateManager.Get("PhotoDetails", StateManager.State.Session);
            }
            int prevPhotoId = int.Parse(objPhotoDetails.PrevRecordCount.ToString()); //to get the previous photo id to be displayed

            Response.Redirect("~/" + Session["TributeURL"] + "/photo.aspx" + "?PhotoId=" + prevPhotoId, false);
            //Redirect.RedirectToPage(Redirect.PageList.PhotoView.ToString()) 
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void lbtnNext_Click(object sender, EventArgs e)
    {
        try
        {
            StateManager stateManager = StateManager.Instance;
            objPhotoDetails = (Photos)stateManager.Get("PhotoDetails", StateManager.State.Session);
            if (objPhotoDetails == null)
            {
                this._presenter.GetPhotoDetails();
                objPhotoDetails = (Photos)stateManager.Get("PhotoDetails", StateManager.State.Session);
            }
            int nextPhotoId = int.Parse(objPhotoDetails.NextRecordCount.ToString()); //to get the previous photo id to be displayed
            Response.Redirect("~/" + Session["TributeURL"] + "/photo.aspx" + "?PhotoId=" + nextPhotoId, false);
            //Redirect.RedirectToPage(Redirect.PageList.PhotoView.ToString())
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public PhotoViewPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    public int PhotoId
    {
        get
        {
            return _photoId;
        }
    }
    public int UserId
    {
        get
        {
            return _userId;
        }
    }
    public int TributeId
    {
        get
        {
            return _tributeId;
        }
    }
    public int PageSize
    {
        get
        {
            return pageSize;
        }
        set
        {
        }
    }
    public int CurrentPage
    {
        get
        {
            return currentPage;
        }
    }

    public Photos PhotoDetails
    {
        set
        {
            string photoName = value.PhotoCaption == string.Empty ? "Photo" : value.PhotoCaption; //to display Photo if not caption for photo exists
            string breadcrumbCaption = string.Empty;
            if (value.PhotoAlbumCaption.Length != 0 && value.PhotoAlbumCaption.Length > 25)
                breadcrumbCaption = value.PhotoAlbumCaption.Remove(25) + "...";
            else
                breadcrumbCaption = value.PhotoAlbumCaption; //to display album name in navigation bar
            nvgPhoto.InnerHtml = "<a href='" + Session["APP_PATH"] + _tributeUrl + "/" + "'>" + ResourceText.GetString("nvgTributeHome_PV") + "</a> <a href='photos.aspx'>" + ResourceText.GetString("nvgPhotos_PV") + "</a> <a href='photoalbum.aspx?photoAlbumId=" + value.PhotoAlbumId + "'>" + breadcrumbCaption + "</a><span class='selected'>" + photoName + "</span>";
            photoAlbumCaption = value.PhotoAlbumCaption;
            hPhotoCaption.InnerHtml= value.PhotoCaption == string.Empty ? "Photo" : value.PhotoCaption;

            //Set Master Variable value
            if (Request.QueryString["TributeUrl"] != null)
                _tributeUrl = Request.QueryString["TributeUrl"].ToString();
            if (value.PhotoImage.Contains("_Anniversary"))
            {
                strBigImage = value.PhotoImage.Replace("_Anniversary/", "_Anniversary/Big_");
            }
            if (value.PhotoImage.Contains("_Birthday"))
            {
                strBigImage = value.PhotoImage.Replace("_Birthday/", "_Birthday/Big_");
            }
            if (value.PhotoImage.Contains("_Graduation"))
            {
                strBigImage = value.PhotoImage.Replace("_Graduation/", "_Graduation/Big_");
            }
            if (value.PhotoImage.Contains("_Memorial"))
            {
                strBigImage = value.PhotoImage.Replace("_Memorial/", "_Memorial/Big_");
            }
            if (value.PhotoImage.Contains("_New_Baby"))
            {
                strBigImage = value.PhotoImage.Replace("_New_Baby/", "_New_Baby/Big_");
            }
            if (value.PhotoImage.Contains("_Wedding"))
            {
                strBigImage = value.PhotoImage.Replace("_Wedding/", "_Wedding/Big_");
            }           
            SmallImage = value.PhotoImage;
            if (!(string.IsNullOrEmpty(_tributeUrl)))
            {
                if (File.Exists(strBigImage)) //if directory does not exists creates a directory
                    Master._OriginalImageUrl = strBigImage;
                else
                    Master._OriginalImageUrl = value.PhotoImage;
            }

            string[] imageTest = strBigImage.Split('/');
            string[] imagePath = CommonUtilities.GetPath();
            string bigImage = imagePath[0] + "/" + imagePath[1] + "/" + _tributeUrl.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_") + "/" + imageTest[imageTest.Length-1];
            if (!File.Exists(bigImage))
            {
                strBigImage = value.PhotoImage;
            }
            //getting package id
            StateManager objStateManager = StateManager.Instance;
            TributePackage objpackage = new TributePackage();
            Tributes objTributes = objTribute = (Tributes)objStateManager.Get(PortalEnums.SessionValueEnum.TributeSession.ToString(), StateManager.State.Session);
            if (objTributes != null)
            {
                if (string.IsNullOrEmpty(objTributes.TributePackageType))
                {
                    _packageId = _presenter.GetTributePackageId(_tributeId);
                }
                else
                {
                    if (objTributes.TributePackageType.Equals("Tribute (Never)"))
                        _packageId = 4;
                    else if (objTributes.TributePackageType.StartsWith("Tribute ("))
                        _packageId = 5;
                }
            }
            MiscellaneousController objMisc = new MiscellaneousController();
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
            isAllowedPhotoCheck = objMisc.IsAllowedPhotoCheckonPhotoId(PhotoId);
            _packageId = objMisc.GetPackIdonPhotoId(PhotoId);

            //till here 
            // Ashu : Set width & height of PhotoImage
            if ((_packageId == 3) || (_packageId == 6) || (_packageId == 7) || (_packageId == 8)||(((_packageId == 5) && !isAllowedPhotoCheck && (date2 < DateTime.Now))))
            {
                divPhotoDisplay.InnerHtml = "<a href='photo.aspx?PhotoId=" + value.PhotoId.ToString() + "&view=true'> <img src='" + value.PhotoImage + "' alt='' class='yt-MediaImgSize'/></a>";
               
                // New added on june 07, 2011 to handle Pop openning problem in Appale safari by Rupendra
                //HtmlAnchor _anchr = new HtmlAnchor();
                //_anchr = (HtmlAnchor)this.Master.FindControl("anchrVieFullPhoto");
                //if (_anchr != null)
                //_anchr.HRef = value.PhotoImage;
            }
            else
            {
                divPhotoDisplay.InnerHtml = "<a href='" + strBigImage + "' target='_blank'>  <img src='" + value.PhotoImage + "' alt='' class='yt-MediaImgSize'/></a>";
                HtmlGenericControl _anchr = new HtmlGenericControl();
                _anchr = (HtmlGenericControl)this.Master.FindControl("anchrVieFullPhoto");
                if (_anchr != null)
                {
                    _anchr.InnerHtml = "<a href='" + strBigImage + "' target='_blank' href='" + value.PhotoImage + "'>View Full size Photo</a>";
                    _anchr.Visible = true;
                    _anchr.Style.Add(HtmlTextWriterStyle.Display, "block"); 

                }
                LinkButton _link = new LinkButton();
                _link = (LinkButton)this.Master.FindControl("lBtnVieFullPhoto");
                if (_link != null)
                    _link.Visible = false;
               // _anchr.Attributes.Add("onClick","");

            }
            if (!string.IsNullOrEmpty(value.PhotoDesc.ToString()))
                Master.fbDescription = value.PhotoDesc.ToString();
            if (!string.IsNullOrEmpty(value.PhotoImage.ToString()))
                Master.fbThumbnail = value.PhotoImage.ToString();

            pPhotoDesc.InnerHtml = value.PhotoDesc.Replace("\n", "</br>");
            pUploadedBy.InnerHtml = "Uploaded by <a href='javascript:void(0);' onclick=\"UserProfileModal_1('" + value.CreatedBy + "');\">" + value.UserName + "</a> on " + value.CreationDate;

            //to add values of video details to session for previous and next button
            objStateManager.Add("PhotoDetails", value, StateManager.State.Session);
        }
    }
    public List<CommentTributeAdministrator> Comments
    {
        set
        {
            if (value.Count == 0 && currentPage > 1)
            {
                //string queryString = "?TributeId=" + _tributeId + "&TributeName=" + _tributeName + "&TributeType=" + _tributeType + "&PageNo=" + (currentPage - 1) + "&PhotoId=" + _photoId;
                string queryString = "?PageNo=" + (currentPage - 1) + "&PhotoId=" + _photoId;
                Response.Redirect("~/" + Session["TributeURL"] + "/photo.aspx" + queryString, false);
            }
            rptComments.DataSource = value;
            rptComments.DataBind();
        }
    }
    public string SetRecordCount
    {
        set
        {
            spRecordCount.InnerText = value;
        }
    }
    public int NextCount
    {
        set
        {
            if (value > 0)
                lbtnNext.Enabled = true;
            else
                lbtnNext.Enabled = false;
        }
    }
    public int PrevCount
    {
        set
        {
            if (value > 0)
                lbtnPre.Enabled = true;
            else
                lbtnPre.Enabled = false;
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
    public string DrawPaging
    {
        set
        {
            spanPagingTop.InnerHtml = value;
            spanPagingBottom.InnerHtml = value;
        }
    }
    public string XmlFilePath
    {
        set
        {
            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add("XMLFilePath", value, StateManager.State.Session);
            xmlPath = value;
        }
    }
    public int RecordNumber
    {
        set
        {
            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add("SlideShowStartPhoto", value, StateManager.State.Session);
            recordNumber = value;
        }
    }
    public int CommentCount
    {
        get
        {
            return totalComments;
        }
        set
        {
            totalComments = value;
            //to set the visibility of paging and message if no note exists for the tribute.
            if (value > 0)
            {
                //divMessage.Visible = false;
                divPagingTop.Visible = true;
                divCommentsList.Visible = true;
                divPagingBottom.Visible = true;
                //divComments.Visible = true;
            }
            else
            {
                //divMessage.Visible = true;
                divPagingTop.Visible = false;
                divCommentsList.Visible = false;
                divPagingBottom.Visible = false;
                //divMessage.InnerText = ResourceText.GetString("strNoCommentMessage_PV");
            }
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
    /// Method to return the filled comment object to save
    /// </summary>
    /// <returns>Filled Comments entity</returns>
    public Comments GetCommentDataToSave()
    {
        Comments objComment = new Comments();
        objComment.UserId = _userId;
        objComment.CodeTypeName = TYPE_NAME_TO_SAVE;
        objComment.ModuleFunctionalityName = MODULE_FUNCTIONALITY_NAME;
        objComment.TypeCodeId = 0; //typeCodeId;
        objComment.TributeId = _tributeId;
        objComment.CommentTypeId = _photoId;
        objComment.CreatedBy = _userId;
        objComment.CreatedDate = DateTime.Now;
        objComment.IsActive = true;
        objComment.IsDeleted = false;
        objComment.Message = replaceSpecialCharacter(txtPhotoComment.Text.ToString().Trim());
        objComment.UserName = _userName;
        objComment.TributeName = _tributeName;
        objComment.TributeType = _tributeType;
        objComment.TributeUrl = _tributeUrl;
        objComment.PathToVisit = Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;
        return objComment;
    }

    private void GetValuesFromSession()
    {
        StateManager objStateManager = StateManager.Instance;

        //to get user id from session as user is logged in user
        objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionValue, null))
        {
            _userId = objSessionValue.UserId;
            _userName = objSessionValue.FirstName == string.Empty ? objSessionValue.UserName : (objSessionValue.FirstName + " " + objSessionValue.LastName);
        }

        //to get photo id from query string
        if (Request.QueryString["PhotoId"] != null) //to pick value of selected note from querystring
        {
            _photoId = int.Parse(Request.QueryString["PhotoId"].ToString());
            objStateManager.Add("PhotoViewSession", _photoId, StateManager.State.Session);
        }
        else if (objStateManager.Get("PhotoViewSession", StateManager.State.Session) != null)
        {
            _photoId = int.Parse(objStateManager.Get("PhotoViewSession", StateManager.State.Session).ToString());
        }

        objTribute = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);

        pageSize = (int.Parse(WebConfig.Pagesize_Notes_Comments));

        //to get current page number, if user clicks on page number in paging it gets tha page number from query string
        //else page number is 1
        if (Request.QueryString["PageNo"] != null)
            currentPage = int.Parse(Request.QueryString["PageNo"].ToString());
        else
            currentPage = 1;

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
            _tributeId = objTribute.TributeId;
            _tributeName = objTribute.TributeName;
            _tributeType = objTribute.TypeDescription;
            _tributeUrl = objTribute.TributeUrl;
            _isActive = objTribute.IsActive;
        }
        else
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);

        if (Session["TributeSession"] == null)
            CreateTributeSession(); //to create the tribute session values if user comest o this page from link or from favorites list.

        objPhotoDetails = (Photos)objStateManager.Get("PhotoDetails", StateManager.State.Session);
        // get package id 
        //TributePackage objpackage = new TributePackage();
        //objpackage.UserTributeId = objTribute.TributeId;
        //object[] param = { objpackage };
        //_packageId = _presenter.TriputePackageId(param);

    }

    /// <summary>
    /// Method to get user is admin or owner
    /// </summary>
    private bool UserIsAdmin()
    {
        UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
        objUserInfo.UserId = _userId;
        objUserInfo.TributeId = _tributeId;
        objUserInfo.TypeId = _photoId;
        objUserInfo.TypeName = TYPE_NAME;

        if (_userId != 0)
        {
            if (IsUserAdmin(objUserInfo))
            {
                objUserInfo.IsAdmin = true;
                isAdmin = true;
            }
            else if (IsUserOwner(objUserInfo))
            {
                objUserInfo.IsOwner = true;
                isAdmin = false;
            }
        }
        else
        {
            objUserInfo.IsAdmin = false;
            objUserInfo.IsOwner = false;
            isAdmin = false;
        }
        StateManager objStateManager = StateManager.Instance;
        objStateManager.Add("UserAdminInfo_PhotoFullView", objUserInfo, StateManager.State.Session);
        return isAdmin;
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

    private void SetValuesToControls()
    {
        hPhoto.InnerText = ResourceText.GetString("lblPhotoViewHeader_PV");
        lbtnPre.Text = ResourceText.GetString("lbtnPrevious_PV");
        lbtnNext.Text = ResourceText.GetString("lbtnNext_PV");
        lblToComment.InnerText = ResourceText.GetString("lblCommentHeader_PV");
        lbtnPost.Text = ResourceText.GetString("lbtnPostComment_PV");
        spanPageTop.InnerText = ResourceText.GetString("lblPage_PV");
        spanPageBottom.InnerText = ResourceText.GetString("lblPage_PV");
        divLogin.InnerHtml = ResourceText.GetString("lblLogin_PV") + " " + "<a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>" + ResourceText.GetString("lbtnLogin_PV") + "</a>" + " " + ResourceText.GetString("lblOr_PV") + " " + "<a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>" +ResourceText.GetString("lbtnSignUp_PV") + "</a>";
        rfvMessage.ErrorMessage = ResourceText.GetString("errCommentRequired_PV"); ;
        cvMessage.ErrorMessage = ResourceText.GetString("errCommentLength_PV");

        string tributeHome;
        string photoUrl;
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
        photoUrl = tributeHome + "Photo.aspx";

        string query_string = string.Empty;
        //if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
        //{
        //    query_string = "?PageNo=" + HttpUtility.UrlEncode(_tributeType);
        //    photoUrl = photoUrl + query_string;
        //    tributeHome = tributeHome + query_string;
        //}
        photoUrl += (photoUrl.Contains("?") ? "&" : "?") + "PhotoId=" + _photoId.ToString();

        photoWallTributeHome.Text = tributeHome;
        photoWallTributeHome1.Text = tributeHome;


        photoWallPostSubject.Text = string.Format("{0} commented on a photo on the: {1} {2} Tribute", _userName, _tributeName, _tributeType);
        photoWallLink.Text = photoUrl ;        
        photoWallLink1.Text = photoUrl;
        photoWallTributeType.Text = _tributeType;
        if (objPhotoDetails  != null)
        {
            photoWallThumbnail.Text = "http://your-tribute-dev.dyndns.org/TributePhotos/images/baby_TributePhoto.jpg";
                //"http://img.youtube.com/vi/" + objPhotoDetails.PhotoId + "/default.jpg";
        }

    }

    // Ashu: MADE A METHOD to REPLACE SPECIAL CHARCAHTERS 
    private string replaceSpecialCharacter(string StrName)
    {
        StrName = StrName.Replace("%", "&#37");
        StrName = StrName.Replace("$", "&#36");
        StrName = StrName.Replace("'", "&#39");
        StrName = StrName.Replace("(", "&#40");
        StrName = StrName.Replace(")", "&#41");
        StrName = StrName.Replace("*", "&#42");
        StrName = StrName.Replace("+", "&#43");
        StrName = StrName.Replace(",", "&#44");
        StrName = StrName.Replace("-", "&#45");
        StrName = StrName.Replace(".", "&#46");
        StrName = StrName.Replace("/", "&#47");
        StrName = StrName.Replace(":", "&#58");
        StrName = StrName.Replace(";", "&#59");
        StrName = StrName.Replace("<", "&#60");
        StrName = StrName.Replace("=", "&#61");
        StrName = StrName.Replace(">", "&#62");
        StrName = StrName.Replace("?", "&#63");
        StrName = StrName.Replace("@", "&#64");
        StrName = StrName.Replace("[", "&#91");
        StrName = StrName.Replace("\\", "&#92");
        StrName = StrName.Replace("]", "&#93");
        StrName = StrName.Replace("^", "&#94");
        StrName = StrName.Replace("_", "&#95");
        StrName = StrName.Replace("{", "&#123");
        StrName = StrName.Replace("|", "&#124");
        StrName = StrName.Replace("}", "&#125");
        StrName = StrName.Replace("`", "&#180");
        StrName = StrName.Replace("~", "&#126");

        return StrName;
    }

    /// <summary>
    /// Method to set the control visibility
    /// </summary>
    private void SetControlsVisibility()
    {
        //to set visibility of Post comment section
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
    }
    #endregion

}