//===============================================================================
// Microsoft patterns & practices
// Web Client Software Factory
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================


#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;

#endregion

///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Gift.GiftController.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Controller class for the Gift.
///Audit Trail     : Date of Modification  Modified By         Description

namespace TributesPortal.Gift
{
    public class GiftController
    {
        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the GiftController class
        /// </summary>
        public GiftController()
        {
        }

        #endregion


        #region PUBLIC METHODS

        /// <summary>
        ///  This method will call the Gift Manager access class method to get the Gift detail from database
        /// </summary>
        /// 
        /// <param name="objGift">This is the Gift object which contain the Tribute ID to get 
        ///the Gift for that tribute and user ID to get that user is admin or not for that tribute </param>
        /// 
        /// <returns> This method will return the List of Gifts </returns>
        public List<Gifts> GetGifts(Gifts objGift)
        {
            try
            {
                return FacadeManager.GiftManager.GetGifts(objGift);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Gift Manager access class method to get the Image list for the passed Tribute Type
        /// </summary>
        /// <param name="objGiftImage">An object which contain the Tribute type for which wants to  
        ///                             get the Image list</param>
        /// <returns>This method will return the Gifts object which contain the list of Image</returns>
        public List<GiftImage> GetImage(GiftImage objGift)
        {
            try
            {
                return FacadeManager.GiftManager.GetImage(objGift);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Gift Manager access class method to Add the Gift detail
        /// </summary>
        /// <param name="objGift">Gift object which contain the Gift detail which user want to Add</param>
        public void InsertGift(Gifts objGift)
        {
            try
            {
                FacadeManager.GiftManager.InsertGift(objGift);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Gift Manager access class method to delete the Gift
        /// </summary>
        /// <param name="objGift">Gift object which contain the Giftid which Gift user wants to delete</param>
        public void DeleteGift(Gifts objGift)
        {
            try
            {
                FacadeManager.GiftManager.DeleteGift(objGift);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        #endregion
    }
}
