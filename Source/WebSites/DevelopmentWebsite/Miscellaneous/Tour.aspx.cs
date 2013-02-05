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
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;

public partial class Miscellaneous_Tour : System.Web.UI.Page
{
    private SessionValue objSessionValue = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //code for YT Mobile redirections
            string redirctMobileUrl = string.Empty;

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
                    redirctMobileUrl = string.Format("{0}{1}{2}", "https://www.", WebConfig.TopLevelDomain, "/mobile/Tour.html");
                    Response.Redirect(redirctMobileUrl, false);
                }
            }

        }
        if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
        {
            yourtribute.Visible = false;
            yourmoments.Visible = true;
            tourTitle.InnerHtml=@"Tour - See how to create a Website for your significant event";
        }
        else if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourtribute")
        {
            yourtribute.Visible = true;
            yourmoments.Visible = false;
        }
    }
}
