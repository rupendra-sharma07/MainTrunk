///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Gift.Views.GiftPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Gifts.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using System.Reflection;
using System.ComponentModel;
using TributesPortal.MultipleLangSupport;

#endregion

namespace TributesPortal.Gift.Views
{
    public class GiftPresenter : Presenter<IGift>
    {

        #region CLASS VARIABLES

        private GiftController _controller;

        #endregion


        #region CONSTANT
        private const string DefaulImageUrl = "images/GiftImages/Memorial/Memorial_Gift19.jpg";
        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="controller">A GiftController object to call the method of controller</param>
        public GiftPresenter([CreateNew] GiftController controller)
        {
            _controller = controller;
        }

        #endregion


        #region METHODS

        #region PUBLIC METHODS

        /// <summary>
        /// This method will call every time the view loads
        /// </summary>
        public override void OnViewLoaded()
        {
            UserIsAdmin();
        }


        /// <summary>
        /// This method will call the first time the view loads
        /// </summary>
        public override void OnViewInitialized()
        {
            try
            {
                // this method will get the list of Gift
                GetGifts();

                //This method will get the Image List
                GiftImage tmpImage = new GiftImage();
                tmpImage.TributeType = int.Parse(View.TributeType);

                View.ImageList = _controller.GetImage(tmpImage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       /// <summary>
        /// This method will call the Gift Controller access class method to get the Gift detail from database
       /// </summary>
        public void GetGifts()
        {
            try
            {
                // Get the Gift List
                Gifts objGift = CreateGiftObject("GetGift");
                List<Gifts> objGiftList = _controller.GetGifts(objGift);

                // Populate Gifts values in the control
                PopulateValueInControl(objGiftList, objGift);

                // display paging on the basis of the record
                DisplayPaging(objGiftList.Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will call the Gift Controller class method to Insert the Gift Detail 
        /// </summary>
        public void InsertGifts()
        {
            try
            {
                _controller.InsertGift(CreateGiftObject("InsertGift"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       /// <summary>
        /// This method will call the Gift Controller class method to delete the Gift
       /// </summary>
        public void DeleteGift()
        {
            try
            {
                Gifts objGift = new Gifts();

                objGift.GiftId = View.GiftId;
                objGift.UserId = View.UserID;

                _controller.DeleteGift(objGift);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// This method will Populate the Gift object from the UI control to Insert values in the database
        /// </summary>
        /// <returns>This method will return the Gifts Object</returns>
        private Gifts CreateGiftObject(string operation)
        {
            Gifts objGift = null;

            try
            {
                objGift = new Gifts();

                if (View.UserID == 0)
                {
                    objGift.UserId = null;
                }
                else
                {
                    objGift.UserId = View.UserID;
                }
                objGift.TributeId = View.TributeID;

                if (operation == "InsertGift")
                {
                    string[] virtualDir = CommonUtilities.GetPath();

                    objGift.ImageUrl = View.ImageUrl.Substring(View.ImageUrl.IndexOf(virtualDir[2]) + virtualDir[2].Length, View.ImageUrl.Length - virtualDir[2].Length );
                    objGift.GiftMessage = View.GiftMessage;
                    objGift.GiftSentBy = View.GiftSentBy;
                    objGift.CreatedBy = View.UserID;
                    objGift.CurrentPage = View.CurrentPage;
                    objGift.PageSize = View.PageSize;
                }
                else if (operation == "GetGift")
                {
                    objGift.PageSize = View.PageSize;
                    objGift.CurrentPage = View.CurrentPage;
                }

                objGift.FirstName = View.FirstName;
                objGift.LastName = View.LastName;
                objGift.UrlToEmail = View.UrlToEmail;
                objGift.TributeName = View.TributeName;
                objGift.TributeType = View.TributeType;
                objGift.UserName = View.UserName;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objGift;
        }


        /// <summary>
        /// This method will populate the value of the Gift in the controls
        /// </summary>
        /// <param name="objGiftList">A list of Gifts</param>
        /// <param name="objGift">A Gift Object</param>
        private void PopulateValueInControl(List<Gifts> objGiftList, Gifts objGift)
        {
            View.IsAdmin = objGift.IsAdmin;
            View.TributeName = objGift.TributeName;
            View.TributeType = objGift.TributeType;
            View.TotalRecordCount = objGift.TotalRecordCount;
            if(View.TributeType == "3")
                View.ImageUrl = "images/GiftImages/Birthday/Birthday_Gift10.jpg";
            else if(View.TributeType == "5")
                View.ImageUrl = "images/GiftImages/Wedding/Wedding_Gift13.jpg";
            else if (View.TributeType == "2")
                View.ImageUrl = "images/GiftImages/New Baby/Baby_Gift18.jpg";
            else if (View.TributeType == "7")
                View.ImageUrl = "images/GiftImages/Memorial/Memorial_Gift18.jpg";
            else if (View.TributeType == "6")
                View.ImageUrl = "images/GiftImages/Anniversary/Anniversary_Gift16.jpg";
            else if (View.TributeType == "4")
                View.ImageUrl = "images/GiftImages/Graduation/Grad_Gift16.jpg";
            View.GiftList = objGiftList;
        }


        /// <summary>
        /// This method will display the paging on the basis of teh record
        /// </summary>
        /// <param name="objGift"></param>
        private void DisplayPaging(int recordCount)
        {
            //to display the Message count
            this.View.RecordCount = GetMessageCount(recordCount);

            //to display the Paging
            this.View.DrawPaging = GetPaging();
        }


        /// <summary>
        /// Method to get the method count text
        /// </summary>
        /// <param name="recordCount">Number of records to be displayed</param>
        /// <returns>String containing text to display</returns>
        private string GetMessageCount(int recordCount)
        {
            int endCount = 0;
            string strMessage = "";

            int startCount = View.CurrentPage == 1 ? 1 : View.CurrentPage * View.PageSize - (View.PageSize - 1);

            if (recordCount < View.PageSize)
            {
                endCount = View.CurrentPage * View.PageSize - (View.PageSize - recordCount);
            }
            else
            {
                endCount = View.CurrentPage * View.PageSize;
            }

            if (recordCount > 1)
            {
                strMessage = ResourceText.GetString("strMessages_GT") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_GT") + " " + endCount.ToString() +
                          " " + ResourceText.GetString("strOf_GT") + " " + View.TotalRecordCount;
            }
            else if (recordCount == 1)
            {
                strMessage = ResourceText.GetString("strMessage_GT") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_GT") + " " + endCount.ToString() +
                          " " + ResourceText.GetString("strOf_GT") + " " + View.TotalRecordCount;
            }
            else
            {
                strMessage = ResourceText.GetString("strNoMessage_GT");
            }

            return strMessage;
        }


        /// <summary>
        /// Method to create string for paging
        /// </summary>
        /// <returns>string containing paging</returns>
        private string GetPaging()
        {
            int pageCount = 0;

            if (View.TotalRecordCount % View.PageSize == 0)
            {
                pageCount = View.TotalRecordCount / View.PageSize;
            }
            else
            {
                pageCount = (View.TotalRecordCount / View.PageSize) + 1;
            }

            return DrawPaging(View.CurrentPage, pageCount);
        }


        /// <summary>
        /// Method to draw paging
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageCount">Total number of pages</param>
        /// <returns>string containing paging</returns>
        public string DrawPaging(int pageNumber, int pageCount)
        {
            StringBuilder sb = new StringBuilder();
            int x, y, z, pageEnd, pageStart;
            int pagingSize = int.Parse(WebConfig.PagingSize_Gift);

            if (pageCount == 1)
            {
                sb.Append("<strong>&nbsp;&nbsp;");
                sb.Append(pageCount);
                sb.Append("</strong>&nbsp;&nbsp;");
            }
            if (pageCount > 1)
            {
                if (pageNumber % pagingSize == 0)
                {
                    pageStart = pageNumber - (pagingSize - 1);
                }
                else
                {
                    y = pageNumber.ToString().Length;
                    if (y == 1)
                    {
                        pageStart = 1;
                    }
                    else
                    {
                        z = int.Parse(pageNumber.ToString().Substring(0, y - 1));
                        pageStart = (z * pagingSize) + 1;
                    }
                }
                if (pageStart + (pagingSize - 1) > pageCount)
                {
                    pageEnd = pageCount;
                }
                else
                {
                    pageEnd = pageStart + (pagingSize - 1);
                }

                if (pageStart > pagingSize)
                {
                    sb.Append("&nbsp;<a href=");
                    sb.Append(DrawLink(pageStart - 1));
                    sb.Append(">Prev </a>&nbsp;&nbsp;");
                }



                if (pageStart != 1)
                {
                    //sb.Append("<a href=");
                    //sb.Append(DrawLink(pageNumber - 1));
                    //sb.Append("><<</a>&nbsp;&nbsp;");
                }
                else
                {
                    sb.Append("&nbsp;&nbsp;");
                }

                for (x = pageStart; x <= pageEnd; x++)
                {
                    if (x == pageNumber)
                    {
                        sb.Append("<strong>");
                        sb.Append(x);
                        sb.Append("</strong>&nbsp;&nbsp;");
                    }
                    else
                    {
                        sb.Append("<a href=");
                        sb.Append(DrawLink(x));
                        sb.Append(">");
                        sb.Append(x);
                        sb.Append("</a>&nbsp;&nbsp;");
                    }
                }
                if (pageNumber < pageCount)
                {
                    //sb.Append("<a href=");
                    //sb.Append(DrawLink(pageNumber + 1));
                    //sb.Append(">></a>");
                }
                else
                {
                    sb.Append("&nbsp;&nbsp;");
                }
                if (pageEnd < pageCount)
                {
                    sb.Append("&nbsp;&nbsp;<a href=");
                    sb.Append(DrawLink(pageEnd + 1));
                    sb.Append(">Next</a>");
                }

                sb.Append("</CENTER>");

                return sb.ToString();
            }

            return sb.ToString();
        }


        /// <summary>
        /// Method to add query string in paging links
        /// </summary>
        /// <param name="pageNumber">Current Page number</param>
        /// <returns>string querystring</returns>
        private string DrawLink(int pageNumber)
        {
            return "gift.aspx?PageNo=" + pageNumber;            
        }


        /// <summary>
        /// Method to get user is admin or owner
        /// </summary>
        private void UserIsAdmin()
        {
            UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
            objUserInfo.UserId = View.UserID;
            objUserInfo.TributeId = View.TributeID;
            objUserInfo.TypeName = PortalEnums.TributeContentEnum.Gift.ToString();

            objUserInfo.IsAdmin = View.IsAdmin;

            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add(PortalEnums.AdminInfoEnum.UserAdminInfo_Gift.ToString(), objUserInfo, StateManager.State.Session);
        }

        #endregion

        #endregion
    }
}




