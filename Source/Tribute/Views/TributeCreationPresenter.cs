///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.TributeCreationPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for creating a tribute.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;
using TributesPortal.Utilities;

using System.Text.RegularExpressions;
using System.Configuration;
using SWFToImage;
namespace TributesPortal.Tribute.Views
{
    public class TributeCreationPresenter : Presenter<ITributeCreation>
    {
        private TributeController _controller;
        System.Decimal Identity;
        System.Decimal CCIdentity = 0;
        public TributeCreationPresenter([CreateNew] TributeController controller)
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
            //object[] param ={ View.TransactionId };
            //this.View.TransactionDetails = _controller.GetTransactionDetails(param);
            object[] param = { View.TributePackageId };
            //_controller.GetTransactionDetails(param);
            _controller.GetTransactionDetailsForEmail(View.PackageId, View.TransactionId, param);
        }

        public void SendSponsorTransactionEmail(string confirmationId)
        {
            object[] param = { View.TributePackageId };
            _controller.GetTransactionDetailsForEmail(View.PackageId, confirmationId, param);
        }

        public void GetTributeLists(string applicationType)
        {
            View.TributeTypes = _controller.GetListofTributes(applicationType);
        }
        public void GetTributeThemes()
        {
            View.ThemeNames = _controller.GetThemes(View.SelectedTheme);
        }

        public void GetTributeThemes_(string themename)
        {
            View.ThemeNames_ = _controller.GetThemes(themename);
        }
        public void GetThemesForCategory_(string themename, string categoryName, string applicationType)
        {
            View.ThemeNames_ = _controller.GetThemesForCategory(themename, categoryName, applicationType);
        }

        public void GetTributeThemesforEdit()
        {
            View.ThemeNames = _controller.GetThemes(View.EditTheme);
        }

        public void GetCountryList()
        {
            List<Locations> objList = new List<Locations>();
            List<Locations> objList_ = new List<Locations>();

            Locations objCount = new Locations();
            objCount.LocationParentId = 0;
            objList = (List<Locations>)_controller.CountryList(objCount);

            foreach (Locations obj in objList)
            {
                if (obj.LocationName == "Canada")
                    objList_.Insert(0, obj);
                else if (obj.LocationName == "United States")
                    objList_.Insert(0, obj);
                else
                    objList_.Add(obj);
            }

            /*if (objList.Count > 0)
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
                    if (objloc.LocationName != "United States" && objloc.LocationName != "Canada")
                        objList_.Add(objloc);
                }
            }*/
            View.CountryList = objList_;

            //Locations objCount = new Locations();
            //objCount.LocationParentId = 0;
            //View.CountryList = _controller.CountryList(objCount);
        }
        public void GetStateList()
        {
            Locations objCount = new Locations();
            objCount.LocationParentId = int.Parse(View.SelectedCountry);
            View.StateList = _controller.StateList(objCount);
        }
        public void GetCCCountryList()
        {
            List<Locations> objList = new List<Locations>();
            List<Locations> objList_ = new List<Locations>();

            Locations objCount = new Locations();
            objCount.LocationParentId = 0;
            objList = (List<Locations>)_controller.CountryList(objCount);

            foreach (Locations obj in objList)
            {
                if (obj.LocationName == "Canada")
                    objList_.Insert(0, obj);
                else if (obj.LocationName == "United States")
                    objList_.Insert(0, obj);
                else
                    objList_.Add(obj);
            }

            /*if (objList.Count > 0)
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
                        if (objloc.LocationName != "United States" && objloc.LocationName != "Canada")
                            objList_.Add(objloc);                    
                }                
            }*/
            View.CCCountryList = objList_;

            // View.CCCountryList = _controller.CountryList(objCount);
        }
        public void GetCCStateList()
        {
            Locations objCount = new Locations();
            objCount.LocationParentId = int.Parse(View.SelectedCCCountry);
            View.CCStateList = _controller.StateList(objCount);
        }

        /// <summary>
        /// Populate the List of countries for donation box
        /// </summary>
        public void GetDonationCountryList()
        {
            List<Locations> objList = new List<Locations>();
            List<Locations> objList_ = new List<Locations>();

            Locations objCount = new Locations();
            objCount.LocationParentId = 0;
            objList = (List<Locations>)_controller.CountryList(objCount);

            foreach (Locations obj in objList)
            {
                if (obj.LocationName == "Canada")
                    objList_.Insert(0, obj);
                else if (obj.LocationName == "United States")
                    objList_.Insert(0, obj);
                else
                    objList_.Add(obj);
            }

            //Set the drop down with the countries list
            View.DonationCountryList = objList_;
        }

