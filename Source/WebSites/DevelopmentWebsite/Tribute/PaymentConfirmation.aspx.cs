///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.PaymentConfirmation.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the Payment Confirmation page after a successful sponsorship transaction
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES
using System;
using System.Configuration;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.Tribute.Views;
using TributesPortal.Utilities;
#endregion

public partial class Tribute_PaymentConfirmation : System.Web.UI.Page, IPaymentConfirmation
{
    #region CLASS VARIABLES
    private PaymentConfirmationPresenter _presenter;
    private SessionValue objSessionValue = null;
    private int _userId = 0;
    protected string _userName = string.Empty;
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            GetSessionValues();
            if (Request.QueryString["tid"] != null)
            {

                if (int.Parse(Request.QueryString["tid"].ToString()) != 0)
                {
                    if (Request.QueryString["SentFrom"] != null)
                    {
                        if (Request.QueryString["SentFrom"] == "VideoTributeSpons")
                        {
                            this._presenter.OnViewInitializedForVideoTribute();
                        }
                    }
                    else
                    {
                        this._presenter.OnViewInitialized();

                    }
                    CreditOrderDiv.Visible = true;
                }

            }
            //else

            SetControlVisibility();
            if (Request.QueryString["SentFrom"] != null)
            {
                if (Request.QueryString["SentFrom"] == "OrderCredit")
                {
                    CreditOrderDiv.Visible = true;
                    lblCreditOrder.Text = Session["LineForPayConf"].ToString();
                }

            }
        }
        this._presenter.OnViewLoaded();
        if (myprofile.Visible)
        {
            // Added by Ashu on Oct 3, 2011 for rewrite URL 
            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                myprofile.HRef = Session["APP_BASE_DOMAIN"] + "moments.aspx";
            else
                myprofile.HRef = Session["APP_BASE_DOMAIN"] + "tributes.aspx";
        }
    }

    protected void lbtnNext_Click(object sender, EventArgs e)
    {

        StateManager stateTribure = StateManager.Instance;
        Tributes objTribute = (Tributes)stateTribure.Get("TributeSession", StateManager.State.Session);

        if (WebConfig.ApplicationMode.Equals("local"))
        {
            if (Request.QueryString["SentFrom"] != null)
            {
                if (Request.QueryString["SentFrom"] == "OrderCredit")
                {
                    // Added by Ashu on Oct 3, 2011 for rewrite URL 
                    if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                        Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + "moments.aspx");
                    else
                        Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + "tributes.aspx");
                }
                if (Request.QueryString["SentFrom"] == "VideoTributeSpons")
                {
                    Response.Redirect(Session["APP_PATH"].ToString() + "video/videotribute.aspx?tributeId=" + objTribute.TributeId.ToString());
                }
            }

            else if ((Request.QueryString["PageName"] != null) && (Request.QueryString["PageName"].Equals("AdminMytributesPrivacy")))
            {
                Session["Sentby"] = "TributeSponsor";
                // Added by Ashu on Oct 4, 2011 for rewrite URL 
                if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                    Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + "AdminMyMomentsPrivacy.aspx");
                else
                    Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + "AdminMytributesPrivacy.aspx");
            }
            else
                Response.Redirect(Session["APP_BASE_DOMAIN"].ToString() + Request.QueryString["TributeUrl"].ToString() + "/");
        }
        else
        {
           if (Request.QueryString["SentFrom"] != null)
            {
                if (Request.QueryString["SentFrom"] == "OrderCredit")
                {
                    // Added by Ashu on Oct 3, 2011 for rewrite URL 
                    if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                     Response.Redirect("http://www." + WebConfig.TopLevelDomain + "/" + "moments.aspx");
                    else
                     Response.Redirect("http://www." + WebConfig.TopLevelDomain + "/" + "tributes.aspx");
                }
                if (Request.QueryString["SentFrom"] == "VideoTributeSpons")
                {
                    Response.Redirect("http://video." + WebConfig.TopLevelDomain + "/video/videotribute.aspx?tributeId=" + objTribute.TributeId.ToString());
                }
            }


            else if ((Request.QueryString["PageName"] != null) && (Request.QueryString["PageName"].Equals("AdminMytributesPrivacy")))
            {
                Session["Sentby"] = "TributeSponsor";
                // Added by Ashu on Oct 4, 2011 for rewrite URL 
                if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                    Response.Redirect("http://www." + WebConfig.TopLevelDomain + "/" + "AdminMyMomentsPrivacy.aspx");
                else
                    Response.Redirect("http://www." + WebConfig.TopLevelDomain + "/" + "AdminMytributesPrivacy.aspx");

            }
            else
                Response.Redirect("http://" + Request.QueryString["TributeType"].ToString().ToLower() + "." + WebConfig.TopLevelDomain + "/" + Request.QueryString["TributeUrl"].ToString() + "/");
        }
    }
    #endregion

    #region PROPERTIES
    [CreateNew]
    public PaymentConfirmationPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    public int TransactionId
    {
        get
        {
            //return int.Parse(Session["TransactionId"].ToString());
            return int.Parse(Request.QueryString["tid"].ToString());
        }
    }

    public PaymentReceipt TransactionDetails
    {
        set
        {

            if (value.Packagename.Contains("Life"))
            {
                //if (HttpContext.Current.Session["SentFrom"] == "VideoTributeSpons")
                if (Request.QueryString["SentFrom"] != null)
                {
                    if (Request.QueryString["SentFrom"] == "VideoTributeSpons")
                    {
                        package1.InnerHtml = "The <b>" + value.Tributename + "</b> " + value.TypeDescription + " Tribute has been extended for <b>life</b> and will <b>never expire</b>. <br/><br/> Your credit card will show a charge from \"YOUR TRIBUTE\" for <b>$" + value.AmountPaidDouble + "</b>. Your transaction ID is <b>" + value.TransactionId.ToString() + "</b>.<br/>";
                    }
                }
                else
                {
                    if (value.TypeDescription.ToLower().Equals("memorial"))
                    {
                        package1.InnerHtml = "The <b>" + value.Tributename + "</b> obituary has been upgraded and the advertising has been removed. <br/><br/> Your credit card will show a charge from \"YOUR TRIBUTE\" for <b>$" + value.AmountPaidDouble + "</b>. Your transaction ID is <b>" + value.TransactionId.ToString() + "</b>.<br/>";
                    }
                    else
                    {
                        package1.InnerHtml = "The <b>" + value.Tributename + "</b> " + value.TypeDescription + " " + WebConfig.ApplicationWordForInternalUse + " has been extended for <b>life</b> and will <b>never expire</b>. <br/><br/> Your credit card will show a charge from \"YOUR TRIBUTE\" for <b>$" + value.AmountPaidDouble + "</b>. Your transaction ID is <b>" + value.TransactionId.ToString() + "</b>.<br/>";
                    }

                }
                package1.Visible = true;
                package2.Visible = false;
            }
            else if (value.Packagename.Contains("Yearly"))
            {
                if (value.IsAutomaticRenew)
                {
                    if (value.TypeDescription.ToLower().Equals("memorial"))
                    {
                        package2.InnerHtml = "The <b>" + value.Tributename + "</b> obituary has been upgraded and the advertising has been removed for <b>1 Year</b> and will automatically renew on <b>" + value.Enddate + "</b>.  You can turn off auto-renewal at any time in tribute management.";
                    }
                    else
                    {
                        package2.InnerHtml = "The <b>" + value.Tributename + "</b> " + value.TypeDescription + " " + WebConfig.ApplicationWordForInternalUse + " has been extended for <b>1 Year</b> and will automatically renew on <b>" + value.Enddate + "</b>.  You can turn off auto-renewal at any time in tribute management.";
                    }
                    //autoRenew.Visible = true;
                    //if (HttpContext.Current.Session["SentFrom"] == "VideoTributeSpons")
                    if (Request.QueryString["SentFrom"] != null)
                    {
                        if (Request.QueryString["SentFrom"] == "VideoTributeSpons")
                        {
                            package2.InnerHtml += "<br/><br/><p>Your credit card will show a charge from \"YOUR TRIBUTE\" for <b>$" + value.AmountPaidDouble + "</b>. Your transaction ID is <b>" + value.TransactionId.ToString() + "</b>.</P>";
                        }
                    }
                    else
                    {
                        package2.InnerHtml += "<br/><br/><p>Your credit card will show a charge from \"YOUR TRIBUTE\" for <b>$" + value.AmountPaidDouble+ "</b>. Your transaction ID is <b>" + value.TransactionId.ToString() + "</b>.</P>";

                    }
                }
                else
                {
                    //autoRenew.Visible = false;
                    if (value.TypeDescription.ToLower().Equals("memorial"))
                    {
                        package2.InnerHtml = "The <b>" + value.Tributename + "</b> obituary has been upgraded and the advertising has been removed for <b>1 Year</b> and will now expire on <b>" + value.Enddate + "</b>.<br/>";
                    }
                    else
                    {
                        package2.InnerHtml = "The <b>" + value.Tributename + "</b> " + value.TypeDescription + " " + WebConfig.ApplicationWordForInternalUse + " has been extended for <b>1 Year</b> and will now expire on <b>" + value.Enddate + "</b>.<br/>";
                    }
                    //if (HttpContext.Current.Session["SentFrom"] == "VideoTributeSpons")
                    if (Request.QueryString["SentFrom"] != null)
                    {
                        if (Request.QueryString["SentFrom"] == "VideoTributeSpons")
                        {
                            package2.InnerHtml += "<br/><p>Your credit card will show a charge from \"YOUR TRIBUTE\" for <b>$" + value.AmountPaidDouble + "</b>. Your transaction ID is <b>" + value.TransactionId.ToString() + "</b>.</P>";
                        }
                    }

                    else
                    {
                        package2.InnerHtml += "<br/><p>Your credit card will show a charge from \"YOUR TRIBUTE\" for <b>$" + value.AmountPaidDouble + "</b>. Your transaction ID is <b>" + value.TransactionId.ToString() + "</b>.</P>";
                    }
                }

                package2.Visible = true;
                package1.Visible = false;
            }
        }

    }
   
    #endregion

    #region METHODS
    /// <summary>
    /// Method to set controls visibility.
    /// </summary>
    private void SetControlVisibility()
    {
        if (_userId > 0)
        {
            divProfile.Visible = true;
            spanLogout.InnerHtml = "<a href='logout.aspx'>Log out</a>";
        }
        else
        {
            divProfile.Visible = false;
            spanLogout.InnerHtml = "<a href='javascript: void(0);' onclick='UserLoginModalpopup(location.href,document.title);' >Log in</a>";
        }
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
            if (objSessionValue.UserType == 1)
            {
                _userName = objSessionValue.FirstName;
            }
            else
            {
                _userName = objSessionValue.UserName;
            }
        }
        else if (Equals(objSessionValue, null) || _userId == 0)
        {
            if (Request.QueryString["tid"] != null && int.Parse(Request.QueryString["tid"].ToString()) > 0)
            {
                _userId = 0;
                _userName = string.Empty;
            }
            else
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
        }
    }
    #endregion

}


