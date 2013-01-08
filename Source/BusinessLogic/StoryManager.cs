///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.StoryManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the basic functions for story
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
///Tribute Portal-Story Manager Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the Manager class for the Story.
/// </summary>

namespace TributesPortal.BusinessLogic
{
    public class StoryManager
    {
        #region METHODS

        #region PUBLIC METHODS

        /// <summary>
        /// This method will call the Story Resource access class method to get the Tribute Detail,  
        /// Story, and List of topic in More About section. and calcumate the age of the User.
        /// </summary>
        /// 
        /// <param name="objStory"> This is the stories object which contain the Tribute ID to get 
        ///the story for that tribute and user ID to get that user is admin or not for that tribute 
        /// </param>
        /// 
        /// <returns> This method will return the story object 
        /// </returns>
        public Stories GetStoryDetail(Stories objStoryParam)
        {
            try
            {
                StoryResource objStoryRes = new StoryResource();
                Stories objStory = objStoryRes.GetStoryDetail(objStoryParam);

                if (objStory.TributeType == "Memorial")
                {
                    objStory.Age = CalculateAge( objStory.Date1, objStory.Date2 );
                }

                return objStory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will call the Story Resource access class method to update the tribute detail
        /// and also send a email to all admin user
        /// </summary>
        /// 
        /// <param name="objTribute">Stories object which contain the Tribute detail which user want to update
        /// </param>
        public void UpdateTributeDetail(Stories objStory)
        {
            try
            {
                StoryResource objStoryRes = new StoryResource();
                objStoryRes.UpdateTributeDetail(objStory);

                StateManager objStateManager = StateManager.Instance;
                SessionValue objSessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);

                StateManager objStateManager_ = StateManager.Instance;
                Tributes objTribute = (Tributes)objStateManager_.Get(PortalEnums.SessionValueEnum.TributeSession.ToString(), StateManager.State.Session);

                TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
                string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);

                string UserName = objSessionValue.FirstName == string.Empty ? objSessionValue.UserName : (objSessionValue.FirstName + " " + objSessionValue.LastName);
                string EmailSubject = UserName + " updated a story on Your Tribute...";
                StringBuilder obhstrb = new StringBuilder();
                obhstrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + UserName + " updated the story in the " + objTribute.TributeName + " " + objTribute.TypeDescription + " Tribute.</p>");
                obhstrb.Append("<p>To read the story, follow the link below:<br/>");
                string strLink = "http://" + objTribute.TypeDescription.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objTribute.TributeUrl + "/story.aspx";
                obhstrb.Append("<a href='" + strLink + "' >" + strLink + "</a><p>");
                obhstrb.Append("<p>---<br/>");
                obhstrb.Append("Your Tribute Team</p></font>");

                SendEmail(objStory.TributeId, EmailSubject, obhstrb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void UpdateObituaryDetail(Stories objStory)
        {
            try
            {
                StoryResource objStoryRes = new StoryResource();
                objStoryRes.UpdateObituaryDetail(objStory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  This method will call the Story Resource access class method to Add and update the Story detail 
        ///  and also add new topic in the more about section. and also send a email to all admin user
        /// </summary>
        /// 
        /// <param name="objStory">Stories object which contain the story detail which user want to update
        /// </param>
        public void UpdateStoryDetail(Stories objStory)
        {
            try
            {
                StoryResource objStoryRes = new StoryResource();
                objStoryRes.UpdateStoryDetail(objStory);


                StateManager objStateManager = StateManager.Instance;
                SessionValue objSessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);

                StateManager objStateManager_ = StateManager.Instance;
                Tributes objTribute = (Tributes)objStateManager_.Get(PortalEnums.SessionValueEnum.TributeSession.ToString(), StateManager.State.Session);

                 TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
                string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);

                string UserName = objSessionValue.FirstName == string.Empty ? objSessionValue.UserName : (objSessionValue.FirstName + " " + objSessionValue.LastName);
                string EmailSubject = UserName + " updated a story on Your Tribute...";                
                StringBuilder obhstrb = new StringBuilder();
                obhstrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + UserName + " updated the story in the " + objTribute.TributeName + " " + objTribute.TypeDescription + " Tribute.</p>");
                obhstrb.Append("<p>To read the story, follow the link below:<br/>");
                string strLink = "http://" + objTribute.TypeDescription.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objTribute.TributeUrl + "/story.aspx";
                obhstrb.Append("<a href='" + strLink + "' >" + strLink + "</a><p>");
                //obhstrb.Append("<P> <a href='http://" + Servername + "/Story/Story.aspx?Tributeid=" + objTribute.TributeId + "&TributeName=" + objTribute.TributeName+"' >Click to view the story</a></P>");
                obhstrb.Append("<p>---<br/>");
                obhstrb.Append("Your Tribute Team</p></font>");



                SendEmail(objStory.TributeId, EmailSubject, obhstrb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This Method will call the Story Resource access class method to get the Topic list
        /// </summary>
        /// 
        /// <param name="objStoryParam">An object which contain the Tribute type for which wants to  get the Topic list
        /// </param>
        /// 
        /// <returns>This method will return the StoryMoreAbout object which contain the list of topic
        /// </returns>
        public IList<StoryMoreAbout> GetTopic(object[] objStoryParam)
        {
            try
            {
                StoryResource objStoryRes = new StoryResource();

                return objStoryRes.GetTopic(objStoryParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will call the Story Resource access class method to Delete the Topic from the 
        /// More about section and also send a email to all admin user
        /// </summary>
        /// <param name="objStory">stories object which contain the Section id and Userbioagraphy ID
        ///                        of the topic which topic user wants to delete
        /// </param>
        public void DeleteTopic(Stories objStory)
        {
            try
            {
                StoryResource objStoryRes = new StoryResource();
                objStoryRes.DeleteTopic(objStory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        
        #region PRIVATE METHODS

        /// <summary>
        /// This method will calculate the age of the user if tribute is memorial
        /// </summary>
        /// <param name="Date1">This is datetime object which is date of Birth</param>
        /// <param name="Date2">This is datetime object which is date of Death</param>
        /// <returns>This will return the </returns>
        private string CalculateAge(Nullable<DateTime> Date1, Nullable<DateTime> Date2)
        {
            try
            {
                if ((Date1 != null) && (Date1.ToString() != "") && (Date2 != null) && (Date2.ToString() != ""))
                {
                    Stories story = new Stories();
                    story.Date1 = Date1;
                    story.Date2 = Date2;
                   TributesPortal.ResourceAccess.StoryResource sr=new StoryResource ();
                   story =sr.ClaculateAge(story);
                   if (story.Age != "0")
                   return story.Age;
                   else
                   return "<1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Method to send email to the list of users
        /// </summary>
        /// <param name="TribuetId">Tribute ID to get the list of admin</param>
        /// <param name="strSubject">Subject of the mail</param>
        public void SendEmail(int TribuetId, string strSubject, string EmailBody)
        {
            StoryResource objStoryRes = new StoryResource();

            UserInfo objUser = objStoryRes.GetTributeAdministrators(TribuetId, "Story");

            //Function to send the mail to the list of users who have added the Tribute in their list of favourites 
            UserInfo objUserFav = objStoryRes.GetFavouriteTributeUsers(TribuetId, "Story");

            EmailMessages objEmail = EmailMessages.Instance;

            if (objUser.UserEmail != "")
            {
                bool val = objEmail.SendMessages("Your Tribute<" + WebConfig.NoreplyEmail + ">", objUser.UserEmail, strSubject, CreateBody(EmailBody), EmailMessages.TextFormat.Html.ToString());
            }
            
            //favourite mail
            //As per discussion with Rupendra: will send the mail in "To" field. 
            //ie a comma separated list of users in the "to" field
            if (objUserFav.UserEmail != "")
            {
                bool val = objEmail.SendMessages("Your Tribute<" + WebConfig.NoreplyEmail + ">", objUserFav.UserEmail, strSubject, CreateBody(EmailBody), EmailMessages.TextFormat.Html.ToString());
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
