///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.SearchTributeResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Searching tributes
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Collections.ObjectModel;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;

#endregion

/// <summary>
///Tribute Portal-SearchTribute Resource Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the Resource Access class for the SearchTribute which is responsible for following opertion in the database
// 1) Basic Search
// 2) Advance Search
// 3) Get Tribuet Type List
/// </summary>
/// 
namespace TributesPortal.ResourceAccess
{
    public class SearchTributeResource : PortalResourceAccess
    {
        #region METHODS

        #region PUBLIC METHODS


        /// <summary>
        /// This method will call the databse methods for getting the list of tribute type
        /// </summary>
        /// <returns>This method will return the list of tribute type</returns>
        public List<Tributes> GetTributeTypeList(string applicationType)
        {
            try
            {
                DataSet dsTributes = GetDataSet("usp_GetTributeList", new string[] { "TRIBUTE_TYPE", applicationType });

                List<Tributes> lstTributes = new List<Tributes>();

                if (dsTributes.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTributes.Tables[0].Rows)
                    {
                        Tributes objTributes = new Tributes();

                        objTributes.TributeId = int.Parse(dr["TypeCode"].ToString());
                        if (applicationType == "yourmoments")
                            objTributes.TributeName = dr["TypeDescription"].ToString() + " Websites";
                        else
                        objTributes.TributeName = dr["TypeDescription"].ToString() + " Tributes";
                        
                        lstTributes.Add(objTributes);
                        objTributes = null;
                    }
                }

                return lstTributes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the database method for searching the tribute (basic search)
        /// </summary>
        /// <param name="objBasicSearchParam">This is the SearchTribute object which contain the Search Parameter 
        /// to get the tribute list</param>
        /// <returns>This method will return the List of Tribute</returns>
        public List<SearchTribute> BasicSearch(SearchTribute objBasicSearchParam)
        {
            try
            {
                object[] objSearchParam = { objBasicSearchParam.TributeType, 
                                            objBasicSearchParam.ChangeSearchString,
                                            objBasicSearchParam.SortOrder,
                                            objBasicSearchParam.PageSize, objBasicSearchParam.PageNumber,objBasicSearchParam.ApplicationType 
                                          };

                DataSet dsSearch = new DataSet();

                dsSearch = GetDataSet("usp_BasicSearch", objSearchParam);

                List<SearchTribute> objBasicSearchTributeList = GetTributeList(dsSearch, objBasicSearchParam);

                return objBasicSearchTributeList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the database method for searching the tribute (Advance search)
        /// </summary>
        /// <param name="objAdvanceSearchParam">This is the SearchTribute object which contain the Search Parameter 
        /// to get the tribute list</param>
        /// <returns>This method will return the List of Tribute</returns>
        public List<SearchTribute> AdvanceSearch(SearchTribute objAdvanceSearchParam)
        {
            try
            {
                object[] objSearchParam = { objAdvanceSearchParam.TributeType, 
                                            objAdvanceSearchParam.ChangeSearchString, objAdvanceSearchParam.City,
                                            objAdvanceSearchParam.State, objAdvanceSearchParam.Country,  
                                            objAdvanceSearchParam.CreatedAfterDate, objAdvanceSearchParam.CreatedBeforeDate,
                                            objAdvanceSearchParam.SortOrder,
                                            objAdvanceSearchParam.PageSize, objAdvanceSearchParam.PageNumber,objAdvanceSearchParam.ApplicationType 
                                          };

                DataSet dsSearch = new DataSet();

                dsSearch = GetDataSet("usp_AdvanceSearch", objSearchParam);

                List<SearchTribute> objAdvanceSearchTributeList = GetTributeList(dsSearch, objAdvanceSearchParam);

                return objAdvanceSearchTributeList;
            }
            catch (Exception ex)
            {
                throw ex;   
            }
        }

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// This method will populate the Tribute List object from the dataset
        /// </summary>
        /// <param name="dsSearch">A Dataset object which contain tribuet listing</param>
        /// <param name="objSearchParam">A SerachTribuet object which contain the search parameter</param>
        /// <returns>This method will return the List of Tribute</returns>
        private List<SearchTribute> GetTributeList(DataSet dsSearch, SearchTribute objSearchParam)
        {
            try
            {
                List<SearchTribute> objTributeList = new List<SearchTribute>();

                string virtualPath = GetPath();

                if (dsSearch.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drSearch in dsSearch.Tables[0].Rows)
                    {
                        SearchTribute objSearchTribute = new SearchTribute();

                        objSearchTribute.TributeID = int.Parse(drSearch["TributeId"].ToString());
                        objSearchTribute.UserTributeID = int.Parse(drSearch["UserTributeId"].ToString());
                        objSearchTribute.TributeName = drSearch["TributeName"].ToString();
                        objSearchTribute.TributeType = drSearch["TributeType"].ToString();
                        if (drSearch["TributeImage"].ToString().Split('/').Length <= 2)
                         objSearchTribute.TributeImage = virtualPath + "thumbnails/" +  drSearch["TributeImage"].ToString();
                        else
                          objSearchTribute.TributeImage = virtualPath + drSearch["TributeImage"].ToString();

                        objSearchTribute.TributeUrl = drSearch["TributeUrl"].ToString();
                        objSearchTribute.Location = GetLocation(drSearch);
                        objSearchTribute.DateForSorting = DateTime.Parse(drSearch["CreatedDate"].ToString());
                        objSearchTribute.CreatedDate = DateTime.Parse(drSearch["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");
                        objSearchTribute.Date1 = GetDate(drSearch);
                        objSearchTribute.TotalRecords = objSearchParam.TotalRecords = int.Parse(drSearch["TotalRecords"].ToString());
                        objSearchTribute.CreatedBy = GetUserName(drSearch);

                        objTributeList.Add(objSearchTribute);
                        objSearchTribute = null;
                    }
                }
                else
                {
                    objSearchParam.TotalRecords = 0;
                }

                return objTributeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will combine the city, state and country to get the location
        /// </summary>
        /// <param name="drGift">A DataRow object which contain the City, State and Country</param>
        /// <returns>This method will return the string object which contain the location</returns>
        private string GetLocation(DataRow drSearch)
        {
            try
            {
                string location = "";

                if (drSearch["City"].ToString() != "")
                {
                    location += drSearch["City"].ToString() + ", ";
                }

                if (drSearch["State"].ToString() != "")
                {
                    location += drSearch["State"].ToString() + ", ";
                }

                location += drSearch["Country"].ToString();

                return location;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will get the userName on the basis of the attribute IsUserNameVisiable
        /// </summary>
        /// <param name="drGift">A DataRow object which contain the City, State and Country</param>
        /// <returns>This method will return the string object which contain the userName</returns>
        private string GetUserName(DataRow drEvent)
        {
            string userName = "";

            try
            {
                //if user is registered User
                if ((drEvent["UserTributeId"] != null) && (drEvent["UserTributeId"].ToString() != ""))
                {
                    // if IsUserNameVisiable is True then get the userName
                    if (bool.Parse(drEvent["IsUserNameVisiable"].ToString()) == true)
                    {
                        // if user name exist means Registered user add the gift then get teh userName
                        if ((drEvent["UserName"] != null) && (drEvent["UserName"].ToString() != ""))
                        {
                            userName = drEvent["UserName"].ToString();
                        }
                    }
                    // if IsUserNameVisiable is false then get the Name of the user
                    else
                    {
                        // if User is Personal user then get the "FirstName + LastName"
                        if ((drEvent["UserType"] != null) && (int.Parse(drEvent["UserType"].ToString()) == 1))
                        {
                            userName = drEvent["FirstName"].ToString() + " " + drEvent["LastName"].ToString();
                        }
                        // if User is Business user then get the "CompanyName"
                        else if ((drEvent["UserType"] != null) && (int.Parse(drEvent["UserType"].ToString()) == 2))
                        {
                            userName = drEvent["CompanyName"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userName;
        }

        /// <summary>
        /// This method will get the date on the basis of teh tribute type
        /// </summary>
        /// <param name="drSearch">A Datarow object which contain the tribute detail</param>
        /// <returns>returns a string object which contain date</returns>
        private string GetDate(DataRow drSearch)
        {
            try
            {
                string dateVal = "";

                if (drSearch["Date1"].ToString() != "")
                {
                    DateTime BornDate = DateTime.Parse(drSearch["Date1"].ToString());

                    if ((drSearch["TributeType"].ToString() == "Birthday") && (BornDate.Year.ToString() == "1780"))
                    {
                        dateVal = BornDate.ToString("MMMM dd"); 
                    }
                    else
                    {
                        dateVal = BornDate.ToString("MMMM dd, yyyy");
                    }
                }

                if (drSearch["TributeType"].ToString() == "Memorial")
                {
                    if (drSearch["Date2"].ToString() != "")
                    {
                        if (dateVal != "")
                        {
                            dateVal += " - ";
                        }
                        dateVal += DateTime.Parse(drSearch["Date2"].ToString()).ToString("MMMM dd, yyyy");
                    }
                }
                else if (drSearch["TributeType"].ToString() == "New Baby")
                {
                    if (drSearch["Date2"].ToString() != "")
                    {
                        dateVal = DateTime.Parse(drSearch["Date2"].ToString()).ToString("MMMM dd, yyyy");
                    }
                }

                return dateVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This function is used to get the path for photos to get and save.
        /// </summary>
        /// <returns>Array of string containing drive name, root folder name and photo virtual directory name.</returns>
        public string GetPath()
        {
            string strXmlPath = AppDomain.CurrentDomain.BaseDirectory + "Common\\XML\\PhotoConfiguration.xml";

            FileStream docIn = new FileStream(strXmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            XmlDocument contactDoc = new XmlDocument();
            //Load the Xml Document
            contactDoc.Load(docIn);

            //Get a node
            XmlNodeList photoVirtualDirectory = contactDoc.GetElementsByTagName("PhotoVirtualDirectory");

            //get the value
            return photoVirtualDirectory.Item(0).InnerText;
        }

        #endregion

        #endregion
    }
}
