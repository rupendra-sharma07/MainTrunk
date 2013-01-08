///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       :TributesPortal.Users.Views.UserProfilePresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for viewing the user profile pages.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.Users;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
//using TributesPortal.ResourceAccess;
//using System.Data;
using TributesPortal.MultipleLangSupport;


namespace TributesPortal.Users.Views
{
    public class UserProfilePresenter : Presenter<IUserProfile>
    {
        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        
        private UsersController _controller;
      //  private IList<ParameterTypesCodes> entities = null;
       
        
        public UserProfilePresenter([CreateNew] UsersController controller)
        {
            _controller = controller;
        }

        //private IUserProfile _IUserProfile;
        //public UserProfilePresenter(IUserProfile _IUF)
        //{
        //    this._IUserProfile = _IUF;
        //    this.Initialize();
        //}

        //private void Initialize()
        //{
        //    this._IUserProfile.SaveChanges += new EventHandler<EventArgs>(SaveChanges);
        //}



        public void SaveEmailNotification()
        {
            UserRegistration _objUserReg = new UserRegistration();
            EmailNotification _objEmaNoti = new EmailNotification();
            _objEmaNoti.UserId = View.UserId;
            _objEmaNoti.StoryNotify = View.StoryNotify;
            _objEmaNoti.NotesNotify = View.NotesNotify;
            _objEmaNoti.EventsNotify = View.EventsNotify;
            _objEmaNoti.GuestBookNotify = View.GuestBookNotify;
            _objEmaNoti.GiftsNotify = View.GiftsNotify;
            _objEmaNoti.PhotosNotify = View.PhotosNotify;
            _objEmaNoti.PhotoAlbumNotify = View.PhotoAlbumNotify;
            _objEmaNoti.VideosNotify = View.VideosNotify;
            _objEmaNoti.CommentsNotify = View.CommentsNotify;
            _objEmaNoti.MessagesNotify = View.MessagesNotify;
            _objEmaNoti.NewsLetterNotify = View.NewsLetterNotify;
            _objUserReg.EmailNotification = _objEmaNoti;
            object [] param={_objUserReg};
            this._controller.UpdateEmailNotofication(param);
            //UpdateEmailNotofication
        
        }
        public void SaveChanges(object sender, EventArgs e)
        {
            UpdateAccount();
        }


        public void UpdatePrivacySettings()
        {
            UserRegistration objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = View.UserId;
            objUsers.IsUsernameVisiable = View.IsUsernameVisiable;
            objUsers.IsLocationHide = View.IsLocationHide;
            objUsers.AllowIncomingMsg = View.AllowIncomingMsg;
            objUserReg.Users = objUsers;
            _controller.UpdatePrivacySettings(objUserReg);        
        }
        private void UpdateAccount()
        {
            UserRegistration objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
                objUsers.UserId=View.UserId;
                objUsers.FirstName=View.FirstName;
                objUsers.LastName=View.LastName;
                objUsers.UserImage = View.UserImage;
                objUsers.Country=View.Country;
                objUsers.State=View.State;
                objUsers.City=View.City;
                objUserReg.Users=objUsers;
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
                                                   View.ObituaryLinkPage
                                                   );

                    objUserReg.UserBusiness = objUserBus;
               }

