//===============================================================================
// Microsoft patterns & practices
// Web Client Software Factory
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;

public partial class _Default : System.Web.UI.Page 
{
    //private SessionValue objSessionValue = null;
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    System.Diagnostics.Debug.WriteLine("In Default -- Page_Load");
    //    string redirctMobileUrl = string.Empty;
    //    if (!IsPostBack)
    //    {
    //        System.Diagnostics.Debug.WriteLine("In Default -- Postback");
    //        DeviceManager deviceManager = new DeviceManager
    //        {
    //            UserAgent = Request.UserAgent,
    //            IsMobileBrowser = Request.Browser.IsMobileDevice
    //        };

    //        // Added by Varun Goel on 25 Jan 2013 for NoRedirection functionality
    //        TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;

    //        objSessionValue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
    //        System.Diagnostics.Debug.WriteLine("In Default -- Validating");
    //        if (objSessionValue != null)
    //            System.Diagnostics.Debug.WriteLine("In Default -- Validating redirection:" + objSessionValue.NoRedirection);
    //        if (objSessionValue == null || objSessionValue.NoRedirection == null || objSessionValue.NoRedirection == false)
    //        {
    //            System.Diagnostics.Debug.WriteLine("In Default -- After validation inside if for redirection");
    //            if (deviceManager.IsMobileDevice())
    //            {
    //                // Redirection URL
    //                redirctMobileUrl = string.Format("{0}{1}{2}", "https://www.", WebConfig.TopLevelDomain, "/mobile/Search.html");
    //                Response.Redirect(redirctMobileUrl, false);
    //            }
    //        }
    //    }
    //}
}
