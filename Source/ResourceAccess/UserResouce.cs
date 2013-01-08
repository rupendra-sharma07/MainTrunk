///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.UserResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Users
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using System.Data;
namespace TributesPortal.ResourceAccess
{
    public class UserResource : PortalResourceAccess, IResourceAccess
    {
        #region IResourceAccess Members


        public void GetData(object[] objValue)
        {
            try
            {
                UserRole objUR = (UserRole)objValue[0];
                object[] objParam = { objUR.UserId.ToString() };
                DataSet ds = GetDataSet("GetUserRole", objParam);
                // ds.Tables[0].
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {
                    objUR.roles = ds.Tables[0].Rows[0][1].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CheckLogin(object[] objValue)
        {
            GeneralUser objUser = (GeneralUser)objValue[0];
            try
            {

                object[] objParam = { null, objUser.CustomUsers.Password.ToString() };
                DataSet ds = GetDataSet("ValidateUser", objParam);
                // ds.Tables[0].
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {
                    UserRole objuserrole = new UserRole();
                    objuserrole.roles = ds.Tables[0].Rows[0][1].ToString();

                    objUser.CustomUserRole = objuserrole;

                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUser.CustomError = objError;
                }
            }


        }

        /// <summary>
        /// Method to get the list of users based on the entered criteria.
        /// </summary>
        /// <param name="objUsers">Filled Users entity.</param>
        /// <returns>List of Users.</returns>
        public List<Users> SearchUsers(object[] objUsers)
        {
            List<Users> objUsersList = new List<Users>();
            Users objUser = (Users)objUsers[0];

            object[] objParam = {objUser.SearchUserId, objUser.UserName, objUser.FirstName, objUser.LastName,
                                    objUser.Email, objUser.City, objUser.State, objUser.Country,
                                    objUser.UserType, objUser.CreatedAfter, objUser.CreatedBefore,objUser.ApplicationType};

            DataSet dsUsers = GetDataSet("usp_UserSearch", objParam);

            if (dsUsers.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsUsers.Tables[0].Rows)
                {
                    Users obj = new Users();
                    obj.UserId = int.Parse(dr["UserId"].ToString());
                    obj.UserName = dr["UserName"].ToString();
                    obj.FirstName = dr["FirstName"].ToString();
                    obj.LastName = dr["LastName"].ToString();
                    obj.AccountType = dr["AccountType"].ToString();
                    obj.City = dr["City"].ToString();
                    obj.StateName = dr["State"].ToString();
                    obj.CountryName = dr["Country"].ToString();
                    obj.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    obj.CreationDate = DateTime.Parse(dr["CreatedOn"].ToString()).ToString("MMMM dd, yyyy");
                    //obj.Email = dr["City"].ToString();
                    objUsersList.Add(obj);
                    obj = null;
                }
            }

            return objUsersList;
        }

        /// <summary>
        /// Method to delete user.
        /// </summary>
        /// <param name="Params">User entity containing userid to be deleted.</param>
        public void Delete(object[] Params)
        {
            Users objUsers = (Users)Params[0];

            if (!Equals(objUsers, null))
            {
                try
                {
                    string[] strParam = {Users.UserEnum.UserId.ToString(),
                                            Users.UserEnum.IsDeleted.ToString() };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64,
                                        DbType.Boolean };
                    //sets the values in the entity to the parameters
                    object[] objValue = { objUsers.UserId,
                                            objUsers.IsDeleted };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_DeleteUser", strParam, dbType, objValue);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    throw sqlEx;
                }
            }
        }

        public void InsertRecord(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void UpdateRecord(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public AddRemoveCreditInfo AddOrDebitCredits(object[] objUsers)
        {
            AddRemoveCreditInfo objAddRemoveCreditInfo = (AddRemoveCreditInfo)objUsers[0];

            object[] objParam = {objAddRemoveCreditInfo.UserId,objAddRemoveCreditInfo.UserName,
                                    objAddRemoveCreditInfo.UserIdOptedOruserName, objAddRemoveCreditInfo.CreditOrDebit,
                                    objAddRemoveCreditInfo.CreditCount,objAddRemoveCreditInfo.ApplicationType
                                   };

            DataSet dsUsers = GetDataSet("usp_AddOrDebitCredits", objParam);


            if (dsUsers.Tables[0].Rows.Count > 0)
            {
                objAddRemoveCreditInfo.CreditCount = int.Parse(dsUsers.Tables[0].Rows[0]["NetCreditPoints"].ToString());
            }

            return objAddRemoveCreditInfo;
        }

        public EnableRSSFeedInfo EnableRSSFeedForBussUser(object[] objFeed)
        {
            EnableRSSFeedInfo objEnableRSSFeedInfo = (EnableRSSFeedInfo)objFeed[0];

            object[] objParam = {objEnableRSSFeedInfo.UserId,objEnableRSSFeedInfo.UserName,
                                    objEnableRSSFeedInfo.AtomEnabled
                                };
            DataSet dsUsers = GetDataSet("usp_AtomEnableOnUserIdOrName", objParam);
            if (dsUsers != null)
            {
                if (dsUsers.Tables[0].Rows.Count > 0)
                {
                    objEnableRSSFeedInfo.UpdateOutput = int.Parse(dsUsers.Tables[0].Rows[0]["UpdateOutput"].ToString());
                    objEnableRSSFeedInfo.UserId = int.Parse(dsUsers.Tables[0].Rows[0]["UserId"].ToString());
                }
                else
                {
                    objEnableRSSFeedInfo.UpdateOutput = 0;
                    objEnableRSSFeedInfo.UserId = 0;
                }
            }

            return objEnableRSSFeedInfo;
        }

        public object InsertDataAndReturnId(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        

        public EnableRSSFeedInfo EnableXmlFeedForBussUser(object[] param)
        {

            EnableRSSFeedInfo objEnableRSSFeedInfo = (EnableRSSFeedInfo)param[0];

            object[] objParam = {objEnableRSSFeedInfo.UserId,objEnableRSSFeedInfo.UserName,
                                    objEnableRSSFeedInfo.EnableXMLFeed
                                };
            DataSet dsUsers = GetDataSet("usp_EnableXMLFeedOnUserIdOrName", objParam);
            if (dsUsers != null)
            {
                if (dsUsers.Tables[0].Rows.Count > 0)
                {
                    objEnableRSSFeedInfo.UpdateOutput = int.Parse(dsUsers.Tables[0].Rows[0]["UpdateOutput"].ToString());
                    objEnableRSSFeedInfo.UserId = int.Parse(dsUsers.Tables[0].Rows[0]["UserId"].ToString());
                }
                else
                {
                    objEnableRSSFeedInfo.UpdateOutput = 0;
                    objEnableRSSFeedInfo.UserId = 0;
                }
            }

            return objEnableRSSFeedInfo;

        }
        #endregion
    }
}
