

///Copyright       : Copyright (c) Optimus Information
///Project         : Your Tributes
///File Name       : TributePortal.DevelopmentWebsite.Videos.Views.VideoTributePresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This class defines the presenter to be implemented  by  video type tribute pages  on the site
///Audit Trail     : Date of Modification  Modified By         Description



#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
#endregion

namespace TributesPortal.Video.Views
{
    public class VideoTributePresenter : Presenter<IVideoTribute>
    {
        #region CLASS VARIABLES
        private VideoController _controller;
        #endregion

        #region CONSTRUCTOR
        public VideoTributePresenter([CreateNew] VideoController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS

        #region TributeHeader Details
        public void GetHeaderDetailsOnUserId(int userId)
        {
            if (!userId.Equals(0))
            {
                UserBusiness objUserBusiness = new UserBusiness();

                objUserBusiness = _controller.GetHeaderDetailsOnUserId(userId);
                if (objUserBusiness != null)
                {
                    //this.View.UserBussCity = objUserBusiness.Attribute1;
                    //this.View.UserBussState = objUserBusiness.Attribute2;
                    //this.View.BusinessAddress = GetBussAdd(objUserBusiness.BusinessAddress);
                    //this.View.Phone = objUserBusiness.Phone;
                    //this.View.HeaderBGColor = objUserBusiness.HeaderBGColor;
                    //this.View.HeaderLogo = objUserBusiness.HeaderLogo;
                    //this.View.WebSite = objUserBusiness.Website;
                    //this.View.IsAddressOn = Convert.ToBoolean(objUserBusiness.IsAddressOn);
                    //this.View.IsPhoneOn = Convert.ToBoolean(objUserBusiness.IsPhoneNoOn);
                    //this.View.IsWebSiteOn = Convert.ToBoolean(objUserBusiness.IsWebAddressOn);
                    this.View.IsCustomHeaderOn = Convert.ToBoolean(objUserBusiness.DisplayCustomHeader);
                    //this.View.IsObituaryURLOn = objUserBusiness.IsObUrlLinkOn;
                    //this.View.ObituaryURL = objUserBusiness.ObituaryLinkPage;

                }
            }
        }

        
        #endregion


        public string GetTributeEndDate(int tributeId)
        {
            return _controller.GetTributeEndDate(tributeId);
        }
        /// <summary>
        /// LHK: To get tribute details for Tribute
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns>tribute type of object</returns>
        public Tributes GetTributeFieldDetails(int tributeId)
        {
            Tributes objTrb = new Tributes();
            objTrb = _controller.GetTributeFieldDetails(tributeId);

            if (objTrb!= null)
            {
                this.View.TributeName = objTrb.TributeName;
                this.View.UserTributeId = objTrb.UserTributeId;
                this.View.TributeUrl = objTrb.TributeUrl;
                this.View.TributeUserEmail = objTrb.TypeDescription;
                if (!(objTrb.Date1.Equals(null) || objTrb.Date1.Equals("")))
                {
                    this.View.Date1 = objTrb.Date1;
                    this.View.Date2 = objTrb.Date2;
                    this.View.Age = CalculateAge(objTrb.Date1.ToString(), objTrb.Date2.ToString());
                }
                else
                    this.View.Date2 = objTrb.Date2;

                this.View.TributeImage = objTrb.TributeImage;
                this.View.City = objTrb.City;
                this.View.State = objTrb.Attribute1;
                this.View.Country = objTrb.Attribute2;
                this.View.IsOrderDVDChecked = objTrb.IsOrderDVDChecked;
                this.View.IsMemTributeBoxChecked = objTrb.IsMemTributeBoxChecked;
                this.View.TributeType = objTrb.TributeType;
                this.View.LinkMemoTributeId = objTrb.LinkMemTributeId;
                this.View.IsPrivate = objTrb.IsPrivate;
                this.View.Location = GetLocation();
            }

            return objTrb;

        }

        public Tributes GetDetailOfLinkedtribute(int tributeId)
        {
            return _controller.GetTributeFieldDetails(tributeId);
        }

        public Videos GetVideoDetailsOnUserTributeId(int _tributeId)
        {
            Videos objVideos = new Videos();

            objVideos = _controller.GetVideoDetailsOnUserTributeId(_tributeId);
            if (objVideos != null)
            {
                if (objVideos.VideoId != null)
                {
                    //this.View.CreatedBy = objVideos.UserId;
                    this.View.VideoId = objVideos.VideoId;
                    this.View.VideoUserId = objVideos.UserId;
                    //this.View.UserTributeId = objVideos.UserTributeId;
                    //this.View.VideoTypeId = objVideos.VideoTypeId;
                    this.View.TributeVideoId = objVideos.TributeVideoId;
                    //this.View.VideoUrl = objVideos.VideoUrl;
                }
            }
            return objVideos;
        }

        public void GetLinkVideoMemorialTribute(int? UserId, int TributeId)
        {
            this.View.LinkMemoTributeId = _controller.GetLinkVideoMemorialTribute(UserId, TributeId);
        }


        private string GetLocation()
        {
            string location = "";

            try
            {
                if (this.View.City.ToString() != "")
                {
                    location = this.View.City.ToString();
                }

                if (this.View.State.ToString() != "")
                {
                    if (location != "")
                    {
                        location += ", ";
                    }

                    location += this.View.State.ToString();
                }

                if (location != "")
                {
                    location += ", ";
                }

                location += this.View.Country.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return location;
        }

        public int CalculateAge(string Date1, string Date2)
        {

            if (!string.IsNullOrEmpty(Date1) && !string.IsNullOrEmpty(Date1))
            {
                DateTime DOB = DateTime.Parse(Date1);
                DateTime DOD = DateTime.Parse(Date2);
                int YearsPassed = DOD.Year - DOB.Year;
                TimeSpan span = DOD.Subtract(DOB);
                int ageyears = span.Days / 365;
                // Are we before the birth date this year? If so subtract one year from the mix
                if (DOD.Month < DOB.Month || (DOD.Month == DOB.Month && DOD.Day < DOB.Day))
                {
                    --YearsPassed;
                }
                return YearsPassed;
                //return ageyears;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// to get B.Address for header (street+city+state/province)
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        //private string GetBussAdd(string p)
        //{
        //    string BussAddress = string.Empty;
        //    try
        //    {
        //        if (p != "")
        //        {
        //            BussAddress = p;
        //        }

        //        if (this.View.UserBussCity.ToString() != "")
        //        {
        //            if (BussAddress != "")
        //            {
        //                BussAddress += ", ";
        //            }
        //            BussAddress += this.View.UserBussCity.ToString();
        //        }
        //        if (this.View.UserBussState.ToString() != "")
        //        {
        //            if (BussAddress != "")
        //            {
        //                BussAddress += ", ";
        //            }
        //            BussAddress += this.View.UserBussState.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return BussAddress;
        //}
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
        public Users GetUserNameOnUserId(int _userId)
        {
            return _controller.GetUserNameOnUserId(_userId);
        }
        #endregion

        public void AddTributeCount(int tributeId)
        {
            this._controller.AddTributeCount(tributeId);
        }

        public int GetTributePackageId(int _tributeId)
        {
            return _controller.GetTributePackageId(_tributeId);
        }
    }//end class
}//end namespace

