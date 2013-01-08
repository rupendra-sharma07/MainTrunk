///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Myhome.AdminMytributesPrivacy.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays and allows the user to modify the privacy settings for the selected tribute.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Configuration;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.MyHome.Views;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using System.Collections.Generic;
using System.Text;
using com.optimalpayments.webservices;
using System.Globalization;


public partial class MyHome_AdminMytributesPrivacy : PageBase, IAdminMytributesPrivacy
{
    protected static string _tributeName;
    protected string amount = string.Empty;
    private int _TribureId;
    private int _TributeIype;
    private string _tributeUrl;
    protected static string tributeType = string.Empty;
    int _userid;
    static int Accounttype = 0;
    static DateTime _StartedDate;
    private AdminMytributesPrivacyPresenter _presenter;
    protected string _userName = string.Empty;

    private int _packageid = 0;

    [CreateNew]
    public AdminMytributesPrivacyPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        //LHK: for secured links
        Session["SECURED_APP_SCRIPT_PATH"] = WebConfig.SecuredAppBaseDomain.ToString();

        // Added by Ashu on Oct 3, 2011 for rewrite URL 
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            lnkTributes.HRef = "moments.aspx";
            lnkTributesList.HRef = "moments.aspx";
            lnkTributes.InnerHtml = "My Websites";
            trbBlog.InnerHtml = "News from Your Moments";
            extendTrb.InnerHtml = "Upgrade/Extend Website";
            rdoYearlyAutoRenew.Text = @"I want this website to be renewed automatically on a yearly basis.";
            rdoNotifyBeforeRenew.Text = @"I do not want this website to be renewed automatically on a yearly basis, but I
                                                                    will be notified when the account is near to expiry.";
            lblFindTribute.Text = "Find a Website";
            txtSearchKeyword.Text = "Enter the name of a Tribute";
            btnSearchSubmit.AlternateText = "Search Websites";
            this.Page.Title = "My Websites";
        }
        else
        {
            lnkTributes.HRef = "tributes.aspx";
            lnkTributesList.HRef = "tributes.aspx";
        }
        
        if (!this.IsPostBack)
        {
            rfvTributeName.ErrorMessage = "Who is this "+WebConfig.ApplicationWordForInternalUse.ToString()+" for is a required field.";
            if (Session["APP_BASE_DOMAIN"]!=null)
                lnkAdvanceSearch.NavigateUrl = Session["APP_BASE_DOMAIN"] + "advancedsearch.aspx";
            StateManager stateManagerP = StateManager.Instance;
            string PageName = "AdminMytributesPrivacy";
            stateManagerP.Add(PortalEnums.SessionValueEnum.SearchPageName.ToString(), PageName, StateManager.State.Session);

            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {

                _userName = objSessionvalue.UserName;
                lbtnAddToAdministrators.Attributes.Add("onclick", "SetValidation();");
                lbtnDeleteSelectedAdmin.Attributes.Add("onclick", "SetValidationClear();");
                lbtnPrivacy21.Attributes.Add("onclick", "SetValidationClear();");
                lbtnAccountType23.Attributes.Add("onclick", "SetValidationClear();");
                lbtnTributeDetails24.Attributes.Add("onclick", "SetValidationClear();");
                lbtnDeleteTribute25.Attributes.Add("onclick", "SetValidationClear();");
                lbtnsaveTributeName.Attributes.Add("onclick", "SetDetailsValidation();");
                lbtnPrivacy41.Attributes.Add("onclick", "SetValidationClear();");
                lbtnAdministrators42.Attributes.Add("onclick", "SetValidationClear();");
                lbtnAccountType43.Attributes.Add("onclick", "SetValidationClear();");
                lbtnDeleteTribute45.Attributes.Add("onclick", "SetValidationClear();");
                rdoYearlyAutoRenew.Attributes.Add("onclick", "MakeAutoRenew();");
                rdoNotifyBeforeRenew.Attributes.Add("onclick", "MakeAutoRenew();");
                SetTextValues();
                _userid = objSessionvalue.UserId;
                Usernamelong.InnerText = objSessionvalue.UserName;
                spanLogout.InnerHtml = "<a href='Logout.aspx'>Log out</a>";
                _tributeName = (string)Session["lbtntributeName"];
                this._presenter.GetTributeByID();
                 if (Session["Sentby"] != null)
                {
                    if (Session["Sentby"].ToString().Equals("Manage") || Session["Sentby"].ToString().Equals("Expires"))
                        ManageTribute.ActiveViewIndex = 0;
                    else
                        ManageTribute.ActiveViewIndex = 2;
                }
                else if (Request.QueryString["sby"] != null)
                {
                    if (Request.QueryString["sby"].ToString().ToLower() == "manage")
                        ManageTribute.ActiveViewIndex = 0;
                    else
                        ManageTribute.ActiveViewIndex = 2;

                }


                SetAccountInfo();

                if (Session["lblTypeDescription"].ToString() == "Video")
                {
                    lbtnAdministrators12.Visible = false;
                    lbtnAccountType13.Visible = false;
                    lbtnTributeDetails14.Visible = false;
                 
                }
              
                if (Session["myfavourite"] != null)
                {
                    limyfavourite.Visible = bool.Parse(Session["myfavourite"].ToString());
                }
            }
            else
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }
        }
        // Set the controls value
        SetControlsValue();
    }
    protected void lbtnmytribute_Click(object sender, EventArgs e)
    {

        //LHK:(5:01 PM 3/15/2011) video tribute home page link on privacysetting page.
        if (lbltributetype.Text.ToLower() == "video")
        {
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                Response.Redirect(Session["APP_PATH"].ToString() + "video/videotribute.aspx?tributeId=" + TributeId.ToString());
            }
            else
            {
                //Use this line for server and comment the line written above
                Response.Redirect("http://video." + WebConfig.TopLevelDomain + "/video/videotribute.aspx?tributeId=" + TributeId.ToString());
            }
        }

        else
        {
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                Response.Redirect(Session["APP_PATH"].ToString() + (string)Session["lblTributeUrl"] + "/");
            }
            else
            {
                //Use this line for server and comment the line written above
                Response.Redirect("http://" + lbltributetype.Text.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + (string)Session["lblTributeUrl"] + "/");
            }
        }
    }
    private void SetTextValues()
    {
        if (Request.QueryString["tributeId"] != null)
        {
            this._presenter.getTributeDetails(int.Parse(Request.QueryString["tributeId"].ToString()));
            Session["TributeId"] = int.Parse(Request.QueryString["tributeId"].ToString());
        }
        else
        {
            lbtnmytribute.Text = (string)Session["lbtntributeName"];
            lbltributetype.Text = (string)Session["lblTypeDescription"];
            lblCreateddate.Text = Session["lblACreated"].ToString();
            lbtnexpiresdate.Text = Session["lbtnExpires"].ToString();
            lblVisits.Text = Session["txtVisits"].ToString();
            CheckBoxEmailErt.Checked = (bool)Session["cbxEmailAlerts"];
        }
    }
    protected void lbtnexpiresdate_Click(object sender, EventArgs e)
    {
        ManageTribute.ActiveViewIndex = 2;
    }
    protected void CheckBoxEmailErt_CheckedChanged(object sender, EventArgs e)
    {

        try
        {
            this._presenter.UpdateEmailAlerts(CheckBoxEmailErt.Checked);
            setmessage("<h2>Email alert updated</h2><P>Email alert is updated successfully.</P>", 2);
        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + "</li></ul>", 1);
        }

    }
    protected void lbtnPrivacy11_Click(object sender, EventArgs e)
    {
        ManageTribute.ActiveViewIndex = 0;
    }
    protected void lbtnDeleteTribute15_Click(object sender, EventArgs e)
    {
        setDefault();
        ManageTribute.ActiveViewIndex = 4;
        if (Session["lblTypeDescription"].ToString() == "Video")
        {
            lbtnAdministrators52.Visible = false;
            lbtnAccountType53.Visible = false;
            lbtnTributeDetails54.Visible = false;

        }
    }
    protected void lbtnPrivacy51_Click(object sender, EventArgs e)
    {
        setDefault();
        ManageTribute.ActiveViewIndex = 0;
        if (Session["lblTypeDescription"].ToString() == "Video")
        {
            lbtnAdministrators12.Visible = false;
            lbtnAccountType13.Visible = false;
            lbtnTributeDetails14.Visible = false;

        }
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        setDefault();
        ManageTribute.ActiveViewIndex = 4;
    }
    protected void lbtnTributeDetails14_Click(object sender, EventArgs e)
    {
        setDefault();
        ManageTribute.ActiveViewIndex = 3;
    }
    protected void lbtnTributeDetails54_Click(object sender, EventArgs e)
    {
        setDefault();
        ManageTribute.ActiveViewIndex = 3;
        if (Session["lblTypeDescription"].ToString() == "Video")
        {
            lbtnAdministrators12.Visible = false;
            lbtnAccountType13.Visible = false;
            lbtnTributeDetails14.Visible = false;

        }
    }
    protected void lbtnPrivacy41_Click(object sender, EventArgs e)
    {
        setDefault();
        ManageTribute.ActiveViewIndex = 0;
        if (Session["lblTypeDescription"].ToString() == "Video")
        {
            lbtnAdministrators12.Visible = false;
            lbtnAccountType13.Visible = false;
            lbtnTributeDetails14.Visible = false;

        }
    }
    protected void lbtnDeleteTribute45_Click(object sender, EventArgs e)
    {
        ManageTribute.ActiveViewIndex = 4;

        if (Session["lblTypeDescription"].ToString() == "Video")
        {
            lbtnAdministrators52.Visible = false;
            lbtnAccountType53.Visible = false;
            lbtnTributeDetails54.Visible = false;

        }
    }
    protected void lbtnAdministrators12_Click(object sender, EventArgs e)
    {
        setDefault();
        _presenter.GetTributeAdministrators();
        ManageTribute.ActiveViewIndex = 1;
       
    }
    protected void lbtnAccountType13_Click(object sender, EventArgs e)
    {
        setDefault();
        ManageTribute.ActiveViewIndex = 2;
        if (Session["lblTypeDescription"].ToString() == "Video")
        {
            lbtnAdministrators12.Visible = false;
            lbtnAccountType13.Visible = false;
            lbtnTributeDetails14.Visible = false;

        }
    }
    protected void lbtnAdministrators42_Click(object sender, EventArgs e)
    {
        _presenter.GetTributeAdministrators();
        ManageTribute.ActiveViewIndex = 1;
    }
    protected void lbtnAdministrators52_Click(object sender, EventArgs e)
    {
        setDefault();
        _presenter.GetTributeAdministrators();
        ManageTribute.ActiveViewIndex = 1;
    }
    protected void lbtnPrivacy21_Click(object sender, EventArgs e)
    {
        setDefault();
        ManageTribute.ActiveViewIndex = 0;
    }
    protected void lbtnTributeDetails24_Click(object sender, EventArgs e)
    {
        setDefault();
        ManageTribute.ActiveViewIndex = 3;
    }
    protected void lbtnDeleteTribute25_Click(object sender, EventArgs e)
    {
        setDefault();
        ManageTribute.ActiveViewIndex = 4;
        if (Session["lblTypeDescription"].ToString() == "Video")
        {
            lbtnAdministrators52.Visible = false;
            lbtnAccountType53.Visible = false;
            lbtnTributeDetails54.Visible = false;

        }
    }
    protected void lbtnPrivacy31_Click(object sender, EventArgs e)
    {
        setDefault();
        ManageTribute.ActiveViewIndex = 0;
    }
    protected void Administrators31_Click(object sender, EventArgs e)
    {
        setDefault();
        _presenter.GetTributeAdministrators();
        ManageTribute.ActiveViewIndex = 1;
    }
    protected void lbtnTributeDetails34_Click(object sender, EventArgs e)
    {
        setDefault();
        ManageTribute.ActiveViewIndex = 3;
    }
    protected void lbtnDeleteTribute35_Click(object sender, EventArgs e)
    {
        setDefault();
        ManageTribute.ActiveViewIndex = 4;
        if (Session["lblTypeDescription"].ToString() == "Video")
        {
            lbtnAdministrators52.Visible = false;
            lbtnAccountType53.Visible = false;
            lbtnTributeDetails54.Visible = false;

        }
    }
    protected void lbtnAccountType53_Click(object sender, EventArgs e)
    {
        setDefault();
        ManageTribute.ActiveViewIndex = 2;
    }
    protected void lbtnAccountType43_Click(object sender, EventArgs e)
    {
        ManageTribute.ActiveViewIndex = 2;
    }
  
    private void SetAccountInfo()
    {
        // update account types for phase 1  27/Nov/2012 by ud  changes Tribute (Never) to Memorial Tribute (Lifetime), Photo Tribute (Never) to Premium Obituary (Lifetime)
        if ((lbtnexpiresdate.Text.Equals("Celebrate (Never)")) || (lbtnexpiresdate.Text.Equals("Memorial Tribute (Lifetime)")) || (lbtnexpiresdate.Text.Equals("Premium Obituary (Lifetime)")) || (lbtnexpiresdate.Text.Equals("Video Tribute (Never)")))//if (lbtnexpiresdate.Text.Equals("Never"))
        {
            if (lbtnexpiresdate.Text.Equals("Celebrate (Never)"))
                AccounttypeInfo.InnerText = "This tribute currently has a Celebrate (Lifetime) account, which will never expire. (Lucky you!)";
            else if (lbtnexpiresdate.Text.Equals("Memorial Tribute (Lifetime)"))
                AccounttypeInfo.InnerText = "This tribute currently has a Memorial Tribute (Lifetime) account, which will never expire. (Lucky you!)";
            else if (lbtnexpiresdate.Text.Equals("Premium Obituary (Lifetime)"))
                AccounttypeInfo.InnerText = "This tribute currently has a Premium Obituary (Lifetime) account, which will never expire. (Lucky you!)";
            else if (lbtnexpiresdate.Text.Equals("Video Tribute (Never)"))
                AccounttypeInfo.InnerText = "This tribute currently has a Video Tribute (Lifetime) account, which will never expire. (Lucky you!)";
            else 
            _presenter.TriputePackageInfo();
            if (this._presenter.View.IsSponsor == true)
            {
                _presenter.GetUserDetails();
                string[] _Setstartdate = DateTime.Parse(Session["lblRenewaldate"].ToString()).ToString("MM/dd/yyyy").Split('/');
                DateTime date1 = new DateTime(int.Parse(_Setstartdate[2]), int.Parse(_Setstartdate[0]), int.Parse(_Setstartdate[1]));

                AccountTypeDesc.InnerHtml = "Your account was upgraded (or extended) to Lifetime by " + this._presenter.View.Sponsorname + " on " + date1.ToString("MMMM dd, yyyy") + ".";
                AccountTypeDesc.Visible = true;

            }
            else
                AccountTypeDesc.Visible = false;

            // showaccounttype.Visible = false;
            tblccounttype.Visible = true;
            if ((lbtnexpiresdate.Text.Equals("Celebrate (Never)")) || (lbtnexpiresdate.Text.Equals("Memorial Tribute (Lifetime)")) ||(lbtnexpiresdate.Text.Equals("Video Tribute (Never)")))
            {
                tblccounttype.Visible = false;
            }


        } // update account types for phase 1  27/Nov/2012 by ud  added Obituary (Lifetime), Tribute (Expired) to Memorial Tribute (Expired), Photo Tribute (Expired) to Premium Obituary (Expired)
        else if ((lbtnexpiresdate.Text.Equals("Announce (Free)")) || (lbtnexpiresdate.Text.Equals("Obituary (Lifetime)")) || (lbtnexpiresdate.Text.Equals("Memorial Tribute (Expired)")) || (lbtnexpiresdate.Text.Equals("Premium Obituary (Expired)")) || (lbtnexpiresdate.Text.Equals("Video Tribute (Expired)")))//else if (lbtnexpiresdate.Text.Equals("Expired!"))
        {
            if (lbtnexpiresdate.Text.Equals("Announce (Free)"))
                AccounttypeInfo.InnerText = "This tribute currently has an Announce (Free) account. Click “Upgrade Account” to upgrade your tribute.";
            else if (lbtnexpiresdate.Text.Equals("Obituary (Lifetime)"))
                AccounttypeInfo.InnerText = "This tribute currently has an Obituary (Lifetime) account. Click “Upgrade Account” to upgrade your tribute.";
            else if (lbtnexpiresdate.Text.Equals("Memorial Tribute (Expired)"))
                AccounttypeInfo.InnerText = "This tribute currently has an Memorial Tribute (Expired) account. Click “Upgrade Account” to upgrade your tribute.";
            else if (lbtnexpiresdate.Text.Equals("Premium Obituary (Expired)"))
                AccounttypeInfo.InnerText = "This tribute currently has an Premium Obituary(Expired) account. Click “Upgrade Account” to upgrade your tribute.";
            else if (lbtnexpiresdate.Text.Equals("Video Tribute (Expired)"))
                AccounttypeInfo.InnerText = "This tribute currently has an Video Tribute (Expired) account. Click “Upgrade Account” to upgrade your tribute.";

            tblccounttype.Visible = true;
            Accounttype = 1;
            AccountTypeDesc.Visible = false;
        }
        else
        {
            DateTime date1 = DateTime.Now;
            DateTime.TryParseExact(Session["lblRenewaldate"].ToString(), "MM'-'dd'-'yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date1);

            //end date
            string[] _Setenddate = null;
            string datePart = string.Empty;
            if (lbtnexpiresdate.Text.Contains("Celebrate"))
            {
                if (lbtnexpiresdate.Text.IndexOf("Celebrate") >= 0)
                {
                    if (lbtnexpiresdate.Text.IndexOf("Trial") >= 0)
                    {
                        datePart = lbtnexpiresdate.Text.Substring(17, 10);
                        _Setenddate = datePart.Split('/'); //lbtnexpiresdate.Text.Split('/');
                    }
                    else
                    {
                        datePart = lbtnexpiresdate.Text.Substring(11, 10);
                        _Setenddate = datePart.Split('/'); //lbtnexpiresdate.Text.Split('/');
                    }
                }
                DateTime date2 = new DateTime(int.Parse(_Setenddate[2]), int.Parse(_Setenddate[0]), int.Parse(_Setenddate[1]));
                TimeSpan diff = date2.Subtract(date1);
                int days = diff.Days;
                if (days <= 31)
                {
                    //Phase4 Account Type UI changes
                    //AccounttypeInfo.InnerText = "This tribute currently has a 30 Day Trial account, which expires on " + date2.ToString("MMMM dd, yyyy") + ".";
                    if (lbtnexpiresdate.Text.Contains("Tribute Free Trial"))
                        AccounttypeInfo.InnerText = "This tribute currently has a Tribute (Free Trial) account, which will expire on " + date2.ToString("MMMM dd, yyyy") + ". Click “Upgrade Account” to upgrade your tribute.";
                    tblccounttype.Visible = true;
                    Accounttype = 1;
                    AccountTypeDesc.Visible = false;
                }
                else
                {
                    SetInfofor1year(date1, date2);
                }
            }

            else if (lbtnexpiresdate.Text.Contains("Premium Obituary"))
            {
                if (lbtnexpiresdate.Text.IndexOf("Premium Obituary") >= 0)
                {
                    datePart = lbtnexpiresdate.Text.Substring(18, 10);
                    _Setenddate = datePart.Split('/'); //lbtnexpiresdate.Text.Split('/');
                }
                DateTime date2 = new DateTime(int.Parse(_Setenddate[2]), int.Parse(_Setenddate[0]), int.Parse(_Setenddate[1]));
                SetInfofor1year(date1, date2);
            }
            else if (lbtnexpiresdate.Text.Contains("Tribute Free"))
            {

                if (lbtnexpiresdate.Text.IndexOf("Trial") >= 0)
                {
                    datePart = lbtnexpiresdate.Text.Substring(20, 10);
                    _Setenddate = datePart.Split('/'); //lbtnexpiresdate.Text.Split('/');
                }

                DateTime date2 = new DateTime(int.Parse(_Setenddate[2]), int.Parse(_Setenddate[0]), int.Parse(_Setenddate[1]));
                TimeSpan diff = date2.Subtract(date1);
                int days = diff.Days;
                if (days <= 31)
                {
                    //Phase4 Account Type UI changes
                    if (lbtnexpiresdate.Text.Contains("Tribute Free Trial"))
                        AccounttypeInfo.InnerText = "This tribute currently has a Tribute (Free Trial) account, which will expire on " + date2.ToString("MMMM dd, yyyy") + ". Click “Upgrade Account” to upgrade your tribute.";
                    tblccounttype.Visible = true;
                    Accounttype = 3;
                    AccountTypeDesc.Visible = false;
                }
                else
                {
                    SetInfofor1year(date1, date2);
                }
            }
            else if (lbtnexpiresdate.Text.Contains("Video Tribute"))
            {
                if (lbtnexpiresdate.Text.IndexOf("Tribute") >= 0)
                {
                    datePart = lbtnexpiresdate.Text.Substring(15, 10);
                    _Setenddate = datePart.Split('/'); //lbtnexpiresdate.Text.Split('/');
                }
                DateTime date2 = new DateTime(int.Parse(_Setenddate[2]), int.Parse(_Setenddate[0]), int.Parse(_Setenddate[1]));
                SetInfofor1year(date1, date2);
            }
            else if (lbtnexpiresdate.Text.Contains("Tribute"))
            {
                if (lbtnexpiresdate.Text.IndexOf("Tribute") >= 0)
                {
                    datePart = lbtnexpiresdate.Text.Substring(18, 10);
                    _Setenddate = datePart.Split('/'); //lbtnexpiresdate.Text.Split('/');
                }
                DateTime date2 = new DateTime(int.Parse(_Setenddate[2]), int.Parse(_Setenddate[0]), int.Parse(_Setenddate[1]));
                SetInfofor1year(date1, date2);
            }
            
        }

    }

    private void SetInfofor1year(DateTime date1, DateTime date2)
    {
        _presenter.TriputePackageInfo();
        if (this._presenter.View.IsAutomaticRenew.Equals(true))
        {
            AccounttypeInfo.Visible = false;
            AccountTypeDesc.Visible = false;
            case3.Visible = true;
            liautorenewal.Visible = true;
            case3headline.InnerHtml = "This tribute will automatically renew for 1 Year on " + date2.ToString("MMMM dd, yyyy") + ".";
            if (this._presenter.View.SponserId != 0)
            {
                _presenter.GetUserDetails();
                case3desc.InnerHtml = "Your account was upgraded (or extended) for 1 Year by " + this._presenter.View.Sponsorname + " on " + date1.ToString("MMMM dd, yyyy") + ".";
            }
            tblccounttype.Visible = true;
            Accounttype = this._presenter.View.PackageId;
        }
        else if (this._presenter.View.IsSponsor.Equals(true))
        {
            case3.Visible = false;
            if (this._presenter.View.PackageId == 2)
            case2headline.InnerHtml = "This tribute currently has a Celebrate (1 Year) account, which will expire on " + date2.ToString("MMMM dd, yyyy") + ". Click “Upgrade Account” to upgrade your tribute.";
            else if (this._presenter.View.PackageId == 5)
            case2headline.InnerHtml = "This tribute currently has a Tribute (1 Year) account, which will expire on " + date2.ToString("MMMM dd, yyyy") + ". Click “Upgrade Account” to upgrade your tribute.";
            else if (this._presenter.View.PackageId == 7)
            case2headline.InnerHtml = "This tribute currently has a Photo Tribute (1 Year) account, which will expire on " + date2.ToString("MMMM dd, yyyy") + ". Click “Upgrade Account” to upgrade your tribute.";

            if (_IsSponserHide.Equals(true))
            {
                _presenter.GetUserDetails();
                if (this._presenter.View.SponserId != 0)
                    case2desc.InnerHtml = "Your account was upgraded (or extended) for 1 Year by " + this._presenter.View.Sponsorname + " on " + date1.ToString("MMMM dd, yyyy") + "."; //by <a href='javascript:void(0);' onclick=\"UserProfileModal_1('" + this._presenter.View.SponserId + "');\">" + this._presenter.View.Sponsorname + "</a> on " + date1.ToString("MMMM dd, yyyy") + ".";
                else
                    case2desc.InnerHtml = "Your account was upgraded (or extended) for 1 Year by an anonymous sponsor, on " + date1.ToString("MMMM dd, yyyy") + ".";
            }
            else
                case2desc.InnerHtml = "Your account was upgraded (or extended) by an anonymous sponsor, on " + date1.ToString("MMMM dd, yyyy") + ".";
            case2.Visible = true;
            tblccounttype.Visible = true;
            Accounttype = this._presenter.View.PackageId;
            AccounttypeInfo.Visible = false;
            AccountTypeDesc.Visible = false;
        }
        else
        {
            AccounttypeInfo.InnerText = "This tribute currently has a 1 Year account, which expires on " + date2.ToString("MMMM dd, yyyy") + ".";
            tblccounttype.Visible = true;
            Accounttype = this._presenter.View.PackageId;
            AccountTypeDesc.Visible = false;
        }
    }



    protected void lbtnSavePrivacyChanges_Click(object sender, EventArgs e)
    {
        setDefault();
        try
        {
            this._presenter.UpdateTributePrivacy();
            setmessage("<h2>Privacy Updated</h2><P>Tribute privacy is updated successfully.</P>", 2);
        }
        catch (Exception ex)
        {

            setmessage("<h2>Oops - there is a problem.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + "</li></ul>", 1);
        }
    }
    protected void lbtnDeleteTribute_Click(object sender, EventArgs e)
    {
        setDefault();
        try
        {
            this._presenter.DeleteTribute();
            setmessage("<h2>Tribute deleted!</h2><P>Tribute is deleted successfully.</P>", 2);
            //remove tribute session
            Session["lbtntributeName"] = null;
            Session["lblTypeDescription"] = null;
            Session["lblACreated"] = null;
            Session["lbtnExpires"] = null;
            Session["txtVisits"] = null;
            Session["cbxEmailAlerts"] = null;


            // Added by Ashu on Oct 3, 2011 for rewrite URL 
            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
            {
                Response.Redirect("moments.aspx", false);
            }
            else
            {
                Response.Redirect("tributes.aspx", false);
            }
        }
        catch (Exception ex)
        {

            setmessage("<h2>Oops - there is a problem.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + "</li></ul>", 1);
        }
    }
    protected void lbtnsaveTributeName_Click(object sender, EventArgs e)
    {
        setDefault();
        try
        {
            this._presenter.UpdateTributeName();
            if (divDonation.Visible == true)
            {
                this._presenter.UpdateDonationDetails();
                _presenter.GetTributeByID();
            }
            lbtnmytribute.Text = txtTributeName.Text;
            _tributeName = txtTributeName.Text;
            setmessage("<h2>Tribute Details Updated!</h2><P>Tribute details are updated successfully.</P>", 2);
        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + "</li></ul>", 1);
        }
    }
    protected void lbtnConfirm_Click(object sender, EventArgs e)
    {
        setDefault();
        try
        {
            this._presenter.UpdateAutoRenew();
            liautorenewal.Visible = false;
            case3headline.InnerHtml = "This tribute currently has a 1 Year account.";
            setmessage("<h2>Tribute Auto Renewal Updated!</h2><P>Tribute auto renewal is updated successfully.</P>", 2);
        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + "</li></ul>", 1);
        }
    }
    protected void lbtnUpgradeAccount_Click(object sender, EventArgs e)
    {

        this._presenter.GetCCCountryList();
        this._presenter.OnStateLoad();
        StateManager stateTribure = StateManager.Instance;
        Tributes objTribute = (Tributes)stateTribure.Get("TributeSession", StateManager.State.Session);
        if (!Equals(objTribute, null))
        {
            _TribureId = objTribute.TributeId;
            _tributeUrl = objTribute.TributeUrl;
            tributeType = objTribute.TypeDescription;
            Session["TributeURL"] = _tributeUrl;
        }
        // Added by Ashu on Oct 3, 2011 for rewrite URL 
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
            Response.Redirect(Session["APP_BASE_DOMAIN"] + "MomentsSponsor.aspx?TributeURL=" + Session["TributeURL"] + "&TributeType=" + tributeType + "&pageName=" + "AdminMytributesPrivacy");
        else
            Response.Redirect(Session["APP_BASE_DOMAIN"] + "TributeSponsor.aspx?TributeURL=" + Session["TributeURL"] + "&TributeType=" + tributeType + "&pageName=" + "AdminMytributesPrivacy");

    }
    protected void ddlCCCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.OnStateLoad();
    }
    protected void lbtnPay_Click(object sender, EventArgs e)
    {

        String UserMail = string.Empty;
        string Firstname = string.Empty;
        string LastName = string.Empty;

        try
        {
            //check for credit card lengths
            if (Request.Form["rdoCCType"] == "Visa" || Request.Form["rdoCCType"] == "MasterCard" || Request.Form["rdoCCType"] == "Discover")
            {
                if (txtCCNumber.Text.Length != 16 && txtCCVerification.Text.Length != 3)
                {
                    ShowMessage(VSAccountTrpe.HeaderText, "Transaction Failed", true);
                    return;

                }
            }
            else if (Request.Form["rdoCCType"] == "Amex" && txtCCNumber.Text.Length != 15 && txtCCVerification.Text.Length != 4)
            {
                ShowMessage(VSAccountTrpe.HeaderText, "Transaction Failed", true);
                return;

            }
            //Remove Commented code for Payment Gateway :Amit :2/5/8
            StateManager statemail = StateManager.Instance;
            SessionValue objSessionmail = (SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionmail != null)
            {
                Firstname = objSessionmail.FirstName;
                LastName = objSessionmail.LastName;
                UserMail = objSessionmail.UserEmail;
            }

            PaymentGateWay objPay = new PaymentGateWay();
            bool _transaction = true;
            if (_transaction)
            {
                this._presenter.InsertCCDetails();

                setmessage("<h2>Account Updated</h2><P>Tribute account is updated successfully.</P>", 2);
                SetBlank();
                SetAccountInfo();
            }
            else
            {
                setmessage("<h2>Oops - there is a problem with transaction.</h2> <h3>Please correct the errors below:</h3><ul><li>Transaction faild.</li></ul>", 1);
            }
        }
        catch //(Exception ex) //by ud 
        { }
    }
    private void SetBlank()
    {
        ArrayList all = Common.GetControls(this.Controls);
        for (int i = 0; i <= all.Count - 1; i++)
        {
            Control ctl = (Control)all[i];
            if (ctl.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
            {
                if (!((TextBox)ctl).ID.Equals("txtTributeName"))
                {
                    ((TextBox)ctl).Text = string.Empty;
                }
            }
            rdoYearlyAutoRenew.Checked = false;
            rdoNotifyBeforeRenew.Checked = true;
            chkSaveBillingInfo.Checked = false;
            ddlCCMonth.SelectedIndex = 0;

        }

    }
    private void setmessage(string msg, int type)
    {

        if (type == 1)
            errormsg.Attributes.Add("class", "yt-Error");
        else
            errormsg.Attributes.Add("class", "yt-Notice");

        errormsg.InnerHtml = msg;
        errormsg.Visible = true;
    }
    private void setDefault()
    {
        errormsg.InnerHtml = "";
        errormsg.Visible = false;
    }
    protected void lbtnDeleteAdministrator_Click(object sender, EventArgs e)
    {
        try
        {
            string AdminList = GetSelectedValue();
            this._presenter.DeleteTributeAdminis(AdminList);
            _presenter.GetTributeAdministrators();
            setmessage("<h2>Admin(s) deleted</h2><P>Tribute admins deleted successfully.</P>", 2);
        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem with tribute deletion.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + "</li></ul>", 1);
        }
    }
    private string GetSelectedValue()
    {
        string _selectedValues = String.Empty;
        for (int i = 0; i < cblstAdminis.Items.Count; i++)
        {
            if (cblstAdminis.Items[i].Selected)
            {
                if (_selectedValues.Equals(string.Empty))
                {

                    _selectedValues = cblstAdminis.Items[i].Value;
                }
                else
                {
                    _selectedValues += "," + cblstAdminis.Items[i].Value;
                }
            }
        }
        return _selectedValues;

    }

    /// <summary>
    /// This method will return the selected tribute type
    /// </summary>
    /// <returns>A string which contain the tribute type</returns>
    private string GetTributeType()
    {
        string tributeType = "";

        if (rdoSearch_All.Checked == true)
        {
            tributeType = lblSearch_All.InnerText;
        }
        else if (rdoSearch_Anniversary.Checked == true)
        {
            tributeType = lblSearch_Anniversary.InnerText;
        }
        else if (rdoSearch_Birthday.Checked == true)
        {
            tributeType = lblSearch_Birthday.InnerText;
        }
        else if (rdoSearch_Graduation.Checked == true)
        {
            tributeType = lblSearch_Graduation.InnerText;
        }
        else if (rdoSearch_Memorial.Checked == true)
        {
            tributeType = lblSearch_Memorial.InnerText;
        }
        else if (rdoSearch_NewBaby.Checked == true)
        {
            tributeType = lblSearch_NewBaby.InnerText;
        }
        else if (rdoSearch_Wedding.Checked == true)
        {
            tributeType = lblSearch_Wedding.InnerText;
        }

        return tributeType;
    }
    // Added By Parul
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            // Create SearchTribute object
            SearchTribute objSearchTribute = new SearchTribute();

            // Assign the search parameter to this object
            objSearchTribute.TributeType = GetTributeType();
            objSearchTribute.SearchString = txtSearchKeyword.Text.ToString();
            objSearchTribute.SearchType = PortalEnums.SearchEnum.Basic.ToString();
            objSearchTribute.SortOrder = "DESC";

            // Create StateManager object and add search paramter in the session
            StateManager objStateMgr = StateManager.Instance;
            objStateMgr.Add(PortalEnums.SearchEnum.Search.ToString(), objSearchTribute, StateManager.State.Session);

            // Redirect to the Search Result page
            Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.SearchResult.ToString()));
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }
    protected void lbtnAddToAdministrators_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAdminEmail1.Text.Length > 0)
            {
                string UserName = string.Empty;
                setDefault();
                StateManager stateManager = StateManager.Instance;
                SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
                if (objSessionvalue != null)
                {
                    if (objSessionvalue.UserType == 1)
                    {
                        if (objSessionvalue.FirstName == string.Empty)
                            UserName = objSessionvalue.UserName;
                        else
                            UserName = objSessionvalue.FirstName + " " + objSessionvalue.LastName;
                    }
                    else
                    {
                        UserName = objSessionvalue.FirstName;
                    }

                }
                this._presenter.SendMailtoAdmins(txtAdminEmail1.Text, lbtnmytribute.Text, Session["lblTypeDescription"].ToString(), UserName);
                txtAdminEmail1.Text = string.Empty;
                setmessage("<h2>Invite Admin</h2><P>Tribute admin is invited successfully.</P>", 2);
            }
            else
            {
                setmessage("<h2>Oops - there is a problem.</h2> <h3>Please correct the errors below:</h3><ul><li>Enter email address before inviting.</li></ul>", 1);
            }
        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + "</li></ul>", 1);
        }
    }
    /// <summary>
    /// This Function will set the value of the control and error messages from the resource File
    /// Added By Parul
    /// </summary>
    private void SetControlsValue()
    {
        try
        {
            //Text for labels from the resource file
            lblFindTribute.Text = ResourceText.GetString("lblFindTribute_MP");                      // Find a Tribute
            lblSearchFor.InnerText = ResourceText.GetString("lblSearchFor_MP");                     // Search for:
            lblSearch_All.InnerText = ResourceText.GetString("lblSearch_All_MP");                   // All Tributes
            lblSearch_Anniversary.InnerText = ResourceText.GetString("lblSearch_Anniversary_MP");   // Anniversary Tributes
            lblSearch_Birthday.InnerText = ResourceText.GetString("lblSearch_Birthday_MP");         // Birthday Tribute
            lblSearch_Graduation.InnerText = ResourceText.GetString("lblSearch_Graduation_MP");     // Graduation Tributes
            lblSearch_Memorial.InnerText = ResourceText.GetString("lblSearch_Memorial_MP");         // Memorial Tributes
            lblSearch_NewBaby.InnerText = ResourceText.GetString("lblSearch_NewBaby_MP");           // New Baby Tributes
            lblSearch_Wedding.InnerText = ResourceText.GetString("lblSearch_Wedding_MP");           // Wedding Tributes
            lnkAdvanceSearch.Text = ResourceText.GetString("lnkAdvanceSearch_MP");                  // Advanced Search
            lnkClose.InnerText = ResourceText.GetString("lnkClose_MP");                             // Close

            txtSearchKeyword.Attributes.Add("onclick", "this.select();");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnValidateCoupon_Click(object sender, EventArgs e)
    {
        HtmlInputRadioButton Lifetime = (HtmlInputRadioButton)this.FindControl("rdoMembershipLifetime");
        HtmlInputRadioButton Yearly = (HtmlInputRadioButton)this.FindControl("rdoMembershipYearly");
        int couponType = 0;
        if (Lifetime.Checked)
            couponType = 3;
        else
            couponType = 2;
        int availability = _presenter.GetCouponAvailable(txtCouponCode.Text, couponType);

        if (availability == 1)
            SetCouponAvailableStatus();
        else
            SetCouponUnAvailableStatus();
    }
    private void SetCouponUnAvailableStatus()
    {
        int Couponamount = 0;
        HtmlInputRadioButton Lifetime = (HtmlInputRadioButton)this.FindControl("rdoMembershipLifetime");
        HtmlInputRadioButton Yearly = (HtmlInputRadioButton)this.FindControl("rdoMembershipYearly");
        if (Lifetime.Checked)
        {
            amount = WebConfig.LifeTimeAmount;
            Couponamount = Convert.ToInt32(amount.Substring(1, amount.Length - 1));
        }
        else
        {
            amount = WebConfig.OneyearAmount;
            Couponamount = Convert.ToInt32(amount.Substring(1, amount.Length - 1));
        }

        StringBuilder Script = new StringBuilder();
        Script.Append("var notice = $('spanCoupon');");
        Script.Append("if(notice)");
        Script.Append("{");
        Script.Append("notice.innerHTML='';");
        Script.Append("notice.addClass('couponNotice-Unavailable');");
        Script.Append("notice.innerHTML = 'This is not a valid coupon code.';");
        Script.Append("$('BillingTotal').innerHTML = '" + "$" + amount.ToString() + "';");
        Script.Append("}");
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", Script.ToString(), true);

    }
    private void SetCouponAvailableStatus()
    {

        int Couponamount = 0;
        HtmlInputRadioButton Lifetime = (HtmlInputRadioButton)this.FindControl("rdoMembershipLifetime");
        HtmlInputRadioButton Yearly = (HtmlInputRadioButton)this.FindControl("rdoMembershipYearly");
        if (Lifetime.Checked)
        {
            amount = WebConfig.LifeTimeAmount;
            Couponamount = Convert.ToInt32(amount.Substring(1, amount.Length - 1));
        }
        else
        {
            amount = WebConfig.OneyearAmount;
            Couponamount = Convert.ToInt32(amount.Substring(1, amount.Length - 1));
        }
        StringBuilder Script = new StringBuilder();
        Script.Append("var notice = $('spanCoupon');");
        Script.Append("if(notice)");
        Script.Append("{");
        Script.Append("notice.innerHTML='';");
        Script.Append("notice.addClass('couponNotice-Available');");
        Script.Append("notice.innerHTML = 'Coupon is valid';");
        if (this._presenter.View.IsPercentage == false)
        {
            Couponamount = Couponamount - int.Parse(this._presenter.View.Denomination);
        }
        else
        {
            Couponamount = Couponamount - ((int.Parse(this._presenter.View.Denomination) * Couponamount) / 100);
        }
        if (Couponamount < 0)
            Couponamount = 0;
        Script.Append("$('BillingTotal').innerHTML = '" + "$" + Couponamount.ToString() + "';");
        Script.Append("}");
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", Script.ToString(), true);

    }

    #region IAdminMytributesPrivacy Members


    bool _IsPercentage = false;
    public bool IsPercentage
    {
        get
        {
            return _IsPercentage;
        }
        set
        {
            _IsPercentage = value;
        }
    }

    string _Denom = string.Empty;
    public string Denomination
    {
        get
        {
            return _Denom;
        }
        set
        {
            _Denom = value;
        }
    }



    public decimal AmountPaid
    {
        get
        {
            return Decimal.Parse(BillingTotal.InnerHtml.ToString().Replace("$", ""));
        }
    }
    /// <summary>
    /// This function used to get selected card Type
    /// </summary>
    /// <returns></returns>
    private CardTypeV1 SelectCreditCardType()
    {
        CardTypeV1 CcType = CardTypeV1.VI;

        if (Request.Form["rdoCCType"] == "Visa")
        {
            CcType = CardTypeV1.VI;
        }
        if (Request.Form["rdoCCType"] == "MasterCard")
        {
            CcType = CardTypeV1.MC;
        }
        if (Request.Form["rdoCCType"] == "Discover")
        {
            CcType = CardTypeV1.DC;
        }
        if (Request.Form["rdoCCType"] == "Amex")
        {
            CcType = CardTypeV1.AM;
        }
        return CcType;
    }



    //used to set the Donation details
    public Donation DonationCharity
    {
        set
        {
            //set the charity name in case charity Name and URL to epartners - both - exist
            if (value.CharityName != null && value.CharityName.Length != 0 && value.DonationUrl != null && value.DonationUrl.Length != 0)
            {
                txtCharityName.Text = value.CharityName;
                txtDonationURL.Text = value.DonationUrl;
                chkDonation.Checked = false;
                divDonation.Visible = true;
            }
            //set the empty text as charity name in case only URL to epartners exist
            else if (value.DonationUrl != null && value.DonationUrl.Length != 0)
            {
                txtCharityName.Text = string.Empty;
                txtDonationURL.Text = value.DonationUrl;
                chkDonation.Checked = false;
                divDonation.Visible = true;
            }
            //hide the donation box completely in case charity name and url to epartners do not exist
            else
            {
                chkDonation.Checked = true;
                divDonation.Visible = false;
            }
        }
        get
        {
            Donation objDonation = new Donation();
            objDonation.TributeID = TributeId;
            objDonation.ModifiedBy = UserId;
            objDonation.TributeName = TributeName;
            objDonation.CharityName = txtCharityName.Text.Trim();
            objDonation.DonationUrl = txtDonationURL.Text.Trim();
            return objDonation;
        }
    }

    public bool IsDonation
    {
        get
        {
            return !chkDonation.Checked;
        }
    }

    public int UserId
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
                _userid = objSessionvalue.UserId;
            return _userid;
        }
    }

    public int TributeId
    {
        get
        {
            return (int)Session["TributeId"];
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public IList<UserInfo> TributeAdministrators
    {
        set
        {
            if (value != null)
            {
                if (value.Count > 0)
                {
                    cblstAdminis.DataSource = value;
                    cblstAdminis.DataTextField = "FirstName";
                    cblstAdminis.DataValueField = "UserID";
                    cblstAdminis.DataBind();
                    divbtndeleteadminis.Visible = true;
                    CustomValidator1.Visible = true;
                    lbtnDeleteSelectedAdmin.Attributes.Add("onclick", "doModalDeleteConfirm_();");
                }
                else
                {
                    divbtndeleteadminis.Visible = false;
                    CustomValidator1.Visible = false;
                }
            }
            else
            {
                divbtndeleteadminis.Visible = false;
                cblstAdminis.Visible = false;
            }
        }
    }

    public string TributeOwner
    {
        set
        {
            lblowner.InnerText = value;
            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
            {
                lblowner.InnerText = value.ToString().Replace("Tribute", "Website");
            }



        }
    }

    public string TributeName
    {
        get
        {
            return txtTributeName.Text;
        }
        set
        {
            txtTributeName.Text = value;
        }
    }

    public bool IsPrivate
    {
        get
        {
            bool _privacy = false;
            if (rdoPrivacyPrivate.Checked)
                _privacy = true;
            return _privacy;
        }
        set
        {
            if (value == false)
                rdoPrivacyPublic.Checked = true;
            else
                rdoPrivacyPrivate.Checked = true;


        }
    }

    bool _IsAutomaticRenew = false;
    public bool IsAutomaticRenew
    {
        get
        {
            return _IsAutomaticRenew;
        }
        set
        {
            _IsAutomaticRenew = value;
        }
    }

    bool _IsSponsor = false;
    public bool IsSponsor
    {
        get
        {
            return _IsSponsor;
        }
        set
        {
            _IsSponsor = value;
        }
    }

    static bool _IsSponserHide = false;
    public bool IsSponserHide
    {
        get
        {
            return _IsSponserHide;
        }
        set
        {
            _IsSponserHide = value;
        }
    }
    public IList<GetMyTributes> Mytribute
    {
        set
        {
            foreach (GetMyTributes obj in value)
            {
                lbtnmytribute.Text = obj.TributeName;  // (string)Application["lbtntributeName"];
                lbltributetype.Text = obj.TypeDescription; // (string)Application["lblTypeDescription"];
                lblCreateddate.Text = obj.StartDate.ToShortDateString(); // Application["lblACreated"].ToString();
                lbtnexpiresdate.Text = obj.Enddate.ToString(); // Application["lbtnExpires"].ToString();
                lblVisits.Text = obj.Visit.ToString(); // Application["txtVisits"].ToString();
                CheckBoxEmailErt.Checked = obj.EmailAlert; // (bool)Application["cbxEmailAlerts"];

                Session["TributeId"] = obj.TributeId;
                Session["lbtntributeName"] = obj.TributeName;
                Session["lblTypeDescription"] = obj.TypeDescription;
                Session["lblTributeUrl"] = obj.TributeUrl;
                Session["lblACreated"] = "";
                Session["lbtnExpires"] = obj.Enddate;
                Session["lblExpiredOn"] = obj.ExpiredOn;
                Session["lblRenewaldate"] = obj.Renewaldate.ToString();
                Session["txtVisits"] = obj.Visit;
                Session["cbxEmailAlerts"] = obj.EmailAlert;
            }

        }

    }

   


    public IList<Locations> CCCountryList
    {
        set
        {
            ddlCCCountry.DataSource = value;
            ddlCCCountry.DataTextField = Locations.Location.LocationName.ToString();
            ddlCCCountry.DataValueField = Locations.Location.LocationId.ToString();
            ddlCCCountry.DataBind();
        }

    }

    public IList<Locations> CCStateList
    {
        set
        {
            ddlCCStateProvince.Items.Clear();
            if (value.Count > 0)
            {
                ddlCCStateProvince.DataSource = value;
                ddlCCStateProvince.DataTextField = Locations.Location.LocationName.ToString();
                ddlCCStateProvince.DataValueField = Locations.Location.LocationId.ToString();
                ddlCCStateProvince.DataBind();
            }
        }
    }

    static int state = -1;
    public int SelectedCCState
    {
        get
        {
            if (ddlCCStateProvince.SelectedIndex != -1)
            {
                state = int.Parse(ddlCCStateProvince.SelectedValue.ToString());
                return state;
            }
            else
                return state;
        }
        set
        {
            state = value;
        }
    }
    public string SelectedCCCountry
    {
        get { return ddlCCCountry.SelectedValue.ToString(); }
        set { ddlCCCountry.SelectedIndex = ddlCCCountry.Items.IndexOf(ddlCCCountry.Items.FindByValue(value.ToString())); }

    }
    public string SelectedCCCity
    {
        get { return txtCCCity.Text; }
        set
        {
            txtCCCity.Text = value;
        }
    }

    public string CreditCardNo
    {
        get { return txtCCNumber.Text; }
        set { txtCCNumber.Text = value; }
    }

    public string CVC
    {
        get { return txtCCVerification.Text; }
        set { txtCCVerification.Text = value; }
    }

    public string CardholdersName
    {
        get { return txtCCName.Text; }
        set { txtCCName.Text = value; }
    }

    public DateTime ExpirationDate
    {
        get
        {
            DateTime _DateTime = new DateTime(int.Parse(txtCCYear.Text), ddlCCMonth.SelectedIndex, 1);
            return _DateTime;
        }
        set
        {
            DateTime _DateTime = value;
            txtCCYear.Text = _DateTime.Year.ToString();
            ddlCCMonth.SelectedIndex = ddlCCMonth.Items.IndexOf(ddlCCMonth.Items.FindByValue(_DateTime.Month.ToString()));

        }
    }

    public string Telephone
    {
        get { return txtPhoneNumber1.Text + txtPhoneNumber2.Text + txtPhoneNumber3.Text; }
        set
        {
            txtPhoneNumber1.Text = value.Substring(0, 3);
            txtPhoneNumber2.Text = value.Substring(4, 3);
            txtPhoneNumber3.Text = value.Substring(6, 4);
        }
    }


    static string _paymentmethod = string.Empty;
    public string PaymentMethod
    {
        get
        {
            if (hfPaymentMethod.Value.Length != 0)
                _paymentmethod = hfPaymentMethod.Value;

            return _paymentmethod;

        }
        set
        {
            _paymentmethod = value;
        }
    }

    public string Address
    {
        get { return txtCCBillingAddress.Text + "`" + txtCCBillingAddress2.Text; }
        set
        {
            string[] _address = value.Split('`');
            txtCCBillingAddress.Text = _address[0].ToString();
            if (_address.Length >= 2)
                txtCCBillingAddress2.Text = _address[1].ToString();

        }
    }

    public string ZipCode
    {
        get { return txtCCZipCode.Text; }
        set { txtCCZipCode.Text = value; }
    }

    public bool NotifyBeforeRenew
    {
        get
        {
            return rdoYearlyAutoRenew.Checked;
        }
    }
    public bool IsCardDetailsReusable
    {
        get
        {
            return chkSaveBillingInfo.Checked;
        }
    }

    public int PackageId
    {
        get
        {
            return _packageid;
        }
        set
        {
            _packageid = value;///remove the code.
        }
    }


    int _SponserId;
    public int SponserId
    {
        get
        {
            return _SponserId;
        }
        set
        {
            _SponserId = value;
        }
    }

    string _Sponsorname;
    public string Sponsorname
    {
        get
        {
            return _Sponsorname;
        }
        set
        {
            _Sponsorname = value;
        }
    }



    static int _TributePackageId;
    public int TributePackageId
    {
        get
        {
            return _TributePackageId;
        }
        set
        {
            _TributePackageId = value;
        }
    }

    public int TributeType
    {
        get
        {
            return _TributeIype;
        }
    }


    public int ACT
    {
        get
        {
            return Accounttype;
        }
    }

    public DateTime NewStartedDate
    {
        get
        {
            if (Accounttype == 2)
            {
                string[] _Setenddate = lbtnexpiresdate.Text.Split('/');
                DateTime date2 = new DateTime(int.Parse(_Setenddate[2]), int.Parse(_Setenddate[0]), int.Parse(_Setenddate[1]));
                _StartedDate = date2;
            }
            return _StartedDate;
        }
    }


    public bool GoogleAdSense
    {
        get
        {
            return chkbGoogleAdSense.Checked;
        }
        set
        {
            chkbGoogleAdSense.Checked = value;
        }
    }

    public string NewExpiryDate
    {
        set
        {
            lbtnexpiresdate.Text = value;
        }
    }

    #endregion
}
