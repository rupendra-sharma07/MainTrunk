///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Users.UserAccounts.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to view his/her account
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Users.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;

public partial class Users_UserAccounts : PageBase, IUserAccounts
{
    private UserAccountsPresenter _presenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
         //   StateManager stateManager = StateManager.Instance;
          //  SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            //if (objSessionvalue != null)
           // {
                this._presenter.GetAllTributeList();    
           // }
            //else
            //{
            //    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            //}
            
        }
        this._presenter.OnViewLoaded();
    }

    [CreateNew]
    public UserAccountsPresenter Presenter
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
                repSearchTribute.DataBind();
            }
          
        }
    }
    protected void repSearchTribute_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //Changes done by Gaurav
        Tributes objTribute = new Tributes();
        objTribute.TributeId = int.Parse(e.CommandArgument.ToString());
        HiddenField hdnTributeName = (HiddenField)e.Item.FindControl("hdnTributeName");
        HiddenField hdnTributeType = (HiddenField)e.Item.FindControl("hdnTributeType");
        objTribute.TributeName = hdnTributeName.Value;
        objTribute.TypeDescription = hdnTributeType.Value;
        objTribute.TributeId = int.Parse(e.CommandArgument.ToString());
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add("TributeSession", objTribute, TributesPortal.Utilities.StateManager.State.Session);
        //end changes

        //string tr = "1";
        //TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        //stateManager.Add("TributeId", tr, TributesPortal.Utilities.StateManager.State.Session);
       // Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.GuestBook.ToString()) + "?TributeId=" + e.CommandArgument.ToString() + "&TributeName=" + hdnTributeName.Value + "&TributeType=" + hdnTributeType.Value);
        Response.Redirect("~/Tribute/TributeHomePage_.aspx");

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("myhome.aspx");
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        Response.Redirect("userprofile.aspx");
    }
}


