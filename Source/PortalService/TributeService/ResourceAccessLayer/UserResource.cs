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
    public class UserResource : ResourceAccess
    {
        public int CheckForLogin(Users objUser)
        {
            try
            {
                object[] objParam ={ objUser.UserName.ToString(), objUser.Password.ToString() };
                DataSet ds = GetDataSet("usp_ValidateUser", objParam);
                int userId = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    userId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                }
                if (userId > 0)
                    return userId;
                else
                    return -1;
                /*if (userId > 0)
                    return true;
                else
                    return false;*/
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
