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

public partial class Miscellaneous_Tour : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
