///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Photo.EditPhotoAlbum.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to edit the photo album details like the album name
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
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Photo.Views;
using TributesPortal.Utilities;
#endregion

public partial class Photo_EditPhotoAlbum : PageBase, IEditPhotoAlbum
{
    #region CLASS VARIABLES
    private EditPhotoAlbumPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    private int _userId = 0;
//    private bool isUserAdmin; by ud
    private int _tributeId;
    private string _tributeType;
    protected string _tributeName;
    protected int _photoAlbumId;
    protected string photoAlbumList;
    private bool isAdmin;
    private string result = string.Empty;
    #endregion

    #region CONSTANTS
    private const string TYPE_NAME = "EditPhotoAlbum";
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lbtnDeletePhotoAlbum.Attributes.Add("onclick", "if(confirm('" + ResourceText.GetString("msgDelete_EPA") + "')){}else{return false}");
            GetValuesFromSession();
            UserIsAdmin();
            if (!this.IsPostBack)
            {
                this._presenter.GetPhotoAlbumDetails();
            }
            this._presenter.GetPhotoAlbumList();
            SetValuesToControls();
            Page.SetFocus(txtPhotoAlbumCaption);

            if (result == string.Empty)
                lblErrMsg.Visible = false;
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
            Response.Redirect("~/" + Session["TributeURL"] + "/photoalbum.aspx" + "?photoAlbumId=" + _photoAlbumId, false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnDeletePhotoAlbum_Click(object sender, EventArgs e)
    {
        try
        {
            this._presenter.DeletePhotoAlbum();
            Response.Redirect("~/" + Session["TributeURL"] + "/photos.aspx", false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnSavePhotoAlbum_Click(object sender, EventArgs e)
    {
        try
        {
            string result = this._presenter.UpdatePhotoAlbumDetails();
            if (result != "Failure")
            {
                Response.Redirect("~/" + Session["TributeURL"] + "/photoalbum.aspx" + "?photoAlbumId=" + _photoAlbumId, false);
            }
            else
            {
                //string headertext = "<div id='lblErrMsg' align='left' class='yt-Error'>";
                string headertext = " <h2>Oops - there is a problem with your album.</h2><br/>";
                headertext += "<ul>";
                headertext += "<li>";
                headertext += ResourceText.GetString("errDuplicateName_EPA");
                headertext += "</li>";
                headertext += "</ul>";
                //headertext += "</div>";
                lblErrMsg.Visible = true;
                lblErrMsg.InnerHtml = headertext;
                //spnErrDuplicate.Visible = true;
                //spnErrDuplicate.InnerHtml = headertext;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region PROEPRTIES
    public PhotoAlbum PhotoAlbumDetails
    {
        set
        {
            Label html = new Label();
            txtPhotoAlbumCaption.Text =replaceHtmlCharacterCode(value.PhotoAlbumCaption);
            txtPhotoAlbumDesc.Text = replaceHtmlCharacterCode(value.PhotoAlbumDesc);
            nvgEditPhotoAlbum.InnerHtml = "<a href=''>" + ResourceText.GetString("nvgTributeHome_PV") + "</a> <a href='photos.aspx'>" + ResourceText.GetString("nvgPhotos_PV") + "</a> <a href='photoalbum.aspx?photoAlbumId=" + value.PhotoAlbumId + "'>" + value.PhotoAlbumCaption + "</a><span class='selected'>Edit</span>";
            hdnOldName.Value = replaceHtmlCharacterCode(value.PhotoAlbumCaption);
            //to add photo album id in session
            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add("PhotoAlbumId", value.PhotoAlbumId, StateManager.State.Session);
        }
    }
    public int TributeId
    {
        get
        {
            return _tributeId;
        }
    }
    public int PhotoAlbumId
    {
        get
        {
            return _photoAlbumId;
        }
    }
    public int UserId
    {
        get
        {
            return _userId;
        }
    }
    public string PhotoAlbumCaption
    {
        get
        {
            return replaceSpecialCharacter(txtPhotoAlbumCaption.Text);
        }
    }
    public string PhotoAlbumDesc
    {
        get
        {
            return replaceSpecialCharacter(txtPhotoAlbumDesc.Text);
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
    public string PhotoAlbumList
    {
        set
        {
            hdnAlbumList.Value = value;
            //photoAlbumList = value;
        }
    }

    [CreateNew]
    public EditPhotoAlbumPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
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
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);

        //to get photo id from query string
        if (Request.QueryString["photoAlbumId"] != null) //to pick value of selected note from querystring
            _photoAlbumId = int.Parse(Request.QueryString["photoAlbumId"].ToString());
        else
            _photoAlbumId = 0;

        objTribute = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);

        if (!Equals(objTribute, null))
        {
            _tributeId = objTribute.TributeId;
            _tributeName = objTribute.TributeName;
            _tributeType = objTribute.TypeDescription;
        }
    }

    /// <summary>
    /// Method to get user is admin or owner
    /// </summary>
    private bool UserIsAdmin()
    {
        UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
        objUserInfo.UserId = _userId;
        objUserInfo.TributeId = _tributeId;
        objUserInfo.TypeId = _photoAlbumId;
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
        objStateManager.Add("UserAdminOwnerInfo_EditPhotoAlbum", objUserInfo, StateManager.State.Session);
        return isAdmin;
    }

    /// <summary>
    /// Method to set values to controls and error messages.
    /// </summary>
    private void SetValuesToControls()
    {
        hPhotoAlbumEdit.InnerText = ResourceText.GetString("hdrPhotoAlbumEdit_EPA");
        lblPhotoAlbumCaption.InnerText = ResourceText.GetString("lblPhotoAlbumCaption_EPA");
        lblPhotoAlbumDesc.InnerText = ResourceText.GetString("lblPhotoAlbumDesc_EPA");
        lbtnCancel.Text = ResourceText.GetString("lbtnCancel_EPA");
        lbtnDeletePhotoAlbum.Text = ResourceText.GetString("lbtnDeletePhotoAlbum_EPA");
        lbtnSavePhotoAlbum.Text = ResourceText.GetString("lbtnSavePhotoAlbum_EPA");
        cvPhotoAlbumDesc.ErrorMessage = ResourceText.GetString("errAlbumDescription_EPA");
        rfvPhotoAlbumCaption.ErrorMessage = ResourceText.GetString("errAlbumCaption_EPA");
        cvAlbumNameDuplicate.ErrorMessage = ResourceText.GetString("errDuplicateName_EPA");
    }
   

    #endregion


}
