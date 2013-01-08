//Copyright       : Copyright (c) Sopra Group India Ltd
//Project         : Tribute Portal
//File Name       : TributePortal.DevelopmentWebsite.MyHome.AdminProfileBilling.aspx.cs
//Author Name     : 
//Creation        : 
//Last Updation   : 
//Description     : This page displays the billing info for a user. This page shows the credit card details 
//                  and all the receipts for which the user has paid for.


using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.MyHome.Views;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using System.IO;
using System.Collections.Generic;
using System.Text;

public partial class MyHome_AdminProfileBilling : PageBase, IAdminProfileBilling
{
    private AdminProfileBillingPresenter _presenter;
    int UserID;
    string bannerMessage = string.Empty;

    #region CCRecipt variables

    #endregion CCRecipt variables
    protected void Page_Load(object sender, EventArgs e)
    {
        // Code to implement SSL Functionality.
        //if (!WebConfig.ApplicationMode.Equals("local"))
        //    if (Request.Url.ToString().Contains(@"http://"))
        //        Response.Redirect(@"https://www." + WebConfig.TopLevelDomain + @"/adminprofilebilling.aspx");
        this.Master.NavPrimary = Shared_InnerSecure.AdminNavPrimaryEnum.myprofile.ToString();
        this.Master.NavSecondary = Shared_InnerSecure.AdminNavSecondaryEnum.billing.ToString();
        this.Master.PageTitle = "Billing Info";

        if (!this.IsPostBack)
        {
            StateManager stateManagerP = StateManager.Instance;
            string PageName = "AdminProfileBilling";
            stateManagerP.Add(PortalEnums.SessionValueEnum.SearchPageName.ToString(), PageName, StateManager.State.Session);

            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                UserID = objSessionvalue.UserId;
                LoggedInUserName = objSessionvalue.UserName;
                this._presenter.OnViewInitialized();
                this._presenter.OnBillingInformation();
                this._presenter.GetCreditCardDetails_();
                this._presenter.GetCCCountryList_();
                this._presenter.OnStateLoad_();
                lbtnSaveChanges.Attributes.Add("onclick", "HideIndicator();");
                //rdoYearlyAutoRenew.Attributes.Add("onclick", "MakeAutoRenew();");
                // rdoNotifyBeforeRenew.Attributes.Add("onclick", "MakeAutoRenew();");
            }
            else
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }
        }

        setDefault();
    }

    [CreateNew]
    public AdminProfileBillingPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }


    public IList<BillingHistory> BillingInformation
    {
        set
        {
            gvBillingHistory.DataSource = value;
            gvBillingHistory.DataBind();

        }
    }
    protected void lbtnSaveChanges_Click(object sender, EventArgs e)
    {
        setDefault();
        try
        {
            if ((rdoCCVisa.Checked == true) || (rdoCCMasterCard.Checked == true))
            {
                this._presenter.UpdateCCDetails();
                this._presenter.GetCreditCardDetails_();
                setmessage("<h2>Creditcard Details updated</h2><P>Your creditcard details updated successfully.</P>", 2);
            }
            else
                setmessage("<h2>Oops - there is a problem.</h2> <h3>Please correct the errors below:</h3><ul><li>" + "Select your payment method." + "</li></ul>", 1);

        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + "</li></ul>", 1);
        }
    }


    private void setmessage(string msg, int type)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");

        if (type == 1)
            errormsg.Attributes.Add("class", "yt-Error");
        else
            errormsg.Attributes.Add("class", "yt-Notice");

        errormsg.InnerHtml = msg;
        errormsg.Visible = true;
    }

    private void setDefault()
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");

        errormsg.InnerHtml = "";
        errormsg.Visible = false;
    }

    #region IAdminProfileBilling Members


    public string BannerMessage
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {

            string msg = value;
            setmessage("<h2>Oops - there is a problem.</h2> <h3>Please correct the errors below:</h3><ul><li>" + msg + "</li></ul>", 1);
        }
    }

    public int UserId
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
                UserID = objSessionvalue.UserId;

            return UserID;
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    #endregion


    public IList<ParameterTypesCodes> PaymentModes
    {
        set
        {
            StringBuilder sbPayementModes = new StringBuilder();
            if (value.Count > 0)
            {
                for (int i = 0; i < value.Count-2; i++)
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
                //ltrPaymentMethod.Text = sbPayementModes.ToString();
            }
        }
    }

    public IList<ParameterTypesCodes> PaymentModes_
    {
        set
        {
            StringBuilder sbPayementModes = new StringBuilder();
            if (value.Count > 0)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    sbPayementModes.Append("<div class='yt-Form-Field yt-Form-Field-Radio' id='yt-CC" + value[i].TypeDescription + "'>");
                    sbPayementModes.Append("<input type='radio' name='rdoCCType' runat='server' onclick='Check_(this);' id='1rdoCC" + value[i].TypeDescription + "' value='" + value[i].TypeDescription + "'");
                    sbPayementModes.Append("/>");
                    sbPayementModes.Append("<label for='rdoCC" + value[i].TypeDescription + "'>" + value[i].TypeDescription + "</label>");
                    sbPayementModes.Append(" </div>");
                }
                Literal1.Text = sbPayementModes.ToString();
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
                if (state != -1)
                {
                    ddlCCStateProvince.DataSource = value;
                    ddlCCStateProvince.DataTextField = Locations.Location.LocationName.ToString();
                    ddlCCStateProvince.DataValueField = Locations.Location.LocationId.ToString();
                    ddlCCStateProvince.DataBind();
                    ddlCCStateProvince.SelectedIndex = ddlCCStateProvince.Items.IndexOf(ddlCCStateProvince.Items.FindByValue(state.ToString()));
                    ddlCCStateProvince.Enabled = true;
                }
            }
            else
            {
                ddlCCStateProvince.Enabled = false;
            }
        }
    }

    private string LoggedInUserName
    {
        set
        {
            ViewState["LoggedInUserName"] = value;
        }
        get
        {
            if (ViewState["LoggedInUserName"] == null)
                return null;
            else
                return ViewState["LoggedInUserName"].ToString();
        }
    }


    protected void ddlCCCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.OnStateLoad();
    }
    #region CCR ITributeHomePage Members


    public string SelectedCCCountry
    {
        get { return ddlCCCountry.SelectedValue.ToString(); }
        set { ddlCCCountry.SelectedIndex = ddlCCCountry.Items.IndexOf(ddlCCCountry.Items.FindByValue(value.ToString())); }

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
            //ddlCCStateProvince.SelectedIndex = ddlCCStateProvince.Items.IndexOf(ddlCCStateProvince.Items.FindByValue(value.ToString())); 
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
            txtPhoneNumber2.Text = value.Substring(3, 3);
            txtPhoneNumber3.Text = value.Substring(6, 4);
        }
    }


    static string _paymentmethod = string.Empty;
    public string PaymentMethod
    {
        get
        {
            if (rdoCCVisa.Checked == true)
                hfPaymentMethod.Value = rdoCCVisa.Value.ToString();
            else if (rdoCCMasterCard.Checked == true)
                hfPaymentMethod.Value = rdoCCMasterCard.Value.ToString();
            if (hfPaymentMethod.Value.Length != 0)
                _paymentmethod = hfPaymentMethod.Value;
            return _paymentmethod;
        }
        set
        {
            if (value.ToLower().Equals("mastercard"))
            {
                rdoCCMasterCard.Checked = true;
            }
            else
                rdoCCVisa.Checked = true;
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
            string[] splitter ={ WebConfig.AddressSeparator };
            string[] _address = value.Split(splitter,System.StringSplitOptions.None);
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
            return false;
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
    #endregion

    protected void gvBillingHistory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = int.Parse(gvBillingHistory.SelectedRow.RowIndex.ToString());
        int ColumnCount = gvBillingHistory.Columns.Count;
        string[] Gridval = new string[ColumnCount - 1];
        Label lbtnTributeid = (Label)gvBillingHistory.Rows[index].FindControl("lbtnTributeid");
        Label lblTributePackageId = (Label)gvBillingHistory.Rows[index].FindControl("lblTributePackageId");
        this._presenter.GetPaymentReceipt(int.Parse(lblTributePackageId.Text));
        //gvBillingHistory.TemplateControl.Attributes.Add("onClick", "doModalAdminReceipt();");
        //this._presenter.PaymentReceipt(int.Parse(lbtnTributeid.Text));            
    }

    protected void gvBillingHistory_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SelectTribute")
        {
            Tributes objTributeDetails = _presenter.GetTributeDetails(int.Parse(e.CommandArgument.ToString()));

            if (WebConfig.ApplicationMode.Equals("local"))
            {
                Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + objTributeDetails.TributeUrl, false);
            }
            else
            {
                //Comment the line above and uncomment the line below for server
                Response.Redirect("http://" + objTributeDetails.TypeDescription.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objTributeDetails.TributeUrl);
            }
        }
    }
    #region IAdminProfileBilling Members


    public IList<PaymentReceipt> PaymentReceipt
    {
        set
        {
            if (value.Count > 0)
            {
                string Amountpaid = value[0].AmountPaid.ToString();
                Amountpaid = int.Parse(Amountpaid).ToString();
                Amountpaid = "$" + Amountpaid;

                string date = value[0].Enddate.ToString();
                if (value[0].Enddate.ToString() != "")
                {
                    char[] spliter ={ '/', ' ' };
                    string[] _date = value[0].Enddate.ToString().Split(spliter);
                    DateTime expdate = new DateTime(int.Parse(_date[2].ToString()), int.Parse(_date[0].ToString()), int.Parse(_date[1].ToString()));
                    //DateTime expdate = new DateTime(int.Parse(_date[2].ToString()), int.Parse(_date[1].ToString()), int.Parse(_date[0].ToString()));
                    date = expdate.ToString("MMMM dd, yyyy");
                }
                else
                {
                    date = "Never";
                }

                string CCNumber = string.Empty;
                if (value[0].CreditCardNo != string.Empty)
                {
                    string strCredit = string.Empty;
                    string ccnumber = TributePortalSecurity.Security.DecryptSymmetric(value[0].CreditCardNo.Trim());
                    for (int indexCredit = 0; indexCredit < ccnumber.Length - 4; indexCredit++)
                        strCredit += "X";
                    CCNumber = strCredit + ccnumber.Substring(ccnumber.Length - 4);
                }
                else
                    CCNumber = "NA";

                ScriptManager.RegisterClientScriptBlock(gvBillingHistory, GetType(), "Billing", "Test('" + value[0].Tributename + "','" + value[0].TypeDescription + "','" + value[0].Packagename.Replace("Life Time", "Lifetime") + "','" + value[0].StartDate.ToString("MMMM dd,yyyy") + "','" + date + "','" + value[0].CardholdersName + "','" + value[0].Address.Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay) + "','" + value[0].City + "','" + value[0].State + "','" + value[0].Country + "','" + value[0].Zip + "','" + value[0].Telephone + "','" + value[0].CreditCardType + "','" + CCNumber + "','" + Amountpaid + "');", true);
                //                                                                                                TributeName,                     trubuteType,                PackageType,                        BillingDate,                                       ExpiryDate,                                         Name,                              Address,                                                                                       City,     StateProvince,                        Country,        ZipPostal,                         Telephone,           PaymentType,                    CreditCard         AmountBilled

            }

        }
    }

    #endregion
    protected void lbtndeleteCCinfo_Click(object sender, EventArgs e)
    {
        setDefault();
        try
        {
            this._presenter.DeleteCreditCardDetails();
            this._presenter.OnBillingInformation();
            this._presenter.GetCreditCardDetails_();
            setmessage("<h2>Credit Card Details deleted</h2><P>Your creditcard details deleted successfully.</P>", 2);
        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem in credit card details.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + ".</li></ul>", 1);
        }
    }

    #region IAdminProfileBilling Members


    public IList<Locations> CCCountryList_
    {
        set
        {
            ddlCCCountry1.DataSource = value;
            ddlCCCountry1.DataTextField = Locations.Location.LocationName.ToString();
            ddlCCCountry1.DataValueField = Locations.Location.LocationId.ToString();
            ddlCCCountry1.DataBind();
        }
    }

    public IList<Locations> CCStateList_
    {
        set
        {
            ddlCCStateProvince1.Items.Clear();
            if (value.Count > 0)
            {
                ddlCCStateProvince1.DataSource = value;
                ddlCCStateProvince1.DataTextField = Locations.Location.LocationName.ToString();
                ddlCCStateProvince1.DataValueField = Locations.Location.LocationId.ToString();
                ddlCCStateProvince1.DataBind();
            }
        }
    }

    public string SelectedCCCountry_
    {
        get { return ddlCCCountry1.SelectedValue.ToString(); }
    }


    static int state1 = -1;
    public int SelectedCCState_
    {
        get
        {
            if (ddlCCStateProvince1.SelectedIndex != -1)
            {
                state1 = int.Parse(ddlCCStateProvince1.SelectedValue.ToString());
                return state1;
            }
            else
                return state1;
        }
    }

    public string SelectedCCCity_
    {
        get
        {
            return txtCCCity1.Text;
        }
    }

    public string CreditCardNo_
    {
        get
        {
            return TributePortalSecurity.Security.EncryptSymmetric(txtCCNumber1.Text);
            //return txtCCNumber1.Text;
        }
    }

    public string CardholdersName_
    {
        get
        {
            return txtCCName1.Text;
        }
    }

    public DateTime ExpirationDate_
    {
        get
        {
            DateTime _DateTime = new DateTime(int.Parse(txtCCYear1.Text), ddlCCMonth1.SelectedIndex, 1);
            return _DateTime;
        }
    }

    public string Telephone_
    {
        get { return txtPhoneNumber_1.Text + txtPhoneNumber_2.Text + txtPhoneNumber_3.Text; }
    }


    static string _paymentmethod1 = string.Empty;
    public string PaymentMethod_
    {
        get
        {
            if (HiddenField1.Value.Length != 0)
                _paymentmethod1 = HiddenField1.Value;

            return _paymentmethod1;

        }
    }

    public string Address_
    {
        get
        {
            if (txtCCBillingAddress_2.Text.Length != 0)
                return txtCCBillingAddress_.Text + WebConfig.AddressSeparator + txtCCBillingAddress_2.Text;
            else
                return txtCCBillingAddress_.Text;
        }
    }

    public string ZipCode_
    {
        get { return txtCCZipCode1.Text; }
    }

    public bool NotifyBeforeRenew_
    {
        get
        {
            //return rdoYearlyAutoRenew.Checked;
            return true;
        }
    }
    public bool IsCardDetailsReusable_
    {
        get
        {
            //return chkSaveBillingInfo.Checked;
            return true;
        }
    }

    public int PackageId_
    {
        get
        {
            return 2;
        }
    }



    #endregion
    protected void ddlCCCountry1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.OnStateLoad_();
    }

    #region IAdminProfileBilling Members


    public int Visibility
    {
        set
        {
            if (value == 1)
            {
                CCdetails.Visible = true;
                Fieldset1.Visible = false;
            }
            else
            {
                Fieldset1.Visible = true;
                CCdetails.Visible = false;
                setmessage("<h2>Oops - there is a problem .</h2> <h3>Please correct the errors below:</h3><ul><li>You have not stored any credit card information.</li></ul>", 1);
            }
        }
    }

    #endregion
    protected void lbtnPay_Click(object sender, EventArgs e)
    {
        try
        {
            this._presenter.InsertCCDetails();
            this._presenter.OnBillingInformation();
            this._presenter.GetCreditCardDetails_();
            // SetBlank();
            setmessage("<h2>Creditcard Details updated</h2><P>Your creditcard details saved successfully.</P>", 2);
        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + ".</li></ul>", 1);
        }

    }

    #region IAdminProfileBilling Members


    public string CVC
    {
        get
        {
            return TributePortalSecurity.Security.EncryptSymmetric(txtCCVerification.Text);
            //return txtCCVerification.Text;
        }
        set
        {
            string CVCCode = TributePortalSecurity.Security.DecryptSymmetric(value);
            txtCCVerification.Text = CVCCode;
            txtCCVerification1.Text = CVCCode;
            txtCCVerification.Attributes.Add("value", CVCCode);
            //txtCCVerification.Text = value;
            //txtCCVerification1.Text = value;
            //txtCCVerification.Attributes.Add("value", value);
        }
    }

    public string CVC_
    {
        get
        {
            return TributePortalSecurity.Security.EncryptSymmetric(txtCCVerification1.Text);
            //return txtCCVerification1.Text;
        }
    }

    #endregion

    private void SetBlank()
    {
        ArrayList all = Common.GetControls(this.Controls);
        for (int i = 0; i <= all.Count - 1; i++)
        {
            Control ctl = (Control)all[i];
            if (ctl.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
            {
                ((TextBox)ctl).Text = string.Empty;
            }
            //rdoYearlyAutoRenew.Checked = false;
            //rdoNotifyBeforeRenew.Checked = true;
            //chkSaveBillingInfo.Checked = false;
        }

    }
    protected void lbtnAddCreditCardInformation_Click(object sender, EventArgs e)
    {

        this._presenter.GetCCCountryList_();
        this._presenter.OnStateLoad_();
        ScriptManager.RegisterClientScriptBlock(lbtnAddCreditCardInformation, GetType(), "Billing", "Blanktextboxes();", true);

    }


    protected void lbtnValidateCoupon_Click(object sender, EventArgs e)
    {
        //int availability = _presenter.GetCouponAvailable(txtCouponCode.Text);
        //if (availability == 1)
        //    SetCouponAvailableStatus();
        //else
        //    SetCouponUnAvailableStatus();
    }
    #region IAdminProfileBilling Members


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
    #endregion
}


