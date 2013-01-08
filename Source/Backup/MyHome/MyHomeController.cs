///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.MyHomeController.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Controller class for the files under My Home.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.BusinessLogic;
//using System.Data;


namespace TributesPortal.MyHome
{
    public class MyHomeController
    {
        public MyHomeController()
        {
        }
        public IList<BillingHistory> BillingHistory(object[] param)
        {
            return FacadeManager.BillingManager.BillingHistory(param);
        }
        public void OnChangeEmailPassword(GenralUserInfo _objGenralUserInfo)
        {
            FacadeManager.UserInfoManager.ChangeEmailPassword(_objGenralUserInfo);
        }
        public void UpdateEmailNotofication(object[] param)
        {
            FacadeManager.UserInfoManager.UpdateEmailNotofication(param);
        }
        public void GetEmailNotofication(UserRegistration _objUserRegistration)
        {
            //code here
            FacadeManager.UserInfoManager.GetEmailNotofication(_objUserRegistration);
        }

        public void GetUserDetails(UserRegistration _objUserRegistration)
        {
            //code here
            FacadeManager.UserInfoManager.GetUserDetails(_objUserRegistration);
        }

        public void GetUserCompleteDetails(UserRegistration _objUserRegistration)
        {
            //code here
            FacadeManager.UserInfoManager.GetUserCompleteDetails(_objUserRegistration);
        }
        public void UpdatePrivacySettings(UserRegistration _objUserRegistration)
        {
            FacadeManager.UserInfoManager.UpdatePrivacySettings(_objUserRegistration);
        }
        //State and country
        public IList<Locations> CountryList(Locations location)
        {
            return FacadeManager.UserManager.Locations(location);
        }
        public IList<ParameterTypesCodes> BusinessTypes()
        {
            return FacadeManager.UserManager.BusinessTypes();
        }
        
        public void UpdateFacebookAssociation(UserRegistration objUserReg)
        {
            FacadeManager.UserInfoManager.UpdateFacebookAssociation(objUserReg);
        }

        public void RemoveFacebookAssociation(UserRegistration objUserReg)
        {
            FacadeManager.UserInfoManager.RemoveFacebookAssociation(objUserReg);
        }

        public void UpdatePersonalDetails(UserRegistration _objUserRegistration)
        {
            FacadeManager.UserInfoManager.UpdatePersonalDetails(_objUserRegistration);
        }
        public IList<ParameterTypesCodes> PaymentModes()
        {
            return FacadeManager.BillingManager.PaymentModes();
        }
        public void GetCreditCardDetails(object[] param)
        {
            FacadeManager.BillingManager.GetCreditCardDetails(param);
        }
        public void UpdateCCDetails(object[] param)
        {
            FacadeManager.BillingManager.UpdateRecord(param);
        }
        public IList<PaymentReceipt> PaymentReceipt(object[] param)
        {
            return FacadeManager.BillingManager.PaymentReceipt(param);
        }

        public List<GetMyTributes> GetMyTributes(object[] _tribute)
        {
            return FacadeManager.UserInfoManager.GetMyTributes(_tribute);
        }
        public IList<GetMyTributes> GetMyTribute(object[] _tribute,string name)
        {
            return FacadeManager.UserInfoManager.GetMyTribute(_tribute);
        }
        public List<GetMyTributes> GetMyFavourites(object[] _tribute)
        {
            return FacadeManager.UserInfoManager.GetMyFavourites(_tribute);
        }
        public List<MailMessage> GetMailMessage(object[] objValue)
        {
            return FacadeManager.UserInfoManager.GetMailMessage(objValue);
        }

        public List<MailMessage> GetMailThread(object[] objValue)
        {
            return FacadeManager.UserInfoManager.GetMailThread(objValue);
        }

        
        public List<MailMessage> GetuserSentMessages(object[] objValue)
        {
            return FacadeManager.UserInfoManager.GetuserSentMessages(objValue);
        }
        public List<ParameterTypesCodes> GetListofTributes(string applicationType)
        {
            return FacadeManager.TributeManager.GetListofTributes(applicationType);
        }

