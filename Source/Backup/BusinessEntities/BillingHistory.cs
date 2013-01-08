///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.BillingHistory.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to generate the billing history
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class BillingHistory
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public BillingHistory()
        { }

        public enum Billing
        { 
          StartDate,
          TributeName,
          PackageName,
          AmountPaid,
          Tributeid
        }

        public System.DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private System.DateTime _StartDate;


        public System.String TributeName
        {
            get { return _TributeName; }
            set { _TributeName = value; }
        }
        private System.String _TributeName;


        public System.String PackageName
        {
            get { return _PackageName; }
            set { _PackageName = value; }
        }
        private System.String _PackageName;


        public System.Decimal AmountPaid
        {
            get { return _AmountPaid; }
            set { _AmountPaid = value; }
        }
        private System.Decimal _AmountPaid;

        public System.Decimal Tributeid
        {
            get { return _Tributeid; }
            set { _Tributeid = value; }
        }
        private System.Decimal _Tributeid;

        public string AmountToDisplay
        {
            get { return _amountToDisplay; }
            set { _amountToDisplay = value; }
        }
        private string _amountToDisplay;

        public int TributePackageId
        {
            get { return _TributePackageId; }
            set { _TributePackageId = value; }
        }
        private int _TributePackageId;

        /// <summary>
        /// User defined Contructor
        /// <summary>
        public BillingHistory(System.DateTime StartDate,
            System.String TributeName,
            System.String PackageName,
            System.Decimal AmountPaid,
            System.Decimal Tributeid)
        {
           
            //prov.GetFormat(
           // string dt=StartDate.ToString("MMMM dd,yyyy"); 
            this._StartDate = StartDate;
            this._TributeName = TributeName;
            this._PackageName = PackageName;
            this._AmountPaid = AmountPaid;
            this._Tributeid = Tributeid;
        }

        public BillingHistory(System.DateTime StartDate,
            System.String TributeName,
            System.String PackageName,
            System.Decimal AmountPaid,
            System.Decimal Tributeid,
            int TributePackageId)
        {

            //prov.GetFormat(
            // string dt=StartDate.ToString("MMMM dd,yyyy"); 
            this._StartDate = StartDate;
            this._TributeName = TributeName;
            this._PackageName = PackageName;
            this._AmountPaid = AmountPaid;
            this._Tributeid = Tributeid;
            this._TributePackageId = TributePackageId;
        }

    }
}
