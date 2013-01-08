///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.SearchTributeManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the basic search methods
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;

#endregion

/// <summary>
///Tribute Portal-SearchTribute Manager Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the Manager class for the searching the tribute ( Basic Search and Advance Search).
/// </summary>
/// 
namespace TributesPortal.BusinessLogic
{
    public class SearchTributeManager
    {

        #region METHODS

        #region PUBLIC METHODS

        
        /// <summary>
        /// This method will call the SearchTribute Resource access class method for searching the tribute (basic search)
        /// </summary>
        /// <param name="objBasicSearchParam">This is the SearchTribute object which contain the Search Parameter 
        /// to get the tribute list</param>
        /// <returns>This method will return the List of Tribute</returns>
        public List<SearchTribute> BasicSearch(SearchTribute objBasicSearchParam)
        {
            try
            {
                SearchTributeResource objTributeRes = new SearchTributeResource();

                // Replace wildcard character (*, ?) by the (% and _)
                objBasicSearchParam.ChangeSearchString = ReplaceWildcardChar(objBasicSearchParam.SearchString);

                return objTributeRes.BasicSearch(objBasicSearchParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the SearchTribute Resource access class method for searching the tribute (Advance search)
        /// </summary>
        /// <param name="objAdvanceSearchParam">This is the SearchTribute object which contain the Search Parameter 
        /// to get the tribute list</param>
        /// <returns>This method will return the List of Tribute</returns>
        public List<SearchTribute> AdvanceSearch(SearchTribute objAdvanceSearchParam)
        {
            try
            {
                SearchTributeResource objTributeRes = new SearchTributeResource();

                // Replace wildcard character (*, ?) by the (% and _)
                objAdvanceSearchParam.ChangeSearchString = ReplaceWildcardChar(objAdvanceSearchParam.SearchString);

                if (objAdvanceSearchParam.City != "")
                {
                    objAdvanceSearchParam.City = ReplaceWildcardChar(objAdvanceSearchParam.City);
                }

                return objTributeRes.AdvanceSearch(objAdvanceSearchParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the SearchTribute Resource access class method for getting the list of tribute type
        /// </summary>
        /// <returns>This method will return the list of tribute type</returns>
        public List<Tributes> GetTributeTypeList(string applicationType)
        {
            try
            {
                SearchTributeResource objTributeRes = new SearchTributeResource();
                return objTributeRes.GetTributeTypeList(applicationType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// This method will replace the screen wildcard character to SQL server wildcard character
        /// </summary>
        /// <param name="withWildcardChar">A String object which contain the wild card character</param>
        /// <returns>A String object with replaced wildcard characters</returns>
        private string ReplaceWildcardChar(string withWildcardChar)
        {
            try
            {
                String searchString = withWildcardChar.ToString().Replace('*', '%');

                if (!searchString.Contains("%"))
                {
                    string newsearchString = "%" + searchString + "%";
                    searchString = newsearchString;
                }

                return searchString.Replace('?', '_');
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion
    }
}
