///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Notes.ManageNote.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to add/edit a note added to a tribute.
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES
using System;
using System.Configuration;
using System.Collections;
using System.Data;

using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.Notes.Views;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
#endregion

public partial class Notes_ManageNote : PageBase, IManageNote
{
    #region CLASS VARIABLES
    private ManageNotePresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    private int _userId;
    private string _userName;
    private int _tributeId;
    private int _noteId; //used when user comes to this page for editing note.
    protected string _tributeName;
    private string _tributeType;
    private string _tributeUrl;
    private string mode;
    #endregion

    #region CONSTANTS
    private const string ADD_TYPE_NAME = "AddNote";
    private const string EDIT_TYPE_NAME = "EditNote";
    private const string MODULE_TYPE_NAME = "Note";
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetSessionValues();
            aTributeHome.HRef = Session["APP_PATH"] + _tributeUrl + "/";
            lbtnPost.Attributes.Add("onclick", "hideControl()");
            if (!this.IsPostBack)
            {
                SetValuesToLabelsAndErrors();

                if (!Equals(mode, string.Empty))
                {
                    if (Equals(mode, "edit"))
                        _presenter.GetNoteDetails(_noteId);
                }
                Page.SetFocus(txtNoteTitle);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnPost_Click(object sender, EventArgs e)
    {
        try
        {
            //Check for valid data in Note Title
            string error = string.Empty;
            TributesPortal.ResourceAccess.IOVS.Sanitise(txtNoteTitle.Text, ref error);
            if (!string.IsNullOrEmpty(error))
            {
                //Display the error message
                lblErrMsg.InnerHtml = ShowErrorMessage(error);
                lblErrMsg.Visible = true;
                spnMessage.Visible = true;
                return;
            }

            //if (StripHtml(ftbNoteMessage.Value).Length > 10000)
            if (StripHtml(ftbNoteMessage.PlainText).Length > 10000)
            {
                lblErrMsg.InnerHtml = ShowErrorMessage(ResourceText.GetString("errNoteMessage_MN"));
                lblErrMsg.Visible = true;
                spnMessage.Visible = true;
            }
            else
            {
                Note objNote = FillNoteEntity();

                if (Equals(mode, "edit"))
                    _presenter.UpdateNote(objNote);
                else
                    _presenter.SaveNote(objNote);

                if (facebook_share.Checked == true)
                {
                    //may also need to add noteId as well.
                    Response.Redirect("~/" + Session["TributeURL"] + 
                        "/notes.aspx?post_on_facebook=" + facebook_share.Checked+
                        "&noteId="+objNote.NotesId, false);
                }
                else
                {
                    Response.Redirect("~/" + Session["TributeURL"] + "/notes.aspx", false);
                }
                lblErrMsg.Visible = false;
                spnMessage.Visible = false;
                //Redirect.RedirectToPage(Redirect.PageList.TributeNotes.ToString())
            }
        }
        catch (Exception ex)
        {
            lblErrMsg.InnerHtml = "Unable to save record. Check for input values.";
            lblErrMsg.Visible = true;
            spnMessage.Visible = true;
            //throw ex;
        }
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Equals(mode, "edit"))
                Response.Redirect("~/" + Session["TributeURL"] + "/note.aspx" + "?noteId=" + _noteId, false);
            else
                Response.Redirect("~/" + Session["TributeURL"] + "/notes.aspx", false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public ManageNotePresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    public Note NoteDetails
    {
        set
        {
            this.txtNoteTitle.Text = value.Title;
            //this.ftbNoteMessage.Value = value.PostMessage;
            this.ftbNoteMessage.Text = value.PostMessage;
        }
    }
    #endregion

    #region METHODS
    /// <summary>
    /// Method to set text to labels, error controls and buttons
    /// </summary>
    public void SetValuesToLabelsAndErrors()
    {
        lblNoteHeader.Text = ResourceText.GetString("lblNoteHeader_MN");
        lblRequired.Text = ResourceText.GetString("lblRequired_MN");
        lblNoteTitle.InnerHtml = "<em class='required'>* </em>" + ResourceText.GetString("lblNoteTitle_MN");
        lblNoteMessage.InnerHtml = ResourceText.GetString("lblNoteMessage_MN"); // "<em class='required'>* </em>" + ResourceText.GetString("lblNoteMessage_MN");
        lbtnCancel.Text = ResourceText.GetString("btnCancel_MN");

        if (Equals(mode, "edit"))
            lbtnPost.Text = ResourceText.GetString("lbtnSaveNote_MN");
        else
            lbtnPost.Text = ResourceText.GetString("btnPost_MN");

        rfvNoteTitle.ErrorMessage = ResourceText.GetString("errNoteTitle_MN");
        cvNoteMessage.ErrorMessage = ResourceText.GetString("errNoteMessage_MN");
        if (Equals(mode, "edit"))
            spPageMode.InnerText = ResourceText.GetString("nvgEditNote_MN");
        else
            spPageMode.InnerText = ResourceText.GetString("nvgAddNote_MN");
    }

    /// <summary>
    /// Method to fill the notes entity with the data entered in the controls
    /// </summary>
    public Note FillNoteEntity()
    {
        Note objNote = new Note();
        objNote.UserId = _userId;
        objNote.UserTributeId = _tributeId;
        objNote.Title = txtNoteTitle.Text.Trim();
        //objNote.MessageWithoutHtml = StripHtml(ftbNoteMessage.Value); //ftbNoteMessage.HtmlStrippedText;
        objNote.MessageWithoutHtml = ftbNoteMessage.PlainText.ToString(); //cute editor;
        //objNote.PostMessage = ftbNoteMessage.Value; // ftbNoteMessage.Text;
        objNote.PostMessage = ftbNoteMessage.Text.ToString(); // cute editor;
        objNote.ModuleTypeName = MODULE_TYPE_NAME;
        objNote.UserName = _userName;
        objNote.TributeName = _tributeName;
        objNote.TributeType = _tributeType;
        objNote.TributeUrl = _tributeUrl;
        objNote.PathToVisit = "<a href='http://" + Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath; //
        if (Equals(mode, "edit")) //if user is editing the note note id is required to update it.
        {
            objNote.NotesId = _noteId;
            objNote.ModifiedBy = _userId;
            objNote.ModifiedDate = DateTime.Now;
        }
        else
        {
            objNote.CreatedBy = _userId;
            objNote.CreatedDate = DateTime.Now;
        }
        return objNote;
    }

    /// <summary>
    /// Method to get user is admin or owner
    /// </summary>
    private bool UserIsAdmin()
    {
        bool isAdmin;
        UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
        objUserInfo.UserId = _userId;
        objUserInfo.TributeId = _tributeId;
        if (Equals(mode, "edit"))
            objUserInfo.TypeName = EDIT_TYPE_NAME;
        else
            objUserInfo.TypeName = ADD_TYPE_NAME;

        if (_userId != 0)
            isAdmin = IsUserAdmin(objUserInfo);
        else
            isAdmin = false;

        objUserInfo.IsAdmin = isAdmin;
        StateManager objStateManager = StateManager.Instance;
        objStateManager.Add("UserAdminInfo_ManageNote", objUserInfo, StateManager.State.Session);
        return isAdmin;
    }

    private void GetSessionValues()
    {
        StateManager objStateManager = StateManager.Instance;
        //to get user id from session as user is logged in user
        objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionValue, null))
        {
            _userId = objSessionValue.UserId;
            _userName = objSessionValue.FirstName == string.Empty ? objSessionValue.UserName : (objSessionValue.FirstName + " " + objSessionValue.LastName);
        }

        //to get tribute id and name from session
        objTribute = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);
        if (!Equals(objTribute, null))
        {
            _tributeId = objTribute.TributeId;
            _tributeName = objTribute.TributeName;
            _tributeType = objTribute.TypeDescription;
            _tributeUrl = objTribute.TributeUrl;
        }

