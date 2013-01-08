///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.ParameterTypesCodes.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the 
///                  Details about Parameter Types Codes
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class ParameterTypesCodes
    {
        /// <summary>
        /// Default Contructor
        /// <summary>

        public enum Parameters
        { 
             ParameterType,
             TypeCode,
             TypeDescription
        }

        public ParameterTypesCodes()
        { }


        private string _ParameterType;
        public string ParameterType
        {
            get { return _ParameterType; }
            set { _ParameterType = value; }
        }


        private int _TypeCode;
        public int TypeCode
        {
            get { return _TypeCode; }
            set { _TypeCode = value; }
        }


        private string _TypeDescription;
        public string TypeDescription
        {
            get { return _TypeDescription; }
            set { _TypeDescription = value; }
        }


        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        /// <summary>
        /// User defined Contructor
        /// <summary>
        public ParameterTypesCodes(string ParameterType,
            int TypeCode,
            string TypeDescription
            )
        {
            this._ParameterType = ParameterType;
            this._TypeCode = TypeCode;
            this._TypeDescription = TypeDescription;
        }

    }
}
