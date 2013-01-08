#region USING DIRECTIVES
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TributeService.BusinessEntities;
#endregion

namespace TributeService.ResourceAccessLayer
{
    public class VideoUploadResource : ResourceAccess
    {
        /// <summary>
        /// Method to update vidoe status in VideoUpload Table
        /// </summary>
        /// <param name="objStatus"></param>
        /// <returns>Token Id</returns>
        public string UpdateVideoStatus(VideoStatus objStatus)
        {
            if (!Equals(objStatus, null))
            {
                try
                {
                    string[] strParam = { "UserId", "Status", "FileName", 
                                            "IsActive", "IsDeleted", "CreatedBy", 
                                            "CreatedDate" };
                    DbType[] dbType ={ DbType.Int64, DbType.Byte, DbType.String, 
                                            DbType.Byte, DbType.Byte, DbType.Int64, 
                                            DbType.DateTime };
                    object[] objValue ={ objStatus.UserId, objStatus.Status, objStatus.FileName, 
                                            objStatus.IsActive, objStatus.IsDeleted, objStatus.CreatedBy, 
                                            objStatus.CreatedDate };

                    //sends request to insert record and get the identity of the record inserted
                    object Identity = base.InsertDataAndReturnId("usp_UpdateVideoStatus", strParam, dbType, objValue);
                    return Identity.ToString();
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    return "error";
                }
                catch (Exception ex)
                {
                    return "error";
                }
            }
            else
                return "error";
        }
    }
}
