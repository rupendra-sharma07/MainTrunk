///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.TributePortalAdmin.TributeSummaryReport.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the admin to view the tribute summary report
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
using TributesPortal.Utilities;
#endregion

public partial class TributePortalAdmin_TributeSummaryReport : System.Web.UI.Page, ITributeSummaryReport
{
    #region CLASS VARIABLES
    private TributeSummaryReportPresenter _presenter;
    private int _userId;
    public string WebsiteWord = WebConfig.ApplicationWordForInternalUse.ToString();
    #endregion

    #region EVENT
    protected void Page_Load(object sender, EventArgs e)
    {
        Button cmdSearch = btnSearch as Button;
        cmdSearch.Attributes.Add("onClick", "javascript:return CheckFields();");
        if (!this.IsPostBack)
        {
            //this._presenter.OnViewInitialized();
            SetValuesToControlsAndErrorMsgs();
            FillDays(drpAfterDay);
            FillDays(drpBeforeDay);
            FillDays(drpPurchasedAfterDay);
            FillDays(drpPurchasedBeforeDay);
            this._presenter.OnCountryLoad(Locationid(null));
            this._presenter.OnStateLoad(Locationid(ddlCountry.SelectedValue.ToString()));
        }
        //this._presenter.OnViewLoaded();
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.OnStateLoad(Locationid(ddlCountry.SelectedValue.ToString()));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblError.Visible = false;
        string smonthAfter = drpAfterMonth.Text.ToString();
        string sdayAfter = drpAfterDay.Text.ToString();
        string syearAfter = txtAfterYear.Text.ToString();
        string smonthBefore = drpBeforeMonth.Text.ToString();
        string sdayBefore = drpBeforeDay.Text.ToString();
        string syearBefore = txtBeforeYear.Text.ToString();
        string stributeId = txtTributeId.Text.ToString();
        string stributeName = txtTributeName.Text.ToString();
        string suserName = txtUserName.Text.ToString();
        string suserId = txtUserId.Text.ToString();
        string scity = txtCity.Text.ToString();
        string scountry = ddlCountry.Text.ToString();
        string sstateProvince = ddlStateProvince.Text.ToString();
        string schklstTypes = string.Empty;
        for (int i = 0; i < chklstTypes.Items.Count; i++)
        {
            if (chklstTypes.Items[i].Selected)
            {
                schklstTypes = schklstTypes + chklstTypes.Items[i].Value + ",";
            }
        }
        string schklstStatus = string.Empty;
        for (int i = 0; i < chklstStatus.Items.Count; i++)
        {
            if (chklstStatus.Items[i].Selected)
            {
                if (i == 3)
                    chklstStatus.Items[i].Value = "0";
                schklstStatus = schklstStatus + chklstStatus.Items[i].Value + ",";
            }
        }
        if (string.IsNullOrEmpty(smonthAfter) && string.IsNullOrEmpty(sdayAfter)
            && string.IsNullOrEmpty(syearAfter) && string.IsNullOrEmpty(smonthBefore)
            && string.IsNullOrEmpty(sdayBefore) && string.IsNullOrEmpty(syearBefore)
            && string.IsNullOrEmpty(stributeId) && string.IsNullOrEmpty(stributeName)
            && string.IsNullOrEmpty(suserName) && string.IsNullOrEmpty(suserId)
            && string.IsNullOrEmpty(scity) && scountry.Equals("-1")
            && sstateProvince.Equals("-1") && string.IsNullOrEmpty(schklstTypes)
            && string.IsNullOrEmpty(schklstStatus))
        {
            lblError.Visible = true;

        }
        else
            this._presenter.SearchTribute(GetTributeObject());
    }

    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        List<TributeSearch> objTributeList = (List<TributeSearch>)ViewState["TributeReport"];
        StringBuilder sb = new StringBuilder();
        sb.Append("<table border=1 cellpadding=1 cellpadding=1>");
        sb.Append("<tr>");
        sb.AppendFormat("<th>{0}</th>", "Tribute Id");
        sb.AppendFormat("<th>{0}</th>", "Tribute Name");
        sb.AppendFormat("<th>{0}</th>", "City");
        sb.AppendFormat("<th>{0}</th>", "State");
        sb.AppendFormat("<th>{0}</th>", "Country");
        sb.AppendFormat("<th>{0}</th>", "Tribute Type");
        sb.AppendFormat("<th>{0}</th>", "Tribute Status");
        sb.AppendFormat("<th>{0}</th>", "Date Created");
        sb.AppendFormat("<th>{0}</th>", "Date Expires");
        sb.Append("</tr>");

