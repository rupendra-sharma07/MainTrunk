///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.CustomerState.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the Customer State object
///Audit Trail     : Date of Modification  Modified By         Description

using System;
namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class CustomerState
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public CustomerState()
        { }
                

        public int StateID
        {
            get { return _StateID; }
            set { _StateID = value; }
        }
        private int _StateID;


        public string StateName
        {
            get { return _StateName; }
            set { _StateName = value; }
        }
        private string _StateName;

        /// <summary>
        /// User defined Contructor
        /// <summary>
        public CustomerState(
            int StateID,
            string StateName)
        {

            this._StateID = StateID;
            this._StateName = StateName;
        }

    }
}
