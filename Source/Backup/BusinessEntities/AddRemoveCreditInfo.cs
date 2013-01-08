using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class AddRemoveCreditInfo
    {

        public enum AddRemoveCreditInfoEnum
        { 
            UserId,
            UserName,
            UserIdOptedOrUsername,
            CreditOrDebit,
            CreditCount
        }

        public AddRemoveCreditInfo()
        { }


        private int _UserId;
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }


        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private int _creditOrDebit;
        public int CreditOrDebit
        {
            get { return _creditOrDebit; }
            set { _creditOrDebit = value; }
        }

        private int _creditCount;
        public int CreditCount
        {
            get { return _creditCount; }
            set { _creditCount = value; }
        }

        private string _applicationType;
        public string ApplicationType
        {
            get { return _applicationType; }
            set { _applicationType=value;}
        }

        private int _UserIdOptedOrUsername;
        public int UserIdOptedOruserName
        {
            get { return _UserIdOptedOrUsername; }
            set { _UserIdOptedOrUsername = value; }
        }

        public AddRemoveCreditInfo(
            int UserId,            
            int CreditOrDebit,
            int UserIdOptedOrUsername,
            int CreditCount           
            )
        {
            this._UserId= UserId;
            this._creditOrDebit = CreditOrDebit;
            this._creditCount = CreditCount;
           
        }

        public AddRemoveCreditInfo(           
            string UserName,
            int CreditOrDebit,
            int UserIdOptedOrUsername,
            int CreditCount
            )
        {
            this._UserName = UserName;
            this._creditOrDebit = CreditOrDebit;
            this._creditCount = CreditCount;

        }

    }
}
