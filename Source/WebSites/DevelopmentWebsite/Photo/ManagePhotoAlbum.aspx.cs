///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Photo.ManagePhotoAlbum.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to select photos to be uploaded to the photo album
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Photo.Views;
using TributesPortal.Utilities;
using Aurigma.ImageUploader;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using TributesPortal.ResourceAccess;
using Facebook.Web;
#endregion

public partial class Photo_ManagePhotoAlbum : PageBase, IManagePhotoAlbum
{
    #region CLASS VARIABLES
    private ManagePhotoAlbumPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    private int _userId = 0;
    private bool isUserAdmin;
    private int _tributeId;
    private string _tributeType;
    private string _mode;
    private string _photoAlbumName;
    private string _result;
    protected int _photoAlbumId;
    protected string _tributeName;
    private string _tributeUrl;
    public IList<Photos> objListPhoto = new List<Photos>();
    private string galleryPath = string.Empty;
    private string galleryPathForThumbnails = string.Empty;
    private string galleryPathForBigImages = string.Empty;
    private int result;
    private string _userName;
    public bool AddPhotoEmail = false;
    Random objRand = new Random();
    static int filecount;

    enum AnchorPosition
    {
        Top,
        Center,
        Bottom,
        Left,
        Right
    }


    #endregion

    #region CONSTANTS
    private const string MODULE_TYPE_NAME = "Photo";
    private const int MIN_RAND_VALUE_FOR_FILENAME = 123456789;
    private const int MAX_RAND_VALUE_FOR_FILENAME = 999999999;
    private const string PAGE_MODE_ADD_PHOTOS = "addphotos";
    private const string TYPE_NAME_CREATE_ALBUM = "AddPhotoAlbum";
    private const string TYPE_NAME_ADD_PHOTOS = "AddPhotosToAlbum";
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Master != null)
            {
                HtmlGenericControl currdiv = (HtmlGenericControl)Master.FindControl("divContentSecondary");
                currdiv.Style.Add("display", "none");
                HtmlGenericControl containerDiv = (HtmlGenericControl)Master.FindControl("cotentPrimaryContainer");
                containerDiv.Style.Add("margin-left", "-223px");
            }
            Ajax.Utility.RegisterTypeForAjax(typeof(Photo_ManagePhotoAlbum));
            GetPhotoSessionValues();
            CreatePath();

            var fbWebContext = FacebookWebContext.Current;
            if (FacebookWebContext.Current.Session != null)
            {
                Session["facebookflag"] = "true";
            }
            else
            {
                Session["facebookflag"] = "false";
            }



            //Set the Photo Upload Key into the Hidden field
            hdnPhotoUploaderKey.Value = WebConfig.PhotoUploaderKey;

            // To Retain last value of text box   by UD
            GetSessionValues();
            this._presenter.GetPhotoAlbumList();
            if (!this.IsPostBack)
            {
                lbtnSavePhoto.Text = "Update photo album";
                /* Made sessions to retain the value of albumid & albumname by Ashu ---*/
                if (Request.QueryString["albummode"] != null)
                {
                    if (Request.QueryString["albummode"].ToString() == "Create")
                    {
                        Session["PhotoAlbumId"] = null;
                        Session["PhotoAlbumName"] = null;
                        //HttpContext.Current.Session["AlbumName"] = null;
                        //HttpContext.Current.Session["AlbumDesc"] = null;
                        lbtnSavePhoto.Text = "Create photo album";
                    }
                }
                if (Request.QueryString["photoAlbumId"] != null)
                {
                    _photoAlbumId = 0;
                    int.TryParse(Request.QueryString["photoAlbumId"], out _photoAlbumId);
                    Session["PhotoAlbumId"] = _photoAlbumId.ToString();

                    StateManager objStateManager = StateManager.Instance;
                    if (objStateManager.Get("PhotoAlbumName", StateManager.State.Session) != null)
                        Session["PhotoAlbumName"] = objStateManager.Get("PhotoAlbumName", StateManager.State.Session).ToString();
                    else
                    {
                        Photos objPhotos = new Photos();
                        objPhotos.PhotoAlbumId = _photoAlbumId;
                        Session["PhotoAlbumName"] = _presenter.GetPhotoAlbumDetails(objPhotos);

                    }
                }
                /*--------------------------------*/
                AddPhotoEmail = true;
                SetControlsVisibility();
                SetValuesToControls();
                if (_mode == PAGE_MODE_ADD_PHOTOS)
                    this._presenter.GetPhotoCount();
                else
                    hdnAlbumId.Value = string.Empty;

            }
            //if (HttpContext.Current.Session["AlbumName"] != null || HttpContext.Current.Session["AlbumDesc"] != null)
            //{
            //    txtAlbumName.Text = HttpContext.Current.Session["AlbumName"].ToString();
            //    txtAlbumDesc.Text = HttpContext.Current.Session["AlbumDesc"].ToString();
            //}
            UserIsAdminOrOwner(); //to set the visibility of options in side menu.
            Page.SetFocus(txtAlbumName);

        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (_mode == PAGE_MODE_ADD_PHOTOS)
            {
                Response.Redirect("~/" + Session["TributeURL"] + "/photoalbum.aspx" + "?photoAlbumId=" + _photoAlbumId, false);
            }
            else
            {
                Response.Redirect("~/" + Session["TributeURL"] + "/photos.aspx", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
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

    public static string ExtractHtmlInnerText(string htmlText)
    {
        Regex regex = new Regex("(<.*?>\\s*)+", RegexOptions.Singleline);
        string resultText = regex.Replace(htmlText, " ").Trim();
        return resultText;
    }

    protected void lbtnCreateAlbum_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        string images = ExtractHtmlInnerText(hdnImageNames.Value);
        string[] imageNames = images.Split(',');

        try
        {
            #region  SaveImage
            objListPhoto = new List<Photos>();
            //string[] photo;
            Photos objPhoto = new Photos();
            string[] getPath = CommonUtilities.GetPath();
            string tempPath = getPath[0] + "/" + getPath[1] + "/" + _tributeUrl.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_") + "/" + "temp_" + _userId;

            if (Session["PhotoAlbumId"] != null)
            {

                int.TryParse(Session["PhotoAlbumId"].ToString(), out _photoAlbumId);
                PhotoAlbumID = _photoAlbumId;
            }

            if (HttpContext.Current.Session["FileCount"] != null && HttpContext.Current.Session["FileCount"] != "")
            {
                int.TryParse(HttpContext.Current.Session["FileCount"].ToString(), out filecount);
                HttpContext.Current.Session["FileCount"] = null;
            }

            if (PhotoAlbumID == 0)
            {
                _photoAlbumName = txtAlbumName.Text;
                _presenter.CreateAlbum();
                AddPhotoEmail = false;
            }

            if (PhotoAlbumID > 0)
            {
                foreach (var imageName in imageNames)
                {
                    int thumbnailCount = 2;
                    string[] thumbnailNames = new string[thumbnailCount];
                    string RandomFileName = ""; // variable to get random number  // by ud

                    #region MyRegion


                    if (!string.IsNullOrEmpty(imageName))
                    {
                        Image thumbnailFile = new Bitmap(tempPath + "//" + imageName.Trim());

                        // Save thumbnail

                        //string thumbnailName = imageName.Trim();
                        //thumbnailName = thumbnailName.Replace("'", "_");
                        //thumbnailName = thumbnailName.Replace("`", "_");
                        //thumbnailName = thumbnailName.Replace(" ", "_");
                        //thumbnailName = thumbnailName.Replace("@", "");
                        //thumbnailName = thumbnailName.Replace("#", "");
                        //thumbnailName = thumbnailName.Replace("$", "");
                        //thumbnailName = thumbnailName.Replace("%", "");
                        //thumbnailName = thumbnailName.Replace("^", "");
                        //thumbnailName = thumbnailName.Replace("&", "");
                        //thumbnailName = thumbnailName.Replace(",", "");
                        //thumbnailName = thumbnailName.Replace("!", "");


                        // get the random number to concatenate with filename   by ud

                        //thumbnailName = thumbnailName.Replace("_Thumbnail0.jpg", "");
                        //RandomFileName = GetSafeFileName(thumbnailName);
                        RandomFileName = "DSC" + DateTime.Now.ToString("yyyyMMddHHmmssffff")+".jpg";
                        // concatenate filename by ud
                        objPhoto.PhotoImage = RandomFileName;
                        string fileName = objPhoto.PhotoImage;
                        //Get first thumbnail and save it to disk. It stores 480x360 image.
                        thumbnailFile.Save(galleryPath + "/" + fileName); // .jpg
                        thumbnailFile.Dispose();
                        //Get the second thumbnail and save it to disk. It stores 120x120 preview image. 
                        System.Drawing.Image PhotoImg =
                            System.Drawing.Image.FromFile(galleryPath + "/" + fileName);
                        System.Drawing.Image imgPhoto = Crop(PhotoImg, 100, 100, AnchorPosition.Center);
                        imgPhoto.Save(galleryPathForThumbnails + "/" + fileName);
                        imgPhoto.Dispose();
                        PhotoImg.Dispose();


                        if (chbxhighResPhoto.Checked)
                        {
                            //Get second thumbnail and save it to disk. It stores 600x400 image.

                            //thumbnailName = thumbnailName.Replace("_Thumbnail1.jpg", "");
                            // conctenate filename with big image name by ud
                            string bigfileName = "Big_" + RandomFileName;
                            string targetpath = galleryPathForBigImages;
                            DirectoryInfo objDir = new DirectoryInfo(targetpath);
                            if (!objDir.Exists) //if directory does not exists creates a directory
                                objDir.Create();
                            Bitmap bigFile = new Bitmap(Image.FromFile(tempPath + "//" + imageName.Trim()), new Size(1024, 680));
                            bigFile.Save(targetpath + "/" + bigfileName);
                            bigFile.Dispose();
                        }

                        //thumbnailNames[i] = thumbnailName;
                        //to get tribute id and name from session
                        if (Session["PhotoAlbumTributeSession"] != null)
                        {
                            objTribute = Session["PhotoAlbumTributeSession"] as Tributes;
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

                        // set the ID to the last photo ID uploaded so that we can send the email in PhotoUploadPresenter
                        objPhoto.PhotoId = UploadedPhotoID;
                        if (objPhoto.PhotoCaption == null)
                            objPhoto.PhotoCaption = string.Empty;
                        //objPhoto.PhotoDesc = replaceSpecialCharacter(uploadedFile.Description.ToString());
                        objPhoto.ModuleTypeName = MODULE_TYPE_NAME;
                        objPhoto.UserTributeId = _tributeId;
                        objPhoto.CreatedBy = _userId;
                        objPhoto.CreatedDate = DateTime.Now;
                        objPhoto.IsActive = true;
                        objPhoto.IsDeleted = false;
                        objPhoto.UserName = _userName;
                        objPhoto.TributeName = _tributeName;
                        objPhoto.TributeType = _tributeType;
                        objPhoto.TributeUrl = _tributeUrl;
                        objPhoto.PathToVisit = Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;
                        objPhoto.PhotoAlbumId = PhotoAlbumID;

                        _presenter.SaveImageList(objPhoto, AddPhotoEmail);

                        filecount--;
                        if (filecount == 0)
                        {
                            Session["PhotoAlbumId"] = null;
                            Session["PhotoAlbumName"] = null;
                            if (WebConfig.ApplicationMode.Equals("local"))
                                Session["PhotoAlbumTributeSession"] = null;
                            Session["PhotoAlbumobjUserInfo"] = null;
                            //HttpContext.Current.Session["AlbumName"] = null;
                            //HttpContext.Current.Session["AlbumDesc"] = null;
                        }
                        if (UploadedPhotoID > 0)
                        {
                            AddPhotoEmail = false;
                        }

                        // Added by rupendra on June 20, 2011 to redirect on album aftwer updating the page
                        if (Request.QueryString["photoAlbumId"] != null)
                        {
                            _photoAlbumId = 0;
                            int.TryParse(Request.QueryString["photoAlbumId"], out _photoAlbumId);
                            if (PhotoAlbumId > 0)
                                Session["PhotoAlbumID2"] = _photoAlbumId;
                        }
                    }
                    #endregion
                }
            }
            DirectoryInfo dir = new DirectoryInfo(tempPath);
            foreach (FileInfo files in dir.GetFiles())
            {
                files.Delete();
            }

            if (_mode == PAGE_MODE_ADD_PHOTOS)
            {
                Response.Redirect("~/" + Session["TributeURL"] + "/photoalbum.aspx?photoAlbumId=" + _photoAlbumId, false);
            }
            else
            {
                Response.Redirect("~/" + Session["TributeURL"] + "/photos.aspx", false);
            }

            #endregion
        }
        catch (Exception ex)
        {
            LogError(ex.Message, ex.StackTrace);
            if(ex.Message.Contains("The process cannot access the file"))
            {
                if (_mode == PAGE_MODE_ADD_PHOTOS)
                {
                    Response.Redirect("~/" + Session["TributeURL"] + "/photoalbum.aspx?photoAlbumId=" + _photoAlbumId, false);
                }
                else
                {
                    Response.Redirect("~/" + Session["TributeURL"] + "/photos.aspx", false);
                } 
            }
        }
    }

    // Ashu: MADE A METHOD to REPLACE SPECIAL CHARCAHTERS 
    private string replaceSpecialCharacter(string StrName)
    {
        if (StrName != null)
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
        }
        return StrName;
    }


    #endregion

    #region PROPERTIES
    [CreateNew]
    public ManagePhotoAlbumPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    public int PhotoCount
    {
        set
        {
            hdnPhotoCount.Value = value.ToString();
        }
    }
    public int PhotoAlbumId
    {
        get
        {
            return _photoAlbumId;
        }
    }
    public int TributeId
    {
        get
        {
            Session["Tributeid"] = _tributeId;
            return _tributeId;
        }
    }
    public string PhotoAlbumList
    {
        set
        {
            hdnAlbumList.Value = value;
        }
    }
    public PhotoAlbum PhotoAlbumDetails
    {
        set
        {
            PhotoAlbumDetails = value;
        }
        get
        {
            return GetAlbumData();
        }
    }
    public List<Photos> PhotoList
    {
        set
        {
            PhotoList = value;
        }
        get
        {
            return GetListOfPhotos();
        }
    }

    public int Result
    {
        set
        {
            result = value;
        }
        get
        {
            return result;
        }
    }

    public int PhotoAlbumID
    {
        set
        {
            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add("NewPhotoAlbumId", value, StateManager.State.Session);
        }
        get
        {
            StateManager objStateManager = StateManager.Instance;
            int phAlbumId = 0;

            if (Request.QueryString["photoAlbumId"] != null)
            {
                int.TryParse(Request.QueryString["photoAlbumId"].ToString(), out phAlbumId);
            }
            if (phAlbumId > 0)
            {
                objStateManager.Add("NewPhotoAlbumId", phAlbumId, StateManager.State.Session);
                return phAlbumId;
            }
            else
            {
                object albumSession = objStateManager.Get("NewPhotoAlbumId", StateManager.State.Session);
                if (!Equals(albumSession, null))
                    return int.Parse(objStateManager.Get("NewPhotoAlbumId", StateManager.State.Session).ToString());
                else
                    return 0;
            }
        }
    }

    /// <summary>
    /// Photo counter for an existing album
    /// </summary>
    public int ExistingPhotoCount
    {
        get
        {
            int count = 0;
            if (Session["PhotoCount"] != null)
            {
                count = int.Parse(Session["PhotoCount"].ToString());
            }
            return count;
        }
    }

    /// <summary>
    /// Photo counter for the newly uploaded photos
    /// </summary>
    public int UploadedPhotoCount
    {
        set
        {
            Session["NewPhotoCount"] = value;
        }
        get
        {
            if (Session["NewPhotoCount"] != null)
                return Convert.ToInt32(Session["NewPhotoCount"]);
            else
                return 0;
        }
    }

    /// <summary>
    /// First uploaded PhotoID
    /// </summary>
    public int UploadedPhotoID
    {
        set
        {
            Session["NewPhotoID"] = value;
        }
        get
        {
            if (Session["NewPhotoID"] != null)
                return Convert.ToInt32(Session["NewPhotoID"]);
            else
                return 0;
        }
    }

    #endregion

    #region METHODS
    /// <summary>
    /// method to set values to labels, buttons and error messages etc.
    /// </summary>
    private void SetValuesToControls()
    {

        stgRequired.InnerHtml = ResourceText.GetString("lblRequired_MPA") + "<em class='required'>* </em>";
        lblAlbumName.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblAlbumName_MPA");
        lblAlbumDesc.InnerText = ResourceText.GetString("lblAlbumDesc_MPA");
        hUploadPhoto.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblUploadPhoto_MPA");
        lbtnCancel.Text = ResourceText.GetString("lbtnCancel_MPA");
        liInstruction1.InnerText = ResourceText.GetString("txtInstruction1_MPA");
        //liInstruction2.InnerText = ResourceText.GetString("txtInstruction2_MPA");
        liInstruction3.InnerText = ResourceText.GetString("txtInstruction3_MPA");

        /*-- Done changes for breadcrums by Ashu ---*/

        if (Session["PhotoAlbumName"] != null && Session["PhotoAlbumId"] != null)
        {
            int.TryParse(Session["PhotoAlbumId"].ToString(), out _photoAlbumId);
            _photoAlbumName = Session["PhotoAlbumName"].ToString();

        }
        string redirecturl = "";
        if (_tributeUrl != null && _tributeType != null)
        {
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                redirecturl = WebConfig.AppBaseDomain + _tributeUrl + "/";
            }
            else
            {
                redirecturl = "http://" + _tributeType.ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + _tributeUrl + "/";


            }
        }

        //to set the navigation string
        if (_mode == PAGE_MODE_ADD_PHOTOS)
        {
            hPhoto.InnerText = "PHOTOS: UPDATE ALBUM";
            divNavigation.InnerHtml = "<a href=" + redirecturl + "photoalbum.aspx?photoAlbumId=" + _photoAlbumId + ">< Back to Photo Album</a>";

            secondLine.InnerText =
                @"2. After you have finished uploading your photos, click 'Update photo album' at the bottom of the page to update the album.";
            if (_photoAlbumId != 0)
            {
                nvgCreateAlbum.InnerHtml = "<a href=" + redirecturl + "> " +
                                           ResourceText.GetString("txtTributeHome_MPA") + "</a> <a href=" + redirecturl +
                                           "photos.aspx>" + ResourceText.GetString("txtPhotos_MPA") + "</a> <a href=" +
                                           redirecturl + "photoalbum.aspx?photoAlbumId=" + _photoAlbumId + ">" +
                                           _photoAlbumName + "</a> <span class='selected'>" +
                                           ResourceText.GetString("txtAddPhotos_MPA") + "</span>";
            }
            else
            {
                nvgCreateAlbum.InnerHtml = "<a href=" + redirecturl + "> " +
                                           ResourceText.GetString("txtTributeHome_MPA") + "</a> <a href=" + redirecturl +
                                           "photos.aspx>" + ResourceText.GetString("txtPhotos_MPA") +
                                           "</a> <span class='selected'>" + ResourceText.GetString("txtCreateAlbum_MPA") +
                                           "</span>";
            }
        }
        else
        {
            hPhoto.InnerText = ResourceText.GetString("lblPhotoHeader_MPA");
            nvgCreateAlbum.InnerHtml = "<a href=" + redirecturl + "> " + ResourceText.GetString("txtTributeHome_MPA") + "</a> <a href=" + redirecturl + "photos.aspx>" + ResourceText.GetString("txtPhotos_MPA") + "</a> <span class='selected'>" + ResourceText.GetString("txtCreateAlbum_MPA") + "</span>";
            divNavigation.InnerHtml = "<a href=" + redirecturl + "photos.aspx>< Back to Photos</a>";
        }
    }

    private void GetSessionValues()
    {
        StateManager objStateManager = StateManager.Instance;
        //to get user id from session as user is logged in user
        objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
        if (String.IsNullOrEmpty(objSessionValue.ToString()))
        {
            LogError("GetSessionValues", "User session is null");
        }
        if (!Equals(objSessionValue, null))
        {
            _userId = objSessionValue.UserId;

        }
        if (!Equals(Request.QueryString["mode"], null))
        {
            _mode = Request.QueryString["mode"].ToString();
            _photoAlbumId = int.Parse(Request.QueryString["photoAlbumId"].ToString());
            hdnAlbumId.Value = _photoAlbumId.ToString();
        }
        else
            hdnAlbumId.Value = string.Empty;

        //to get photo album name from session
        if (objStateManager.Get("PhotoAlbumName", StateManager.State.Session) != null)
            _photoAlbumName = objStateManager.Get("PhotoAlbumName", StateManager.State.Session).ToString();
        else
            _photoAlbumName = string.Empty;
        //to check if user is not loggedin
        if (_userId == 0) //if user is not a logged in user redirect to login page
        {
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
        }

        if (!Equals(Request.QueryString["result"], null))
            _result = Request.QueryString["result"].ToString();
    }

    /// <summary>
    /// Method to get user is admin or owner
    /// </summary>
    private void UserIsAdminOrOwner()
    {
        if (objTribute != null)
        {
            if (objTribute.TributeId > 0)
            {
                _tributeId = objTribute.TributeId;
            }
        }
        UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
        objUserInfo.UserId = _userId;
        objUserInfo.TributeId = _tributeId;
        if (_mode == PAGE_MODE_ADD_PHOTOS)
            objUserInfo.TypeName = TYPE_NAME_ADD_PHOTOS;
        else
            objUserInfo.TypeName = TYPE_NAME_CREATE_ALBUM;

        if (IsUserAdmin(objUserInfo))
            objUserInfo.IsAdmin = true;
        else
            objUserInfo.IsAdmin = false;

        isUserAdmin = objUserInfo.IsAdmin;

        StateManager objStateManager = StateManager.Instance;
        objStateManager.Add("UserAdminOwnerInfo_AddPhotoAlbum", objUserInfo, StateManager.State.Session);
        Session["PhotoAlbumobjUserInfo"] = objUserInfo;
    }

    /// <summary>
    /// Method to set the visibility of controls based on page mode.
    /// </summary>
    private void SetControlsVisibility()
    {
        if (_mode == PAGE_MODE_ADD_PHOTOS)
        {
            divAlbumName.Visible = false;
            divAlbumDesc.Visible = false;
        }
        else
        {
            divAlbumName.Visible = true;
            divAlbumDesc.Visible = true;
        }

        //displays error message if album name already exists if system goes to database on clicking create
        if (_result == "error")
        {
            string headertext = " <h2>Oops - there is a problem with your album.</h2><br/>";
            headertext += "<ul>";
            headertext += "<li>";
            headertext += ResourceText.GetString("errDuplicateName_MPA");
            headertext += "</li>";
            headertext += "</ul>";

            spnErrDuplicate.Visible = true;
            spnErrDuplicate.InnerHtml = headertext;
        }
        else
            spnErrDuplicate.Visible = false;

    }

    private void CreatePath()
    {
        string[] getPath = CommonUtilities.GetPath();
        //to create directory for image.
        if (Session["PhotoAlbumTributeSession"] != null)
        {
            objTribute = Session["PhotoAlbumTributeSession"] as Tributes;
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
        if (_tributeType != null && _tributeUrl != null)
        {
            galleryPath = getPath[0] + "/" + getPath[1] + "/" + _tributeUrl.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_");
            DirectoryInfo objDir = new DirectoryInfo(galleryPath);
            if (!objDir.Exists) //if directory does not exists creates a directory
                objDir.Create();

            // for big pictures
            galleryPathForBigImages = getPath[0] + "/" + getPath[1] + "/" + _tributeUrl.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_");
            DirectoryInfo objBigImagesDir = new DirectoryInfo(galleryPathForBigImages);
            if (!objBigImagesDir.Exists) //if directory does not exists creates a directory
                objBigImagesDir.Create();

            //to create directory for thumnail of that image.
            galleryPathForThumbnails = getPath[0] + "/" + getPath[1] + "/" + getPath[3] + "/" + _tributeUrl.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_");
            DirectoryInfo objThumbnailDir = new DirectoryInfo(galleryPathForThumbnails);
            if (!objThumbnailDir.Exists) //if directory does not exists creates a directory
                objThumbnailDir.Create();
        }
    }
    /// <summary>
    /// Method to get Photo Album data
    /// </summary>
    /// <returns>Filled PhotoAlbum entity.</returns>
    private PhotoAlbum GetAlbumData()
    {
        string albumName = txtAlbumName.Text;
        string albumDesc = txtAlbumDesc.Text;

        //if (Session["AlbumName"] != null)
        //{
        //    albumName = Session["AlbumName"].ToString();
        //}
        //if (Session["AlbumDesc"] != null)
        //{
        //    albumDesc = Session["AlbumDesc"].ToString();
        //}
        PhotoAlbum objPhotoAlbum = new PhotoAlbum();

        objPhotoAlbum.PhotoAlbumId = PhotoAlbumID;
        objPhotoAlbum.PhotoAlbumCaption = albumName;
        objPhotoAlbum.PhotoAlbumDesc = albumDesc;
        objPhotoAlbum.ModuleTypeName = MODULE_TYPE_NAME;
        objPhotoAlbum.UserId = _userId;
        objPhotoAlbum.UserTributeId = _tributeId;
        objPhotoAlbum.CreatedBy = _userId;
        objPhotoAlbum.CreatedDate = DateTime.Now;
        objPhotoAlbum.IsActive = true;
        objPhotoAlbum.IsDeleted = false;
        objPhotoAlbum.UserName = _userName;
        objPhotoAlbum.TributeName = _tributeName;
        objPhotoAlbum.TributeType = _tributeType;
        objPhotoAlbum.TributeUrl = _tributeUrl;
        objPhotoAlbum.PhotoCount = ExistingPhotoCount + UploadedPhotoCount;
        objPhotoAlbum.PathToVisit = Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;
        return objPhotoAlbum;
    }
    private List<Photos> GetListOfPhotos()
    {
        List<Photos> objPhotoList = new List<Photos>();

        //Get total number of uploaded files (all files are uploaded in a single package).
        int fileCount = Int32.Parse(Request.Form["FileCount"]);
        for (int i = 1; i <= fileCount; i++)
        {
            Photos objPhoto = new Photos();
            string fileName = GetSafeFileName("DSC");

            //Save file info. If you modify the script.js to send more fields, do not forget to 
            //extract appropriate variables here.
            // set the ID to the last photo ID uploaded so that we can send the email in PhotoUploadPresenter
            objPhoto.PhotoId = UploadedPhotoID;
            objPhoto.PhotoImage = fileName;
            objPhoto.PhotoCaption = Request.Form["Title_" + i].ToString().Trim();
            objPhoto.PhotoDesc = Request.Form["Description_" + i].ToString().Trim();
            objPhoto.ModuleTypeName = MODULE_TYPE_NAME;
            objPhoto.UserTributeId = _tributeId;
            objPhoto.CreatedBy = _userId;
            objPhoto.CreatedDate = DateTime.Now;
            objPhoto.IsActive = true;
            objPhoto.IsDeleted = false;
            objPhoto.UserName = _userName;
            objPhoto.TributeName = _tributeName;
            objPhoto.TributeType = _tributeType;
            objPhoto.TributeUrl = _tributeUrl;
            objPhoto.PathToVisit = Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;
            objPhotoList.Add(objPhoto);
        }
        return objPhotoList;
    }

    private System.Drawing.Image Crop(System.Drawing.Image imgPhoto, int Width, int Height, AnchorPosition Anchor)
    {
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);

        if (nPercentH < nPercentW)
        {
            nPercent = nPercentW;
            switch (Anchor)
            {
                case AnchorPosition.Top:
                    destY = 0;
                    break;
                case AnchorPosition.Bottom:
                    destY = (int)(Height - (sourceHeight * nPercent));
                    break;
                default:
                    destY = (int)((Height - (sourceHeight * nPercent)) / 2);
                    break;
            }
        }
        else
        {
            nPercent = nPercentH;
            switch (Anchor)
            {
                case AnchorPosition.Left:
                    destX = 0;
                    break;
                case AnchorPosition.Right:
                    destX = (int)(Width - (sourceWidth * nPercent));
                    break;
                default:
                    destX = (int)((Width - (sourceWidth * nPercent)) / 2);
                    break;
            }
        }

        int destWidth = (int)(sourceWidth * nPercent) + 2;
        int destHeight = (int)(sourceHeight * nPercent) + 2;

        Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new Rectangle(destX, destY, destWidth, destHeight),
            new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }
    private void GetPhotoSessionValues()
    {
        try
        {
            StateManager objStateManager = StateManager.Instance;
            //to get user id from session as user is logged in user
            objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionValue == null)
            {
                LogError("3", "User session is null");
            }
            if (!Equals(objSessionValue, null))
            {

                _userId = objSessionValue.UserId;
                _userName = objSessionValue.FirstName == string.Empty ? objSessionValue.UserName : (objSessionValue.FirstName + " " + objSessionValue.LastName);
            }

            string strPath = "";
            if (Request.RawUrl.Contains("/"))
            {
                strPath = Request.RawUrl.ToString().Substring(0, Request.RawUrl.ToString().LastIndexOf('/'));
                strPath = strPath.Substring(strPath.LastIndexOf('/') + 1);
            }
            if ((strPath != "") && (Request.QueryString["Type"] != null))
            {
                objTribute = _presenter.GetTributeSessionForUrlAndType(strPath, Request.QueryString["Type"].ToString().ToLower().Replace("newbaby", "new baby"));
                Session["PhotoAlbumTributeSession"] = objTribute;
            }

            //to get tribute id and name from session
            if (Session["PhotoAlbumTributeSession"] != null)
            {
                objTribute = Session["PhotoAlbumTributeSession"] as Tributes;
            }
            else
            {
                LogError("Null", "PhotoAlbumTributeSession is null");
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
            //to check if user is not loggedin
            if (_userId == 0) //if user is not a logged in user redirect to login page
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }
        }
        catch (Exception ex)
        {
            LogError(ex.Message, ex.StackTrace);
        }
    }
    public void SaveFilesOnStorage(List<Photos> objPhotoList)
    {
        if (objPhotoList.Count > 0)
        {
            for (int i = 0; i < objPhotoList.Count; i++)
            {

                Photos objPhoto = objPhotoList[i];
                string fileName = objPhoto.PhotoImage;

                //Get first thumbnail and save it to disk. It stores 600x400 image.
                HttpPostedFile thumbnail1File = Request.Files["Thumbnail1_" + (i + 1)];
                thumbnail1File.SaveAs(galleryPath + "/" + fileName); // .jpg



                //Get the second thumbnail and save it to disk. It stores 120x120 preview image. 
                System.Drawing.Image PhotoImg = System.Drawing.Image.FromFile(galleryPath + "/" + fileName);
                System.Drawing.Image imgPhoto = Crop(PhotoImg, 100, 100, AnchorPosition.Center);
                imgPhoto.Save(galleryPathForThumbnails + "/" + fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                imgPhoto.Dispose();
                PhotoImg.Dispose();
            }
        }
    }
    /// <summary>
    /// Method to get the safe file name. 
    /// it appends text in the file name if file with the same name already exists. by UD
    /// </summary>
    /// <param name="fileName">Actual Filename</param>
    /// <returns>Modified file name</returns>
    private string GetSafeFileName(string fileName)
    {
        string newFileName = "";
        newFileName = fileName;
        int i = 0;
        while (File.Exists(galleryPath + "/" + newFileName))
        {
            string file = Path.GetFileNameWithoutExtension(galleryPath + "/" + fileName);
            string fileExtension = Path.GetExtension(galleryPath + "/" + fileName);
            newFileName = file + "_" + (++i) + fileExtension;
        }
        return newFileName;
    }

    // Check validation of album by Ashu

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public int CheckAlbumValidation(string name, string desc, int filecount)
    {
        PhotoResource _photoResorce = new PhotoResource();
        int tributeid = 0;
        int PhotoAlbumCount = 0;
        int.TryParse(HttpContext.Current.Session["Tributeid"].ToString(), out tributeid);
        name = replaceSpecialCharacter(name);
        desc = replaceSpecialCharacter(desc);
        //HttpContext.Current.Session["AlbumName"] = name;
        //HttpContext.Current.Session["AlbumDesc"] = desc;
        HttpContext.Current.Session["FileCount"] = filecount;

        if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(desc)))
        {
            if (string.IsNullOrEmpty(name))
            {
                PhotoAlbumCount = -2;
            }
            //length of photo album name
            if (!string.IsNullOrEmpty(name) && name.Length > 100)
            {
                PhotoAlbumCount = -3;
            }
            //length of photo album description
            if (!string.IsNullOrEmpty(desc) && desc.Length > 1000)
            {
                PhotoAlbumCount = -4;
            }
            if (filecount > 60)
            {
                PhotoAlbumCount = -5;
            }
            else
            {

                object obj = _photoResorce.CheckUniquePhotoAlbum(name, tributeid);
                if (obj != null)
                    PhotoAlbumCount = Convert.ToInt16(obj.ToString());
            }
        }
        return PhotoAlbumCount;
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public int CheckAlbumFileCount(int filecount, int ExistingFileCount)
    {
        int PhotoAlbumCount = 0;
        if ((filecount + ExistingFileCount) > 60)
        {
            PhotoAlbumCount = -5;
        }
        else
        {
            HttpContext.Current.Session["FileCount"] = filecount;

        }
        return PhotoAlbumCount;
    }

    // to set value in session for album name and desc  by UD

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SetAlbumNameDesc(string name, string desc)
    {
        HttpContext.Current.Session["AlbumName"] = name;
        HttpContext.Current.Session["AlbumDesc"] = desc;
    }

    #endregion


}