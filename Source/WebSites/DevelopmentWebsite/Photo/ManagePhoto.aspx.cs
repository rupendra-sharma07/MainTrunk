///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Photo.ManagePhoto.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to edit the details of the selected photo
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using CodeCarvings.Piczard;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Photo.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
#endregion

public partial class Photo_ManagePhoto : PageBase, IManagePhoto
{
    #region CLASS VARIABLES
    private ManagePhotoPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    private int _photoId;
    private int _userId;
    private int _tributeId;
    private string _tributeName;
    private string _tributeType;
    private string _tributeUrl;
    private bool isAdmin;
    #endregion

    #region CONSTANTS
    private const string TYPE_NAME = "ManagePhoto";
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Master != null)
        {
            HtmlGenericControl currdiv = (HtmlGenericControl)Master.FindControl("divContentSecondary");
            currdiv.Style.Add("display", "none");
            HtmlGenericControl containerDiv = (HtmlGenericControl)Master.FindControl("cotentPrimaryContainer");
            containerDiv.Style.Add("margin-left", "-223px");
        }
        if (!IsPostBack)
        {
            this.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = true;
            switch (this.ddlInterfaceMode.SelectedValue)
            {
                case "Full":
                    this.InlinePictureTrimmer1.ShowRulers = true;
                    this.InlinePictureTrimmer1.ShowCropAlignmentLines = true;

                    this.InlinePictureTrimmer1.ShowDetailsPanel = true;
                    this.InlinePictureTrimmer1.ShowZoomPanel = true;
                    this.InlinePictureTrimmer1.ShowResizePanel = true;
                    this.InlinePictureTrimmer1.ShowRotatePanel = true;
                    this.InlinePictureTrimmer1.ShowFlipPanel = true;
                    this.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = true;
                    break;
                case "Standard":
                    this.InlinePictureTrimmer1.ShowRulers = true;
                    this.InlinePictureTrimmer1.ShowCropAlignmentLines = true;
                    this.InlinePictureTrimmer1.ShowDetailsPanel = true;
                    this.InlinePictureTrimmer1.ShowZoomPanel = true;
                    this.InlinePictureTrimmer1.ShowResizePanel = true;
                    this.InlinePictureTrimmer1.ShowRotatePanel = true;
                    this.InlinePictureTrimmer1.ShowFlipPanel = true;
                    this.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = false;
                    break;
                case "Easy":
                    this.InlinePictureTrimmer1.ShowRulers = true;
                    this.InlinePictureTrimmer1.ShowCropAlignmentLines = true;
                    this.InlinePictureTrimmer1.ShowDetailsPanel = false;
                    this.InlinePictureTrimmer1.ShowZoomPanel = false;
                    this.InlinePictureTrimmer1.ShowResizePanel = true;
                    this.InlinePictureTrimmer1.ShowRotatePanel = true;
                    this.InlinePictureTrimmer1.ShowFlipPanel = true;
                    this.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = false;
                    break;
                case "Minimal":
                    this.InlinePictureTrimmer1.ShowRulers = true;
                    this.InlinePictureTrimmer1.ShowCropAlignmentLines = true;
                    this.InlinePictureTrimmer1.ShowDetailsPanel = false;
                    this.InlinePictureTrimmer1.ShowZoomPanel = false;
                    this.InlinePictureTrimmer1.ShowResizePanel = false;
                    this.InlinePictureTrimmer1.ShowRotatePanel = false;
                    this.InlinePictureTrimmer1.ShowFlipPanel = false;
                    this.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = false;
                    break;
                case "Poor":
                    this.InlinePictureTrimmer1.ShowRulers = false;
                    this.InlinePictureTrimmer1.ShowCropAlignmentLines = false;
                    this.InlinePictureTrimmer1.ShowDetailsPanel = false;
                    this.InlinePictureTrimmer1.ShowZoomPanel = false;
                    this.InlinePictureTrimmer1.ShowResizePanel = false;
                    this.InlinePictureTrimmer1.ShowRotatePanel = false;
                    this.InlinePictureTrimmer1.ShowFlipPanel = false;
                    this.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = false;
                    break;
            }
        }

        this.InlinePictureTrimmer1.UIUnit = (GfxUnit)Enum.Parse(typeof(GfxUnit), this.ddlGfxUnit.SelectedValue);
        this.InlinePictureTrimmer1.CanvasColor = System.Drawing.ColorTranslator.FromHtml(this.txtCanvasColor.Text);
        // Reset the OnClientControlLoad event hanlder (used to display the output image when the btnProcessImage is clicked
        this.InlinePictureTrimmer1.OnClientControlLoadFunction = "";
        try
        {
            lbtnDeletePhoto.Attributes.Add("onclick", "if(confirm('" + ResourceText.GetString("msgDelete_MP") + "')){}else{return false}");
            GetValuesFromSession();
            UserIsAdmin();
            if (!this.IsPostBack)
            {
                _presenter.GetPhotoDetails();
                if (Session["ImageUrl"] != null)
                {
                    LoadImageInPiczard(Session["ImageUrl"].ToString());
                }
            }
            SetValuesToControls();
            Page.SetFocus(txtPhotoCaption);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void LoadImageInPiczard(string photoImage)
    {

        string imageUrl = Session["ImageUrl"].ToString();
        string lastPart = imageUrl.Split('/').Last();

        string[] getPath = CommonUtilities.GetPath();
        //for normal pictures
        string galleryPath = getPath[0] + "/" + getPath[1] + "/" + _tributeUrl.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_");
        galleryPath = galleryPath + "/" + lastPart;

        System.Drawing.Image uploadedImage = System.Drawing.Image.FromFile(galleryPath);
        float uploadedImageWidth = uploadedImage.PhysicalDimension.Width;
        float uploadedImageHeight = uploadedImage.PhysicalDimension.Height;

        float outputResolution = float.Parse(this.ddlOutputResolution.SelectedValue,
                                             System.Globalization.CultureInfo.InvariantCulture);
        CropConstraint cropConstrant = new FixedCropConstraint(GfxUnit.Pixel, uploadedImageWidth, uploadedImageHeight);
        cropConstrant.DefaultImageSelectionStrategy =
            (CropConstraintImageSelectionStrategy)
            Enum.Parse(typeof(CropConstraintImageSelectionStrategy), this.ddlImageSelectionStrategy.SelectedValue);
        if (cropConstrant.DefaultImageSelectionStrategy == CropConstraintImageSelectionStrategy.Slice)
        {
            cropConstrant.Margins.SetZero();
        }

        this.InlinePictureTrimmer1.AllowResize = cropConstrant.DefaultImageSelectionStrategy !=
                                                 CropConstraintImageSelectionStrategy.DoNotResize;

        this.InlinePictureTrimmer1.LoadImageFromFileSystem(galleryPath, outputResolution, cropConstrant);
    }


    protected void lbtnDeletePhoto_Click(object sender, EventArgs e)
    {
        try
        {
            this._presenter.DeletePhoto();
            Response.Redirect("~/" + Session["TributeURL"] + "/photoalbum.aspx", false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public static System.Drawing.Image ResizeImage(System.Drawing.Image sourceImage, int width, int height)
    {
        System.Drawing.Image oThumbNail = new Bitmap(sourceImage, width, height);
        using (Graphics oGraphic = Graphics.FromImage(oThumbNail))
        {
            oGraphic.CompositingQuality = CompositingQuality.HighQuality;
            oGraphic.SmoothingMode = SmoothingMode.HighQuality;
            oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle oRectangle = new Rectangle(0, 0, width, height);
            oGraphic.DrawImage(sourceImage, oRectangle);
        }
        return oThumbNail;
    }

    protected void lbtnSavePhoto_Click(object sender, EventArgs e)
    {
        string imageUrl = Session["ImageUrl"].ToString();

        string lastPart = imageUrl.Split('/').Last();

        try
        {
            if (!this.InlinePictureTrimmer1.ImageLoaded)
            {
                // Image not loaded !!!
                return;
            }
            string[] getPath = CommonUtilities.GetPath();
            //for normal pictures
            string galleryPath = getPath[0] + "/" + getPath[1] + "/" + _tributeUrl.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_");
            galleryPath = galleryPath + "/" + lastPart;
            // Update the image
            this.InlinePictureTrimmer1.SaveProcessedImageToFileSystem(galleryPath);



            // for big pictures
            string galleryPathForBigImages = getPath[0] + "/" + getPath[1] + "/" + _tributeUrl.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_");
            galleryPathForBigImages = galleryPathForBigImages + "/" + "Big_" + lastPart;

            if (File.Exists(galleryPathForBigImages))
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(galleryPath);
                ResizeImage(image, 1024, 768).Save(galleryPathForBigImages);
            }

            //for thumbnail pictures
            string galleryPathForThumbnails = getPath[0] + "/" + getPath[1] + "/" + getPath[3] + "/" + _tributeUrl.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_");

            galleryPathForThumbnails = galleryPathForThumbnails +"/"+ lastPart;
            if (File.Exists(galleryPathForThumbnails))
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(galleryPath);
                ResizeImage(image, 100, 100).Save(galleryPathForThumbnails);
            }

            this._presenter.UpdatePhoto();
            Response.Redirect("~/" + Session["TributeURL"] + "/photo.aspx?PhotoId=" + _photoId, false);
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
            Response.Redirect("~/" + Session["TributeURL"] + "/photo.aspx?PhotoId=" + _photoId, false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public ManagePhotoPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    public Photos PhotoDetails
    {
        set
        {
            string photoName = value.PhotoCaption == string.Empty ? "Photo" : value.PhotoCaption; //to display Photo if not caption for photo exists
            txtPhotoCaption.Text = replaceHtmlCharacterCode(photoName);
            txtPhotoDesc.Text = replaceHtmlCharacterCode(value.PhotoDesc);
            //imgPhoto.Src = value.PhotoImage;
            imgPhoto.Visible = false;
            Session["ImageUrl"] = value.PhotoImage;
            pUploadedBy.InnerHtml = ResourceText.GetString("lblUploadedBy_MP") + " " + "<a href='javascript:void(0);' onclick=\"UserProfileModal_1('" + value.CreatedBy + "');\">" + value.UserName + "</a> " + ResourceText.GetString("lblOn_MP") + " " + value.CreatedDate.ToString("MMMM dd, yyyy");
            nvgManagePhoto.InnerHtml = "<a href=''>" + ResourceText.GetString("nvgTributeHome_PV") + "</a> <a href='photos.aspx'>" + ResourceText.GetString("nvgPhotos_PV") + "</a> <a href='photoalbum.aspx?photoAlbumId=" + value.PhotoAlbumId + "'>" + value.PhotoAlbumCaption + "</a><a href='photo.aspx?PhotoId=" + value.PhotoId + "'>" + photoName + "</a><span class='selected'>Edit</span>";

            nvgEditPage.InnerHtml = "<a href='photo.aspx?PhotoId=" + value.PhotoId + "'>< Back to Photo</a>";
            //to add photo album id in session
            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add("PhotoAlbumId", value.PhotoAlbumId, StateManager.State.Session);

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
    public string PhotoCaption
    {
        get
        {
            return replaceSpecialCharacter(txtPhotoCaption.Text.Trim());
        }
    }
    public string PhotoDesc
    {
        get
        {
            return replaceSpecialCharacter(txtPhotoDesc.Text.Trim());
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

    // Ashu: MADE A METHOD TO REPLACE HTML CHARCAHTERS CODE WITH SPECIAL CHARATERS
    private string replaceHtmlCharacterCode(string StrName)
    {
        StrName = StrName.Replace("&#37", "%");
        StrName = StrName.Replace("&#36", "$");
        StrName = StrName.Replace("&#39", "'");
        StrName = StrName.Replace("&#40", "(");
        StrName = StrName.Replace("&#41", ")");
        StrName = StrName.Replace("&#42", "*");
        StrName = StrName.Replace("&#43", "+");
        StrName = StrName.Replace("&#44", ",");
        StrName = StrName.Replace("&#45", "-");
        StrName = StrName.Replace("&#46", ".");
        StrName = StrName.Replace("&#47", "/");
        StrName = StrName.Replace("&#58", ":");
        StrName = StrName.Replace("&#59", ";");
        StrName = StrName.Replace("&#60", "<");
        StrName = StrName.Replace("&#61", "=");
        StrName = StrName.Replace("&#62", ">");
        StrName = StrName.Replace("&#63", "?");
        StrName = StrName.Replace("&#64", "@");
        StrName = StrName.Replace("&#91", "[");
        StrName = StrName.Replace("&#92", "\\");
        StrName = StrName.Replace("&#93", "]");
        StrName = StrName.Replace("&#94", "^");
        StrName = StrName.Replace("&#95", "_");
        StrName = StrName.Replace("&#123", "{");
        StrName = StrName.Replace("&#124", "|");
        StrName = StrName.Replace("&#125", "}");
        StrName = StrName.Replace("&#180", "`");
        StrName = StrName.Replace("&#126", "~");

        return StrName;
    }



    #region METHODS
    /// <summary>
    /// Method to get values from session and querystring
    /// </summary>
    private void GetValuesFromSession()
    {
        StateManager objStateManager = StateManager.Instance;

        //to get user id from session as user is logged in user
        objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionValue, null))
            _userId = objSessionValue.UserId;

        //if user is not logged in user redirect to login page
        if (_userId == 0)
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()) + "?PhotoId=" + _photoId, false);

        //to get photo id from query string
        if (Request.QueryString["PhotoId"] != null) //to pick value of selected note from querystring
            _photoId = int.Parse(Request.QueryString["PhotoId"].ToString());
        else
            _photoId = 0;

        objTribute = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);

        if (!Equals(objTribute, null))
        {
            _tributeId = objTribute.TributeId;
            _tributeName = objTribute.TributeName;
            _tributeType = objTribute.TypeDescription;
            _tributeUrl = objTribute.TributeUrl;
        }
    }

    private void SetValuesToControls()
    {
        hPhotoEdit.InnerText = ResourceText.GetString("hdrPhotoEdit_MP");
        lblPhotoCaption.InnerText = ResourceText.GetString("lblPhotoName_MP");
        lblPhotoDesc.InnerText = ResourceText.GetString("lblPhotoDesc_MP");
        lbtnCancel.Text = ResourceText.GetString("lbtnCancel_MP");
        lbtnDeletePhoto.Text = ResourceText.GetString("lbtnDeletePhoto_MP");
        lbtnSavePhoto.Text = ResourceText.GetString("lbtnSavePhoto_MP");
        cvPhotoDesc.ErrorMessage = ResourceText.GetString("errAlbumDescription_MP");
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
        objStateManager.Add("UserAdminOwnerInfo_ManagePhoto", objUserInfo, StateManager.State.Session);
        return isAdmin;
    }

    #endregion


    protected void ddlInterfaceMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.InlinePictureTrimmer1.ImageLoaded)
        {
            // Reset the zoom factor to force the interface to center the image
            this.InlinePictureTrimmer1.UserState.UIParams.ZoomFactor = null;
            this.InlinePictureTrimmer1.UserState.UIParams.PictureScrollH = null;
            this.InlinePictureTrimmer1.UserState.UIParams.PictureScrollV = null;
        }
    }


}