///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.GiftResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Gifts
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Collections.ObjectModel;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;
using System.Xml;

#endregion

/// <summary>
///Tribute Portal-Gifts Resource Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the Resource Access class for the Gifts which is responsible for following opertion in the database
// 1) Add Gifts
// 2) View Gifts
// 3) Delete Gifts
/// </summary>
/// 
namespace TributesPortal.ResourceAccess
{
    public class GiftResource : PortalResourceAccess 
    {
        #region METHODS

        #region PUBLIC METHODS

        /// <summary>
        ///  This method will get the Gift detail from database
        /// </summary>
        /// 
        /// <param name="objGift">This is the Gift object which contain the Tribute ID to get 
        ///the Gift for that tribute and user ID to get that user is admin or not for that tribute </param>
        /// 
        /// <returns> This method will return the List of Gifts </returns>
        public List<Gifts> GetGifts(Gifts objGift)
        {
            List<Gifts> lstGifts = null;

            try
            {
                if (objGift != null)
                {
                    object[] objGiftParam = { objGift.TributeId, objGift.UserId, objGift.PageSize, objGift.CurrentPage  };

                    if (objGiftParam != null)
                    {
                        DataSet dsGifts = new DataSet();

                        dsGifts = GetDataSet("usp_GetGift", objGiftParam);

                        lstGifts = PopulateGiftObject(dsGifts, objGift);
                    }
                }

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objGift.CustomError = objError;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstGifts;
        }


        /// <summary>
        /// This Method will get the Image list for the passed Tribute Type
        /// </summary>
        /// <param name="objGiftImage">An object which contain the Tribute type for which wants to  
        ///                             get the Image list</param>
        /// <returns>This method will return the Gifts object which contain the list of Image</returns>
        public List<GiftImage> GetImage(GiftImage objGiftImage)
        {
            List<GiftImage> lstGifts = null;

            try
            {
                if (objGiftImage != null)
                {
                    object[] objGiftParam = { objGiftImage.TributeType };

                    if (objGiftParam != null)
                    {
                        DataSet dsGifts = new DataSet();

                        dsGifts = GetDataSet("usp_GetImage", objGiftParam);

                        lstGifts = PopulateGiftImageObject(dsGifts);
                    }
                }

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objGiftImage.CustomError = objError;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstGifts;
        }


        /// <summary>
        /// This method will Add the Gift detail
        /// </summary>
        /// <param name="objGift">Gift object which contain the Gift detail which user want to Add</param>
        public object InsertGift(Gifts objGift)
        {
            object Identity = new object();

            try
            {
                if (objGift != null)
                {
                    // sets the name of the parameters
                    string[] strParam = { Gifts.GiftEnum.UserId.ToString(),  
                                          Gifts.GiftEnum.TributeId.ToString(),
                                          Gifts.GiftEnum.ImageUrl.ToString(),
                                          Gifts.GiftEnum.GiftMessage.ToString(),
                                          Gifts.GiftEnum.GiftSentBy.ToString(),
                                          Gifts.GiftEnum.CreatedBy.ToString(),
                                          Gifts.GiftEnum.CreatedDate.ToString()
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
                    object[] objValue = { objGift.UserId,
                                          objGift.TributeId,
                                          objGift.ImageUrl,
                                          objGift.GiftMessage,
                                          objGift.GiftSentBy, 
                                          objGift.CreatedBy,
                                          DateTime.Now
                                        };

                    // call stored procedure to save the story
                    Identity = InsertDataAndReturnId("usp_InsertGift", strParam, enumType, objValue);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objGift.CustomError = objError;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Identity;
        }


        /// <summary>
        /// This method will delete the Gift
        /// </summary>
        /// <param name="objGift">Gift object which contain the Giftid which Gift user wants to delete</param>
        public void DeleteGift(Gifts objGift)
        {
            try
            {
                if (objGift != null)
                {
                    object[] objGiftParam = { objGift.GiftId, objGift.UserId };

                    Delete("usp_DeleteGift", objGiftParam);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objGift.CustomError = objError;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// This method will populate the Gift object from the dataset
        /// </summary>
        /// <param name="dsGift">A Dataset object which contain Gift data</param>
        /// <param name="objGift"></param>
        /// <returns>This method will return the List of Gift object populated with the All Gift detail</returns>
        private List<Gifts> PopulateGiftObject(DataSet dsGift, Gifts objGift)
        {
            try
            {
                List<Gifts> lstGifts = new List<Gifts>();

                // Get the User role for the tribute
                if (dsGift.Tables.Count > 0)
                {
                    if (dsGift.Tables[0].Rows.Count > 0)
                    {
                        DataRow drAdmin = dsGift.Tables[0].Rows[0];
                        if (int.Parse(drAdmin["IsAdmin"].ToString()) == 0)
                        {
                            objGift.IsAdmin = false;
                        }
                        else
                        {
                            objGift.IsAdmin = true;
                        }
                    }
                }

                // Get the Tribute Detail
                if (dsGift.Tables.Count > 1)
                {
                    if (dsGift.Tables[1].Rows.Count > 0)
                    {
                        DataRow drTribute = dsGift.Tables[1].Rows[0];

                        objGift.TributeType = drTribute["TributeType"].ToString();
                        objGift.TributeName = drTribute["TributeName"].ToString();
                    }
                }

                string virtalPath = GetPath();

                // Get the Gift Detail
                if (dsGift.Tables.Count > 2)
                {
                    if (dsGift.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow drGift in dsGift.Tables[2].Rows)
                        {
                            Gifts tmpGift = new Gifts();

                            tmpGift.GiftId = int.Parse(drGift["GiftId"].ToString());
                            if ((drGift["UserId"] != null) && (drGift["UserId"].ToString() != ""))
                            {
                                tmpGift.UserId = int.Parse(drGift["UserId"].ToString());
                            }
                            else
                            {
                                tmpGift.UserId = 0;
                            }

                            tmpGift.ImageUrl = virtalPath + drGift["ImageUrl"].ToString();

                            //if user is not registered User
                            if ((drGift["UserId"] == null) || (drGift["UserId"].ToString() == ""))
                            {
                                tmpGift.GiftSentBy = GetUserName(drGift);
                                tmpGift.UserName = "";
                            }
                            else
                            {
                                tmpGift.UserName = GetUserName(drGift);
                                tmpGift.GiftSentBy = "";
                            }

                            // Replace "\n" by "</br>" in Gift message
                            if (drGift["GiftMessage"].ToString() != string.Empty)
                            {
                                tmpGift.GiftMessage = drGift["GiftMessage"].ToString().Replace("\n", "<br />"); ;
                            }
                            else
                            {
                                tmpGift.GiftMessage = "";
                            }

                            string date1 = DateTime.Parse(drGift["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");
                            string time = DateTime.Parse(drGift["CreatedDate"].ToString()).ToString("hh:mm tt");

                            tmpGift.CreatedDate = "at " + time + " on " + date1 + ".";
                            tmpGift.Location = GetLocation(drGift);

                            objGift.TotalRecordCount = int.Parse(drGift["TotalRecords"].ToString());
                            objGift.TributeId = int.Parse(drGift["TributeId"].ToString());

                            lstGifts.Add(tmpGift);
                            tmpGift = null;
                        }
                    }

                }

                return lstGifts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will combine the City State and country to get the location
        /// If IsLocationHide Attribute is false maens Privacy Setting is on to get the loaction
        /// </summary>
        /// <param name="drGift">A DataRow object which contain the City, State and Country</param>
        /// <returns>This method will return the string object which contain the location</returns>
        private string GetLocation(DataRow drGift)
        {
            string location = "";

            try
            {
                // If Privacy Setting IsLocationHide is false then get the location
                if ((drGift["UserId"] == null) || (drGift["UserId"].ToString() == ""))
                {
                    return location;
                }
                else
                {
                    if (bool.Parse(drGift["IsLocationHide"].ToString()) == false)
                    {
                        location = "(";
                        if (drGift["City"].ToString() != "" && drGift["State"].ToString() != "")
                        {
                            location += drGift["City"].ToString() + ", " + drGift["State"].ToString() + ", " + drGift["Country"].ToString();
                        } else if (drGift["City"].ToString() == "" && drGift["State"].ToString() != "")
                        {
                            location += drGift["State"].ToString() + ", " + drGift["Country"].ToString();
                        }
                        else if (drGift["City"].ToString() != "" && drGift["State"].ToString() == "")
                        {
                            location += drGift["City"].ToString() + ", " + drGift["Country"].ToString();
                        }
                        else
                        {
                            location += drGift["Country"].ToString();
                        }
                        location += ")";
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return location;
        }


        /// <summary>
        /// This method will get the userName on the basis of the attribute IsUserNameVisiable
        /// </summary>
        /// <param name="drGift">A DataRow object which contain the City, State and Country</param>
        /// <returns>This method will return the string object which contain the userName</returns>
        private string GetUserName(DataRow drGift)
        {
            string userName = "";

            try
            {
                //if user is not registered User
                if ((drGift["UserId"] == null) || (drGift["UserId"].ToString() == ""))
                {
                    // If name of the person exist means Anonymous user add the Gift
                    if ((drGift["GiftSentBy"] != null) && (drGift["GiftSentBy"].ToString() != ""))
                    {
                        userName = drGift["GiftSentBy"].ToString();
                    }
                    // If Name of the person not exist means Anonymous user add the Gift
                    // but name is not entered then display the Unregistered user
                    else
                    {
                        userName = "Unregistered User";
                    }
                }
                else //if user is registered User
                {
              
                    // if IsUserNameVisiable is True then get the userName
                    if (bool.Parse(drGift["IsUserNameVisiable"].ToString()) == true)
                    {
                        // if user name exist means Registered user add the gift then get teh userName
                        if ((drGift["UserName"] != null) && (drGift["UserName"].ToString() != ""))
                        {
                            userName = drGift["UserName"].ToString();
                        }
                    }
                    // if IsUserNameVisiable is false then get the Name of the user
                    else
                    {
                        // if User is Personal user then get the "FirstName + LastName"
                        if ((drGift["UserType"] != null) && (int.Parse(drGift["UserType"].ToString()) == 1))
                        {
                            userName = drGift["FirstName"].ToString() + " " + drGift["LastName"].ToString();
                        }
                        // if User is Business user then get the "CompanyName"
                        else if ((drGift["UserType"] != null) && (int.Parse(drGift["UserType"].ToString()) == 2))
                        {
                            userName = drGift["CompanyName"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userName;
        }


        /// <summary>
        /// This method will populate the Gift Image object from the dataset
        /// </summary>
        /// <param name="dsGiftImage">A Dataset object which contain Gift Image data</param>
        /// <returns>This method will return the List of Gift Image object populated with the All Image</returns>
        private List<GiftImage> PopulateGiftImageObject(DataSet dsGiftImage)
        {
            try
            {
                List<GiftImage> lstGiftsImage = new List<GiftImage>();

                string virtalPath = GetPath();

                // Get the User role for the tribute
                if (dsGiftImage.Tables.Count > 0)
                {
                    if (dsGiftImage.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drGift in dsGiftImage.Tables[0].Rows)
                        {
                            GiftImage objGiftImage = new GiftImage();

                            objGiftImage.ImageId = int.Parse(drGift["ImageId"].ToString());
                            objGiftImage.ImageUrl = virtalPath + drGift["ImageUrl"].ToString();

                            lstGiftsImage.Add(objGiftImage);
                            objGiftImage = null;
                        }
                    }
                }

                return lstGiftsImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
