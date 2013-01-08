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
using TributesPortal.Tribute.Views;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TributesPortal.MultipleLangSupport;
using System.Web.SessionState;

//using com.optimalpayments.test.webservices;
using com.optimalpayments.webservices;
using System.Xml;
using PerceptiveMCAPI.Types;
using PerceptiveMCAPI.Methods;
using PerceptiveMCAPI;

public partial class Tribute_VideoTributeSponsor : PageBase, ITributeSponsor
{
    #region <<variables>>

    private VideoTributeSponsorPresenter _presenter;
    protected static string _tributeName = string.Empty;
    private int _UserID = 0;
    private int _TribureId;
    //protected int amount = 0;
    protected string amount = string.Empty;
    private string _tributeUrl;
    protected string _userName;
    private string _firstName = "";
    private string _lastName = "";
    private string _emailId = "";
    protected string _themeName;
    private bool _isUserAdmin = false;
    private int _transactionId = 0;
    private int _tributPackageId = 0;
    private string confirmationId = string.Empty;
    private string errorMesg = string.Empty;
    private int _adminCount = 0;
    static string Status;
    private int initailPackageId = 0;
    #endregion <<variables>>
    #region BeanStream varriables
    string sBeanStreamResponce = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Request.QueryString["TributeId"] != null))
            _presenter.GetTributeSessionForId(int.Parse(Request.QueryString["TributeId"].ToString()));

        if (!WebConfig.ApplicationMode.Equals("local"))
        {
            string strXmlPath = AppDomain.CurrentDomain.BaseDirectory + "Common\\XML\\SSLPage.xml";
            FileStream docIn = new FileStream(strXmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            XmlDocument pageDoc = new XmlDocument();
            //Load the Xml Document
            pageDoc.Load(docIn);
            XmlNodeList XNodeList = pageDoc.SelectNodes("//Pages/page");
            string requestedURL = Request.Url.ToString().ToUpper();
            string[] rawURL = Request.RawUrl.Split("/".ToCharArray());
            int length = rawURL.Length;
            string redirectedPageName = rawURL[length - 1].ToString();
            if (requestedURL.Contains(@"HTTP://"))
            {
                foreach (XmlElement XElement in XNodeList)
                {
                    string pagename = XElement["pagename"].InnerText.ToString().ToUpper();
                    if (requestedURL.Contains(pagename))
                    {
                        Response.Redirect(@"https://www." + WebConfig.TopLevelDomain + "/" + redirectedPageName);
                        //string redirectUrl = Request.Url.ToString().Replace(@"http://", @"https://");
                        //Response.Redirect(redirectUrl);
                    }
                }
            }
            else
            {
                bool isPageSecure = false;
                foreach (XmlElement XElement in XNodeList)
                {
                    string pagename = XElement["pagename"].InnerText.ToString().ToUpper();
                    if (requestedURL.Contains(pagename))
                    {
                        isPageSecure = true;
                    }

                }
                if (!isPageSecure)
                    Response.Redirect(@"http://www." + WebConfig.TopLevelDomain + "/" + redirectedPageName);

            }
        }

        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
        if (objSessionvalue != null)
        {
            _userName = objSessionvalue.UserName;
            _UserID = objSessionvalue.UserId;
            _firstName = objSessionvalue.FirstName;
            _lastName = objSessionvalue.LastName;
            _emailId = objSessionvalue.UserEmail;
        }

        if (!this.IsPostBack)
        {
            StateManager stateManager_ = StateManager.Instance;
            SessionValue objSessionvalue_ = (SessionValue)stateManager_.Get("objSessionvalue", StateManager.State.Session);

            this._presenter.GetMaymentModes();
            this._presenter.GetCCCountryList();
            this._presenter.GetCCStateList();

            if (_userName != null)
            {

                rdoMembershipLifetime.Checked = true;
                rdoMembershipLifetime_CheckedChanged(sender, e);
                //DivRenew.Visible = false;
                chkAgree.Checked = chkSaveBillingInfo.Checked = false;
                this._presenter.IsUserTributeAdmin();

                //Get details of the user for displaying automaitically when the panel the payement panel  is visible
                this._presenter.GetCreditCardDetails_();
                this._presenter.GetTributePackageInfo();
                txtEmailAddress.Text = _emailId;
                if (!string.IsNullOrEmpty(_presenter.CcSelectedState))
                    ddlCCStateProvince.SelectedValue = _presenter.CcSelectedState;
            }
            else
            {
                this._presenter.GetTributePackageInfo();
                initailPackageId = PackageId;
                _UserID = 0;
                rdoMembershipLifetime.Checked = true;
                rdoMembershipLifetime_CheckedChanged(sender, e);
                SetDefault();
                txtEmailAddress.Text = string.Empty;
            }

        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            try
            {
                string s = string.Empty;
                txtCCVerification.Attributes.Add("value", txtCCVerification.Text);
                ltrPaymentMethod.Text = ltrPaymentMethod.Text.Replace("checked", string.Empty);
                ltrPaymentMethod.Text = ltrPaymentMethod.Text.Replace("id='rdoCC" + Request.Form["rdoCCType"] + "'", "checked " + "id='rdoCC" + Request.Form["rdoCCType"] + "'");

                //to ensure that the Coupon Validity check is maintained on postback
                if (txtCouponCode.Text != null && txtCouponCode.Text.Length > 0)
                {
                    if (spanCoupon.InnerHtml != null && spanCoupon.InnerHtml.Length > 0)
                        CheckCoupon();
                }
            }
            catch { }
        }
    }

    [CreateNew]
    public VideoTributeSponsorPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    #region << Methods >>

    /// <summary>
    /// This function used to get selected card Type
    /// </summary>
    /// <returns></returns>
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
            CcType = CardTypeV1.DI;
        }
        if (Request.Form["rdoCCType"] == "Amex")
        {
            CcType = CardTypeV1.AM;
        }
        return CcType;
    }

    private void SaveValues(int TributeId)
    {
        Tributes objTribute = new Tributes();
        objTribute.TributeId = TributeId;
        _presenter.GetTributeSession(objTribute);
        _tributeName = objTribute.TributeName;
        _tributeType = objTribute.TypeDescription;
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add("TributeSession", objTribute, TributesPortal.Utilities.StateManager.State.Session);
    }

    private void SetDefault()
    {
        rdoMembershipYearly.Checked = false;
        rdoMembershipLifetime.Checked = true;
        txtCCNumber.Text = txtCCVerification.Text = txtCCYear.Text = txtCCName.Text = txtCCBillingAddress.Text = txtCCBillingAddress2.Text = String.Empty;
        txtCCCity.Text = txtCCZipCode.Text = string.Empty;
        txtPhoneNumber1.Text = txtPhoneNumber2.Text = txtPhoneNumber3.Text = string.Empty;
        ddlCCMonth.SelectedIndex = ddlCCCountry.SelectedIndex = 0;
        if (ddlCCStateProvince.SelectedIndex != -1)
        {
            ddlCCStateProvince.SelectedIndex = 0;
        }
        chkAgree.Checked = chkSaveBillingInfo.Checked = false;

    }

    private void SetCouponUnAvailableStatus()
    {
        double Couponamount = 0;
        if (rdoMembershipLifetime.Checked)
        {
            amount = WebConfig.LifeTimeVideoTributeUpgrade;
            Couponamount = double.Parse(amount.Substring(1, amount.Length - 1).Trim());
            BillingTotal.InnerHtml = amount;
        }
        else
        {
            amount = WebConfig.OneYearVideoTributeUpgrade;
            Couponamount = double.Parse(amount.Substring(1, amount.Length - 1).Trim());
            BillingTotal.InnerHtml = amount;
        }

        StringBuilder Script = new StringBuilder();
        Script.Append("<script>");
        Script.Append("var notice = $('spanCoupon');");
        Script.Append("if(notice)");
        Script.Append("{");
        Script.Append("notice.innerHTML='';");
        Script.Append("notice.addClass('couponNotice-Unavailable');");
        Script.Append("notice.innerHTML = 'This is not a valid coupon code.';");
        Script.Append("$('BillingTotal').innerHTML = '" + "$" + Couponamount.ToString() + "';");
        Script.Append("}");
        Script.Append("</script>");
        spanCoupon.Visible = true;
        Page.RegisterStartupScript("HidePanel", Script.ToString());
        // ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", Script.ToString(), true);

    }

    private void CheckCoupon()
    {
        int couponType = 0;
        if (rdoMembershipLifetime.Checked)
            couponType = 3;
        else
            couponType = 2;
        int availability = _presenter.GetCouponAvailable(txtCouponCode.Text, couponType);
        if (availability == 1)
        {
            SetCouponAvailableStatus();
        }
        else
            SetCouponUnAvailableStatus();
    }

    private void SetCouponAvailableStatus()
    {
        double Couponamount = 0;
        spanCoupon.Visible = true;
        //HtmlInputRadioButton Lifetime = (HtmlInputRadioButton)this.FindControl("rdoMembershipLifetime");
        //HtmlInputRadioButton Yearly = (HtmlInputRadioButton)this.FindControl("rdoMembershipYearly");
        if (rdoMembershipLifetime.Checked)
        {
            amount = WebConfig.LifeTimeVideoTributeUpgrade;
            Couponamount = double.Parse(amount.Substring(1, amount.Length - 1).Trim());
            BillingTotal.InnerHtml = amount;
        }
        else
        {
            amount = WebConfig.OneYearVideoTributeUpgrade;
            Couponamount = double.Parse(amount.Substring(1, amount.Length - 1).Trim());
            BillingTotal.InnerHtml = amount;
        }
        StringBuilder Script = new StringBuilder();
        Script.Append("<script>");
        Script.Append("var notice = $('spanCoupon');");
        Script.Append("if(notice)");
        Script.Append("{");
        Script.Append("notice.innerHTML='';");
        Script.Append("notice.addClass('couponNotice-Available');");
        Script.Append("notice.innerHTML = 'Coupon is valid';");

        if (this._presenter.View.IsPercentage == false)
            Couponamount = Couponamount - double.Parse(this._presenter.View.Denomination);
        else
            Couponamount = Couponamount - ((double.Parse(this._presenter.View.Denomination) * Couponamount) / 100);
        if (Couponamount < 0)
        {
            Couponamount = 0;
        }
        Script.Append("$('BillingTotal').innerHTML = '" + "$" + Couponamount.ToString() + "';");
        Script.Append("}");
        Script.Append("</script>");
        Page.RegisterStartupScript("HidePanel", Script.ToString());
        if (Couponamount == 0)
        {
            PnlPaymentDetails.Visible = false;
        }
    }

    /// <summary>
    /// MaqilChimp Integartion
    /// </summary>
    /// <param name="UserType"></param>
    /// <returns></returns>
    private bool AddMailChimpSubscriber(int OldTributePackageId, int NewTributePackageId)
    {
        bool returnVal = false;
        try
        {

            listSubscribe Subscribe = new listSubscribe();
            listSubscribeInput input = new listSubscribeInput();
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            List<UserInfo> objAdminList = new List<UserInfo>();
            objAdminList = _presenter.GetTributeAdmins();
            foreach (UserInfo objUInfo in objAdminList)
            {
                #region subscribe with useremail and package id

                input.api_AccessType = PerceptiveMCAPI.EnumValues.AccessType.Serial;
                input.api_CustomErrorMessages = true;
                input.api_MethodType = PerceptiveMCAPI.EnumValues.MethodType.POST;
                input.api_Validate = true;
                input.api_OutputType = PerceptiveMCAPI.EnumValues.OutputType.XML;
                input.parms.email_address = objSessionvalue.UserEmail;
                input.parms.send_welcome = true;
                input.parms.update_existing = true;
                input.parms.replace_interests = true;
                input.parms.double_optin = false;

                input.parms.apikey = WebConfig.MailChimpApiKeyNew;
                input.parms.id = WebConfig.UserNewsLetterListID;

                // ------------------------------ address
                List<Dictionary<string, object>> batch = new List<Dictionary<string, object>>();
                Dictionary<string, object> entry = new Dictionary<string, object>();
                List<interestGroupings> groupings = new List<interestGroupings>();


                input.parms.merge_vars.Add("EMAIL", objSessionvalue.UserEmail);

                //create list of packages subscribed as per packages available wrt to a userid.
                IList<int> iPackageList = new List<int>();
                iPackageList = _presenter.GetMyTributesPackages(objUInfo.UserID);
                string strPackageGroup = string.Empty;
                string packageGroup = string.Empty;
                foreach (int packId in iPackageList)
                {
                    packageGroup = FetchInterestGroupOnPackage(packId);
                    if (!strPackageGroup.Contains(packageGroup))
                    {
                        if (string.IsNullOrEmpty(strPackageGroup))
                        {
                            strPackageGroup = packageGroup;
                        }
                        else
                        {
                            strPackageGroup = strPackageGroup + " , " + packageGroup;
                        }
                    }
                }
                interestGroupings ig = FetchInterestOnPackage(NewTributePackageId, strPackageGroup);
                groupings.Add(ig);

                input.parms.merge_vars.Add("groupings", groupings);

                // execution
                listSubscribeOutput output = Subscribe.Execute(input);
                //phase-1 enhancement


                if ((output != null) && (output.api_ErrorMessages.Count > 0))
                {
                    //string ErrorCode = output.api_ErrorMessages.ToString();
                    //string Error = "Error occured. " + output.api_ErrorMessages.FirstOrDefault().error;

                    //lblErrMsg.InnerHtml = output.api_ErrorMessages.ToString();
                    //lblErrMsg.Visible = true;
                    returnVal = false;
                }
                else
                {
                    if (output.result == true)
                    {
                        returnVal = true;
                    }
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            returnVal = false;
        }

        return returnVal;
    }
    /// <summary>
    /// Fetch Interest Group On Package
    /// </summary>
    /// <param name="packId"></param>
    /// <returns>packagelist</returns>
    private string FetchInterestGroupOnPackage(int packId)
    {
        string returnValue = string.Empty;
        switch (packId)
        {
            case 1:
                returnValue = "Memorial Tribute (Lifetime)";
                break;
            case 2:
                returnValue = "Memorial Tribute (Yearly)";
                break;
            case 3:
                returnValue = "Obituary (Free)";
                break;
            case 4:
                returnValue = "Memorial Tribute (Lifetime)";
                break;
            case 5:
                returnValue = "Memorial Tribute (Yearly)";
                break;
            case 6:
                returnValue = "Obituary (Lifetime)";
                break;
            case 7:
                returnValue = "Obituary (Free)";
                break;
            case 8:
                returnValue = "Obituary (Free)";
                break;
            default:
                returnValue = string.Empty;
                break;
        }
        return returnValue;
    }

    /// <summary>
    /// Fetch Interest On Packageid
    /// </summary>
    /// <param name="PackageId"></param>
    /// <param name="packagelist"></param>
    /// <returns>interestGroupings</returns>
    private interestGroupings FetchInterestOnPackage(int PackageId, string packagelist)
    {
        interestGroupings objIg = new interestGroupings { name = "Tribute Type", groups = new List<string> { packagelist } };
        return objIg;
    }


    #endregion << Methods >>

    #region << Properties>>

    public string SelectedCCCountry
    {
        get { return ddlCCCountry.SelectedValue.ToString(); }
        set { ddlCCCountry.SelectedIndex = ddlCCCountry.Items.IndexOf(ddlCCCountry.Items.FindByValue(value.ToString())); }
    }

    static string state;
    public string SelectedCCState
    {
        get
        {
            if (ddlCCStateProvince.SelectedIndex != -1)
            {
                state = ddlCCStateProvince.SelectedValue.ToString();
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
        get
        {
            //int x = int.Parse(txtCCNumber.Text);
            if (!txtCCNumber.Text.Contains("XXXXXXXXXX"))
            {
                return TributePortalSecurity.Security.EncryptSymmetric(txtCCNumber.Text);
            }
            else if (Session["CCNumber"] != null && Session["CCNumber"].ToString().Length > 0)
                return TributePortalSecurity.Security.EncryptSymmetric(Session["CCNumber"].ToString());
            else return null;
            //return txtCCNumber.Text; 
        }

        set
        {
            string strCredit = string.Empty;
            string ccnumber = TributePortalSecurity.Security.DecryptSymmetric(value.Trim());
            Session["CCNumber"] = ccnumber;
            for (int indexCredit = 0; indexCredit < ccnumber.Length - 4; indexCredit++)
                strCredit += "X";

            txtCCNumber.Text = strCredit + ccnumber.Substring(ccnumber.Length - 4);
            //TributePortalSecurity.Security.DecryptSymmetric(value);
            //txtCCNumber.Text=value; 
        }

        //get
        //{
        //    return TributePortalSecurity.Security.EncryptSymmetric(txtCCNumber.Text);
        //    //return txtCCNumber.Text;
        //}
        //set
        //{
        //    txtCCNumber.Text = TributePortalSecurity.Security.DecryptSymmetric(value);
        //    //string CVCCode = ;
        //    //txtCCVerification.Attributes.Add("value", CVCCode);
        //}
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
            DateTime _DateTime = new DateTime(int.Parse((string.Empty == txtCCYear.Text.ToString().Trim()) ? null : txtCCYear.Text.ToString().Trim())
                , ddlCCMonth.SelectedIndex, 1);
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
            txtPhoneNumber2.Text = value.Substring(3, 3);
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
        get
        {
            if (txtCCBillingAddress2.Text.Length != 0)
                return txtCCBillingAddress.Text + WebConfig.AddressSeparator + txtCCBillingAddress2.Text;
            else
                return txtCCBillingAddress.Text;
        }
        set
        {
            string[] splitter = { WebConfig.AddressSeparator };
            string[] _address = value.Split(splitter, System.StringSplitOptions.None);
            //string[] _address = value.Split(',');
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
            HtmlInputRadioButton rdoNotify = (HtmlInputRadioButton)this.FindControl("ctl00$TributePlaceHolder$rdoRenew");
            if (Request.Form["ctl00$TributePlaceHolder$rdoRenew"] != null)
            {
                if (Request.Form["ctl00$TributePlaceHolder$rdoRenew"] == "rdoNotifyBeforeRenew")
                    return false;
                else
                    return true;
            }
            else
                return false;
            //if (ctl00_TributePlaceHolder_rdoNotifyBeforeRenew.Visible == true) 
            //{
            //    if (ctl00_TributePlaceHolder_rdoNotifyBeforeRenew.Checked == true)
            //        return true;
            //    else
            //        return false;
            //}
            //else return false;
        }
    }

    public bool IsCardDetailsReusable
    {
        get
        {
            //Control ctrl = this.FindControl("ctl00$TributePlaceHolder$rdoRenew");
            HtmlInputRadioButton autoRenew = (HtmlInputRadioButton)this.FindControl("rdoAutoRenew");
            if (autoRenew != null)
            {
                if (autoRenew.Checked)
                    return true;
                else
                    if (chkSaveBillingInfo.Checked)
                        return true;
                    else
                        return false;
            }
            else
            {
                if (chkSaveBillingInfo.Checked)
                    return true;
                else
                    return false;
            }
            //return chkSaveBillingInfo.Checked;
        }
    }

    static int _CCID;
    public int CreditCardId
    {
        get
        {
            return _CCID;
        }
        set
        {
            _CCID = value;
        }
    }

    public decimal AmountPaid
    {
        get
        {
            return Decimal.Parse(BillingTotal.InnerHtml.ToString().Replace("$", ""));
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

    public Tributes GetTributeSession
    {
        set
        {
            StateManager stateTributes = StateManager.Instance;
            stateTributes.Add("TributeSession", value, StateManager.State.Session);
        }
    }

    public IList<Locations> CCStateList
    {
        set
        {
            ddlCCStateProvince.Items.Clear();
            if (value.Count > 0)
            {
                ddlCCStateProvince.Enabled = true;
                ddlCCStateProvince.DataSource = value;
                ddlCCStateProvince.DataTextField = Locations.Location.LocationName.ToString();
                ddlCCStateProvince.DataValueField = Locations.Location.LocationId.ToString();
                ddlCCStateProvince.DataBind();
            }
            else
            {
                ddlCCStateProvince.Enabled = false;
            }
        }
    }

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

    public int UserID
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                _UserID = objSessionvalue.UserId;
            }
            return _UserID;
        }
    }

    public string CVC
    {
        get
        {
            return TributePortalSecurity.Security.EncryptSymmetric(txtCCVerification.Text);
            //return txtCCVerification.Text;
        }
        set
        {
            //txtCCVerification.Text = value;
            string CVCCode = TributePortalSecurity.Security.DecryptSymmetric(value);
            txtCCVerification.Attributes.Add("value", CVCCode);
        }
    }

    static int state1 = -1;
    public int SelectedCCState_
    {
        get
        {
            if (ddlCCStateProvince.SelectedIndex != -1)
            {
                state1 = int.Parse(ddlCCStateProvince.SelectedValue.ToString());
                return state1;
            }
            else
                return state1;
        }
        set
        {
            state1 = value;
        }
    }

    private int _PackageId;
    public int PackageId
    {
        get
        {
            return _PackageId;

        }
        set
        {
            _PackageId = value;
        }
    }

    /*This code will subdomaians*/
    private string _SubDomain;
    public string SubDomain
    {
        get { return _SubDomain; }
        set { _SubDomain = value; }
    }

    private string _TributeURL;
    public string TributeURL
    {
        get { return _TributeURL; }
        set { _TributeURL = value; }
    }

    private Nullable<DateTime> dt1;
    public DateTime? EndDate
    {
        get
        {
            if (rdoMembershipYearly.Checked)
                return dt1.Value.AddMonths(12);
            else //(rdoMembershipLifetime.Checked)
                return null;
        }
        set
        {
            dt1 = value;

        }
    }

    public int getPackageId
    {
        get
        {
            //HtmlInputRadioButton rdoMembershipYearly = (HtmlInputRadioButton)this.FindControl("rdoMembershipYearly");
            //HtmlInputRadioButton rdoMembershipLifetime = (HtmlInputRadioButton)this.FindControl("rdoMembershipLifetime");
            int _packageid = 0;
            if (rdoMembershipYearly.Checked)
            {
                _packageid = 2;
            }
            if (rdoMembershipLifetime.Checked)
            {
                _packageid = 1;
            }
            return _packageid;
        }
    }

    public int TributeId
    {
        get
        {
            StateManager stateTribure1 = StateManager.Instance;
            Tributes objTribute = (Tributes)stateTribure1.Get("TributeSession", StateManager.State.Session);
            if (!Equals(objTribute, null))
            {
                _TribureId = objTribute.TributeId;
            }
            return _TribureId;
        }
    }

    protected string _tributeType;
    string ITributeSponsor.TributeType
    {
        get
        {
            return _tributeType;
        }
        set
        {
            _tributeType = value.ToString();
        }
    }


    public IList<ParameterTypesCodes> PaymentModes
    {
        set
        {
            StringBuilder sbPayementModes = new StringBuilder();
            if (value.Count > 0)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    sbPayementModes.Append("<div class='yt-Form-Field yt-Form-Field-Radio' id='yt-CC" + value[i].TypeDescription + "'>");
                    sbPayementModes.Append("<input type='radio' name='rdoCCType' runat='server' onclick='Check(this);' id='rdoCC" + value[i].TypeDescription + "' value='" + value[i].TypeDescription + "'");
                    if (_paymentmethod.Equals(value[i].TypeDescription))
                        sbPayementModes.Append("checked='checked' />");
                    else
                        sbPayementModes.Append("/>");
                    sbPayementModes.Append("<label for='rdoCC" + value[i].TypeDescription + "'>" + value[i].TypeDescription + "</label>");
                    sbPayementModes.Append(" </div>");
                }
                ltrPaymentMethod.Text = sbPayementModes.ToString();
            }
        }
    }

    public bool IsSponserHide
    {

        get
        {
            return true;
        }
    }

    public bool IsSponsor
    {

        get
        {
            return true;
        }
    }
    public TributePackage TributePackageDetails
    {
        set
        {
            if (value.IsAutomaticRenew && !_isUserAdmin)
            {
                //HtmlInputRadioButton Yearly = (HtmlInputRadioButton)this.FindControl("rdoMembershipYearly");
                trYearly.Visible = false;
                chooseoption.Visible = false;
            }

            StateManager objState = StateManager.Instance;
            objState.Add("TributePackageDetailsOnLoad", value, StateManager.State.Session);
        }
        get
        {
            StateManager objState = StateManager.Instance;
            return (TributePackage)objState.Get("TributePackageDetailsOnLoad", StateManager.State.Session);
        }
    }

    public bool IsUserAdmin
    {
        set
        {
            _isUserAdmin = value;
        }
    }

    public string SponsorEmailAddress
    {
        get
        {
            return txtEmailAddress.Text.Trim();
        }
    }

    public int TransactionId
    {
        set
        {
            //Session["TransactionId"] = value.ToString();
            _transactionId = value;
        }
        get
        {
            return _transactionId;
        }
    }

    //to be used for getting the package details
    public int TributePackageId
    {
        set
        {
            _tributPackageId = value;
        }
        get
        {
            return _tributPackageId;
        }
    }

   // static string countryForSignup; //by ud
    public string SelectedCountry
    {
        get { return string.Empty; }
        set { }
    }

   // static string stateForSignup; // by ud
    public string SelectedState
    {
        get
        {
            return string.Empty;
        }
        set
        {
        }
    }
    public IList<Locations> CountryList
    {
        set
        {
            ddlCCCountry.DataSource = value;
            ddlCCCountry.DataTextField = Locations.Location.LocationName.ToString();
            ddlCCCountry.DataValueField = Locations.Location.LocationId.ToString();
            ddlCCCountry.DataBind();
        }
    }

    public IList<Locations> StateList
    {
        set
        {
            ddlCCStateProvince.Items.Clear();

            if (value.Count > 0)
            {
                ddlCCStateProvince.Enabled = true;
                ddlCCStateProvince.DataSource = value;
                ddlCCStateProvince.DataTextField = Locations.Location.LocationName.ToString();
                ddlCCStateProvince.DataValueField = Locations.Location.LocationId.ToString();
                ddlCCStateProvince.DataBind();
            }
            else
            {
                ddlCCStateProvince.Enabled = false;
            }
        }
    }

    public string AdminOwner
    {
        get
        {
            return string.Empty;
        }
        set
        {

        }
    }
    public int AdminOwnerId
    {
        set
        {
        }
        get
        {
            return 0;
        }
    }

    public System.Collections.Generic.IList<TributesPortal.BusinessEntities.UserInfo> OtherAdmins
    {
        set
        {
        }
        get
        {
            IList<UserInfo> objUserInfo = new List<UserInfo>();
            return objUserInfo;
        }

    }

    public int TributeAdminCount
    {
        get
        {
            return _adminCount;
        }
        set
        {
            _adminCount = value;
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

    public string UserEmail
    {
        get { return string.Empty; }
    }

    public string chkAvailability
    {
        set { Status = value; }
    }

    string[] NameAndMsg = new string[2];
    public string[] SponsorNameandMsgForEmail
    {
        get
        {
            NameAndMsg[0] = NameAndMsg[1] = string.Empty;
            return NameAndMsg;
        }
    }

    #endregion << Properties>>

    #region << Events >>


    protected void lbtnPay_Click(object sender, EventArgs e)
    {
        bool _transaction = false;
        String UserMail = string.Empty;
        string Firstname = string.Empty;
        string LastName = string.Empty;
        StateManager statemail = StateManager.Instance;
        StateManager stateTribure = StateManager.Instance;
        Tributes objTribute = (Tributes)stateTribure.Get("TributeSession", StateManager.State.Session);

        try
        {
            //check for credit card lengths
            if (Request.Form["rdoCCType"] == "Visa" || Request.Form["rdoCCType"] == "MasterCard" || Request.Form["rdoCCType"] == "Discover")
            {
                if (txtCCNumber.Text.Length != 16 && txtCCVerification.Text.Length != 3)
                {
                    ShowMessage(ValidationSummary1.HeaderText, "Transaction Failed", true);
                    return;

                }
            }
            else if (Request.Form["rdoCCType"] == "Amex" && txtCCNumber.Text.Length != 15 && txtCCVerification.Text.Length != 4)
            {
                ShowMessage(ValidationSummary1.HeaderText, "Transaction Failed", true);
                return;

            }
            //else
            //{
            //HtmlInputRadioButton Lifetime = (HtmlInputRadioButton)this.FindControl("rdoMembershipLifetime");
            //HtmlInputRadioButton Yearly = (HtmlInputRadioButton)this.FindControl("rdoMembershipYearly");
            int couponType = 0;
            if (rdoMembershipLifetime.Checked)
                couponType = 3;
            else
                couponType = 2;
            if (txtCouponCode.Text != string.Empty)
            {
                int availability = _presenter.GetCouponAvailable(txtCouponCode.Text, couponType);

                if (availability == 1)
                {
                    //SetCouponAvailableStatus();
                    double Couponamount = 0;
                    if (rdoMembershipLifetime.Checked)
                    {
                        amount = WebConfig.LifeTimeVideoTributeUpgrade;
                        Couponamount = double.Parse(amount.Substring(1, amount.Length - 1));
                        // BillingTotal.InnerHtml = amount;
                    }
                    else
                    {
                        amount = WebConfig.OneYearVideoTributeUpgrade;
                        Couponamount = double.Parse(amount.Substring(1, amount.Length - 1));
                        //BillingTotal.InnerHtml = amount;
                    }
                    if (this._presenter.View.IsPercentage == false)
                        Couponamount = Couponamount - double.Parse(this._presenter.View.Denomination);
                    else
                        Couponamount = Couponamount - ((double.Parse(this._presenter.View.Denomination) * Couponamount) / 100);
                    if (Couponamount < 0)
                        Couponamount = 0;
                    BillingTotal.InnerHtml = ("$" + Couponamount.ToString());

                    //Remove Commented code for Payment Gateway :Amit :2/5/8
                    SessionValue objSessionmail = (SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session);
                    if (objSessionmail != null)
                    {
                        Firstname = objSessionmail.FirstName;
                        LastName = objSessionmail.LastName;
                        UserMail = objSessionmail.UserEmail;
                    }

                    PaymentGateWay objPay = new PaymentGateWay();
                    if (Couponamount > 0)
                    {
                        //_transaction = true;//objPay.PayYourBill(TributePortalSecurity.Security.DecryptSymmetric(this._presenter.View.CreditCardNo), txtCCVerification.Text, int.Parse(ddlCCMonth.SelectedValue), int.Parse(txtCCYear.Text), double.Parse(Couponamount.ToString()), SelectCreditCardType(), txtCCName.Text, "", _presenter.View.Address.Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay), txtCCCity.Text, StateV1.CA, CountryV1.US, txtCCZipCode.Text, _presenter.View.Telephone.ToString(), txtEmailAddress.Text.Trim(), HttpContext.Current.Request.UserHostAddress.ToString(), out confirmationId, out errorMesg);
                        sBeanStreamResponce = objPay.PayYourBill(TributePortalSecurity.Security.DecryptSymmetric(this._presenter.View.CreditCardNo), txtCCVerification.Text, int.Parse(ddlCCMonth.SelectedValue), int.Parse(txtCCYear.Text), double.Parse(Couponamount.ToString()), SelectCreditCardType(), txtCCName.Text.Trim(), "", _presenter.View.Address.Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay), txtCCCity.Text, StateV1.CA, CountryV1.US, txtCCZipCode.Text, _presenter.View.Telephone.ToString(), txtEmailAddress.Text.Trim(), HttpContext.Current.Request.UserHostAddress.ToString(), out confirmationId, out errorMesg, out _transaction);
                    }
                    else
                        _transaction = true;
                    //End

                    if (_transaction)
                    {
                        bool result = AddMailChimpSubscriber(initailPackageId, getPackageId);
                        if (!Equals(objTribute, null))
                        {
                            // For making Amount Paid of Float type in Video tribute upgradation
                            StateManager stateManager = StateManager.Instance;
                            stateManager.Add("SentFrom", "VideoTributeSpons", StateManager.State.Session);
                            Session["SentFrom"] = "VideoTributeSpons";
                            _TribureId = objTribute.TributeId;
                            _tributeUrl = objTribute.TributeUrl;
                            this._presenter.TriputePackageInfo(TributeId);
                            if (PnlPaymentDetails.Visible == true)
                            {
                                // if billing amount is greater thn 0 then only insert CreditCrd Details and Insert new Package details with updated record
                                this._presenter.InsertCCDetails((SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session), objTribute, confirmationId, SponsorNameandMsgForEmail);
                            }
                            else
                            {
                                // if there is no billing amount then insert only the new Package details
                                this._presenter.InsertPackageDetails(objTribute.TributeId, 0, confirmationId);
                            }
                        }
                        if (!string.IsNullOrEmpty(txtCouponCode.Text))
                            this._presenter.UpdateUsedCouponDetails(txtCouponCode.Text);


                        SetDefault();
                        if (Request.QueryString["PageName"] == "AdminMytributesPrivacy")
                        {
                            Response.Redirect(Session["APP_BASE_DOMAIN"] + this._presenter.View.TributeURL + "/paymentconfirmation.aspx?tid=" + _tributPackageId.ToString() + "&PageName=" + Request.QueryString["PageName"].ToString(), false);
                        }
                        else
                        {
                            try
                            {

                                // if person has not been charged than take him back to Tribute Main Page
                                if (Couponamount == 0)
                                    Response.Redirect(Session["APP_BASE_DOMAIN"] + this._presenter.View.TributeURL + "/", false);

                                // if person has been charged than take him to the payment Confirmation Page
                                if (WebConfig.ApplicationMode.ToLower() == "local")
                                    Response.Redirect(Session["APP_BASE_DOMAIN"] + "tribute/paymentconfirmation.aspx?tid=" + _tributPackageId.ToString() + "&SentFrom=VideoTributeSpons", false);
                                else
                                    Response.Redirect("http://video." + WebConfig.TopLevelDomain + "/tribute/paymentconfirmation.aspx?tid=" + _tributPackageId.ToString() + "&SentFrom=VideoTributeSpons", false);

                            }
                            catch //(Exception po)  by Ud
                            { }
                        }
                        
                    }
                    else
                    {
                        ShowMessage(ValidationSummary1.HeaderText, errorMesg, true);
                        //if (Request.QueryString["PageName"] == "AdminMytributesPrivacy")
                        //{
                        //    Response.Redirect(Session["APP_BASE_DOMAIN"] + "adminmytributesprivacy.aspx", false);
                        //}
                        //else
                        //{
                        //    //Response.Redirect("http://" + this._presenter.View.SubDomain.Replace("New Baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + this._presenter.View.TributeURL);
                        //    //uncomment the line above and comment the line below for server
                        //    Response.Redirect(Session["APP_BASE_DOMAIN"] + this._presenter.View.TributeURL);
                        //}
                    }
                }
                else
                {
                    if (txtCouponCode.Text != string.Empty)
                    {
                        SetCouponUnAvailableStatus();
                    }
                }
            }
            else
            {
                //SetCouponAvailableStatus();
                double Couponamount = 0;
                if (rdoMembershipLifetime.Checked)
                {
                    amount = WebConfig.LifeTimeVideoTributeUpgrade;
                    Couponamount = double.Parse(amount.Substring(1, amount.Length - 1).Trim());
                    BillingTotal.InnerHtml = amount;
                }
                else
                {
                    amount = WebConfig.OneYearVideoTributeUpgrade;
                    Couponamount = double.Parse(amount.Substring(1, amount.Length - 1).Trim());
                    BillingTotal.InnerHtml = amount;
                }


                SessionValue objSessionmail = (SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session);
                if (objSessionmail != null)
                {
                    Firstname = objSessionmail.FirstName;
                    LastName = objSessionmail.LastName;
                    UserMail = objSessionmail.UserEmail;
                }
                PaymentGateWay objPay = new PaymentGateWay();
                //Pay only when coupon amount id greater than 0
                if (Couponamount > 0)
                {
                    //_transaction = true;// objPay.PayYourBill(TributePortalSecurity.Security.DecryptSymmetric(this._presenter.View.CreditCardNo), txtCCVerification.Text, int.Parse(ddlCCMonth.SelectedValue), int.Parse(txtCCYear.Text), double.Parse(Couponamount.ToString()), SelectCreditCardType(), txtCCName.Text, "", _presenter.View.Address.Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay), txtCCCity.Text, StateV1.CA, CountryV1.US, txtCCZipCode.Text, _presenter.View.Telephone.ToString(), txtEmailAddress.Text.Trim(), HttpContext.Current.Request.UserHostAddress.ToString(),  confirmationId,  errorMesg);
                    sBeanStreamResponce = objPay.PayYourBill(TributePortalSecurity.Security.DecryptSymmetric(this._presenter.View.CreditCardNo), txtCCVerification.Text, int.Parse(ddlCCMonth.SelectedValue), int.Parse(txtCCYear.Text), double.Parse(Couponamount.ToString()), SelectCreditCardType(), txtCCName.Text.Trim(), "", _presenter.View.Address.Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay), txtCCCity.Text, StateV1.CA, CountryV1.US, txtCCZipCode.Text, _presenter.View.Telephone.ToString(), txtEmailAddress.Text.Trim(), HttpContext.Current.Request.UserHostAddress.ToString(), out confirmationId, out errorMesg, out _transaction);
                }
                else
                {
                    _transaction = true;

                }
                if (_transaction)
                {
                    if (!Equals(objTribute, null))
                    {
                        // For making Amount Paid of Float type in Video tribute upgradation
                        StateManager stateManager = StateManager.Instance;
                        stateManager.Add("SentFrom", "VideoTributeSpons", StateManager.State.Session);
                        Session["SentFrom"] = "VideoTributeSpons";
                        _TribureId = objTribute.TributeId;
                        _tributeUrl = objTribute.TributeUrl;
                        this._presenter.TriputePackageInfo(TributeId);
                        // Insert Credit Card details, Sending Mail to Sponsor and Insertion in Tribute Package table
                        this._presenter.InsertCCDetails((SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session), objTribute, confirmationId, SponsorNameandMsgForEmail);
                    }

                    SetDefault();
                    if (Request.QueryString["PageName"] == "AdminMytributesPrivacy")
                    {

                        Response.Redirect(Session["APP_BASE_DOMAIN"] + "tribute/paymentconfirmation.aspx?tid=" + _tributPackageId.ToString() + "&PageName=" + Request.QueryString["PageName"].ToString(), false);
                    }
                    else
                    {
                        try
                        {
                            if (WebConfig.ApplicationMode.ToLower() == "local")
                                Response.Redirect(Session["APP_BASE_DOMAIN"] + "tribute/paymentconfirmation.aspx?tid=" + _tributPackageId.ToString() + "&SentFrom=VideoTributeSpons", false);
                            else
                                Response.Redirect("http://video." + WebConfig.TopLevelDomain + "/tribute/paymentconfirmation.aspx?tid=" + _tributPackageId.ToString() + "&SentFrom=VideoTributeSpons", false);

                        }
                        catch //(Exception abc) // by Ud
                        { }
                    }
                }
                else
                {
                    ShowMessage(ValidationSummary1.HeaderText, errorMesg, true);
                }
            }
        }
        catch (Exception excep)
        {
            if (excep.Message.StartsWith("PAYMENT"))
                ShowMessage(ValidationSummary1.HeaderText, "Transaction Failed! An error has occured while trying to connect to the payment gateway. Please try later.", true);
            else
                ShowMessage(ValidationSummary1.HeaderText, "Transaction Failed! While your transaction was successful and your credit card was charged but we could not process it at our end. Please contact the webmaster.", true);
            //ShowMessage(ValidationSummary1.HeaderText, "Transaction Failed. Please try again later.", true);
        }
        //}
    }

    protected void lbtnValidateCoupon_Click(object sender, EventArgs e)
    {
        //HtmlInputRadioButton Lifetime = (HtmlInputRadioButton)this.FindControl("rdoMembershipLifetime");
        //HtmlInputRadioButton Yearly = (HtmlInputRadioButton)this.FindControl("rdoMembershipYearly");

        if (!string.IsNullOrEmpty(txtCouponCode.Text.Trim()))
        {
            //remove leading and trailing spaces
            txtCouponCode.Text = txtCouponCode.Text.Trim();
            CheckCoupon();
        }
        else
            ShowMessage(ValidationSummary1.HeaderText, "Please provide coupon code", true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["PageName"] == "AdminMytributesPrivacy")
            {
                // Added by Ashu on Oct 4, 2011 for rewrite URL 
                if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                    Response.Redirect(Session["APP_BASE_DOMAIN"] + "MyHome/adminmyMomentsprivacy.aspx", false);
                else
                    Response.Redirect(Session["APP_BASE_DOMAIN"] + "MyHome/adminmytributesprivacy.aspx", false);

                Session["Sentby"] = "TributeSponsor";
            }
            else
            {
                Response.Redirect("http://" + this._presenter.View.SubDomain.Replace("New Baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + this._presenter.View.TributeURL);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(Session["APP_BASE_DOMAIN"] + "log_in.aspx", false);
        }
    }

    protected void ddlCCCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.GetCCStateList();
    }

    protected void rdoMembershipLifetime_CheckedChanged(object sender, EventArgs e)
    {
        //ScriptManager.RegisterClientScriptBlock(rdoMembershipLifetime, GetType(), "HidePanel", "hideWideRows();", true);
        BillingTotal.InnerHtml = WebConfig.LifeTimeVideoTributeUpgrade;
        PnlPaymentDetails.Visible = true;
        spanCoupon.Visible = false;
        DivRenew.Visible = false;
    }
    protected void rdoMembershipYearly_CheckedChanged(object sender, EventArgs e)
    {
        //ScriptManager.RegisterClientScriptBlock(rdoMembershipYearly, GetType(), "HidePanel", "hideWideRows();", true);
        BillingTotal.InnerHtml = WebConfig.OneYearVideoTributeUpgrade;
        PnlPaymentDetails.Visible = true;
        spanCoupon.Visible = false;
        DivRenew.Visible = true;
    }
    #endregion << Events>>

    #region ITributeSponsor Members


    public string ApplicationType
    {
        get { throw new NotImplementedException(); }
    }

    public bool IsVideo = false;
    public bool IsContainsVideo
    {
        get { return IsVideo; }
        set { IsVideo = value; }
    }

    #endregion
}