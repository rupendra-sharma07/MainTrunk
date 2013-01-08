///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.AdminProfileSettingsPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Admin Profile Settings.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
//using TributesPortal.Users;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
//using TributesPortal.ResourceAccess;
//using System.Data;
using TributesPortal.MultipleLangSupport;
using TributesPortal.BusinessLogic;

namespace TributesPortal.MyHome.Views
{
    public class AdminProfileSettingsPresenter : Presenter<IAdminProfileSettings>
    {
        private MyHomeController _controller;


        public AdminProfileSettingsPresenter([CreateNew] MyHomeController controller)
        {
            _controller = controller;
        }
        public void OnCountryLoad(Locations location)
        {
            //  View.Locations = _controller.CountryList(location);

            List<Locations> objList = new List<Locations>();
            List<Locations> objList_ = new List<Locations>();
            objList = (List<Locations>)_controller.CountryList(location);
            if (objList.Count > 0)
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    if (objList[i].LocationName.Equals("United States"))
                    {
                        Locations objloc = new Locations();
                        objloc.LocationId = objList[i].LocationId;
                        objloc.LocationName = objList[i].LocationName;
                        objloc.LocationParentId = objList[i].LocationParentId;
                        objList_.Add(objloc);
                    }
                }
                for (int i = 0; i < objList.Count; i++)
                {
                    if (objList[i].LocationName.Equals("Canada"))
                    {
                        Locations objloc = new Locations();
                        objloc.LocationId = objList[i].LocationId;
                        objloc.LocationName = objList[i].LocationName;
                        objloc.LocationParentId = objList[i].LocationParentId;
                        objList_.Add(objloc);
                    }
                }
                for (int i = 0; i < objList.Count; i++)
                {
                    Locations objloc = new Locations();
                    objloc.LocationId = objList[i].LocationId;
                    objloc.LocationName = objList[i].LocationName;
                    objloc.LocationParentId = objList[i].LocationParentId;
                    objList_.Add(objloc);
                }
            }
            View.Locations = objList_;
        }
        public void OnStateLoad(Locations location)
        {
            View.States = _controller.CountryList(location);
        }

        public void BusinessTypes()
        {
            View.Business = _controller.BusinessTypes();
        }

        public void GetImage()
        {
            try
            {
                View.ImageList = _controller.GetImage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserRegistration UpdateFacebookAssociation()
        {
            UserRegistration objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = View.UserId;
            objUsers.FacebookUid = View.FacebookUid;
            objUserReg.Users = objUsers;
            _controller.UpdateFacebookAssociation(objUserReg);
            return objUserReg;
        }

        public void RemoveFacebookAssociation()
        {
            UserRegistration objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = View.UserId;
            objUserReg.Users = objUsers;
            _controller.RemoveFacebookAssociation(objUserReg);
        }

        public void UpdateAccount()
        {

            UserRegistration objUserReg = new UserRegistration();
            string[] virtualDir = CommonUtilities.GetPath();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = View.UserId;
            objUsers.FirstName = View.FirstName;
            objUsers.LastName = View.LastName;
            string strImageURL = View.UserImage;
            if (strImageURL.Contains(virtualDir[9]))
            objUsers.UserImage = View.UserImage.Replace(virtualDir[9],""); //View.UserImage;
            else if (strImageURL.Contains(virtualDir[2]))
                objUsers.UserImage = View.UserImage.Replace(virtualDir[2],""); //View.UserImage;
            objUsers.Country = View.Country;
            objUsers.State = View.State;
            objUsers.City = View.City;
            objUsers.FacebookUid = View.FacebookUid;
            
            objUserReg.Users = objUsers;

            if (View.PanelVisibility == true)
            {
                UserBusiness objUserBus = new UserBusiness(
                                               View.Website,
                                               View.CompanyName,
                                               View.BusinessType,
                                               View.BusinessAddress,
                                               View.Phone,
                                               View.HeaderBGColor,
                                               View.ZipCode,
                                               View.IsAddressOn,
                                               View.IsWebAddressOn,
                                               View.IsObUrlLinkOn,
                                               View.IsPhoneNoOn,
                                               View.DisplayCustomHeader,
                                               View.HeaderLogo,
                                               View.ObituaryLinkPage)
                                               ;

                objUserReg.UserBusiness = objUserBus;
            }

            _controller.UpdatePersonalDetails(objUserReg);
        }
        public void GetUserDetails()
        {
            UserRegistration _objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = View.UserId;
            _objUserReg.Users = objUsers;
            _controller.GetUserDetails(_objUserReg);

            if (_objUserReg.Users != null)
            {
                View.UserName = _objUserReg.Users.UserName.ToString();
                View.FirstName = _objUserReg.Users.FirstName.ToString();
                View.LastName = _objUserReg.Users.LastName.ToString();
                View.Country = _objUserReg.Users.Country;
                View.State = _objUserReg.Users.State;
                View.City = _objUserReg.Users.City.ToString();
                string _UserImage = _objUserReg.Users.UserImage.ToString();
                if (_UserImage.StartsWith("http://") || _UserImage.StartsWith("https://"))
                {
                    View.UserImage = _UserImage;
                }
                else
                {
                    if (WebConfig.ApplicationMode.Equals("local"))
                    {
                        View.UserImage = CommonUtilities.GetPath()[9].ToString() + _UserImage;
                    }
                    else
                    {
                        //View.UserImage = CommonUtilities.GetPath()[9].ToString().ToLower().Replace("http","https") + _UserImage;

                        if (CommonUtilities.GetPath()[9].ToString().ToLower().IndexOf("https") < 0)
                        {
                            View.UserImage = CommonUtilities.GetPath()[9].ToString().ToLower().Replace("http", "https") + _UserImage;
                        }
                        else
                        {
                            View.UserImage = CommonUtilities.GetPath()[9].ToString().ToLower() + _UserImage;
                        }
                    }
                }

            }
            //View.IsLocationHide = _objUserReg.Users.IsLocationHide;
            //View.IsUsernameVisiable = _objUserReg.Users.IsUsernameVisiable;
            //View.AllowIncomingMsg = _objUserReg.Users.AllowIncomingMsg;
            View.PanelVisibility = false;
            if (_objUserReg.UserBusiness != null)
            {
                View.ZipCode = _objUserReg.UserBusiness.ZipCode.ToString();
                View.BusinessType = _objUserReg.UserBusiness.BusinessType;
                View.BusinessAddress = _objUserReg.UserBusiness.BusinessAddress.ToString();
                View.Phone = _objUserReg.UserBusiness.Phone;
                View.HeaderBGColor = _objUserReg.UserBusiness.HeaderBGColor.ToString();
                View.CompanyName = _objUserReg.UserBusiness.CompanyName.ToString();
                View.Website = _objUserReg.UserBusiness.Website.ToString();
                View.HeaderLogo = _objUserReg.UserBusiness.HeaderLogo.ToString();
                View.ObituaryLinkPage = _objUserReg.UserBusiness.ObituaryLinkPage.ToString();
                View.IsAddressOn = bool.Parse(_objUserReg.UserBusiness.IsAddressOn.ToString());
                View.IsWebAddressOn = bool.Parse(_objUserReg.UserBusiness.IsWebAddressOn.ToString());
                View.IsObUrlLinkOn = bool.Parse(_objUserReg.UserBusiness.IsObUrlLinkOn.ToString());
                View.IsPhoneNoOn = bool.Parse(_objUserReg.UserBusiness.IsPhoneNoOn.ToString());
                View.DisplayCustomHeader = bool.Parse(_objUserReg.UserBusiness.DisplayCustomHeader.ToString());

                View.PanelVisibility = true;

            }
        }


        public void GetUserCompleteDetail(int userId)
        {
            UserRegistration _objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = userId;
            _objUserReg.Users = objUsers;

            _controller.GetUserCompleteDetails(_objUserReg);

            GenralUserInfo _objGenralUserInfo = new GenralUserInfo();
            UserInfo objUserInfo = new UserInfo();

            if (_objUserReg.Users.UserName != null)
            {
                objUserInfo.UserName = _objUserReg.Users.UserName;
                objUserInfo.UserPassword = TributePortalSecurity.Security.EncryptSymmetric(_objUserReg.Users.Password);
                objUserInfo.ApplicationType = _objUserReg.Users.ApplicationType;

                _objGenralUserInfo.RecentUsers = objUserInfo;

                FacadeManager.UserInfoManager.UserLogin(_objGenralUserInfo);

                if (_objGenralUserInfo.CustomError == null)
                {
                    SetSessionValue(_objGenralUserInfo);
                    //View.Message = "";
                }
            }
            //View.UserName = _objUserReg.Users.UserName.ToString();
            //View.FirstName = _objUserReg.Users.FirstName.ToString();
            //View.LastName = _objUserReg.Users.LastName.ToString();
            ////View.Country = _objUserReg.Users.Country;
            //View.State = _objUserReg.Users.State;
            //View.City = _objUserReg.Users.City.ToString();
            //View.UserImage = CommonUtilities.GetPath()[9].ToString() + _objUserReg.Users.UserImage.ToString();

            //View.PanelVisibility = false;
            //if (_objUserReg.UserBusiness != null)
            //{
            //    View.ZipCode = _objUserReg.UserBusiness.ZipCode.ToString();
            //    View.BusinessType = _objUserReg.UserBusiness.BusinessType;
            //    View.BusinessAddress = _objUserReg.UserBusiness.BusinessAddress.ToString();
            //    View.Phone = _objUserReg.UserBusiness.Phone;
            //    View.CompanyName = _objUserReg.UserBusiness.CompanyName.ToString();
            //    View.Website = _objUserReg.UserBusiness.Website.ToString();
            //    View.PanelVisibility = true;

            //}



        }

        /// <summary>
        /// SetSessionValues
        /// </summary>
        /// <param name="_objGenralUserInfo"></param>
        private void SetSessionValue(GenralUserInfo _objGenralUserInfo)
        {
            SessionValue _objSessionValue = new SessionValue(_objGenralUserInfo.RecentUsers.UserID,
                                                               _objGenralUserInfo.RecentUsers.UserName,
                                                               _objGenralUserInfo.RecentUsers.FirstName,
                                                               _objGenralUserInfo.RecentUsers.LastName,
                                                               _objGenralUserInfo.RecentUsers.UserEmail,
                                                               int.Parse(_objGenralUserInfo.RecentUsers.UserType),
                                                               _objGenralUserInfo.RecentUsers.UserTypeDescription,
                                                               _objGenralUserInfo.RecentUsers.IsUsernameVisiable
                                                               );
            TributesPortal.Utilities.StateManager stateManager = StateManager.Instance;
            stateManager.Add("objSessionvalue", _objSessionValue, StateManager.State.Session);
        }
    }
}
