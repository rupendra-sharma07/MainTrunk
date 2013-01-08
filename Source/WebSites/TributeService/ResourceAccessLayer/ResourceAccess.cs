#region USING DIRECTIVES
using System;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
#endregion

namespace TributeService
{
    public abstract class ResourceAccess
    {
        string strConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBName"];

        protected DataSet GetDataSet(string strStoreProc, object[] objParam)
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
    }
}