        /// <summary>
        /// Populate the List of states for donation box
        /// </summary>
        public void GetDonationStateList()
        {
            Locations objCount = new Locations();
            objCount.LocationParentId = int.Parse(View.SelectedDonationCountry);

            List<Locations> objList = new List<Locations>();
            objList = (List<Locations>)_controller.StateList(objCount);

            //Set the "Select" as default selected state
            Locations objDefault = new Locations();
            objDefault.LocationId = 0;
            objDefault.LocationName = "";
            objList.Insert(0, objDefault);

            //Set the drop down with the states list
            View.DonationStateList = objList;
        }

        public void GetMaymentModes()
        {
            View.PaymentModes = _controller.PaymentModes();
        }
        private int GetTributeIdCode(string TributeName)
        {
            int count = _controller.GetTributeIdCode(TributeName);
            return count;
        }



        public string SetAccountEmailPassword(string txtEmail)
        {
            GenralUserInfo objGenralUserInfo = new GenralUserInfo();
            UserInfo objUserInfo = new UserInfo();
            objUserInfo.UserID = View.UserId;
            objUserInfo.UserEmail = txtEmail.Trim();
            objUserInfo.UserPassword = RandomPassword.Generate(8, 10);
            objGenralUserInfo.RecentUsers = objUserInfo;

            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);

