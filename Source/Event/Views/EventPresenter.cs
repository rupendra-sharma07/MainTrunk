///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Event.Views.EventPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Event List.
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

namespace TributesPortal.Event.Views
{
    public class EventPresenter : Presenter<IEvent>
    {
        #region CLASS VARIABLES

        private EventController _controller;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="controller">A EventController object to call the method of controller</param>
        public EventPresenter([CreateNew] EventController controller)
        {
            _controller = controller;
        }

        #endregion


        #region METHODS

        #region PUBLIC METHODS

        /// <summary>
        /// This Method will called every time the view loads
        /// </summary>
        public override void OnViewLoaded()
        {
            UserIsAdmin();
        }

        /// <summary>
        /// This method will call the first time the view loads
        /// </summary>
        public override void OnViewInitialized()
        //public void OnViewInitialized(CommentTributeAdministrator objSession)
        {
            Events objEvent = new Events();
            
            objEvent.UserId = View.UserID;
            objEvent.TributeId = View.TributeID;

            IList<Events> eventList = _controller.GetEventList(objEvent);
            View.IsAdmin = objEvent.IsAdmin;
            View.EventList = eventList;

            //to display the Message count
            //this.View.RecordCount = GetMessageCount(objSession.CurrentPage, objSession.PageSize, eventList.Count, objSession.TotalRecords);

            //to display the Paging
            //this.View.DrawPaging = GetPaging(eventList.Count, objSession.PageSize, objSession.CurrentPage);

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
            return "event.aspx?PageNo=" + pageNumber; // +"&TributeId=" + tributeId + "&TributeName=" + _tributeName + "&TributeType=" + _tributeType;
        }

        /// <summary>
        /// This method will call the Event Controller class method to Delete the event in the database
        /// </summary>
        public void DeleteEvent()
        {
            try
            {
                Events objEvent = new Events();

                objEvent.EventID = View.EventID;
                objEvent.UserId = View.UserID;
                objEvent.TributeId = View.TributeID;
                objEvent.FirstName = View.FirstName;
                objEvent.LastName = View.LastName;
                objEvent.TributeName = View.TributeName;
                objEvent.EventName = View.EventName;
                objEvent.TributeType = View.TributeType;
                objEvent.TributeURL = View.TributeURL;
                _controller.DeleteEvent(objEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetTributeEndDate(int tributeId)
        {
            return _controller.GetTributeEndDate(tributeId);
        }
        public int GetCurrentEvents(int tributeId)
        {
            return _controller.GetCurrentEvents(tributeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool GetCustomHeaderDetail(int tributeId)
        {
            return _controller.GetCustomHeaderDetail(tributeId);
        }
        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// Method to set user admin info in the session
        /// </summary>
        private void UserIsAdmin()
        {
            UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
            objUserInfo.UserId = View.UserID;
            objUserInfo.TributeId = View.TributeID;
            objUserInfo.TypeName = PortalEnums.TributeContentEnum.Event.ToString();

            objUserInfo.IsAdmin = View.IsAdmin;

            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add(PortalEnums.AdminInfoEnum.UserAdminInfo_Event.ToString(), objUserInfo, StateManager.State.Session);
        }

        #endregion

        #endregion
    }
}




