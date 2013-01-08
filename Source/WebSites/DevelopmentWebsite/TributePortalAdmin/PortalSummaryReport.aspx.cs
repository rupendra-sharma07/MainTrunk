///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.TributePortalAdmin.PortalSummaryReport.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the admin to view the portal summary report
///Audit Trail     : Date of Modification  Modified By         Description
///                  30-Jan-2009           Rahul Rohella      Properties have been changed 
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
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.TributePortalAdmin.Views;
using TributesPortal.Utilities;

public partial class TributePortalAdmin_PortalSummaryReport : System.Web.UI.Page, IPortalSummaryReport
{
    public string WebsiteWord = WebConfig.ApplicationWordForInternalUse.ToString();
    private PortalSummaryReportPresenter _presenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this._presenter.OnViewInitialized();
            this._presenter.GetTributeActivityReport(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower());
        }
        this._presenter.GetUserActivityReport(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower());
        this._presenter.OnViewLoaded();
        //this._presenter.GetUserActivityReport(ConfigurationManager.AppSettings["ApplicationTYpe"].ToString().ToLower());
        HideLabels(); //As Expired users are not available, for the time being labels are not visible
    }

    [CreateNew]
    public PortalSummaryReportPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    public string PersonalTodayNew
    {
        set 
        {
            if (value != null)
                lblPersonalTodayNew.Text = value; 
            else
                lblPersonalTodayNew.Text = "0"; 
        }
    }
    public string PersonalTodayExpired 
    { 
        set 
        {
            if (value != null)
                lblPersonalTodayExpired.Text = value;
            else
                lblPersonalTodayExpired.Text = "0";
        } 
    }
    public string BusinessTodayNew 
    {
        set 
        { 
            if (value!= null)
                lblBusinessTodayNew.Text = value; 
            else
                lblBusinessTodayNew.Text = "0"; 
        } 
    }
    public string BusniessTodayExpired 
    { 
        set 
        {
            if (value != null)
                lblBusinessTodayExpired.Text = value; 
            else
                lblBusinessTodayExpired.Text = "0"; 
        }
    }
    public string TotalTodayNew 
    { 
        set 
        {
            if (value != null)
            lblTotalTodayNew.Text = value;
            else
            lblTotalTodayNew.Text = "0"; 
        } 
    }
    public string TotalTodayExpired 
    { 
        set
        {
            if (value != null)
            lblTotalTodayExpired.Text = value; 
            else
            lblTotalTodayExpired.Text = "0"; 
        } 
    }

    public string Personal30DaysNew 
    { 
        set 
        {
            if (value != null)
            lblPersonal30DaysNew.Text = value;
            else
            lblPersonal30DaysNew.Text = "0";
        } 
    }
    public string Personal30DaysExpired 
    { 
        set 
        { 
            if (value!= null)
            lblPersonal30DayExpired.Text = value; 
            else
            lblPersonal30DayExpired.Text = "0"; 
        }
    }
    public string Business30DaysNew 
    { 
        set 
        {
            if (value != null)
            lblBusiness30DaysNew.Text = value; 
            else
            lblBusiness30DaysNew.Text = "0"; 
        } 
    }
    public string Busniess30DaysExpired 
    { 
        set 
        {
            if (value != null)
            lblBusiness30DaysExpired.Text = value;
            else
            lblBusiness30DaysExpired.Text = "0";
        } 
    }
    public string Total30DaysNew 
    { 
        set 
        {
            if (value != null)
            lblTotal30DaysNew.Text = value;
            else
            lblTotal30DaysNew.Text = "0";
        } 
    }
    public string Total30DaysExpired 
    { 
        set
        {
            if (value != null)
            lblTotal30DaysExpired.Text = value; 
            else
            lblTotal30DaysExpired.Text = "0"; 
        } 
    }

    public string TotalPesonalActiveAccounts 
    { 
        set 
        {
            if (value != null)
            lblPersonalTotalActiveAccounts.Text = value; 
            else
            lblPersonalTotalActiveAccounts.Text = "0"; 
        } 
    }
    public string TotalBusinessActiveAccounts 
    { 
        set 
        {
            if (value != null)
            lblBusinessTotalActiveAccounts.Text = value; 
            else
            lblBusinessTotalActiveAccounts.Text = "0"; 
        } 
    }
    public string TotalActiveAccounts 
    { 
        set 
        {
            if (value != null)
            lblTotalActiveAccounts.Text = value; 
            else
            lblTotalActiveAccounts.Text = "0"; 
        } 
    }

    public void HideLabels()
    {
        lblPersonalTodayExpired.Visible=false;
        lblBusinessTodayExpired.Visible=false;
        lblTotalTodayExpired.Visible=false;
        lblPersonal30DayExpired.Visible=false;
        lblBusiness30DaysExpired.Visible=false;
        lblTotal30DaysExpired.Visible = false;
    }

    #region FOR TRIBUTE ACTIVITIES
    #region FOR MEMORIAL TRIBUTE
    public string MemorialTribute { set { lblMemorialType.Text = value; } }
    public string MemorialTodayTrial { set { lblMemorialTodayTrial.Text = value; } }
    public string MemorialToday1Year { set { lblMemorialToday1Yr.Text = value; } }
    public string MemorialTodayExpired { set { lblMemorialTodayExpired.Text = value; } }
    public string MemorialTodayLifeTIme { set { lblMemorialTodayLife.Text = value; } }
    public string MemorialLast30DaysTrial { set { lblMemorialLast30DaysTrial.Text = value; } }
    public string MemorialLast30Days1Year { set { lblMemorialLast30Days1Yr.Text = value; } }
    public string MemorialLast30DaysExpired { set { lblMemorialLast30DaysExpired.Text = value; } }
    public string MemorialLast30DaysLifeTIme { set { lblMemorialLast30DaysLife.Text = value; } }
    public string MemorialTotalTrial { set { lblMemorialTotalTrial.Text = value; } }
    public string MemorialTotal1Year { set { lblMemorialTotal1Yr.Text = value; } }
    public string MemorialTotalExpired { set { lblMemorialTotalExpired.Text = value; } }
    public string MemorialTotalLifeTime { set { lblMemorialTotalLife.Text = value; } }
    #endregion

    #region FOR WEDDING TRIBUTE
    public string WeddingTribute { set { lblWeddingType.Text = value; } }
    public string WeddingTodayTrial { set { lblWeddingTodayTrial.Text = value; } }
    public string WeddingToday1Year { set { lblWeddingToday1Yr.Text = value; } }
    public string WeddingTodayExpired { set { lblWeddingTodayExpired.Text = value; } }
    public string WeddingTodayLifeTIme { set { lblWeddingTodayLife.Text = value; } }
    public string WeddingLast30DaysTrial { set { lblWeddingLast30DaysTrial.Text = value; } }
    public string WeddingLast30Days1Year { set { lblWeddingLast30Days1Yr.Text = value; } }
    public string WeddingLast30DaysExpired { set { lblWeddingLast30DaysExpired.Text = value; } }
    public string WeddingLast30DaysLifeTIme { set { lblWeddingLast30DaysLife.Text = value; } }
    public string WeddingTotalTrial { set { lblWeddingTotalTrial.Text = value; } }
    public string WeddingTotal1Year { set { lblWeddingTotal1Yr.Text = value; } }
    public string WeddingTotalExpired { set { lblWeddingTotalExpired.Text = value; } }
    public string WeddingTotalLifeTime { set { lblWeddingTotalLife.Text = value; } }
    #endregion

    #region FOR NEW BABY TRIBUTE
    public string NewBabyTribute { set { lblNewBabyType.Text = value; } }
    public string NewBabyTodayTrial { set { lblNewBabyTodayTrial.Text = value; } }
    public string NewBabyToday1Year { set { lblNewBabyToday1Yr.Text = value; } }
    public string NewBabyTodayExpired { set { lblNewBabyTodayExpired.Text = value; } }
    public string NewBabyTodayLifeTIme { set { lblNewBabyTodayLife.Text = value; } }
    public string NewBabyLast30DaysTrial { set { lblNewBabyLast30DaysTrial.Text = value; } }
    public string NewBabyLast30Days1Year { set { lblNewBabyLast30Days1Yr.Text = value; } }
    public string NewBabyLast30DaysExpired { set { lblNewBabyLast30DaysExpired.Text = value; } }
    public string NewBabyLast30DaysLifeTIme { set { lblNewBabyLast30DaysLife.Text = value; } }
    public string NewBabyTotalTrial { set { lblNewBabyTotalTrial.Text = value; } }
    public string NewBabyTotal1Year { set { lblNewBabyTotal1Yr.Text = value; } }
    public string NewBabyTotalExpired { set { lblNewBabyTotalExpired.Text = value; } }
    public string NewBabyTotalLifeTime { set { lblNewBabyTotalLife.Text = value; } }
    #endregion

    #region FOR ANNIVERSARY TRIBUTE
    public string AnniversaryTribute { set { lblAnniversaryType.Text = value; } }
    public string AnniversaryTodayTrial { set { lblAnniversaryTodayTrial.Text = value; } }
    public string AnniversaryToday1Year { set { lblAnniversaryToday1Yr.Text = value; } }
    public string AnniversaryTodayExpired { set { lblAnniversaryTodayExpired.Text = value; } }
    public string AnniversaryTodayLifeTIme { set { lblAnniversaryTodayLife.Text = value; } }
    public string AnniversaryLast30DaysTrial { set { lblAnniversaryLast30DaysTrial.Text = value; } }
    public string AnniversaryLast30Days1Year { set { lblAnniversaryLast30Days1Yr.Text = value; } }
    public string AnniversaryLast30DaysExpired { set { lblAnniversaryLast30DaysExpired.Text = value; } }
    public string AnniversaryLast30DaysLifeTIme { set { lblAnniversaryLast30DaysLife.Text = value; } }
    public string AnniversaryTotalTrial { set { lblAnniversaryTotalTrial.Text = value; } }
    public string AnniversaryTotal1Year { set { lblAnniversaryTotal1Yr.Text = value; } }
    public string AnniversaryTotalExpired { set { lblAnniversaryTotalExpired.Text = value; } }
    public string AnniversaryTotalLifeTime { set { lblAnniversaryTotalLife.Text = value; } }
    #endregion

    #region FOR BIRTHDAY TRIBUTE
    public string BirthdayTribute { set { lblBirthdayType.Text = value; } }
    public string BirthdayTodayTrial { set { lblBirthdayTodayTrial.Text = value; } }
    public string BirthdayToday1Year { set { lblBirthdayToday1Yr.Text = value; } }
    public string BirthdayTodayExpired { set { lblBirthdayTodayExpired.Text = value; } }
    public string BirthdayTodayLifeTIme { set { lblBirthdayTodayLife.Text = value; } }
    public string BirthdayLast30DaysTrial { set { lblBirthdayLast30DaysTrial.Text = value; } }
    public string BirthdayLast30Days1Year { set { lblBirthdayLast30Days1Yr.Text = value; } }
    public string BirthdayLast30DaysExpired { set { lblBirthdayLast30DaysExpired.Text = value; } }
    public string BirthdayLast30DaysLifeTIme { set { lblBirthdayLast30DaysLife.Text = value; } }
    public string BirthdayTotalTrial { set { lblBirthdayTotalTrial.Text = value; } }
    public string BirthdayTotal1Year { set { lblBirthdayTotal1Yr.Text = value; } }
    public string BirthdayTotalExpired { set { lblBirthdayTotalExpired.Text = value; } }
    public string BirthdayTotalLifeTime { set { lblBirthdayTotalLife.Text = value; } }
    #endregion

    #region FOR GRADUATION TRIBUTE
    public string GraduationTribute { set { lblGraduationType.Text = value; } }
    public string GraduationTodayTrial { set { lblGraduationTodayTrial.Text = value; } }
    public string GraduationToday1Year { set { lblGraduationToday1Yr.Text = value; } }
    public string GraduationTodayExpired { set { lblGraduationTodayExpired.Text = value; } }
    public string GraduationTodayLifeTIme { set { lblGraduationTodayLife.Text = value; } }
    public string GraduationLast30DaysTrial { set { lblGraduationLast30DaysTrial.Text = value; } }
    public string GraduationLast30Days1Year { set { lblGraduationLast30Days1Yr.Text = value; } }
    public string GraduationLast30DaysExpired { set { lblGraduationLast30DaysExpired.Text = value; } }
    public string GraduationLast30DaysLifeTIme { set { lblGraduationLast30DaysLife.Text = value; } }
    public string GraduationTotalTrial { set { lblGraduationTotalTrial.Text = value; } }
    public string GraduationTotal1Year { set { lblGraduationTotal1Yr.Text = value; } }
    public string GraduationTotalExpired { set { lblGraduationTotalExpired.Text = value; } }
    public string GraduationTotalLifeTime { set { lblGraduationTotalLife.Text = value; } }
    #endregion

    #region FOR TOTAL FOR ALL TRIBUTE
    public string Total_Tribute { set { lblTotal_Type.Text = value; } }
    public string Total_TodayTrial { set { lblTotal_TodayTrial.Text = value; } }
    public string Total_Today1Year { set { lblTotal_Today1Yr.Text = value; } }
    public string Total_TodayExpired { set { lblTotal_TodayExpired.Text = value; } }
    public string Total_TodayLifeTIme { set { lblTotal_TodayLife.Text = value; } }
    public string Total_Last30DaysTrial { set { lblTotal_Last30DaysTrial.Text = value; } }
    public string Total_Last30Days1Year { set { lblTotal_Last30Days1Yr.Text = value; } }
    public string Total_Last30DaysExpired { set { lblTotal_Last30DaysExpired.Text = value; } }
    public string Total_Last30DaysLifeTIme { set { lblTotal_Last30DaysLife.Text = value; } }
    public string Total_TotalTrial { set { lblTotal_TotalTrial.Text = value; } }
    public string Total_Total1Year { set { lblTotal_Total1Yr.Text = value; } }
    public string Total_TotalExpired { set { lblTotal_TotalExpired.Text = value; } }
    public string Total_TotalLifeTime { set { lblTotal_TotalLife.Text = value; } }
    #endregion
    #endregion
}


