///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.TributeActivityResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Tribute Activity
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
#endregion

namespace TributesPortal.ResourceAccess
{
    public partial class TributeActivityResource : PortalResourceAccess
    {
        /// <summary>
        /// Method to get the tribute activity list.
        /// </summary>
        /// <returns>Filled TributeActivityReport entity.</returns>
        public List<TributeActivityReport> GetTributeActivityReport(string applicationType)
        {
            List<TributeActivityReport> objActivityList = new List<TributeActivityReport>();
            object[] param = { applicationType };
            DataSet dsTributeActivity = GetDataSet("usp_GetTributeActivityReport", param);

            /********** For New Baby Tribute Type *************/
            TributeActivityReport objTributeActivity1 = new TributeActivityReport();
            //For Today
            if (dsTributeActivity.Tables[0].Rows.Count > 0)
            {
                objTributeActivity1.TributeTypeName = "New Baby " + WebConfig.ApplicationWordForInternalUse.ToString() + "s:";
                objTributeActivity1.TributeType = dsTributeActivity.Tables[0].Rows[0]["TributeType"].ToString();
                objTributeActivity1.TodayTrial = dsTributeActivity.Tables[0].Rows[0]["Free"].ToString();
                objTributeActivity1.Today1Year = dsTributeActivity.Tables[0].Rows[0]["1Year"].ToString();
                objTributeActivity1.TodayExpired = dsTributeActivity.Tables[0].Rows[0]["Expired"].ToString();
                objTributeActivity1.TodayLifeTime = dsTributeActivity.Tables[0].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity1.TributeTypeName = "New Baby " + WebConfig.ApplicationWordForInternalUse.ToString() + "s:";
                objTributeActivity1.TributeType = "2";
                objTributeActivity1.TodayTrial = "0";
                objTributeActivity1.Today1Year = "0";
                objTributeActivity1.TodayExpired = "0";
                objTributeActivity1.TodayLifeTime = "0";
            }
            //For 30 days back
            if (dsTributeActivity.Tables[1].Rows.Count > 0)
            {
                objTributeActivity1.Last30DaysTrial = dsTributeActivity.Tables[1].Rows[0]["Free"].ToString();
                objTributeActivity1.Last30Days1Year = dsTributeActivity.Tables[1].Rows[0]["1Year"].ToString();
                objTributeActivity1.Last30DaysExpired = dsTributeActivity.Tables[1].Rows[0]["Expired"].ToString();
                objTributeActivity1.Last30DaysLifeTime = dsTributeActivity.Tables[1].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity1.Last30DaysTrial = "0";
                objTributeActivity1.Last30Days1Year = "0";
                objTributeActivity1.Last30DaysExpired = "0";
                objTributeActivity1.Last30DaysLifeTime = "0";
            }

            //For Total
            if (dsTributeActivity.Tables[2].Rows.Count > 0)
            {
                objTributeActivity1.TotalTrial = dsTributeActivity.Tables[2].Rows[0]["Free"].ToString();
                objTributeActivity1.Total1Year = dsTributeActivity.Tables[2].Rows[0]["1Year"].ToString();
                objTributeActivity1.TotalExpired = dsTributeActivity.Tables[2].Rows[0]["Expired"].ToString();
                objTributeActivity1.TotalLifeTime = dsTributeActivity.Tables[2].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity1.TotalTrial = "0";
                objTributeActivity1.Total1Year = "0";
                objTributeActivity1.TotalExpired = "0";
                objTributeActivity1.TotalLifeTime = "0";
            }
            objActivityList.Add(objTributeActivity1);

            /********** For Birthday Tribute Type *************/
            TributeActivityReport objTributeActivity2 = new TributeActivityReport();
            //For Today
            if (dsTributeActivity.Tables[3].Rows.Count > 0)
            {
                objTributeActivity2.TributeTypeName = "Birthday " + WebConfig.ApplicationWordForInternalUse.ToString() + "s:";
                objTributeActivity2.TributeType = dsTributeActivity.Tables[3].Rows[0]["TributeType"].ToString();
                objTributeActivity2.TodayTrial = dsTributeActivity.Tables[3].Rows[0]["Free"].ToString();
                objTributeActivity2.Today1Year = dsTributeActivity.Tables[3].Rows[0]["1Year"].ToString();
                objTributeActivity2.TodayExpired = dsTributeActivity.Tables[3].Rows[0]["Expired"].ToString();
                objTributeActivity2.TodayLifeTime = dsTributeActivity.Tables[3].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity2.TributeTypeName = "Birthday " + WebConfig.ApplicationWordForInternalUse.ToString() + "s:";
                objTributeActivity2.TributeType = "3";
                objTributeActivity2.TodayTrial = "0";
                objTributeActivity2.Today1Year = "0";
                objTributeActivity2.TodayExpired = "0";
                objTributeActivity2.TodayLifeTime = "0";
            }
            //For 30 days back
            if (dsTributeActivity.Tables[4].Rows.Count > 0)
            {
                objTributeActivity2.Last30DaysTrial = dsTributeActivity.Tables[4].Rows[0]["Free"].ToString();
                objTributeActivity2.Last30Days1Year = dsTributeActivity.Tables[4].Rows[0]["1Year"].ToString();
                objTributeActivity2.Last30DaysExpired = dsTributeActivity.Tables[4].Rows[0]["Expired"].ToString();
                objTributeActivity2.Last30DaysLifeTime = dsTributeActivity.Tables[4].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity2.Last30DaysTrial = "0";
                objTributeActivity2.Last30Days1Year = "0";
                objTributeActivity2.Last30DaysExpired = "0";
                objTributeActivity2.Last30DaysLifeTime = "0";
            }
            //For Total
            if (dsTributeActivity.Tables[5].Rows.Count > 0)
            {
                objTributeActivity2.TotalTrial = dsTributeActivity.Tables[5].Rows[0]["Free"].ToString();
                objTributeActivity2.Total1Year = dsTributeActivity.Tables[5].Rows[0]["1Year"].ToString();
                objTributeActivity2.TotalExpired = dsTributeActivity.Tables[5].Rows[0]["Expired"].ToString();
                objTributeActivity2.TotalLifeTime = dsTributeActivity.Tables[5].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity2.TotalTrial = "0";
                objTributeActivity2.Total1Year = "0";
                objTributeActivity2.TotalExpired = "0";
                objTributeActivity2.TotalLifeTime = "0";
            }
            objActivityList.Add(objTributeActivity2);

            /********** For Graduation Tribute Type *************/
            TributeActivityReport objTributeActivity3 = new TributeActivityReport();
            //For Today
            if (dsTributeActivity.Tables[6].Rows.Count > 0)
            {
                objTributeActivity3.TributeTypeName = "Graduation " + WebConfig.ApplicationWordForInternalUse.ToString() + "s:";
                objTributeActivity3.TributeType = dsTributeActivity.Tables[6].Rows[0]["TributeType"].ToString();
                objTributeActivity3.TodayTrial = dsTributeActivity.Tables[6].Rows[0]["Free"].ToString();
                objTributeActivity3.Today1Year = dsTributeActivity.Tables[6].Rows[0]["1Year"].ToString();
                objTributeActivity3.TodayExpired = dsTributeActivity.Tables[6].Rows[0]["Expired"].ToString();
                objTributeActivity3.TodayLifeTime = dsTributeActivity.Tables[6].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity3.TributeTypeName = "Graduation " + WebConfig.ApplicationWordForInternalUse.ToString() + "s:";
                objTributeActivity3.TributeType = "4";
                objTributeActivity3.TodayTrial = "0";
                objTributeActivity3.Today1Year = "0";
                objTributeActivity3.TodayExpired = "0";
                objTributeActivity3.TodayLifeTime = "0";
            }
            //For 30 days back
            if (dsTributeActivity.Tables[7].Rows.Count > 0)
            {
                objTributeActivity3.Last30DaysTrial = dsTributeActivity.Tables[7].Rows[0]["Free"].ToString();
                objTributeActivity3.Last30Days1Year = dsTributeActivity.Tables[7].Rows[0]["1Year"].ToString();
                objTributeActivity3.Last30DaysExpired = dsTributeActivity.Tables[7].Rows[0]["Expired"].ToString();
                objTributeActivity3.Last30DaysLifeTime = dsTributeActivity.Tables[7].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity3.Last30DaysTrial = "0";
                objTributeActivity3.Last30Days1Year = "0";
                objTributeActivity3.Last30DaysExpired = "0";
                objTributeActivity3.Last30DaysLifeTime = "0";
            }
            //For Total
            if (dsTributeActivity.Tables[8].Rows.Count > 0)
            {
                objTributeActivity3.TotalTrial = dsTributeActivity.Tables[8].Rows[0]["Free"].ToString();
                objTributeActivity3.Total1Year = dsTributeActivity.Tables[8].Rows[0]["1Year"].ToString();
                objTributeActivity3.TotalExpired = dsTributeActivity.Tables[8].Rows[0]["Expired"].ToString();
                objTributeActivity3.TotalLifeTime = dsTributeActivity.Tables[8].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity3.TotalTrial = "0";
                objTributeActivity3.Total1Year = "0";
                objTributeActivity3.TotalExpired = "0";
                objTributeActivity3.TotalLifeTime = "0";
            }
            objActivityList.Add(objTributeActivity3);

            /********** For Wedding Tribute Type *************/
            TributeActivityReport objTributeActivity4 = new TributeActivityReport();
            //For Today
            if (dsTributeActivity.Tables[9].Rows.Count > 0)
            {
                objTributeActivity4.TributeTypeName = "Wedding " + WebConfig.ApplicationWordForInternalUse.ToString() + "s:";
                objTributeActivity4.TributeType = dsTributeActivity.Tables[9].Rows[0]["TributeType"].ToString();
                objTributeActivity4.TodayTrial = dsTributeActivity.Tables[9].Rows[0]["Free"].ToString();
                objTributeActivity4.Today1Year = dsTributeActivity.Tables[9].Rows[0]["1Year"].ToString();
                objTributeActivity4.TodayExpired = dsTributeActivity.Tables[9].Rows[0]["Expired"].ToString();
                objTributeActivity4.TodayLifeTime = dsTributeActivity.Tables[9].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity4.TributeTypeName = "Wedding " + WebConfig.ApplicationWordForInternalUse.ToString() + "s:";
                objTributeActivity4.TributeType = "5";
                objTributeActivity4.TodayTrial = "0";
                objTributeActivity4.Today1Year = "0";
                objTributeActivity4.TodayExpired = "0";
                objTributeActivity4.TodayLifeTime = "0";
            }
            //For 30 days back
            if (dsTributeActivity.Tables[10].Rows.Count > 0)
            {
                objTributeActivity4.Last30DaysTrial = dsTributeActivity.Tables[10].Rows[0]["Free"].ToString();
                objTributeActivity4.Last30Days1Year = dsTributeActivity.Tables[10].Rows[0]["1Year"].ToString();
                objTributeActivity4.Last30DaysExpired = dsTributeActivity.Tables[10].Rows[0]["Expired"].ToString();
                objTributeActivity4.Last30DaysLifeTime = dsTributeActivity.Tables[10].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity4.Last30DaysTrial = "0";
                objTributeActivity4.Last30Days1Year = "0";
                objTributeActivity4.Last30DaysExpired = "0";
                objTributeActivity4.Last30DaysLifeTime = "0";
            }
            //For Total
            if (dsTributeActivity.Tables[11].Rows.Count > 0)
            {
                objTributeActivity4.TotalTrial = dsTributeActivity.Tables[11].Rows[0]["Free"].ToString();
                objTributeActivity4.Total1Year = dsTributeActivity.Tables[11].Rows[0]["1Year"].ToString();
                objTributeActivity4.TotalExpired = dsTributeActivity.Tables[11].Rows[0]["Expired"].ToString();
                objTributeActivity4.TotalLifeTime = dsTributeActivity.Tables[11].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity4.TotalTrial = "0";
                objTributeActivity4.Total1Year = "0";
                objTributeActivity4.TotalExpired = "0";
                objTributeActivity4.TotalLifeTime = "0";
            }
            objActivityList.Add(objTributeActivity4);

            /********** For Anniversary Tribute Type *************/
            TributeActivityReport objTributeActivity5 = new TributeActivityReport();
            //For Today
            if (dsTributeActivity.Tables[12].Rows.Count > 0)
            {
                objTributeActivity5.TributeTypeName = "Anniversary " + WebConfig.ApplicationWordForInternalUse.ToString() + "s:";
                objTributeActivity5.TributeType = dsTributeActivity.Tables[12].Rows[0]["TributeType"].ToString();
                objTributeActivity5.TodayTrial = dsTributeActivity.Tables[12].Rows[0]["Free"].ToString();
                objTributeActivity5.Today1Year = dsTributeActivity.Tables[12].Rows[0]["1Year"].ToString();
                objTributeActivity5.TodayExpired = dsTributeActivity.Tables[12].Rows[0]["Expired"].ToString();
                objTributeActivity5.TodayLifeTime = dsTributeActivity.Tables[12].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity5.TributeTypeName = "Anniversary " + WebConfig.ApplicationWordForInternalUse.ToString() + "s:";
                objTributeActivity5.TributeType = "6";
                objTributeActivity5.TodayTrial = "0";
                objTributeActivity5.Today1Year = "0";
                objTributeActivity5.TodayExpired = "0";
                objTributeActivity5.TodayLifeTime = "0";
            }
            //For 30 days back
            if (dsTributeActivity.Tables[13].Rows.Count > 0)
            {
                objTributeActivity5.Last30DaysTrial = dsTributeActivity.Tables[13].Rows[0]["Free"].ToString();
                objTributeActivity5.Last30Days1Year = dsTributeActivity.Tables[13].Rows[0]["1Year"].ToString();
                objTributeActivity5.Last30DaysExpired = dsTributeActivity.Tables[13].Rows[0]["Expired"].ToString();
                objTributeActivity5.Last30DaysLifeTime = dsTributeActivity.Tables[13].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity5.Last30DaysTrial = "0";
                objTributeActivity5.Last30Days1Year = "0";
                objTributeActivity5.Last30DaysExpired = "0";
                objTributeActivity5.Last30DaysLifeTime = "0";
            }
            //For Total
            if (dsTributeActivity.Tables[14].Rows.Count > 0)
            {
                objTributeActivity5.TotalTrial = dsTributeActivity.Tables[14].Rows[0]["Free"].ToString();
                objTributeActivity5.Total1Year = dsTributeActivity.Tables[14].Rows[0]["1Year"].ToString();
                objTributeActivity5.TotalExpired = dsTributeActivity.Tables[14].Rows[0]["Expired"].ToString();
                objTributeActivity5.TotalLifeTime = dsTributeActivity.Tables[14].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity5.TotalTrial = "0";
                objTributeActivity5.Total1Year = "0";
                objTributeActivity5.TotalExpired = "0";
                objTributeActivity5.TotalLifeTime = "0";
            }
            objActivityList.Add(objTributeActivity5);


            /********** For Memorial Tribute Type *************/
            TributeActivityReport objTributeActivity6 = new TributeActivityReport();
            //For Today
            if (dsTributeActivity.Tables[15].Rows.Count > 0)
            {
                objTributeActivity6.TributeTypeName = "Memorial " + WebConfig.ApplicationWordForInternalUse.ToString() + "s:";
                objTributeActivity6.TributeType = dsTributeActivity.Tables[15].Rows[0]["TributeType"].ToString();
                objTributeActivity6.TodayTrial = dsTributeActivity.Tables[15].Rows[0]["Free"].ToString();
                objTributeActivity6.Today1Year = dsTributeActivity.Tables[15].Rows[0]["1Year"].ToString();
                objTributeActivity6.TodayExpired = dsTributeActivity.Tables[15].Rows[0]["Expired"].ToString();
                objTributeActivity6.TodayLifeTime = dsTributeActivity.Tables[15].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity6.TributeTypeName = "Memorial " + WebConfig.ApplicationWordForInternalUse.ToString() + "s:";
                objTributeActivity6.TributeType = "7";
                objTributeActivity6.TodayTrial = "0";
                objTributeActivity6.Today1Year = "0";
                objTributeActivity6.TodayExpired = "0";
                objTributeActivity6.TodayLifeTime = "0";
            }
            //For 30 days back
            if (dsTributeActivity.Tables[16].Rows.Count > 0)
            {
                objTributeActivity6.Last30DaysTrial = dsTributeActivity.Tables[16].Rows[0]["Free"].ToString();
                objTributeActivity6.Last30Days1Year = dsTributeActivity.Tables[16].Rows[0]["1Year"].ToString();
                objTributeActivity6.Last30DaysExpired = dsTributeActivity.Tables[16].Rows[0]["Expired"].ToString();
                objTributeActivity6.Last30DaysLifeTime = dsTributeActivity.Tables[16].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity6.Last30DaysTrial = "0";
                objTributeActivity6.Last30Days1Year = "0";
                objTributeActivity6.Last30DaysExpired = "0";
                objTributeActivity6.Last30DaysLifeTime = "0";
            }
            //For Total
            if (dsTributeActivity.Tables[17].Rows.Count > 0)
            {
                objTributeActivity6.TotalTrial = dsTributeActivity.Tables[17].Rows[0]["Free"].ToString();
                objTributeActivity6.Total1Year = dsTributeActivity.Tables[17].Rows[0]["1Year"].ToString();
                objTributeActivity6.TotalExpired = dsTributeActivity.Tables[17].Rows[0]["Expired"].ToString();
                objTributeActivity6.TotalLifeTime = dsTributeActivity.Tables[17].Rows[0]["LifeTime"].ToString();
            }
            else
            {
                objTributeActivity6.TotalTrial = "0";
                objTributeActivity6.Total1Year = "0";
                objTributeActivity6.TotalExpired = "0";
                objTributeActivity6.TotalLifeTime = "0";
            }
            objActivityList.Add(objTributeActivity6);

            //to get total of all
            TributeActivityReport objAct = new TributeActivityReport();
            foreach (TributeActivityReport obj in objActivityList)
            {
                objAct.TributeTypeName = "TOTAL ACCOUNTS:";

                objAct.TotalAccountTrial_Today += int.Parse(obj.TodayTrial);// +int.Parse(obj.Last30DaysTrial) + int.Parse(obj.TodayExpired) + int.Parse(obj.TotalTrial);
                objAct.TotalAccount1Year_Today += int.Parse(obj.Today1Year);
                objAct.TotalAccountExpired_Today += int.Parse(obj.TodayExpired);
                objAct.TotalAccountLifeTime_Today += int.Parse(obj.TodayLifeTime);

                objAct.TotalAccountTrial_30Days += int.Parse(obj.Last30DaysTrial);
                objAct.TotalAccount1Year_30Days += int.Parse(obj.Last30Days1Year);
                objAct.TotalAccountExpired_30Days += int.Parse(obj.Last30DaysExpired);
                objAct.TotalAccountLifeTime_30Days += int.Parse(obj.Last30DaysLifeTime);

                objAct.TotalAccountTrial_Total += int.Parse(obj.TotalTrial);
                objAct.TotalAccount1Year_Total += int.Parse(obj.Total1Year);
                objAct.TotalAccountExpired_Total += int.Parse(obj.TotalExpired);
                objAct.TotalAccountLifeTime_Total += int.Parse(obj.TotalLifeTime);
            }
            objActivityList.Add(objAct);

            return objActivityList;
        }
    }
}
