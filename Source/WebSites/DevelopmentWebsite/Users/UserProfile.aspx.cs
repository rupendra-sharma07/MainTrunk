///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Users.UserProfile.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows a user to view his/her profile
///Audit Trail     : Date of Modification  Modified By         Description

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
using TributesPortal.Users.Views;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using System.IO;
using System.Collections.Generic;


public partial class Users_UserProfile : PageBase, IUserProfile
{
    #region private variables
    private UserProfilePresenter _presenter;
    static int UserID;
    string bannerMessage = string.Empty;
    #endregion private variables

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        // Session["UserID"] = 17;
        string str = this.Page.Title.ToString();
        if (!this.IsPostBack)
        {
            #region Changes for YourMoments/YourTribute
            Label7.Text = "NOTE: Even if you turn off all email notifications, we may sometimes need to email you important notices about your account (" + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToLower() + " expiry notices, etc).";
            cbNewsLetter.Text = "NEWSLETTERS – Email me newsletters periodically from Your " + ConfigurationManager.AppSettings["ApplicationWord"] + " with advice and promotions.";
            Label4.Text = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"]+" notifications apply to all " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"] + " that you have created or have marked as a favorite. Note that you can turn on/off ALL email alerts for specific " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"] + " on your “My " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"] + "” and “My Favorites” pages.";
            Label3.Text = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToUpper()+" NOTIFICATIONS";
            lblAllowUser.Text = "Users who view your profile have the ability to send you private messages. The user can only send messages using our internal messaging system, your email address will never be displayed on Your "+ ConfigurationManager.AppSettings["ApplicationWord"]+".";
            lblHideLocation.Text = "Your location will not be displayed anywhere on the Your " + ConfigurationManager.AppSettings["ApplicationWord"] + " website.";
            lblDisplayLocation.Text = "Your location will be displayed throughout the Your "+ ConfigurationManager.AppSettings["ApplicationWord"] + " website. This includes when users view your profile and you add content to the website (post comments, upload photos, etc)";
            Label1.Text = "Your username will be displayed throughout the Your " + ConfigurationManager.AppSettings["ApplicationWord"] + " website. This includes when users view your profile and you add content to the website (post comments, upload photos, etc)";
            lblFullName.Text = "Your full name will be displayed throughout the Your " + ConfigurationManager.AppSettings["ApplicationWord"] + " website. This includes when users view your profile and you add content to the website (post comments, upload photos, etc)";

            #endregion

            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                UserID = objSessionvalue.UserId;
                this._presenter.OnViewInitialized();
                btnUpload.Attributes.Add("onclick", "return ImageValidation()");
                btnDeleteCreditCardInformation.Attributes.Add("onclick", "return confirmSubmit();");
                //  btnSubmitEmailPassword.Attributes.Add("onclick","return ChangeEmailPassword()");

                this._presenter.OnCountryLoad(Locationid(null));
                this._presenter.BusinessTypes();

                SetControlText();
            }
            else
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));

            }
        }
        this._presenter.OnViewLoaded();
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.OnStateLoad(Locationid(ddlCountry.SelectedValue.ToString()));
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        SetUserImage();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this._presenter.SaveChanges(this, EventArgs.Empty);
    }


    protected void rdbHideMyLocation_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        try
        {
            _presenter.UpdatePrivacySettings();
            ShowMessage("Updated Successfully");
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message.ToString());
        }
    }

    protected void btnenSaveChanges_Click(object sender, EventArgs e)
    {
        this._presenter.SaveEmailNotification();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        this._presenter.OnChangeEmailPassword();
        ShowMessage(bannerMessage);
    }

    protected void ddlCountryExpiry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.OnStateLoad(Locationid(ddlCountryExpiry.SelectedValue.ToString()));
    }
    protected void btnDeleteCreditCardInformation_Click(object sender, EventArgs e)
    {
        if (bannerMessage != "Deleted")
        {
            pnlAddCreditInformation.Visible = true;
            btnDeleteCreditCardInformation.Visible = false;
            PanelCreditCardInfo.Visible = false;
            _presenter.DeleteCreditCardDetails();
            ShowMessage("Deleted successfully");
        }
        else
        {
            ShowMessage("No billing infornation for: " +UserID);
        }

    }
    protected void btnbiSaveChanges_Click(object sender, EventArgs e)
    {
        this._presenter.UpdateCCDetails();
    }

    #endregion Events

    #region Public methods
    [CreateNew]
    public UserProfilePresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    #endregion Public methods

    #region Private methods
    private void SetText()
    {
        lblEmail.Text = ResourceText.GetString("lblEmailTitle_CEP");
        lblEmailTitle.Text = ResourceText.GetString("lblEmail_CEP");
        lblPasswordTitle.Text = ResourceText.GetString("lblPasswordTitle_CEP");
        lblPassword.Text = ResourceText.GetString("lblPassword_CEP");
        lblConformPassword.Text = ResourceText.GetString("lblConformPassword_CEP");
        btnSubmitEmailPassword.Text = ResourceText.GetString("btnSubmit_CEP");
        revEmail.ErrorMessage = ResourceText.GetString("revEmail_CEP");
        cvConformPassword.ErrorMessage = ResourceText.GetString("cvConformPassword_CEP");
    }
    private void SetControlText()
    {
        /* Set CallOut Box Messages */
        CBCity.Text = ResourceText.GetString("txtCity_UR");
        CBCompanyName.Text = ResourceText.GetString("txtBusinessName_UR");
        CBFirstName.Text = ResourceText.GetString("txtFirstName_UR");
        CBLastName.Text = ResourceText.GetString("txtLastName_UR");
        CBStreetAddress.Text = ResourceText.GetString("txtStreetAddress_UR");
        CBWebsiteAddress.Text = ResourceText.GetString("txtWebsiteAddress_UR");
        CBZipCode.Text = ResourceText.GetString("txtZipCode_UR");

        /* Set Validator Messages */

        rfvFirstName.ErrorMessage = ResourceText.GetString("rfvFirstName_UR");
        rfvCity.ErrorMessage = ResourceText.GetString("rfvCity_UR");
        rfvZipCode.ErrorMessage = ResourceText.GetString("rfvZipCode_UR");
        revPhoneNumber.ErrorMessage = ResourceText.GetString("txtPhone_UP");



        ///* Set Control Text */
        btnSave.Text = ResourceText.GetString("btnSave_UP");

        /*Set Labels Text*/
        lblPhone.Text = ResourceText.GetString("lblPhone_UP");
        lblProfileInfo.Text = ResourceText.GetString("lblProfileInfo_UP");
        lblWebsiteAddress.Text = ResourceText.GetString("lblWebsiteAddress_UR");
        lblUsername.Text = ResourceText.GetString("lblUsername_UR");
        lblBusinessName.Text = ResourceText.GetString("lblBusinessName_UR");
        lblBusinessType.Text = ResourceText.GetString("lblBusinessType_UR");
        lblStreetAddress.Text = ResourceText.GetString("lblStreetAddress_UR");
        lblCountry.Text = ResourceText.GetString("lblCountry_UR");
        lblCity.Text = ResourceText.GetString("lblCity_UR");
        lblZipCode.Text = ResourceText.GetString("lblZipCode_UR");
        lblFirstName.Text = ResourceText.GetString("lblFirstName_UR");
        lblLastName.Text = ResourceText.GetString("lblLastName_UR");
        lblStateProvince.Text = ResourceText.GetString("lblStateProvince_UR");

    }
    private Locations Locationid(string ID)
    {
        Locations objLocations = new Locations();
        if (ID != null)
        { objLocations.LocationParentId = int.Parse(ID); }
        else
        { objLocations.LocationParentId = 0; }
        return objLocations;
    }
    private void SetPannelVisibility(bool val)
    {

        BusinessTypePanel.Visible = val;
        WebsitePanel.Visible = val;
        StreetAddressPanel.Visible = val;
        CompanyNamePanel.Visible = val;
        ZipCodePanel.Visible = val;
        FirstNamePanel.Visible = !val;
        LastNamePanel.Visible = !val;

    }
    // Writes file to current folder
    private void WriteToFile(string strPath, ref byte[] Buffer)
    {
        // Create a file
        FileStream newFile = new FileStream(strPath, FileMode.Create);

        // Write data to the file
        newFile.Write(Buffer, 0, Buffer.Length);

        // Close file
        newFile.Close();
    }
    private void SetUserImage()
    {
        // Check to see if file was uploaded
        if (filMyFile.PostedFile != null)
        {
            // Get a reference to PostedFile object
            HttpPostedFile myFile = filMyFile.PostedFile;

            // Get size of uploaded file
            int nFileLen = myFile.ContentLength;

            // make sure the size of the file is > 0
            if (nFileLen > 0)
            {
                // Allocate a buffer for reading of the file
                //byte[] myData = new byte[nFileLen];
                // Read uploaded file from the Stream
                // myFile.InputStream.Read(myData, 0, nFileLen);

                // Create a name for the file to store
                string strFilename = Path.GetFileName(myFile.FileName);
                imgUserImage.ImageUrl = strFilename;
                //imgUserImage.Visible = true;
            }
        }
    }
    private void SetAttributes()
    {
        foreach (GridViewRow row in gvBillingHistory.Rows)
        {
            //LinkButton lbtnDate = (LinkButton)row.FindControl("lbtnDate");
            //LinkButton lbtnTributeName = (LinkButton)row.FindControl("lbtnTributeName");
            //LinkButton lbtnPackage = (LinkButton)row.FindControl("lbtnPackage");
            //LinkButton lbtnAmount = (LinkButton)row.FindControl("lbtnAmount");
            //lbtnDate.Attributes.Add("onclick", "Summery();");
            //lbtnTributeName.Attributes.Add("onclick", "Summery();");
            //lbtnPackage.Attributes.Add("onclick", "Summery();");
            //lbtnAmount.Attributes.Add("onclick", "Summery();");
        }
    }

    #endregion Private methods  

    #region SetMultiview Index
    protected void btnChangeEmailPassword_Click(object sender, EventArgs e)
    {
        mvProfile.ActiveViewIndex = 2;
        SetText();
    }
    protected void btnProfileSettings_Click(object sender, EventArgs e)
    {
        mvProfile.ActiveViewIndex = 0;
        this._presenter.GetUserDetails(UserID);
        pnlAccount.Visible = true;
    }
    protected void btnPrivacySettings_Click(object sender, EventArgs e)
    {
        mvProfile.ActiveViewIndex = 1;

    }
    protected void btnEmailNotification_Click(object sender, EventArgs e)
    {
        mvProfile.ActiveViewIndex = 3;
        this._presenter.SetEmailNotification(UserId);
        if (bannerMessage != "")
        {
            ShowMessage(bannerMessage);
            PanelEmailNotification.Visible = false;
        }
    }
    protected void btnBillingInformation_Click(object sender, EventArgs e)
    {
        mvProfile.ActiveViewIndex = 4;
        this._presenter.OnBillingInformation(UserID);
        if (bannerMessage != null && bannerMessage != "Not Found")
        {
            pnlAddCreditInformation.Visible = !true;
            btnDeleteCreditCardInformation.Visible = !false;
            PanelCreditCardInfo.Visible = !false;
        }
        else
        {
            btnDeleteCreditCardInformation.Visible = false;
            lblCreditCardInformation.Visible = false;
            PanelCreditCardInfo.Visible = false;
            pnlAddCreditInformation.Visible = !false;
            ShowMessage("No billing infornation for: " + UserID);
        }


    }
    #endregion SetMultiview Index    
  
    #region Profile settings

    public System.Collections.Generic.IList<Locations> Locations
    {
        set
        {
            ddlCountry.DataSource = value;
            ddlCountry.DataTextField = "LocationName";
            ddlCountry.DataValueField = "LocationId";
            ddlCountry.DataBind();

            ddlCountryExpiry.DataSource = value;
            ddlCountryExpiry.DataTextField = "LocationName";
            ddlCountryExpiry.DataValueField = "LocationId";
            ddlCountryExpiry.DataBind();

        }
    }

    public System.Collections.Generic.IList<Locations> States
    {
        set
        {
            ddlStateProvince.DataSource = value;
            ddlStateProvince.DataTextField = "LocationName";
            ddlStateProvince.DataValueField = "LocationId";
            ddlStateProvince.DataBind();
            ddlStateExpiry.DataSource = value;
            ddlStateExpiry.DataTextField = "LocationName";
            ddlStateExpiry.DataValueField = "LocationId";
            ddlStateExpiry.DataBind();
        }
    }

    public System.Collections.Generic.IList<ParameterTypesCodes> Business
    {
        set
        {
            ddlBusinessType.DataSource = value;
            ddlBusinessType.DataTextField = ParameterTypesCodes.Parameters.TypeDescription.ToString();
            ddlBusinessType.DataValueField = ParameterTypesCodes.Parameters.TypeCode.ToString();
            ddlBusinessType.DataBind();
        }
    }

    public int UserId
    {
        get
        {
            return UserID;
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public string UserName
    {
        get
        {
            return txtUsername.Text;
        }
        set
        {
            txtUsername.Text = value;
        }
    }

    public string FirstName
    {
        get
        {
            return txtFirstName.Text;
        }
        set
        {
            txtFirstName.Text = value;
        }
    }

    public string LastName
    {
        get
        {
            return txtLastName.Text;
        }
        set
        {
            txtLastName.Text = value;
        }
    }

    public string UserImage
    {
        get
        {
            return null;
        }
        set
        {
            string ImageName = value;
        }
    }

    public string City
    {
        get
        {
            return txtCity.Text;
        }
        set
        {
            txtCity.Text = value;
            
        }
    }

    public Nullable<int> State
    {
        get
        {
            if (mvProfile.ActiveViewIndex == 4)
                return int.Parse(ddlStateExpiry.SelectedValue.ToString());
            else
                return int.Parse(ddlStateProvince.SelectedValue.ToString());
        }
        set
        {
            
            if (mvProfile.ActiveViewIndex == 4)
            ddlStateExpiry.SelectedIndex = ddlStateExpiry.Items.IndexOf(ddlStateExpiry.Items.FindByValue(value.ToString()));
            else
            ddlStateProvince.SelectedIndex = ddlStateProvince.Items.IndexOf(ddlStateProvince.Items.FindByValue(value.ToString()));
        }
    }

    public Nullable<int> Country
    {
        get
        {
            if (mvProfile.ActiveViewIndex == 4)
            {
                return int.Parse(ddlCountryExpiry.SelectedValue.ToString());
            }
            else
            {
                return int.Parse(ddlCountry.SelectedValue.ToString());
            }
        }
        set
        {
            if (mvProfile.ActiveViewIndex == 4)
            {
                ddlCountryExpiry.SelectedIndex = ddlCountryExpiry.Items.IndexOf(ddlCountryExpiry.Items.FindByValue(value.ToString()));
                this._presenter.OnStateLoad(Locationid(ddlCountryExpiry.SelectedValue.ToString()));
            }
            else
            {
                ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(value.ToString()));
                this._presenter.OnStateLoad(Locationid(ddlCountry.SelectedValue.ToString()));
            }
            
            
        }
    }

    public string Website
    {
        get
        {
            return txtWebsiteAddress.Text;
        }
        set
        {
            txtWebsiteAddress.Text = value;
        }
    }

    public string CompanyName
    {
        get
        {
            return txtBusinessName.Text;
        }
        set
        {
            txtBusinessName.Text = value;
        }
    }

    public int BusinessType
    {
        get
        {
            return int.Parse(ddlBusinessType.SelectedValue.ToString());
        }
        set
        {
            ddlBusinessType.SelectedIndex = ddlBusinessType.Items.IndexOf(ddlBusinessType.Items.FindByValue(value.ToString()));
        }
    }

    public string BusinessAddress
    {
        get
        {
            if (mvProfile.ActiveViewIndex == 4)            
                return txtBillingAddress.Text.ToString();            
            else            
                return txtStreetAddress.Text.ToString();            
        }
        set
        {
            if(mvProfile.ActiveViewIndex == 4)
                txtBillingAddress.Text = value;
            else
            txtStreetAddress.Text = value;
            

        }
    }

    public string Phone
    {
        get
        {
            return txtPhone.Text;
        }
        set
        {
            if (value.ToString() != "0")
            {
                txtPhone.Text = value;                
            }
        }
    }

    public string ZipCode
    {
        get
        {
            if (mvProfile.ActiveViewIndex == 4)            
                return txtPostalCodeExpiry.Text;
                else
            return txtZipCode.Text.ToString();
        }
        set
        {
            
            if (mvProfile.ActiveViewIndex == 4)
                txtPostalCodeExpiry.Text = value;
            else
               txtZipCode.Text = value;          

        }
    }

    #endregion

    #region Privacy Settings
    public bool IsUsernameVisiable
    {
        get
        {
            if (rdbDisplayMyUsername.Checked == true)
            {
                return rdbDisplayMyUsername.Checked;
            }
            else
            {
                return false;
            }
        }
        set
        {
            rdbDisplayMyUsername.Checked = value;
            rdbDisplayMyFullName.Checked = !value;
        }
    }

    public bool IsLocationHide
    {
        get
        {
            if (rdbDisplayMyLocation.Checked == true)
            {
                return rdbDisplayMyLocation.Checked;
            }
            else
            {
                return false;
            }
        }
        set
        {

            rdbDisplayMyLocation.Checked = value;
            rdbHideMyLocation.Checked = !value;

        }
    }

    public bool AllowIncomingMsg
    {
        get
        {
            if (rdbAllowUsers.Checked == true)
            {
                return rdbAllowUsers.Checked;
            }
            else
            {
                return false;
            }
        }
        set
        {
            rdbAllowUsers.Checked = value;
            rdbDoNotAllow.Checked = !value;
        }
    }
    #endregion Privacy Settings

    #region EmailNotifications


    public bool StoryNotify
    {
        get
        {
            return cbSTORY.Checked;
        }
        set
        {
            cbSTORY.Checked = value;
        }
    }

    public bool NotesNotify
    {
        get
        {
            return cbNOTES.Checked;
        }
        set
        {
            cbNOTES.Checked = value;
        }
    }

    public bool EventsNotify
    {
        get
        {
            return cbEVENTS.Checked;
        }
        set
        {
            cbEVENTS.Checked = value;
        }
    }

    public bool GuestBookNotify
    {
        get
        {
            return cbGUESTBOOK.Checked;
        }
        set
        {
            cbGUESTBOOK.Checked = value;
        }
    }

    public bool GiftsNotify
    {
        get
        {
            return cbGifts.Checked;
        }
        set
        {
            cbGifts.Checked = value;
        }
    }

    public bool PhotoAlbumNotify
    {
        get
        {
            return cbPHOTOALBUM.Checked;
        }
        set
        {
            cbPHOTOALBUM.Checked = value;
        }
    }

    public bool PhotosNotify
    {
        get
        {
            return cbPHOTOS.Checked;
        }
        set
        {
            cbPHOTOS.Checked = value;
        }
    }

    public bool VideosNotify
    {
        get
        {
            return cbVideo.Checked;
        }
        set
        {
            cbVideo.Checked = value;
        }
    }

    public bool CommentsNotify
    {
        get
        {
            return cbComments.Checked;
        }
        set
        {
            cbComments.Checked = value;
        }
    }

    public bool MessagesNotify
    {
        get
        {
            return cbMessages.Checked;
        }
        set
        {
            cbMessages.Checked = value;
        }
    }

    public bool NewsLetterNotify
    {
        get
        {
            return cbNewsLetter.Checked;
        }
        set
        {
            cbNewsLetter.Checked = value;
        }
    }

    #endregion

    #region ChangeEmailPassword


    public string Email
    {
        get { return txtEmail.Text.ToString(); }
    }

    public string Password
    {
        get { return txtPassword.Text.ToString(); }
    }



    #endregion

    #region Billing Information


    public IList<BillingHistory> BillingInformation
    {
        set
        {
            gvBillingHistory.DataSource = value;
            gvBillingHistory.DataBind();
            SetAttributes();
        }
    }

    public string CardNumber
    {
        get
        {
            return txtCardNumber.Text;
        }
        set
        {
            txtCardNumber.Text = value;
        }
    }

    public string CardName
    {
        get
        {
            return txtCardName.Text;
        }
        set
        {
            txtCardName.Text = value;
        }
    }

    public DateTime ExpiryDate
    {
        get
        {
            //string date=DateTime.Now.Date.ToString("dd")+"/"+ddlExpiryMonth.SelectedIndex.ToString()+"/"+txtExpiryYear.Text.ToString();
            DateTime _DateTime = DateTime.Parse(DateTime.Now.Date.ToString("dd") + "/" + ddlExpiryMonth.SelectedIndex.ToString() + "/" + txtExpiryYear.Text.ToString());
            return _DateTime;
        }
        set
        {
            DateTime _DateTime = value;
            txtExpiryYear.Text = _DateTime.Year.ToString();
            ddlExpiryMonth.SelectedIndex = ddlExpiryMonth.Items.IndexOf(ddlExpiryMonth.Items.FindByValue(_DateTime.Month.ToString()));

        }
    }

    public string Telephone
    {
        get
        {
            return txtTelephoneExpiry.Text;
        }
        set
        {
            txtTelephoneExpiry.Text = value;
        }
    }

    //BussinessUser Properties.

    public string HeaderBGColor
    {
        get
        {
            string strTemp = "";
            return strTemp;
        }
        set
        {
            string strTemp;
            strTemp = value;
        }
    }

    public bool IsAddressOn
    {
        get
        {
            bool bTemp = false;
            return bTemp;
        }
        set
        {
            bool bTemp;
            bTemp = value;
        }
    }

    public bool IsWebAddressOn
    {
        get
        {
            bool bTemp = false;
            return bTemp;
        }
        set
        {
            bool bTemp;
            bTemp = value;
        }
    }

    public bool IsObUrlLinkOn
    {
        get
        {
            bool bTemp = false;
            return bTemp;
        }
        set
        {
            bool bTemp;
            bTemp = value;
        }
    }

    public bool IsPhoneNoOn
    {
        get
        {
            bool bTemp = false;
            return bTemp;
        }
        set
        {
            bool bTemp;
            bTemp = value;
        }
    }

    public bool DisplayCustomHeader
    {
        get
        {
            bool bTemp = false;
            return bTemp;
        }
        set
        {
            bool bTemp;
            bTemp = value;
        }
    }

    public string HeaderLogo
    {

        get
        {
            string strTemp = "";
            return strTemp;
        }
        set
        {
            string strTemp;
            strTemp = value;
        }
    }
    public string ObituaryLinkPage
    {

        get
        {
            string strTemp = "";
            return strTemp;
        }
        set
        {
            string strTemp;
            strTemp = value;
        }
    }


    #endregion

    #region IUserProfile Members


    public bool PanelVisibility
    {
        get
        {
            return BusinessTypePanel.Visible;
        }
        set
        {

            SetPannelVisibility(value);

        }
    }
    public string BannerMessage
    {
        get
        {
            return bannerMessage;
        }
        set
        {
            bannerMessage = value;
        }
    }

    #endregion

    #region IUserProfile Members


    public IList<ParameterTypesCodes> PaymentModes
    {
        set
        {
            rdoPaymentMode.DataSource = value;
            rdoPaymentMode.DataTextField = ParameterTypesCodes.Parameters.TypeDescription.ToString();
            rdoPaymentMode.DataValueField = ParameterTypesCodes.Parameters.TypeCode.ToString();            
            rdoPaymentMode.DataBind();
        }
    }

    #endregion

    #region IUserProfile Members


    public string PaymentMethod
    {
        get
        {
            return rdoPaymentMode.SelectedItem.Text.ToString();
        }
        set
        {
            rdoPaymentMode.SelectedIndex = rdoPaymentMode.Items.IndexOf(rdoPaymentMode.Items.FindByText(value));
        }
    }

    #endregion

    protected void gvBillingHistory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = int.Parse(gvBillingHistory.SelectedRow.RowIndex.ToString());
        int ColumnCount = gvBillingHistory.Columns.Count;
        string[] Gridval = new string[ColumnCount - 1];        
        Label lbtnTributeid = (Label)gvBillingHistory.Rows[index].FindControl("lbtnTributeid");                    
        Session["Tributeid"]= lbtnTributeid.Text;

        //var sURL = "BillingSummary.aspx";
        // var xx = window.showModalDialog(sURL,"","dialogTop=10px; dialogHeight:200px;dialogWidth:200px;scroll:no;status:no;help:no;center:yes;resizable:no");
       // var xx = window.showModalDialog(sURL, "", "titlebar:no;dialogHide:0;dialogTop=100px;dialogHeight:310px;dialogWidth:500px;scroll:no;status:no;help:no;center:yes;resizable:no");
        string settings = "titlebar:no;dialogHide:0;dialogTop=100px;dialogHeight:500px;dialogWidth:500px;scroll:no;status:no;help:no;center:yes;resizable:no";
        string fontString = "window.showModalDialog('BillingSummary.aspx','','" + settings + "');";
        
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "", fontString, true);
    }


}


