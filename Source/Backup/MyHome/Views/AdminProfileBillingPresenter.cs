///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.AdminProfileBillingPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Admin Profile Billing Settings.
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;

//using System.Data;
using TributesPortal.MultipleLangSupport;

namespace TributesPortal.MyHome.Views
{
    public class AdminProfileBillingPresenter : Presenter<IAdminProfileBilling>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
         private MyHomeController _controller;
         public AdminProfileBillingPresenter([CreateNew] MyHomeController controller)
         {
         		_controller = controller;
         }

        public override void OnViewLoaded()
        {
            // TODO: Implement code that will be executed every time the view loads
        }

        public override void OnViewInitialized()
        {
            // TODO: Implement code that will be executed the first time the view loads
        }

        public void GetMaymentModes()
        {
            View.PaymentModes = _controller.PaymentModes();
        }

        public void GetMaymentModes_()
        {
            View.PaymentModes_ = _controller.PaymentModes();
        }

        public void GetCCCountryList()
        {

            List<Locations> objList = new List<Locations>();
            List<Locations> objList_ = new List<Locations>();

            Locations objCount = new Locations();
            objCount.LocationParentId = 0;
            objList = (List<Locations>)_controller.CountryList(objCount);
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
            View.CCCountryList = objList_;
            //View.CCCountryList = _controller.CountryList(objCount);
        }

        public void OnStateLoad()
        {
            Locations location = new Locations();
            location.LocationParentId = int.Parse(View.SelectedCCCountry);
            View.CCStateList = _controller.CountryList(location);
        }
       
        public void OnBillingInformation()
        {
            UserRegistration _objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId =View.UserId;
            _objUserReg.Users = objUsers;
            object[] param ={ _objUserReg };

            IList<BillingHistory> objBilling = _controller.BillingHistory(param);
            foreach (BillingHistory obj in objBilling)
            {
                obj.AmountToDisplay = "$" + obj.AmountPaid.ToString() + "<br/>(View Receipt)";
            }

            View.BillingInformation = objBilling;
            //View.BillingInformation = _controller.BillingHistory(param);            
        }

        public void GetCreditCardDetails_()
        {
            UserRegistration _objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = View.UserId;
            _objUserReg.Users = objUsers;
            object[] param ={ _objUserReg };
            _controller.GetCreditCardDetails(param);
            //UserRegistration _objUserReg = (UserRegistration)param[0];
            if (_objUserReg.UserCreditcardDetails != null)
            {
                View.CreditCardId =_objUserReg.UserCreditcardDetails.CreditCardId;
                View.Telephone = _objUserReg.UserCreditcardDetails.Telephone.ToString();
                View.ZipCode = _objUserReg.UserCreditcardDetails.Zip.ToString();
                GetCCCountryList();                
                View.SelectedCCCountry = _objUserReg.UserCreditcardDetails.Country.ToString();
                if (_objUserReg.UserCreditcardDetails.State.ToString() != "")
                    View.SelectedCCState = int.Parse(_objUserReg.UserCreditcardDetails.State.ToString());
                else
                    View.SelectedCCState = -1;
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
                View.Visibility = 1;
            }
            else
            {
                View.BannerMessage = "You have not stored any credit card information";
                View.Visibility = 2;
            }

        }

        public void UpdateCCDetails()
        {
            UserCreditcardDetails onjCCDetails = new UserCreditcardDetails();
            onjCCDetails.CreditCardType = View.PaymentMethod;
            onjCCDetails.CreditCardNo = View.CreditCardNo;
            onjCCDetails.ExpirationDate = View.ExpirationDate;
            onjCCDetails.CardholdersName = View.CardholdersName;
            onjCCDetails.Address = View.Address;
            onjCCDetails.Country = int.Parse(View.SelectedCCCountry);
            if (View.SelectedCCState != -1)
                onjCCDetails.State = View.SelectedCCState;
            else
                onjCCDetails.State = null;
            onjCCDetails.City = View.SelectedCCCity;
            onjCCDetails.Zip = View.ZipCode;
            onjCCDetails.Telephone = View.Telephone;
            onjCCDetails.UserId = View.UserId;
            onjCCDetails.CreditCardId = View.CreditCardId;
            onjCCDetails.CVC = View.CVC;
            UserRegistration _objUserReg = new UserRegistration();
            _objUserReg.UserCreditcardDetails = onjCCDetails;
            object[] param ={ _objUserReg };
            _controller.UpdateCCDetails(param);
        }

        public void PaymentReceipt(int TributeId)
        {
            object[] param ={ TributeId };
            View.PaymentReceipt = _controller.PaymentReceipt(param);
        }

        /// <summary>
        /// Method to get the payment receipt based on the tribute package id
        /// </summary>
        /// <param name="TributePackageId">TributePackageId</param>
        public void GetPaymentReceipt(int TributePackageId)
        {
            object[] param ={ TributePackageId };
            View.PaymentReceipt = _controller.GetPaymentReceipt(param);
        }

