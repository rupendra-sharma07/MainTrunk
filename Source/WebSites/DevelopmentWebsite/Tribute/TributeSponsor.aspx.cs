///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.TributeSponsor.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page is used to sponsor any tribute to extend the lifetime of a tribute on the site
///Audit Trail     : Date of Modification  Modified By         Description
///

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Tribute.Views;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using System.Text;
using Facebook;
using Facebook.Web;
using com.optimalpayments.webservices;
using PerceptiveMCAPI.Types;
using PerceptiveMCAPI.Methods;
using PerceptiveMCAPI;

public partial class Tribute_TributeSponsor : PageBase, ITributeSponsor
{
    #region <<variables>>

    private TributeSponsorPresenter _presenter;
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
    private int linkedVideoTributeId = 0;
    private int _userType = 0;
    private int _videoTributeOwnerid = 0;
    private int _adminOwnerId = 0;
    private IList<UserInfo> _otherAdmins = null;
    private string _adminOwner = string.Empty;
    private int emailPassSuccess = 0;
    private int flagForAdminCndtns = 0;
    private int _email = -1;
    private int _adminCount = -1;
    private int _userTypeOfTributeOwner = 0;
    private bool hasPersonalUserAdmin = false;
    static string Status;
    private static double valueInDouble1;
    string Domainname = string.Empty;
    private static string OldTributeURL = string.Empty;
    private string SponsorName = string.Empty;
    private int initailPackageId = 0;
    public string todayYear = DateTime.Today.Year.ToString();
    public string todayMonth = DateTime.Today.Month.ToString();

    #region BeanStream varriables
    string sBeanStreamResponce = string.Empty;
    #endregion

    #endregion <<variables>>

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.Action = Request.RawUrl;
        revTributeAddress.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()+" Address can only contain letters, numbers and '_'";
        rfvTributeAddress.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " web address is a required field.";
        revTributeAddressNext.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " Address can only contain letters, numbers and '_'";
        rfvTributeAddressNext.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " web address is a required field.";

        revTributeaddress2.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "Address can only contain letters, numbers and '_'";
        cvTributeAddressOther.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " web address is a required field";
        revTributeaddressOtherNext.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " Address can only contain letters, numbers and '_'";
        cvTributeAddressOtherNext.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " web address is a required field";
        RequiredFieldValidator1.ErrorMessage = "You have not entered any name for the " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower() + " creator to know who you are";

