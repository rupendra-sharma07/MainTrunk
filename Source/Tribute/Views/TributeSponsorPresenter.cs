///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.TributeSponsorPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for sponsoring a tribute.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using System.IO;

namespace TributesPortal.Tribute.Views
{
    public class TributeSponsorPresenter : Presenter<ITributeSponsor>
    {
        private TributeController _controller;
        public TributeSponsorPresenter([CreateNew] TributeController controller)
        {
            _controller = controller;
        }

        private string _ccSelectedState;
        public string CcSelectedState
        {
            get { return _ccSelectedState; }
            set { _ccSelectedState = value; }
        }

        private string _selectedStateForSignup;
        public string SelectedState
        {
            get { return _selectedStateForSignup; }
            set { _selectedStateForSignup = value; }
        }
        public override void OnViewLoaded()
        {
            // TODO: Implement code that will be executed every time the view loads
        }

        public override void OnViewInitialized()
        {
            // TODO: Implement code that will be executed the first time the view loads
            object[] param = { View.TransactionId };
            _controller.GetTransactionDetails(param);
        }

        // TODO: Handle other view events and set state in the view



        #region METHODS

        public void GetCCCountryList()
        {

            List<Locations> objList = new List<Locations>();
            List<Locations> objList_ = new List<Locations>();

            Locations objCount = new Locations();
            objCount.LocationParentId = 0;
            objList = (List<Locations>)_controller.CountryList(objCount);
            if (objList.Count > 0)
            {
                foreach (Locations obj in objList)
                {
                    if (obj.LocationName == "Canada")
                        objList_.Insert(0, obj);
                    else if (obj.LocationName == "United States")
                        objList_.Insert(0, obj);
                    else
                        objList_.Add(obj);
                }
            }
            View.CCCountryList = objList_;
        }

        public void GetCCStateList()
        {
            Locations objCount = new Locations();
            objCount.LocationParentId = int.Parse(View.SelectedCCCountry);
            View.CCStateList = _controller.StateList(objCount);
        }


        public void GetCountryListForSignUp()
        {

            List<Locations> objList = new List<Locations>();
            List<Locations> objList_ = new List<Locations>();

            Locations objCount = new Locations();
            objCount.LocationParentId = 0;
            objList = (List<Locations>)_controller.CountryList(objCount);
            if (objList.Count > 0)
            {
                foreach (Locations obj in objList)
                {
                    if (obj.LocationName == "Canada")
                        objList_.Insert(0, obj);
                    else if (obj.LocationName == "United States")
                        objList_.Insert(0, obj);
                    else
                        objList_.Add(obj);
                }
                View.CountryList = objList_;
            }
        }
        public void GetStateListForSignUp()
        {
            Locations objCount = new Locations();
            objCount.LocationParentId = int.Parse(View.SelectedCountry);
            View.StateList = _controller.StateList(objCount);
        }

        public void InsertCCDetails(SessionValue objSessionVal, Tributes objTribute, string confirmationId, string[] SponsorNameandMsgForEmail)
        {
            UserRegistration objUserReg = new UserRegistration();
            UserCreditcardDetails objCCdetail = new UserCreditcardDetails();
            TributePackage objpackage = new TributePackage();
            objpackage.PackageId = View.getPackageId;
            objpackage.EndDate = View.EndDate;
            objpackage.IsSponserHide = View.IsSponserHide;
            objpackage.IsSponsor = View.IsSponsor;
            objCCdetail.UserId = View.UserID;
            objCCdetail.CardholdersName = View.CardholdersName;
            objCCdetail.CreditCardType = View.PaymentMethod;
            objCCdetail.CreditCardNo = View.CreditCardNo;
            objCCdetail.CVC = View.CVC;
            objCCdetail.ExpirationDate = View.ExpirationDate;
            objCCdetail.Address = View.Address;
            objCCdetail.City = View.SelectedCCCity;
            objCCdetail.Zip = View.ZipCode;
            if (string.IsNullOrEmpty(View.SelectedCCState))
            {
                objCCdetail.State = null;
            }
            else
            {
                objCCdetail.State = int.Parse(View.SelectedCCState);
            }
            objCCdetail.Country = int.Parse(View.SelectedCCCountry);
            objCCdetail.Telephone = View.Telephone;
            objCCdetail.IsCardDetailsReusable = View.IsCardDetailsReusable;
            objCCdetail.NotifyBeforeRenew = View.NotifyBeforeRenew;
            objCCdetail.SponsorEmailAddress = View.SponsorEmailAddress;
            objUserReg.UserCreditcardDetails = objCCdetail;
            object[] param = { objUserReg, objSessionVal, objTribute, objpackage, SponsorNameandMsgForEmail };
            int CreditcardId = int.Parse((_controller.InsertCCDetails(param)).ToString());
            InsertPackageDetails(View.TributeId, CreditcardId, confirmationId);
        }

