///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.TributePortalAdmin.PortalAdmin.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page defines the master page for portal administration
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
using System.IO;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using System.Xml;

public partial class TributePortalAdmin_PortalAdmin : System.Web.UI.MasterPage
{
    public string WebsiteWord = WebConfig.ApplicationWordForInternalUse.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objSessionvalueAdmin"] == null)
        {
            string fileName = Server.MapPath("~/Common") + "\\User.xml";
            if (File.Exists(fileName))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(fileName);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int UserID = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
                    string UserName = ds.Tables[0].Rows[0][1].ToString();
                    string FirstName = ds.Tables[0].Rows[0][2].ToString();
                    string LastName = ds.Tables[0].Rows[0][3].ToString();
                    bool IsUsernameVisiable = Convert.ToBoolean(ds.Tables[0].Rows[0][4].ToString());


                    SetSessionValue(UserID,UserName,FirstName,LastName,IsUsernameVisiable);
                    FileInfo objfile = new FileInfo(fileName);
                    if (objfile.Exists)
                    {
                        objfile.Delete();
                    }
                }
              }
            else
            {
                Response.Redirect("AdmintratorLogin.aspx");
            }
        }
    }
         private void SetSessionValue(int UserID,string UserName,string FirstName, string LastName, bool IsUsernameVisiable)
        {
            SessionValue _objSessionValue = new SessionValue(UserID, UserName, FirstName, LastName,"",1,"", IsUsernameVisiable);

            TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;
            stateManager.Add("objSessionvalueAdmin", _objSessionValue, StateManager.State.Session);
        }
        
}
