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

public partial class Errors_Error500 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Request.QueryString != null) && (Request.QueryString.ToString().Length > 0))
        {
            Response.Redirect("../Errors/Error500.aspx");
        }
    }
}
