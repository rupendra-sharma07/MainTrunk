///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.TributePortalAdmin.Views.IPortalSummaryReport.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Portal Summary Report
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.TributePortalAdmin.Views
{
    public interface IPortalSummaryReport
    {
        string PersonalTodayNew { set; }
        string PersonalTodayExpired { set; }
        string BusinessTodayNew { set; }
        string BusniessTodayExpired { set; }
        string TotalTodayNew { set; }
        string TotalTodayExpired { set; }

        string Personal30DaysNew { set; }
        string Personal30DaysExpired { set; }
        string Business30DaysNew { set; }
        string Busniess30DaysExpired { set; }
        string Total30DaysNew { set; }
        string Total30DaysExpired { set; }

        string TotalPesonalActiveAccounts { set; }
        string TotalBusinessActiveAccounts { set; }
        string TotalActiveAccounts { set; }

        #region For TRIBUTE ACTIVITY REPORT
        #region NEWBABY
        string NewBabyTribute { set;}
        string NewBabyTodayTrial { set;}
        string NewBabyToday1Year { set;}
        string NewBabyTodayExpired { set;}
        string NewBabyTodayLifeTIme { set;}
        string NewBabyLast30DaysTrial { set;}
        string NewBabyLast30Days1Year { set;}
        string NewBabyLast30DaysExpired { set;}
        string NewBabyLast30DaysLifeTIme { set;}
        string NewBabyTotalTrial { set;}
        string NewBabyTotal1Year { set;}
        string NewBabyTotalExpired { set;}
        string NewBabyTotalLifeTime { set;}
        #endregion

        #region BIRTHDAY
        string BirthdayTribute { set;}
        string BirthdayTodayTrial { set;}
        string BirthdayToday1Year { set;}
        string BirthdayTodayExpired { set;}
        string BirthdayTodayLifeTIme { set;}
        string BirthdayLast30DaysTrial { set;}
        string BirthdayLast30Days1Year { set;}
        string BirthdayLast30DaysExpired { set;}
        string BirthdayLast30DaysLifeTIme { set;}
        string BirthdayTotalTrial { set;}
        string BirthdayTotal1Year { set;}
        string BirthdayTotalExpired { set;}
        string BirthdayTotalLifeTime { set;}
        #endregion

        #region GRADUATION
        string GraduationTribute { set;}
        string GraduationTodayTrial { set;}
        string GraduationToday1Year { set;}
        string GraduationTodayExpired { set;}
        string GraduationTodayLifeTIme { set;}
        string GraduationLast30DaysTrial { set;}
        string GraduationLast30Days1Year { set;}
        string GraduationLast30DaysExpired { set;}
        string GraduationLast30DaysLifeTIme { set;}
        string GraduationTotalTrial { set;}
        string GraduationTotal1Year { set;}
        string GraduationTotalExpired { set;}
        string GraduationTotalLifeTime { set;}
        #endregion

        #region WEDDING
        string WeddingTribute { set;}
        string WeddingTodayTrial { set;}
        string WeddingToday1Year { set;}
        string WeddingTodayExpired { set;}
        string WeddingTodayLifeTIme { set;}
        string WeddingLast30DaysTrial { set;}
        string WeddingLast30Days1Year { set;}
        string WeddingLast30DaysExpired { set;}
        string WeddingLast30DaysLifeTIme { set;}
        string WeddingTotalTrial { set;}
        string WeddingTotal1Year { set;}
        string WeddingTotalExpired { set;}
        string WeddingTotalLifeTime { set;}
        #endregion

        #region ANNIVERSARY
        string AnniversaryTribute { set;}
        string AnniversaryTodayTrial { set;}
        string AnniversaryToday1Year { set;}
        string AnniversaryTodayExpired { set;}
        string AnniversaryTodayLifeTIme { set;}
        string AnniversaryLast30DaysTrial { set;}
        string AnniversaryLast30Days1Year { set;}
        string AnniversaryLast30DaysExpired { set;}
        string AnniversaryLast30DaysLifeTIme { set;}
        string AnniversaryTotalTrial { set;}
        string AnniversaryTotal1Year { set;}
        string AnniversaryTotalExpired { set;}
        string AnniversaryTotalLifeTime { set;}
        #endregion

        #region MEMORIAL
        string MemorialTribute { set;}
        string MemorialTodayTrial { set;}
        string MemorialToday1Year { set;}
        string MemorialTodayExpired { set;}
        string MemorialTodayLifeTIme { set;}
        string MemorialLast30DaysTrial { set;}
        string MemorialLast30Days1Year { set;}
        string MemorialLast30DaysExpired { set;}
        string MemorialLast30DaysLifeTIme { set;}
        string MemorialTotalTrial { set;}
        string MemorialTotal1Year { set;}
        string MemorialTotalExpired { set;}
        string MemorialTotalLifeTime { set;}
        #endregion

        #region TOTAL OF ALL
        string Total_Tribute { set;}
        string Total_TodayTrial { set;}
        string Total_Today1Year { set;}
        string Total_TodayExpired { set;}
        string Total_TodayLifeTIme { set;}
        string Total_Last30DaysTrial { set;}
        string Total_Last30Days1Year { set;}
        string Total_Last30DaysExpired { set;}
        string Total_Last30DaysLifeTIme { set;}
        string Total_TotalTrial { set;}
        string Total_Total1Year { set;}
        string Total_TotalExpired { set;}
        string Total_TotalLifeTime { set;}
        #endregion
        #endregion

    }

}