        if (!Equals(Request.QueryString["mode"], null))
        {
            mode = Request.QueryString["mode"].ToString();
            if(objStateManager.Get("NoteSession", StateManager.State.Session)!=null)
                _noteId = int.Parse(objStateManager.Get("NoteSession", StateManager.State.Session).ToString());
        }

        //to check if user is loggedin or is admin
        if (_userId == 0) //if user is not a logged in user redirect to login page
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
        else if (!UserIsAdmin()) //if user is not admin of tribute redirect to login page
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
    }

    /// <summary>
    /// Method to show error message
    /// </summary>
    /// <param name="strErrorMessage">Error message</param>
    /// <returns>Html for error message</returns>
    public string ShowErrorMessage(string strErrorMessage)
    {
        string headertext = " <h2>Oops - there is a problem.</h2><br/>";
        headertext += "<ul>";
        headertext += "<li>";
        headertext += strErrorMessage;
        headertext += "</li>";
        return headertext += "</ul>";
    }

    public string StripHtml(string htmlString)
    {
        //Regex regex = new Regex("</?(.*)>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        string finalString = Regex.Replace(htmlString, @"<(.|\n)*?>", string.Empty);  //regex.Replace(htmlString, regex, string.Empty);
        return finalString;
        //return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
    }
    #endregion
}
