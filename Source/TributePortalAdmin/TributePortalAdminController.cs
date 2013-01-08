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
///File Name       : TributesPortal.TributePortalAdmin.TributePortalAdminController.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Controller class for the files under Tribute Portal Admin.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;

namespace TributesPortal.TributePortalAdmin
{
    public class TributePortalAdminController
    {
        public TributePortalAdminController()
        {
        }
        public void ValidateLogin(GenralUserInfo objGenralUserInfo)
        {
            //code here
            FacadeManager.UserInfoManager.UserSiteAdminLogin(objGenralUserInfo);

        }
        public void UserSummaryReport(UsersSummaryReport objSummary, string applicationType)
        {
            //code here
            FacadeManager.UserInfoManager.UserSummaryReport(objSummary, applicationType);

        }
        public IList<Locations> CountryList(Locations location)
        {
            return FacadeManager.UserManager.Locations(location);
        }

        /// <summary>
        /// Method to search user based on the entered criteria.
        /// </summary>
        /// <param name="objUser">Filled Users entity.</param>
        /// <returns>List of users.</returns>
        public List<Users> SearchUsers(Users objUser)
        {
            return FacadeManager.UserManager.SearchUsers(objUser);
        }

        /// <summary>
        /// Method to delete users.
        /// </summary>
        /// <param name="objUser">User entity containing userid to be deleted.</param>
        public void DeleteUser(Users objUser)
        {
            try { FacadeManager.UserManager.DeleteUser(objUser); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Method to search tributes based on the entered criteria.
        /// </summary>
        /// <param name="objTribute">TributeSearch entity containing search criteria.</param>
        /// <returns>List of Tributes.</returns>
        public List<TributeSearch> SearchTributes(TributeSearch objTribute)
        {
            return FacadeManager.TributeManager.SearchTribute(objTribute);
        }

        /// <summary>
        /// Method to delete tribute
        /// </summary>
        /// <param name="objTribute">Tribute entity containing TributeId and UserId.</param>
        public void DeleteTribute(Tributes objTribute)
        {
            object[] objParam = { objTribute };
            FacadeManager.TributeManager.DeleteTribute(objParam);
        }

        /// <summary>
        /// Method to get the tribute activity list.
        /// </summary>
        /// <returns>Filled TributeActivityReport entity.</returns>
        public List<TributeActivityReport> GetTributeActivityReport(string applicationType)
        {
            return FacadeManager.TributeActivityManager.GetTributeActivityList(applicationType);
        }

        public IList<EventInvitationCategory> GetEventInvitationCategoryList(string tributeType)
        {
            return FacadeManager.EventManager.EventInvitationCategories(tributeType);
        }

        public IList<Themes> GetCategoryList(string applicationType)
        {
            return FacadeManager.EventManager.GetCategoryList(applicationType);
        }
        public IList<Themes> GetSubCategoryList(string strSubCategory)
        {
            return FacadeManager.EventManager.GetSubCategoryList(strSubCategory);
        }
        /// <summary>
        /// Get Themes for delete and  Update theme in Admin portal By Ashu
        /// </summary>
        /// <param name="strCategoryName"></param>
        /// <param name="strSubCategoryName"></param>
        public IList<Themes> GetThemesList(string strSubCategory, string strSubCategoryName, string applicationType)
        {
            return FacadeManager.EventManager.GetThemesList(strSubCategory, strSubCategoryName, applicationType);
        }
        /// <summary>
        ///  Get Folder Name for Update and delete theme in Admin portal By Ashu
        /// </summary>
        /// <param name="themeId"></param>
        public string GetFoldername(int themeid)
        {
            return FacadeManager.EventManager.GetFoldername(themeid);
        }
        /// <summary>
        /// To delete theme from database By Ashu
        /// </summary>
        /// <param name="themeId"></param>
        public void DeleteBasedTheme(int themeId)
        {
            FacadeManager.EventManager.DeleteTheme(themeId);
        }

        public IList<EventTheme> GetEventThemeInfo(int invitationCategory, string tributeType)
        {
            return FacadeManager.EventManager.EventThemeInfo(invitationCategory, tributeType);
        }

        public object SaveInvitationCategory(EventInvitationCategory objEventInvitationCategory)
        {
            return FacadeManager.EventManager.SaveInvitationCategory(objEventInvitationCategory);
        }



        public object SaveCategoryBasedTheme(Themes objThemes)
        {
            return FacadeManager.EventManager.SaveCategoryBasedTheme(objThemes);
        }


        public object SaveEventTheme(EventTheme objEventTheme)
        {
            return FacadeManager.EventManager.SaveEventTheme(objEventTheme);
        }


        public void Deletecategory(int rowId)
        {
            object[] objParam = { rowId };
            FacadeManager.TributeManager.Deletecategory(objParam);

        }

        public void DeleteTheme(int rowId)
        {
            object[] objParam = { rowId };
            FacadeManager.TributeManager.DeleteTheme(objParam);

        }

        public AddRemoveCreditInfo AddOrDebitCredits(AddRemoveCreditInfo objUser)
        {
            return FacadeManager.UserManager.AddOrDebitCredits(objUser);
        }

        public EnableRSSFeedInfo EnableRSSFeedForBussUser(EnableRSSFeedInfo enableRSSFeedInfo)
        {
            return FacadeManager.UserManager.EnableRSSFeedForBussUser(enableRSSFeedInfo);
        }

        public EnableRSSFeedInfo EnableXmlFeedForBussUser(EnableRSSFeedInfo enableRSSFeedInfo)
        {
            return FacadeManager.UserManager.EnableXmlFeedForBussUser(enableRSSFeedInfo);
        }

        public UpdateTribute GetTributeDetailsOnTributeId(int _tributeId)
        {
            return FacadeManager.TributeManager.GetTributeListOnTributeId(_tributeId);
        }

        public bool UpdateTributeExpiry(UpdateTribute updateTribute)
        {
            return FacadeManager.TributeManager.UpdateTributeExpiry(updateTribute);
        }

        public void UpdateAdminTributeUpdate(AdminTributeUpdate adminTributeUpdate)
        {
            FacadeManager.TributeManager.UpdateAdminTributeUpdate(adminTributeUpdate);
        }

        public bool UpdateTributePackage(UpdateTribute updateTribute)
        {
            return FacadeManager.TributeManager.UpdateTributePackage(updateTribute);
        }

        public IList<AdminTributeUpdate> GetAdminTransactions()
        {
            return FacadeManager.TributeManager.GetAdminTransactions();
        }

        public bool IsNewTypeURLValid(Tributes objTribute)
        {
            return FacadeManager.TributeManager.IsNewTypeURLValid(objTribute);
        }

        public bool UpdateNewTributeUrlTributeTypeinAlltables(UpdateTribute _objUpdateTribute, Tributes _newTribute)
        {
            return FacadeManager.TributeManager.UpdateNewTributeUrlTributeTypeinAlltables(_objUpdateTribute, _newTribute);
        }
    }
}
