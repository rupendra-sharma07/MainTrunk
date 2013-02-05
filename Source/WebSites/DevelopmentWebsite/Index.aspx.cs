///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.ParentHomepage.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the Parent Home page
///Audit Trail     : Date of Modification  Modified By         Description

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
//using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Tribute.Views;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TributesPortal.MultipleLangSupport;
using System.Web.SessionState;
using Facebook;
//using Facebook.Rest;
//using Facebook.Web;
//using Facebook.Session;

public partial class Tribute_RootHomepage :PageBase//System.Web.UI.Page
{
    public string LinkOtherDomain = string.Empty;
    private SessionValue objSessionValue = null;

    protected void Page_Load(object sender, EventArgs e)
    {       
        string strHostDomain = "";

        string subDomainName = string.Empty;

        strHostDomain = Request.ServerVariables["SERVER_NAME"];
        string[] urlArr = strHostDomain.Split(".".ToCharArray());
       
        //code for YT Mobile redirections
        string redirctMobileUrl = string.Empty;
        if (!IsPostBack)
        {
            DeviceManager deviceManager = new DeviceManager
            {
                UserAgent = Request.UserAgent,
                IsMobileBrowser = Request.Browser.IsMobileDevice
            };

            // Added by Varun Goel on 25 Jan 2013 for NoRedirection functionality
            TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;

            objSessionValue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionValue == null || objSessionValue.NoRedirection == null || objSessionValue.NoRedirection == false)
            {
                if (deviceManager.IsMobileDevice())
                {
                    // Redirection URL
                    redirctMobileUrl = string.Format("{0}{1}{2}", "https://www.", WebConfig.TopLevelDomain, "/mobile/Search.html");
                    Response.Redirect(redirctMobileUrl, false);
                }
            }
        }

        if (urlArr.Length > 2)
        {
            if (urlArr.Length == 4)
                subDomainName = urlArr[1];
            else if (urlArr.Length == 3)
                subDomainName = urlArr[0];

            if (subDomainName != "www" && subDomainName != "your-tribute-dev")
            {
                //Added condition to avoid redirection in case of tribute domain sites
                if (subDomainName != "memorial" && subDomainName != "newbaby" && subDomainName != "wedding" && subDomainName != "anniversary" && subDomainName != "graduation" && subDomainName != "birthday" && subDomainName != "video")
                {
                    Response.Redirect("businessuserhome.aspx?username=" + subDomainName);
                    //Server.Transfer("myhome/businessuserhome.aspx?username=" + subDomainName);
                }
            }
        }
        //this.Form.Action = Request.RawUrl;

        // Added by Ashu on Oct 11, 2011
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            YT2.Attributes["class"] = "blue";
            YT4.Attributes["class"] = "blue";
           // YT5.Attributes["class"] = "";
            YMDiv.Attributes.Add("style", "display:block");
            YTDiv.Attributes.Add("style", "display:none");
            CreateBtn.Attributes["class"] = "actionbuttonBlue";
            YMSlider.Attributes.Add("style", "display:block");
            YTSlider.Attributes.Add("style", "display:none");
            Bottombackground.Attributes.Add("style", "display:block;");
            YTAnnouncement.Attributes.Add("style", "display:none;");
            YT11.InnerHtml = @"Create your own Website for free!";
            HomeTitle.InnerHtml= @"Your Moments Event Websites – Celebrate a wedding, baby, anniversary or other significant
        event.";
            if (TributesPortal.Utilities.WebConfig.TopLevelDomain.ToLower().Contains(".in"))
                LinkOtherDomain = "http://www.yourtribute.in";
            else
                LinkOtherDomain = "http://www.yourtribute.com";

        }
        else if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourtribute")
        {
            HomeTitle.InnerHtml = @"Your Tribute - Free Online Obituaries & Premium Memorial Websites";
            YT2.Attributes["class"] = "Purple-MT";
            YT4.Attributes["class"] = "Gray-MT";
           // YT5.Attributes["class"] = "Gray-MT";
            bButtons.Attributes["class"] = "bigButtons-MT";
            YMDiv.Attributes.Add("style", "display:none");
            YTDiv.Attributes.Add("style", "display:block");
            CreateBtn.Attributes["class"] = "actionbuttonPurple";
            YMSlider.Attributes.Add("style", "display:none");
            YTSlider.Attributes.Add("style", "display:block");
            Bottombackground.Attributes.Add("style", "display:none;");
            YTAnnouncement.Attributes.Add("style", "display:block;");
            if (TributesPortal.Utilities.WebConfig.TopLevelDomain.ToLower().Contains(".in"))
                LinkOtherDomain = "http://www.yourtribute.in";
            else
                LinkOtherDomain = "http://www.yourtribute.com";
        }


