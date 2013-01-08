///Copyright       : Copyright (c) Optimus Information
///Project         : Your Tributes
///File Name       : TributePortal.DevelopmentWebsite.UserControl.TributePage Header.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page defines the items to be displayed on the Video tribute page 
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Text;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.BusinessLogic;
using Facebook;
using Facebook.Web;
using System.Media;
using TributesPortal.ResourceAccess;
using Microsoft.Practices.ObjectBuilder;


public partial class UserControl_TributePageHeader : System.Web.UI.UserControl
{
    #region Variable Declaration

    private int _tributeId;
    private int _userId;
    private string _UserName;
    private string strImagePath;
    private string _Website;
    private string _CompanyName;
    private string _BusinessAddress;
    private string _Phone;
    private const string DefaultPath = "~/TributePhotos/";
    private string _BGColor;
    private string strObituaryURL;
    private string _TributeLogoURL;
    public string websiteAddress;// = string.Empty;
    public string lnkFuneralHome;
    public string sObituaryURL = string.Empty;
    private string _tributeUrl;

    private string strBusinessAddress;
    private string strPhone;
    private string strHeaderBGColor;
    private string strHeaderLogo;
    private string strWebsite;
    private bool boolIsAddressOn;
    private bool boolIsPhoneon;
    private bool boolIsWebSiteon;
    private bool boolCustomHeaderOn;
    private bool boolObituaryLinkOn;
    private string strObituaryLink;

