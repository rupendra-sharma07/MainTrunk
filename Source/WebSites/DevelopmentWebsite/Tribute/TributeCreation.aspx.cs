///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.TributeCreation.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page is used to create new tribute
///Audit Trail     : Date of Modification  Modified By         Description
///
#region Refrences
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
using TributesPortal.Tribute.Views;
using TributesPortal.Tribute;
using TributesPortal.BusinessEntities;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TributesPortal.Utilities;
using AjaxControlToolkit;
using TributesPortal.MultipleLangSupport;
using System.Web.Services;
using System.Text;
using System.IO;

//using com.optimalpayments.test.webservices;
using com.optimalpayments.webservices;
using PerceptiveMCAPI.Types;
using PerceptiveMCAPI.Methods;
using PerceptiveMCAPI;

//using System.Web.SessionState;

#endregion Refrences
/*Designed and Developed by Sopra Group India Pvt Ltd
'
' Description   :   This Class willfollow the the life cycle of Tribute Creation.
' Date Created  :   9 January,2008 fonview
 * 
' Revision History:-*/

public partial class Tribute_TributeCreation : PageBase, ITributeCreation
{
    #region Variables
    private TributeCreationPresenter _presenter;
    int UserID;
    protected string _UserMail;
    static string Status = string.Empty;
    //    static System.Decimal Identity;
    protected System.Decimal CCIdentity = 0;
    //private static int TributeTheme;
    protected string _tributeName;
    protected string _tributeURL;
    protected string _tributeType;
    protected static string _tributefor;
    protected string _Themename;
    protected static int _TributeCount;
    private string _Tributeimage;
    private int _transactionId;
    private int _tributPackageId;
    string Domainname = string.Empty;
    ArrayList alistData = null;
    protected string amount = string.Empty;
    protected string tributeImgURL = "../assets/images/bg_TributePhoto.gif";
    private string _selectedPaymentMode = string.Empty;
    private string confirmationId = string.Empty;
    private string errorMesg = string.Empty;
    private string _amount = string.Empty;
    private int _NetCreditCount;
    private IList<CreditCostMapping> _creditCostMappingList = null;
    // private int _videoTributeOwnerid = 0; //by ud

    private int _accountType = 0;

    private int _userType;
    protected bool isUserBussiness = false;
    private int _creditCost = 0;
    public double totalValue = 1;
    private string _postMessage;
    private string _messageWithoutHtml;
    string[] NameAndMsg = new string[2];

    int countadmin = 0;
    private int _defaultTheme;
    #region BeanStream varriables
    string sBeanStreamResponce = string.Empty;



