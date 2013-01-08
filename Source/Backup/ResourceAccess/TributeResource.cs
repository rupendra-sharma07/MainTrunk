///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.TributeResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Tributes
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using System.Data;
using System.Data.SqlClient;
using TributesPortal.Utilities;

namespace TributesPortal.ResourceAccess
{
    public partial class TributeResource : PortalResourceAccess
    {
        public List<Tributes> GetTributes(object[] objValue)
        {
            try
            {
                DataSet dsTributes = GetDataSet("usp_GetTributeList", new string[] { "TRIBUTE_TYPE" });
                List<Tributes> lstTributes = new List<Tributes>();
                if (dsTributes.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTributes.Tables[0].Rows)
                    {
                        Tributes objTributes = new Tributes();
                        objTributes.TributeId = int.Parse(dr["TypeCode"].ToString());
                        objTributes.TributeName = dr["TypeDescription"].ToString();
                        lstTributes.Add(objTributes);
                        objTributes = null; 
                    }
                }
                return lstTributes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CheckUrlExistsInTable(string TributeUrl, int TributeType)
        {
            object[] param = { TributeUrl, TributeType };
            Tributes objTributes = new Tributes();
            string strReturn = null;
            if (!Equals(objTributes, null))
            {
                try
                {
                    DataSet dsUrlExists = GetDataSet("usp_TributeDomainAvaibality", param);
                    if (dsUrlExists.Tables[0].Rows.Count > 0)
                    {
                        return dsUrlExists.Tables[0].Rows[0][0].ToString();
                    }

                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    if (ex.Number > 50000)
                    {
                        Errors objError = new Errors();
                        objError.ErrorMessage = ex.Message;
                        objTributes.CustomError = objError;
                        return strReturn;
                    }
                }
            }
            return strReturn;
        }
        public object InsertTribute(object[] objTributes)
        {
            Tributes tributes = (Tributes)objTributes[0];
            object Identity = new object();
            if (!Equals(objTributes, null))
            {
                try
                {

                    string[] strTributeParams ={Tributes.TributeEnum.UserTributeId.ToString(),
                                                   Tributes.TributeEnum.TributeFirstName.ToString(),
                                                   Tributes.TributeEnum.TributeLastName.ToString(),
                                                Tributes.TributeEnum.TributeName.ToString(),
                                                Tributes.TributeEnum.TributeType.ToString(),
                                                Tributes.TributeEnum.WelcomeMessage.ToString(),
                                                Tributes.TributeEnum.TributeImage.ToString(),
                                                Tributes.TributeEnum.TributeUrl.ToString(),                                              
                                                Tributes.TributeEnum.ThemeId.ToString(),
                                                Tributes.TributeEnum.City.ToString(),                                                
                                                Tributes.TributeEnum.State.ToString(), 
                                                Tributes.TributeEnum.Country.ToString(),
                                                Tributes.TributeEnum.IsPrivate.ToString(), 
                                                Tributes.TributeEnum.IsOrderDVDChecked.ToString(), 
                                                Tributes.TributeEnum.IsMemTributeBoxChecked.ToString(), 
                                                Tributes.TributeEnum.Date1.ToString(), 
                                                Tributes.TributeEnum.Date2.ToString(),
                                                Tributes.TributeEnum.PostMessage.ToString(),
                                                Tributes.TributeEnum.MessageWithoutHtml.ToString()
                                                };
                    DbType[] dbType ={DbType.Int64,
                                      DbType.String, 
                                      DbType.String, 
                                      DbType.String,
                                      DbType.Int64,
                                      DbType.String,
                                      DbType.String,
                                      DbType.String,
                                      DbType.Int64,
                                      DbType.String,
                                      DbType.Int64, 
                                      DbType.Int64, 
                                      DbType.Boolean,
                                      DbType.Boolean,
                                      DbType.Boolean,
                                      DbType.DateTime,
                                      DbType.DateTime,
                                      DbType.String,
                                      DbType.String
                        };
                    object[] objValue ={tributes.UserTributeId,tributes.TributeFirstName,tributes.TributeLastName, tributes.TributeName,
                                         tributes.TributeType,tributes.WelcomeMessage,tributes.TributeImage,
                                         tributes.TributeUrl,tributes.ThemeId,tributes.City,  tributes.State,
                                         tributes.Country, tributes.IsPrivate,tributes.IsOrderDVDChecked,
                                         tributes.IsMemTributeBoxChecked, tributes.Date1,tributes.Date2,
                                         tributes.PostMessage,tributes.MessageWithoutHtml
                                         };
                    Identity = InsertDataAndReturnIdMinusIOVS("usp_CreateTribute", strTributeParams, dbType, objValue);

                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number > 50000)
                    {
                        Errors errors = new Errors();
                        errors.ErrorMessage = sqlEx.Message;
                        tributes.CustomError = errors;
                        return Identity;
                    }
                }
            }
            return Identity;
        }

        //Save the Donation object into DB
        public object InsertDonation(object[] objDonation)
        {
            Donation donations = (Donation)objDonation[0];
            object Identity = new object();
            if (!Equals(objDonation, null))
            {
                try
                {

                    string[] strTributeParams ={Donation.DonationEnum.TributeId.ToString(),
                                                Donation.DonationEnum.TributeName.ToString(),
                                                Donation.DonationEnum.TributeType.ToString(),
                                                Donation.DonationEnum.TributeURL.ToString(),
                                                Donation.DonationEnum.TributeCreatorEmail.ToString(),
                                                Donation.DonationEnum.CharityName.ToString(),
                                                Donation.DonationEnum.CharityCountry.ToString(),
                                                Donation.DonationEnum.CharityState.ToString(),
                                                Donation.DonationEnum.CharityCity.ToString(),
                                                Donation.DonationEnum.CharityAddress.ToString(),
                                                Donation.DonationEnum.DonationNotificationEmail.ToString(),
                                                Donation.DonationEnum.DonationPageURL.ToString(),
                                                Donation.DonationEnum.CreatedBy.ToString()
                                                };
                    DbType[] dbType ={DbType.Int64,
                                      DbType.String,
                                      DbType.String,
                                      DbType.String,
                                      DbType.String,
                                      DbType.String,
                                      DbType.String,
                                      DbType.String,
                                      DbType.String, 
                                      DbType.String, 
                                      DbType.String,
                                      DbType.String,
                                      DbType.Int32
                        };
                    object[] objValue ={donations.TributeID, donations.TributeName,
                                         donations.TributeType, donations.TributeUrl, donations.CreatorMail, 
                                         donations.CharityName, donations.CharityCountry, donations.CharityState, donations.CharityCity, donations.CharityAddress, 
                                         donations.DonationNotifyMail, donations.DonationUrl, donations.CreatedBy
                                         };
                    Identity = InsertDataAndReturnId("usp_CreateDonation", strTributeParams, dbType, objValue);
                    return Identity;
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number > 50000)
                    {
                        Errors errors = new Errors();
                        errors.ErrorMessage = sqlEx.Message;
                        donations.CustomError = errors;
                        return Identity;
                    }
                }
            }
            return Identity;
        }

        /// <summary>
        /// This function count the number of visit.
        /// </summary>
        /// <param name="objWebstatics"></param>
        public void GetTributeCount(object[] objWebstatics)
        {
            TributeVisitCount objTribute = (TributeVisitCount)objWebstatics[0];
            if (!Equals(objTribute, null))
            {
                object[] objParam = { objTribute.SectionTypeID, objTribute.SectionTypeCodeId };
                DataSet dsTribute = GetDataSet("usp_GetTributeCount", objParam);
                if (dsTribute.Tables[0].Rows.Count > 0)
                {
                    objTribute.Count = int.Parse(dsTribute.Tables[0].Rows[0][0].ToString());
                }
            }
        }
        /// <summary>
        /// This function count the number of Tributes.
        /// </summary>
        /// <param name="objWebstatics"></param>
        public void GetMyTributeCount(object[] objWebstatics)
        {
            TributeVisitCount objTribute = (TributeVisitCount)objWebstatics[0];
            if (!Equals(objTribute, null))
            {
                object[] objParam = { objTribute.SectionTypeID, objTribute.SectionTypeCodeId };
                DataSet dsTribute = GetDataSet("usp_MyTributeCount", objParam);
                if (dsTribute.Tables[0].Rows.Count > 0)
                {
                    objTribute.Count = int.Parse(dsTribute.Tables[0].Rows[0][0].ToString());
                }
            }
        }


