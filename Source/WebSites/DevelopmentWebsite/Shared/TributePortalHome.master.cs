///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Shared.Story.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page is used as a general purpose master pages
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
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;

public partial class Shared_TributePortalHome : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            #region style sheet Linking
            link1.Href = "../assets/" + ConfigurationManager.AppSettings["CssDir"].ToString() + "/default.css";
            link2.Href = "../assets/" + ConfigurationManager.AppSettings["CssDir"].ToString() + "/layouts/centered1024_1.css";
            link3.Href = "../assets/" + ConfigurationManager.AppSettings["CssDir"].ToString() + "/print.css";
            link4.Href = "../assets/" + ConfigurationManager.AppSettings["CssDir"].ToString() + "/ScreenLatest.css";
            #endregion

            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (Request.QueryString["Type"] != null)
            {
                ytHeader.TributeType = Request.QueryString["Type"].ToString();
            }
            string appPath = string.Empty;
            if (WebConfig.ApplicationMode.ToLower().Equals("local"))
            {
                appPath = WebConfig.AppBaseDomain;
            }
            else
            {
                appPath = string.Format("{0}{1}{2}", "http://www.", WebConfig.TopLevelDomain, "/");
            }
            if (Request.QueryString["Theme"] != null)
            {
                idSheet.Href = appPath + "assets/themes/" + Request.QueryString["Theme"].ToString().Replace("'", "") + "/theme.css";
            }

        }
        //if (Request.QueryString["Theme"] != null)
        //{
        //    string Theme = Request.QueryString["Theme"].ToString();
        //    Body.Attributes.Add("onload", "Themer(" + Theme + ");");
        //}



    }
    protected void lbtnMyProfile_Click(object sender, EventArgs e)
    {

    }
}
