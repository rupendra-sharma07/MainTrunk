///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Myhome.AdminProfileSettings.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays and allows the user to modify his/her profile settings.
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
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.MyHome.Views;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using TributesPortal.Users;
using TributesPortal.BusinessEntities;
using System.IO;
using System.Collections.Generic;
using TributesPortal.Users.Views;
using Facebook;
using Facebook.Web;
using System.Drawing;
using System.Drawing.Imaging;//Lx:for Image resize


public partial class MyHome_AdminProfileSettings : PageBase, IAdminProfileSettings
{

    #region private variables
    private AdminProfileSettingsPresenter _presenter;
    int UserID;
    protected Nullable<Int64> _FacebookUid;
    string bannerMessage = string.Empty;
    protected string _userName;
    // private ConnectSession _connectSession;
    protected String returnCbValue;  // for facebook login callback
    #endregion private variables

    #region Public methods
    [CreateNew]
    public AdminProfileSettingsPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    #endregion Public methods
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Header.DataBind();
        // Code to implement SSL Functionality.
        //if (!WebConfig.ApplicationMode.Equals("local"))
        //if (Request.Url.ToString().Contains(@"http://"))
        //    Response.Redirect(@"https://www." + WebConfig.TopLevelDomain + @"/adminprofilesettings.aspx");
        this.Master.NavPrimary = Shared_InnerSecure.AdminNavPrimaryEnum.myprofile.ToString();
        this.Master.NavSecondary = Shared_InnerSecure.AdminNavSecondaryEnum.profile.ToString();
        this.Master.PageTitle = "Profile Settings";