//        Session["Site"] = "Moment";
//        if (Session["Site"] != null)
//        {
//            if (Session["Site"].ToString() == "Moment")
//            {
//                YTTile.InnerHtml = @"Your Moments Event Websites – Celebrate a wedding, baby or other significant
//                                   event.";
//                YT1.InnerHtml = "Your Moments";
//                YT2.InnerHtml = "What is Your Moment?";
//                YT3.InnerHtml=@" A web-based tool, that lets you set up a personal website to plan, share
//                                and remember a significant event or special someone. A Website can be created in
//                                minutes, but remains online for life to provide an everlasting record of the special
//                                occasion.";
//                YT4.InnerHtml = "Why Your Moments?";
//                YT5.InnerHtml= @"It is easy and elegant. Create a personalized Website for your event in minutes.
//                                Your Moments includes many of the features of popular online invitation, photo sharing,blogging,
//                                and social networking websites, in an easy-to-use intuitive interface.";
//                YT6.InnerHtml = @"Choose from our collection of themes created by our top designers. Multiple themes
//                                are available for each website type with more added all the time!";
//                YT7.InnerHtml = @" One-step login using your Facebook account. Invite Facebook friends to your website
//                                and events and easily publish to your wall in one click.";
//                YT8.InnerHtml = @"We don’t think you should have to keep paying to keep your important event online.
//                                Your Moments and all of its content will remain online for life, we guarantee it!";
//                YT9.InnerHtml = @"Celebrate a significant event or a special someone with Your Moments.";
//                YT10.InnerHtml = @"Website";
//                YT11.InnerHtml = @"Create your own Website for free!";
//                YT12.InnerHtml = "Your Moments";
//                YT13.InnerHtml = @" Create a personal website  for your significant event in minutes. Send
//                                    stylish online invitations with RSVP. Add photos and videos and let your friends
//                                    and family do the same. Receive personal messages in your guestbook as well as virtual
//                                    gifts. Plus many more easy-to-use features!";
//                YT14.InnerHtml = @"Create a personal website for your new baby in minutes. Send beautiful
//                                                online baby anouncements. Share stories and add photos and videos from before and
//                                                after the birth. Receive personal messages in your guestbook. Plus much more!";
//                YT15.InnerHtml = "New Baby Website:";
//                YT16.InnerHtml = @" Create a personal website for your graduation in minutes. Send beautiful
//                                                grad invites. Add photos from before and after the event. Receive personal messages
//                                                in your guestbook and virtual gifts. Plus many more easy-to-use features!";
//                YT17.InnerHtml = "Graduation Website:";
//                YT18.InnerHtml = @" Create a personal website for your wedding in minutes. Send beautiful
//                                                online invitations with RSVP. Add photos from before and after the wedding and let
//                                                your guests do the same. Receive personal messages in your guestbook. Plus much
//                                                more!";
//                YT19.InnerHtml = "Wedding Website:";
//                YT20.InnerHtml=@" Create a personal website for your birthday in minutes. Send stylish
//                                    online birthday invitations with RSVP. Add photos from before and after the event
//                                    and let your friends do the same. Receive messages in your guestbook, virtual gifts,
//                                    and more!";
//                YT21.InnerHtml = "Birthday Tribute:";
//                YT22.InnerHtml = @"Create a personal website for your loved one in minutes. Share their
//                                                story. Send stylish online thank you cards. Add photos and videos and let friends
//                                                and family do the same. Receive personal messages in the guestbook and much more!";
//                YT23.InnerHtml = " Memorial Website:";
//                YT24.InnerHtml = @" Create a personal website (a Tribute) for your anniversary in minutes. Send stylish
//                                                online invitations. Add photos and videos from before and after the event. Receive
//                                                personal messages in the guestbook and virtual gifts. Plus many more easy-to-use
//                                                features!";
//                YT25.InnerHtml = " Anniversary Tribute:";

//                lnkCreateBtn.HRef = Session["APP_BASE_DOMAIN"].ToString() + "pricing.aspx";
//                lnkCreateBtn.Attributes["class"] = "MomentleftBigButton";
               
