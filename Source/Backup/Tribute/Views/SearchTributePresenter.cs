///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.SearchTributePresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for searching for a tribute.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;

#endregion


namespace TributesPortal.Tribute.Views
{
    public class SearchTributePresenter : Presenter<ISearchTribute>
    {
        #region CLASS VARIABLES

        private TributeController _controller;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="controller">A TributeController object to call the method of controller</param>
        public SearchTributePresenter([CreateNew] TributeController controller)
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
            // get the List of all tribute type
            //GetTributeTypeList();
        }

        public void GetTributeTypeList(string applicationType)
        {
            View.TributeTypeList = _controller.GetTributeTypeList(applicationType);
        }

        public void Search(object[] objSerachParam, string searchPage, int currentPage, int pageSize)
        {
            SearchTribute objSearchTribute = null;

            if (objSerachParam != null)
            {
                if (searchPage == "Advance")
                {
                    objSearchTribute = SetAdvanceSearchEntityObject(currentPage, pageSize, objSerachParam);
                    View.SearchTributesList = _controller.AdvanceSearch(objSearchTribute);
                }
                else
                {
                    objSearchTribute = SetBasicSearchEntityObject(currentPage, pageSize, objSerachParam);
                    View.SearchTributesList = _controller.BasicSearch(objSearchTribute);
                }

                View.TotalRecords = objSearchTribute.TotalRecords;
            }
        }

        #endregion


        #region PRIVATE METHODS

        private SearchTribute SetBasicSearchEntityObject(int currentPage, int pageSize, object[] objSerachParam)
        {
            SearchTribute objSearchTribute = new SearchTribute();

            objSearchTribute.TributeType = objSerachParam[0].ToString();
            objSearchTribute.SearchString = objSerachParam[1].ToString();
            objSearchTribute.PageNumber = currentPage;
            objSearchTribute.PageSize = pageSize;

            return objSearchTribute;
        }

        private SearchTribute SetAdvanceSearchEntityObject(int currentPage, int pageSize, object[] objSerachParam)
        {
            SearchTribute objSearchTribute = new SearchTribute();

            objSearchTribute.TributeType = objSerachParam[0].ToString();
            objSearchTribute.SearchString = objSerachParam[1].ToString();
            objSearchTribute.City = objSerachParam[2].ToString();

            Locations objState = (Locations)objSerachParam[3];

            if (objState == null)
            {
                objSearchTribute.State = -1;
            }
            else
            {
                objSearchTribute.State = objState.LocationId;
            }

            Locations objCountry = (Locations)objSerachParam[4];
            if (objCountry == null)
            {
                objSearchTribute.Country = -1;
            }
            else
            {
                objSearchTribute.Country = objCountry.LocationId;
            }

            objSearchTribute.CreatedAfterDate = (Nullable<DateTime>)objSerachParam[5];
            objSearchTribute.CreatedBeforeDate = (Nullable<DateTime>)objSerachParam[6];
            objSearchTribute.PageNumber = currentPage;
            objSearchTribute.PageSize = pageSize;

            return objSearchTribute;
        }

        public string DisplaySearchString(object[] objSerachParam)
        {
            string searchString = "";

            if (objSerachParam != null)
            {
                for (int i = 1; i < 2; i++)
                {
                    if (objSerachParam[i].ToString() != "")
                    {
                        if (i != 1)
                        {
                            searchString += ",";
                        }
                        searchString += objSerachParam[i];
                    }
                }

                int length = objSerachParam.Length;
                if (length > 2)
                {
                    if (objSerachParam[2].ToString() != "")
                    {
                        searchString += ",";
                        searchString += objSerachParam[2];
                    }
                    if (objSerachParam[3] != null)
                    {
                        Locations objState = (Locations)objSerachParam[3];
                        if (objState.LocationName != "")
                        {
                            searchString += ",";
                            searchString += objState.LocationName;
                        }
                    }
                    if (objSerachParam[4] != null)
                    {
                        Locations objCountry = (Locations)objSerachParam[4];
                        if (objCountry.LocationName != "")
                        {
                            searchString += ",";
                            searchString += objCountry.LocationName;
                        }
                    }
                }
            }

            return searchString;
        }

        public string DisplayRecordValue(int currentPage, int pageSize, int totalRecord)
        {
            string recordValue = "";

            int startValue = currentPage * pageSize - 1;
            int endValue = startValue + pageSize - 1;

            if (endValue <= totalRecord)
            {
                recordValue = "Tributes " + startValue.ToString() + " to " + endValue.ToString() + " of " + totalRecord;
            }
            else
            {
                recordValue = "Tributes " + startValue.ToString() + " of " + totalRecord;
            }

            return recordValue;
        }

        public int CountNumberOfpages(int totalRecord, int PageSize)
        {
            int pageCount = 0;

            int Numb = (int)((int)totalRecord / (int)PageSize);

            if (((int)totalRecord % (int)PageSize) == 0)
            {
                pageCount = Numb;
            }
            else
            {
                pageCount = Numb + 1;
            }

            return pageCount;
        }

        public IList<SearchTribute> Sorting(IList<SearchTribute> objParam, string sortingOrder)
        {
            IList<SearchTribute> objTribute = objParam;

            //sorting 
            if (objTribute != null)
            {
                if (sortingOrder == "Created: Newest")
                {
                    ArrayList.Adapter((IList)objTribute).Sort(new SearchTribute.DescendingSorter());
                }
                else if (sortingOrder == "Created: Oldest")
                {
                    ArrayList.Adapter((IList)objTribute).Sort(new SearchTribute.AscendingSorter());
                }
            }

            return objTribute;
        }

        #endregion

        #endregion
    }
}




