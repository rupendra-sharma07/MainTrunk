///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.TributePortalAdmin.Views.PortalSummaryReportPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for generating the reports by the portal admin.
///Audit Trail     : Date of Modification  Modified By         Description
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;

namespace TributesPortal.TributePortalAdmin.Views
{
    public class PortalSummaryReportPresenter : Presenter<IPortalSummaryReport>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        private TributePortalAdminController _controller;
        public PortalSummaryReportPresenter([CreateNew] TributePortalAdminController controller)
        {
            _controller = controller;
        }

        // commented by ud to pass parameter to function
        public override void OnViewLoaded()
        {
            //UsersSummaryReport objSummary = new UsersSummaryReport();
            //_controller.UserSummaryReport(objSummary);
            //View.PersonalTodayNew = objSummary.PersonalAccountTodayNew;
            //View.PersonalTodayExpired = objSummary.PersonalAccountTodayExpired;
            //View.Personal30DaysNew = objSummary.PersonalAccount30DaysNew;
            //View.Personal30DaysExpired = objSummary.PersonalAccount30DaysExpired;
            //View.BusinessTodayNew = objSummary.BusinessAccountTodayNew;
            //View.BusniessTodayExpired = objSummary.BusinessAccountTodayExpired;
            //View.Business30DaysNew = objSummary.BusinessAccount30DaysNew;
            //View.Busniess30DaysExpired = objSummary.BusinessAccount30DaysExpired;
            //View.TotalTodayNew = objSummary.TotalAccountTodayNew;
            //View.TotalTodayExpired = objSummary.TotalAccountTodayExpired;
            //View.Total30DaysNew = objSummary.TotalAccount30DaysNew;
            //View.Total30DaysExpired = objSummary.TotalAccount30DaysExpired;
            //View.TotalPesonalActiveAccounts = objSummary.PersonalTotalActiveAccount;
            //View.TotalBusinessActiveAccounts = objSummary.BusinessTotalActiveAccount;
            //View.TotalActiveAccounts = objSummary.TotalActiveAccounts;
        }
        public void GetUserActivityReport(string applicationType)
        {
            UsersSummaryReport objSummary = new UsersSummaryReport();
            _controller.UserSummaryReport(objSummary, applicationType);
            View.PersonalTodayNew = objSummary.PersonalAccountTodayNew;
            View.PersonalTodayExpired = objSummary.PersonalAccountTodayExpired;
            View.Personal30DaysNew = objSummary.PersonalAccount30DaysNew;
            View.Personal30DaysExpired = objSummary.PersonalAccount30DaysExpired;
            View.BusinessTodayNew = objSummary.BusinessAccountTodayNew;
            View.BusniessTodayExpired = objSummary.BusinessAccountTodayExpired;
            View.Business30DaysNew = objSummary.BusinessAccount30DaysNew;
            View.Busniess30DaysExpired = objSummary.BusinessAccount30DaysExpired;
            View.TotalTodayNew = objSummary.TotalAccountTodayNew;
            View.TotalTodayExpired = objSummary.TotalAccountTodayExpired;
            View.Total30DaysNew = objSummary.TotalAccount30DaysNew;
            View.Total30DaysExpired = objSummary.TotalAccount30DaysExpired;
            View.TotalPesonalActiveAccounts = objSummary.PersonalTotalActiveAccount;
            View.TotalBusinessActiveAccounts = objSummary.BusinessTotalActiveAccount;
            View.TotalActiveAccounts = objSummary.TotalActiveAccounts;
        }
        
        public override void OnViewInitialized()
        {
            // TODO: Implement code that will be executed the first time the view loads
        }

