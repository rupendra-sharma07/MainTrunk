///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.FeaturedTributeManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the methods for Featured Tributes
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.ResourceAccess;
/// <summary>
///Tribute Portal-Facade Manager Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.
/// </summary>
namespace TributesPortal.BusinessLogic
{
    public partial class FeaturedTributeManager
    {
        /// <summary>
        /// Function to get the list of featured tributes
        /// </summary>
        /// <returns>List containing featured tributes</returns>
        public List<FeaturedTribute> GetFeaturedTributes(FeaturedTribute objFeaturedTribute)
        {
            FeaturedTributeResource objResource = new FeaturedTributeResource();
            object[] obj = { objFeaturedTribute };
            return objResource.GetData(obj);
        }
    }
}