               _controller.UpdatePersonalDetails(objUserReg);
        }
        public override void OnViewLoaded()
        {
            // TODO: Implement code that will be executed every time the view loads
        }

        public void OnChangeEmailPassword()
        {
            GenralUserInfo objGenralUserInfo = new GenralUserInfo();
            UserInfo objUserInfo = new UserInfo();
            objUserInfo.UserID = View.UserId;
            objUserInfo.UserEmail = View.Email;
            objUserInfo.UserPassword = View.Password;
            objGenralUserInfo.RecentUsers = objUserInfo;
            _controller.OnChangeEmailPassword(objGenralUserInfo);
            if (objGenralUserInfo.CustomError == null)
            {
                View.BannerMessage = ResourceText.GetString("msgSuccess_UP");
            }
            else
            {
                View.BannerMessage = objGenralUserInfo.CustomError.ErrorMessage.ToString();
            }
            
        }

        public override void OnViewInitialized()
        {
            // TODO: Implement code that will be executed the first time the view loads
        }

        public void GetUserDetails(int UserID)
        {
            UserRegistration _objUserReg = new UserRegistration();            
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = UserID;
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
                View.IsLocationHide = _objUserReg.Users.IsLocationHide;
                View.IsUsernameVisiable = _objUserReg.Users.IsUsernameVisiable;
                View.AllowIncomingMsg = _objUserReg.Users.AllowIncomingMsg;
                View.PanelVisibility = false;
               // SetEmailNotification(_objUserReg);
                if (_objUserReg.UserBusiness != null)
                {
                    View.ZipCode = _objUserReg.UserBusiness.ZipCode.ToString();
                    View.BusinessType = _objUserReg.UserBusiness.BusinessType;
                    View.BusinessAddress = _objUserReg.UserBusiness.BusinessAddress.ToString();
                    View.Phone = _objUserReg.UserBusiness.Phone;
                    View.CompanyName = _objUserReg.UserBusiness.CompanyName.ToString();
                    View.Website = _objUserReg.UserBusiness.Website.ToString();
                    View.PanelVisibility = true;

                }
            }
        }

        public void SetEmailNotification(int UserID)
        {
            UserRegistration _objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = UserID;
            _objUserReg.Users = objUsers;
            _controller.GetEmailNotofication(_objUserReg);
            try
            {
                if (_objUserReg.EmailNotification != null)
                {
                    View.StoryNotify = _objUserReg.EmailNotification.StoryNotify;
                    View.NotesNotify = _objUserReg.EmailNotification.NotesNotify;
                    View.EventsNotify = _objUserReg.EmailNotification.EventsNotify;
                    View.GuestBookNotify = _objUserReg.EmailNotification.GuestBookNotify;
                    View.GiftsNotify = _objUserReg.EmailNotification.GiftsNotify;
                    View.PhotosNotify = _objUserReg.EmailNotification.PhotosNotify;
                    View.PhotoAlbumNotify = _objUserReg.EmailNotification.PhotoAlbumNotify;
                    View.VideosNotify = _objUserReg.EmailNotification.VideosNotify;
                    View.CommentsNotify = _objUserReg.EmailNotification.CommentsNotify;
                    View.MessagesNotify = _objUserReg.EmailNotification.MessagesNotify;
                    View.NewsLetterNotify = _objUserReg.EmailNotification.NewsLetterNotify;
                }
                else
                {
                    View.BannerMessage = "Email Notification is not for you";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
        public void OnCountryLoad(Locations location)
        {
            View.Locations = _controller.CountryList(location);
        }
        public void BusinessTypes()
        {
            View.Business = _controller.BusinessTypes();            
        }

        public void OnBillingInformation(int UserID)
        {
            UserRegistration _objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = UserID;
            _objUserReg.Users = objUsers;
            object[] param ={ _objUserReg };
            View.BillingInformation = _controller.BillingHistory(param);
            GetCreditCardDetails(param);
        }
      

        public void OnStateLoad(Locations location)
        {
            View.States = _controller.CountryList(location);
        }

        public void GetCreditCardDetails(object[] param)
        {            
            _controller.GetCreditCardDetails(param);
            UserRegistration _objUserReg = (UserRegistration)param[0];
            if (_objUserReg.UserCreditcardDetails != null)
            {
                View.PaymentModes = _controller.PaymentModes();
                View.Telephone = _objUserReg.UserCreditcardDetails.Telephone.ToString();
                View.ZipCode = _objUserReg.UserCreditcardDetails.Zip.ToString();
                View.Country = _objUserReg.UserCreditcardDetails.Country;
                ///View.State = _objUserReg.UserCreditcardDetails.State;
                View.BusinessAddress = _objUserReg.UserCreditcardDetails.Address;
                View.ExpiryDate = _objUserReg.UserCreditcardDetails.ExpirationDate;
                View.CardNumber = _objUserReg.UserCreditcardDetails.CreditCardNo;
                View.CardName = _objUserReg.UserCreditcardDetails.CardholdersName;
                View.PaymentMethod = _objUserReg.UserCreditcardDetails.CreditCardType;
            }
            else
            {
                View.BannerMessage = "Not Found";
            }
            
        }
        public void UpdateCCDetails()
        {
            UserCreditcardDetails onjCCDetails = new UserCreditcardDetails();
            onjCCDetails.CreditCardType = View.PaymentMethod;
            onjCCDetails.CreditCardNo = View.CardNumber;
            onjCCDetails.ExpirationDate = View.ExpiryDate;
            onjCCDetails.CardholdersName = View.CardName;
            onjCCDetails.Address = View.BusinessAddress;
            onjCCDetails.Country = View.Country;
            onjCCDetails.State = View.State;
            onjCCDetails.Zip = View.ZipCode;
            onjCCDetails.Telephone = View.Telephone;
            onjCCDetails.UserId = View.UserId;
            UserRegistration _objUserReg = new UserRegistration();
            _objUserReg.UserCreditcardDetails = onjCCDetails;
            object[] param ={ _objUserReg };
            _controller.UpdateCCDetails(param);
        }

        public void DeleteCreditCardDetails()
        {
            UserCreditcardDetails onjCCDetails = new UserCreditcardDetails();
            onjCCDetails.UserId = View.UserId;
            UserRegistration _objUserReg = new UserRegistration();
            _objUserReg.UserCreditcardDetails = onjCCDetails;
            object[] param ={ _objUserReg };
            _controller.DeleteCreditCardDetails(param);
            View.BannerMessage = "Deleted";
        }
    }
}
