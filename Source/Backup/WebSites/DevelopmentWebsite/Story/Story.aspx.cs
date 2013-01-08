///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Story.Story.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the stories added by a user for a selected tribute
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
using System.IO;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Story.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;
using System.Text.RegularExpressions;

#endregion

/// <summary>
///Tribute Portal-Story UI Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the UI class Story_ViewStory for the Story View, Edit and Delete. This will implement the 
// All the Properties in the IViewStory interface. and will extend PageBase class which provides 
// 1. Error Event Handler
// 2. Exception handling
/// </summary>

public partial class Story_Story : PageBase, IViewStory
{

    #region CLASS VARIABLES

    private ViewStoryPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;

    private int _UserId;
    protected int _TributeId;
    private int _SectionId;
    private bool _IsAdmin;
    private int _UserBiographyId;
    private string _PrimaryTitle;
    private string _SecondaryTitle;
    private string _SectionAnswer;
    private string _NewTopic;
    private string _Operation;
    protected string _TributeType;
    protected string _TributeName;
    private string _TributeURL;
    protected string _NewBaby;
    protected string _Memorial;
    protected string _Birhday;
    protected bool _isActive;
    protected int _RowCount;
    private string StoryImageURL = "";
    private string _FirstName = "";
    private string _LastName = "";

   // private string _Themename; //by ud
    private int _Row = -1;

    private string _postMessage;
    private string _messageWithoutHtml;

    #endregion


    #region CONSTANT

    private const string DefaultPath = "../assets/images/Story";

    protected string todayDay = DateTime.Now.Day.ToString();
    protected string todayMonth = DateTime.Now.Month.ToString();
    protected string todayYear = DateTime.Now.Year.ToString();

    #endregion


    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
          
