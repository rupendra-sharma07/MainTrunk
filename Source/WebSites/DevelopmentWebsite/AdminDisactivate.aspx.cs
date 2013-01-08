using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;

public partial class AdminDisactivate : System.Web.UI.Page
{
    private string _tester_password = "U61eoLYbi911&";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnDisactivate_Click(object sender, EventArgs e)
    {
        string _UserName = txtUsername.Text;
        string _Password = txtPassword.Text;
        if (!_Password.Equals(_tester_password))
        {
            lblErrMsg.InnerHtml = "You had entered invalid password. Please, try again.";
            lblErrMsg.Visible = true;
        }
        else
        {
            UserInfoResource ra = new UserInfoResource();
            ra.AdminDisactivateAccount(_UserName);
            lblErrMsg.InnerHtml = "Account "+_UserName+" was disactivated and can not be used for login into YT anymore.";
            lblErrMsg.Visible = true;
            lblErrMsg.Attributes.Add("class", "yt-Notice");
        }
    }
}
