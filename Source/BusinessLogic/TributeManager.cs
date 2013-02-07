///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.TributeManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the functions associated with a tribute
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;
using System.Resources;
//using TributesPortal.MultipleLangSupport;
using System.Transactions;

namespace TributesPortal.BusinessLogic
{
    public partial class TributeManager
    {
        public List<Tributes> GetTributeList(Tributes tributes)
        {
            TributeResource objResource = new TributeResource();
            object[] obj ={ tributes };
            return objResource.GetTributes(obj);
        }
        public List<ParameterTypesCodes> GetListofTributes(string applicationType)
        {
            ParameterResource objparares = new ParameterResource();
            return objparares.GetTributeTypes(applicationType);
        }

        public List<Templates> GetTemplateList(string strTheme)
        {
            TemplateResource objTemplate = new TemplateResource();
            return objTemplate.GetTemplates(strTheme);
        }
        public List<Themes> GetThemes(string strTheme)
        {
            TemplateResource objTemplate = new TemplateResource();
            return objTemplate.GetThemes(strTheme);
        }
        public List<Themes> GetThemesForCategory(string strTheme, string categoryName,string applicationType)
        {
            TemplateResource objTemplate = new TemplateResource();
            return objTemplate.GetThemesForCategory(strTheme, categoryName, applicationType);
        }
        //
        public IList<Locations> GetCountryList(Locations locations)
        {
            LocationResource objLocations = new LocationResource();
            return objLocations.LocationList(locations);
        }

        public IList<Locations> GetCountryState(object[] param)
        {
            LocationResource objLocations = new LocationResource();
            return objLocations.GetCountryState(param);
        }
        public IList<Locations> GetStateList(Locations locations)
        {
            LocationResource objResource = new LocationResource();
            return objResource.LocationList(locations);
        }

        public IList<UserInfo> GetTributeAdminis(object[] UserTributeID)
        {
            TributeResource objResource = new TributeResource();
            return objResource.GetTributeAdminis(UserTributeID);
        }

        public IList<UserInfo> GetTributeAdministrators(object[] UserTributeID)
        {
            TributeResource objResource = new TributeResource();
            return objResource.GetTributeAdministrators(UserTributeID);
        }

        public IList<UserInfo> GetAdministrators(object[] UserTributeID)
        {
            TributeResource objResource = new TributeResource();
            return objResource.GetAdministrators(UserTributeID);
        }


        public string CheckUrlExists(string strUrl, int tributetype)
        {
            string strSetButtonText = null;
            TributeResource objResource = new TributeResource();
            return objResource.CheckUrlExistsInTable(strUrl, tributetype);


        }
        public object SaveTribute(Tributes tributes)
        {
            TributeResource objTributes = new TributeResource();
            object[] obj ={ tributes };

            object objTributeReturn = new object();
            using (TransactionScope trans = new TransactionScope())
            {
                objTributeReturn = objTributes.InsertTribute(obj);
                //Transaction Commited
                trans.Complete();
            }
            return objTributeReturn;
        }

        /// <summary>
        /// Save the Donation Object
        /// </summary>
        /// <param name="objDonation"></param>
        /// <returns></returns>
        public object SaveDonation(Donation objDonation)
        {
            TributeResource objTributes = new TributeResource();
            object[] obj ={ objDonation };

            object objDonationReturn = new object();
            using (TransactionScope trans = new TransactionScope())
            {
                objDonationReturn = objTributes.InsertDonation(obj);
                //Transaction Commited
                trans.Complete();
            }
            return objDonationReturn;
        }


        public List<ParameterTypesCodes> GetTributeTypesbyTypeCode(int TypeCode)
        {
            ParameterResource objparares = new ParameterResource();
            return objparares.GetTributeTypesbyTypeCode(TypeCode);
        }
        public void UpdateTributeMessage(object[] tribute)
        {
            TributeResource objResource = new TributeResource();
            using (TransactionScope trans = new TransactionScope())
            {
                objResource.UpdateTributeMessage(tribute);
                trans.Complete();
            }
        }