    #endregion
    #endregion Variables

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.Action = Request.RawUrl;
        if (Request.QueryString["AccountType"] != null)
        {
            int.TryParse(Request.QueryString["AccountType"].ToString(), out _accountType);
        }
        if (WebConfig.ApplicationType.ToLower() == "yourmoments")
        {
            Memorial.Attributes.Add("style", "display:none;");
            autoRenew.InnerHtml = @"You can turn off auto-renewal at any time in 
                                                website management.";
        }
        if (!IsPostBack)
        {
            rdoNotifyBeforeRenew.Text = "I do not want this " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower() + " to be renewed automatically on a yearly basis, but I will be notified when the account is near to expiry.";
            rdoYearlyAutoRenew.Text = "I want this " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower() + " to be renewed automatically on a yearly basis.";
            chkSaveBillingInfo.Text = "I would like to save the above billing information in my profile.";

            rfvTirbuteName.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " name is a required field";
            cvTributeName.ErrorMessage = "Invalid " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " Name,* and ? is not allowed,Please try again. ";
            revTributeAddress.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " Address can only contain letters, numbers and '_'";
            rfvTributeAddress.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " Address can only contain letters, numbers and '_'";
            rfvTributeAddressNext.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower() + "web address is a required field.";
            revTributeaddress2.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower() + " Address can only contain letters, numbers and '_'";
            cvTributeAddressOther.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower() + " web address is a required field";
            revTributeaddressOtherNext.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower() + " Address can only contain letters, numbers and '_'";
            cvTributeAddressOtherNext.ErrorMessage = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower() + " web address is a required field";
            imgTributePhoto.AlternateText = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower() + " photo";
            lblarMessage.Text = "Welcome Message to " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower() + " visitors";
            rfvarMessage.ErrorMessage = "Please enter a Welcome Message to " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower() + " visitors.";

            PortalValidationSummary.HeaderText = " <h2>Oops - there was a problem with your " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower() + " details.</h2>                                                             <h3>Please correct the errors below:</h3>";
            cvPrivacy.ErrorMessage = " Please select the level of privacy for this " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower();


            SetErrorHeader12();
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                Setdefault();
                UserID = objSessionvalue.UserId;
                _UserMail = objSessionvalue.UserEmail;
                _userType = objSessionvalue.UserType;
                txtarMessage.Attributes.Add("onkeyup", "CheckLenght();");
                lbtnCreatetribute.Attributes.Add("onclick", "HideIndicator();");
                lbtncheckAddress.Attributes.Add("onclick", "HideIndicator2();");
                lbtnNextstep.Attributes.Add("onclick", "HideIndicator2();");
                lbtn2Nextstep.Attributes.Add("onclick", "HideIndicator3();");
                txtTributeAddress.Attributes.Add("onfocus", "CalloutBox();");
                rdoYearlyAutoRenew.Attributes.Add("onclick", "MakeAutoRenew();");
                rdoNotifyBeforeRenew.Attributes.Add("onclick", "MakeAutoRenew();");
                SetMessageMoxLength();
                cpYear.ValueToCompare = DateTime.Now.Year.ToString();
                // to get the default theme for tribute by Ud
                if (Request.QueryString["Type"] != null)
                    GetDefaultThemeForTribute(Request.QueryString["Type"].ToString());
                Makedata(1);
                this._presenter.GetTributeLists(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower());
                this._presenter.GetMaymentModes();
                FillDays(ddlDay);
                FillDays(ddlDay2);
                CreateTributeView.ActiveViewIndex = 0;
                Page.SetFocus(txtTributeFirstName);
                this._presenter.GetCountryList();
                this._presenter.GetStateList();
                this._presenter.GetCCCountryList();
                this._presenter.GetCCStateList();
                this._presenter.GetDonationCountryList();
                this._presenter.GetDonationStateList();
                lbtncheckAddress.CssClass = "yt-checkAvailability";
                SetSelectedDefault();
                //AG(25-Nov-2010): Added for setting of prepopulating video tributes vallues
                if (Request.QueryString["VideoTributeId"] != null)
                {
                    SetVideoTributeDefault(int.Parse(Request.QueryString["VideoTributeId"].ToString()));
                }
                pnlSubCategory.Visible = false;
                dvThemes.Visible = false;
                if (_userType == 2)
                {
                    DefaultThemeCheckBox.Visible = true;
                }
            }
            else
            {
                //Here
                string Querystring = "?PageName=" + "TributeCreation";
                //AG(25-Nov-2010): Added for tribute creation from video tribute display page
                if (Request.QueryString["VideoTributeId"] != null)
                {
                    Querystring += "&VideoTributeId=" + Request.QueryString["VideoTributeId"].ToString();
                }

                if (Request.QueryString["Type"] != null)
                {
                    Querystring += "&Type=" + Request.QueryString["Type"].ToString();
                }
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()) + Querystring, false);
            }
            if (Request.QueryString["Type"] != null)
            {
                string strTType = Request.QueryString["Type"].ToString();
                switch (strTType.ToLower())
                {
                    case "new baby":
                        NewBaby.Checked = true;
                        FillSubCategory(NewBaby.Text);
                        CommonFunction(NewBaby.Text, NewBaby.ClientID);
                        break;
                    case "newbaby":
                        NewBaby.Checked = true;
                        FillSubCategory(NewBaby.Text);
                        CommonFunction(NewBaby.Text, NewBaby.ClientID);
                        break;
                    case "birthday":
                        Birthday.Checked = true;
                        FillSubCategory(Birthday.Text);
                        CommonFunction(Birthday.Text, Birthday.ClientID);
                        break;
                    case "graduation":
                        Graduation.Checked = true;
                        FillSubCategory(Graduation.Text);
                        CommonFunction(Graduation.Text, Graduation.ClientID);
                        break;
                    case "wedding":
                        Wedding.Checked = true;
                        FillSubCategory(Wedding.Text);
                        CommonFunction(Wedding.Text, Wedding.ClientID);
                        break;
                    case "anniversary":
                        Anniversary.Checked = true;
                        FillSubCategory(Anniversary.Text);
                        CommonFunction(Anniversary.Text, Anniversary.ClientID);
                        break;
                    case "memorial":
                        if (WebConfig.ApplicationType.ToLower() == "yourtribute")
                        {
                            Memorial.Checked = true;
                            FillSubCategory(Memorial.Text);
                            CommonFunction(Memorial.Text, Memorial.ClientID);
                        }
                        break;
                }
            }
            if (WebConfig.ApplicationType.ToLower() == "yourtribute")
            {
                Memorial.Checked = true;
                TributeTypeChkBox.Visible = false;

                GetDefaultThemeForTribute("Memorial");
                FillSubCategory(Memorial.Text);
                CommonFunction(Memorial.Text, Memorial.ClientID);
            }
        }
        

        if (Request.QueryString["VideoTributeId"] != null)
        {
            StateManager stateMgr = StateManager.Instance;

            if (stateMgr.Get("DODYear", StateManager.State.Session) != null)
                txtYear2.Text = (String)stateMgr.Get("DODYear", StateManager.State.Session);

            if (stateMgr.Get("DODMonth", StateManager.State.Session) != null)
                ddlMonth2.SelectedIndex = (Int32)stateMgr.Get("DODMonth", StateManager.State.Session);

            if (stateMgr.Get("DODDay", StateManager.State.Session) != null)
                ddlDay2.SelectedIndex = (Int32)stateMgr.Get("DODDay", StateManager.State.Session);

            if (stateMgr.Get("DOBYear", StateManager.State.Session) != null)
                txtYear.Text = (String)stateMgr.Get("DOBYear", StateManager.State.Session);

            if (stateMgr.Get("DOBMonth", StateManager.State.Session) != null)
                ddlMonth.SelectedIndex = (Int32)stateMgr.Get("DOBMonth", StateManager.State.Session);

            if (stateMgr.Get("DOBDay", StateManager.State.Session) != null)
                ddlDay.SelectedIndex = (Int32)stateMgr.Get("DOBDay", StateManager.State.Session);

            if (stateMgr.Get("VideoTributeCity", StateManager.State.Session) != null)
                txtCity.Text = (String)stateMgr.Get("VideoTributeCity", StateManager.State.Session);

            if (stateMgr.Get("VideoTributeCountry", StateManager.State.Session) != null)
            {
                ddlCountry.SelectedValue = stateMgr.Get("VideoTributeCountry", StateManager.State.Session).ToString();
            }

            if (stateMgr.Get("VideoTributeState", StateManager.State.Session) != null)
            {
                this._presenter.GetStateList();
                ddlStateProvince.SelectedValue = stateMgr.Get("VideoTributeState", StateManager.State.Session).ToString();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetVideoTributeDefault(int videoTributeId)
    {
        this._presenter.SetVideoTributeValue(videoTributeId);
        //throw new NotImplementedException();
    }
    /// <summary>
    /// by Ud : for getting default theme from DataBase
    /// </summary>
    public void GetDefaultThemeForTribute(string tributeType)
    {
        this._presenter.GetDefaultTheme(this.UserId, tributeType);
        if (this.DefaultTheme == 0)
        {
            chbxDefaultTheme.Checked = false;
        }
        else
        {
            chbxDefaultTheme.Checked = true;
        }
    }
    /// <summary>
    /// By UD: to save default theme selected by User 
    /// </summary>
    public void SaveDefaultheme()
    {
        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);

        if (objSessionvalue.UserType == 2)
        {
            if (chbxDefaultTheme.Checked == true)
                this._presenter.SaveDefaultTheme(this.UserId, this.TributeTypeDescription, Convert.ToInt32(hfSeledctedTheme.Value));
            else
                this._presenter.SaveDefaultTheme(this.UserId, this.TributeTypeDescription, 0);
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            try
            {
                if (Session["CVCCode"] != null)
                {
                    txtCCVerification.Text = Session["CVCCode"].ToString();
                    txtCCVerification.Attributes.Add("value", txtCCVerification.Text);
                }

                if ((Request.Form["rdoCCType"] != null) || (Request.Form["rdoCCType"] != null))
                {
                    ltrPaymentMethod.Text = ltrPaymentMethod.Text.Replace("Checked='Checked'", "");
                    ltrPaymentMethod.Text = ltrPaymentMethod.Text.Replace("id='rdoCC" + Request.Form["rdoCCType"] + "'", "Checked='Checked' " + "id='rdoCC" + Request.Form["rdoCCType"] + "'");
                }

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


    private void SetSelectedDefault()
    {
        if (Request.QueryString["Type"] != null)
        {
            if (Request.QueryString["Type"].ToString().Equals("newbaby"))
            {
                NewBaby.Checked = true;
                _tributeType = "New Baby";
                CommonFunction(NewBaby.Text, NewBaby.ClientID);
            }
            if (Request.QueryString["Type"].ToString().Equals("Anniversary"))
            {
                Anniversary.Checked = true;
                _tributeType = "Anniversary";
                CommonFunction(Anniversary.Text, Anniversary.ClientID);
            }
            if (Request.QueryString["Type"].ToString().Equals("Wedding"))
            {
                Wedding.Checked = true;
                _tributeType = "Wedding";
                CommonFunction(Wedding.Text, Wedding.ClientID);
            }
            if (Request.QueryString["Type"].ToString().Equals("Birthday"))
            {
                Birthday.Checked = true;
                _tributeType = "Birthday";
                CommonFunction(Birthday.Text, Birthday.ClientID);
            }
            if (Request.QueryString["Type"].ToString().Equals("Graduation"))
            {
                Graduation.Checked = true;
                _tributeType = "Graduation";
                CommonFunction(Graduation.Text, Graduation.ClientID);
            }
            if (Request.QueryString["Type"].ToString().Equals("Memorial"))
            {
                Memorial.Checked = true;
                _tributeType = "Memorial";
                CommonFunction(Memorial.Text, Memorial.ClientID);
            }
        }
    }
    private void SetMessageMoxLength()
    {
        int length = txtarMessage.Rows * txtarMessage.Columns - txtarMessage.Text.Length;
        //numberRemaining.Text = length.ToString();
        numberRemaining.InnerText = length.ToString();
    }

    private void SetWarning()
    {
        NewBaby.Attributes.Add("onclick", "check(this,0)");
        Birthday.Attributes.Add("onclick", "check(this,1)");
        Graduation.Attributes.Add("onclick", "check(this,2)");
        Wedding.Attributes.Add("onclick", "check(this,3)");
        Anniversary.Attributes.Add("onclick", "check(this,4)");
        Memorial.Attributes.Add("onclick", "check(this,5)");
    }

    private void RemoveWarning()
    {
        NewBaby.Attributes.Remove("onclick");
        Birthday.Attributes.Remove("onclick");
        Graduation.Attributes.Remove("onclick");
        Wedding.Attributes.Remove("onclick");
        Anniversary.Attributes.Remove("onclick");
        Memorial.Attributes.Remove("onclick");
    }

    private void hideerrormessage()
    {
        lblErrMsg.Visible = false;
        lblErrMsg.InnerHtml = String.Empty;

    }

    private void SetThemestep4()
    {
        string[] st1 = HiddenField1.Value.Split(':');
        if (st1.Length >= 3)
        {
            SetThemeforStep4(st1[0].ToString(), st1[1].ToString(), st1[2].ToString(), st1[3].ToString());
        }

    }
    //step 1
    protected void lbtnNextstep_Click(object sender, EventArgs e)
    {
        try
        {
            if (pnlSubCategory.Visible == true)
            {
                ptagtribute.InnerHtml = "Please enter some more information about the tribute you are creating for " + txtTributeName.Text;

                string error = string.Empty;
                TributesPortal.ResourceAccess.IOVS.Sanitise(txtTributeName.Text, ref error);
                if (string.IsNullOrEmpty(error))
                    TributesPortal.ResourceAccess.IOVS.Sanitise(txtTributeAddress.Text, ref error);
                if (!string.IsNullOrEmpty(error))
                {
                    lblErrMsg.InnerHtml = SetHeaderMessage(error, PortalValidationSummary.HeaderText);
                    lblErrMsg.Visible = true;
                    return;
                }

                //for announcement and photo announcement accounts.
                if ((_accountType == 1) || (_accountType == 2))
                {
                    string tempTributeUrl = string.Empty;
                    tempTributeUrl = txtTributeName.Text.Trim().Replace(' ', '-');
                    if (tempTributeUrl.Contains("_"))
                    {
                        tempTributeUrl = tempTributeUrl.Replace("_", string.Empty);
                    }
                    txtTributeAddress.Text = tempTributeUrl;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtTributeAddress.Text.Trim()))
                    {
                        lblErrMsg.InnerHtml = SetHeaderMessage("Tribute web address is a required field.", PortalValidationSummary.HeaderText);
                        lblErrMsg.Visible = true;
                        return;
                    }
                }

                SetTheme_();                   // comment to test
                SetTributeUrlname();
                hideerrormessage();
                SetWarning();
                SetMessageMoxLength();
                SetErrorHeader12();
                lblErrMsg.Visible = false;


                StateManager objtributeType = StateManager.Instance;
                _Themename = objtributeType.Get("TributeType", StateManager.State.Session).ToString();

                if (!(_Themename.Equals(string.Empty)))
                {
                    string tribuType=_Themename.Replace("New Baby","NewBaby");
                    //welcome message customization
                    if (WebConfig.ApplicationType.ToLower() == "yourmoments")
                        tribuType = tribuType + "1";
                    txtarMessage.Text = ResourceText.GetString(tribuType);
                    if (_Themename.Equals("Memorial"))
                    {
                        ObituaryNote.Visible = true;
                        CuteeditorNoteMessage.Attributes.Add("style", "display:block;");

                        //welcome message customization
                        txtarMessage.Text = "This memorial tribute was created to celebrate the life of " + txtTributeName.Text + ". We encourage you to leave a condolence in the online guestbook. You can also use this tribute to share photos, videos, stories and other memories.";

                    }
                    else
                    {
                        CuteeditorNoteMessage.Attributes.Add("style", "display:none;");
                    }

                    if (ValidateTributeTheme())
                    {
                        if (_accountType == 3)
                        {
                            this._presenter.CheckAvailability();
                            if (int.Parse(Status) != 0)
                            {
                                AvailabilityStatus();
                            }
                            if (errorAddress.Visible != true)
                            {
                                CreateTributeView.ActiveViewIndex = 1;
                                hfTributeValue.Value = _Themename;
                                PersonalDetails(_Themename);
                            }
                            else
                            {
                                pnlTributeAddressAvailable.Visible = false;
                            }
                        }
                        else
                        {
                            this._presenter.CheckAvailability();
                            if (int.Parse(Status) != 0)
                            {
                                // for handling _ in tribute url.
                                string tempTrbUrl = txtTributeName.Text.Replace(' ', '-');
                                tempTrbUrl = tempTrbUrl.Replace("_", "");
                                TributeUrl = txtTributeAddress.Text = _presenter.SequenceTributeName(tempTrbUrl, _Themename);
                            }
                            CreateTributeView.ActiveViewIndex = 1;
                            hfTributeValue.Value = _Themename;
                            PersonalDetails(_Themename);
                        }
                    }
                    else
                    {
                        ShowMessage(PortalValidationSummary.HeaderText, "Select the theme you would like to use for this tribute.", true);
                    }
                }
                else
                {
                    ShowMessage(PortalValidationSummary.HeaderText, "Select the type of tribute you would like to create.", true);
                }

                if (!string.IsNullOrEmpty(txtAdminEmail.Text))
                {
                    if (this._presenter.CheckEmailAvailability(txtAdminEmail.Text.Trim()))
                    {
                        _presenter.SetAccountEmailPassword(txtAdminEmail.Text);
                        StateManager stateManager = StateManager.Instance;
                        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
                        if (objSessionvalue != null)
                        {
                            _UserMail = objSessionvalue.UserEmail;
                        }
                    }
                    else
                    {
                        CreateTributeView.ActiveViewIndex = 0;
                        ShowMessage(PortalValidationSummary.HeaderText, "Email is already used in another Your Tribute account.", true);
                    }
                }
            }
            else
            {
                ShowMessage(PortalValidationSummary.HeaderText, "Select the type of " + WebConfig.ApplicationWord.ToString() + " you would like to create.", true);
            }
        }


        catch (Exception ex)
        {
            ShowMessage(PortalValidationSummary.HeaderText, ex.Message, true);
            lblErrMsg.Visible = true;
        }

    }
    //step 2
    protected void lbtn2Nextstep_Click(object sender, EventArgs e)
    {
        hideerrormessage();
        string error = string.Empty;
        TributesPortal.ResourceAccess.IOVS.Sanitise(txtarMessage.Text, ref error);
        if (!string.IsNullOrEmpty(error))
        {
            error = error + " in Welcome Message to tribute visitors! Only following characters are allowed! <br> a-z,A-Z,0-9,?,!,-,@,”,“,.,:,#,\",;,=,+,_,%";
            lblErrMsg.InnerHtml = SetHeaderMessage(error, PortalValidationSummary.HeaderText);
            lblErrMsg.Visible = true;
            return;
        }
        StateManager objtributeType = StateManager.Instance;
        if (objtributeType.Get("TributeType", StateManager.State.Session) != null)
            _Themename = objtributeType.Get("TributeType", StateManager.State.Session).ToString();
        else
            RedirectToLoginPage();

        SetTributeImage();
        SetErrorHeader3();
        Indecator1.Visible = false;
        Indecator2.Visible = false;
        DateTime? _DateTimeone = date1;
        DateTime? _DateTimetwo = date2;

        if (_Themename == "Memorial")
        {

            if ((_DateTimeone != null) && _DateTimeone > DateTime.Now)
            {
                lblErrMsg.InnerHtml = SetHeaderMessage("Date of birth should be less than or equal to current date.", PortalValidationSummary.HeaderText);
                lblErrMsg.Visible = true;
                Indecator1.Visible = true;
            }
            else
            {
                if ((_DateTimeone != null) && (_DateTimetwo != null))
                {
                    if (_DateTimetwo >= _DateTimeone)
                    {
                        if (_DateTimetwo < DateTime.Now)
                            CreateTributeView.ActiveViewIndex = 2;
                        else
                        {
                            lblErrMsg.InnerHtml = SetHeaderMessage("Date of death should be less than or equal to current date.", PortalValidationSummary.HeaderText);
                            lblErrMsg.Visible = true;
                            Indecator2.Visible = true;
                        }
                    }
                    else
                    {
                        lblErrMsg.InnerHtml = SetHeaderMessage("Date of death cannot be earlier than date of birth.", PortalValidationSummary.HeaderText);
                        lblErrMsg.Visible = true;
                    }
                }
                else if (_DateTimetwo != null)
                {
                    if (_DateTimetwo < DateTime.Now)
                        CreateTributeView.ActiveViewIndex = 2;
                    else
                    {
                        lblErrMsg.InnerHtml = SetHeaderMessage("Date of death should be less then or equal to current date.", PortalValidationSummary.HeaderText);
                        lblErrMsg.Visible = true;
                        Indecator2.Visible = true;
                    }
                }
                else if (_DateTimetwo == null)
                {
                    lblErrMsg.InnerHtml = SetHeaderMessage("Date of death cannot be earlier than date of birth.", PortalValidationSummary.HeaderText);
                    lblErrMsg.Visible = true;
                }
            }

        }
        else if (_Themename == "New Baby")
        {
            if ((_DateTimeone != null) && (_DateTimetwo != null))
            {
                lblErrMsg.InnerHtml = SetHeaderMessage("Please enter only a date of birth or a due date.", PortalValidationSummary.HeaderText);
                lblErrMsg.Visible = true;
            }
            else if ((_DateTimeone == null) && (_DateTimetwo != null))
            {
                if (_DateTimetwo > DateTime.Now)
                    CreateTributeView.ActiveViewIndex = 2;
                else
                {
                    lblErrMsg.InnerHtml = SetHeaderMessage("Due date should be greater then or equal to current date.", PortalValidationSummary.HeaderText);
                    lblErrMsg.Visible = true;
                    Indecator2.Visible = true;
                }
            }
            else if ((_DateTimeone != null) && (_DateTimetwo == null))
            {
                if (_DateTimeone < DateTime.Now)
                    CreateTributeView.ActiveViewIndex = 2;
                else
                {
                    lblErrMsg.InnerHtml = SetHeaderMessage("Date of birth should be less then or equal to current date.", PortalValidationSummary.HeaderText);
                    lblErrMsg.Visible = true;
                    Indecator1.Visible = true;
                }

            }
            else if ((_DateTimeone == null) && (_DateTimetwo == null))
            {
                lblErrMsg.InnerHtml = SetHeaderMessage("Please enter one of them valid date.", PortalValidationSummary.HeaderText);
                lblErrMsg.Visible = true;
            }

        }
        else if (_Themename == "Birthday")
        {
            if ((_DateTimeone != null) && _DateTimeone > DateTime.Now)
            {
                lblErrMsg.InnerHtml = SetHeaderMessage("Date of birth should be less then or equal to current date.", PortalValidationSummary.HeaderText);
                lblErrMsg.Visible = true;
            }
            else
            {
                if (_DateTimeone != null)
                {
                    CreateTributeView.ActiveViewIndex = 2;
                }
                else
                {
                    lblErrMsg.InnerHtml = SetHeaderMessage("Please enter a valid birth date.", PortalValidationSummary.HeaderText);
                    lblErrMsg.Visible = true;
                }
            }
        }
        else
        {
            if (_DateTimeone != null)
            {
                CreateTributeView.ActiveViewIndex = 2;
            }
            else
            {
                lblErrMsg.InnerHtml = SetHeaderMessage("Please enter valid date.", PortalValidationSummary.HeaderText);
                lblErrMsg.Visible = true;
            }
        }

    }
    protected void lbtn3Nextstep_Click(object sender, EventArgs e)
    {
        hideerrormessage();
        if (GetAdminEmails1())
        {
            StateManager objtributeType = StateManager.Instance;
            if (objtributeType.Get("TributeType", StateManager.State.Session) != null)
                _Themename = objtributeType.Get("TributeType", StateManager.State.Session).ToString();
            else
                RedirectToLoginPage();

            if (_accountType == 3)
                divTributrUrl.Visible = true;

            CreateTributeView.ActiveViewIndex = 3;
            PersonalDetailsEdit(_Themename);
            EditStep1();
        }
        else
        {
            lblErrMsg.InnerHtml = SetHeaderMessage("There is a duplicate value for administrator email address.", PortalValidationSummary.HeaderText);
            lblErrMsg.Visible = true;
        }
    }
    //step 4 next button
    protected void ltbn4Nextstep_Click(object sender, EventArgs e)
    {

        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);

        StateManager objtributeType = StateManager.Instance;
        if (objtributeType.Get("TributeType", StateManager.State.Session) != null)
        {
            _Themename = objtributeType.Get("TributeType", StateManager.State.Session).ToString();
        }
        #region Type 1 LabelText
        if (_accountType == 1)
        {
            
            SpanLowerBillingInfo.InnerHtml = "$ 0";
            if (objSessionvalue.UserType == 1)
            {
                tBodyPersonalUser.Visible = true;
                pnlLowerBillingInfo.Visible = true;
                if (_Themename.Equals("Memorial"))
                {
                    rdbAnnounceFree.Text = "Obituary";
                    rdbAnnounceFreeNoAds.Text = "Obituary (No Ads)";
                }
                else
                {
                    rdbAnnounceFree.Text = "Announcement";
                    rdbAnnounceFreeNoAds.Text = "Announcement (No Ads)";
                }
                rdbAnnounceFree.Checked = true;
                lblYearlyNoAdsAmount.Text = WebConfig.PhotoOneyearAmount;
            }
            if (objSessionvalue.UserType == 2)
            {
                if (_Themename.Equals("Memorial"))
                {
                    rdoMembershipFree.Text = "Obituary";
                }
                else
                {
                    rdoMembershipFree.Text = "Announcement";
                }
                rdoMembershipFree.Checked = true;
                tBodyPersonalUser.Visible = false;
                tableBodyAnnouncement.Visible = true;
                divCreditPointMessage.Visible = true;
                divTotalCredit.Visible = true;
                lblTotalCredit.Text = "0";
                _presenter.GetCreditPointCount();
            }
        }
        #endregion
        #region Type 2LabelText
        else if (_accountType == 2 && objSessionvalue.UserType == 2)
        {
            rdoMembershipYearly.Checked = true;
            tableBodyPhoto.Visible = true;

            if (_Themename.Equals("Memorial"))
            {
                rdoMembershipYearly.Text = "Photo Obituary (1 Year)";
                rdoMembershipLifetime.Text = "Photo Obituary (Life)";
            }
            else
            {
                rdoMembershipYearly.Text = "Photo Announcement (1 Year)";
                rdoMembershipLifetime.Text = "Photo Announcement (Life)";
            }
            //UsreType check

            if (objSessionvalue.UserType == 2)
            {
                divCreditPointMessage.Visible = true;
                divTotalCredit.Visible = true;
                _creditCost = int.Parse(WebConfig.PhotoYearlyCreditCost.Substring(0, 2).Trim());
                lblTotalCredit.Text = WebConfig.PhotoYearlyCreditCost.Substring(0, 2).Trim();
                lblYearlyCost.Text = WebConfig.PhotoYearlyCreditCost;//"1 Credit";
                lblLifetimeCost.Text = WebConfig.PhotoLifeTimeCreditCost;//"2 Credits";

                _presenter.GetCreditPointCount();

                //Get credit and cost /credit from DB
                _presenter.GetCreditCostMapping();
                grdCreditCostTable.DataSource = _creditCostMappingList;
                grdCreditCostTable.DataBind();

                if (_NetCreditCount == 0)
                {
                    DisplayBillingPanel();
                }
                else if (_NetCreditCount < _creditCost)
                {
                    DisplayBillingPanel();
                }

            }
            else
            {
                lblYearlyCost.Text = WebConfig.PhotoOneyearAmount;
                lblLifetimeCost.Text = WebConfig.PhotoLifeTimeAmount;

                BillingTotal.InnerHtml = WebConfig.PhotoOneyearAmount;
                DisplayBillingPanel();
                divCouponCode.Visible = true;
            }
        }
        #endregion
        #region Type 3 Labeltext
        else if (_accountType == 3)
        {
            rdoMembershipYearly.Checked = true;
            tableBodyPhoto.Visible = true;
            //UsreType check 
            if (objSessionvalue.UserType == 2)
            {
                divCreditPointMessage.Visible = true;
                divTotalCredit.Visible = true;
                lblTotalCredit.Text = WebConfig.TributeYearlyCreditCost.Substring(0, 2).Trim();
                _creditCost = int.Parse(WebConfig.TributeYearlyCreditCost.Substring(0, 2).Trim());

                lblYearlyCost.Text = WebConfig.TributeYearlyCreditCost;
                lblLifetimeCost.Text = WebConfig.TributeLifeTimeCreditCost;

                _presenter.GetCreditPointCount();

                //Get credit and cost /credit from DB
                _presenter.GetCreditCostMapping();
                grdCreditCostTable.DataSource = _creditCostMappingList;
                grdCreditCostTable.DataBind();

                if (_NetCreditCount == 0)
                {
                    DisplayBillingPanel();
                }
                else if (_NetCreditCount < _creditCost)
                {
                    DisplayBillingPanel();
                }

            }
            else
            {
                lblYearlyCost.Text = WebConfig.TributeOneyearAmount;
                lblLifetimeCost.Text = WebConfig.TributeLifeTimeAmount;

                BillingTotal.InnerHtml = WebConfig.TributeOneyearAmount;
                DisplayBillingPanel();
                divCouponCode.Visible = true;
            }

            rdoMembershipYearly.Text = "Tribute (1 Year)";
            rdoMembershipLifetime.Text = "Tribute (Life)";
        }
        #endregion
        hideerrormessage();
        _presenter.GetSelectedPaymentMode();
        CreateTributeView.ActiveViewIndex = 4;
        SetErrorHeader5();
    }
    protected void lbtnCreatetribute_Click(object sender, EventArgs e)
    {
        hideerrormessage();
        SpanExpirDate.Visible = false;
        try
        {
            if (PnlPaymentDetails.Visible == true)
            {
                if (ExpirationDate != null)
                {
                    if (ExpirationDate.AddMonths(1) > DateTime.Now)
                    {
                        if (CreateTribute())
                        {
                            CreateTributeView.ActiveViewIndex = 5;
                            PanelStep6.Visible = true;
                            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
                            Tributes objVal = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
                            _tributeURL = "http://video." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl;
                            SaveDefaultheme();
                        }
                    }
                    else
                    {
                        lblErrMsg.InnerHtml = SetHeaderMessage("Expiry Date cannot be less than current date.", PortalValidationSummary.HeaderText);
                        lblErrMsg.Visible = true;
                        SpanExpirDate.Visible = true;
                    }
                }
            }
            else if (rdoMembershipLifetime.Checked || rdoMembershipYearly.Checked)
            {
                if (CreateTribute())
                {
                    CreateTributeView.ActiveViewIndex = 5;
                    PanelStep6.Visible = true;
                    TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
                    Tributes objVal = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
                    if (WebConfig.ApplicationMode.Equals("local"))
                    {
                        _tributeURL = "http://localhost:4941/DevelopmentWebsite/" + objVal.TributeUrl;
                    }
                    else
                    {
                        if (objVal.TypeDescription.Equals("New Baby"))
                            _tributeURL = "http://newbaby." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl;
                        else
                            _tributeURL = "http://" + objVal.TypeDescription.ToLower() + "." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl;
                    }
                    SaveDefaultheme();
                }
                else
                {
                    lblErrMsg.InnerHtml = SetHeaderMessage("Expiry Date cannot be less than current date.", PortalValidationSummary.HeaderText);
                    lblErrMsg.Visible = true;
                    SpanExpirDate.Visible = true;
                }




            }
            // Free account
            else if (rdbAnnounceFree.Checked || rdbAnnounceFreeNoAds.Checked || rdoMembershipFree.Checked)
            {
                if (_accountType == 1)
                {
                    if (CreateTribute())
                    {
                        CreateTributeView.ActiveViewIndex = 5;
                        PanelStep6.Visible = true;
                        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
                        Tributes objVal = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
                        //LHK: redirection
                        if (WebConfig.ApplicationMode.Equals("local"))
                        {
                            _tributeURL = "http://localhost:4941/DevelopmentWebsite/" + objVal.TributeUrl;
                        }
                        else
                        {
                            if (objVal.TypeDescription.Equals("New Baby"))
                                _tributeURL = "http://newbaby." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl;
                            else
                                _tributeURL = "http://" + objVal.TypeDescription.ToLower() + "." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl;
                        }
                        SaveDefaultheme();
                    }
                    else
                    {
                        lblErrMsg.InnerHtml = SetHeaderMessage("Expiry Date cannot be less than current date.", PortalValidationSummary.HeaderText);
                        lblErrMsg.Visible = true;
                        SpanExpirDate.Visible = true;
                    }
                }
            }
        }
        catch (Exception a)
        {
            if (a.Message.StartsWith("PAYMENT"))
                lblErrMsg.InnerHtml = SetHeaderMessage("Transaction Failed! An error has while trying to connect to the payment gateway. Please try later. ", PortalValidationSummary.HeaderText);
            else if (a.Message.StartsWith("INTERNAL"))
                lblErrMsg.InnerHtml = SetHeaderMessage("Transaction Failed! While your transaction was successful and your credit card was charged but we could not process it at our end. Please contact the webmaster. ", PortalValidationSummary.HeaderText);
            else
                lblErrMsg.InnerHtml = SetHeaderMessage("Transaction Failed! Please try again later. ", PortalValidationSummary.HeaderText);
            lblErrMsg.Visible = true;
        }

        // we've finished processing, so let's display again lbtnCreatetribute (it was disabled on client side)
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>isValid = false; ToggleCreateTributeLink();</script>", false);
    }

    protected void lbtn65ChooseAccountType_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 4;
        SetErrorHeader5();
        lblErrMsg.Visible = false;
    }
    protected void lbtn64Review_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 3;
        lblErrMsg.Visible = false;
    }
    protected void lbtn63TributeManagement_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 2;
        lblErrMsg.Visible = false;
        SetErrorHeader3();
    }
    protected void lbtn61EnterTributeDetails_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 0;
        SetErrorHeader12();
        SetDefaultFirst();
        lblErrMsg.Visible = false;
    }
    protected void lbtn62MoreTributeDetails_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 1;
        SetErrorHeader12();
        SetMessageMoxLength();
        lblErrMsg.Visible = false;
    }
    protected void lbtn54Review_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 3;
        lblErrMsg.Visible = false;
    }
    protected void lbtn53TributeManagement_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 2;
        lblErrMsg.Visible = false;
        SetErrorHeader3();
    }
    protected void lbtn52MoreTributeDetails_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 1;
        SetErrorHeader12();
        SetMessageMoxLength();
        lblErrMsg.Visible = false;
    }
    protected void lbtn51EnterTributeDetails_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 0;
        SetErrorHeader12();
        SetDefaultFirst();

    }
    protected void lbtn41EnterTributeDetails_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 0;
        lblErrMsg.Visible = false;
        SetErrorHeader12();
        SetDefaultFirst();
    }
    protected void lbtn42MoreTributeDetails_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 1;
        lblErrMsg.Visible = false;
        SetErrorHeader12();
        SetMessageMoxLength();
    }
    protected void lbtn43TributeManagement_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 2;
        lblErrMsg.Visible = false;
        SetErrorHeader3();
    }
    protected void lbtn21EnterTributeDetails_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 0;
        SetErrorHeader12();
        SetDefaultFirst();
        SetTributeImage();
    }
    protected void lbtn31EnterTributeDetails_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 0;
        SetErrorHeader12();
        SetDefaultFirst();
    }
    protected void lbtn32MoreTributeDetails_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 1;
        SetErrorHeader12();
        SetMessageMoxLength();
        lblErrMsg.Visible = false;
    }

    protected void rdoTributeType_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.GetStateList();
    }
    protected void lbtnEdit1_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 0;
        SetErrorHeader12();
        SetDefaultFirst();

    }
    protected void rdoEditTributeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.GetTributeThemesforEdit();
    }

    protected void lbtnEdit2_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 1;
        SetErrorHeader12();
        SetMessageMoxLength();
    }
    protected void lbtneditstep3_Click(object sender, EventArgs e)
    {
        CreateTributeView.ActiveViewIndex = 2;
        SetErrorHeader3();
    }


   protected void rdbAnnounceFreeNoAds_CheckedChanged(object sender, EventArgs e)
   {
       StateManager stateManager = StateManager.Instance;
       SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);

       ScriptManager.RegisterClientScriptBlock(rdbAnnounceFreeNoAds, GetType(), "HidePanel", "executeBeforeLoad();", true);
       
       if (objSessionvalue.UserType == 2)
       {
           CreditGrid.Visible = false;
           PanelBillingInfo.Visible = false;
           PnlPaymentDetails.Visible = false;

           //Getting current Credit points in the user account
           _presenter.GetCreditPointCount();

           //Getting credit cost mapping list to display in a tabular form.
           _presenter.GetCreditCostMapping();
           //Rebinding the grid to update as per the package type selected.
           grdCreditCostTable.DataSource = _creditCostMappingList;
           grdCreditCostTable.DataBind();

           if (_accountType == 2)
           {
               lblTotalCredit.Text = int.Parse(WebConfig.PhotoLifeTimeCreditCost.Substring(0, 2).Trim()).ToString();
               _creditCost = int.Parse(WebConfig.PhotoLifeTimeCreditCost.Substring(0, 2).Trim());
               if (_NetCreditCount < _creditCost)
               {
                   DisplayBillingPanel();
               }
           }
           else if (_accountType == 3)
           {
               lblTotalCredit.Text = int.Parse(WebConfig.TributeLifeTimeCreditCost.Substring(0, 2).Trim()).ToString();
               _creditCost = int.Parse(WebConfig.TributeLifeTimeCreditCost.Substring(0, 2).Trim());
               if (_NetCreditCount < _creditCost)
               {
                   DisplayBillingPanel();
               }
           }
       }
       else
       {
           ScriptManager.RegisterClientScriptBlock(rdoMembershipLifetime, GetType(), "HidePanel", "hideWideRows();", true);
           if(_accountType==1)
           {
               BillingTotal.InnerHtml = WebConfig.PhotoOneyearAmount;
           }

           else if (_accountType == 2)
           {
               BillingTotal.InnerHtml = WebConfig.PhotoLifeTimeAmount;
           }
           else if (_accountType == 3)
           {
               BillingTotal.InnerHtml = WebConfig.TributeLifeTimeAmount;
           }
           DisplayBillingPanel();
           _presenter.GetCrditCardDetails();
           _presenter.GetSelectedPaymentMode();
           PamentSel.Visible = false;
           txtCouponCode.Focus();
           divCouponCode.Visible = true;
       }
       pnlLowerBillingInfo.Attributes.Add("style","display:none");
   }


   protected void rdbAnnounceFree_CheckedChanged(object sender, EventArgs e)
   {
       ScriptManager.RegisterClientScriptBlock(rdbAnnounceFree, GetType(), "HidePanel", "hideWideRows();", true);
       pnlLowerBillingInfo.Attributes.Add("style","display:block");
       PortalValidationSummary.Attributes.Add("style", "display:none;");// = false;
       lblErrMsg.Visible = true;
       PanelBillingInfo.Visible = false;
       PanelFreeTrial.Visible = false;
       PnlPaymentDetails.Visible = false;
       divTotalCredit.Visible = false;
       lblErrMsg.Attributes.Add("style","display:none;");// = false;
       lblErrMsg.InnerHtml = String.Empty;
       ScriptManager.RegisterClientScriptBlock(rdbAnnounceFree, GetType(), "ChangePanel", "executeBeforeLoad();", true);
   }


    protected void rdoMembershipLifetime_CheckedChanged(object sender, EventArgs e)
    {
        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);


        if (objSessionvalue.UserType == 2)
        {
            CreditGrid.Visible = false;
            PanelBillingInfo.Visible = false;
            PnlPaymentDetails.Visible = false;

            //Getting current Credit points in the user account
            _presenter.GetCreditPointCount();

            //Getting credit cost mapping list to display in a tabular form.
            _presenter.GetCreditCostMapping();
            //Rebinding the grid to update as per the package type selected.
            grdCreditCostTable.DataSource = _creditCostMappingList;
            grdCreditCostTable.DataBind();

            if (_accountType == 2)
            {
                lblTotalCredit.Text = int.Parse(WebConfig.PhotoLifeTimeCreditCost.Substring(0, 2).Trim()).ToString();
                _creditCost = int.Parse(WebConfig.PhotoLifeTimeCreditCost.Substring(0, 2).Trim());
                if (_NetCreditCount < _creditCost)
                {
                    DisplayBillingPanel();
                }
            }
            else if (_accountType == 3)
            {
                lblTotalCredit.Text = int.Parse(WebConfig.TributeLifeTimeCreditCost.Substring(0, 2).Trim()).ToString();
                _creditCost = int.Parse(WebConfig.TributeLifeTimeCreditCost.Substring(0, 2).Trim());
                if (_NetCreditCount < _creditCost)
                {
                    DisplayBillingPanel();
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(rdoMembershipLifetime, GetType(), "HidePanel", "hideWideRows();", true);
            if (_accountType == 2)
            {
                BillingTotal.InnerHtml = WebConfig.PhotoLifeTimeAmount;
            }
            else if (_accountType == 3)
            {
                BillingTotal.InnerHtml = WebConfig.TributeLifeTimeAmount;
            }
            DisplayBillingPanel();
            _presenter.GetCrditCardDetails();
            _presenter.GetSelectedPaymentMode();
            PamentSel.Visible = false;
            txtCouponCode.Focus();
        }


    }

    protected void rdoMembershipYearly_CheckedChanged(object sender, EventArgs e)
    {

        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);

        if (objSessionvalue.UserType == 2)
        {
            CreditGrid.Visible = false;
            PanelBillingInfo.Visible = false;
            PnlPaymentDetails.Visible = false;

            //Getting current Credit points in the user account
            _presenter.GetCreditPointCount();

            //Getting credit cost mapping list to display in a tabular form.
            _presenter.GetCreditCostMapping();
            //Rebinding the grid to update as per the package type selected.
            grdCreditCostTable.DataSource = _creditCostMappingList;
            grdCreditCostTable.DataBind();

            if (_accountType == 2)
            {
                lblTotalCredit.Text = int.Parse(WebConfig.PhotoYearlyCreditCost.Substring(0, 2).Trim()).ToString();
                _creditCost = int.Parse(WebConfig.PhotoYearlyCreditCost.Substring(0, 2).Trim());
                if (_NetCreditCount < _creditCost)
                {
                    DisplayBillingPanel();
                }
            }
            else if (_accountType == 3)
            {
                lblTotalCredit.Text = int.Parse(WebConfig.TributeYearlyCreditCost.Substring(0, 2).Trim()).ToString();
                _creditCost = int.Parse(WebConfig.TributeYearlyCreditCost.Substring(0, 2).Trim());
                if (_NetCreditCount < _creditCost)
                {
                    DisplayBillingPanel();
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(rdoMembershipYearly, GetType(), "HidePanel", "hideWideRows();", true);
            if (_accountType == 2)
            {
                BillingTotal.InnerHtml = WebConfig.PhotoOneyearAmount;
            }
            else if (_accountType == 3)
            {
                BillingTotal.InnerHtml = WebConfig.TributeOneyearAmount;
            }
            DisplayBillingPanel();
            _presenter.GetCrditCardDetails();
            _presenter.GetSelectedPaymentMode();
            txtCouponCode.Focus();
            PamentSel.Visible = true;
        }
    }

    protected void ddlCCCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.GetCCStateList();
    }
    protected void dlTributeTheames_ItemDataBound(object sender, DataListItemEventArgs e)
    {

        if ((e.Item.ItemType != ListItemType.Item) && (e.Item.ItemType != ListItemType.AlternatingItem))
        {
            return;
        }
        else
        {
            //String SCRIPT = "SetUniqueRadioButton('Portfolios', this,'" + tableid.ClientID + "')";            
            String SCRIPT = "SetUniqueRadioButton('Portfolios', this)";
            RadioButton rdb = (RadioButton)e.Item.FindControl("rdbButton");
            rdb.Attributes.Add("onclick", SCRIPT);
        }
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

    private void SetTributeUrlname()
    {

        StateManager objtributeType = StateManager.Instance;
        if (objtributeType.Get("TributeType", StateManager.State.Session) != null)
        {
            _Themename = objtributeType.Get("TributeType", StateManager.State.Session).ToString();
            _tributeName = _Themename.ToLower().Replace(" ", "");
        }
        else
            RedirectToLoginPage();


    }

    protected void lbtncheckEmail_Click(object sender, EventArgs e)
    {
        lblErrMsg.Visible = false;
        if (!string.IsNullOrEmpty(txtAdminEmail.Text))
        {
            if (this._presenter.CheckEmailAvailability(txtAdminEmail.Text.Trim()))
            {
                spanAvailableEmail.Attributes.Add("class", "availabilityNotice-Available");
                spanAvailableEmail.InnerHtml = "Available!";
            }
            else
            {
                spanAvailableEmail.Attributes.Add("class", "availabilityNotice-Unavailable");
                spanAvailableEmail.InnerHtml = "Unavailable";
            }

            spanAvailableEmail.Visible = true;
        }
    }

    protected void lbtncheckAddress_Click(object sender, EventArgs e)
    {
        SetTributeUrlname();
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
            txtTributeAddress.Enabled = false;
            SetImageUnavailableStatus();
        }
        else
        {
            SetImageAvailableStatus();

        }
        SpanAvailable.Visible = true;
    }

    protected void lbtncheckAvailability_Click(object sender, EventArgs e)
    {
        Status = "0";
        SetTributeUrlname();
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

    private void SetTheme_()
    {
        ltrltheme.Text = string.Empty;
        string[] controlname = HiddenField1.Value.Split(':');
        if (Session["themes_"] != null)
        {
            IList<Themes> themes_ = (IList<Themes>)Session["themes_"];

            if (themes_.Count > 0)
            {
                string folderPathPrefix = string.Empty;
                if (WebConfig.ApplicationMode.Equals("local"))
                {
                    folderPathPrefix = WebConfig.AppBaseDomain;
                }
                else
                {
                    folderPathPrefix = WebConfig.AppBaseDomain.ToLower().Replace("http", "https");
                }
                StringBuilder strtheme = new StringBuilder();
                strtheme.Append("<div class='yt-ChannelSelected'>");
                strtheme.Append("<fieldset class='yt-ThemeSelection yt-CompactRadioList'>");
                strtheme.Append("<div class='yt-ThemeSet' id='yt-Themes" + themes_[0].Tributetype.Replace(" ", "") + "'>");
                for (int i = 0; i < themes_.Count; i++)
                {

                    strtheme.Append("<div class='yt-Form-Field yt-Form-Field-Radio yt-top' style ='margin-top: -12px;' id='yt-" + themes_[i].ThemeValue + "'>");
                    strtheme.Append("<input name='rdoTheme' onclick='SetThemeFolder(" + '"' + themes_[i].ThemeValue + '"' + "," + '"' + themes_[i].ThemeName.Replace("'", "%26") + '"' + "," + '"' + themes_[i].ThemeId + '"' + "," + '"' + themes_[i].FolderName + '"' + ");' type='radio' id='rdo" + themes_[i].ThemeValue + "'");
                    if (controlname.Length >= 2)
                    {
                        if (themes_[i].ThemeValue == controlname[0].ToString())
                        {
                            strtheme.Append(" checked='checked' ");
                            hfSeledctedTheme.Value = themes_[i].ThemeId.ToString();
                            SetThemeforStep4(themes_[i].ThemeValue, themes_[i].ThemeName, themes_[i].ThemeId.ToString(), themes_[i].FolderName);
                        }
                    }
                    else
                    {
                        if (i == 2)
                        {
                            strtheme.Append(" checked='checked' ");
                            hfSeledctedTheme.Value = themes_[i].ThemeId.ToString();
                            SetThemeforStep4(themes_[i].ThemeValue, themes_[i].ThemeName, themes_[i].ThemeId.ToString(), themes_[i].FolderName);
                        }
                    }
                    if (themes_.Count == 1)
                    {
                        strtheme.Append(" checked='checked' ");
                        hfSeledctedTheme.Value = themes_[i].ThemeId.ToString();
                        SetThemeforStep4(themes_[i].ThemeValue, themes_[i].ThemeName, themes_[i].ThemeId.ToString(), themes_[i].FolderName);
                    }
                    strtheme.Append("value='" + themes_[i].ThemeValue + "' />");
                    strtheme.Append("<img src='" + folderPathPrefix + "assets/themes/" + themes_[i].FolderName + "/thumb.gif' />");
                    strtheme.Append("<a href='javascript: void(0);' onclick=\"viewThemeFolderSample('" + themes_[i].FolderName + "');\">View Sample</a>");
                    strtheme.Append(" <label for='rdo" + themes_[i].ThemeValue + "'>");
                    strtheme.Append(themes_[i].ThemeName + "</label>");
                    strtheme.Append("</div>");
                }
                strtheme.Append("</div>");
                strtheme.Append("</fieldset>");
                strtheme.Append("</div>");

                ltrltheme.Text = strtheme.ToString();
            }
        }

    }


    /// <summary>
    /// Set Tribute Image.
    /// </summary>
    //static int statimg = false;
    private void SetTributeImage()
    {
        StateManager objtributeType = StateManager.Instance;
        if (objtributeType.Get("TributeType", StateManager.State.Session) != null)
            _Themename = objtributeType.Get("TributeType", StateManager.State.Session).ToString();
        else
            RedirectToLoginPage();

        string srcPath_ = string.Empty;

        //Path where you want to upload the file.
        string[] eventPath = CommonUtilities.GetPath();
        string fileName = Path.GetFileName(hdnStoryImageURL.Value);
        if (fileName.Length == 0)
        {
            srcPath_ = Server.MapPath(imgTributePhoto.ImageUrl);
            fileName = Path.GetFileName(srcPath_);
            srcPath_ = eventPath[0] + "/" + eventPath[1] + "/images/" + fileName;

        }
        string DefaultPath = eventPath[0] + "/" + eventPath[1] + "/" + _presenter.View.TributeUrl.Replace(" ", "_") + "_" + _Themename.Replace(" ", "_") + "/" + eventPath[7];
        string srcPath = eventPath[0] + "/" + eventPath[1] + "/" + eventPath[6] + "/" + fileName;
        if (hdnStoryImageURL.Value != "")
        {
            _Tributeimage = _presenter.View.TributeUrl.Replace(" ", "_") + "_" + _Themename.Replace(" ", "_") + "/" + eventPath[7] + "/" + fileName;
        }
        else
        {
            hdnStoryImageURL.Value = imgTributePhoto.ImageUrl;
            string fileName_ = Path.GetFileName(imgTributePhoto.ImageUrl);
            _Tributeimage = "images/" + fileName_;
            //_Tributeimage = _presenter.View.TributeUrl.Replace(" ", "_") + "_" + _Themename.Replace(" ", "_") + "/" + eventPath[7] + "/" + fileName_;
            //fileName = fileName_;

        }

        Session["_Tributeimage"] = _Tributeimage;

        try
        {
            DirectoryInfo dirObj = new DirectoryInfo(DefaultPath);
            if (!dirObj.Exists)
            {
                dirObj.Create();
            }

            if (File.Exists(srcPath))
            {
                if (File.Exists(Path.Combine(DefaultPath, fileName)))
                {
                    File.Delete(Path.Combine(DefaultPath, fileName));
                }
                File.Copy(srcPath, Path.Combine(DefaultPath, fileName));
                imgTributePhoto.ImageUrl = eventPath[9] + _Tributeimage;
                tributephoto_.Src = imgTributePhoto.ImageUrl;

            }
            else
            {
                if (srcPath_.Length != 0)
                {
                    if (!File.Exists(Path.Combine(DefaultPath, fileName)))
                        File.Copy(srcPath_, Path.Combine(DefaultPath, fileName));
                    imgTributePhoto.ImageUrl = eventPath[9] + _Tributeimage;
                    tributephoto_.Src = imgTributePhoto.ImageUrl;
                }
            }
            if (!WebConfig.ApplicationMode.Equals("local"))
            {
                // ss in https(httpss) Issue: Mohit Gupta 16-July
                if (imgTributePhoto.ImageUrl.ToLower().IndexOf("https") < 0)
                {
                    tributephoto_.Src = imgTributePhoto.ImageUrl.ToLower().Replace("http", "https");
                }
            }
        }
        catch (Exception a)
        {
            lblErrMsg.InnerHtml = SetHeaderMessage(a.Message, PortalValidationSummary.HeaderText);
            lblErrMsg.Visible = true;
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
            CcType = CardTypeV1.DI;
        }
        if (Request.Form["rdoCCType"] == "Amex")
        {
            CcType = CardTypeV1.AM;
        }
        return CcType;
    }

    private bool CreateTribute()
    {
        bool retvalue = false;
        bool _transaction = true;
        PaymentGateWay objPay = new PaymentGateWay();
        String UserMail = string.Empty;
        string Firstname = string.Empty;
        string LastName = string.Empty;
        double Couponamount = 0;
        string strBillingTotal;
        int NewUpdatedCredit = 0;

        //BeanStream Responce string
        var bIsSuccess = false;

        //Remove Commented code for Payment Gateway :Amit :2/5/8

        try
        {
            int couponType = 0;
            if (rdoMembershipLifetime.Checked)
                couponType = 3;
            else
                couponType = 2;
            StateManager statemail = StateManager.Instance;
            SessionValue objSessionmail = (SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionmail != null)
            {
                Firstname = objSessionmail.FirstName;
                LastName = objSessionmail.LastName;
                UserMail = objSessionmail.UserEmail;
            }
            if (_accountType == 3)
            {
                this._presenter.CheckAvailability();
                if (int.Parse(Status) != 0)
                {
                    //Tribute Url already exists
                    AvailabilityStatus();
                    return false;
                }
            }
            if (!rdoMembershipFree.Checked)
            {
                #region CouponCheck&Update
                if (txtCouponCode.Text != string.Empty)
                {
                    int availability = _presenter.GetCouponAvailable(txtCouponCode.Text, couponType);

                    if (availability == 1)
                    {
                        //SetCouponAvailableStatus();

                        if (rdoMembershipLifetime.Checked)
                        {
                            if (_accountType == 2)
                            {
                                amount = WebConfig.PhotoLifeTimeAmount;
                            }
                            else if (_accountType == 3)
                            {
                                amount = WebConfig.TributeLifeTimeAmount;
                            }
                            Couponamount = Convert.ToDouble(amount.Substring(1, amount.Length - 1));

                            // BillingTotal.InnerHtml = amount;
                        }
                        else if (rdoMembershipYearly.Checked)
                        {
                            if (_accountType == 2)
                            {
                                amount = WebConfig.PhotoOneyearAmount;
                            }
                            else if (_accountType == 3)
                            {
                                amount = WebConfig.TributeOneyearAmount;
                            }
                            Couponamount = Convert.ToDouble(amount.Substring(1, amount.Length - 1));
                            //BillingTotal.InnerHtml = amount;
                        }
                            //LHK: is this case tested ever while development. issue in local not on staging
                        else if (rdbAnnounceFreeNoAds.Checked)
                        {
                            if (_accountType == 1)
                            {
                                amount = WebConfig.PhotoOneyearAmount;
                            }
                            Couponamount = Convert.ToDouble(amount.Substring(1, amount.Length - 1));
                        }
                        //LHK:till here is this case tested ever while development.
                        if (this._presenter.View.IsPercentage == false)
                            Couponamount = Couponamount - int.Parse(this._presenter.View.Denomination);
                        else
                            Couponamount = Couponamount - ((int.Parse(this._presenter.View.Denomination) * Couponamount) / 100);
                        if (Couponamount < 0)
                            Couponamount = 0;
                        BillingTotal.InnerHtml = ("$" + Couponamount.ToString());
                        //totalValue = Convert.ToInt32(amount);
                        double.TryParse(amount.Substring(1, 5).Trim(), out totalValue);
                    }
                    else
                    {
                        SetCouponUnAvailableStatus();
                        return false;
                    }
                }
                #endregion
                else if (PnlPaymentDetails.Visible == true)
                {
                    strBillingTotal = Convert.ToString(BillingTotal.InnerText);
                    Couponamount = Convert.ToDouble(strBillingTotal.Substring(1, strBillingTotal.Length - 1).Trim());
                    Couponamount = Math.Round(Couponamount, 2);
                    // totalValue = Convert.ToInt32(Couponamount);
                    totalValue = Couponamount;
                    //double.TryParse(Couponamount, out totalValue);
                }
                else
                {
                    Couponamount = 0;
                }
                try
                {
                    StateManager stateManager = StateManager.Instance;
                    SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);

                    _amount = Couponamount.ToString();
                    // Pay only if coupon amount is graeter than 0 or billing panel is visible
                    if (Couponamount > 0 && PnlPaymentDetails.Visible == true)
                    {
                        //diffrentiating a numeric and a decimal number.
                        if (Couponamount == Math.Round(Couponamount))
                        {
                            //for Non- decimal values.
                            sBeanStreamResponce = objPay.PayYourBill(TributePortalSecurity.Security.DecryptSymmetric(this._presenter.View.CreditCardNo), txtCCVerification.Text, int.Parse(ddlCCMonth.SelectedValue), int.Parse(txtCCYear.Text), Couponamount, SelectCreditCardType(), txtCCName.Text.Trim(), "", _presenter.View.Address.Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay), txtCCCity.Text, StateV1.CA, CountryV1.US, txtCCZipCode.Text, _presenter.View.Telephone.ToString(), txtEmailAddress.Text.Trim(), HttpContext.Current.Request.UserHostAddress.ToString(), out confirmationId, out errorMesg, out _transaction);
                        }
                        else
                        {
                            //for decimal case
                            sBeanStreamResponce = objPay.PayYourBill(TributePortalSecurity.Security.DecryptSymmetric(this._presenter.View.CreditCardNo), txtCCVerification.Text, int.Parse(ddlCCMonth.SelectedValue), int.Parse(txtCCYear.Text), Couponamount, SelectCreditCardType(), txtCCName.Text.Trim(), "", _presenter.View.Address.Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay), txtCCCity.Text, StateV1.CA, CountryV1.US, txtCCZipCode.Text, _presenter.View.Telephone.ToString(), txtEmailAddress.Text.Trim(), HttpContext.Current.Request.UserHostAddress.ToString(), out confirmationId, out errorMesg, out _transaction);
                        }
                    }
                    else
                    {
                        _transaction = true;
                        confirmationId = "";
                    }
                }
                catch (ApplicationException ex)
                {
                    throw ex;
                }
            }

            #region TransactionTrue
            if (_transaction)
            {
                Application["Identity"] = (System.Decimal)_presenter.CreateTribute();
                
                bool resultSubscribe = AddMailChimpSubscriber(PackageId);
                if (int.Parse(Application["Identity"].ToString()) > 0)
                {
                    if (Request.QueryString["VideoTributeId"] != null)
                    {
                        _presenter.LinkVideoTribute(int.Parse(Request.QueryString["VideoTributeId"]), int.Parse(Application["Identity"].ToString()));
                    }

                    #region NewUpdatedCredit
                    if ((objSessionmail.UserType == 2) && (_accountType > 1))
                    {
                        //make a fresh entry for New Credit Points in CreditPointTransaction table
                        _presenter.GetCreditPointCount();
                        if (PnlPaymentDetails.Visible == true)
                        {
                            if (_accountType == 2)
                            {
                                if (rdoMembershipYearly.Checked)
                                {
                                    NewUpdatedCredit = int.Parse(Session["CreditPointSelected"].ToString()) + _NetCreditCount - int.Parse(WebConfig.PhotoYearlyCreditCost.Substring(0, 2).Trim());
                                    double.TryParse(WebConfig.PhotoOneyearAmount.Substring(1, 5).Trim(), out totalValue);
                                }
                                else if (rdoMembershipLifetime.Checked)
                                {
                                    NewUpdatedCredit = int.Parse(Session["CreditPointSelected"].ToString()) + _NetCreditCount - int.Parse(WebConfig.PhotoLifeTimeCreditCost.Substring(0, 2).Trim());
                                    double.TryParse(WebConfig.PhotoLifeTimeAmount.Substring(1, 5).Trim(), out totalValue);
                                }
                            }
                            else if (_accountType == 3)
                            {
                                if (rdoMembershipYearly.Checked)
                                {
                                    NewUpdatedCredit = int.Parse(Session["CreditPointSelected"].ToString()) + _NetCreditCount - int.Parse(WebConfig.TributeYearlyCreditCost.Substring(0, 2).Trim());
                                    double.TryParse(WebConfig.TributeOneyearAmount.Substring(1, 5).Trim(), out totalValue);
                                }
                                else if (rdoMembershipLifetime.Checked)
                                {
                                    NewUpdatedCredit = int.Parse(Session["CreditPointSelected"].ToString()) + _NetCreditCount - int.Parse(WebConfig.TributeLifeTimeCreditCost.Substring(0, 2).Trim());
                                    double.TryParse(WebConfig.TributeLifeTimeAmount.Substring(1, 5).Trim(), out totalValue);
                                }
                            }
                        }
                        else
                        {
                            if (_accountType == 2)
                            {
                                if (rdoMembershipYearly.Checked)
                                {
                                    NewUpdatedCredit = _NetCreditCount - int.Parse(WebConfig.PhotoYearlyCreditCost.Substring(0, 2).Trim());
                                    double.TryParse(WebConfig.PhotoOneyearAmount.Substring(1, 5).Trim(), out totalValue);
                                }
                                else if (rdoMembershipLifetime.Checked)
                                {
                                    NewUpdatedCredit = _NetCreditCount - int.Parse(WebConfig.PhotoLifeTimeCreditCost.Substring(0, 2).Trim());
                                    double.TryParse(WebConfig.PhotoLifeTimeAmount.Substring(1, 5).Trim(), out totalValue);
                                }
                            }
                            else if (_accountType == 3)
                            {
                                if (rdoMembershipYearly.Checked)
                                {
                                    NewUpdatedCredit = _NetCreditCount - int.Parse(WebConfig.TributeYearlyCreditCost.Substring(0, 2).Trim());
                                    double.TryParse(WebConfig.TributeOneyearAmount.Substring(1, 5).Trim(), out totalValue);
                                }
                                else if (rdoMembershipLifetime.Checked)
                                {
                                    NewUpdatedCredit = _NetCreditCount - int.Parse(WebConfig.TributeLifeTimeCreditCost.Substring(0, 2).Trim());
                                    double.TryParse(WebConfig.TributeLifeTimeAmount.Substring(1, 5).Trim(), out totalValue);
                                }
                            }
                        }
                    }

                    #endregion

                    if (!string.IsNullOrEmpty(txtCouponCode.Text))
                        this._presenter.UpdateUsedCouponDetails(txtCouponCode.Text);

                    #region Package specification
                    if ((PackageId == 8) || (PackageId == 3))
                    {
                        //to get the information abt the payment package used
                        _presenter.TriputePackageInfo(int.Parse(Application["Identity"].ToString()));
                    }
                    StateManager objtributeType = StateManager.Instance;
                    if (objtributeType.Get("TributeType", StateManager.State.Session) != null)
                        _Themename = objtributeType.Get("TributeType", StateManager.State.Session).ToString();
                    else
                        RedirectToLoginPage();

                    Tributes objTribute = new Tributes();
                    objTribute.TributeId = int.Parse(Application["Identity"].ToString());
                    objTribute.TributeName = txtTributeName.Text;
                    objTribute.TypeDescription = _Themename;
                    objTribute.CreatedDate = DateTime.Today;
                    objTribute.TributeUrl = this._presenter.View.TributeUrl;
                    TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
                    stateManager.Add("TributeSession", objTribute, TributesPortal.Utilities.StateManager.State.Session);

                    //Insert CCDetails
                    if ((PnlPaymentDetails.Visible == true) && (_accountType > 1))
                    {
                        CCIdentity = _presenter.InsertCCDetails((SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session), objTribute);
                    }

                    this._presenter.InsertPackageDetails(Application["Identity"].ToString(), CCIdentity.ToString(), confirmationId);

                    if ((PnlPaymentDetails.Visible == true) && (objSessionmail.UserType == 1) && (_accountType > 1))
                    {
                        //send sponsor email
                        _presenter.SendSponsorTransactionEmail(confirmationId);
                    }
                    //updating credits and sending mail.
                    if ((objSessionmail.UserType == 2) && (_accountType > 1))
                    {
                        // Make an entry for new credits points attained by the user if the new updated credit is more than 0
                        if (NewUpdatedCredit >= 0)
                        {
                            this._presenter.InsertCurrentCreditPoints(NewUpdatedCredit, CCIdentity.ToString(), confirmationId);


                            //to get the information abt the payment package used
                            _presenter.TriputePackageInfo(int.Parse(Application["Identity"].ToString()));

                            //If the New updated credit in the Ctreator's  is greater than or equal to 0, then only the receipt and mail should be sent
                            if (NewUpdatedCredit >= 0)
                                //to get the sponsorship receipt and send the same on Email 
                                _presenter.OnViewInitialized();
                            //End

                            Master.CreditLinkButton = NewUpdatedCredit.ToString();
                        }
                    }

                    if ((PackageId == 1) || (PackageId == 4) || (PackageId == 6))
                    {
                        divPaid.Visible = true;
                        package11.InnerHtml = "The <b>" + objTribute.TributeName + "</b> " + objTribute.TypeDescription + " Tribute has a <b>lifetime</b> account and will <b>never expire</b>. <br/>";
                        package12.InnerHtml = "<br/>Your credit card will show a charge from \"YOUR TRIBUTE\" for <b>$" + AmountPaid + "</b>. Your transaction ID is <b>" + TransactionId + "</b>. <br/>";


                        package11.Visible = true;
                        package12.Visible = true;
                        package2.Visible = false;
                        trial.Visible = false;
                        autoRenew.Visible = false;
                    }
                    else if ((PackageId == 2) || (PackageId == 5) || ((PackageId == 7)))
                    {
                        divPaid.Visible = true;
                        if (rdoYearlyAutoRenew.Checked)
                        {
                            package2.InnerHtml = "The <b>" + objTribute.TributeName + "</b> " + objTribute.TypeDescription + " Tribute will automatically renew in <b>1 Year</b> on <b>" + DateTime.Now.AddMonths(12).ToString("MMMM dd, yyyy") + "</b>.<br/><br/>";
                            autoRenew.Visible = true;
                            autoRenew.InnerHtml += "<br/><p>Your credit card will show a charge from \"YOUR TRIBUTE\" for <b>$" + AmountPaid + "</b>. Your transaction ID is <b>" + TransactionId + "</b>.</P>";
                        }
                        else
                        {
                            autoRenew.Visible = false;
                            package2.InnerHtml = "The <b>" + objTribute.TributeName + "</b> " + objTribute.TypeDescription + " Tribute will expire in <b>1 Year</b> on <b>" + DateTime.Now.AddMonths(12).ToString("MMMM dd, yyyy") + "</b>.<br/><br/>";
                            //Start - Modification on 17-Dec-09 for the enhancement 1 of the Phase 1
                            //This message should be shown only when the credit card is charged
                            if (Couponamount > 0)
                                package2.InnerHtml += "<br/><p>Your credit card will show a charge from \"YOUR TRIBUTE\" for <b>$" + AmountPaid + "</b>. Your transaction ID is <b>" + TransactionId + "</b>.</P>";
                            //End
                        }

                        package2.Visible = true;
                        package11.Visible = false;
                        package12.Visible = false;
                        trial.Visible = false;
                    }
                    else
                    {
                        divPaid.Visible = false;
                        package11.Visible = false;
                        package12.Visible = false;
                        package2.Visible = false;
                        autoRenew.Visible = false;
                        recieveMail.Visible = false;
                        trial.Visible = true;
                    }
                    #endregion
                    //to create default file for url rewriting.
                    if (_Themename == "New Baby")
                        _presenter.CreateDefaultFolder(WebConfig.NewBabyFolderPath);
                    else if (_Themename == "Birthday")
                        _presenter.CreateDefaultFolder(WebConfig.BirthdayFolderPath);
                    else if (_Themename == "Graduation")
                        _presenter.CreateDefaultFolder(WebConfig.GraduationFolderPath);
                    else if (_Themename == "Wedding")
                        _presenter.CreateDefaultFolder(WebConfig.WeddingFolderPath);
                    else if (_Themename == "Anniversary")
                        _presenter.CreateDefaultFolder(WebConfig.AnniversaryFolderPath);
                    else if (_Themename == "Memorial")
                        _presenter.CreateDefaultFolder(WebConfig.MemorialFolderPath);

                    //to save video tribute if user is coming from Video Upload page
                    if (!Equals((stateManager.Get("TokenDetails", StateManager.State.Session)), null))
                    {
                        _presenter.SaveVideoTribute();
                        stateManager.Add("TokenDetails", null, StateManager.State.Session); //to set null to tokendetails session
                    }

                    // Get the Video object of the Video Tribute to save in Corresponding Memorial Tribute
                    if (Request.QueryString["VideoTributeId"] != null)
                    {
                        _presenter.SaveVideoForMemTribute(int.Parse(Request.QueryString["VideoTributeId"]));
                    }
                    objtributeType.Remove("TributeType", StateManager.State.Session);
                    _Themename = "";
                    UserID = 0;
                    retvalue = true;
                }
                else
                {
                    Exception exc = new Exception("Problem creating Tribute.");
                    throw new ApplicationException("INTERNAL", exc);//due to multiple clicks if not able to generate TributeId
                }
            }
            else
            {
                //lblErrMsg.InnerHtml = SetHeaderMessage(errorMesg, PortalValidationSummary.HeaderText);
                var sResponseArr = sBeanStreamResponce.Split('&');
                var sErrorMsg = sResponseArr.Length > 3 && sResponseArr[3].Split('=').Length > 1 && !string.IsNullOrEmpty(sResponseArr[3].Split('=')[1]) ? sResponseArr[3].Split('=')[1].Replace("+", " ") : "";
                //LHK:17-11-2011- for gracefull error message display.
                sErrorMsg = HttpUtility.UrlDecode(sErrorMsg);
                lblErrMsg.InnerHtml = SetHeaderMessage(sErrorMsg, PortalValidationSummary.HeaderText);
                lblErrMsg.Visible = true;
                retvalue = false;
            }
            SaveDefaultheme();

        }
            #endregion
        catch (Exception ex)
        {
            retvalue = false;
            if (ex.Message.StartsWith("PAYMENT"))
                throw ex;
            else
                throw new ApplicationException("INTERNAL", ex);
        }
        return retvalue;
    }
    protected void lbtnStartaddingcontent_Click(object sender, EventArgs e)
    {
        hideerrormessage();
        SpanExpirDate.Visible = false;

        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        Tributes objVal = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
        if (WebConfig.ApplicationMode.Equals("local"))
        {
            Response.Redirect(Session["APP_PATH"].ToString() + objVal.TributeUrl +
                "/?TributeType=" + HttpUtility.UrlEncode(objVal.TypeDescription));
            //}
        }
        else
        {
            if (objVal.TypeDescription.Equals("New Baby"))
                Response.Redirect("http://newbaby." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl);
            else
                Response.Redirect("http://" + objVal.TypeDescription.ToLower() + "." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl);
            }
    }



    // Binding Credit Cost Table Conditionally depending on the no. of credits available
    //LHK:
    protected void grdCreditCostTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        _presenter.GetCreditPointCount();
        Label lblCreditPoint = (Label)e.Row.FindControl("lblCreditPoint");
        Label lblTotalCost = (Label)e.Row.FindControl("lblTotalCost");
        Label lblCostPercredit = (Label)e.Row.FindControl("lblCostPercredit");

        if (_accountType == 2)
        {
            if (rdoMembershipLifetime.Checked == true)
                int.TryParse(WebConfig.PhotoLifeTimeCreditCost.Substring(0, 2).Trim(), out _creditCost);
            else if (rdoMembershipYearly.Checked == true)
                int.TryParse(WebConfig.PhotoYearlyCreditCost.Substring(0, 2).Trim(), out _creditCost);
        }
        else if (_accountType == 3)
        {
            if (rdoMembershipLifetime.Checked == true)
                int.TryParse(WebConfig.TributeLifeTimeCreditCost.Substring(0, 2).Trim(), out _creditCost);
            else if (rdoMembershipYearly.Checked == true)
                int.TryParse(WebConfig.TributeYearlyCreditCost.Substring(0, 2).Trim(), out _creditCost);
        }

        if (e.Row.RowIndex == 0)
        {
            ((RadioButton)e.Row.FindControl("rbtnCreditSelection")).Checked = true;

            int rowOneCredit = int.Parse(lblCreditPoint.Text);

            if (_NetCreditCount < _creditCost)
            {
                lblCreditPoint.Text = Convert.ToString(rowOneCredit * (_creditCost - _NetCreditCount));
                lblTotalCost.Text = "$" + Convert.ToString(int.Parse(lblCreditPoint.Text.ToString()) * (double.Parse(lblCostPercredit.Text.ToString()))) + ".00";
                BillingTotal.InnerHtml = lblTotalCost.Text.Remove(lblTotalCost.Text.Length - 3, 3);
            }
            else
            {
                lblTotalCost.Text = "$" + Convert.ToString(int.Parse(lblCreditPoint.Text.ToString()) * (double.Parse(lblCostPercredit.Text.ToString()))) + ".00";
            }
            Session["CreditPointSelected"] = int.Parse(lblCreditPoint.Text);
        }
        if (e.Row.RowIndex > 0)
        {
            lblTotalCost.Text = "$" + Convert.ToString(int.Parse(lblCreditPoint.Text.ToString()) * (double.Parse(lblCostPercredit.Text.ToString()))) + ".00";
        }
        if (e.Row.RowIndex >= 0)
        {
            lblCostPercredit.Text = "$" + Convert.ToDouble(lblCostPercredit.Text).ToString("#.00") + "/credit";
        }

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbtncreditSelection_CheckedChanged(object sender, EventArgs e)
    {
        //Clear the existing selected row 
        foreach (GridViewRow oldrow in grdCreditCostTable.Rows)
        {
            ((RadioButton)oldrow.FindControl("rbtnCreditSelection")).Checked = false;
        }

        //Set the new selected row
        RadioButton rb = (RadioButton)sender;
        GridViewRow row = (GridViewRow)rb.NamingContainer;
        ((RadioButton)row.FindControl("rbtnCreditSelection")).Checked = true;
        Label lblCreditPoint = (Label)row.FindControl("lblCreditPoint");
        Label lblTotalCost = (Label)row.FindControl("lblTotalCost");
        Session["CreditPointSelected"] = int.Parse(lblCreditPoint.Text);
        BillingTotal.InnerHtml = lblTotalCost.Text.Remove(lblTotalCost.Text.Length - 3, 3);


    }

    // Display the billing panel when enough credits are not available with the Tribute Creator.
    private void DisplayBillingPanel()
    {
        PanelBillingInfo.Visible = true;
        PnlPaymentDetails.Visible = true;
        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);

        if (objSessionvalue.UserType == 2)
        {
            CreditGrid.Visible = true;
            foreach (GridViewRow row in grdCreditCostTable.Rows)
            {
                if (((RadioButton)row.FindControl("rbtnCreditSelection")).Checked)
                {
                    Label lblTotalCost = (Label)row.FindControl("lblTotalCost");
                    //LHK:Commented (6:31 PM 2/2/2011)
                    BillingTotal.InnerHtml = lblTotalCost.Text.Remove(lblTotalCost.Text.Length - 3, 3);
                }
            }
        }

    }

    protected void Step2_Load(object sender, EventArgs e)
    {

    }

    protected void lbtnTributeAdmin_Click(object sender, EventArgs e)
    {
        int Admincount = int.Parse(ViewState["Count"].ToString());
        Admincount = Admincount + 1;
        GetAdminEmails();
        Makedata(Admincount);
    }

    private void Makedata(int count)
    {
        ViewState["Count"] = count;
        ArrayList alist = new ArrayList();
        for (int i = 0; i < count; i++)
        {
            alist.Add(i);
        }
        RepeaterEmailAddress.DataSource = alist;
        RepeaterEmailAddress.DataBind();
    }

    protected void RepeaterEmailAddress_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (alistData != null && alistData.Count > 0)
        {
            TextBox txtAdminEmail = (TextBox)e.Item.FindControl("txtAdminEmailAddress");
            if (alistData.Count > countadmin)
            {
                txtAdminEmail.Text = alistData[countadmin].ToString();
            }
            countadmin++;

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
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
    protected void NewBaby_CheckedChanged(object sender, EventArgs e)
    {
        GetDefaultThemeForTribute("New Baby");
        FillSubCategory(NewBaby.Text);
        CommonFunction(NewBaby.Text, NewBaby.ClientID);
    }
    protected void Birthday_CheckedChanged(object sender, EventArgs e)
    {
        GetDefaultThemeForTribute("Birthday");
        FillSubCategory(Birthday.Text);
        CommonFunction(Birthday.Text, Birthday.ClientID);
    }
    protected void Graduation_CheckedChanged(object sender, EventArgs e)
    {
        GetDefaultThemeForTribute("Graduation");
        FillSubCategory(Graduation.Text);
        CommonFunction(Graduation.Text, Graduation.ClientID);
    }
    protected void Wedding_CheckedChanged(object sender, EventArgs e)
    {
        GetDefaultThemeForTribute("Wedding");
        FillSubCategory(Wedding.Text);
        CommonFunction(Wedding.Text, Wedding.ClientID);
    }
    protected void Anniversary_CheckedChanged(object sender, EventArgs e)
    {
        GetDefaultThemeForTribute("Anniversary");
        FillSubCategory(Anniversary.Text);
        CommonFunction(Anniversary.Text, Anniversary.ClientID);
    }
    protected void Memorial_CheckedChanged(object sender, EventArgs e)
    {
        GetDefaultThemeForTribute("Memorial");
        FillSubCategory(Memorial.Text);
        CommonFunction(Memorial.Text, Memorial.ClientID);
    }
    protected void lbtnValidateCoupon_Click(object sender, EventArgs e)
    {
        CheckCoupon();
    }
    #endregion Events

    #region Private Members

    private bool GetAdminEmails1()
    {

        bool chkdup = true;
        ArrayList alist = new ArrayList();
        alistData = new ArrayList();
        foreach (RepeaterItem item in this.RepeaterEmailAddress.Items)
        {
            TextBox txtAdminEmail = (TextBox)item.FindControl("txtAdminEmailAddress");
            if (txtAdminEmail.Text.Length > 0)
            {
                if (alist.Contains(txtAdminEmail.Text))
                {
                    lblErrMsg.InnerHtml = SetHeaderMessage("There is a duplicate value for administrator email address.", PortalValidationSummary.HeaderText);
                    lblErrMsg.Visible = true;
                    chkdup = false;
                }
                else
                {
                    alist.Add(txtAdminEmail.Text);
                    alistData.Add(txtAdminEmail.Text);
                    Otheradministrators.Visible = true;
                }
            }

        }
        RepeaterEditAdminEmail.DataSource = alist;
        RepeaterEditAdminEmail.DataBind();
        return chkdup;

    }

    private void GetAdminEmails()
    {
        ArrayList alist = new ArrayList();
        alistData = new ArrayList();
        foreach (RepeaterItem item in this.RepeaterEmailAddress.Items)
        {
            TextBox txtAdminEmail = (TextBox)item.FindControl("txtAdminEmailAddress");
            if (txtAdminEmail.Text.Length > 0)
            {
                if (alist.Contains(txtAdminEmail.Text))
                {
                    lblErrMsg.InnerHtml = SetHeaderMessage("There is a duplicate value for administrator email address.", PortalValidationSummary.HeaderText);
                    lblErrMsg.Visible = true;
                }
                alist.Add(txtAdminEmail.Text);
                alistData.Add(txtAdminEmail.Text);
                Otheradministrators.Visible = true;

            }
        }
        RepeaterEditAdminEmail.DataSource = alist;
        RepeaterEditAdminEmail.DataBind();
    }

    private void FillDays(DropDownList ddldays)
    {
        for (int i = 0; i <= 31; i++)
        {
            if (i == 0)
            {
                ddldays.Items.Insert(i, " ");
            }
            else
            {
                ddldays.Items.Insert(i, i.ToString());
            }
        }
    }

    private void AvailabilityStatus()
    {
        pnlTributeAddressAvailable.Visible = false;
        lbtncheckAddress.Visible = true;
        txtTributeAddress.Enabled = true;
        rdbAvailableAddress1.Checked = rdbAvailableAddress2.Checked = rdbAvailableAddress3.Checked = false;
        txtTributeAddressOther.Text = "";
        lblErrMsg.InnerHtml = SetHeaderMessage("Web address is unavailable, please try again.", PortalValidationSummary.HeaderText);
        lblErrMsg.Visible = true;
        errorAddress.Visible = true;
    }

    [CreateNew]
    public TributeCreationPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    private void SetAvailableAddresstext_()
    {
        StateManager objtributeType = StateManager.Instance;
        if (objtributeType.Get("TributeType", StateManager.State.Session) != null)
            _Themename = objtributeType.Get("TributeType", StateManager.State.Session).ToString().ToLower().Replace(" ", "");
        else
            RedirectToLoginPage();


        rdbAvailableAddress1.Text = "http://" + _Themename + "." + WebConfig.TopLevelDomain + "/" + this._presenter.SequenceTributeName(txtTributeAddress.Text, _Themename);
        rdbAvailableAddress2.Text = "http://" + _Themename + "." + WebConfig.TopLevelDomain + "/" + _Themename.ToString().Replace(" ", "") + DateTime.Now.Year + txtTributeAddress.Text;
        rdbAvailableAddress3.Text = "http://" + _Themename + "." + WebConfig.TopLevelDomain + "/" + txtTributeAddress.Text + DateTime.Now.Year.ToString();
    }

    private void EditStep1()
    {
        string[] st1 = HiddenField1.Value.Split(':');
        StateManager objtributeType = StateManager.Instance;
        if (objtributeType.Get("TributeType", StateManager.State.Session) != null)
            _Themename = objtributeType.Get("TributeType", StateManager.State.Session).ToString();
        else
            RedirectToLoginPage();

        lblSelectedTributeType.Text = _Themename;
        txttributefor.Text = txtTributeName.Text;
        txtDomainName.Text = "http://" + _Themename.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + this._presenter.View.TributeUrl;
    }
    private void SetDefaultFirst()
    {
        SetTributeUrlname();
        txtTributeAddress.Text = this._presenter.View.TributeUrl;
        pnlTributeAddressAvailable.Visible = false;
        lbtncheckAddress.Visible = true;
        txtTributeAddress.Enabled = true;
        rdbAvailableAddress1.Checked = rdbAvailableAddress2.Checked = rdbAvailableAddress3.Checked = false;
        txtTributeAddressOther.Text = "";
        errorAddress.Visible = false;
        lblErrMsg.Visible = false;
        //SetTheme_();      
        SpanAvailable.Visible = false;
        imgMsgStatus2.Visible = false;
    }

    private void PersonalDetails(string TributeType)
    {
        switch (TributeType)
        {
            case "New Baby":
                TributeTypeDetails("Date of Birth:", "Due Date:", true, true, false);
                break;
            case "Birthday":
                TributeTypeDetails("<em class='required'>* </em>Date of Birth:", "", true, false, false);
                break;
            case "Graduation":
                TributeTypeDetails("<em class='required'>* </em>Date of Graduation:", "", true, false, false);
                break;
            case "Wedding":
                TributeTypeDetails("<em class='required'>* </em>Wedding Date:", "", true, false, false);
                break;
            case "Anniversary":
                TributeTypeDetails("<em class='required'>* </em>Anniversary Date:", "", true, false, false);
                break;
            case "Memorial":
                TributeTypeDetails("Date of Birth:", "<em class='required'>* </em>Date of Death:", true, true, true);
                break;
        }
    }
    private void TributeTypeDetails(string strdate1, string strdate2, bool val1, bool val2, bool val3)
    {

        //"<a href='../Users/Logout.aspx'>Log out</a>";
        lblDate1.InnerHtml = strdate1;
        lblDate2.InnerHtml = strdate2;
        PanelDate1.Visible = val1;
        PanelDate2.Visible = val2;
        //PanelAge.Visible = val3;
    }
    private void PersonalDetailsEdit(string TributeType)
    {
        string date1 = string.Empty;
        string date2 = string.Empty;
        string Location = string.Empty;
        string was;
        switch (TributeType)
        {
            case "New Baby":
                date1 = ddlMonth.SelectedItem.Text + " " + ddlDay.SelectedItem.Text + ", " + txtYear.Text;
                date2 = ddlMonth2.SelectedItem.Text + " " + ddlDay2.SelectedItem.Text + ", " + txtYear2.Text;
                Location = SetLocation();
                if (txtYear.Text != string.Empty)
                    EditPersonalDetails(" born on:", date1, " born in:", date2, " born in:", Location, true, true, true);
                else
                    EditPersonalDetails(" is due on:", date1, " is due on:", date2, " will be born in:", Location, true, true, true);
                break;
            case "Birthday":
                if (txtYear.Text.Length != 0)
                {
                    date1 = ddlMonth.SelectedItem.Text + " " + ddlDay.SelectedItem.Text + ", " + txtYear.Text;
                    DateTime dt10 = new DateTime(int.Parse(txtYear.Text.ToString()), ddlMonth.SelectedIndex, ddlDay.SelectedIndex);
                    if (dt10 > DateTime.Now)
                    {
                        was = "will";
                    }
                    else
                    {
                        was = "was";
                    }
                }
                else
                {
                    date1 = ddlMonth.SelectedItem.Text + " " + ddlDay.SelectedItem.Text;
                    DateTime dt11 = new DateTime(1978, ddlMonth.SelectedIndex, ddlDay.SelectedIndex);
                    if (dt11 > DateTime.Now)
                    {
                        was = "will";
                    }
                    else
                    {
                        was = "was";
                    }

                }
                date2 = "";
                Location = SetLocation();
                EditPersonalDetails(" " + was + " born on:", date1, "", date2, " " + was + "  born in:", Location, true, false, true);
                break;
            case "Graduation":

                date1 = ddlMonth.SelectedItem.Text + " " + ddlDay.SelectedItem.Text + ", " + txtYear.Text;
                date2 = "";
                Location = SetLocation();

                DateTime dt1 = new DateTime(int.Parse(txtYear.Text.ToString()), ddlMonth.SelectedIndex, ddlDay.SelectedIndex);
                if (dt1 > DateTime.Now)
                {
                    was = "will";
                    EditPersonalDetails(" " + was + " graduate on:", date1, "", date2, " " + was + " graduate in:", Location, true, false, true);
                }
                else
                {
                    was = "was";
                    EditPersonalDetails(" graduated on:", date1, "", date2, " graduated in:", Location, true, false, true);
                }

                break;
            case "Wedding":
                date1 = ddlMonth.SelectedItem.Text + " " + ddlDay.SelectedItem.Text + ", " + txtYear.Text;
                date2 = "";
                DateTime dt12 = new DateTime(int.Parse(txtYear.Text.ToString()), ddlMonth.SelectedIndex, ddlDay.SelectedIndex);
                if (dt12 > DateTime.Now)
                {
                    was = "will be";
                }
                else
                {
                    was = "was";
                }
                Location = SetLocation();
                EditPersonalDetails(" " + was + " married on:", date1, " ", date2, " " + was + " married in:", Location, true, false, true);
                break;
            case "Anniversary":
                date1 = ddlMonth.SelectedItem.Text + " " + ddlDay.SelectedItem.Text + ", " + txtYear.Text;
                date2 = "";
                DateTime dt13 = new DateTime(int.Parse(txtYear.Text.ToString()), ddlMonth.SelectedIndex, ddlDay.SelectedIndex);
                if (dt13 > DateTime.Now)
                {
                    was = "will have their";
                }
                else
                {
                    was = "had their";
                }
                Location = SetLocation();
                EditPersonalDetails(" " + was + " anniversary on:", date1, " ", date2, " " + was + " anniversary in:", Location, true, false, true);
                break;
            case "Memorial":
                date1 = ddlMonth.SelectedItem.Text + " " + ddlDay.SelectedItem.Text + ", " + txtYear.Text;
                date2 = ddlMonth2.SelectedItem.Text + " " + ddlDay2.SelectedItem.Text + ", " + txtYear2.Text;
                Location = SetLocation();
                EditPersonalDetails(" was born on:", date1, " died on:", date2, " died in:", Location, true, true, true);
                break;
        }
        //lblEditMeaasge.Text = txtarMessage.Text;
        lblEditMeaasge.InnerText = txtarMessage.Text.Length > 250 ? txtarMessage.Text.Substring(0, 250) : txtarMessage.Text;
        EditPrivacy();
    }
    private string SetLocation()
    {
        string _Location = string.Empty;
        string state = string.Empty;
        if (ddlStateProvince.Items.Count > 0)
        {
            state = ddlStateProvince.SelectedItem.Text.ToString();
        }
        if (txtCity.Text.Length != 0 && state.Length != 0)
            _Location = txtCity.Text + ", " + state + ", " + ddlCountry.SelectedItem.Text.ToString();
        else
        {
            if (txtCity.Text.Length == 0 && state.Length != 0)
                _Location = state + ", " + ddlCountry.SelectedItem.Text;
            else if (txtCity.Text.Length != 0 && state.Length == 0)
                _Location = txtCity.Text + ", " + ddlCountry.SelectedItem.Text;
            else
                _Location = ddlCountry.SelectedItem.Text;
        }
        return _Location;
    }
    private void EditPersonalDetails(string strtext1, string strtext2, string strtext3, string strtext4, string strtext5, string strtext6, bool val1, bool val2, bool val3)
    {
        lblEditBornOn.Text = txtEditBornOn.Text = lblEditDeathOn.Text = txtEditDeathOn.Text = lblEditBornin.Text = txtEditBornin.Text = string.Empty;
        //Date1
        if (strtext2.Length > 6)
        {
            lblEditBornOn.Text = txtTributeName.Text + strtext1;
            txtEditBornOn.Text = strtext2;
        }
        ////Date2
        if (strtext4.Length > 6)
        {
            lblEditDeathOn.Text = txtTributeName.Text + strtext3;
            txtEditDeathOn.Text = strtext4;
        }
        ////Location
        lblEditBornin.Text = txtTributeName.Text + strtext5;
        txtEditBornin.Text = strtext6;

        if (strtext2.Replace(" ", "").Length > 1)
        {
            PaneleditDate1.Visible = val1;
        }
        if (strtext4.Replace(" ", "").Length > 1)
        {
            PaneleditDate2.Visible = val2;
        }
        PaneleditLocation.Visible = val3;
    }

    private void EditPrivacy()
    {
        if (rdoPrivate.Checked)
        {
            lbleditPrivacy.Text = rdoPrivate.Text;
            lbleditPrivacyDetail.Text = "This means that this tribute will not show up in search results, and will not be featured anywhere on Your Tribute.";

        }
        else
        {
            lbleditPrivacy.Text = rdoPublic.Text;
            lbleditPrivacyDetail.Text = "This means that this tribute will be found in search results and may also be featured on Your Tribute.";
        }
    }

    private void DateValisdation(string TributeType)
    {
        if (TributeType == "New Baby")
        {
            cvDate12.Visible = true;
            cvNewbaby.Visible = true;
            reset1.Visible = true;
            reset2.Visible = true;
        }
        else
        {
            cvDate12.Visible = false;
            cvNewbaby.Visible = false;
            reset1.Visible = false;
            reset2.Visible = false;
        }
    }
    private void Setdefault()
    {
        pnlTributeAddressAvailable.Visible = false;
        lbtncheckAddress.Visible = true;
        txtTributeAddress.Text = "";
        txtTributeAddress.Enabled = true;
        rdbAvailableAddress1.Checked = rdbAvailableAddress2.Checked = rdbAvailableAddress3.Checked = false;
        txtTributeAddressOther.Text = "";
        Makedata(1);
        foreach (RepeaterItem item in this.RepeaterEmailAddress.Items)
        {
            TextBox txtAdminEmail = (TextBox)item.FindControl("txtAdminEmailAddress");
            txtAdminEmail.Text = string.Empty;
        }
        rdoPrivate.Checked = rdoPublic.Checked = false;
        rdoMembershipLifetime.Checked = rdoMembershipYearly.Checked = false;
        //
        ddlCCMonth.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        ddlMonth2.SelectedIndex = 0;
        ddlDay.SelectedIndex = 0;
        ddlDay2.SelectedIndex = 0;
        //     
        ArrayList all = Common.GetControls(this.Controls);
        for (int i = 0; i <= all.Count - 1; i++)
        {
            Control ctl = (Control)all[i];
            if (ctl.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
                if (!((TextBox)ctl).ID.Equals("txtTributeName"))
                    ((TextBox)ctl).Text = string.Empty;

        }
        txtTributeName.Attributes.Add("value", txtTributeName.Text);
    }

    private bool ValidateTributeTheme()
    {
        return true;
    }
    private void SetTributeTheme()
    {
        
    }

    #endregion Private Members

    #region ITributeCreation Members

    #region Bind DroppdownLists

    public IList<ParameterTypesCodes> TributeTypes
    {
        set
        {
            if (value.Count > 0)
            {
                _TributeCount = value.Count;
            }
        }
    }

    public IList<Themes> ThemeNames
    {
        set
        {
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

    public IList<Locations> DonationCountryList
    {
        set
        {
            //Populate the countries list in donation box
            ddlDonationCountry.DataSource = value;
            ddlDonationCountry.DataTextField = Locations.Location.LocationName.ToString();
            ddlDonationCountry.DataValueField = Locations.Location.LocationId.ToString();
            ddlDonationCountry.DataBind();
            ddlDonationCountry.Enabled = false;
        }
    }

    public IList<Locations> DonationStateList
    {
        set
        {
            //Populate the states list in donation box
            ddlDonationState.Items.Clear();
            if (value.Count > 0)
            {
                ddlDonationState.DataSource = value;
                ddlDonationState.DataSource = value;
                ddlDonationState.DataTextField = Locations.Location.LocationName.ToString();
                ddlDonationState.DataValueField = Locations.Location.LocationId.ToString();
                ddlDonationState.DataBind();
                ddlDonationState.Enabled = true;
            }
            else
            {
                ddlDonationState.Enabled = false;
            }

        }
    }

    public IList<Locations> StateList
    {
        set
        {
            ddlStateProvince.Items.Clear();
            if (value.Count > 0)
            {
                ddlStateProvince.DataSource = value;
                ddlStateProvince.DataSource = value;
                ddlStateProvince.DataTextField = Locations.Location.LocationName.ToString();
                ddlStateProvince.DataValueField = Locations.Location.LocationId.ToString();
                ddlStateProvince.DataBind();
                ddlStateProvince.Enabled = true;
            }
            else
            {
                ddlStateProvince.Enabled = false;
            }

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
                    sbPayementModes.Append("<input type='radio' name='rdoCCType' onclick='Check(this);' id='rdoCC" + value[i].TypeDescription + "' value='" + value[i].TypeDescription + "' />");
                    sbPayementModes.Append("<label for='rdoCC" + value[i].TypeDescription + "'>" + value[i].TypeDescription + "</label>");
                    sbPayementModes.Append(" </div>");
                }
                ltrPaymentMethod.Text = sbPayementModes.ToString();
            }
        }
    }

    #endregion Bind DroppdownLists

    #region Get Set Values
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
    }

    public string chkAvailability
    {
        set { Status = value; }
    }

    #region Tribute Creation
    public string SelectedTheme
    {
        get
        {

            return _Themename;
            //return rdoTributeTypes.SelectedItem.Text.ToString();
        }
    }

    /// <summary>
    /// Get whether Donation Box is selected or not
    /// </summary>
    public bool IsDonationActive
    {
        get { return false; }
    }

    /// <summary>
    /// Get the Email address in Donation Box
    /// </summary>
    public string DonationEmail
    {
        get { return txtDonationEmail.Text.Trim(); }
    }

    /// <summary>
    /// Get the Charity in Donation Box
    /// </summary>
    public string CharityName
    {
        get { return txtDonationCharityName.Text.Trim(); }
    }

    /// <summary>
    /// Get the selected Country in Donation Box
    /// </summary>
    public string SelectedDonationCountry
    {
        get { return ddlDonationCountry.SelectedValue.ToString(); }
    }

    /// <summary>
    /// Get the selected Country Text in Donation Box
    /// </summary>
    public string SelectedDonationCountryText
    {
        get { return ddlDonationCountry.SelectedItem.Text.Trim(); }
    }

    /// <summary>
    /// Get the selected State in Donation Box
    /// </summary>
    public string SelectedDonationState
    {
        get
        {
            if (ddlDonationState.SelectedValue.Trim() != "0")
                return ddlDonationState.SelectedItem.Text.Trim();
            return string.Empty;
        }
    }

    /// <summary>
    /// Get the City in Donation Box
    /// </summary>
    public string DonationCity
    {
        get { return txtDonationCity.Text.Trim(); }
    }

    /// <summary>
    /// Get the Address in Donation Box
    /// </summary>
    public string DonationAddress
    {
        get { return txtDonationAddress.Text.Trim(); }
    }

    public string EditTheme
    {
        get
        {
            return hfSeledctedTheme.Value;
            //return TributeTheme.ToString(); 
        }
    }

    public string TributeFor
    {
        get
        {
            return CleanString(txtTributeName.Text);
        }
    }

    public string TributeFirstName
    {
        get
        {
            return CleanString(txtTributeFirstName.Text);
        }
    }

    public string TributeLastName
    {
        get
        {
            return CleanString(txtTributeLastName.Text);
        }
    }

    public string TributeType
    {
        get
        {
            StateManager objtributeType = StateManager.Instance;
            if (objtributeType.Get("TributeType", StateManager.State.Session) != null)
                _Themename = objtributeType.Get("TributeType", StateManager.State.Session).ToString();
            else
                RedirectToLoginPage();

            return _Themename;
        }
    }

    public string TributeUrl
    {
        get
        {
            if (txtTributeAddressOther.Text.Length != 0)
                Domainname = RemoveSpecialChars(txtTributeAddressOther.Text);
            else if (rdbAvailableAddress1.Checked)
                Domainname = rdbAvailableAddress1.Text.ToString().Replace("http://" + this._presenter.View.TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/", "");
            else if (rdbAvailableAddress2.Checked)
                Domainname = rdbAvailableAddress2.Text.ToString().Replace("http://" + this._presenter.View.TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/", "");
            else if (rdbAvailableAddress3.Checked)
                Domainname = rdbAvailableAddress3.Text.ToString().Replace("http://" + this._presenter.View.TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/", "");
            else
                Domainname = RemoveSpecialChars(txtTributeAddress.Text);

            return Domainname;
        }
        set
        {
            Domainname = value;
        }
    }

    public DateTime? date1
    {
        get
        {
            DateTime _DateTime1 = new DateTime();
            if (_Themename == "Birthday")
            {
                try
                {
                    if (txtYear.Text.Length == 4)
                        _DateTime1 = new DateTime(int.Parse(txtYear.Text), ddlMonth.SelectedIndex, ddlDay.SelectedIndex);
                    else
                        _DateTime1 = new DateTime(1780, ddlMonth.SelectedIndex, ddlDay.SelectedIndex);
                    return _DateTime1;
                }
                catch
                {
                    return null;
                }

            }
            else if ((ddlDay.SelectedIndex != 0) && (ddlMonth.SelectedIndex != 0) && (txtYear.Text.Length != 0))
            {
                _DateTime1 = new DateTime(int.Parse(txtYear.Text), ddlMonth.SelectedIndex, ddlDay.SelectedIndex);
                return _DateTime1;
            }
            else
                return null;
        }
        set
        {
            if (value != null)
            {
                DateTime dt = (DateTime)value;
                txtYear.Text = dt.Year.ToString();
                ddlMonth.SelectedIndex = dt.Month;
                ddlDay.SelectedIndex = dt.Day;

                StateManager stateMgr = StateManager.Instance;
                stateMgr.Add("DOBYear", txtYear.Text, StateManager.State.Session);
                stateMgr.Add("DOBMonth", ddlMonth.SelectedIndex, StateManager.State.Session);
                stateMgr.Add("DOBDay", ddlDay.SelectedIndex, StateManager.State.Session);
            }
        }
    }

    public DateTime? date2
    {
        get
        {
            if ((ddlDay2.SelectedIndex != 0) && (ddlMonth2.SelectedIndex != 0) && (txtYear2.Text.Length != 0))
            {
                DateTime _DateTime2 = new DateTime(int.Parse(txtYear2.Text), ddlMonth2.SelectedIndex, ddlDay2.SelectedIndex);
                return _DateTime2;
            }
            else
                return null;
        }
        set
        {
            if (value != null)
            {
                DateTime dt = (DateTime)value;
                txtYear2.Text = dt.Year.ToString();
                ddlMonth2.SelectedIndex = dt.Month;
                ddlDay2.SelectedIndex = dt.Day;

                StateManager stateMgr = StateManager.Instance;
                stateMgr.Add("DODYear", txtYear2.Text, StateManager.State.Session);
                stateMgr.Add("DODMonth", ddlMonth2.SelectedIndex, StateManager.State.Session);
                stateMgr.Add("DODDay", ddlDay2.SelectedIndex, StateManager.State.Session);

            }
        }
    }

    public string SelectedState
    {
        get { return ddlStateProvince.SelectedValue.ToString(); }
        set
        {
            if (value != null)
            {
                this._presenter.GetStateList();
                //ddlStateProvince.Items.FindByText(value).Selected = true;
                ddlStateProvince.SelectedValue = value;
                StateManager stateMgr = StateManager.Instance;
                stateMgr.Add("VideoTributeState", value, StateManager.State.Session);
            }
        }
    }

    public string SelectedCity
    {
        get { return txtCity.Text; }
        set
        {
            txtCity.Text = value;
            StateManager stateMgr = StateManager.Instance;
            stateMgr.Add("VideoTributeCity", value, StateManager.State.Session);
        }
    }

    public string SelectedCountry
    {
        get { return ddlCountry.SelectedValue.ToString(); }
        set
        {
            if (value != null)
            {
                ddlCountry.SelectedValue = value;
                StateManager stateMgr = StateManager.Instance;
                stateMgr.Add("VideoTributeCountry", value, StateManager.State.Session);
            }
        }
    }

    public int Age
    {
        get { return int.Parse("0"); }
    }

    public string TributeImage
    {
        get
        {
            if (Session["_Tributeimage"] == null)
                return string.Empty;
            else
                return Session["_Tributeimage"].ToString();
        }
    }

    public string WelcomeMsg
    {
        get
         {

            return txtarMessage.Text.Length > 250 ? txtarMessage.Text.Substring(0, 250) : txtarMessage.Text; ;
        }
    }

    public bool IsPrivate
    {
        get { return rdoPrivate.Checked; }
    }

    public bool IsOrderDVDChecked
    {
        get { return false; }
    }

    public bool IsMemTributeBoxChecked
    {
        get { return false; }
    }
    #endregion Tribute Creation

    #region Tribute CC information

    #region GET CC DETAILS

    public string CreditCardNo
    {
        get
        {
            if (!txtCCNumber.Text.Contains("XXXXXXXXXX"))
            {
                return TributePortalSecurity.Security.EncryptSymmetric(txtCCNumber.Text);
            }
            else if (Session["CCNumber"] != null && Session["CCNumber"].ToString().Length > 0)
                return TributePortalSecurity.Security.EncryptSymmetric(Session["CCNumber"].ToString());
            else return null;
        }
    }

    public string CardholdersName
    {
        get
        {
            return txtCCName.Text;
        }
    }

    public DateTime ExpirationDate
    {
        get
        {
            DateTime _DateTime = new DateTime(int.Parse(txtCCYear.Text), ddlCCMonth.SelectedIndex, 1);
            return _DateTime;
        }
    }

    public bool IsCardDetailsReusable
    {

        get
        {
            return chkSaveBillingInfo.Checked;
        }
    }
    public string SelectedCCState
    {
        get
        {
            return ddlCCStateProvince.SelectedValue.ToString();
        }
    }

    public string SelectedCCCity
    {
        get
        {
            return txtCCCity.Text;
        }
    }

    public bool NotifyBeforeRenew
    {
        get
        {
            return rdoYearlyAutoRenew.Checked;
        }
    }

    public string Telephone
    {
        get { return txtPhoneNumber1.Text + txtPhoneNumber2.Text + txtPhoneNumber3.Text; }
    }
    public string SelectedCCCountry
    {
        get { return ddlCCCountry.SelectedValue.ToString(); }
    }
    public string PaymentMethod
    {
        get { return hfPaymentMethod.Value; }
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
    }

    public string ZipCode
    {
        get { return txtCCZipCode.Text; }
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
            if ((_NetCreditCount == 0))
            {
                NetCreditCount.InnerHtml = "<img src='../assets/images/error_pic.png' style='margin-right:4px' alt='error'/><b>You have <span class='bold_red'>no credits</span> in your account!</b> &nbsp;You can order more credits below:";
            }
            else if (_NetCreditCount < _creditCost)
            {
                NetCreditCount.InnerHtml = "<img src='../assets/images/error_pic.png' style='margin-right:4px' alt='error'/><b>You do not have <span class='bold_red'>enough credits</span> in your account!</b> &nbsp;You can order more credits below:";
            }
            else
            {
                NetCreditCount.InnerHtml = "<img src='../assets/images/dollor-pic.png' style='margin-right:4px' alt='dollor'/> <b>You have <span class='bold_green'>" + _NetCreditCount.ToString() + " credits</span> in your account! </b>";
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

    public string ObPostMessage
    {
        get
        {
            _postMessage = ftbNoteMessage.Text.ToString();
            return _postMessage;
        }
        set
        {
            _postMessage = value;
        }
    }
    public string ObMessageWithoutHtml
    {
        get
        {
            _messageWithoutHtml = StripHtml(ftbNoteMessage.PlainText.ToString());
            _messageWithoutHtml = _messageWithoutHtml.Trim().Replace("/**/", "");
            _messageWithoutHtml = _messageWithoutHtml.Trim().Replace("\r\n", "");
            return _messageWithoutHtml;
        }
        set
        {
            _messageWithoutHtml = value;
        }
    }

    public string[] SponsorNameandMsgForEmail
    {
        get
        {
            NameAndMsg[0] = string.Empty;
            NameAndMsg[1] = string.Empty;
            return NameAndMsg;
        }
    }

    #endregion

    #region SET CC DETAILS
    public UserRegistration CreditCardDetails
    {
        set
        {
            if (value.UserCreditcardDetails != null)
            {
                txtCouponCode.Text = string.Empty;

                string strCredit = string.Empty;
                string ccnumber = TributePortalSecurity.Security.DecryptSymmetric(value.UserCreditcardDetails.CreditCardNo.Trim());
                Session["CCNumber"] = ccnumber;
                for (int indexCredit = 0; indexCredit < ccnumber.Length - 4; indexCredit++)
                    strCredit += "X";

                txtCCNumber.Text = strCredit + ccnumber.Substring(ccnumber.Length - 4);
                string CVCCode = TributePortalSecurity.Security.DecryptSymmetric(value.UserCreditcardDetails.CVC.ToString());
                txtCCVerification.Attributes.Add("value", CVCCode);
                txtCCVerification.Text = CVCCode;
                Session["CVCCode"] = CVCCode;
                txtCCYear.Text = value.UserCreditcardDetails.ExpirationDate.Year.ToString();
                ddlCCMonth.SelectedValue = value.UserCreditcardDetails.ExpirationDate.Month.ToString();
                ddlCCCountry.SelectedValue = value.UserCreditcardDetails.Country.ToString();
                if (ddlCCStateProvince.SelectedIndex != -1)
                {
                    this._presenter.GetCCStateList();
                }
                ddlCCStateProvince.SelectedValue = value.UserCreditcardDetails.State.ToString();
                txtCCName.Text = value.UserCreditcardDetails.CardholdersName;
                txtCCCity.Text = value.UserCreditcardDetails.City;
                txtCCZipCode.Text = value.UserCreditcardDetails.Zip;
                txtPhoneNumber1.Text = value.UserCreditcardDetails.Telephone.Substring(0, 3);
                txtPhoneNumber2.Text = value.UserCreditcardDetails.Telephone.Substring(3, 3);
                txtPhoneNumber3.Text = value.UserCreditcardDetails.Telephone.Substring(6, value.UserCreditcardDetails.Telephone.Length - 6); ;

                string[] splitter = { WebConfig.AddressSeparator };
                string[] addr = value.UserCreditcardDetails.Address.Split(splitter, System.StringSplitOptions.None);
                txtCCBillingAddress.Text = addr[0].ToString();
                if (addr.Length > 1)
                    txtCCBillingAddress2.Text = addr[1].ToString();
                else
                    txtCCBillingAddress2.Text = string.Empty;
                if (!string.IsNullOrEmpty(this._presenter.View.UserMail))
                    txtEmailAddress.Text = this._presenter.View.UserMail;
            }
            else
            {
                SetCCDefault();
            }
        }
    }
    public IList<ParameterTypesCodes> GetSelectedPaymentMode
    {
        set
        {
            StringBuilder sbPayementModes = new StringBuilder();
            if (value.Count > 0)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    sbPayementModes.Append("<div class='yt-Form-Field yt-Form-Field-Radio' id='yt-CC" + value[i].TypeDescription + "'>");
                    if (_selectedPaymentMode == value[i].TypeDescription)
                        sbPayementModes.Append("<input type='radio' name='rdoCCType' Checked='Checked' onclick='Check(this);' id='rdoCC" + value[i].TypeDescription + "' value='" + value[i].TypeDescription + "' />");
                    else
                        sbPayementModes.Append("<input type='radio' name='rdoCCType' onclick='Check(this);' id='rdoCC" + value[i].TypeDescription + "' value='" + value[i].TypeDescription + "' />");
                    sbPayementModes.Append("<label for='rdoCC" + value[i].TypeDescription + "'>" + value[i].TypeDescription + "</label>");
                    sbPayementModes.Append(" </div>");
                }
                ltrPaymentMethod.Text = sbPayementModes.ToString();
            }
        }
    }
    public string SelectedPaymentMode
    {
        set
        {
            _selectedPaymentMode = value;
            hfPaymentMethod.Value = value;
        }
    }

    private int _PackageId;

    public DateTime? EndDate
    {
        get
        {
            if (rdoMembershipYearly.Checked)
                return DateTime.Now.AddMonths(12);
            else //(rdoMembershipLifetime.Checked)
                return null;
        }
        set
        {
            Nullable<DateTime> dt1 = value;

        }
    }

    public int TransactionId
    {
        set
        {
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
    #endregion

    #endregion Tribute CC information


    #endregion Get Set Values
    #endregion


    #region ITributeCreation Members


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
                ddlCCStateProvince.Enabled = true;
            }
            else
                ddlCCStateProvince.Enabled = false;
        }
    }

    public int PackageId
    {
        get
        {
            int _packageid = 0;
            if (rdoMembershipFree.Checked)
            {
                if (_accountType == 1)
                {
                    StateManager stateManager = StateManager.Instance;
                    if (!Equals((stateManager.Get("TokenDetails", StateManager.State.Session)), null))
                    {
                        _packageid = 3;
                    }
                    else
                    {
                        _packageid = 8;
                    }
                }
                    
            }
            // code added by UAttri for Phase 1 update
            else if (rdbAnnounceFree.Checked)
            {
                if (_accountType == 1)
                    _packageid = 8;
            }
            else if (rdbAnnounceFreeNoAds.Checked)
            {
                if (_accountType == 1)
                    _packageid = 6;
            }
            else if (rdoMembershipYearly.Checked)
            {
                if (_accountType == 3)
                {
                    _packageid = 5;
                }
                else if (_accountType == 2)
                {
                    _packageid = 7;
                }
            }
            else if (rdoMembershipLifetime.Checked)
            {
                if (_accountType == 3)
                {
                    _packageid = 4;
                }
                else if (_accountType == 2)
                {
                    _packageid = 6;
                }
            }
            return _packageid;
        }
        set
        {
            _PackageId = value;
            Session["PackageId"] = value;
        }
    }

    ArrayList ITributeCreation.AdminEmailLists
    {
        get
        {
            GetAdminEmails();
            return alistData;
        }
    }

    public string UserMail
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                _UserMail = objSessionvalue.UserEmail;
            }
            return _UserMail;
        }
    }

    #endregion


    private void SetErrorHeader12()
    {
        PortalValidationSummary.HeaderText = " <h2>Oops - there was a problem with your tribute details.</h2>                                                             <h3>Please correct the errors below:</h3>";
    }
    private void SetErrorHeader3()
    {
        PortalValidationSummary.HeaderText = " <h2>Oops - there was a problem with your tribute management.</h2>                                                             <h3>Please correct the errors below:</h3>";
    }
    private void SetErrorHeader5()
    {
        PortalValidationSummary.HeaderText = " <h2>Oops - there was a problem with your account type.</h2>                                                             <h3>Please correct the errors below:</h3>";
    }   

    private void FillSubCategory(string categoryName)
    {
        IList<Themes> objThemeList = _presenter.GetSubCategoryForTheme(categoryName);
        if (objThemeList.Count > 0)
        {
            ddlSubCategory.Items.Clear();
            foreach (Themes objTheme in objThemeList)
            {
                ddlSubCategory.Items.Add(objTheme.SubCategory);
            }
        }
        ddlSubCategory.Items.Insert(0, new ListItem(string.Format("{0} {1}", "All", categoryName), "All"));
        pnlSubCategory.Visible = true;
        dvThemes.Visible = true;
    }

    private void SetVisibilityStatus()
    {
        StringBuilder Script = new StringBuilder();
        Script.Append("var notice = $('ctl00_TributePlaceHolder_SpanAvailable');");
        Script.Append("if(notice)");
        Script.Append("{");
        Script.Append("notice.innerHTML='';");
        Script.Append("notice.removeClass('availabilityNotice');");
        Script.Append("}");
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", Script.ToString(), true);
    }
    //string _tributetypetext;
    private void CommonFunction(string _text, string rdbid)
    {
        hfSeledctedTheme.Value = string.Empty;
        StateManager objtributeType = StateManager.Instance;
        objtributeType.Add("TributeType", _text, StateManager.State.Session);
        if (_text.ToLower().Equals("wedding"))
            tributeImgURL = "../assets/images/thumbnails/wedding_TributePhoto.jpg";
        if (_text.ToLower().Equals("anniversary"))
            tributeImgURL = "../assets/images/thumbnails/anniversary_TributePhoto.jpg";

        if (_text.ToLower().Equals("new baby"))
            tributeImgURL = "../assets/images/thumbnails/baby_TributePhoto.jpg";

        if (_text.ToLower().Equals("memorial"))
            tributeImgURL = "../assets/images/thumbnails/memorial_TributePhoto.jpg";

        if (_text.ToLower().Equals("birthday"))
            tributeImgURL = "../assets/images/thumbnails/birthday_TributePhoto.jpg";

        if (_text.ToLower().Equals("graduation"))
            tributeImgURL = "../assets/images/thumbnails/grad_TributePhoto.jpg";
        imgTributePhoto.ImageUrl = tributeImgURL;
        SetVisibilityStatus();
        hfSelectedTribute.Value = rdbid;
        Setdefault();
        DateValisdation(_text);
        this._presenter.GetThemesForCategory_(_text, ddlSubCategory.SelectedValue, WebConfig.ApplicationType);
        hfTributeValue.Value = _text;
        ptagtribute.InnerHtml = "Please enter some more information about the tribute you are creating for " + txtTributeName.Text;
        SetTributeUrlname();
        SetAvailableAddresstext_();

        PanelTributeAddress.Visible = true;
        //for announcement and photo announcement accounts.
        if ((_accountType == 1) || (_accountType == 2))
        {
            PanelTributeAddress.Visible = false;
        }
        txtTributeName.Attributes.Add("value", txtTributeName.Text);
        errorAddress.Visible = false;
        string trbType = _text;
        if (trbType == "New Baby")
            trbType = "NewBaby";
        if (WebConfig.ApplicationType.ToLower() == "yourmoments")
            trbType = trbType + "1";
        txtarMessage.Text = ResourceText.GetString(trbType);
        if (Memorial.Checked == true)
            txtarMessage.Text = @"This memorial tribute was created to celebrate the life of " + txtTributeName.Text + @". We encourage you to leave a condolence in the online guestbook. You can also use this tribute to share photos, videos, stories and other memories.";

        lblErrMsg.Visible = false;
        RemoveWarning();

        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
        if (objSessionvalue != null)
        {
            _UserMail = objSessionvalue.UserEmail;
            if (string.IsNullOrEmpty(_UserMail))
            {
                PanelAccountEmail.Visible = true;
            }
        }
    }

    private void SetCCDefault()
    {
        txtCouponCode.Text = string.Empty;
        txtCCNumber.Text = string.Empty;
        txtCCVerification.Text = string.Empty;
        txtCCYear.Text = string.Empty;
        ddlCCMonth.SelectedIndex = 0;
        ddlCCCountry.SelectedIndex = 0;
        if (ddlCCStateProvince.SelectedIndex != -1)
        {
            this._presenter.GetCCStateList();
        }

        txtCCName.Text = string.Empty;
        txtCCCity.Text = string.Empty;
        txtCCZipCode.Text = string.Empty;
        //        txtCCPhone.Text = string.Empty;
        txtPhoneNumber1.Text = txtPhoneNumber2.Text = txtPhoneNumber3.Text = string.Empty;
        txtCCBillingAddress.Text = txtCCBillingAddress2.Text = string.Empty;
        txtEmailAddress.Text = this._presenter.View.UserMail;
    }

    private void CheckCoupon()
    {
        spanCoupon.Visible = false;
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


    #region ITributeCreation Members

    private void SetThemeforStep4(string ThemeValue, string ThemeName, string Thrmeid, string folderName)
    {
        step4Litrel.Text = string.Empty;

        string folderPathPrefix = string.Empty;
        if (WebConfig.ApplicationMode.Equals("local"))
        {
            folderPathPrefix = WebConfig.AppBaseDomain;
        }
        else
        {
            folderPathPrefix = WebConfig.AppBaseDomain.ToLower().Replace("http", "https");
        }
        StringBuilder stp4 = new StringBuilder();
        stp4.Append("<div class='yt-ThemeSet' id='yt-" + ThemeValue.Replace(" ", "") + "'>");
        //stp4.Append(ThemeName + "<span class='yt-ThemeColorPrimary'></span><span class='yt-ThemeColorSecondary'></span>");
        stp4.Append("<img src='" + folderPathPrefix + "assets/themes/" + folderName + "/thumb.gif' />");
        stp4.Append("<div>");
        stp4.Append(ThemeName);
        stp4.Append("</div>");
        stp4.Append("</div>");
        hfSeledctedTheme.Value = Thrmeid;
        step4Litrel.Text = stp4.ToString();

    }

    //private IList<Themes> themes_;
    public IList<Themes> ThemeNames_
    {

        set
        {
            ltrltheme.Text = string.Empty;
            if (value.Count > 0)
            {
                string folderPathPrefix = string.Empty;
                if (WebConfig.ApplicationMode.Equals("local"))
                {
                    folderPathPrefix = WebConfig.AppBaseDomain;
                }
                else
                {
                    folderPathPrefix = WebConfig.AppBaseDomain.ToLower().Replace("http", "https");
                }
                StringBuilder strtheme = new StringBuilder();
                strtheme.Append("<div class='yt-ChannelSelected yt-ShowBox'>");
                strtheme.Append("<fieldset class='yt-ThemeSelection yt-CompactRadioList'>");
                //strtheme.Append("<legend><em class='required'>* </em>Select the theme you would like to use for this");
                //strtheme.Append(" tribute:</legend>");
                strtheme.Append("<div class='yt-ThemeSet' id='yt-Themes" + value[0].Tributetype.Replace(" ", "") + "'>");
                bool def_theme_exists = false;
                foreach (Themes thm in value)
                {
                    if (thm.ThemeId == this.DefaultTheme)
                    {
                        def_theme_exists = true;
                        continue;
                    }
                }
                for (int i = 0; i < value.Count; i++)
                {

                    strtheme.Append("<div class='yt-Form-Field yt-Form-Field-Radio yt-top' style ='margin-top: -12px;' id='yt-" + value[i].ThemeValue + "'>");
                    strtheme.Append("<input name='rdoTheme'  onclick='SetThemeFolder(" + '"' + value[i].ThemeValue + '"' + "," + '"' + value[i].ThemeName.Replace("'", "%26") + '"' + "," + '"' + value[i].ThemeId + '"' + "," + '"' + value[i].FolderName + '"' + ");' type='radio' id='rdo" + value[i].ThemeValue + "'");

                    if (i == 0)
                    {
                        if (this.DefaultTheme == 0 || def_theme_exists == false || this.DefaultTheme == value[i].ThemeId)
                        {
                            if (def_theme_exists == false)
                                chbxDefaultTheme.Checked = false;
                            int d = 0;
                            strtheme.Append(" checked='checked' ");
                            hfSeledctedTheme.Value = value[d].ThemeId.ToString();
                            HiddenField1.Value = value[i].ThemeValue + ":" + value[i].ThemeName.Replace("'", "%26") + ":" + value[i].ThemeId + ":" + value[i].FolderName;
                            SetThemeforStep4(value[d].ThemeValue, value[d].ThemeName, value[d].ThemeId.ToString(), value[d].FolderName);
                        }
                    }
                    else
                    { // to check to default theme of tribute type
                        if (this.DefaultTheme == value[i].ThemeId)
                        {
                            strtheme.Append(" checked='checked' ");
                            hfSeledctedTheme.Value = this.DefaultTheme.ToString();
                            HiddenField1.Value = value[i].ThemeValue + ":" + value[i].ThemeName.Replace("'", "%26") + ":" + value[i].ThemeId + ":" + value[i].FolderName;
                            SetThemeforStep4(value[i].ThemeValue, value[i].ThemeName, value[i].ThemeId.ToString(), value[i].FolderName);
                        }
                    }
                    strtheme.Append("value='" + value[i].ThemeValue + "' />");
                    strtheme.Append("<img src='" + folderPathPrefix + "assets/themes/" + value[i].FolderName + "/thumb.gif' />");
                    strtheme.Append("<a href='javascript: void(0);' onclick=\"viewThemeFolderSample('" + value[i].FolderName + "');\">View Sample</a>");
                    strtheme.Append(" <label for='rdo" + value[i].ThemeValue + "'>");
                    strtheme.Append(value[i].ThemeName + "</label>");
                    strtheme.Append("</div>");
                    
                }
                strtheme.Append("</div>");
                strtheme.Append("</fieldset>");
                strtheme.Append("</div>");

                ltrltheme.Text = strtheme.ToString();
                if (value.Count > 0)
                {
                    Session["themes_"] = value;
                }
            }
        }
    }

    #endregion

    #region ITributeCreation Members

    string ITributeCreation.CVC
    {
        get
        {
            return TributePortalSecurity.Security.EncryptSymmetric(txtCCVerification.Text);
            //return txtCCVerification.Text;
        }
    }

    #endregion

   

    private void SetCouponUnAvailableStatus()
    {
        amount = "0";
        if (rdoMembershipLifetime.Checked)
        {
            amount = WebConfig.LifeTimeAmount;
        }
        else
        {
            amount = WebConfig.OneyearAmount;
        }

        BillingTotal.InnerHtml = amount;

        spanCoupon.Attributes.Add("class", "couponCreationUnavail");
        spanCoupon.InnerHtml = "This is not a valid coupon code.";
        spanCoupon.Visible = true;
    }

    private void SetCouponAvailableStatus()
    {
        Double Couponamount = 0;
        if (rdoMembershipYearly.Checked)
        {
            if (_accountType == 2)
            {
                amount = WebConfig.PhotoOneyearAmount;
            }
            else if (_accountType == 3)
            {
                amount = WebConfig.TributeOneyearAmount;
            }
            Couponamount = Convert.ToDouble(amount.Substring(1, amount.Length - 1));
        }
        else if(rdbAnnounceFreeNoAds.Checked)
        {
            if(_accountType==1)
            {
                amount = WebConfig.PhotoOneyearAmount;
            }
        }
        else
        {
            if (_accountType == 2)
            {
                amount = WebConfig.PhotoLifeTimeAmount;
            }
            else if (_accountType == 3)
            {
                amount = WebConfig.TributeLifeTimeAmount;
            }
            Couponamount = Convert.ToDouble(amount.Substring(1, amount.Length - 1));
        }


        spanCoupon.Attributes.Add("class", "couponCreationAvail");
        spanCoupon.InnerHtml = "Coupon is valid";
        spanCoupon.Visible = true;

        if (this._presenter.View.IsPercentage == false)
        {
            Couponamount = Couponamount - double.Parse(this._presenter.View.Denomination);
        }
        else
        {
            Couponamount = Couponamount - ((double.Parse(this._presenter.View.Denomination) * Couponamount) / 100);
        }
        Couponamount = Math.Round(Couponamount, 2);

        if (Couponamount < 0)
            Couponamount = 0;

        BillingTotal.InnerHtml = "$ " + Couponamount.ToString();
        BillingTotal.Visible = true;
        PanelBillingInfo.Visible = true;
        if (Couponamount == 0)
        {
            package11.Visible = true;
            package12.Visible = false;
            emailreceipt1.Visible = false;
            emailreceipt2.Visible = false;
            PnlPaymentDetails.Visible = false;
        }
    }


    #region ITributeCreation Members

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

    #region ITributeCreation Members

    public decimal AmountPaid
    {
        get
        {
            return Decimal.Parse(_amount);
        }
    }

    #endregion

    #region PROPERTIES FOR VIDEO TRIBUTE
    public int TributeId
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            return ((Tributes)stateManager.Get("TributeSession", StateManager.State.Session)).TributeId;
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
    public string TributeTypeDescription
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            return ((Tributes)stateManager.Get("TributeSession", StateManager.State.Session)).TypeDescription;
        }
    }
    public string CreatedTributeUrl
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            return ((Tributes)stateManager.Get("TributeSession", StateManager.State.Session)).TributeUrl;
        }
    }
    public string VideoCaption
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            return ((Tributes)stateManager.Get("TributeSession", StateManager.State.Session)).TributeName;
        }
    }
    public string VideoDesc
    {
        get
        {
            return string.Empty;
        }
    }
    public string VideoTributeId
    {
        get
        {
            string _fileName = string.Empty;
            StateManager stateManager = StateManager.Instance;
            VideoToken objToken = new VideoToken();
            objToken = (VideoToken)stateManager.Get("TokenDetails", StateManager.State.Session);
            if (!Equals(objToken, null))
            {
                if (objToken.FileName == string.Empty)
                    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
                else
                    _fileName = objToken.FileName;
            }
            else
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));

            return _fileName;
        }
    }
    public string UserName
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            return ((SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session)).UserName;
        }
    }

    public int AccountType
    {
        get
        {
            return _accountType;
        }
        set
        {
            _accountType = value;
        }
    }
    // by UD: Default Theme for  business users
    public int DefaultTheme
    {
        get
        {
            return _defaultTheme;
        }
        set
        {
            _defaultTheme = value;
        }
    }

    #endregion

    
    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Memorial.Checked)
        {
            GetDefaultThemeForTribute("Memorial");
            CommonFunction(Memorial.Text, Memorial.ClientID);
        }
        else if (Anniversary.Checked)
        {
            GetDefaultThemeForTribute("Anniversary");
            CommonFunction(Anniversary.Text, Anniversary.ClientID);
        }
        else if (Birthday.Checked)
        {
            GetDefaultThemeForTribute("Birthady");
            CommonFunction(Birthday.Text, Birthday.ClientID);
        }
        else if (Graduation.Checked)
        {
            GetDefaultThemeForTribute("Graduation");
            CommonFunction(Graduation.Text, Graduation.ClientID);
        }
        else if (NewBaby.Checked)
        {
            GetDefaultThemeForTribute("New Baby");
            CommonFunction(NewBaby.Text, NewBaby.ClientID);
        }
        else if (Wedding.Checked)
        {
            GetDefaultThemeForTribute("Wedding");
            CommonFunction(Wedding.Text, Wedding.ClientID);
        }

    }
    //LHK:(4:51 PM 2/8/2011) for ObNote
    public string StripHtml(string htmlString)
    {
        string finalString = Regex.Replace(htmlString, @"<(.|\n)*?>", string.Empty);  //regex.Replace(htmlString, regex, string.Empty);
        return finalString;
    }
    public string RemoveSpecialChars(string str)
    {
        string[] chars = new string[] { ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]" };
        for (int i = 0; i < chars.Length; i++)
        {
            if (str.Contains(chars[i]))
            {
                str = str.Replace(chars[i], "");
            }
        }
        str = str.Replace(" ", "-");
        while (str.Contains("--"))
        {
            str = str.Replace("--", "-");
        }
        return str;
    }
    // UD: for saving Default Theme in DefaultTheme

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
            var errorPage = "http://localhost:4941/DevelopmentWebsite/Create.aspx";  //ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "tributes/TributeCreation.aspx";
            var approvedPage = "http://localhost:4941/DevelopmentWebsite/Create.aspx";//ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "tributes/TributeCreation.aspx";
            var declinedPage = "http://localhost:4941/DevelopmentWebsite/Create.aspx";//ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "tributes/TributeCreation.aspx";
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
            Console.WriteLine(((System.Net.HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.

            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            return responseFromServer;
        }

    }
    protected void TempButton_Click(object sender, EventArgs e)
    {
        string sReturnString = CallBeanStream("https://www.beanstream.com/scripts/process_transaction.asp?merchant_id=237120001&requestType=BACKEND&trnType=P&trnOrderNumber=cbdbdefb-0&trnAmount=99.95&trnCardOwner=Rupendra%20&trnCardNumber=5100000010001004&trnExpMonth=08&trnExpYear=14&ordName=Rupendra%20&ordAddress1=a-21,LA&ordCity=LA&ordProvince=CA&ordCountry=US&ordPostalCode=23232&ordPhoneNumber=2013453333&ordEmailAddress=monika.sahu@optimusinfo.com&errorPage=http://localhost:4941/DevelopmentWebsite/Create.aspx&approvedPage=http://localhost:4941/DevelopmentWebsite/Create.aspx?AccountType=3&declinedPage=http://localhost:4941/DevelopmentWebsite/Create.aspx?AccountType=3&trnCardCvd=123&username=admin&password=Ab3b82cd");
    }
    public string ApplicationType
    {
        get { return ConfigurationManager.AppSettings["ApplicationType"].ToString(); }
    }

    /// <summary>
    /// method to remove the HTML symbols for special characters before displaying on screen
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private string CleanString(string message)
    {
        //if (!string.IsNullOrEmpty(message))
        //{
        //    if (message.Contains(" "))
        //        message = message.Replace(" ", "&nbsp;");
        //    if (message.Contains("\'"))
        //        message = message.Replace("\'","&quot;" );
        //    if (message.Contains("&"))
        //        message = message.Replace("&","&amp;" );
        //    if (message.Contains(">"))
        //        message = message.Replace(">", "&gt;");
        //    if (message.Contains("<"))
        //        message = message.Replace( "<","&lt;");
        //    if (message.Contains("!"))
        //        message = message.Replace("!", "&#33;");
        //    if (message.Contains("\""))
        //        message = message.Replace("\"", "&lt;");
        //    if (message.Contains("#"))
        //        message = message.Replace("#", "&#35;");
        //    if (message.Contains("$"))
        //        message = message.Replace("$", "&#36;");
        //    if (message.Contains("%"))
        //        message = message.Replace("%", "&#37;");
        //    if (message.Contains("("))
        //        message = message.Replace("(","&#40;" );
        //    if (message.Contains(")"))
        //        message = message.Replace(")", "&#41;");
        //}
        return message;
    }

    /// <summary>
    /// MaqilChimp Integartion
    /// </summary>
    /// <param name="UserType"></param>
    /// <returns></returns>
    private bool AddMailChimpSubscriber(int NewTributePackageId)
    {
        bool returnVal = false;
        try
        {

            listSubscribe Subscribe = new listSubscribe();
            listSubscribeInput input = new listSubscribeInput();
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {

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
                iPackageList = _presenter.GetMyTributesPackages(objSessionvalue.UserId);
                string strPackageGroup = string.Empty;
                string packageGroup = string.Empty;
                foreach (int packId in iPackageList)
                {
                    packageGroup = FetchInterestGroupOnPackage(packId);
                    if (!strPackageGroup.Contains(packageGroup))
                    {
                        if (string.IsNullOrEmpty(strPackageGroup))
                        {
                            strPackageGroup =  packageGroup;
                        }
                        else
                        {
                            strPackageGroup = strPackageGroup + " , " + packageGroup;
                        }
                    }

                }
                if (string.IsNullOrEmpty(strPackageGroup))
                {
                    strPackageGroup = FetchInterestGroupOnPackage(PackageId);
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

}//end class


