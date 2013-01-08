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
///File Name       : TributesPortal.Users.UsersController.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Controller class for the files under Users.
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Users;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.BusinessLogic;
//using System.Data;


namespace TributesPortal.Users
{
    public class UsersController
    {
        public UsersController()
        {
        }


        public void OnLogin()
        {
            
        }

        public object DoShortFacebookSignup(UserRegistration objUserRegistration)
        {
            return FacadeManager.UserInfoManager.SavePersonalAccount(objUserRegistration);
        }

        public bool FacebookAccountIsAvailable(GenralUserInfo objGenralUserInfo)
        {
            // TODO: check if exists record in USERS with given FacebookUid
            FacadeManager.UserInfoManager.CheckFacebookAccountAvailability(objGenralUserInfo);
            //if (objGenralUserInfo.RecentUsers.UserID != null && objGenralUserInfo.RecentUsers.UserID > 0)  // commented by Ud to remove warning
            if ( objGenralUserInfo.RecentUsers.UserID > 0)
            {
                return true;
            }
            return false;
        }

        public void ValidateLogin(GenralUserInfo objGenralUserInfo)
        {
            //code here
            FacadeManager.UserInfoManager.UserLogin(objGenralUserInfo);
            
        }

        public void UserAvailability(UserRegistration objUserRegistration)
        {
            //code here
            FacadeManager.UserInfoManager.UserAvailability(objUserRegistration);

        }
        // modified by udham 3 October,2011
        public int EmailAvailable(string Email,string ApplicationType)
        {
            //code here
           return FacadeManager.UserInfoManager.EmailAvailable(Email,ApplicationType);
        }

      

        public void GetUserDetails(UserRegistration _objUserRegistration)
        {
            //code here
            FacadeManager.UserInfoManager.GetUserDetails(_objUserRegistration);

        }

        public void GetEmailNotofication(UserRegistration _objUserRegistration)
        {
            //code here
            FacadeManager.UserInfoManager.GetEmailNotofication(_objUserRegistration);

        }
        

        public void CheckAndSendPassword(GenralUserInfo _objGenralUserInfo, bool _Reset)
        {
            FacadeManager.UserInfoManager.CheckAndSendPassword(_objGenralUserInfo, _Reset);
        }
        public void ConformAdmin(Tributes objTributesUserInfo, SessionValue objGenralUserInfo, bool _Accept)
        {
            FacadeManager.UserInfoManager.ConformAdmin(objTributesUserInfo, objGenralUserInfo, _Accept);
        }

        public void OnChangeEmailPassword(GenralUserInfo _objGenralUserInfo)
        {
            FacadeManager.UserInfoManager.ChangeEmailPassword(_objGenralUserInfo);
        }
        public void GetUserData(GenralUserInfo _objGenralUserInfo)
        {
            FacadeManager.UserInfoManager.GetUserData(_objGenralUserInfo);
        }

        public IList<Locations> CountryList(Locations location)
        {
            return FacadeManager.UserManager.Locations(location);
        }

        public object SavePersonalAccount(UserRegistration objUserRegistration)
        {
            return FacadeManager.UserInfoManager.SavePersonalAccount(objUserRegistration);
        }

        public IList<ParameterTypesCodes> BusinessTypes()
        {
           return FacadeManager.UserManager.BusinessTypes();
        }

        public IList<ParameterTypesCodes> EmailNotifications(object[] param)
        {
            return FacadeManager.UserInfoManager.EmailNotifications(param);
        }

        public IList<ParameterTypesCodes> PaymentModes()
        {
            return FacadeManager.BillingManager.PaymentModes();
        }

        public void GetTributeOnId(TributesUserInfo _objTributeUserinfo)
        {
            FacadeManager.UserInfoManager.GetTributeOnId(_objTributeUserinfo);
        }

        public void GetTributeByID(TributesUserInfo _objTributeUserinfo)
        {
            FacadeManager.UserInfoManager.GetTributeByID(_objTributeUserinfo);
        }
        
        /*public string GetEventName(int _EventId)
        {
            return FacadeManager.UserInfoManager.GetEventName(_EventId);
        }*/
        public void UpdatePersonalDetails(UserRegistration _objUserRegistration)
        {
            FacadeManager.UserInfoManager.UpdatePersonalDetails(_objUserRegistration);
        }
        public void UpdatePrivacySettings(UserRegistration _objUserRegistration)
        {
            FacadeManager.UserInfoManager.UpdatePrivacySettings(_objUserRegistration);
        }
        public void UpdateEmailNotofication(object[] param)
        {
            FacadeManager.UserInfoManager.UpdateEmailNotofication(param);
        }
        public IList<BillingHistory> BillingHistory(object[] param)
        {
          return  FacadeManager.BillingManager.BillingHistory(param);
        }
        public IList<PaymentReceipt> PaymentReceipt(object[] param)
        {
            return FacadeManager.BillingManager.PaymentReceipt(param);
        }
        //
        public void GetCreditCardDetails(object[] param)
        {
            FacadeManager.BillingManager.GetCreditCardDetails(param);
        }
        public void UpdateCCDetails(object[] param)
        {
            FacadeManager.BillingManager.UpdateRecord(param);
        }

        public void DeleteCreditCardDetails(object[] param)
        {
            FacadeManager.BillingManager.DeleteCreditCardDetails(param);
        }
        //Todo This method will remove once the final user home will be created.
        public List<SearchTribute> GetAllTributeList()
        {
            return FacadeManager.AllTributeManager.GetAllTributeList();
        }

        public List<GetMyTributes> GetMyTributes(object[] _tribute)
        {
           return FacadeManager.UserInfoManager.GetMyTributes(_tribute);
        }
        public List<GetMyTributes> GetMyFavourites(object[] _tribute)
        {
            return FacadeManager.UserInfoManager.GetMyFavourites(_tribute);
        }
        public List<MailMessage> GetMailMessage(object[] objValue)
        {
            return FacadeManager.UserInfoManager.GetMailMessage(objValue);
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
        public void SendMail(int SendByUserId, int SendToUserId, string Subject, string _EmailBody)
        {
            EmailManager objec = new EmailManager();
            objec.SendMail(SendByUserId, SendToUserId, Subject, _EmailBody);
        }

        public Events GetEventName(int EventID)
        {
            return FacadeManager.UserInfoManager.GetEventName(EventID);
        }

        #region<< Storing Session Values >>

        /// <summary>
        /// Insert Session Values,Pass to userinfo manager.
        /// </summary>
        public void SessionStore(SessionValue _objSessionValue, string strId)
        {
            FacadeManager.UserInfoManager.InsertSession(_objSessionValue, strId);
        }

        /// <summary>
        /// Method to get the details of Session values 
        /// </summary>
        public List<SessionValue> GetSessionValuesDetails(string objSessionValue)
        {
            return FacadeManager.UserInfoManager.GetSessionDetail(objSessionValue);
        }

        //TEST
        public void DeleteSessionDetails(string SessionId)
        {
            FacadeManager.UserInfoManager.DeleteSessionKeyDetails(SessionId);
        }
        public int GetCountryIdByName(string countryName)
        {
            return FacadeManager.UserManager.GetCountryIdByName(countryName);
        }
        public int GetstateIdByName(object[] objValue)
        {
            return FacadeManager.UserManager.GetstateIdByName(objValue);
        }

        #endregion<< Storing Session Values >>
        
    }
}
