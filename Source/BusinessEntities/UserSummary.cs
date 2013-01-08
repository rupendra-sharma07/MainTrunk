///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.UsersSummaryReport.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the 
///                     Details about Users Summary Report
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class UsersSummaryReport
    {
        public UsersSummaryReport()
        { }

        private DataSet _UserSummary;
        public DataSet UserSummary
        {
            get { return _UserSummary; }
            set { _UserSummary = value; }
        }

        private string _PersonalAccountTodayNew;
        public string PersonalAccountTodayNew
        {
            get {return _PersonalAccountTodayNew;}
            set {_PersonalAccountTodayNew = value; }
        }
        private string _PersonalAccountTodayExpired;
        public string PersonalAccountTodayExpired
        {
            get { return _PersonalAccountTodayExpired; }
            set { _PersonalAccountTodayExpired = value; }
        }
        private string _PersonalAccount30DaysNew;
        public string PersonalAccount30DaysNew
        {
            get { return _PersonalAccount30DaysNew; }
            set { _PersonalAccount30DaysNew = value; }
        }
        private string _PersonalAccount30DaysExpired;
        public string PersonalAccount30DaysExpired
        {
            get { return _PersonalAccount30DaysExpired; }
            set { _PersonalAccount30DaysExpired = value; }
        }
        private string _PersonalTotalActiveAccount;
        public string PersonalTotalActiveAccount
        {
            get { return _PersonalTotalActiveAccount; }
            set { _PersonalTotalActiveAccount = value; }
        }

        private string _BusinessAccountTodayNew;
        public string BusinessAccountTodayNew
        {
            get { return _BusinessAccountTodayNew; }
            set { _BusinessAccountTodayNew = value; }
        }

        private string _BusinessAccountTodayExpired;
        public string BusinessAccountTodayExpired
        {
            get { return _BusinessAccountTodayExpired; }
            set { _BusinessAccountTodayExpired = value; }
        }
        private string _BusinessAccount30DaysNew;
        public string BusinessAccount30DaysNew
        {
            get { return _BusinessAccount30DaysNew; }
            set { _BusinessAccount30DaysNew = value; }
        }
        private string _BusinessAccount30DaysExpired;
        public string BusinessAccount30DaysExpired
        {
            get { return _BusinessAccount30DaysExpired; }
            set { _BusinessAccount30DaysExpired = value; }
        }
        private string _BusinessTotalActiveAccount;
        public string BusinessTotalActiveAccount
        {
            get { return _BusinessTotalActiveAccount; }
            set { _BusinessTotalActiveAccount = value; }
        }

        //Total Accounts
        private string _TotalAccountTodayNew;
        public string TotalAccountTodayNew
        {
            get { return _TotalAccountTodayNew; }
            set { _TotalAccountTodayNew = value; }
        }

        private string _TotalAccountTodayExpired;
        public string TotalAccountTodayExpired
        {
            get { return _TotalAccountTodayExpired; }
            set { _TotalAccountTodayExpired = value; }
        }
        private string _TotalAccount30DaysNew;
        public string TotalAccount30DaysNew
        {
            get { return _TotalAccount30DaysNew; }
            set { _TotalAccount30DaysNew = value; }
        }
        private string _TotalAccount30DaysExpired;
        public string TotalAccount30DaysExpired
        {
            get { return _TotalAccount30DaysExpired; }
            set { _TotalAccount30DaysExpired = value; }
        }

        private string _TotalActiveAccounts;
        public string TotalActiveAccounts
        {
            get { return _TotalActiveAccounts; }
            set { _TotalActiveAccounts = value; }
        }

    }
}