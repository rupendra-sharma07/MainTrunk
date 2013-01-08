    ///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Notes.NoteFullView.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page note added to a tribute in its full view and also allows user 
///                  to add comments to the note.
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
using TributesPortal.Notes.Views;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using System.Text.RegularExpressions;
#endregion

public partial class Notes_NoteFullView : PageBase, INoteFullView
{
    #region CLASS VARIABLES
    protected string _noteTitle;
    private NoteFullViewPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    protected int _noteId;
    private int _userId;
    private string _userName;
    private int _tributeId;
    protected string _tributeName;
    private string _tributeType;
    private string _tributeUrl;
    protected string _tributeImage;
    private int pageSize = 1;
    private int currentPage;
    private int typeCodeId = 6; //Code 6 is for Notes.
    private string typeCodeName = PortalEnums.ModuleName.Notes.ToString();
    private int totalComments;
    private bool isAdmin;
    protected bool _isActive;
    private string profile_prefix = CommonUtilities.GetPath()[2].ToString();
    private string noteUrl;
    private string _tributePackageType;
    private DateTime _endDate;
    public int _tableType = 1;
    #endregion

    #region CONSTANT
    private const string TYPE_NAME_TO_SAVE = "Notes";
    private const string TYPE_NAME = "NoteFullView";
    private const string MODULE_FUNCTIONALITY_NAME = "Comment";
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        int topHeight = 165;
        Response.Cache.SetExpires(DateTime.Now);
        try
        {
            StateManager objStateManager = StateManager.Instance;

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
            
            GetValuesFromSession(); //to get values of logged in user and selected tribute from session.

            //Start - Modification on 9-Dec-09 for the enhancement 3 of the Phase 1
            if (_tributeName != null) Page.Title = _tributeName + " | Note";
            //End            
            UserIsAdmin();
            //to get page size from config file
            pageSize = (int.Parse(WebConfig.Pagesize_Notes_Comments));


            //DJ: For Expiry Notice

            string appDomian = string.Empty;
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                appDomian = WebConfig.AppBaseDomain.ToString();
            }
            else
            {
                appDomian = "http://" + _tributeType.ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/";
            }

            
            string tributeEndDate = _presenter.GetTributeEndDate(_tributeId);

            //MG:Expiry Notice
            //AG(20-jan-11): Added topheight for expiry page
            bool isCustomeHeaderOn = _presenter.GetCustomHeaderDetail(_tributeId);
            if (Equals(objSessionValue, null))//when not logged in
            {
                if (isCustomeHeaderOn)
                    topHeight = 197;
                else
                    topHeight = 74;
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
            //to get current page number, if user clicks on page number in paging it gets tha page number from query string
            //else page number is 1
            if (Request.QueryString["PageNo"] != null)
                currentPage = int.Parse(Request.QueryString["PageNo"].ToString());
            else
                currentPage = 1;

            if (!this.IsPostBack)
            {                
                SetControlsVisibiltiy();
                _presenter.LoadNoteData(SetNoteObject(), SetCommentObject(currentPage));                
                Page.SetFocus(txtMessage);
                //_presenter.LoadComments(SetCommentObject(currentPage));
            }
            SetTextToLabelsAndButtons();
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
            this._presenter.SaveComment(GetCommentDataToSave());
            //string queryString = "?PageNo=1"; //"?TributeId=" + _tributeId + "&TributeName=" + _tributeName + "&TributeType=" + _tributeType + "&PageNo=1";
            //Response.Redirect("note.aspx" + queryString, false);
            Response.Redirect(noteUrl, false);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    protected void lbtnEditNote_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/" + Session["TributeURL"] + "/managenote.aspx?mode=edit", false);
    }

    protected void dlComments_DeleteCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            Comments objComment = new Comments();
            objComment.CommentId = int.Parse(e.CommandArgument.ToString());
            objComment.UserId = _userId;
            objComment.TableType = _tableType.ToString();
            this._presenter.DeleteComment(objComment);
            SetControlsVisibiltiy();
            this._presenter.LoadNoteData(SetNoteObject(), SetCommentObject(currentPage));
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    protected void dlComments_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
            btnDelete.Text = ResourceText.GetString("btnDelete_NFV");
            btnDelete.Attributes.Add("onclick", "if(confirm('" + ResourceText.GetString("msgDelete_NFV") + "')){}else{return false}");