        public void UpdateUsedCouponDetail(string couponcode)
        {
            //Coupons objcoupons = (Coupons)objTributes[0];
            if (!Equals(couponcode, null))
            {
                try
                {

                    string[] strTributeParams ={                                              
                                                "couponCode"                                                
                                                };
                    DbType[] dbType ={
                                          DbType.String
                                     };
                    object[] objValue = { couponcode };

                    base.InsertRecord("UpdateUsedCouponDetail", strTributeParams, dbType, objValue);

                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number > 50000)
                    {
                        Errors errors = new Errors();
                        errors.ErrorMessage = sqlEx.Message;
                        //objcoupons.CustomError = errors;

                    }
                }
            }

        }

        public int GetCouponAvailable(object[] objWebstatics, int couponType)
        {
            int CouponAvailable = 0;//0 for not av,1 for av
            Coupons objcoup = (Coupons)objWebstatics[0];
            CouponsAvailable objTribute = objcoup.Couponsavailable;
            if (!Equals(objTribute, null))
            {
                object[] objParam = { objTribute.CouponCode, couponType };
                DataSet dsTribute = GetDataSet("usp_CouponAvailablity", objParam);
                if (dsTribute.Tables[0].Rows.Count > 0)
                {

                    CouponAvailable = int.Parse(dsTribute.Tables[0].Rows[0][0].ToString());

                }
                if (dsTribute.Tables[1].Rows.Count > 0)
                {
                    Couponmaster objcoupnmaster = new Couponmaster();
                    objcoupnmaster.CouponDenomination = decimal.Parse(dsTribute.Tables[1].Rows[0][0].ToString());
                    objcoupnmaster.IsPercentage = bool.Parse(dsTribute.Tables[1].Rows[0][1].ToString());
                    objcoup.CouponMaster = objcoupnmaster;
                }
            }
            return CouponAvailable;
        }

        /// <summary>
        /// This function count the number of Tributes.
        /// </summary>
        /// <param name="objWebstatics"></param>
        public void GetMyFavouritesCount(object[] objWebstatics)
        {
            TributeVisitCount objTribute = (TributeVisitCount)objWebstatics[0];
            if (!Equals(objTribute, null))
            {
                object[] objParam = { objTribute.SectionTypeCodeId, objTribute.SectionTypeID };
                DataSet dsTribute = GetDataSet("usp_GetMyFavouritesCount", objParam);
                if (dsTribute.Tables[0].Rows.Count > 0)
                {
                    objTribute.Count = int.Parse(dsTribute.Tables[0].Rows[0][0].ToString());
                }
            }
        }

        public void GetuserInboxTotalCount(object[] objWebstatics)
        {
            TributeVisitCount objTribute = (TributeVisitCount)objWebstatics[0];
            if (!Equals(objTribute, null))
            {
                object[] objParam = { objTribute.SectionTypeCodeId };
                DataSet dsTribute = GetDataSet("usp_GetuserInboxTotalCount", objParam);
                if (dsTribute.Tables[0].Rows.Count > 0)
                {
                    objTribute.Count = int.Parse(dsTribute.Tables[0].Rows[0][0].ToString());
                }
            }
        }

        public void GetuserSentMessagesCount(object[] objWebstatics)
        {
            TributeVisitCount objTribute = (TributeVisitCount)objWebstatics[0];
            if (!Equals(objTribute, null))
            {
                object[] objParam = { objTribute.SectionTypeCodeId };
                DataSet dsTribute = GetDataSet("usp_GetuserSentMessagesCount", objParam);
                if (dsTribute.Tables[0].Rows.Count > 0)
                {
                    objTribute.Count = int.Parse(dsTribute.Tables[0].Rows[0][0].ToString());
                }
            }
        }

        public void GetUsereventsCount(object[] objWebstatics)
        {
            TributeVisitCount objTribute = (TributeVisitCount)objWebstatics[0];
            if (!Equals(objTribute, null))
            {
                object[] objParam = { objTribute.SectionTypeCodeId };
                DataSet dsTribute = GetDataSet("usp_GetUsereventsCount", objParam);
                if (dsTribute.Tables[0].Rows.Count > 0)
                {
                    objTribute.Count = int.Parse(dsTribute.Tables[0].Rows[0][0].ToString());
                }
            }
        }



        /// <summary>
        /// This SP will get the type code of Tribute.
        /// </summary>
        /// <param name="objWebstatics"></param>
        public int GetTributeIdCode(string TributeName)
        {
            int count = 0;
            object[] objParam = { TributeName };
            DataSet dsTribute = GetDataSet("usp_GetTributeIdCode_", objParam);
            if (dsTribute.Tables[0].Rows.Count > 0)
            {
                count = int.Parse(dsTribute.Tables[0].Rows[0][0].ToString());
            }
            return count;

        }





        /// <summary>
        /// Update Tribute Name
        /// </summary>
        /// <param name="objtribute"></param>
        public void UpdateTributeName(object[] objtribute)
        {

            Tributes objTributeTheme = (Tributes)objtribute[0];
            if (!Equals(objTributeTheme, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {"UserId",  
                                            Tributes.TributeEnum.TributeId.ToString(),
                        Tributes.TributeEnum.TributeName.ToString()
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {
                                        DbType.Int64,
                                        DbType.Int64,
                        DbType.String                
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { objTributeTheme.UserTributeId,
                                            objTributeTheme.TributeId,
                        objTributeTheme.TributeName
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_UpdateTributeName", strParam, dbType, objValue);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        /// <summary>
        /// Update Tribute Privacy
        /// </summary>
        /// <param name="objtribute"></param>
        public void UpdateTributePrivacy(object[] objtribute)
        {

            Tributes objTributeTheme = (Tributes)objtribute[0];
            if (!Equals(objTributeTheme, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {"UserId",  
                                            Tributes.TributeEnum.TributeId.ToString(),
                        Tributes.TributeEnum.IsPrivate.ToString(),
                         "GoogleAdSense"
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {
                                        DbType.Int64,
                                        DbType.Int64,
                        DbType.Boolean, DbType.Boolean                     
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { objTributeTheme.UserTributeId,
                                            objTributeTheme.TributeId,
                        objTributeTheme.IsPrivate, objTributeTheme.GoogleAdSense
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_UpdateTributePrivacy", strParam, dbType, objValue);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        /// <summary>
        /// Delete Tribute
        /// </summary>
        /// <param name="objtribute"></param>
        public void DeleteTribute(object[] objtribute)
        {

            Tributes objTributeTheme = (Tributes)objtribute[0];
            if (!Equals(objTributeTheme, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {"UserId",  
                                            Tributes.TributeEnum.TributeId.ToString()
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {
                                        DbType.Int64,
                                        DbType.Int64                                        
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { objTributeTheme.UserTributeId,
                                            objTributeTheme.TributeId
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_DeleteTribute", strParam, dbType, objValue);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        /// <summary>
        /// Method to update Tribute theme
        /// </summary>
        /// <param name="objTheme">Object containing Tribute entity.</param>
        public void UpdateTributeMessage(object[] objTheme)
        {
            Tributes objTributeTheme = (Tributes)objTheme[0];

            if (!Equals(objTributeTheme, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {Tributes.TributeEnum.WelcomeMessage.ToString(),  
                                            Tributes.TributeEnum.TributeId.ToString(),
                                            Tributes.TributeEnum.ModifiedBy.ToString()                                            
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.String,
                                        DbType.Int64,
                                        DbType.Int64                                        
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { objTributeTheme.WelcomeMessage,
                                            objTributeTheme.TributeId,
                                            objTributeTheme.ModifiedBy                                            
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_UpdateTributeMessage", strParam, dbType, objValue);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<UserInfo> GetTributeAdminis(object[] objUserTributeId)
        {
            TributeAdministrator objTribute = (TributeAdministrator)objUserTributeId[0];
            List<UserInfo> objUserInfo = new List<UserInfo>();
            if (!Equals(objTribute, null))
            {
                object[] objParam = { objTribute.UserTributeId };
                DataSet dsTributeAdmins = GetDataSet("usp_GetAdministrators", objParam);

                //to fill records in entity
                if (dsTributeAdmins.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTributeAdmins.Tables[0].Rows)
                    {
                        //string UserName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                        UserInfo objUser = new UserInfo();
                        objUser.UserID = int.Parse(dr["UserId"].ToString());
                        objUser.FirstName = dr["FirstName"].ToString();
                        objUser.LastName = dr["LastName"].ToString();
                        objUser.UserEmail = dr["Email"].ToString();
                        objUser.IsOwner = bool.Parse(dr["IsOwner"].ToString());
                        objUser.UserType = dr["usertype"].ToString();
                        objUserInfo.Add(objUser);
                        objUser = null;
                    }
                }
            }
            return objUserInfo;
        }

        /// <summary>
        /// Function to get the list of Tribute Administrators for the Tribute id
        /// </summary>
        /// <param name="objUserTributeId">Entity containing User Tribute id</param>
        /// <returns>List of Administrators constaing email ids</returns>

        public List<UserInfo> GetTributeAdministrators(object[] objUserTributeId)
        {
            Tributes objTribute = (Tributes)objUserTributeId[0];
            List<UserInfo> objUserInfo = new List<UserInfo>();
            if (!Equals(objTribute, null))
            {
                object[] objParam = { objTribute.TributeId, objTribute.TypeDescription };
                DataSet dsTributeAdmins = GetDataSet("usp_GetTributeAdministrators", objParam);

                //to fill records in entity
                if (dsTributeAdmins.Tables.Count > 0)
                {
                    if (dsTributeAdmins.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsTributeAdmins.Tables[0].Rows)
                        {
                            UserInfo objUser = new UserInfo();
                            objUser.UserID = int.Parse(dr["UserId"].ToString());
                            objUser.FirstName = dr["FirstName"].ToString();
                            objUser.LastName = dr["LastName"].ToString();
                            objUser.UserEmail = dr["Email"].ToString();
                            objUser.IsOwner = bool.Parse(dr["IsOwner"].ToString());
                            objUserInfo.Add(objUser);
                            objUser = null;
                        }
                    }
                }
            }
            return objUserInfo;
        }

        /// <summary>
        /// fetch tribute admin on tributeid
        /// </summary>
        /// <param name="objTribute"></param>
        /// <returns></returns>
        public List<UserInfo> GetTributeAdmins(Tributes objTribute)
        {
            List<UserInfo> objUserInfo = new List<UserInfo>();
            if (!Equals(objTribute, null))
            {
                object[] objParam = { objTribute.TributeId };
                DataSet dsTributeAdmins = GetDataSet("usp_GetTributeAdmins", objParam);

                //to fill records in entity
                if (dsTributeAdmins.Tables.Count > 0)
                {
                    if (dsTributeAdmins.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsTributeAdmins.Tables[0].Rows)
                        {
                            UserInfo objUser = new UserInfo();
                            objUser.UserID = int.Parse(dr["UserId"].ToString());
                            objUser.FirstName = dr["FirstName"].ToString();
                            objUser.LastName = dr["LastName"].ToString();
                            objUser.UserEmail = dr["Email"].ToString();
                            objUser.IsOwner = bool.Parse(dr["IsOwner"].ToString());
                            objUserInfo.Add(objUser);
                            objUser = null;
                        }
                    }
                }
            }
            return objUserInfo;
        }


        /// <summary>
        /// Function to get the list of Tribute Administrators whether email alert is true or false
        /// </summary>
        /// <param name="objUserTributeId">Entity containing User Tribute id</param>
        /// <returns>List of Administrators constaing email ids</returns>

        public List<UserInfo> GetAdministrators(object[] objUserTributeId)
        {
            Tributes objTribute = (Tributes)objUserTributeId[0];
            List<UserInfo> objUserInfo = new List<UserInfo>();
            if (!Equals(objTribute, null))
            {
                object[] objParam = { objTribute.TributeId };
                DataSet dsTributeAdmins = GetDataSet("usp_GetAdministrators", objParam);

                //to fill records in entity
                if (dsTributeAdmins.Tables.Count > 0)
                {
                    if (dsTributeAdmins.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsTributeAdmins.Tables[0].Rows)
                        {
                            UserInfo objUser = new UserInfo();
                            objUser.UserID = int.Parse(dr["UserId"].ToString());
                            objUser.FirstName = dr["FirstName"].ToString();
                            objUser.LastName = dr["LastName"].ToString();
                            objUser.UserEmail = dr["Email"].ToString();
                            objUser.IsOwner = bool.Parse(dr["IsOwner"].ToString());
                            objUserInfo.Add(objUser);
                            objUser = null;
                        }
                    }
                }
            }
            return objUserInfo;
        }

        /// <summary>
        /// Function to get the list of users who have added the Tribute in their list of favourites
        /// </summary>
        /// <param name="objUserTributeId"></param>
        /// <returns></returns>
        public List<UserInfo> GetFavouriteTributeUsers(object[] objUserTributeId)
        {
            Tributes objTribute = (Tributes)objUserTributeId[0];
            List<UserInfo> objUserInfo = new List<UserInfo>();
            if (!Equals(objTribute, null))
            {
                object[] objParam = { objTribute.TributeId, objTribute.TypeDescription };
                DataSet dsTributeFav = GetDataSet("usp_GetFavouriteTributes", objParam);

                //to fill records in entity
                if (dsTributeFav.Tables.Count > 0)
                {
                    if (dsTributeFav.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsTributeFav.Tables[0].Rows)
                        {
                            UserInfo objUser = new UserInfo();
                            objUser.UserID = int.Parse(dr["UserId"].ToString());
                            objUser.FirstName = dr["FirstName"].ToString();
                            objUser.LastName = dr["LastName"].ToString();
                            objUser.UserEmail = dr["Email"].ToString();
                            objUser.UserName = dr["Username"].ToString();
                            //objUser.IsOwner = bool.Parse(dr["IsOwner"].ToString());
                            objUserInfo.Add(objUser);
                            objUser = null;
                        }
                    }
                }
            }
            return objUserInfo;
        }


        /// <summary>
        /// Function to get the email address of tribute owner
        /// </summary>
        /// <param name="objUserTributeId">Entity containing User Tribute id</param>
        /// <returns>Owner email address</returns>
        public UserInfo GetTributeOwner(object[] objUserTributeId)
        {
            Tributes objTribute = (Tributes)objUserTributeId[0];
            //Videos objTribute = (Videos)objUserTributeId[0];
            UserInfo objUserInfo = new UserInfo();
            if (!Equals(objTribute, null))
            {
                object[] objParam = { objTribute.TributeId };
                DataSet dsTributeAdmins = GetDataSet("usp_GetTributeOwner", objParam);

                //to fill records in entity
                if (dsTributeAdmins.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTributeAdmins.Tables[0].Rows)
                    {
                        objUserInfo.FirstName = dr["FirstName"].ToString();
                        objUserInfo.UserEmail = dr["Email"].ToString();
                    }
                }
            }
            return objUserInfo;
        }

        /// <summary>
        /// LHK: get all latest date to traverse get latest activities
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public List<DateTime> GetAllLatestDates(int tributeId)
        {
            List<DateTime> objListDatetime = new List<DateTime>();
            TributeLatest objTribute = new TributeLatest();

            if (tributeId > 0)
            {
                objTribute.TributeId = tributeId;
                DateTime objDateToday = new DateTime();

                objDateToday = DateTime.Now;
                objTribute.CreationDate = objDateToday;
                object[] objParam = { objTribute.TributeId, objTribute.CreationDate};
                DataSet dsTribute = GetDataSet("usp_GetAllLatestDates", objParam);
                if (dsTribute.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTribute.Tables[0].Rows)
                    {
                        DateTime objDate = new DateTime();
                        DateTime.TryParse(dr["CreatedDate"].ToString(), out objDate);
                        objListDatetime.Add(objDate);
                    }
                }
            }
            return objListDatetime;
        }

        /// <summary>
        /// Function for Getting Latest Video.
        /// </summary>
        /// <param name="objWebstatics"></param>
        public List<TributeLatest> GetLatestVideos(object[] objTributeLatest)
        {
            TributeLatest objTribute = (TributeLatest)objTributeLatest[0];
            List<TributeLatest> objtribInfo = new List<TributeLatest>();
            try
            {
                if (!Equals(objTribute, null))
                {
                    object[] objParam = { objTribute.TributeId, objTribute.CreationDate, objTribute.SecondDate };
                    DataSet dsTribute = GetDataSet("usp_GetLatestVideo", objParam);
                    if (dsTribute.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsTribute.Tables[0].Rows)
                        {
                            TributeLatest objTribute_ = new TributeLatest();
                            objTribute_.UserId = int.Parse(dr["Userid"].ToString());
                            objTribute_.FirstName = dr["FirstName"].ToString();
                            objTribute_.VideoCaption = dr["VideoCaption"].ToString();
                            objTribute_.VideoTypeId = dr["VideoTypeId"].ToString();
                            objTribute_.VideoUrl = dr["VideoUrl"].ToString();
                            objTribute_.VideoDesc = dr["Message"].ToString();
                            objTribute_.Type_ = dr["Type"].ToString();
                            objTribute_.ID = int.Parse(dr["ID"].ToString());
                            objTribute_.VideoTributeUrl = dr["TributeVideoId"].ToString(); //Added by Gaurav Puri on 16-May-2008
                            objTribute_.Mode = dr["MODE"].ToString();
                            objtribInfo.Add(objTribute_);
                            objTribute_ = null;
                        }
                    }
                }
                return objtribInfo;
            }
            catch (Exception ex)
            {
                Errors obj = new Errors();
                obj.ErrorMessage = ex.ToString();
                objTribute.CustomError = obj;
                return objtribInfo;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objParam"></param>
        public void GetTributeTypebyCode(object[] objParam)
        {
            Tributes _objTributeUserInfo = (Tributes)objParam[0];
            object[] _objParam = { _objTributeUserInfo.TributeId };
            DataSet ds = GetDataSet("usp_TributeTypebyCode", _objParam);
            // ds.Tables[0].
            int count = ds.Tables[0].Rows.Count;
            if (count > 0)
            {

                _objTributeUserInfo.TypeDescription = ds.Tables[0].Rows[0]["TypeDescription"].ToString();

            }
        }

        /// <summary>
        /// This function will check the story visibility.
        /// </summary>
        /// <param name="objUserAdmin"></param>
        /// <returns></returns>
        public bool IsStoryAdded(object[] objUserAdmin)
        {
            bool isOwner = false;
            UserAdminOwnerInfo objUserInfo = (UserAdminOwnerInfo)objUserAdmin[0];
            if (!Equals(objUserAdmin, null))
            {
                try
                {
                    object[] objparam = { objUserInfo.UserId, 
                                         objUserInfo.TributeId
                                        };
                    DataSet dsIsAdmin = GetDataSet("usp_GetStoryStatus", objparam);
                    int count = int.Parse(dsIsAdmin.Tables[0].Rows[0][0].ToString());
                    if (count > 0)
                        isOwner = false;
                    else
                        isOwner = true;
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        Errors objError = new Errors();
                        objError.ErrorMessage = sqlEx.Message;
                        return isOwner;
                    }
                }
            }
            return isOwner;
        }

        public bool IsStoryHide(int TributeId)
        {
            bool isOwner = true;
            //UserAdminOwnerInfo objUserInfo = (UserAdminOwnerInfo)objUserAdmin[0];
            if (!Equals(TributeId, null))
            {
                try
                {
                    object[] objparam = {
                                         TributeId
                                        };
                    DataSet dsIsAdmin = GetDataSet("usp_GetStoryVisibility", objparam);
                    int count = dsIsAdmin.Tables[0].Rows.Count;
                    if (count > 0)
                    {
                        isOwner = bool.Parse(dsIsAdmin.Tables[0].Rows[0][0].ToString());
                    }
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        Errors objError = new Errors();
                        objError.ErrorMessage = sqlEx.Message;
                        return isOwner;
                    }
                }
            }
            return isOwner;
        }


        public void setStoryVisibility(int TributeId)
        {
            try
            {
                //sets the parameters
                string[] strParam = {"TributeId"                                           
                                    };
                //sets the types of parameters
                DbType[] dbType = {
                                    DbType.Int64
                };

                //sets the values in the entity to the parameters
                object[] objValue = { TributeId };

                //sends request to insert record
                base.InsertRecord("usp_SetStoryVisibility", strParam, dbType, objValue);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Method to add tribute to user favorite list 
        /// </summary>
        /// <param name="objAddToFavorite">Filled AddToFavorite entity.</param>
        /// <returns>0 if value added else > 0 (already in favorite list)</returns>
        public int AddToFavorites(object[] objAddToFavorite)
        {
            AddToFavorite objFavorite = (AddToFavorite)objAddToFavorite[0];
            object objReturnVal = null;
            if (!Equals(objFavorite, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {AddToFavorite.AddToFavoriteEnum.UserId.ToString(),
                                            AddToFavorite.AddToFavoriteEnum.TributeId.ToString(),
                                            AddToFavorite.AddToFavoriteEnum.EmailAlert.ToString(),
                                            AddToFavorite.AddToFavoriteEnum.CreatedBy.ToString(),
                                            AddToFavorite.AddToFavoriteEnum.CreatedDate.ToString(),
                                            AddToFavorite.AddToFavoriteEnum.IsActive.ToString(),
                                            AddToFavorite.AddToFavoriteEnum.IsDeleted.ToString()
                                    };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64, DbType.Int64, DbType.Boolean,
                                    DbType.Int64, DbType.DateTime,
                                    DbType.Boolean, DbType.Boolean};

                    //sets the values in the entity to the parameters
                    object[] objValue = {objFavorite.UserId,
                                            objFavorite.TributeId,
                                            objFavorite.EmailAlert,
                                            objFavorite.CreatedBy,
                                            objFavorite.CreatedDate,
                                            objFavorite.IsActive,
                                            objFavorite.IsDeleted
                                        };

                    //sends request to insert record
                    objReturnVal = base.InsertDataAndReturnId("usp_AddToFavorite", strParam, dbType, objValue);
                    return (int)objReturnVal;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return 0;
            }
        }

        //uspGetTributeSession

        /// <summary>
        /// Get Values in Session.
        /// </summary>
        /// <param name="objTribute"></param>
        public void GetTributeSession(Tributes objTribute)
        {
            try
            {
                if (!Equals(objTribute, null))
                {
                    object[] objParam = { objTribute.TributeId };
                    DataSet dsTribute = GetDataSet("uspGetTributeSession", objParam);
                    if (dsTribute.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsTribute.Tables[0].Rows)
                        {
                            objTribute.TributeName = dr["TributeName"].ToString();
                            objTribute.TypeDescription = dr["TypeDescription"].ToString();
                            objTribute.TributeUrl = dr["TributeUrl"].ToString();
                            objTribute.GoogleAdSense = bool.Parse(dr["GoogleAdSense"].ToString());
                            objTribute.IsActive = bool.Parse(dr["IsActive"].ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Errors obj = new Errors();
                obj.ErrorMessage = ex.ToString();
                // objTribute.CustomError = obj;
                // return objtribInfo;
            }
        }

        /// <summary>
        /// Method to add tribute Count
        /// </summary>
        /// <param name="objAddToFavorite">Filled AddToFavorite entity.</param>
        /// <returns>0 if value added else > 0 (already in favorite list)</returns>
        public void AddTributeCount(int TributeId)
        {


            if (!Equals(TributeId, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {"SectionTypeCodeId",
                                            "SectionTypeID",
                                            "Operation",
                                            "UserId",
                                            "SessionId",
                                            "Notes",
                                            "UserType"
                                    };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.String, DbType.Int64, 
                                    DbType.String,
                                    DbType.Int64, DbType.Int64,
                                    DbType.String, DbType.String};

                    //sets the values in the entity to the parameters
                    object[] objValue = {"Tribute",
                                            TributeId,
                                            'V',
                                            1,
                                            1,
                                            null,
                                            "Basic"
                                        };

                    //sends request to insert record
                    base.InsertRecord("usp_InsertWebstatics", strParam, dbType, objValue);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        /// <summary>
        /// Method to get if user is having selected tribute in his favorite list.
        /// </summary>
        /// <param name="objUserFavorite">AddToFavorite entity containing TributeId and UserId</param>
        /// <returns>Count for favorite, if 0 not in favorite list else in favorite list.</returns>
        public int GetUserTributeFavorites(object[] objUserFavorite)
        {
            AddToFavorite objFavorite = (AddToFavorite)objUserFavorite[0];

            if (!Equals(objUserFavorite, null))
            {
                try
                {
                    object[] param = { objFavorite.TributeId, objFavorite.UserId };

                    //sends request to get count
                    DataSet ds = base.GetDataSet("usp_GetUserTributeFavorite", param);
                    if (ds.Tables.Count > 0)
                        return int.Parse(ds.Tables[0].Rows[0][0].ToString());
                    else
                        return 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Method to remove favorite from users list.
        /// </summary>
        /// <param name="objRemoveFavorite">Filled AddToFavorite entity.</param>
        public void RemoveFromFavorites(object[] objRemoveFavorite)
        {
            AddToFavorite objFavorite = (AddToFavorite)objRemoveFavorite[0];

            if (!Equals(objFavorite, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {AddToFavorite.AddToFavoriteEnum.UserId.ToString(),
                                            AddToFavorite.AddToFavoriteEnum.TributeId.ToString(),
                                            AddToFavorite.AddToFavoriteEnum.ModifiedBy.ToString(),
                                            AddToFavorite.AddToFavoriteEnum.ModifiedDate.ToString(),
                                            AddToFavorite.AddToFavoriteEnum.IsDeleted.ToString()
                                    };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64, DbType.Int64, 
                                    DbType.Int64, DbType.DateTime,
                                    DbType.Boolean};

                    //sets the values in the entity to the parameters
                    object[] objValue = {objFavorite.UserId,
                                            objFavorite.TributeId,
                                            objFavorite.ModifiedBy,
                                            objFavorite.ModifiedDate,
                                            objFavorite.IsDeleted
                                        };

                    //sends request to update record
                    base.InsertRecord("usp_RemoveTributeFromFavorite", strParam, dbType, objValue);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Method to get the search tributes based on the entered criteria.
        /// </summary>
        /// <param name="objTribute">Filled TributeSearch entity containing search criteria.</param>
        /// <returns>List of tributes.</returns>
        public List<TributeSearch> SearchTributes(object[] objTribute)
        {
            List<TributeSearch> objTributeList = new List<TributeSearch>();
            TributeSearch objTrib = (TributeSearch)objTribute[0];

            object[] objParam = {objTrib.SearchTributeId, objTrib.TributeName, objTrib.UserName, objTrib.SearchUserId,
                                    objTrib.City, objTrib.State, objTrib.Country, objTrib.TypeDescription,
                                    objTrib.TributeStatus, objTrib.CreatedAfter, objTrib.CreatedBefore,
                                    objTrib.PurchasedAfter, objTrib.PurchasedBefore,objTrib.ApplicationType};

            DataSet dsTributes = GetDataSet("usp_SearchTribute", objParam);

            if (dsTributes.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsTributes.Tables[0].Rows)
                {
                    TributeSearch obj = new TributeSearch();
                    //add data to tribute object
                    //Tributes objTributes = new Tributes();
                    obj.TributeId = int.Parse(dr["TributeId"].ToString());
                    obj.TributeName = dr["TributeName"].ToString();
                    obj.City = dr["City"].ToString();
                    obj.TypeDescription = dr["TributeTypeName"].ToString();
                    obj.CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString());
                    obj.CreationDate = DateTime.Parse(dr["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");
                    //set tribute entity to TributeSearch.Tributes
                    //obj.Tributes = objTributes;

                    //set tribute serach entity
                    obj.StateName = dr["StateName"].ToString();
                    obj.CountryName = dr["CountryName"].ToString();
                    obj.TributeStatus = dr["TributeStatus"].ToString();
                    obj.EndDate = dr["EndDate"].ToString() == string.Empty ? string.Empty : DateTime.Parse(dr["EndDate"].ToString()).ToString("MMMM dd, yyyy");
                    obj.IsActive = bool.Parse(dr["IsActive"].ToString());
                    //set usertributeId ---03-Feb-09---- ANKI
                    obj.UserTributeId = int.Parse(dr["CreatedBy"].ToString());
                    obj.TributeUrl = dr["TributeUrl"].ToString();
                    //add data to list
                    objTributeList.Add(obj);
                    //objTributes = null;
                    obj = null;
                }
            }

            return objTributeList;
        }

        /// <summary>
        /// Method to get Tribute Details for session based on Tribute Url and TributeType.
        /// </summary>
        /// <param name="objTribute">Tribute entity containing Tribute Url and Tribute Type.</param>
        /// <returns>Filled Tributes entity.</returns>
        public Tributes GetTributeSessionForUrlAndType(object[] objTribute,string ApplicationType)
        {
            Tributes objTrib = (Tributes)objTribute[0];
            try
            {
                if (!Equals(objTrib, null))
                {
                    object[] objParam = { objTrib.TributeUrl, objTrib.TypeDescription, ApplicationType };
                    DataSet dsTribute = GetDataSet("usp_GetTributeDetails", objParam);
                    if (dsTribute.Tables.Count > 0 && dsTribute.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsTribute.Tables[0].Rows)
                        {
                            objTrib.TributeId = int.Parse(dr["TributeId"].ToString());
                            objTrib.TributeName = dr["TributeName"].ToString();
                            objTrib.TypeDescription = dr["TypeDescription"].ToString();
                            objTrib.TributeUrl = dr["TributeUrl"].ToString();
                            objTrib.TributeImage = dr["TributeImage"].ToString();
                            objTrib.ThemeId = int.Parse(dr["ThemeId"].ToString());
                            if (!string.IsNullOrEmpty(dr["CreatedDate"].ToString()))
                                objTrib.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                            if (!string.IsNullOrEmpty(dr["Date2"].ToString()))
                                objTrib.Date2 = Convert.ToDateTime(dr["Date2"].ToString());
                            objTrib.GoogleAdSense = bool.Parse(dr["GoogleAdSense"].ToString());
                            objTrib.IsActive = bool.Parse(dr["IsActive"].ToString());
                            objTrib.TributePackageType = dr["TributePackageType"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Errors obj = new Errors();
                obj.ErrorMessage = ex.ToString();
                // objTribute.CustomError = obj;
                // return objtribInfo;
            }
            return objTrib;
        }

        // Getting tribute details from TributeID
        public Tributes GetTributeSessionForId(object[] objTribute)
        {
            Tributes objTrib = (Tributes)objTribute[0];
            try
            {
                if (!Equals(objTrib, null))
                {
                    object[] objParam = { objTrib.TributeId };
                    DataSet dsTribute = GetDataSet("usp_GetTributeDetailfromTributeId", objParam);
                    if (dsTribute.Tables.Count > 0 && dsTribute.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsTribute.Tables[0].Rows)
                        {
                            objTrib.TributeId = int.Parse(dr["TributeId"].ToString());
                            objTrib.TributeName = dr["TributeName"].ToString();
                            objTrib.TypeDescription = dr["TypeDescription"].ToString();
                            objTrib.TributeUrl = dr["TributeUrl"].ToString();
                            objTrib.TributeImage = dr["TributeImage"].ToString();
                            objTrib.ThemeId = int.Parse(dr["ThemeId"].ToString());
                            if (!string.IsNullOrEmpty(dr["CreatedDate"].ToString()))
                                objTrib.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                            if (!string.IsNullOrEmpty(dr["Date2"].ToString()))
                                objTrib.Date2 = Convert.ToDateTime(dr["Date2"].ToString());
                            if (!string.IsNullOrEmpty(dr["IsOrderDVDChecked"].ToString()))
                                objTrib.IsOrderDVDChecked = bool.Parse(dr["IsOrderDVDChecked"].ToString());
                            if (!string.IsNullOrEmpty(dr["IsMemTributeBoxChecked"].ToString()))
                                objTrib.IsMemTributeBoxChecked = bool.Parse(dr["IsMemTributeBoxChecked"].ToString());
                            objTrib.GoogleAdSense = bool.Parse(dr["GoogleAdSense"].ToString());
                            objTrib.IsActive = bool.Parse(dr["IsActive"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Errors obj = new Errors();
                obj.ErrorMessage = ex.ToString();
                // objTribute.CustomError = obj;
                // return objtribInfo;
            }
            return objTrib;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTributeName"></param>
        /// <returns></returns>
        public string SequenceTributeName(string strTributeName, string strTributeType)
        {
            object[] param = { strTributeName, strTributeType };

            //sends request to get count
            DataSet ds = base.GetDataSet("usp_GetSequenceTribute", param);
            if (ds.Tables.Count > 0)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return strTributeName;

        }

        /// <summary>
        /// Check whether Tribute exists or not
        /// </summary>
        /// <param name="TributeId"></param>
        /// <returns></returns>
        public bool IsTributeExists(int TributeId)
        {
            object[] param = { TributeId };

            //sends request to get count
            DataSet ds = base.GetDataSet("usp_IsTributeExists", param);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return true;
            return false;
        }


        /// <summary>
        /// This is used for the Donation Project
        /// This function fetches the information about a charity.
        /// </summary>
        /// <param name="objTributeDonation">used to specify the tribute id</param>
        public void GetDonationInfo(object[] objTributeDonation)
        {
            Donation objDonation = (Donation)objTributeDonation[0];
            if (!Equals(objDonation, null))
            {
                object[] param = { objDonation.TributeID };
                DataSet dsDonation = GetDataSet("usp_GetDonation", param);
                //check whether user wants to show/hide donation details on the tribute
                if (dsDonation.Tables.Count > 0)
                {
                    if (dsDonation.Tables[0].Rows.Count > 0)
                    {
                        objDonation.CharityName = dsDonation.Tables[0].Rows[0][0].ToString();
                        objDonation.DonationUrl = dsDonation.Tables[0].Rows[0][1].ToString();
                    }
                }
            }
        }

        /// <summary>
        /// This is used for the Donation Project
        /// This function updates the information about a charity.
        /// </summary>
        /// <param name="objTributes"></param>
        public void UpdateDonationDetails(object[] objTributes)
        {
            Donation objDonation = (Donation)objTributes[0];
            //Tributes objTribute = (Tributes)objTributes[1];
            if (!Equals(objTributes, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {
                                            "TributeId",  
                                            "IsDonation",
                                            "CharityName",
                                            "DonationPageURL",
                                            "ModifiedBy"
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {
                                            DbType.Int64,
                                            DbType.Boolean,
                                            DbType.String,
                                            DbType.String,
                                            DbType.Int64
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { 
                                            objDonation.TributeID,
                                            (bool)objTributes[1],
                                            objDonation.CharityName,
                                            objDonation.DonationUrl,
                                            objDonation.ModifiedBy
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_UpdateDonation", strParam, dbType, objValue);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //AG:   
        public void UpdateTributePackage(int tributeId, string tributePackageType)
        {
            if (!Equals(tributeId, 0))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {
                                            "TributeId",  
                                            "TributePackageType"
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {
                                            DbType.Int64,                                            
                                            DbType.String
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { 
                                            tributeId,
                                            tributePackageType
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_UpdateTributePackageType", strParam, dbType, objValue);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public object[] GetTributeDetailOnTributeId(int tributeId)
        {
            object[] retObj = new object[5];
            if (!Equals(tributeId, 0))
            {
                object[] param = { tributeId };
                DataSet dsTributes = GetDataSet("usp_GetTributeDetailOnTributeId", param);

                if (dsTributes.Tables.Count > 0)
                {
                    if (dsTributes.Tables[0].Rows.Count > 0)
                    {
                        retObj[0] = dsTributes.Tables[0].Rows[0][0].ToString();
                        retObj[1] = dsTributes.Tables[0].Rows[0][1].ToString();
                        retObj[2] = dsTributes.Tables[0].Rows[0][2].ToString();
                        retObj[3] = dsTributes.Tables[0].Rows[0][3].ToString();
                        retObj[4] = dsTributes.Tables[0].Rows[0][4].ToString();
                    }
                }
            }
            return retObj;
        }

        public UpdateTribute GetTrbDetailOnTributeId(int tributeId)
        {
            UpdateTribute objtrb = new UpdateTribute();
            
            try
            {
                if (!Equals(tributeId, 0))
                {
                    object[] param = { tributeId };
                    DataSet dsTributes = GetDataSet("usp_GetTributeDetailOnTributeId", param);

                    if (dsTributes.Tables.Count > 0)
                    {
                        if (dsTributes.Tables[0].Rows.Count > 0)
                        {
                            objtrb.TributeId = int.Parse(dsTributes.Tables[0].Rows[0][0].ToString());
                            objtrb.TributeName = dsTributes.Tables[0].Rows[0][1].ToString();
                            objtrb.TributeUrl = dsTributes.Tables[0].Rows[0][2].ToString();
                            objtrb.TypeDescription = dsTributes.Tables[0].Rows[0][3].ToString();
                            objtrb.PackageId = int.Parse(dsTributes.Tables[0].Rows[0][4].ToString());
                            objtrb.TributeType = int.Parse(dsTributes.Tables[0].Rows[0][5].ToString());
                            objtrb.CreatedBy = int.Parse(dsTributes.Tables[0].Rows[0][6].ToString());
                            objtrb.PackageName = dsTributes.Tables[0].Rows[0][7].ToString();
                            if (!(string.IsNullOrEmpty(dsTributes.Tables[0].Rows[0][8].ToString())))
                                objtrb.EndDate = DateTime.Parse(dsTributes.Tables[0].Rows[0][8].ToString());
                            if (!(string.IsNullOrEmpty(dsTributes.Tables[0].Rows[0][9].ToString())))
                                objtrb.Date1 = DateTime.Parse(dsTributes.Tables[0].Rows[0][9].ToString());
                            if (!(string.IsNullOrEmpty(dsTributes.Tables[0].Rows[0][10].ToString())))
                                objtrb.Date2 = DateTime.Parse(dsTributes.Tables[0].Rows[0][10].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return objtrb;
            }
            return objtrb;
        }

        public object[] GetTributeUserDetailOnTributeId(int tributeId)
        {
            object[] retObj = new object[13];
            if (!Equals(tributeId, 0))
            {
                object[] param = { tributeId };
                DataSet dsTributes = GetDataSet("usp_GetTributeUserDetailOnTributeId", param);

                if (dsTributes.Tables.Count > 0)
                {
                    if (dsTributes.Tables[0].Rows.Count > 0)
                    {
                        retObj[0] = dsTributes.Tables[0].Rows[0][0].ToString();
                        retObj[1] = dsTributes.Tables[0].Rows[0][1].ToString();
                        retObj[2] = dsTributes.Tables[0].Rows[0][2].ToString();
                        retObj[3] = dsTributes.Tables[0].Rows[0][3].ToString();
                        retObj[4] = dsTributes.Tables[0].Rows[0][4].ToString();
                        retObj[5] = dsTributes.Tables[0].Rows[0][5].ToString();
                        retObj[6] = dsTributes.Tables[0].Rows[0][6].ToString();
                        retObj[7] = dsTributes.Tables[0].Rows[0][7].ToString();
                        retObj[8] = dsTributes.Tables[0].Rows[0][8].ToString();
                        retObj[9] = dsTributes.Tables[0].Rows[0][9].ToString();
                        retObj[10] = dsTributes.Tables[0].Rows[0][10].ToString();
                        retObj[11] = dsTributes.Tables[0].Rows[0][11].ToString();
                        retObj[12] = dsTributes.Tables[0].Rows[0][12].ToString();
                    }
                }
            }
            return retObj;
        }

        public void Deletecategory(object[] objRowID)
        {
            if (!Equals(objRowID, null))
            {
                try
                {
                    base.Delete("usp_Deletecategory", objRowID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void DeleteTheme(object[] objRowID)
        {
            if (!Equals(objRowID, null))
            {
                try
                {
                    base.Delete("usp_DeleteTheme", objRowID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="videoTributeId"></param>
        /// <param name="tributeId"></param>
        public void LinkVideoTribute(LinkVideoMemorialTribute objLinkTribute)
        {
            if (!Equals(objLinkTribute, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {
                                            "UserId",
                                            "VideoTributeId",  
                                            "MemTributeId"
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {
                                            DbType.Int64,
                                            DbType.Int64,
                                            DbType.Int64
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { 
                                            objLinkTribute.UserId,
                                            objLinkTribute.VideoTributeId,
                                            objLinkTribute.MemTributeId
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_InsertLinkVideoMemTribute", strParam, dbType, objValue);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objParam"></param>
        /// <returns></returns>
        public Tributes GetVideoTributeDetailById(int videoTributeId)
        {
            Tributes objTrib = new Tributes();
            try
            {
                if (!Equals(videoTributeId, 0))
                {
                    object[] objParam = { videoTributeId };
                    DataSet dsTribute = GetDataSet("usp_GetTributeDetailfromTributeId", objParam);
                    if (dsTribute.Tables.Count > 0 && dsTribute.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsTribute.Tables[0].Rows)
                        {
                            objTrib.TributeId = int.Parse(dr["TributeId"].ToString());
                            objTrib.TributeName = dr["TributeName"].ToString();
                            objTrib.TypeDescription = dr["TypeDescription"].ToString();
                            objTrib.TributeUrl = dr["TributeUrl"].ToString();
                            objTrib.TributeImage = dr["TributeImage"].ToString();
                            objTrib.ThemeId = int.Parse(dr["ThemeId"].ToString());
                            if (!string.IsNullOrEmpty(dr["CreatedDate"].ToString()))
                                objTrib.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                            if (!string.IsNullOrEmpty(dr["Date1"].ToString()))
                                objTrib.Date1 = Convert.ToDateTime(dr["Date1"].ToString());
                            if (!string.IsNullOrEmpty(dr["Date2"].ToString()))
                                objTrib.Date2 = Convert.ToDateTime(dr["Date2"].ToString());
                            if (!string.IsNullOrEmpty(dr["IsOrderDVDChecked"].ToString()))
                                objTrib.IsOrderDVDChecked = bool.Parse(dr["IsOrderDVDChecked"].ToString());
                            if (!string.IsNullOrEmpty(dr["IsMemTributeBoxChecked"].ToString()))
                                objTrib.IsMemTributeBoxChecked = bool.Parse(dr["IsMemTributeBoxChecked"].ToString());
                            objTrib.GoogleAdSense = bool.Parse(dr["GoogleAdSense"].ToString());
                            objTrib.IsActive = bool.Parse(dr["IsActive"].ToString());
                            if (!string.IsNullOrEmpty(dr["City"].ToString()))
                                objTrib.City = dr["City"].ToString();
                            if (!string.IsNullOrEmpty(dr["State"].ToString()))
                                objTrib.State = int.Parse(dr["State"].ToString());
                            if (!string.IsNullOrEmpty(dr["Country"].ToString()))
                                objTrib.Country = int.Parse(dr["Country"].ToString());
                            if (!string.IsNullOrEmpty(dr["StateName"].ToString()))
                                objTrib.Attribute1 = dr["StateName"].ToString();
                            if (!string.IsNullOrEmpty(dr["CountryName"].ToString()))
                                objTrib.Attribute2 = dr["CountryName"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Errors obj = new Errors();
                obj.ErrorMessage = ex.ToString();
                // objTribute.CustomError = obj;
                // return objtribInfo;
            }
            return objTrib;
        }

        public IList<Themes> GetSubCategoryForTheme(string categoryName)
        {
            IList<Themes> objThemeList = new List<Themes>();
            Themes objTheme;
            if (!string.IsNullOrEmpty(categoryName))
            {
                object[] param = { categoryName };
                DataSet dsThemes = GetDataSet("usp_GetSubCategoryForTributeType", param);

                if (dsThemes.Tables.Count > 0)
                {
                    if (dsThemes.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsThemes.Tables[0].Rows)
                        {
                            objTheme = new Themes();
                            if (!string.IsNullOrEmpty(dr["TributeType"].ToString()))
                                objTheme.Tributetype = dr["TributeType"].ToString();

                            if (!string.IsNullOrEmpty(dr["SubCategory"].ToString()))
                                objTheme.SubCategory = dr["SubCategory"].ToString();

                            objThemeList.Add(objTheme);
                        }
                    }
                }
            }
            return objThemeList;
        }

        public void UpdateTributeURL(int tributeId, string tributeUrl)
        {
            if (!string.IsNullOrEmpty(tributeUrl))
            {
                Tributes objTribute = new Tributes();
                objTribute.TributeId = tributeId;
                objTribute.TributeUrl = tributeUrl;

                try
                {
                    //sets the parameters
                    string[] strParam = {
                                            Tributes.TributeEnum.TributeId.ToString(),
                                             Tributes.TributeEnum.TributeUrl.ToString()
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {
                                        DbType.Int64,
                                        DbType.String,
                                 
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { 
                                            objTribute.TributeId,
                                            objTribute.TributeUrl
                                        };

                    //sends request to update record and get the identity of the record inserted
                    base.UpdateRecord("usp_UpdateTributeURL", strParam, dbType, objValue);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public string GetTributeUrlOnTributeId(int _TribureId)
        {
            string _trbUrl = "";
            if (_TribureId > 0)
            {
                object[] param = { _TribureId };
                DataSet dsTrbId = GetDataSet("usp_GetTributeUrlOnTributeId", param);

                if (dsTrbId.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dsTrbId.Tables[0].Rows[0]["TributeUrl"].ToString()))
                        _trbUrl = dsTrbId.Tables[0].Rows[0]["TributeUrl"].ToString();
                }
            }
            return _trbUrl;
        }

        public Tributes GetTributeUrlOnOldTributeUrl(Tributes objtribute, string ApplicationType)
        {
            Tributes objTrb = new Tributes();

            if (objtribute!=null)
            {
                object[] param = { objtribute.TributeUrl, objtribute.TypeDescription, ApplicationType };
                DataSet dsTrbId = GetDataSet("usp_GetTributeUrlOnOldTributeUrl", param);

                if (dsTrbId.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dsTrbId.Tables[0].Rows[0]["TributeUrl"].ToString()))
                        objTrb.TributeUrl = dsTrbId.Tables[0].Rows[0]["TributeUrl"].ToString();

                    if (!string.IsNullOrEmpty(dsTrbId.Tables[0].Rows[0]["TypeDescription"].ToString()))
                        objTrb.TypeDescription = dsTrbId.Tables[0].Rows[0]["TypeDescription"].ToString();
                }
            }
            return objTrb;
        }

        /// <summary>
        /// visit count display or not:Ud
        /// </summary>
        /// <param name="_tributeUrl"></param>
        /// <returns></returns>
        public bool GetVisitorCountVisible(string _tributeUrl, string AppliactionType)
        {
            bool isVisitCountVisible = true;
            try
            {
                if (!Equals(_tributeUrl, null))
                {
                    object[] objParam = { _tributeUrl, AppliactionType };
                    DataSet dsUName = GetDataSet("usp_GetVisitorCountVisible", objParam);
                    if (dsUName.Tables[0].Rows.Count > 0)
                    {
                        if (!(dsUName.Tables[0].Rows[0]["IsVisitCountHide"].ToString() == ""))
                        {
                            isVisitCountVisible = bool.Parse(dsUName.Tables[0].Rows[0]["IsVisitCountHide"].ToString());
                        }
                    }
                }
                return isVisitCountVisible;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            //return isVisitCountVisible;
        }

        /// <summary>
        /// get visitor count Display or not :Ud
        /// </summary>
        /// <param name="_trbId"></param>
        /// <returns></returns>
        public bool GetVisitorCountVisible(int _trbId)
        {
            bool isVisitCountVisible = true;
            try
            {
                if (_trbId > 0)
                {
                    object[] objParam = { _trbId };
                    DataSet dsUName = GetDataSet("usp_GetVisitorCountVisibleOnId", objParam);
                    if (dsUName.Tables[0].Rows.Count > 0)
                    {
                        if (!(dsUName.Tables[0].Rows[0]["IsVisitCountHide"].ToString() == ""))
                        {
                            isVisitCountVisible = bool.Parse(dsUName.Tables[0].Rows[0]["IsVisitCountHide"].ToString());
                        }
                    }
                }
                return isVisitCountVisible;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            //return isVisitCountVisible;
        }

        public Tributes GetTributeOnTributeId(int tributeId)
        {
            Tributes objTrb = new Tributes();
            if (!Equals(tributeId, 0))
            {
                object[] param = { tributeId };
                DataSet dsTributes = GetDataSet("usp_GetTributeDetailOnTributeId", param);

                if (dsTributes.Tables.Count > 0)
                {
                    if (dsTributes.Tables[0].Rows.Count > 0)
                    {
                        int TempTrbId = 0;
                        int.TryParse(dsTributes.Tables[0].Rows[0][0].ToString(), out TempTrbId);
                        objTrb.TributeId = TempTrbId;
                        objTrb.TributeName = dsTributes.Tables[0].Rows[0][1].ToString();
                        objTrb.TributeUrl = dsTributes.Tables[0].Rows[0][2].ToString();
                        objTrb.TypeDescription = dsTributes.Tables[0].Rows[0][3].ToString();
                    }
                }
            }
            return objTrb;
        }

        public bool UpdateTributeExpiry(UpdateTribute objUTrb)
        {
            bool OutputResult = false;
            if (objUTrb != null)
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {
                                            UpdateTribute.UpdateTributeEnum.TributeId.ToString(),
                                             UpdateTribute.UpdateTributeEnum.EndDate.ToString()
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {
                                        DbType.Int64,
                                        DbType.DateTime
                                 
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { 
                                            objUTrb.TributeId,
                                            objUTrb.EndDate
                                        };

                    //sends request to update record and get the identity of the record inserted
                    base.UpdateRecord("usp_UpdateTributeEndDate", strParam, dbType, objValue);
                    OutputResult = true;
                }
                catch (Exception ex)
                {
                    OutputResult = false;
                }
            }
            return OutputResult;
        }

        public void UpdateAdminTributeUpdate(AdminTributeUpdate adminTributeUpdate)
        {
            if (adminTributeUpdate != null)
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {
                                            AdminTributeUpdate.AdminTributeUpdateEnum.TributeId.ToString(),
                                             AdminTributeUpdate.AdminTributeUpdateEnum.ChangeType.ToString(),
                                             AdminTributeUpdate.AdminTributeUpdateEnum.OldValue.ToString(),
                                            AdminTributeUpdate.AdminTributeUpdateEnum.NewValue.ToString()
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {
                                        DbType.Int64,
                                        DbType.String,
                                        DbType.String,
                                        DbType.String
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { 
                                            adminTributeUpdate.TributeId,
                                            adminTributeUpdate.ChangeType,
                                            adminTributeUpdate.OldValue,
                                            adminTributeUpdate.NewValue
                                        };

                    //sends request to update record and get the identity of the record inserted
                    base.UpdateRecord("usp_InsertAdminTributeUpdate", strParam, dbType, objValue);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateTributePackage(UpdateTribute updateTribute)
        {
            bool OutputResult = false;
            if (updateTribute != null)
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {
                                            UpdateTribute.UpdateTributeEnum.TributeId.ToString(),
                                            UpdateTribute.UpdateTributeEnum.PackageId.ToString(),
                                            UpdateTribute.UpdateTributeEnum.EndDate.ToString()
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {
                                        DbType.Int64,
                                        DbType.Int64,
                                        DbType.DateTime

                                 
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { 
                                            updateTribute.TributeId,
                                            updateTribute.PackageId,
                                            updateTribute.EndDate
                                        };

                    //sends request to update record and get the identity of the record inserted
                    base.UpdateRecord("usp_UpdateTributePackage", strParam, dbType, objValue);
                    OutputResult = true;
                }
                catch (Exception ex)
                {
                    OutputResult = false;
                }
            }
            return OutputResult;
        }

        public IList<AdminTributeUpdate> GetAdminTransactions()
        {
            IList<AdminTributeUpdate> objList = new List<AdminTributeUpdate>();
                try
                {
                    object[] param = {};
                    DataSet dsThemes = GetDataSet("usp_GetAdminTributeUpdateTrans", param);

                    if (dsThemes.Tables.Count > 0)
                    {
                        if (dsThemes.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsThemes.Tables[0].Rows)
                            {
                                AdminTributeUpdate objATU = new AdminTributeUpdate();
                                objATU.TributeUpdateId = int.Parse(dr["TributeUpdateId"].ToString());
                                objATU.TributeId = int.Parse(dr["TributeId"].ToString());
                                objATU.ChangeType=dr["ChangeType"].ToString();
                                objATU.ModifiedDate = DateTime.Parse(dr["ModifiedDate"].ToString());
                                objATU.OldValue = dr["OldValue"].ToString();
                                objATU.NewValue = dr["NewValue"].ToString();
                                objList.Add(objATU);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return objList;
        }

        public bool IsNewTypeURLValid(Tributes objTribute)
        {
            bool result = false;
            try
            {
                if (objTribute != null)
                {
                    object[] param = { objTribute.TributeUrl, objTribute.TypeDescription, WebConfig.ApplicationType };
                    DataSet dsTrbId = GetDataSet("usp_GetIsNewTypeURLValid", param);

                    if (dsTrbId.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsTrbId.Tables[0].Rows[0]["Result"].ToString()))
                            result = bool.Parse(dsTrbId.Tables[0].Rows[0]["Result"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool UpdateNewTributeUrlTributeTypeinAlltables(UpdateTribute _objUpdateTribute, Tributes _newTribute)
        {
            bool result = false;
            try
            {
                if (_objUpdateTribute != null)
                {
                    object[] param = { _objUpdateTribute.TributeId, _objUpdateTribute.TributeUrl, _objUpdateTribute.TypeDescription, _newTribute.TributeUrl, _newTribute.TypeDescription, _newTribute.Date1, _newTribute.Date2 };
                    DataSet dsTrbId = GetDataSet("usp_UpdateNewTributeUrlTributeTypeinAlltables", param);

                    if (dsTrbId.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsTrbId.Tables[0].Rows[0]["Result"].ToString()))
                            result = bool.Parse(dsTrbId.Tables[0].Rows[0]["Result"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }



        /// <summary>
        /// To Check whether a tribute conatins video tribute: added by UAttri in Phase 1
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool IsTributeContainsVideoTribute(int tributeId)
        {
            bool containsVideo = false;
            if (!Equals(tributeId, 0))
            {
                object[] param = { tributeId };
                DataSet dsTributes = GetDataSet("usp_IsVideoTribute", param);

                if (dsTributes.Tables.Count > 0)
                {
                    if (dsTributes.Tables[0].Rows.Count > 0)
                    {
                        containsVideo =Convert.ToBoolean(dsTributes.Tables[0].Rows[0][0]);
                    }
                }
            }
            return containsVideo;
        }


        public IList<int> GetMyTributesPackages(int UserId)
        {
            IList<int> objList = new List<int>();
            if (UserId > 0)
            {
                try
                {
                    object[] param = { UserId };
                    DataSet dsThemes = GetDataSet("usp_GetMyTributesPackages", param);

                    if (dsThemes.Tables.Count > 0)
                    {
                        if (dsThemes.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsThemes.Tables[0].Rows)
                            {
                                int iPackageId = 0;
                                if (int.TryParse(dr["PackageId"].ToString(), out iPackageId))
                                {
                                    objList.Add(iPackageId);
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return objList;
        }

        public bool IsAllowedPhotoCheck(int _photoAlbumId)
        {
            bool isAllowedPhotoCheck = false;
            PhotoAlbum objPAlbum = new PhotoAlbum();
            
            if (_photoAlbumId > 0)
            {
                objPAlbum.PhotoAlbumId = _photoAlbumId;
                try
                {
                    object[] objparam = { objPAlbum.PhotoAlbumId
                                        };
                    DataSet dsIsAllowed = GetDataSet("usp_IsAllowedPhotoCheck", objparam);
                    if (dsIsAllowed.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsIsAllowed.Tables[0].Rows)
                        {
                            bool.TryParse(dr["IsAllowed"].ToString(), out isAllowedPhotoCheck);
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        Errors objError = new Errors();
                        objError.ErrorMessage = sqlEx.Message;
                        return isAllowedPhotoCheck;
                    }
                }
            }
            return isAllowedPhotoCheck;
        }

        public int GetPackIdonPhotoAlbumId(int photoAlbumId)
        {
            int packId = 0;
            PhotoAlbum objPAlbum = new PhotoAlbum();

            if (photoAlbumId > 0)
            {
                objPAlbum.PhotoAlbumId = photoAlbumId;
                try
                {
                    object[] objparam = { objPAlbum.PhotoAlbumId
                                        };
                    DataSet dsIsAllowed = GetDataSet("usp_GetPackIdonPhotoAlbumId", objparam);
                    if (dsIsAllowed.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsIsAllowed.Tables[0].Rows)
                        {
                            int.TryParse(dr["PackageId"].ToString(), out packId);
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        Errors objError = new Errors();
                        objError.ErrorMessage = sqlEx.Message;
                        return packId;
                    }
                }
            }
            return packId;
        }

        public bool IsAllowedPhotoCheckonPhotoId(int PhotoId)
        {
            bool isAllowedPhotoCheck = false;
            Photos objP = new Photos();

            if (PhotoId > 0)
            {
                objP.PhotoId = PhotoId;
                try
                {
                    object[] objparam = { objP.PhotoId
                                        };
                    DataSet dsIsAllowed = GetDataSet("usp_IsAllowedPhotoCheckonPhotoId", objparam);
                    if (dsIsAllowed.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsIsAllowed.Tables[0].Rows)
                        {
                            bool.TryParse(dr["IsAllowed"].ToString(), out isAllowedPhotoCheck);
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        Errors objError = new Errors();
                        objError.ErrorMessage = sqlEx.Message;
                        return isAllowedPhotoCheck;
                    }
                }
            }
            return isAllowedPhotoCheck;
        }
    }//end class
}//end namespace
