///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.PortalResourceAccess.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods to be used by all other resource acess classess
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

namespace TributesPortal.ResourceAccess
{
    public abstract class PortalResourceAccess
    {


        string strConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBName"];
        string strErrorMessage = "";


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
            Insertdata(strProcName, strParamNM, enumType, objValue);
        }

        protected void InsertRecordMinusIovs(string strProcName, string[] strParamNM, DbType[] enumType, object[] objValue)
        {
            InsertdataMinusIovs(strProcName, strParamNM, enumType, objValue);
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
        protected DataSet GetDataSet(string strStoreProc, object[] objParam)
        {
            //Check for SQL Injection & Special characters
            //CheckIOVS(objParam);

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
                dbCommand.CommandTimeout = 5000;
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
        /// Method will delete the theme from database By Ashu
        /// </summary>
        /// <param name="strStoreProc"></param>
        /// <param name="objParam"></param>
        protected void DeleteTheme(string strStoreProc, object[] objParam)
        {
            //Check for SQL Injection & Special characters
            //CheckIOVS(objParam);

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
        protected DataSet GetDataSetWithoutCheckingIOVS(string strStoreProc, object[] objParam)
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
                dbCommand.CommandTimeout = 5000;
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
            Insertdata(strProcName, strParamNM, enumType, objValue);
        }

        protected void UpdateRecordMinusIovs(string strProcName, string[] strParamNM, DbType[] enumType, object[] objValue)
        {
            InsertdataMinusIovs(strProcName, strParamNM, enumType, objValue);
        }

        private void Insertdata(string strProcName, string[] strParamNM, DbType[] enumType, object[] objValue)
        {

            //Check for SQL Injection & Special characters
            CheckIOVS(objValue);

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


        private void InsertdataMinusIovs(string strProcName, string[] strParamNM, DbType[] enumType, object[] objValue)
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
            //Check for SQL Injection & Special characters
            CheckIOVS(objValue);

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
        /// to insert into database values that have encrypted parameters
        /// </summary>
        /// <param name="strProcName"></param>
        /// <param name="strParamNM"></param>
        /// <param name="enumType"></param>
        /// <param name="objValue"></param>
        /// <returns></returns>
        protected object InsertDataAndReturnIdMinusIOVS(string strProcName, string[] strParamNM, DbType[] enumType, object[] objValue)
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
            //Check for SQL Injection & Special characters
            //CheckIOVS(objParam);

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


        /// <summary>
        /// This method is used for Paging
        /// </summary>
        /// <param name="strStoreProc"></param>
        /// <param name="objParam"></param>
        /// <param name="startindex"></param>
        /// <param name="maxrecords"></param>
        /// <returns></returns>
        protected DataSet GetDataSet(string strStoreProc, object[] objParam, int startindex, int maxrecords)
        {
            //Check for SQL Injection & Special characters
            //CheckIOVS(objParam);

            try
            {
                // Create the DbProviderFactory and DbConnection.
                DbProviderFactory factory =
                    DbProviderFactories.GetFactory("System.Data.SqlClient");
                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings[strConnectionString].ConnectionString;

                using (connection)
                {
                    DbCommand dbCommand = factory.CreateCommand();
                    Database db = DatabaseFactory.CreateDatabase(strConnectionString);

                    if (objParam != null)
                        dbCommand = db.GetStoredProcCommand(strStoreProc, objParam);
                    else
                        dbCommand = db.GetStoredProcCommand(strStoreProc);
                    dbCommand.Connection = connection;
                    // Create the DbDataAdapter.
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = dbCommand;

                    // Fill the DataTable.
                    DataSet _ds = new DataSet();
                    adapter.Fill(_ds, startindex, maxrecords, "Table");
                    return _ds;

                }
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

        private void CheckIOVS(object[] objParam)
        {
            if (objParam != null)
            {
                foreach (object param in objParam)
                {
                    if (param != null && param.GetType() == typeof(string) && !IOVS.Sanitise(Convert.ToString(param), ref strErrorMessage))
                        throw new IovsException(strErrorMessage);
                }
            }
        }

    }
}
