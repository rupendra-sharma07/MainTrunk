///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Gift.Gift.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to select and send gifts.
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
using TributesPortal.Gift.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;
using Facebook;

#endregion


/// <summary>
///Tribute Portal-Gifts UI Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the UI class Gift_Gift for the Gifts View, Edit and Delete. This will implement the 
// All the Properties in the IGift interface. and will extend PageBase class which provides 
// 1. Error Event Handler
// 2. Exception handling
/// </summary>
/// 
public partial class Gift_Gift : PageBase, IGift
{
    #region CLASS VARIABLES

    private GiftPresenter _presenter;

    private SessionValue objSessionValue = null;
    private Tributes objTribute = null;

    private int _UserId;
    protected int _TributeId;
    private int _GiftId;
    private int _ImageId;
    private bool _IsAdmin;
    protected string _TributeName;
    protected string _TributeType;
    protected bool _isActive;
    private string _TributeURL;
    private int _PageSize = 10;
    private int _CurrentPage = 1;
    private int _TotalRecordCount;
    private string GiftImageURL = "";
    private string _FirstName = "";
    private string _LastName = "";
    private string _userName = string.Empty;
    private string _anonymusUserName = string.Empty;

    //AG:Addd for Expiry Notice
    private string _TributePackageType;

    //LHK: WordPress Integration
    private bool isInIframe = false;
    private string _TopUrl = string.Empty;
    private int _UserTributeId;
    string strQueryString = string.Empty;

