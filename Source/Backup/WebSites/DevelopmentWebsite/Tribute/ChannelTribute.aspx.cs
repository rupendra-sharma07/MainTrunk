///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.ChannelTribute.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the channel home page
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
///Tribute Portal-AllTribute UI Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the UI class Tribute_AllTribute for dispalying the most recently created and most popular tribute. 
// This will implement the All the Properties in the IAllTribute interface. and will extend PageBase class which provides 
// 1. Error Event Handler
// 2. Exception handling
/// </summary>

public partial class Tribute_ChannelTribute : PageBase, IAllTribute
{

    #region CLASS VARIABLES

    private AllTributePresenter _presenter;
    private int tributeType = 1; //need to pick from session on the basis of the channel
    protected String strTributeType;
    protected String strBcTributeType;
    protected String strChannelHomepage;
    protected string _TributeList = "Created:";
    #endregion


    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request.QueryString["Type"] != null && Request.QueryString["Type"] != "")
            {
                strTributeType = Request.QueryString["Type"].ToString();
                if (strTributeType != null && (strTributeType.ToLower().Equals("newbaby") || strTributeType.ToLower().Equals("new baby")))
                    Session["tributeType"] = "newbaby";
                else
                    Session["tributeType"] = strTributeType;
            }
            else
            {
                if (Session["tributeType"] != null)
                    strTributeType = Session["tributeType"].ToString();

            }

            if (!string.IsNullOrEmpty(strTributeType))
            {
                if (strTributeType.ToLower().Equals("memorial"))
                    tributeType = 7;
                else if (strTributeType.ToLower().Equals("anniversary"))
                    tributeType = 6;
                else if (strTributeType.ToLower().Equals("wedding"))
                    tributeType = 5;
                else if (strTributeType.ToLower().Equals("graduation"))
                    tributeType = 4;
                else if (strTributeType.ToLower().Equals("birthday"))
                    tributeType = 3;
                else if (strTributeType.ToLower().Equals("newbaby") || strTributeType.ToLower().Equals("new baby"))
                    tributeType = 2;
                strChannelHomepage = strTributeType.Clone().ToString();
                strChannelHomepage.Replace("newbaby", "New Baby");

                if (strTributeType != null && (strTributeType.ToLower().Equals("newbaby") || strTributeType.ToLower().Equals("new baby")))
                {
                    strBcTributeType = "newbaby";
                }
                else
                    strBcTributeType = strTributeType;

                this._presenter.OnViewInitialized();
                _presenter.GetRecentlyCreatedTribute(tributeType);
            }
        }
        Page.Title = strTributeType + " Tributes";
        this._presenter.OnViewLoaded();

        SetControlsVisibility();
    }

    protected void lbtnRecentlyCreated_Click(object sender, EventArgs e)
    {
        try
        {
            strTributeType = Request.QueryString["Type"].ToString();

            if (strTributeType !=null && (strTributeType.ToLower().Equals("newbaby") || strTributeType.ToLower().Equals("new baby")))
            {
                strBcTributeType = "newbaby";
            }
            else
                strBcTributeType = strTributeType;

             if(strTributeType.ToLower().Equals("memorial"))
                tributeType = 7;
            else if(strTributeType.ToLower().Equals("anniversary"))
                 tributeType = 6;
            else if(strTributeType.ToLower().Equals("wedding"))
                 tributeType = 5;
             else if (strTributeType.ToLower().Equals("graduation"))
                 tributeType = 4;
            else if(strTributeType.ToLower().Equals("birthday"))
                 tributeType = 3;
            else if(strTributeType.ToLower().Equals("newbaby") || strTributeType.ToLower().Equals("new baby"))
                 tributeType = 2;
            strChannelHomepage = strTributeType.Clone().ToString();
            strChannelHomepage.Replace("newbaby", "New Baby");
            Page.Title = strTributeType + " Tributes";
            _TributeList = "Created:";
            _presenter.GetRecentlyCreatedTribute(tributeType);

            SetControlsVisibility();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnPopular_Click(object sender, EventArgs e)
    {
        try
        {
            strTributeType = Request.QueryString["Type"].ToString();

            if (strTributeType != null && (strTributeType.ToLower().Equals("newbaby") || strTributeType.ToLower().Equals("new baby")))
            {
                strBcTributeType = "newbaby";
            }
            else
                strBcTributeType = strTributeType;

             if(strTributeType.ToLower().Equals("memorial"))
                tributeType = 7;
            else if(strTributeType.ToLower().Equals("anniversary"))
                 tributeType = 6;
            else if(strTributeType.ToLower().Equals("wedding"))
                 tributeType = 5;
             else if (strTributeType.ToLower().Equals("graduation"))
                 tributeType = 4;
            else if(strTributeType.ToLower().Equals("birthday"))
                 tributeType = 3;
            else if(strTributeType.ToLower().Equals("newbaby") || strTributeType.ToLower().Equals("new baby"))
                 tributeType = 2;
            strChannelHomepage = strTributeType.Clone().ToString();
            strChannelHomepage.Replace("newbaby", "New Baby");
            Page.Title = strTributeType + " Tributes";
            _TributeList = "Views:";
            _presenter.GetPopularTribute(tributeType);

            SetControlsVisibility();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void repSearchTribute_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            LinkButton lnkTributeName = (LinkButton)e.Item.FindControl("lbtnTributeName");
            HiddenField hdnTributeType = (HiddenField)e.Item.FindControl("hdnTributeType");
            HiddenField hdnTributeUrl = (HiddenField)e.Item.FindControl("hdnTributeUrl");
            string strSubDomain = "";
            string strTributeURL = "";


            Tributes objTribute = new Tributes();

            objTribute.TributeName = lnkTributeName.Text;
            objTribute.TypeDescription = hdnTributeType.Value;
            objTribute.TributeId = int.Parse(e.CommandArgument.ToString());

            strSubDomain = hdnTributeType.Value.ToString();
            strTributeURL = hdnTributeUrl.Value.ToString();
            StateManager stateManager = StateManager.Instance;
            stateManager.Add(PortalEnums.SessionValueEnum.TributeSession.ToString(), objTribute, StateManager.State.Session);

            if (WebConfig.ApplicationMode.Equals("local"))
            {
                Response.Redirect(Session["APP_PATH"].ToString() + strTributeURL + "/"); //TributeHomePage.aspx");
            }
            else
            {
                //Use this line for server and comment the line written above
                Response.Redirect("http://" + strSubDomain.Replace("New Baby", "NewBaby") + "." + WebConfig.TopLevelDomain + "/" + strTributeURL, false);
            }

            // Redirect to the Tribute Home Page
            //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Login2HomePage.ToString()), false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion


    #region PROPERTIES

    [CreateNew]
    public AllTributePresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    #region IAllTribute Members

    public IList<SearchTribute> SearchTributesList
    {
        set
        {
            if (value.Count > 0)
            {
                repSearchTribute.Visible = true;
                repSearchTribute.DataSource = value;
                repSearchTribute.DataBind();
            }
            else
            {
                ShowMessage(ResourceText.GetString("ErrMsgSearchTribute_ST"));
                repSearchTribute.Visible = false;
            }
        }
    }

    #endregion

    #endregion


    #region METHODS

    /// <summary>
    /// Set Visibility of control on the basis of user right and total number of gifts
    /// </summary>
    private void SetControlsVisibility()
    {
        try
        {
            //to set visibility of comments list. if no record the found displays message else displays list.
            if (repSearchTribute.Items.Count == 0)
            {
                divTributeMain.Visible = false;
                divMessage.Visible = true;
                divMessage.InnerText = ResourceText.GetString("strNoMessage_AT");
            }
            else
            {
                divTributeMain.Visible = true;
                divMessage.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

}