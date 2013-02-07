///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.UserInfoResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with getting info about Users
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using System.Data;
using System.Xml;
using System.IO;
using TributesPortal.Utilities;


namespace TributesPortal.ResourceAccess
{
    public class UserInfoResource : PortalResourceAccess, IResourceAccess
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


        public void GetTributeByID(object[] objParam)
        {
            TributesUserInfo _objTributeUserInfo = (TributesUserInfo)objParam[0];
            TributeAdministrator objTributeAdministrator = new TributeAdministrator();
            object[] _objParam = { _objTributeUserInfo.Tributes.TributeId };
            DataSet ds = GetDataSet("usp_GetTributeByID", _objParam);
            // ds.Tables[0].
            int count = ds.Tables[0].Rows.Count;
            if (count > 0)
            {
                _objTributeUserInfo.Tributes.UserTributeId = int.Parse(ds.Tables[0].Rows[0]["UserTributeId"].ToString());
                _objTributeUserInfo.Tributes.TributeName = ds.Tables[0].Rows[0]["TributeName"].ToString();
                _objTributeUserInfo.Tributes.TributeType = int.Parse(ds.Tables[0].Rows[0]["TributeType"].ToString());
                _objTributeUserInfo.Tributes.TributeUrl = ds.Tables[0].Rows[0]["TributeUrl"].ToString();
                _objTributeUserInfo.Tributes.TypeDescription = ds.Tables[0].Rows[0]["TypeDescription"].ToString();
                _objTributeUserInfo.Tributes.WelcomeMessage = ds.Tables[0].Rows[0]["WelcomeMessage"].ToString();
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Date1"].ToString()))
                    _objTributeUserInfo.Tributes.Date1 = null;
                else
                    _objTributeUserInfo.Tributes.Date1 = DateTime.Parse(ds.Tables[0].Rows[0]["Date1"].ToString());

                if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Date2"].ToString()))
                    _objTributeUserInfo.Tributes.Date2 = null;
                else
                    _objTributeUserInfo.Tributes.Date2 = DateTime.Parse(ds.Tables[0].Rows[0]["Date2"].ToString());

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CreatedDate"].ToString()))
                    _objTributeUserInfo.Tributes.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());


                _objTributeUserInfo.Tributes.PostMessage = ds.Tables[0].Rows[0]["PostMessage"].ToString();
                _objTributeUserInfo.Tributes.MessageWithoutHtml = ds.Tables[0].Rows[0]["MessageWithoutHtml"].ToString();

                _objTributeUserInfo.Tributes.City = ds.Tables[0].Rows[0]["City"].ToString();
                _objTributeUserInfo.Tributes.Country = int.Parse(ds.Tables[0].Rows[0]["Country"].ToString());

                if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["State"].ToString()))
                    _objTributeUserInfo.Tributes.State = null;
                else
                    _objTributeUserInfo.Tributes.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());

                _objTributeUserInfo.Tributes.TributeImage = ds.Tables[0].Rows[0]["TributeImage"].ToString();
                objTributeAdministrator.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                _objTributeUserInfo.Tributes.IsPrivate = bool.Parse(ds.Tables[0].Rows[0]["IsPrivate"].ToString());
                _objTributeUserInfo.Tributes.GoogleAdSense = bool.Parse(ds.Tables[0].Rows[0]["GoogleAdSense"].ToString());
                //added for Donation implementation
                //this gets the status of donation w.r.t a tribute
                _objTributeUserInfo.Tributes.IsDonation = bool.Parse(ds.Tables[0].Rows[0]["IsDonation"].ToString());

                _objTributeUserInfo.TributeAdministrator = objTributeAdministrator;

            }
        }


        public void GetTributeOnId(object[] objParam)
        {
            TributesUserInfo _objTributeUserInfo = (TributesUserInfo)objParam[0];
            object[] _objParam = { _objTributeUserInfo.Tributes.TributeId };
            DataSet ds = GetDataSet("usp_GetTributeOnId", _objParam);
            // ds.Tables[0].
            int count = ds.Tables[0].Rows.Count;
            if (count > 0)
            {
                _objTributeUserInfo.Tributes.UserTributeId = int.Parse(ds.Tables[0].Rows[0]["UserTributeId"].ToString());
                _objTributeUserInfo.Tributes.TributeName = ds.Tables[0].Rows[0]["TributeName"].ToString();
                _objTributeUserInfo.Tributes.TributeType = int.Parse(ds.Tables[0].Rows[0]["TributeType"].ToString());
                _objTributeUserInfo.Requester.UserID = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                _objTributeUserInfo.Requester.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                _objTributeUserInfo.Requester.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                _objTributeUserInfo.Requester.UserEmail = ds.Tables[0].Rows[0]["Email"].ToString();
                _objTributeUserInfo.Requester.UserType = ds.Tables[0].Rows[0]["TypeCode"].ToString();
                _objTributeUserInfo.Requester.UserTypeDescription = ds.Tables[0].Rows[0]["TypeDescription"].ToString();

            }
        }

        public int ConformAdmin(object[] objPValue)
        {
            Tributes objTributesUserInfo = (Tributes)objPValue[0];
            SessionValue objUserinfo = (SessionValue)objPValue[1];
            try
            {
                string[] strParam = { 
                                     "UserTributeId",
                                     "UserId",
                                     "Email",
                                     "IsOwner" 
                                };

                DbType[] enumDbType ={ 
                                    DbType.Int64,
                                    DbType.Int64,
                                    DbType.String,
                                    DbType.Int16,
                                 };

                object[] objValue ={
                                    objTributesUserInfo.TributeId,
                                    objUserinfo.UserId,
                                    objUserinfo.UserEmail,
                                    false
                               };

                base.InsertRecord("usp_SaveAdminConfirm", strParam, enumDbType, objValue);
                return 1;
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                object[] param = { 4 };
                Delete(param);
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    //_objGenralUserInfo.CustomError = objError;
                    return 0;
                }
            }
            return 0;

        }

        public int DeclineAdmin(object[] objPValue)
        {
            Tributes objTributesUserInfo = (Tributes)objPValue[0];
            SessionValue objUserinfo = (SessionValue)objPValue[1];
            try
            {
                string[] strParam = { 
                                     "UserTributeId",
                                     "UserId",
                                     "Email",
                                     "IsOwner",
                                     "IsActive",
                                     "IsDeleted"
                                };

                DbType[] enumDbType ={ 
                                    DbType.Int64,
                                    DbType.Int64,
                                    DbType.String,
                                    DbType.Int16,
                                    DbType.Boolean,
                                    DbType.Boolean
                                 };

                object[] objValue ={
                                    objTributesUserInfo.TributeId,
                                    objUserinfo.UserId,
                                    objUserinfo.UserEmail,
                                    false,
                                    false,
                                    true
                               };

                base.InsertRecord("usp_SaveAdminConfirm", strParam, enumDbType, objValue);
                return 1;
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                object[] param = { 4 };
                Delete(param);
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    //_objGenralUserInfo.CustomError = objError;
                    return 0;
                }
            }
            return 0;

        }

        public bool GetTributeAdmin(object[] objPValue)
        {
            Tributes objTributesUserInfo = (Tributes)objPValue[0];
            SessionValue objUserinfo = (SessionValue)objPValue[1];
            try
            {
                object[] objValue ={
                                    objTributesUserInfo.TributeId,
                                    objUserinfo.UserId,
                               };

                //base.InsertRecord("usp_GetTributeAdmin", strParam, enumDbType, objValue);
                DataSet objDataSet = GetDataSet("usp_GetTributeAdmin", objValue);
                if (objDataSet.Tables != null)
                {
                    if (objDataSet.Tables[0].Rows.Count != 0)
                        return false;
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                object[] param = { 4 };
                Delete(param);
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    //_objGenralUserInfo.CustomError = objError;
                    return false;
                }
            }
            return true;
        }

        public void UpdateEmailNotofication(object[] objValue)
        {
            UserRegistration objUserReg = (UserRegistration)objValue[0];
            try
            {
                string[] strParam = {
                                        EmailNotification.EmailNotify.UserId.ToString(),
                                        EmailNotification.EmailNotify.StoryNotify.ToString(),
                                        EmailNotification.EmailNotify.NotesNotify.ToString(),
                                        EmailNotification.EmailNotify.EventsNotify.ToString(),
                                        EmailNotification.EmailNotify.GuestBookNotify.ToString(),
                                        EmailNotification.EmailNotify.GiftsNotify.ToString(),
                                        EmailNotification.EmailNotify.PhotoAlbumNotify.ToString(),
                                        EmailNotification.EmailNotify.PhotosNotify.ToString(),
                                        EmailNotification.EmailNotify.VideosNotify.ToString(),
                                        EmailNotification.EmailNotify.CommentsNotify.ToString(),
                                        EmailNotification.EmailNotify.MessagesNotify.ToString(),
                                        EmailNotification.EmailNotify.NewsLetterNotify.ToString()
                                };

                DbType[] enumDbType ={ 
                                    DbType.Int32,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Boolean
                                                                                                      
                                 };

                object[] objValueS ={                                    
                                    objUserReg.EmailNotification.UserId,
                                    objUserReg.EmailNotification.StoryNotify, 
                                    objUserReg.EmailNotification.NotesNotify,
                                    objUserReg.EmailNotification.EventsNotify, 
                                    objUserReg.EmailNotification.GuestBookNotify,
                                    objUserReg.EmailNotification.GiftsNotify,
                                    objUserReg.EmailNotification.PhotoAlbumNotify,
                                    objUserReg.EmailNotification.PhotosNotify, 
                                    objUserReg.EmailNotification.VideosNotify,
                                    objUserReg.EmailNotification.CommentsNotify,
                                    objUserReg.EmailNotification.MessagesNotify,
                                    objUserReg.EmailNotification.NewsLetterNotify
                                                                      
                                   };
                base.UpdateRecord("usp_UpdateEmailNotification", strParam, enumDbType, objValueS);
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }
        }

        public void GetEmailNotofication(object[] objValue)
        {
            UserRegistration objUserReg = (UserRegistration)objValue[0];
            try
            {
                object[] objParam = { objUserReg.Users.UserId };
                DataSet _objDataSet = GetDataSet("usp_GetEmailNotification", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {


                    EmailNotification objEmail = new EmailNotification(
                         int.Parse(_objDataSet.Tables[0].Rows[0][EmailNotification.EmailNotify.EmailNotifyId.ToString()].ToString()),
                         int.Parse(_objDataSet.Tables[0].Rows[0][EmailNotification.EmailNotify.UserId.ToString()].ToString()),
                        bool.Parse(_objDataSet.Tables[0].Rows[0][EmailNotification.EmailNotify.StoryNotify.ToString()].ToString()),
                         bool.Parse(_objDataSet.Tables[0].Rows[0][EmailNotification.EmailNotify.NotesNotify.ToString()].ToString()),
                         bool.Parse(_objDataSet.Tables[0].Rows[0][EmailNotification.EmailNotify.EventsNotify.ToString()].ToString()),
                         bool.Parse(_objDataSet.Tables[0].Rows[0][EmailNotification.EmailNotify.GuestBookNotify.ToString()].ToString()),
                         bool.Parse(_objDataSet.Tables[0].Rows[0][EmailNotification.EmailNotify.GiftsNotify.ToString()].ToString()),
                         bool.Parse(_objDataSet.Tables[0].Rows[0][EmailNotification.EmailNotify.PhotoAlbumNotify.ToString()].ToString()),
                         bool.Parse(_objDataSet.Tables[0].Rows[0][EmailNotification.EmailNotify.PhotosNotify.ToString()].ToString()),
                         bool.Parse(_objDataSet.Tables[0].Rows[0][EmailNotification.EmailNotify.VideosNotify.ToString()].ToString()),
                         bool.Parse(_objDataSet.Tables[0].Rows[0][EmailNotification.EmailNotify.CommentsNotify.ToString()].ToString()),
                         bool.Parse(_objDataSet.Tables[0].Rows[0][EmailNotification.EmailNotify.MessagesNotify.ToString()].ToString()),
                         bool.Parse(_objDataSet.Tables[0].Rows[0][EmailNotification.EmailNotify.NewsLetterNotify.ToString()].ToString()));
                    objUserReg.EmailNotification = objEmail;
                }

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }
        }
        public void UserSummaryReport(UsersSummaryReport objSummary, string applicationType)
        {
            object[] param = { applicationType };
            DataSet dsData = GetDataSet("usp_GetUserSummaryReport", param);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                objSummary.PersonalAccountTodayNew = dsData.Tables[0].Rows[0]["PERSONAL_TODAY_NEW"].ToString();
                objSummary.PersonalAccountTodayExpired = dsData.Tables[0].Rows[0]["PERSONAL_TODAY_EXPIRED"].ToString();
                objSummary.PersonalAccount30DaysNew = dsData.Tables[0].Rows[0]["PERSONAL_30DAYS_NEW"].ToString();
                objSummary.PersonalAccount30DaysExpired = dsData.Tables[0].Rows[0]["PERSONAL_30DAYS_EXPIRED"].ToString();
                objSummary.PersonalTotalActiveAccount = dsData.Tables[0].Rows[0]["PERSONAL_ACTIVE_ACCOUNTS"].ToString();
                objSummary.BusinessAccountTodayNew = dsData.Tables[0].Rows[0]["BUSINESS_TODAY_NEW"].ToString();
                objSummary.BusinessAccountTodayExpired = dsData.Tables[0].Rows[0]["BUSINESS_TODAY_EXPIRED"].ToString();
                objSummary.BusinessAccount30DaysNew = dsData.Tables[0].Rows[0]["BUSINESS_30DAYS_NEW"].ToString();
                objSummary.BusinessAccount30DaysExpired = dsData.Tables[0].Rows[0]["BUSINESS_30DAYS_EXPIRED"].ToString();
                objSummary.BusinessTotalActiveAccount = dsData.Tables[0].Rows[0]["BUSINESS_ACTIVE_ACCOUNTS"].ToString();
                objSummary.TotalAccountTodayNew = dsData.Tables[0].Rows[0]["TOTAL_TODAY_NEW"].ToString();
                objSummary.TotalAccountTodayExpired = dsData.Tables[0].Rows[0]["TOTAL_TODAY_EXPIRED"].ToString();
                objSummary.TotalAccount30DaysNew = dsData.Tables[0].Rows[0]["TOTAL_30DAYS_NEW"].ToString();
                objSummary.TotalAccount30DaysExpired = dsData.Tables[0].Rows[0]["TOTAL_30DAYS_EXPIRED"].ToString();
                objSummary.TotalActiveAccounts = dsData.Tables[0].Rows[0]["TOTAL_ACTIVE_ACCOUNTS"].ToString();
            }

        }

        /// <summary>
        /// get data to update user profile details.
        /// </summary>
        /// <param name="objValue"></param>
        public void GetUserDetails(object[] objValue)
        {
            GetEmailNotofication(objValue);
            UserRegistration objUserReg = (UserRegistration)objValue[0];
            try
            {
                object[] objParam = { objUserReg.Users.UserId };
                DataSet _objDataSet = GetDataSet("usp_GetUserDetails", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    Users objUser = new Users();
                    objUser.EmailNotification = objUserReg.EmailNotification;
                    objUser.UserId = int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.UserId.ToString()].ToString());
                    objUser.UserName = _objDataSet.Tables[0].Rows[0][Users.UserEnum.UserName.ToString()].ToString();
                    objUser.FirstName = _objDataSet.Tables[0].Rows[0][Users.UserEnum.FirstName.ToString()].ToString();
                    objUser.LastName = _objDataSet.Tables[0].Rows[0][Users.UserEnum.LastName.ToString()].ToString();
                    objUser.Email = _objDataSet.Tables[0].Rows[0][Users.UserEnum.Email.ToString()].ToString();
                    objUser.UserImage = _objDataSet.Tables[0].Rows[0][Users.UserEnum.UserImage.ToString()].ToString();
                    objUser.Status = int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.Status.ToString()].ToString());
                    objUser.City = _objDataSet.Tables[0].Rows[0][Users.UserEnum.City.ToString()].ToString();

                    if (_objDataSet.Tables[0].Rows[0][Users.UserEnum.State.ToString()].ToString() != "")
                        objUser.State = int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.State.ToString()].ToString());
                    else
                        objUser.State = -1;

                    if (_objDataSet.Tables[0].Rows[0][Users.UserEnum.Country.ToString()].ToString() != "")
                        objUser.Country = int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.Country.ToString()].ToString());
                    else
                        objUser.Country = -1;

                    objUser.IsUsernameVisiable = bool.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.IsUsernameVisiable.ToString()].ToString());
                    objUser.AllowIncomingMsg = bool.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.AllowIncomingMsg.ToString()].ToString());
                    objUser.IsLocationHide = bool.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.IsLocationHide.ToString()].ToString());
                    objUser.UserType = int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.UserType.ToString()].ToString());
                    objUser.IsVisitCountHide=bool.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.IsVisitCountHide.ToString()].ToString());

                    if (int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.UserType.ToString()].ToString()) == 2)
                    {
                        UserBusiness objUserBus = new UserBusiness();
                        objUserBus.Website = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.Website.ToString()].ToString();
                        objUserBus.CompanyName = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.CompanyName.ToString()].ToString();
                        objUserBus.BusinessType = int.Parse(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.BusinessType.ToString()].ToString());
                        objUserBus.BusinessAddress = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.BusinessAddress.ToString()].ToString();
                        objUserBus.ZipCode = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.ZipCode.ToString()].ToString();
                        //Start - Modification on 17-Dec-09 for the enhancement 6 of the Phase 1
                        objUserBus.CompanyLogo = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.CompanyLogo.ToString()].ToString();
                        //End
                        if (_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.Phone.ToString()] != DBNull.Value)
                        {
                            objUserBus.Phone = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.Phone.ToString()].ToString();
                        }
                        objUserBus.HeaderBGColor = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.HeaderBGColor.ToString()].ToString();
                        objUserBus.HeaderLogo = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.HeaderLogo.ToString()].ToString();

                        if (!(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.ObituaryLinkPage.ToString()] == null || _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.ObituaryLinkPage.ToString()].ToString() == ""))
                            objUserBus.ObituaryLinkPage = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.ObituaryLinkPage.ToString()].ToString();
                        else
                            objUserBus.ObituaryLinkPage = "";

                        if (!(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsWebAddressOn.ToString()] == null || _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsWebAddressOn.ToString()].ToString() == ""))
                            objUserBus.IsWebAddressOn = bool.Parse(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsWebAddressOn.ToString()].ToString());
                        else
                            objUserBus.IsWebAddressOn = false;

                        if (!(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsAddressOn.ToString()] == null || _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsAddressOn.ToString()].ToString() == ""))
                            objUserBus.IsAddressOn = bool.Parse(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsAddressOn.ToString()].ToString());
                        else
                            objUserBus.IsAddressOn = false;

                        if (!(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsObUrlLinkOn.ToString()] == null || _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsObUrlLinkOn.ToString()].ToString() == ""))
                            objUserBus.IsObUrlLinkOn = bool.Parse(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsObUrlLinkOn.ToString()].ToString());
                        else
                            objUserBus.IsObUrlLinkOn = false;

                        if (!(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsPhoneNoOn.ToString()] == null || _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsPhoneNoOn.ToString()].ToString() == ""))
                            objUserBus.IsPhoneNoOn = bool.Parse(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsPhoneNoOn.ToString()].ToString());
                        else
                            objUserBus.IsPhoneNoOn = false;

                        if (!(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.DisplayCustomHeader.ToString()] == null || _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.DisplayCustomHeader.ToString()].ToString() == ""))
                            objUserBus.DisplayCustomHeader = bool.Parse(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.DisplayCustomHeader.ToString()].ToString());
                        else
                            objUserBus.DisplayCustomHeader = false;


                        objUserReg.UserBusiness = objUserBus;
                    }
                    objUserReg.Users = objUser;

                }


            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }
        }

        public void GetUserDetailsFromEmail(object[] objValue)
        {
            UserRegistration objUserReg = (UserRegistration)objValue[0];
            int tributeId = (int)objValue[1];
            try
            {
                object[] objParam = { objUserReg.Users.Email, objUserReg.Users.Password };
                DataSet _objDataSet = GetDataSet("usp_GetUserDetailsFromEmail", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    Users objUser = new Users();
                    objUser.EmailNotification = objUserReg.EmailNotification;
                    objUser.UserId = int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.UserId.ToString()].ToString());
                    objUser.UserName = _objDataSet.Tables[0].Rows[0][Users.UserEnum.UserName.ToString()].ToString();
                    objUser.FirstName = _objDataSet.Tables[0].Rows[0][Users.UserEnum.FirstName.ToString()].ToString();
                    objUser.LastName = _objDataSet.Tables[0].Rows[0][Users.UserEnum.LastName.ToString()].ToString();
                    objUser.Email = _objDataSet.Tables[0].Rows[0][Users.UserEnum.Email.ToString()].ToString();
                    objUser.UserImage = _objDataSet.Tables[0].Rows[0][Users.UserEnum.UserImage.ToString()].ToString();
                    objUser.Status = int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.Status.ToString()].ToString());
                    objUser.City = _objDataSet.Tables[0].Rows[0][Users.UserEnum.City.ToString()].ToString();

                    if (_objDataSet.Tables[0].Rows[0][Users.UserEnum.State.ToString()].ToString() != "")
                        objUser.State = int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.State.ToString()].ToString());
                    else
                        objUser.State = -1;

                    if (_objDataSet.Tables[0].Rows[0][Users.UserEnum.Country.ToString()].ToString() != "")
                        objUser.Country = int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.Country.ToString()].ToString());
                    else
                        objUser.Country = -1;

                    objUser.IsUsernameVisiable = bool.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.IsUsernameVisiable.ToString()].ToString());
                    objUser.AllowIncomingMsg = bool.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.AllowIncomingMsg.ToString()].ToString());
                    objUser.IsLocationHide = bool.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.IsLocationHide.ToString()].ToString());
                    objUser.UserType = int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.UserType.ToString()].ToString());

                    if (int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.UserType.ToString()].ToString()) == 2)
                    {
                        UserBusiness objUserBus = new UserBusiness();
                        objUserBus.Website = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.Website.ToString()].ToString();
                        objUserBus.CompanyName = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.CompanyName.ToString()].ToString();
                        objUserBus.BusinessType = int.Parse(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.BusinessType.ToString()].ToString());
                        objUserBus.BusinessAddress = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.BusinessAddress.ToString()].ToString();
                        objUserBus.ZipCode = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.ZipCode.ToString()].ToString();
                        //Start - Modification on 17-Dec-09 for the enhancement 6 of the Phase 1
                        objUserBus.CompanyLogo = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.CompanyLogo.ToString()].ToString();
                        //End
                        if (_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.Phone.ToString()] != DBNull.Value)
                        {
                            objUserBus.Phone = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.Phone.ToString()].ToString();
                        }
                        objUserBus.HeaderBGColor = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.HeaderBGColor.ToString()].ToString();
                        objUserBus.HeaderLogo = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.HeaderLogo.ToString()].ToString();

                        if (!(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.ObituaryLinkPage.ToString()] == null || _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.ObituaryLinkPage.ToString()].ToString() == ""))
                            objUserBus.ObituaryLinkPage = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.ObituaryLinkPage.ToString()].ToString();
                        else
                            objUserBus.ObituaryLinkPage = "";

                        if (!(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsWebAddressOn.ToString()] == null || _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsWebAddressOn.ToString()].ToString() == ""))
                            objUserBus.IsWebAddressOn = bool.Parse(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsWebAddressOn.ToString()].ToString());
                        else
                            objUserBus.IsWebAddressOn = false;

                        if (!(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsAddressOn.ToString()] == null || _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsAddressOn.ToString()].ToString() == ""))
                            objUserBus.IsAddressOn = bool.Parse(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsAddressOn.ToString()].ToString());
                        else
                            objUserBus.IsAddressOn = false;

                        if (!(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsObUrlLinkOn.ToString()] == null || _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsObUrlLinkOn.ToString()].ToString() == ""))
                            objUserBus.IsObUrlLinkOn = bool.Parse(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsObUrlLinkOn.ToString()].ToString());
                        else
                            objUserBus.IsObUrlLinkOn = false;

                        if (!(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsPhoneNoOn.ToString()] == null || _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsPhoneNoOn.ToString()].ToString() == ""))
                            objUserBus.IsPhoneNoOn = bool.Parse(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.IsPhoneNoOn.ToString()].ToString());
                        else
                            objUserBus.IsPhoneNoOn = false;

                        if (!(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.DisplayCustomHeader.ToString()] == null || _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.DisplayCustomHeader.ToString()].ToString() == ""))
                            objUserBus.DisplayCustomHeader = bool.Parse(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.DisplayCustomHeader.ToString()].ToString());
                        else
                            objUserBus.DisplayCustomHeader = false;


                        objUserReg.UserBusiness = objUserBus;
                    }
                    objUserReg.Users = objUser;

                    if (tributeId != 0)
                    {
                        Tributes objTribute = new Tributes();
                        objTribute.TributeId = tributeId;
                        SessionValue objSession = new SessionValue();
                        objSession.UserId = objUser.UserId;
                        objSession.UserEmail = objUser.Email;
                        object[] param = { objTribute, objSession };
                        ConformAdmin(param);
                    }
                }
                else
                {
                    objUserReg.Users = null;
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }
        }

        /// <summary>
        /// get data to update user profile details.
        /// </summary>
        /// <param name="objValue"></param>
        public void GetUserCompleteDetails(object[] objValue)
        {
            GetEmailNotofication(objValue);
            UserRegistration objUserReg = (UserRegistration)objValue[0];
            try
            {
                object[] objParam = { objUserReg.Users.UserId };
                DataSet _objDataSet = GetDataSet("usp_GetUserDetails", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    Users objUser = new Users();
                    objUser.EmailNotification = objUserReg.EmailNotification;
                    objUser.UserId = int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.UserId.ToString()].ToString());
                    objUser.UserName = _objDataSet.Tables[0].Rows[0][Users.UserEnum.UserName.ToString()].ToString();
                    objUser.Password = TributePortalSecurity.Security.DecryptSymmetric(_objDataSet.Tables[0].Rows[0][Users.UserEnum.Password.ToString()].ToString());
                    objUser.FirstName = _objDataSet.Tables[0].Rows[0][Users.UserEnum.FirstName.ToString()].ToString();
                    objUser.LastName = _objDataSet.Tables[0].Rows[0][Users.UserEnum.LastName.ToString()].ToString();
                    objUser.Email = _objDataSet.Tables[0].Rows[0][Users.UserEnum.Email.ToString()].ToString();
                    objUser.UserImage = _objDataSet.Tables[0].Rows[0][Users.UserEnum.UserImage.ToString()].ToString();
                    objUser.Status = int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.Status.ToString()].ToString());
                    objUser.City = _objDataSet.Tables[0].Rows[0][Users.UserEnum.City.ToString()].ToString();
                    if (_objDataSet.Tables[0].Rows[0][Users.UserEnum.State.ToString()].ToString() != "")
                        objUser.State = int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.State.ToString()].ToString());
                    else
                        objUser.State = -1;
                    objUser.Country = int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.Country.ToString()].ToString());
                    objUser.IsUsernameVisiable = bool.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.IsUsernameVisiable.ToString()].ToString());
                    objUser.AllowIncomingMsg = bool.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.AllowIncomingMsg.ToString()].ToString());
                    objUser.IsLocationHide = bool.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.IsLocationHide.ToString()].ToString());
                    objUser.ApplicationType = int.Parse(_objDataSet.Tables[0].Rows[0]["coApplicationId"].ToString()) == 1 ? "yourtribute" : "yourmoments";



                    if (int.Parse(_objDataSet.Tables[0].Rows[0][Users.UserEnum.UserType.ToString()].ToString()) == 2)
                    {
                        UserBusiness objUserBus = new UserBusiness();
                        objUserBus.Website = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.Website.ToString()].ToString();
                        objUserBus.CompanyName = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.CompanyName.ToString()].ToString();
                        objUserBus.BusinessType = int.Parse(_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.BusinessType.ToString()].ToString());
                        objUserBus.BusinessAddress = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.BusinessAddress.ToString()].ToString();
                        objUserBus.ZipCode = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.ZipCode.ToString()].ToString();
                        if (_objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.Phone.ToString()] != DBNull.Value)
                        {
                            objUserBus.Phone = _objDataSet.Tables[0].Rows[0][UserBusiness.UserRegistrationEnum.Phone.ToString()].ToString();
                        }


                        objUserReg.UserBusiness = objUserBus;
                    }
                    objUserReg.Users = objUser;

                }


            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }
        }

        public void AdminDisactivateAccount(string UserName)
        {
            string[] strParam = { "UserName" };

            DbType[] enumDbType = { DbType.String };

            object[] objValueS = { UserName };
            base.InsertRecord("usp_AdminDisactivateAccount", strParam, enumDbType, objValueS);
        }

        public void UpdatePrivacySettings(object[] objValue)
        {
            UserRegistration objUserReg = (UserRegistration)objValue[0];

            try
            {
                string[] strParam = {
                                        "UserId",
                                        "IsUsernameVisiable",
                                        "AllowIncomingMsg",
                                        "@IsLocationHide",
                                        "IsVisitCountHide"
                      
                                };

                DbType[] enumDbType ={ 
                                    DbType.Int32,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Boolean                                                                                                      
                                 };

                object[] objValueS ={
                                    objUserReg.Users.UserId,
                                    objUserReg.Users.IsUsernameVisiable,
                                    objUserReg.Users.AllowIncomingMsg,
                                    objUserReg.Users.IsLocationHide,
                                    objUserReg.Users.IsVisitCountHide
                                   };
                base.InsertRecord("usp_UpdatePrivacySettings", strParam, enumDbType, objValueS);
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }

        }

        public void UpdatePersonalDetails(object[] objValue)
        {
            UserRegistration objUserReg = (UserRegistration)objValue[0];

            try
            {
                string[] strParam = {
                                        "UserId",
                                        "FirstName",
                                        "LastName ",
                                        "UserImage",
                                        "Country",
                                        "State",
                                        "City" 
                                };

                DbType[] enumDbType ={ 
                                    DbType.Int32,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.Int32,
                                    DbType.Int32,
                                    DbType.String                                                                    
                                 };

                Nullable<int> state = null;
                if (objUserReg.Users.State != -1)
                {

                    state = objUserReg.Users.State;
                }
                object[] objValueS ={
                                    objUserReg.Users.UserId,
                                     objUserReg.Users.FirstName,
                                     objUserReg.Users.LastName,
                                     objUserReg.Users.UserImage,
                                     objUserReg.Users.Country,
                                     state,
                                    objUserReg.Users.City
                                   };
                base.InsertRecord("usp_UpdateUserDetails", strParam, enumDbType, objValueS);
                if (objUserReg.UserBusiness != null)
                {
                    UpdateBusinessDetails(objUserReg);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }

        }
        public void RemoveFacebookAssociation(UserRegistration objUserReg)
        {
            try
            {
                string[] strParam = { "UserId" };

                DbType[] enumDbType = { DbType.Int32 };

                object[] objValueS = { objUserReg.Users.UserId };
                base.InsertRecord("usp_RemoveUserFacebookAssociation", strParam, enumDbType, objValueS);
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }
        }

        public void UpdateFacebookAssociation(UserRegistration objUserReg)
        {
            try
            {
                string[] strParam = {
                                        "UserId",
                                        "FacebookUid" 
                };

                DbType[] enumDbType ={ 
                                    DbType.Int32,
                                    DbType.Int64                                                                    
                                 };

                object[] objValueS ={
                                    objUserReg.Users.UserId,
                                    objUserReg.Users.FacebookUid
                                   };
                base.InsertRecord("usp_UpdateUserFacebookAssociation", strParam, enumDbType, objValueS);
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 2601)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }
        }

        private void UpdateBusinessDetails(UserRegistration objUserReg)
        {
            try
            {
                string[] strParam = {
                                        "UserId",
                                        "Website",
                                        "CompanyName",
                                        "BusinessType",
                                        "BusinessAddress",
                                        "Phone",
                                        "ZipCode",
                                        "HeaderBGColor",
                                        "IsAddressOn",
                                        "IsWebAddressOn",
                                        "IsObUrlLinkOn",
                                        "IsPhoneNoOn",
                                        "DisplayCustomHeader",
                                        "HeaderLogo",
                                        "ObituaryLinkPage"
                                };

                DbType[] enumDbType ={ 
                                    DbType.Int32,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,  
                                    DbType.String,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.String,
                                    DbType.String
                                 };

                object[] objValue ={
                                    objUserReg.Users.UserId,
                                     objUserReg.UserBusiness.Website.ToString(),
                                     objUserReg.UserBusiness.CompanyName.ToString(),
                                     objUserReg.UserBusiness.BusinessType.ToString(),
                                     objUserReg.UserBusiness.BusinessAddress.ToString(),
                                     string.IsNullOrEmpty(objUserReg.UserBusiness.Phone) ? null :objUserReg.UserBusiness.Phone.ToString() ,
                                     objUserReg.UserBusiness.ZipCode.ToString(),
                                    objUserReg.UserBusiness.HeaderBGColor.ToString(),
                                    objUserReg.UserBusiness.IsAddressOn,
                                    objUserReg.UserBusiness.IsWebAddressOn,
                                    objUserReg.UserBusiness.IsObUrlLinkOn,
                                    objUserReg.UserBusiness.IsPhoneNoOn,
                                    objUserReg.UserBusiness.DisplayCustomHeader,
                                    objUserReg.UserBusiness.HeaderLogo,
                                    objUserReg.UserBusiness.ObituaryLinkPage
                                   };
                base.InsertRecord("usp_UpdateBusinessDetails", strParam, enumDbType, objValue);

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }

        }

        public void CheckFacebookAccountAvailability(GenralUserInfo objUser)
        {
            try
            {

                object[] objParam = { objUser.RecentUsers.FacebookUid, objUser.RecentUsers.ApplicationType };
                DataSet _objDataSet = GetDataSet("usp_GetFacebookUser", objParam);

                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    objUser.RecentUsers.UserID = (int)_objDataSet.Tables[0].Rows[0]["UserId"];
                    objUser.RecentUsers.UserName = _objDataSet.Tables[0].Rows[0]["UserName"].ToString();
                    objUser.RecentUsers.FirstName = _objDataSet.Tables[0].Rows[0]["FirstName"].ToString();
                    objUser.RecentUsers.LastName = _objDataSet.Tables[0].Rows[0]["LastName"].ToString();
                    objUser.RecentUsers.UserType = _objDataSet.Tables[0].Rows[0]["UserType"].ToString();
                    objUser.RecentUsers.IsUsernameVisiable = Convert.ToBoolean(_objDataSet.Tables[0].Rows[0]["IsUsernameVisiable"].ToString());
                    objUser.RecentUsers.UserTypeDescription = _objDataSet.Tables[0].Rows[0]["TypeDescription"].ToString();
                    objUser.RecentUsers.UserEmail = _objDataSet.Tables[0].Rows[0]["Email"].ToString();
                    objUser.RecentUsers.UserImage = _objDataSet.Tables[0].Rows[0]["UserImage"].ToString();
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

        public void SignInSiteAdmin(object[] objValue)
        {
            GenralUserInfo objUser = (GenralUserInfo)objValue[0];
            try
            {

                object[] objParam = { objUser.RecentUsers.UserName.ToString(), objUser.RecentUsers.UserPassword.ToString() };
                DataSet _objDataSet = GetDataSet("usp_SigninSiteAdimUser", objParam);


                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    objUser.RecentUsers.UserID = (int)_objDataSet.Tables[0].Rows[0]["UserId"];
                    objUser.RecentUsers.UserName = _objDataSet.Tables[0].Rows[0]["UserName"].ToString();
                    objUser.RecentUsers.FirstName = _objDataSet.Tables[0].Rows[0]["FirstName"].ToString();
                    objUser.RecentUsers.LastName = _objDataSet.Tables[0].Rows[0]["LastName"].ToString();
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


        public void CheckLogin(object[] objValue)
        {
            GenralUserInfo objUser = (GenralUserInfo)objValue[0];
            try
            {

                object[] objParam ={ objUser.RecentUsers.UserName.ToString(), 
                                     objUser.RecentUsers.UserPassword.ToString(),
                                     objUser.RecentUsers.FacebookUid,
                                     objUser.RecentUsers.ApplicationType.ToString()
                                   };
                //DataSet _objDataSet = GetDataSet("usp_ValidateUser", objParam);
                DataSet _objDataSet = GetDataSetWithoutCheckingIOVS("usp_ValidateWebsiteUser", objParam);
                // ds.Tables[0].
                int count = _objDataSet.Tables[0].Rows.Count;
                if (count > 0)
                {
                    objUser.RecentUsers.UserID = (int)_objDataSet.Tables[0].Rows[0]["UserId"];
                    objUser.RecentUsers.UserName = _objDataSet.Tables[0].Rows[0]["UserName"].ToString();
                    objUser.RecentUsers.FirstName = _objDataSet.Tables[0].Rows[0]["FirstName"].ToString();
                    objUser.RecentUsers.LastName = _objDataSet.Tables[0].Rows[0]["LastName"].ToString();
                    objUser.RecentUsers.UserType = _objDataSet.Tables[0].Rows[0]["UserType"].ToString();
                    objUser.RecentUsers.IsUsernameVisiable = Convert.ToBoolean(_objDataSet.Tables[0].Rows[0]["IsUsernameVisiable"].ToString());
                    objUser.RecentUsers.UserTypeDescription = _objDataSet.Tables[0].Rows[0]["TypeDescription"].ToString();
                    objUser.RecentUsers.UserEmail = _objDataSet.Tables[0].Rows[0]["Email"].ToString();
                    objUser.RecentUsers.UserImage = _objDataSet.Tables[0].Rows[0]["UserImage"].ToString();
                    if (string.IsNullOrEmpty(_objDataSet.Tables[0].Rows[0]["FacebookUid"].ToString()))
                    {
                        objUser.RecentUsers.FacebookUid = null;
                    }
                    else
                    {
                        objUser.RecentUsers.FacebookUid = (Nullable<Int64>)_objDataSet.Tables[0].Rows[0]["FacebookUid"];
                    }
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

        public int ChangeEmailPassword(object[] objValue)
        {
            GenralUserInfo _objGenralUserInfo = (GenralUserInfo)objValue[0];
            int _count = 0;
            try
            {
                _count = int.Parse(SaveEmailAndPassword(objValue).ToString());
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    _objGenralUserInfo.CustomError = objError;
                }
            }
            return _count;
        }

        public void GetUserData(object[] objValue)
        {
            GenralUserInfo objUser = (GenralUserInfo)objValue[0];
            try
            {

                object[] objParam = { objUser.RecentUsers.UserID, "", "", 1 };
                DataSet _objDataSet = GetDataSet("usp_GetSetUserEmailPassword", objParam);
                int count = _objDataSet.Tables[0].Rows.Count;
                if (count > 0)
                {
                    objUser.RecentUsers.UserID = (int)_objDataSet.Tables[0].Rows[0]["UserId"];
                    objUser.RecentUsers.UserEmail = _objDataSet.Tables[0].Rows[0]["Email"].ToString();
                    objUser.RecentUsers.UserPassword = _objDataSet.Tables[0].Rows[0]["Password"].ToString();
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


        public int EmailAvailable(string Email, string ApplicationType)
        {
            int count = 0;
            try
            {

                object[] objParam = { Email, ApplicationType };
                DataSet _objDataSet = GetDataSet("usp_AvailableEmail", objParam);
                count = int.Parse(_objDataSet.Tables[0].Rows[0][0].ToString());
                return count;
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                }
                return 1;
            }

        }

        public void UserAvailability(object[] objValue)
        {
            UserRegistration objUser = (UserRegistration)objValue[0];
            try
            {

                object[] objParam = { objUser.Users.UserName.ToString(), objUser.Users.ApplicationType.ToString() };
                DataSet _objDataSet = GetDataSet("usp_AvailableUser", objParam);
                int count = _objDataSet.Tables[0].Rows.Count;
                if (count > 0)
                {
                    objUser.Users.UserName = _objDataSet.Tables[0].Rows[0][0].ToString();

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
        public void CheckAndSendPassword(object[] objValue, bool _Reset)
        {
            GenralUserInfo objUser = (GenralUserInfo)objValue[0];
            try
            {
                if (_Reset == false)
                {
                    object[] objParam = { objUser.RecentUsers.UserEmail.ToString() };
                    DataSet _objDataSet = GetDataSet("usp_CheckEmail", objParam);
                    int count = _objDataSet.Tables[0].Rows.Count;
                    if (count > 0)
                    {
                        objUser.RecentUsers.UserID = (int)_objDataSet.Tables[0].Rows[0]["UserId"];
                        objUser.RecentUsers.UserPassword = _objDataSet.Tables[0].Rows[0]["Password"].ToString();
                        objUser.RecentUsers.UserName = _objDataSet.Tables[0].Rows[0]["UserName"].ToString();
                        objUser.RecentUsers.FirstName = _objDataSet.Tables[0].Rows[0]["FirstName"].ToString();
                        objUser.RecentUsers.LastName = _objDataSet.Tables[0].Rows[0]["LastName"].ToString();
                        objUser.RecentUsers.UserType = _objDataSet.Tables[0].Rows[0]["UserType"].ToString();
                        objUser.RecentUsers.IsUsernameVisiable = Convert.ToBoolean(_objDataSet.Tables[0].Rows[0]["IsUsernameVisiable"].ToString());
                        objUser.RecentUsers.UserTypeDescription = _objDataSet.Tables[0].Rows[0]["TypeDescription"].ToString();
                        objUser.RecentUsers.UserEmail = _objDataSet.Tables[0].Rows[0]["Email"].ToString();
                        objUser.RecentUsers.UserImage = _objDataSet.Tables[0].Rows[0]["UserImage"].ToString();
                    }
                }
                //else
                //{
                //    object[] objParam ={ objUser.RecentUsers.UserEmail.ToString() };
                //    DataSet ds = GetDataSet("usp_CheckEmail", objParam);
                //    int count = ds.Tables[0].Rows.Count;
                //    // to Update the Password 
                //}
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

        /*public string GetEventName(int _EventId)
        {
            string _EventName=string.Empty;
            try
            {
                //UserRole objUR = (UserRole)objValue[0];
                object[] objParam ={ _EventId };
                DataSet ds = GetDataSet("usp_GetEventOnId", objParam);
                // ds.Tables[0].
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {
                    //objUR.roles = ds.Tables[0].Rows[0][1].ToString();
                    _EventName=ds.Tables[0].Rows[0]["EventName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _EventName;
        }*/

        public int SaveEmail(object[] objParams)
        {
            MailMessage objEmail = (MailMessage)objParams[0];
            int _count = 0;
            try
            {
                _count = int.Parse(SaveEmailAndReturnIdentity(objParams).ToString());

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objEmail.CustomError = objError;
                }
            }
            return _count;
        }

        public int SaveEmailReply(object[] objParams)
        {
            MailMessage objEmail = (MailMessage)objParams[0];
            int _count = 0;
            try
            {
                _count = int.Parse(SaveEmailReplies(objParams).ToString());

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objEmail.CustomError = objError;
                }
            }
            return _count;
        }

        public int SaveEmailAndPassword(object[] Params)
        {
            GenralUserInfo _objGenralUserInfo = (GenralUserInfo)Params[0];
            try
            {

                string[] strParam = { 
                                    "UserId",
                                    "Email",
                                    "Password",
                                    "Get",
                                };

                DbType[] enumDbType ={ 
                                    DbType.Int64,
                                    DbType.String,
                                    DbType.String,
                                    DbType.Int16,
                                 };

                object[] objValue ={
                                     _objGenralUserInfo.RecentUsers.UserID,
                                    _objGenralUserInfo.RecentUsers.UserEmail!=""?_objGenralUserInfo.RecentUsers.UserEmail:null,
                                    _objGenralUserInfo.RecentUsers.UserPassword!=""?TributePortalSecurity.Security.EncryptSymmetric(_objGenralUserInfo.RecentUsers.UserPassword):null,
                                     0
                               };

                base.InsertRecord("usp_GetSetUserEmailPassword", strParam, enumDbType, objValue);
                return 1;
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                object[] param = { 4 };
                Delete(param);
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    _objGenralUserInfo.CustomError = objError;
                    return 0;
                }
            }
            return 0;
        }

        public object SaveEmailAndReturnIdentity(object[] Params)
        {
            object Identity = new object();
            MailMessage objMailMessage = (MailMessage)Params[0];
            try
            {
                string[] strParam = { 
                                    "SendByUserId",
                                    "Subject",
                                    "Body",
                                    "SendToUserId",
                                    "SendDate",
                                    "Status",
                                    "RecievedDate",
                                    "CreatedBy",
                                    "CreatedDate",
                                    "ModifiedBy",
                                    "ModifiedDate",
                                    "IsActive",
                                    "IsDeleted"
                                    
                                };

                DbType[] enumDbType ={ 
                                    DbType.Int64,
                                    DbType.String,
                                    DbType.String,
                                    DbType.Int64,
                                    DbType.DateTime,
                                    DbType.Int64,
                                    DbType.DateTime,
                                    DbType.Int64,
                                    DbType.DateTime,
                                    DbType.Int64,
                                    DbType.DateTime,
                                    DbType.Boolean,
                                    DbType.Boolean                                 
                                 };

                object[] objValue ={

                                     objMailMessage.SendByUserId,
                                    objMailMessage.Subject,
                                    objMailMessage.Body,
                                    objMailMessage.SendToUserId,
                                    objMailMessage.SendDate,
                                    objMailMessage.Status,
                                    objMailMessage.RecievedDate,
                                    objMailMessage.CreatedBy,
                                    objMailMessage.CreatedDate,
                                    objMailMessage.ModifiedBy,
                                    objMailMessage.ModifiedDate,
                                    objMailMessage.IsActive,
                                    objMailMessage.IsDeleted                                  
                               };
                Identity = base.InsertDataAndReturnId("usp_SaveMail", strParam, enumDbType, objValue);


            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                object[] param = { Identity };
                Delete(param);
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objMailMessage.CustomError = objError;
                    return Identity;
                }
            }
            return Identity;
        }

        public object SaveEmailReplies(object[] Params)
        {
            object Identity = new object();
            MailMessage objMailMessage = (MailMessage)Params[0];
            try
            {
                string[] strParam = { 
                                    "SendByUserId",
                                    "Subject",
                                    "Body",
                                    "SendToUserId",
                                    "SendDate",
                                    "Status",
                                    "RecievedDate",
                                    "CreatedBy",
                                    "CreatedDate",
                                    "ModifiedBy",
                                    "ModifiedDate",
                                    "IsActive",
                                    "IsDeleted",
                                    "MessageId"
                                    
                                };

                DbType[] enumDbType ={ 
                                    DbType.Int64,
                                    DbType.String,
                                    DbType.String,
                                    DbType.Int64,
                                    DbType.DateTime,
                                    DbType.Int64,
                                    DbType.DateTime,
                                    DbType.Int64,
                                    DbType.DateTime,
                                    DbType.Int64,
                                    DbType.DateTime,
                                    DbType.Boolean,
                                    DbType.Boolean,
                                    DbType.Int64             
                                 };

                object[] objValue ={

                                     objMailMessage.SendByUserId,
                                    objMailMessage.Subject,
                                    objMailMessage.Body,
                                    objMailMessage.SendToUserId,
                                    objMailMessage.SendDate,
                                    objMailMessage.Status,
                                    objMailMessage.RecievedDate,
                                    objMailMessage.CreatedBy,
                                    objMailMessage.CreatedDate,
                                    objMailMessage.ModifiedBy,
                                    objMailMessage.ModifiedDate,
                                    objMailMessage.IsActive,
                                    objMailMessage.IsDeleted,
                    objMailMessage.MessageId
                                  
                               };
                Identity = base.InsertDataAndReturnId("usp_SaveMailReply", strParam, enumDbType, objValue);


            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                object[] param = { Identity };
                Delete(param);
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objMailMessage.CustomError = objError;
                    return Identity;
                }
            }
            return Identity;
        }
        public void InsertRecord(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void UpdateRecord(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Delete(object[] Params)
        {
            //
            try
            {
                base.Delete("usp_DeleteUserAccount", Params);
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                }
            }

        }

        public object SaveUserAccount(object[] Params)
        {
            UserRegistration objUserReg = (UserRegistration)Params[0];
            object[] obj = { objUserReg };
            InsertDataAndReturnId(obj);
            if (objUserReg.UserBusiness != null && objUserReg.Users.UserId > 0)
            {
                try
                {
                    string[] strParam = { 
                                    UserBusiness.UserRegistrationEnum.UserId.ToString(),
                                    UserBusiness.UserRegistrationEnum.Website.ToString(),
                                    UserBusiness.UserRegistrationEnum.CompanyName.ToString(),
                                    UserBusiness.UserRegistrationEnum.BusinessType.ToString(),
                                    UserBusiness.UserRegistrationEnum.BusinessAddress.ToString(),
                                    UserBusiness.UserRegistrationEnum.ZipCode.ToString(),
                                    "Phone",
                                    ////LHK
                                    //UserBusiness.UserRegistrationEnum.HeaderBGColor.ToString(),
                                    //bool.Parse(UserBusiness.UserRegistrationEnum.IsAddressOn.ToString()),
                                    //bool.Parse(UserBusiness.UserRegistrationEnum.IsWebAddressOn.ToString()),
                                    //bool.Parse(UserBusiness.UserRegistrationEnum.IsObUrlLinkOn.ToString()),
                                    //bool.Parse(UserBusiness.UserRegistrationEnum.IsPhoneNoOn.ToString()),
                                    //bool.Parse(UserBusiness.UserRegistrationEnum.DisplayCustomHeader.ToString())
                                                    };

                    DbType[] enumDbType ={ 
                                    DbType.Int64,
                                    DbType.String,
                                    DbType.String,
                                    DbType.Int64,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    ////LHK
                                    //DbType.Boolean,
                                    //DbType.Boolean,
                                    //DbType.Boolean,
                                    //DbType.Boolean,
                                    //DbType.Boolean
                                 };

                    object[] objValue ={
                                    (Int64)objUserReg.Users.UserId,
                                    objUserReg.UserBusiness.Website.ToString(),
                                    objUserReg.UserBusiness.CompanyName.ToString(),
                                    objUserReg.UserBusiness.BusinessType.ToString(),
                                    objUserReg.UserBusiness.BusinessAddress.ToString(),
                                    objUserReg.UserBusiness.ZipCode.ToString(),                                   
                                    objUserReg.UserBusiness.Phone.ToString()
                    //                //LHK
                    //objUserReg.UserBusiness.HeaderBGColor.ToString(),
                    //objUserReg.UserBusiness.IsAddressOn.ToString(),
                    //objUserReg.UserBusiness.IsWebAddressOn.ToString(),
                    //objUserReg.UserBusiness.IsObUrlLinkOn.ToString(),
                    //objUserReg.UserBusiness.IsPhoneNoOn.ToString(),
                    //objUserReg.UserBusiness.DisplayCustomHeader.ToString()
                               };
                    //base.InsertRecord("usp_SaveUserBusinessAccount", strParam, enumDbType, objValue);
                    base.InsertRecordMinusIovs("usp_SaveUserBusinessAccount", strParam, enumDbType, objValue);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        Errors objError = new Errors();

                        objUserReg.CustomError = objError;
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return objUserReg.Users.UserId;
        }

        public object InsertDataAndReturnId(object[] Params)
        {
            object Identity = new object();
            UserRegistration objUserReg = (UserRegistration)Params[0];
            try
            {
                string[] strParam = { 
                                    Users.UserEnum.UserName.ToString(),
                                    Users.UserEnum.Password.ToString(),
                                    Users.UserEnum.FirstName.ToString(),
                                    Users.UserEnum.LastName.ToString(),
                                    Users.UserEnum.Email.ToString(),
                                    Users.UserEnum.VerificationCode.ToString(),
                                    Users.UserEnum.AllowIncomingMsg.ToString(),
                                    Users.UserEnum.City.ToString(),
                                    Users.UserEnum.State.ToString(),
                                    Users.UserEnum.Country.ToString(),   
                                    Users.UserEnum.UserImage.ToString(),
                                    Users.UserEnum.UserType.ToString(),
                                    Users.UserEnum.FacebookUid.ToString(),
                                    Users.UserEnum.ApplicationType.ToString()
                                };

                DbType[] enumDbType ={ 
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.Boolean,
                                    DbType.String,
                                    DbType.Int32,
                                    DbType.Int32,
                                    DbType.String,
                                    DbType.Int32,
                                    DbType.Int64,
                                    DbType.String
                                 };
                if (objUserReg.Users.State == -1)
                {
                    objUserReg.Users.State = null;
                }

                object[] objValue ={
                                        objUserReg.Users.UserName.ToString(),
                                        objUserReg.Users.Password.ToString(),
                                        objUserReg.Users.FirstName.ToString(),
                                        objUserReg.Users.LastName.ToString(),
                                        objUserReg.Users.Email.ToString(),
                                        objUserReg.Users.VerificationCode.ToString(),
                                        objUserReg.Users.AllowIncomingMsg.ToString(),
                                        objUserReg.Users.City.ToString(),
                                        objUserReg.Users.State,
                                        objUserReg.Users.Country,
                                        objUserReg.Users.UserImage,
                                        objUserReg.Users.UserType,
                                        objUserReg.Users.FacebookUid ,
                                        objUserReg.Users.ApplicationType
                                   };
                //Identity = base.InsertDataAndReturnIdMinusIOVS("usp_SaveUserPersonalAccount", strParam, enumDbType, objValue);

                DataSet _objDataSet = GetDataSetWithoutCheckingIOVS("usp_SaveUserPersonalAccount", objValue);
                // ds.Tables[0].
                int count = _objDataSet.Tables[0].Rows.Count;
                if (count > 0)
                {
                    objUserReg.Users.UserId = (int)_objDataSet.Tables[0].Rows[0]["UserId"];
                    objUserReg.Users.UserName = _objDataSet.Tables[0].Rows[0]["UserName"].ToString();
                }

                return objUserReg.Users.UserId;
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    if (sqlEx.Message.Contains("Cannot insert duplicate key row in object 'dbo.USERS' with unique index 'IX_USER_FACEBOOK_UID'"))
                    {
                        objError.ErrorMessage = "Those Facebook credentials are already used by some other Your Tribute Account.";
                    }
                    else
                    {
                        objError.ErrorMessage = sqlEx.Message;
                    }
                    objUserReg.CustomError = objError;
                    return Identity;
                }
            }
            return Identity;

        }


        /// <summary>
        /// This function will update email alerts of tributes.
        /// </summary>
        /// <param name="Params"></param>
        public void UpdateEmailAlerts(object[] Params)
        {
            object Identity = new object();
            Tributes objTributes = (Tributes)Params[0];
            try
            {
                string[] strParam = { 
                    "UserId",
                    Tributes.TributeEnum.TributeId.ToString(),
                    "EmailAlert"};

                DbType[] enumDbType ={ 
                                    DbType.Int32,
                                    DbType.Int32,
                                    DbType.Boolean                                   
                                 };


                object[] objValue ={
                                        objTributes.UserTributeId,
                        objTributes.TributeId,
                    objTributes.IsActive// This parameter will update email alert.
                                       
                                   };
                base.UpdateRecord("usp_UpdateEmailAlert", strParam, enumDbType, objValue);
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objTributes.CustomError = objError;
                }
            }

        }

        public void DeleteMyFavourite(object[] Params)
        {
            Tributes objTributes = (Tributes)Params[0];
            try
            {
                string[] strParam = { 
                    "ModifiedBy",
                    "FavoriteTributeId",
                    "IsDeleted"};

                DbType[] enumDbType ={ 
                                    DbType.Int32,
                                    DbType.Int32,
                                    DbType.Boolean                                   
                                 };


                object[] objValue ={
                                        objTributes.UserTributeId,
                        objTributes.TributeId,
                    objTributes.IsDeleted// This parameter will Delete Tribute from My favourite Tributes
                                       
                                   };
                base.UpdateRecord("usp_DeleteMyFavourite", strParam, enumDbType, objValue);
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objTributes.CustomError = objError;
                }
            }

        }
        //

        #endregion

        /// <summary>
        /// Method to check if user is a tribute administrator
        /// Added By: Gaurav Puri
        /// </summary>
        /// <param name="objUserAdmin">UserAdminOwnerInfo containing UserId and TributeId</param>
        /// <returns>True/False for IsAdmin</returns>
        public bool IsUserAdmin(object[] objUserAdmin)
        {
            bool isAdmin = false;
            UserAdminOwnerInfo objUserInfo = (UserAdminOwnerInfo)objUserAdmin[0];
            if (!Equals(objUserAdmin, null))
            {
                try
                {
                    object[] objparam = { objUserInfo.UserId, 
                                        objUserInfo.TributeId };
                    DataSet dsIsAdmin = GetDataSet("usp_IsAdmin", objparam);
                    return bool.Parse(dsIsAdmin.Tables[0].Rows[0]["IsAdmin"].ToString());
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        Errors objError = new Errors();
                        objError.ErrorMessage = sqlEx.Message;
                        return isAdmin;
                    }
                }
            }
            return isAdmin;
        }

        /// <summary>
        /// Method to check if user is an owner of Type (Video, Photo etc.).
        /// Added By: Gaurav Puri
        /// </summary>
        /// <param name="objUserAdmin">UserAdminOwnerInfo containing UserId, TypeId and TypeName</param>
        /// <returns>True/False for IsOwner</returns>
        public bool IsUserOwner(object[] objUserAdmin)
        {
            bool isOwner = false;
            UserAdminOwnerInfo objUserInfo = (UserAdminOwnerInfo)objUserAdmin[0];
            if (!Equals(objUserAdmin, null))
            {
                try
                {
                    object[] objparam = { objUserInfo.UserId, 
                                        objUserInfo.TypeId,
                                        objUserInfo.TypeName};
                    DataSet dsIsAdmin = GetDataSet("usp_IsOwner", objparam);
                    if (dsIsAdmin.Tables[0].Rows.Count != 0)
                    {
                        return bool.Parse(dsIsAdmin.Tables[0].Rows[0]["IsOwner"].ToString());
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



        public List<GetMyTributes> GetMyFavourites(object[] objValue)
        {
            GetMyTributes objUserReg = (GetMyTributes)objValue[0];
            List<GetMyTributes> lstTributes = new List<GetMyTributes>();
            try
            {
                object[] objParam = { objUserReg.UserId, int.Parse(objValue[1].ToString()), int.Parse(objValue[2].ToString()), int.Parse(objValue[3].ToString()) };
                DataSet _objDataSet = GetDataSet("usp_GetMyFavourites", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                    {
                        GetMyTributes objMyTributes = new GetMyTributes();
                        objMyTributes.TributeId = int.Parse(dr["TributeId"].ToString());
                        objMyTributes.TributeName = dr["TributeName"].ToString();
                        objMyTributes.TypeDescription = dr["TypeDescription"].ToString();
                        objMyTributes.Enddate = dr["CreatedBy"].ToString();
                        objMyTributes.EmailAlert = bool.Parse(dr["EmailAlert"].ToString());
                        objMyTributes.UserId = int.Parse(dr["UserId"].ToString());
                        objMyTributes.TributeUrl = dr["TributeUrl"].ToString();
                        lstTributes.Add(objMyTributes);
                        objMyTributes = null;
                    }
                }
                return lstTributes;

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    GetMyTributes objMyTributes = new GetMyTributes();
                    objMyTributes.CustomError = objError;
                    lstTributes.Add(objMyTributes);
                    objMyTributes = null;
                }
                return lstTributes;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public List<GetMyTributes> GetMyTributes(object[] objValue)
        {
            GetMyTributes objUserReg = (GetMyTributes)objValue[0];
            List<GetMyTributes> lstTributes = new List<GetMyTributes>();
            try
            {
                object[] objParam = { objUserReg.UserId, int.Parse(objValue[1].ToString()), int.Parse(objValue[2].ToString()), int.Parse(objValue[3].ToString()) };
                DataSet _objDataSet = GetDataSet("usp_GetMyTributes", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                    {
                        int _packageId = 0;
                        int tributeId = 0;
                        GetMyTributes objMyTributes = new GetMyTributes();
                        objMyTributes.TributeId = int.Parse(dr["TributeId"].ToString());
                        tributeId = int.Parse(dr["TributeId"].ToString());
                        objMyTributes.TributeName = dr["TributeName"].ToString();
                        objMyTributes.TypeDescription = dr["TypeDescription"].ToString();
                        objMyTributes.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                        #region PakageId
                        if (dr["PackageId"] != null)
                        {
                            if (dr["Enddate"].ToString().Equals("Never"))
                            {
                                //LHK: 12/19/2011 Yt speedup Issue
                                int.TryParse(dr["PackageId"].ToString(), out _packageId);
                                switch (_packageId)
                                {
                                    case 1:
                                        if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                            objMyTributes.Enddate = "Video Tribute (Lifetime)";//  + " (" + dr["Enddate"].ToString() + ")"; Updated in yt phase 1
                                        else
                                            objMyTributes.Enddate = "Memorial Tribute (Lifetime)";// +" (" + dr["Enddate"].ToString() + ")";// updated in yt phase 1
                                        break;
                                    case 4:
                                        objMyTributes.Enddate = "Memorial Tribute (Lifetime)";// +" (" + dr["Enddate"].ToString() + ")";// updated in yt phase 1
                                        break;
                                    case 6:
                                        objMyTributes.Enddate = "Premium Obituary (Lifetime)";// +" (" + dr["Enddate"].ToString() + ")"; //"Photo Tribute" + " (" + dr["Enddate"].ToString() + ")"; updated in phase 1
                                        break;
                                    case 8:
                                        objMyTributes.Enddate = "Obituary (Lifetime)";// "Announce (Free)"; updated in yt phase 1
                                        break;
                                    case 11:
                                        if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                            objMyTributes.Enddate = "Video (Lifetime)";//
                                        break;
                                    default:
                                        objMyTributes.Enddate = "Celebrate (Lifetime)";// +" (" + dr["Enddate"].ToString() + ")";
                                        break;
                                }
                            }
                            else
                            {
                                string[] date = dr["Enddate"].ToString().Split('/');
                                DateTime date2 = new DateTime(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1]));
                                if (date2 < DateTime.Now)
                                {
                                    //LHK: 12/19/2011 Yt speedup Issue
                                    int.TryParse(dr["PackageId"].ToString(), out _packageId);
                                    switch (_packageId)
                                    {
                                        case 2:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video Tribute (Expired)";
                                            else
                                                objMyTributes.Enddate = "Memorial Tribute (Expired)";// "Tribute (Expired)"; updated in phase 1
                                            break;
                                        case 3:
                                            objMyTributes.Enddate = "Announce (Free)"; 
                                            break;
                                        case 5:
                                            objMyTributes.Enddate = "Memorial Tribute (Expired)"; //Tribute (Expired)"; updated in yt phase 1
                                            break;
                                        case 7:
                                            objMyTributes.Enddate = "Premium Obituary (Expired)"; //"Photo Tribute (Expired)"; updates in yt phase 1
                                            break;
                                        case 8:
                                            objMyTributes.Enddate = "Obituary (Lifetime)";// "Announce (Free)"; updated in yt phase 1
                                            break;
                                        case 9:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video Tribute (Expired)";
                                            break;
                                        case 10:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video Tribute (Expired)";
                                            break;
                                        case 12:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video (Expired)";
                                            break;
                                        case 13:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video (Expired)";
                                            break;
                                        case 14:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video (Expired)";
                                            break;
                                        default:
                                            objMyTributes.ExpiredOn = date2.ToString("MMMM dd, yyyy");
                                            break;
                                    }
                                }
                                else
                                {
                                    //LHK: 12/19/2011 Yt speedup Issue
                                    int.TryParse(dr["PackageId"].ToString(), out _packageId);
                                    switch (_packageId)
                                    {
                                        case 2:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video Tribute" + " (" + dr["Enddate"].ToString() + ")";
                                            else
                                                objMyTributes.Enddate = "Memorial Tribute" + " (" + dr["Enddate"].ToString() + ")"; // updated in yt phase 1
                                            break;
                                        case 3:
                                            objMyTributes.Enddate = "Tribute Free Trial" + " (" + dr["Enddate"].ToString() + ")"; 
                                            break;
                                        case 5:
                                            objMyTributes.Enddate = "Memorial Tribute" + " (" + dr["Enddate"].ToString() + ")";// updated in yt phase 1
                                            break;
                                        case 7:
                                            objMyTributes.Enddate = "Premium Obituary" + " (" + dr["Enddate"].ToString() + ")"; //"Photo Tribute" + " (" + dr["Enddate"].ToString() + ")"; updated in phase 1
                                            break;
                                        case 8:
                                            objMyTributes.Enddate = "Obituary (Lifetime)";// "Tribute Free Trial" + " (" + dr["Enddate"].ToString() + ")"; updated for phase 1
                                            break;
                                        case 9:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video Tribute" + " (" + dr["Enddate"].ToString() + ")";
                                            break;
                                        case 10:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video Tribute" + " (" + dr["Enddate"].ToString() + ")";
                                            break;
                                        case 12:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video " + " (" + dr["Enddate"].ToString() + ")";
                                            break;
                                        case 13:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video " + " (" + dr["Enddate"].ToString() + ")";
                                            break;
                                        case 14:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video " + " (" + dr["Enddate"].ToString() + ")";
                                            break;
                                        default:
                                            objMyTributes.ExpiredOn = date2.ToString("MMMM dd, yyyy");
                                            break;
                                    }
                                }
                            }
                        }
                        #endregion
                        objMyTributes.Visit = int.Parse(dr["Visit"].ToString());
                        objMyTributes.EmailAlert = bool.Parse(dr["EmailAlert"].ToString());
                        objMyTributes.Renewaldate = DateTime.Parse(dr["Renewaldate"].ToString());
                        objMyTributes.TributeUrl = dr["TributeUrl"].ToString();
                        //LHK: speed up issue
                        if (dr["TypeDescription"].ToString().ToLower() == "video")
                        {
                            if (WebConfig.ApplicationMode.Equals("local"))
                            {
                                objMyTributes.TributeHomeUrl = WebConfig.AppBaseDomain.ToString() + "video/videotribute.aspx?tributeId=" + tributeId.ToString();
                            }
                            else
                            {
                                //Use this line for server and comment the line written above
                                objMyTributes.TributeHomeUrl = "http://video." + WebConfig.TopLevelDomain + "/video/videotribute.aspx?tributeId=" + tributeId.ToString();
                            }
                        }
                        else
                        {
                            if (WebConfig.ApplicationMode.Equals("local"))
                            {
                                objMyTributes.TributeHomeUrl = WebConfig.AppBaseDomain.ToString() + dr["TributeUrl"].ToString() + "/?" + "TributeType=" + dr["TypeDescription"].ToString();
                            }
                            else
                            {
                                //Use this line for server and comment the line written above
                                objMyTributes.TributeHomeUrl = "http://" + dr["TypeDescription"].ToString().Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + dr["TributeUrl"].ToString() + "/";
                            }
                        }

                        lstTributes.Add(objMyTributes);
                        objMyTributes = null;
                    }
                }
                return lstTributes;

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    GetMyTributes objMyTributes = new GetMyTributes();
                    objMyTributes.CustomError = objError;
                    lstTributes.Add(objMyTributes);
                    objMyTributes = null;
                }
                return lstTributes;
            }
        }

        /// <summary>
        /// Method to get my tributes for Rss Feed.
        /// Added By: Laxman Hari Kulshrestha
        /// </summary>
        /// <param name="objUserAdmin">GetMyTributes containing UserId</param>
        /// <returns>List<GetMyTributes></returns>
        public List<GetTributesFeed> GetTributesFeed(object[] objValue)
        {
            int _packageId;
            GetTributesFeed objUserReg = (GetTributesFeed)objValue[0];
            List<GetTributesFeed> lstTributes = new List<GetTributesFeed>();
            try
            {
                object[] objParam = { objUserReg.UserId, int.Parse(objValue[1].ToString()), int.Parse(objValue[2].ToString()), int.Parse(objValue[3].ToString()) };
                DataSet _objDataSet = GetDataSet("usp_GetTributesFeed", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                    {
                        GetTributesFeed objMyTributes = new GetTributesFeed();
                        objMyTributes.TributeId = int.Parse(dr["TributeId"].ToString());
                        objMyTributes.TributeName = dr["TributeName"].ToString();
                        objMyTributes.TributeImage = dr["TributeImage"].ToString();
                        if (dr["Date1"].ToString() != null)
                            objMyTributes.DOB = dr["Date1"].ToString();
                        objMyTributes.DOD = dr["Date2"].ToString();
                        objMyTributes.MessageWithoutHtml = dr["MessageWithoutHtml"].ToString();
                        if (!(string.IsNullOrEmpty(dr["ModifiedDate"].ToString())))
                            objMyTributes.ModifiedDate = DateTime.Parse(dr["ModifiedDate"].ToString());
                        objMyTributes.TypeDescription = dr["TypeDescription"].ToString();
                        objMyTributes.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                        if (dr["PackageId"] != null)
                        {
                            if (dr["Enddate"].ToString().Equals("Never"))
                            {
                                //LHK: 12/19/2011 Yt speedup Issue
                                int.TryParse(dr["PackageId"].ToString(), out _packageId);
                                switch (_packageId)
                                {
                                    case 1:
                                        if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                            objMyTributes.Enddate = "Video Tribute" + " (" + dr["Enddate"].ToString() + ")";
                                        else
                                            objMyTributes.Enddate = "Memorial Tribute" + " (" + dr["Enddate"].ToString() + ")";// updated in yt phase 1
                                        break;
                                    case 4:
                                        objMyTributes.Enddate = "Memorial Tribute" + " (" + dr["Enddate"].ToString() + ")";// updated in yt phase 1
                                        break;
                                    case 6:
                                        objMyTributes.Enddate = "Premium Obituary" + " (" + dr["Enddate"].ToString() + ")"; //"Photo Tribute" + " (" + dr["Enddate"].ToString() + ")"; updated in phase 1
                                        break;
                                    case 11:
                                        if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                            objMyTributes.Enddate = "Video " + " (" + dr["Enddate"].ToString() + ")";
                                        break;
                                    default:
                                        objMyTributes.Enddate = "Celebrate" + " (" + dr["Enddate"].ToString() + ")";
                                        break;
                                }
                            }
                            else
                            {
                                string[] date = dr["Enddate"].ToString().Split('/');
                                DateTime date2 = new DateTime(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1]));
                                if (date2 < DateTime.Now)
                                {
                                    //LHK: 12/19/2011 Yt speedup Issue
                                    int.TryParse(dr["PackageId"].ToString(), out _packageId);
                                    switch (_packageId)
                                    {
                                        case 2:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video Tribute (Expired)";
                                            else
                                                objMyTributes.Enddate = "Memorial Tribute (Expired)"; //Tribute (Expired)"; updated in yt phase 1
                                            break;
                                        case 3:
                                            objMyTributes.Enddate = "Obituary (Lifetime)";// "Announce (Free)"; updated in yt phase 1
                                            break;
                                        case 5:
                                            objMyTributes.Enddate = "Memorial Tribute (Expired)"; //Tribute (Expired)"; updated in yt phase 1
                                            break;
                                        case 7:
                                            objMyTributes.Enddate = "Premium Obituary (Expired)"; //"Photo Tribute (Expired)"; updates in yt phase 1
                                            break;
                                        case 8:
                                            objMyTributes.Enddate = "Obituary (Lifetime)";// "Announce (Free)"; updated in yt phase 1
                                            break;
                                        case 9:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video Tribute (Expired)";
                                           break;
                                        case 10:
                                           if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                               objMyTributes.Enddate = "Video Tribute (Expired)";
                                           break;
                                        case 12:
                                           if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                               objMyTributes.Enddate = "Video (Expired)";
                                           break;
                                        case 13:
                                           if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                               objMyTributes.Enddate = "Video (Expired)";
                                           break;
                                        case 14:
                                           if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                               objMyTributes.Enddate = "Video (Expired)";
                                           break;
                                        default:
                                            objMyTributes.ExpiredOn = date2.ToString("MMMM dd, yyyy");
                                            break;
                                    }

                                }
                                else
                                {
                                    //LHK: 12/19/2011 Yt speedup Issue
                                    int.TryParse(dr["PackageId"].ToString(), out _packageId);
                                    switch (_packageId)
                                    {
                                        case 2:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video Tribute" + " (" + dr["Enddate"].ToString() + ")";
                                            else
                                                objMyTributes.Enddate = "Memorial Tribute" + " (" + dr["Enddate"].ToString() + ")"; // updated in yt phase 1
                                            break;
                                        case 3:
                                            objMyTributes.Enddate = "Obituary (Lifetime)";// "Tribute Free Trial" + " (" + dr["Enddate"].ToString() + ")"; updated for phase 1
                                            break;
                                        case 5:
                                            objMyTributes.Enddate = "Memorial Tribute" + " (" + dr["Enddate"].ToString() + ")";// updated in yt phase 1
                                            break;
                                        case 7:
                                            objMyTributes.Enddate = "Premium Obituary" + " (" + dr["Enddate"].ToString() + ")"; //"Photo Tribute" + " (" + dr["Enddate"].ToString() + ")"; updated in phase 1
                                            break;
                                        case 8:
                                            objMyTributes.Enddate = "Obituary (Lifetime)";// "Tribute Free Trial" + " (" + dr["Enddate"].ToString() + ")"; updated for phase 1
                                            break;
                                        case 9:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video Tribute" + " (" + dr["Enddate"].ToString() + ")";
                                            break;
                                        case 10:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video Tribute" + " (" + dr["Enddate"].ToString() + ")";
                                            break;
                                        case 12:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video " + " (" + dr["Enddate"].ToString() + ")";
                                            break;
                                        case 13:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video " + " (" + dr["Enddate"].ToString() + ")";
                                            break;
                                        case 14:
                                            if (dr["TypeDescription"].ToString().ToLower().Equals("video"))
                                                objMyTributes.Enddate = "Video " + " (" + dr["Enddate"].ToString() + ")";
                                            break;
                                        default:
                                            objMyTributes.ExpiredOn = date2.ToString("MMMM dd, yyyy");
                                            break;
                                    }

                                }
                            }

                        }
                        objMyTributes.Visit = int.Parse(dr["Visit"].ToString());
                        objMyTributes.EmailAlert = bool.Parse(dr["EmailAlert"].ToString());
                        objMyTributes.Renewaldate = DateTime.Parse(dr["Renewaldate"].ToString());
                        objMyTributes.TributeUrl = dr["TributeUrl"].ToString();
                        lstTributes.Add(objMyTributes);
                        objMyTributes = null;
                    }
                }
                return lstTributes;

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    GetTributesFeed objMyTributes = new GetTributesFeed();
                    objMyTributes.CustomError = objError;
                    lstTributes.Add(objMyTributes);
                    objMyTributes = null;
                }
                return lstTributes;
            }
        }

        #region Temp MyRegion
        public Users GetUserDetailsOnUserId(int userId)
        {
            Users objUser = new Users();
            try
            {
                string returnVal = string.Empty;
                if (userId > 0)
                {
                    object[] objParam = { userId };
                    DataSet dsUser = GetDataSet("usp_GetUserDetailsOnUserId", objParam);

                    if (dsUser.Tables[0].Rows.Count > 0)
                    {
                        objUser.UserName = dsUser.Tables[0].Rows[0]["UserName"].ToString();
                        objUser.UserType = int.Parse(dsUser.Tables[0].Rows[0]["UserType"].ToString());
                        objUser.AtomEnabled = bool.Parse(dsUser.Tables[0].Rows[0]["AtomEnabled"].ToString());
                    }
                }
                return objUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetMyTributes> GetMyTribute(object[] objValue)
        {
            int _packageId;
            GetMyTributes objUserReg = (GetMyTributes)objValue[0];
            List<GetMyTributes> lstTributes = new List<GetMyTributes>();
            try
            {
                object[] objParam = { objUserReg.UserId, int.Parse(objValue[1].ToString()) };
                DataSet _objDataSet = GetDataSet("usp_GetMyTribute", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                    {
                        GetMyTributes objMyTributes = new GetMyTributes();
                        objMyTributes.TributeId = int.Parse(dr["TributeId"].ToString());
                        objMyTributes.TributeName = dr["TributeName"].ToString();
                        objMyTributes.TypeDescription = dr["TypeDescription"].ToString();
                        objMyTributes.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                        if (dr["Enddate"].ToString().Equals("Never"))
                        {
                            //  objMyTributes.Enddate = dr["Enddate"].ToString();  By Mohit Gupta
                            objMyTributes.Enddate = "Celebrate" + " (" + dr["Enddate"].ToString() + ")";
                        }
                        else
                        {
                            string[] date = dr["Enddate"].ToString().Split('/');
                            DateTime date2 = new DateTime(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1]));
                            if (date2 < DateTime.Now)
                            {
                                objMyTributes.Enddate = "Obituary (Lifetime)";// "Announce (Free)"; updated in yt phase 1
                                objMyTributes.ExpiredOn = date2.ToString("MMMM dd, yyyy");
                            }
                            else
                            {
                                objMyTributes.Enddate = "Celebrate Trial" + " (" + dr["Enddate"].ToString() + ")";
                                objMyTributes.ExpiredOn = date2.ToString("MMMM dd, yyyy");
                            }
                        }
                        objMyTributes.Visit = int.Parse(dr["Visit"].ToString());
                        objMyTributes.EmailAlert = bool.Parse(dr["EmailAlert"].ToString());
                        objMyTributes.Renewaldate = DateTime.Parse(dr["Renewaldate"].ToString());
                        objMyTributes.TributeUrl = dr["TributeUrl"].ToString();
                        lstTributes.Add(objMyTributes);
                        objMyTributes = null;
                    }
                }
                return lstTributes;
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    GetMyTributes objMyTributes = new GetMyTributes();
                    objMyTributes.CustomError = objError;
                    lstTributes.Add(objMyTributes);
                    objMyTributes = null;
                }
                return lstTributes;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public List<MailMessage> GetMailThread(object[] objValue)
        {

            List<MailMessage> lstTributes = new List<MailMessage>();
            try
            {
                object[] objParam = { int.Parse(objValue[0].ToString()) };
                DataSet _objDataSet = GetDataSet("usp_GetMailThread", objParam);
                int ParantMsgId = 0;
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                    {
                        MailMessage objMailMessage = new MailMessage();
                        //objMailMessage.MessageId = int.Parse(dr["MessageId"].ToString());
                        objMailMessage.SendByUser = dr["SendByUser"].ToString();
                        objMailMessage.Subject = dr["Subject"].ToString();
                        objMailMessage.Body = dr["Body"].ToString();
                        objMailMessage.UserImage = dr["UserImage"].ToString();
                        objMailMessage.SendDate = DateTime.Parse(dr["SendDate"].ToString());
                        objMailMessage.Status = int.Parse(dr["Status"].ToString());
                        objMailMessage.SendByUserId = int.Parse(dr["SendByUserId"].ToString());
                        //objMailMessage.ParantMsgId = int.Parse(dr["ParantMsgId"].ToString());
                        lstTributes.Add(objMailMessage);
                        objMailMessage = null;
                        ParantMsgId = int.Parse(dr["ParantMsgId"].ToString());

                    }
                }
                return lstTributes;

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    MailMessage objMailMessage = new MailMessage();
                    objMailMessage.CustomError = objError;
                    lstTributes.Add(objMailMessage);
                    objMailMessage = null;
                }
                return lstTributes;
            }
        }

        /// <summary>
        /// Method to get User Inbox mail.
        /// Added By: Deepak nagar
        /// </summary>
        /// <param name="objUserAdmin">GetMailMessage containing UserId</param>
        /// <returns>List<GetMyTributes></returns>
        public List<MailMessage> GetMailMessage(object[] objValue)
        {

            List<MailMessage> lstTributes = new List<MailMessage>();
            try
            {
                object[] objParam = { int.Parse(objValue[0].ToString()), int.Parse(objValue[1].ToString()), int.Parse(objValue[2].ToString()) };
                DataSet _objDataSet = GetDataSet("usp_GetuserInbox", objParam);
                int ParantMsgId = 0;
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                    {
                        MailMessage objMailMessage = new MailMessage();
                        if (ParantMsgId != int.Parse(dr["ParantMsgId"].ToString()))
                        {
                            objMailMessage.MessageId = int.Parse(dr["MessageId"].ToString());
                            objMailMessage.SendByUser = dr["SendByUser"].ToString();
                            objMailMessage.Subject = dr["Subject"].ToString();
                            objMailMessage.Body = dr["Body"].ToString();
                            objMailMessage.UserImage = dr["UserImage"].ToString();
                            objMailMessage.SendDate = DateTime.Parse(dr["SendDate"].ToString());
                            objMailMessage.Status = int.Parse(dr["Status"].ToString());
                            objMailMessage.SendByUserId = int.Parse(dr["SendByUserId"].ToString());
                            objMailMessage.ParantMsgId = int.Parse(dr["ParantMsgId"].ToString());
                            lstTributes.Add(objMailMessage);
                            objMailMessage = null;
                            ParantMsgId = int.Parse(dr["ParantMsgId"].ToString());
                        }

                    }
                }
                return lstTributes;

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    MailMessage objMailMessage = new MailMessage();
                    objMailMessage.CustomError = objError;
                    lstTributes.Add(objMailMessage);
                    objMailMessage = null;
                }
                return lstTributes;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public List<MailMessage> GetuserSentMessages(object[] objValue)
        {

            List<MailMessage> lstTributes = new List<MailMessage>();
            try
            {
                int ParantMsgId = 0;
                object[] objParam = { int.Parse(objValue[0].ToString()), int.Parse(objValue[1].ToString()), int.Parse(objValue[2].ToString()) };
                DataSet _objDataSet = GetDataSet("usp_GetuserSentMessages", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                    {
                        MailMessage objMailMessage = new MailMessage();
                        if (ParantMsgId != int.Parse(dr["ParantMsgId"].ToString()))
                        {
                            objMailMessage.MessageId = int.Parse(dr["MessageId"].ToString());
                            objMailMessage.SendByUser = dr["SendByUser"].ToString();
                            objMailMessage.Subject = dr["Subject"].ToString();
                            objMailMessage.Body = dr["Body"].ToString();
                            objMailMessage.UserImage = dr["UserImage"].ToString();
                            objMailMessage.SendDate = DateTime.Parse(dr["SendDate"].ToString());
                            objMailMessage.Status = int.Parse(dr["Status"].ToString());
                            objMailMessage.SendByUserId = int.Parse(dr["Userid"].ToString());
                            objMailMessage.ParantMsgId = int.Parse(dr["ParantMsgId"].ToString());
                            lstTributes.Add(objMailMessage);
                            objMailMessage = null;
                            ParantMsgId = int.Parse(dr["ParantMsgId"].ToString());
                        }
                    }
                }
                return lstTributes;

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    MailMessage objMailMessage = new MailMessage();
                    objMailMessage.CustomError = objError;
                    lstTributes.Add(objMailMessage);
                    objMailMessage = null;
                }
                return lstTributes;
            }
        }


        public void UpdateMessageStstus(object[] Params)
        {
            MailMessage objMailMessage = (MailMessage)Params[0];
            try
            {
                string[] strParam = { 
                    "Status",
                    "ModifiedBy",
                    "MessageId"};

                DbType[] enumDbType ={ 
                                    DbType.Int32,
                                    DbType.Int32,
                                    DbType.Int32                                   
                                 };

                string _selectedValue = (string)Params[1];
                string[] selectedValue = _selectedValue.Split(',');
                for (int i = 0; i < selectedValue.Length; i++)
                {

                    object[] objValue ={
                                        objMailMessage.Status,
                        objMailMessage.ModifiedBy,
                    int.Parse(selectedValue[i].ToString())                                       
                                   };

                    base.UpdateRecord("usp_UpdateMessageStatus", strParam, enumDbType, objValue);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objMailMessage.CustomError = objError;
                }
            }

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Params"></param>
        public void DeleteSentMessages(object[] Params)
        {
            MailMessage objMailMessage = (MailMessage)Params[0];
            try
            {
                string[] strParam = { 
                    "IsDeleted",
                    "ModifiedBy",
                    "MessageId"};

                DbType[] enumDbType ={ 
                                    DbType.Boolean,
                                    DbType.Int32,
                                    DbType.Int32                                   
                                 };

                string _selectedValue = (string)Params[1];
                string[] selectedValue = _selectedValue.Split(',');
                for (int i = 0; i < selectedValue.Length; i++)
                {

                    object[] objValue ={
                                        objMailMessage.IsDeleted,
                        objMailMessage.ModifiedBy,
                    int.Parse(selectedValue[i].ToString())                                       
                                   };

                    base.UpdateRecord("usp_DeleteSentMessages", strParam, enumDbType, objValue);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objMailMessage.CustomError = objError;
                }
            }

        }

        /// <summary>
        /// Delete Inbox Messages.
        /// </summary>
        /// <param name="Params"></param>
        public void DeleteMessages(object[] Params)
        {
            MailMessage objMailMessage = (MailMessage)Params[0];
            try
            {
                string[] strParam = { 
                    "IsDeleted",
                    "ModifiedBy",
                    "MessageId"};

                DbType[] enumDbType ={ 
                                    DbType.Boolean,
                                    DbType.Int32,
                                    DbType.Int32                                   
                                 };

                string _selectedValue = (string)Params[1];
                string[] selectedValue = _selectedValue.Split(',');
                for (int i = 0; i < selectedValue.Length; i++)
                {

                    object[] objValue ={
                                        objMailMessage.IsDeleted,
                        objMailMessage.ModifiedBy,
                    int.Parse(selectedValue[i].ToString())                                       
                                   };

                    base.UpdateRecord("usp_DeleteMessages", strParam, enumDbType, objValue);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objMailMessage.CustomError = objError;
                }
            }

        }


        public void DeleteTributeAdminis(object[] Params)
        {

            TributeAdministrator objMailMessage = (TributeAdministrator)Params[0];
            try
            {
                string[] strParam = { 
                    "UserId",
                    "UserTributeId"
                    };

                DbType[] enumDbType ={ 
                                   
                                    DbType.Int32,
                                    DbType.Int32                                   
                                 };

                string _selectedValue = (string)Params[1];
                string[] selectedValue = _selectedValue.Split(',');
                for (int i = 0; i < selectedValue.Length; i++)
                {

                    object[] objValue ={                                       
                                int.Parse(selectedValue[i].ToString()),objMailMessage.UserTributeId    
                    };

                    base.UpdateRecord("usp_DeleteTributeAdministrarors", strParam, enumDbType, objValue);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                }
            }
        }


        public List<Events> GetMyEvents(object[] objValue)
        {
            Events objUserReg = (Events)objValue[0];
            List<Events> lstEvents = new List<Events>();

            object[] objParam = { objUserReg.UserId, int.Parse(objValue[1].ToString()), int.Parse(objValue[2].ToString()) };
            DataSet _objDataSet = GetDataSet("usp_GetUserevents", objParam);
            if (_objDataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                {
                    Events objMyEvents = new Events();
                    objMyEvents.EventID = int.Parse(dr["EventID"].ToString());
                    objMyEvents.EventName = dr["EventName"].ToString();
                    objMyEvents.EventDesc = dr["TypeDescription"].ToString();
                    objMyEvents.EventDate = DateTime.Parse(dr["EventDate"].ToString());
                    objMyEvents.EventRsvp = dr["RSVP"].ToString();
                    objMyEvents.TributeId = int.Parse(dr["TributeId"].ToString());
                    lstEvents.Add(objMyEvents);
                    objMyEvents = null;
                }
            }
            return lstEvents;
        }


        public void GetUserProfile(object[] objValue)
        {
            //GetEmailNotofication(objValue);
            UserProfile objUserReg = (UserProfile)objValue[0];
            try
            {
                object[] objParam = { objUserReg.UserId };
                DataSet _objDataSet = GetDataSet("usp_GetUserProfile", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {

                    objUserReg.FirstName = _objDataSet.Tables[0].Rows[0]["FirstName"].ToString();
                    objUserReg.UserName = _objDataSet.Tables[0].Rows[0]["UserName"].ToString();
                    objUserReg.CreatedOn = DateTime.Parse(_objDataSet.Tables[0].Rows[0]["CreatedOn"].ToString());
                    objUserReg.City = _objDataSet.Tables[0].Rows[0]["City"].ToString();
                    objUserReg.State = _objDataSet.Tables[0].Rows[0]["State"].ToString();
                    objUserReg.Country = _objDataSet.Tables[0].Rows[0]["Country"].ToString();
                    objUserReg.StreetAddress = _objDataSet.Tables[0].Rows[0]["StreetAddress"].ToString();
                    objUserReg.PhoneNumber = _objDataSet.Tables[0].Rows[0]["Phone"].ToString();
                    objUserReg.Website = _objDataSet.Tables[0].Rows[0]["Website"].ToString();
                    //objUserReg.HeaderBGColor = _objDataSet.Tables[0].Rows[0]["HeaderBGColor"].ToString();
                    objUserReg.IsUsernameVisiable = bool.Parse(_objDataSet.Tables[0].Rows[0]["IsUsernameVisiable"].ToString());
                    objUserReg.IsLocationHide = bool.Parse(_objDataSet.Tables[0].Rows[0]["IsLocationHide"].ToString());
                    objUserReg.UserImage = _objDataSet.Tables[0].Rows[0]["UserImage"].ToString();
                    objUserReg.AllowIncomingMsg = bool.Parse(_objDataSet.Tables[0].Rows[0]["AllowIncomingMsg"].ToString());
                    if (string.IsNullOrEmpty(_objDataSet.Tables[0].Rows[0]["FacebookUid"].ToString()))
                    {
                        objUserReg.FacebookUid = null;
                    }
                    else
                    {
                        objUserReg.FacebookUid = (Nullable<Int64>)_objDataSet.Tables[0].Rows[0]["FacebookUid"];
                    }
                }

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                }
            }
        }



        public void UpdateFavouriteEmailAlert(object[] Params)
        {
            object Identity = new object();
            Tributes objTributes = (Tributes)Params[0];
            try
            {
                string[] strParam = { 
                    "UserId",
                    Tributes.TributeEnum.TributeId.ToString(),
                    "EmailAlert"};

                DbType[] enumDbType ={ 
                                    DbType.Int32,
                                    DbType.Int32,
                                    DbType.Boolean                                   
                                 };


                object[] objValue ={
                                        objTributes.UserTributeId,
                        objTributes.TributeId,
                    objTributes.IsActive// This parameter will update email alert.
                                       
                                   };
                base.UpdateRecord("usp_UpdateFavouriteEmailAlert", strParam, enumDbType, objValue);
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objTributes.CustomError = objError;
                }
            }

        }

        /// <summary>
        /// Method to get User Inbox mail count.
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public int UserInboxCount(object[] objValue)
        {
            int _Inboxcount = 0;
            try
            {
                object[] objParam = { int.Parse(objValue[0].ToString()) };
                DataSet _objDataSet = GetDataSet("usp_GetuserInboxCount", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                    {
                        _Inboxcount = int.Parse(dr["Inboxcount"].ToString());
                    }
                }
                return _Inboxcount;

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                }
                return _Inboxcount;
            }
        }

        public Events GetEventName(int EventID)
        {
            Events objEvent = new Events();
            try
            {
                object[] objParam = { EventID };
                DataSet dsEvent = GetDataSet("usp_GetEventOnId", objParam);

                if (dsEvent.Tables.Count > 0)
                {
                    if (dsEvent.Tables[0].Rows.Count > 0)
                    {
                        DataRow drEvent = dsEvent.Tables[0].Rows[0];

                        objEvent.EventName = drEvent["EventName"].ToString();
                        objEvent.EventID = int.Parse(drEvent["EventId"].ToString());
                        objEvent.TributeId = int.Parse(drEvent["TributeId"].ToString());
                        objEvent.UserId = int.Parse(drEvent["UserId"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objEvent;
        }

        /// <summary>
        /// This Method will get the Image list for the passed Tribute Type
        /// Added By Parul Jain
        /// </summary>
        /// <returns>This method will return the Gifts object which contain the list of Image</returns>
        public List<GiftImage> GetImage()
        {
            List<GiftImage> lstGifts = null;

            try
            {
                DataSet dsGifts = new DataSet();

                dsGifts = GetDataSet("usp_GetUserImage", null);
                lstGifts = PopulateGiftImageObject(dsGifts);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstGifts;
        }

        /// <summary>
        /// This method will populate the Gift Image object from the dataset
        /// Added By Parul Jain
        /// </summary>
        /// <param name="dsGiftImage">A Dataset object which contain Gift Image data</param>
        /// <returns>This method will return the List of Gift Image object populated with the All Image</returns>
        private List<GiftImage> PopulateGiftImageObject(DataSet dsGiftImage)
        {
            try
            {
                List<GiftImage> lstGiftsImage = new List<GiftImage>();

                string virtalPath = GetPath();

                // Get the User role for the tribute
                if (dsGiftImage.Tables.Count > 0)
                {
                    if (dsGiftImage.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drGift in dsGiftImage.Tables[0].Rows)
                        {
                            GiftImage objGiftImage = new GiftImage();

                            objGiftImage.ImageId = int.Parse(drGift["ImageId"].ToString());
                            objGiftImage.ImageUrl = virtalPath + drGift["ImageUrl"].ToString();

                            lstGiftsImage.Add(objGiftImage);
                            objGiftImage = null;
                        }
                    }
                }

                return lstGiftsImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This function is used to get the path for photos to get and save.
        /// </summary>
        /// <returns>Array of string containing drive name, root folder name and photo virtual directory name.</returns>
        public string GetPath()
        {
            string strXmlPath = AppDomain.CurrentDomain.BaseDirectory + "Common\\XML\\PhotoConfiguration.xml";

            FileStream docIn = new FileStream(strXmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            XmlDocument contactDoc = new XmlDocument();
            //Load the Xml Document
            contactDoc.Load(docIn);

            //Get a node
            XmlNodeList photoVirtualDirectory = contactDoc.GetElementsByTagName("PhotoVirtualDirectory");

            //get the value
            return photoVirtualDirectory.Item(0).InnerText;
        }

        /// <summary>
        ///  This method will call the Database method for saving the message
        /// Added By Parul Jain
        /// </summary>
        /// <param name="objTributeParam">A object which contain the welcome message which wants to save</param>
        public void SaveMessage(UserBusiness objBusinessUser, string ApplicationType)
        {
            object Identity = new object();

            try
            {
                if (objBusinessUser != null)
                {
                    // sets the name of the parameters
                    string[] strParam = { UserBusiness.UserRegistrationEnum.UserName.ToString(),  
                                          UserBusiness.UserRegistrationEnum.WelcomeMessage.ToString(),
                                          UserBusiness.UserRegistrationEnum.ApplicationType.ToString()
                                        };


                    // sets the types of parameters
                    DbType[] enumType = { DbType.String,
                                          DbType.String,
                                          DbType.String
                                        };


                    // sets the value of the paramter
                    object[] objValue = { objBusinessUser.UserName,
                                          objBusinessUser.WelcomeMessage,
                                          ApplicationType
                                        };

                    // call stored procedure to Insert teh event
                    Identity = InsertDataAndReturnId("usp_UpdateWelcomeMessage", strParam, enumType, objValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        ///  This method will call the Database method for saving the  company logo 
        /// Added By Parul Jain
        /// </summary>
        /// <param name="objTributeParam">A object which contain the company logo which wants to save</param>
        public void SaveImage(UserBusiness objBusinessUser)
        {
            object Identity = new object();

            try
            {
                if (objBusinessUser != null)
                {
                    // sets the name of the parameters
                    string[] strParam = { UserBusiness.UserRegistrationEnum.UserName.ToString(),  
                                          UserBusiness.UserRegistrationEnum.CompanyLogo.ToString(),
                                          UserBusiness.UserRegistrationEnum.ApplicationType.ToString()
                                        };


                    // sets the types of parameters
                    DbType[] enumType = { DbType.String,
                                          DbType.String,
                                          DbType.String
                                        };


                    // sets the value of the paramter
                    object[] objValue = { objBusinessUser.UserName,
                                          objBusinessUser.CompanyLogo,
                                          objBusinessUser.ApplicationType
                                        };

                    // call stored procedure to Insert teh event
                    Identity = InsertDataAndReturnId("usp_UpdateCompanyLogo", strParam, enumType, objValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This method will call the Database method for getting the tribute Listing for the
        /// Business user.
        /// Added By Parul Jain
        /// </summary>
        /// <param name="objTributeParam">This is the SearchTribute object which contain the Parameter 
        /// to get the tribute list - Sort Order, Tribuet Type and User ID</param>
        /// <returns>This method will return the List of Tribute</returns>
        public List<SearchTribute> GetBusinessUserTributeList(SearchTribute objTributeParam, string ApplicationType)
        {
            int coApplicationId = GetCoApplicationIdOnAppliactionType(ApplicationType);
            try
            {
                object[] objParam =     {   objTributeParam.TributeType, 
                                            objTributeParam.UserName,
                                            objTributeParam.ChangeSearchString,
                                            objTributeParam.SortOrder,
                                            objTributeParam.PageSize, 
                                            objTributeParam.PageNumber,
                                            coApplicationId
                                          };

                DataSet dsSearch = new DataSet();

                dsSearch = GetDataSet("usp_GetBusinessUserTribute", objParam);

                List<SearchTribute> objBasicSearchTributeList = GetTributeList(dsSearch, objTributeParam);

                return objBasicSearchTributeList;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private int GetCoApplicationIdOnAppliactionType(string ApplicationType)
        {
            int coApplicationId = 0;
            try
            {
                object[] objParam =     {   ApplicationType };

                DataSet dsSearch = new DataSet();

                dsSearch = GetDataSet("usp_GetCoApplicationIdOnAppliactionType", objParam);

                if (dsSearch.Tables.Count > 0)
                {
                    if (dsSearch.Tables[0].Rows.Count > 0)
                    {
                        //if (dsSearch.Tables["USERS"].ToString()
                        foreach (DataRow dr in dsSearch.Tables[0].Rows)
                        {
                            int.TryParse(dr["coApplicationId"].ToString(), out coApplicationId);
                        }
                    }
                }
                return coApplicationId;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will populate the Tribute List object from the dataset
        /// Added By Parul Jain
        /// </summary>
        /// <param name="dsSearch">A Dataset object which contain tribuet listing</param>
        /// <param name="objSearchParam">A SerachTribuet object which contain the search parameter</param>
        /// <returns>This method will return the List of Tribute</returns>
        private List<SearchTribute> GetTributeList(DataSet dsSearch, SearchTribute objSearchParam)
        {
            try
            {
                List<SearchTribute> objTributeList = new List<SearchTribute>();

                string virtualPath = GetVirtualPath();

                if (dsSearch.Tables.Count > 0)
                {
                    if (dsSearch.Tables[0].Rows.Count > 0)
                    {
                        //if (dsSearch.Tables["USERS"].ToString()
                        foreach (DataRow drBusineeUser in dsSearch.Tables[0].Rows)
                        {
                            objSearchParam.UserBusinessInfo = new UserBusiness();

                            objSearchParam.UserBusinessInfo.BusinessAddress = drBusineeUser["BusinessAddress"].ToString();
                            objSearchParam.UserBusinessInfo.City = drBusineeUser["City"].ToString();
                            objSearchParam.UserBusinessInfo.State = drBusineeUser["State"].ToString();
                            objSearchParam.UserBusinessInfo.Country = drBusineeUser["Country"].ToString();
                            objSearchParam.UserBusinessInfo.CompanyName = drBusineeUser["CompanyName"].ToString();
                            objSearchParam.UserBusinessInfo.Phone = drBusineeUser["Phone"].ToString();
                            objSearchParam.UserBusinessInfo.Website = drBusineeUser["Website"].ToString();
                            objSearchParam.UserBusinessInfo.ZipCode = drBusineeUser["ZipCode"].ToString();
                            objSearchParam.UserBusinessInfo.WelcomeMessage = drBusineeUser["WelcomeMessage"].ToString();
                            objSearchParam.UserBusinessInfo.CompanyLogo = virtualPath + drBusineeUser["CompanyLogo"].ToString();
                            objSearchParam.UserBusinessInfo.UserId = int.Parse(drBusineeUser["UserId"].ToString());
                            objSearchParam.UserBusinessInfo.Email = drBusineeUser["EmailId"].ToString();
                            objSearchParam.UserBusinessInfo.UserType = int.Parse(drBusineeUser["UserType"].ToString());
                        }
                    }
                }

                if (dsSearch.Tables.Count > 1)
                {
                    if (dsSearch.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow drSearch in dsSearch.Tables[1].Rows)
                        {
                            SearchTribute objSearchTribute = new SearchTribute();

                            objSearchTribute.TributeID = int.Parse(drSearch["TributeId"].ToString());
                            objSearchTribute.UserTributeID = int.Parse(drSearch["UserTributeId"].ToString());
                            objSearchTribute.TributeName = drSearch["TributeName"].ToString();
                            objSearchTribute.TributeUrl = drSearch["TributeUrl"].ToString();
                            objSearchTribute.TributeType = drSearch["TributeType"].ToString();
                            if (drSearch["TributeImage"].ToString().Split('/').Length <= 2)
                                objSearchTribute.TributeImage = virtualPath + "thumbnails/" + drSearch["TributeImage"].ToString();
                            else
                                objSearchTribute.TributeImage = virtualPath + drSearch["TributeImage"].ToString();

                            objSearchTribute.Location = GetLocation(drSearch);
                            objSearchTribute.DateForSorting = DateTime.Parse(drSearch["CreatedDate"].ToString());
                            objSearchTribute.CreatedDate = DateTime.Parse(drSearch["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");
                            objSearchTribute.Date1 = GetDate(drSearch);
                            objSearchTribute.TotalRecords = objSearchParam.TotalRecords = int.Parse(drSearch["TotalRecords"].ToString());
                            objSearchTribute.CreatedBy = GetUserName(drSearch);
                            objSearchTribute.Hits = drSearch["hits"].ToString();
                            objSearchTribute.VideoTributeId = drSearch["VideoTributeId"].ToString();

                            objTributeList.Add(objSearchTribute);
                            objSearchTribute = null;
                        }
                    }
                    else
                    {
                        objSearchParam.TotalRecords = 0;
                    }
                }

                return objTributeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will combine the city, state and country to get the location
        /// Added By Parul Jain
        /// </summary>
        /// <param name="drGift">A DataRow object which contain the City, State and Country</param>
        /// <returns>This method will return the string object which contain the location</returns>
        private string GetLocation(DataRow drSearch)
        {
            try
            {
                string location = "";

                if (drSearch["City"].ToString() != "")
                {
                    location += drSearch["City"].ToString() + ", ";
                }

                if (drSearch["State"].ToString() != "")
                {
                    location += drSearch["State"].ToString() + ", ";
                }

                location += drSearch["Country"].ToString();

                return location;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will get the userName on the basis of the attribute IsUserNameVisiable
        /// Added By Parul Jain
        /// </summary>
        /// <param name="drGift">A DataRow object which contain the City, State and Country</param>
        /// <returns>This method will return the string object which contain the userName</returns>
        private string GetUserName(DataRow drEvent)
        {
            string userName = "";

            try
            {
                //if user is registered User
                if ((drEvent["UserTributeId"] != null) && (drEvent["UserTributeId"].ToString() != ""))
                {
                    // if IsUserNameVisiable is True then get the userName
                    if (bool.Parse(drEvent["IsUserNameVisiable"].ToString()) == true)
                    {
                        // if user name exist means Registered user add the gift then get teh userName
                        if ((drEvent["UserName"] != null) && (drEvent["UserName"].ToString() != ""))
                        {
                            userName = drEvent["UserName"].ToString();
                        }
                    }
                    // if IsUserNameVisiable is false then get the Name of the user
                    else
                    {
                        // if User is Personal user then get the "FirstName + LastName"
                        if ((drEvent["UserType"] != null) && (int.Parse(drEvent["UserType"].ToString()) == 1))
                        {
                            userName = drEvent["FirstName"].ToString() + " " + drEvent["LastName"].ToString();
                        }
                        // if User is Business user then get the "CompanyName"
                        else if ((drEvent["UserType"] != null) && (int.Parse(drEvent["UserType"].ToString()) == 2))
                        {
                            userName = drEvent["CompanyName"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userName;
        }

        /// <summary>
        /// This method will get the different tribute date on the basis of the tribute type
        /// Added By Parul Jain
        /// </summary>
        /// <param name="drSearch">A Datarow object which contain the tribute detail</param>
        /// <returns>returns a string object which contain date</returns>
        private string GetDate(DataRow drSearch)
        {
            try
            {
                string dateVal = "";

                if (drSearch["Date1"].ToString() != "")
                {
                    DateTime BornDate = DateTime.Parse(drSearch["Date1"].ToString());

                    if ((drSearch["TributeType"].ToString() == "Birthday") && (BornDate.Year.ToString() == "1780"))
                    {
                        dateVal = BornDate.ToString("MMMM dd");
                    }
                    else
                    {
                        dateVal = BornDate.ToString("MMMM dd, yyyy");
                    }
                }

                if (drSearch["TributeType"].ToString() == "Memorial")
                {
                    if (drSearch["Date2"].ToString() != "")
                    {
                        if (dateVal != "")
                        {
                            dateVal += " - ";
                        }
                        dateVal += DateTime.Parse(drSearch["Date2"].ToString()).ToString("MMMM dd, yyyy");
                    }
                }
                else if (drSearch["TributeType"].ToString() == "New Baby")
                {
                    if (drSearch["Date2"].ToString() != "")
                    {
                        dateVal = DateTime.Parse(drSearch["Date2"].ToString()).ToString("MMMM dd, yyyy");
                    }
                }

                return dateVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This function is used to get the path for photos to get and save.
        /// </summary>
        /// <returns>Array of string containing virtual directory name.</returns>
        public string GetVirtualPath()
        {
            string strXmlPath = AppDomain.CurrentDomain.BaseDirectory + "Common\\XML\\PhotoConfiguration.xml";

            FileStream docIn = new FileStream(strXmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            XmlDocument contactDoc = new XmlDocument();
            //Load the Xml Document
            contactDoc.Load(docIn);

            //Get a node
            XmlNodeList photoVirtualDirectory = contactDoc.GetElementsByTagName("PhotoVirtualDirectory");

            //get the value
            return photoVirtualDirectory.Item(0).InnerText;
        }

        /// <summary>
        /// Method to check if business user exists of not based on the username.
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>If users exists return userid else returns empty string.</returns>
        public string CheckBusinessUser(string userName)
        {
            try
            {
                string returnVal = string.Empty;
                if (!Equals(userName, string.Empty))
                {
                    object[] objParam = { userName };
                    DataSet dsUser = GetDataSet("usp_GetBusinessUser", objParam);

                    if (dsUser.Tables[0].Rows.Count > 0)
                        returnVal = dsUser.Tables[0].Rows[0]["UserId"].ToString();
                }
                return returnVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        /// To get the creation date of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DateTime GetUserCreationDate(int userId)
        {
            DateTime creationDate;
            Users objuser = new Users();
            try
            {
                if (userId > 0)
                {
                    object[] param = { userId };
                    DataSet dsUser = GetDataSet("usp_GetUserCreationDate", param);
                    //check whether user wants to show/hide donation details on the tribute
                    if (dsUser.Tables.Count > 0)
                    {
                        if (dsUser.Tables[0].Rows.Count > 0)
                        {
                            DateTime.TryParse(dsUser.Tables[0].Rows[0][0].ToString(), out creationDate);
                            objuser.CreatedOn = creationDate;
                        }
                    }
                }

                return objuser.CreatedOn;
            }
            catch (Exception ex)
            {
                return objuser.CreatedOn;
            }

        }

        #region << Storing session values >>

        /// <summary>
        /// Method to Insert Session Values in Session Table.
        /// </summary>
        public object InsertSessionValues(object[] Params)
        {
            object Identity = new object();
            SessionValue objSessionValue = (SessionValue)Params[0];
            string strId = Params[1].ToString();
            try
            {
                string[] strParam = { 
                                    "SessionId",
                                    "IsActive",
                                    "CreatedDate",
                                    "LastUpdated",
                                };

                DbType[] enumDbType ={ 
                                    DbType.String,
                                    DbType.Boolean,
                                    DbType.DateTime,
                                    DbType.DateTime,
                                    };
                object[] objValue ={
                                        strId,
                                        1,
                                        System.DateTime.Now,
                                        System.DateTime.Now
                                   };
                Identity = base.InsertDataAndReturnId("usp_InsertSessionValues", strParam, enumDbType, objValue);
                return Identity;
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    //objUserReg.CustomError = objError;
                    return Identity;
                }
            }
            return Identity;

        }


        /// <summary>
        /// Method to Insert Session & Key Values in Sessionvalues Table
        /// </summary>
        public void InsertSessionKeyValues(int Ids, string SessionKey, string SessionValue)
        {
            object Identity = new object();
            try
            {
                string[] strParam = { 
                                    "Id",
                                    "SessionKey",
                                    "SessionValue"
                                };

                DbType[] enumDbType ={ 
                                    DbType.Int32,
                                    DbType.String,
                                    DbType.String
                                    };
                object[] objValue ={
                                        Ids,
                                        SessionKey,
                                        SessionValue
                                   };
                base.InsertDataAndReturnId("usp_InsertSessionKeyValues", strParam, enumDbType, objValue);
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    //objUserReg.CustomError = objError;
                }
            }
        }

        /// <summary>
        /// Method to get SessionDetail.
        /// </summary>
        public List<SessionValue> GetSessionDetail(string SessionValues)
        {
            try
            {
                DataSet ds = new DataSet();
                object[] objValue = { SessionValues };
                //ds = GetDataSet("usp_GetSessionValues", objValue);
                ds = GetDataSetWithoutCheckingIOVS("usp_GetSessionValues", objValue);
                int count = ds.Tables[0].Rows.Count;
                List<SessionValue> TempSession = new List<SessionValue>();
                for (int i = 0; i < count; i++)
                {
                    int x = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                    TempSession.Add(new SessionValue(x, ds.Tables[0].Rows[i]["SessionKey"].ToString(), ds.Tables[0].Rows[i]["SessionValues"].ToString()));

                }
                return TempSession;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to delete session.
        /// </summary>
        /// <param name="Params">User entity containing userid to be deleted.</param>
        public void DeleteSession(string SessionID)
        {
            object[] objParam = { SessionID };

            if (objParam != null)
            {
                base.Delete("usp_DeleteSessionValues", objParam);
            }
        }
        #endregion

        #endregion << Storing session Values >>

        public IList<GetTributesFeed> GetYourTributeFeedOnTributeName(object[] objparam)
        {
            List<GetTributesFeed> lstTributes = new List<GetTributesFeed>();
            try
            {
                DataSet _objDataSet = GetDataSet("usp_GetYourTributeFeedOnTributeName", objparam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                    {
                        GetTributesFeed objMyTributes = new GetTributesFeed();
                        objMyTributes.TributeId = int.Parse(dr["TributeId"].ToString());
                        objMyTributes.UserId = int.Parse(dr["UserTributeId"].ToString());
                        objMyTributes.TributeName = dr["TributeName"].ToString();
                        objMyTributes.TributeImage = dr["TributeImage"].ToString();
                        if (dr["Date1"].ToString() != null)
                            objMyTributes.DOB = dr["Date1"].ToString();
                        objMyTributes.DOD = dr["Date2"].ToString();
                        objMyTributes.MessageWithoutHtml = dr["MessageWithoutHtml"].ToString();
                        objMyTributes.TypeDescription = dr["TypeDescription"].ToString();
                        objMyTributes.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                        objMyTributes.TributeUrl = dr["TributeUrl"].ToString();
                        lstTributes.Add(objMyTributes);
                        objMyTributes = null;
                    }
                }
                return lstTributes;

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    GetTributesFeed objMyTributes = new GetTributesFeed();
                    objMyTributes.CustomError = objError;
                    lstTributes.Add(objMyTributes);
                    objMyTributes = null;
                }
                return lstTributes;
            }
        }

        public IList<GetTributesFeed> GetYourTributesFeed(object[] objparam)
        {
            List<GetTributesFeed> lstTributes = new List<GetTributesFeed>();
            try
            {
                DataSet _objDataSet = GetDataSet("usp_GetYourTributesFeed", objparam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                    {
                        GetTributesFeed objMyTributes = new GetTributesFeed();
                        objMyTributes.TributeId = int.Parse(dr["TributeId"].ToString());
                        objMyTributes.UserId = int.Parse(dr["UserTributeId"].ToString());
                        objMyTributes.TributeName = dr["TributeName"].ToString();
                        objMyTributes.TributeImage = dr["TributeImage"].ToString();
                        if (dr["Date1"].ToString() != null)
                            objMyTributes.DOB = dr["Date1"].ToString();
                        objMyTributes.DOD = dr["Date2"].ToString();
                        objMyTributes.MessageWithoutHtml = dr["MessageWithoutHtml"].ToString();
                        if (!(string.IsNullOrEmpty(dr["ModifiedDate"].ToString())))
                            objMyTributes.ModifiedDate = DateTime.Parse(dr["ModifiedDate"].ToString());
                        objMyTributes.TypeDescription = dr["TypeDescription"].ToString();
                        objMyTributes.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                        objMyTributes.TributeUrl = dr["TributeUrl"].ToString();
                        lstTributes.Add(objMyTributes);
                        objMyTributes = null;
                    }
                }
                return lstTributes;

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    GetTributesFeed objMyTributes = new GetTributesFeed();
                    objMyTributes.CustomError = objError;
                    lstTributes.Add(objMyTributes);
                    objMyTributes = null;
                }
                return lstTributes;
            }
        }

        public int GetTotalActiveObituaries(int _businessUserId)
        {
            int _TotalObitCount = 0;
            try
            {
               object[] objParam = { _businessUserId };
               DataSet _objDataSet = GetDataSet("usp_GetTotalActiveObituaries", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                    {
                        int.TryParse(dr["TotalObitCount"].ToString(),out _TotalObitCount);
                    }
                }
                return _TotalObitCount;

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                }
                return _TotalObitCount;
            }
        }


        public int GetTotalActiveObituariesOnTributeName(object[] objprm)
        {
            int _TotalObitCount = 0;
            try
            {
                if (objprm != null)
                {
                    DataSet _objDataSet = GetDataSet("usp_GetTotalActiveObituariesOnTributeName", objprm);
                    if (_objDataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                        {
                            int.TryParse(dr["TotalObitCount"].ToString(), out _TotalObitCount);
                        }
                    }
                }
                return _TotalObitCount;
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                }
                return _TotalObitCount;
            }
        }
    }
}
