///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.CountryResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Countries
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;
using System.Data;

namespace TributesPortal.ResourceAccess
{
   public class CountryResource : PortalResourceAccess, IResourceAccess
    {


        public List<CustomerCountry> CountryList()
        {
            DataSet ds = new DataSet();
            List<CustomerCountry> Country = new List<CustomerCountry>();
            try
            {
                ds = GetDataSet("GetCountry", null);
                int count = ds.Tables[0].Rows.Count;

                if (count > 0)
                {

                    for (int i = 0; i < count; i++)
                    {
                        int x = int.Parse(ds.Tables[0].Rows[i][0].ToString());

                        Country.Add(new CustomerCountry(int.Parse(ds.Tables[0].Rows[i][0].ToString()),
                                    ds.Tables[0].Rows[i][1].ToString()));


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Country;
        }


        #region IResourceAccess Members

        public void InsertRecord(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void GetData(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void UpdateRecord(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
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
