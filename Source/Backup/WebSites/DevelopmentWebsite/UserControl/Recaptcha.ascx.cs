///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.UserControl.Recaptcha.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page defines the control used to generate the coupon code
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

public partial class UserControl_Recaptcha : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        recaptcha.InnerHtml = GetRandomCouponNumber(6);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        recaptcha.InnerHtml = GetRandomCouponNumber(6);
    }

    /// <summary>
    /// Auto generated numbers.
    /// </summary>
    /// <param name="numChars"></param>
    /// <param name="seed"></param>
    /// <returns></returns>
    public string GetRandomCouponNumber(int numChars)
    {
        string[] chars = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S",
                        "T", "U", "V", "W", "X", "Y", "Z", "2", "3", "4", "5", "6", "7", "8", "9" };

        Random rnd = new Random();
        string random = string.Empty;
        for (int i = 0; i < numChars; i++)
        {
            random += chars[rnd.Next(0, 33)];
        }
        return random;
    }
}
