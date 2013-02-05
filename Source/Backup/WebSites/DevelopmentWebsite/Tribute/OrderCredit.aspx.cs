// Credit Order Form
// By Mohit Gupta 5 Nov 2010

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
using com.optimalpayments.webservices;

public partial class Tribute_OrderCredit : PageBase, IOrderCredit
{
    #region <<variables>>

    private OrderCreditPresenter _presenter;
    protected static string _tributeName = string.Empty;
    private int _UserID = 0;
    private int _TribureId;
    //protected int amount = 0;
    protected string amount = string.Empty;
    protected int CCIdentity = 0;
    //private string _tributeUrl;
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
    private int _NetCreditCount;
    private IList<CreditCostMapping> _creditCostMappingList = null;

     #region BeanStream varriables
    string sBeanStreamResponce = string.Empty;
    #endregion
   
    #endregion <<variables>>

    protected void Page_Load(object sender, EventArgs e)
    {
        // Code to implement SSL Functionality.
        //if (!WebConfig.ApplicationMode.Equals("local"))
        //{
        //    string tributeurl = "";
        //    string tributetype = "";
        //    if ((Request.QueryString["TributeUrl"] != null) && (Request.QueryString["TributeType"] != null))
        //    {
        //        tributeurl = Request.QueryString["TributeUrl"].ToString();
        //        tributetype = Request.QueryString["TributeType"].ToString();
        //    }

        //    if (Request.Url.ToString().Contains(@"http://"))
        //        Response.Redirect(@"https://www." + WebConfig.TopLevelDomain + @"/TributeSponsor.aspx?TributeURL=" + tributeurl + "&TributeType=" + tributetype);
        //}

        //lblCopyRight.Text = DateTime.Now.Year.ToString();
        if ((Request.QueryString["TributeUrl"] != null) && (Request.QueryString["TributeType"] != null))
            _presenter.GetTributeSessionForUrlAndType(Request.QueryString["TributeUrl"].ToString(), Request.QueryString["TributeType"].ToString(),WebConfig.ApplicationType.ToString());

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
            // Getting list of Credit and Cost/Credit details from DB
            _presenter.GetCreditCostMapping();
            grdCreditCostTable.DataSource = _creditCostMappingList;
            grdCreditCostTable.DataBind();
            Session["CreditPointSelected"] = "1";

            if (_userName != null)
            {
                chkAgree.Checked = chkSaveBillingInfo.Checked = false;
                this._presenter.IsUserTributeAdmin();
                this._presenter.GetCreditCardDetails_();
                this._presenter.GetTributePackageInfo();
                txtEmailAddress.Text = _emailId;
                if (!string.IsNullOrEmpty(_presenter.CcSelectedState))
                    ddlCCStateProvince.SelectedValue = _presenter.CcSelectedState;
            }
            else
            {
                this._presenter.GetTributePackageInfo();
                _UserID = 0;
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
            }
            catch { }
        }
    }

    [CreateNew]
    public OrderCreditPresenter Presenter
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

        //rdoInformNo.Checked = false;
        //rdoInformYes.Checked = true;
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
        get { return   _TributeURL; }
        set { _TributeURL = value; }
    }

    private Nullable<DateTime> dt1;
    public DateTime? EndDate
    {
        get
        {
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
    string IOrderCredit.TributeType
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
            bool _IsSponserHide = false;
            HtmlInputRadioButton rdoInformYes = (HtmlInputRadioButton)this.FindControl("rdoInformYes");
            if (rdoInformYes.Checked)
                _IsSponserHide = true;
            return _IsSponserHide;
        }
    }

    public TributePackage TributePackageDetails
    {
        set
        {
            if (value.IsAutomaticRenew && !_isUserAdmin)
            {
                //HtmlInputRadioButton Yearly = (HtmlInputRadioButton)this.FindControl("rdoMembershipYearly");

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

    public int NetCreditPoints
    {
        get
        {
            return _NetCreditCount;
        }
        set
        {
            _NetCreditCount = value;
            if ((_NetCreditCount == 0) ) //|| (_NetCreditCount == null)) by Ud
            {
                NetCreditCount.InnerHtml = "<b><img align ='top' src='../assets/images/error_pic.png' alt='error'/>You have <span class='bold_red'>no credits</span> in your account!</b>&nbsp; You can order more credits below.";
            }
            else
            {
                if (_NetCreditCount == 1)
                {
                    NetCreditCount.InnerHtml = "<img align ='top' src='../assets/images/dollor-pic.png' style='margin-right:10px' alt='dollor'/> <b>You have <span class='bold_green'>" + _NetCreditCount.ToString() + " credit</span> in your account!</b> &nbsp;You can order more credits below:";
                }
                else
                {
                    NetCreditCount.InnerHtml = "<img align ='top' src='../assets/images/dollor-pic.png' style='margin-right:10px' alt='dollor'/> <b>You have <span class='bold_green'>" + _NetCreditCount.ToString() + " credits</span> in your account! </b>&nbsp;You can order more credits below:";
                }
            }

        }
    }

    public IList<CreditCostMapping> CreditCostMappingList
    {
        set
        {
            _creditCostMappingList = value;
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
        int NewUpdatedCredit = 0;
        string strBillingTotal;
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

            int Couponamount = 0;
            // Getting Value from Billing span at the bootm of the page
            strBillingTotal = Convert.ToString(BillingTotal.InnerText);
            Couponamount = Convert.ToInt32(strBillingTotal.Substring(1, strBillingTotal.Length - 1));



            SessionValue objSessionmail = (SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionmail != null)
            {
                Firstname = objSessionmail.FirstName;
                LastName = objSessionmail.LastName;
                UserMail = objSessionmail.UserEmail;
            }
            PaymentGateWay objPay = new PaymentGateWay();
            // bool _transaction = true;

            //Start - Modification on 15-Dec-09 for the enhancement 1 of the Phase 1
            //If the bill amount (Couponamount) is greater than $0, then only the transaction should go to the payment gateway
            // Pay only if CouponAmount is greater than only pay the bill
            if (Couponamount > 0)
               // _transaction = objPay.PayYourBill(TributePortalSecurity.Security.DecryptSymmetric(this._presenter.View.CreditCardNo), txtCCVerification.Text, int.Parse(ddlCCMonth.SelectedValue), int.Parse(txtCCYear.Text), Couponamount.ToString(), SelectCreditCardType(), txtCCName.Text, "", _presenter.View.Address.Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay), txtCCCity.Text, StateV1.CA, CountryV1.US, txtCCZipCode.Text, _presenter.View.Telephone.ToString(), txtEmailAddress.Text.Trim(), HttpContext.Current.Request.UserHostAddress.ToString(), out confirmationId, out errorMesg);
                sBeanStreamResponce=objPay.PayYourBill(TributePortalSecurity.Security.DecryptSymmetric(this._presenter.View.CreditCardNo), txtCCVerification.Text, int.Parse(ddlCCMonth.SelectedValue), int.Parse(txtCCYear.Text), Couponamount, SelectCreditCardType(), txtCCName.Text.Trim(), "", _presenter.View.Address.Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay), txtCCCity.Text, StateV1.CA, CountryV1.US, txtCCZipCode.Text, _presenter.View.Telephone.ToString(), txtEmailAddress.Text.Trim(), HttpContext.Current.Request.UserHostAddress.ToString(), out confirmationId, out errorMesg, out _transaction);
            else
                _transaction = true;
            //End

            if (_transaction)
            {

                if (PnlPaymentDetails.Visible == true)
                {
                    // Insert Credit Card Info if payment panel is visible else not
                    CCIdentity = this._presenter.InsertCreditPointCCDetails((SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session), confirmationId);
                }
                //     this._presenter.InsertPackageDetails(objTribute.TributeId, CCIdentity, confirmationId);                            

                // Get current credit Point in the User account
                _presenter.GetCreditPointCount();
                NewUpdatedCredit = int.Parse(Session["CreditPointSelected"].ToString()) + _NetCreditCount;

                // Insert updated Credit Point in CreditPointTransaction Table
                this._presenter.InsertCurrentCreditPoints(NewUpdatedCredit, CCIdentity.ToString(), confirmationId);

                if (Couponamount > 0)
                    //to get the sponsorship receipt and send the same on Email 
                    _presenter.OnViewInitialized();

                SetDefault();
                if (Convert.ToInt32(Session["CreditPointSelected"].ToString()) > 1)
                {
                    Session["LineForPayConf"] = "Thank you for your order of <b>" + Session["CreditPointSelected"].ToString() + " credits</b>.<br/><br/>Your credit card will show a charge from \"Your"+ConfigurationManager.AppSettings["ApplicationWord"]+"\" for<b> $" + Couponamount.ToString() + "</b>. Your transaction ID is<b> " + TransactionId.ToString() + ".</b>";
                }
                else
                    Session["LineForPayConf"] = "Thank you for your order of <b> " + Session["CreditPointSelected"].ToString() + " credit</b>.<br/><br/>Your credit card will show a charge from \"Your"+ConfigurationManager.AppSettings["ApplicationWord"]+"\" for<b> $" + Couponamount.ToString() + "</b>. Your transaction ID is<b> " + TransactionId.ToString() + ".</b>";

                Session["SentFrom"] = "OrderCredit";
                if (WebConfig.ApplicationMode.ToLower() == "local")
                    Response.Redirect(Session["APP_BASE_DOMAIN"] + "tribute/paymentconfirmation.aspx?SentFrom=OrderCredit", false);
                else
                    Response.Redirect("http://" + WebConfig.TopLevelDomain + "/tribute/paymentconfirmation.aspx?SentFrom=OrderCredit", false);
                //Response.Redirect("http://video." + WebConfig.TopLevelDomain + "/tribute/paymentconfirmation.aspx?SentFrom=OrderCredit", false);
                // commented by udham to redirect to top level domain
            }
            else
            {
                //ShowMessage(ValidationSummary1.HeaderText, errorMesg, true);
                var sResponseArr = sBeanStreamResponce.Split('&');
                var sErrorMsg = sResponseArr.Length > 3 && sResponseArr[3].Split('=').Length > 1 && !string.IsNullOrEmpty(sResponseArr[3].Split('=')[1]) ? sResponseArr[3].Split('=')[1].Replace("+", " ") : "";
                //LHK:17-11-2011- for gracefull error message display.
                sErrorMsg = HttpUtility.UrlDecode(sErrorMsg);
                ShowMessage(ValidationSummary1.HeaderText, sErrorMsg, true);

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



    protected void btnCancel_Click(object sender, EventArgs e)
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
            // Added by Ashu on Oct 3, 2011 for rewrite URL 
            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                Response.Redirect("http://www." + WebConfig.TopLevelDomain + "/moments.aspx");
            else
                Response.Redirect("http://www." + WebConfig.TopLevelDomain + "/tributes.aspx");
        }
    }

    protected void ddlCCCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.GetCCStateList();
    }

    protected void grdCreditCostTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        _presenter.GetCreditPointCount();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            //string CreditPointValue = e.Row.Cells[0].Controls[1].ToString();        

            //Finding the label control in the GridView that will recieve the data
            Label lblCreditPoint = (Label)e.Row.FindControl("lblCreditPoint");
            Label lblTotalCost = (Label)e.Row.FindControl("lblTotalCost");
            Label lblCostPercredit = (Label)e.Row.FindControl("lblCostPercredit");
            lblTotalCost.Text = "$" + Convert.ToString(int.Parse(lblCreditPoint.Text) * (double.Parse(lblCostPercredit.Text.ToString()))) + ".00";
            double CostPerCredit = double.Parse(lblCostPercredit.Text);
            lblCostPercredit.Text = "$" + CostPerCredit.ToString("#.00") + "/credit";

            // Check the First Radio Button initially
            if (e.Row.RowIndex.Equals(0))
            {
                RadioButton rbtnfirst = (RadioButton)e.Row.Cells[0].FindControl("rbtnCreditSelection");
                rbtnfirst.Checked = true;
                // Set the value of the span at the bottom of the image equal to the cost selected in the radio Button
                BillingTotal.InnerHtml = lblTotalCost.Text.Remove(lblTotalCost.Text.Length - 3, 3);
                Session["CreditPointSelected"] = int.Parse(lblCreditPoint.Text);
            }
            //Setting the text of the label founded with the data we got it
            //  myLabel.Text = CreditPointValue;

        }


    }





    protected void rbtncreditSelection_CheckedChanged(object sender, EventArgs e)
    {
        // Initially set all radio buttons to unchecked
        foreach (GridViewRow oldrow in grdCreditCostTable.Rows)
        {
            ((RadioButton)oldrow.FindControl("rbtnCreditSelection")).Checked = false;
        }

        //Set the radio button checked for the radio button that is slected
        RadioButton rb = (RadioButton)sender;
        GridViewRow row = (GridViewRow)rb.NamingContainer;
        ((RadioButton)row.FindControl("rbtnCreditSelection")).Checked = true;
        Label lblCreditPoint = (Label)row.FindControl("lblCreditPoint");
        Label lblTotalCost = (Label)row.FindControl("lblTotalCost");
        Session["CreditPointSelected"] = int.Parse(lblCreditPoint.Text);
        BillingTotal.InnerHtml = lblTotalCost.Text.Remove(lblTotalCost.Text.Length - 3, 3);


    }
    #endregion << Events>>
}