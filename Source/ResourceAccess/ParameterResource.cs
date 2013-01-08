///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.ParameterResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Parameters
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
    public class ParameterResource : PortalResourceAccess, IResourceAccess
    {
        public List<ParameterTypesCodes> BusinessTypes()
        {
            DataSet ds = new DataSet();
            List<ParameterTypesCodes> objBusTypes = new List<ParameterTypesCodes>();
            try
            {
                object[] param ={ "BUSINESS_TYPE"};
                ds = GetParameterCodes(param); 
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {

                    for (int i = 0; i < count; i++)
                    {

                            objBusTypes.Add(new ParameterTypesCodes(
                                ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.ParameterType.ToString()].ToString(),
                                int.Parse(ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.TypeCode.ToString()].ToString()),
                                 ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.TypeDescription.ToString()].ToString()
                                        ));


                    }
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    throw sqlEx;
                }
            }
            return objBusTypes;
        }

        public List<ParameterTypesCodes> EmailNotifications(object[] param)
        {
            DataSet ds = new DataSet();
            List<ParameterTypesCodes> objBusTypes = new List<ParameterTypesCodes>();
            try
            {                
                ds = GetParameterCodes(param);
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {

                    for (int i = 0; i < count; i++)
                    {

                        objBusTypes.Add(new ParameterTypesCodes(
                            ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.ParameterType.ToString()].ToString(),
                            int.Parse(ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.TypeCode.ToString()].ToString()),
                             ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.TypeDescription.ToString()].ToString()
                                    ));


                    }
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    throw sqlEx;
                }
            }
            return objBusTypes;
        }

        public List<ParameterTypesCodes> PaymentModes()
        {
            DataSet ds = new DataSet();
            List<ParameterTypesCodes> objBusTypes = new List<ParameterTypesCodes>();
            try
            {
                object[] param ={ "PAYMENT_MODE" };
                ds = GetParameterCodes(param);
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {
                    //LHK: commented last two types on jason's new implementation of BeanStream API 
                    for (int i = 0; i < count-2; i++)
                    {

                        objBusTypes.Add(new ParameterTypesCodes(
                            ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.ParameterType.ToString()].ToString(),
                            int.Parse(ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.TypeCode.ToString()].ToString()),
                             ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.TypeDescription.ToString()].ToString()
                                    ));


                    }
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    throw sqlEx;
                }
            }
            return objBusTypes;
        }

        private DataSet GetParameterCodes(object [] Params)
        {
            DataSet paramDs = new DataSet();
            try
            {

                paramDs = GetDataSet("usp_GetPortalTypesAndCodes", Params);
                return paramDs;
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                throw sqlEx;
            }
         //   return paramDs;
        }




        public List<ParameterTypesCodes> GetTributeTypesbyTypeCode(int typecode)
        {
            DataSet ds = new DataSet();
            List<ParameterTypesCodes> objBusTypes = new List<ParameterTypesCodes>();
            try
            {
                object[] param ={ "TRIBUTE_TYPE", typecode };
                ds = GetDataSet("usp_GetPortalTypesAndCodesbyTypeCode", param);
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {

                    for (int i = 0; i < count; i++)
                    {

                        objBusTypes.Add(new ParameterTypesCodes(
                            ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.ParameterType.ToString()].ToString(),
                            int.Parse(ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.TypeCode.ToString()].ToString()),
                             ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.TypeDescription.ToString()].ToString()
                                    ));


                    }
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    throw sqlEx;
                }
            }
            return objBusTypes;
        }

        public List<ParameterTypesCodes> GetTributeTypes(string applicationType)
        {
            DataSet ds = new DataSet();
            List<ParameterTypesCodes> objBusTypes = new List<ParameterTypesCodes>();
            try
            {
                object[] param = { "TRIBUTE_TYPE", applicationType };
                ds = GetDataSet("usp_GetListOfTributes", param);
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {

                    for (int i = 0; i < count; i++)
                    {

                        objBusTypes.Add(new ParameterTypesCodes(
                            ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.ParameterType.ToString()].ToString(),
                            int.Parse(ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.TypeCode.ToString()].ToString()),
                             ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.TypeDescription.ToString()].ToString()
                                    ));


                    }
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    throw sqlEx;
                }
            }
            return objBusTypes;
        }


        public List<ParameterTypesCodes> CouponUses()
        {
            DataSet ds = new DataSet();
            List<ParameterTypesCodes> objBusTypes = new List<ParameterTypesCodes>();
            try
            {
                object[] param ={ "COUPON_USES" };
                ds = GetParameterCodes(param);
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {

                    for (int i = 0; i < count; i++)
                    {

                        objBusTypes.Add(new ParameterTypesCodes(
                            ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.ParameterType.ToString()].ToString(),
                            int.Parse(ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.TypeCode.ToString()].ToString()),
                             ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.TypeDescription.ToString()].ToString()
                                    ));


                    }
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    throw sqlEx;
                }
            }
            return objBusTypes;
        }

        public List<ParameterTypesCodes> CouponPackages()
        {
            DataSet ds = new DataSet();
            List<ParameterTypesCodes> objBusTypes = new List<ParameterTypesCodes>();
            try
            {
                object[] param ={ "COUPON_PACKAGE" };
                ds = GetParameterCodes(param);
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {

                    for (int i = 0; i < count; i++)
                    {

                        objBusTypes.Add(new ParameterTypesCodes(
                            ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.ParameterType.ToString()].ToString(),
                            int.Parse(ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.TypeCode.ToString()].ToString()),
                             ds.Tables[0].Rows[i][ParameterTypesCodes.Parameters.TypeDescription.ToString()].ToString()
                                    ));


                    }
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    throw sqlEx;
                }
            }
            return objBusTypes;
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

        public object InsertDataAndReturnId(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