            HiddenField hdnUserID = (HiddenField)e.Item.FindControl("hdnUserId");
            int UserId = int.Parse(hdnUserID.Value.ToString());

            if (isAdmin)
            {
                btnDelete.Visible = true;
            }
            else if (_userId == UserId)
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }

            CommentTributeAdministrator comment = (CommentTributeAdministrator)e.Item.DataItem;
            StringBuilder html = new StringBuilder();
            HtmlGenericControl itemProfilePicture = (HtmlGenericControl)e.Item.FindControl("itemProfilePicSpn");
            HtmlImage itemprofilepic = (HtmlImage)e.Item.FindControl("itemProfilePicImg");
            //if (comment.FacebookUid != null &&
            //    (comment.UserImage.EndsWith("images/bg_ProfilePhoto.gif") ||
            //     !comment.UserImage.StartsWith(profile_prefix)))
            if (comment.FacebookUid != null)
            {
                if (Facebook.Web.FacebookWebContext.Current.Session != null)
                {
                    html.Append("<span style='border-bottom:solid 1px white ;border-right:solid 1px white ; width:58px;height:58px; '>");
                    html.Append("<fb:profile-pic uid=\"");
                    html.Append(comment.FacebookUid.ToString());
                    html.Append("\" size=\"square\" facebook-logo=\"true\"></fb:profile-pic></span>");
                    itemProfilePicture.InnerHtml = html.ToString();
                    itemprofilepic.Visible = false;
                }
                else
                {
                    itemprofilepic.Src = "http://graph.facebook.com/" + comment.FacebookUid.ToString() + "/picture?type=square";
                    itemProfilePicture.Visible = false;
                }
            }
            else
            {
               html.Append("<img style='border-bottom:solid 1px white ;border-right:solid 1px white ; width:48px; ' src='");
               html.Append(comment.UserImage.ToString());
                html.Append("' alt='Photo of ");
                html.Append(comment.UserName.ToString());
                html.Append("'  height='48' />");
                itemProfilePicture.InnerHtml = html.ToString();
                itemprofilepic.Visible = false;
            }            
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public NoteFullViewPresenter Presenter
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
            if (value.CreatedBy != null && value.CreatedBy != 0)
            {
                _noteTitle = value.Title;
                //divTitle.InnerText = value.Title;
                hTitle.InnerText = value.Title;
                //divPostedBy.InnerHtml = "Posted  by " + "<a href='../Miscellaneous/UnderConstruction.aspx?userId=" + value.UserId + "' class='yt-ListName'>" + value.UserName + "</a> (" + value.Location + value.Country + ")";
                hPostedBy.InnerHtml = "Posted  by " + "<a href='javascript:void(0);' onclick=\"UserProfileModal_1('" + value.CreatedBy + "');\" class='yt-ListName'>" + value.UserName + "</a> " + value.Location; // (" + value.Location + value.Country + ")";
                pDate.InnerText = " at " + value.CreationTime + " on " + value.CreationDate;
                pMessage.InnerHtml = value.PostMessage;
                if (!string.IsNullOrEmpty(StripHtml(value.PostMessage)))
                    Master.fbDescription = StripHtml(value.PostMessage);
                StringBuilder html = new StringBuilder();
                //if (value.FacebookUid != null &&
                //    (value.UserImage.EndsWith("images/bg_ProfilePhoto.gif") ||
                //     !value.UserImage.StartsWith(profile_prefix)))
                if (value.FacebookUid != null)
                {
                    if (Facebook.Web.FacebookWebContext.Current.Session != null)
                    {
                        html.Append("<span style='border-bottom:solid 1px white ;border-right:solid 1px white ; width:58px;height:58px; '>");
                        html.Append("<fb:profile-pic uid=\"");
                        html.Append(value.FacebookUid.ToString());
                        html.Append("\" size=\"square\" facebook-logo=\"true\"></fb:profile-pic></span>");
                        divImage.InnerHtml = html.ToString();                       
                    }
                    else
                    {
                        html.Append("<img src='http://graph.facebook.com/" + value.FacebookUid.ToString() + "/picture?type=square' alt='Photo of " + value.UserName + "' width='48' height='48' />");
                        divImage.InnerHtml= html.ToString();
                        
                    }
                }
                else
                {
                    html.Append("<img src='");
                    html.Append(value.UserImage.ToString());
                    html.Append("' alt='Photo of ");
                    html.Append(value.UserName.ToString());
                    html.Append("' width='50' height='50' />");
                    divImage.InnerHtml = html.ToString();
                }
                 // "<img src='" + value.UserImage + "' alt='Photo of " + value.UserName + "' width='48' height='48' />";
            }
            else
            {
                StateManager objStateManager = StateManager.Instance;
                objStateManager.Remove("NoteSession", StateManager.State.Session);
                Session["NoteDeleted"]="The Note you are trying to view has been deleted.";
                Response.Redirect("~/" + Session["TributeURL"] + "/notes.aspx", false);
            }
        }
    }
    public IList<CommentTributeAdministrator> Comments
    {
        set
        {
            if (value.Count == 0 && currentPage > 1)
            {
                string queryString = "?PageNo=" + (currentPage - 1) + "&noteId=" + _noteId;
                //string queryString = "?TributeId=" + _tributeId + "&TributeName=" + _tributeName + "&TributeType=" + _tributeType + "&PageNo=" + (currentPage - 1) + "&noteId=" + _noteId;
                Response.Redirect("note.aspx" + queryString, false);
            }
            dlComments.DataSource = value;
            dlComments.DataBind();
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
    public int CommentCount
    {
        set
        {
            totalComments = value;
            //to set the visibility of paging and message if no note exists for the tribute.
            if (value > 0)
            {
                //divMessage.Visible = false;
                divPagingTop.Visible = true;
                divCommentsList.Visible = true;
                divPagingBottom.Visible = true;
                //divComments.Visible = true;
            }
            else
            {
                //divMessage.Visible = true;
                divPagingTop.Visible = false;
                divCommentsList.Visible = false;
                divPagingBottom.Visible = false;
                // divMessage.InnerText = ResourceText.GetString("strNoCommentMessage_NFV");
            }
        }
        get
        {
            return totalComments;
        }
    }
    public string RecordCount
    {
        set
        {
            spanRecordCountBottom.InnerText = value;
            spanRecordCountTop.InnerText = value;
        }
    }
    #endregion

    #region METHODS
    public string StripHtml(string htmlString)
    {
        Regex regex = new Regex("</?(.*)>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        string finalString = Regex.Replace(htmlString, @"<(.|\n)*?>", string.Empty);  //regex.Replace(htmlString, regex, string.Empty);
        return finalString;
        //return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
    }

    /// <summary>
    /// Method to fill the Note object to get details of notes
    /// </summary>
    /// <returns>Filled Note entity</returns>
    private Note SetNoteObject()
    {
        Note objNote = new Note();
        objNote.NotesId = _noteId;
        return objNote;
    }

    /// <summary>
    /// Method to set text to labels and buttons.
    /// </summary>
    private void SetTextToLabelsAndButtons()
    {
        lblNotesHeader.Text = ResourceText.GetString("lblNoteHeader_NFV");
        lbtnEditNote.Text = ResourceText.GetString("lbtnEditNote_NFV");
        lbtnPost.Text = ResourceText.GetString("lbtnPost_NFV");
        lblMessage.InnerText = ResourceText.GetString("lblToLeave_NFV");
        spanPageBottom.InnerText = ResourceText.GetString("txtPage_NFV");
        spanPageTop.InnerText = ResourceText.GetString("txtPage_NFV");
        cvMessage.ErrorMessage = ResourceText.GetString("errNoteComment_NFV");
        rfvMessage.ErrorMessage = ResourceText.GetString("errMessage_NFV");
        //lblComments.Text = ResourceText.GetString("lblCommentHeader_NFV");
        divLogin.InnerHtml = ResourceText.GetString("lblLoginMessage_NFV") + " " + "<a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);'>Log in</a>" + " " + ResourceText.GetString("lblOr_NFV") + " " + "<a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>Sign up</a>";

        string tributeHome;        
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
        noteUrl = tributeHome + "Note.aspx";

        string query_string = string.Empty;
        if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
        {
            query_string = "?TributeType=" + HttpUtility.UrlEncode(_tributeType);
            noteUrl = noteUrl + query_string;
            tributeHome = tributeHome + query_string;
        }
        noteUrl += (noteUrl.Contains("?") ? "&" : "?") + "noteId=" + _noteId.ToString();

        aTributeHome.HRef = tributeHome;
        noteWallTributeHome.Text = tributeHome;
        noteWallTributeHome1.Text = tributeHome;

        noteCommentWallPostSubject.Text = string.Format("{0} commented on a note on the: {1} {2} Tribute", _userName, _tributeName, _tributeType);
        noteWallLink.Text = noteUrl;
        noteWallLink1.Text = noteUrl;
        noteWallTributeType.Text = _tributeType;
        noteWallTributeImage.Text = profile_prefix+_tributeImage;       
    }

    /// <summary>
    /// Method to set the control visibility
    /// </summary>
    private void SetControlsVisibiltiy()
    {
        //for setting edit button visibility
        if (isAdmin)
            lbtnEditNote.Visible = true;
        else
            lbtnEditNote.Visible = false;

        //to set visibility of Post comment section
        if (!Equals(_userId, 0))
        {
            divPostComment.Visible = true;
            divLogin.Visible = false;
        }
        else
        {
            divPostComment.Visible = false;
            divLogin.Visible = true;
        }
    }

    /// <summary>
    /// Method to set values to the comment object
    /// </summary>
    /// <param name="CurrentPage">Current page number</param>
    /// <returns>Filled CommentTributeAdministrator entity.</returns>
    public CommentTributeAdministrator SetCommentObject(int CurrentPage)
    {
        CommentTributeAdministrator objComAdmin = new CommentTributeAdministrator();
        objComAdmin.UserId = _userId;
        //objComAdmin.TypeCodeId = typeCodeId;
        objComAdmin.TypeCodeName = typeCodeName;
        objComAdmin.CommentTypeId = _noteId;
        objComAdmin.TributeId = _tributeId;
        objComAdmin.CurrentPage = (int)CurrentPage;
        objComAdmin.PageSize = (int)pageSize;
        //objComAdmin.TotalRecords = totalRecordCount;
        return objComAdmin;
    }

    /// <summary>
    /// Method to return the filled comment object to save
    /// </summary>
    /// <returns>Filled Comments entity</returns>
    public Comments GetCommentDataToSave()
    {
        Comments objComment = new Comments();
        objComment.UserId = _userId;
        objComment.CodeTypeName = TYPE_NAME_TO_SAVE;
        objComment.ModuleFunctionalityName = MODULE_FUNCTIONALITY_NAME;
        objComment.TypeCodeId = typeCodeId;
        objComment.TributeId = _tributeId;
        objComment.CommentTypeId = _noteId;
        objComment.CreatedBy = _userId;
        objComment.CreatedDate = DateTime.Now;
        objComment.IsActive = true;
        objComment.IsDeleted = false;
        objComment.Message = txtMessage.Text.ToString().Trim();
        objComment.UserName = _userName;
        objComment.TributeName = _tributeName;
        objComment.TributeType = _tributeType;
        objComment.TributeUrl = _tributeUrl;
        objComment.PathToVisit = Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;
        return objComment;
    }

    /// <summary>
    /// Method to get the data from session for Logged in user and selected tribute details.
    /// </summary>
    private void GetValuesFromSession()
    {
        StateManager objStateManager = StateManager.Instance;

        //to get user id from session as user is logged in user
        objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionValue, null))
        {
            _userId = objSessionValue.UserId;
            _userName = objSessionValue.FirstName == string.Empty ? objSessionValue.UserName : (objSessionValue.FirstName + " " + objSessionValue.LastName);
        }

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

        //CreateTributeSession(); //to create the tribute session values if user comes o this page from link or from favorites list.
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
        objStateManager.Add("UserAdminInfo_NoteFullView", objUserInfo, StateManager.State.Session);
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
    #endregion
}