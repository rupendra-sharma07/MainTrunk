///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.Video.VideoUpload.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page is used to upload video tributes created using the Tribute Creator software
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.Video.Views;
using TributesPortal.ResourceAccess;
#endregion

public partial class Video_VideoUpload : PageBase, IVideoUpload
{
    #region CLASS VARIABLES
    private VideoUploadPresenter _presenter;
    private SessionValue objSessionValue = null;
    private VideoToken objToken = null;
    private int _userId = 0;
    private int _tributeId;
    protected string _tributeType;
    private string _tokenId = string.Empty;
    private VideoToken objTokenDetails;
    protected string _userName = string.Empty;

    protected string _section = HeaderSecionEnum.home.ToString();
    public string HomeNavValue = string.Empty;
    public string TourNavValue = string.Empty;
    public string FeaturesNavValue = string.Empty;
    public string ExamplesNavValue = string.Empty;
    public string PricingNavValue = string.Empty;
    public string _validityText = "7";
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["TokenId"] != null)
            _tokenId = Request.QueryString["TokenId"].ToString();
        else
        {
            //Response.Redirect("log_in.aspx");
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
        }
        lblMessage.Text = "Video tribute already exists for the selected tribute.";
        lblYes.Text = "Do you want to replace the existing video tribute with the new one?";

        if (!this.IsPostBack)
        {
            lblPhotoTributeYearlyCost.Text = WebConfig.PhotoOneyearAmount;
            lblPhotoTributeLifeTimeCost.Text = WebConfig.PhotoLifeTimeAmount;
            lblTributeYearlyCost.Text = WebConfig.TributeOneyearAmount;
            lblTributeLifeTimeCost.Text = WebConfig.TributeLifeTimeAmount;

            this._presenter.GetTokenDetails();
            this._presenter.GetUserDetails();
            GetSessionValues();
            SetControlVisibility();
            _presenter.GetTributesList();
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (!Equals(objSessionvalue, null) && !_section.Equals(HeaderSecionEnum.registration.ToString()))
            {
                spanLogout.InnerHtml = "<a class='logoutLink' id='header_logout' href='Logout.aspx'>Log out</a>";
                int intUserType = objSessionvalue.UserType;
                if (intUserType == 1)
                {
                    // _userName = objSessionvalue.FirstName + " " + objSessionvalue.LastName;
                    _userName = objSessionvalue.FirstName;
                }
                else if (intUserType == 2)
                {
                    _validityText = "30";
                    _userName = objSessionvalue.UserName;
                    double NetCreditPoints;
                    UserRegistration _objUserReg = new UserRegistration();
                    Users objUsers = new Users();
                    objUsers.UserId = objSessionvalue.UserId;
                    _objUserReg.Users = objUsers;
                    object[] param = { _objUserReg };
                    BillingResource objBillingResource = new BillingResource();
                    objBillingResource.GetCreditPointCount(param);
                    UserRegistration objDetails = (UserRegistration)param[0];
                    if (objDetails.CreditPointTransaction == null)
                    {
                        NetCreditPoints = 0;
                    }
                    else
                    {
                        NetCreditPoints = objDetails.CreditPointTransaction.NetCreditPoints;
                    }
                    //lbtnCreditCount.Text = "Credits (" + NetCreditPoints.ToString() + ")";
                    lnCreditCount.InnerHtml = "Credits (" + NetCreditPoints.ToString() + ")";

                    
                }
                // Added by Ashu on Oct 3, 2011 for rewrite URL 
                if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                    myprofile.HRef = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"].ToString() + "moments.aspx";//Session["APP_BASE_DOMAIN"].ToString() + "moments.aspx";
                else
                    myprofile.HRef = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"].ToString() + "tributes.aspx";//Session["APP_BASE_DOMAIN"].ToString() + "tributes.aspx";

                divProfile.Visible = true;
                //spanSignUp.Visible = false;
            }
            else if (!_section.Equals(HeaderSecionEnum.inner.ToString()))
            {
                StringBuilder sbl = new StringBuilder();
               
                sbl.Append("<a class='yt-horizontalSpacer' href='");
                if (_section.Equals(HeaderSecionEnum.home.ToString()))
                {
                    sbl.Append("log_in.aspx");
                }
                else
                {
                    sbl.Append("javascript: void(0);' onclick='UserLoginModalpopupFromSubDomain(location.href,document.title);");
                }
                sbl.Append("'>Log in</a>");

                //spanSignUp.Visible = !(_section.Equals(HeaderSecionEnum.registration.ToString()));
                spanLogout.InnerHtml = sbl.ToString();
                divProfile.Visible = false;
            }
            //LHK:(1:44 PM 2/2/2011) To show price in credits to a Business User
            if (objSessionvalue.UserType == 2)
            {
                lblPhotoTributeYearlyCost.Text = WebConfig.PhotoYearlyCreditCost;
                lblPhotoTributeLifeTimeCost.Text = WebConfig.PhotoLifeTimeCreditCost;
                lblTributeYearlyCost.Text = WebConfig.TributeYearlyCreditCost;
                lblTributeLifeTimeCost.Text = WebConfig.TributeLifeTimeCreditCost;
            }
        }
        //added to handle the session issue on selection of a tribute from the tribute list
        Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", Session.SessionID));
        GetSessionValues();
        
    }

    protected void lbtnAddVideo_Click(object sender, EventArgs e)
    {
        //_presenter.SaveVideoTribute();
        StateManager stateManager = StateManager.Instance;
        List<Tributes> objTributesList = (List<Tributes>)stateManager.Get("objTributeList", StateManager.State.Session);
        int havingVideoTribute = 0;
        foreach (Tributes tribute in objTributesList)
        {
            if (tribute.TributeId == int.Parse(lstTributes.SelectedValue))
            {
                CreateTributeSession(tribute);
                havingVideoTribute = tribute.HavingVideoTribute;
                //_tributeName = tribute.TributeName;
            }
        }
        if (havingVideoTribute == 0)
            _presenter.SaveVideoTribute();

        stateManager.Add("TokenDetails", null, StateManager.State.Session); //to set null to tokendetails session
        stateManager.Add("objTributeList", null, StateManager.State.Session);
        if (_tributeType == "New Baby")
            _tributeType = "newbaby";

        if (WebConfig.ApplicationMode.Equals("local"))
        {
            Response.Redirect(Session["APP_BASE_DOMAIN"] + Session["TributeURL"].ToString() + "/videos.aspx");
        }
        else
        {
            //Use this commented part for server and comment the line written above this commented part
            Response.Redirect("http://" + _tributeType + "." + WebConfig.TopLevelDomain + "/" + Session["TributeURL"] + "/videos.aspx", false);
        }
    }

    protected void lstTributes_SelectedIndexChanged(object sender, EventArgs e)
    {
        StateManager stateManager = StateManager.Instance;
        List<Tributes> objTributesList = (List<Tributes>)stateManager.Get("objTributeList", StateManager.State.Session);
        int havingVideoTribute = 0;
        foreach (Tributes tribute in objTributesList)
        {
            if (tribute.TributeId == int.Parse(lstTributes.SelectedValue))
            {
                CreateTributeSession(tribute);
                havingVideoTribute = tribute.HavingVideoTribute;
                //_tributeName = tribute.TributeName;
                break;
            }
        }
        if (havingVideoTribute > 0)
        {
            hdnReplace.Value = "1";
            lbtnAddVideo.Attributes.Add("onclick", "CheckForAcceptance();return false;");
            //ScriptManager.RegisterClientScriptBlock(lbtnAddVideo, GetType(), "Cancel", "doContactSend();", true);
        }
        else
        {
            lbtnAddVideo.Attributes.Remove("onclick");
        }
    }

    protected void lbtnYes_Click(object sender, EventArgs e)
    {
        _presenter.DeleteVideoTribute(); //to delete the existing video tribute
        _presenter.SaveVideoTribute(); //to save the newly uploaded video tribute

        StateManager stateManager = StateManager.Instance;
        stateManager.Add("TokenDetails", null, StateManager.State.Session); //to set null to tokendetails session
        stateManager.Add("objTributeList", null, StateManager.State.Session);
        //if (TributeType == "New Baby")
        //    TributeType = "newbaby";

        //Use this commented part for server and comment the line written below this commented part
        Tributes objTributeFromSession = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);

        // Mohit Video tribute Replacement
        if (WebConfig.ApplicationMode.Equals("local"))
        {
            if (objTributeFromSession.TypeDescription.ToLower() != "video")
            {
                Response.Redirect(Session["APP_BASE_DOMAIN"] + Session["TributeURL"].ToString() + "/videos.aspx");
            }
            else
            {
                Response.Redirect(Session["APP_BASE_DOMAIN"] + "video/videotribute.aspx?tributeId=" + Session["TributeId"]);
            }
        }
        else
        {
            if (objTributeFromSession.TypeDescription.ToLower() != "video")
            {
                Response.Redirect("http://" + objTributeFromSession.TypeDescription.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + Session["TributeURL"] + "/videos.aspx", false);
            }
            else
            {
                Response.Redirect("http://" + objTributeFromSession.TypeDescription.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/video/videotribute.aspx?tributeId=" + Session["TributeId"], false);
            }

        }
    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public VideoUploadPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    public List<Tributes> TributesList
    {
        set
        {
            lstTributes.DataSource = value;
            lstTributes.DataTextField = "TributeName";
            lstTributes.DataValueField = "TributeId";
            lstTributes.DataBind();

            if (value.Count > 0)
            {
                StateManager stateManager = StateManager.Instance;
                stateManager.Add("objTributeList", value, StateManager.State.Session);
            }

        }
    }

    public int UserId
    {
        get
        {
            return _userId;
        }
        set
        {
            _userId = value;
        }
    }

    public int TributeId
    {
        get
        {
            _tributeId = int.Parse(lstTributes.SelectedValue);
            return _tributeId;
            //return int.Parse(lstTributes.SelectedValue);
        }
    }

    public string VideoCaption
    {
        get
        {
            //if (!(string.IsNullOrEmpty(_tributeName)))
            //    return _tributeName; //TO DO:
            //else
            //{
            StateManager stateManager = StateManager.Instance;
            return ((Tributes)stateManager.Get("TributeSession", StateManager.State.Session)).TributeName;
            //}
        }
    }
    public string VideoDesc
    {
        get
        {
            return string.Empty; //TO DO:
        }
    }
    public string VideoTributeId
    {
        get
        {
            string _fileName = string.Empty;
            StateManager stateManager = StateManager.Instance;
            objToken = (VideoToken)stateManager.Get("TokenDetails", StateManager.State.Session);
            if (!Equals(objToken, null))
            {
                if (objToken.FileName == string.Empty)
                    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
                else
                    _fileName = objToken.FileName;
            }
            else
            {
                //Response.Redirect("log_in.aspx");
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }

            return _fileName;
        }
    }
    public string TokenId
    {
        get
        {
            return _tokenId;
        }
    }
    public VideoToken TokenDetails
    {
        get
        {
            return objTokenDetails;
        }
        set
        {
            if (!Equals(value, null))
            {
                objTokenDetails = value;
                StateManager stateManager = StateManager.Instance;
                stateManager.Add("TokenDetails", value, StateManager.State.Session);
            }
            else
            {
                //Response.Redirect("log_in.aspx");
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }
        }
    }
    public UserRegistration UserDetails
    {
        set
        {
            if (!Equals(value, null))
            {
                SessionValue _objSessionValue = new SessionValue(value.Users.UserId,
                                                                    value.Users.UserName,
                                                                   value.Users.FirstName,
                                                                   value.Users.LastName,
                                                                   value.Users.Email,
                                                                   value.UserBusiness == null ? 1 : 2, "Basic", value.Users.IsUsernameVisiable
                                                                   );
                StateManager stateManager = StateManager.Instance;
                stateManager.Add("objSessionvalue", _objSessionValue, StateManager.State.Session);
            }
            else
            {
                //Response.Redirect("log_in.aspx");
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }
        }
        get
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objValue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            UserRegistration objUserReg = new UserRegistration();
            Users objUser = new Users();
            objUser.UserId = objValue.UserId;
            objUser.UserName = objValue.UserName;
            objUser.UserType = objValue.UserType;
            objUser.Email = objValue.UserEmail;
            objUser.FirstName = objValue.FirstName;
            objUserReg.Users = objUser;
            return objUserReg;
        }
    }

    public string TributeName
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            return ((Tributes)stateManager.Get("TributeSession", StateManager.State.Session)).TributeName;
        }
    }
    public string TributeType
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            return ((Tributes)stateManager.Get("TributeSession", StateManager.State.Session)).TypeDescription;
        }
    }
    public string TributeUrl
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            return ((Tributes)stateManager.Get("TributeSession", StateManager.State.Session)).TributeUrl;
        }
    }

    public string NavigationName { get; set; }

    public string CreditLink
    {
        set
        {
            //lbtnCreditCount.Text = "Credits (" + value + ")";
            //lnCreditCount.InnerHtml = "Credits (" + value + ")";
        }
    }
    public string Section
    {
        get
        {
            return _section;
        }
        set
        {
            _section = value;
        }
    }
    public enum HeaderSecionEnum
    {
        tribute,
        home,
        channelHome,
        inner,
        login,
        registration
    }
    #endregion

    #region METHODS

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private string RemoveSpecialCharacter(string str)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            if ((str[i] >= '0' && str[i] <= '9') || (str[i] >= 'A' && str[i] <= 'z' || (str[i] == '.' || str[i] == '_')))
                sb.Append(str[i]);
        }
        return sb.ToString();
    }


    /// <summary>
    /// Method to get values from session
    /// </summary>
    private void GetSessionValues()
    {
        StateManager objStateManager = StateManager.Instance;
        //to get user id from session as user is logged in user
        objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionValue, null))
        {
            _userId = objSessionValue.UserId;
            _userName = objSessionValue.UserName;
        }
        else if (Equals(objSessionValue, null) || _userId == 0)
        {
            //Response.Redirect("log_in.aspx");
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
        }
    }

    /// <summary>
    /// Method to set controls visibility.
    /// </summary>
    private void SetControlVisibility()
    {
        if (_userId > 0)
        {
            //divProfile.Visible = true;
            //spanLogout.InnerHtml = "<a href='logout.aspx'>Log out</a>";
        }
        else
        {
            //divProfile.Visible = false;
        }
    }

    /// <summary>
    /// Method to create the tribute session values if user comes o this page from link or from favorites list.
    /// </summary>
    private void CreateTributeSession(Tributes objTrib)
    {
        Tributes objTribute = new Tributes();
        objTribute.TributeId = objTrib.TributeId;
        objTribute.TributeName = objTrib.TributeName;
        objTribute.TypeDescription = objTrib.TypeDescription;
        objTribute.TributeUrl = objTrib.TributeUrl;
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add("TributeSession", objTribute, TributesPortal.Utilities.StateManager.State.Session);
        _tributeType = objTribute.TypeDescription;
        Session["TributeURL"] = objTrib.TributeUrl;
        Session["TributeId"] = objTrib.TributeId;
    }
    #endregion



}