        if (objTributeList != null)
        {
            foreach (TributeSearch obj in objTributeList)
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", obj.TributeId);
                sb.AppendFormat("<td>{0}</td>", obj.TributeName);
                sb.AppendFormat("<td>{0}</td>", obj.City);
                sb.AppendFormat("<td>{0}</td>", obj.StateName);
                sb.AppendFormat("<td>{0}</td>", obj.CountryName);
                sb.AppendFormat("<td>{0}</td>", obj.TypeDescription);
                sb.AppendFormat("<td>{0}</td>", obj.TributeStatus);
                sb.AppendFormat("<td>{0}</td>", obj.CreationDate);
                sb.AppendFormat("<td>{0}</td>", obj.EndDate);
                sb.Append("</tr>");
            }
        }

        Response.Clear();
        Response.ContentType = "application/vnd-ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetRandomFileName() + ".xls");
        Response.Write(sb.ToString());
        Response.End();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void gvTributeList_Command(Object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblTributeId = (Label)gvTributeList.Rows[index].FindControl("lblTributeId");
            List<TributeSearch> lstobjTributeSearch = (List<TributeSearch>)ViewState["TributeReport"]; //New list ---ANKI
            string _tributeStatus = "";
            //To get the userId of the tribute to delete -- ANKI
            foreach (TributeSearch obj in lstobjTributeSearch)
            {
                if (lstobjTributeSearch.IndexOf(obj) == index)
                {
                    _userId = obj.UserTributeId;
                    _tributeStatus = obj.TributeStatus;
                    break;
                }
            }

            this._presenter.DeleteTribute(int.Parse(lblTributeId.Text), _userId);
            string RedirectURL = string.Empty;
            //if (WebConfig.ApplicationMode.ToLower().Equals("local"))
            //{
            RedirectURL = "../TributeSummaryReport.aspx";
            //}
            //else
            //    RedirectURL = "https://" + WebConfig.TopLevelDomain.ToString() + "/TributePortalAdmin/TributeSummaryReport.aspx";
            Response.Redirect(RedirectURL, true);
        }
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblTributeId = (Label)gvTributeList.Rows[index].FindControl("lblTributeId");
            List<TributeSearch> lstobjTributeSearch = (List<TributeSearch>)ViewState["TributeReport"]; //New list ---ANKI
            string _tributeStatus = "";
            string _tributeType = "";
            string _tributeUrl = "";
            //To get the userId of the tribute to delete -- ANKI
            foreach (TributeSearch obj in lstobjTributeSearch)
            {
                if (lstobjTributeSearch.IndexOf(obj) == index)
                {
                    _userId = obj.UserTributeId;
                    _tributeStatus = obj.TributeStatus;
                    _tributeType = obj.TypeDescription;
                    _tributeUrl = obj.TributeUrl;
                }
            }

            //Response.Redirect(Session["APP_PATH"].ToString() + _tributeUrl + "/"); //TributeHomePage.aspx");
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                Response.Redirect(Session["APP_PATH"].ToString() + _tributeUrl + "/?TributeId=" + lblTributeId.Text.ToString()); //TributeHomePage.aspx");
            }
            else
            {
                //Use this line for server and comment the line written above
                Response.Redirect("http://" + _tributeType.Replace("New Baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + _tributeUrl + "/?TributeId=" + lblTributeId.Text.ToString());
            }
        }
    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public TributeSummaryReportPresenter Presenter
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

    public List<TributeSearch> TributeList
    {
        set
        {
            if (value.Count > 0)
            {
                lblNoRecord.Visible = false;
                gvTributeList.Visible = true;

                gvTributeList.DataSource = value;
                gvTributeList.DataBind();

                ViewState.Add("TributeReport", value);
            }
            else
            {
                lblNoRecord.Visible = true;
                gvTributeList.Visible = false;

                gvTributeList.DataSource = null;
                gvTributeList.DataBind();
            }
           
        }
    }
    #endregion

    #region METHODS
    private void SetValuesToControlsAndErrorMsgs()
    {
        cvCreatedAfter.ErrorMessage = "Invalid created after date.";
        cvCreatedBefore.ErrorMessage = "Invalid created before date.";
        cvChkDate.ErrorMessage = "Before created date is to be greater than After created  date.";
        lblNoRecord.Text = "No record exists for the entered criteria.";
        cvCheckPurchaseDate.ErrorMessage = "Purchades Before Date is to be greater than Purchased After Date.";
        cvPurchasedAfter.ErrorMessage = "Invalid purchased after date.";
        cvPurchasedBefore.ErrorMessage = "Invalid purchased before date.";
        

        chklstTypes.Items.Add(new ListItem("New Baby", "2"));
        chklstTypes.Items.Add(new ListItem("Birthday", "3"));
        chklstTypes.Items.Add(new ListItem("Graduation", "4"));
        chklstTypes.Items.Add(new ListItem("Wedding", "5"));
        chklstTypes.Items.Add(new ListItem("Anniversary", "6"));
        chklstTypes.Items.Add(new ListItem("Memorial", "7"));

        chklstStatus.Items.Add(new ListItem("Free", "3"));
        chklstStatus.Items.Add(new ListItem("1 Year", "2"));
        chklstStatus.Items.Add(new ListItem("LifeTime", "1"));
        chklstStatus.Items.Add(new ListItem("Expired", "4"));

    }

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

    private TributeSearch GetTributeObject()
    {
        TributeSearch objTribute = new TributeSearch();

        //Tributes objTrib = new Tributes();
        objTribute.TributeName = txtTributeName.Text.Replace("?", "_").Replace("*", "%").Trim();
        objTribute.City = txtCity.Text.Replace("?", "_").Replace("*", "%").Trim();
        objTribute.State = int.Parse(ddlStateProvince.SelectedValue);
        objTribute.Country = int.Parse(ddlCountry.SelectedValue);
        //to get selected tribute types
        string selTypes = string.Empty;
        for (int i = 0; i < chklstTypes.Items.Count; i++)
        {
            if (chklstTypes.Items[i].Selected)
            {
                selTypes = selTypes + chklstTypes.Items[i].Value + ",";
            }
        }

        if (!Equals(selTypes, string.Empty))
            objTribute.TypeDescription = selTypes.Remove(selTypes.LastIndexOf(","));

        //to set values to tribute in tributesearch entity
        //objTribute.Tributes = objTrib;

        objTribute.SearchTributeId = txtTributeId.Text.Trim();
        objTribute.UserName = txtUserName.Text.Trim();
        objTribute.SearchUserId = txtUserId.Text.Trim();
        objTribute.ApplicationType = ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower();

        //to get selected tribute status
        string selStatus = string.Empty;
        for (int i = 0; i < chklstStatus.Items.Count; i++)
        {
            if (chklstStatus.Items[i].Selected)
            {
                if (i == 3)
                    chklstStatus.Items[i].Value = "0";
                selStatus = selStatus + chklstStatus.Items[i].Value+ ",";
            }
        }
            if (chklstStatus.Items[0].Selected)
            {
                selStatus = selStatus + "8,";
            }
            if (chklstStatus.Items[1].Selected)
            {
                selStatus = selStatus + "5,7,";
            }
            if (chklstStatus.Items[2].Selected)
            {
                selStatus = selStatus + "4,6,";
            }
        

        if (!Equals(selStatus, string.Empty))
            objTribute.TributeStatus = selStatus.Remove(selStatus.LastIndexOf(","));

        if (drpBeforeDay.SelectedValue != string.Empty && drpBeforeMonth.SelectedValue != string.Empty && txtBeforeYear.Text != string.Empty)
            objTribute.CreatedBefore = DateTime.Parse(drpBeforeMonth.SelectedValue + "/" + drpBeforeDay.SelectedValue + "/" + txtBeforeYear.Text);

        if (drpAfterDay.SelectedValue != string.Empty && drpAfterMonth.SelectedValue != string.Empty && txtAfterYear.Text != string.Empty)
            objTribute.CreatedAfter = DateTime.Parse(drpAfterMonth.SelectedValue + "/" + drpAfterDay.SelectedValue + "/" + txtAfterYear.Text);


        if (drpPurchasedBeforeDay.SelectedValue != string.Empty && drpPurchasedBeforeMonth.SelectedValue != string.Empty && txtPurchasedBeforeYear.Text != string.Empty)
            objTribute.PurchasedBefore = DateTime.Parse(drpPurchasedBeforeMonth.SelectedValue + "/" + drpPurchasedBeforeDay.SelectedValue + "/" + txtPurchasedBeforeYear.Text);

        if (drpPurchasedAfterDay.SelectedValue != string.Empty && drpPurchasedAfterMonth.SelectedValue != string.Empty && txtPurchasedAfterYear.Text != string.Empty)
            objTribute.PurchasedAfter = DateTime.Parse(drpPurchasedAfterMonth.SelectedValue + "/" + drpPurchasedAfterDay.SelectedValue + "/" + txtPurchasedAfterYear.Text);


        return objTribute;
    }
    #endregion

    protected void gvTributeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton LinkButtonDelete = e.Row.FindControl("LinkButtonDelete") as LinkButton;
            LinkButtonDelete.Attributes.Add("onClick", "javascript:return dispconfirm();");
        }
    }
}


