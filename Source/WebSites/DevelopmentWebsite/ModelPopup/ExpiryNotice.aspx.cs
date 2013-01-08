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


public partial class ModelPopup_ExpiryNotice : PageBase
{
    //public int _packageId;
    //public string _TributeType = string.Empty;
    //private string _TributURL = string.Empty;

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

            }
        }       
    }

    protected void btnUpGradeClick(object sender, EventArgs e)
    {
        string location = "";
        // Added by Ashu on Oct 10, 2011 for rewrite URL 
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            location = WebConfig.AppBaseDomain + "Moments/MomentsSponsor.aspx?TributeURL=" + Session["TributURL"] + "&TributeType=" + Session["TributeType"];
        }
        else
        {
            location = WebConfig.AppBaseDomain + "Tribute/TributeSponsor.aspx?TributeURL=" + Session["TributURL"] + "&TributeType=" + Session["TributeType"];
        } StringBuilder strb = new StringBuilder();
        //strb.Append("<script type=\"text/javascript\">");
        strb.Append("parent.setLocation('" + location + "');");
        //strb.Append("</script>");
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "HidePanel", strb.ToString(), true);

    }

    protected void btnTakeTourClick(object sender, EventArgs e)
    {
        // 15 _July Mohit Gupta Link Issue
        string location = WebConfig.AppBaseDomain + "tour.aspx";
        StringBuilder strb = new StringBuilder();
        //strb.Append("<script type=\"text/javascript\">");
        strb.Append("parent.setLocation('" + location + "');");
        //strb.Append("</script>");
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "HidePanel", strb.ToString(), true);

    }
}