        if ((Request.QueryString["TributeUrl"] != null) && (Request.QueryString["TributeType"] != null))
            _presenter.GetTributeSessionForUrlAndType(Request.QueryString["TributeUrl"].ToString(), Request.QueryString["TributeType"].ToString(), WebConfig.ApplicationType.ToString());
        else if((Request.QueryString["WebsiteUrl"] != null) && (Request.QueryString["WebsiteType"] != null))
            _presenter.GetTributeSessionForUrlAndType(Request.QueryString["WebsiteUrl"].ToString(), Request.QueryString["WebsiteType"].ToString(),WebConfig.ApplicationType.ToString());
        
        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue) stateManager.Get("objSessionvalue", StateManager.State.Session);
        TributePackage objTributePackage = new TributePackage();
        if (objSessionvalue != null)
        {
            _userName = objSessionvalue.UserName;
            _UserID = objSessionvalue.UserId;
            _firstName = objSessionvalue.FirstName;
            _lastName = objSessionvalue.LastName;
            _emailId = objSessionvalue.UserEmail;
            _userType = objSessionvalue.UserType;
        }

        if (!this.IsPostBack)
        {
            StateManager stateManager_ = StateManager.Instance;
            SessionValue objSessionvalue_ = (SessionValue)stateManager_.Get("objSessionvalue", StateManager.State.Session);

            this._presenter.GetMaymentModes();
            this._presenter.GetCCCountryList();
            this._presenter.GetCCStateList();
            this._presenter.GetCountryListForSignUp();
            this._presenter.GetStateListForSignUp();
            this._presenter.GetTributePackageInfo();
            this._presenter.IsTributeContainsVideoTribute(TributeId);
            objTributePackage = TributePackageDetails;

            //  Getting Admin List
            //  Mohit Gupta ::  2 Feb 2011
            hasPersonalUserAdmin = this._presenter.GetTributeAdminis();
            _userTypeOfTributeOwner = this._presenter.GetUserTypeOfTributeOwner();

            OldTributeURL = (Request.QueryString["TributeUrl"] != null) ? Request.QueryString["TributeUrl"].ToString() : Request.QueryString["WebsiteUrl"].ToString();
            SetControls(_userTypeOfTributeOwner, hasPersonalUserAdmin);

            SetUpgradeOptions(objTributePackage.PackageId, objTributePackage.EndDate);

            //if the user is coming from reach limit modal popup then tribute type upgrade is only possible
            if (Request.QueryString["ReachLimit"] != null)
            {
                if (Request.QueryString["ReachLimit"].ToLower() == "max")
                {

                    // For photo tribute yearly account from limit Modal popup
                    if (objTributePackage.PackageId == 7)
                    {
                        midColumn1.Visible =
                            midColumn2.Visible =
                            midColumn3.Visible =
                            midColumn4.Visible =
                            midColumn5.Visible =
                            midColumn6.Visible =
                            midColumn7.Visible =
                            midColumn8.Visible =
                            midColumn9.Visible =
                            midColumn10.Visible =
                            midColumn11.Visible =
                            midColumn12.Visible =
                            midColumn13.Visible =
                            midColumn14.Visible = secondLastRow.Visible = midColumn16.Visible = false;
                        rdoTributeMembershipYearly_CheckedChanged(sender, e);
                        rdoTributeMembershipYearly.Checked = true;
                    }
                    else if (objTributePackage.PackageId == 6)
                    {
                        midColumn1.Visible =
                            midColumn2.Visible =
                            midColumn3.Visible =
                            midColumn4.Visible =
                            midColumn5.Visible =
                            midColumn6.Visible =
                            midColumn7.Visible =
                            midColumn8.Visible =
                            midColumn9.Visible =
                            midColumn10.Visible =
                            midColumn11.Visible =
                            midColumn12.Visible =
                            midColumn13.Visible =
                            midColumn14.Visible = secondLastRow.Visible = midColumn16.Visible = false;
                        rdoTributeMembershipLifeTime_CheckedChanged(sender, e);
                        rdoTributeMembershipYearly.Enabled = false;
                        rdoTributeMembershipLifeTime.Checked = true;
                    }
                }
            }
            if (_userName != null)
            {

                this._presenter.IsUserTributeAdmin();
                this._presenter.GetCreditCardDetails_();


                // If the Tribute package type is Tribute Yearly then display only the Tribute Column options in the upgrade page

                txtEmailAddress.Text = _emailId;

                if (!string.IsNullOrEmpty(_presenter.CcSelectedState))
                    ddlCCStateProvince.SelectedValue = _presenter.CcSelectedState;

                if (!string.IsNullOrEmpty(_presenter.SelectedState))
                    ddlState.SelectedValue = _presenter.SelectedState;
            }
            else
            {
                this._presenter.GetTributePackageInfo();
                _UserID = 0;
                SetDefault();
                txtEmailAddress.Text = string.Empty;
            }
        }
        //LHK: to hide custom url for tribute yearly upgrade
        objTributePackage = TributePackageDetails;
        if (objTributePackage != null)
        {
            //initailPackageId = objTributePackage.PackageId;
            //if(initailPackageId==3 || initailPackageId==8)
            //{
            //    rdoPhotoMembershipYearly.Visible = true;
            //    rdoPhotoMembershipYearly.Checked = true;
            //    rdoPhotoMembershipYearly_CheckedChanged(sender,e);
            //}
            if (initailPackageId == 5)
            {
                PanelChangeAddress.Visible = false;
                PanelTributeAddress.Visible = false;
                txtTributeAddress.Text = (Request.QueryString["TributeUrl"] != null)
                                             ? Request.QueryString["TributeUrl"].ToString()
                                             : Request.QueryString["WebsiteUrl"].ToString();

            }
        }
        error1.Visible = false;
        ErrorAccept.Visible = false;

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
            catch
            {
            }
        }
    }

    [CreateNew]
    public TributeSponsorPresenter Presenter
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
        if (rdoTributeMembershipYearly.Checked || rdoTributeMembershipLifeTime.Checked)
        {
            pnlTributeAddressAvailable.Visible = false;
            PanelChangeAddress.Visible = true;
            PanelTributeAddress.Visible = false;
        }
        else
        {
            pnlTributeAddressAvailable.Visible = false;
            PanelTributeAddress.Visible = false;
            PanelChangeAddress.Visible = false;

        }

        if (rdoChangeAddressYes.Checked)
        {
            PanelTributeAddress.Visible = true;
            lbtncheckAddress.Visible = true;
            txtTributeAddress.Text = "";
            txtTributeAddress.Enabled = true;
            rdbAvailableAddress1.Checked = rdbAvailableAddress2.Checked = rdbAvailableAddress3.Checked = false;
            txtTributeAddressOther.Text = "";
        }
        else if (rdoChangeAddressNo.Checked)
        {
            pnlTributeAddressAvailable.Visible = false;
            PanelTributeAddress.Visible = false;
            PanelChangeAddress.Visible = false;
            txtTributeAddress.Text = Request.QueryString["TributeUrl"].ToString();
        }
        else
        {
            pnlTributeAddressAvailable.Visible = false;
            PanelTributeAddress.Visible = false;
            PanelChangeAddress.Visible = false;
        }


        txtCCNumber.Text = txtCCVerification.Text = txtCCYear.Text = txtCCName.Text = txtCCBillingAddress.Text = txtCCBillingAddress2.Text = String.Empty;
        txtCCCity.Text = txtCCZipCode.Text = string.Empty;
        txtPhoneNumber1.Text = txtPhoneNumber2.Text = txtPhoneNumber3.Text = string.Empty;
        ddlCCMonth.SelectedIndex = ddlCCCountry.SelectedIndex = ddlCountry.SelectedIndex = 0;
        if (ddlCCStateProvince.SelectedIndex != -1)
        {
            ddlCCStateProvince.SelectedIndex = 0;
        }
        if (ddlState.SelectedIndex != -1)
        {
            ddlState.SelectedIndex = 0;
        }
        chkAgree.Checked = chkSaveBillingInfo.Checked = false;

    }


    private void SetControls(int userTypeOfTributeOwner, bool hasPersonalUserAdmin)
    {
        int NeedtoDisplay = 0;
        NeedtoDisplay = this._presenter.DisplayAdminPanel(userTypeOfTributeOwner, hasPersonalUserAdmin);

        // If Tribute created by Business User and has no administrator other than tribute creator
        if (NeedtoDisplay == 1)
        {
            BecomeFieldsAdmin.Visible = true;
            //rdoPhotoMembershipYearly.Checked = true;
            haveAccountFields.Visible =
                loginInfoFields.Visible =
                SignUpFields.Visible =
                asGiftOptions.Visible = knowIdentityFields.Visible = NameMsgFields.Visible = false;
            rdoAdminYes.Checked =
                rdoAdminNo.Checked =
                rdoHaveAccountNo.Checked =
                rdoHaveAccountYes.Checked = rdoAsGiftNo.Checked = rdoAsGiftYes.Checked = rdoKnowYouNo.Checked = false;

        }

        if (NeedtoDisplay == 2)
        {
            BecomeFieldsAdmin.Visible =
                haveAccountFields.Visible =
                loginInfoFields.Visible =
                SignUpFields.Visible = knowIdentityFields.Visible = NameMsgFields.Visible = false;
            asGiftOptions.Visible = true;
            rdoAsGiftNo.Checked = rdoAsGiftYes.Checked = false;
        }


        if (rdoTributeMembershipYearly.Checked || rdoTributeMembershipLifeTime.Checked)
        {
            pnlTributeAddressAvailable.Visible = false;
            PanelChangeAddress.Visible = true;
            PanelTributeAddress.Visible = false;
        }
        else
        {
            pnlTributeAddressAvailable.Visible = false;
            PanelTributeAddress.Visible = false;
            PanelChangeAddress.Visible = false;

        }

        if (rdoChangeAddressYes.Checked)
        {
            PanelTributeAddress.Visible = true;
            lbtncheckAddress.Visible = true;
            txtTributeAddress.Text = "";
            txtTributeAddress.Enabled = true;
            rdbAvailableAddress1.Checked = rdbAvailableAddress2.Checked = rdbAvailableAddress3.Checked = false;
            txtTributeAddressOther.Text = "";
        }
        else if (rdoChangeAddressNo.Checked)
        {
            pnlTributeAddressAvailable.Visible = false;
            PanelTributeAddress.Visible = false;
            PanelChangeAddress.Visible = false;
            txtTributeAddress.Text = Request.QueryString["TributeUrl"].ToString();
        }
        else
        {
            pnlTributeAddressAvailable.Visible = false;
            PanelTributeAddress.Visible = false;
            PanelChangeAddress.Visible = false;
        }


    }

    private void SetCouponUnAvailableStatus()
    {
        double Couponamount = 0;
        amount = BillingTotalInDollars;
        Couponamount = Convert.ToDouble(amount.Substring(1, amount.Length - 1));
        BillingTotal.InnerHtml = BillingTotalAbove.InnerHtml = Couponamount.ToString("#,0.00"); ;


        StringBuilder Script = new StringBuilder();
        Script.Append("<script>");
        Script.Append("var notice = $('spanCoupon');");
        Script.Append("if(notice)");
        Script.Append("{");
        Script.Append("notice.innerHTML='';");
        Script.Append("notice.addClass('couponNotice-Unavailable');");
        Script.Append("notice.innerHTML = 'This is not a valid coupon code.';");
        Script.Append("$('BillingTotal').innerHTML = '" + "$" + Couponamount.ToString("#,0.00") + "';");
        Script.Append("$('BillingTotalAbove').innerHTML = '" + "$" + Couponamount.ToString("#,0.00") + "';");
        Script.Append("}");
        Script.Append("</script>");
        spanCoupon.Visible = true;
        Page.RegisterStartupScript("HidePanel", Script.ToString());
        if (Couponamount == 0)
        {
            PnlPaymentDetails.Visible = false;
        }
        else
        {
            PnlPaymentDetails.Visible = true;
        }



    }

    private void CheckCoupon()
    {
        int couponType = 0;
        if (rdoPhotoMembershipLifeTime.Checked || rdoTributeMembershipLifeTime.Checked)
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
        amount = BillingTotalInDollars;
        Couponamount = Convert.ToDouble(amount.Substring(1, amount.Length - 1));
        BillingTotal.InnerHtml = BillingTotalAbove.InnerHtml = amount;


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
            Couponamount = Couponamount - ((double.Parse(this._presenter.View.Denomination)*Couponamount)/100);
        if (Couponamount < 0)
        {
            Couponamount = 0;
        }
        if (Couponamount != 0)
        {
            Script.Append("$('BillingTotal').innerHTML = '" + "$" + Couponamount.ToString() + "';");
        }
        Script.Append("$('BillingTotalAbove').innerHTML = '" + "$" + Couponamount.ToString() + "';");
        Script.Append("}");
        Script.Append("</script>");
        Page.RegisterStartupScript("HidePanel", Script.ToString());
        if (Couponamount == 0)
        {
            PnlPaymentDetails.Visible = false;
        }
    }

    #endregion << Methods >>

    #region << Properties>>

    public string SelectedCCCountry
    {
        get { return ddlCCCountry.SelectedValue.ToString(); }
        set { ddlCCCountry.SelectedIndex = ddlCCCountry.Items.IndexOf(ddlCCCountry.Items.FindByValue(value.ToString())); }
    }

    private static string state;

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
        set { state = value; }
    }

    public string SelectedCCCity
    {
        get { return txtCCCity.Text; }
        set { txtCCCity.Text = value; }
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


    private static string _paymentmethod = string.Empty;

    public string PaymentMethod
    {
        get
        {
            if (hfPaymentMethod.Value.Length != 0)
                _paymentmethod = hfPaymentMethod.Value;

            return _paymentmethod;

        }
        set { _paymentmethod = value; }
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
            string[] splitter = {WebConfig.AddressSeparator};
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
            HtmlInputRadioButton autoRenew = (HtmlInputRadioButton) this.FindControl("rdoAutoRenew");
            if (autoRenew != null)
            {
                if (autoRenew.Checked)
                    return true;
                else if (chkSaveBillingInfo.Checked)
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
        }
    }

    private static int _CCID;

    public int CreditCardId
    {
        get { return _CCID; }
        set { _CCID = value; }
    }

    public decimal AmountPaid
    {
        get { return Decimal.Parse(BillingTotal.InnerHtml.ToString().Replace("$", "")); }
    }

    public string BillingTotalInDollars
    {
        get { return "$ " + valueInDouble1.ToString(); }
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

    public IList<Locations> CountryList
    {
        set
        {
            ddlCountry.DataSource = value;
            ddlCountry.DataTextField = Locations.Location.LocationName.ToString();
            ddlCountry.DataValueField = Locations.Location.LocationId.ToString();
            ddlCountry.DataBind();
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

    public IList<Locations> StateList
    {
        set
        {
            ddlState.Items.Clear();
            if (value.Count > 0)
            {
                ddlState.Enabled = true;
                ddlState.DataSource = value;
                ddlState.DataTextField = Locations.Location.LocationName.ToString();
                ddlState.DataValueField = Locations.Location.LocationId.ToString();
                ddlState.DataBind();
            }
            else
            {
                ddlState.Enabled = false;
            }
        }
    }

    public string SelectedCountry
    {
        get { return ddlCountry.SelectedValue.ToString(); }
        set { ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(value.ToString())); }
    }

    private static string stateForSignup;

    public string SelectedState
    {
        get
        {
            if (ddlState.SelectedIndex != -1)
            {
                stateForSignup = ddlState.SelectedValue.ToString();
                return stateForSignup;
            }
            else
                return stateForSignup;
        }
        set { stateForSignup = value; }
    }

    private bool _IsPercentage = false;

    public bool IsPercentage
    {
        get { return _IsPercentage; }
        set { _IsPercentage = value; }
    }

    private string _Denom = string.Empty;

    public string Denomination
    {
        get { return _Denom; }
        set { _Denom = value; }
    }

    public int UserID
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue =
                (SessionValue) stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                _UserID = objSessionvalue.UserId;
            }
            return _UserID;
        }
    }

    public string CVC
    {
        get { return TributePortalSecurity.Security.EncryptSymmetric(txtCCVerification.Text); }
        set
        {
            string CVCCode = TributePortalSecurity.Security.DecryptSymmetric(value);
            txtCCVerification.Attributes.Add("value", CVCCode);
        }
    }

    private static int state1 = -1;

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
        set { state1 = value; }
    }

    private int _PackageId;

    public int PackageId
    {
        get { return _PackageId; }
        set { _PackageId = value; }
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
        get
        {
            if (txtTributeAddressOther.Text.Length != 0)
                Domainname = txtTributeAddressOther.Text;
            else if (rdbAvailableAddress1.Checked)
                Domainname = rdbAvailableAddress1.Text.ToString().Replace("http://" + this._presenter.View.TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/", "");
            else if (rdbAvailableAddress2.Checked)
                Domainname = rdbAvailableAddress2.Text.ToString().Replace("http://" + this._presenter.View.TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/", "");
            else if (rdbAvailableAddress3.Checked)
                Domainname = rdbAvailableAddress3.Text.ToString().Replace("http://" + this._presenter.View.TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/", "");
            else
                Domainname = txtTributeAddress.Text;

            return Domainname;
        }
        set { _TributeURL = value; }
    }

    private Nullable<DateTime> dt1;

    public DateTime? EndDate
    {
        get
        {
            if (getPackageId == 5 || getPackageId == 7)
            {
                if (dt1 != null)
                {
                    if (dt1 < DateTime.Now)
                        return System.DateTime.Now.AddMonths(12);
                    else
                        return dt1.Value.AddMonths(12);
                }
                else
                    return System.DateTime.Now.AddMonths(12);
            }
            else if (getPackageId == 4 || getPackageId == 6)
            {
                return null;
            }
            else
                return dt1.Value.AddMonths(12);
        }
        set { dt1 = value; }
    }

    public int getPackageId
    {
        get
        {
            int _packageid = 0;
            //if (rdoPhotoMembershipYearly.Checked)
            //{
            //    _packageid = 7;
            //}
            if (rdoPhotoMembershipLifeTime.Checked)
            {
                _packageid = 6;
            }
            if (rdoTributeMembershipYearly.Checked)
            {
                _packageid = 5;
            }
            if (rdoTributeMembershipLifeTime.Checked)
            {
                _packageid = 4;
            }
            return _packageid;
        }
    }

    public int TributeId
    {
        get
        {
            StateManager stateTribure1 = StateManager.Instance;
            Tributes objTribute = (Tributes) stateTribure1.Get("TributeSession", StateManager.State.Session);
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
        get { return _tributeType; }
        set { _tributeType = value.ToString().ToLower().Replace(" ", ""); }
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
            bool _IsSponserHide = true;

            if (rdoAsGiftNo.Checked)
            {
                _IsSponserHide = false;
            }
            return _IsSponserHide;
        }
    }

    public bool IsSponsor
    {

        get
        {
            bool _IsSponser = true;

            if (rdoKnowYouNo.Checked)
            {
                _IsSponser = false;
            }
            return _IsSponser;
        }
    }

    public TributePackage TributePackageDetails
    {
        set
        {
            if (value.IsAutomaticRenew && !_isUserAdmin)
            {
                chooseoption.Visible = false;
            }

            StateManager objState = StateManager.Instance;
            objState.Add("TributePackageDetailsOnLoad", value, StateManager.State.Session);
        }
        get
        {
            StateManager objState = StateManager.Instance;
            return (TributePackage) objState.Get("TributePackageDetailsOnLoad", StateManager.State.Session);
        }
    }

    public bool IsUserAdmin
    {
        set { _isUserAdmin = value; }
    }


    public string SponsorEmailAddress
    {
        get { return txtEmailAddress.Text.Trim(); }
    }

    public int TransactionId
    {
        set
        {
            //Session["TransactionId"] = value.ToString();
            _transactionId = value;
        }
        get { return _transactionId; }
    }

    // Mohit Gupta 31 Jan Phase 2

    public string AdminOwner
    {
        get { return _adminOwner; }
        set { _adminOwner = value; }
    }

    public int AdminOwnerId
    {
        set { _adminOwnerId = value; }
        get { return _adminOwnerId; }
    }

    public System.Collections.Generic.IList<TributesPortal.BusinessEntities.UserInfo> OtherAdmins
    {
        set { _otherAdmins = value; }
        get { return _otherAdmins; }

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
                                                                 value.UserBusiness == null ? 1 : 2, "Basic",
                                                                 value.Users.IsUsernameVisiable
                    );
                StateManager stateManager = StateManager.Instance;
                stateManager.Add("objSessionvalue", _objSessionValue, StateManager.State.Session);
            }
            else
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }
        }
        get
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objValue = (SessionValue) stateManager.Get("objSessionvalue", StateManager.State.Session);
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

    public string Password
    {
        get
        {
            if (rdoAdminYes.Checked && rdoHaveAccountYes.Checked)
            {
                return TributePortalSecurity.Security.EncryptSymmetric(txtPassword.Text.ToString());
            }
            else
            {
                return TributePortalSecurity.Security.EncryptSymmetric(txtPasswordSignUp.Text.ToString());
            }

        }
    }

    public int TributeAdminCount
    {
        get { return _adminCount; }
        set { _adminCount = value; }
    }

    public int TributePackageId
    {
        set { _tributPackageId = value; }
        get { return _tributPackageId; }
    }

    public string UserEmail
    {
        get { return txtEmailSignUp.Text; }
    }

    public string chkAvailability
    {
        set { Status = value; }
    }

    private string[] NameAndMsg = new string[2];

    public string[] SponsorNameandMsgForEmail
    {
        get
        {
            if (rdoAsGiftYes.Checked && rdoKnowYouYes.Checked)
            {
                NameAndMsg[0] = txtNameForGift.Text;
                NameAndMsg[1] = txtMessage.Text;
                return NameAndMsg;
            }
            else
            {
                NameAndMsg[0] = NameAndMsg[1] = string.Empty;
                return NameAndMsg;
            }
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
        Tributes objTribute = (Tributes) stateTribure.Get("TributeSession", StateManager.State.Session);

        DateTime expiryDate = new DateTime();
        int cardExMonth = ddlCCMonth.SelectedIndex;
        int cardExYear = 0; // = txtCCYear.ToString();
        int.TryParse(txtCCYear.Text.ToString(), out cardExYear);


        //Phone number check
        bool IsValidPhone = false;
        bool checkPh1 = false;
        bool checkPh2 = false;
        bool checkPh3 = false;

        int TestPhoneNo = 0;
        checkPh1 = int.TryParse(txtPhoneNumber1.Text.ToString(), out TestPhoneNo);
        checkPh2 = int.TryParse(txtPhoneNumber2.Text.ToString(), out TestPhoneNo);
        checkPh3 = int.TryParse(txtPhoneNumber3.Text.ToString(), out TestPhoneNo);

        IsValidPhone = checkPh1 && checkPh2 && checkPh3;
        //Phone check till here

        DateTime currDate = DateTime.Now.Date;
        bool ValidExpiry = false;
        if ((cardExYear >= currDate.Year) && ((cardExYear == currDate.Year) ? (cardExMonth >= currDate.Month) ? true : false : true))
        {
            ValidExpiry = true;
        }

        if (((IsValidPhone) && (ValidExpiry)) || (PnlPaymentDetails.Visible == false))
        {

            if (chkAgree.Checked == true)
            {
                # region Upgrade

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

                    int couponType = 0;
                    if (rdoPhotoMembershipLifeTime.Checked || rdoTributeMembershipLifeTime.Checked)
                        couponType = 3;
                    else
                        couponType = 2;
                    if (rdoAdminYes.Checked)
                    {
                        if (rdoHaveAccountYes.Checked)
                        {
                            if ((txtEmail.Text != string.Empty) && (txtPassword.Text != string.Empty))
                            {
                                emailPassSuccess = this._presenter.GetUserDetails(txtEmail.Text, Password, 0);
                                if (emailPassSuccess == -1)
                                {
                                    ShowMessage(ValidationSummary1.HeaderText, "Enter valid email or password", true);
                                    return;
                                }
                                flagForAdminCndtns = 1;
                            }
                            else
                            {
                                ShowMessage(ValidationSummary1.HeaderText, "Either you have not entered the email or the password", true);
                                return;
                            }
                        }
                        if (rdoHaveAccountNo.Checked)
                        {
                            if (txtEmailSignUp.Text != string.Empty)
                            {
                                _email = _presenter.EmailAvailable();
                                if (_email > 0)
                                {
                                    ShowMessage(ValidationSummary1.HeaderText, "Email you entered already exists!! Please enter a different email", true);
                                    return;
                                }
                            }

                            if ((txtEmailSignUp.Text == string.Empty) && (txtPasswordSignUp.Text == string.Empty) && (txtPasswordSignUp.Text == string.Empty) && (txtConfrmPassword.Text == string.Empty) && (txtFirstName.Text == string.Empty))
                            {
                                ShowMessage(ValidationSummary1.HeaderText, "You have not entered the complete Sign up info", true);
                                return;
                            }


                            if ((txtPasswordSignUp.Text != string.Empty) && (txtConfrmPassword.Text != string.Empty))
                            {
                                if (txtPasswordSignUp.Text != txtConfrmPassword.Text)
                                {
                                    ShowMessage(ValidationSummary1.HeaderText, "Password fields do not match", true);
                                    return;
                                }
                                if (txtPasswordSignUp.Text.Length < 6)
                                {
                                    ShowMessage(ValidationSummary1.HeaderText, "Password should be at least of 6 digits", true);
                                    return;
                                }
                            }
                            else
                            {
                                ShowMessage(ValidationSummary1.HeaderText, "Please enter password fields", true);
                                return;
                            }

                            flagForAdminCndtns = 2;
                        }
                    }


                    // Custom URL is mandatory when the upgrade 
                    if (rdoTributeMembershipYearly.Checked || rdoTributeMembershipLifeTime.Checked)
                    {
                        if (txtTributeAddress.Visible == true)
                        {
                            if (txtTributeAddress.Text != string.Empty)
                            {
                                this._presenter.CheckAvailability();
                                if (int.Parse(Status) != 0)
                                {
                                    ShowMessage(ValidationSummary1.HeaderText, "Please enter a different URL", true);
                                    return;
                                }
                            }
                        }
                    }

                    if (txtCouponCode.Text != string.Empty)
                    {
                        int availability = _presenter.GetCouponAvailable(txtCouponCode.Text, couponType);

                        if (availability == 1)
                        {
                            //SetCouponAvailableStatus();
                            double Couponamount = 0;
                            if (rdoPhotoMembershipLifeTime.Checked)
                            {
                                amount = BillingTotalInDollars;
                                Couponamount = Convert.ToDouble(amount.Substring(1, amount.Length - 1));
                            }
                            else if (rdoTributeMembershipYearly.Checked)
                            {
                                amount = BillingTotalInDollars;
                                Couponamount = Convert.ToDouble(amount.Substring(1, amount.Length - 1));
                            }
                            else
                            {
                                amount = BillingTotalInDollars;
                                Couponamount = Convert.ToDouble(amount.Substring(1, amount.Length - 1));
                            }
                            if (this._presenter.View.IsPercentage == false)
                                Couponamount = Couponamount - double.Parse(this._presenter.View.Denomination);
                            else
                                Couponamount = Couponamount -
                                               ((double.Parse(this._presenter.View.Denomination)*Couponamount)/100);
                            if (Couponamount < 0)
                                Couponamount = 0;
                            //LHK; coupon amount upto 2 decimal places.
                            Couponamount = Math.Round(Couponamount, 2);
                            BillingTotal.InnerHtml = BillingTotalAbove.InnerHtml = ("$" + Couponamount.ToString());

                            //Remove Commented code for Payment Gateway :Amit :2/5/8
                            SessionValue objSessionmail =
                                (SessionValue) statemail.Get("objSessionvalue", StateManager.State.Session);
                            if (objSessionmail != null)
                            {
                                Firstname = objSessionmail.FirstName;
                                LastName = objSessionmail.LastName;
                                UserMail = objSessionmail.UserEmail;
                            }
                            PaymentGateWay objPay = new PaymentGateWay();
                            //Start - Modification on 15-Dec-09 for the enhancement 1 of the Phase 1
                            //If the bill amount (Couponamount) is greater than $0, then only the transaction should go to the payment gateway
                            if (Couponamount > 0)
                            {
                                if (Couponamount == Math.Round(Couponamount))
                                {
                                    sBeanStreamResponce = PayYourBill(TributePortalSecurity.Security.DecryptSymmetric(this._presenter.View.CreditCardNo), txtCCVerification.Text, int.Parse(ddlCCMonth.SelectedValue), int.Parse(txtCCYear.Text), Couponamount, SelectCreditCardType(), txtCCName.Text.Trim(), "", _presenter.View.Address.Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay), txtCCCity.Text, StateV1.CA, CountryV1.US, txtCCZipCode.Text, _presenter.View.Telephone.ToString(), txtEmailAddress.Text.Trim(), HttpContext.Current.Request.UserHostAddress.ToString(), out confirmationId, out errorMesg, out _transaction);
                                }
                                else
                                {
                                    sBeanStreamResponce = PayYourBill(TributePortalSecurity.Security.DecryptSymmetric(this._presenter.View.CreditCardNo), txtCCVerification.Text, int.Parse(ddlCCMonth.SelectedValue), int.Parse(txtCCYear.Text), double.Parse(Couponamount.ToString()), SelectCreditCardType(), txtCCName.Text.Trim(), "", _presenter.View.Address.Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay), txtCCCity.Text, StateV1.CA, CountryV1.US, txtCCZipCode.Text, _presenter.View.Telephone.ToString(), txtEmailAddress.Text.Trim(), HttpContext.Current.Request.UserHostAddress.ToString(), out confirmationId, out errorMesg, out _transaction);
                                }
                            }
                            else
                                _transaction = true;
                            //End

                            if (_transaction)
                            {
                                if (!Equals(objTribute, null))
                                {
                                    _TribureId = objTribute.TributeId;
                                    _tributeUrl = objTribute.TributeUrl;
                                    _tributeType = objTribute.TypeDescription;
                                    this._presenter.TriputePackageInfo(TributeId);
                                    if (PnlPaymentDetails.Visible == true)
                                    {
                                        this._presenter.InsertCCDetails((SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session), objTribute, confirmationId, SponsorNameandMsgForEmail);
                                    }
                                    else
                                    {
                                        //MG using 100% coupon off
                                        this._presenter.SendSponsorEmailOnFreeUpgrade((SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session), objTribute, confirmationId, SponsorNameandMsgForEmail);
                                        this._presenter.InsertPackageDetails(objTribute.TributeId, 0, confirmationId);
                                    }
                                }

                                linkedVideoTributeId = this._presenter.GetLinkedVideoTributeId(objTribute.TributeId, UserID);
                                _videoTributeOwnerid = _presenter.GetUserIdByTributeId(linkedVideoTributeId);

                                if (linkedVideoTributeId > 0 && _videoTributeOwnerid != UserID)
                                {
                                    _presenter.UpdateCreditPointOfVideoTributeOwner(_videoTributeOwnerid);
                                }

                                if (flagForAdminCndtns == 1)
                                {
                                    if ((txtEmail.Text != string.Empty) || (txtPassword.Text != string.Empty))
                                    {
                                        emailPassSuccess = this._presenter.GetUserDetails(txtEmail.Text, Password, _TribureId);
                                        SendEmailtoNewAdmin();
                                    }
                                    else
                                    {
                                        ShowMessage(ValidationSummary1.HeaderText, "Please enter both email and password", true);
                                    }
                                }
                                else if (flagForAdminCndtns == 2)
                                {
                                    if (_email == 0)
                                    {
                                        UserRegistration objUserReg = SaveAccount();
                                        _presenter.SavePersonalAccount(objUserReg);
                                        emailPassSuccess = this._presenter.GetUserDetails(txtEmailSignUp.Text, Password, _TribureId);
                                        SendEmailtoNewAdmin();
                                    }
                                }

                                if (rdoTributeMembershipLifeTime.Checked || rdoTributeMembershipYearly.Checked)
                                {
                                    if (rdoChangeAddressYes.Checked)
                                    {
                                        if (TributeURL != null)
                                        {
                                            this._presenter.UpdateTributeURL(_TribureId, TributeURL);
                                            CreateFoldersForNewTributeUrl(_TribureId, OldTributeURL, TributeURL, _tributeType);
                                        }
                                    }
                                }

                                if (!string.IsNullOrEmpty(txtCouponCode.Text))
                                    this._presenter.UpdateUsedCouponDetails(txtCouponCode.Text);


                                SetDefault();
                                if (Request.QueryString["PageName"] == "AdminMytributesPrivacy")
                                {
                                    Response.Redirect(Session["APP_BASE_DOMAIN"] + objTribute.TributeUrl + "/paymentconfirmation.aspx?tid=" + _tributPackageId.ToString() + "&PageName=" + Request.QueryString["PageName"].ToString(), false);
                                }
                                else
                                {
                                    try
                                    {
                                        //Start - Modification on 16-Dec-09 for the enhancement 1 of the Phase 1
                                        //If the amount is zero the application is redirected to the Tribute page
                                        if (Couponamount == 0)
                                            Response.Redirect(Session["APP_BASE_DOMAIN"] + objTribute.TributeUrl + "/", false);
                                        //End
                                        if (WebConfig.ApplicationMode.ToLower() == "local")
                                        {
                                            Response.Redirect(
                                                Session["APP_BASE_DOMAIN"] + objTribute.TributeUrl +
                                                "/paymentconfirmation.aspx?tid=" + _tributPackageId.ToString(), false);
                                        }
                                        else
                                        {
                                            Response.Redirect(
                                                "http://" +
                                                this._presenter.View.SubDomain.Replace("New Baby", "newbaby").ToLower() +
                                                "." + WebConfig.TopLevelDomain + "/" + objTribute.TributeUrl +
                                                "/paymentconfirmation.aspx?tid=" + _tributPackageId.ToString(),false);
                                        }

                                    }
                                    catch (Exception po)
                                    {
                                    }
                                }

                                bool result = AddMailChimpSubscriber(initailPackageId, getPackageId);
                            }
                            else
                            {
                                var sResponseArr = sBeanStreamResponce.Split('&');
                                var sErrorMsg = sResponseArr.Length > 3 && sResponseArr[3].Split('=').Length > 1 && !string.IsNullOrEmpty(sResponseArr[3].Split('=')[1]) ? sResponseArr[3].Split('=')[1].Replace("+", " ") : "";
                                //LHK:17-11-2011- for gracefull error message display.
                                sErrorMsg = HttpUtility.UrlDecode(sErrorMsg);
                                ShowMessage(ValidationSummary1.HeaderText, sErrorMsg, true);

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
                        double Couponamount = 0;

                        amount = BillingTotal.InnerHtml;
                        Couponamount = Convert.ToDouble(amount.Substring(1, amount.Length - 1));
                        BillingTotal.InnerHtml = BillingTotalAbove.InnerHtml = amount;

                        //Remove Commented code for Payment Gateway :Amit :2/5/8
                        SessionValue objSessionmail = (SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session);
                        if (objSessionmail != null)
                        {
                            Firstname = objSessionmail.FirstName;
                            LastName = objSessionmail.LastName;
                            UserMail = objSessionmail.UserEmail;
                        }
                        PaymentGateWay objPay = new PaymentGateWay();
                        if (Couponamount == Math.Round(Couponamount))
                        {
                            sBeanStreamResponce = objPay.PayYourBill(TributePortalSecurity.Security.DecryptSymmetric(this._presenter.View.CreditCardNo), txtCCVerification.Text, int.Parse(ddlCCMonth.SelectedValue), int.Parse(txtCCYear.Text), Couponamount, SelectCreditCardType(), txtCCName.Text, "", _presenter.View.Address.Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay), txtCCCity.Text, StateV1.CA, CountryV1.US, txtCCZipCode.Text, _presenter.View.Telephone.ToString(), txtEmailAddress.Text.Trim(), HttpContext.Current.Request.UserHostAddress.ToString(), out confirmationId, out errorMesg, out _transaction);
                        }
                        else
                        {
                            sBeanStreamResponce = objPay.PayYourBill(TributePortalSecurity.Security.DecryptSymmetric(this._presenter.View.CreditCardNo), txtCCVerification.Text, int.Parse(ddlCCMonth.SelectedValue), int.Parse(txtCCYear.Text), double.Parse(Couponamount.ToString()), SelectCreditCardType(), txtCCName.Text, "", _presenter.View.Address.Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay), txtCCCity.Text, StateV1.CA, CountryV1.US, txtCCZipCode.Text, _presenter.View.Telephone.ToString(), txtEmailAddress.Text.Trim(), HttpContext.Current.Request.UserHostAddress.ToString(), out confirmationId, out errorMesg, out _transaction);
                        }


                        if (_transaction)
                        {
                            #region Transaction True

                            if (!Equals(objTribute, null))
                            {
                                _TribureId = objTribute.TributeId;
                                _tributeUrl = objTribute.TributeUrl;
                                _tributeType = objTribute.TypeDescription;
                                this._presenter.TriputePackageInfo(TributeId);
                                this._presenter.InsertCCDetails((SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session), objTribute, confirmationId, SponsorNameandMsgForEmail);
                            }

                            linkedVideoTributeId = this._presenter.GetLinkedVideoTributeId(objTribute.TributeId, UserID);
                            _videoTributeOwnerid = _presenter.GetUserIdByTributeId(linkedVideoTributeId);

                            if (linkedVideoTributeId > 0 && _videoTributeOwnerid != UserID)
                            {

                                _presenter.UpdateCreditPointOfVideoTributeOwner(_videoTributeOwnerid);
                            }


                            if (flagForAdminCndtns == 1)
                            {
                                if ((txtEmail.Text != string.Empty) || (txtPassword.Text != string.Empty))
                                {
                                    emailPassSuccess = this._presenter.GetUserDetails(txtEmail.Text, Password, _TribureId);
                                    SendEmailtoNewAdmin();
                                }
                                else
                                {
                                    ShowMessage(ValidationSummary1.HeaderText, "Please enter both email and password", true);
                                }
                            }
                            else if (flagForAdminCndtns == 2)
                            {
                                if (_email == 0)
                                {
                                    UserRegistration objUserReg = SaveAccount();
                                    _presenter.SavePersonalAccount(objUserReg);
                                    emailPassSuccess = this._presenter.GetUserDetails(txtEmailSignUp.Text, Password, _TribureId);
                                    SendEmailtoNewAdmin();
                                }
                            }

                            if (rdoTributeMembershipLifeTime.Checked || rdoTributeMembershipYearly.Checked)
                            {

                                if (rdoChangeAddressYes.Checked)
                                {
                                    if (TributeURL != null)
                                    {
                                        this._presenter.UpdateTributeURL(_TribureId, TributeURL);
                                        //Copying all the data of existing TributeUrl for new TributeURL
                                        CreateFoldersForNewTributeUrl(_TribureId, OldTributeURL, TributeURL, _tributeType);
                                    }
                                }
                            }
                            SetDefault();
                            if (Request.QueryString["PageName"] == "AdminMytributesPrivacy")
                            {
                                Response.Redirect(Session["APP_BASE_DOMAIN"] + objTribute.TributeUrl + "/paymentconfirmation.aspx?tid=" + _tributPackageId.ToString() + "&PageName=" + Request.QueryString["PageName"].ToString(), false);
                            }
                            else
                            {
                                try
                                {
                                    if (WebConfig.ApplicationMode.ToLower() == "local")
                                    {
                                        Response.Redirect(
                                            Session["APP_BASE_DOMAIN"] + objTribute.TributeUrl +
                                            "/paymentconfirmation.aspx?tid=" + _tributPackageId.ToString(), false);
                                    }
                                    else
                                    {
                                        Response.Redirect(
                                            "http://" +
                                            _tributeType.Replace("New Baby", "newbaby").ToLower() +
                                            "." + WebConfig.TopLevelDomain + "/" + _tributeUrl +
                                            "/paymentconfirmation.aspx?tid=" + _tributPackageId.ToString(),false);
                                    }
                                }
                                catch (Exception abc)
                                {
                                }
                            }
                        }
                        else
                        {
                            //ShowMessage(ValidationSummary1.HeaderText, errorMesg, true);
                            var sResponseArr = sBeanStreamResponce.Split('&');
                            var sErrorMsg = sResponseArr.Length > 3 && sResponseArr[3].Split('=').Length > 1 && !string.IsNullOrEmpty(sResponseArr[3].Split('=')[1]) ? sResponseArr[3].Split('=')[1].Replace("+", " ") : "";

                            sErrorMsg = HttpUtility.UrlDecode(sErrorMsg);

                            ShowMessage(ValidationSummary1.HeaderText, sErrorMsg, true);
                        }

                        #endregion
                    }

                }
                catch (Exception excep)
                {
                    if (excep.Message.StartsWith("PAYMENT"))
                        ShowMessage(ValidationSummary1.HeaderText, "Transaction Failed! An error has occured while trying to connect to the payment gateway. Please try later.", true);
                    else
                        ShowMessage(ValidationSummary1.HeaderText, "Transaction Failed! While your transaction was successful and your credit card was charged but we could not process it at our end. Please contact the webmaster.", true);
                }
                # endregion
            }
            else
            {
                ShowMessage(ValidationSummary1.HeaderText, "Please accept that you have read and agree to the terms of use, cancellation/refund policy and the privacy policy.", true);
                ErrorAccept.Visible = true;
            }
        }

        else
        {
            if (!IsValidPhone)
            {
                ShowMessage(ValidationSummary1.HeaderText, "Please enter valid phone number. Format should be XXX-XXX-XXXX..", true);
            }
            ShowMessage(ValidationSummary1.HeaderText, "Please enter a valid Expiry date", true);
            error1.Visible = true;
        }

    }

    //Mohit Gupta  4 Feb 2011
    private UserRegistration SaveAccount()
    {

        //int Usertype = 1;
        //if (rdoBusinessAccount.Checked)
        //    Usertype = 2;
        int Usertype = 1;
        UserRegistration objUserReg = new UserRegistration();
        int state = -1;
        if (ddlState.SelectedValue.ToString() != "")
        {
            state = int.Parse(ddlState.SelectedValue.ToString());
        }

        string _Pass = TributePortalSecurity.Security.EncryptSymmetric(txtPasswordSignUp.Text.ToLower().ToString());
        string _UserImage = "images/bg_ProfilePhoto.gif";
        //verfication code has been set blank, we are using recaptcha control
        Nullable<Int64> _FacebookUid = null;

        var fbWebContext = FacebookWebContext.Current;
        if (FacebookWebContext.Current.Session != null)
        {
            _FacebookUid = fbWebContext.UserId;
            try
            {
                var fbwc = new FacebookWebClient(FacebookWebContext.Current.AccessToken);
                string fql = "Select pic_square from user where uid = " + fbWebContext.UserId;
                JsonArray me2 = (JsonArray) fbwc.Query(fql);
                var mm = (IDictionary<string, object>) me2[0];
                if (!string.IsNullOrEmpty((string) mm["pic_square"]))
                {
                    _UserImage = (string) mm["pic_square"]; // get user image
                }
            }
            catch (Exception ex)
            {
            }
        }

        TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users(
            string.Empty, //txtUsername.Text.Trim(),                 
            _Pass,
            txtFirstName.Text.ToString(),
            txtLastName.Text.ToString(),
            txtEmailSignUp.Text.ToString(),
            "",
            false,
            string.Empty, // txtCity.Text.ToString(),
            state,
            int.Parse(ddlCountry.SelectedValue.ToString()),
            Usertype, _FacebookUid
            );
        objUsers.ApplicationType = ApplicationType;
        objUsers.UserImage = _UserImage;
        objUserReg.Users = objUsers;
        return objUserReg;
    }

    protected void lbtnValidateCoupon_Click(object sender, EventArgs e)
    {
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
            StateManager stateManager = StateManager.Instance;
            SessionValue objValue = (SessionValue) stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objValue != null)
            {
                if (Request.QueryString["PageName"] == "AdminMytributesPrivacy")
                {
                    // Added by Ashu on Oct 4, 2011 for rewrite URL 
                    if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                        Response.Redirect(Session["APP_BASE_DOMAIN"] + "adminMyMomentsprivacy.aspx", false);
                    else
                        Response.Redirect(Session["APP_BASE_DOMAIN"] + "MyHome/adminmytributesprivacy.aspx", false);

                    Session["Sentby"] = "TributeSponsor";
                }
                else
                {
                    Response.Redirect("http://" + this._presenter.View.SubDomain.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + this._presenter.View.TributeURL);
                }
            }
            else
            {
                Response.Redirect(Session["APP_BASE_DOMAIN"] + "log_in.aspx", false);
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

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.GetStateListForSignUp();
    }


    #endregion << Events>>

    protected void rdoPhotoMembershipYearly_CheckedChanged(object sender, EventArgs e)
    {
        BillingTotal.InnerHtml =
            BillingTotalAbove.InnerHtml =
            "$ " + Convert.ToString(rdoPhotoMembershipYearly.Text.ToString().Remove(0, 2));
        valueInDouble1 = Convert.ToDouble(rdoTributeMembershipYearly.Text.ToString().Remove(0, 2));
        PnlPaymentDetails.Visible = true;
        spanCoupon.Visible = false;
        DivRenew.Visible = false;
        PanelChangeAddress.Visible = true;
        PanelTributeAddress.Visible = true;
        rdoChangeAddressYes.Checked = true;
        rdoChangeAddressNo.Checked = false;
        pnlTributeAddressAvailable.Visible = false;
        TributePackage objTributePackage = new TributePackage();
        objTributePackage = TributePackageDetails;
        if (objTributePackage.PackageId != null)
        {
            if (objTributePackage.PackageId == 5)
            {
                PanelTributeAddress.Visible = false;
                PanelChangeAddress.Visible = false;
                pnlTributeAddressAvailable.Visible = false;
            }
        }
    }

    protected void rdoTributeMembershipYearly_CheckedChanged(object sender, EventArgs e)
    {
        BillingTotal.InnerHtml =
            BillingTotalAbove.InnerHtml =
            "$ " + Convert.ToString(rdoTributeMembershipYearly.Text.ToString().Remove(0, 2));
        valueInDouble1 = Convert.ToDouble(rdoTributeMembershipYearly.Text.ToString().Remove(0, 2));
        PnlPaymentDetails.Visible = true;
        spanCoupon.Visible = false;
        DivRenew.Visible = false;
        PanelChangeAddress.Visible = true;
        PanelTributeAddress.Visible = true;
        rdoChangeAddressYes.Checked = true;
        rdoChangeAddressNo.Checked = false;
        pnlTributeAddressAvailable.Visible = false;
        TributePackage objTributePackage = new TributePackage();
        objTributePackage = TributePackageDetails;
        if (objTributePackage.PackageId != null)
        {
            if (objTributePackage.PackageId == 5)
            {
                PanelTributeAddress.Visible = false;
                PanelChangeAddress.Visible = false;
                pnlTributeAddressAvailable.Visible = false;
            }
        }
    }

    protected void rdoPhotoMembershipLifeTime_CheckedChanged(object sender, EventArgs e)
    {
        rdoChangeAddressYes.Checked = true;
        BillingTotal.InnerHtml =
            BillingTotalAbove.InnerHtml =
            "$ " + Convert.ToString(rdoPhotoMembershipLifeTime.Text.ToString().Remove(0, 2));
        valueInDouble1 = Convert.ToDouble(rdoPhotoMembershipLifeTime.Text.ToString().Remove(0, 2));
        PnlPaymentDetails.Visible = true;
        spanCoupon.Visible = false;
        DivRenew.Visible = false;
        PanelTributeAddress.Visible = false;
        PanelChangeAddress.Visible = false;
        pnlTributeAddressAvailable.Visible = false;
    }

    protected void rdoTributeMembershipLifeTime_CheckedChanged(object sender, EventArgs e)
    {

        BillingTotal.InnerHtml =
            BillingTotalAbove.InnerHtml =
            "$ " + Convert.ToString(rdoTributeMembershipLifeTime.Text.ToString().Remove(0, 2));
        valueInDouble1 = Convert.ToDouble(rdoTributeMembershipLifeTime.Text.ToString().Remove(0, 2));
        PnlPaymentDetails.Visible = true;
        spanCoupon.Visible = false;
        DivRenew.Visible = false;
        PanelChangeAddress.Visible = true;
        PanelTributeAddress.Visible = true;
        rdoChangeAddressYes.Checked = true;
        rdoChangeAddressNo.Checked = false;
        pnlTributeAddressAvailable.Visible = false;
        TributePackage objTributePackage = new TributePackage();
        objTributePackage = TributePackageDetails;
        if (objTributePackage.PackageId != null)
        {
            if (objTributePackage.PackageId == 5)
            {
                PanelTributeAddress.Visible = false;
                PanelChangeAddress.Visible = false;
                pnlTributeAddressAvailable.Visible = false;
            }
        }
    }

    protected void rdoChangeAddressYes_CheckedChanged(object sender, EventArgs e)
    {
        PanelTributeAddress.Visible = true;

    }

    protected void rdoChangeAddressNo_CheckedChanged(object sender, EventArgs e)
    {
        PanelTributeAddress.Visible = false;
    }

    protected void rdoAdminYes_CheckedChanged(object sender, EventArgs e)
    {
        haveAccountFields.Visible = true;
        rdoHaveAccountYes.Checked = rdoHaveAccountNo.Checked = false;
        loginInfoFields.Visible = SignUpFields.Visible = NameMsgFields.Visible = false;
    }

    protected void rdoAdminNo_CheckedChanged(object sender, EventArgs e)
    {
        rdoHaveAccountYes.Checked = rdoHaveAccountNo.Checked = false;
        haveAccountFields.Visible = loginInfoFields.Visible = SignUpFields.Visible = NameMsgFields.Visible = false;
    }

    protected void rdoHaveAccountYes_CheckedChanged(object sender, EventArgs e)
    {
        loginInfoFields.Visible = true;
        SignUpFields.Visible = NameMsgFields.Visible = false;
        txtEmail.Text = txtPassword.Text = string.Empty;

    }

    protected void rdoHaveAccountNo_CheckedChanged(object sender, EventArgs e)
    {
        BecomeFieldsAdmin.Visible = SignUpFields.Visible = true;
        loginInfoFields.Visible = NameMsgFields.Visible = false;
        txtEmailSignUp.Text =
            txtPasswordSignUp.Text = txtConfrmPassword.Text = txtFirstName.Text = txtLastName.Text = string.Empty;
    }


    protected void rdoAsGiftYes_CheckedChanged(object sender, EventArgs e)
    {
        knowIdentityFields.Visible = true;
        rdoKnowYouNo.Checked = rdoKnowYouYes.Checked = false;
        haveAccountFields.Visible = loginInfoFields.Visible = SignUpFields.Visible = BecomeFieldsAdmin.Visible = false;
    }

    protected void rdoAsGiftNo_CheckedChanged(object sender, EventArgs e)
    {
        haveAccountFields.Visible =
            loginInfoFields.Visible =
            SignUpFields.Visible = BecomeFieldsAdmin.Visible = knowIdentityFields.Visible = false;
    }

    protected void rdoKnowYouYes_CheckedChanged(object sender, EventArgs e)
    {
        NameMsgFields.Visible = true;
        haveAccountFields.Visible = loginInfoFields.Visible = SignUpFields.Visible = BecomeFieldsAdmin.Visible = false;
    }

    protected void rdoKnowYouNo_CheckedChanged(object sender, EventArgs e)
    {
        haveAccountFields.Visible =
            loginInfoFields.Visible = SignUpFields.Visible = BecomeFieldsAdmin.Visible = NameMsgFields.Visible = false;
    }

    protected void lbtncheckAddress_Click(object sender, EventArgs e)
    {
        errorAddress.Visible = false;
        lblErrMsg.Visible = false;
        StringBuilder Script = new StringBuilder();
        Script.Append("<script>");
        Script.Append("var notice = $('ctl00_TributePlaceHolder_SpanAvailable');");
        Script.Append("</script>");
        this._presenter.CheckAvailability();


        if (int.Parse(Status) != 0)
        {
            pnlTributeAddressAvailable.Visible = true;
            SetAvailableAddresstext_();
            //lbtncheckAddress.Visible = false;
            txtTributeAddress.Enabled = false;
            SetImageUnavailableStatus();
        }
        else
        {
            SetImageAvailableStatus();

        }
        SpanAvailable.Visible = true;
    }

    private void SetTributeUrlname()
    {

        StateManager objtributeType = StateManager.Instance;
        if (objtributeType.Get("TributeType", StateManager.State.Session) != null)
        {
            _tributeType = objtributeType.Get("TributeType", StateManager.State.Session).ToString().ToLower().Replace(" ", ""); ;
        }
        else
            RedirectToLoginPage();


    }

    private void SetAvailableAddresstext_()
    {
        StateManager objtributeType = StateManager.Instance;
        if (_tributeType == null)
        {
            RedirectToLoginPage();
        }


        rdbAvailableAddress1.Text = "http://" + _tributeType + "." + WebConfig.TopLevelDomain + "/" + this._presenter.SequenceTributeName(txtTributeAddress.Text, _tributeType);
        rdbAvailableAddress2.Text = "http://" + _tributeType + "." + WebConfig.TopLevelDomain + "/" + _tributeType.ToString().Replace(" ", "") + DateTime.Now.Year + txtTributeAddress.Text;
        rdbAvailableAddress3.Text = "http://" + _tributeType + "." + WebConfig.TopLevelDomain + "/" + txtTributeAddress.Text + DateTime.Now.Year.ToString();
    }

    private void SetImageUnavailableStatus()
    {
        SpanAvailable.Attributes.Add("class", "availabilityNotice-Unavailable");
        SpanAvailable.InnerHtml = "Unavailable";
    }

    private void SetImageAvailableStatus()
    {
        SpanAvailable.Attributes.Add("class", "availabilityNotice-Available");
        SpanAvailable.InnerHtml = "Available!";
    }

    protected void rdbAvailableAddress1_CheckedChanged(object sender, EventArgs e)
    {
        txtTributeAddressOther.Text = string.Empty;

    }

    protected void rdbAvailableAddress2_CheckedChanged(object sender, EventArgs e)
    {
        txtTributeAddressOther.Text = string.Empty;

    }

    protected void rdbAvailableAddress3_CheckedChanged(object sender, EventArgs e)
    {
        txtTributeAddressOther.Text = string.Empty;

    }

    protected void lbtncheckAvailability_Click(object sender, EventArgs e)
    {
        StringBuilder Script = new StringBuilder();
        lblErrMsg.Visible = false;
        if (!string.IsNullOrEmpty(txtTributeAddressOther.Text))
        {
            this._presenter.CheckAvailability();
            if (int.Parse(Status) != 0)
            {
                imgMsgStatus2.Attributes.Add("class", "availabilityNotice-Unavailable");
                imgMsgStatus2.InnerHtml = "Unavailable";

            }
            else
            {
                imgMsgStatus2.Attributes.Add("class", "availabilityNotice-Available");
                imgMsgStatus2.InnerHtml = "Available!";
            }
            imgMsgStatus2.Visible = true;
        }
    }

    private void SetUpgradeOptions(int tributePackageId, DateTime? endDate)
    {
        PnlPaymentDetails.Visible = true;
        spanCoupon.Visible = false;
        DivRenew.Visible = false;
        bool videoCount = IsContainsVideo;
        if (tributePackageId == 8 || tributePackageId == 3)
        {
            rdoPhotoMembershipLifeTime.Text = " " + WebConfig.PhotoOneyearAmount;
            rdoTributeMembershipYearly.Text = " " + WebConfig.TributeOneyearAmount;
            rdoTributeMembershipLifeTime.Text = " " + WebConfig.TributeLifeTimeAmount;
            
            //Setting initial value of green Billing total when the tribute first loads
            valueInDouble1 =Convert.ToDouble(WebConfig.PhotoOneyearAmount.Substring(1, WebConfig.PhotoOneyearAmount.Length - 1));
            BillingTotal.InnerHtml = BillingTotalAbove.InnerHtml = "$ " + valueInDouble1.ToString("#,0.00");


            if(videoCount)
            {
                rdoPhotoMembershipYearly.Visible = true;
                rdoPhotoMembershipYearly.Text = " " + WebConfig.PhotoOneyearAmount;
                rdoPhotoMembershipLifeTime.Text = " " + WebConfig.PhotoLifeTimeAmount;
                rdoTributeMembershipYearly.Text = " " + WebConfig.TributeOneyearAmount;
                rdoTributeMembershipLifeTime.Text = " " + WebConfig.TributeLifeTimeAmount;
                rdoPhotoMembershipYearly.Checked = true;
                //Setting initial value of green Billing total when the tribute first loads
                valueInDouble1 = Convert.ToDouble(WebConfig.PhotoOneyearAmount.Substring(1, WebConfig.PhotoOneyearAmount.Length - 1));
                BillingTotal.InnerHtml = BillingTotalAbove.InnerHtml = "$ " + valueInDouble1.ToString("#,0.00");
                               
            }
            else
            {
                rdoPhotoMembershipLifeTime.Checked = true;
            }
        }

        else if (tributePackageId == 7)
        {
            rdoPhotoMembershipLifeTime.Text = " " + WebConfig.PhotoOneyearAmount;
            rdoTributeMembershipYearly.Text = " " + WebConfig.TributeOneyearAmount;
            rdoTributeMembershipLifeTime.Text = " " + WebConfig.TributeLifeTimeAmount;

            if (endDate < DateTime.Now)
            {
                rdoPhotoMembershipLifeTime.Text = " " + WebConfig.PhotoLifeTimeAmount;
                rdoTributeMembershipLifeTime.Text = " " + WebConfig.TributeLifeTimeAmount;
            }

            //Setting initial value of green Billing total when the tribute first loads
            valueInDouble1 =
                Convert.ToDouble(WebConfig.PhotoOneyearAmount.Substring(1, WebConfig.PhotoOneyearAmount.Length - 1));
            BillingTotal.InnerHtml = BillingTotalAbove.InnerHtml = "$ " + valueInDouble1.ToString("#,0.00");
            midColumn1.Visible =
                midColumn2.Visible =
                midColumn3.Visible =
                midColumn4.Visible =
                midColumn5.Visible =
                midColumn6.Visible =
                midColumn7.Visible =
                midColumn8.Visible =
                midColumn9.Visible =
                midColumn10.Visible =
                midColumn11.Visible =
                midColumn12.Visible =
                midColumn13.Visible = midColumn14.Visible = secondLastRow.Visible = midColumn16.Visible = true;
            rdoTributeMembershipYearly.Enabled = true;

            if (videoCount)
            {
                rdoPhotoMembershipYearly.Visible = true;
                rdoPhotoMembershipYearly.Text = " " + WebConfig.PhotoOneyearAmount;
                rdoPhotoMembershipLifeTime.Text = " " + WebConfig.PhotoLifeTimeAmount;
                rdoTributeMembershipYearly.Text = " " + WebConfig.TributeOneyearAmount;
                rdoTributeMembershipLifeTime.Text = " " + WebConfig.TributeLifeTimeAmount;

                midColumn1.Visible =
                                        midColumn2.Visible =
                                        midColumn3.Visible =
                                        midColumn4.Visible =
                                        midColumn5.Visible =
                                        midColumn6.Visible =
                                        midColumn7.Visible =
                                        midColumn8.Visible =
                                        midColumn9.Visible =
                                        midColumn10.Visible =
                                        midColumn11.Visible =
                                        midColumn12.Visible =
                                        midColumn13.Visible = midColumn14.Visible = secondLastRow.Visible = midColumn16.Visible = true;
                rdoTributeMembershipYearly.Enabled = true;

                rdoPhotoMembershipYearly.Checked = true;
                //Setting initial value of green Billing total when the tribute first loads
                valueInDouble1 = Convert.ToDouble(WebConfig.PhotoOneyearAmount.Substring(1, WebConfig.PhotoOneyearAmount.Length - 1));
                BillingTotal.InnerHtml = BillingTotalAbove.InnerHtml = "$ " + valueInDouble1.ToString("#,0.00");

            }
            else
            {
                rdoPhotoMembershipLifeTime.Checked = true;
            }
        }
        else if (tributePackageId == 6)
        {
            rdoPhotoMembershipLifeTime.Text = " " + WebConfig.PhotoLifeTimeAmount;

            rdoTributeMembershipYearly.Text = " " + WebConfig.TributeOneyearAmount;
            rdoTributeMembershipLifeTime.Text = " " + WebConfig.TributeLifeTimeAmount;

            // If the Photo Tribute Life type is Tribute Yearly then display only the Tribute Column options in the upgrade page
            midColumn1.Visible =
                midColumn2.Visible =
                midColumn3.Visible =
                midColumn4.Visible =
                midColumn5.Visible =
                midColumn6.Visible =
                midColumn7.Visible =
                midColumn8.Visible =
                midColumn9.Visible =
                midColumn10.Visible =
                midColumn11.Visible =
                midColumn12.Visible =
                midColumn13.Visible = midColumn14.Visible = secondLastRow.Visible = midColumn16.Visible = false;
            rdoTributeMembershipYearly.Enabled = true;

            rdoTributeMembershipLifeTime.Checked = true;

            //Setting initial value of green Billing total when the tribute first loads
            valueInDouble1 =
                Convert.ToDouble(WebConfig.TributeLifeTimeAmount.Substring(1, WebConfig.TributeLifeTimeAmount.Length - 1)) -
                Convert.ToDouble(WebConfig.PhotoLifeTimeAmount.Substring(1, WebConfig.PhotoLifeTimeAmount.Length - 1));
            BillingTotal.InnerHtml = BillingTotalAbove.InnerHtml = rdoTributeMembershipLifeTime.Text;
        }
        else if (tributePackageId == 5 || tributePackageId == 2)
        {
            rdoPhotoMembershipLifeTime.Text = " " + WebConfig.PhotoLifeTimeAmount;
            rdoTributeMembershipYearly.Text = " " + WebConfig.TributeOneyearAmount;
            rdoTributeMembershipLifeTime.Text = " " + WebConfig.TributeLifeTimeAmount;
            

            rdoTributeMembershipYearly.Checked = true;
            // If the Tribute package type is Tribute Yearly then display only the Tribute Column options in the upgrade page
            midColumn1.Visible =
                midColumn2.Visible =
                midColumn3.Visible =
                midColumn4.Visible =
                midColumn5.Visible =
                midColumn6.Visible =
                midColumn7.Visible =
                midColumn8.Visible =
                midColumn9.Visible =
                midColumn10.Visible =
                midColumn11.Visible =
                midColumn12.Visible =
                midColumn13.Visible = midColumn14.Visible = secondLastRow.Visible = midColumn16.Visible = false;

            //Setting initial value of green Billing total when the tribute first loads
            valueInDouble1 =
                Convert.ToDouble(WebConfig.TributeOneyearAmount.Substring(1, WebConfig.TributeOneyearAmount.Length - 1));
            BillingTotal.InnerHtml = BillingTotalAbove.InnerHtml = "$ " + valueInDouble1.ToString("#,0.00");

        }
        if (rdoTributeMembershipYearly.Checked || rdoTributeMembershipLifeTime.Checked)
        {
            PanelChangeAddress.Visible = true;
            PanelTributeAddress.Visible = false;
            pnlTributeAddressAvailable.Visible = false;
        }
        else
        {
            PanelChangeAddress.Visible = false;
            PanelTributeAddress.Visible = false;
            pnlTributeAddressAvailable.Visible = false;
        }

    }

    private void CreateFoldersForNewTributeUrl(int tributeId, string OldTributeURL, string NewtributeURL,
                                               string tributeType)
    {
        if (!(OldTributeURL.Equals(NewtributeURL)))
        {
            // For default.aspx
            if (tributeType == "New Baby")
                _presenter.CreateDefaultFolder(WebConfig.NewBabyFolderPath, NewtributeURL);
            else if (tributeType == "Birthday")
                _presenter.CreateDefaultFolder(WebConfig.BirthdayFolderPath, NewtributeURL);
            else if (tributeType == "Graduation")
                _presenter.CreateDefaultFolder(WebConfig.GraduationFolderPath, NewtributeURL);
            else if (tributeType == "Wedding")
                _presenter.CreateDefaultFolder(WebConfig.WeddingFolderPath, NewtributeURL);
            else if (tributeType == "Anniversary")
                _presenter.CreateDefaultFolder(WebConfig.AnniversaryFolderPath, NewtributeURL);
            else if (tributeType == "Memorial")
                _presenter.CreateDefaultFolder(WebConfig.MemorialFolderPath, NewtributeURL);

            //For TributePhotos
            string[] eventPath = CommonUtilities.GetPath();
            string OldDefaultPath = eventPath[0] + "/" + eventPath[1] + "/" + OldTributeURL.Replace(" ", "_") + "_" +
                                    tributeType.Replace(" ", "_");
            string NewTributeURLFolderPath = eventPath[0] + "/" + eventPath[1] + "/" + NewtributeURL.Replace(" ", "_") +
                                             "_" + tributeType.Replace(" ", "_");
            // For Tribute Thumbnails

            string OldDefaultPath_thumbs = eventPath[0] + "/" + eventPath[1] + "/thumbnails/" +
                                           OldTributeURL.Replace(" ", "_") + "_" + tributeType.Replace(" ", "_");
            string NewTributeURLFolderPath_thumbs = eventPath[0] + "/" + eventPath[1] + "/thumbnails/" +
                                                    NewtributeURL.Replace(" ", "_") + "_" +
                                                    tributeType.Replace(" ", "_");

            try
            {
                this._presenter.CopyOldURlFolderToNewURLFolder(OldDefaultPath, NewTributeURLFolderPath);
            }
            catch (Exception a)
            {
                lblErrMsg.InnerHtml = SetHeaderMessage(a.Message, ValidationSummary1.HeaderText);
                lblErrMsg.Visible = true;
            }
            try
            {
                this._presenter.CopyOldURlFolderToNewURLFolder(OldDefaultPath_thumbs, NewTributeURLFolderPath_thumbs);
            }
            catch (Exception a)
            {
                lblErrMsg.InnerHtml = SetHeaderMessage(a.Message, ValidationSummary1.HeaderText);
                lblErrMsg.Visible = true;
            }

            //For TributeVideo
            string[] paths = CommonUtilities.GetVideoTributePath();
            string srcPath = paths[1] + OldTributeURL + "_" + tributeType;
            string destPath = paths[1] + NewtributeURL + "_" + tributeType;
            try
            {
                this._presenter.CopyOldURlFolderToNewURLFolder(srcPath, destPath);
            }
            catch (Exception a)
            {
                lblErrMsg.InnerHtml = SetHeaderMessage(a.Message, ValidationSummary1.HeaderText);
                lblErrMsg.Visible = true;
            }
        }

    }

    private void SendEmailtoNewAdmin()
    {
        StateManager stateManager = StateManager.Instance;
        Tributes objtribute = (Tributes)stateManager.Get("TributeSession", TributesPortal.Utilities.StateManager.State.Session);
        StateManager stateManager1 = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue) stateManager1.Get("objSessionvalue", StateManager.State.Session);

        this._presenter.SendEmailtoNewAdmin(objtribute, objSessionvalue, true);
    }


    // New added to Call BeanStream server

    protected string PayYourBill(string strCardNumber, string strCCNumber, int strCardExpiryMonth, int strCardExpiryYear
         , double strAmount, CardTypeV1 CardType, string strFirstName, string strLastname,
         string strStreet, string strCity, StateV1 State, CountryV1 Country, string ZipCode,
         string strPhone, string strEmail, string strCustomerIp, out string confirmationId, out string errorMesg, out bool bIsSuccess)
    {
        try
        {
            #region BeanStream code

            errorMesg = "";
            confirmationId = "";
            bIsSuccess = false;
            var trnOrderNumber = Guid.NewGuid().ToString().Substring(0, 10);
            var errorPage = "http://localhost:4941/DevelopmentWebsite/Create.aspx";
            //ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "tributes/TributeCreation.aspx";
            var approvedPage = "http://localhost:4941/DevelopmentWebsite/Create.aspx";
            //ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "tributes/TributeCreation.aspx";
            var declinedPage = "http://localhost:4941/DevelopmentWebsite/Create.aspx";
            //ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "tributes/TributeCreation.aspx";
            var username = ConfigurationManager.AppSettings["BeanUserName"];
            var password = ConfigurationManager.AppSettings["BeanUserPwd"];
            var month = strCardExpiryMonth.ToString().Length > 0 && strCardExpiryMonth.ToString().Length == 1 ? "0" + strCardExpiryMonth.ToString() : strCardExpiryMonth.ToString();
            var year = strCardExpiryYear.ToString().Length > 0 && strCardExpiryYear.ToString().Length == 4 ? strCardExpiryYear.ToString().Substring(2, 2) : strCardExpiryYear.ToString();

            var redirectedURL = ConfigurationManager.AppSettings["BeanStreamUrl"]
                                + "?merchant_id=" + ConfigurationManager.AppSettings["MerchantId"]
                                + "&requestType=" + ConfigurationManager.AppSettings["requestType"]
                                + "&trnType=" + ConfigurationManager.AppSettings["trnType"]
                                + "&trnOrderNumber=" + trnOrderNumber
                                + "&trnAmount=" + strAmount
                                + "&trnCardOwner=" + strFirstName + " " + strLastname
                                + "&trnCardNumber=" + strCardNumber
                                + "&trnExpMonth=" + month
                                + "&trnExpYear=" + year
                                + "&ordName=" + strFirstName + " " + strLastname
                                + "&ordAddress1=" + strStreet
                                + "&ordCity=" + strCity
                                + "&ordProvince=" + State
                                + "&ordCountry=" + Country
                                + "&ordPostalCode=" + ZipCode
                                + "&ordPhoneNumber=" + strPhone
                                + "&ordEmailAddress=" + strEmail
                                + "&errorPage=" + errorPage
                                + "&approvedPage=" + approvedPage
                                + "&declinedPage=" + declinedPage
                                + "&trnCardCvd=" + strCCNumber
                                + "&username=" + username
                                + "&password=" + password;

            // Caliing Beanstream server
            string sResponceText = CallBeanStream(redirectedURL);
            if (!string.IsNullOrEmpty(sResponceText) && sResponceText.Contains("trnApproved=1"))
                bIsSuccess = true;
            else if (!string.IsNullOrEmpty(sResponceText) && sResponceText.Contains("trnApproved=0"))
                bIsSuccess = false;

            var sResponseArr = sResponceText.Split('&');
            if (sResponseArr.Length >= 2)
                confirmationId = sResponseArr[1].Split('=')[1];
            else confirmationId = "0";

            return sResponceText;

            #endregion
        }
        catch (Exception ex)
        {
            throw new ApplicationException("PAYMENT");
        }


    }

    // Function to call BeanStream server
    public string CallBeanStream(string sRequestString)
    {
        //throw new NotImplementedException();
        System.Net.WebRequest _objReq = System.Net.WebRequest.Create(sRequestString);
        //System.Net.WebResponse _objResponce = _objReq.GetResponse();
        using (System.Net.WebResponse response = _objReq.GetResponse())
        using (System.IO.Stream responseStream = response.GetResponseStream())
        using (System.IO.StreamReader reader = new System.IO.StreamReader(responseStream))
        {
            Console.WriteLine(((System.Net.HttpWebResponse) response).StatusDescription);
            // Get the stream containing content returned by the server.

            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            return responseFromServer;
        }

    }

    public string ApplicationType
    {
        get { return ConfigurationManager.AppSettings["ApplicationType"].ToString(); }
    }

    public bool IsContainsVideo { get; set; }



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

}