//            }
//            else
//            {
//                YTTile.InnerHtml = @"Your Tribute Event Websites – Celebrate a wedding, baby, memorial or other significant
//                                     event.";
//                YT1.InnerHtml = "Your Tribute";
//                YT2.InnerHtml = "What is Your Tribute?";
//                YT3.InnerHtml = @" A web-based tool, that lets you set up a personal website (a Tribute) to plan, share
//                            and remember a significant event or special someone. A Tribute can be created in
//                            minutes, but remains online for life to provide an everlasting record of the special
//                            occasion.";
//                YT4.InnerHtml = "Why Your Tribute?";
//                YT5.InnerHtml = @"It is easy and elegant. Create a personalized Tribute for your event in minutes.
//                            Your Tribute includes many of the features of popular online invitation, photo sharing,
//                            blogging, and social networking websites, in an easy-to-use intuitive interface.";
//                YT6.InnerHtml=@"Choose from our collection of themes created by our top designers. Multiple themes
//                                are available for each tribute type with more added all the time!";
//                YT7.InnerHtml = @" One-step login using your Facebook account. Invite Facebook friends to your tribute
//                                and events and easily publish to your wall in one click.";
//                YT8.InnerHtml = @"We don’t think you should have to keep paying to keep your important event online.
//                                Your Tribute and all of its content will remain online for life, we guarantee it!";
//                YT9.InnerHtml = @"Celebrate a significant event or a special someone with Your Tribute.";
//                YT10.InnerHtml = "Tributes";
//                YT11.InnerHtml = @"Create your own Tribute for free!";
//                YT12.InnerHtml = "Your Tribute";
//                YT13.InnerHtml = @" Create a personal website (a Tribute) for your significant event in minutes. Send
//                                    stylish online invitations with RSVP. Add photos and videos and let your friends
//                                    and family do the same. Receive personal messages in your guestbook as well as virtual
//                                    gifts. Plus many more easy-to-use features!";
//                YT14.InnerHtml = @"Create a personal website (a Tribute) for your new baby in minutes. Send beautiful
//                                   online baby anouncements. Share stories and add photos and videos from before and
//                                   after the birth. Receive personal messages in your guestbook. Plus much more!";
//                YT15.InnerHtml="New Baby Tribute:";
//                YT16.InnerHtml = @" Create a personal website (a Tribute) for your graduation in minutes. Send beautiful
//                                    grad invites. Add photos from before and after the event. Receive personal messages
//                                    in your guestbook and virtual gifts. Plus many more easy-to-use features!";
//                YT17.InnerHtml = "Graduation Tribute:";
//                YT18.InnerHtml = @" Create a personal website (a Tribute) for your wedding in minutes. Send beautiful
//                                    online invitations with RSVP. Add photos from before and after the wedding and let
//                                    your guests do the same. Receive personal messages in your guestbook. Plus much
//                                    more!";
//                YT19.InnerHtml = "Wedding Tribute:";
//                YT20.InnerHtml = @" Create a personal website (a Tribute) for your birthday in minutes. Send stylish
//                                    online birthday invitations with RSVP. Add photos from before and after the event
//                                    and let your friends do the same. Receive messages in your guestbook, virtual gifts,
//                                    and more!";
//                YT21.InnerHtml = "Birthday Tribute:";
//                YT22.InnerHtml = @"Create a personal website (a Tribute) for your loved one in minutes. Share their
//                                    story. Send stylish online thank you cards. Add photos and videos and let friends
//                                    and family do the same. Receive personal messages in the guestbook and much more!";
//                YT23.InnerHtml = " Memorial Tribute:";
//                YT24.InnerHtml = @" Create a personal website (a Tribute) for your anniversary in minutes. Send stylish
//                                                online invitations. Add photos and videos from before and after the event. Receive
//                                                personal messages in the guestbook and virtual gifts. Plus many more easy-to-use
//                                                features!";
//                YT25.InnerHtml = " Anniversary Tribute:";

