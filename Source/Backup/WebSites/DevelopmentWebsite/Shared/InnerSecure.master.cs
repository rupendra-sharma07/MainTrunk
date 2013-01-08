using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Shared_InnerSecure : System.Web.UI.MasterPage
{
    #region Navigation Menu Enums
    public enum AdminNavPrimaryEnum
    {
        tributes,
        favorites,
        inbox,
        events,
        myprofile
    }

    public enum AdminNavSecondaryEnum
    {
        profile,
        privacy,
        emailpassword,
        emailnotifications,
        billing
    }
    #endregion Navigation Menu Enums

    #region Properties
    protected string _page_title;
    public string PageTitle
    {
        get
        {
            return _page_title;
        }
        set
        {
            _page_title = value;
        }
    }

    protected string _nav_primary;
    public string NavPrimary
    {
        get
        {
            return _nav_primary;
        }
        set
        {
            _nav_primary = value;
        }
    }

    protected string _nav_secondary = string.Empty;
    public string NavSecondary
    {
        get
        {
            return _nav_secondary;
        }
        set
        {
            _nav_secondary = value;
        }
    }
    #endregion Properties

    #region Page Load
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            trbBlog.InnerHtml = "News from Your Moments";
        }

        if (!this.IsPostBack)
        {
            //set visibility of my tribute
            if (Session["mytribute"] != null)
            {
                limytribute.Visible = bool.Parse(Session["mytribute"].ToString());
            }

            //set visibility of my favourite tribute.
            if (Session["myfavourite"] != null)
            {
                limyfavourite.Visible = bool.Parse(Session["myfavourite"].ToString());
            }

            if (_nav_primary.Equals(AdminNavPrimaryEnum.tributes.ToString()))
            {
                limytribute.Attributes.Add("class", "yt-Selected");
                limytribute.InnerHtml = "<a href='javascript:void(0);'>My Tributes</a>";
                if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                {
                    limytribute.InnerHtml = "<a href='javascript:void(0);'>My Websites</a>";
                }
            }
            else
            {
                // Added by Ashu on Oct 3, 2011 for rewrite URL 
                if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                    limytribute.InnerHtml = "<a href='" + Session["APP_BASE_DOMAIN"] + "moments.aspx'>My Websites</a>";
                else
                    limytribute.InnerHtml = "<a href='" + Session["APP_BASE_DOMAIN"] + "tributes.aspx'>My Tributes</a>";
            }

            if (_nav_primary.Equals(AdminNavPrimaryEnum.favorites.ToString()))
            {
                limyfavourite.Attributes.Add("class", "yt-Selected");
                limyfavourite.InnerHtml = "<a href='javascript:void(0);'>My Favorites</a>";
            }
            else
            {
                limyfavourite.InnerHtml = "<a href='" + Session["APP_BASE_DOMAIN"] + "favorites.aspx'>My Favorites</a>";
            }

            if (_nav_primary.Equals(AdminNavPrimaryEnum.inbox.ToString()))
            {
                liinbox.Attributes.Add("class", "yt-Selected");
                liinbox.InnerHtml = "<a href='javascript:void(0);'>Inbox</a>";
            }
            else
            {
                liinbox.InnerHtml = "<a href='" + Session["APP_BASE_DOMAIN"] + "inbox.aspx'>Inbox</a>";
            }

            if (_nav_primary.Equals(AdminNavPrimaryEnum.events.ToString()))
            {
                lievents.Attributes.Add("class", "yt-Selected");
                lievents.InnerHtml = "<a href='javascript:void(0);'>Events</a>";
            }
            else
            {
                lievents.InnerHtml = "<a href='" + Session["APP_BASE_DOMAIN"] + "userevents.aspx'>Events</a>";
            }

            if (_nav_primary.Equals(AdminNavPrimaryEnum.myprofile.ToString()))
            {
                limyprofile.Attributes.Add("class", "yt-Selected yt-MyProfileTab");
                limyprofile.InnerHtml = "<a href='javascript:void(0);'>My Profile</a>";
            }
            else
            {
                limyprofile.Attributes.Add("class", "yt-MyProfileTab");
                limyprofile.InnerHtml = "<a href='" + Session["APP_BASE_DOMAIN"] + "adminprofilesettings.aspx'>My Profile</a>";
            }
        }

    }
    #endregion
}
