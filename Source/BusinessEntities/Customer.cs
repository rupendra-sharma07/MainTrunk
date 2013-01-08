///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Customer.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the Customer object
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class Customer
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public Customer()
        { }


        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private int _Id;


        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _FirstName;


        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _LastName;


        public System.DateTime DateofBirth
        {
            get { return _DateofBirth; }
            set { _DateofBirth = value; }
        }
        private System.DateTime _DateofBirth;


        public int Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        private int _Country;


        public int State
        {
            get { return _State; }
            set { _State = value; }
        }
        private int _State;


        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _City;

        /// <summary>
        /// User defined Contructor
        /// <summary>
        public Customer(int Id,
            string FirstName,
            string LastName,
            System.DateTime DateofBirth,
            int Country,
            int State,
            string City)
        {
            this._Id = Id;
            this._FirstName = FirstName;
            this._LastName = LastName;
            this._DateofBirth = DateofBirth;
            this._Country = Country;
            this._State = State;
            this._City = City;
        }

    }
}
