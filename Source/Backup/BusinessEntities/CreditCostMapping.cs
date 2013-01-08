using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{[Serializable]
    public class CreditCostMapping
    {    
   
        public enum CreditCost
        {
            CreditPoints,
            CostPerCredit,
            TributeType
        }

        public CreditCostMapping()
        { }

        public int CreditPoints
        {
            get { return _CreditPoints; }
            set { _CreditPoints = value; }
        }
        private int _CreditPoints;

        public double CostPerCredit
        {
            get { return _CostPerCredit; }
            set { _CostPerCredit = value; }
        }
        private double _CostPerCredit;

        public int TributeType
        {
            get { return _TributeType; }
            set { _TributeType = value; }
        }
        private int _TributeType;

        public CreditCostMapping(
            int CreditPoints,           
            double CostPerCredit,
            int TributeType)
        {           
            
            this._CreditPoints = CreditPoints;
            this._CostPerCredit = CostPerCredit;
            this._TributeType = TributeType;
            
        }

    }
}