        //Delete tribute.
        public void DeleteTribute(object[] tribute)
        {
            TributeResource objResource = new TributeResource();
            using (TransactionScope trans = new TransactionScope())
            {
                objResource.DeleteTribute(tribute);
                trans.Complete();
            }
        }
        //Update tribute privacy
        public void UpdateTributePrivacy(object[] tribute)
        {
            TributeResource objResource = new TributeResource();
            using (TransactionScope trans = new TransactionScope())
            {
                objResource.UpdateTributePrivacy(tribute);
                trans.Complete();
            }
        }
        //Update tribute name
        public void UpdateTributeName(object[] tribute)
        {
            TributeResource objResource = new TributeResource();
            using (TransactionScope trans = new TransactionScope())
            {
                objResource.UpdateTributeName(tribute);
                trans.Complete();
            }
        }

        //Get Tribute Visit.
        public void GetTributeCount(object[] webstat)
        {
            TributeResource objResource = new TributeResource();
            objResource.GetTributeCount(webstat);
        }

        //get MyTribute Count
        public void GetMyTributeCount(object[] webstat)
        {
            TributeResource objResource = new TributeResource();
            objResource.GetMyTributeCount(webstat);
        }
        public void GetMyFavouritesCount(object[] webstat)
        {
            TributeResource objResource = new TributeResource();
            objResource.GetMyFavouritesCount(webstat);
        }

        public void GetuserInboxTotalCount(object[] webstat)
        {
            TributeResource objResource = new TributeResource();
            objResource.GetuserInboxTotalCount(webstat);
        }

        public void GetuserSentMessagesCount(object[] webstat)
        {
            TributeResource objResource = new TributeResource();
            objResource.GetuserSentMessagesCount(webstat);
        }
        public void GetUsereventsCount(object[] webstat)
        {
            TributeResource objResource = new TributeResource();
            objResource.GetUsereventsCount(webstat);
        }
        /// <summary>
        /// LHK: get all latest date to traverse get latest activities
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public List<DateTime> GetAllLatestDates(int tributeId)
        {
            TributeResource objResource = new TributeResource();
            return objResource.GetAllLatestDates(tributeId);
        }

        public IList<TributeLatest> GetLatestVideos(object[] objTributeLatest)
        {
            TributeResource objResource = new TributeResource();
            return objResource.GetLatestVideos(objTributeLatest);
        }
        public bool IsStoryAdded(object[] objUserAdmin)
        {
            TributeResource objResource = new TributeResource();
            return objResource.IsStoryAdded(objUserAdmin);
        }

        public bool IsStoryHide(int TributeId)
        {
            TributeResource objResource = new TributeResource();
            return objResource.IsStoryHide(TributeId);
        }
        public void SetStoryVisibility(int TributeId)
        {
            TributeResource objResource = new TributeResource();
            objResource.setStoryVisibility(TributeId);
        }

        public int GetTributeIdCode(string TributeName)
        {
            TributeResource objResource = new TributeResource();
            return objResource.GetTributeIdCode(TributeName);

        }
        public void GetTributeTypebyCode(object[] objParam)
        {
            TributeResource objResource = new TributeResource();
            objResource.GetTributeTypebyCode(objParam);

        }

        /// <summary>
        /// Method to add tribute to user favorite list
        /// </summary>
        /// <param name="objParam">Filled AddToFavorite entity.</param>
        /// <returns>0 if value added else > 0 (already in favorite list)</returns>
        public int AddToFavourites(AddToFavorite objFavorite)
        {
            TributeResource objTributeResource = new TributeResource();
            object[] objParam = { objFavorite };
            return objTributeResource.AddToFavorites(objParam);
        }


