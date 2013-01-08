///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.SearchTribute.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page helps to search for tributes
///Audit Trail     : Date of Modification  Modified By         Description


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

public partial class Tribute_SearchTribute : PageBase, ISearchTribute
{
    #region CLASS VARIABLES

    private SearchTributePresenter _presenter;
    object[] objSerachParam = null;
    private static int totalRecord = 0;
    private int PageSize = 2; //TO DO: to be picked dynamically
    private int PageIndexOnPage = 3; //TO DO: to be picked dynamically
    private int FirstPage = 0;
    private int LastPage = 0;
    private int PageCount = 0;
    private int currentPage = 1;

    #endregion


    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                //this._presenter.OnViewInitialized();
                this._presenter.GetTributeTypeList(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower());
                // Get Serach Paramter From session
                GetSearchParameterFromSession(currentPage);

                // Set the controls value
                SetControlsValue();

                // Display how many records are displaying on this page
                DisplayRecordCount(currentPage, objSerachParam);

                // Get the paging index
                GetPageIndex();
            }
            else
            {
                // Get the values of the paging from View State
                GetPagingValueFromViewState();   
            }

            this._presenter.OnViewLoaded();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    protected void ddlTributeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblTotalRecords.Text = "";
            lblDisplayedRecords.Text = "";

            StateManager stateManager = StateManager.Instance;
            object[] objSerachParam = (object[])stateManager.Get("Search", StateManager.State.Session);

            objSerachParam[0] = ddlTributeType.SelectedValue;

            ResetPagingValue();

            Search(currentPage, objSerachParam);

            // Display how many records are displaying on this page
            DisplayRecordCount(currentPage, objSerachParam);

            // Get the paging index
            GetPageIndex();

            if (ddlSort.SelectedItem.Text == "Created: Oldest")
            {
                IList<SearchTribute> objTribute = _presenter.Sorting(GetTributeListFromSession(), ddlSort.SelectedItem.Text);

                SearchTributesList = objTribute;
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
    {
        //sorting 

        if (totalRecord > 0)
        {
            IList<SearchTribute> objTribute = _presenter.Sorting(GetTributeListFromSession(), ddlSort.SelectedItem.Text);

            SearchTributesList = objTribute;
        }
    }

    protected void lbtnNext_Click(object sender, EventArgs e)
    {
        try
        {
            SetNextPageIndex(totalRecord, PageSize);

            GetSearchParameterFromSession(FirstPage);

            DisplayRecordCount(FirstPage, objSerachParam);

            SetPagingValueInViewState();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    protected void lbtnPre_Click(object sender, EventArgs e)
    {
        try
        {
            LastPage = FirstPage - 1;
            FirstPage = FirstPage - PageIndexOnPage;
            SetPreviousPageIndex(totalRecord, PageSize);

            GetSearchParameterFromSession(currentPage);
            DisplayRecordCount(FirstPage, objSerachParam);

            SetPagingValueInViewState();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    protected void dlstIndex_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            LinkButton lnbtn = (LinkButton)e.Item.FindControl("lbtnIndex");
            int CurrentPage = int.Parse(lnbtn.Text);

            GetSearchParameterFromSession(CurrentPage);

            DisplayRecordCount(CurrentPage, objSerachParam);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }
    
    #endregion


    #region METHODS

    private void Search(int currentPage, object[] objSerachParam)
    {
        string searchPage = Request.QueryString["Search"];

        _presenter.Search(objSerachParam, searchPage, currentPage, PageSize);
    }

    private void SetControlsValue()
    {
        lblOption.Text = ResourceText.GetString("lblOption_ST");
        lnkAdvanceSearch.Text = ResourceText.GetString("lnkAdvanceSearch_ST");

        ddlSort.Items.Add("Created: Newest"); // ResourceText.GetString("ddlSort_");
        ddlSort.Items.Add("Created: Oldest"); // ResourceText.GetString("ddlSort_");

        lblTotalRecords.Text = "";
        lblDisplayedRecords.Text = "";
    }

    private void DisplayRecordCount(int currentPage, object[] objSerachParam)
    {
        if (totalRecord != 0)
        {
            string searchString = _presenter.DisplaySearchString(objSerachParam);
            lblTotalRecords.Text = totalRecord.ToString() + " search results for " + searchString;

            lblDisplayedRecords.Text = _presenter.DisplayRecordValue(currentPage, PageSize, totalRecord);
        }
    }

    
    private void GetPageIndex()
    {
        try
        {
            SetNextPageIndex(totalRecord, PageSize);

            SetPagingValueInViewState();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //To generate Paging Indexes
    public void SetNextPageIndex(int totalRecord, int intPageSize)
    {
        PageCount = _presenter.CountNumberOfpages(totalRecord, intPageSize);

        if ((PageCount - LastPage) > PageIndexOnPage)
        {
            FirstPage = LastPage + 1;
            LastPage = LastPage + PageIndexOnPage;
            lbtnNext.Enabled = true;
        }
        else
        {
            FirstPage = LastPage + 1;
            LastPage = PageCount;
            lbtnNext.Enabled = false;
            if (LastPage > PageIndexOnPage)
            {
                lbtnPre.Enabled = true;
            }
        }
        
        //For Previouse Button
        if (FirstPage == 1)
        {
            lbtnPre.Enabled = false;
        }
        else
        {
            lbtnPre.Enabled = true;
        }

        CreatePageIndex();
    }


    public void SetPreviousPageIndex(int totalRecord, int intPageSize)
    {
        PageCount = _presenter.CountNumberOfpages(totalRecord, intPageSize);

        lbtnPre.Enabled = false;
        lbtnNext.Enabled = false;

        //For Previouse Button
        if (FirstPage > 1)
        {
            lbtnPre.Enabled = true;
        }

        //For Next button
        if (LastPage >= PageIndexOnPage && PageCount > PageIndexOnPage)
        {
            lbtnNext.Enabled = true;
        }
        
        CreatePageIndex();
    }

    private void CreatePageIndex()
    {
        DataTable dtButtonList = new DataTable();
        DataColumn dcButton = new DataColumn();
        DataRow drButton;

        dcButton.DataType = System.Type.GetType("System.Int32");
        dcButton.ColumnName = "Count";
        dtButtonList.Columns.Add(dcButton);

        for (int i = FirstPage; i <= LastPage; i++)
        {
            drButton = dtButtonList.NewRow();
            drButton[dcButton] = i;
            dtButtonList.Rows.Add(drButton);
        }

        dlstIndex.DataSource = dtButtonList;
        dlstIndex.DataBind();
    }

    private void ResetPagingValue()
    {
       FirstPage = 0;
       LastPage = 0;
       PageCount = 0;
       currentPage = 1;
    }

    private void SetPagingValueInViewState()
    {
        StateManager objStatetmgr = StateManager.Instance;
        object[] objPaging = { FirstPage, LastPage, PageCount };

        objStatetmgr.Add("Paging", objPaging, StateManager.State.ViewState);
    }

    private void GetPagingValueFromViewState()
    {
        StateManager objStatetmgr = StateManager.Instance;
        object[] objPaging = (object[])objStatetmgr.Get("Paging", StateManager.State.ViewState);

        if (objPaging != null)
        {
            FirstPage = int.Parse(objPaging[0].ToString());
            LastPage = int.Parse(objPaging[1].ToString());
            PageCount = int.Parse(objPaging[2].ToString());
        }
    }

    private void SetTributeListInSession(IList<SearchTribute> objTribute)
    {
        StateManager objStatetmgr = StateManager.Instance;

        objStatetmgr.Add("TributeList", objTribute, StateManager.State.Session);
    }

    private IList<SearchTribute> GetTributeListFromSession()
    {
        StateManager objStatetmgr = StateManager.Instance;

        IList<SearchTribute> objTribute = (IList<SearchTribute>)objStatetmgr.Get("TributeList", StateManager.State.Session);

        return objTribute;
    }

    private void GetSearchParameterFromSession(int PageNumber)
    {
        StateManager stateManager = StateManager.Instance;
        objSerachParam = (object[])stateManager.Get("Search", StateManager.State.Session);

        Search(PageNumber, objSerachParam);
    }

    #endregion


    #region PROPERTIES
    
    [CreateNew]
    public SearchTributePresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    public IList<SearchTribute> SearchTributesList
    {
        set
        {
            if (value.Count > 0)
            {
                repSearchTribute.Visible = true;
                repSearchTribute.DataSource = value;
                SetTributeListInSession(value);
                repSearchTribute.DataBind();
            }
            else
            {
                ShowMessage(ResourceText.GetString("ErrMsgSearchTribute_ST"));
                repSearchTribute.Visible = false;
            }
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

    public int TotalRecords
    {
        set
        {
            totalRecord = value;
        }
    }

    #endregion
}