            if (!(string.IsNullOrEmpty(objUserInfo.UserEmail) && string.IsNullOrEmpty(objUserInfo.UserPassword)))
                _controller.SetAccountEmailPassword(objGenralUserInfo);
            if (objGenralUserInfo.CustomError == null)
            {
                objSessionvalue.UserEmail = objGenralUserInfo.RecentUsers.UserEmail;
                stateManager.Add("objSessionvalue", objSessionvalue, StateManager.State.Session);
                return "Successfully saved email within your account's data.";
            }
            else
            {
                return "There was problem with setting your account's email.";
            }
        }

        public object CreateTribute()
        {
            string userName = string.Empty;
            Tributes tributes = new Tributes();
            tributes.UserTributeId = View.UserId;
            tributes.TributeName = View.TributeFor;
            //YT enhancement phase 1
            tributes.TributeFirstName = View.TributeFirstName;
            tributes.TributeLastName = View.TributeLastName;
            //tributes.TributeType = int.Parse(View.TributeType);
            tributes.TributeType = GetTributeIdCode(View.TributeType);
            if (tributes.TributeType > 0)
            {
                tributes.WelcomeMessage = View.WelcomeMsg;
                tributes.TributeImage = View.TributeImage;
                tributes.TributeUrl = View.TributeUrl.Trim();
                tributes.ThemeId = int.Parse(View.EditTheme);
                tributes.City = View.SelectedCity;
                if (string.IsNullOrEmpty(View.SelectedState))
                {
                    tributes.State = null;
                }
                else
                {
                    tributes.State = int.Parse(View.SelectedState);
                }
                tributes.Country = int.Parse(View.SelectedCountry);
                tributes.IsPrivate = View.IsPrivate;
                tributes.IsOrderDVDChecked = View.IsOrderDVDChecked;
                tributes.IsMemTributeBoxChecked = View.IsMemTributeBoxChecked;
                tributes.Date1 = View.date1;
                tributes.Date2 = View.date2;

                tributes.PostMessage = View.ObPostMessage.ToString();
                tributes.MessageWithoutHtml = View.ObMessageWithoutHtml.ToString();

                Identity = (System.Decimal)_controller.CreateTribute(tributes);
                StateManager stateManager = StateManager.Instance;
                SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
                if (objSessionvalue != null)
                {
                    if (objSessionvalue.UserType == 1)
                    {
                        if (objSessionvalue.FirstName == string.Empty)
                            userName = objSessionvalue.UserName;
                        else
                            userName = objSessionvalue.FirstName + " " + objSessionvalue.LastName;
                    }
                    else
                    {
                        userName = objSessionvalue.FirstName;
                    }
                }
                SentMailtoOwner(Identity.ToString(), View.TributeFor, View.PackageId, View.TributeType, View.TributeUrl, View.NotifyBeforeRenew, userName);
                SendMailtoAdmins(Identity.ToString(), View.TributeFor, View.TributeType, View.TributeUrl, userName);
                // InsertPackageDetails(Identity.ToString());
            } 
            return Identity;
        }

        /// <summary>
        /// Create the Donation Service for the Tribute
        /// </summary>
        public void CreateDonation(int TributeId)
        {
            if (View.IsDonationActive)
            {
                //Set the donation object
                Donation objDonation = new Donation();
                objDonation.TributeID = TributeId;
                objDonation.TributeName = View.TributeFor.Trim();
                objDonation.TributeType = View.TributeType.Trim().ToLower();
                objDonation.TributeUrl = View.TributeUrl.Trim();
                //objDonation.CreatorMail = 
                objDonation.CharityName = View.CharityName;
                objDonation.CharityCountry = View.SelectedDonationCountryText;
                objDonation.CharityState = View.SelectedDonationState;
                objDonation.CharityCity = View.DonationCity;
                objDonation.CharityAddress = View.DonationAddress;
                objDonation.DonationNotifyMail = View.DonationEmail;
                objDonation.DonationUrl = WebConfig.DonationURL.Trim() + View.TributeType.Replace("New Baby", "newbaby").Trim().ToLower() + "/" + View.TributeUrl.Trim() + "/";

                //Get the Users' details from the session
                StateManager stateManager = StateManager.Instance;
                SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
                if (objSessionvalue != null)
                {
                    objDonation.CreatorMail = objSessionvalue.UserEmail;
                    objDonation.CreatedBy = objSessionvalue.UserId;
                }

                //Save the Donation object
                _controller.CreateDonation(objDonation);
            }
        }

        public void SentMailtoOwner(string Identitiy, string TributeName, int Packageid, string TributeType, string TributeUrl, bool Notifyme, string userName)
        {
            TributesPortal.BusinessLogic.EmailManager objEmailManager = new TributesPortal.BusinessLogic.EmailManager();
            objEmailManager.SendOwnerMails(View.UserMail, Identitiy, TributeName, Packageid, TributeType, TributeUrl, Notifyme, userName);
        }
        private void SendMailtoAdmins(string Identitiy, string TributeName, string TributeType, string TributeUrl, string userName)
        {
            ArrayList AdminMails = View.AdminEmailLists;
            string _OtherMails = string.Empty;
            if (AdminMails.Count > 0)
            {
                for (int count = 0; count < AdminMails.Count; count++)
                {
                    if (_OtherMails.Length == 0)
                    {
                        _OtherMails = AdminMails[count].ToString();
                    }
                    else
                    {
                        _OtherMails = _OtherMails + "," + AdminMails[count].ToString();
                    }
                }
                TributesPortal.BusinessLogic.EmailManager objEmailManager = new TributesPortal.BusinessLogic.EmailManager();
                objEmailManager.SendAdminMails(View.UserMail, _OtherMails, Identitiy, TributeName, TributeType, TributeUrl, userName);
            }
        }

        public Decimal InsertCCDetails(SessionValue objSessionValue, Tributes objTribute)
        {
            UserRegistration objUserReg = new UserRegistration();
            UserCreditcardDetails objCCdetail = new UserCreditcardDetails();
            //Tributes objTributes = new Tributes();
            //SessionValue objSessionValue = new SessionValue();
            TributePackage objpackage = new TributePackage();
            objpackage.PackageId = View.PackageId;
            objpackage.EndDate = View.EndDate;
            objCCdetail.UserId = View.UserId;
            objCCdetail.CardholdersName = View.CardholdersName;
            objCCdetail.CreditCardType = View.PaymentMethod;
            objCCdetail.CreditCardNo = View.CreditCardNo;
            objCCdetail.ExpirationDate = View.ExpirationDate;
            objCCdetail.Address = View.Address;
            objCCdetail.City = View.SelectedCCCity;
            objCCdetail.Zip = View.ZipCode.Trim();
            //use this to store the mail id of the person who is paying for the tribute
            objCCdetail.SponsorEmailAddress = objSessionValue.UserEmail;// sponsorMail; 
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
            objCCdetail.NotifyBeforeRenew = View.NotifyBeforeRenew;
            //objCCdetail.SponsorEmailAddress = View.SponsorEmailAddress;
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
            string[] NameAndMsg = new string[2];
            object[] param = { objUserReg, objSessionValue, objTribute, objpackage, NameAndMsg };
            try
            {

                CCIdentity = (System.Decimal)_controller.InsertCCDetails(param);
                return CCIdentity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return CCIdentity;
        }

        public void UpdateUsedCouponDetails(string couponCode)
        {
            CouponsAvailable coupon = new CouponsAvailable();
            coupon.CouponCode = couponCode;
            _controller.UpdateUsedCouponDetail(couponCode);
        }
        //LHK:package details
        public void InsertPackageDetails(string Identity, string CCIdentity, string confirmationId)
        {
            TributePackage objpackage = new TributePackage();
            StateManager statemail = StateManager.Instance;
            SessionValue objSessionmail = (SessionValue)statemail.Get("objSessionvalue", StateManager.State.Session);
            objpackage.UserId = View.UserId;
            objpackage.UserTributeId = int.Parse(Identity.ToString());
            objpackage.PackageId = View.PackageId;
            if (View.PackageId == 8)
            {
                objpackage.StartDate = System.DateTime.Now;
                TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
                if (!Equals((stateManager.Get("TokenDetails", StateManager.State.Session)), null))
                {
                    objpackage.EndDate = System.DateTime.Now.AddDays(30);
                }
                else
                {
                    objpackage.EndDate = null;
                }
                objpackage.AmountPaid = 0;
            }
            else if (View.PackageId == 7)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = System.DateTime.Now.AddMonths(12);
                objpackage.AmountPaid = View.AmountPaid;
            }
            else if (View.PackageId == 6)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = null;
                objpackage.AmountPaid = View.AmountPaid;
            }
            else if (View.PackageId == 5)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = System.DateTime.Now.AddMonths(12);
                objpackage.AmountPaid = View.AmountPaid;
            }
            else if (View.PackageId == 4)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = null;
                objpackage.AmountPaid = View.AmountPaid;
            }
            else if (View.PackageId == 3)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = System.DateTime.Now.AddDays(30);
                objpackage.AmountPaid = 0;
            }
            else if (View.PackageId == 2)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.EndDate = System.DateTime.Now.AddMonths(12);
                objpackage.AmountPaid = View.AmountPaid;

            }
            else if (View.PackageId == 1)
            {
                objpackage.StartDate = System.DateTime.Now;
                objpackage.AmountPaid = View.AmountPaid;
                objpackage.EndDate = null;

            }
            objpackage.IsAutomaticRenew = View.NotifyBeforeRenew;
            objpackage.CouponId = null;
            objpackage.IsSponsor = false;
            objpackage.IsSponserHide = false;
            objpackage.CreditCardId = int.Parse(CCIdentity);
            object[] param = { objpackage, confirmationId };
            //_controller.InsertPackageDetails(param);
            this.View.TributePackageId = int.Parse(_controller.InsertPackageDetails(param).ToString());
            this.View.TransactionId = confirmationId.Length == 0 ? 0 : int.Parse(confirmationId);
        }

        public bool CheckEmailAvailability(string txtEmail)
        {
            return _controller.CheckEmailAvailability(txtEmail, View.ApplicationType);
        }
        //public void GetAvailableTributeUrl()
        //{
        //    View.TributeUrl = _controller.GetAvailableTributeUrl(View.TributeUrl.ToString());
        //}

        public void CheckAvailability()
        {
            View.chkAvailability = _controller.CheckUrlExists(View.TributeUrl, GetTributeIdCode(View.TributeType));
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

        /// <summary>
        ///  Method to create a folder in the root directory with TributeUrl as Folder name containing default.aspx file.
        /// </summary>
        /// <param name="applicationPath">Application physical path.</param>
        public void CreateDefaultFolder(string applicationPath)
        {
            //to create complete file path by appending TributeUrl with application path
            string filePath = applicationPath + this.View.TributeUrl;
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

        //to be able to send mail of the receipt
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


        #region METHODS FOR VIDEO TRIBUTE
        /// <summary>
        /// Method to save video tribute.
        /// </summary>
        public void SaveVideoTribute()
        {
            int CopyIteration = 0;
            int MaxCopyIteration = 0; //= 5;
            int.TryParse(WebConfig.VideoFileCopyIteration.ToString(), out MaxCopyIteration);

            Videos objVid = GetVideoObject();

            //to copy video from ftp folder to tributeportal folder
            string[] paths = CommonUtilities.GetVideoTributePath();
            string srcPath = paths[0] + this.View.UserName + "\\" + objVid.TributeVideoId + "_files";
            string destPath = paths[1] + this.View.CreatedTributeUrl + "_" + this.View.TributeTypeDescription;

            //either when not copied or iterations < MaxCopyIteration

            Directory.CreateDirectory(destPath);
            foreach (string strFileName in Directory.GetFileSystemEntries(srcPath))
            {
                //LHK : Added check MaxCopyIteration times if Video Copied successfully.
                while (CopyIteration < MaxCopyIteration)
                {
                    if (!File.Exists(destPath + "/" + Path.GetFileName(strFileName)))
                    {
                        File.Copy(strFileName, destPath + "/" + Path.GetFileName(strFileName), true); // strFileName.Substring(strFileName.LastIndexOf(@"\")));
                        try
                        {
                            if (strFileName.ToLower().Contains(".swf"))
                            {
                                string VideofileName = Path.GetFileName(strFileName);
                                int dotIndex = VideofileName.IndexOf('.');
                                int LastDotIndex = VideofileName.LastIndexOf('.');
                                if ((LastDotIndex - dotIndex) > 0)
                                {
                                    string strRemoveName = VideofileName.Substring(dotIndex, LastDotIndex - dotIndex);
                                    VideofileName = VideofileName.Replace(strRemoveName, "");
                                    File.Copy(strFileName, destPath + "/" + VideofileName, true);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //throw ex;
                        }
                    }
                    else
                    {
                        break;
                    }
                    CopyIteration++;

                }
            }

            SWFToImageObject objSwf = new SWFToImageObject();
            SWFToImageObject objSwfThumb = new SWFToImageObject();

            objSwf.InitLibrary("demo", "demo");
            objSwfThumb.InitLibrary("demo", "demo");
            //FIND ALL FOLDERS IN FOLDER
            string Location = destPath;
            DirectoryInfo dir = new DirectoryInfo(Location);
            //foreach (DirectoryInfo innerdir in dir.GetDirectories())
            //{
            foreach (FileInfo file in dir.GetFiles("*.*"))
            {
                int captureIndex = 0;
                int.TryParse(WebConfig.captureFrameIndexNumber.ToString(), out captureIndex);
                //Directory.SetCurrentDirectory(Location + "\\" + innerdir.Name);
                Directory.SetCurrentDirectory(Location);
                if (file.Extension.Equals(".swf"))
                {
                    string safeFileName = string.Empty;
                    objSwf.InputSWFFileName = file.FullName; // input SWF file
                    objSwf.FrameIndex = captureIndex;
                    objSwf.ImageWidth = 480;
                    objSwf.ImageHeight = 320;
                    objSwf.ImageOutputType = (TImageOutputType)1;
                    objSwf.JPEGQuality = 100;
                    objSwf.Execute();

                    String fileName = file.Name.Substring(0, file.Name.IndexOf("."));
                    //LHK: to avoid special characters in the file name.
                    safeFileName = RemoveSpecialCharacter(fileName);
                    if (safeFileName != fileName)
                    {
                        objSwfThumb.InputSWFFileName = file.FullName; // input SWF file
                        objSwfThumb.FrameIndex = 35;
                        objSwfThumb.ImageWidth = 144;
                        objSwfThumb.ImageHeight = 96;
                        objSwfThumb.ImageOutputType = (TImageOutputType)1;
                        objSwfThumb.JPEGQuality = 100;
                        objSwfThumb.Execute();

                        objSwf.SaveToFile(safeFileName + "_big.jpg"); // save to jpg file
                        objSwfThumb.SaveToFile(safeFileName + ".jpg"); // save to jpg file

                        if (!File.Exists(destPath + "\\" + safeFileName + ".swf"))
                            file.CopyTo(destPath + "\\" + safeFileName + ".swf");

                        objVid.TributeVideoId = safeFileName;
                        //this.View.VideoTributeId = fileName;
                    }
                    else
                    {
                        objSwf.SaveToFile(fileName + "_big.jpg"); // save to jpg file
                    }
                }
            }
            //}
            //Directory.Move(srcPath, destPath);
            //File.Copy(srcPath, destPath);


            //to save record in database and Send Email
            _controller.SaveVideoTribute(objVid);
        }

        private string RemoveSpecialCharacter(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] >= '0' && str[i] <= '9') || (str[i] >= 'A' && str[i] <= 'z' || str[i] == '_'))
                    sb.Append(str[i]);
            }
            return sb.ToString();
        }

        public void SaveVideoForMemTribute(int VideoTributeId)
        {
            Videos objVid = GetVideoObjectofVideoTribute(VideoTributeId);

            //to copy video from ftp folder to tributeportal folder
            string[] paths = CommonUtilities.GetVideoTributePath();
            string srcPath = paths[1] + objVid.VideoUrl.Trim() + "_" + objVid.VideoTypeId.Trim() + "_Video";
            string destPath = paths[1] + this.View.CreatedTributeUrl + "_" + this.View.TributeTypeDescription;
            objVid.VideoUrl = objVid.VideoTypeId = string.Empty;

            Directory.CreateDirectory(destPath);
            foreach (string strFileName in Directory.GetFileSystemEntries(srcPath))
            {
                File.Copy(strFileName, destPath + "/" + Path.GetFileName(strFileName), true); // strFileName.Substring(strFileName.LastIndexOf(@"\")));
            }

            SWFToImageObject objSwf = new SWFToImageObject();

            objSwf.InitLibrary("demo", "demo");
            //FIND ALL FOLDERS IN FOLDER
            string Location = destPath;
            DirectoryInfo dir = new DirectoryInfo(Location);
            //foreach (DirectoryInfo innerdir in dir.GetDirectories())
            //{
            foreach (FileInfo file in dir.GetFiles("*.*"))
            {
                //Directory.SetCurrentDirectory(Location + "\\" + innerdir.Name);
                Directory.SetCurrentDirectory(Location);
                if (file.Extension.Equals(".swf"))
                {
                    objSwf.InputSWFFileName = file.FullName; // input SWF file
                    objSwf.FrameIndex = 35;
                    objSwf.ImageWidth = 480;
                    objSwf.ImageHeight = 320;
                    objSwf.ImageOutputType = (TImageOutputType)1;
                    objSwf.JPEGQuality = 100;
                    objSwf.Execute();

                    String fileName = file.Name.Substring(0, file.Name.IndexOf("."));
                    objSwf.SaveToFile(fileName + "_big.jpg"); // save to jpg file
                }
            }
            //}
            //Directory.Move(srcPath, destPath);
            //File.Copy(srcPath, destPath);


            //to save record in database and Send Email
            _controller.SaveVideoTribute(objVid);
        }

        // Method to get the Video object of the Video Tribute to save in Memorial
        private Videos GetVideoObjectofVideoTribute(int VideoTributeId)
        {
            Videos objVideo = new Videos();
            objVideo.UserTributeId = VideoTributeId;
            Videos objVideoTribute = GetVideoTributeDetails(objVideo);
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                objVideo.CreatedBy = objSessionvalue.UserId;
                objVideo.UserId = objSessionvalue.UserId;
            }
            objVideo.VideoTypeId = objVideoTribute.UserId.ToString();
            objVideo.VideoUrl = objVideoTribute.VideoCaption;
            objVideo.CreatedDate = DateTime.Now;
            objVideo.IsActive = true;
            objVideo.IsDeleted = false;
            objVideo.TributeVideoId = objVideoTribute.TributeVideoId;

            objVideo.UserTributeId = this.View.TributeId;
            objVideo.VideoCaption = this.View.VideoCaption;
            objVideo.VideoDesc = this.View.VideoDesc;
            objVideo.TributeName = this.View.TributeName;
            objVideo.TributeType = this.View.TributeTypeDescription;
            objVideo.TributeUrl = this.View.TributeUrl;
            objVideo.UserName = this.View.UserName;
            return objVideo;
        }
        /// <summary>
        /// Method to get the video object to save
        /// </summary>
        /// <returns>Filled Video entity</returns>
        private Videos GetVideoObject()
        {
            Videos objVideo = new Videos();
            objVideo.CreatedBy = this.View.UserId;
            objVideo.CreatedDate = DateTime.Now;
            objVideo.IsActive = true;
            objVideo.IsDeleted = false;
            objVideo.TributeVideoId = this.View.VideoTributeId;
            objVideo.UserId = this.View.UserId;
            objVideo.UserTributeId = this.View.TributeId;
            objVideo.VideoCaption = this.View.VideoCaption;
            objVideo.VideoDesc = this.View.VideoDesc;
            objVideo.TributeName = this.View.TributeName;
            objVideo.TributeType = this.View.TributeTypeDescription;
            objVideo.TributeUrl = this.View.TributeUrl;
            objVideo.UserName = this.View.UserName;
            return objVideo;
        }

        public void GetCrditCardDetails()
        {
            UserRegistration _objUserReg = new UserRegistration();
            Users objUsers = new Users();
            objUsers.UserId = View.UserId;
            _objUserReg.Users = objUsers;
            object[] param = { _objUserReg };
            _controller.GetCreditCardDetails(param);
            UserRegistration objDetails = (UserRegistration)param[0];
            View.CreditCardDetails = objDetails; //(UserRegistration)param[0];
            if (objDetails.UserCreditcardDetails != null)
                View.SelectedPaymentMode = _objUserReg.UserCreditcardDetails.CreditCardType;
        }

        public void GetSelectedPaymentMode()
        {
            View.GetSelectedPaymentMode = _controller.PaymentModes();
        }

        public string SequenceTributeName(string strTributeName, string strTributeType)
        {
            return _controller.SequenceTributeName(strTributeName, strTributeType);
        }


        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="videoTributeId"></param>
        public void SetVideoTributeValue(int videoTributeId)
        {
            Tributes videoTribute;
            videoTribute = _controller.GetVideoTributeDetailById(videoTributeId);
            this.View.date2 = videoTribute.Date2;
            this.View.date1 = videoTribute.Date1;
            this.View.SelectedCity = videoTribute.City;

            if (videoTribute.Attribute2 != null)
                this.View.SelectedCountry = videoTribute.Attribute2.ToString();

            if (videoTribute.Attribute1 != null)
                this.View.SelectedState = videoTribute.Attribute1.ToString();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="videoTributeId"></param>
        /// <param name="tributeId"></param>
        public void LinkVideoTribute(int videoTributeId, int tributeId)
        {
            LinkVideoMemorialTribute objLinkTribute = new LinkVideoMemorialTribute(View.UserId, videoTributeId, tributeId);
            _controller.LinkVideoTribute(objLinkTribute);
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

        public IList<Themes> GetSubCategoryForTheme(string categoryName)
        {
            return _controller.GetSubCategoryForTheme(categoryName);
        }

        public Videos GetVideoTributeDetails(Videos objVideo)
        {
            VideoGallery objVideoTribute = _controller.GetVideoTributeDetails(objVideo);
            return objVideoTribute.Videos;
        }

        public void GetCreditPointCount()
        {
            UserRegistration _objUserReg = new UserRegistration();
            Users objUsers = new Users();
            objUsers.UserId = View.UserId;
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

        public void InsertCurrentCreditPoints(double NewUpdatedCredit, string CCIdentity, string confirmationId)
        {
            CreditPointTransaction objCreditTransaction = new CreditPointTransaction();
            objCreditTransaction.UserId = View.UserId;
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
        //LHK:(4:51 PM 2/8/2011) for ObNote
        public string StripHtml(string htmlString)
        {
            //Regex regex = new Regex("</?(.*)>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string finalString = Regex.Replace(htmlString, @"<(.|\n)*?>", string.Empty);  //regex.Replace(htmlString, regex, string.Empty);
            return finalString;
            //return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }
        public string UrlCreator(string str)
        {
            //special char removal
            StringBuilder sb = new StringBuilder();
            str = str.Replace(" ", "-");
            while (str.Contains("--"))
            {
                str = str.Replace("--", "-");
            }
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] >= '0' && str[i] <= '9') || (str[i] >= 'A' && str[i] <= 'z') || str[i] >= '(' || str[i] >= ')')
                    sb.Append(str[i]);
            }
            return sb.ToString();
        }
        // by Ud: for getting default theme for business type user
        public void GetDefaultTheme(int UserId, string strTributeType)
        {

            View.DefaultTheme = _controller.GetDefaultTheme(UserId, strTributeType);
        }
        // by Ud: for Saving default theme for business type user
        public void SaveDefaultTheme(int userId, string tributeType, int themeId)
        {
            _controller.SaveDefaultTheme(userId, tributeType, themeId);
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
    }
}