        public List<Events> GetMyEvents(object[] objValue)
        {
            return FacadeManager.UserInfoManager.GetMyEvents(objValue);
        }
        public void UpdateEmailAlerts(object[] _tribute)
        {
            FacadeManager.UserInfoManager.UpdateEmailAlerts(_tribute);
        }
        public void UpdateFavouriteEmailAlert(object[] _tribute)
        {
            FacadeManager.UserInfoManager.UpdateFavouriteEmailAlert(_tribute);
        }
        public void DeleteMyFavourite(object[] _tribute)
        {
            FacadeManager.UserInfoManager.DeleteMyFavourite(_tribute);
        }
        public void UpdateMessageStstus(object[] Params)
        {
            FacadeManager.UserInfoManager.UpdateMessageStstus(Params);
        }
        public void DeleteMessages(object[] Params)
        {
            FacadeManager.UserInfoManager.DeleteMessages(Params);
        }
        public void DeleteSentMessages(object[] Params)
        {
            FacadeManager.UserInfoManager.DeleteSentMessages(Params);
        }
        
        public void SendMail(int SendByUserId, int SendToUserId, string Subject, string _EmailBody)
        {
            EmailManager objec = new EmailManager();
            objec.SendMail(SendByUserId, SendToUserId, Subject, _EmailBody);
        }

        public void SendMailReply(int SendByUserId, int SendToUserId, string Subject, string _EmailBody, int Messageid)
        {
            EmailManager objec = new EmailManager();
            objec.SendMailReply(SendByUserId, SendToUserId, Subject, _EmailBody,Messageid);
        }
        public IList<UserInfo> GetTributeAdministrators(object[] param)
        {
            return FacadeManager.TributeManager.GetTributeAdministrators(param);
        }

        public IList<UserInfo> GetAdministrators(object[] param)
        {
            return FacadeManager.TributeManager.GetAdministrators(param);
        }

        public void GetTributeByID(TributesUserInfo _objTributeUserinfo)
        {
            FacadeManager.UserInfoManager.GetTributeByID(_objTributeUserinfo);
        }

        public void DeleteTribute(object[] Params)
        {
            FacadeManager.TributeManager.DeleteTribute(Params);
        }
        public void UpdateTributePrivacy(object[] Params)
        {
            FacadeManager.TributeManager.UpdateTributePrivacy(Params);
        }
        public void UpdateTributeName(object[] Params)
        {
            FacadeManager.TributeManager.UpdateTributeName(Params);
        }
        public void TriputePackageInfo(object[] param)
        {
            FacadeManager.BillingManager.TriputePackageInfo(param);
        }
        public object InsertCCDetails(object[] param)
        {
            return FacadeManager.BillingManager.InsertCCDetails(param);
        }
        public void InsertPackageDetails(object[] param)
        {
            FacadeManager.BillingManager.InsertPackageDetails(param);
        }

        public void DeleteTributeAdminis(object[] Params)
        {
            FacadeManager.UserInfoManager.DeleteTributeAdminis(Params);            
        }
        public void UpdateAutoRenew(object[] Params)
        {
            FacadeManager.BillingManager.UpdateAutoRenew(Params);
        }
        public void DeleteCreditCardDetails(object[] param)
        {
            FacadeManager.BillingManager.DeleteCreditCardDetails(param);
        }

        public void GetMyTributeCount(object[] webstat)
        {
            FacadeManager.TributeManager.GetMyTributeCount(webstat);
        }

        public void GetMyFavouritesCount(object[] webstat)
        {
            FacadeManager.TributeManager.GetMyFavouritesCount(webstat);
        }
        public void GetuserInboxTotalCount(object[] webstat)
        {
            FacadeManager.TributeManager.GetuserInboxTotalCount(webstat);
        }
        public void GetuserSentMessagesCount(object[] webstat)
        {
            FacadeManager.TributeManager.GetuserSentMessagesCount(webstat);
        }
        public void GetUsereventsCount(object[] webstat)
        {
            FacadeManager.TributeManager.GetUsereventsCount(webstat);
        }

        public void GetUserProfile(object[] webstat)
        {
            FacadeManager.UserInfoManager.GetUserProfile(webstat);
        }

