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

///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Story.StoryController.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Controller class for the files under Story.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;

namespace TributesPortal.Story
{
    public class StoryController
    {
        /// <summary>
        /// This is the constructor of the StoryController class
        /// </summary>
        public StoryController()
        {
        }


        #region PUBLIC METHODS

        /// <summary>
        /// This method will call the Story Mananger class method to get the Tribute Detail, Story, 
        /// and List of topic in More About section. and calcumate the age of the User.
        /// </summary>
        /// 
        /// <param name="objStory"> This is the stories object which contain the Tribute ID to get 
        ///the story for that tribute and user ID to get that user is admin or not for that tribute 
        /// </param>
        /// 
        /// <returns> This method will return the story object 
        /// </returns>
        public Stories GetStoryDetail(Stories objStory)
        {
            try
            {
                return FacadeManager.StoryManager.GetStoryDetail(objStory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        /// <summary>
        /// This method will call the Tribute Manager class to get the Country list and State list
        /// </summary>
        /// 
        /// <param name="countries">This will pass the parent location (country) for the state and null for the country
        /// </param>
        /// <returns>This method will return the list of location(state, country)</returns>
        public IList<Locations> GetCountryList(Locations countries)
        {
            try
            {
                return FacadeManager.TributeManager.GetCountryList(countries);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will call the Story Mananger class method to update the tribute detail
        /// </summary>
        /// 
        /// <param name="objTribute">Stories object which contain the Tribute detail which user want to update
        /// </param>
        public void UpdateTributeDetail(Stories objTribute)
        {
            try
            {
                FacadeManager.StoryManager.UpdateTributeDetail(objTribute);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void UpdateObituaryDetail(Stories objsTribute)
        {
            try
            {
                FacadeManager.StoryManager.UpdateObituaryDetail(objsTribute);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  This method will call the Story Mananger class method to Add and update the Story detail 
        ///  and also add new topic in the more about section
        /// </summary>
        /// 
        /// <param name="objStory">Stories object which contain the story detail which user want to update
        /// </param>
        public void UpdateStoryDetail(Stories objStory)
        {
            try
            {
                FacadeManager.StoryManager.UpdateStoryDetail(objStory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This Method will call the Story Mananger class method to get the Topic list
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
                return FacadeManager.StoryManager.GetTopic(objStoryParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will call the Story Mananger class method to Delete the Topic from the More about section
        /// </summary>
        /// <param name="objStory">stories object which contain the Section id and Userbioagraphy ID
        ///                        of the topic which topic user wants to delete
        /// </param>
        public void DeleteTopic(Stories objStory)
        {
            try
            {
                FacadeManager.StoryManager.DeleteTopic(objStory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
