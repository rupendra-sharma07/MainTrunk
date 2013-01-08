///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.ModelPopup.MemVideoExpNotice.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the list of events organized for the selected tribute.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
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
using TributesPortal.Utilities;
using TributesPortal.BusinessLogic;
#endregion

public partial class ModelPopup_MemVideoExpNotice : PageBase
{
    public int _packageId;
    public string _TributeType = string.Empty;
    private string _TributURL = string.Empty;

    #region PageLoad
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            object[] objTribute = new object[13];
            if (Request.QueryString["TributeId"] != null && Request.QueryString["TributeId"].ToString() != string.Empty)
            {
                int tributeId = int.Parse(Request.QueryString["TributeId"].ToString());
                TributeManager tributeMgr = new TributeManager();
                objTribute = tributeMgr.GetTributeUserDetailOnTributeId(tributeId);

                Session["TributURL"] = objTribute[2] != null ? Convert.ToString(objTribute[2]) : string.Empty;
                Session["TributeType"] = objTribute[3] != null ? Convert.ToString(objTribute[3]).ToLower().Replace("new baby", "newbaby") : string.Empty;
                Session["PackageId"] = objTribute[4] != null ? Convert.ToInt32(objTribute[4]) : 0;

                Session["DOB"] = objTribute[6] != null ? objTribute[6].ToString() : string.Empty;
                Session["DOD"] = objTribute[7] != null ? objTribute[7].ToString() : string.Empty;
                Session["ImagePath"] = objTribute[12] != null ? Convert.ToString(objTribute[12]) : string.Empty;



                Session["UName"] = objTribute[1] != null ? Convert.ToString(objTribute[1]) : string.Empty;

                if (objTribute[8].ToString() != "" && objTribute[9].ToString() != "" && objTribute[10].ToString() != "")
                    Session["Location"] = String.Format("{0}, {1}, {2}", objTribute[8], objTribute[9], objTribute[10]);
                else if (objTribute[8].ToString() != "" && objTribute[9].ToString() != "" && objTribute[10].ToString() == "")
                    Session["Location"] = String.Format("{0}, {1}", objTribute[8], objTribute[9]);
                else if (objTribute[8].ToString() != "" && objTribute[9].ToString() == "" && objTribute[10].ToString() != "")
                    Session["Location"] = String.Format("{0}, {1}", objTribute[8], objTribute[10]);
                else if (objTribute[8].ToString() == "" && objTribute[9].ToString() != "" && objTribute[10].ToString() != "")
                    Session["Location"] = String.Format("{0}, {1}", objTribute[9], objTribute[10]);
                else if (objTribute[8].ToString() == "" && objTribute[9].ToString() == "" && objTribute[10].ToString() != "")
                    Session["Location"] = String.Format("{0}",  objTribute[10]);
                else if (objTribute[8].ToString() != "" && objTribute[9].ToString() == "" && objTribute[10].ToString() == "")
                    Session["Location"] = String.Format("{0}", objTribute[8]);




                //Session["Location"] = String.Format("{0}, {1}, {2}", objTribute[8], objTribute[9], objTribute[10]);
            }

            if (Session["DOB"] != null)
            {
                if (Session["DOB"].ToString().Length > 0)
                {
                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(Session["DOB"].ToString());
                    lblDateInfo.Text = dt.ToString("MMMM dd, yyyy");
                }
            }
            if (Session["DOD"] != null)
            {
                if (Session["DOD"].ToString().Length > 0)
                {
                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(Session["DOD"].ToString());
                    lblDateInfo.Text += " - " + dt.ToString("MMMM dd, yyyy");
                }
            }

            if (Session["Location"] != null)
                lblCountryInfo.Text = Session["Location"].ToString();
            
            string[] virtualDir = CommonUtilities.GetPath();

            if (Session["ImagePath"] != null)
            {
                 if (virtualDir != null)
                {
                    imgUserPhoto.ImageUrl = virtualDir[2] + Session["ImagePath"];
                    imgUserPhoto.AlternateText = "User Image";
                 }                
            }
            if (Session["UName"] != null)
            {
                lblName.Text = Session["UName"].ToString();
                lblUser2.Text = Session["UName"].ToString();
                lblUser3.Text = Session["UName"].ToString();
                lblUser4.Text = Session["UName"].ToString();
            }
        }
        
    } 
    #endregion

    #region btnUpGradeClick
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpGradeClick(object sender, EventArgs e)
    {
        string location = WebConfig.AppBaseDomain + "Tribute/TributeSponsor.aspx?TributeURL=" + Session["TributURL"] + "&TributeType=" + Session["TributeType"];
        StringBuilder sb = new StringBuilder();
        //sb.Append("<script type=\"text/javascript\">");
        sb.Append("parent.setLocation('" + location + "');");
        //sb.Append("</script>");
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "HidePanel", sb.ToString(), true);

    } 
    #endregion

    #region btnTakeTourClick
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnTakeTourClick(object sender, EventArgs e)
    { 
        // 15 _July Mohit Gupta :Link Issue
        string location = WebConfig.AppBaseDomain + "tour.aspx";
        StringBuilder sb = new StringBuilder();
        //sb.Append("<script type=\"text/javascript\">");
        sb.Append("parent.setLocation('" + location + "');");
        //sb.Append("</script>");
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "HidePanel", sb.ToString(), true);

    } 
    #endregion

}//end class
