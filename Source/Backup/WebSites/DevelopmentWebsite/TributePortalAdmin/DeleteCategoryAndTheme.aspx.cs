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

public partial class TributePortalAdmin_DeleteCategoryAndTheme : System.Web.UI.Page, IAdminThemeUpload
{

    #region CLASS VARIABLES
    private AdminThemeUploadPresenter _presenter;
    private IList<EventInvitationCategory> _eventInvitationCategoryList;   
    private string _invitationCategoryName;
    private string _ThumbnailImagePath = "";
    private string _PreviewImagePath = "";
    private string _FullSizeImagePath = "";

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            lblTributeType.Text = "Select Website Type";
        } 

        if (!Page.IsPostBack)
        {
            this._presenter.OnViewInitialized();

            LoadTributeType();

            LoadInvitationCategory();

            LoadCategory();
            lblDelCategoryMessage.Visible = false;
            lblDelThemeMessage.Visible = false;
            lblNoCategoryExist.Visible = false;
            lblNoThemeExist.Visible = false;

        }
        this._presenter.OnViewLoaded();
    }

    protected void ddlTributeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.LoadInvitationCategory();
        this.LoadCategory();
        PopulateCategory(ddlTributeType.SelectedValue.ToString());
    }

    protected void ddlInvitationCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateTheme(Convert.ToInt32(ddlInvitationCategory.SelectedValue.ToString()), ddlTributeType.SelectedValue.ToString());
    }

    private void PopulateCategory(string tributeType)
    {
        gridCategories.Visible = true;
        GridThemes.Visible = false;
        gridCategories.DataSource = _presenter.GetEventInvitationCategories(tributeType);
        gridCategories.DataBind();
        if (gridCategories.Rows.Count != 0)
        {
            lblDelCategoryMessage.Visible = true;
            lblDelThemeMessage.Visible = false;
            lblNoCategoryExist.Visible = false;
            lbl_delCategoryName.Visible = false;
            lblNoThemeExist.Visible = false;
        }
        else
        {
            lblDelCategoryMessage.Visible = false;
            lblDelThemeMessage.Visible = false;
            lblNoCategoryExist.Visible = true;
            lbl_delCategoryName.Visible = false;
            lblNoThemeExist.Visible = false;
        }
        if (ddlTributeType.SelectedValue.Equals(0))
        {
            lblNoCategoryExist.Visible = false;
            lblNoThemeExist.Visible = false;
        }

    }
    private void PopulateTheme(int invitationCategory, string tributeType)
    {
        GridThemes.Visible = true;
        gridCategories.Visible = false;
        GridThemes.DataSource = _presenter.GetEventThemeInfo(invitationCategory, tributeType);
        GridThemes.DataBind();
        if (GridThemes.Rows.Count != 0)
        {
            lblDelCategoryMessage.Visible = false;
            lblDelThemeMessage.Visible = true;
            lblNoCategoryExist.Visible = false;
            lblNoThemeExist.Visible = false;

        }
        else
        {
            lblDelCategoryMessage.Visible = false;
            lblNoCategoryExist.Visible = false;
            lblDelThemeMessage.Visible = false;
            lblNoThemeExist.Visible = true;
        }
        if (ddlInvitationCategory.SelectedItem.Text.Contains("Select Invitation"))
        {
            lblNoThemeExist.Visible = false;
            PopulateCategory(ddlTributeType.SelectedValue.ToString());
        }
    }



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
    #endregion

    #region Methods
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


    protected void GridRowDeleteCategory(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowId = Convert.ToInt32(btn.CommandArgument.ToString());
        //btn.Attributes.Add("onclick",  "return confirm('are you Sure To delete this Record ?');"); 
        _presenter.Deletecategory(rowId);
        lbl_delCategoryName.Text = "Category " + btn.CommandName.ToString() + " deleted !!";
        lbl_delCategoryName.Visible = true;
        lblDelCategoryMessage.Visible = false;
        PopulateCategory(ddlTributeType.SelectedValue.ToString());
        this._presenter.LoadInvitationCategory();
        this.LoadCategory();
        lbl_delCategoryName.Visible = true;
    }
    protected void GridRowDeleteThemes(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowId = Convert.ToInt32(btn.CommandArgument.ToString());
        //btn.Attributes.Add("onclick",  "return confirm('are you Sure To delete this Record ?');"); 
        _presenter.DeleteTheme(rowId);
        lbl_delCategoryName.Text = "Theme " + btn.CommandName.ToString() + " deleted !!";
        lblDelThemeMessage.Visible = false;
        PopulateTheme(Convert.ToInt32(ddlInvitationCategory.SelectedValue.ToString()), ddlTributeType.SelectedValue.ToString());
        lbl_delCategoryName.Visible = true;
    }
    protected void gridCategories_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridCategories.PageIndex = e.NewPageIndex;
        gridCategories.DataSource = _presenter.GetEventInvitationCategories(ddlTributeType.SelectedValue.ToString());
        gridCategories.DataBind();
    }

    protected void GridThemes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridThemes.PageIndex = e.NewPageIndex;
        GridThemes.DataSource = _presenter.GetEventThemeInfo(Convert.ToInt32(ddlInvitationCategory.SelectedValue.ToString()), ddlTributeType.SelectedValue.ToString());
        GridThemes.DataBind();
    }

}
    #endregion
