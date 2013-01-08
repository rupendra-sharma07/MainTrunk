///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.GuestBook.GuestBook.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays and allows users to add entries to the guestbook for a tribute
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
using TributesPortal.GuestBook.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;
using Facebook;
using Facebook.Web;

#endregion

/// <summary>
///Tribute Portal-Channel Home Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.
/// </summary>
public partial class GuestBook_GuestBook : PageBase, IGuestBook
{
    #region CLASS VARIABLES
    //public static int usertypeid;

    private GuestBookPresenter _presenter;
    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;
    PagedDataSource pagedDataSource = new PagedDataSource();
    private int _userId = 0;
    private int intPageSize;
    public int _tributeId = 0;
    private int currentPage;
    private int totalRecordCount;   //Get value from store procedure
    private bool isAdmin;
    private int typeCodeId = 3; //Type code id is 3 for comments
    protected string _tributeName;
    protected string _tributeType;
    private string _tributeUrl;
    private string _userName;
    protected bool _isActive;
    public string _gbUrl;
    private string profile_prefix = CommonUtilities.GetPath()[2].ToString();

    //AG:Addd for Expiry Notice
    private string _TributePackageType;

    //LHK: WordPress Integration
    private string _TopUrl = string.Empty;
    private bool isInIframe = false;
    #endregion

    #region CONSTANT
    private const string _typeName = "GuestBook";
    #endregion

    // New to save Guest user name and Message
    ArrayList _arrComments = new ArrayList();
    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        //LHK: 3:59 PM 9/5/2011 - Wordpress topURL
        if (Request.QueryString["topurl"] != null)
        {
            _TopUrl = Request.QueryString["topurl"].ToString();
            Response.Cookies["topurl"].Value = _TopUrl;
            Response.Cookies["topurl"].Domain = "." + WebConfig.TopLevelDomain;
            Response.Cookies["topurl"].Expires = DateTime.Now.AddHours(4);
        }
        Response.AppendHeader("Pragma", "no-cache");
        Response.AppendHeader("Cache-Control", "no-cache");

        Response.CacheControl = "no-cache";
        Response.Expires = -1;

        Response.ExpiresAbsolute = new DateTime(1900, 1, 1);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Ajax.Utility.RegisterTypeForAjax(typeof(GuestBook_GuestBook));

