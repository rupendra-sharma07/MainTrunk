///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.StateResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with States
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;
using System.Data;

namespace TributesPortal.ResourceAccess
{
    public class StateResource: PortalResourceAccess, IResourceAccess
    {
        public List<CustomerState> StateList(string CountryID)
        {
            DataSet ds = new DataSet();

            object[] objValue ={ CountryID };
            ds = GetDataSet("GetState", objValue);
            int count = ds.Tables[0].Rows.Count;
            List<CustomerState> State = new List<CustomerState>();

            for (int i = 0; i < count; i++)
            {
                int x = int.Parse(ds.Tables[0].Rows[i][0].ToString());

                State.Add(new CustomerState(int.Parse(ds.Tables[0].Rows[i][0].ToString()),
                           ds.Tables[0].Rows[i][1].ToString()));

            }
            return State;
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
