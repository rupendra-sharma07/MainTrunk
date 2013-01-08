///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.AdvanceSearch .aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays allows the user to perform an advanced search on tributes
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
using TributesPortal.Tribute.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;

#endregion

/// <summary>
///Tribute Portal-AdvanceSearch UI Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the UI class Tribute_AdvanceSearch for Advance Search. This will implement the 
// All the Properties in the IAdvanceSearch interface. and will extend PageBase class which provides 
// 1. Error Event Handler
// 2. Exception handling
/// </summary>
/// 
public partial class Tribute_AdvanceSearch : PageBase, IAdvanceSearch
{

    #region CLASS VARIABLES

    private AdvanceSearchPresenter _presenter;
    
    #endregion


    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        //
        //{
        //    lblTributeType.InnerText = "Website Type:";
        //    //lblTributeName.InnerHtml = @"<em class=\"required\">* </em>
        //    //                Website 
        //    //                Name:";
             
        //}

        revTributeNameSpecialchar.ErrorMessage = "Please enter valid characters in " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " Name.";
        try
        {
            if (!this.IsPostBack)
            {
                this._presenter.OnViewInitialized();
                this._presenter.GetTributeTypeList(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower());

                //To Set State
               // this._presenter.GetStateList(ddlCountry.SelectedValue.ToString());
                //To Set State

                SetControlsValue();
            }

            this._presenter.OnViewLoaded();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    protected void lbtnSearch_Click(object sender, EventArgs e)
     {
        try
         {
            string error = string.Empty;
            bool pass1 = true;
            bool pass2 = true;
            DateTime objDate1;
            DateTime objDate2;
            error1.Visible = false;
            error2.Visible = false;

            if ((!string.IsNullOrEmpty(txtBeforeYear.Text)) || (ddlBeforeMonth.SelectedValue != "0") || (ddlBeforeDay.SelectedValue != "00"))
            {
                pass2 = DateTime.TryParse(ddlBeforeMonth.SelectedValue + "-" + ddlBeforeDay.SelectedValue + "-" + txtBeforeYear.Text.ToString(), out objDate2);
                if (pass2)
                {
                    if (objDate2 <= DateTime.Parse("01-01-1753"))
                    {
                        pass2 = false;
                        error2.Visible = true;
                    }
                }
                else
                    error2.Visible = true;
            }
            if ((!string.IsNullOrEmpty(txtAfterYear.Text)) || (ddlAfterMonth.SelectedValue != "0") || (ddlAfterDay.SelectedValue != "00"))
            {
                pass1 = DateTime.TryParse(ddlAfterMonth.SelectedValue + "-" + ddlAfterDay.SelectedValue + "-" + txtAfterYear.Text.ToString(), out objDate1);
                if (pass1)
                {
                    if (objDate1 <= DateTime.Parse("01-01-1753"))
                    {
                        pass1 = false;
                        error1.Visible = true;
                    }
                }
                else
                    error1.Visible = true;
            }
            if (pass1 && pass2)
            {
                # region process
                TributesPortal.ResourceAccess.IOVS.Sanitise(txtTributeName.Text, ref error);
                if (string.IsNullOrEmpty(error))
                    TributesPortal.ResourceAccess.IOVS.Sanitise(txtCity.Text, ref error);
                if (!string.IsNullOrEmpty(error))
                {
                    ShowMessage(error);
                    return;
                }


                // Create an object to pass to search tribute page
                SearchTribute objSearchTribute = new SearchTribute();

                objSearchTribute.TributeType = ddlTributeType.SelectedItem.Text;
                objSearchTribute.SearchString = txtTributeName.Text.ToString();
                objSearchTribute.SearchType = PortalEnums.SearchEnum.Advance.ToString();
                objSearchTribute.SortOrder = "DESC";

                objSearchTribute.City = txtCity.Text.ToString();

                if (ddlState.SelectedIndex > 0)
                {
                    objSearchTribute.State = int.Parse(ddlState.SelectedValue.ToString());
                    objSearchTribute.StateName = ddlState.SelectedItem.Text;
                }
                else
                {
                    objSearchTribute.State = -1;
                }

                if (ddlCountry.SelectedIndex >= 0)
                {
                    objSearchTribute.Country = int.Parse(ddlCountry.SelectedValue.ToString());
                    objSearchTribute.CountryName = ddlCountry.SelectedItem.Text;
                }
                else
                {
                    objSearchTribute.Country = -1;
                }

                // Format the created after Date and time
                if ((ddlAfterMonth.SelectedIndex != 0) && (ddlAfterDay.SelectedIndex != 0) && (txtAfterYear.Text.ToString() != ""))
                {
                    objSearchTribute.CreatedAfterDate = FormatDate(ddlAfterDay.SelectedIndex.ToString(), ddlAfterMonth.SelectedIndex.ToString(), txtAfterYear.Text.ToString());
                }

                // Format the created before Date and time
                if ((ddlBeforeMonth.SelectedIndex != 0) && (ddlBeforeDay.SelectedIndex != 0) && (txtBeforeYear.Text.ToString() != ""))
                {
                    objSearchTribute.CreatedBeforeDate = FormatDate(ddlBeforeDay.SelectedIndex.ToString(), ddlBeforeMonth.SelectedIndex.ToString(), txtBeforeYear.Text.ToString());
                }

                // Create StateManager object and add search paramter in the session
                StateManager objStateMgr = StateManager.Instance;
                objStateMgr.Add("Search", objSearchTribute, StateManager.State.Session);

                // Redirect to the Search Result page
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.SearchResult.ToString()));
                //Response.Redirect("searchresult.aspx");
                # endregion
            }
            else
            {
                string errorText=string.Empty;
                if (!pass1)
                    errorText = "Please enter valid Created After date.";
                if (!pass2)
                {
                    if (string.IsNullOrEmpty(errorText))
                        errorText = "Please enter valid Created Before date.";
                    else
                        errorText = errorText + "</br> Please enter valid Created Before date.";
                }
                ShowMessage("<h2>Oops - there was a problem with your Search.</h2><br><h3>Please correct the error(s) below:</h3> ", errorText, true);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this._presenter.GetStateList(ddlCountry.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    #endregion


    #region PROPERTIES

    [CreateNew]
    public AdvanceSearchPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    #region IAdvanceSearch Members

    public IList<Locations> Country
    {
        set
        {
            ddlCountry.DataSource = value;
            ddlCountry.DataTextField = "LocationName";
            ddlCountry.DataValueField = "LocationId";
            ddlCountry.DataBind();
            //Insert "Empty" in Country List
            ddlCountry.Items.Insert(0, new ListItem("", "-1"));
            ddlCountry.SelectedIndex = 0;
        }
    }

    public IList<Locations> State
    {
        set
        {
            ddlState.Items.Clear();
            
            if (value.Count > 0)
            {
                ddlState.DataSource = value;
                ddlState.DataTextField = "LocationName";
                ddlState.DataValueField = "LocationId";
                ddlState.DataBind();
                //Insert "Empty" in State List
                ddlState.Items.Insert(0, new ListItem("","-1"));

                ddlState.SelectedIndex = 0;
                ddlState.Enabled=true;
            }
            else
                ddlState.Enabled = false;
        }
    }

    public IList<Tributes> TributeTypeList
    {
        set
        {
            ddlTributeType.DataSource = value;
            ddlTributeType.DataTextField = "TributeName";
            ddlTributeType.DataValueField = "TributeID";
            ddlTributeType.DataBind();
            ddlTributeType.SelectedIndex = 0;
        }
    }

    #endregion

    #endregion


    #region METHODS

    /// <summary>
    /// This Function will set the value of the control and error messages from the resource File
    /// </summary>
    private void SetControlsValue()
    {
        //Text for labels from the resource file

        lblAdvanceSearch.Text = ResourceText.GetString("lblAdvanceSearch_AS");  //Advanced Search
        lblRequired.InnerHtml = ResourceText.GetString("lblRequired_AS"); // <strong>Required fields are indicated with <em class="required">* </em></strong>        
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            lblTributeName.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblTributeName_AS1");
            lblTributeType.InnerText = ResourceText.GetString("lblTributeType_AS1");
            valTributeName.ErrorMessage = ResourceText.GetString("valTributeName_AS1");
        }
        else
        {
            lblTributeName.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblTributeName_AS");
            lblTributeType.InnerText = ResourceText.GetString("lblTributeType_AS");
            valTributeName.ErrorMessage = ResourceText.GetString("valTributeName_AS");
        }
        lblCity.InnerText = ResourceText.GetString("lblCity_AS");
        lblState.InnerText = ResourceText.GetString("lblState_AS");
        lblCountry.InnerText = ResourceText.GetString("lblCountry_AS");
        lblCreatedAfter.InnerText = ResourceText.GetString("lblCreatedAfter_AS");
        lblCreatedBefore.InnerText = ResourceText.GetString("lblCreatedBefore_AS");
        //btnCancel.Text = ResourceText.GetString("btnCancel_AS");
        lbtnSearch.Text = ResourceText.GetString("btnSearch_AS");

        lblAfterMonth.InnerText = ResourceText.GetString("lblAfterMonth_AS");
        lblAfterDay.InnerText = ResourceText.GetString("lblAfterDay_AS");
        lblAfterYear.InnerText = ResourceText.GetString("lblAfterYear_AS");
        lblBeforeMonth.InnerText = ResourceText.GetString("lblBeforeMonth_AS");
        lblBeforeDay.InnerText = ResourceText.GetString("lblBeforeDay_AS");
        lblBeforeYear.InnerText = ResourceText.GetString("lblBeforeYear_AS");

        if (ddlAfterMonth.Items.Count <= 0)
        {
            int i = 0;
            ListItem item = new ListItem("", i.ToString());

            ddlAfterMonth.Items.Add(item);
            ddlBeforeMonth.Items.Add(item);
            for (i = 1; i <= 12; i++)
            {
                string month = "Month" + i + "_ST";
                item = new ListItem(ResourceText.GetString(month), i.ToString());
                ddlAfterMonth.Items.Add(item);
                ddlBeforeMonth.Items.Add(item);
            }
        }

        //Text for error messages from the resource file
        
        valCity.ErrorMessage = ResourceText.GetString("valCity_AS");
        //valAfterDate.ErrorMessage = ResourceText.GetString("valAfterDate_AS");
        //valBeforeDate.ErrorMessage = ResourceText.GetString("valBeforeDate_AS");
        //valDateCompare.ErrorMessage = ResourceText.GetString("valDateCompare_AS");
        
    }

    /// <summary>
    /// This method will format a Date object by passed date, month and year value
    /// </summary>
    /// <param name="day">A string object which contain the day</param>
    /// <param name="month">A string object which contain the month</param>
    /// <param name="year">A string object which contain the Year</param>
    /// <returns>This method will return the DateTime object</returns>
    private DateTime FormatDate(string day, string month, string year)
    {
        DateTime Date1;

        // Format the created after Date and time
        string afterDate = month + "/" + day + "/" + year;

        try
        {
            Date1 = DateTime.Parse(afterDate.ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return Date1;
    }

    #endregion
    
}


