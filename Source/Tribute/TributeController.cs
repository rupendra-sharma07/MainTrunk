//===============================================================================
// Microsoft patterns & practices
// Web Client Software Factory
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.TributeController.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Controller class for the files under Tributes.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;

namespace TributesPortal.Tribute
{
    public class TributeController
    {
        public TributeController()
        {
        }

        /// <summary>
        /// Function to get the list of featured tributes
        /// </summary>
        /// <param name="objFeaturedTribute"></param>
        /// <returns>list of featured tributes</returns>
        public List<FeaturedTribute> GetFeaturedTributes(FeaturedTribute objFeaturedTribute)
        {
            return FacadeManager.FeaturedTributeManager.GetFeaturedTributes(objFeaturedTribute);      
        }

        public IList<Tributes> TributeList(Tributes tributes)
        {
            return FacadeManager.TributeManager.GetTributeList(tributes);
        }
        public IList<Templates> TemplatesList(string strTheme)
        {
            return FacadeManager.TributeManager.GetTemplateList(strTheme);
        }
        public IList<Locations> CountryList(Locations countries)
        {
            return FacadeManager.TributeManager.GetCountryList(countries);
        }
        public IList<Locations> GetCountryState(object[] param)
        {
            return FacadeManager.TributeManager.GetCountryState(param);
        }
        
        public IList<Locations> StateList(Locations states)
        {
            return FacadeManager.TributeManager.GetStateList(states);
        }

        public void ValidateLogin(GenralUserInfo objGenralUserInfo)
        {
            //code here
            FacadeManager.UserInfoManager.UserLogin(objGenralUserInfo);

        }
        public object CreateTribute(Tributes tributes)
        {
            return FacadeManager.TributeManager.SaveTribute(tributes);
        }

        /// <summary>
        /// Create the Donation service for the Tribute
        /// </summary>
        /// <param name="objDonation"></param>
        /// <returns></returns>
        public object CreateDonation(Donation objDonation)
        {
            return FacadeManager.TributeManager.SaveDonation(objDonation);
        }
        // modified by Udham for Your Moments.
        public bool CheckEmailAvailability(string txtEmail,string ApplicationType)
        {
            return (FacadeManager.UserInfoManager.EmailAvailable(txtEmail, ApplicationType) == 1) ? false : true;
        }

        public string CheckUrlExists(string strExistsOrNot, int tributetype)
        {
            return FacadeManager.TributeManager.CheckUrlExists(strExistsOrNot, tributetype);
        }
        public IList<Templates> BindTheme(string strTheme)
        {
            return FacadeManager.TributeManager.GetTemplateList(strTheme);
        }
        public IList<Themes> GetThemes(string strTheme)
        {
            return FacadeManager.TributeManager.GetThemes(strTheme);
        }
        public IList<Themes> GetThemesForCategory(string strTheme, string categoryName, string applicationType)
        {
            return FacadeManager.TributeManager.GetThemesForCategory(strTheme, categoryName, applicationType);
        }

        //  deepak Nagar
        public List<ParameterTypesCodes> GetListofTributes(string applicationType)
        {
            return FacadeManager.TributeManager.GetListofTributes(applicationType);
        }
        public IList<ParameterTypesCodes> PaymentModes()
        {
            return FacadeManager.BillingManager.PaymentModes();
        }
         //
        public List<ParameterTypesCodes> GetTributeTypesbyTypeCode(int TypeCode)
        {
            return FacadeManager.TributeManager.GetTributeTypesbyTypeCode(TypeCode);
        }
        

        public object InsertCCDetails(object [] param)
        {
           return FacadeManager.BillingManager.InsertCCDetails(param);
        }

        public object InsertCreditPointCCDetails(object[] param)
        {
            return FacadeManager.BillingManager.InsertCreditPointCCDetails(param);
        }
        public object InsertCurrentCreditPoints(object[] param)
        {
            return FacadeManager.BillingManager.InsertCurrentCreditPoints(param);
        }

        public object InsertPackageDetails(object[] param)
        {
            return FacadeManager.BillingManager.InsertPackageDetails(param);
        }

        public void TriputePackageInfo(object[] param)
        {
            FacadeManager.BillingManager.TriputePackageInfo(param);
        }

        public IList<UserInfo> GetTributeAdminis(object[] param)
        {
            return FacadeManager.TributeManager.GetTributeAdminis(param);
        }

        public void GetTributeByID(TributesUserInfo _objTributeUserinfo)
        {
            FacadeManager.UserInfoManager.GetTributeByID(_objTributeUserinfo);
        }

