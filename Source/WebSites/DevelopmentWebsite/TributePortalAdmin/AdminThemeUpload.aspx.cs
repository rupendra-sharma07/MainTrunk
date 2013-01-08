#region USING DIRECTIVES
using System;
using System.Data;
///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.TributePortalAdmin.UserSummaryReport.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the admin to view the user summary report
///Audit Trail     : Date of Modification  Modified By         Description

using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.TributePortalAdmin.Views;
using TributesPortal.BusinessEntities;
using System.Collections.Generic;
using TributesPortal.Utilities;
#endregion

public partial class TributePortalAdmin_AdminThemeUpload : System.Web.UI.Page, IAdminThemeUpload
{
    #region CLASS VARIABLES
    private AdminThemeUploadPresenter _presenter;
    private IList<EventInvitationCategory> _eventInvitationCategoryList;
    private string _invitationCategoryName;
    private string _ThumbnailImagePath = "";
    private string _PreviewImagePath = "";
    private string _FullSizeImagePath = "";

    private const string THUMBNAILPATH = "../assets/images/Thumbnails/";
    private const string PREVIEWLPATH = "../assets/images/Preview/";
    private const string FULLSIZELPATH = "../assets/images/FullSize/";

    #endregion

    #region Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            lblTributeType.Text = "Select Website Type";
        }
        
        if (!Page.IsPostBack)
        {
            reqValTributeType.ErrorMessage = "Please select " + WebConfig.ApplicationWordForInternalUse.ToString().ToLower() + " type";
            lblErrorMessage.Text = string.Empty;

            this._presenter.OnViewInitialized();

            pnlInvitationCategory.Visible = false;

            LoadTributeType();

            LoadInvitationCategory();

            LoadCategory();           
           
        }
        this._presenter.OnViewLoaded();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddTheme_Click(object sender, EventArgs e)
    {
        string filename = string.Empty;
        if (fileUploadPreviewImage.HasFile && fileUploadThumbnailImage.HasFile && fileUploadFullSizeImage.HasFile)
        {
            try
            {
                filename = Path.GetFileName(fileUploadThumbnailImage.FileName);
                fileUploadThumbnailImage.SaveAs(Server.MapPath(THUMBNAILPATH) + filename);
                _ThumbnailImagePath = string.Format("{0}{1}", THUMBNAILPATH, filename);

                filename = Path.GetFileName(fileUploadPreviewImage.FileName);
                fileUploadPreviewImage.SaveAs(Server.MapPath(PREVIEWLPATH) + filename);
                _PreviewImagePath = string.Format("{0}{1}", PREVIEWLPATH, filename);

                filename = Path.GetFileName(fileUploadFullSizeImage.FileName);
                fileUploadFullSizeImage.SaveAs(Server.MapPath(FULLSIZELPATH) + filename);
                _FullSizeImagePath = string.Format("{0}{1}", FULLSIZELPATH, filename);

                _presenter.SaveEventTheme();

                lblErrorMessage.Text = "Event Theme has been added successfully!";
                txtEventThemeName.Text = string.Empty;
                txtBackgroundcolor.Text = string.Empty;
            }
            catch(Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Event Theme name already exists!";
                txtEventThemeName.Text = "";
                txtBackgroundcolor.Text = "";
            }

        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminThemeUpload.aspx");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddInvitationCategory_Click(object sender, EventArgs e)
    {
        pnlInvitationCategory.Visible = true;
        lblErrorMessage.Text = string.Empty;
        txtInvitationCategory.Text = "";
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        try
        {
            _presenter.SaveInvitationCategory();
            pnlInvitationCategory.Visible = false;
            LoadInvitationCategory();
            this.LoadCategory();
            lblErrorMessage.Text = "Event invitation category successfully added!";
        }
        catch (Exception ex)
        {
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = "Event invitation category already exists!";
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelCategory_Click(object sender, EventArgs e)
    {
        pnlInvitationCategory.Visible = false;
        lblErrorMessage.Text = string.Empty;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTributeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblErrorMessage.Text = string.Empty;
        this._presenter.LoadInvitationCategory();
        this.LoadCategory();        

    }
    
    #endregion

    #region Properties
    [CreateNew]
    public AdminThemeUploadPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    #region IAdminThemeUpload Members

    public string TributeType
    {
        get { return ddlTributeType.SelectedItem.Text; }
    }

    public int TributeTypeID
    {
        get { return int.Parse(ddlTributeType.SelectedItem.Value); }
    }

    public int InvitationCategoryID
    {
        get { return int.Parse(ddlInvitationCategory.SelectedItem.Value); }
    }

    public string EventThemeName
    {
        get { return txtEventThemeName.Text; }
    }

    public string ThemeBackgroundColor
    {
        get { return txtBackgroundcolor.Text; }
    }

    public string ThumbnailImagePath
    {
        get { return _ThumbnailImagePath; }
        set { _ThumbnailImagePath = value; }
    }

    public string PreviewImagePath
    {
        get { return _PreviewImagePath; }
        set { _PreviewImagePath = value; }
    }

    public string FullSizeImagePath
    {
        get { return _FullSizeImagePath; }
        set { _FullSizeImagePath = value; }
    }
    public IList<EventInvitationCategory> EventInvitationCategoryList
    {
        set
        {
            _eventInvitationCategoryList = value;
        }
    }
    public string InvitationCategoryName
    {
        get
        {
            return txtInvitationCategory.Text;
        }
        set
        {
            _invitationCategoryName = value;
        }

    }

    #endregion

    #endregion

    #region Methods
    /// <summary>
    /// 
    /// </summary>
    private void LoadCategory()
    {
        ddlInvitationCategory.DataSource = _eventInvitationCategoryList;
        ddlInvitationCategory.DataTextField = "InvitationCategoryName";
        ddlInvitationCategory.DataValueField = "InvitationCategoryID";
        ddlInvitationCategory.DataBind();

        ddlInvitationCategory.Items.Insert(0, new ListItem("Select Invitation category...", "0"));

    }


    /// <summary>
    /// 
    /// </summary>
    private void LoadInvitationCategory()
    {
        this._presenter.LoadInvitationCategory();
    }
    /// <summary>
    /// 
    /// </summary>
    private void LoadTributeType()
    {
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            ddlTributeType.Items.Insert(0, new ListItem("Select Website Type......", "0"));
            ddlTributeType.Items.Add(new ListItem("New Baby", "2"));
            ddlTributeType.Items.Add(new ListItem("Birthday", "3"));
            ddlTributeType.Items.Add(new ListItem("Graduation", "4"));
            ddlTributeType.Items.Add(new ListItem("Wedding", "5"));
            ddlTributeType.Items.Add(new ListItem("Anniversary", "6"));            
        }
        else
        {
            ddlTributeType.Items.Insert(0, new ListItem("Select Tribute Type......", "0"));
            ddlTributeType.Items.Add(new ListItem("New Baby", "2"));
            ddlTributeType.Items.Add(new ListItem("Birthday", "3"));
            ddlTributeType.Items.Add(new ListItem("Graduation", "4"));
            ddlTributeType.Items.Add(new ListItem("Wedding", "5"));
            ddlTributeType.Items.Add(new ListItem("Anniversary", "6"));
            ddlTributeType.Items.Add(new ListItem("Memorial", "7"));
        }
        
    }
        
    #endregion


}//end class