        /// <summary>
        /// Method to insert Tribute Count.
        /// </summary>
        /// <param name="TributeId"></param>
        public void AddTributeCount(int TributeId)
        {
            TributeResource objTributeResource = new TributeResource();
            objTributeResource.AddTributeCount(TributeId);
        }
        public void GetTributeSession(Tributes objtribute)
        {
            TributeResource objTributeResource = new TributeResource();
            objTributeResource.GetTributeSession(objtribute);
        }

        /// <summary>
        /// Method to get count for Tribute is in User favorite list or not.
        /// </summary>
        /// <param name="objFavorite">AddToFavorite entity containing TributeId and UserId</param>
        /// <returns>Count if > 0 already in list else not in list</returns>
        public int GetUserTributeFavorites(AddToFavorite objFavorite)
        {
            TributeResource objTributeResource = new TributeResource();
            object[] objParam = { objFavorite };
            return objTributeResource.GetUserTributeFavorites(objParam);
        }

        public UpdateTribute GetTributeListOnTributeId(int tributeId)
        {
            TributeResource objTributeResource = new TributeResource();
            return objTributeResource.GetTrbDetailOnTributeId(tributeId);
        }

        //AG:
        public object[] GetTributeDetailOnTributeId(int tributeId)
        {
            TributeResource objTributeResource = new TributeResource();
            return objTributeResource.GetTributeDetailOnTributeId(tributeId);
        }

        //AG:
        public object[] GetTributeUserDetailOnTributeId(int tributeId)
        {
            TributeResource objTributeResource = new TributeResource();
            return objTributeResource.GetTributeUserDetailOnTributeId(tributeId);
        }

        /// <summary>
        /// Method to remove favorite from list.
        /// </summary>
        /// <param name="objFavorite">Filled AddToFavorite entity.</param>
        public void RemoveFromFavotire(AddToFavorite objFavorite)
        {
            TributeResource objTributeResource = new TributeResource();
            object[] objParam = { objFavorite };
            objTributeResource.RemoveFromFavorites(objParam);
        }

        /// <summary>
        /// Method to check coupon availability.
        /// </summary>
        /// <param name="objParam"></param>
        /// <returns></returns>
        public int GetCouponAvailable(object[] objParam, int couponType)
        {
            TributeResource objTributeResource = new TributeResource();
            return objTributeResource.GetCouponAvailable(objParam, couponType);

        }


        /// <summary>
        /// Method to update used coupon details.
        /// </summary>
        /// <param name="objParam"></param>
        /// <returns></returns>
        public void UpdateUsedCouponDetail(string couponCode)
        {
            TributeResource objTributeResource = new TributeResource();
            objTributeResource.UpdateUsedCouponDetail(couponCode);
        }

        




        /// <summary>
        /// Method to search tributes based on the entered criteria.
        /// </summary>
        /// <param name="objTribute">Filled TributeSearch entity containing search criteria.</param>
        /// <returns>List of tributes.</returns>
        public List<TributeSearch> SearchTribute(TributeSearch objTribute)
        {
            TributeResource objTributeResource = new TributeResource();
            object[] objParam = { objTribute };
            return objTributeResource.SearchTributes(objParam);
        }

        /// <summary>
        /// Method to get Tribute Details for session based on Tribute Url and TributeType.
        /// </summary>
        /// <param name="objTribute">Tribute entity containing Tribute Url and Tribute Type.</param>
        /// <returns>Filled Tributes entity.</returns>
        public Tributes GetTributeSessionForUrlAndType(Tributes objTribute, string ApplicationType)
        {
            TributeResource objTributeResource = new TributeResource();
            object[] objParam = { objTribute };
            return objTributeResource.GetTributeSessionForUrlAndType(objParam, ApplicationType);
        }


        public Tributes GetTributeSessionForId(Tributes objTribute)
        {
            TributeResource objTributeResource = new TributeResource();
            object[] objParam = { objTribute };
            return objTributeResource.GetTributeSessionForId(objParam);
        }


