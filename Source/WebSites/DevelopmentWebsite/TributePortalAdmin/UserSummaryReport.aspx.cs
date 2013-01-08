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

public partial class TributePortalAdmin_UserSummaryReport : System.Web.UI.Page, IUserSummaryReport
{
    #region CLASS VARIABLES
    private UserSummaryReportPresenter _presenter;
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        //Added by Rahul
        Button cmdSearch = btnSearch as Button;
        cmdSearch.Attributes.Add("onClick", "javascript:return CheckFields();");

        cvCreatedAfter.ErrorMessage = "Invalid created after date.";
        cvCreatedBefore.ErrorMessage = "Invalid created before date.";
        cvChkDate.ErrorMessage = "Before date is to be greater than After date.";
        lblNoRecord.Text = "No record exists for the entered criteria.";

        lblErrorMessage.Visible = false;
        if (!this.IsPostBack)
        {
            this._presenter.OnViewInitialized();
            FillDays(drpAfterDay);
            FillDays(drpBeforeDay);
            this._presenter.OnCountryLoad(Locationid(null));
            this._presenter.OnStateLoad(Locationid(ddlCountry.SelectedValue.ToString()));
        }
        this._presenter.OnViewLoaded();



        //this._presenter.OnStateLoad(Locationid(ddlCountry.SelectedValue.ToString()));
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.OnStateLoad(Locationid(ddlCountry.SelectedValue.ToString()));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this._presenter.SearchUsers(GetUserObject());
    }

    protected void dgUsersList_Command(Object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            //int index = Convert.ToInt32(e.CommandArgument);
            //Label lblUserId = (Label)dgUsersList.Rows[index].FindControl("lblUserId");
            // string jScript="";
            try
            {
                this._presenter.DeleteUsers(int.Parse(e.CommandArgument.ToString()));
                dgUsersList.DataSource = null;
                dgUsersList.DataBind();

                string RedirectURL = string.Empty;
                //if(WebConfig.ApplicationMode.ToLower().Equals("local"))
                //{
                RedirectURL = "../UserSummaryReport.aspx";
                //}
                //else
                //RedirectURL = "https://" + WebConfig.TopLevelDomain.ToString() + "/TributePortalAdmin/UserSummaryReport.aspx";
                Response.Redirect(RedirectURL, true);
                //Response.Redirect("../TributePortalAdmin/UserSummaryReport.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "This user has active tributes, it cannot be deleted";
                lblErrorMessage.Visible = true;
            }

        }
    }

    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        List<Users> objUserList = (List<Users>)ViewState["UserReport"];
        //List<Users> objUserList = new List<Users>();
        StringBuilder sb = new StringBuilder();
        sb.Append("<table border=1 cellpadding=1 cellpadding=1>");
        sb.Append("<tr>");
        sb.AppendFormat("<th>{0}</th>", "User Id");
        sb.AppendFormat("<th>{0}</th>", "User Name");
        sb.AppendFormat("<th>{0}</th>", "First Name");
        sb.AppendFormat("<th>{0}</th>", "Last Name");
        sb.AppendFormat("<th>{0}</th>", "City");
        sb.AppendFormat("<th>{0}</th>", "State");
        sb.AppendFormat("<th>{0}</th>", "Country");
        sb.AppendFormat("<th>{0}</th>", "Account Type");
        sb.AppendFormat("<th>{0}</th>", "Date Created");
        //for (int jCount = 0; jCount < dgUsersList.Columns.Count; jCount++)
        //{
        //    sb.AppendFormat("<th>{0}</th>", dgUsersList.Columns[jCount].HeaderText);
        //}
        sb.Append("</tr>");
        if (objUserList!=null)
        {
            foreach (Users obj in objUserList)
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", obj.UserId);
                sb.AppendFormat("<td>{0}</td>", obj.UserName);
                sb.AppendFormat("<td>{0}</td>", obj.FirstName);
                sb.AppendFormat("<td>{0}</td>", obj.LastName);
                sb.AppendFormat("<td>{0}</td>", obj.City);
                sb.AppendFormat("<td>{0}</td>", obj.StateName);
                sb.AppendFormat("<td>{0}</td>", obj.CountryName);
                sb.AppendFormat("<td>{0}</td>", obj.AccountType);
                sb.AppendFormat("<td>{0}</td>", obj.CreationDate);
                sb.Append("</tr>");
            }
        }

        //for (int iCount = 0; iCount < dgUsersList.Rows.Count; iCount++)
        //{
        //    sb.Append("<tr>");
        //    for (int jCount = 0; jCount < dgUsersList.Columns.Count; jCount++)
        //    {
        //        sb.AppendFormat("<td>{0}</td>", dgUsersList.Rows[iCount].Cells[jCount].Text);
        //    }
        //    sb.Append("</tr>");
        //}

        Response.Clear();
        Response.ContentType = "application/vnd-ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetRandomFileName() + ".xls");
        Response.Write(sb.ToString());
        Response.End();

        /*Response.Clear();
        Response.AddHeader("content-disposition", "fileattachment;filename=" + System.IO.Path.GetRandomFileName() + ".xls");
        Response.ContentType = "text/xml";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        dgUsersList.RenderControl(hw);
        Response.Write(sw.ToString());
        Response.End();*/
    }
    #endregion

    #region Properties
    [CreateNew]
    public UserSummaryReportPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    private Locations Locationid(string ID)
    {
        Locations objLocations = new Locations();
        if (ID != null)
        {
            objLocations.LocationParentId = int.Parse(ID);
        }
        else
        {
            objLocations.LocationParentId = 0;
        }
        return objLocations;

    }
    public IList<Locations> Locations
    {
        set
        {
            //to add empty value in the list
            Locations objLocation = new Locations(-1, "--", 0);
            value.Insert(0, objLocation);

            ddlCountry.DataSource = value;
            ddlCountry.DataTextField = "LocationName";
            ddlCountry.DataValueField = "LocationId";
            ddlCountry.DataBind();
            ddlCountry.SelectedIndex = 0;
        }
    }
    public IList<Locations> States
    {
        set
        {
            ddlStateProvince.Items.Clear();

            //to add empty value in the list
            Locations objLocation = new Locations(-1, "--", 0);
            value.Insert(0, objLocation);

            if (value.Count > 0)
            {
                ddlStateProvince.DataSource = value;
                ddlStateProvince.DataTextField = "LocationName";
                ddlStateProvince.DataValueField = "LocationId";
                ddlStateProvince.DataBind();
                ddlStateProvince.SelectedIndex = 0;
            }
        }
    }

    public List<Users> UsersList
    {
        set
        {
            if (value.Count > 0)
            {
                lblNoRecord.Visible = false;
                dgUsersList.Visible = true;

                dgUsersList.DataSource = value;
                dgUsersList.DataBind();

                ViewState.Add("UserReport", value);
            }
            else
            {
                lblNoRecord.Visible = true;
                dgUsersList.Visible = false;

                dgUsersList.DataSource = null;
                dgUsersList.DataBind();
            }
        }
    }

    
    #endregion

    #region METHODS
    private void FillDays(DropDownList ddldays)
    {
        for (int i = 0; i <= 31; i++)
        {
            if (i == 0)
            {
                ddldays.Items.Insert(i, "");
            }
            else
            {
                ddldays.Items.Insert(i, i.ToString());
            }
        }
    }

    /// <summary>
    /// Method to get the User object to be searched.
    /// </summary>
    /// <returns>Filled Users entity.</returns>
    private Users GetUserObject()
    {
        Users objUser = new Users();
        objUser.SearchUserId = txtUserId.Text.Trim();
        objUser.City = txtCity.Text.Replace("?", "_").Replace("*", "%").Trim();
        objUser.Country = int.Parse(ddlCountry.SelectedValue);
        objUser.Email = txtEmailAddress.Text.Trim();
        objUser.FirstName = txtFirstName.Text.Replace("?", "_").Replace("*", "%").Trim();
        objUser.LastName = txtLastName.Text.Replace("?", "_").Replace("*", "%").Trim();
        objUser.State = int.Parse(ddlStateProvince.SelectedValue);
        objUser.UserName = txtUsername.Text.Replace("?", "_").Replace("*", "%").Trim();
        objUser.ApplicationType = ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower();
        //if both options are checked/unchecked set usertype to 0 else set to the selected value.
        if (chkPersonal.Checked && chkBusiness.Checked)
            objUser.UserType = 0;
        else if (chkPersonal.Checked)
            objUser.UserType = 1;
        else if (chkBusiness.Checked)
            objUser.UserType = 2;
        else
            objUser.UserType = 0;


        if (drpBeforeDay.SelectedValue != string.Empty && drpBeforeMonth.SelectedValue != string.Empty && txtBeforeYear.Text != string.Empty)
            objUser.CreatedBefore = DateTime.Parse(drpBeforeMonth.SelectedValue + "/" + drpBeforeDay.SelectedValue + "/" + txtBeforeYear.Text);

        if (drpAfterDay.SelectedValue != string.Empty && drpAfterMonth.SelectedValue != string.Empty && txtAfterYear.Text != string.Empty)
            objUser.CreatedAfter = DateTime.Parse(drpAfterMonth.SelectedValue + "/" + drpAfterDay.SelectedValue + "/" + txtAfterYear.Text);

        return objUser;
    }

    #endregion


    protected void dgUsersList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton LinkButtonDelete = e.Row.FindControl("LinkButtonDelete") as LinkButton;
            LinkButtonDelete.Attributes.Add("onClick", "javascript:return dispconfirm();");
        }


    }
    protected void dgUsersList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}



