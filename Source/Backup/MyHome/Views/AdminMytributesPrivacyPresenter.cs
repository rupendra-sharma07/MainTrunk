///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.AdminMytributesPrivacyPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Admin My tributes Privacy Settings.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
//using TributesPortal.ResourceAccess;
//using System.Data;
using TributesPortal.MultipleLangSupport;


namespace TributesPortal.MyHome.Views
{
    public class AdminMytributesPrivacyPresenter : Presenter<IAdminMytributesPrivacy>
    {
        private MyHomeController _controller;

        public AdminMytributesPrivacyPresenter([CreateNew] MyHomeController controller)
        {
            _controller = controller;
        }

        public void getTributeDetails(int tributeId)
        {
            GetMyTributes objtribute = new GetMyTributes();
            objtribute.UserId = View.UserId;
            object[] param ={ objtribute, tributeId };
            if (objtribute.CustomError == null)
            {
                View.Mytribute = _controller.GetMyTribute(param, "Tribute");
            }
        }

        public void GetTributeAdministrators()
        {
            Tributes objtribute = new Tributes();
            objtribute.TributeId = View.TributeId;
            objtribute.TypeDescription = null;
            object[] param ={ objtribute };
            IList<UserInfo> _info = _controller.GetAdministrators(param);   //_controller.GetTributeAdministrators(param);
            if (_info.Count > 0)
            {
                if (_info.Count == 1)
                {
                    View.TributeOwner = _info[0].FirstName + " (Tribute Creator)";
                    View.TributeAdministrators = null;
                }
                else
                {
                    List<UserInfo> objList_ = new List<UserInfo>();
                    for (int i = 0; i < _info.Count; i++)
                    {
                        if (_info[i].IsOwner.Equals(true))
                        {
                            View.TributeOwner = _info[i].FirstName + " (Tribute Creator)";
                        }
                        else
                        {
                            UserInfo objloc = new UserInfo();
                            objloc.FirstName = _info[i].FirstName;
                            objloc.UserID = _info[i].UserID;
                            objList_.Add(objloc);
                        }
                    }
                    View.TributeAdministrators = objList_;
                }
            }
            //View.TributeAdministrators = _controller.GetTributeAdministrators(param);
        }

        //get all the tribute details to populate the tribute admin pages
        public void GetTributeByID()
        {
            TributesUserInfo _objTributeUserInfo = new TributesUserInfo();
            Tributes objTributes = new Tributes();

            //added for the Donation project
            Donation objDonation = new Donation();
            objDonation.TributeID = View.TributeId;
            object[] param ={ objDonation };

            objTributes.TributeId = View.TributeId;
            _objTributeUserInfo.Tributes = objTributes;
            _controller.GetTributeByID(_objTributeUserInfo);

            //fetvh donation info only if a donation box exists for a tribute
            if (_objTributeUserInfo.Tributes.IsDonation)
                _controller.GetDonationInfo(param);
            View.DonationCharity = objDonation;
            View.TributeName = _objTributeUserInfo.Tributes.TributeName;
            View.IsPrivate = _objTributeUserInfo.Tributes.IsPrivate;
            View.GoogleAdSense = _objTributeUserInfo.Tributes.GoogleAdSense;

        }

        public void DeleteTribute()
        {
            Tributes objTributes = new Tributes();
            objTributes.TributeId = View.TributeId;
            objTributes.UserTributeId = View.UserId;
            object[] param ={ objTributes };
            _controller.DeleteTribute(param);
        }

        public void UpdateTributeName()
        {
            Tributes objTributes = new Tributes();
            objTributes.TributeId = View.TributeId;
            objTributes.UserTributeId = View.UserId;
            objTributes.TributeName = View.TributeName;
            object[] param ={ objTributes };
            _controller.UpdateTributeName(param);
        }

        public void UpdateTributePrivacy()
        {
            Tributes objTributes = new Tributes();
            objTributes.TributeId = View.TributeId;
            objTributes.UserTributeId = View.UserId;
            objTributes.IsPrivate = View.IsPrivate;
            objTributes.GoogleAdSense = View.GoogleAdSense;
            object[] param ={ objTributes };
            _controller.UpdateTributePrivacy(param);
        }

        public void TriputePackageInfo()
        {
            TributePackage objpackage = new TributePackage();
            objpackage.UserTributeId = View.TributeId;
            object[] param ={ objpackage };
            this._controller.TriputePackageInfo(param);
            if (objpackage.CustomError == null)
            {
                View.IsAutomaticRenew = objpackage.IsAutomaticRenew;
                View.IsSponserHide = objpackage.IsSponserHide;
                View.IsSponsor = objpackage.IsSponsor;
                View.SponserId = objpackage.UserId;
                View.TributePackageId = objpackage.TributePackageId;
                View.PackageId = objpackage.PackageId;
            }

        }

        //public void TriputePackageInfo(int TributeId)
        //{
        //    TributePackage objpackage = new TributePackage();
        //    objpackage.UserTributeId = TributeId;
        //    object[] param ={ objpackage };
        //    this._controller.TriputePackageInfo(param);
        //    if (objpackage.CustomError == null)
        //    {
        //        View.PackageId = objpackage.PackageId;
        //        View.EndDate = objpackage.EndDate;
        //    }

        //}

