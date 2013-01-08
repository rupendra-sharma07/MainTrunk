///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.GiftManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the basic gift methods
///Audit Trail     : Date of Modification  Modified By         Description



#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;
using TributesPortal.Utilities;

#endregion


/// <summary>
///Tribute Portal-Gift Manager Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the Manager class for the Gift.
/// </summary>

namespace TributesPortal.BusinessLogic
{
    public class GiftManager
    {
        #region METHODS

        #region PUBLIC METHODS

        /// <summary>
        ///  This method will call the Gift Resource access class method to get the Gift detail from database
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
                GiftResource objGiftRes = new GiftResource();

                return objGiftRes.GetGifts(objGift);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will call the Gift Resource access class method to get the Image list for the passed Tribute Type
        /// </summary>
        /// <param name="objGiftImage">An object which contain the Tribute type for which wants to  
        ///                             get the Image list</param>
        /// <returns>This method will return the Gifts object which contain the list of Image</returns>
        public List<GiftImage> GetImage(GiftImage objGift)
        {
            try
            {
                GiftResource objGiftRes = new GiftResource();

                return objGiftRes.GetImage(objGift);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will call the Gift Resource access class method to Add the Gift detail
        /// </summary>
        /// <param name="objGift">Gift object which contain the Gift detail which user want to Add</param>
        public void InsertGift(Gifts objGift)
        {
            try
            {
                GiftResource objGiftRes = new GiftResource();

                object Identity = objGiftRes.InsertGift(objGift);

                if ((objGift != null) && (objGift.CustomError == null) && (Identity != null))
                {
                    string name;

                    if (objGift.FirstName == "" && objGift.UserName == string.Empty)
                    {
                        name = objGift.GiftSentBy;

                        if (objGift.GiftSentBy == "")
                        {
                            name = "Anonymous User";
                        }
                    }
                    else
                    {
                        if (objGift.FirstName == string.Empty)
                            name = objGift.UserName;
                        else
                            name = objGift.FirstName + " " + objGift.LastName;
                    }

                    string EmailSubject = name + " added a new gift on Your " + WebConfig.ApplicationWord + "...";
                    string EmailBody = "<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + name + " added a new gift in the " + objGift.TributeName + " " + objGift.TributeType + " Tribute. <br/> <br/> To view the gift, follow the link below: <br/>" + objGift.UrlToEmail + "<br/> <br/>" + "----" + "<br/>" + "Your " + WebConfig.ApplicationWord + " Team</p></font>";

                    SendEmail(objGift.TributeId, EmailSubject, EmailBody);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Gift Resource access class method to delete the Gift
        /// </summary>
        /// <param name="objGift">Gift object which contain the Giftid which Gift user wants to delete</param>
        public void DeleteGift(Gifts objGift)
        {
            try
            {
                GiftResource objGiftRes = new GiftResource();

                objGiftRes.DeleteGift(objGift);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Method to send email to the list of users
        /// </summary>
        /// <param name="TribuetId">Tribute ID to get the list of admin</param>
        /// <param name="strSubject">Subject of the mail</param>
        public void SendEmail(int TribuetId, string strSubject, string EmailBody)
        {
            StoryResource objStoryRes = new StoryResource();

            UserInfo objUser = objStoryRes.GetTributeAdministrators(TribuetId, "Gift");
            //Function to send the mail to the list of users who have added the Tribute in their list of favourites 
            UserInfo objUserFav = objStoryRes.GetFavouriteTributeUsers(TribuetId, "Gift");

            EmailMessages objEmail = EmailMessages.Instance;

            if (objUser.UserEmail != "")
            {
                bool val = objEmail.SendMessages("Your " + WebConfig.ApplicationWord + "<" + WebConfig.NoreplyEmail + ">", objUser.UserEmail, strSubject, CreateBody(EmailBody), EmailMessages.TextFormat.Html.ToString());
            }
            
            //favourite mail
            //As per discussion with Rupendra: will send the mail in "To" field. 
            //ie a comma separated list of users in the "to" field
            if (objUserFav.UserEmail != "")
            {
                bool val = objEmail.SendMessages("Your " + WebConfig.ApplicationWord + "<" + WebConfig.NoreplyEmail + ">", objUserFav.UserEmail, strSubject, CreateBody(EmailBody), EmailMessages.TextFormat.Html.ToString());
            }
        }


        /// <summary>
        /// This method is used to create the Body of the mail
        /// </summary>
        /// <param name="strSubject">Body of the Mail</param>
        /// <returns>This method will return the body of the mail</returns>
        private string CreateBody(string strBody)
        {
            StringBuilder sbBody = new StringBuilder();
            //sbBody.Append("<br/>");
            sbBody.Append(strBody);
            sbBody.Append("<br/>");

            return sbBody.ToString();
        }

        #endregion
       
        #endregion
    }
}