        try
        {
            StringBuilder html = new StringBuilder();
            //imgAppLogo.Attributes.Add("onerror", "this.src='" + ResolveUrl("~/assets/images/bg_ProfilePhoto.gif") + "'");
            StateManager objStateManager = StateManager.Instance;
            //to get user id from session as user is logged in user
            objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
            if (!Equals(objSessionValue, null))
            {
                _userId = objSessionValue.UserId;
                _userName = objSessionValue.FirstName == string.Empty ? objSessionValue.UserName : (objSessionValue.FirstName + " " + objSessionValue.LastName);
                if (FacebookWebContext.Current.Session != null)
                {
                    html.Append("<span style='cursor: default;' class='yt-Thumb' >");
                   // html.Append("<span style='border-bottom:solid 1px white ;border-right:solid 1px white ; width:58px;height:58px; '>");
                    html.Append("<fb:profile-pic uid=\"");
                    html.Append(FacebookWebContext.Current.UserId);
                    html.Append("\" size=\"square\" facebook-logo=\"true\"></fb:profile-pic></span>");
                    divImage.InnerHtml = html.ToString();

                }
                else
                {
                    string sImagePath = objSessionValue.UserImage != null && !string.IsNullOrEmpty(objSessionValue.UserImage.ToString()) ? objSessionValue.UserImage.ToString() : "";
                    if (!sImagePath.Equals(""))
                    {
                        if (!sImagePath.StartsWith("http://") && !sImagePath.StartsWith("https://"))
                        {
                            string[] virtualDir = CommonUtilities.GetPath();
                            if (virtualDir != null)
                            {
                                sImagePath = virtualDir[2] + sImagePath;  //+ Latest[i].VideoUrl 
                            }
                        }                    
                        
                        html.Append("<a class='yt-Thumb' style='cursor: default;' href='javascript:void(0);'> <img src='");
                        html.Append(sImagePath);
                        html.Append("' width='54' height='54'  /></a>");//class='yt-ItemPhoto'
                        divImage.InnerHtml = html.ToString();
                    }
                    else
                    {
                        //html.Append("<a class='yt-Thumb' style='cursor: default;' <img src='");
                        //html.Append(ResolveUrl("~/assets/images/bg_ProfilePhoto.gif"));
                        //html.Append("' width='50' height='50' class='yt-ItemPhoto' style='cursor: default;'  /></a>");
                        //divImage.InnerHtml = html.ToString();      
                        html.Append("<a class='yt-Thumb' style='cursor: default;' href='javascript:void(0);'> <img src='");
                        html.Append(ResolveUrl("~/assets/images/bg_ProfilePhoto.gif"));
                        html.Append("' width='54' height='54'  /></a>");
                        divImage.InnerHtml = html.ToString(); 
                    }
                    //
                }            
                
            }
            if (!this.IsPostBack)
            {
                //New code added for YT phase 4 to display Different divs accordi ngly ser is loggedin using FB/YT or not logged in

                if (_userId > 0)
                {
                    //New added to check wather user is connected with facebook
                    // UserType   0---> Not loggedin,  1--> YT logged in , 2--> FB logeed in

                    divAuthUser.Style.Add(HtmlTextWriterStyle.Display, "inline");
                    divUnAuthUser.Style.Add(HtmlTextWriterStyle.Display, "none");
                    //rfvUserName.Enabled = false;

                    FacebookWebContext fbwebcon = new FacebookWebContext();

                    if (fbwebcon.Session != null)
                    {
                        imgAppLogo.Src = ResolveUrl("~/assets/images/icon_Facebook.gif");
                        lblUserName.InnerHtml = "Logged in as " + _userName;

                    }
                    else
                    {
                        imgAppLogo.Src = ResolveUrl("~/assets/images/favicon.ico");
                        lblUserName.InnerHtml = "Logged in as " + _userName;
                    }


                }
                else
                {
                    divUnAuthUser.Style.Add(HtmlTextWriterStyle.Display, "inline");
                    divAuthUser.Style.Add(HtmlTextWriterStyle.Display, "none");

                    html.Append("<a class='yt-Thumb' style='cursor: default;' href='javascript:void(0);'> <img src='");
                    html.Append(ResolveUrl("~/assets/images/bg_ProfilePhoto.gif"));
                    html.Append("' width='54' height='54'  /></a>");
                    divImage.InnerHtml = html.ToString();

                    //html.Append("<img src='");
                    //html.Append(ResolveUrl("~/assets/images/bg_ProfilePhoto.gif"));
                    //html.Append("' width='50' height='50' />");
                    //divImage.InnerHtml = html.ToString();     
                }
                // End

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
                    _TributePackageType = objTribute.TributePackageType;

                }
                else
                    Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/Error404.aspx");
                    //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);

                //AG: Added code for expiry message
                if (!Equals(_TributePackageType, null))
                {
                    if (_TributePackageType.Contains("Announce"))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "fnExpiryNoticePopupClose();", true);
                    }
                }

                //Start - Modification on 9-Dec-09 for the enhancement 3 of the Phase 1
                if (_tributeName != null) Page.Title = _tributeName + " | Guestbook";
                //End

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
                _gbUrl = tributeHome + "GuestBook.aspx";
                if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
                {
                    _gbUrl = _gbUrl + this.Master.query_string;
                    tributeHome = tributeHome + this.Master.query_string;
                }

                aTributeHome.HRef = tributeHome;
                gbWallTributeHome.Text = tributeHome;
                gbWallTributeHome1.Text = tributeHome;

                gbWallPostSubject.Text = string.Format("{0} added a guestbook message to the: {1} {2} Tribute", _userName, _tributeName, _tributeType);
                gbWallLink.Text = _gbUrl;
                gbWallLink1.Text = _gbUrl;
                gbWallTributeImage.Text = profile_prefix + objTribute.TributeImage;

                if (Session["TributeSession"] == null)
                    CreateTributeSession(); //to create the tribute session values if user comest o this page from link or from favorites list.

                //to get page size from config file
                intPageSize = (int.Parse(WebConfig.Pagesize_guestBook));

                //to get current page number, if user clicks on page number in paging it gets tha page number from query string
                //else page number is 1

                if (VwCurrentPage == 0)
                {
                    if (Request.QueryString["PageNo"] != null)
                        currentPage = int.Parse(Request.QueryString["PageNo"].ToString());
                    else
                        currentPage = 1;
                }
                else
                    currentPage = VwCurrentPage;

                //if user is coming to this page through a link in email gets the Tribute Id from the querystring
                //else Tribute id is to be picked from the session
                //if (Request.QueryString["TributeId"] != null)
                //{
                //    _tributeId = int.Parse(Request.QueryString["TributeId"].ToString());
                //}
                //else
                //{
                //    if (objStateManager.Get("TributeId", StateManager.State.Session) != null)
                //        _tributeId = int.Parse(objStateManager.Get("TributeId", StateManager.State.Session).ToString());
                //}

                UserIsAdmin(); //to check if user is tribute admin or not

                if (_tributeId > 0)
                {
                    //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);

                    if (!this.IsPostBack)
                    {
                        // btnPost.Attributes["OnClick"] += ";return Validate_Comments();";

                        SetControlText(); //to set values to labels and buttons

                        txtMessage.Attributes.Add("onkeyup", "CheckGuestBookCommentLength();");

                        totalRecordCount = this._presenter.OnPaging(GetSessionObject(currentPage)); //to get total number of records
                        //this._presenter.OnViewLoaded();
                        this._presenter.OnViewInitialized(GetSessionObject(currentPage), _tributeName, _tributeType);
                        ControlsVisibility(); //to set controls visibility
                        // Page.SetFocus(txtMessage);

                        if (_userId == 0)
                        {
                            btnPost.Attributes.Add("onClick", "return setSessionMsg(); return false;");
                            // rfvMessage.Enabled = false;
                        }
                        else
                        {
                            btnPost.Attributes.Add("onClick", "return validateInput(); return false;");
                        }
                    }
                }
                else
                {
                    Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/Error404.aspx");
                }


                string nonLoggedIn = Request.QueryString["GuestBook_without_login"];

                if (Session["CommentsSession"] != null && !string.IsNullOrEmpty(Session["CommentsSession"].ToString()))
                {

                    // code here for save
                    ArrayList _arrNew = (ArrayList)Session["CommentsSession"];
                    if (_arrNew != null && _arrNew.Count == 2)
                    {
                        txtMessage.Text = _arrNew[1].ToString();
                        txtUserName.Text = _arrNew[0].ToString();
                    }
                    if ((nonLoggedIn == "true")||(_userId > 0))
                    {
                        if (!txtMessage.Text.Trim().ToLower().Equals("message") && !txtUserName.Text.ToLower().Equals("name") && !txtMessage.Text.Trim().Equals("") && !txtUserName.Text.Trim().Equals("")) //
                        {
                            BtnClick_deligate _objBtnClickDeligate = new BtnClick_deligate(btnPost_Click);
                            object o = new object();
                            EventArgs obje = new EventArgs();
                            _objBtnClickDeligate(o, obje);
                        }
                    }
                    Session.Remove("CommentsSession");
                    txtMessage.Text = "Message";
                    txtUserName.Text = "Name";
                }


            }

        }
        catch (Exception ex)
        {
            Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/Error404.aspx");
        }

    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["topurl"] != null)
            {
                _TopUrl = Request.Cookies["topurl"].Value.ToString().Trim().ToLower();
            }
            if (Session["isInIframe"] != null)
            {
                isInIframe = bool.Parse(Session["isInIframe"].ToString());
                Session["isInIframe"] = null;
            }
            if (!(string.IsNullOrEmpty(_TopUrl)) && isInIframe)
            {
                this._presenter.OnSaveComments(SaveData(), _TopUrl);
            }
            else
            {
                if (!txtMessage.Text.Trim().ToLower().Equals("message") && !txtMessage.Text.Trim().Equals("")) //
                {
                    this._presenter.OnSaveComments(SaveData());
                }

            }
            txtMessage.Text = "";
            totalRecordCount = this._presenter.OnPaging(GetSessionObject(1));
            //this._presenter.OnViewInitialized(GetSessionObject(1), _tributeName, _tributeType);
            //string queryString = "?TributeId=" + _tributeId + "&TributeName=" + _tributeName + "&TributeType=" + _tributeType + "&PageNo=1";
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
            _gbUrl = tributeHome + "GuestBook.aspx";

            string queryString = (_gbUrl.Contains("?") ? "&" : "?") + "PageNo=1";

            Response.Redirect(_gbUrl + queryString, false);
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
            int UserId = Convert.ToInt32(dlComments.DataKeys[e.Item.ItemIndex]);
            //HiddenField hdnCommentID = (HiddenField)e.Item.FindControl("hdnCommentId");
            int intCommentId = int.Parse(e.CommandArgument.ToString()); //int.Parse(hdnCommentID.Value.ToString());

            Comments objComment = new Comments();
            objComment.CommentId = intCommentId;
            objComment.UserId = _userId;

            // Added new on 23-jun-2011 for YT phase 4 by rupendra
            LinkButton lnkbtnDel = new LinkButton();
            lnkbtnDel = (LinkButton)e.Item.FindControl("btnDelete");
            if (lnkbtnDel != null)
            {
                objComment.TableType = !string.IsNullOrEmpty(lnkbtnDel.Attributes["TableType"]) ? lnkbtnDel.Attributes["TableType"] : "0";
            }
            else objComment.TableType = "0";
            //end
            this._presenter.OnDeleteComments(objComment);


            string tributeGuestbook;
            StateManager objStateManager = StateManager.Instance;
            objTribute = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);
            if (objTribute != null)
            {
                if (objTribute.TributeId > 0)
                {
                    if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
                    {
                        tributeGuestbook = Session["APP_PATH"] + objTribute.TributeUrl + "/guestbook.aspx";
                    }
                    else
                    {
                        tributeGuestbook = "http://" + objTribute.TypeDescription.Replace("New Baby", "newbaby").ToLower() + "." +
                            WebConfig.TopLevelDomain + "/" + objTribute.TributeUrl + "/guestbook.aspx";
                    }
                    Response.Redirect(tributeGuestbook);
                }
            }
            else
            {
                int totalRecordOnPage = 0;
                totalRecordOnPage = dlComments.Items.Count;
                totalRecordOnPage = totalRecordOnPage - 1;

                if (totalRecordOnPage == 0 && currentPage > 1)
                {
                    currentPage = currentPage - 1;
                    VwCurrentPage = currentPage;
                }

                totalRecordCount = this._presenter.OnPaging(GetSessionObject(currentPage));
                this._presenter.OnViewInitialized(GetSessionObject(currentPage), _tributeName, _tributeType);
                ControlsVisibility();
            }
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

            CommentTributeAdministrator drv = (CommentTributeAdministrator)e.Item.DataItem;

            LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
            btnDelete.Text = ResourceText.GetString("btnDelete_GB");
            btnDelete.Attributes.Add("onclick", "if(confirm('" + ResourceText.GetString("msgDelete_GB") + "')){}else{return false}");
            //btnDelete.Attributes.Add("onclick", "if(confirm('Are you sure to Delete?')){}else{return false}");

            HiddenField hdnUserID = (HiddenField)e.Item.FindControl("hdnUserId");
            int UserId = int.Parse(hdnUserID.Value.ToString());

            HtmlAnchor anchrUsername = (HtmlAnchor)e.Item.FindControl("anchrUserName");

            //UserProfileModal_1(\'"+Eval("UserId")+"\')

            // Display User popup for registered users
            if (UserId > 0)
            {
                if (anchrUsername != null)
                {
                    //anchrUsername.InnerHtml+="<span style='border:none 0px !important;'>( "+DataBinder.Eval(e.Item.DataItem,"City").ToString()+", "+DataBinder.Eval(e.Item.DataItem,"State").ToString()+","+DataBinder.Eval(e.Item.DataItem,"Country").ToString()+" ) </span>";
                    anchrUsername.Attributes["onclick"] = "UserProfileModal_1('" + DataBinder.Eval(e.Item.DataItem, "UserId").ToString() + "')";

                    //Setting location
                    //var sLocation = string.Empty;
                    //sLocation += " (";
                    //sLocation += !string.IsNullOrEmpty(DataBinder.Eval(e.Item.DataItem, "City").ToString())
                    //                 ? (DataBinder.Eval(e.Item.DataItem, "City") + ", ")
                    //                 : "";
                    //sLocation += !string.IsNullOrEmpty(DataBinder.Eval(e.Item.DataItem, "State").ToString())
                    //                 ? (DataBinder.Eval(e.Item.DataItem, "State") + ", ")
                    //                 : "";
                    //sLocation += !string.IsNullOrEmpty(DataBinder.Eval(e.Item.DataItem, "Country").ToString())
                    //                ? (DataBinder.Eval(e.Item.DataItem, "Country").ToString())
                    //                : "";
                    //sLocation = sLocation.TrimEnd(',', ' ');
                    //sLocation += ")";
                    //if (sLocation.Trim().Equals("()") || sLocation.Trim().Equals("( )") || sLocation.Trim().Equals("(  )"))
                    //    sLocation = "";
                    //var spnAddrs = new HtmlGenericControl();
                    //spnAddrs = (HtmlGenericControl)e.Item.FindControl("spnAddrs");
                    //if (spnAddrs != null)
                    {
                    //    spnAddrs.InnerHtml = sLocation;
                    }
                }
            }
            else
            {
                if (anchrUsername != null)
                {
                    anchrUsername.Style.Add(HtmlTextWriterStyle.TextDecoration, "none");
                    anchrUsername.Attributes.Add("title", "Guest User");
                    anchrUsername.Style.Add(HtmlTextWriterStyle.Cursor, "default");

                }
            }
            //End
            if (isAdmin)
            {
                btnDelete.Visible = true;

            }
            else if (UserId > 0 && _userId == UserId)
            {
                btnDelete.Visible = true;

            }
            else
            {
                btnDelete.Visible = false;

            }

            StringBuilder html = new StringBuilder();
            HtmlGenericControl itemProfilePicture = (HtmlGenericControl)e.Item.FindControl("itemProfilePicSpn");
            HtmlImage itemprofilepic = (HtmlImage)e.Item.FindControl("itemProfilePicImg");
            //if (drv.FacebookUid != null &&
            //    (drv.UserImage.EndsWith("images/bg_ProfilePhoto.gif") ||
            //     !drv.UserImage.StartsWith(profile_prefix)))
            if (drv.FacebookUid != null)
            {
                if (FacebookWebContext.Current.Session != null)
                {
                    html.Append("<span style='border-bottom:solid 1px white ;border-right:solid 1px white ; width:58px;height:58px; '>");
                    html.Append("<fb:profile-pic uid=\"");
                    html.Append(drv.FacebookUid.ToString());
                    html.Append("\" size=\"square\" facebook-logo=\"true\"></fb:profile-pic></span>");
                    itemProfilePicture.InnerHtml = html.ToString();
                    itemprofilepic.Visible = false;
                }
                else
                {
                    itemprofilepic.Src = "http://graph.facebook.com/" + drv.FacebookUid.ToString() + "/picture?type=square";
                    itemProfilePicture.Visible = false;
                }
            }
            else             if (UserId > 0)
            {
                html.Append("<img style='border-bottom:solid 1px white ;border-right:solid 1px white ; width:48px; ' src='");
                html.Append(drv.UserImage.ToString());
                html.Append("' alt='Photo of ");
                html.Append(drv.UserName.ToString());
                html.Append("'  height='48' />");
                itemProfilePicture.InnerHtml = html.ToString();
                itemprofilepic.Visible = false;
            }
            else
            {
                html.Append("<img style=' border-bottom:solid 1px white ;border-right:solid 1px white ;width:48px;height:48px;'src='" + ResolveUrl("~/assets/images/bg_ProfilePhoto.gif") + "' ");
                html.Append(" alt='Guest User");
                html.Append("' />");
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
    public GuestBookPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    public IList<CommentTributeAdministrator> Comments
    {
        set
        {
            IList<CommentTributeAdministrator> objCommentList = value;
            //this is to check if after deleting records the count of record on page is 0 then the page to be redirected to previous page
            if (objCommentList.Count == 0 && currentPage > 1)
            {
                //string queryString = "?TributeId=" + _tributeId + "&TributeName=" + _tributeName + "&TributeType=" + _tributeType + "&PageNo=" + (currentPage - 1);
                string queryString = "?PageNo=" + (currentPage - 1);
                Response.Redirect("guestbook.aspx" + queryString, false);
                //Redirect.RedirectToPage(Redirect.PageList.GuestBook.ToString())
            }
            dlComments.DataSource = objCommentList;
            if (objCommentList.Count > 0)
            {
                if (!string.IsNullOrEmpty(objCommentList[0].Message.ToString()))
                { Master.fbDescription = objCommentList[0].Message.ToString(); }
            }
            dlComments.DataBind();
        }
    }
    public string RecordCount
    {
        set
        {
            spanRecordCountTop.InnerText = value; // +totalRecordCount;
            spanRecordCountBottom.InnerText = value;
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

    public string UserImage
    {
        set
        {
            StringBuilder html = new StringBuilder();
            html.Append("<img src='");
            html.Append(value);
            html.Append("' width='50' height='50' />");
            divImage.InnerHtml = html.ToString();

        }
    }
    //}

    #endregion

    #region METHODS
    /// <summary>
    /// Method to set the text to labels, buttons and validation controls
    /// </summary>
    private void SetControlText()
    {
        //Added by Rupendra on 20-jun-2011 YT phase4 GuestBook  for changing the message
        //lblMessage.InnerText = ResourceText.GetString("lblMessage_GB");
        if (_userId > 0)
            lblMessage.InnerText = ResourceText.GetString("lblMessage_GB_Auth");
        else
            lblMessage.InnerText = ResourceText.GetString("lblMessage_GB_NAuth");

        btnPost.Text = ResourceText.GetString("btnPost_GB");
        // rfvMessage.ErrorMessage = ResourceText.GetString("errMessage_GB");
        // cvMessage.ErrorMessage = ResourceText.GetString("errMessageLength_GB");

        StringBuilder lt = new StringBuilder();
        // MG 15-Jun 2010:  Wrong text corrected
        lt.Append(string.Format(ResourceText.GetString("lblloginmsg_GB_Master")
            + " <a href='javascript: void(0);' onclick='UserLoginModalpopupFromSubDomain(location.href,document.title);'>Log in</a> or <a href='javascript: void(0);' onclick='UserSignupModalpopupFromSubDomain(location.href,document.title);'>Sign up</a>"));
        lt.Append("<div id='gb-fblogin'>");
        lt.Append(ResourceText.GetString("lblloginmsg_GB_FB"));
        lt.Append("<fb:login-button size=\"small\"");
        lt.Append("\" onlogin=\"doAjaxLogin();\" v=\"2\"><fb:intl>");
        lt.Append("Login with Facebook");
        lt.Append("</fb:intl></fb:login-button>");
        lt.Append("</div>");
        // divLogin.InnerHtml = lt.ToString();

    }

    public CommentTributeAdministrator GetSessionObject(int CurrentPage)
    {
        try
        {
            //TributesPortal.Utilities.StateManager SMobject = StateManager.Instance;
            CommentTributeAdministrator objComAdmin = new CommentTributeAdministrator();
            objComAdmin.UserId = _userId;
            objComAdmin.TypeCodeId = typeCodeId;
            objComAdmin.CommentTypeId = _tributeId;
            objComAdmin.TributeId = _tributeId;
            objComAdmin.CurrentPage = (int)CurrentPage;
            objComAdmin.PageSize = (int)intPageSize;
            objComAdmin.TotalRecords = totalRecordCount;

            return objComAdmin;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method to return the filled comment object to save
    /// </summary>
    /// <returns>Filled Comments entity</returns>
    public Comments SaveData()
    {
        try
        {
            if (_tributeId == 0)
            {
                StateManager objStateManager = StateManager.Instance;
                Tributes objTribute = (Tributes)objStateManager.Get("TributeSession", StateManager.State.Session);
                if ((objTribute != null) && (objTribute.TributeId > 0))
                {
                    _tributeId = objTribute.TributeId;
                    _tributeName = objTribute.TributeName;
                    _tributeType = objTribute.TypeDescription;
                    _tributeUrl = objTribute.TributeUrl;
                    _isActive = objTribute.IsActive;
                    _TributePackageType = objTribute.TributePackageType;
                }

            }
            Comments objComment = new Comments();
            objComment.UserId = _userId;
            objComment.TypeCodeId = typeCodeId;
            objComment.CommentTypeId = _tributeId;
            objComment.CreatedBy = _userId;
            objComment.CodeTypeName = _typeName;
            
            // changed on 21-june -2011 by rupendra for YT phase 4
            if (_userId > 0)
                objComment.UserName = _userName;
            else
                objComment.UserName = txtUserName.Text.Trim();

            //New added to check wather user is connected with facebook
            // UserType   0---> Not loggedin,  1--> YT logged in , 2--> FB logeed in              
            var fbWebContext = FacebookWebContext.Current;
            if (FacebookWebContext.Current.Session != null)
            {
                objComment.UserType = "2";
            }
            else if (_userId > 0)
            {
                objComment.UserType = "1";
            }
            else objComment.UserType = "0";
            // End
            objComment.TributeName = _tributeName;
            objComment.TributeType = _tributeType;
            objComment.TributeUrl = _tributeUrl;
            objComment.PathToVisit = Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;
            //if (radLst.SelectedItem.Value.ToString() == "0")
            //    objComment.IsPrivate = false;
            //else
            //    objComment.IsPrivate = true;
            objComment.Message = txtMessage.Text.ToString().Trim();
            return objComment;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method to get user is admin or owner
    /// </summary>
    private void UserIsAdmin()
    {
        UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
        objUserInfo.UserId = _userId;
        objUserInfo.TributeId = _tributeId;

        objUserInfo.TypeName = _typeName; // "GuestBook";

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
        objStateManager.Add("UserAdminInfo_GuestBook", objUserInfo, StateManager.State.Session);
    }

    /// <summary>
    /// Method to set the Visibility of controls
    /// </summary>
    private void ControlsVisibility()
    {
        if (totalRecordCount == 0) //to set visibility of comments list. if no record the found displays message else displays list.
        {
            divComments.Visible = false;
            divMessage.Visible = true;
            divMessage.InnerText = ResourceText.GetString("strNoMessage_GB");
        }
        else
        {
            divComments.Visible = true;
            divMessage.Visible = false;
        }
        //this should be checked :  rupendra 24-june-2011
        if (dlComments.Items.Count > 0)
        {
            divComments.Visible = true;
            divMessage.Visible = false;
        }


        //if (_userId == 0)
        //{
        //    //divPostMessage.Visible = false;
        //    //divLogin.Visible = true;
        //    btnPost.Attributes.Add("onclick", "setSessionMsg();PostMessageModalpopup(location.href,document.title); return false;");
        //   // btnPost.Attributes["onclick"]+= "javascript:PostMessageModalpopup(location.href,document.title); return false;";

        //}
        //else
        //{
        //    //divPostMessage.Visible = true;
        //    //divLogin.Visible = false;
        //}
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
    #region "Properties"
    private int VwCurrentPage
    {
        set
        {
            ViewState["VwCurrentPage"] = value;
        }
        get
        {
            if (ViewState["VwCurrentPage"] == null)
                return 0;  //return first page
            else
                return Int32.Parse(ViewState["VwCurrentPage"].ToString());
        }
    }
    #endregion




    //deligate added for firing button click event by rupendra 
    public delegate void BtnClick_deligate(object sender, EventArgs e);

    //Method added for Saving Comment box/user names in session by rupendra 
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SetSessionValues(string sname, string smessg)
    {
        //GiftImageURL = hdnGiftImageURL.Value.ToString();
        ArrayList _arrComent = new ArrayList();
        _arrComent.Insert(0, sname);
        _arrComent.Insert(1, smessg);
        HttpContext.Current.Session["CommentsSession"] = _arrComent;
        //HttpContext.Current.Session["GiftMessage"] = messg ;     

    }


    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SetSessionTopurl(bool InIframe)
    {
        Session["isInIframe"] = InIframe;
    }
}