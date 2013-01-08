///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.AllTributeResource.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Resource Access class for the AllTribut which is responsible for following opertion in the database
///                 1) Get Most Recently created tribute
///                 2) Get Most Popular Tribute
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

namespace TributesPortal.ResourceAccess
{
    public class AllTributeResource : PortalResourceAccess
    {
        #region METHODS

        #region PUBLIC METHODS

        /// <summary>
        /// This method will call the database method for getting the Recently created tribute
        /// on the basis of last created tribute
        /// </summary>
        /// <param name="tributeType">A int object which contain the tribute type for which we want to get the 
        /// Recently created tribute. by default it will be 1 ( for All Tribute)</param>
        /// <returns>This method will return the recently created tribute list</returns>
        public List<SearchTribute> GetRecentlyCreatedTribute(int tributeType)
        {
            try
            {
                object[] objSearch = { tributeType };

                DataSet dsSearch = new DataSet();

                dsSearch = GetDataSet("usp_GetMostRecentlyCeatedTribute", objSearch);

                List<SearchTribute> objTributeList = GetTributeList(dsSearch, false);

                return objTributeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the database method for getting the most popular tribute
        /// on the basis on number of hits
        /// </summary>
        /// <param name="tributeType">A int object which contain the tribute type for which we want to get the 
        /// most popular tribute. by default it will be 1 ( for All Tribute)</param>
        /// <returns>This method will return the recently created tribute list</returns>
        public List<SearchTribute> GetPopularTribute(int tributeType)
        {
            try
            {
                object[] objSearch = { tributeType };

                DataSet dsSearch = new DataSet();

                dsSearch = GetDataSet("usp_GetMostPopularTribute", objSearch);

                List<SearchTribute> objTributeList = GetTributeList(dsSearch, true);

                return objTributeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // TODO - Remove
        public List<SearchTribute> GetAllTributesList()
        {
            try
            {
                object[] objSearch = { };

                DataSet dsSearch = new DataSet();

                dsSearch = GetDataSet("usp_GetAllTribute", objSearch);

                List<SearchTribute> objTributeList = GetAllTributeList(dsSearch, true);

                return objTributeList;
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
        /// <param name="isPopular">A bool object which contain whether populate the most popular tribute or most recently
        /// Tribute</param>
        /// <returns>his method will return the List of Tribute</returns>
        private List<SearchTribute> GetTributeList(DataSet dsSearch, bool isPopular)
        {
            try 
            {
                List<SearchTribute> objTributeList = new List<SearchTribute>();

                // Append This virtual path with the Image
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
                           objSearchTribute.TributeImage = virtualPath + "thumbnails/" + drSearch["TributeImage"].ToString();
                        else
                            objSearchTribute.TributeImage = virtualPath + drSearch["TributeImage"].ToString();

                        objSearchTribute.TributeUrl = drSearch["TributeUrl"].ToString();
                        objSearchTribute.Location = GetLocation(drSearch);
                        objSearchTribute.CreatedDate = DateTime.Parse(drSearch["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");
                        objSearchTribute.Date1 = GetDate(drSearch);
                        objSearchTribute.CreatedBy = GetUserName(drSearch);
                        objSearchTribute.Hits = drSearch["hits"].ToString();
                        objSearchTribute.TributeUrl = drSearch["TributeUrl"].ToString();
                        objTributeList.Add(objSearchTribute);
                        objSearchTribute = null;
                    }
                }

                return objTributeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // TODO - Remove
        private List<SearchTribute> GetAllTributeList(DataSet dsSearch, bool isPopular)
        {
            try
            {
                List<SearchTribute> objTributeList = new List<SearchTribute>();

                if (dsSearch.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drSearch in dsSearch.Tables[0].Rows)
                    {
                        SearchTribute objSearchTribute = new SearchTribute();

                        objSearchTribute.TributeID = int.Parse(drSearch["TributeId"].ToString());
                        objSearchTribute.UserTributeID = int.Parse(drSearch["UserTributeId"].ToString());
                        objSearchTribute.TributeName = drSearch["TributeName"].ToString();
                        objSearchTribute.TributeType = drSearch["TributeType"].ToString();
                        objSearchTribute.TributeImage = drSearch["TributeImage"].ToString();
                        objSearchTribute.CreatedDate = DateTime.Parse(drSearch["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");
                        objSearchTribute.TypeDescription = drSearch["TypeDescription"].ToString();
                        objSearchTribute.TributeUrl = drSearch["TributeUrl"].ToString();
                        objSearchTribute.Date1 = GetDate(drSearch);
                        objTributeList.Add(objSearchTribute);
                        objSearchTribute = null;
                    }
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
                if ((drEvent["UserTributeId"] != null) &&
                    (drEvent["UserTributeId"].ToString() != "") &&
                    (drEvent["IsUserNameVisiable"].ToString() != ""))
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
                else
                {
                    // if tribute was created by user, and that user account was deleted,
                    // all user fields returned are null
                    // bool.Parse(drEvent["IsUserNameVisiable"].ToString()) == true 
                    // then causes exception
                    userName = "Unknown";
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
