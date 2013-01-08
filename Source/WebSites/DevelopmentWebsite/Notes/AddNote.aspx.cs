///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Notes.AddNote.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : The page allows the user to add a note
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
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
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.Notes.Views;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
#endregion

public partial class Notes_AddNote : PageBase, IAddNote
{
    #region CLASS VARIABLES
    private AddNotePresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    private int _userId;
    private int _tributeId;
    private int _noteId; //used when user comes to this page for editing note.
    private string _tributeName;
    private string mode;
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        GetSessionValues();

        if (_userId == 0) //if user is not a logged in user redirect to login page
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
        else if (!UserIsAdmin()) //if user is not admin of tribute redirect to login page
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));

        if (!this.IsPostBack)
        {
            SetValuesToLabelsAndErrors();

            if (!Equals(mode, string.Empty))
            {
                if (Equals(mode, "edit"))
                    _presenter.GetNoteDetails(_noteId);
            }

            lblNoteTitle.Focus();

        }
    }

    protected void lbtnPost_Click(object sender, EventArgs e)
    {
        if (Equals(mode, "edit"))
            _presenter.UpdateNote(FillNoteEntity());
        else
            _presenter.SaveNote(FillNoteEntity());

        Response.Redirect("~/" + Session["TributeURL"] + "/Notes.aspx", false);
    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public AddNotePresenter Presenter
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
        lblNoteHeader.Text = ResourceText.GetString("lblNoteHeader_AN");
        lblRequired.Text = ResourceText.GetString("lblRequired_AN");
        lblNoteTitle.Text = ResourceText.GetString("lblNoteTitle_AN");
        lblNoteMessage.Text = ResourceText.GetString("lblNoteMessage_AN");
        lbtnCancel.Text = ResourceText.GetString("btnCancel_AN");

        if (Equals(mode, "edit"))
            lbtnPost.Text = ResourceText.GetString("lbtnSaveNote_AN");
        else
            lbtnPost.Text = ResourceText.GetString("btnPost_AN");

        rfvNoteTitle.ErrorMessage = ResourceText.GetString("errNoteTitle");
        cvNoteMessage.ErrorMessage = ResourceText.GetString("errNoteMessage_AN");
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
        objNote.PostMessage = ftbNoteMessage.Text;
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
        objUserInfo.TypeName = "TributeNotes";

        if (_userId != 0)
            isAdmin = IsUserAdmin(objUserInfo);
        else
            isAdmin = false;

        return isAdmin;
    }

    private void GetSessionValues()
    {
        StateManager objStateManager = StateManager.Instance;
        //to get user id from session as user is logged in user
        objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionValue, null))
            _userId = objSessionValue.UserId;

        //to get tribute id and name from session
        objTribute = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);
        if (!Equals(objTribute, null))
        {
            _tributeId = objTribute.TributeId;
            _tributeName = objTribute.TributeName;
        }

        if (!Equals(Request.QueryString["mode"], null))
        {
            mode = Request.QueryString["mode"].ToString();
            _noteId = int.Parse(objStateManager.Get("NoteSession", StateManager.State.Session).ToString());
        }
    }
    #endregion



}