        if (!this.IsPostBack)
        {
            this._presenter.GetImage();
            StateManager stateManagerP = StateManager.Instance;
            string PageName = "AdminProfileSettings";
            stateManagerP.Add(PortalEnums.SessionValueEnum.SearchPageName.ToString(), PageName, StateManager.State.Session);

            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (Request["id"] != null && Session["objSessionvalueAdmin"] != null)
            {

                //Get Useron UserId
                //_presenter.()
                OnAdminLogin();
                if (string.IsNullOrEmpty(this.UserName))
                {
                    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
                }
                objSessionvalue = null;
                objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
                if (objSessionvalue != null)
                {
                    OnLoggedIn(objSessionvalue);
                }
            }
            else if (objSessionvalue != null)
            {
                OnLoggedIn(objSessionvalue);
            }
            else
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }
        }
        else
        {
            //set _userName
            _userName = LoggedInUserName;
        }
        if (HeaderBGColor.Length > 0)
        {
            txtColorPicker.Attributes.Add("value", HeaderBGColor);
            txtColorPicker.Style.Add(HtmlTextWriterStyle.Color, HeaderBGColor);
        }
        else
        {
            txtColorPicker.Attributes.Add("value", WebConfig.DefaultColorPickerValue);
            txtColorPicker.Style.Add(HtmlTextWriterStyle.Color, WebConfig.DefaultColorPickerValue);
        }
        if (FacebookWebContext.Current.Session != null)
        {
            var fbwc = new FacebookWebClient(FacebookWebContext.Current.AccessToken);            
            try
            {            
                
                string fql = "Select pic_square from user where uid = " + FacebookWebContext.Current.UserId;
                JsonArray me2 = (JsonArray)fbwc.Query(fql);
                var mm = (IDictionary<string, object>)me2[0];

                if (!string.IsNullOrEmpty((string)mm["pic_square"]))
                {
                    ImgUserImage.Src = (string)mm["pic_square"]; // get user image
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    private void OnLoggedIn(SessionValue objSessionvalue)
    {
        setDefault();
        UserID = objSessionvalue.UserId;
        _userName = objSessionvalue.UserName;
        LoggedInUserName = objSessionvalue.UserName;
        this._presenter.OnCountryLoad(Locationid(null));
        this._presenter.BusinessTypes();
        this._presenter.GetUserDetails();
        //  btnSubmitEmailPassword.Attributes.Add("onclick","return ChangeEmailPassword()");
        //SetControlText();
        lbtnSaveChanges.Attributes.Add("onclick", "HideIndicator();");
    }

    /// <summary>
    /// When Admin Login to Change the User Settings
    /// </summary>
    public void OnAdminLogin()
    {

        GenralUserInfo _objGenralUserInfo = new GenralUserInfo();
        UserInfo objUserInfo = new UserInfo();
        UserRegistration _objUserReg = new UserRegistration();

        objUserInfo.UserID = Convert.ToInt32(Request["id"].ToString());

        this._presenter.GetUserCompleteDetail(objUserInfo.UserID);
    }

    protected void lbtnSaveChanges_Click(object sender, EventArgs e)
    {
        try
        {
            SetTributeImage();
            SetHeaderLogoImage();
            this._presenter.UpdateAccount();
            setmessage("<h2>Profile Settings Updated</h2><P>Your profile settings has been updated successfully.</P>", 2);
            if (HeaderBGColor.Length > 0)
            {
                txtColorPicker.Attributes.Add("value", HeaderBGColor);
                txtColorPicker.Style.Add(HtmlTextWriterStyle.Color, HeaderBGColor);
            }
            else
            {
                txtColorPicker.Attributes.Add("value", WebConfig.DefaultColorPickerValue);
                txtColorPicker.Style.Add(HtmlTextWriterStyle.Color, WebConfig.DefaultColorPickerValue);
            }
            StateManager objStateManager = StateManager.Instance;
            string[] virtualDir = CommonUtilities.GetPath();
            SessionValue objSessionValue = (SessionValue)objStateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionValue != null)
            {
                string strImageURL = UserImage.ToString();
                string newImgPath=string.Empty;
                if (strImageURL.Contains(virtualDir[9]))
                    newImgPath = UserImage.Replace(virtualDir[9], ""); //View.UserImage;
                else if (strImageURL.Contains(virtualDir[2]))
                    newImgPath = UserImage.Replace(virtualDir[2], ""); //View.UserImage;

                if (!(string.IsNullOrEmpty(newImgPath)))
                    objSessionValue.UserImage = newImgPath;
            }
            objStateManager.Add("objSessionvalue", objSessionValue, TributesPortal.Utilities.StateManager.State.Session);
        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem with profile settings.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + "</li></ul>", 1);

        }
    }

    // Added By Parul
    protected void repImage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType != ListItemType.Item) && (e.Item.ItemType != ListItemType.AlternatingItem))
        {
            return;
        }
        else
        {
            System.Web.UI.WebControls.Image tmpImageUrl = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgImageList");

            //((Image)e.Item.FindControl("imgImageList")).ImageUrl = tmpImageUrl.ImageUrl.Replace("http://","https://");

            if (tmpImageUrl.ImageUrl.ToString().ToLower().IndexOf("https") < 0)
            {
                if (WebConfig.ApplicationMode.Equals("local"))
                    ((System.Web.UI.WebControls.Image)e.Item.FindControl("imgImageList")).ImageUrl = tmpImageUrl.ImageUrl.Replace("https://","http://" );
                else
                ((System.Web.UI.WebControls.Image)e.Item.FindControl("imgImageList")).ImageUrl = tmpImageUrl.ImageUrl.Replace("http://", "https://");
            }
            else
            {
                ((System.Web.UI.WebControls.Image)e.Item.FindControl("imgImageList")).ImageUrl = tmpImageUrl.ImageUrl.ToString();
            }
            string function = string.Empty;
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                function = "SetImageURL('" + tmpImageUrl.ImageUrl.Replace("https://","http://" ) + "')";
            }
            else
            {
                function = "SetImageURL('" + tmpImageUrl.ImageUrl.Replace("http://", "https://") + "')";
            }
            tmpImageUrl.Attributes.Add("onclick", function);
            //tmpImageUrl.Checked = false;
        }
    }

    /// <summary>
    /// Set Tribute Image.
    /// </summary>
    private void SetTributeImage()
    {

        //Path where you want to upload the file.
        string[] eventPath = CommonUtilities.GetPath();
        string fileName = Path.GetFileName(hdnStoryImageURL.Value);
        string DefaultPath = eventPath[0] + "/" + eventPath[1] + "/" + _userName + "/" + eventPath[7];
        string srcPath = eventPath[0] + "/" + eventPath[1] + "/" + eventPath[6] + "/" + fileName;
        string _Tributeimage = _userName + "/" + eventPath[7] + "/" + fileName;

        string srcPath_ = Server.MapPath(fileName);

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
            //if (!WebConfig.ApplicationMode.Equals("local"))
            //{                
            //    ImgUserImage.Src = eventPath[9].ToLower().Replace("http", "https") +_Tributeimage;
            //}
            ImgUserImage.Src = eventPath[9] + _Tributeimage;
        }
        else
        {
            //File.Copy(srcPath_, Path.Combine(DefaultPath, fileName));
            if (hdnStoryImageURL.Value != "")
                ImgUserImage.Src = hdnStoryImageURL.Value;
            else
                ImgUserImage.Src = ImgUserImage.Src;

        }
        //if (!WebConfig.ApplicationMode.Equals("local"))
        //{
        //    ImgUserImage.Src = ImgUserImage.Src.ToLower().Replace("http", "https");
        //}
        if (!WebConfig.ApplicationMode.Equals("local"))
        {
            // ss in https(httpss) Issue: Mohit Gupta 16-July
            if (ImgUserImage.Src.ToLower().IndexOf("https") < 0)
            {
                ImgUserImage.Src = ImgUserImage.Src.ToLower().Replace("http", "https");
            }
        }
    }

    //LHK: for HeaderLogo Upload Nov 2010.
    private void SetHeaderLogoImage()
    {

        //Path where you want to upload the file.
        string[] eventPath = CommonUtilities.GetPath();
        string fileName = FileUploadHeaderLogo.FileName.ToString();
        string srcPath = eventPath[0] + "/" + eventPath[1] + "/" + eventPath[6] + "/" + fileName;
        string srcPath_ = Server.MapPath(fileName);
        System.Drawing.Bitmap bmpCompress = null;

        if (FileUploadHeaderLogo.HasFile)
        {

            // Destination Path = Drive + TributePhotos Folder + TributeURL_TributeType Folder + Story Folder
            string DefaultPath = eventPath[0] + "/" + eventPath[1] + "/" + _userName + "/" + eventPath[10] + "/";
            string filePath = FileUploadHeaderLogo.PostedFile.FileName;
            srcPath = Path.GetFullPath(filePath);

            //LHK :Directory search
            DirectoryInfo dirObj = new DirectoryInfo(DefaultPath);
            if (!dirObj.Exists)
            {
                dirObj.Create();
            }
            // LHK: File copy
            string extension = (System.IO.Path.GetExtension(FileUploadHeaderLogo.PostedFile.FileName)).ToLower();

            if (!(extension != ".jpg" && extension != ".gif"))
            {
                //Even if new file is with same name it will saved with an appended nuber in filename.
                if (File.Exists(Path.Combine(DefaultPath, fileName)))
                {
                    File.Delete(Path.Combine(DefaultPath, fileName));
                }
                //FileUploadHeaderLogo.SaveAs(DefaultPath + fileName);

                //Lx:HeaderLogo Resize
                System.IO.MemoryStream img = new System.IO.MemoryStream(FileUploadHeaderLogo.FileBytes);
                Bitmap oImage;
                oImage = new Bitmap(img);
                bmpCompress = new Bitmap(oImage, 250, 100);

                //bmpCompress.Save(DefaultPath + "/" + fileName + ".jpeg", ImageFormat.Jpeg);
                bmpCompress.Save(DefaultPath + "/" + fileName);
                bmpCompress.Dispose();
                //File.Copy(srcPath, Path.Combine(DefaultPath, fileName));
                hdnHeaderLogo.Value = _userName + "/" + eventPath[10] + "/" + fileName;
            }
        }
    }

    private void SetText()
    {
        if (txtPhoneNumber1.Visible == true)
            lblCity.Text = "<label><em class='required'>* </em>" + ResourceText.GetString("lblCity_UR") + "</label>";
        else
            lblCity.Text = "<label>" + ResourceText.GetString("lblCity_UR") + "</label>";
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
        PanelPhone.Visible = val;
        rfvtxtCity.Visible = val;

        //LHK: for custom header check box visibility for normal user.
        ChkBoxIsAddressOn.Visible = val;
        ChkBoxIsWebAddressOn.Visible = val;
        ChkBoxIsPhoneNoOn.Visible = val;
        ObPagePanel.Visible = val;
        divCustomHeader.Visible = val;

        SetText();
    }
    #region Profile settings


    public IList<GiftImage> ImageList
    {
        set
        {
            repImage.DataSource = value;
            repImage.DataBind();
        }
    }

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


    public System.Collections.Generic.IList<Locations> Locations
    {
        set
        {
            ddlCountry.DataSource = value;
            ddlCountry.DataTextField = "LocationName";
            ddlCountry.DataValueField = "LocationId";
            ddlCountry.DataBind();

            // ddlCountryExpiry.DataSource = value;
            //  ddlCountryExpiry.DataTextField = "LocationName";
            //  ddlCountryExpiry.DataValueField = "LocationId";
            //  ddlCountryExpiry.DataBind();

        }
    }

    public System.Collections.Generic.IList<Locations> States
    {
        set
        {
            if (value.Count > 0)
            {
                ddlStateProvince.DataSource = value;
                ddlStateProvince.DataTextField = "LocationName";
                ddlStateProvince.DataValueField = "LocationId";
                ddlStateProvince.DataBind();
                ddlStateProvince.Enabled = true;
            }
            else
            {
                ddlStateProvince.Enabled = false;
            }

            //ddlStateExpiry.DataSource = value;
            //ddlStateExpiry.DataTextField = "LocationName";
            //ddlStateExpiry.DataValueField = "LocationId";
            //ddlStateExpiry.DataBind();
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

    public Nullable<Int64> FacebookUid
    {
        get { return _FacebookUid; }
        set { _FacebookUid = value; }
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
            return ImgUserImage.Src;
        }
        set
        {
            ImgUserImage.Src = value;

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
            int state = -1;
            if (ddlStateProvince.SelectedIndex != -1)
            {
                state = int.Parse(ddlStateProvince.SelectedValue.ToString());
            }
            return state;
        }
        set
        {

            ddlStateProvince.SelectedIndex = ddlStateProvince.Items.IndexOf(ddlStateProvince.Items.FindByValue(value.ToString()));
        }
    }

    public Nullable<int> Country
    {
        get
        {

            return int.Parse(ddlCountry.SelectedValue.ToString());

        }
        set
        {

            ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(value.ToString()));
            this._presenter.OnStateLoad(Locationid(ddlCountry.SelectedValue.ToString()));



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

            return txtStreetAddress.Text.ToString();
        }
        set
        {
            txtStreetAddress.Text = value;
        }
    }

    public string Phone
    {
        get
        {
            return txtPhoneNumber1.Text + txtPhoneNumber2.Text + txtPhoneNumber3.Text;
        }
        set
        {
            if (value != null)
            {
                if (value.Length > 0)
                {
                    if (value.Length > 2)
                        txtPhoneNumber1.Text = value.Substring(0, 3);
                    if (value.Length > 5)
                        txtPhoneNumber2.Text = value.Substring(3, 3);
                    if (value.Length > 9)
                        txtPhoneNumber3.Text = value.Substring(6, 4);
                }
            }
        }
    }

    public string HeaderBGColor
    {
        get
        {

            return colorPickerSpan.Text.ToString();
        }
        set
        {
            colorPickerSpan.Text = value;
        }
    }

    public bool IsAddressOn
    {
        get
        {
            return ChkBoxIsAddressOn.Checked;
        }
        set
        {
            ChkBoxIsAddressOn.Checked = value;
        }
    }

    public bool IsWebAddressOn
    {
        get
        {
            return ChkBoxIsWebAddressOn.Checked;
        }
        set
        {
            ChkBoxIsWebAddressOn.Checked = value;
        }
    }

    public bool IsObUrlLinkOn
    {
        get
        {
            return ChkBoxIsObUrlLinkOn.Checked;
        }
        set
        {
            ChkBoxIsObUrlLinkOn.Checked = value;
        }
    }

    public bool IsPhoneNoOn
    {
        get
        {
            return ChkBoxIsPhoneNoOn.Checked;
        }
        set
        {
            ChkBoxIsPhoneNoOn.Checked = value;
        }
    }

    public bool DisplayCustomHeader
    {
        get
        {
            return ChkBoxDisplayCustomHeader.Checked;
        }
        set
        {
            ChkBoxDisplayCustomHeader.Checked = value;
        }
    }

    public string HeaderLogo
    {

        get
        {
            return hdnHeaderLogo.Value.ToString();
        }
        set
        {
            //FileUploadControl.FileName = value;
            hdnHeaderLogo.Value = value;
        }
    }

    public string ObituaryLinkPage
    {

        get
        {
            return txtObPage.Text.ToString();
        }
        set
        {
            txtObPage.Text = value;
        }
    }
    public string ZipCode
    {
        get
        {

            return txtZipCode.Text.ToString();
        }
        set
        {
            txtZipCode.Text = value;
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

    #endregion
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._presenter.OnStateLoad(Locationid(ddlCountry.SelectedValue.ToString()));
    }

    #region IAdminProfileSettings Members


    public bool IsUsernameVisiable
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public bool AllowIncomingMsg
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public bool IsLocationHide
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public bool StoryNotify
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public bool NotesNotify
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public bool EventsNotify
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public bool GuestBookNotify
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public bool GiftsNotify
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public bool PhotoAlbumNotify
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public bool PhotosNotify
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public bool VideosNotify
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public bool CommentsNotify
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public bool MessagesNotify
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public bool NewsLetterNotify
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }



    #endregion

}
