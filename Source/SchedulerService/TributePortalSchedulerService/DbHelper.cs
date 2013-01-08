///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.Services.TributePortalSchedulerService.ResourceAccess.cs
///Author          : 
///Creation Date   : 
///Description     : This class acts as the data access layer of the scheduler service
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Configuration;
using System.Data;
using System.Xml;
using System.Data.SqlClient;

namespace TributePortal.SchedulerService.DataAccess
{
    public class ResourceAccess
    {


        string strConnectionString = ConfigurationSettings.AppSettings["DBName"];

            

        /// <summary>
        /// This method will insert records.
        /// </summary>
        /// <param name="strProcName">
        /// Name of Stored procedure.
        /// </param>
        /// <param name="strParamNM">
        /// List of parameter names.
        /// </param>
        /// <param name="enumType">
        /// List of parameter Type.
        /// </param>
        /// <param name="objValue">
        /// List of parameter values. 
        /// </param>     
        protected void InsertRecord(string strProcName, string[] strParamNM, DbType[] enumType, object[] objValue)
        {
            try
            {
                Insertdata(strProcName, strParamNM, enumType, objValue);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }
        /// <summary>
        /// This method will return records.
        /// </summary>      
        /// <param name="strStoreProc">
        /// SP Name. 
        /// </param>     
        /// <param name="objParam">
        /// List of parameter values. 
        /// </param>     
        /// <returns>
        /// returns DataSet.
        /// </returns>
        public DataSet GetDataSet(string strStoreProc, object[] objParam)
        {
            try
            {
                DbCommand dbCommand = null;
                 Database db = DatabaseFactory.CreateDatabase(strConnectionString);

                if (objParam != null)
                    dbCommand = db.GetStoredProcCommand(strStoreProc, objParam);
                else
                    dbCommand = db.GetStoredProcCommand(strStoreProc);
                // Retrieve ProdcutName. ExecuteScalar returns an object, so
                // we cast to the correct type (string).
                return db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }

        }


        /// <summary>
        /// This method will Update records.
        /// </summary>
        /// <param name="strProcName">
        /// Name of Stored procedure.
        /// </param>
        /// <param name="strParamNM">
        /// List of parameter names.
        /// </param>
        /// <param name="enumType">
        /// List of parameter Type.
        /// </param>
        /// <param name="objValue">
        /// List of parameter values. 
        /// </param>       
        protected void UpdateRecord(string strProcName, string[] strParamNM, DbType[] enumType, object[] objValue)
        {
            try
            {
                Insertdata(strProcName, strParamNM, enumType, objValue);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new ApplicationException(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }


        private void Insertdata(string strProcName, string[] strParamNM, DbType[] enumType, object[] objValue)
        {
            try
            {
                // SqlTransaction objTra = new SqlTransaction();                                

                SqlDatabase dbSQL = DatabaseFactory.CreateDatabase(strConnectionString) as SqlDatabase;
                string sqlCommand = strProcName;
                DbCommand dbCommand = dbSQL.GetStoredProcCommand(sqlCommand);

                for (int i = 0; i < strParamNM.Length; i++)
                {
                    dbSQL.AddInParameter(dbCommand, strParamNM[i], enumType[i], objValue[i]);
                }
                dbSQL.ExecuteNonQuery(dbCommand);
                //  objTra.Commit();

            }
            catch (System.Data.SqlClient.SqlException sqlexp)
            {
                throw sqlexp;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }
        /// <summary>
        /// This method will insert records and return Ids
        /// </summary>
        /// <param name="strProcName">
        /// Name of Stored procedure.
        /// </param>
        /// <param name="strParamNM">
        /// List of parameter names.
        /// </param>
        /// <param name="enumType">
        /// List of parameter Type.
        /// </param>
        /// <param name="objValue">
        /// List of parameter values. 
        /// </param>
        /// <returns>
        /// returns oblect.
        /// </returns>
        protected object InsertDataAndReturnId(string strProcName, string[] strParamNM, DbType[] enumType, object[] objValue)
        {
            SqlDatabase dbSQL = DatabaseFactory.CreateDatabase(strConnectionString) as SqlDatabase;
            StringBuilder sbrItemList = new StringBuilder();

            DbCommand dbCommand = dbSQL.GetStoredProcCommand(strProcName);
            try
            {
                for (int i = 0; i < strParamNM.Length; i++)
                {
                    dbSQL.AddInParameter(dbCommand, strParamNM[i], enumType[i], objValue[i]);
                }
                return dbSQL.ExecuteScalar(dbCommand);

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new ApplicationException(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }

        }
        /// <summary>
        /// This method will the records from database
        /// </summary>        
        /// <param name="strStoreProc">
        /// Name of Stored procedure.
        /// </param>
        /// <param name="objParam">
        /// List of parameters.
        /// </param>
        protected void Delete(string strStoreProc, object[] objParam)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(strConnectionString);
                DbCommand dbCommand = null;
                if (objParam != null)
                    dbCommand = db.GetStoredProcCommand(strStoreProc, objParam);
                else
                    dbCommand = db.GetStoredProcCommand(strStoreProc);
                db.ExecuteNonQuery(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new ApplicationException(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }
    }
}
