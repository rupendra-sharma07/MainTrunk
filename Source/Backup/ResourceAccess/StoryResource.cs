//Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.StoryResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Stories
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;

#endregion

/// <summary>
///Tribute Portal-Story Resource Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the Resource Access class for the Story which is responsible for following opertion in the database
// 1) Add Story and Topic
// 2) Update Tribute Detail and Story
// 3) Delete Topic
/// </summary>

namespace TributesPortal.ResourceAccess
{
    public class StoryResource : PortalResourceAccess 
    {
        #region METHODS

        #region PUBLIC METHODS

        /// <summary>
        /// This method will get the Tribute Detail, Story, and List of topic in More About
        /// </summary>
        /// 
        /// <param name="objStory"> This is the stories object which contain the Tribute ID to get 
        ///the story for that tribute and user ID to get that user is admin or not for that tribute 
        /// </param>
        /// 
        /// <returns> This method will return the story object which is populated with the 
        ///           Tribute Detail, story and more about for the Tribute
        /// </returns>
        public Stories GetStoryDetail(Stories objStory)
        {
            Stories objStoryDetail = null;

            try
            {
                if (objStory != null)
                {
                    object[] objStoryParam = { objStory.TributeId, objStory.UserId };

                    if (objStoryParam != null)
                    {
                        DataSet dsStory = new DataSet();

                        dsStory = GetDataSet("usp_GetStory", objStoryParam);

                        objStoryDetail = PopulateStoryObject(dsStory);
                    }
                }

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objStory.CustomError = objError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objStoryDetail;
        }


        /// <summary>
        /// This method will update the tribute detail
        /// </summary>
        /// 
        /// <param name="objTribute">Stories object which contain the Tribute detail which user want to update
        /// </param>
        public void UpdateTributeDetail(Stories objTribute)
        {
            try
            {
                if (objTribute != null)
                {
                    //sets the parameters
                    string[] strParam = { Stories.StoriesEnum.TributeId.ToString(),  
                                          Stories.StoriesEnum.TributeName.ToString(),  
                                          Stories.StoriesEnum.TributeImage.ToString(),  
                                          Stories.StoriesEnum.City.ToString(),  
                                          Stories.StoriesEnum.State.ToString(),  
                                          Stories.StoriesEnum.Country.ToString(),  
                                          Stories.StoriesEnum.Date1.ToString(),  
                                          Stories.StoriesEnum.Date2.ToString(),
                                          Stories.StoriesEnum.ModifiedBy.ToString(),  
                                          Stories.StoriesEnum.ModifiedDate.ToString(),  
                                        };


                    //sets the types of parameters
                    DbType[] enumType = { DbType.Int64,
                                          DbType.String,
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.Int64,
                                          DbType.Int64,
                                          DbType.DateTime,
                                          DbType.DateTime,
                                          DbType.Int64,
                                          DbType.DateTime,
                                        };

                    object[] objTributeVal = { objTribute.TributeId,
                                               objTribute.TributeName,
                                               objTribute.TributeImage,
                                               objTribute.City, 
                                               objTribute.State,
                                               objTribute.Country,
                                               objTribute.Date1,
                                               objTribute.Date2,
                                               objTribute.ModifiedBy,
                                               objTribute.ModifiedDate 
                                             };
                    UpdateRecord("usp_UpdateTributeDetail", strParam, enumType, objTributeVal);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objTribute.CustomError = objError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //UpdateObituaryDetail
        public void UpdateObituaryDetail(Stories objsTribute)
        {
            try
            {
                if (objsTribute != null)
                {
                    //sets the parameters
                    string[] strParam = { Stories.ObituaryMaintainState.TributeId.ToString(),  
                                          Stories.ObituaryMaintainState.PostMessage.ToString(),  
                                          Stories.ObituaryMaintainState.MessageWithoutHtml.ToString(), 
                                          Stories.ObituaryMaintainState.MessageAddedModifiedBy.ToString(),
                                        };


                    //sets the types of parameters
                    DbType[] enumType = { DbType.Int64,
                                          DbType.String,
                                          DbType.String,
                                          DbType.Int64,
                                        };

                    object[] objTributeVal = { objsTribute.TributeId,
                                               objsTribute.PostMessage,
                                               objsTribute.MessageWithoutHtml,
                                               objsTribute.MessageAddedModifiedBy
                                             };
                    UpdateRecordMinusIovs("usp_UpdateObituaryDetail", strParam, enumType, objTributeVal);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objsTribute.CustomError = objError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        ///  This method will Add and update the Story detail and also add new topic in the more about section
        /// </summary>
        /// 
        /// <param name="objStory">Stories object which contain the story detail which user want to update
        /// </param>
        public void UpdateStoryDetail(Stories objStory)
        {
            
            try
            {
                if (objStory != null)
                {
                    if (objStory.Operation == "Add")
                    {
                        InsertStory(objStory);
                    }
                    else if (objStory.Operation == "Update")
                    {
                        UpdateStory(objStory);
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objStory.CustomError = objError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        /// <summary>
        /// this Method will get the Topic list
        /// </summary>
        /// 
        /// <param name="objStoryParam">An object which contain the Tribute type for which wants to  
        ///                             get the Topic list
        /// </param>
        /// 
        /// <returns>This method will return the StoryMoreAbout object which contain the list of topic
        /// </returns>
        public IList<StoryMoreAbout> GetTopic(object[] objStory)
        {
            List<StoryMoreAbout> lstTopic = new List<StoryMoreAbout>();

            try
            {
                if (objStory != null)
                {
                    DataSet dsTopic = new DataSet();
                    dsTopic = GetDataSet("usp_GetTopicList", objStory);

                    if (dsTopic.Tables.Count > 0)
                    {
                        if (dsTopic.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drTopic in dsTopic.Tables[0].Rows)
                            {
                                if (drTopic["Topic"].ToString() != "")
                                {
                                    StoryMoreAbout objTopic = new StoryMoreAbout();
                                    objTopic.SecondaryTitle = drTopic["Topic"].ToString();

                                    lstTopic.Add(objTopic);
                                    objTopic = null;
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;

                    StoryMoreAbout objTopic = new StoryMoreAbout();
                    objTopic.CustomError = objError;

                    lstTopic.Add(objTopic);
                    objTopic = null;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstTopic;
        }

        
        /// <summary>
        /// This method will delete the Topic from the More about section
        /// </summary>
        /// <param name="objStory">stories object which contain the Section id and Userbioagraphy ID
        ///                        of the topic which topic user wants to delete
        /// </param>
        public void DeleteTopic(Stories objStory)
        {
            try
            {
                if (objStory != null)
                {
                    object[] objStoryParam = { objStory.MoreAboutSection[0].SectionId, objStory.MoreAboutSection[0].UserBiographyId };

                    Delete("usp_DeleteTopic", objStoryParam);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objStory.CustomError = objError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Function to get the list of Tribute Administrators for the Tribute id
        /// </summary>
        /// <param name="objUserTributeId">Entity containing User Tribute id</param>
        /// <returns>List of Administrators constaing email ids</returns>
        public UserInfo GetTributeAdministrators(int TribuetId, string moduleType)
        {
            UserInfo objUser = new UserInfo();
            object[] objParam = { TribuetId, moduleType };
            DataSet dsTributeAdmins = GetDataSet("usp_GetTributeAdministrators", objParam);

            //to fill records in entity
            if (dsTributeAdmins.Tables.Count > 0)
            {
                if (dsTributeAdmins.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTributeAdmins.Tables[0].Rows)
                    {
                        if (dr["Email"].ToString() != "")
                        {
                            if (objUser.UserEmail != null)
                            {
                                objUser.UserEmail += ", ";
                            }

                            objUser.UserEmail += dr["Email"].ToString();
                        }
                    }
                }
            }
            return objUser;
        }

        /// <summary>
        /// Method to get list of users who have added the tribute to their list of favourites
        /// </summary>
        /// <param name="TribuetId"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public UserInfo GetFavouriteTributeUsers(int TribuetId, string moduleType)
        {
            UserInfo objUser = new UserInfo();
            object[] objParam = { TribuetId, moduleType };
            DataSet dsTributeFav = GetDataSet("usp_GetFavouriteTributes", objParam);

            //to fill records in entity
            if (dsTributeFav.Tables.Count > 0)
            {
                if (dsTributeFav.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTributeFav.Tables[0].Rows)
                    {
                        if (dr["Email"].ToString() != "")
                        {
                            if (objUser.UserEmail != null)
                            {
                                objUser.UserEmail += ", ";
                            }

                            objUser.UserEmail += dr["Email"].ToString();
                        }
                    }
                }
            }
            return objUser;
        }


        public Stories ClaculateAge(Stories objStory)
        {
            Stories objStoryDetail = new Stories ();

            try
            {
                if (objStory != null)
                {
                    object[] objStoryParam = { objStory.Date1, objStory.Date2,objStory.Age,0 ,0};

                    if (objStoryParam != null)
                    {
                        DataSet dsStory = new DataSet();

                        dsStory = GetDataSet("uspCalculateAge", objStoryParam);
                        objStoryDetail.Age = dsStory.Tables[0].Rows[0][0].ToString(); //int.Parse (dsStory.Tables[0].Rows[0][0].ToString());
                    }
                }

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objStory.CustomError = objError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objStoryDetail;
        }

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// This method will populate the stories object from the dataset
        /// </summary>
        /// <param name="dsStory">A Dataset which contain story data</param>
        /// <returns>This method will return the Stories object populated with the story detail</returns>
        private Stories PopulateStoryObject(DataSet dsStory)
        {
            try
            {
                Stories objStory = new Stories();

                // Get the User role for the tribute
                if (dsStory.Tables.Count > 0)
                {
                    if (dsStory.Tables[0].Rows.Count > 0)
                    {
                        DataRow drAdmin = dsStory.Tables[0].Rows[0];
                        if (int.Parse(drAdmin["IsAdmin"].ToString()) == 0)
                        {
                            objStory.IsAdmin = false;
                        }
                        else
                        {
                            objStory.IsAdmin = true;
                        }
                    }
                }

                // Get the Tribute Detail
                if (dsStory.Tables.Count > 1)
                {
                    if (dsStory.Tables[1].Rows.Count > 0)
                    {
                        DataRow drStory = dsStory.Tables[1].Rows[0];

                        objStory.TributeName = drStory["TributeName"].ToString();
                        objStory.TributeType = drStory["TributeType"].ToString();
                        objStory.TributeImage = drStory["TributeImage"].ToString();
                        if ( (drStory["Date1"] != null) && (drStory["Date1"].ToString() != ""))
                        {
                            objStory.Date1 = DateTime.Parse(drStory["Date1"].ToString());
                        }
                        if ( (drStory["Date2"] != null) && (drStory["Date2"].ToString() != ""))
                        {
                            objStory.Date2 = DateTime.Parse(drStory["Date2"].ToString());
                        }

                        objStory.PostMessage = drStory["PostMessage"].ToString();
                        objStory.MessageWithoutHtml = drStory["MessageWithoutHtml"].ToString();

                        objStory.City = drStory["City"].ToString();
                        if ((drStory["State"] != null) && (drStory["State"].ToString() != ""))
                        {
                            objStory.State = int.Parse(drStory["State"].ToString());
                        }
                        if ((drStory["Country"] != null) && (drStory["Country"].ToString() != ""))
                        {
                            objStory.Country = int.Parse(drStory["Country"].ToString());
                        }

                        objStory.StateName = drStory["StateName"].ToString();
                        objStory.CountryName = drStory["CountryName"].ToString();
                    }

                }

                // Get the Story and More About section
                List<StoryMoreAbout> objMoreAboutList = new List<StoryMoreAbout>();
                if (dsStory.Tables.Count > 2)
                {
                    if (dsStory.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow drStory in dsStory.Tables[2].Rows)
                        {
                            objMoreAboutList.Add(GetMoreAboutSection(drStory));
                        }
                        objStory.MoreAboutSection = objMoreAboutList;
                    }
                }

                return objStory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This Method will populate the StoryMoreAbout object from the Data row
        /// </summary>
        /// 
        /// <param name="drStory">A DataRow which contain the story detail</param>
        /// 
        /// <returns>This method will return the StoryMoreAbout object</returns>
        private StoryMoreAbout GetMoreAboutSection(DataRow drStory)
        {
            try
            {
                StoryMoreAbout objMoreAbout = new StoryMoreAbout();

                objMoreAbout.PrimaryTitle = drStory["PrimaryTitle"].ToString();
                objMoreAbout.SecondaryTitle = drStory["SecondaryTitle"].ToString();
                objMoreAbout.SectionAnswer = drStory["SectionAnswer"].ToString().Replace("\n", "<br />");
                objMoreAbout.UserId = int.Parse(drStory["UserId"].ToString());
                objMoreAbout.SectionId = int.Parse(drStory["SectionId"].ToString());
                objMoreAbout.UserBiographyId = int.Parse(drStory["UserBiographyId"].ToString());

                return objMoreAbout;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will update the story in database
        /// </summary>
        /// 
        /// <param name="objStory">A Stories object which contain the stories detail which have to update</param>
        private void UpdateStory(Stories objStory)
        {
            try
            {
                // sets the name of the parameters
                string[] strParam = { StoryMoreAbout.StoriesMoreAboutEnum.SectionId.ToString(),  
                                      StoryMoreAbout.StoriesMoreAboutEnum.UserBiographyId.ToString(),
                                      StoryMoreAbout.StoriesMoreAboutEnum.SecondaryTitle.ToString(),
                                      StoryMoreAbout.StoriesMoreAboutEnum.SectionAnswer.ToString(),
                                      StoryMoreAbout.StoriesMoreAboutEnum.ModifiedBy.ToString(),
                                      StoryMoreAbout.StoriesMoreAboutEnum.ModifiedDate.ToString()
                                    };


                // sets the types of parameters
                DbType[] enumType = { DbType.Int64,
                                      DbType.Int64,
                                      DbType.String, 
                                      DbType.String, 
                                      DbType.Int64,
                                      DbType.DateTime,
                                    };

                // sets the value of the paramter
                object[] objStoryParam = { objStory.MoreAboutSection[0].SectionId,
                                           objStory.MoreAboutSection[0].UserBiographyId,
                                           objStory.MoreAboutSection[0].SecondaryTitle,
                                           objStory.MoreAboutSection[0].SectionAnswer, 
                                           objStory.MoreAboutSection[0].ModifiedBy,
                                           objStory.MoreAboutSection[0].ModifiedDate 
                                         };

                // call stored procedure to update the story
                UpdateRecord("usp_UpdateStory", strParam, enumType, objStoryParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        ///  This method will Insert the story in database
        /// </summary>
        /// 
        /// <param name="objStory">A Stories object which contain the stories detail which have to add
        /// </param>
        private void InsertStory(Stories objStory)
        {

            try
            {
                // sets the name of the parameters
                string[] strParam = { Stories.StoriesEnum.UserId.ToString(),  
                                      Stories.StoriesEnum.TributeId.ToString(),
                                      StoryMoreAbout.StoriesMoreAboutEnum.PrimaryTitle.ToString(),
                                      StoryMoreAbout.StoriesMoreAboutEnum.SecondaryTitle.ToString(),
                                      StoryMoreAbout.StoriesMoreAboutEnum.SectionAnswer.ToString(),
                                      StoryMoreAbout.StoriesMoreAboutEnum.CreatedBy.ToString(),
                                      StoryMoreAbout.StoriesMoreAboutEnum.CreatedDate.ToString()
                                    };


                // sets the types of parameters
                DbType[] enumType = { DbType.Int64,
                                      DbType.Int64,
                                      DbType.String, 
                                      DbType.String, 
                                      DbType.String,
                                      DbType.Int64,
                                      DbType.DateTime,
                                    };

                // sets the value of the paramter
                object[] objValue = { objStory.UserId,
                                      objStory.TributeId,
                                      objStory.MoreAboutSection[0].PrimaryTitle,
                                      objStory.MoreAboutSection[0].SecondaryTitle,
                                      objStory.MoreAboutSection[0].SectionAnswer, 
                                      objStory.MoreAboutSection[0].CreatedBy,
                                      objStory.MoreAboutSection[0].CreatedDate 
                                    };

                // call stored procedure to save the story
                InsertRecord("usp_InsertStory", strParam, enumType, objValue);
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
