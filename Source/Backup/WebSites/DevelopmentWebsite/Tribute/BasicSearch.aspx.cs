///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.BasicSearch.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to perorm a basic search for tributes
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

public partial class Tribute_BasicSearch : PageBase, IBasicSearch
{
    #region CLASS VARIABLES
    private BasicSearchPresenter _presenter;
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

                SetControlsValue(); 
            }

            this._presenter.OnViewLoaded();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            object[] objBasicSearchParam ={ int.Parse(rdoTributeTypeGroup.SelectedValue.ToString()), txtSearch.Text.ToString() };

            StateManager objStateMgr = StateManager.Instance;
            objStateMgr.Add("Search", objBasicSearchParam, StateManager.State.Session);

            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.BasicSearchTribute.ToString()));
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    #endregion


    #region PROPERTIES

    [CreateNew]
    public BasicSearchPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    public IList<Tributes> TributeTypeList
    {
        set
        {
            rdoTributeTypeGroup.DataSource = value;
            rdoTributeTypeGroup.DataTextField = "TributeName";
            rdoTributeTypeGroup.DataValueField = "TributeID";
            rdoTributeTypeGroup.DataBind();
            rdoTributeTypeGroup.SelectedIndex = 0;
        }
    }

    #endregion


    #region METHODS

    private void SetControlsValue()
    {
        //Text for labels from the resource file
        lblSearch.Text  = ResourceText.GetString("lblSearch_BS");
        //txtSearch.Text = ResourceText.GetString("txtSearch_BS");
        lnkAdvanceSearch.Text = ResourceText.GetString("lnkAdvanceSearch_BS");
        
        //Text for error messages from the resource file
        valSearch.ErrorMessage = ResourceText.GetString("valSearch_BS");
    }

    #endregion
   
}


