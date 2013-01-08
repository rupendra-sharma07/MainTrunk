///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.MessagingSystem.EmailUrl.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page gives the urls for emails
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
using TributesPortal.MessagingSystem.Views;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
#endregion

public partial class MessagingSystem_EmailUrl : PageBase, IEmailUrl
{
    #region CLASS VARIABLES
    private EmailUrlPresenter _presenter;
    private string _typeName;
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        StateManager stateManager = StateManager.Instance;
        //EmailLink objEmail = (EmailLink)stateManager.Get("objEmailLink_GuestBook", StateManager.State.Session);
        EmailLink objEmail = (EmailLink)stateManager.Get("objEmailLink", StateManager.State.Session);
        _typeName = objEmail.TypeName;

        if (!this.IsPostBack)
        {
            try
            {
                this._presenter.OnViewInitialized();
                SetTextToControls(); //to assign text to labels and error controls
                _presenter.LoadControlsData(); //to load data in controls
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        this._presenter.OnViewLoaded();
    }

    protected void lbtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            _presenter.SendEmail();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        if (_typeName == "GuestBook" || _typeName == "Tribute")
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.GuestBook.ToString()), false);
        else if (_typeName == "TributeNotes")
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.TributeNotes.ToString()), false);
        else if (_typeName == "NoteFullView")
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.NoteFullView.ToString()), false);
        else
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.VideoGallery.ToString()), false);
    }

    /*protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            _presenter.SendEmail();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        if (_typeName == "GuestBook" || _typeName == "Tribute")
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.GuestBook.ToString()), false);
        else if (_typeName == "TributeNotes")
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.TributeNotes.ToString()), false);
        else if (_typeName == "NoteFullView")
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.NoteFullView.ToString()), false);
        else
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.VideoGallery.ToString()), false);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (_typeName == "GuestBook" || _typeName == "Tribute")
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.GuestBook.ToString()), false);
        else if (_typeName == "TributeNotes")
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.TributeNotes.ToString()), false);
        else if (_typeName == "NoteFullView")
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.NoteFullView.ToString()), false);
        else
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.VideoGallery.ToString()), false);
    }
    */
    #endregion

    #region PROPERTIES
    [CreateNew]
    public EmailUrlPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    public string FromUserName
    {
        set
        {
            txtUserName.Text = value;
        }
        get
        {
            return txtUserName.Text.Trim();
        }
    }

    public string FromEmailAddress
    {
        set
        {
            txtUserEmailAddress.Text = value;
        }
        get
        {
            return txtUserEmailAddress.Text.Trim();
        }
    }
    public string TypeName
    {
        set
        {
            _typeName = value;
        }
    }
    //public string UrlToEmail
    //{
    //    get { return Request.UrlReferrer.ToString(); }
    //}
    public List<string> Receipients
    {
        get
        {
            List<string> lstEmailAddresses = new List<string>();
            string[] emailAddresses = (txtEmailAddress.Text.Replace(",", ";")).Split(';');

            foreach (string strEmailAddress in emailAddresses)
            {
                lstEmailAddresses.Add(strEmailAddress);
            }
            return lstEmailAddresses;
        }
    }
    #endregion
    
    #region METHODS
    private void SetTextToControls()
    {
        hEmailPage.InnerText = ResourceText.GetString("hEmailPage_EU");
        pRequired.InnerHtml = "<strong>" + ResourceText.GetString("lblRequired_EU") + "<em class=\"required\">*</em></strong>";
        lblUserName.InnerHtml = "<em class=\"required\">* </em>" + ResourceText.GetString("lblUserName_EU");
        lblUserEmailAddress.InnerHtml = "<em class=\"required\">* </em>" + ResourceText.GetString("lblUserEmailAddress_EU");
        lblEmailAddress.InnerText = ResourceText.GetString("lblEmailAddress_EU");
        rfvUserName.ErrorMessage = ResourceText.GetString("errUserName_EU");
        rfvFromEmailAddress.ErrorMessage = ResourceText.GetString("errUserEmailAddress_EU");
        rfvEmailAddress.ErrorMessage = ResourceText.GetString("errUserEmailAddress_EU");
        cvCheckValidEmail.ErrorMessage = ResourceText.GetString("errUserEmailAddress_EU");
        lbtnSubmit.Text = ResourceText.GetString("btnSubmit_EU");
    }
    #endregion


    
}