        /// <summary>
        /// This method will call the UserInfoManager Manager class method for getting the tribute Listing for the
        /// Business user.
        /// Added By Parul Jain
        /// </summary>
        /// <param name="objTributeParam">This is the SearchTribute object which contain the Parameter 
        /// to get the tribute list - Sort Order, Tribuet Type and User ID</param>
        /// <returns>This method will return the List of Tribute</returns>
        public List<SearchTribute> GetBusinessUserTributeList(SearchTribute objTributeParam,string ApplicationType)
        {
            try
            {
                return FacadeManager.UserInfoManager.GetBusinessUserTributeList(objTributeParam, ApplicationType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCouponAvailable(object[] objParam, int couponType)
        {
            return FacadeManager.TributeManager.GetCouponAvailable(objParam, couponType);
        }

        /// <summary>
        ///  This method will call the UserInfoManager Manager class method for saving the message
        /// Added By Parul Jain
        /// </summary>
        /// <param name="objTributeParam">A object which contain the welcome message which wants to save</param>
        public void SaveMessage(UserBusiness objBusinessUser, string ApplicationType)
        {
            try
            {
                FacadeManager.UserInfoManager.SaveMessage(objBusinessUser, ApplicationType);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        ///  This method will call the UserInfoManager Manager class method for saving the company logo
        /// Added By Parul Jain
        /// </summary>
        /// <param name="objTributeParam">A object which contain the company logo which wants to save</param>
        public void SaveImage(UserBusiness objBusinessUser)
        {
            try
            {
                objBusinessUser.ApplicationType = WebConfig.ApplicationType.ToString();
                FacadeManager.UserInfoManager.SaveImage(objBusinessUser);
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
        /// This method will call the Gift Manager access class method to get the Image list
        /// Added By Parul Jain
        /// </summary>
        /// <returns>This method will return the Gifts object which contain the list of Image</returns>
        public List<GiftImage> GetImage()
        {
            try
            {
                return FacadeManager.UserInfoManager.GetImage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Check for Email Availability. Modified by Udham 5 October, 2011
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public int EmailAvailable(string Email,string ApplicationType)
        {
            //code here
            return FacadeManager.UserInfoManager.EmailAvailable(Email,ApplicationType);
        }

        /// <summary>
        /// Method to get details of the selected tribute based on the tributeid.
        /// </summary>
        /// <param name="tributeId">TributeId</param>
        /// <returns>Tribute details in TributeUserInfo entity.</returns>
        public TributesUserInfo GetTributeDetails(int tributeId)
        {
            TributesUserInfo objTributeDetails = new TributesUserInfo();
            Tributes objTributeId = new Tributes();
            objTributeId.TributeId = tributeId;
            objTributeDetails.Tributes = objTributeId;
            FacadeManager.UserInfoManager.GetTributeByID(objTributeDetails);
            return objTributeDetails;
        }

        /// <summary>
        /// Method to get the payment receipt based on the TributePackageId
        /// </summary>
        /// <param name="param">TributePackageId</param>
        /// <returns>Payment receipt info</returns>
        public IList<PaymentReceipt> GetPaymentReceipt(object[] param)
        {
            return FacadeManager.BillingManager.GetPaymentReceipt(param);
        }

        /// <summary>
        /// return true if tribute is not deleted else false
        /// </summary>
        /// <param name="TributeId"></param>
        public bool IsTributeExists(int TributeId)
        {
            return FacadeManager.TributeManager.IsTributeExists(TributeId);
        }

        /// <summary>
        /// method to get the information regarding the donation box of a tribute
        /// </summary>
        /// <param name="objTributes"></param>
        public void GetDonationInfo(object[] objTributes)
        {
             FacadeManager.TributeManager.GetDonationDetails(objTributes);
        }

        /// <summary>
        /// method to update the information regarding the donation box of a tribute
        /// </summary>
        /// <param name="param"></param>
        public void UpdateDonationDetails(object[] param)
        {
            FacadeManager.TributeManager.UpdateDonationDetails(param);
        }

        //AG:
        public void UpdateTributePackage(int tributeId, string tributePackageType)
        {
            FacadeManager.TributeManager.UpdateTributePackage(tributeId, tributePackageType);
        }
    }
}
