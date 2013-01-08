///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.GuestBook.Views.GuestBookPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the GuestBook.
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

namespace TributesPortal.GuestBook.Views
{
    public class GuestBookPresenter : Presenter<IGuestBook>
    {
        #region CLASS VARIABLES
        private GuestBookController _controller;
        private string _tributeName;
        private string _tributeType;
        private int tributeId;
        #endregion

        #region CONSTRUCTOR
        public GuestBookPresenter([CreateNew] GuestBookController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        public void OnViewInitialized(CommentTributeAdministrator objSession, string tributeName, string tributeType)
        {
            tributeId = objSession.TributeId;
            _tributeName = tributeName;
            _tributeType = tributeType;
            //to get the comments list
            List<CommentTributeAdministrator> objComments = _controller.CommentList(objSession);

            //to change the \n from message to </br>
            List<CommentTributeAdministrator> objUpdatedComments = new List<CommentTributeAdministrator>();
            foreach(CommentTributeAdministrator obj in objComments)
            {
                CommentTributeAdministrator objComment = new CommentTributeAdministrator();
                //objComment = obj;
                //objComment.Message.Replace("\n", "</br>");
                objComment.City = obj.City;
                objComment.CommentId = obj.CommentId;
                objComment.CommentTypeId = obj.CommentTypeId;
                objComment.Country = obj.Country;
                objComment.CreatedBy = obj.CreatedBy;
                objComment.CreatedDate = obj.CreatedDate;
                objComment.CurrentPage = obj.CurrentPage;
                objComment.IsAdmin = obj.IsAdmin;
                objComment.IsPrivate = obj.IsPrivate;
                objComment.Message = obj.Message.Replace("\n", "</br>");
                objComment.PageSize = obj.PageSize;
                objComment.State = obj.State;
                objComment.TotalRecords = obj.TotalRecords;
                objComment.TributeId = obj.TributeId;
                objComment.TypeCodeId = obj.TypeCodeId;
                objComment.UserId = obj.UserId;
                if (obj.UserImage.StartsWith("http://") || obj.UserImage.StartsWith("https://")) {
                    objComment.UserImage = obj.UserImage;
                } else {
                    objComment.UserImage = CommonUtilities.GetPath()[2].ToString() + obj.UserImage;
                }
                objComment.FacebookUid = obj.FacebookUid;
                objComment.UserName = obj.UserName;
                objComment.UserType = obj.UserType;

                //Added new on 22 jun 2011 by rupendra to get the Table type from wich the comments are fetched
                objComment.TableType = obj.TableType;
                if (obj.IsLocationHide)
                {
                    objComment.Location = string.Empty;
                }
                else
                {
                    //LHK: code changes for roland obituaries 
                    string location = string.Empty;
                    if (!(string.IsNullOrEmpty(obj.City)))
                        location = obj.City;

                    if (!(string.IsNullOrEmpty(obj.State)))
                    {
                        if (string.IsNullOrEmpty(location))
                            location = obj.State;
                        else
                            location = location + ", " + obj.State;
                    }
                    if (!(string.IsNullOrEmpty(obj.Country)))
                    {
                        if (string.IsNullOrEmpty(location))
                            location = obj.Country;
                        else
                            location = location + ", " + obj.Country;
                    }

                    if (string.IsNullOrEmpty(location))
                        objComment.Location = string.Empty;
                    else
                        objComment.Location = "(" + location + ")";


                }

                objUpdatedComments.Add(objComment);
            }

            View.Comments = objUpdatedComments; //objComments;

            //to display the Message count
            this.View.RecordCount = GetMessageCount(objSession.CurrentPage, objSession.PageSize, objComments.Count, objSession.TotalRecords);

            //to display the Paging
            this.View.DrawPaging = GetPaging(objSession.TotalRecords, objSession.PageSize, objSession.CurrentPage);
        }

        public int OnPaging(CommentTributeAdministrator objSesion)
        {
            return _controller.RecordCount(objSesion);
        }

        public void OnSaveComments(Comments objComment)
        {
            _controller.SaveComment(objComment);
        }

        public void OnSaveComments(Comments objComment, string topUrl)
        {
            _controller.SaveComment(objComment, topUrl);
        }

        public void OnDeleteComments(Comments objComment)
        {
            _controller.DeleteComment(objComment);
        }

        /// <summary>
        /// Method to get the method count text
        /// </summary>
        /// <param name="currentPage">Current page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="recordCount">Number of records to be displayed</param>
        /// <param name="totalRecords">Total number of records</param>
        /// <returns>String containing text to display</returns>
        private string GetMessageCount(int currentPage, int pageSize, int recordCount, int totalRecords)
        {
            int endCount = 0;
            string strMessage;
            int startCount = currentPage == 1 ? 1 : currentPage * pageSize - (pageSize - 1);

            if (recordCount < pageSize)
                endCount = currentPage * pageSize - (pageSize - recordCount);
            else
                endCount = currentPage * pageSize;

            if (recordCount > 1)
                strMessage = ResourceText.GetString("strMessages_GB") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_GB") + " " + endCount.ToString() + " " + ResourceText.GetString("strOf_GB") + " " + totalRecords;
            else
                strMessage = ResourceText.GetString("strMessage_GB") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_GB") + " " + endCount.ToString() + " " + ResourceText.GetString("strOf_GB") + " " + totalRecords;

            return strMessage;
        }

        /// <summary>
        /// Method to create string for paging
        /// </summary>
        /// <param name="totalRecords">Total number of records</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="currentPage">Current size</param>
        /// <returns>string containing paging</returns>
        private string GetPaging(int totalRecords, int pageSize, int currentPage)
        {
            int pageCount = 0;
            if (pageSize == 0)
                int.TryParse(WebConfig.Pagesize_guestBook, out pageSize);
            if (totalRecords % pageSize == 0)
                pageCount = totalRecords / pageSize;
            else
                pageCount = (totalRecords / pageSize) + 1;

            return DrawPaging(currentPage, pageCount);
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
            int pagingSize = int.Parse(WebConfig.PagingSize_guestBook);
            if (pageCount == 1)
            {
                sb.Append("<strong>&nbsp;&nbsp;&nbsp;");
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
                    sb.Append(">Previous </a>&nbsp;&nbsp;&nbsp;&nbsp;");
                    //sb.Append(">Previous 10</a>&nbsp;&nbsp;&nbsp;&nbsp;");
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
                        //sb.Append("<strong>[");
                        sb.Append("<strong>");
                        sb.Append(x);
                        sb.Append("</strong>&nbsp;&nbsp;");
                        //sb.Append("]</strong>&nbsp;&nbsp;");
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
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;<a href=");
                    sb.Append(DrawLink(pageEnd + 1));
                    sb.Append(">Next</a>");
                    //sb.Append(">Next 10</a>");
                }

                //sb.Append("<small><br>Total Page Count: ");
                //sb.Append(pageCount);
                //sb.Append("</small>");
                //sb.Append("</CENTER>");
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
            return "guestbook.aspx?PageNo=" + pageNumber; // +"&TributeId=" + tributeId + "&TributeName=" + _tributeName + "&TributeType=" + _tributeType;
        }
        #endregion
    }
}