        public void GetTributeActivityReport(string applicationType)
        {
            List<TributeActivityReport> objReportList = new List<TributeActivityReport>();
            objReportList = _controller.GetTributeActivityReport(applicationType);
            foreach (TributeActivityReport objReport in objReportList)
            {

                if (objReport.TributeType == "2")
                {
                    #region FOR NEWBABY
                    this.View.NewBabyTribute = objReport.TributeTypeName;
                    this.View.NewBabyToday1Year = objReport.Today1Year;
                    this.View.NewBabyTodayExpired = objReport.TodayExpired;
                    this.View.NewBabyTodayLifeTIme = objReport.TodayLifeTime;
                    this.View.NewBabyTodayTrial = objReport.TodayTrial;
                    this.View.NewBabyLast30Days1Year = objReport.Last30Days1Year;
                    this.View.NewBabyLast30DaysExpired = objReport.Last30DaysExpired;
                    this.View.NewBabyLast30DaysLifeTIme = objReport.Last30DaysLifeTime;
                    this.View.NewBabyLast30DaysTrial = objReport.Last30DaysTrial;
                    this.View.NewBabyTotal1Year = objReport.Total1Year;
                    this.View.NewBabyTotalExpired = objReport.TotalExpired;
                    this.View.NewBabyTotalLifeTime = objReport.TotalLifeTime;
                    this.View.NewBabyTotalTrial = objReport.TotalTrial;
                    #endregion
                }
                else if (objReport.TributeType == "3")
                {
                    #region FOR BIRTHDAY
                    this.View.BirthdayTribute = objReport.TributeTypeName;
                    this.View.BirthdayToday1Year = objReport.Today1Year;
                    this.View.BirthdayTodayExpired = objReport.TodayExpired;
                    this.View.BirthdayTodayLifeTIme = objReport.TodayLifeTime;
                    this.View.BirthdayTodayTrial = objReport.TodayTrial;
                    this.View.BirthdayLast30Days1Year = objReport.Last30Days1Year;
                    this.View.BirthdayLast30DaysExpired = objReport.Last30DaysExpired;
                    this.View.BirthdayLast30DaysLifeTIme = objReport.Last30DaysLifeTime;
                    this.View.BirthdayLast30DaysTrial = objReport.Last30DaysTrial;
                    this.View.BirthdayTotal1Year = objReport.Total1Year;
                    this.View.BirthdayTotalExpired = objReport.TotalExpired;
                    this.View.BirthdayTotalLifeTime = objReport.TotalLifeTime;
                    this.View.BirthdayTotalTrial = objReport.TotalTrial;
                    #endregion
                }
                else if (objReport.TributeType == "4")
                {
                    #region FOR GRADUATION
                    this.View.GraduationTribute = objReport.TributeTypeName;
                    this.View.GraduationToday1Year = objReport.Today1Year;
                    this.View.GraduationTodayExpired = objReport.TodayExpired;
                    this.View.GraduationTodayLifeTIme = objReport.TodayLifeTime;
                    this.View.GraduationTodayTrial = objReport.TodayTrial;
                    this.View.GraduationLast30Days1Year = objReport.Last30Days1Year;
                    this.View.GraduationLast30DaysExpired = objReport.Last30DaysExpired;
                    this.View.GraduationLast30DaysLifeTIme = objReport.Last30DaysLifeTime;
                    this.View.GraduationLast30DaysTrial = objReport.Last30DaysTrial;
                    this.View.GraduationTotal1Year = objReport.Total1Year;
                    this.View.GraduationTotalExpired = objReport.TotalExpired;
                    this.View.GraduationTotalLifeTime = objReport.TotalLifeTime;
                    this.View.GraduationTotalTrial = objReport.TotalTrial;
                    #endregion
                }
                else if (objReport.TributeType == "5")
                {
                    #region FOR WEDDING
                    this.View.WeddingTribute = objReport.TributeTypeName;
                    this.View.WeddingToday1Year = objReport.Today1Year;
                    this.View.WeddingTodayExpired = objReport.TodayExpired;
                    this.View.WeddingTodayLifeTIme = objReport.TodayLifeTime;
                    this.View.WeddingTodayTrial = objReport.TodayTrial;
                    this.View.WeddingLast30Days1Year = objReport.Last30Days1Year;
                    this.View.WeddingLast30DaysExpired = objReport.Last30DaysExpired;
                    this.View.WeddingLast30DaysLifeTIme = objReport.Last30DaysLifeTime;
                    this.View.WeddingLast30DaysTrial = objReport.Last30DaysTrial;
                    this.View.WeddingTotal1Year = objReport.Total1Year;
                    this.View.WeddingTotalExpired = objReport.TotalExpired;
                    this.View.WeddingTotalLifeTime = objReport.TotalLifeTime;
                    this.View.WeddingTotalTrial = objReport.TotalTrial;
                    #endregion
                }
                else if (objReport.TributeType == "6")
                {
                    #region FOR ANNIVERSARY
                    this.View.AnniversaryTribute = objReport.TributeTypeName;
                    this.View.AnniversaryToday1Year = objReport.Today1Year;
                    this.View.AnniversaryTodayExpired = objReport.TodayExpired;
                    this.View.AnniversaryTodayLifeTIme = objReport.TodayLifeTime;
                    this.View.AnniversaryTodayTrial = objReport.TodayTrial;
                    this.View.AnniversaryLast30Days1Year = objReport.Last30Days1Year;
                    this.View.AnniversaryLast30DaysExpired = objReport.Last30DaysExpired;
                    this.View.AnniversaryLast30DaysLifeTIme = objReport.Last30DaysLifeTime;
                    this.View.AnniversaryLast30DaysTrial = objReport.Last30DaysTrial;
                    this.View.AnniversaryTotal1Year = objReport.Total1Year;
                    this.View.AnniversaryTotalExpired = objReport.TotalExpired;
                    this.View.AnniversaryTotalLifeTime = objReport.TotalLifeTime;
                    this.View.AnniversaryTotalTrial = objReport.TotalTrial;
                    #endregion
                }
                else if (objReport.TributeType == "7")
                {
                    #region FOR MEMORIAL
                    this.View.MemorialTribute = objReport.TributeTypeName;
                    this.View.MemorialToday1Year = objReport.Today1Year;
                    this.View.MemorialTodayExpired = objReport.TodayExpired;
                    this.View.MemorialTodayLifeTIme = objReport.TodayLifeTime;
                    this.View.MemorialTodayTrial = objReport.TodayTrial;
                    this.View.MemorialLast30Days1Year = objReport.Last30Days1Year;
                    this.View.MemorialLast30DaysExpired = objReport.Last30DaysExpired;
                    this.View.MemorialLast30DaysLifeTIme = objReport.Last30DaysLifeTime;
                    this.View.MemorialLast30DaysTrial = objReport.Last30DaysTrial;
                    this.View.MemorialTotal1Year = objReport.Total1Year;
                    this.View.MemorialTotalExpired = objReport.TotalExpired;
                    this.View.MemorialTotalLifeTime = objReport.TotalLifeTime;
                    this.View.MemorialTotalTrial = objReport.TotalTrial;
                    #endregion
                }
                else
                {
                    #region FOR TOTAL OF ALL
                    this.View.Total_Tribute = objReport.TributeTypeName;
                    this.View.Total_Today1Year = objReport.TotalAccount1Year_Today.ToString();
                    this.View.Total_TodayExpired = objReport.TotalAccountExpired_Today.ToString();
                    this.View.Total_TodayLifeTIme = objReport.TotalAccountLifeTime_Today.ToString();
                    this.View.Total_TodayTrial = objReport.TotalAccountTrial_Today.ToString();
                    this.View.Total_Last30Days1Year = objReport.TotalAccount1Year_30Days.ToString();
                    this.View.Total_Last30DaysExpired = objReport.TotalAccountExpired_30Days.ToString();
                    this.View.Total_Last30DaysLifeTIme = objReport.TotalAccountLifeTime_30Days.ToString();
                    this.View.Total_Last30DaysTrial = objReport.TotalAccountTrial_30Days.ToString();
                    this.View.Total_Total1Year = objReport.TotalAccount1Year_Total.ToString();
                    this.View.Total_TotalExpired = objReport.TotalAccountExpired_Total.ToString();
                    this.View.Total_TotalLifeTime = objReport.TotalAccountLifeTime_Total.ToString();
                    this.View.Total_TotalTrial = objReport.TotalAccountTrial_Total.ToString();
                    #endregion
                }

            }
        }
    }
}




