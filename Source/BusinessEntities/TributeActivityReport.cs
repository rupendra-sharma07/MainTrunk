///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.TributeActivityReport.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details for the 
///                  tribute activity
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public partial class TributeActivityReport
    {
        #region FIEDLS
        private string _tributeTypeName;
        private string _tributeType;
        private string _todayTrial;
        private string _todayExpired;
        private string _today1Year;
        private string _todayLifeTime;
        private string _last30DaysTrial;
        private string _last30DaysExpired;
        private string _last30Days1Year;
        private string _last30DaysLifeTime;
        private string _totalTrial;
        private string _totalExpired;
        private string _total1Year;
        private string _totalLifeTime;
        private int _totalAccountTrial_Today;
        private int _totalAccountExpired_Today;
        private int _totalAccount1Year_Today;
        private int _totalAccountLifeTime_Today;
        private int _totalAccountTrial_30Days;
        private int _totalAccountExpired_30Days;
        private int _totalAccount1Year_30Days;
        private int _totalAccountLifeTime_30Days;
        private int _totalAccountTrial_Total;
        private int _totalAccountExpired_Total;
        private int _totalAccount1Year_Total;
        private int _totalAccountLifeTime_Total;
        #endregion

        #region PROEPRTIES
        public string TributeTypeName
        {
            get { return _tributeTypeName; }
            set { _tributeTypeName = value; }
        }
        public string TributeType
        {
            get { return _tributeType; }
            set { _tributeType = value; }
        }
        public string TodayTrial
        {
            get { return _todayTrial; }
            set { _todayTrial = value; }
        }
        public string TodayExpired
        {
            get { return _todayExpired; }
            set { _todayExpired = value; }
        }
        public string Today1Year
        {
            get { return _today1Year; }
            set { _today1Year = value; }
        }
        public string TodayLifeTime
        {
            get { return _todayLifeTime; }
            set { _todayLifeTime = value; }
        }
        public string Last30DaysTrial
        {
            get { return _last30DaysTrial; }
            set { _last30DaysTrial = value; }
        }
        public string Last30DaysExpired
        {
            get { return _last30DaysExpired; }
            set { _last30DaysExpired = value; }
        }
        public string Last30Days1Year
        {
            get { return _last30Days1Year; }
            set { _last30Days1Year = value; }
        }
        public string Last30DaysLifeTime
        {
            get { return _last30DaysLifeTime; }
            set { _last30DaysLifeTime = value; }
        }
        public string TotalTrial
        {
            get { return _totalTrial; }
            set { _totalTrial = value; }
        }
        public string TotalExpired
        {
            get { return _totalExpired; }
            set { _totalExpired = value; }
        }
        public string Total1Year
        {
            get { return _total1Year; }
            set { _total1Year = value; }
        }
        public string TotalLifeTime
        {
            get { return _totalLifeTime; }
            set { _totalLifeTime = value; }
        }
        public int TotalAccountTrial_Today
        {
            get { return _totalAccountTrial_Today; }
            set { _totalAccountTrial_Today = value; }
        }
        public int TotalAccountExpired_Today
        {
            get { return _totalAccountExpired_Today; }
            set { _totalAccountExpired_Today = value; }
        }
        public int TotalAccount1Year_Today
        {
            get { return _totalAccount1Year_Today; }
            set { _totalAccount1Year_Today = value; }
        }
        public int TotalAccountLifeTime_Today
        {
            get { return _totalAccountLifeTime_Today; }
            set { _totalAccountLifeTime_Today = value; }
        }
        public int TotalAccountTrial_30Days
        {
            get { return _totalAccountTrial_30Days; }
            set { _totalAccountTrial_30Days = value; }
        }
        public int TotalAccountExpired_30Days
        {
            get { return _totalAccountExpired_30Days; }
            set { _totalAccountExpired_30Days = value; }
        }
        public int TotalAccount1Year_30Days
        {
            get { return _totalAccount1Year_30Days; }
            set { _totalAccount1Year_30Days = value; }
        }
        public int TotalAccountLifeTime_30Days
        {
            get { return _totalAccountLifeTime_30Days; }
            set { _totalAccountLifeTime_30Days = value; }
        }
        public int TotalAccountTrial_Total
        {
            get { return _totalAccountTrial_Total; }
            set { _totalAccountTrial_Total = value; }
        }
        public int TotalAccountExpired_Total
        {
            get { return _totalAccountExpired_Total; }
            set { _totalAccountExpired_Total = value; }
        }
        public int TotalAccount1Year_Total
        {
            get { return _totalAccount1Year_Total; }
            set { _totalAccount1Year_Total = value; }
        }
        public int TotalAccountLifeTime_Total
        {
            get { return _totalAccountLifeTime_Total; }
            set { _totalAccountLifeTime_Total = value; }
        }
        #endregion
    }
}
