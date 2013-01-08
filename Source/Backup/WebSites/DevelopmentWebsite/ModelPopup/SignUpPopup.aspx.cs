using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using TributesPortal.BusinessLogic;
using TributesPortal.Utilities;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.Security.Cryptography;
using Facebook;
using Facebook.Web;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using System.Collections.Generic;
using PerceptiveMCAPI.Types;
using PerceptiveMCAPI.Methods;
using PerceptiveMCAPI;

public partial class ModelPopup_SignUp : PageBase
{

    UserManager _usrmgr;
    UserInfoManager _uinfomgr;
    const string headertext = " <h2>Oops - there was a problem with your sign up.</h2>";
    protected string ApplicationType = ConfigurationManager.AppSettings["ApplicationType"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            lblSignUp.Text = "Sign up to Your Moments";

        }
        _usrmgr = new UserManager();
        _uinfomgr = new UserInfoManager();
        if (!IsPostBack)
        {
            onCountryLoad();
            Response.Cookies["ASP.NET_SessionId"].Value = Session.SessionID;
            Response.Cookies["ASP.NET_SessionId"].Domain = "." + WebConfig.TopLevelDomain + "";
            if (Request.QueryString["source"] != null)
                ClientScript.RegisterStartupScript(GetType(), "onload", "<script>ToggleElementDisplay('yt-ForgetUserNamePassword')</script>");

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

    protected void btnSignUp_Click(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsValid)
            {
                return;
            }

            if (txtPassword.Text.Length >= 4)
            {
                SignMe();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
        txtPassword.Attributes.Add("value", txtPassword.Text);
        txtConfirmPassword.Attributes.Add("value", txtPassword.Text);
    }
    /// <summary>
    /// function to load country names to dropdownlist
    /// </summary>
    public void onCountryLoad()
    {
        Locations loc = new Locations();
        loc.LocationParentId = 0;

        ddlCountry.DataSource = _usrmgr.Locations(loc);
        ddlCountry.DataTextField = "LocationName";
        ddlCountry.DataValueField = "LocationId";
        ddlCountry.DataBind();
        ddlCountry.SelectedValue = Convert.ToString(5);
    }
    /// <summary>
    /// function to sign in to database
    /// </summary>

    private void SignMe()
    {
        errorVerification.Visible = false;
        string ApplicationType = ConfigurationManager.AppSettings["ApplicationType"].ToString();
        int _email = _uinfomgr.EmailAvailable(txtEmail.Text, ApplicationType);
        if (_email == 0)
        {
            //Check for News Latters. 
            UserRegistration objUserReg = new UserRegistration();
            objUserReg = SaveAccount();

            /*System.Decimal identity = (System.Decimal)*/
            _uinfomgr.SavePersonalAccount(objUserReg);

            if (objUserReg.CustomError != null)
            {
                if (objUserReg.CustomError.ErrorMessage.Contains("Facebook"))
                {
                    objUserReg.CustomError.ErrorMessage = objUserReg.CustomError.ErrorMessage +
              " Please <a href=\"#\" onclick=\"fb_logout(); return false;\">" +
              "   <img id=\"fb_logout_image\" src=\"http://static.ak.fbcdn.net/images/fbconnect/logout-buttons/logout_small.gif\" alt=\"Connect\"/>" +
              "</a> and try again.";
                }
                messages.InnerHtml = ShowMessage(headertext, objUserReg.CustomError.ErrorMessage, 1);
                messages.Visible = true;
            }
            else
            {
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", Session.SessionID));
                Response.Cookies["ASP.NET_SessionId"].Domain = "." + WebConfig.TopLevelDomain;
                SetSessionValue(objUserReg);
                string location = Convert.ToString(Request.QueryString["location"]);
                if (chkAgreeReceiveNewsletters.Checked)
                {
                    //bool retval = AddSiteVisitor(objUserReg.Users.UserType);
                    bool retval = AddMailChimpSubscriber(objUserReg.Users.UserType);
                }
                if (Request.QueryString["title"] != null)
                {
                    string title = Request.QueryString["title"].ToString();
                    if (title.ToLower().Equals("sign up"))
                    {
                        string location_ = "../MyHome/AdminMytributesHome.aspx";
                        StringBuilder strb = new StringBuilder();
                        strb.Append("parent.setLocation(" + "'" + location_ + "'" + ");");
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", strb.ToString(), true);
                    }
                    else
                    {
                        StringBuilder Script = new StringBuilder();
                        if (string.IsNullOrEmpty(location))
                        {
                            location = "../MyHome/AdminMytributesHome.aspx";
                            Script.Append("parent.setLocation(" + "'" + location + "'" + ");");
                        }
                        else
                            Script.Append("chk(" + "'" + location + "'" + ");");
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", Script.ToString(), true);
                    }
                }
                else
                {
                    StringBuilder Script = new StringBuilder();
                    if (string.IsNullOrEmpty(location))
                    {
                        location = "../MyHome/AdminMytributesHome.aspx";
                        Script.Append("parent.setLocation(" + "'" + location + "'" + ");");
                    }
                    else
                        Script.Append("chk(" + "'" + location + "'" + ");");
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", Script.ToString(), true);
                }
            }
        }
        else
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("emailexist(" + "'" + _email + "'" + ");");
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", strb.ToString(), true);
            //messages.InnerHtml = ShowMessage(headertext, "User already exists for this email: " + txtEmail.Text, 1);//COMDIFFRES: is this message correct?
            //messages.Visible = true;
            //Page.RegisterStartupScript("myScript", "<script language=JavaScript>emailexists('');</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(),
            //        "alert", "emailexist("+_email+");", true);

            // btnSignUp.Attributes.Add("onclick", "return emailexist();");

            //ScriptManager.RegisterClientScriptBlock(Page, GetType(), "ShowPanel", strb.ToString(), true);
        }
    }
    /// <summary>
    ///  udham attri: function to set values for user object
    /// </summary>
    /// <returns>object userRegistration </returns>
    private UserRegistration SaveAccount()
    {

        int Usertype = 1;

        UserRegistration objUserReg = new UserRegistration();

        string _Pass = TributePortalSecurity.Security.EncryptSymmetric(txtPassword.Text.ToLower().ToString());
        string _UserImage = "images/bg_ProfilePhoto.gif";

        Nullable<Int64> _FacebookUid = null;
        var fbwebContext = FacebookWebContext.Current;
        if (FacebookWebContext.Current.Session != null)
        {
            _FacebookUid = fbwebContext.UserId;
            try
            {
                var fbwc = new FacebookWebClient(FacebookWebContext.Current.AccessToken);
                string fql = "Select pic_square from user where uid = " + fbwebContext.UserId;
                JsonArray me2 = (JsonArray)fbwc.Query(fql);
                var mm = (IDictionary<string, object>)me2[0];

                if (!string.IsNullOrEmpty((string)mm["pic_square"]))
                {
                    _UserImage = (string)mm["pic_square"]; // get user image
                }

            }
            catch (Exception ex)
            {
            }
        }

        TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users(
                                    txtEmail.Text.Trim(),
                                    _Pass,
                                    txtFirstName.Text.ToString(),
                                    txtLastName.Text.ToString(),
                                    txtEmail.Text.ToString(),
                                    "",
                                    chkAgreeReceiveNewsletters.Checked,
                                    "",
                                    null,
                                    int.Parse(ddlCountry.SelectedValue.ToString()),
                                    Usertype, _FacebookUid
                                      );
        objUsers.ApplicationType = ApplicationType;
        objUsers.UserImage = _UserImage;
        objUserReg.Users = objUsers;
        return objUserReg;
    }

