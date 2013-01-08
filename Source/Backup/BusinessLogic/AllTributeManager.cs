///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.AllTributeManager.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Manager class for AllTribute  
///                 ( Recently Created Tribute and Most Popular Tribute).
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;

#endregion

/// <summary>
///Tribute Portal-AllTributeManager Manager Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

/// </summary>
/// 

namespace TributesPortal.BusinessLogic
{
    public class AllTributeManager
    {
       
        #region PUBLIC METHODS

        /// <summary>
        /// This method will call the AllTribute Resource Access class method for getting the Recently created tribute
        /// on the basis of last created tribute
        /// </summary>
        /// <param name="tributeType">A int object which contain the tribute type for which we want to get the 
        /// Recently created tribute. by default it will be 1 ( for All Tribute)</param>
        /// <returns>This method will return the recently created tribute list</returns>
        public List<SearchTribute> GetRecentlyCreatedTribute(int tributeType)
        {
            AllTributeResource objTributeRes = new AllTributeResource();

            return objTributeRes.GetRecentlyCreatedTribute(tributeType);
        }

        /// <summary>
        /// This method will call the AllTribute Resource Access class method for getting the most popular tribute
        /// on the basis on number of hits
        /// </summary>
        /// <param name="tributeType">A int object which contain the tribute type for which we want to get the 
        /// most popular tribute. by default it will be 1 ( for All Tribute)</param>
        /// <returns>This method will return the recently created tribute list</returns>
        public List<SearchTribute> GetPopularTribute(int tributeType)
        {
            AllTributeResource objTributeRes = new AllTributeResource();

            return objTributeRes.GetPopularTribute(tributeType);
        }

        // TODO - remove
        public List<SearchTribute> GetAllTributeList()
        {
            AllTributeResource objTributeRes = new AllTributeResource();

            return objTributeRes.GetAllTributesList();
        }

        #endregion
    }
}
