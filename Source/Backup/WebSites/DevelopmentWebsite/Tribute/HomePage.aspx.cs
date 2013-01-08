///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.HomePage.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the home page
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
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
#endregion

public partial class Tribute_HomePage : PageBase
{
    #region CLASS VARIABLES
    protected int _tributeId;
    protected string _tributeName;
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        //added to get tribute id in the querystring, if user adds th epage to favorites Tribute Id is required there.
        if (Request.QueryString["TributeId"] != null)
            _tributeId = int.Parse(Request.QueryString["TributeId"].ToString());

        if (Request.QueryString["TributeName"] != null)
            _tributeName = Request.QueryString["TributeName"].ToString();

        if (!IsPostBack)
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                Usernamelong.Text = objSessionvalue.UserName;
            }
        
        }
    }
    #endregion
}