        public void GetUserDetails()
        {
            UserRegistration _objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = View.SponserId;
            _objUserReg.Users = objUsers;
            _controller.GetUserDetails(_objUserReg);

            if (_objUserReg.Users != null)
            {
                if (_objUserReg.Users.UserId != 0 && _objUserReg.Users.FirstName == string.Empty && _objUserReg.Users.LastName == string.Empty && _objUserReg.Users.UserName != string.Empty)
                    View.Sponsorname = _objUserReg.Users.UserName;
                else if (_objUserReg.Users.UserId != 0 && _objUserReg.Users.FirstName != string.Empty && _objUserReg.Users.LastName != string.Empty)
                    View.Sponsorname = _objUserReg.Users.FirstName.ToString() + " " + _objUserReg.Users.LastName.ToString();
                else //if (_objUserReg.Users.FirstName != string.Empty && _objUserReg.Users.LastName != string.Empty && _objUserReg.Users.UserName == string.Empty)
                    View.Sponsorname = "An Anonymous User";
            }
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

        public void InsertCCDetails()
        {
            UserRegistration objUserReg = new UserRegistration();
            UserCreditcardDetails objCCdetail = new UserCreditcardDetails();
            objCCdetail.UserId = View.UserId;
            objCCdetail.CardholdersName = View.CardholdersName;
            objCCdetail.CreditCardType = View.PaymentMethod;
            objCCdetail.CreditCardNo = View.CreditCardNo;
            objCCdetail.ExpirationDate = View.ExpirationDate;
            objCCdetail.Address = View.Address;
            objCCdetail.City = View.SelectedCCCity;
            objCCdetail.Zip = View.ZipCode;
            if (View.SelectedCCState == -1)
            {
                objCCdetail.State = null;
            }
            else
            {
                objCCdetail.State = View.SelectedCCState;
            }
            objCCdetail.Country = int.Parse(View.SelectedCCCountry);
            objCCdetail.Telephone = View.Telephone;
            //objCCdetail.IsCardDetailsReusable = View.IsCardDetailsReusable;
            objCCdetail.NotifyBeforeRenew = View.NotifyBeforeRenew;
            if (View.NotifyBeforeRenew)
            {
                objCCdetail.IsCardDetailsReusable = true;
            }
            else
            {
                objCCdetail.IsCardDetailsReusable = View.IsCardDetailsReusable;
            }
            objCCdetail.CVC = View.CVC;
            objUserReg.UserCreditcardDetails = objCCdetail;
            object[] param ={ objUserReg };
            try
            {

                decimal CCIdentity = (System.Decimal)_controller.InsertCCDetails(param);
                InsertPackageDetails(CCIdentity.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateEmailAlerts(bool emailalert)
        {
            Tributes objTribute = new Tributes();
            objTribute.UserTributeId = View.UserId;
            objTribute.TributeId = View.TributeId;
            objTribute.IsActive = emailalert;
            object[] _tribute ={ objTribute };
            _controller.UpdateEmailAlerts(_tribute);
        }

        public void InsertPackageDetails(string CCIdentity)
        {
            TributePackage objpackage = new TributePackage();
            objpackage.UserId = View.UserId;
            objpackage.UserTributeId = View.TributeId;
            objpackage.PackageId = View.PackageId;
            if (View.PackageId == 3)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = System.DateTime.Now.AddMonths(1);
                objpackage.AmountPaid = 0;
            }
            if (View.PackageId == 2)
            {
                if (View.ACT == 1)
                {
                    objpackage.StartDate = System.DateTime.Now;
                    objpackage.EndDate = System.DateTime.Now.AddMonths(12);
                    View.NewExpiryDate = System.DateTime.Now.AddMonths(12).ToString("MM/dd/yyyy");
                }
                else
                {
                    objpackage.StartDate = View.NewStartedDate; ;
                    objpackage.EndDate = View.NewStartedDate.AddMonths(12);
                    View.NewExpiryDate = System.DateTime.Now.AddMonths(12).ToString("MM/dd/yyyy");
                }
                objpackage.AmountPaid = Convert.ToDecimal(WebConfig.OneyearAmount);

            }
            if (View.PackageId == 1)
            {
                // Discuss with Pooja
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = null;
                objpackage.AmountPaid = Convert.ToDecimal(WebConfig.LifeTimeAmount);
                View.NewExpiryDate = "Never";
            }
            //objpackage.IsAutomaticRenew = View.IsCardDetailsReusable;                   
            objpackage.IsAutomaticRenew = View.NotifyBeforeRenew;
            objpackage.CouponId = null;
            objpackage.IsSponsor = false;
            objpackage.IsSponserHide = false;
            objpackage.CreditCardId = int.Parse(CCIdentity);
            object[] param ={ objpackage };
            _controller.InsertPackageDetails(param);

        }

        //Delete tribute adminis
        public void DeleteTributeAdminis(string Adminlist)
        {
            TributeAdministrator objadmin = new TributeAdministrator();
            objadmin.UserTributeId = View.TributeId;
            object[] param ={ objadmin, Adminlist };
            _controller.DeleteTributeAdminis(param);
        }

        public void SendMailtoAdmins(string AdminEmailLists, string tributename, string TruibuteType, string UserName)
        {
            TributesPortal.BusinessLogic.EmailManager objEmailManager = new TributesPortal.BusinessLogic.EmailManager();
            objEmailManager.SendAdminMails(View.UserId, AdminEmailLists, View.TributeId.ToString(), tributename, TruibuteType, UserName);
        }

        public void UpdateAutoRenew()
        {
            TributePackage objtribute = new TributePackage();
            objtribute.IsAutomaticRenew = false;
            objtribute.TributePackageId = View.TributePackageId;
            object[] Params ={ objtribute };
            _controller.UpdateAutoRenew(Params);
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
        /// update the donation details of a tribute
        /// </summary>
        public void UpdateDonationDetails()
        {
            
                Donation objDonation = new Donation();
                objDonation = View.DonationCharity;
                object[] param ={ objDonation,View.IsDonation };
                _controller.UpdateDonationDetails(param);
            
        }
    }
}