    private string URL;
    private string _emailID;
    private string _typeName;
    private string _userBussCity;
    private string _userBussState;
    private bool isTributeIdNumeric;
    private SessionValue objSessionValue = null;

    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["TributeUrl"] != null)
        {
            _tributeUrl = Request.QueryString["TributeUrl"].ToString();
            GetTributeIdOnTributeUrl(_tributeUrl, WebConfig.ApplicationType.ToString());
        }
        if((Request.QueryString["tributeId"] != null) && (int.TryParse(Request.QueryString["tributeId"].ToString(), out _tributeId)))
        {
            //fetching tributeId from url
        }
        StateManager objStateManager = StateManager.Instance;
        objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
        if (!Equals(objSessionValue, null))
        {
            _userId = objSessionValue.UserId;
        }
        if (Request.QueryString["username"] != null)
        {
            string _BusinessUserName = Request.QueryString["username"].ToString();
            VideoResource objVideoResource = new VideoResource();
            _userId = objVideoResource.GetUserIdByUserName(_BusinessUserName, WebConfig.ApplicationType.ToString());
        }
        if ((_tributeId > 0) || (_userId > 0))
        {
            if (_tributeId > 0)
            {
                GetUserIdByTributeId(_tributeId);
            }
            GetHeaderDetailsOnUserId(_userId);

            lblUserStreet.Text = BusinessAddress;
            if (Phone != null)
            {
                if (Phone.Length > 0)
                {
                    lblUsercontactNo.Visible = true;
                    //AG: for using format like (555) 555-5555, checked that length should be atleast 6 character.
                    if (Phone.Length > 6)
                    {
                        Phone = String.Format("({0}) {1}-{2}", Phone.Substring(0, 3), Phone.Substring(3, 3), Phone.Substring(6));
                    }
                    lblUsercontactNo.Text = string.Format("{0} {1}", "Tel: ", Phone);
                }
                else
                {
                    lblUsercontactNo.Visible = false;
                }
            }
            if (WebSite != null)
            {
                if (WebSite.Length > 0)
                {
                    lblWebAddress.Visible = true;
                    lblWebsite.Text = WebSite.ToString();
                    websiteAddress = GetAsUrl(WebSite.ToString());
                    linkWebAddress.Attributes.Add("href", websiteAddress);
                }
                else
                {
                    lblWebAddress.Visible = false;
                }
            }
            else
            {
                lblWebAddress.Visible = false;
            }
            dvCustomeHeader.Attributes.Add("style", "background-color:#" + HeaderBGColor + "");
            if (ObituaryURL != null)
            {
                if (ObituaryURL.Length > 0)
                {
                    divFuneralObHomePage.Visible = true;
                    sObituaryURL = GetAsUrl(ObituaryURL.ToString());
                }
                else
                {
                    divFuneralObHomePage.Visible = false;
                }
            }
            else
            {
                divFuneralObHomePage.Visible = false;
            }
            setTributeLogo();

            lnkFuneralHome = String.Format("http://{0}.{1}", _UserName, WebConfig.TopLevelDomain);
            linkViewFuneralHome.Attributes.Add("href", lnkFuneralHome);
        }



    }//end page_load

    #endregion

    #region properties

    public int TributeId
    {
        get
        {
            return _tributeId;
        }
        set
        {
            _tributeId = value;
        }
    }

    public string TributeUrl
    {
        get
        {
            return _tributeUrl;
        }
        set
        {
            _tributeUrl = value;
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

    public string UserName
    {
        get
        {
            return _UserName;
        }

        set
        {
            _UserName = value;
        }
    }

    public string TributeLogoPath
    {
        get
        {
            return _TributeLogoURL;

        }
        set
        {
            _TributeLogoURL = value;

        }
    }

    public string Website
    {
        get
        {
            return websiteAddress;
        }
        set
        {
            websiteAddress = value;
        }

    }

    public string HeaderBGColor
    {
        get
        {
            return _BGColor;
            Session["HeaderBGColor"] = _BGColor;
        }
        set
        {
            _BGColor = value;
        }
    }


    public string CompanyName
    {
        get
        {
            return _CompanyName;
        }
        set
        {
            _CompanyName = value;
        }
    }

    public string BusinessAddress
    {
        get
        {
            return _BusinessAddress;
        }
        set
        {
            _BusinessAddress = value;
        }
    }

    public string Phone
    {
        get
        {
            return _Phone;
        }
        set
        {
            _Phone = value;
        }
    }

    public string ObituaryURL
    {
        get
        {
            return strObituaryURL;
        }
        set
        {
            strObituaryURL = value;
        }
    }
    
    public string HeaderLogo
    {
        get
        {
            return strHeaderLogo;

        }
        set
        {
            strHeaderLogo = value;
            //Session["HeaderLogo"] = value.ToString();
            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add("HeaderLogo", value, StateManager.State.Session);
        }

    }

    public string WebSite
    {
        get
        {
            return strWebsite;
        }
        set
        {
            strWebsite = value;

        }

    }
    public bool IsAddressOn
    {
        get
        {
            return boolIsAddressOn;
        }
        set
        {
            boolIsAddressOn = Convert.ToBoolean(value);

        }
    }
    public bool IsPhoneOn
    {
        get
        {
            return boolIsPhoneon;
        }
        set
        {
            boolIsPhoneon = Convert.ToBoolean(value);

        }
    }

    public bool IsWebSiteOn
    {
        get
        {
            return boolIsWebSiteon;
        }
        set
        {
            boolIsWebSiteon = Convert.ToBoolean(value);

        }
    }
    public bool IsCustomHeaderOn
    {
        get
        {
            return boolCustomHeaderOn;
        }
        set
        {
            boolCustomHeaderOn = Convert.ToBoolean(value);
            TributePageHeader.Visible = Convert.ToBoolean(value);
        }
    }

    public bool IsObituaryURLOn
    {
        get
        {
            return boolObituaryLinkOn;
        }
        set
        {
            boolObituaryLinkOn = Convert.ToBoolean(value);

        }
    }
  
    public string UserBussCity
    {
        get
        {
            return _userBussCity;
        }
        set
        {
            _userBussCity = value;
        }
    }
    public string UserBussState
    {
        get
        {
            return _userBussState;
        }
        set
        {
            _userBussState = value;
        }
    }

    #endregion

    #region Methods

    private void setTributeLogo()
    {
        string[] virtualDir = CommonUtilities.GetPath();
        if (virtualDir != null)
        {
            StateManager objStateManager = StateManager.Instance;
            Object objHeaderLogo = objStateManager.Get("HeaderLogo", StateManager.State.Session);
            if (objHeaderLogo != null)
            {
                imgTributeImageView.ImageUrl = String.Format("{0}{1}", virtualDir[2], objHeaderLogo); ;
                imgTributeImageView.Visible = true;
            }
        }
    }
    private string GetAsUrl(string str)
    {
        string Url = string.Empty;
        if (!(str.Contains("http://")) || (str.Contains("https://")))
            Url = string.Format("http://{0}", str);
        else
            Url = str;
        return Url;

    }

    private string GetBussAdd(string p)
    {
        string BussAddress = string.Empty;
        try
        {
            if (p != "")
            {
                BussAddress = p;
            }

            if (UserBussCity.ToString() != "")
            {
                if (BussAddress != "")
                {
                    BussAddress += ", ";
                }
                BussAddress += UserBussCity.ToString();
            }
            if (UserBussState.ToString() != "")
            {
                if (BussAddress != "")
                {
                    BussAddress += ", ";
                }
                BussAddress += UserBussState.ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return BussAddress;
    }

    public void GetHeaderDetailsOnUserId(int userId)
    {
        VideoResource objVideoResource = new VideoResource();
        UserBusiness objUserBusiness = objVideoResource.GetHeaderDetailsOnUserId(userId);
        if (objUserBusiness != null)
        {
            UserName = objUserBusiness.UserName;
            UserBussCity = objUserBusiness.Attribute1;
            UserBussState = objUserBusiness.Attribute2;
            
            IsCustomHeaderOn = Convert.ToBoolean(objUserBusiness.DisplayCustomHeader);
            
            HeaderBGColor = objUserBusiness.HeaderBGColor;
            if (string.IsNullOrEmpty(HeaderBGColor))
            {
                HeaderBGColor = WebConfig.DefaultCustomeHeaderBGColor;
            }

            HeaderLogo = objUserBusiness.HeaderLogo;

            if (IsAddressOn = Convert.ToBoolean(objUserBusiness.IsAddressOn))
            {
                BusinessAddress = GetBussAdd(objUserBusiness.BusinessAddress);
            }
            else
            {
                BusinessAddress = "";
            }
            if (IsPhoneOn = Convert.ToBoolean(objUserBusiness.IsPhoneNoOn))
            {
                Phone = objUserBusiness.Phone;
            }
            else
            {
                Phone = "";
            }
            if (IsWebSiteOn = Convert.ToBoolean(objUserBusiness.IsWebAddressOn))
            {
                WebSite = objUserBusiness.Website;
            }
            else
            {
                Website = "";
            }
            if (IsObituaryURLOn = objUserBusiness.IsObUrlLinkOn)
            {
                ObituaryURL = objUserBusiness.ObituaryLinkPage;
            }
            else
            {
                ObituaryURL = "";
            }

        }
    }
    public void GetUserIdByTributeId(int tributeId)
    {
        //LHK:exceptions are caught in the EventResource
        EventResource objEventRes = new EventResource();
        _userId = objEventRes.GetUserIdByTributeId(tributeId);
    }

    public void GetTributeIdOnTributeUrl(string tributeUrl, string ApplicationType)
    {
        EventResource objEventRes = new EventResource();
        _tributeId = objEventRes.GetTributeIdOnTributeUrl(tributeUrl, ApplicationType);
    }
    #endregion
}
