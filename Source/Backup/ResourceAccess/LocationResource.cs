///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.LocationResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Locations
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;
using System.Collections.ObjectModel;
using System.Data;

namespace TributesPortal.ResourceAccess
{
    public class LocationResource : PortalResourceAccess, IResourceAccess
    {
        public IList<Locations> LocationList(Locations location)
        {
            try
            {
                DataSet ds = new DataSet();
                object[] objValue ={ location.LocationParentId.ToString() };
                ds = GetDataSet("usp_GetLocation", objValue);
                int count = ds.Tables[0].Rows.Count;
                List<Locations> Location = new List<Locations>();
                for (int i = 0; i < count; i++)
                {
                    int x = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                    Location.Add(new Locations(int.Parse(ds.Tables[0].Rows[i][Locations.Location.LocationId.ToString()].ToString()),
                                                ds.Tables[0].Rows[i][Locations.Location.LocationName.ToString()].ToString(),
                                                int.Parse(ds.Tables[0].Rows[i][Locations.Location.LocationParentId.ToString()].ToString())));

                }
                return Location;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Locations> GetCountryState(object[] objValue)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = GetDataSet("usp_GetCountryState", objValue);
                int count = ds.Tables[0].Rows.Count;
                List<Locations> Location = new List<Locations>();
                for (int i = 0; i < count; i++)
                {
                    int x = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                    Location.Add(new Locations(int.Parse(ds.Tables[0].Rows[i][Locations.Location.LocationId.ToString()].ToString()),
                                                ds.Tables[0].Rows[i][Locations.Location.LocationName.ToString()].ToString(),
                                                int.Parse(ds.Tables[0].Rows[i][Locations.Location.LocationParentId.ToString()].ToString())));

                }
                return Location;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetstateIdByName(object[] objValue)
        {
            int locationid = 0;            
            try
            {
                DataSet ds = new DataSet();
                ds = GetDataSet("usp_GetstateIdByName", objValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    locationid = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return locationid;
        }
        public int GetCountryIdByName(string countryName)
        {
            int locationid = 0;
            object[] param = { countryName };
            try
            {
                DataSet ds = new DataSet();
                ds = GetDataSet("usp_GetCountryIdByName", param);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    locationid = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return locationid;
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
