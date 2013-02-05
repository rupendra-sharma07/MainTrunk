///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.TributeHomePagePresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for displaying the tribute home page.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;
using System.Configuration;

namespace TributesPortal.Tribute.Views
{
    public class TributeHomePagePresenter : Presenter<ITributeHomePage>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
         private TributeController _controller;
         public TributeHomePagePresenter([CreateNew] TributeController controller)
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

        public void GetTributeAdminis()
        {
            TributeAdministrator objCount = new TributeAdministrator();
            objCount.UserTributeId = View.TributeId;  
            object[] param ={ objCount };
            IList<UserInfo> userinfo = _controller.GetTributeAdminis(param);
            if (userinfo.Count > 0)
            {
                List<UserInfo> objuserinfo = new List<UserInfo>();
                for (int i = 0; i < userinfo.Count; i++)
                {
                    if (userinfo[i].IsOwner == true)
                    {
                        View.AdminOwnerId = userinfo[i].UserID.ToString();
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
                       // objUser.LastName = userinfo[i].LastName;
                        objUser.UserEmail = userinfo[i].UserEmail;
                        objuserinfo.Add(objUser);                       
                    }
                }
                View.OtherAdmins = objuserinfo;
            
            }
            GetTributeByID(View.TributeId);
            
            
        }
        public void GetTributeByID(int TributeId)
        {
            TributesUserInfo _objTributeUserInfo = new TributesUserInfo();
            Tributes objTributes = new Tributes();
            objTributes.TributeId = TributeId;
            _objTributeUserInfo.Tributes = objTributes;
            _controller.GetTributeByID(_objTributeUserInfo);
            GetTributeTypesbyTypeCode(int.Parse(_objTributeUserInfo.Tributes.TributeType.ToString()));
            View.TributeName = _objTributeUserInfo.Tributes.TributeName;
            View.TributeMessage = _objTributeUserInfo.Tributes.WelcomeMessage;          
            View.Date1 = _objTributeUserInfo.Tributes.Date1;
            View.Date2 = _objTributeUserInfo.Tributes.Date2;
            
            View.ObPostMessage=_objTributeUserInfo.Tributes.PostMessage;
            View.ObMessageWithoutHtml = _objTributeUserInfo.Tributes.MessageWithoutHtml;

            View.TributeImage=_objTributeUserInfo.Tributes.TributeImage;
            if (string.IsNullOrEmpty(_objTributeUserInfo.Tributes.City))
            View.City =GetCountryState(_objTributeUserInfo.Tributes.Country, _objTributeUserInfo.Tributes.State);
            else
            View.City = _objTributeUserInfo.Tributes.City + ", " + GetCountryState(_objTributeUserInfo.Tributes.Country, _objTributeUserInfo.Tributes.State);
            
            TriputePackageInfo(TributeId);
            GetLastNoteForTribute(TributeId);
            //GetStoryDetail();
            //GetTodayLatest(TributeId);
            ////GetTributeCount(TributeId);
            //GetYesterdayLatest(TributeId);
            //GetThirdLatest(TributeId);
            //GetExistingTheme(TributeId);
           // GetTributeTypebyCode(TributeId);            

        }
        //public void GetTributeTypebyCode(int TributeId)
        //{
        //    Tributes objTributes = new Tributes();
        //    objTributes.TributeId = TributeId;
        //    object[] objParam ={ objTributes };
        //    _controller.GetTributeTypebyCode(objParam);
            //View.TypeDescription = objTributes.TypeDescription;
        //}
        public void GetTributeCount()
        {
            TributeVisitCount objcount = new TributeVisitCount();
            objcount.SectionTypeCodeId = 5;
            objcount.SectionTypeID = View.TributeId;
            object[] param ={ objcount };
            this._controller.GetTributeCount(param);
            if (objcount.CustomError == null)
            {
                View.VisitCount = objcount.Count; 
            }
        }
        public void GetTodayLatest(int tributeid)
        {
            TributeLatest objLatest = new TributeLatest();
            objLatest.TributeId = tributeid;
            objLatest.CreationDate = DateTime.Now;
            objLatest.SecondDate = DateTime.Now;
            object[] param ={ objLatest };
            //GetPhotosDatewise(DateTime.Now, DateTime.Now);
            View.TodayVideos = this._controller.GetLatestVideos(param);
        }
        public void GetYesterdayLatest(int tributeid)
        {
            TributeLatest objLatest = new TributeLatest();
            objLatest.TributeId = tributeid;
            objLatest.CreationDate = DateTime.Now.AddDays(-1); ;
            objLatest.SecondDate = DateTime.Now.AddDays(-1);            
            object[] param ={ objLatest };            
            View.YesterdayLatest = this._controller.GetLatestVideos(param);
        }
        public void GetThirdLatest(int tributeid)
        {
            TributeLatest objLatest = new TributeLatest();
            objLatest.TributeId = tributeid;
            objLatest.CreationDate = DateTime.Now.AddDays(-2); 
            objLatest.SecondDate = DateTime.Now.AddMonths(-1);
            object[] param ={ objLatest };
            View.ThirdLatest = this._controller.GetLatestVideos(param);
        }

