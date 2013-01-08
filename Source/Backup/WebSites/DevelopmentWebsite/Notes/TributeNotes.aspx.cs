///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Notes.TributeNotes.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the list of all notes added to a tribute
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
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;
using TributesPortal.Notes.Views;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
#endregion

public partial class Notes_TributeNotes : PageBase, ITributeNotes
{
    #region CLASS VARIABLES
    protected string clickHere = ResourceText.GetString("lnkClickHere_TN");
    protected string comments = ResourceText.GetString("txtComments_TN");
    protected string postedBy = ResourceText.GetString("txtPostedBy_TN");
    protected string on = ResourceText.GetString("txtOn_TN");
    protected string at = ResourceText.GetString("txtAt_TN");
    protected string _tributeName;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    private TributeNotesPresenter _presenter;
    //private int _totalRecords;
    private int _userId;
    private int _tributeId;
    private string _tributeType;
    private string _tributeUrl;
    protected string _tributeImage;
    private int currentPage;
    private int pageSize;
    private bool isAdmin;
    protected bool _isActive;
    private string profile_prefix = CommonUtilities.GetPath()[2].ToString();
    private string tribute_base_url;
    //AG:
    private string _tributePackageType;
    private DateTime _endDate;
    string appDomian = string.Empty;
    string tributeEndDate = string.Empty;
    int topHeight = 0;
    #endregion

