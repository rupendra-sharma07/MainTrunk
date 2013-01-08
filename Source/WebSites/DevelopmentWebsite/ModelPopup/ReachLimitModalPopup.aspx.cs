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
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Tribute.Views;
using TributesPortal.Utilities;
using TributePortalSecurity;
using TributesPortal.Miscellaneous;
using TributesPortal.BusinessLogic;
public partial class ModelPopup_ReachLimitModalPopup : System.Web.UI.Page
{
    public string _tabName ;
    public string _upgradeUrl = string.Empty;
    public string _tributeUrl;
    public string location = string.Empty;
    public int photoId = 0;
    public int photoAlbumId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            object[] objTribute = new object[5];
            if (Request.QueryString["TributeId"] != null && Request.QueryString["TributeId"].ToString() != string.Empty)
            {
                int tributeId = int.Parse(Request.QueryString["TributeId"].ToString());
                TributeManager tributeMgr = new TributeManager();
                objTribute = tributeMgr.GetTributeDetailOnTributeId(tributeId);

                Session["TributURL"] = objTribute[2] != null ? Convert.ToString(objTribute[2]) : string.Empty;
                Session["TributeType"] = objTribute[3] != null ? Convert.ToString(objTribute[3]).ToLower().Replace("new baby", "newbaby") : string.Empty;
                Session["PackageId"] = objTribute[4] != null ? Convert.ToInt32(objTribute[4]) : 0;

                if (Request.QueryString["TabName"] != null && Request.QueryString["TabName"].ToString() != string.Empty)
                {
                    _tabName = Request.QueryString["TabName"].ToString();
                    if (_tabName.Equals("Notes"))
                    {
                        divNotes.Visible = true;
                    }
                    else if (_tabName.Equals("Events"))
                    {
                        divEvents.Visible = true;
                    }
                    else if (_tabName.Equals("Photos"))
                    {
                        divPhotoAlbum.Visible = true;
                    }
                    else if (_tabName.Equals("Videos"))
                    {
                        divVideo.Visible = true;
                    }
                    else if (_tabName.Equals("UpgradePhoto"))
                    {
                        divUpgradeFullView.Visible = true;
                        if (Session["PhotoId"] != null)
                            int.TryParse(Session["PhotoId"].ToString(), out photoId);
                    }
                    else if (_tabName.Equals("UpgradeAlbum"))
                    {
                        divUpgradeAlbum.Visible = true;
                        if (Session["PhotoAlbumId"] != null)
                            int.TryParse(Session["PhotoAlbumId"].ToString(), out photoAlbumId);
                    }
                }
                if (Request.QueryString["TributeUrl"] != null && Request.QueryString["TributeUrl"].ToString() != string.Empty)
                    _tributeUrl = Request.QueryString["TributeUrl"].ToString();
            }
        }
    }
    protected void lbtnUpgradeClick(object sender, EventArgs e)
    {
        // Added by Ashu on Oct 3, 2011 for rewrite URL 
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
            _upgradeUrl = WebConfig.AppBaseDomain + "MomentsSponsor.aspx?TributeURL=" + Session["TributURL"] + "&TributeType=" + Session["TributeType"] + "&ReachLimit=Max";
        else
            _upgradeUrl = WebConfig.AppBaseDomain + "TributeSponsor.aspx?TributeURL=" + Session["TributURL"] + "&TributeType=" + Session["TributeType"] + "&ReachLimit=Max";
        StringBuilder sb = new StringBuilder();
        //sb.Append("<script type=\"text/javascript\">");
        sb.Append("parent.setLocation('" + _upgradeUrl + "');");
        //sb.Append("</script>");
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "HidePanel", sb.ToString(), true);

    }
    protected void btnContinueClick(object sender, EventArgs e)
    {
        //string strJScript = string.Empty;
        //strJScript = "<script language=javascript>";
        //strJScript += "parent.fnExpiryNoticePopupClose();";
        //strJScript += "</script>";

        //Response.Write(strJScript);
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "fnExpiryNoticePopupClose();", true);
        if (Request.QueryString["TabName"] != null && Request.QueryString["TabName"].ToString() != string.Empty)
        {
            _tabName = Request.QueryString["TabName"].ToString();
            if (_tabName.Contains("UpgradePhoto"))
            {
                _tabName = "photo";
            }
            else if (_tabName.Contains("UpgradeAlbum"))
            {
                _tabName = "photoalbum";
            }
        }
        if (Session["PhotoId"] != null)
            int.TryParse(Session["PhotoId"].ToString(), out photoId);
        if (Session["PhotoAlbumId"] != null)
            int.TryParse(Session["PhotoAlbumId"].ToString(), out photoAlbumId);
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        Tributes objVal = (Tributes)stateManager.Get("TributeSession", StateManager.State.Session);
        if (objVal.TypeDescription != null)
        {
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                if (_tabName.Equals("photoalbum"))
                    location = WebConfig.AppBaseDomain + objVal.TributeUrl + "/" + _tabName + ".aspx" + "?PhotoAlbumId=" + photoAlbumId.ToString();
                else if (_tabName.Equals("photo"))
                    location = WebConfig.AppBaseDomain + objVal.TributeUrl + "/" + _tabName + ".aspx" + "?PhotoId=" + photoId.ToString();
                else
                    location = WebConfig.AppBaseDomain + objVal.TributeUrl + "/" + _tabName + ".aspx";
            }
            else
            {
                if (_tabName.Equals("photoalbum"))
                {
                    if (objVal.TypeDescription.Equals("New Baby"))
                        location = "http://newbaby." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl + "/" + _tabName + ".aspx" + "?PhotoAlbumId=" + photoAlbumId.ToString();
                    else
                        location = "http://" + objVal.TypeDescription.ToLower() + "." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl + "/" + _tabName + ".aspx" + "?PhotoAlbumId=" + photoAlbumId.ToString();
                }
                else if (_tabName.Equals("photo"))
                {
                    if (objVal.TypeDescription.Equals("New Baby"))
                        location = "http://newbaby." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl + "/" + _tabName + ".aspx" + "?PhotoId=" + photoId.ToString();
                    else
                        location = "http://" + objVal.TypeDescription.ToLower() + "." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl + "/" + _tabName + ".aspx" + "?PhotoId=" + photoId.ToString();
                }
                else
                {
                    if (objVal.TypeDescription.Equals("New Baby"))
                        location = "http://newbaby." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl + "/" + _tabName + ".aspx";
                    else
                        location = "http://" + objVal.TypeDescription.ToLower() + "." + WebConfig.TopLevelDomain + "/" + objVal.TributeUrl + "/" + _tabName + ".aspx";
                }
            }
        }
        //string location = WebConfig.AppBaseDomain + Session["TributURL"] + "/" + _tabName + ".aspx";
        StringBuilder sb = new StringBuilder();
        sb.Append("parent.setLocation('" + location + "');");
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "HidePanel", sb.ToString(), true);
    }
}
