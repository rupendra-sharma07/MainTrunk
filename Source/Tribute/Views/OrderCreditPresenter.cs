using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using System.Collections;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;

namespace TributesPortal.Tribute.Views
{
    public class OrderCreditPresenter : Presenter<IOrderCredit>
    {

        private TributeController _controller;
        public OrderCreditPresenter([CreateNew] TributeController controller)
        {
            _controller = controller;
        }

        private string _ccSelectedState;
        public string CcSelectedState
        {
            get { return _ccSelectedState; }
            set { _ccSelectedState = value; }
        }

        public override void OnViewLoaded()
        {
            // TODO: Implement code that will be executed every time the view loads
        }

        public override void OnViewInitialized()
        {
            // TODO: Implement code that will be executed the first time the view loads
            object[] param = { View.TransactionId };
            _controller.GetCreditTransactionDetails(param);
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

                /*for (int i = 0; i < objList.Count; i++)
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
                    Locations objloc = new Locations();
                    objloc.LocationId = objList[i].LocationId;
                    objloc.LocationName = objList[i].LocationName;
                    objloc.LocationParentId = objList[i].LocationParentId;
                    objList_.Add(objloc);
                }*/
            }
            View.CCCountryList = objList_;
            //View.CCCountryList = _controller.CountryList(objCount);
        }

        public void GetCCStateList()
        {
            Locations objCount = new Locations();
            objCount.LocationParentId = int.Parse(View.SelectedCCCountry);
            View.CCStateList = _controller.StateList(objCount);
        }

        public int InsertCCDetails(SessionValue objSessionVal, Tributes objTribute, string confirmationId)
        {
            UserRegistration objUserReg = new UserRegistration();
            UserCreditcardDetails objCCdetail = new UserCreditcardDetails();
            TributePackage objpackage = new TributePackage();
            objpackage.PackageId = View.getPackageId;
            objpackage.EndDate = View.EndDate;
            objpackage.IsSponsor = true;

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
            object[] param = { objUserReg, objSessionVal, objTribute, objpackage };
            //int CreditcardId = int.Parse((_controller.InsertCCDetails(param)).ToString());
            int CreditcardId = (int.Parse)(_controller.InsertCCDetails(param).ToString());
            return CreditcardId;
        }

        public int InsertCreditPointCCDetails(SessionValue objSessionVal, string confirmationId)
        {
            UserRegistration objUserReg = new UserRegistration();
            UserCreditcardDetails objCCdetail = new UserCreditcardDetails();
            

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
            object[] param = { objUserReg, objSessionVal};
            //int CreditcardId = int.Parse((_controller.InsertCCDetails(param)).ToString());
            int CreditcardId = (int.Parse)(_controller.InsertCreditPointCCDetails(param).ToString());
            return CreditcardId;
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

        public void InsertCurrentCreditPoints(int NewUpdatedCredit, string CCIdentity, string confirmationId)
        {
            CreditPointTransaction objCreditTransaction = new CreditPointTransaction();
            objCreditTransaction.UserId = View.UserID;
            objCreditTransaction.NetCreditPoints = NewUpdatedCredit;
            objCreditTransaction.AmountPaid = int.Parse(View.AmountPaid.ToString());
            objCreditTransaction.CreditPackageId = View.PackageId;
            objCreditTransaction.PurchasedDate = System.DateTime.Now;
            objCreditTransaction.IsDeleted = false;
            objCreditTransaction.ModifiedDate = System.DateTime.Now;
            objCreditTransaction.CouponId = 0;
            objCreditTransaction.CreditCardId = int.Parse(CCIdentity);
            objCreditTransaction.CreatedDate = System.DateTime.Now;
            object[] param = { objCreditTransaction, confirmationId };
            //_controller.InsertPackageDetails(param);
            this.View.TransactionId = int.Parse(_controller.InsertCurrentCreditPoints(param).ToString());


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
                objpackage.EndDate = System.DateTime.Now.AddMonths(1);
                objpackage.AmountPaid = 0;
            }
            if (View.getPackageId == 2)
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
            if (View.getPackageId == 1)
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
            objTribute.TypeDescription = tributeType;
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

        public void GetCreditPointCount()
        {
            UserRegistration _objUserReg = new UserRegistration();
            Users objUsers = new Users();
            objUsers.UserId = View.UserID;
            _objUserReg.Users = objUsers;
            object[] param = { _objUserReg };
            _controller.GetCreditPointCount(param);
            UserRegistration objDetails = (UserRegistration)param[0];
            if (objDetails.CreditPointTransaction == null)
            {
                View.NetCreditPoints = 0;
            }
            else
            {
                View.NetCreditPoints = objDetails.CreditPointTransaction.NetCreditPoints;
            }
        }

        public void GetCreditCostMapping()
        {

            View.CreditCostMappingList = _controller.GetCreditCostMapping();
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
        #endregion METHODS

    }
}