    #region CONSTANTS
    private const string TYPE_NAME = "TributeNotes";
    #endregion

 
    protected void page_Prerender(object sender, EventArgs e)
    {
        
    }

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.Now);
        try
        {

            StateManager objStateManager = StateManager.Instance;
            //to get user id from session as user is logged in user
            objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
            if (!Equals(objSessionValue, null))
                _userId = objSessionValue.UserId;

            topHeight = 165;
            objTribute = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);

            if (Request.QueryString["mode"] != null || Request.QueryString["fbmode"] != null) //if user is coming through link
            {
                if (Request.QueryString["TributeId"] != null)
                    _tributeId = int.Parse(Request.QueryString["TributeId"].ToString());

                if (Request.QueryString["TributeName"] != null)
                    _tributeName = Request.QueryString["TributeName"].ToString();

                if (Request.QueryString["TributeType"] != null)
                    _tributeType = Request.QueryString["TributeType"].ToString();

                if (Request.QueryString["TributeUrl"] != null)
                    _tributeUrl = Request.QueryString["TributeUrl"].ToString();

                //CreateTributeSession(); //to create the tribute session values if user comes o this page from link or from favorites list.
            }
            else if (!Equals(objTribute, null))
            {
                _tributeId = objTribute.TributeId;
                _tributeName = objTribute.TributeName;
                _tributeType = objTribute.TypeDescription;
                _tributeUrl = objTribute.TributeUrl;
                _isActive = objTribute.IsActive;
                _tributeImage = objTribute.TributeImage;
                _tributePackageType = objTribute.TributePackageType;
                if (!objTribute.Date2.Equals(null))
                {
                    _endDate = (DateTime)objTribute.Date2;
                }
            }

            else
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);

            SetAppDomain();
            
            tributeEndDate = _presenter.GetTributeEndDate(_tributeId);            
            //MG:Expiry Notice
            //AG(20-jan-11): Added topheight for expiry page
            bool isCustomeHeaderOn = _presenter.GetCustomHeaderDetail(_tributeId);
            if (Equals(objSessionValue, null))//when not logged in
            {
                if (isCustomeHeaderOn)
                    topHeight = 197;
                else
                    topHeight = 88;
            }
            else
            {
                if (isCustomeHeaderOn)
                    topHeight = 258;
                else
                    topHeight = 131;
            }      
            if (!tributeEndDate.Equals("Never"))
            {
                string[] date = tributeEndDate.Split('/');
                DateTime date2 = new DateTime(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1]));
                if (date2 < DateTime.Now)
                {
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "fnExpiryNoticePopupClose();", true);
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "awe", "fnExpiryNoticePopup('location.href','document.title','NonMemo','" + _tributeId + "','" + appDomian + "','" + topHeight + "');", true);
                }
            }
            //Start - Modification on 9-Dec-09 for the enhancement 3 of the Phase 1
            if (_tributeName != null)
                Page.Title = _tributeName + " | Notes";
            //End

            if (Session["NoteDeleted"] != null && Session["NoteDeleted"].ToString().Length > 0)
            {
                Error.InnerHtml = Session["NoteDeleted"].ToString();
                Error.Visible = true;
                Session["NoteDeleted"] = null;
            }

            if (Session["TributeSession"] == null)
                CreateTributeSession(); //to create the tribute session values if user comest o this page from link or from favorites list.

            //to get page size from config file
            pageSize = (int.Parse(WebConfig.Pagesize_Notes));

            //to get current page number, if user clicks on page number in paging it gets tha page number from query string
            //else page number is 1
            if (Request.QueryString["PageNo"] != null)
                currentPage = int.Parse(Request.QueryString["PageNo"].ToString());
            else
                currentPage = 1;

            if (!this.IsPostBack)
            {
                SetTextToLabelsAndButtons();
                SetControlsVisibility();
                _presenter.GetTributeNotes(SetNoteObject());
            }
            // !!! It manages also url handling stuff...
            NoteCreatioFacebookWallPost();
         
        }
        catch (Exception ex)
        {
            Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/Error404.aspx");
        }
    }

    private void SetAppDomain()
    {
        if (WebConfig.ApplicationMode.Equals("local"))
        {
            appDomian = WebConfig.AppBaseDomain.ToString();
        }
        else
        {
            appDomian = "http://" + _tributeType.ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";
        }
    }

    protected void NoteCreatioFacebookWallPost()
    {
        // common YT url-handling stuff ... ugh!
        string tributeHome;
        string noteUrl;
        if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
        {
            tributeHome = Session["APP_PATH"] + _tributeUrl;
        }
        else
        {
            tributeHome = "http://" + _tributeType.Replace("New Baby", "newbaby").ToLower() + "." +
                WebConfig.TopLevelDomain + "/" + _tributeUrl;
        }
        tributeHome += "/";
        tribute_base_url = tributeHome;
        noteUrl = tributeHome + "Note.aspx";

        string query_string = string.Empty;
        if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
        {
            query_string = "?TributeType=" + HttpUtility.UrlEncode(_tributeType);
            noteUrl = noteUrl + query_string;
            tributeHome = tributeHome + query_string;
        }
        aTributeHome.HRef = tributeHome;

        StateManager objStateManager = StateManager.Instance;
        if (Request.QueryString["post_on_facebook"] != null &&
            Request.QueryString["post_on_facebook"].ToString().Equals("True"))
        {
            int _noteId=0;
            if (Request.QueryString["noteId"] != null) //to pick value of selected note from querystring
            {
                _noteId = int.Parse(Request.QueryString["noteId"].ToString());
                objStateManager.Add("NoteSession", _noteId, StateManager.State.Session);
            }
            else if (objStateManager.Get("NoteSession", StateManager.State.Session) != null)
            {
                _noteId = int.Parse(objStateManager.Get("NoteSession", StateManager.State.Session).ToString());
            }
            else if (objStateManager.Get("NoteSession", StateManager.State.Session) == null)
                return;

            noteUrl += (noteUrl.Contains("?") ? "&" : "?") + "noteId=" + _noteId.ToString();

            Note objNote = new Note();
            objNote.NotesId = _noteId;

            NotesManager mgr = new NotesManager();
            Note objNoteDetails = mgr.GetNoteDetails(objNote);

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">\n");
            sb.Append("$(document).addEvent('fb_connected', function() {\n");
            sb.Append("    var attachments = {\n");
            sb.Append("        name: '");
            sb.Append(string.Format("{0} added a note to the: {1} {2} Tribute", objNoteDetails.UserName, _tributeName, _tributeType));
            sb.Append("',\n");
            sb.Append("        href: '");
            sb.Append(noteUrl);
            sb.Append("',\n");
            sb.Append("        caption: '<b>Website:</b> ");
            sb.Append(tributeHome);
            sb.Append("',\n");
            sb.Append("        description: '<b>Note:</b> ");
            sb.Append(objNoteDetails.Title);
            sb.Append("',\n");
            sb.Append("        media: [{\n");
            sb.Append("          type: 'image',\n");
            sb.Append("          src:'");
            sb.Append(profile_prefix);
            sb.Append(_tributeImage);
            sb.Append("',\n");
            sb.Append("          href: '");
            sb.Append(noteUrl);
            sb.Append("'\n");
            sb.Append("               }]\n");
            sb.Append("    };\n");
            sb.Append("    var action_link = [{\n");
            sb.Append("        text: '");
            sb.Append(string.Format("Visit {0} Tribute", _tributeType));
            sb.Append("',\n");
            sb.Append("        href: '");
            sb.Append(tributeHome);
            sb.Append("'\n");
            sb.Append("    }]\n");
            sb.Append("    publish_stream('', attachments, action_link, null, null, function() {});");
            sb.Append("});\n");
            sb.Append("</script>");

            ClientScript.RegisterStartupScript(GetType(), "facebook_wall_post", sb.ToString());
        }
    }

    protected void lbtnAddNote_Click(object sender, EventArgs e)
    {
        Response.Redirect(tribute_base_url + "managenote.aspx" + this.Master.query_string, false);
    }

    protected void dlComments_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
            btnDelete.Text = ResourceText.GetString("btnDelete_TN");
            btnDelete.Attributes.Add("onclick", "if(confirm('" + ResourceText.GetString("msgDelete_TN") + "')){}else{return false}");

            if (isAdmin)
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }
            Note note = (Note)e.Item.DataItem;
            StringBuilder html = new StringBuilder();
            HtmlGenericControl itemProfilePicture = (HtmlGenericControl)e.Item.FindControl("itemProfilePicSpn");
            HtmlImage itemprofilepic = (HtmlImage)e.Item.FindControl("itemProfilePicImg");

            //if (note.FacebookUid != null &&
            //    (note.UserImage.EndsWith("images/bg_ProfilePhoto.gif") ||
            //     !note.UserImage.StartsWith(profile_prefix)))

            if (note.FacebookUid != null)
            {
                if (Facebook.Web.FacebookWebContext.Current.Session != null)
                {
                    html.Append("<span style='border-bottom:solid 1px white ;border-right:solid 1px white ; width:58px;height:58px; '>");
                    html.Append("<fb:profile-pic uid=\"");
                    html.Append(note.FacebookUid.ToString());
                    html.Append("\" size=\"square\" facebook-logo=\"true\"></fb:profile-pic></span>");
                    itemProfilePicture.InnerHtml = html.ToString();
                    itemprofilepic.Visible = false;
                }
                else
                {
                    itemprofilepic.Src = "http://graph.facebook.com/" + note.FacebookUid.ToString() + "/picture?type=square";
                    itemProfilePicture.Visible = false;
                }
            }
            else
            {
                html.Append("<img src='");
                html.Append(note.UserImage.ToString());
                html.Append("' alt='Photo of ");
                html.Append(note.UserName.ToString());
                html.Append("' width='50' height='50' />");
                itemProfilePicture.InnerHtml = html.ToString();
                itemprofilepic.Visible = false;
            }   
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    protected void dlComments_DeleteCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            //int intNoteId = int.Parse(e.CommandArgument.ToString()); //int.Parse(hdnCommentID.Value.ToString());
            Note objNote = new Note();
            objNote.NotesId = int.Parse(e.CommandArgument.ToString());
            objNote.UserId = _userId;
            _presenter.DeleteNote(objNote); //to delete note
            SetControlsVisibility();
            _presenter.GetTributeNotes(SetNoteObject()); //to bind the datalist with updated data
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public TributeNotesPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    public IList<Note> TributeNotesList
    {
        set
        {
            if (value.Count == 0 && currentPage > 1)
            {
                string queryString = "?TributeId=" + _tributeId + "&TributeName=" + _tributeName + "&TributeType=" + _tributeType + "&PageNo=" + (currentPage - 1);
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.TributeNotes.ToString()) + queryString, false);
            }
            //divNotes.InnerHtml = GetDataToDisplay(value);
            dlNotes.DataSource = value;
            dlNotes.DataBind();
        }
    }
    public int TotalRecords
    {
        set
        {
            //to set the visibility of paging and message if no note exists for the tribute.
            if (value > 0)
            {
                divNoRecord.Visible = false;
                divNotes.Visible = true;
                divForTopPaging.Visible = true;
                divForBottomPaging.Visible = true;
            }
            else
            {
                divNoRecord.Visible = true;
                divNotes.Visible = false;
                divForTopPaging.Visible = false;
                divForBottomPaging.Visible = false;
                divNoRecord.InnerText = ResourceText.GetString("strNoNoteMessage_TN");
            }
        }
    }
    public string DrawPaging
    {
        set
        {
            spanPagingTop.InnerHtml = value;
            spanPagingBottom.InnerHtml = value;
        }
    }
    public string RecordCount
    {
        set
        {
            spanRecordCountTop.InnerText = value;
            spanRecordCountBottom.InnerText = value;
        }
    }
    #endregion

    #region METHODS
    /// <summary>
    /// Method to fill the Note object to get the list of notes
    /// </summary>
    /// <returns>Filled Note entity</returns>
    private Note SetNoteObject()
    {
        Note objNote = new Note();
        objNote.UserTributeId = _tributeId;
        objNote.CurrentPage = currentPage;
        objNote.PageSize = pageSize;

        return objNote;
    }

    /// <summary>
    /// Method to set the visibility of controls.
    /// </summary>
    private void SetControlsVisibility()
    {
        if (UserIsAdmin())
        {
            lbtnAddNote.Visible = true;
            divAddNote.Visible = true;
        }
        else
        {
            lbtnAddNote.Visible = false;
            divAddNote.Visible = false;
        }
    }

    /// <summary>
    /// Method to set text to labels and buttons.
    /// </summary>
    private void SetTextToLabelsAndButtons()
    {
        //lblNotesHeader.Text = ResourceText.GetString("lblNoteHeader_TN");
        lbtnAddNote.Text = ResourceText.GetString("lbtnAddNote_TN");
        //spanPage.InnerText = ResourceText.GetString("txtPage");
        spanPageTop.InnerText = ResourceText.GetString("txtPage_TN");
        spanPageBottom.InnerText = ResourceText.GetString("txtPage_TN");
    }

    /// <summary>
    /// Method to get user is admin or owner
    /// </summary>
    private bool UserIsAdmin()
    {
        UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
        objUserInfo.UserId = _userId;
        objUserInfo.TributeId = _tributeId;
        objUserInfo.TypeName = TYPE_NAME;

        if (_userId != 0)
        {
            if (IsUserAdmin(objUserInfo))
            {
                objUserInfo.IsAdmin = true;
                isAdmin = true;
            }
        }
        else
        {
            objUserInfo.IsAdmin = false;
            isAdmin = false;
        }
        StateManager objStateManager = StateManager.Instance;
        objStateManager.Add("UserAdminInfo_TributeNote", objUserInfo, StateManager.State.Session);
        return isAdmin;
    }

    /// <summary>
    /// Method to create the tribute session values if user comes o this page from link or from favorites list.
    /// </summary>
    private void CreateTributeSession()
    {
        Tributes objTribute = new Tributes();
        objTribute.TributeId = _tributeId;
        objTribute.TributeName = _tributeName;
        objTribute.TypeDescription = _tributeType;
        objTribute.TributeUrl = _tributeUrl;
        objTribute.IsActive = _isActive;
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add("TributeSession", objTribute, TributesPortal.Utilities.StateManager.State.Session);
    }

    private string GetDataToDisplay(IList<Note> objNoteList)
    {
        StringBuilder sbToDisplay = new StringBuilder();
        foreach (Note objNote in objNoteList)
        {
            sbToDisplay.Append("<div class='yt-ListItem'>");
            sbToDisplay.Append("<h4><a href='note.aspx?noteId=" + objNote.NotesId + "'>");
            sbToDisplay.Append(objNote.Title + "</a></h4>");
            sbToDisplay.Append("<h6><a href='" + Session["APP_BASE_DOMAIN"] + "UnderConstruction.aspx?userId=" + objNote.UserId + "' class='yt-ListName'>");
            sbToDisplay.Append(objNote.UserName + "</a> (" + objNote.Location + objNote.Country + ")</h6>");
            sbToDisplay.Append("<div class='yt-ItemPhoto'>");
            sbToDisplay.Append("<img src='" + objNote.UserImage + "' alt='Photo of" + objNote.UserName + "' width='48' height='48' />");
            sbToDisplay.Append("</div>");
            sbToDisplay.Append("<p class='yt-ItemDate'>");
            sbToDisplay.Append(ResourceText.GetString("txtAt_TN"));
            sbToDisplay.Append(objNote.CreationTime);
            sbToDisplay.Append(ResourceText.GetString("txtOn_TN"));
            sbToDisplay.Append(objNote.CreationDate + ".");
            sbToDisplay.Append("</p>");
            sbToDisplay.Append("<p>");
            sbToDisplay.Append(objNote.PostMessage);
            sbToDisplay.Append("</p>");
            sbToDisplay.Append("<div class='yt-ItemTools'> ");
            sbToDisplay.Append("<a href='note.aspx?noteId=" + objNote.NotesId + "' class='yt-More'> ");
            sbToDisplay.Append(ResourceText.GetString("lnkClickHere_TN"));
            sbToDisplay.Append("</a>");
            sbToDisplay.Append("<a href='note.aspx?noteId=" + objNote.NotesId + "' class='yt-Comments'> ");
            sbToDisplay.Append(ResourceText.GetString("txtComments_TN") + "(" + objNote.CommentCount + ")</a>");
            sbToDisplay.Append("</div>");
            sbToDisplay.Append("<asp:LinkButton ID='btnDelete' CommandArgument='" + objNote.NotesId + "' runat='server' CssClass='yt-MiniButton yt-DeleteButton'");
            sbToDisplay.Append(" Text='Delete' CommandName='Delete' CausesValidation='false' />");
            sbToDisplay.Append("</div>");
        }
        return sbToDisplay.ToString();
    }
    #endregion
}