        public void DeleteCreditCardDetails()
        {
            try
            {
                UserCreditcardDetails onjCCDetails = new UserCreditcardDetails();
                onjCCDetails.UserId = View.UserId;
                onjCCDetails.CreditCardId = View.CreditCardId;
                UserRegistration _objUserReg = new UserRegistration();
                _objUserReg.UserCreditcardDetails = onjCCDetails;
                object[] param ={ _objUserReg };
                _controller.DeleteCreditCardDetails(param);
                
             
            }
            catch
            { 
              //Do nothing.
            }
        }

        public void GetCCCountryList_()
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
            View.CCCountryList_ = objList_;
            GetMaymentModes_();
        }

        public void OnStateLoad_()
        {
            Locations location = new Locations();
            location.LocationParentId = int.Parse(View.SelectedCCCountry_);
            View.CCStateList_ = _controller.CountryList(location);
        }

        public void InsertCCDetails()
        {
            UserRegistration objUserReg = new UserRegistration();
            UserCreditcardDetails objCCdetail = new UserCreditcardDetails();
            objCCdetail.UserId = View.UserId;
            objCCdetail.CardholdersName = View.CardholdersName_;
            objCCdetail.CreditCardType = View.PaymentMethod_;
            objCCdetail.CreditCardNo = View.CreditCardNo_;
            objCCdetail.ExpirationDate = View.ExpirationDate_;
            objCCdetail.Address = View.Address_;
            objCCdetail.City = View.SelectedCCCity_;
            objCCdetail.Zip = View.ZipCode_;
            if (View.SelectedCCState_ == -1)
            {
                objCCdetail.State = null;
            }
            else
            {
                objCCdetail.State = View.SelectedCCState_;
            }
            objCCdetail.Country = int.Parse(View.SelectedCCCountry_);
            objCCdetail.Telephone = View.Telephone_;
            objCCdetail.NotifyBeforeRenew = View.NotifyBeforeRenew_;
            if (View.NotifyBeforeRenew_)
            {
                objCCdetail.IsCardDetailsReusable = true;
            }
            else
            {
                objCCdetail.IsCardDetailsReusable = View.IsCardDetailsReusable_;
            }            
            objCCdetail.CVC = View.CVC_;
            objUserReg.UserCreditcardDetails = objCCdetail;
            object[] param ={ objUserReg };
            try
            {

                decimal CCIdentity = (System.Decimal)_controller.InsertCCDetails(param);
             //   InsertPackageDetails(CCIdentity.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void InsertPackageDetails(string CCIdentity)
        {
            TributePackage objpackage = new TributePackage();
            objpackage.UserId = View.UserId;
            //objpackage.UserTributeId = View.TributeId;
            objpackage.PackageId = View.PackageId_;
            if (View.PackageId_ == 3)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = System.DateTime.Now.AddMonths(1);
                objpackage.AmountPaid = 0;
            }
            if (View.PackageId_ == 2)
            {   
                  objpackage.StartDate = System.DateTime.Now;
                  objpackage.EndDate = System.DateTime.Now.AddMonths(12);
                  objpackage.AmountPaid = 20;
            }
            if (View.PackageId_ == 1)
            {
                // Discuss with Pooja
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = null;
                objpackage.AmountPaid = 50;
            }
            //objpackage.IsAutomaticRenew = View.IsCardDetailsReusable;                   
            objpackage.IsAutomaticRenew = View.NotifyBeforeRenew_;
            objpackage.CouponId = null;
            objpackage.IsSponsor = false;
            objpackage.IsSponserHide = false;
            objpackage.CreditCardId = int.Parse(CCIdentity);
            object[] param ={ objpackage };
       //     _controller.InsertPackageDetails(param);

        }

        public int GetCouponAvailable(string CouponCode, int couponType)
        {
            CouponsAvailable objavab = new CouponsAvailable();
            objavab.CouponCode = CouponCode;
            Coupons objcou = new Coupons();
            objcou.Couponsavailable = objavab;
            object[] objParam ={ objcou };
            int couponavail = _controller.GetCouponAvailable(objParam, couponType);
            if (couponavail == 1)
            {
                Couponmaster objmas = objcou.CouponMaster;
                View.IsPercentage = bool.Parse(objmas.IsPercentage.ToString());
                View.Denomination = objmas.CouponDenomination.ToString();
            }
            return couponavail;
        }

        /// <summary>
        /// Method to get tribute details based on the selected tribute id
        /// </summary>
        /// <param name="tributeId">TributeId</param>
        /// <returns>Tribute entity containing details of tribute.</returns>
        public Tributes GetTributeDetails(int tributeId)
        {
            return _controller.GetTributeDetails(tributeId).Tributes;
        }
     
    }
}