        /// <summary>
        /// LHK: get all latest date to traverse get latest activities
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public List<DateTime> GetAllLatestDates()
        {
            return this._controller.GetAllLatestDates(this.View.TributeId);
        }

        public IList<TributeLatest> GetAllLatest(DateTime date)
        {
            TributeLatest objLatest = new TributeLatest();
            objLatest.TributeId = View.TributeId;
            objLatest.CreationDate = date;
            objLatest.SecondDate = date;
            object[] param ={ objLatest };
            return this._controller.GetLatestVideos(param);
        }
        
        public void TriputePackageInfo(int TributeId)
        {
            TributePackage objpackage = new TributePackage();
            objpackage.UserTributeId = TributeId;
            object[] param ={ objpackage };
            this._controller.TriputePackageInfo(param);
            if (objpackage.CustomError == null)
            {
                View.PackageId = objpackage.PackageId;
                //View.NotifyBeforeRenew = objpackage.IsAutomaticRenew;
                View.EndDate = objpackage.EndDate;
            }
        }

        public void GetLastNoteForTribute(int TributeId)
        {
            Note objNote = new Note();
            objNote.UserTributeId = TributeId;
            object[] param ={ objNote };
            this._controller.GetLastNoteForTribute(param);
            if (objNote.Title != null)
            {
                View.NotePostMessage = objNote.MessageWithoutHtml;  //objNote.PostMessage;
                View.NoteCreatedDate = objNote.CreatedDate;
                View.NoteTitle = objNote.Title;
                View.NotesId = objNote.NotesId.ToString();
            }
        }
        public string GetCountryState(int country,Nullable<int> state)
        {
            string location=string.Empty;
            object[] param ={ country,state};
            IList<Locations> list = _controller.GetCountryState(param);
            if (list.Count > 0)
            {
                if (list.Count == 1)
                    location = list[0].LocationName;
                else
                    location = list[1].LocationName + ", " + list[0].LocationName;
            }           

        return location;
        }
        public void GetTributeTypesbyTypeCode(int typecode)
        {
            List<ParameterTypesCodes> list = _controller.GetTributeTypesbyTypeCode(typecode);
            if (list.Count > 0)
            {
                View.TributeType = list[0].TypeDescription;
                View.TypeDescription = list[0].TypeDescription;
            }
        }

        public void UpdateTributeMessage(string message)
        {
            Tributes objtiibutes = new Tributes();
            objtiibutes.TributeId = View.TributeId;
            objtiibutes.ModifiedBy = View.UserID;
            objtiibutes.WelcomeMessage = message;
            object[] param ={ objtiibutes };
            _controller.UpdateTributeMessage(param);
        }