        public void GetLastNoteForTribute(object[] objTributeId)
        {
            FacadeManager.NotesManager.GetLastNoteForTribute(objTributeId);
        }
        public void UpdateTributeMessage(object[] objTributeId)
        {
            FacadeManager.TributeManager.UpdateTributeMessage(objTributeId);
        }
        public void GetTributeCount(object[] webstat)
        {
            FacadeManager.TributeManager.GetTributeCount(webstat);            
        }
        /// <summary>
        /// LHK: get all latest date to traverse get latest activities
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public List<DateTime> GetAllLatestDates(int tributeId)
        {
            return FacadeManager.TributeManager.GetAllLatestDates(tributeId);
        }

        public IList<TributeLatest> GetLatestVideos(object[] webstat)
        {
            return  FacadeManager.TributeManager.GetLatestVideos(webstat);            
        }

        public bool IsStoryAdded(object[] objUserAdmin)
        {
            return FacadeManager.TributeManager.IsStoryAdded(objUserAdmin);              
        }



        public bool IsStoryHide(int TributeId)
        {
            return FacadeManager.TributeManager.IsStoryHide(TributeId);
            
        }
        public void SetStoryVisibility(int TributeId)
        {
            FacadeManager.TributeManager.SetStoryVisibility(TributeId);
        }

        public void SetAccountEmailPassword(GenralUserInfo _objGenralUserInfo)
        {
            FacadeManager.UserInfoManager.ChangeEmailPassword(_objGenralUserInfo);
        }