        try
        {
            GetValuesFromSession();

            //Start - Modification on 9-Dec-09 for the enhancement 3 of the Phase 1
            if (_TributeName != null) Page.Title = _TributeName + " | Story";
            //End

            SetControlsValue();
            // aTributeHome.HRef = Session["APP_PATH"] + _TributeURL + "/" + this.Master.query_string;
            if (!this.IsPostBack)
            {
                valNameExp.ErrorMessage = "Invalid " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " Name,* and ? is not allowed,Please try again. ";
                lblNameHint.Text = "Enter the name of the person or people who this " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " is for, for example: 'John Spitfire Williams', or 'Aunt Jill'. You are limited to 40 characters.";

                this._presenter.OnViewInitialized();

                this._presenter.GetStoryDetail();

                AddAttributes();

                PopulatePersonalDetailEditControl();
            }

            this._presenter.OnViewLoaded();

            SetTributeImageUrl();

            if (!(string.IsNullOrEmpty(_TributeType)))
            {
                if (objSessionValue != null)
                {                    
                    if (_TributeType.Equals("Memorial"))
                    {
                        panObituaryView.Attributes.Add("style", "display:block;");
                       // panObituaryView.Visible = true;
                        if (IsAdmin == true)
                            lbtnEditObituary.Attributes.Add("style", "display:block;");
                        else
                            lbtnEditObituary.Attributes.Add("style", "display:none;");
                       // lbtnEditObituary.Visible = IsAdmin;
                    }
                }

            }
              // Made by Ashu(15 june,2011) For cuteeditor Safari issue


            //if (ftbNoteMessage.Attributes["class"] == "display_block")
            //{
            //    panObituaryView.Attributes.Add("style", "display:none;");
            //    lbtnEditPersonalDetail.Attributes.Add("style", "display:none;");
            //    lbtnEditStory.Attributes.Add("style", "display:none;");
            //    lbtnAddMoreAbout.Attributes.Add("style", "display:none;");
            //    //panObituaryView.Visible = false;
            //    //lbtnEditPersonalDetail.Visible = false;
            //    //lbtnEditStory.Visible = false;
            //    //lbtnAddMoreAbout.Visible = false;
            //}

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnEditPersonalDetail_Click(object sender, EventArgs e)
    {
        try
        {
            // Display Personal Detail Edit mode and Hide the view mode
            this._presenter.VisiblePersonalDetailEdit();

            // Add Current satte of the page in View State
            object[] objValue = { false, true, true, true };
            AddValuesInViewState(objValue, Stories.StoryMaintainState.StoryPage_CurrentState.ToString());

            // Populate value in edit control by the View control
            PopulatePersonalDetailEditControl();

            // Set the defualt focus of teh page
            Page.SetFocus(txtName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // get the State list for selected country
            this._presenter.GetStateList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void lbtnSavePersonalDetail_Click(object sender, EventArgs e)
    {
        try
        {
            SaveImage();
            StoryImageURL = hdnStoryImageURL.Value.ToString();

            this._presenter.UpdateTributeDetail();

            VisibleViewMode();

            if (_TributeName.ToString() != txtName.Text.ToString())
            {
                _TributeName = txtName.Text;

                if (Session["TributeSession"] == null)
                    CreateTributeSession(); //to create the tribute session values if user comest o this page from link or from favorites list.

                //string queryString = "?TributeId=" + _TributeId + "&TributeName=" + _TributeName;
                //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Story.ToString()) + queryString, false);
                //Response.Redirect("story.aspx", false);
                Response.Redirect(Context.Request.RawUrl, false);
            }
            else
            {
                this._presenter.GetStoryDetail();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnCancelPersonalDetail_Click(object sender, EventArgs e)
    {
        try
        {
            VisibleViewMode();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnEditStory_Click(object sender, EventArgs e)
    {
        try
        {
            this._presenter.VisibleStoryEdit();

            if (lblStoryDetail.Text != ResourceText.GetString("lblStoryDetail_ST"))
            {
                txtStoryDetail.Text = lblStoryDetail.Text.Replace("<br />", "\n");
            }
            else
            {
                txtStoryDetail.Text = ResourceText.GetString("txtStoryDetail_ST");
            }

            // Add Current satte of the page in View State
            object[] objValue = { true, false, true, true };
            AddValuesInViewState(objValue, Stories.StoryMaintainState.StoryPage_CurrentState.ToString());

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnSaveStory_Click(object sender, EventArgs e)
    {
        try
        {
            _PrimaryTitle = Stories.StorySectionEnum.Story.ToString();
            _SecondaryTitle = "";

            this._presenter.UpdateStoryDetail();

            VisibleViewMode();

            this._presenter.GetStoryDetail();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnCancelStory_Click(object sender, EventArgs e)
    {
        try
        {
            VisibleViewMode();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnEditObituary_Click(object sender, EventArgs e)
    {
        try
        {
            //this._presenter.VisibleStoryEdit();
            panObituaryView.Attributes.Add("style", "display:none;");
           // panObituaryView.Visible = false;
            lbtnEditPersonalDetail.Visible = false;
            lbtnEditStory.Visible = false;
            lbtnAddMoreAbout.Visible = false;

            panObituaryEdit.Visible = true;
            ftbNoteMessage.Visible = true;
            //ftbNoteMessage.ToolbarStyleConfiguration = FreeTextBoxControls.ToolbarStyleConfiguration.NotSet;
            //ftbNoteMessage.BackColor = System.Drawing.ColorTranslator.FromHtml("#f4f4f2");
            //ftbNoteMessage.GutterBackColor = System.Drawing.ColorTranslator.FromHtml("#f4f4f2");


            // Add Current satte of the page in View State
            object[] objValue = { true, false, true, true, true };
            AddValuesInViewState(objValue, Stories.StoryMaintainState.StoryPage_CurrentState.ToString());

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnSaveObituary_Click(object sender, EventArgs e)
    {
        try
        {
            this._presenter.UpdateObituaryDetail();
            Literal2.Text = ftbNoteMessage.Text.ToString();
            VisibleViewMode();

            this._presenter.GetStoryDetail();
            panObituaryEdit.Attributes.Add("style","display:none;");
            //panObituaryEdit.Visible = false;
            panObituaryView.Attributes.Add("style", "display:block;");
           // panObituaryView.Visible = true;
           lbtnAddMoreAbout.Attributes.Add("style", "display:block;");
            //lbtnAddMoreAbout.Visible = true;
          

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnCancelObituary_Click(object sender, EventArgs e)
    {
        try
        {
            VisibleViewMode();
            panObituaryEdit.Attributes.Add("style", "display:none;");
           // panObituaryEdit.Visible = false;
            panObituaryView.Attributes.Add("style", "display:block;");
           // panObituaryView.Visible = true;
            lbtnAddMoreAbout.Attributes.Add("style", "display:block;");
           // lbtnAddMoreAbout.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void repMoreAbout_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton btnDelete = (LinkButton)e.Item.FindControl("lbtnDeleteTopicMoreAbout");
        btnDelete.Text = ResourceText.GetString("lbtnDeleteTopicMoreAbout_ST");
        btnDelete.Attributes.Add("onclick", "if(confirm('" + ResourceText.GetString("msgDelete_ST") + "')){}else{return false}");

        LinkButton btnCancel = (LinkButton)e.Item.FindControl("lbtnCancelMoreAbout");
        btnCancel.Text = ResourceText.GetString("lbtnCancel_ST");

        LinkButton btnSaveTopic = (LinkButton)e.Item.FindControl("lbtnSaveTopicMoreAbout");
        btnSaveTopic.Text = ResourceText.GetString("lbtnSave_ST");

        LinkButton btnEditTopic = (LinkButton)e.Item.FindControl("lbtnEditTopic");
        btnEditTopic.Text = ResourceText.GetString("lbtnEdit_ST");

        RequiredFieldValidator valTopicMoreAbout = (RequiredFieldValidator)e.Item.FindControl("valOtherTopicMoreAbout");
        valTopicMoreAbout.ErrorMessage = ResourceText.GetString("valAddNewtopic_ST");

        RequiredFieldValidator valTopicAnswer = (RequiredFieldValidator)e.Item.FindControl("valTopicAnswerMoreAbout");
        valTopicAnswer.ErrorMessage = ResourceText.GetString("valTopicAnswer_ST");

        TextBox txtTopicAnswer = (TextBox)e.Item.FindControl("txtTopicAnswerMoreAbout");
        txtTopicAnswer.Attributes.Add("onkeyup", "CheckTopicLength();");
    }

    protected void repMoreAbout_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            _Row = e.Item.ItemIndex;

            Panel addPanel = (Panel)e.Item.FindControl("panMoreAboutAddNewTopic");
            Panel viewPanel = (Panel)e.Item.FindControl("panMoreAboutViewTopic");

            DropDownList tmpList = (DropDownList)e.Item.FindControl("ddlTopicListMoreAbout");

            _SecondaryTitle = ((Label)e.Item.FindControl("lblSecondaryTitle")).Text;

            if (e.CommandName == "Edit")
            {
                this._presenter.VisibleMoreAboutEdit();

                // Add Current satte of the page in View State
                object[] objValue = { true, true, false, true };
                AddValuesInViewState(objValue, Stories.StoryMaintainState.StoryPage_CurrentState.ToString());

                RedirectPage(addPanel, viewPanel, false);

                if (tmpList != null)
                {
                    this._presenter.GetTopic();

                    ListItem tmpItem = tmpList.Items.FindByValue(_SecondaryTitle);
                    if (tmpItem == null)
                    {
                        string otherTopic = this._presenter.GetEnumValueDescription(Stories.StorySectionEnum.OtherTopic);
                        tmpList.SelectedValue = otherTopic;
                        TextBox txtTopic = (TextBox)e.Item.FindControl("txtOtherTopicMoreAbout");
                        if (txtTopic != null)
                        {
                            txtTopic.Visible = true;
                            txtTopic.Text = ((Label)e.Item.FindControl("lblSecondaryTitle")).Text;
                        }
                    }
                    else
                    {
                        tmpList.SelectedValue = tmpItem.Text;
                    }
                }

                string strTopic = ((Label)e.Item.FindControl("lblSectionAnswer")).Text;
                ((TextBox)e.Item.FindControl("txtTopicAnswerMoreAbout")).Text = strTopic.Replace("<br />", "\n");
            }
            else if (e.CommandName == "Save")
            {
                _SectionAnswer = ((TextBox)e.Item.FindControl("txtTopicAnswerMoreAbout")).Text;

                _SectionId = int.Parse(((Label)e.Item.FindControl("lblSectionId")).Text);
                _UserBiographyId = int.Parse(((Label)e.Item.FindControl("lblUserBiographyId")).Text);

                _Operation = Stories.OperationEnum.Update.ToString();
                _PrimaryTitle = this._presenter.GetEnumValueDescription(Stories.StorySectionEnum.MoreAbout);

                _NewTopic = ((TextBox)e.Item.FindControl("txtOtherTopicMoreAbout")).Text;

                if (tmpList != null)
                {
                    _SecondaryTitle = tmpList.SelectedValue;
                }

                if (_SecondaryTitle == "Select a topic:")
                {
                    ShowMessage(ResourceText.GetString("valSelectTopicAdd_ST"));
                    return;
                }
                else if (_SecondaryTitle == "Other Topic")
                {
                    if (_NewTopic == "")
                    {
                        ShowMessage(ResourceText.GetString("valAddNewtopic_ST"));
                        return;
                    }
                }

                this._presenter.SaveTopic();

                RedirectPage(addPanel, viewPanel, true);
            }
            else if (e.CommandName == "Delete")
            {
                _SectionId = int.Parse(((Label)e.Item.FindControl("lblSectionId")).Text);
                _UserBiographyId = int.Parse(((Label)e.Item.FindControl("lblUserBiographyId")).Text);

                this._presenter.DeleteTopic();

                RedirectPage(addPanel, viewPanel, true);

            }
            else if (e.CommandName == "Cancel")
            {
                RedirectPage(addPanel, viewPanel, true);
            }

            _Row = -1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlTopicListMoreAbout_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= repMoreAbout.Items.Count - 1; i++)
            {
                DropDownList ddlList = (DropDownList)repMoreAbout.Items[i].FindControl("ddlTopicListMoreAbout");
                if (ddlList != null)
                {
                    string otherTopic = this._presenter.GetEnumValueDescription(Stories.StorySectionEnum.OtherTopic);
                    if (ddlList.SelectedValue == otherTopic)
                    {
                        TextBox txtTopic = (TextBox)repMoreAbout.Items[i].FindControl("txtOtherTopicMoreAbout");
                        txtTopic.Visible = true;
                    }
                    else
                    {
                        TextBox txtTopic = (TextBox)repMoreAbout.Items[i].FindControl("txtOtherTopicMoreAbout");
                        txtTopic.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnAddMoreAbout_Click(object sender, EventArgs e)
    {
        try
        {
            //display add panel

            this._presenter.VisibleAddNewTopic();

            // Add Current satte of the page in View State
            object[] objValue = { true, true, true, false };
            AddValuesInViewState(objValue, Stories.StoryMaintainState.StoryPage_CurrentState.ToString());

            txtTopicAnswer.Text = "";

            //if (txtOtherTopic.Text != "Type Your Topic Here")
            //{
            //    txtOtherTopic.Text = "Type Your Topic Here";
            //}

            txtOtherTopic.Visible = false;
            _SecondaryTitle = "";

            this._presenter.GetTopic();

            ddlTopicList.Focus();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlTopicList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string otherTopic = this._presenter.GetEnumValueDescription(Stories.StorySectionEnum.OtherTopic);

            if (ddlTopicList.SelectedValue == otherTopic)
            {
                txtOtherTopic.Visible = true;
                txtOtherTopic.Text = "Type Your Topic Here";
            }
            else
            {
                txtOtherTopic.Visible = false;
            }
            txtTopicAnswer.Focus();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnSaveTopic_Click(object sender, EventArgs e)
    {
        try
        {
            _Operation = Stories.OperationEnum.Add.ToString();
            _PrimaryTitle = this._presenter.GetEnumValueDescription(Stories.StorySectionEnum.MoreAbout);

            _SectionAnswer = txtTopicAnswer.Text;
            _SecondaryTitle = ddlTopicList.SelectedValue;
            _NewTopic = txtOtherTopic.Text;

            this._presenter.SaveTopic();

            VisibleViewMode();

            this._presenter.GetStoryDetail();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnCancelTopic_Click(object sender, EventArgs e)
    {
        try
        {
            VisibleViewMode();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion


    #region PROPERTIES

    [CreateNew]
    public ViewStoryPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    #region IViewStory Members

    public string TributeName
    {
        set
        {
            lblNameView.Text = value;
            imgTributeImageView.AlternateText = imgStoryImage.Alt = value;
        }

        get
        {
            string name = txtName.Text.ToString().Trim();
            if (name.Length > 40)
            {
                return name.Substring(0, 40);
            }
            else
            {
                return name;
            }
        }
    }

    public string TributeType
    {
        get
        {
            return _TributeType;
        }

        set
        {
            TributeType = value;
        }
    }

    public string TributeImage
    {
        get
        {
            return hdnStoryImageURL.Value.ToString();
        }
        set
        {
            string[] virtualDir = CommonUtilities.GetPath();

            if (virtualDir != null)
            {
                hdnStoryImageURL.Value = virtualDir[2] + value;
                imgTributeImageView.ImageUrl = virtualDir[2] + value;
                imgTributeImageView.AlternateText = _TributeName;
            }
        }
    }


    public string Date1Value
    {
        set
        {
            lblDate1View.Text = value;
        }
    }

    public string Date2Value
    {
        set
        {
            lblDate2View.Text = value;
        }
    }

    public string Date1Text
    {
        set
        {
            lblDate1.Text = value;
            lblDate1Edit.InnerHtml += value;
        }
    }

    public bool Date1VisibleView
    {
        set
        {
            lblDate1View.Visible = value;
        }
    }

    public bool Date2VisibleView
    {
        set
        {
            panDate2View.Visible = value;
        }
    }

    public string Date2Text
    {
        set
        {
            lblDate2.Text = value;
            lblDate2Edit.InnerHtml += value;
        }
    }

    public bool Date2VisibleEdit
    {
        set
        {
            divDate2Edit.Visible = value;
        }
    }
    public string Date1Day
    {
        set
        {
            ddlDate1Day.SelectedValue = value;
        }

        get
        {
            if (ddlDate1Day.Visible == true)
            {
                return ddlDate1Day.SelectedValue;
            }
            else
            {
                return null;
            }

        }
    }

    public string Date1Month
    {
        set
        {
            ddlDate1Month.SelectedValue = value;
        }

        get
        {
            if (ddlDate1Month.Visible == true)
            {
                return ddlDate1Month.SelectedValue;
            }
            else
            {
                return null;
            }
        }
    }

    public string Date1Year
    {
        set
        {
            txtDate1Year.Text = value;
        }

        get
        {
            if (txtDate1Year.Visible == true)
            {
                if ((txtDate1Year.Text == null) || (txtDate1Year.Text == ""))
                {
                    txtDate1Year.Text = "1780";
                }

                return txtDate1Year.Text;
            }
            else
            {
                return null;
            }
        }
    }

    public string Date2Day
    {
        set
        {
            ddlDate2Day.SelectedValue = value;
        }

        get
        {
            if (ddlDate2Day.Visible == true)
            {
                return ddlDate2Day.SelectedValue;
            }
            else
            {
                return null;
            }
        }
    }

    public string Date2Month
    {
        set
        {
            ddlDate2Month.SelectedValue = value;
        }

        get
        {
            if (ddlDate2Month.Visible == true)
            {
                return ddlDate2Month.SelectedValue;
            }
            else
            {
                return null;
            }
        }
    }

    public string Date2Year
    {
        set
        {
            txtDate2Year.Text = value;
        }

        get
        {
            if (txtDate2Year.Visible == true)
            {
                return txtDate2Year.Text;
            }
            else
            {
                return null;
            }
        }
    }

    public bool isVisibleDate2
    {
        set
        {
            if (value)
            {
                lblDate1Edit.InnerText = ResourceText.GetString("lblDate1Born_ST");
                lblDate2Edit.InnerHtml += lblDate1.Text;
            }
            else
            {
                lblDate2Edit.InnerHtml += ResourceText.GetString("lblDate1Due_ST");
            }
        }
    }

    public bool isRequiredFieldDate1
    {
        set
        {
            if (value)
            {
                lblDate1Edit.InnerHtml = "<em class='required'>* </em>";
            }
            else
            {
                lblDate1Edit.InnerHtml = "";
            }
        }
    }

    public bool isRequiredFieldDate2
    {
        set
        {
            if (value)
            {
                lblDate2Edit.InnerHtml = "<em class='required'>* </em>";
            }
            else
            {
                lblDate2Edit.InnerHtml = "";
            }
        }
    }


    public string Age
    {
        set
        {
            lblAgeView.Text = value.ToString();
        }
    }

    public string City
    {
        set
        {
            txtCity.Text = value;
        }

        get
        {
            string city = txtCity.Text.ToString().Trim();
            if (city.Length > 50)
            {
                return city.Substring(0, 50);
            }
            else
            {
                return city;
            }
        }
    }

    public string State
    {
        set
        {
            ddlState.SelectedValue = value;
        }

        get
        {
            return ddlState.SelectedValue.ToString();
        }
    }

    public string Country
    {
        set
        {
            ddlCountry.SelectedValue = value;
        }

        get
        {
            return ddlCountry.SelectedValue.ToString();
        }
    }

    public string Location
    {
        set
        {
            lblLocationView.Text = value;
        }

        get
        {
            return ddlCountry.SelectedValue.ToString();
        }
    }

    public int UserID
    {
        get
        {
            return _UserId;
        }

        set
        {
            UserID = value;
        }
    }

    public int TributeID
    {
        get
        {
            return _TributeId;
        }

        set
        {
            TributeID = value;
        }
    }

    public int SectionId
    {
        get
        {
            return _SectionId;
        }

        set
        {
            SectionId = value;
        }
    }

    public int UserBiographyId
    {
        get
        {
            return _UserBiographyId;
        }

        set
        {
            UserBiographyId = value;
        }
    }

    public string StoryDetail
    {
        set
        {
            panStoryEdit.Visible = false;
            panStoryView.Visible = true;

            if ((value != null) && (value != ""))
            {
                lblStoryDetail.Text = value;
            }
        }

        get
        {
            string story = txtStoryDetail.Text.ToString().Trim();
            if (story.Length > 5000)
            {
                return story.Substring(0, 5000);
            }
            else
            {
                return story;
            }
        }
    }

    public IList<StoryMoreAbout> MoreAbout
    {
        set
        {
            repMoreAbout.DataSource = value;
            repMoreAbout.DataBind();
        }
    }

    public IList<Locations> CountryList
    {
        set
        {
            ddlCountry.DataSource = value;
            ddlCountry.DataTextField = Locations.Location.LocationName.ToString();
            ddlCountry.DataValueField = Locations.Location.LocationId.ToString();
            ddlCountry.DataBind();
        }
    }

    public IList<Locations> StateList
    {
        set
        {
            ddlState.Items.Clear();
            if (value.Count > 0)
            {
                ddlState.DataSource = value;
                ddlState.DataTextField = Locations.Location.LocationName.ToString();
                ddlState.DataValueField = Locations.Location.LocationId.ToString();
                ddlState.DataBind();
                ddlState.Enabled = true;
            }
            else
                ddlState.Enabled = false;
        }
    }

    public string PrimaryTitle
    {
        get
        {
            return _PrimaryTitle;
        }

        set
        {
            PrimaryTitle = value;
        }
    }

    public string SecondaryTitle
    {
        get
        {
            string title = _SecondaryTitle.ToString().Trim();
            if (title.Length > 80)
            {
                return title.Substring(0, 80);
            }
            else
            {
                return title;
            }
        }

        set
        {
            SecondaryTitle = value;
        }
    }

    public string SectionAnswer
    {
        get
        {
            string answer = _SectionAnswer.ToString().Trim();
            if (answer.Length > 5000)
            {
                return answer.Substring(0, 5000);
            }
            else
            {
                return answer;
            }
        }

        set
        {
            SectionAnswer = value;
        }
    }

    public string NewTopic
    {
        get
        {
            string newtopic = _NewTopic.ToString().Trim();
            if (newtopic.Length > 80)
            {
                return newtopic.Substring(0, 80);
            }
            else
            {
                return newtopic;
            }
        }
    }

    public string Operation
    {
        get
        {
            return _Operation;
        }

        set
        {
            Operation = value;
        }
    }

    public IList<StoryMoreAbout> TopicList
    {
        set
        {
            _RowCount = value.Count;

            if (_Row == -1)
            {
                ddlTopicList.DataSource = value;
                ddlTopicList.DataTextField = StoryMoreAbout.StoriesMoreAboutEnum.SecondaryTitle.ToString();
                ddlTopicList.DataBind();
            }
            else
            {
                for (int i = 0; i <= repMoreAbout.Items.Count - 1; i++)
                {
                    if (i == _Row)
                    {
                        DropDownList tmpList = (DropDownList)repMoreAbout.Items[i].FindControl("ddlTopicListMoreAbout");
                        if (tmpList != null)
                        {
                            tmpList.DataSource = value;
                            tmpList.DataTextField = StoryMoreAbout.StoriesMoreAboutEnum.SecondaryTitle.ToString();
                            tmpList.DataBind();
                        }
                    }
                }
            }
        }
    }

    public bool IsVisiblePersonalDetailView
    {
        set
        {
            panPersonalDetailView.Visible = value;
            panPersonalDetailEdit.Visible = !value;
            lbtnEditPersonalDetail.Visible = value;
        }
    }

    public bool IsVisibleObituaryView
    {
        set
        {
            if (value == true)
            {
                panObituaryView.Attributes.Add("style", "display:block");
                lbtnEditObituary.Attributes.Add("style", "display:block");
                panObituaryEdit.Attributes.Add("style", "display:none");
            }
            else
            {
                panObituaryView.Attributes.Add("style", "display:none");
                lbtnEditObituary.Attributes.Add("style", "display:none");
                panObituaryEdit.Attributes.Add("style", "display:block");
            }
            panObituaryView.Visible = value;
            lbtnEditObituary.Visible = value;

            panObituaryEdit.Visible = !value;
        }
    }

    public bool IsVisibleStoryView
    {
        set
        {
            panStoryView.Visible = value;
            lbtnEditStory.Visible = value;

            panStoryEdit.Visible = !value;
        }
    }



    public bool IsVisibleMoreAboutView
    {
        set
        {
            panAddNewTopic.Visible = false;

            VisibleMoreAboutEdit(value);

            panAddButton.Visible = value;
        }
    }

    public bool IsVisibleEdit
    {
        set
        {
            lbtnEditObituary.Visible = value;
            lbtnEditStory.Visible = value;
            lbtnEditPersonalDetail.Visible = value;

            VisibleMoreAboutEdit(value);

            panAddButton.Visible = value;
        }
    }

    public bool IsVisibleAddNewTopic
    {
        set
        {
            panAddNewTopic.Visible = value;
        }
    }

    public bool IsAdmin
    {
        get
        {
            object[] objVal = GetValuesFromViewState(Stories.StoryMaintainState.Story_Admin.ToString());

            if (objVal != null)
            {
                return bool.Parse(objVal[0].ToString());
            }
            else
            {
                return false;
            }
        }
        set
        {
            object[] objVal = { value };
            AddValuesInViewState(objVal, Stories.StoryMaintainState.Story_Admin.ToString());

            _IsAdmin = value;

            object[] objState = GetValuesFromViewState(Stories.StoryMaintainState.StoryPage_CurrentState.ToString());
            if (objState == null)
            {
                // Visible all the view mode
                this._presenter.VisibleAllViewMode();
            }
            else
            {
                if (bool.Parse(objState[0].ToString()) == false) // display personal detail view
                {
                    this._presenter.VisiblePersonalDetailEdit();
                }
                else if (bool.Parse(objState[1].ToString()) == false) // display story view
                {
                    this._presenter.VisibleStoryEdit();
                }
                else if (bool.Parse(objState[2].ToString()) == false) // display More about view
                {
                    this._presenter.VisibleMoreAboutEdit();
                }
                else if (bool.Parse(objState[3].ToString()) == false) // display Add new topic
                {
                    this._presenter.VisibleAddNewTopic();
                }
                else
                {
                    // Visible all the view mode
                    this._presenter.VisibleAllViewMode();
                }
            }

            if (!IsAdmin)
            {
                if (lblStoryDetail.Text == ResourceText.GetString("lblStoryDetail_ST"))
                    lblStoryDetail.Visible = false;
                else
                {
                    if (lblStoryDetail.Text != "TIP: Use the “Story” section to give visitors more detailed information on the person(s) this tribute was created for. Use the “More About” section to include biographical information, dates, places, milestones, history, significant events, and more. Click the EDIT button to begin adding your story.")
                        lblStoryDetail.Visible = true;
                    else
                        lblStoryDetail.Visible = false;
                }
            }
        }
    }

    public string CSSClassPersonalDetail
    {
        set
        {
            panPersonalDetailView.CssClass = value;
        }
    }

    public string CSSClassStory
    {
        set
        {
            panStoryView.CssClass = value;
        }
    }

    public string CSSClassMoreAbout
    {
        set
        {
            panMoreAbout.CssClass = value;
        }
    }

    public string CSSClassMoreAboutRows
    {
        set
        {
            for (int i = 0; i <= repMoreAbout.Items.Count - 1; i++)
            {
                if (i != _Row)
                {
                    Panel viewPanel = (Panel)repMoreAbout.Items[i].FindControl("panMoreAboutViewTopic");
                    viewPanel.CssClass = value;
                }
            }
        }
    }

    public string StoryImagePrevURL
    {
        set
        {
            string[] virtualDir = CommonUtilities.GetPath();
            ViewState[Stories.StoryMaintainState.StoryImageURL.ToString()] = virtualDir[2] + value;
        }
    }

    public object[] storyViewState
    {
        get
        {
            return (object[])ViewState[Stories.StorySectionEnum.Story.ToString()];
        }
        set
        {
            ViewState[Stories.StorySectionEnum.Story.ToString()] = value;
        }
    }

    public object[] LocationViewState
    {
        get
        {
            return (object[])ViewState[Stories.StoryMaintainState.Location.ToString()];
        }
        set
        {
            ViewState[Stories.StoryMaintainState.Location.ToString()] = value;
        }
    }

    public string FirstName
    {
        get
        {
            return _FirstName;
        }
    }

    public string LastName
    {
        get
        {
            return _LastName;
        }
    }

    public string UrlToEmail
    {
        get
        {
            string EmailHref = "http://" + _TributeType + "." + WebConfig.TopLevelDomain + "/" + _TributeURL + "/story.aspx" + "</a>";
            string QueryString = "?TributeId=" + _TributeId + "&TributeName=" + _TributeName + "&TributeType=" + _TributeType + "&TributeUrl=" + _TributeURL + "&mode=emailPage";
            string ApplicationPath = "<a href='http://" + Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;

            return ApplicationPath + "/Story/Story.aspx" + QueryString + "'>" + EmailHref;
        }
    }

    public string ObPostMessage
    {
        get
        {
            _postMessage = ftbNoteMessage.Text.ToString();
            return _postMessage;
        }
        set
        {
            _postMessage = value;
            if (!(string.IsNullOrEmpty(_postMessage)))
            {
                if (_TributeType.Equals("Memorial"))
                {
                    if (_postMessage.Length > 0)
                    {
                        panObituaryView.Attributes.Add("style", "display:block;");
                      //  panObituaryView.Visible = true;
                        Literal2.Text = _postMessage;
                        ftbNoteMessage.Text = _postMessage;
                    }
                }
            }
        }
    }
    public string ObMessageWithoutHtml
    {
        get
        {
            //_messageWithoutHtml = StripHtml(ftbNoteMessage.Text.ToString()); //ftbNoteMessage.HtmlStrippedText;
            _messageWithoutHtml = ftbNoteMessage.PlainText.ToString();
            _messageWithoutHtml = _messageWithoutHtml.Trim().Replace("/**/", "");
            _messageWithoutHtml = _messageWithoutHtml.Trim().Replace("\r\n", "");
            return _messageWithoutHtml;
        }
        set
        {
            _messageWithoutHtml = value;
        }
    }

    #endregion

    #endregion


    #region METHODS

    /// <summary>
    /// This function will show and hide the Edit button in More about Section
    /// </summary>
    /// <param name="value"></param>
    private void VisibleMoreAboutEdit(bool value)
    {
        try
        {
            for (int i = 0; i <= repMoreAbout.Items.Count - 1; i++)
            {
                LinkButton lbtn = (LinkButton)repMoreAbout.Items[i].FindControl("lbtnEditTopic");
                if (lbtn != null)
                {
                    lbtn.Visible = value;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// This function will populate the personal detail Edit mode vales from the view mode
    /// </summary>
    private void PopulatePersonalDetailEditControl()
    {
        try
        {
            imgStoryImage.Src = imgTributeImageView.ImageUrl;
            imgStoryImage.Alt = imgTributeImageView.AlternateText;

            txtName.Text = lblNameView.Text;

            lblAgeEdit.Text = lblAgeView.Text;

            ddlDate1Day.SelectedIndex = 0;
            ddlDate1Month.SelectedIndex = 0;
            txtDate1Year.Text = "";

            ddlDate2Day.SelectedIndex = 0;
            ddlDate2Month.SelectedIndex = 0;
            txtDate2Year.Text = "";

            if (lblDate1View.Text != "")
            {
                if (lblDate1.Text == ResourceText.GetString("lblDate1Due_ST"))
                {
                    DateTime tmpDate = DateTime.Parse(lblDate1View.Text);

                    ddlDate2Day.SelectedValue = tmpDate.Day.ToString();
                    ddlDate2Month.SelectedValue = tmpDate.Month.ToString();
                    txtDate2Year.Text = tmpDate.Year.ToString();
                }
                else
                {
                    DateTime tmpDate = DateTime.Parse(lblDate1View.Text);
                    char[] seperator = { ' ', ',' };
                    string[] dt = lblDate1View.Text.Split(seperator);

                    if (dt.Length == 4)
                    {
                        ddlDate1Day.SelectedValue = tmpDate.Day.ToString();
                        ddlDate1Month.SelectedValue = tmpDate.Month.ToString();
                        txtDate1Year.Text = tmpDate.Year.ToString();
                    }
                    else
                    {
                        ddlDate1Day.SelectedValue = tmpDate.Day.ToString();
                        ddlDate1Month.SelectedValue = tmpDate.Month.ToString();
                    }
                }
            }

            if (lblDate2View.Text != "")
            {
                DateTime tmpDate = DateTime.Parse(lblDate2View.Text);

                ddlDate2Day.SelectedValue = tmpDate.Day.ToString();
                ddlDate2Month.SelectedValue = tmpDate.Month.ToString();
                txtDate2Year.Text = tmpDate.Year.ToString();
            }

            if (lblStoryDetail.Text != ResourceText.GetString("lblStoryDetail_ST"))
            {
                txtStoryDetail.Text = lblStoryDetail.Text.Replace("<br />", "\n");
            }
            else
            {
                txtStoryDetail.Text = ResourceText.GetString("txtStoryDetail_ST");
            }
            //LHK: Obituary Text
            if (lblStoryDetail.Text != ResourceText.GetString("lblStoryDetail_ST"))
            {
                txtStoryDetail.Text = lblStoryDetail.Text.Replace("<br />", "\n");
            }
            else
            {
                txtStoryDetail.Text = ResourceText.GetString("txtStoryDetail_ST");
            }


            string date1 = "";
            if (lblDate1Edit.InnerHtml != "")
            {
                if (lblDate1Edit.InnerHtml.StartsWith("<em"))
                {
                    date1 = lblDate1Edit.InnerHtml.Substring(lblDate1Edit.InnerHtml.LastIndexOf('>') + 1, lblDate1Edit.InnerHtml.Length - lblDate1Edit.InnerHtml.LastIndexOf('>') - 2);
                }
                else
                {
                    date1 = lblDate1Edit.InnerHtml.Substring(0, lblDate1Edit.InnerHtml.Length - 1);
                }
            }
            valRequireDate1.ErrorMessage = date1 + " " + ResourceText.GetString("ErrMsgDate_ST");
            valCheckDate1.ErrorMessage = ResourceText.GetString("valCheckDate1_ST") + " " + date1 + ".";

            if (lblDate2Edit.InnerHtml != "")
            {
                date1 = "";
                if (lblDate2Edit.InnerHtml.StartsWith("<em"))
                {
                    date1 = lblDate2Edit.InnerHtml.Substring(lblDate2Edit.InnerHtml.LastIndexOf('>') + 1, lblDate2Edit.InnerHtml.Length - lblDate2Edit.InnerHtml.LastIndexOf('>') - 2);
                }
                else
                {
                    date1 = lblDate2Edit.InnerHtml.Substring(0, lblDate2Edit.InnerHtml.Length - 1);
                }

                valRequireDate2.ErrorMessage = date1 + " " + ResourceText.GetString("ErrMsgDate_ST");
                valCheckDate2.ErrorMessage = ResourceText.GetString("valCheckDate1_ST") + " " + date1 + ".";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// This function will hide and show the panel and redirect to View Mode
    /// </summary>
    /// <param name="addPanel">A panel object which is Add Panel of the MoreAbout section</param>
    /// <param name="viewPanel">A panel object which is view Panel of the MoreAbout section</param>
    /// <param name="value">A bool varaible if it is true then redirect to the same page</param>
    private void RedirectPage(Panel addPanel, Panel viewPanel, bool value)
    {
        try
        {
            if ((addPanel != null) && (viewPanel != null))
            {
                viewPanel.Visible = value;
                addPanel.Visible = !value;
            }

            if (value)
            {
                //string queryString = "?TributeId=" + _TributeId + "&TributeName=" + _TributeName;
                //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Story.ToString()) + queryString, false);
                //Response.Redirect("story.aspx", false);
                Response.Redirect(Context.Request.RawUrl, false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// This function will get the values (User Id and Tribute Detail) from the session
    /// </summary>
    private void GetValuesFromSession()
    {
        try
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            // get values from session
            StateManager objStateManager = StateManager.Instance;

            //to get user id from session as user is logged in user
            objSessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);
            if (objSessionValue != null)
            {
                _UserId = objSessionValue.UserId;
                _FirstName = objSessionValue.FirstName;
                _LastName = objSessionValue.LastName;
            }
            else
            {
                _IsAdmin = false;
            }

            //if user is coming through link
            if (Request.QueryString["mode"] != null)
            {
                if (Request.QueryString["TributeId"] != null)
                    _TributeId = int.Parse(Request.QueryString["TributeId"].ToString());

                if (Request.QueryString["TributeName"] != null)
                    _TributeName = Request.QueryString["TributeName"].ToString();

                if (Request.QueryString["TributeType"] != null)
                    _TributeType = Request.QueryString["TributeType"].ToString();

                if (Request.QueryString["TributeURL"] != null)
                    _TributeURL = Request.QueryString["TributeURL"].ToString();

                //to create the tribute session values if user comes o this page from link.
                if (Session["TributeSession"] == null)
                    CreateTributeSession();
            }
            //if (Request.QueryString["Id"] != null)
            //{
            //    int.TryParse(Request.QueryString["Id"].ToString(), out _TributeId);
            //    if (Session["TributeSession"] == null)
            //        CreateTributeSession();
            //}
            else
            {
                // to get tribute detail from session
                objTribute = (Tributes)objStateManager.Get(PortalEnums.SessionValueEnum.TributeSession.ToString(), StateManager.State.Session);
                if (objTribute != null)
                {
                    _TributeId = objTribute.TributeId;
                    _TributeName = objTribute.TributeName;
                    _TributeType = objTribute.TypeDescription;
                    _TributeURL = objTribute.TributeUrl;
                    _isActive = objTribute.IsActive;
                }
            }

            _Memorial = PortalEnums.TributeTypeEnum.Memorial.ToString();
            _NewBaby = this._presenter.GetEnumValueDescription(PortalEnums.TributeTypeEnum.NewBaby);
            _Birhday = PortalEnums.TributeTypeEnum.Birthday.ToString();

            if (_TributeId == 0)
            {
                //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
                Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/Error404.aspx");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method to create the tribute session values if user comes to this page from link or from favorites list.
    /// </summary>
    private void CreateTributeSession()
    {
        Tributes objTribute = new Tributes();
        objTribute.TributeId = _TributeId;
        objTribute.TributeName = _TributeName;
        objTribute.TypeDescription = _TributeType;
        objTribute.TributeUrl = _TributeURL;
        objTribute.IsActive = _isActive;
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add(PortalEnums.SessionValueEnum.TributeSession.ToString(), objTribute, TributesPortal.Utilities.StateManager.State.Session);
    }


    /// <summary>
    /// This Function will set the value of the control and error messages from the resource File
    /// </summary>
    private void SetControlsValue()
    {
        try
        {
            //Text for labels from the resource file
            lblPersonalDetailView.Text = lblPersonalDetailEdit.Text = ResourceText.GetString("lblPersonalDetail_ST");
            lblName.Text = ResourceText.GetString("lblName_ST");
            lblNameEdit.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblName_ST");
            //lblDate1.Text = lblDate1Edit.Text = ResourceText.GetString("lblDate1_ST");
            //lblDate2.Text = lblDate2Edit.Text = ResourceText.GetString("lblDate2_ST");
            lblDate1Month.InnerText = lblDate2Month.InnerText = ResourceText.GetString("lblDate1Month_ST");
            lblDate1Day.InnerText = lblDate2day.InnerText = ResourceText.GetString("lblDate1Day_ST");
            lblDate1Year.InnerText = lblDate2Year.InnerText = ResourceText.GetString("lblDate1Year_ST");
            lblAge.Text = ResourceText.GetString("lblAge_ST");
            lblLocation.Text = ResourceText.GetString("lblLocation_ST");
            lblCityEdit.InnerHtml = ResourceText.GetString("lblCity_ST");
            lblStateEdit.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblState_ST");
            lblCountryEdit.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblCountry_ST");
            lblStoryHeadView.Text = lblStoryHeadEdit.Text = ResourceText.GetString("lblStoryHead_ST");
            lblMoreAbout.Text = ResourceText.GetString("lblMoreAboutHead_ST");
            lblRequiredFields.InnerHtml = ResourceText.GetString("lblRequiredFields_ST") + "<em class='required'>* </em>";

            if (ddlDate1Month.Items.Count <= 0)
            {
                int i = 0;
                ListItem item = new ListItem("", i.ToString());

                ddlDate1Month.Items.Add(item);
                ddlDate2Month.Items.Add(item);
                for (i = 1; i <= 12; i++)
                {
                    string month = "Month" + i + "_ST";
                    item = new ListItem(ResourceText.GetString(month), i.ToString());
                    ddlDate1Month.Items.Add(item);
                    ddlDate2Month.Items.Add(item);
                }
            }

            lbtnAddMoreAbout.Text = ResourceText.GetString("lbtnAddMoreAbout_ST");
            lbtnCancelTopic.Text = lbtnCancelStory.Text = lbtnCancelPersonalDetail.Text = ResourceText.GetString("lbtnCancel_ST");
            lbtnSavePersonalDetail.Text = lbtnSaveTopic.Text = lbtnSaveStory.Text = ResourceText.GetString("lbtnSave_ST");
            lbtnEditStory.Text = lbtnEditPersonalDetail.Text = ResourceText.GetString("lbtnEdit_ST");

            //Text for error messages from the resource file
            valTributeName.ErrorMessage = ResourceText.GetString("valTributeName_ST");
            valCheckNewBaby.ErrorMessage = ResourceText.GetString("valCheckNewBaby_ST");
            valCheckDate1.ErrorMessage = valCheckDate2.ErrorMessage = ResourceText.GetString("valCheckDate1_ST");
            valCheckFutureDate1.ErrorMessage = valCheckFutureDate2.ErrorMessage = ResourceText.GetString("valCheckFutureDate_ST");
            valSelectTopicAdd.ErrorMessage = ResourceText.GetString("valSelectTopicAdd_ST");
            valAddNewtopic.ErrorMessage = ResourceText.GetString("valAddNewtopic_ST");
            valTopicAnswer.ErrorMessage = ResourceText.GetString("valTopicAnswer_ST");
            valStoryDetail.ErrorMessage = ResourceText.GetString("valStoryDetail_ST");
            valCheckDueDate.ErrorMessage = ResourceText.GetString("valCheckDueDate_ST");

            valNameExp.ErrorMessage = ResourceText.GetString("valNameExp_ST");
            valCity.ErrorMessage = ResourceText.GetString("valCity_ST");
            ValDateCompare.ErrorMessage = ResourceText.GetString("ValDateCompare_ST");

            string tributeHome;
            string storyUrl;
            if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
            {
                tributeHome = Session["APP_PATH"] + _TributeURL;
            }
            else
            {
                tributeHome = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." +
                    WebConfig.TopLevelDomain + "/" + _TributeURL;
            }
            tributeHome += "/";
            storyUrl = tributeHome + "Story.aspx";
            if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
            {
                storyUrl = storyUrl + this.Master.query_string;
                tributeHome = tributeHome + this.Master.query_string;
            }

            aTributeHome.HRef = tributeHome;
            storyWallTributeHome.Text = tributeHome;
            storyWallTributeHome1.Text = tributeHome;
            if (!(string.IsNullOrEmpty(_TributeName)))
            {
                string _strTributeName = _TributeName.Replace("\'", "\\'");
            }
            //string _strTributeName = _TributeName;
            //storyWallPostSubject.Text = string.Format("{0} updated story on the: {1} {2} Tribute", _FirstName, _strTributeName, _TributeType);
            storyWallPostSubject.Text = string.Format("{0} updated story on the: {1} {2} Tribute", _FirstName, _TributeURL, _TributeType);
            storyWallLink.Text = storyUrl;
            storyWallLink1.Text = storyUrl;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// This method will add the Attributes in the Date Controls
    /// </summary>
    private void AddAttributes()
    {
        try
        {
            txtStoryDetail.Attributes.Add("onclick", "return ClearContent();");
            txtStoryDetail.Attributes.Add("onkeyup", "CheckStoryLength();");
            txtTopicAnswer.Attributes.Add("onkeyup", "CheckNewTopicLength();");

            txtDate1Year.Attributes.Add("onblur", "return calculateAge();");
            txtDate2Year.Attributes.Add("onblur", "return calculateAge();");
            ddlDate1Day.Attributes.Add("onchange", "return calculateAge();");
            ddlDate1Month.Attributes.Add("onchange", "return calculateAge();");
            ddlDate2Month.Attributes.Add("onchange", "return calculateAge();");
            ddlDate2Day.Attributes.Add("onchange", "return calculateAge();");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// This Method Will Visible the Edit controls and invisible the View Controls 
    /// of the Personal Detail, Story and more about section
    /// </summary>
    public void VisibleViewMode()
    {
        this._presenter.VisibleAllViewMode();

        // Add Current satte of the page in View State
        object[] objValue = { true, true, true, IsAdmin };
        AddValuesInViewState(objValue, Stories.StoryMaintainState.StoryPage_CurrentState.ToString());
    }


    /// <summary>
    /// This method will save the object in the View State
    /// </summary>
    /// <param name="objValue">A object which contain value which want to add in view state</param>
    /// <param name="key">A string varaible which is key for setting the values from view state</param>
    private void AddValuesInViewState(object[] objValue, string key)
    {
        ViewState.Add(key, objValue);
    }


    /// <summary>
    /// This method will get the Values in the View State for the passed key
    /// </summary>
    /// <param name="key">A string varaible which is key for getting the values from view state</param>
    /// <returns>This method will return a object which conatin View State value for the passed key</returns>
    private object[] GetValuesFromViewState(string key)
    {
        object[] objValue = (object[])ViewState[key];

        return objValue;
    }

    /// <summary>
    /// This function will set the Image Url of the Event Image
    /// </summary>
    private void SetTributeImageUrl()
    {
        if (StoryImageURL != string.Empty)
        {
            hdnStoryImageURL.Value = StoryImageURL.ToString();
            imgStoryImage.Src = StoryImageURL.ToString();
            StoryImageURL = string.Empty;
        }
    }

    /// <summary>
    /// This method will save the final image in the default path and set the URL to that path
    /// </summary>
    /// <param name="isEdit">A bool variable which contains whether user wants to Edit or wants to add new one</param>
    private void SaveImage()
    {

        // path of the previous image file ( in case of edit )
        string prevImage = (string)ViewState[Stories.StoryMaintainState.StoryImageURL.ToString()];

        // in case of edit if image path is not changed then exit from the function
        if (prevImage != hdnStoryImageURL.Value)
        {
            string fileName = Path.GetFileName(hdnStoryImageURL.Value);

            //Path where you want to upload the file.
            string[] eventPath = CommonUtilities.GetPath();

            if (eventPath == null)
            {
                return;
            }

            // Destination Path = Drive + TributePhotos Folder + TributeURL_TributeType Folder + Story Folder
            string destPath = eventPath[0] + "/" + eventPath[1] + "/" + _TributeURL.Replace(" ", "_") + "_" + _TributeType.Replace(" ", "_") + "/" + eventPath[7];

            // Source Path = Drive + TributePhotos Folder + Temp Folder + File Name
            string srcPath = eventPath[0] + "/" + eventPath[1] + "/" + eventPath[6] + "/" + fileName;

            DirectoryInfo dirObj = new DirectoryInfo(destPath);

            //if directory does not exists creates a directory
            if (!dirObj.Exists)
            {
                dirObj.Create();
            }

            // FileName - TributeURL_TributeType + .JPEG
            //fileName = _TributeName.Replace(" ", "_") + "_" + _TributeType.Replace(" ", "_") + ".jpeg";

            // Check whether directory exist or not
            DirectoryInfo dirObj1 = new DirectoryInfo(destPath);
            if (dirObj1.Exists)
            {
                string tempfileName = fileName;
                int counter = 1;

                // Check to see if a file already exists with the same name as the file to upload.  
                while (File.Exists(destPath + "/" + tempfileName))
                {
                    // If a file with this name already exists, add a number in the filename.
                    tempfileName = counter.ToString() + fileName;
                    counter = counter + 1;
                }

                fileName = tempfileName;

                // if is in edit mode then delete the previous file
                if (prevImage != "")
                {
                    string newPrevImage = destPath + "/" + Path.GetFileName(prevImage.ToString());
                    if (File.Exists(newPrevImage))
                    {
                        File.Delete(newPrevImage);
                    }

                    ViewState[Stories.StoryMaintainState.StoryImageURL.ToString()] = "";
                }

                // Copy the file from the temp location to final destination folder
                if (File.Exists(srcPath))
                {
                    File.Copy(srcPath, destPath + "/" + fileName);

                    string newFilename = _TributeURL.Replace(" ", "_") + "_" + _TributeType.Replace(" ", "_") + "/" + eventPath[7] + "/" + fileName;

                    hdnStoryImageURL.Value = newFilename;
                    imgStoryImage.Src = newFilename;
                    imgTributeImageView.ImageUrl = newFilename;
                }
            }
        }
        else
        {
            //Path where you want to upload the file.
            string[] path = CommonUtilities.GetPath();

            string newFilename = hdnStoryImageURL.Value.Substring(hdnStoryImageURL.Value.IndexOf(path[2]) + path[2].Length, hdnStoryImageURL.Value.Length - path[2].Length); ;

            hdnStoryImageURL.Value = newFilename;
            imgStoryImage.Src = newFilename;
            imgTributeImageView.ImageUrl = newFilename;
        }

    }
    public string StripHtml(string htmlString)
    {
        Regex regex = new Regex("</?(.*)>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        string finalString = Regex.Replace(htmlString, @"<(.|\n)*?>", string.Empty);  //regex.Replace(htmlString, regex, string.Empty);
        return finalString;
        //return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
    }

    #endregion

}