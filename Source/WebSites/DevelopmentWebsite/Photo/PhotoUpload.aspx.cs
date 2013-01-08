///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Photo.PhotoUpload.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page does the actual job of Uploading photos to the server
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.Photo.Views;
using TributesPortal.Utilities;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

#endregion

public partial class Photo_PhotoUpload : PageBase, IPhotoUpload
{
    #region CLASS VARIABLES
    private PhotoUploadPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    private int _userId;
    private int _tributeId;
    private string _tributeType;
    protected string _tributeName;
    protected string _tributeUrl;
    private string galleryPath = string.Empty;
    private string BigImagePathForBigImages = string.Empty;
    private string galleryPathForThumbnails = string.Empty;
    private int result;
    private string _userName;
    Random objRand = new Random();

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
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetSessionValues();
            CreatePath();
            if (!this.IsPostBack)
            {
                this._presenter.CreateAlbumAndSavePhoto();
                Session["latest_photo_album_id"] = this.PhotoAlbumDetails.PhotoAlbumId;
                Session["latest_photo_album_caption"] = this.PhotoAlbumDetails.PhotoAlbumCaption;
                Session["latest_photo_album_pics"] = this.PhotoList;
            }
            if (result < 0)
            {
                Session["Result_photo_upload"] = result;
                // we need it to be passed back from PhotoGallery page
                StateManager objStateManager = StateManager.Instance;
                objStateManager.Add("PhotoAlbumId", this.PhotoAlbumDetails.PhotoAlbumId, StateManager.State.Session);
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
    public PhotoUploadPresenter Presenter
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

            string photoAlbumID = Request.Form["AlbumId"].ToString();
            
            if (photoAlbumID != string.Empty && Convert.ToInt32(photoAlbumID) > 0)
            {
                objStateManager.Add("NewPhotoAlbumId", photoAlbumID, StateManager.State.Session);
                return Convert.ToInt32(photoAlbumID);
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
    /// Method to get Photo Album data
    /// </summary>
    /// <returns>Filled PhotoAlbum entity.</returns>
    private PhotoAlbum GetAlbumData()
    {
        PhotoAlbum objPhotoAlbum = new PhotoAlbum();

        objPhotoAlbum.PhotoAlbumId = PhotoAlbumID;
        objPhotoAlbum.PhotoAlbumCaption = Request.Form["AlbumName"].ToString().Trim();
        objPhotoAlbum.PhotoAlbumDesc = Request.Form["AlbumDesc"].ToString().Trim();
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


    public void SaveFilesOnStorage(List<Photos> objPhotoList)
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
            imgPhoto.Save(galleryPathForThumbnails + "/" + fileName, ImageFormat.Jpeg);
            imgPhoto.Dispose();
            PhotoImg.Dispose();
        }
    }


    /// <summary>
    /// Method to get the safe file name. 
    /// it appends text in the file name if file with the same name already exists.
    /// </summary>
    /// <param name="fileName">Actual Filename</param>
    /// <returns>Modified file name</returns>
    private string GetSafeFileName(string fileName)
    {
        string newFileName = fileName + objRand.Next(MIN_RAND_VALUE_FOR_FILENAME, MAX_RAND_VALUE_FOR_FILENAME).ToString() + ".jpg";

        while (File.Exists(galleryPath + "/" + newFileName))
        {
            newFileName = fileName + objRand.Next(MIN_RAND_VALUE_FOR_FILENAME, MAX_RAND_VALUE_FOR_FILENAME).ToString() + ".jpg";
        }
        return newFileName;
    }


    /// <summary>
    /// Method to get the values from session.
    /// </summary>
    private void GetSessionValues()
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

        //to check if user is not loggedin
        if (_userId == 0) //if user is not a logged in user redirect to login page
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
    }

    /// <summary>
    /// Function to create  the directory by appending tribute name and tribute type.
    /// </summary>
    private void CreatePath()
    {
        string[] getPath = CommonUtilities.GetPath();
        //to create directory for image.
        //galleryPath = getPath[0] + "/" + getPath[1] + "/" + _tributeName.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_");
        galleryPath = getPath[0] + "/" + getPath[1] + "/" + _tributeUrl.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_");
        DirectoryInfo objDir = new DirectoryInfo(galleryPath);
        if (!objDir.Exists) //if directory does not exists creates a directory
            objDir.Create();
        // for big pictures
        BigImagePathForBigImages = getPath[0] + "/" + getPath[1] + "/" + _tributeUrl.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_") + "/" + getPath[11];
        //to create directory for thumnail of that image.
        //galleryPathForThumbnails = getPath[0] + "/" + getPath[1] + "/" + getPath[3] + "/" + _tributeName.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_");
        galleryPathForThumbnails = getPath[0] + "/" + getPath[1] + "/" + getPath[3] + "/" + _tributeUrl.Replace(" ", "_") + "_" + _tributeType.Replace(" ", "_");
        DirectoryInfo objThumbnailDir = new DirectoryInfo(galleryPathForThumbnails);
        if (!objThumbnailDir.Exists) //if directory does not exists creates a directory
            objThumbnailDir.Create();
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

    #endregion
}