        public void GetMaymentModes()
        {
            View.PaymentModes = _controller.PaymentModes();
        }

        public int GetCouponAvailable(string CouponCode, int couponType)
        {
            CouponsAvailable objavab = new CouponsAvailable();
            objavab.CouponCode = CouponCode;
            Coupons objcou = new Coupons();
            objcou.Couponsavailable = objavab;
            object[] objParam = { objcou };
            int couponavail = _controller.GetCouponAvailable(objParam, couponType);
            if (couponavail == 1)
            {
                Couponmaster objmas = objcou.CouponMaster;
                View.IsPercentage = bool.Parse(objmas.IsPercentage.ToString());
                View.Denomination = objmas.CouponDenomination.ToString();
            }
            return couponavail;
        }

        public void UpdateUsedCouponDetails(string couponCode)
        {
            CouponsAvailable coupon = new CouponsAvailable();
            coupon.CouponCode = couponCode;
            _controller.UpdateUsedCouponDetail(couponCode);


        }




        public void InsertPackageDetails(int Identity, int creditCardId, string confirmationId)
        {
            TributePackage objpackage = new TributePackage();
            objpackage.UserId = View.UserID;
            objpackage.UserTributeId = int.Parse(Identity.ToString());
            objpackage.PackageId = View.getPackageId;
            objpackage.CreditCardId = creditCardId;
            if (View.getPackageId == 3)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = System.DateTime.Now.AddDays(14);
                objpackage.AmountPaid = 0;
            }
            if (View.getPackageId == 5 || View.getPackageId == 7)
            {
                TributePackage objPackageDetails = this.View.TributePackageDetails; // GetTributePackageInfo(View.TributeId);
                objpackage.StartDate = System.DateTime.Now;
                if (objPackageDetails != null)
                {
                    if (objPackageDetails.EndDate != null)
                    {
                        if (objPackageDetails.EndDate < DateTime.Now)
                            objpackage.EndDate = System.DateTime.Now.AddMonths(12);
                        else
                            objpackage.EndDate = objPackageDetails.EndDate.Value.AddMonths(12);
                    }
                    else
                        objpackage.EndDate = System.DateTime.Now.AddMonths(12);
                }
                else
                    objpackage.EndDate = System.DateTime.Now.AddMonths(12);

                objpackage.AmountPaid = View.AmountPaid;
            }
            if (View.getPackageId == 4 || View.getPackageId == 6)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = null;
                objpackage.AmountPaid = View.AmountPaid;
            }
            objpackage.IsAutomaticRenew = View.NotifyBeforeRenew; //.IsCardDetailsReusable;
            objpackage.CouponId = null;
            objpackage.IsSponsor = true;
            objpackage.IsSponserHide = View.IsSponserHide;
            object[] param = { objpackage, confirmationId };
            this.View.TributePackageId = int.Parse(_controller.InsertPackageDetails(param).ToString());
            this.View.TransactionId = confirmationId.Length == 0 ? 0 : int.Parse(confirmationId);
        }

        public void TriputePackageInfo(int TributeId)
        {
            TributePackage objpackage = new TributePackage();
            objpackage.UserTributeId = TributeId;
            object[] param = { objpackage };
            this._controller.TriputePackageInfo(param);
            if (objpackage.CustomError == null)
            {
                View.PackageId = objpackage.PackageId;
                View.EndDate = objpackage.EndDate;
            }

        }

        /// <summary>
        /// Method to get Tribute package details for page load.
        /// </summary>
        /// <param name="TributeId">TributeId</param>
        /// <returns>Sets value to TributePackageDetails property.</returns>
        public void GetTributePackageInfo()
        {
            TributePackage objpackage = new TributePackage();
            objpackage.UserTributeId = View.TributeId;
            object[] param = { objpackage };
            this._controller.TriputePackageInfo(param);
            View.TributePackageDetails = (TributePackage)param[0];
        }

        public void GetTributeSession(Tributes objtribute)
        {
            this._controller.GetTributeSession(objtribute);
        }

        /// <summary>
        /// Method to get Tribute Details for session based on Tribute Url and TributeType.
        /// </summary>
        /// <param name="tributeUrl">Tribute Url</param>
        /// <param name="tributeType">Tribute type description</param>
        public void GetTributeSessionForUrlAndType(string tributeUrl, string tributeType,string ApplicationType)
        {
            Tributes objTribute = new Tributes();
            objTribute.TributeUrl = tributeUrl;
            View.TributeType = objTribute.TypeDescription = tributeType;
            this.View.GetTributeSession = _controller.GetTributeSessionForUrlAndType(objTribute, ApplicationType);
            View.SubDomain = objTribute.TypeDescription;
            View.TributeURL = objTribute.TributeUrl;
        }


        /// <summary>
        /// Method to get the theme for tribute
        /// </summary>
        public string GetExistingTheme(int _tributeId)
        {

            Tributes objTribute = new Tributes();
            objTribute.TributeId = _tributeId;
            Templates objtaml = _controller.GetThemeForTribute(objTribute);
            return objtaml.ThemeValue;

            //return _controller.GetThemeForTribute(objTribute).TemplateID;

        }

        public void GetCreditCardDetails_()
        {
            UserRegistration _objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = View.UserID;
            _objUserReg.Users = objUsers;
            object[] param = { _objUserReg };
            _controller.GetCreditCardDetails(param);
            //UserRegistration _objUserReg = (UserRegistration)param[0];
            if (_objUserReg.UserCreditcardDetails != null)
            {
                View.CreditCardId = _objUserReg.UserCreditcardDetails.CreditCardId;
                View.Telephone = _objUserReg.UserCreditcardDetails.Telephone.ToString();
                View.ZipCode = _objUserReg.UserCreditcardDetails.Zip.ToString();
                GetCCCountryList();
                View.SelectedCCCountry = _objUserReg.UserCreditcardDetails.Country.ToString();
                if (_objUserReg.UserCreditcardDetails.State.ToString() != "")
                {
                    View.SelectedCCState_ = int.Parse(_objUserReg.UserCreditcardDetails.State.ToString());
                    this.CcSelectedState = _objUserReg.UserCreditcardDetails.State.ToString();
                }
                else
                    View.SelectedCCState_ = -1;
                OnStateLoad();

                View.SelectedCCCity = _objUserReg.UserCreditcardDetails.City;
                View.Address = _objUserReg.UserCreditcardDetails.Address;
                View.ExpirationDate = _objUserReg.UserCreditcardDetails.ExpirationDate;
                View.CreditCardNo = _objUserReg.UserCreditcardDetails.CreditCardNo;
                View.CardholdersName = _objUserReg.UserCreditcardDetails.CardholdersName;
                View.PaymentMethod = _objUserReg.UserCreditcardDetails.CreditCardType;
                View.CVC = _objUserReg.UserCreditcardDetails.CVC;
                //View.PaymentModes = _controller.PaymentModes();
                GetMaymentModes();
                //  View.Visibility = 1;
            }
            else
            {
                // View.BannerMessage = "You have not stored any credit card information";
                //View.Visibility = 2;
            }

        }

        public void OnStateLoad()
        {
            Locations location = new Locations();
            location.LocationParentId = int.Parse(View.SelectedCCCountry);
            View.CCStateList = _controller.CountryList(location);
        }

        /// <summary>
        /// Method to check if the logged in user is admin or not for the selected tribute.
        /// </summary>
        public void IsUserTributeAdmin()
        {
            TributeAdministrator objTributeId = new TributeAdministrator();
            objTributeId.UserTributeId = View.TributeId;
            object[] param = { objTributeId };
            IList<UserInfo> TributeAdmins = _controller.GetTributeAdminis(param);
            foreach (UserInfo obj in TributeAdmins)
            {
                if (obj.UserID == View.UserID)
                {
                    View.IsUserAdmin = true;
                    break;
                }
            }
        }

        public int GetLinkedVideoTributeId(int tributeId, int userId)
        {
            TributePackage objpackage = new TributePackage();
            objpackage.UserTributeId = tributeId;
            objpackage.UserId = userId;
            object[] param = { objpackage };
            return _controller.GetLinkedVideoTributeId(param);
        }

        public void UpdateCreditPointOfVideoTributeOwner(int videoTributeUserID)
        {
            UserRegistration _objUserReg = new UserRegistration();
            Users objUsers = new Users();
            objUsers.UserId = videoTributeUserID;
            _objUserReg.Users = objUsers;
            object[] param = { _objUserReg };
            _controller.GetCreditPointCount(param);
            UserRegistration objDetails = (UserRegistration)param[0];
            if (objDetails.CreditPointTransaction != null)
            {
                _controller.UpdateCreditPointOfVideoTributeOwner(param);
            }

        }

        public int GetUserIdByTributeId(int tributeId)
        {
            return _controller.GetUserIdByTributeId(tributeId);

        }


        public bool GetTributeAdminis()
        {
            bool hasPeronalAdmin = false;
            TributeAdministrator objCount = new TributeAdministrator();
            objCount.UserTributeId = View.TributeId;
            object[] param = { objCount };
            IList<UserInfo> userinfo = _controller.GetTributeAdminis(param);
            if (userinfo.Count > 0)
            {
                List<UserInfo> objuserinfo = new List<UserInfo>();
                for (int i = 0; i < userinfo.Count; i++)
                {
                    if (userinfo[i].IsOwner == true)
                    {
                        View.AdminOwnerId = int.Parse(userinfo[i].UserID.ToString());
                        //View.AdminOwner = userinfo[i].FirstName + " " + userinfo[i].LastName;  
                        if (userinfo[i].FirstName.Length != 0)
                            View.AdminOwner = userinfo[i].FirstName;
                        else
                            View.AdminOwner = "Business User";

                    }
                    else
                    {
                        UserInfo objUser = new UserInfo();
                        objUser.UserID = userinfo[i].UserID;
                        if (userinfo[i].FirstName.Length != 0)
                            objUser.FirstName = userinfo[i].FirstName;
                        else
                            View.AdminOwner = "Business User";

                        objUser.UserEmail = userinfo[i].UserEmail;
                        objUser.UserType = userinfo[i].UserType;
                        if (int.Parse(objUser.UserType) == 1)
                        {
                            hasPeronalAdmin = true;
                        }
                        objuserinfo.Add(objUser);
                    }
                }
                View.OtherAdmins = objuserinfo;
            }
            View.TributeAdminCount = userinfo.Count;
            return hasPeronalAdmin;
        }


        public int GetUserDetails(string email, string password, int tributeId)
        {
            UserRegistration objUser = new UserRegistration();
            Users objUsers = new Users(email, password);
            objUser.Users = objUsers;
            _controller.GetUserDetailsFromEmail(objUser, tributeId);
            if (objUser.Users != null)
            {
                View.UserDetails = objUser;
                return 1;
            }
            return -1;
        }


        public int EmailAvailable()
        {
            return _controller.EmailAvailable(View.UserEmail,View.ApplicationType);
        }

        public object SavePersonalAccount(UserRegistration objUserRegistration)
        {
            return _controller.SavePersonalAccount(objUserRegistration);
        }

        // Getting User Details of Tribute Owner
        public int GetUserTypeOfTributeOwner()
        {
            int UserId = GetUserIdByTributeId(View.TributeId);

            UserRegistration objUser = new UserRegistration();
            Users objUsers = new Users(UserId);
            objUser.Users = objUsers;
            _controller.GetUserDetails(objUser);
            return objUser.Users.UserType;
        }

        // Display the becoming an admin Panel based on conditions
        // Mohit Gupta
        // 2 Feb 2011
        public int DisplayAdminPanel(int userTypeOfTributeOwner, bool hasPersonalUserAdmin)
        {
            int NeedtoDisplay = 0;
            // If Tribute created by Business User and has no administrator other than tribute creator

            if (userTypeOfTributeOwner == 2 && View.TributeAdminCount == 1)
            {
                NeedtoDisplay = 1;
            }
            // If Tribute creator is business user and has Administartors  OR   Tribute creator is a personal user
            else if ((userTypeOfTributeOwner == 2 && View.TributeAdminCount > 1) || (userTypeOfTributeOwner == 1))
            {
                NeedtoDisplay = 2;
            }

            else if (userTypeOfTributeOwner == 2 && hasPersonalUserAdmin == false)
            {
                NeedtoDisplay = 1;
            }

            return NeedtoDisplay;

        }

        public void CheckAvailability()
        {
            View.chkAvailability = _controller.CheckUrlExists(View.TributeURL, GetTributeIdCode(View.TributeType));
        }

        private int GetTributeIdCode(string TributeName)
        {
            int count = _controller.GetTributeIdCode(TributeName);
            return count;
        }

        public string SequenceTributeName(string strTributeName, string strTributeType)
        {
            return _controller.SequenceTributeName(strTributeName, strTributeType);
        }

        public void UpdateTributeURL(int tributeId, string tributeUrl)
        {
            _controller.UpdateTributeURL(tributeId, tributeUrl);
        }
        public void CreateDefaultFolder(string applicationPath, string NewTributeURL)
        {
            //to create complete file path by appending TributeUrl with application path
            string filePath = applicationPath + NewTributeURL;
            //to get File name from web config file
            string fileName = WebConfig.DefaultFileName;
            //to create full path where the file is to be copied
            string fullPath = filePath + "\\" + fileName;
            //to create directory for tribute with tributeurl as folder name.
            DirectoryInfo objDir = new DirectoryInfo(filePath);
            if (!objDir.Exists) //if directory does not exists creates a directory
                objDir.Create();

            //to create Default.aspx file in the folder
            if (!File.Exists(fullPath)) //if file does not exists creates a file
                File.Copy(applicationPath + WebConfig.DefaultFolderUrl_ToGetDefaultFile, fullPath, false);
        }



        public void CopyOldURlFolderToNewURLFolder(string sourceFolder, string destFolder)
        {
            int CopyIteration = 0;
            int MaxCopyIteration = 0; //= 5;
            int.TryParse(WebConfig.VideoFileCopyIteration.ToString(), out MaxCopyIteration);
            if (Directory.Exists(sourceFolder))
            {
                if (!Directory.Exists(destFolder))
                    Directory.CreateDirectory(destFolder);
                string[] files = Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    CopyIteration = 0;
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(destFolder, name);
                    
                    //LHK : Added check MaxCopyIteration times if Video Copied successfully.
                    while (CopyIteration < MaxCopyIteration)
                    {
                        if (!(File.Exists(dest)) && (File.Exists(file)))
                        {
                            File.Copy(file, dest);
                        }
                        else
                        {
                            CopyIteration = 5;
                        }
                        CopyIteration++;

                    }
                }
                string[] folders = Directory.GetDirectories(sourceFolder);
                foreach (string folder in folders)
                {
                    string name = Path.GetFileName(folder);
                    string dest = Path.Combine(destFolder, name);
                    CopyOldURlFolderToNewURLFolder(folder, dest);
                }
            }
        }
        public void SendSponsorEmailOnFreeUpgrade(SessionValue objSessionVal, Tributes objTribute, string confirmationId, string[] SponsorNameandMsgForEmail)
        {
            UserRegistration objUserReg = new UserRegistration();
            UserCreditcardDetails objCCdetail = new UserCreditcardDetails();
            TributePackage objpackage = new TributePackage();
            objpackage.PackageId = View.getPackageId;
            objpackage.EndDate = View.EndDate;
            objpackage.IsSponsor = View.IsSponsor;
            objpackage.IsSponserHide = View.IsSponserHide;
            objCCdetail.UserId = View.UserID;
            objCCdetail.SponsorEmailAddress = View.SponsorEmailAddress;
            objUserReg.UserCreditcardDetails = objCCdetail;
            object[] param = { objUserReg, objSessionVal, objTribute, objpackage, SponsorNameandMsgForEmail };
            _controller.SendSponsorEmailOnFreeUpgrade(param);

        }


        public void SendEmailtoNewAdmin(Tributes objTributesUserInfo, SessionValue objGenralUserInfo, bool _Accept)
        {
            _controller.SendEmailtoNewAdmin(objTributesUserInfo, objGenralUserInfo, _Accept);
        }


        /// <summary>
        /// Added by UAttri : YT Phase 1
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public void IsTributeContainsVideoTribute(int tributeId)
        {
             View.IsContainsVideo=_controller.IsTributeContainsVideoTribute(tributeId);
        }

        /// <summary>
        /// Fetch Interest Group On Package
        /// </summary>
        /// <param name="packId"></param>
        /// <returns>packagelist</returns>
        public IList<int> GetMyTributesPackages(int UserId)
        {
            return _controller.GetMyTributesPackages(UserId);
        }

        #endregion METHODS

        /// <summary>
        /// fetch all admins
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetTributeAdmins()
        {
            Tributes objtrb = new Tributes();
            objtrb.TributeId = this.View.TributeId;
            return _controller.GetTributeAdmins(objtrb);
        }
    }
}




