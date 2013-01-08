///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Miscellaneous.ChangeSiteTheme.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : 
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
using TributesPortal.BusinessEntities;
using TributesPortal.Miscellaneous.Views;
using TributesPortal.Utilities;
#endregion

public partial class Miscellaneous_ChangeSiteTheme : System.Web.UI.Page, IChangeSiteTheme
{
    #region CLASS VARIABLES
    private ChangeSiteThemePresenter _presenter;
    private SessionValue objSessionValue = null;
    private int _tributeId = 17; //TO DO: to be picked from session
    private int _userId = 0;
    private string _themeType = "Wedding"; //TO DO: to be picked from session
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        StateManager stateManager = StateManager.Instance;
        objSessionValue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionValue, null))
            _userId = objSessionValue.UserId;

        if (!this.IsPostBack)
        {
            try
            {
                this._presenter.OnViewInitialized();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        this._presenter.OnViewLoaded();
    }

    protected void btnChangeTheme_Click(object sender, EventArgs e)
    {
        try
        {
            _presenter.UpdateTributeTheme();
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.VideoGallery.ToString()), false);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public ChangeSiteThemePresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    public List<Templates> ThemesList
    {
        set
        {
            rblThemes.DataSource = value;
            rblThemes.DataTextField = "TemplateName";
            rblThemes.DataValueField = "TemplateID";
            rblThemes.DataBind();
        }
    }
    public int ExistingTheme
    {
        set
        {
            rblThemes.SelectedValue = value.ToString();
        }
    }
    public int TributeId
    {
        get
        {
            return _tributeId;
        }
    }

    public int ThemeId
    {
        get
        {
            return int.Parse(rblThemes.SelectedValue);
        }
    }
    public int ModifiedBy
    {
        get
        {
            return _userId;
        }
    }
    public DateTime ModifiedDate
    {
        get
        {
            return DateTime.Now;
        }
    }
    public string ThemeType
    {
        get
        {
            return _themeType;
        }
    }
    #endregion
    // TODO: Forward events to the presenter and show state to the user.
    // For examples of this, see the View-Presenter (with Application Controller) QuickStart:
    //		ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.wcsf.2007jun/wcsf/html/02-480-ViewPresenter.htm

    
}