//                lnkCreateBtn.HRef= Session["APP_BASE_DOMAIN"].ToString() + "pricing.aspx";
//                lnkCreateBtn.Attributes["class"]="leftBigButton";
//            }
//        }
    }
    
    
    //protected String _userName = string.Empty;
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    ConnectSession sess = new ConnectSession(
    //        ConfigurationManager.AppSettings["APIKey"], 
    //        ConfigurationManager.AppSettings["Secret"]);
    //    Facebook.Schema.user user;

    //    if (!this.IsPostBack)
    //    {
    //        //Response.Cookies["ASP.NET_SessionId"].Value = Session.SessionID;
    //        //Response.Cookies["ASP.NET_SessionId"].Domain = "." + WebConfig.TopLevelDomain + "";
    //        StateManager stateManager = StateManager.Instance;
    //        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
    //        if (!Equals(objSessionvalue, null))
    //        {
    //            if (sess.IsConnected())
    //            {
    //                try
    //                {
    //                    Facebook.Rest.Api api = new Facebook.Rest.Api(sess);                        //Display user data captured from the Facebook API.  
    //                    user = api.Users.GetInfo();

    //                    if (objSessionvalue.UserName == null ||
    //                        string.Empty.Equals(objSessionvalue.UserName))
    //                    {
    //                        objSessionvalue.UserName = user.first_name + " " + user.last_name;
    //                    }
    //                    //objSessionvalue.UserName = "fb:name uid='loggedinuser' useyou='false'></fb:name>";
    //                    spanLogout.InnerHtml = "<a href=\"#\" onclick=\"FB.Connect.logoutAndRedirect('" + Session["APP_BASE_DOMAIN"] + "Logout.aspx')\">" +
    //                      "   <img id=\"fb_logout_image\" src=\"http://static.ak.fbcdn.net/images/fbconnect/logout-buttons/logout_small.gif\" alt=\"Connect\"/>" +
    //                      "</a>";
    //                }
    //                catch (Exception ex)
    //                {
    //                    spanLogout.InnerHtml = "<a href='Logout.aspx'>Log out</a>";
    //                    killFacebookCookies();
    //                    ShowMessage("Your Facebook session has timed out. Please clear private data in browser and log in again");
    //                   // todo: delete facebook cookies and show message?
    //                }
    //            }
    //            else
    //            {
    //                spanLogout.InnerHtml = "<a href='Logout.aspx'>Log out</a>";

    //            }
    //            _userName = objSessionvalue.UserName;
    //            myprofile.HRef = Session["APP_BASE_DOMAIN"].ToString() + "tributes.aspx"; 
    //            myprofile.Visible = true;
    //            lnRegistration.Visible = false;
    //        }
    //        else
    //        {
    //            lnRegistration.Visible = true;
    //            spanLogout.InnerHtml = "<a href='log_in.aspx'>Log in</a>";
    //            myprofile.Visible = false;

    //        }
    //        if (!sess.IsConnected())
    //        {
    //            spanLogout.InnerHtml = "<fb:login-button onlogin=\"window.location='" + Session["APP_BASE_DOMAIN"] + "log_in.aspx?location='+encodeURIComponent(location.href)\" v=\"2\"></fb:login-button>" + spanLogout.InnerHtml;
    //        }
    //    }
    //}

    protected void btnSearch_Click(object sender,EventArgs e)
    {
        SearchTribute SerachParam = null;

        StateManager stateManager = StateManager.Instance;
        SerachParam = (SearchTribute)stateManager.Get(PortalEnums.SearchEnum.Search.ToString(), StateManager.State.Session);

        if ((Page.GetType().Name.ToLower() == PortalEnums.PageNameEnum.tribute_alltribute_aspx.ToString()) || (SerachParam == null))
        {
            SerachParam = new SearchTribute();

            SerachParam.SearchString = txtsearch.Text.Trim();
            SerachParam.TributeType = "All Tributes";
            SerachParam.SearchType = PortalEnums.SearchEnum.Basic.ToString();
            SerachParam.SortOrder = PortalEnums.Sorting.DESC.ToString();

            stateManager.Add(PortalEnums.SearchEnum.Search.ToString(), SerachParam, StateManager.State.Session);
        }
        else
        {
            if (SerachParam != null)
            {
                SerachParam.SearchString = txtsearch.Text.Trim();

                SerachParam.TributeType += " Tributes";
                SerachParam.SearchType = PortalEnums.SearchEnum.Basic.ToString();
                SerachParam.SortOrder = PortalEnums.Sorting.DESC.ToString();
                stateManager.Add(PortalEnums.SearchEnum.Search.ToString(), SerachParam, StateManager.State.Session);
            }
        }

        // Redirect to the Search Result page
         Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.SearchResult.ToString()));
    }
}