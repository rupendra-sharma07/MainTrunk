
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.TributeCreation.aspx.cs
///Author          : Mohit Gupta
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
using System.Xml;
using PerceptiveMCAPI.Types;
using PerceptiveMCAPI.Methods;
using PerceptiveMCAPI;


//using System.Web.SessionState;

#endregion Refrences
/*Designed and Developed by Optimus Information 
'
' Description   :   This Class willfollow the the life cycle of Tribute Creation.
' Date Created  :   7 Nov 2010
' Revision History:-*/

public partial class Tribute_TributeCreation : PageBase, ITributeCreation
{
    #region Variables
    private VideoTributeCreationPresenter _presenter;
    int UserID;
    protected string _UserMail;
    static string Status;
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
    //private int rowOneCredit;     // commented by Ud to remove warning
     
    private string _postMessage = string.Empty;
    private string _messageWithoutHtml = string.Empty;
    private int _defaultTheme;

    #region BeanStream varriables
    string sBeanStreamResponce = string.Empty;
    #endregion

    #endregion Variables

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.Action = Request.RawUrl;
        if (!IsPostBack)
        {
            SetErrorHeader12();
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                Setdefault();
                UserID = objSessionvalue.UserId;
                _UserMail = objSessionvalue.UserEmail;
                lbtnCreatetribute.Attributes.Add("onclick", "HideIndicator();");

                lbtnNextstep.Attributes.Add("onclick", "HideIndicator2();");
                lbtn2Nextstep.Attributes.Add("onclick", "HideIndicator3();");

                SetMessageMoxLength();
                cpvtxtCCYear.ValueToCompare = DateTime.Now.Year.ToString();
                //cpYear.ValueToCompare = DateTime.Now.Year.ToString();

                Makedata(1);
                this._presenter.GetTributeLists(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower());
                this._presenter.GetMaymentModes();
                FillDays(ddlDay);
                FillDays(ddlDay2);
                CreateTributeView.ActiveViewIndex = 0;
                Page.SetFocus(txtTributeName);
                this._presenter.GetCountryList();
                this._presenter.GetStateList();
                this._presenter.GetCCCountryList();
                this._presenter.GetCCStateList();
                // Get current credits in the user account

                _presenter.GetCreditPointCount();
                ThemeBox.Visible = false;
                FillSubCategory();
                CommonFunction("Video", "fdfd");
            }
            else
            {
                string Querystring = "?PageName=" + "TributeCreation";
                if (Request.QueryString["Type"] != null)
                {
                    Querystring += "&Type=" + Request.QueryString["Type"].ToString();
                }
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()) + Querystring, false);
            }
            if (Request.QueryString["Type"] != null)
            {
                string strTType = Request.QueryString["Type"].ToString();
            }
        }

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

            }
            catch { }
        }
    }



    private void SetMessageMoxLength()
    {
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
    protected void lbtnNextstep_Click(object sender, EventArgs e)
    {
        try
        {
            ptagtribute.InnerHtml = "Please enter some more information about the tribute you are creating for " + txtTributeName.Text;

            string error = string.Empty;
            TributesPortal.ResourceAccess.IOVS.Sanitise(txtTributeName.Text, ref error);

            if (!string.IsNullOrEmpty(error))
            {
                lblErrMsg.InnerHtml = SetHeaderMessage(error, PortalValidationSummary.HeaderText);
                lblErrMsg.Visible = true;
                return;
            }

            SetTheme_();
            SetTributeUrlname();
            hideerrormessage();

            SetMessageMoxLength();
            SetErrorHeader12();
            lblErrMsg.Visible = false;

            StateManager objtributeType = StateManager.Instance;
            _Themename = objtributeType.Get("TributeType", StateManager.State.Session).ToString();

            if (!(_Themename.Equals(string.Empty)))
            {
                if (ValidateTributeTheme())
                {
                    CreateTributeView.ActiveViewIndex = 1;
                    hfTributeValue.Value = _Themename;
                    PersonalDetails(_Themename);
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

        catch (Exception ex)
        {
            ShowMessage(PortalValidationSummary.HeaderText, ex.Message, true);
            lblErrMsg.Visible = true;
        }

    }
    protected void lbtn2Nextstep_Click(object sender, EventArgs e)
    {
        hideerrormessage();
        string error = string.Empty;
        bool pass1 = true;
        bool pass2 = false;
        bool pass3 = true;
        DateTime objDob = new DateTime();
        DateTime objDod = new DateTime();
        Indecator1.Visible = false;
        Indecator2.Visible = false;


        DateTime.TryParse(  ddlMonth2.SelectedValue + "-" +ddlDay2.SelectedValue+ "-" + txtYear2.Text, out objDod);
        if ((objDod.Year > 1800) && ((objDod.Date - DateTime.Today.Date).Days < 0))
        {
            pass2 = true;
            Indecator2.Visible = false;
        }
        else
        {
            pass2 = false;
            Indecator2.Visible = true;
        }
        if ((!string.IsNullOrEmpty(txtYear.Text.Trim())) || (!string.IsNullOrEmpty(ddlDay.SelectedValue.Trim())) || (!string.IsNullOrEmpty(ddlMonth.SelectedValue.Trim())))
        {
            pass1 = DateTime.TryParse( ddlMonth.SelectedValue + "-" + ddlDay.SelectedValue + "-" +txtYear.Text, out objDob);
            if ((objDob.Year > 1800) && ((objDob.Date - DateTime.Today.Date).Days < 0))
            {
                if ((objDod.Date - objDob.Date).Days >= 0)
                {
                    pass3 = true;
                    pass1 = true;
                    Indecator1.Visible = false;
                }
                else
                {
                    pass3 = false;
                    //pass1 = false;
                    Indecator1.Visible = true;
                }
            }
            else
            {
                pass1 = false;
                Indecator1.Visible = true;
            }
        }
        if (pass1 && pass2 && pass3)
        {
            # region step2
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

            //if (rdoTributeTypes.SelectedItem.Text == "Memorial")
            if (_Themename == "Video")
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

            else if (_Themename == "Memorial")
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
            # endregion
        }
        else
        {
            string errorText = string.Empty;
            if (!pass1)
                errorText = "Please enter valid Date of Birth.";
            if (!pass2)
            {
                if (string.IsNullOrEmpty(errorText))
                    errorText = "Please enter valid Date of Death.";
                else
                    errorText = errorText + "</br> Please enter valid Date of Death.";
            }
            if (!pass3)
            {
                if (string.IsNullOrEmpty(errorText))
                    errorText = "Date of Death should be greater or equal to Date of Birth.";
                else
                    errorText = errorText + "</br> Date of Birth should be greater or equal to Date of Death.";
            }

            lblErrMsg.InnerHtml = SetHeaderMessage(errorText, PortalValidationSummary.HeaderText);
            lblErrMsg.Visible = true;
        }
    }
    protected void lbtn3Nextstep_Click(object sender, EventArgs e)
    {
        hideerrormessage();
        StateManager objtributeType = StateManager.Instance;
        if (objtributeType.Get("TributeType", StateManager.State.Session) != null)
            _Themename = objtributeType.Get("TributeType", StateManager.State.Session).ToString();
        else
            RedirectToLoginPage();

        CreateTributeView.ActiveViewIndex = 3;
        PersonalDetailsEdit(_Themename);
        EditStep1();

    }
    protected void ltbn4Nextstep_Click(object sender, EventArgs e)
    {
        hideerrormessage();
        CreateTributeView.ActiveViewIndex = 4;
        SetErrorHeader5();
        rdoMembershipYearly.Checked = true;

        // MG Get 1 year cost from webconfig
        lblTotalCredit.Text = WebConfig.OneYearCreditCost;
        // Get current credit Cost
        _presenter.GetCreditPointCount();

        //MG Get credit and cost /credit from DB
        _presenter.GetCreditCostMapping();
        grdCreditCostTable.DataSource = _creditCostMappingList;
        grdCreditCostTable.DataBind();

        // MG Billing panel will only be visible when  curretn credit count in the billing panel; will be 0 or less than what is required based on time selected
        if (_NetCreditCount == 0)
        {
            DisplayBillingPanel();
        }
        else if (_NetCreditCount < int.Parse(WebConfig.OneYearCreditCost))
        {
            DisplayBillingPanel();
        }
        else
        {
            PanelBillingInfo.Visible = false;
            PnlPaymentDetails.Visible = false;
        }

    }
    protected void lbtnCreatetribute_Click(object sender, EventArgs e)
    {
        if (lblErrMsg.Visible==false)
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
                    _tributeURL = "http://video." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl;

                }
                else
                {
                    lblErrMsg.InnerHtml = SetHeaderMessage("Expiry Date cannot be less than current date.", PortalValidationSummary.HeaderText);
                    lblErrMsg.Visible = true;
                    SpanExpirDate.Visible = true;
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
    protected void rdoMembershipLifetime_CheckedChanged(object sender, EventArgs e)
    {

        ScriptManager.RegisterClientScriptBlock(rdoMembershipLifetime, GetType(), "HidePanel", "hideWideRows();", true);
        lblTotalCredit.Text = WebConfig.LifeTimeCreditCost;
        BillingTotal.InnerHtml = WebConfig.LifeTimeAmount;

        //Getting current Credit points in the user account
        _presenter.GetCreditPointCount();

        //Getting credit cost mapping list to display in a tabular form.
        _presenter.GetCreditCostMapping();
        //Rebinding the grid to update as per the package type selected.
        grdCreditCostTable.DataSource = _creditCostMappingList;
        grdCreditCostTable.DataBind();

        //Display Panel if enough credits are not available
        if (_NetCreditCount == 0)
        {
            DisplayBillingPanel();

        }

        //if NetCredit count avialable is less than required
        else if (_NetCreditCount < int.Parse(WebConfig.LifeTimeCreditCost))
        {
            DisplayBillingPanel();
        }
        else
        {
            PanelBillingInfo.Visible = false;
            PnlPaymentDetails.Visible = false;
        }


        PanelFreeTrial.Visible = false;
        SpanExpirDate.Visible = false;

        _presenter.GetCrditCardDetails();
        _presenter.GetSelectedPaymentMode();
    }

    protected void rdoMembershipYearly_CheckedChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(rdoMembershipYearly, GetType(), "HidePanel", "hideWideRows();", true);
        lblTotalCredit.Text = WebConfig.OneYearCreditCost;
        BillingTotal.InnerHtml = WebConfig.OneyearAmount;
        //Getting current Credit points in the user account
        _presenter.GetCreditPointCount();

        //Getting credit cost mapping list to display in a tabular form.
        _presenter.GetCreditCostMapping();

        //Rebinding the grid to update as per the package type selected.
        grdCreditCostTable.DataSource = _creditCostMappingList;
        grdCreditCostTable.DataBind();

        //Display Panel if enough credits are not available
        if (_NetCreditCount == 0)
        {
            DisplayBillingPanel();
        }
        //if NetCredit count avialable is less than required
        else if (_NetCreditCount < int.Parse(WebConfig.OneYearCreditCost))
        {
            DisplayBillingPanel();
        }
        else
        {
            PanelBillingInfo.Visible = false;
            PnlPaymentDetails.Visible = false;
        }
        PanelFreeTrial.Visible = false;

        SpanExpirDate.Visible = false;

        _presenter.GetCrditCardDetails();
        _presenter.GetSelectedPaymentMode();

    }
    protected void ddlCCCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.GetCCStateList();
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
    // St theme to be used for tribute
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
                strtheme.Append("<div class='yt-ChannelSelected yt-ShowBox'>");
                strtheme.Append("<fieldset class='yt-ThemeSelection yt-CompactRadioList'>");
                strtheme.Append("<legend><em class='required'>* </em>Select the theme you would like to use for this");
                strtheme.Append("tribute:</legend>");
                strtheme.Append("<div class='yt-ThemeSet yt-ShowBox' id='yt-Themes" + themes_[0].Tributetype.Replace(" ", "") + "'>");
                for (int i = 0; i < themes_.Count; i++)
                {

                    strtheme.Append("<div class='yt-Form-Field yt-Form-Field-Radio yt-top' id='yt-" + themes_[i].ThemeValue + "'>");
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
                        if (i == 0)
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
                    strtheme.Append("<a href='javascript: void(0);' onclick='viewThemeFolderSample(" + themes_[i].FolderName + ");'>View Sample</a>");
                    strtheme.Append(" <label for='rdo" + themes_[i].ThemeValue + "'>");
                    //strtheme.Append(themes_[i].ThemeName + "<span class='yt-ThemeColorPrimary'></span><span class='yt-ThemeColorSecondary'></span></label>");
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
        string DefaultPath = eventPath[0] + "/" + eventPath[1] + "/" + txtTributeName.Text + "_" + _presenter.View.UserId.ToString() + "_Video" + "/" + eventPath[7];
        string srcPath = eventPath[0] + "/" + eventPath[1] + "/" + eventPath[6] + "/" + fileName;
        if (hdnStoryImageURL.Value != "")
        {
            _Tributeimage = txtTributeName.Text + "_" + _presenter.View.UserId.ToString() + "_Video" + "/" + eventPath[7] + "/" + fileName;
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
                //if (File.Exists(Path.Combine(DefaultPath, fileName)))
                //{
                //    File.Delete(Path.Combine(DefaultPath, fileName));
                //    string getpath = imgTributePhoto.ImageUrl ;
                //    File.Copy(getpath, Path.Combine(DefaultPath, fileName));
                //    tributephoto_.Src = imgTributePhoto.ImageUrl;
                //}
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
        int Couponamount = 0;
        string strBillingTotal;
        int NewUpdatedCredit = 0;

        StateManager statemail = StateManager.Instance;
        try
        {
            SessionValue objSessionmail = (SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionmail != null)
            {
                Firstname = objSessionmail.FirstName;
                LastName = objSessionmail.LastName;
                UserMail = objSessionmail.UserEmail;
            }

            if (PnlPaymentDetails.Visible == true)
            {
                if (rdoMembershipLifetime.Checked)
                {
                    // MG Get life time cost from Web Config
                    amount = WebConfig.LifeTimeCreditCost;

                    // MG Get cost to be charged from Billing span at the bottom
                    strBillingTotal = Convert.ToString(BillingTotal.InnerText);
                    Couponamount = Convert.ToInt32(strBillingTotal.Substring(1, strBillingTotal.Length - 1));
                }
                if (rdoMembershipYearly.Checked)
                {
                    // MG Get 1 year credit  from Web Config
                    amount = WebConfig.OneYearCreditCost;

                    // MG Get cost to be charged from Billing span at the bottom
                    strBillingTotal = Convert.ToString(BillingTotal.InnerText);
                    Couponamount = Convert.ToInt32(strBillingTotal.Substring(1, strBillingTotal.Length - 1));
                }
            }
            else
            {
                Couponamount = 0;
            }

            try
            {
                _amount = Couponamount.ToString();
                // Pay only if coupon amount is graeter than 0 or billing panel is visible
                if (Couponamount > 0 && PnlPaymentDetails.Visible == true)
                    sBeanStreamResponce = sBeanStreamResponce = objPay.PayYourBill(TributePortalSecurity.Security.DecryptSymmetric(this._presenter.View.CreditCardNo), txtCCVerification.Text, int.Parse(ddlCCMonth.SelectedValue), int.Parse(txtCCYear.Text), Couponamount, SelectCreditCardType(), txtCCName.Text.Trim(), "", _presenter.View.Address.Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay), txtCCCity.Text, StateV1.CA, CountryV1.US, txtCCZipCode.Text, _presenter.View.Telephone.ToString(), txtEmailAddress.Text.Trim(), HttpContext.Current.Request.UserHostAddress.ToString(), out confirmationId, out errorMesg, out _transaction);
                else
                    _transaction = true;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }

            if (_transaction)
            {
                //Send mail for Video tribute after checking if session is not null as send mail for Normal Tribute Creation
                Session["FromVideoTributeCreation"] = "VideoTribute";
                // Create a Tribute in TRIBUTES table generating a tributeId as Identity and send a mail to Tribute owner
                Application["Identity"] = (System.Decimal)_presenter.CreateTribute();

                bool resultSubscribe = AddMailChimpSubscriber(PackageId);

                Tributes objTributeDetail = new Tributes();
                objTributeDetail.TributeId = int.Parse(Application["Identity"].ToString());
                objTributeDetail.TributeName = txtTributeName.Text.Trim();
                objTributeDetail.TypeDescription = _Themename;
                objTributeDetail.TributeUrl = txtTributeName.Text;
                if (PnlPaymentDetails.Visible == true)
                {
                    //For Email seetinf as per whether the mail needs to be sent for Normal Tribute Creation or Video tribute Creation
                    Session["ViaCreditCard"] = true;
                    // Insert Credit Card details only if billing panel is visible
                    CCIdentity = (System.Decimal)_presenter.InsertCCDetails(objSessionmail, objTributeDetail, txtEmailAddress.Text.Trim());
                }
                else
                {
                    Session["ViaCreditCard"] = false;
                }

                // Make an entry in TributePackage table with user Id as UserId and tributeUserId as Application Idenetity of Tributes table
                this._presenter.InsertPackageDetails(Application["Identity"].ToString(), CCIdentity.ToString(), confirmationId);

                //MG make a fresh entry for New Credit Points in CreditPointTransaction table
                _presenter.GetCreditPointCount();
                if (PnlPaymentDetails.Visible == true)
                {
                    if (rdoMembershipYearly.Checked)
                    {
                        NewUpdatedCredit = int.Parse(Session["CreditPointSelected"].ToString()) + _NetCreditCount - int.Parse(WebConfig.OneYearCreditCost);
                    }
                    else if (rdoMembershipLifetime.Checked)
                    {
                        NewUpdatedCredit = int.Parse(Session["CreditPointSelected"].ToString()) + _NetCreditCount - int.Parse(WebConfig.LifeTimeCreditCost);
                    }
                }
                else
                {
                    if (rdoMembershipYearly.Checked)
                    {
                        NewUpdatedCredit = _NetCreditCount - int.Parse(WebConfig.OneYearCreditCost);
                    }
                    else if (rdoMembershipLifetime.Checked)
                    {
                        NewUpdatedCredit = _NetCreditCount - int.Parse(WebConfig.LifeTimeCreditCost);
                    }
                }

                // Make an entry for new credits points attained by the user if the new updated credit is more than 0
                if (NewUpdatedCredit >= 0)
                {
                    this._presenter.InsertCurrentCreditPoints(NewUpdatedCredit, CCIdentity.ToString(), confirmationId);


                    //to get the information abt the payment package used
                    _presenter.TriputePackageInfo(int.Parse(Application["Identity"].ToString()));

                    //MG If the New updated credit in the Ctreator's  is greater than or equal to 0, then only the receipt and mail should be sent
                    if (NewUpdatedCredit >= 0)
                        //to get the sponsorship receipt and send the same on Email 
                        _presenter.OnViewInitialized();
                    //End
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
                objTribute.TributeUrl = txtTributeName.Text; 
                TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
                stateManager.Add("TributeSession", objTribute, TributesPortal.Utilities.StateManager.State.Session);


                if (NewUpdatedCredit == 0)
                {
                    NetCreditPointStep6.InnerHtml = "<img align ='top' src='../assets/images/error_pic.png'/> <b>You now have <span class='bold_red'>0 Credits</span> remaining in your account!</b>";

                }
                else if (NewUpdatedCredit == 1)
                {
                    NetCreditPointStep6.InnerHtml = "<img align ='top' src='../assets/images/dollor-pic.png'/> <b>You now have </b><span class='bold_green'>" + NewUpdatedCredit.ToString() + " credit</b></span> <b>remaining in your account!</b>";
                }
                else
                    NetCreditPointStep6.InnerHtml = "<img  align ='top' src='../assets/images/dollor-pic.png'/> <b>You now have</b> <span class='bold_green'><b>" + NewUpdatedCredit.ToString() + " credits</b></span> <b>remaining in your account!</b>";

                Master.CreditLinkButton = NewUpdatedCredit.ToString();
                txtDirectLink.Text = "http://video." + WebConfig.TopLevelDomain + "/video/videotribute.aspx?tributeId=" + Application["Identity"].ToString();
                txtWebsiteLink.Text = "<a href=\"http://video." + WebConfig.TopLevelDomain + "/video/videotribute.aspx?tributeId=" + Application["Identity"].ToString() + "\" target=\"_blank\" alt=\" View Video Tribute\"></a>";

             /*   // Create a folder for Tribute
                if (_Themename.ToLower() == "video")
                    _presenter.CreateDefaultFolder(WebConfig.VideoFolderPath);
                */
                //to save video tribute if user is coming from Video Upload page
                if (!Equals((stateManager.Get("TokenDetails", StateManager.State.Session)), null))
                {
                    _presenter.SaveVideoTribute();
                    stateManager.Add("TokenDetails", null, StateManager.State.Session); //to set null to tokendetails session
                }

                objtributeType.Remove("TributeType", StateManager.State.Session);
                _Themename = "";
                UserID = 0;
                retvalue = true;
            }
            else
            {
                //lblErrMsg.InnerHtml = SetHeaderMessage(errorMesg, PortalValidationSummary.HeaderText);
                //lblErrMsg.Visible = true;
                //retvalue = false;
                var sResponseArr = sBeanStreamResponce.Split('&');
                var sErrorMsg = sResponseArr.Length > 3 && sResponseArr[3].Split('=').Length > 1 && !string.IsNullOrEmpty(sResponseArr[3].Split('=')[1]) ? sResponseArr[3].Split('=')[1].Replace("+", " ") : "";

                lblErrMsg.InnerHtml = SetHeaderMessage(sErrorMsg, PortalValidationSummary.HeaderText);
                lblErrMsg.Visible = true;
                retvalue = false;
            }

        }
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
            Response.Redirect(Session["APP_PATH"].ToString() + "video/videotribute.aspx?tributeId=" + Application["Identity"].ToString());
        }
        else
        {
            Response.Redirect("http://video." + WebConfig.TopLevelDomain + "/video/videotribute.aspx?tributeId=" + Application["Identity"].ToString());
        }
    }
    #endregion Events

    #region Private Members


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
        //pnlTributeAddressAvailable.Visible = false;
        //lbtncheckAddress.Visible = true;
        //  ImageUnAvailable.Visible = false;
        //txtTributeAddress.Text = "";
        //txtTributeAddress.Enabled = true;
        //rdbAvailableAddress1.Checked = rdbAvailableAddress2.Checked = rdbAvailableAddress3.Checked = false;
        //txtTributeAddressOther.Text = "";
        lblErrMsg.InnerHtml = SetHeaderMessage("Web address is unavailable, please try again.", PortalValidationSummary.HeaderText);
        lblErrMsg.Visible = true;
        // ShowMessage("Web address is unavailable, please try again.", true);
        //errorAddress.Visible = true;
    }

    [CreateNew]
    public VideoTributeCreationPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    //private void SetAvailableAddresstext_()
    //{
    //    StateManager objtributeType = StateManager.Instance;
    //    if (objtributeType.Get("TributeType", StateManager.State.Session) != null)
    //        _Themename = objtributeType.Get("TributeType", StateManager.State.Session).ToString().ToLower().Replace(" ", "");
    //    else
    //        RedirectToLoginPage();


    //    rdbAvailableAddress1.Text = "http://" + _Themename + "." + WebConfig.TopLevelDomain + "/" + this._presenter.SequenceTributeName(txtTributeAddress.Text, _Themename);
    //    rdbAvailableAddress2.Text = "http://" + _Themename + "." + WebConfig.TopLevelDomain + "/" + _Themename.ToString().Replace(" ", "") + DateTime.Now.Year + txtTributeAddress.Text;
    //    rdbAvailableAddress3.Text = "http://" + _Themename + "." + WebConfig.TopLevelDomain + "/" + txtTributeAddress.Text + DateTime.Now.Year.ToString();
    //}

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
        //txtDomainName.Text = "http://" + _Themename.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + this._presenter.View.TributeUrl;
    }
    private void SetDefaultFirst()
    {
        SetTributeUrlname();
        //txtTributeAddress.Text = this._presenter.View.TributeUrl;
        //pnlTributeAddressAvailable.Visible = false;
        //lbtncheckAddress.Visible = true;
        //txtTributeAddress.Enabled = true;
        //rdbAvailableAddress1.Checked = rdbAvailableAddress2.Checked = rdbAvailableAddress3.Checked = false;
        //txtTributeAddressOther.Text = "";
        //errorAddress.Visible = false;
        lblErrMsg.Visible = false;
        //SetTheme_();      
        //SpanAvailable.Visible = false;
        //imgMsgStatus2.Visible = false;
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
            case "Video":
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

                //Location = SetLocation();
                //EditPersonalDetails(" " + was + " graduate on:", date1, "", date2, " " + was + " graduate in:", Location, true, false, true);
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

            case "Video":
                date1 = ddlMonth.SelectedItem.Text + " " + ddlDay.SelectedItem.Text + ", " + txtYear.Text;
                date2 = ddlMonth2.SelectedItem.Text + " " + ddlDay2.SelectedItem.Text + ", " + txtYear2.Text;
                Location = SetLocation();
                EditPersonalDetails(" was born on:", date1, " died on:", date2, " died in:", Location, true, true, true);
                break;
        }
        //lblEditMeaasge.Text = txtarMessage.Text;
        //Mohit lblEditMeaasge.InnerText = txtarMessage.Text.Length > 250 ? txtarMessage.Text.Substring(0, 250) : txtarMessage.Text;
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
        //lblEditBornOn.Text = txtTributeName.Text +" "+lblDate1.InnerText.Replace("* ","").ToLower();
        if (strtext2.Length > 6)
        {
            lblEditBornOn.Text = txtTributeName.Text + strtext1;
            txtEditBornOn.Text = strtext2;
        }
        ////Date2
        //lblEditDeathOn.Text = txtTributeName.Text + " " + lblDate2.InnerText.Replace("* ", "").ToLower();
        if (strtext4.Length > 6)
        {
            lblEditDeathOn.Text = txtTributeName.Text + strtext3;
            txtEditDeathOn.Text = strtext4;
        }
        ////Location
        //lblEditBornin.Text = txtTributeName.Text + " location :";
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
        // PaneleditAge.Visible = val4;
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

        //Display the Donaiton Box to Review & Edit
        //Mohit  EditDonation();
    }

    /// <summary>
    /// Display the Donaiton Box to Review & Edit
    /// </summary>


    private void DateValisdation(string TributeType)
    {
        if (TributeType == "New Baby")
        {
            //cvDate12.Visible = true;
            // cvYear.Visible = true;
            // cpYear.Visible = true;
            //cvNewbaby.Visible = true;
            reset1.Visible = true;
            reset2.Visible = true;
        }
        else
        {
            //cvDate12.Visible = false;
            // cpYear.Visible = false;
            //  cvYear.Visible = false;
            //cvNewbaby.Visible = false;
            reset1.Visible = false;
            reset2.Visible = false;
        }

        //if (TributeType == "Memorial")
        //  //  cpYear2.Visible = true;
        //else
        // //   cpYear2.Visible = false;
    }
    private void Setdefault()
    {
        //pnlTributeAddressAvailable.Visible = false;
        //lbtncheckAddress.Visible = true;
        ////  ImageUnAvailable.Visible = false;
        //txtTributeAddress.Text = "";
        //txtTributeAddress.Enabled = true;
        //rdbAvailableAddress1.Checked = rdbAvailableAddress2.Checked = rdbAvailableAddress3.Checked = false;
        //txtTributeAddressOther.Text = "";
        Makedata(1);
        rdoPrivate.Checked = rdoPublic.Checked = false;
        chkOrderDVD.Checked = chkMemTributeBox.Checked = true;
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
        //  ltrlNotice.Text = String.Empty;
    }

    private bool ValidateTributeTheme()
    {
        //bool val = false;
        //foreach (DataListItem row in dlTributeTheames.Items)
        //{
        //    RadioButton rdb = (RadioButton)row.FindControl("rdbButton");
        //    Image TheameImage = (Image)row.FindControl("TheameImage");
        //    Label lblThemeID = (Label)row.FindControl("lblThemeID");
        //    Label lblThemeName = (Label)row.FindControl("lblThemeName");
        //    if (rdb.Checked)
        //    {
        //        val = true;
        //        ImageEditTheme.ImageUrl = TheameImage.ImageUrl;
        //        TributeTheme = int.Parse(lblThemeID.Text);
        //        lblStep4ThemeName.Text = lblThemeName.Text;
        //    }

        //}
        //return val;
        return true;
    }
    private void SetTributeTheme()
    {
        //int count = 0;
        //foreach (DataListItem row in dlTributeTheames.Items)
        //{
        //    count++;
        //    RadioButton rdb = (RadioButton)row.FindControl("rdbButton");
        //    Image TheameImage = (Image)row.FindControl("TheameImage");
        //    Label lblThemeID = (Label)row.FindControl("lblThemeID");
        //    Label lblThemeName = (Label)row.FindControl("lblThemeName");
        //    // HtmlTable t1 = (HtmlTable)row.FindControl("t1");
        //    if (count == 1)
        //    {
        //        rdb.Checked = true;
        //        ImageEditTheme.ImageUrl = TheameImage.ImageUrl;
        //        TributeTheme = int.Parse(lblThemeID.Text);
        //        lblStep4ThemeName.Text = lblThemeName.Text;

        //    }
        //}
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
                //rdoTributeTypes.DataSource = value;
                //rdoTributeTypes.DataTextField = ParameterTypesCodes.Parameters.TypeDescription.ToString();
                ////rdoTributeTypes.DataValueField = ParameterTypesCodes.Parameters.TypeDescription.ToString();
                //rdoTributeTypes.DataValueField = ParameterTypesCodes.Parameters.TypeCode.ToString();
                //rdoTributeTypes.DataBind();
                _TributeCount = value.Count;
            }
        }
    }

    public IList<Themes> ThemeNames
    {
        set
        {
            //dlTributeTheames.DataSource = value;
            //dlTributeTheames.DataBind();           
            //SetTributeTheme();
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
            /*mohit  ddlDonationCountry.DataSource = value;
             ddlDonationCountry.DataTextField = Locations.Location.LocationName.ToString();
             ddlDonationCountry.DataValueField = Locations.Location.LocationId.ToString();
             ddlDonationCountry.DataBind();
             ddlDonationCountry.Enabled = false;*/
        }
    }

    public IList<Locations> DonationStateList
    {
        set
        {
            /* Mohit //Populate the states list in donation box
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
             }  */

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

            // StateManager objtributeType = StateManager.Instance;
            // _Themename = objtributeType.Get("TributeType", StateManager.State.Session).ToString();
            return _Themename;
            //return rdoTributeTypes.SelectedItem.Text.ToString();
        }
    }

    public string SelectedCountry
    {
        get { return ddlCountry.SelectedValue.ToString(); }
        set { }
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
        get { return string.Empty; }
    }

    /// <summary>
    /// Get the Charity in Donation Box
    /// </summary>
    public string CharityName
    {
        get { return string.Empty; }
    }

    /// <summary>
    /// Get the selected Country in Donation Box
    /// </summary>
    public string SelectedDonationCountry
    {
        get { return string.Empty; }
    }

    /// <summary>
    /// Get the selected Country Text in Donation Box
    /// </summary>
    public string SelectedDonationCountryText
    {
        get { return string.Empty; }
    }

    /// <summary>
    /// Get the selected State in Donation Box
    /// </summary>
    public string SelectedDonationState
    {
        get
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Get the City in Donation Box
    /// </summary>
    public string DonationCity
    {
        get { return string.Empty; }
    }

    /// <summary>
    /// Get the Address in Donation Box
    /// </summary>
    public string DonationAddress
    {
        get { return string.Empty; }
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

            return txtTributeName.Text;
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
            //return rdoTributeTypes.SelectedValue.ToString();
        }
    }

    public string TributeUrl
    {
        get
        {
            return string.Empty;
            /*mohit if (txtTributeAddressOther.Text.Length != 0)
                 Domainname = txtTributeAddressOther.Text;
             else if (rdbAvailableAddress1.Checked)
                 Domainname = rdbAvailableAddress1.Text.ToString().Replace("http://" + this._presenter.View.TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/", "");
             else if (rdbAvailableAddress2.Checked)
                 Domainname = rdbAvailableAddress2.Text.ToString().Replace("http://" + this._presenter.View.TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/", "");
             else if (rdbAvailableAddress3.Checked)
                 Domainname = rdbAvailableAddress3.Text.ToString().Replace("http://" + this._presenter.View.TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/", "");
             else
                 Domainname = txtTributeAddress.Text;

             return Domainname;Mohit*/
            /*if (pnlTributeAddressAvailable.Visible==true)
            {
                 if (txtTributeAddressOther.Text.Length != 0)
                    Domainname = txtTributeAddressOther.Text;  
                else if (rdbAvailableAddress1.Checked)
                    Domainname = rdbAvailableAddress1.Text.ToString().Replace("http://" + txtTributeName.Text.Replace(" ","").ToString() + "." + WebConfig.TopLevelDomain + "/", "");
                else if (rdbAvailableAddress2.Checked)
                    Domainname = rdbAvailableAddress2.Text.ToString().Replace("http://" + txtTributeName.Text.Replace(" ", "") + "." + WebConfig.TopLevelDomain + "/", "");
                else if (rdbAvailableAddress3.Checked)
                    Domainname = rdbAvailableAddress3.Text.ToString().Replace("http://" + txtTributeName.Text.Replace(" ", "") + "." + WebConfig.TopLevelDomain + "/", "");
                                 
            }
            else
            {
                Domainname = txtTributeAddress.Text;
            }
            return Domainname;*/
        }
        set
        {
            _tributeURL = value;
        }
    }

    public DateTime? date1
    {
        get
        {
            DateTime _DateTime1 = new DateTime();
            //if (rdoTributeTypes.SelectedItem.Text == "Birthday")
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
                    //ShowMessage(ex.Message.ToString());
                    return null;
                }
                //return _DateTime1;

            }
            else if ((ddlDay.SelectedIndex != 0) && (ddlMonth.SelectedIndex != 0) && (txtYear.Text.Length != 0))
            {
                _DateTime1 = new DateTime(int.Parse(txtYear.Text), ddlMonth.SelectedIndex, ddlDay.SelectedIndex);
                return _DateTime1;
            }
            else
                return null;
        }
        set { }
    }

    public DateTime? date2
    {
        get
        {
            //if (PanelDate2.Visible)
            //{
            if ((ddlDay2.SelectedIndex != 0) && (ddlMonth2.SelectedIndex != 0) && (txtYear2.Text.Length != 0))
            {
                //DateTime _DateTime2 = new DateTime(int.Parse(txtYear2.Text), ddlMonth2.SelectedIndex, ddlDay2.SelectedIndex);
                DateTime _DateTime2;
                DateTime.TryParse(  ddlMonth2.SelectedValue + "-" + ddlDay2.SelectedValue + "-" + txtYear2.Text, out _DateTime2);
                return _DateTime2;
            }
            else
                return null;
            //}
            //else
            //    return null;
        }
        set { }
    }

    public string SelectedState
    {
        get { return ddlStateProvince.SelectedValue.ToString(); }
        set { }
    }

    public string SelectedCity
    {
        get { return txtCity.Text; }
        set { }
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
            //return imgTributePhoto.ImageUrl;
        }
    }

    public string WelcomeMsg
    {
        get
        {
            return string.Empty;
            //return txtarMessage.Text.Length > 250 ? txtarMessage.Text.Substring(0, 250) : txtarMessage.Text; ;
        }
    }

    public bool IsPrivate
    {
        get { return rdoPrivate.Checked; }
    }

    public bool IsOrderDVDChecked
    {
        get { return chkOrderDVD.Checked; }
    }


    public bool IsMemTributeBoxChecked
    {
        get { return chkMemTributeBox.Checked; }
    }

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
    #endregion Tribute Creation

    #region Tribute CC information

    #region GET CC DETAILS

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

        //get
        //{
        //    return TributePortalSecurity.Security.EncryptSymmetric(txtCCNumber.Text);
        //    //return txtCCNumber.Text;
        //}
        //get
        //{
        //    return txtCCNumber.Text;
        //}
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
            //DateTime _DateTime;
            //if (rdoMembershipYearly.Checked)
            //_DateTime=    DateTime.Now.AddMonths(12);
            //else


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
            return false;
        }
    }

    public string Telephone
    {
        //get { return txtCCPhone.Text; }
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
            if (rdoMembershipLifetime.Checked == true)
            {
                if ((_NetCreditCount == 0)) //|| (_NetCreditCount == null)) // commented by Ud cause a value of type int is never equal to null of type int
                {
                    NetCreditCount.InnerHtml = "<img src='../assets/images/error_pic.png' style='margin-right:4px' alt='error'/><b>You have <span class='bold_red'>no credits</span> in your account!</b> &nbsp;You can order more credits below:";
                }
                else if (_NetCreditCount < int.Parse(WebConfig.LifeTimeCreditCost))
                {
                    NetCreditCount.InnerHtml = "<img src='../assets/images/error_pic.png' style='margin-right:4px' alt='error'/><b>You do not have <span class='bold_red'>enough credits</span> in your account!</b> &nbsp;You can order more credits below:";
                }
                else
                {
                    if (_NetCreditCount == 1)
                    {
                        NetCreditCount.InnerHtml = "<img src='../assets/images/dollor-pic.png' style='margin-right:4px' alt='dollor'/><b> You have <span class='bold_green'>" + _NetCreditCount.ToString() + " credit</span> in your account!</b> &nbsp;You can order more credits below:";
                    }
                    else
                    {
                        NetCreditCount.InnerHtml = "<img src='../assets/images/dollor-pic.png' style='margin-right:4px' alt='dollor'/> <b>You have <span class='bold_green'>" + _NetCreditCount.ToString() + " credits</span> in your account! </b>";
                    }
                }
            }
            if (rdoMembershipYearly.Checked == true)
            {
                if ((_NetCreditCount == 0))// || (_NetCreditCount == null)) // commented by Ud cause a value of type int is never equal to null of type int
                {
                    NetCreditCount.InnerHtml = "<img src='../assets/images/error_pic.png' style='margin-right:4px' alt='error'/><b>You have <span class='bold_red'>no credits</span> in your account!</b>&nbsp; You can order more credits below:";
                }
                else if (_NetCreditCount < int.Parse(WebConfig.OneYearCreditCost))
                {
                    NetCreditCount.InnerHtml = "<img src='../assets/images/error_pic.png' style='margin-right:4px' alt='error'/><b>You do not have <span class='bold_red'>enough credits</span> in your account!</b> &nbsp;You can order more credits below:";
                }
                else
                {
                    if (_NetCreditCount == 1)
                    {
                        NetCreditCount.InnerHtml = "<img src='../assets/images/dollor-pic.png' style='margin-right:4px' alt='dollor'/><b> You have <span class='bold_green'>" + _NetCreditCount.ToString() + " credit</span> in your account!</b>";
                    }
                    else
                    {
                        NetCreditCount.InnerHtml = "<img src='../assets/images/dollor-pic.png' style='margin-right:4px' alt='dollor'/><b> You have <span class='bold_green'>" + _NetCreditCount.ToString() + " credits</span> in your account!</b>";
                    }
                }
            }


        }
    }
    public string ObPostMessage
    {
        get
        {
            //_postMessage = ftbNoteMessage.Text.ToString();
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
            //_messageWithoutHtml = StripHtml(ftbNoteMessage.Text.ToString());
            return _messageWithoutHtml;
        }
        set
        {
            _messageWithoutHtml = value;
        }
    }

    public IList<CreditCostMapping> CreditCostMappingList
    {
        set
        {
            _creditCostMappingList = value;
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
                //mohit txtCouponCode.Text = string.Empty;

                string strCredit = string.Empty;
                string ccnumber = TributePortalSecurity.Security.DecryptSymmetric(value.UserCreditcardDetails.CreditCardNo.Trim());
                Session["CCNumber"] = ccnumber;
                for (int indexCredit = 0; indexCredit < ccnumber.Length - 4; indexCredit++)
                    strCredit += "X";

                txtCCNumber.Text = strCredit + ccnumber.Substring(ccnumber.Length - 4);

                //txtCCNumber.Text = TributePortalSecurity.Security.DecryptSymmetric(value.UserCreditcardDetails.CreditCardNo.ToString());
                //txtCCNumber.Text = value.UserCreditcardDetails.CreditCardNo.ToString();
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
                //if (!string.IsNullOrEmpty(value.UserCreditcardDetails.SponsorEmailAddress))
                //    txtEmailAddress.Text = value.UserCreditcardDetails.SponsorEmailAddress;
                //else
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

    protected void Step2_Load(object sender, EventArgs e)
    {

    }


    private void Makedata(int count)
    {
        ViewState["Count"] = count;
        ArrayList alist = new ArrayList();
        for (int i = 0; i < count; i++)
        {
            alist.Add(i);
        }
    }





    #region ITributeCreation Members

    ArrayList ITributeCreation.AdminEmailLists
    {
        get
        {
            return alistData;
        }
    }

    #endregion

    #region ITributeCreation Members

    public string TributeFirstName
    {
        get
        {
            return string.Empty;
        }
    }

    public string TributeLastName
    {
        get
        {
            return string.Empty;
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
            /*Mohit  if (rdoMembershipFree.Checked)
             {
                 _packageid = 3;
             }*/
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
        set
        {
            _PackageId = value;
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

    // Getting value for defualt thumbnails for Video Tribute
    private void CommonFunction(string _text, string rdbid)
    {
        hfSeledctedTheme.Value = string.Empty;
        StateManager objtributeType = StateManager.Instance;
        objtributeType.Add("TributeType", _text, StateManager.State.Session);

        if (_text.ToLower().Equals("video"))
            tributeImgURL = "../assets/images/thumbnails/video_TributePhoto.jpg";

        imgTributePhoto.ImageUrl = tributeImgURL;
        SetVisibilityStatus();
        hfSelectedTribute.Value = rdbid;
        Setdefault();
        DateValisdation(_text);
        //this._presenter.GetTributeThemes_(_text);
        this._presenter.GetThemesForCategory_(_text, ddlSubCategory.SelectedValue,WebConfig.ApplicationType);
        hfTributeValue.Value = _text;
        ptagtribute.InnerHtml = "Please enter some more information about the tribute you are creating for " + txtTributeName.Text;
        SetTributeUrlname();


        PanelTributeAddress.Visible = true;
        txtTributeName.Attributes.Add("value", txtTributeName.Text);
        lblErrMsg.Visible = false;


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
        //Mohit txtCouponCode.Text = string.Empty;
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


    #region ITributeCreation Members

    private void SetThemeforStep4(string ThemeValue, string ThemeName, string Themeid, string folderName)
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
        stp4.Append(ThemeName);
        stp4.Append("<img src='" + folderPathPrefix + "assets/themes/" + folderName + "/thumb.gif' />");
        stp4.Append("</div>");
        hfSeledctedTheme.Value = Themeid;
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
                ThemeBox.Visible = true;
                //strtheme.Append("<div class='yt-ChannelSelected yt-ShowBox'>");
                //strtheme.Append("<fieldset class='yt-ThemeSelection yt-CompactRadioList'>");
                //strtheme.Append("<legend><em class='required'>* </em>Select the theme you would like to use for this");
                //strtheme.Append(" tribute:</legend>");
                strtheme.Append("<div class='yt-ThemeSet yt-ShowBox' id='yt-Themes" + value[0].Tributetype.Replace(" ", "") + "'>");
                for (int i = 0; i < value.Count; i++)
                {

                    strtheme.Append("<div class='yt-Form-Field yt-Form-Field-Radio yt-top' id='yt-" + value[i].ThemeValue + "'>");
                    strtheme.Append("<input name='rdoTheme'  onclick='SetThemeFolder(" + '"' + value[i].ThemeValue + '"' + "," + '"' + value[i].ThemeName.Replace("'", "%26") + '"' + "," + '"' + value[i].ThemeId + '"' + "," + '"' + value[i].FolderName + '"' + ");' type='radio' id='rdo" + value[i].ThemeValue + "'");
                    if (i == 0)
                    {
                        strtheme.Append(" checked='checked' ");
                        hfSeledctedTheme.Value = value[i].ThemeId.ToString();
                        SetThemeforStep4(value[i].ThemeValue, value[i].ThemeName, value[i].ThemeId.ToString(), value[i].FolderName);
                    }
                    strtheme.Append("value='" + value[i].ThemeValue + "' />");
                    strtheme.Append("<img src='" + folderPathPrefix + "assets/themes/" + value[i].FolderName + "/thumb.gif' />");
                    //strtheme.Append("<a href='javascript: void(0);' onclick='viewThemeSample(this);'>View Sample</a>");
                    strtheme.Append("<a href='javascript: void(0);' onclick=\"viewThemeFolderSample('" + value[i].FolderName + "');\">View Sample</a>");
                    strtheme.Append(" <label for='rdo" + value[i].ThemeValue + "'>");
                    //strtheme.Append(value[i].ThemeName + "<span class='yt-ThemeColorPrimary'></span><span class='yt-ThemeColorSecondary'></span></label>");
                    strtheme.Append(value[i].ThemeName + "</label>");
                    strtheme.Append("</div>");
                    //if (i == 0)
                    //{
                    //  SetThemeforStep4(value[i].ThemeValue, value[i].ThemeName, value[i].ThemeId.ToString());
                    //}

                }
                strtheme.Append("</div>");
                //strtheme.Append("</fieldset>");
                //strtheme.Append("</div>");

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

    /* mohit protected void lbtnValidateCoupon_Click(object sender, EventArgs e)
    {
        //spanCoupon.Visible = false;
        //int couponType = 0;
        //if (rdoMembershipLifetime.Checked)
        //    couponType = 3;
        //else
        //    couponType = 2;

        //int availability = _presenter.GetCouponAvailable(txtCouponCode.Text, couponType);

        //if (availability == 1)
        //{
        //    SetCouponAvailableStatus();
        //}
        //else
        //    SetCouponUnAvailableStatus();

        CheckCoupon();
    }mohit */

    /*mohit   private void SetCouponUnAvailableStatus()
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
      }*/

    /*mohit  private void SetCouponAvailableStatus()
     {
         int Couponamount = 0;
         if (rdoMembershipYearly.Checked)
         {
             amount = WebConfig.OneyearAmount;
             Couponamount = Convert.ToInt32(amount.Substring(1, amount.Length - 1));
         }
         else
         {
             amount = WebConfig.LifeTimeAmount;
             Couponamount = Convert.ToInt32(amount.Substring(1, amount.Length - 1));
         }


         spanCoupon.Attributes.Add("class", "couponCreationAvail");
         spanCoupon.InnerHtml = "Coupon is valid";
         spanCoupon.Visible = true;

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
     */

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
            //int amount;
            //if(rdoMembershipLifetime.Checked)
            //    amount = Decimal.Parse(WebConfig.LifeTimeAmount"].ToString());
            // return Decimal.Parse(BillingTotal.InnerHtml.ToString().Replace("$", ""));
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
            //return int.Parse(lstTributes.SelectedValue);
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


    #endregion

    // Binding Credit Cost Table Conditionally depending on the no. of credits available
    protected void grdCreditCostTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        _presenter.GetCreditPointCount();
        Label lblCreditPoint = (Label)e.Row.FindControl("lblCreditPoint");
        Label lblTotalCost = (Label)e.Row.FindControl("lblTotalCost");
        Label lblCostPercredit = (Label)e.Row.FindControl("lblCostPercredit");

        if (e.Row.RowIndex == 0)
        {
            ((RadioButton)e.Row.FindControl("rbtnCreditSelection")).Checked = true;

            int rowOneCredit = int.Parse(lblCreditPoint.Text);
            if (rdoMembershipLifetime.Checked)
            {
                if (_NetCreditCount < int.Parse(WebConfig.LifeTimeCreditCost))
                {
                    lblCreditPoint.Text = Convert.ToString(rowOneCredit * (int.Parse(WebConfig.LifeTimeCreditCost) - _NetCreditCount));
                    lblTotalCost.Text = "$" + Convert.ToString(int.Parse(lblCreditPoint.Text.ToString()) * (double.Parse(lblCostPercredit.Text.ToString()))) + ".00";
                }
                else
                {
                    lblTotalCost.Text = "$" + Convert.ToString(int.Parse(lblCreditPoint.Text.ToString()) * (double.Parse(lblCostPercredit.Text.ToString()))) + ".00";
                }
            }
            if (rdoMembershipYearly.Checked)
            {
                if (_NetCreditCount < int.Parse(WebConfig.OneYearCreditCost))
                {
                    lblCreditPoint.Text = Convert.ToString(rowOneCredit * (int.Parse(WebConfig.OneYearCreditCost) - _NetCreditCount));
                    lblTotalCost.Text = "$" + Convert.ToString(int.Parse(lblCreditPoint.Text.ToString()) * (double.Parse(lblCostPercredit.Text.ToString()))) + ".00";
                }
                else
                {
                    lblTotalCost.Text = "$" + Convert.ToString(int.Parse(lblCreditPoint.Text.ToString()) * (double.Parse(lblCostPercredit.Text.ToString()))) + ".00";
                }
            }
            Session["CreditPointSelected"] = int.Parse(lblCreditPoint.Text);
        }
        if (e.Row.RowIndex > 0)
        {
            lblTotalCost.Text = "$" + Convert.ToString(int.Parse(lblCreditPoint.Text.ToString()) * (double.Parse(lblCostPercredit.Text.ToString()))) + ".00";
        }
        if (e.Row.RowIndex >= 0)
        {
            lblCostPercredit.Text = "$" + lblCostPercredit.Text + "/credit";
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
        foreach (GridViewRow row in grdCreditCostTable.Rows)
        {
            if (((RadioButton)row.FindControl("rbtnCreditSelection")).Checked)
            {
                Label lblTotalCost = (Label)row.FindControl("lblTotalCost");
                BillingTotal.InnerHtml = lblTotalCost.Text.Remove(lblTotalCost.Text.Length - 3, 3);
            }
        }

    }

    private void FillSubCategory()
    {
        IList<Themes> objThemeList = _presenter.GetSubCategoryForTheme("Video");
        if (objThemeList.Count > 0)
        {
            ddlSubCategory.Items.Clear();
            foreach (Themes objTheme in objThemeList)
            {
                ddlSubCategory.Items.Add(objTheme.SubCategory);
            }
        }
        ddlSubCategory.Items.Insert(0, new ListItem(string.Format("{0} {1}", "All", "Video"), "All"));
        //'pnlSubCategory.Visible = true;
        //dvThemes.Visible = true;
    }

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        CommonFunction("Video", "fdfd");
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
                            strPackageGroup = packageGroup;
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

    #region ITributeCreation Members


    public string ApplicationType
    {
        get { return ConfigurationManager.AppSettings["ApplicationType"].ToString(); }
    }

    #endregion
}//end class