    #endregion


    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(Gift_Gift));
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
        try
        {
            
            //if (Session["Pack_type"] != null)
            //{
            //    if (Session["Pack_type"].ToString().Contains("Announce"))
            //    {
            //        if (Session["Flag"] != null)
            //        {
            //            if (Session["Flag"].ToString() == "Open")
            //            {
            //                Page.ClientScript.RegisterStartupScript(GetType(), "a", "fnExpiryNoticePopupClose();", true);
            //                Session["Flag"] = "Close";
            //            }
            //        }
            //    }
            //}
            // get the Tribute and User detail from the Session
            GetValuesFromSession();

            //AG: Added code for expiry message
            if (!Equals(_TributePackageType, null))
            {
                if (_TributePackageType.Contains("Announce"))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "fnExpiryNoticePopupClose();", true);
                }
            }

            // set the value to controls
            SetControlsValue();
            //aTributeHome.HRef = Session["APP_PATH"] + _TributeURL + "/";

            //Start - Modification on 9-Dec-09 for the enhancement 3 of the Phase 1
            if (_TributeName != null) Page.Title = _TributeName + " | Gifts";
            //End

            if (!this.IsPostBack)
            {
                txtMessage.Attributes.Add("onkeyup", "CheckLength();");

                this._presenter.OnViewInitialized();

                // Set Visibility of control on the basis of user right and total number of gifts
                SetControlsVisibility();

            }

            this._presenter.OnViewLoaded();


            // Set the URL of the Gift Image
            SetGiftImageUrl();

            //set message for non logged in user
            if (_UserId > 0)
                lblNonLoggedIn.Text = " and ";
            else
                lblNonLoggedIn.Text = ", enter your name, ";

            // Check for session !=null for nonh logged in user sending gift. LHK
            if (Request.QueryString["gift_without_login"] != null)
            {
                string nonLoggedIn = Request.QueryString["gift_without_login"].ToString();
                if (nonLoggedIn == "true")
                {
                    if (Session["giftObj"] != null)
                    {
                        AddComment();

                    }
                }
            }
            else if (_UserId > 0 && Session["giftObj"] != null)
            {
                AddComment();
            }
            //ConnectSession sess = new ConnectSession(
            //  ConfigurationManager.AppSettings["APIKey"],
            //  ConfigurationManager.AppSettings["Secret"]);
            if (Facebook.Web.FacebookWebContext.Current.Session != null)
            {
                //commented for wile:LHK
                imgFB.Visible = true;
                imgYT.Visible = false;
            }
            else
            {
                imgFB.Visible = false;
                imgYT.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(WebConfig.AppBaseDomain.ToString() + "Errors/Error404.aspx");
        }
    }
    private void AddComment()
    {
        if (_UserId > 0)
            txtMessage.Text = ((object[])(Session["giftObj"]))[0].ToString();
            
        else
        {
            txtAnnMessage.Text = ((object[])(Session["giftObj"]))[0].ToString();
            txtName.Text = ((object[])(Session["giftObj"]))[1].ToString();
        }
        ImageUrl = ((object[])(Session["giftObj"]))[2].ToString();
        _ImageId = 0;
        GiftImageURL = hdnGiftImageURL.Value.ToString();
        this._presenter.InsertGifts();
        txtMessage.Text = "";
        Session["giftObj"] = null;

        string queryString = "?PageNo=1";
        Response.Redirect(Context.Request.RawUrl.Split("?".ToCharArray())[0] + queryString, false);
    }

    protected void lbtnSubmit_Click(object sender, EventArgs e)
    {
        GiftImageURL = hdnGiftImageURL.Value.ToString();
            this._presenter.InsertGifts();
        txtMessage.Text = "";

        string queryString = "?PageNo=1";
        //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Gift.ToString()) + queryString, false);
        //Response.Redirect("gift.aspx" + queryString, false);
        Response.Redirect(Context.Request.RawUrl.Split("?".ToCharArray())[0] + queryString, false);                             

    }

    protected void repImage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType != ListItemType.Item) && (e.Item.ItemType != ListItemType.AlternatingItem))
        {
            return;
        }
        else
        {
            Image tmpImageUrl = (Image)e.Item.FindControl("imgImageList");

            string function = "SetImage('" + tmpImageUrl.ImageUrl + "')";
            tmpImageUrl.Attributes.Add("onclick", function);
            //tmpImageUrl.Checked = false;
        }
    }

    protected void repGift_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            GiftImageURL = hdnGiftImageURL.Value.ToString();

            if (e.CommandName == "Delete")
            {
                _GiftId = int.Parse(((HiddenField)e.Item.FindControl("hdnGiftId")).Value.ToString());
                this._presenter.DeleteGift();

                string queryString = "?PageNo=1";
                //Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Gift.ToString()) + queryString, false);

                if (WebConfig.ApplicationMode.Equals("local"))
                {
                    Response.Redirect("Gift.aspx" + queryString, false);
                }
                else
                {
                    //Use this line for server and comment the above line
                    Response.Redirect("http://" + _TributeType.ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + _TributeURL + "/Gift.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }

    }

    protected void repGift_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
            btnDelete.Text = ResourceText.GetString("lbtnDelete_GT");
            //btnDelete.Attributes.Add("onclick", "doModalDeleteConfirm");
            btnDelete.Attributes.Add("onclick", "if(confirm('" + ResourceText.GetString("msgDelete_GT") + "')){}else{return false}");

            int UserId = int.Parse(((HiddenField)e.Item.FindControl("hdnUserId")).Value.ToString());

            if ((_IsAdmin) && (UserId == 0))
            {
                btnDelete.Visible = true;
            }
            else if (((_IsAdmin) || (_UserId == UserId)) && (UserId != 0))
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
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
    public GiftPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    #region IGift Members

    public int UserID
    {
        get
        {
            return _UserId;
        }

        set
        {
            UserID = value;
        }
    }

    public int TributeID
    {
        get
        {
            return _TributeId;
        }

        set
        {
            TributeID = value;
        }
    }

    public int GiftId
    {
        get
        {
            return _GiftId;
        }

        set
        {
            GiftId = value;
        }
    }

    public string TributeName
    {
        get
        {
            return _TributeName;
        }
        set
        {
            _TributeName = value;
        }
    }

    public string TributeType
    {
        get
        {
            return _TributeType;
        }
        set
        {
            _TributeType = value;
        }
    }

    public string GiftMessage
    {
        get
        {
            string message = "";

            if (_UserId > 0)
            {
                if (txtMessage.Text.ToString().ToLower().Trim().Equals("message"))
                    txtMessage.Text = string.Empty;
                message = txtMessage.Text.ToString().Trim();
            }
            else
            {
                if (txtAnnMessage.Text.ToString().ToLower().Trim().Equals("message"))
                    txtAnnMessage.Text = string.Empty;
                message = txtAnnMessage.Text.ToString().Trim();
            }
                       
            if (message.Length > 200)
            {
                return message.Substring(0, 200);
            }
            else
            {
                return message;
            }
        }
        set
        {
            txtMessage.Text = value;
        }
    }

    public string GiftSentBy
    {
        get
        {
            string name = txtName.Text.ToString().Trim();
            if (name.Length > 40)
            {
                return name.Substring(0, 40);
            }
            else
            {
                return name;
            }
        }
        set
        {
            txtName.Text = value;
        }
    }

    public string ImageUrl
    {
        get
        {
            return hdnGiftImageURL.Value.ToString();
        }
        set
        {
            string[] virtualDir = CommonUtilities.GetPath();

            hdnGiftImageURL.Value = virtualDir[2] + value;
            imgGiftImage.Src = virtualDir[2] + value;
        }
    }

    public IList<Gifts> GiftList
    {
        set
        {
            repGift.DataSource = value;
            if (value.Count > 0)
            {
                if (!string.IsNullOrEmpty(value[0].GiftMessage.ToString()))
                { Master.fbDescription = value[0].GiftMessage.ToString(); }

                if (!string.IsNullOrEmpty(value[0].ImageUrl.ToString()))
                { Master.fbThumbnail = value[0].ImageUrl.ToString(); }
                else
                { Master.fbThumbnail = ImageUrl.ToString(); }
            }
            repGift.DataBind();
        }
    }

    public IList<GiftImage> ImageList
    {
        set
        {
            repImage.DataSource = value;
            repImage.DataBind();
        }
    }

    public bool IsAdmin
    {
        get
        {
            object[] objVal = (object[])ViewState[Gifts.GiftMaintainState.Gift_Admin.ToString()];
            if (objVal != null)
            {
                _IsAdmin = bool.Parse(objVal[0].ToString());
            }
            return _IsAdmin;
        }
        set
        {
            _IsAdmin = value;

            // Add the admin value in View State
            object[] objVal = { _IsAdmin };
            ViewState.Add(Gifts.GiftMaintainState.Gift_Admin.ToString(), objVal);
        }
    }

    public int ImageId
    {
        get
        {
            return _ImageId;
        }

        set
        {
            _ImageId = value;
        }
    }

    public int TotalRecordCount
    {
        get
        {
            return _TotalRecordCount;
        }

        set
        {
            _TotalRecordCount = value;
        }
    }

    public int PageSize
    {
        get
        {
            return _PageSize;
        }

        set
        {
            _PageSize = value;
        }
    }

    public int CurrentPage
    {
        get
        {
            return _CurrentPage;
        }

        set
        {
            _CurrentPage = value;
        }
    }

    public string RecordCount
    {
        set
        {
            spanHeadRecordCount.InnerText = value;
            spanFootRecordCount.InnerText = value;
        }
    }

    public string DrawPaging
    {
        set
        {
            spanPagingHead.InnerHtml = value;
            spanPagingFoot.InnerHtml = value;
        }
    }

    public string FirstName
    {
        get
        {
            return _FirstName;
        }
    }

    public string LastName
    {
        get
        {
            return _LastName;
        }
    }

    public string UserName
    {
        get
        {
            return _userName;
        }
    }
    public string UrlToEmail
    {
        get
        {
            string ApplicationPath;

            if (Request.Cookies["topurl"] != null)
            {
                _TopUrl = Request.Cookies["topurl"].Value.ToString().Trim().ToLower();
            }

            string EmailHref;// = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeURL + "/Gift.aspx" + "</a>";
            string QueryString = "?TributeId=" + _TributeId + "&TributeName=" + _TributeName + "&TributeType=" + _TributeType + "&TributeUrl=" + _TributeURL + "&mode=emailPage";
            //string ApplicationPath = "<a href='http://" + Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;

            if (Session["isInIframe"] != null)
            {
                isInIframe = bool.Parse(Session["isInIframe"].ToString());
                Session["isInIframe"] = null;
            }
            if (!(string.IsNullOrEmpty(_TopUrl)) && isInIframe)
            {
                EmailHref = _TopUrl + "?http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeURL + "/Gift.aspx" + "</a>";
                ApplicationPath = "<a href='" + _TopUrl + "?http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeURL + "/Gift.aspx";
            }
            else
            {
                EmailHref = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeURL + "/Gift.aspx" + "</a>";
                ApplicationPath = "<a href='http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + _TributeURL + "/Gift.aspx";
            }

            return ApplicationPath + "'>" + EmailHref;

        }
    }

    public string AnonymusUserName
    {
        get
        {
            return _anonymusUserName;
        }
    }

    #endregion

    #endregion


    #region METHODS


    /// <summary>
    /// This function will get the values (User Id and Tribute Detail) from the session
    /// </summary>
    private void GetValuesFromSession()
    {
        try
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            // get values from session
            StateManager objStateManager = StateManager.Instance;

            //to get user id from session as user is logged in user
            objSessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);
            if (objSessionValue != null)
            {
                _UserId = objSessionValue.UserId;
                _FirstName = objSessionValue.FirstName;
                _LastName = objSessionValue.LastName;
                _userName = objSessionValue.FirstName == string.Empty ? objSessionValue.UserName : (objSessionValue.FirstName + " " + objSessionValue.LastName);
            }

            //if user is coming through link
            if (Request.QueryString["mode"] != null)
            {
                if (Request.QueryString["TributeId"] != null)
                    _TributeId = int.Parse(Request.QueryString["TributeId"].ToString());

                if (Request.QueryString["TributeName"] != null)
                    _TributeName = Request.QueryString["TributeName"].ToString();

                if (Request.QueryString["TributeType"] != null)
                    _TributeType = Request.QueryString["TributeType"].ToString();

                if (Request.QueryString["TributeUrl"] != null)
                    _TributeURL = Request.QueryString["TributeUrl"].ToString();

                //to create the tribute session values if user comes from the link.
                if (Session["TributeSession"] == null)
                    CreateTributeSession();
            }
            else
            {
                // to get tribute detail from session
                objTribute = (Tributes)objStateManager.Get(PortalEnums.SessionValueEnum.TributeSession.ToString(), StateManager.State.Session);
                if (objTribute != null)
                {
                    _TributeId = objTribute.TributeId;
                    _UserTributeId = objTribute.UserTributeId;
                    _TributeName = objTribute.TributeName;
                    _TributeType = objTribute.TypeDescription;
                    _TributeURL = objTribute.TributeUrl;
                    _isActive = objTribute.IsActive;
                    _TributePackageType = objTribute.TributePackageType;
                }
            }

            //to get current page number, if user clicks on page number in paging it gets tha page number from query string
            //else page number is 1
            if (Request.QueryString["PageNo"] != null)
            {
                _CurrentPage = int.Parse(Request.QueryString["PageNo"].ToString());
            }
            else
            {
                _CurrentPage = 1;
            }

            if (_TributeId == 0)
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
            }

            //to get page size from config file
            _PageSize = (int.Parse(WebConfig.Pagesize_Gift));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SetSessionValues(string name,string messg, string Image)
    {
       string[] virtualDir = CommonUtilities.GetPath();

      string   ImageXPath = virtualDir[2].ToString();
      Image = Image.Replace(ImageXPath, "");
      object[] giftobj = { messg, name, Image };
        
        HttpContext.Current.Session["giftObj"] = giftobj;
        //HttpContext.Current.Session["GiftMessage"] = messg ;     

    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void setIsInTopurl(bool inIframe)
    {
        Session["isInIframe"] = inIframe;
    }

    /// <summary>
    /// This Function will set the value of the control and error messages from the resource File
    /// </summary>
    private void SetControlsValue()
    {
        try
        {
            //Text for labels from the resource file

           // lnkChooseGift.InnerText = ResourceText.GetString("lnkChooseGift_GT");


            imgGiftImage.Alt = ResourceText.GetString("imgGiftImage_GT");
            lblMessage.InnerText = ResourceText.GetString("lblMessage_GT");
            lblName.InnerText = ResourceText.GetString("lblName_GT");
            lbllogin.InnerText = ResourceText.GetString("lbllogin_GT");
            lnkLogin.InnerText = ResourceText.GetString("lnkLogin_GT");
            lnkSignUp.InnerText = ResourceText.GetString("lnkSignUp_GT");
            lbtnSubmit.Text = ResourceText.GetString("lbtnSubmit_GT");
            lnkSignUp.InnerText = ResourceText.GetString("lnkSignUp_GT");

            string tributeHome;
            string giftUrl;
            if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
            {
                tributeHome = Session["APP_PATH"] + _TributeURL;
            }
            else
            {
                tributeHome = "http://" + _TributeType.Replace("New Baby", "newbaby").ToLower() + "." +
                    WebConfig.TopLevelDomain + "/" + _TributeURL;
            }
            tributeHome += "/";
            giftUrl = tributeHome + "Gift.aspx";
            if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
            {
                string query_string = "?TributeType=" + HttpUtility.UrlEncode(_TributeType);
                giftUrl = giftUrl + query_string;
                tributeHome = tributeHome + query_string;
            }

            aTributeHome.HRef = tributeHome;
            giftWallTributeHome.Text = tributeHome;
            giftWallTributeHome1.Text = tributeHome;

            giftWallPostSubject.Text = string.Format("{0} gave a gift on the: {1} {2} Tribute", _FirstName, _TributeName, _TributeType);
            giftWallLink.Text = giftUrl;
            giftWallLink1.Text = giftUrl;
            giftWallTributeType.Text = _TributeType;
            //Text for error messages from the resource file
            //valTributeName.ErrorMessage = ResourceText.GetString("valTributeName_ST");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Set Visibility of control on the basis of user right and total number of gifts
    /// </summary>
    private void SetControlsVisibility()
    {
        try
        {
            //to set visibility of comments list. if no record the found displays message else displays list.
            if (_TotalRecordCount == 0)
            {
                divGiftList.Visible = false;
                divMessage.Visible = true;
                divMessage.InnerText = ResourceText.GetString("strNoMessage_GT");
            }
            else
            {
                divGiftList.Visible = true;
                divMessage.Visible = false;
                // Set the defualt focus of the page
                //Page.SetFocus(txtMessage);
            }

            if (_UserId == 0)
            {
                lbtnSubmit.Attributes.Add("onclick", "setIsInTopurl();");
                lbtnSubmit.Attributes["onclick"]+= "setSessionMsg(); return false;";
                divAnonymousUser.Visible = true;
                divRegisteredUser.Visible = false;

                // Set the defualt focus of the page
               // Page.SetFocus(txtName);
            }
            else
            {
                lbtnSubmit.Attributes.Add("onclick", "setIsInTopurl();");
                divAnonymousUser.Visible = false;
                divRegisteredUser.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// This function will set the Image Url
    /// </summary>
    private void SetGiftImageUrl()
    {
        if (GiftImageURL != string.Empty)
        {
            hdnGiftImageURL.Value = GiftImageURL.ToString();
            imgGiftImage.Src = GiftImageURL.ToString();
            GiftImageURL = string.Empty;
        }
    }

    /// <summary>
    /// Method to create the tribute session values if user comes to this page from link or from favorites list.
    /// </summary>
    private void CreateTributeSession()
    {
        Tributes objTribute = new Tributes();
        objTribute.TributeId = _TributeId;
        objTribute.TributeName = _TributeName;
        objTribute.TypeDescription = _TributeType;
        objTribute.TributeUrl = _TributeURL;
        objTribute.IsActive = _isActive;
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add(PortalEnums.SessionValueEnum.TributeSession.ToString(), objTribute, TributesPortal.Utilities.StateManager.State.Session);
    }

    #endregion

    
}