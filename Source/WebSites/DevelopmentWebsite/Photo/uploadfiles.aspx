<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>
<%@ Import Namespace="TributesPortal.BusinessEntities" %>
<%@ Import Namespace="TributesPortal.Utilities" %>
<script runat="server">
    private Tributes _objTribute = null;
    private string _tributeType;
    protected int PhotoAlbumId;
    protected string TributeName;
    private string _tributeUrl;
    private int _tributeId;
    private SessionValue objSessionValue = null;
    private int _userId = 0;
    private int _photoAlbumId;

    protected void Page_Load(object sender, EventArgs e)
    {
        string galleryPath = null;
        string galleryPathForBigImages = null;
        string galleryPathForThumbnails = null;
        const string MODULE_TYPE_NAME = "Photo";
        
        if (Equals(objSessionValue, null))
        {
            StateManager objStateManager = StateManager.Instance;
            objSessionValue = (SessionValue) objStateManager.Get("objSessionvalue", StateManager.State.Session);
            _userId = objSessionValue.UserId;
        }

        string[] getPath = CommonUtilities.GetPath();
        //to create directory for image.
        if (Session["PhotoAlbumTributeSession"] != null)
        {
            _objTribute = Session["PhotoAlbumTributeSession"] as Tributes;
        }

        if (_objTribute != null)
        {
            if (_objTribute.TributeId > 0)
            {
                _tributeId = _objTribute.TributeId;
                TributeName = _objTribute.TributeName;
                _tributeType = _objTribute.TypeDescription;
                _tributeUrl = _objTribute.TributeUrl;
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
        
        /*-------------------------------------------------------------------------
		* First part of this script is for regular upload method (RFC based) 
		*/
        if (string.IsNullOrEmpty(Request.QueryString["chunkedUpload"]))
        {
            if (Request.Files != null && Request.Files.Count > 0)
            {
                HttpPostedFile myFile = Request.Files[0];
                if (myFile != null && myFile.FileName != "")
                {
                    //myFile.SaveAs(galleryPath + System.IO.Path.GetFileName(myFile.FileName));
                    //Response.Write("File " + myFile.FileName + " was successfully uploaded.");
                    string thumbnailName = myFile.FileName.Trim();
                    thumbnailName = thumbnailName.Replace("'", "_");
                    thumbnailName = thumbnailName.Replace("`", "_");
                    thumbnailName = thumbnailName.Replace(" ", "_");
                    thumbnailName = thumbnailName.Replace("@", "");
                    thumbnailName = thumbnailName.Replace("#", "");
                    thumbnailName = thumbnailName.Replace("$", "");
                    thumbnailName = thumbnailName.Replace("%", "");
                    thumbnailName = thumbnailName.Replace("^", "");
                    thumbnailName = thumbnailName.Replace("&", "");
                    thumbnailName = thumbnailName.Replace(",", "");
                    thumbnailName = thumbnailName.Replace("!", "");


                    if (thumbnailName.Contains("thumbnail_"))
                    {
                        string imageName = thumbnailName.Remove(0, 10);
                        myFile.SaveAs(galleryPathForThumbnails + "/"+imageName);
                        
                    }
                    if (thumbnailName.Contains("DSC_"))
                    {
                        string imageName = thumbnailName.Remove(0, 4);
                        myFile.SaveAs(galleryPath + "/" + imageName);
                        
                        string photoComment = Request.Form["comment"];
                        if (Session["ImageUploaded"]==null)
                        {
                            Session["ImageUploaded"] = imageName + ":" + photoComment;
                        }
                        else
                        {
                            Session["ImageUploaded"] = Session["ImageUploaded"] + "," + imageName + ":" + photoComment;
                        }
                    }
                    if (thumbnailName.Contains("Big_"))
                    {
                        //string imageName = thumbnailName.Remove(0, 4);
                        myFile.SaveAs(galleryPathForBigImages + "/" + thumbnailName);
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
            }
            Response.Write("<br>"); //At least one symbol should be sent to response!!!			
        }
    }

</script>


