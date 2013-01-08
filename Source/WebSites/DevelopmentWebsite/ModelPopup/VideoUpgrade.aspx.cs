//LHK:VideoUpgrade:March 2011

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

public partial class ModelPopup_VideoUpgrade : System.Web.UI.Page
{
    public string _tabName = "Content";
    public string _LimitText = "Max";
    public string _upgradeUrl = string.Empty;
    public string _tributeUrl;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void lbtnUpgradeClick2(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["TributeId"] != null && HttpContext.Current.Session["TributeId"].ToString() != string.Empty)
        {
            _upgradeUrl = WebConfig.AppBaseDomain + "Create.aspx?AccountType=2&Type=memorial&VideoTributeId=" + HttpContext.Current.Session["TributeId"].ToString();
        }
        StringBuilder sb = new StringBuilder();
        //sb.Append("<script type=\"text/javascript\">");
        sb.Append("parent.setLocation('" + _upgradeUrl + "');");
        //sb.Append("</script>");
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "HidePanel", sb.ToString(), true);

    }
    protected void lbtnUpgradeClick3(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["TributeId"] != null && HttpContext.Current.Session["TributeId"].ToString() != string.Empty)
        {
            _upgradeUrl = WebConfig.AppBaseDomain + "Create.aspx?AccountType=3&Type=memorial&VideoTributeId=" + HttpContext.Current.Session["TributeId"].ToString();
        }
        StringBuilder sb = new StringBuilder();
        //sb.Append("<script type=\"text/javascript\">");
        sb.Append("parent.setLocation('" + _upgradeUrl + "');");
        //sb.Append("</script>");
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "HidePanel", sb.ToString(), true);

    }
    protected void btnContinueClick(object sender, EventArgs e)
    {
        string location = string.Empty;
        if (HttpContext.Current.Session["TributeId"] != null && HttpContext.Current.Session["TributeId"].ToString() != string.Empty)
        {
            location = WebConfig.AppBaseDomain + "video/videotribute.aspx?tributeId=" + HttpContext.Current.Session["TributeId"].ToString();
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("parent.setLocation('" + location + "');");
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "HidePanel", sb.ToString(), true);
    }
}
