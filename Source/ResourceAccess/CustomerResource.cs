///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.CustomerResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Customers
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
//using TributesPortal.Utilities;
using TributesPortal.ResourceAccess;
using System.Data;


namespace TributesPortal.ResourceAccess
{
    public class CustomerResource : PortalResourceAccess,IResourceAccess
    {
        public List<Customer> CustomerList()
        {
            DataSet ds = new DataSet();
           // PortalResourceAccess objresource = new PortalResourceAccess();
            
            ds = GetDataSet("GetCustomers", null);
            int count = ds.Tables[0].Rows.Count;
            List<Customer> Cust = new List<Customer>();

            for (int i = 0; i < count; i++)
            {
                int x = int.Parse(ds.Tables[0].Rows[i][0].ToString());

                Cust.Add(new Customer(int.Parse(ds.Tables[0].Rows[i][0].ToString()),
                            ds.Tables[0].Rows[i][1].ToString(),
                            ds.Tables[0].Rows[i][2].ToString(),
                            (DateTime)ds.Tables[0].Rows[i][3],
                            int.Parse(ds.Tables[0].Rows[i][4].ToString()),
                            int.Parse(ds.Tables[0].Rows[i][5].ToString()),
                            ds.Tables[0].Rows[i][6].ToString()  ));

            }
            return Cust;
        }
     
        //public List<CustomerCountry> CountryList()
        //{
        //    DataSet ds = new DataSet();

        //    ds = GetDataSet("GetCountry", null);
        //    int count = ds.Tables[0].Rows.Count;
        //    List<CustomerCountry> Country = new List<CustomerCountry>();

        //    for (int i = 0; i < count; i++)
        //    {
        //        int x = int.Parse(ds.Tables[0].Rows[i][0].ToString());

        //        Country.Add(new CustomerCountry(int.Parse(ds.Tables[0].Rows[i][0].ToString()),
        //                    ds.Tables[0].Rows[i][1].ToString()));
               

        //    }
        //    return Country;
        //}

        //public List<CustomerState> StateList(string CountryID)
        //{
        //    DataSet ds = new DataSet();
           
            
        //    object[] objValue ={ CountryID };


        //    ds = GetDataSet("GetState", objValue);
        //    int count = ds.Tables[0].Rows.Count;            
        //    List<CustomerState> State = new List<CustomerState>();

        //    for (int i = 0; i < count; i++)
        //    {
        //        int x = int.Parse(ds.Tables[0].Rows[i][0].ToString());

        //        State.Add(new CustomerState(int.Parse(ds.Tables[0].Rows[i][0].ToString()),
        //                   ds.Tables[0].Rows[i][1].ToString()));                

        //    }
        //    return State;
        //}

        public void InsertRecords(object[] Params)
        {
            Customer customer = (Customer)Params[0];
            string[] strParam = { "FirstName", "Lastname", "DateofBirth", "Country", "State", "City","Id" };

            DbType[] enumDbType ={ DbType.String, DbType.String, DbType.DateTime, DbType.Int32,
                DbType.Int32,DbType.String,DbType.Int32};

            object[] objValue ={ customer.FirstName, customer.LastName, customer.DateofBirth, customer.Country, customer.State, customer.City,null };

            //InsertRecord("CustomerDetails", strParam, enumDbType, objValue);
            object IDentity=InsertDataAndReturnId("CustomerDetails", strParam, enumDbType, objValue);
            customer.Id = (int)IDentity;
        }


      
        #region IResourceAccess Members

        public void InsertRecord(object[] Params)
        {
            Customer customer = (Customer)Params[0];
            string[] strParam = { "FirstName", "Lastname", "DateofBirth", "Country", "State", "City" };

            DbType[] enumDbType ={ DbType.String, DbType.String, DbType.DateTime, DbType.Int32,
                DbType.Int32,DbType.String};

            object[] objValue ={ customer.FirstName, customer.LastName, customer.DateofBirth, customer.Country, customer.State, customer.City };

            base.InsertRecord("SaveCustomer", strParam,
                enumDbType, objValue);
        }

        public void GetData(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void UpdateRecord(object[] Params)
        {
            Customer customer = (Customer)Params[0];
            string[] strParam = { "Id", "FirstName", "Lastname", "DateofBirth", "Country", "State", "City" };

            DbType[] enumDbType ={DbType.Int64, DbType.String, DbType.String, DbType.DateTime, DbType.Int32,
                DbType.Int32,DbType.String};

            object[] objValue ={ customer.Id, customer.FirstName, customer.LastName, customer.DateofBirth, customer.Country, customer.State, customer.City };

            base.UpdateRecord("UpdateCustomer", strParam,
                enumDbType, objValue);
        }

        public void Delete(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IResourceAccess Members


        public object InsertDataAndReturnId(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