        public void GetStoryDetail()
        {
            try
            {
                View.isStory = true;
                Stories objStory = new Stories();
                objStory.TributeId = View.TributeId;
                objStory.UserId = View.UserID;

                Stories objStoryList = _controller.GetStoryDetail(objStory);

                if (objStoryList != null)
                {
                    if (objStoryList.MoreAboutSection != null)
                    {
                        foreach (StoryMoreAbout moreabout in objStoryList.MoreAboutSection)
                        {
                            if (moreabout.PrimaryTitle == Stories.StorySectionEnum.Story.ToString())
                            {
                                View.isStory = false;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IsStoryAdded()
        {
            UserAdminOwnerInfo objadmin = new UserAdminOwnerInfo();
            objadmin.UserId = View.UserID;
            objadmin.TributeId = View.TributeId;
            object[] objUserAdmin ={ objadmin };
            View.IsStoryAdded = _controller.IsStoryAdded(objUserAdmin);
        }


        public void IsStoryHide()
        {
            View.IsStoryHide = _controller.IsStoryHide(View.TributeId);
            

        }
        public void SetStoryVisibility()
        {
            _controller.SetStoryVisibility(View.TributeId);            
        }

        public void SendMail()
        {
            _controller.SendMail(View.UserID, View.ToUserId, View.Subject, View.PostMessage);
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
            //View.CCCountryList = objList_;             
            //View.CCCountryList = _controller.CountryList(objCount);
        }
        public void GetCCStateList()
        {
            //Locations objCount = new Locations();
            //objCount.LocationParentId = int.Parse(View.SelectedCCCountry);
            //View.CCStateList = _controller.StateList(objCount);
        }

        public void GetMaymentModes()
        {
            //View.PaymentModes = _controller.PaymentModes();
        }

        private void InsertPackageDetails(int Identity)
        {
            TributePackage objpackage = new TributePackage();
            objpackage.UserId = View.UserID;
            objpackage.UserTributeId = int.Parse(Identity.ToString());
            //objpackage.PackageId = View.getPackageId;
            if (View.PackageId == 8)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = System.DateTime.Now.AddMonths(1);
                objpackage.AmountPaid = 0;
            }
            else if (View.PackageId == 7)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = System.DateTime.Now.AddMonths(12);
                objpackage.AmountPaid = Convert.ToDecimal(TributesPortal.Utilities.WebConfig.PhotoOneyearAmount);
            }
            else if (View.PackageId == 6)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = null;
                objpackage.AmountPaid = Convert.ToDecimal(TributesPortal.Utilities.WebConfig.PhotoLifeTimeAmount);
            }
            else if (View.PackageId == 5)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = System.DateTime.Now.AddMonths(12);
                objpackage.AmountPaid = Convert.ToDecimal(TributesPortal.Utilities.WebConfig.TributeOneyearAmount);
            }
            else if (View.PackageId == 4)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = null;
                objpackage.AmountPaid = Convert.ToDecimal(TributesPortal.Utilities.WebConfig.TributeLifeTimeAmount);
            }
            else if (View.PackageId == 3)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = System.DateTime.Now.AddMonths(1);
                objpackage.AmountPaid = 0;
            }
            else if (View.PackageId == 2)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = System.DateTime.Now.AddMonths(12);
                objpackage.AmountPaid = Convert.ToDecimal(TributesPortal.Utilities.WebConfig.OneyearAmount);
            }
            else if (View.PackageId == 1)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = null;
                objpackage.AmountPaid = Convert.ToDecimal(TributesPortal.Utilities.WebConfig.LifeTimeAmount);
            }
            //objpackage.IsAutomaticRenew = View.IsCardDetailsReusable;
            //objpackage.CouponId = null;
            //objpackage.IsSponsor = true;
            //objpackage.IsSponserHide =View.IsSponserHide;
            object[] param ={ objpackage };
            _controller.InsertPackageDetails(param);
        }
        
        public void InsertCCDetails()
        {
            //UserRegistration objUserReg = new UserRegistration();
            //UserCreditcardDetails objCCdetail = new UserCreditcardDetails();
            //objCCdetail.UserId = View.UserID;
            //objCCdetail.CardholdersName = View.CardholdersName;
            //objCCdetail.CreditCardType = View.PaymentMethod;
            //objCCdetail.CreditCardNo = View.CreditCardNo;
            //objCCdetail.ExpirationDate = View.ExpirationDate;
            //objCCdetail.Address = View.Address;
            //objCCdetail.City = View.SelectedCCCity;
            //objCCdetail.Zip = View.ZipCode;
            //if (string.IsNullOrEmpty(View.SelectedCCState))
            //{
            //    objCCdetail.State = null;
            //}
            //else
            //{
            //    objCCdetail.State = int.Parse(View.SelectedCCState);
            //}
            //objCCdetail.Country = int.Parse(View.SelectedCCCountry);
            //objCCdetail.Telephone = View.Telephone;
            //objCCdetail.IsCardDetailsReusable = View.IsCardDetailsReusable;
            //objCCdetail.NotifyBeforeRenew = View.NotifyBeforeRenew;
            //objUserReg.UserCreditcardDetails = objCCdetail;
            //object[] param ={ objUserReg };
            //_controller.InsertCCDetails(param);
            //InsertPackageDetails(View.TributeId);
        }

        public void CheckAndSendPassword(GenralUserInfo _objGenralUserInfo, bool _Reset)
        {
            _controller.CheckAndSendPassword(_objGenralUserInfo, _Reset);
        }

        public void GetPhotosDatewise(int AlbumID)
        {
            Photos objPhoto = new Photos();
            objPhoto.PhotoAlbumId = AlbumID;          
            objPhoto.PageSize = 8;
            objPhoto.PageNumber = 1;            
            View.TodayAlbumPhotos = _controller.GetPhotosDateWise(objPhoto);
        }

        public void AddTributeCount()
        {
            this._controller.AddTributeCount(View.TributeId);
        }

        public void GetTributeSession(Tributes objtribute)
        {            
            this._controller.GetTributeSession(objtribute);
        }

        public int GetCouponAvailable(string CouponCode,int couponType)
        {
            CouponsAvailable objavab = new CouponsAvailable();
            objavab.CouponCode = CouponCode;
            Coupons objcou=new Coupons();
            objcou.Couponsavailable=objavab;
            object[] objParam ={ objcou };
            int couponavail = _controller.GetCouponAvailable(objParam, couponType);
            if (couponavail == 1)
            {
                Couponmaster objmas = objcou.CouponMaster;
                //View.IsPercentage = bool.Parse(objmas.IsPercentage.ToString());
                //View.Denomination = objmas.CouponDenomination.ToString();
            }
            return couponavail;
        }

        /// <summary>
        /// Method to get the theme for tribute
        /// </summary>
        public string GetExistingTheme(int _tributeId)
        {

            Tributes objTribute = new Tributes();           
            objTribute.TributeId = _tributeId;
            Templates objtaml= _controller.GetThemeForTribute(objTribute);
            return objtaml.ThemeValue;

            //return _controller.GetThemeForTribute(objTribute).TemplateID;

        }

        /// <summary>
        /// Method to get the theme for tribute
        /// </summary>
        public string GetExistingFolderName(int _tributeId)
        {

            Tributes objTribute = new Tributes();
            objTribute.TributeId = _tributeId;
            Templates objtaml = _controller.GetExistingFolderName(objTribute);
            return objtaml.FolderName;            

        }


        /// <summary>
        /// Method to get Tribute Details for session based on Tribute Url and TributeType.
        /// </summary>
        /// <param name="tributeUrl">Tribute Url</param>
        /// <param name="tributeType">Tribute type description</param>
        public void GetTributeSessionForUrlAndType(string tributeUrl, string tributeType, string ApplicationType)
        {
            Tributes objTribute = new Tributes();
            objTribute.TributeUrl = tributeUrl;
            objTribute.TypeDescription = tributeType;
            this.View.GetTributeSession = _controller.GetTributeSessionForUrlAndType(objTribute, ApplicationType);
        }
        public void GetTributeSessionForTributeId(int _trbId)
        {
            Tributes objTribute = new Tributes();
            objTribute.TributeId = _trbId;
            this.View.GetTributeSession = _controller.GetTributeSessionForId(objTribute);
        }

        public string GetTributeUrlOnTributeId(int _TribureId)
        {
            return _controller.GetTributeUrlOnTributeId(_TribureId);
           
        }

        /// <summary>
        /// Method to get the information about Donation Box
        /// </summary>
        public void GetDonationDetails()
        {
            Donation objDonation = new Donation();
            objDonation.TributeID = View.TributeId;
            object[] param ={ objDonation };
            this._controller.GetDonationDetails(param);
            if (objDonation.CustomError == null)
            {
                View.DonationCharity = objDonation;
            }

           // throw new Exception("The method or operation is not implemented.");
        }

        //Start - Modification on 7-Dec-09 for the enhancement 5 of the Phase 1
        /// <summary>
        /// Method to set the "Tribute provided by: " or "Tribute created by: " text depending on the type of user
        /// If the user is a Business user the text will be "Tribute provided by: "
        /// and if the user is a normal user the text will be "Tribute created by: "
        /// </summary>
        public void SetTributeCreatedByOrProvidedBy()
        {
            UserRegistration _objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            //objUsers.UserId = View.UserID;
            if (!Equals(View.UserID, 0))
            {
                objUsers.UserId = View.UserID;
            }
            else
            {
                objUsers.UserId = _controller.GetUserIdByTributeId(View.TributeId);
            }
            _objUserReg.Users = objUsers;
            _controller.GetUserDetails(_objUserReg);

            if (_objUserReg.Users != null)
            {
                if (_objUserReg.UserBusiness != null)
                    View.TributeCreatedByOrProvidedBy = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()+" provided by: ";
                else
                    View.TributeCreatedByOrProvidedBy = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()+" created by: ";
            }
        }
        //End

        //Start - Modification on 17-Dec-09 for the enhancement 6 of the Phase 1
        /// <summary>
        /// Method to set the company's logo if the user is a business user
        /// </summary>
        public void SetCompanyLogo()
        {
            UserRegistration _objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
               
            objUsers.UserId = _controller.GetUserIdByTributeId(View.TributeId);           

            _objUserReg.Users = objUsers;
            _controller.GetUserDetails(_objUserReg);

            View.CompanyLogo = string.Empty;
            if (_objUserReg.Users != null)
            {
                if (_objUserReg.UserBusiness != null)
                    View.CompanyLogo = (_objUserReg.UserBusiness.CompanyLogo != null) ? _objUserReg.UserBusiness.CompanyLogo : string.Empty;
            }
        }

        public Tributes GetTributeUrlOnOldTributeUrl(Tributes objTrb, string ApplicationType)
        {
            return _controller.GetTributeUrlOnOldTributeUrl(objTrb, ApplicationType);
        }
        public void OnSaveComments(Comments objComment)
        {
            _controller.SaveComment(objComment);
        }

        public void GetWelcomeMessage()
        {
            TributesUserInfo _objTributeUserInfo = new TributesUserInfo();
            Tributes objTributes = new Tributes();
            objTributes.TributeId = this.View.TributeId;
            _objTributeUserInfo.Tributes = objTributes;
            _controller.GetTributeByID(_objTributeUserInfo);
            //GetTributeTypesbyTypeCode(int.Parse(_objTributeUserInfo.Tributes.TributeType.ToString()));
            //View.TributeName = _objTributeUserInfo.Tributes.TributeName;
            View.TributeMessage = _objTributeUserInfo.Tributes.WelcomeMessage;          
        }

        public bool GetIsMobileViewOn(Tributes oTribute)
        {
            return _controller.GetIsMobileViewOn(oTribute);
        }
    }//End 
}




