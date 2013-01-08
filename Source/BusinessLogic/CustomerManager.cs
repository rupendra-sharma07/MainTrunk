///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.CustomerManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the basic customer methods
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.ResourceAccess;
//using System.Data;

namespace TributesPortal.BusinessLogic
{
    public class CustomerManager
    {
        List<Customer> customers;
        List<CustomerCountry> country;
        List<CustomerState> state;

        public List<CustomerCountry> Country
        {
            get { return CountryList(); }
            set { country = value; }
        }

        public List<CustomerState> State
        {
            //get { return StateList(); }
            set { state = value; }
        }

        public List<Customer> Customers
        {
            get
            {
                return CustomerList();
            }
            set
            {
                customers = value;
            }
        }

        private List<CustomerCountry> CountryList()
        {
            CountryResource objCustRes = new CountryResource();
            return objCustRes.CountryList();          
        
        }

        public List<CustomerState> StateList(string CustomerID)
        {

            StateResource objCustRes = new StateResource();
            return objCustRes.StateList(CustomerID);
        }
    

        private List<Customer> CustomerList()
        {
            CustomerResource objCustRes = new CustomerResource();
            return objCustRes.CustomerList();           
        }

        public void SaveCustomer(Customer customer)
        {
            //CustomerResource objCustRes = new CustomerResource();
            //object[] param ={customer};
            //objCustRes.InsertRecord(param);
            ////objCustRes.SaveCustomer(customer);
            CustomerResource objCustRes = new CustomerResource();
            object[] param ={ customer };
            objCustRes.InsertRecords(param);
            
        }

        public void UpdateCustomer(Customer customer)
        {
            CustomerResource objCustRes = new CustomerResource();
            object[] param ={ customer };
            objCustRes.UpdateRecord(param);
        }

    }
}
