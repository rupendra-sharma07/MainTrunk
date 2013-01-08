///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.FeaturedTributeResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Featured Tribute
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
/// <summary>
///Tribute Portal-Facade Manager Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.
/// </summary>
namespace TributesPortal.ResourceAccess
{
    public partial class FeaturedTributeResource : PortalResourceAccess
    {
        /// <summary>
        /// Function to get the list of featured tributes from the database.
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns>list of featured tributes</returns>
        public List<FeaturedTribute> GetData(object[] objValue)
        {
            try
            {
                DataSet ds = GetDataSet("usp_GetFeaturedTributes", null);

                List<FeaturedTribute> lstFeaturedTribute = new List<FeaturedTribute>();

                //Fills the collection of featured tribute from dataset.
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        FeaturedTribute objFeaturedTribute = new FeaturedTribute();

                        objFeaturedTribute.FeaturedTributeId = int.Parse(dr["FeatureTributeId"].ToString());
                        objFeaturedTribute.UserTributeId = int.Parse(dr["UserTributeId"].ToString());
                        objFeaturedTribute.TributeId = int.Parse(dr["TributeId"].ToString());
                        objFeaturedTribute.TributeName = dr["TributeName"].ToString();
                        objFeaturedTribute.TributeImage = dr["TributeImage"].ToString();
                        objFeaturedTribute.City = dr["City"].ToString();
                        objFeaturedTribute.State = dr["State"].ToString();
                        objFeaturedTribute.Country = dr["Country"].ToString();
                        objFeaturedTribute.CreatedDate = "Created: " + DateTime.Parse(dr["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");

                        lstFeaturedTribute.Add(objFeaturedTribute);
                        objFeaturedTribute = null;
                    }
                }
                return lstFeaturedTribute;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