        public int GetTributeIdCode(string TributeName)
        {
            return FacadeManager.TributeManager.GetTributeIdCode(TributeName);              
        }
        public void SendMail(int SendByUserId, int SendToUserId, string Subject, string _EmailBody)
        {
            EmailManager objec = new EmailManager();
            objec.SendMail(SendByUserId, SendToUserId, Subject, _EmailBody);
        }
        public Stories GetStoryDetail(Stories objStory)
        {
            try
            {
                return FacadeManager.StoryManager.GetStoryDetail(objStory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetTributeOnId(TributesUserInfo _objTributeUserinfo)
        {
            FacadeManager.UserInfoManager.GetTributeOnId(_objTributeUserinfo);
        }
        public void GetTributeTypebyCode(object[] objParam)
        {
            FacadeManager.TributeManager.GetTributeTypebyCode(objParam);            

        }
        public void CheckAndSendPassword(GenralUserInfo _objGenralUserInfo, bool _Reset)
        {
            FacadeManager.UserInfoManager.CheckAndSendPassword(_objGenralUserInfo, _Reset);
        }


        /// <summary>
        /// This method will call the SearchTribute Manager class method for searching the tribute (basic search)
        /// Added By Parul Jain
        /// </summary>
        /// <param name="objBasicSearchParam">This is the SearchTribute object which contain the Search Parameter 
        /// to get the tribute list</param>
        /// <returns>This method will return the List of Tribute</returns>
        public List<SearchTribute> BasicSearch(SearchTribute objBasicSearchParam)
        {
            try
            {
                return FacadeManager.SearchTributeManager.BasicSearch(objBasicSearchParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the SearchTribute Manager class method for searching the tribute (Advance search)
        /// Added By Parul Jain
        /// </summary>
        /// <param name="objAdvanceSearchParam">This is the SearchTribute object which contain the Search Parameter 
        /// to get the tribute list</param>
        /// <returns>This method will return the List of Tribute</returns>
        public List<SearchTribute> AdvanceSearch(SearchTribute objAdvanceSearchParam)
        {
            try
            {
                return FacadeManager.SearchTributeManager.AdvanceSearch(objAdvanceSearchParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the SearchTribute Manager class method for getting the list of tribute type
        /// Added By Parul Jain
        /// </summary>
        /// <returns>This method will return the list of tribute type</returns>
        public List<Tributes> GetTributeTypeList(string applicationType)
        {
            try
            {
                return FacadeManager.SearchTributeManager.GetTributeTypeList(applicationType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the SearchTribute Manager class method for getting the list of country or state
        /// Added By Parul Jain
        /// </summary>
        /// <param name="location">A location object which contain the parent location</param>
        /// <returns>This method will return the list of country</returns>
        public IList<Locations> GetCountryList(Locations location)
        {
            try
            {
                return FacadeManager.AdvanceSearchManager.Locations(location);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the AllTribute Manager class method for getting the Recently created tribute
        /// on the basis of last created tribute.
        /// Added By Parul Jain
        /// </summary>
        /// <param name="tributeType">A int object which contain the tribute type for which we want to get the 
        /// Recently created tribute. by default it will be 1 ( for All Tribute)</param>
        /// <returns>This method will return the recently created tribute list</returns>
        public List<SearchTribute> GetRecentlyCreatedTribute(int tributeType)
        {
            try
            {
                return FacadeManager.AllTributeManager.GetRecentlyCreatedTribute(tributeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the AllTribute Manager class method for getting the most popular tribute
        /// on the basis on number of hits.
        /// Added By Parul Jain
        /// </summary>
        /// <param name="tributeType">A int object which contain the tribute type for which we want to get the 
        /// most popular tribute. by default it will be 1 ( for All Tribute)</param>
        /// <returns>This method will return the recently created tribute list</returns>
        public List<SearchTribute> GetPopularTribute(int tributeType)
        {
            try
            {
                return FacadeManager.AllTributeManager.GetPopularTribute(tributeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Method to get the list of photos.
        /// </summary>
        /// <param name="objPhotoAlbum">Photo entity containing PhotoAlbumId, page number and page size.</param>
        /// <returns>List of photos.</returns>
        public List<Photos> GetPhotosDateWise(Photos objPhoto)
        {
            return FacadeManager.PhotoManager.GetPhotos(objPhoto);
        }

        public void AddTributeCount(int TributeId)
        {
            FacadeManager.TributeManager.AddTributeCount(TributeId);
        }


        public void GetTributeSession(Tributes objtribute)
        {
            FacadeManager.TributeManager.GetTributeSession(objtribute);           
        }

        public int GetCouponAvailable(object[] objParam, int couponType)
        {
            return FacadeManager.TributeManager.GetCouponAvailable(objParam,couponType);
        }


        public void UpdateUsedCouponDetail(string couponCode)
        {
            FacadeManager.TributeManager.UpdateUsedCouponDetail(couponCode);
        }

        /// <summary>
        /// Method to get the theme for the tribute
        /// </summary>
        /// <param name="objTribute">Filled tribute entity</param>
        /// <returns>Filled Template entity.</returns>
        public Templates GetThemeForTribute(Tributes objTribute)
        {
            return FacadeManager.TributeThemeManager.GetThemeForTribute(objTribute);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objTribute"></param>
        /// <returns></returns>
        public Templates GetExistingFolderName(Tributes objTribute)
        {
            return FacadeManager.TributeThemeManager.GetExistingFolderName(objTribute);
        }
        
        /// <summary>
        /// Method to get Tribute Details for session based on Tribute Url and TributeType.
        /// </summary>
        /// <param name="objTribute">Tribute entity containing Tribute Url and Tribute Type.</param>
        /// <returns>Filled Tributes entity.</returns>
        public Tributes GetTributeSessionForUrlAndType(Tributes objTribute, string ApplicationType)
        {
            return FacadeManager.TributeManager.GetTributeSessionForUrlAndType(objTribute,ApplicationType);
        }

        /// <summary>
        /// To get tribute details on tributeId
        /// </summary>
        /// <param name="objTribute"></param>
        /// <returns></returns>
        public Tributes GetTributeSessionForId(Tributes objTribute)
        {
            return FacadeManager.TributeManager.GetTributeSessionForId(objTribute);
        }

        /// <summary>
        /// Method to save Video Tribute
        /// </summary>
        /// <param name="objVideo">Filled Video Entity</param>
        /// <returns>Video Id</returns>
        public void SaveVideoTribute(Videos objVideo)
        {
            FacadeManager.VideoManager.SaveVideo(objVideo, "VideoTribute");
        }

        public void SaveVideoTributeAndSendEmail(Videos objVideo)
        {
            FacadeManager.VideoManager.SaveVideoTributeAndSendEmail(objVideo, "VideoTribute");
        }

        public void GetCreditCardDetails(object[] param)
        {
            FacadeManager.BillingManager.GetCreditCardDetails(param);
        }

        public IList<CreditCostMapping> GetCreditCostMapping()
        {
            return FacadeManager.BillingManager.GetCreditCostMapping();
        }

        public void GetCreditPointCount(object[] param)
        {
            FacadeManager.BillingManager.GetCreditPointCount(param);
        }
        /// <summary>
        /// SequenceTributeName for suggested tribute name in sequence
        /// </summary>
        /// <param name="strTributeName"></param>
        /// <returns></returns>
        public string SequenceTributeName(string strTributeName, string strTributeType)
        {
            return FacadeManager.TributeManager.SequenceTributeName(strTributeName,strTributeType);
        }

        /// <summary>
        /// Method to get the transaction details based on the package id
        /// </summary>
        /// <param name="objValue">Package Id</param>
        /// <returns>Transaction Details</returns>
        public PaymentReceipt GetTransactionDetails(object[] objValue)
        {
            return FacadeManager.BillingManager.GetTransactionDetails(objValue);
        }
        public PaymentReceipt GetTransactionDetailsForEmail(int packId, int transactionId, object[] objVal)
        {
            return FacadeManager.BillingManager.GetTransactionDetailsForEmail(packId,transactionId, objVal);
        }

        public PaymentReceipt GetTransactionDetailsForEmail(int packId, string transactionId, object[] objVal)
        {
            return FacadeManager.BillingManager.GetTransactionDetailsForEmail(packId, transactionId, objVal);
        }

        public PaymentReceipt GetVideoTrUpgradeTransactionDetails(object[] objValue)
        {
            return FacadeManager.BillingManager.GetVideoTrUpgradeTransactionDetails(objValue);
        }

        public PaymentReceipt GetCreditTransactionDetails(object[] objValue)
        {
            return FacadeManager.BillingManager.GetCreditPtTransactionDetails(objValue);
        }


        public PaymentReceipt GetVideoTributeTransactionDetails(object[] objValue)
        {
            return FacadeManager.BillingManager.GetVideoTributeTransactionDetails(objValue);
        }
        /// <summary>
        /// Method to get the information about Donation Box
        /// </summary>
        /// <param name="param">used to specify the tribute id</param>
        public void GetDonationDetails(object[] param)
        {
            FacadeManager.TributeManager.GetDonationDetails(param);
        }

        //Start - Modification on 7-Dec-09 for the enhancement 5 of the Phase 1
        /// <summary>
        /// Method to get the User details
        /// </summary>
        /// <param name="_objUserRegistration">used to specify the UserRegistration object</param>
        public void GetUserDetails(UserRegistration _objUserRegistration)
        {
            FacadeManager.UserInfoManager.GetUserDetails(_objUserRegistration);
        }

        public void GetUserDetailsFromEmail(UserRegistration _objUserRegistration, int tributeId)
        {
            FacadeManager.UserInfoManager.GetUserDetailsFromEmail(_objUserRegistration,tributeId);
        }
        //End

        /// <summary>
        /// This method will call EventManger class to get the 
        /// EventInvitationcategories for the passed TributeType 
        /// </summary>
        /// <param name="tributeType">TributeType</param>
        /// <returns>List of EventInvitationCategory object</returns>
        public IList<EventInvitationCategory> GetEventInvitationCategories(string tributeType)
        {
            try
            {
                return FacadeManager.EventManager.EventInvitationCategories(tributeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call EventManger class to get the EventTheme Information 
        /// for the passed EventInvitationCategoryID and TributeType
        /// </summary>
        /// <param name="eventInvitationCategoryID">EventInvitationCategoryID</param>
        /// <param name="tributeType">TributeType</param>
        /// <returns>List of EventTheme object</returns>
        public IList<EventTheme> GetEventThemeInfo(int eventInvitationCategoryID, string tributeType)
        {
            try
            {
                return FacadeManager.EventManager.EventThemeInfo(eventInvitationCategoryID, tributeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call EventManger class to get the EventTheme Information for the passed EventThemeID
        /// </summary>
        /// <param name="eventThemeID"></param>
        /// <returns>EventTheme object</returns>
        public EventTheme GetEventThemeInfoByID(int eventThemeID)
        {
            try
            {
                return FacadeManager.EventManager.GetEventThemeByID(eventThemeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetUserIdByTributeId(int tributeId)
        {
            try
            {
                return FacadeManager.EventManager.GetUserIdByTributeId(tributeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserBusiness GetHeaderDetailsOnUserId(int userId)
        {
            return FacadeManager.VideoManager.GetHeaderDetailsOnUserId(userId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="videoTributeId"></param>
        /// <param name="tributeId"></param>
        public void LinkVideoTribute(LinkVideoMemorialTribute objLinkTribute)
        {
            try
            {

                FacadeManager.TributeManager.LinkVideoTribute(objLinkTribute);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="videoTributeId"></param>
        /// <returns></returns>
        public Tributes GetVideoTributeDetailById(int videoTributeId)
        {
            try
            {
                return FacadeManager.TributeManager.GetVideoTributeDetailById(videoTributeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public void UpdateCreditPointOfVideoTributeOwner(object[] param)
         {
             FacadeManager.BillingManager.UpdateCreditPointOfVideoTributeOwner(param);
       
         }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int GetLinkedVideoTributeId(object[] param)
        {
            return FacadeManager.BillingManager.GetLinkedVideoTributeId(param);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public IList<Themes> GetSubCategoryForTheme(string categoryName)
        {
            return FacadeManager.TributeManager.GetSubCategoryForTheme(categoryName);
        }

        public VideoGallery GetVideoTributeDetails(Videos objVideo)
        {
            return FacadeManager.VideoManager.GetVideoTributeDetails(objVideo);
        }
        // check email if Email exists
        //Mohit Gupta
        // 1 Feb 2011
        // modified by udham Oct 5, 2011
        public int EmailAvailable(string Email, string ApplicationType)
        {
            return FacadeManager.UserInfoManager.EmailAvailable(Email, ApplicationType);
        }

        //Mohit Gupta
        public object SavePersonalAccount(UserRegistration objUserRegistration)
        {
            return FacadeManager.UserInfoManager.SavePersonalAccount(objUserRegistration);
        }

        //Mohit Gupta Updating tribute URL on Tribute upgrade
        public void UpdateTributeURL(int tributeId,string tributeURL)
        {
            FacadeManager.TributeManager.UpdateTributeURL(tributeId, tributeURL);
        }


        public string GetTributeUrlOnTributeId(int _TribureId)
        {
            return FacadeManager.TributeManager.GetTributeUrlOnTributeId(_TribureId);
        }


        public Tributes GetTributeUrlOnOldTributeUrl(Tributes objTrb, string ApplicationType)
        {
            return FacadeManager.TributeManager.GetTributeUrlOnOldTributeUrl(objTrb, ApplicationType);
        }

        public object SendSponsorEmailOnFreeUpgrade(object[] param)
        {
            return FacadeManager.BillingManager.SendSponsorEmailOnFreeUpgrade(param);
        }
       
        public void SendEmailtoNewAdmin(Tributes objTributesUserInfo, SessionValue objGenralUserInfo, bool _Accept)
        {
            FacadeManager.UserInfoManager.SendEmailtoNewAdmin(objTributesUserInfo, objGenralUserInfo, _Accept);
        }

        public IList<GetTributesFeed> GetTributesFeed(object[] param)
        {
            return FacadeManager.UserInfoManager.GetTributesFeed(param);
        }

        public Users GetUserDetailsOnUserId(int userId)
        {
            return FacadeManager.UserInfoManager.GetUserDetailsOnUserId(userId);
        }
        // by Ud: to default theme for business type user
        public int GetDefaultTheme(int UserId, string strTributeType)
        {
            return FacadeManager.TributeManager.GetDefaultTheme(UserId, strTributeType);
        }
        // by Ud: to default theme for business type user
        public void SaveDefaultTheme(int userId, string tributeType, int themeId)
        {
            FacadeManager.TributeManager.SaveDefaultTheme(userId, tributeType, themeId);
        }

        public IList<GetTributesFeed> GetYourTributeFeedOnTributeName(object[] objparam)
        {
            return FacadeManager.UserInfoManager.GetYourTributeFeedOnTributeName(objparam);
        }

        public IList<GetTributesFeed> GetYourTributesFeed(object[] objparam)
        {
            return FacadeManager.UserInfoManager.GetYourTributesFeed(objparam);
        }

        public int GetTotalActiveObituaries(int _businessUserId)
        {
            return FacadeManager.UserInfoManager.GetTotalActiveObituaries(_businessUserId);
        }

        public int GetTotalActiveObituariesOnTributeName(object[] objprm)
        {
            return FacadeManager.UserInfoManager.GetTotalActiveObituariesOnTributeName(objprm);
        }

        public void SaveComment(Comments Comment)
        {
            FacadeManager.CommentMgr.SaveComment(Comment);
        }


        /// <summary>
        /// Added by Uattri: for YT Phase 1
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool IsTributeContainsVideoTribute(int tributeId)
        {
            return FacadeManager.TributeManager.IsTributeContainsVideoTribute(tributeId);
        }

        /// <summary>
        /// fetch all packages
        /// </summary>
        /// <returns></returns>
        internal IList<int> GetMyTributesPackages(int UserId)
        {
            return FacadeManager.TributeManager.GetMyTributesPackages( UserId);
        }

        /// <summary>
        /// fetch all admins
        /// </summary>
        /// <returns></returns>
        internal List<UserInfo> GetTributeAdmins(Tributes objtrb)
        {
            return FacadeManager.TributeManager.GetTributeAdmins(objtrb);
        }

        internal bool GetIsMobileViewOn(Tributes oTribute)
        {
            return FacadeManager.TributeManager.GetIsMobileViewOn(oTribute);
        }
    }//end class
}//end namespace