    /// <summary>
    /// MaqilChimp Integartion
    /// </summary>
    /// <param name="UserType"></param>
    /// <returns></returns>
   private bool AddMailChimpSubscriber(int UserType)
    {
        bool returnVal = false;
        try
        {
            if (chkAgreeReceiveNewsletters.Checked == true)
            {
                listBatchSubscribeInput input = new listBatchSubscribeInput();
                input.api_AccessType = EnumValues.AccessType.Serial; // access
                input.api_OutputType = EnumValues.OutputType.XML; // output
                input.api_MethodType = PerceptiveMCAPI.EnumValues.MethodType.POST;// method
                input.api_Validate = false;// validate
                input.parms.double_optin = false;
                input.parms.replace_interests = true;
                input.parms.update_existing = true;

                input.parms.apikey = WebConfig.MailChimpApiKeyNew;
                input.parms.id = WebConfig.UserNewsLetterListID;

                // ------------------------------ address
                List<Dictionary<string, object>> batch = new List<Dictionary<string, object>>();
                Dictionary<string, object> entry = new Dictionary<string, object>();
                List<interestGroupings> groupings = new List<interestGroupings>();


                entry.Add("EMAIL", txtEmail.Text);
                batch.Add(entry);
                interestGroupings ig = new interestGroupings { name = "Account Status", groups = new List<string> { "New User Contributor" } };
                //if (UserType == 2)
                //{
                //    ig = new interestGroupings { name = "Account Status", groups = new List<string> { "New User Business" } };
                //}
                groupings.Add(ig);

                entry.Add("groupings", groupings);
                batch.Add(entry);
                input.parms.batch = batch;
                // execution
                listBatchSubscribe cmd = new listBatchSubscribe(input);
                listBatchSubscribeOutput output = cmd.Execute();
                //phase-1 enhancement


                if ((output != null) && (output.api_ErrorMessages.Count > 0))
                {
                    string ErrorCode = output.api_ErrorMessages.FirstOrDefault().code;
                    string Error = "Error occured. " + output.api_ErrorMessages.FirstOrDefault().error;

                    messages.InnerHtml = ShowMessage(headertext, ErrorCode + "</br>" + Error, 1);
                    messages.Visible = true;
                    returnVal = false;
                }
                else
                {
                    if (output.result.success_count > 0)
                    {
                        returnVal = true;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            messages.InnerHtml = ShowMessage(headertext, ex.Message.ToString(), 1);
            messages.Visible = true;
            returnVal = false;
        }
        return returnVal;
    }

    /// <summary>
    /// udham attri: to add visitor to site
    /// </summary>
    /// <param name="UserType"></param>
    /// <returns></returns>
    private bool AddSiteVisitor(int UserType)
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

            string postData = "loginName=yourtribute&loginPassword=yt4you&ea=" + txtEmail.Text + "&ic=" + ContactList; //+ "&first_name=" + objReg.Users.FirstName + "&last_name=" + objReg.Users.LastName;

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
            System.IO.Stream dataStream = objRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();


            // execute the request
            //HttpWebResponse response = (HttpWebResponse)objRequest.GetResponse();
            objRequest.GetResponse();

            //DJ Code
            string strVal = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.constantcontact.com/0.1/API_AddSiteVisitor.jsp");

            // Set some reasonable limits on resources used by this request
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;


            // Set credentials to use for this request.
            request.Credentials = new NetworkCredential(txtEmail.Text, txtPassword.Text);
            //request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Console.WriteLine("Content length is {0}", response.ContentLength);
            Console.WriteLine("Content type is {0}", response.ContentType);
            HttpStatusCode objstatus = response.StatusCode;
            strVal = objstatus.ToString();


            if (strVal == "OK")
                returnVal = true;
            else
                returnVal = false;

            //End DJ Code

            returnVal = true;
        }
        catch (Exception ex)
        {
            messages.InnerHtml = ShowMessage(headertext, ex.Message.ToString(), 1);
            messages.Visible = true;
            returnVal = false;
        }
        return returnVal;
    }
    /// <summary>
    /// function to set values to session :by udham attri
    /// </summary>
    /// <param name="objUserReg"></param>
    private void SetSessionValue(UserRegistration objUserReg)
    {
        SessionValue _objSessionValue = new SessionValue(objUserReg.Users.UserId,
                                                                             objUserReg.Users.UserName,
                                                                             objUserReg.Users.FirstName,
                                                                             objUserReg.Users.LastName,
                                                                             objUserReg.Users.Email,
                                                                             objUserReg.UserBusiness == null ? 1 : 2, "Basic", objUserReg.Users.IsUsernameVisiable
                                                                             );
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        stateManager.Add("objSessionvalue", _objSessionValue, TributesPortal.Utilities.StateManager.State.Session);


    }

}
