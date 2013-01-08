/* Made By Ashu(24th June,2011) */

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

public partial class ModelPopup_Popup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string qStr = Request.QueryString.ToString();
            if (Request.QueryString["Type"] != null)
            {
                string type = Request.QueryString["Type"].ToString();
                if (type == "Gift")
                {
                    PopupTitle.Text = "Gift Modal Popup";
                    DivGift.Visible = true;
                    DivMessage.Visible = false;
                }
                else if (type == "Message")
                {
                    PopupTitle.Text = "PostMessage Modal Popup";
                    DivMessage.Visible = true;
                    DivGift.Visible = false;
                }
            }
            else if (qStr.ToLower().Contains("gift.aspx"))
            {
                PopupTitle.Text = "Gift Modal Popup";
                DivGift.Visible = true;
                DivMessage.Visible = false;
            }
            else if (qStr.ToLower().Contains("guestbook.aspx"))
            {
                PopupTitle.Text = "PostMessage Modal Popup";
                DivMessage.Visible = true;
                DivGift.Visible = false;
            }
        }
    }
}
