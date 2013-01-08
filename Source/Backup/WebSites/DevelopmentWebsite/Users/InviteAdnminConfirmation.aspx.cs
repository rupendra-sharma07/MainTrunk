///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Users.InviteAdnminConfirmation.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to log into the site
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
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Miscellaneous;
using System.Text;

public partial class Users_InviteAdnminConfirmation : PageBase, IAdminConfromation
{


    private AdminConfromationPresenter _presenter;


    [CreateNew]
    public AdminConfromationPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Notice.Visible = false;
            StateManager stateManager1 = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager1.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                // Added by Ashu on Oct 3, 2011 for rewrite URL 
                if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                    myprofile.HRef = Session["APP_BASE_DOMAIN"] + "moments.aspx";
                else
                    myprofile.HRef = Session["APP_BASE_DOMAIN"] + "tributes.aspx";
                Usernamelong.Text = objSessionvalue.UserName;
                lbtnSubmit.Attributes.Add("onclick", "Hideheader();");
                TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
                //Tributes objtribute = (Tributes)stateManager.Get("TributeSession", TributesPortal.Utilities.StateManager.State.Session);
                //SetMessageHeader(objtribute.TributeName, objtribute.TributeId.ToString(), objtribute.TributeUrl, objtribute.TypeDescription);
                if ((Request.QueryString["TributeUrl"] != null) && (Request.QueryString["TributeType"] != null))
                {
                    Tributes objTribute = new Tributes();
                    objTribute.TributeUrl = Request.QueryString["TributeUrl"].ToString();
                    objTribute.TypeDescription = Request.QueryString["TributeType"].ToString();

                    MiscellaneousController objMisc = new MiscellaneousController();
                    Tributes objTributeDetail = objMisc.GetTributeSessionForUrlAndType(objTribute, WebConfig.ApplicationType.ToString());

                    stateManager1.Add("TributeSession", objTributeDetail, StateManager.State.Session);
                    SetMessageHeader(objTributeDetail.TributeName, objTributeDetail.TributeId.ToString(), objTributeDetail.TributeUrl, objTributeDetail.TypeDescription);
                }
                
            }
            else
            {
                Response.Redirect("~/log_in.aspx");
            }
        
        }
    }

    private void SetMessageHeader(string tributeName,string tributeId, string tributeUrl, string tributeType)
    {

        StringBuilder objString = new StringBuilder();
        objString.Append("<h2>");
        objString.Append("You have been selected to be an administrator of the Tribute: <a href='http://" + tributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + tributeUrl + "' target=\"_blank\">" + tributeName + "</a></h2>");
        objString.Append("<h3>");
        objString.Append("A tribute administrator has complete control over the tribute. Administrators can");
        objString.Append(" modify the tribute settings (theme, privacy, tribute details) and can manage content");
        objString.Append(" (delete photos, guestbook entries, comments)");
        objString.Append("</h3>");

        Notice.InnerHtml = objString.ToString();
        Notice.Visible = true;
    
    }
    protected void lbtnSubmit_Click(object sender, EventArgs e)
    {
        TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
        Tributes objtribute =
            (Tributes) stateManager.Get("TributeSession", TributesPortal.Utilities.StateManager.State.Session);

        StateManager stateManager1 = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue) stateManager1.Get("objSessionvalue", StateManager.State.Session);
        if (objSessionvalue != null)
            if (rdbAccept.Checked == true)
            {
                _presenter.OnConformAdmin(objtribute, objSessionvalue, true);
            }
        //Response.Redirect("tributehomepage.aspx");
        if (objtribute != null)
        {

            if (WebConfig.ApplicationMode.ToLower().Equals("local"))
            {
                Response.Redirect(

                    WebConfig.TopLevelDomain + "/" + objtribute.TributeUrl + "/?TributeType=" +
                    objtribute.TypeDescription.Replace("New Baby", "newbaby").ToLower(), false);
            }
            else
            {
                Response.Redirect(
                    "http://" + objtribute.TypeDescription.Replace("New Baby", "newbaby").ToLower() + "." +
                    WebConfig.TopLevelDomain + "/" + objtribute.TributeUrl, false);
            }
        }
        else
        {
            Response.Redirect("~/Error/Error404.aspx");
        }
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.ClearHeaders();
        Response.ExpiresAbsolute = DateTime.Now.AddHours(-1);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AddHeader("Pragma", "no-cache");
        string strDisableBackButton = "window.history.go(1)";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "DisableBack", strDisableBackButton, true);
        Response.Redirect(Session["APP_BASE_DOMAIN"] + "logout.aspx", false);
    }
}