        public string SequenceTributeName(string strTributeName, string strTributeType)
        {
            TributeResource objTributeResource = new TributeResource();
            return objTributeResource.SequenceTributeName(strTributeName, strTributeType);
        }

        public bool IsTributeExists(int TributeId)
        {
            TributeResource objTributeResource = new TributeResource();
            return objTributeResource.IsTributeExists(TributeId);
        }

        /// <summary>
        /// Method to get the information about Donation Box
        /// </summary>
        /// <param name="param">used to specify the tribute id</param>
        public void GetDonationDetails(object[] param)
        {
            TributeResource objResource = new TributeResource();
            objResource.GetDonationInfo(param);
            //throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Method to update information regarding the donation box
        /// this only updaes info at our end not at the epartners end
        /// </summary>
        /// <param name="objTributes"></param>
        public void UpdateDonationDetails(object[] objTributes)
        {
            TributeResource objResource = new TributeResource();
            using (TransactionScope trans = new TransactionScope())
            {
                objResource.UpdateDonationDetails(objTributes);
                trans.Complete();
            }
        }

        public void UpdateTributePackage(int tributeId, string tributePackageType)
        {
            TributeResource objResource = new TributeResource();
            using (TransactionScope trans = new TransactionScope())
            {
                objResource.UpdateTributePackage(tributeId, tributePackageType);
                trans.Complete();
            }
        }

        public void Deletecategory(object[] objRowID)
        {
            TributeResource objResource = new TributeResource();
            using (TransactionScope trans = new TransactionScope())
            {
                objResource.Deletecategory(objRowID);
                trans.Complete();
            }
        }

        public void DeleteTheme(object[] objRowID)
        {
            TributeResource objResource = new TributeResource();
            using (TransactionScope trans = new TransactionScope())
            {
                objResource.DeleteTheme(objRowID);
                trans.Complete();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="videoTributeId"></param>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public void LinkVideoTribute(LinkVideoMemorialTribute objLinkTribute)
        {
            TributeResource objResource = new TributeResource();
            using (TransactionScope trans = new TransactionScope())
            {
                objResource.LinkVideoTribute(objLinkTribute);
                trans.Complete();
            }
        }


        public Tributes GetVideoTributeDetailById(int videoTributeId)
        {
            TributeResource objTributeResource = new TributeResource();
            return objTributeResource.GetVideoTributeDetailById(videoTributeId);
        }

        public IList<Themes> GetSubCategoryForTheme(string categoryName)
        {
            TributeResource objTributeResource = new TributeResource();
            return objTributeResource.GetSubCategoryForTheme(categoryName);
        }


        public void UpdateTributeURL(int tributeId,string tributeUrl)
        {
            TributeResource objResource = new TributeResource();
            objResource.UpdateTributeURL(tributeId, tributeUrl);
        }

        public bool UpdateTributeExpiry(UpdateTribute objUTrb)
        {
            TributeResource objResource = new TributeResource();
            return objResource.UpdateTributeExpiry(objUTrb);
        }


        public string GetTributeUrlOnTributeId(int _TribureId)
        {
            TributeResource objTributeResource = new TributeResource();
            return objTributeResource.GetTributeUrlOnTributeId(_TribureId);
        }

        public Tributes GetTributeUrlOnOldTributeUrl(Tributes objTrb,String ApplicationType)
        {
            TributeResource objTributeResource = new TributeResource();
            return objTributeResource.GetTributeUrlOnOldTributeUrl(objTrb, ApplicationType);
        }

        // by Ud: to getting Default Theme for Business type user
        public int GetDefaultTheme(int UserId, string strTributeType)
        {
            TemplateResource objTemplate = new TemplateResource();
            return objTemplate.GetDefaultTheme(UserId, strTributeType);
        }
        // by Ud: for saving and updating theme for business type User
        public void SaveDefaultTheme(int userId,string tributeType ,int themeId)
        {
            TemplateResource objTemplate = new TemplateResource();
            using (TransactionScope trans = new TransactionScope())
            {
                objTemplate.SaveDefaultTheme(userId, tributeType, themeId);
                //Transaction Commited
                trans.Complete();
            }
            
        }


        public Tributes GetTributeOnTributeId(int tributeId)
        {
            TributeResource objTributeResource = new TributeResource();
            return objTributeResource.GetTributeOnTributeId(tributeId);
        }


        public void UpdateAdminTributeUpdate(AdminTributeUpdate adminTributeUpdate)
        {
            TributeResource objTributeResource = new TributeResource();
            objTributeResource.UpdateAdminTributeUpdate(adminTributeUpdate);
        }

        public bool UpdateTributePackage(UpdateTribute updateTribute)
        {
            TributeResource objResource = new TributeResource();
            return objResource.UpdateTributePackage(updateTribute);
        }

        public IList<AdminTributeUpdate> GetAdminTransactions()
        {
            TributeResource objResource = new TributeResource();
            return objResource.GetAdminTransactions();
        }

        public bool IsNewTypeURLValid(Tributes objTribute)
        {
            TributeResource objResource = new TributeResource();
            return objResource.IsNewTypeURLValid(objTribute);
        }

        public bool UpdateNewTributeUrlTributeTypeinAlltables(UpdateTribute _objUpdateTribute, Tributes _newTribute)
        {
            TributeResource objResource = new TributeResource();
            return objResource.UpdateNewTributeUrlTributeTypeinAlltables(_objUpdateTribute, _newTribute);
        }

        /// <summary>
        /// Added by UAttri: for checking whether a tribute contains video tribute
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool IsTributeContainsVideoTribute(int tributeId)
        {
            TributeResource objResource = new TributeResource();
            return objResource.IsTributeContainsVideoTribute(tributeId);
        }


        /// <summary>
        /// fetch all packages
        /// </summary>
        /// <returns></returns>
        public IList<int> GetMyTributesPackages(int UserId)
        {
            TributeResource objResource = new TributeResource();
            return objResource.GetMyTributesPackages( UserId);
        }

        /// <summary>
        /// fetch all admins
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetTributeAdmins(Tributes objtrb)
        {
            TributeResource objResource = new TributeResource();
            return objResource.GetTributeAdmins(objtrb);
        }

        public int GetTrbDetailOnTributeId(int tributeId)
        {
            int packageId=0;
            TributeResource objResource = new TributeResource();
            UpdateTribute objtrb = new UpdateTribute();
            objtrb = objResource.GetTrbDetailOnTributeId(tributeId);
            if (objtrb != null)
            {
                packageId = objtrb.PackageId;
            }
            return packageId;
        }

        public bool IsAllowedPhotoCheck(int _photoAlbumId)
        {
            TributeResource objResource = new TributeResource();
            return objResource.IsAllowedPhotoCheck(_photoAlbumId);
        }

        public int GetPackIdonPhotoAlbumId(int photoAlbumId)
        {
            TributeResource objResource = new TributeResource();
            return objResource.GetPackIdonPhotoAlbumId( photoAlbumId);
        }

        public bool IsAllowedPhotoCheckonPhotoId(int PhotoId)
        {
            TributeResource objResource = new TributeResource();
            return objResource.IsAllowedPhotoCheckonPhotoId(PhotoId);
        }

        public bool GetIsMobileViewOn(Tributes obTrb)
        {
            TributeResource objResource = new TributeResource();
            return objResource.GetIsMobileViewOn(obTrb);
        }

        public bool UpdateUserTributeMobileView(Users users)
        {
            TributeResource objResource = new TributeResource();
            return objResource.UpdateUserTributeMobileView(users);
        }
    }//end class
}//end namespace
