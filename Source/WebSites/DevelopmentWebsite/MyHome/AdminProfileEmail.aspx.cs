///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Myhome.AdminProfileEmail.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to modify the email settings as applicable to his/her account.
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
using TributesPortal.MyHome.Views;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net;

public partial class MyHome_AdminProfileEmail : PageBase, IAdminProfileEmail
{

    #region private variables
    private AdminProfileEmailPresenter _presenter;
    int UserID;
    string bannerMessage = string.Empty;
    protected string _userName;
    #endregion private variables

    #region Public methods
    [CreateNew]
    public AdminProfileEmailPresenter Presenter
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
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            cbNewsLetter.Text = "NEWSLETTERS – Email me newsletters periodically from Your Moments with advice and promotions.";
        }
        // Code to implement SSL Functionality.
        //if (!WebConfig.ApplicationMode.Equals("local"))
        //if (Request.Url.ToString().Contains(@"http://"))
        //    Response.Redirect(@"https://www." + WebConfig.TopLevelDomain + @"/adminprofileemail.aspx");
        this.Master.NavPrimary = Shared_InnerSecure.AdminNavPrimaryEnum.myprofile.ToString();
        this.Master.NavSecondary = Shared_InnerSecure.AdminNavSecondaryEnum.emailnotifications.ToString();
        this.Master.PageTitle = "Email Notifications";

        if (!this.IsPostBack)
        {
            //lnkAdvanceSearch.NavigateUrl = Session["APP_BASE_DOMAIN"] + "advancedsearch.aspx";
            StateManager stateManagerP = StateManager.Instance;
            string PageName = "AdminProfileEmail";
            stateManagerP.Add(PortalEnums.SessionValueEnum.SearchPageName.ToString(), PageName, StateManager.State.Session);

            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                UserID = objSessionvalue.UserId;
                LoggedInUserName = objSessionvalue.UserName;
                this._presenter.SetEmailNotification();
                btnenSaveChanges.Attributes.Add("onclick", "HideIndicator();");
            }
            else
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }
        }

        setDefault();
    }

    protected void btnenSaveChanges_Click(object sender, EventArgs e)
    {
        try
        {
            this._presenter.SaveEmailNotification();
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                int UserType = objSessionvalue.UserType;
                string email = objSessionvalue.UserEmail;
                if (!cbNewsLetter.Checked)
                    RemoveSiteVisitor(UserType, email);
                else
                    AddSiteVisitor(UserType, email);
            }           
            setmessage("<h2>Email notifications updated</h2><P>Your email notifications are updated successfully.</P>", 2);
        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem in email alert updation.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + "</li></ul>", 1);
        }
    }

    private void AddSiteVisitor(int UserType, string email)
    {
        bool returnVal = false;
        try
        {
            StringBuilder sbSiteVisitor = new StringBuilder();
            sbSiteVisitor.Append("http://api.constantcontact.com/0.1/API_AddSiteVisitor.jsp");

            string ContactList = "";
            if (UserType == 1)
                ContactList = "Your Tribute Newsletter";
            else
                ContactList = "Your Tribute Business Newsletter";

            string postData = "loginName=yourtribute&loginPassword=yt4you&ea=" + email + "&ic=" + ContactList; //+ "&first_name=" + objReg.Users.FirstName + "&last_name=" + objReg.Users.LastName;

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byteArray = encoding.GetBytes(postData);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sbSiteVisitor.ToString());

            Uri objUri = new Uri(sbSiteVisitor.ToString());
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create("http://api.constantcontact.com/0.1/API_AddSiteVisitor.jsp");
            objRequest.Method = "POST";
            //objRequest.KeepAlive = false;
            objRequest.ContentLength = byteArray.Length;
            //objRequest.ProtocolVersion = HttpVersion.Version11;
            objRequest.ContentType = "application/x-www-form-urlencoded";
            //objRequest.Proxy = System.Net.WebProxy.GetDefaultProxy();
            Stream dataStream = objRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            // execute the request
            //HttpWebResponse response = (HttpWebResponse)objRequest.GetResponse();
            objRequest.GetResponse();
            returnVal = true;
        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem in email alert updation.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + "</li></ul>", 1);           
        }
    }

    private void RemoveSiteVisitor(int UserType, string email)
    {
        bool returnVal = false;
        try
        {
            StringBuilder sbSiteVisitor = new StringBuilder();
            sbSiteVisitor.Append("http://api.constantcontact.com/0.1/API_UnsubscribeSiteVisitor.jsp");


            string ContactList = "";
            if (UserType == 1)
                ContactList = "Your Tribute Newsletter";
            else
                ContactList = "Your Tribute Business Newsletter";

            string postData = "loginName=yourtribute" + "&loginPassword=yt4you" + "&ea=" + email + "&ic=" + ContactList; //+ "&first_name=" + objReg.Users.FirstName + "&last_name=" + objReg.Users.LastName;

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byteArray = encoding.GetBytes(postData);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sbSiteVisitor.ToString());

            Uri objUri = new Uri(sbSiteVisitor.ToString());
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create("http://api.constantcontact.com/0.1/API_UnsubscribeSiteVisitor.jsp");
            objRequest.Method = "POST";
            //objRequest.KeepAlive = false;
            objRequest.ContentLength = byteArray.Length;
            //objRequest.ProtocolVersion = HttpVersion.Version11;
            objRequest.ContentType = "application/x-www-form-urlencoded";
            //objRequest.Proxy = System.Net.WebProxy.GetDefaultProxy();
            Stream dataStream = objRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            // execute the request
            //HttpWebResponse response = (HttpWebResponse)objRequest.GetResponse();
            objRequest.GetResponse();
            returnVal = true;
        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem in email alert updation.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + "</li></ul>", 1);           
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
}
