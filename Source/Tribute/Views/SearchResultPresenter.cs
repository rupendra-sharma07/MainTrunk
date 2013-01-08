///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.SearchResultPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for fetching/displaying the search results.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;

#endregion

namespace TributesPortal.Tribute.Views
{
    public class SearchResultPresenter : Presenter<ISearchResult>
    {

        #region CLASS VARIABLES

        private TributeController _controller;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="controller">A TributeController object to call the method of controller</param>
        public SearchResultPresenter([CreateNew] TributeController controller)
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
        }

        /// <summary>
        /// This method will call the first time the view loads
        /// </summary>
        public override void OnViewInitialized()
        {
            // Get the list of all tribute type
            //GetTributeTypeList();
        }

        /// <summary>
        /// This method will call the SearchTribute Controller class method for getting the list of tribute type
        /// </summary>
        public void GetTributeTypeList(string applicationType)
        {
            View.TributeTypeList = _controller.GetTributeTypeList(applicationType);
        }

        /// <summary>
        /// This method will call the SearchTribute Controller class method for searching the tribute 
        /// (Basic and Advance search)
        /// </summary>
        public void Search(string applicationType)
        {
            try
            {
                SearchTribute objSearchTribute = null;
                List<SearchTribute> lstSearchTribute = null;
                objSearchTribute = View.SearchParameter;
                objSearchTribute.ApplicationType = applicationType;
                if (objSearchTribute != null)
                {
                    string type = objSearchTribute.TributeType;
                    if (type.LastIndexOf(' ') > 0)
                    {
                        if (type.Substring(type.LastIndexOf(' ')) == " Tributes" || type.Substring(type.LastIndexOf(' '))==" Websites")
                        {
                            objSearchTribute.TributeType = type.Substring(0, type.LastIndexOf(' '));
                        }
                    }

                    objSearchTribute.PageNumber = View.CurrentPage;
                    objSearchTribute.PageSize = View.PageSize;

                    if (View.SearchType == PortalEnums.SearchEnum.Advance.ToString())
                    {
                        lstSearchTribute = _controller.AdvanceSearch(objSearchTribute);
                    }
                    else if (View.SearchType == PortalEnums.SearchEnum.Basic.ToString())
                    {
                        lstSearchTribute = _controller.BasicSearch(objSearchTribute);
                    }

                    View.TotalRecordCount = objSearchTribute.TotalRecords;
                    IList<SearchTribute> SortedTributeList = Sorting(lstSearchTribute, objSearchTribute.SortOrder);
                    View.SearchTributesList = SortedTributeList;

                    // display paging on the basis of the record
                    DisplayPaging(lstSearchTribute.Count, objSearchTribute);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This method will sort teh passed list of tribute
        /// </summary>
        /// <param name="objParam">A List of SearchTribute which wants to sort</param>
        /// <param name="sortingOrder">A String object which contain the order of sorting</param>
        /// <returns>This method will return the sorted Tribute List</returns>
        public IList<SearchTribute> Sorting(IList<SearchTribute> objParam, string sortingOrder)
        {
            IList<SearchTribute> objTribute = objParam;

            //sorting 
            if (objTribute != null)
            {
                if (sortingOrder == PortalEnums.Sorting.DESC.ToString())
                {
                    ArrayList.Adapter((IList)objTribute).Sort(new SearchTribute.DescendingSorter());
                }
                else if (sortingOrder == PortalEnums.Sorting.ASC.ToString())
                {
                    ArrayList.Adapter((IList)objTribute).Sort(new SearchTribute.AscendingSorter());
                }
            }

            return objTribute;
        }

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// This method will display the paging on the basis of teh record
        /// </summary>
        /// <param name="objGift"></param>
        private void DisplayPaging(int recordCount, SearchTribute objSearchTribute)
        {
            //to display the Message count
            this.View.RecordCount = GetMessageCount(recordCount);

            //to display the Paging
            this.View.DrawPaging = GetPaging();

            // to display the message
            this.View.Message = GetErrorMessage(objSearchTribute);
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
                strMessage = ResourceText.GetString("strLists_SR") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_GT") + " " + endCount.ToString() +
                          " " + ResourceText.GetString("strOf_GT") + " " + View.TotalRecordCount;
            }
            else if (recordCount == 1)
            {
                strMessage = ResourceText.GetString("strList_SR") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_GT") + " " + endCount.ToString() +
                          " " + ResourceText.GetString("strOf_GT") + " " + View.TotalRecordCount;
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

            CommonUtilities objUtilities = new CommonUtilities();

            return objUtilities.DrawPaging(View.CurrentPage, pageCount, "searchresult.aspx");
        }

        /// <summary>
        /// This method will create the error message for dispalying the search result
        /// </summary>
        /// <param name="objSearchTribute"></param>
        /// <returns></returns>
        private string GetErrorMessage(SearchTribute objSearchTribute)
        {
            string searchString = "";

            string location = "";

            if (View.SearchType == "Advance")
            {
                if (objSearchTribute.City != "")
                {
                    location = ",";
                    location += " &quot;" + objSearchTribute.City + "&quot";
                }

                if ((objSearchTribute.StateName != null) && (objSearchTribute.StateName != ""))
                {
                    location = ",";
                    location += " &quot;" + objSearchTribute.StateName + "&quot";
                }

                if ((objSearchTribute.CountryName != null) && (objSearchTribute.CountryName != ""))
                {
                    location = ",";
                    location += " &quot;" + objSearchTribute.CountryName + "&quot";
                }

            }

            if (View.TotalRecordCount > 0)
            {

                searchString = "<h2>" + View.TotalRecordCount + " " + ResourceText.GetString("strResult_SR") + " &quot;" +
                    objSearchTribute.SearchString + "&quot;" + location + ".</h2><h3>" + ResourceText.GetString("lblOption1_SR") +
                    " <a href='advancedsearch.aspx'>" + ResourceText.GetString("lnkAdvanceSearch_SR") + "</a></h3>";

            }
            else
            {
                searchString = "<h2>" + ResourceText.GetString("strResult1_SR") + "</h2><h3>" + ResourceText.GetString("strResult2_SR") +
                   " &quot;" + objSearchTribute.SearchString + "&quot; with 0 results. Please check the following and try again:</h3>" +
                   ResourceText.GetString("strResult3_SR") + "<h3>" + ResourceText.GetString("lblOption2_SR") +
                   " <a href='advancedsearch.aspx'>" + ResourceText.GetString("lnkAdvanceSearch_SR") + "</a></h3>";
            }

            return searchString;
        }

        #endregion

        #endregion
    }
}




