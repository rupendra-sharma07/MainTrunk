///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.VideoResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Videos
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.ResourceAccess
{
    /// <summary>
    ///Tribute Portal-Facade Manager Timeless Software
    ///Resource Access Class for Video.
    //============================================
    // Copyright © Timeless Software  All rights reserved.

    public partial class VideoResource : PortalResourceAccess, IResourceAccess
    {
        #region METHODS

        //Agnesh Arya-- To get tributes personal details over edit modal popup

        public Tributes GetEditTributeFieldDetails(int tributeId)
        {
            try
            {
                Tributes objTrb = new Tributes();
                if (!Equals(tributeId, null))
                {
                    object[] objParam = { tributeId };
                    DataSet dsTribute = GetDataSet("usp_GetEditTributeFieldDetails", objParam);

                    if (dsTribute.Tables[0].Rows.Count > 0)
                    {
                        objTrb.TributeImage = dsTribute.Tables[0].Rows[0]["TributeImage"].ToString();
                        objTrb.TributeName = dsTribute.Tables[0].Rows[0]["TributeName"].ToString();
                        // objTrb.TributeType = int.Parse(dsTribute.Tables[0].Rows[0]["TributeType"].ToString());
                        if (!((dsTribute.Tables[0].Rows[0]["Date1"] == null) || (dsTribute.Tables[0].Rows[0]["Date1"].ToString() == "")))
                        {
                            objTrb.Date1 = DateTime.Parse(dsTribute.Tables[0].Rows[0]["Date1"].ToString());
                        }
                        objTrb.Date2 = DateTime.Parse(dsTribute.Tables[0].Rows[0]["Date2"].ToString());

                        if (!(dsTribute.Tables[0].Rows[0]["City"] == null || dsTribute.Tables[0].Rows[0]["City"].ToString() == ""))
                            objTrb.City = dsTribute.Tables[0].Rows[0]["City"].ToString();
                        else
                            objTrb.City = "";
                        if (!(dsTribute.Tables[0].Rows[0]["State"] == null || dsTribute.Tables[0].Rows[0]["State"].ToString() == ""))
                            objTrb.State = int.Parse(dsTribute.Tables[0].Rows[0]["State"].ToString());

                        objTrb.Country = int.Parse(dsTribute.Tables[0].Rows[0]["Country"].ToString());


                    }
                    else
                    {
                        objTrb = null;
                    }
                }
                return objTrb;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Method to insert the record for Video in database.
        /// </summary>
        /// <param name="objVid">Filled video entity</param>
        /// <returns>Object containing the identity of the record inserted or Error message in case of error.</returns>
        public object InsertDataAndReturnId(object[] objVid)
        {
            Videos objVideo = (Videos)objVid[0];
            object Identity = objVideo;

            if (!Equals(objVideo, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {Videos.VideoEnum.UserId.ToString(), Videos.VideoEnum.UserTributeId.ToString(), 
                                       Videos.VideoEnum.VideoTypeId.ToString(), Videos.VideoEnum.VideoCaption.ToString(), Videos.VideoEnum.VideoDesc.ToString(),
                                        Videos.VideoEnum.VideoUrl.ToString(), Videos.VideoEnum.TributeVideoId.ToString(), Videos.VideoEnum.CreatedBy.ToString(),
                                        Videos.VideoEnum.CreatedDate.ToString(), Videos.VideoEnum.ModifiedBy.ToString(), Videos.VideoEnum.ModifiedDate.ToString(),
                                        Videos.VideoEnum.IsActive.ToString(), Videos.VideoEnum.IsDeleted.ToString()
                                    };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64, DbType.Int64,
                                    DbType.String, DbType.String, DbType.String,
                                    DbType.String, DbType.String, DbType.Int64,
                                    DbType.DateTime, DbType.Int64, DbType.DateTime,
                                    DbType.Boolean, DbType.Boolean};
                    //sets the values in the entity to the parameters
                    object[] objValue = { objVideo.UserId, objVideo.UserTributeId, 
                                        objVideo.VideoTypeId, objVideo.VideoCaption, objVideo.VideoDesc,
                                        objVideo.VideoUrl, objVideo.TributeVideoId, objVideo.CreatedBy,
                                        objVideo.CreatedDate, objVideo.ModifiedBy, objVideo.ModifiedDate,
                                        objVideo.IsActive, objVideo.IsDeleted};

                    //sends request to insert record and get the identity of the record inserted
                    //Identity = base.InsertDataAndReturnId("usp_InsertVideo", strParam, dbType, objValue);
                    Identity = base.InsertDataAndReturnIdMinusIOVS("usp_InsertVideo", strParam, dbType, objValue);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        Errors objError = new Errors();
                        objError.ErrorMessage = sqlEx.Message;
                        objVideo.CustomError = objError;
                        //return Identity;
                    }
                }
            }
            return Identity;
        }

        /// <summary>
        /// Method to get the list of videos in the tribute
        /// </summary>
        /// <param name="objVideoGallery">Video Gallery entity conatining UserTributeId </param>
        /// <returns>List of Videos</returns>
        public List<VideoGallery> GetVideoGalleryRecords(object[] objVideoGallery)
        {
            try
            {
                VideoGallery objVideoGal = (VideoGallery)objVideoGallery[0];
                List<VideoGallery> objListVideo = new List<VideoGallery>();
                if (!Equals(objVideoGal, null))
                {
                    object[] objParam = { objVideoGal.Videos.UserTributeId,
                                            objVideoGal.PageSize,
                                            objVideoGal.PageNumber                                            
                                        };
                    DataSet dsVideoGallery = GetDataSet("usp_GetVideosForTribute", objParam);

                    //to fill records in the Video Gallery list
                    if (dsVideoGallery.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsVideoGallery.Tables[0].Rows)
                        {
                            VideoGallery objGalleryVideo = new VideoGallery();
                            Videos objVideo = new Videos();
                            objVideo.VideoId = int.Parse(dr["VideoId"].ToString());
                            objVideo.UserTributeId = int.Parse(dr["UserTributeId"].ToString());
                            objVideo.VideoTypeId = dr["VideoTypeId"].ToString();
                            objVideo.VideoUrl = dr["VideoUrl"].ToString();
                            objVideo.VideoCaption = dr["VideoCaption"].ToString();
                            objVideo.UserId = int.Parse(dr["UserId"].ToString());
                            //to get the VideoId from Url 
                            if ((objVideo.VideoTypeId == null) || (objVideo.VideoTypeId == string.Empty))
                            {
                                if (objVideo.VideoUrl != string.Empty || objVideo.VideoUrl != null)
                                {
                                    if (!objVideo.VideoUrl.Contains("&"))
                                    {
                                        objGalleryVideo.IdForDisplay = objVideo.VideoUrl.Substring(objVideo.VideoUrl.IndexOf("v=", 0) + 2, objVideo.VideoUrl.Length - objVideo.VideoUrl.IndexOf("v=", 0) - 2);
                                    }
                                    else
                                    {
                                        string strSubValue = objVideo.VideoUrl.Substring(objVideo.VideoUrl.IndexOf("v=", 0) + 2, objVideo.VideoUrl.Length - objVideo.VideoUrl.IndexOf("v=", 0) - 2);
                                        objGalleryVideo.IdForDisplay = strSubValue.Substring(0, strSubValue.IndexOf("&"));
                                    }
                                }
                            }
                            else
                            {
                                objGalleryVideo.IdForDisplay = objVideo.VideoTypeId;
                            }

                            objGalleryVideo.Videos = objVideo;
                            objGalleryVideo.UserName = dr["UserName"].ToString();
                            objGalleryVideo.CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");
                            objGalleryVideo.CommentCount = int.Parse(dr["CommentCount"].ToString());
                            objGalleryVideo.TotalRecords = int.Parse(dr["TotalRecords"].ToString());

                            objListVideo.Add(objGalleryVideo);
                            objGalleryVideo = null;
                            objVideo = null;
                        }
                    }
                }
                return objListVideo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get the video details for the tribute
        /// </summary>
        /// <param name="objVideoGallery"></param>
        /// <returns></returns>
        public VideoGallery GetVideoDetails(object[] objVideoGallery)
        {
            try
            {
                VideoGallery objVideoGal = (VideoGallery)objVideoGallery[0];
                if (!Equals(objVideoGal, null))
                {
                    object[] objParam = { objVideoGal.Videos.VideoId,
                                            objVideoGal.Videos.UserId,
                                            objVideoGal.NextPrev                                           
                                        };
                    DataSet dsVideo = GetDataSet("usp_GetVideoDetails", objParam);

                    if (dsVideo.Tables[0].Rows.Count > 0)
                    {
                        //VideoGallery objGalleryVideo = new VideoGallery();
                        Videos objVideo = new Videos();
                        objVideo.VideoId = int.Parse(dsVideo.Tables[0].Rows[0]["VideoId"].ToString());
                        objVideo.UserTributeId = int.Parse(dsVideo.Tables[0].Rows[0]["UserTributeId"].ToString());
                        objVideo.VideoTypeId = dsVideo.Tables[0].Rows[0]["VideoTypeId"].ToString();
                        //agnesh :objVideo.VideoTypeId = "7";
                        objVideo.VideoUrl = dsVideo.Tables[0].Rows[0]["VideoUrl"].ToString();
                        objVideo.VideoCaption = dsVideo.Tables[0].Rows[0]["VideoCaption"].ToString();
                        objVideo.VideoDesc = dsVideo.Tables[0].Rows[0]["VideoDesc"].ToString();
                        objVideo.UserId = int.Parse(dsVideo.Tables[0].Rows[0]["UserId"].ToString());
                        objVideo.TributeVideoId = dsVideo.Tables[0].Rows[0]["TributeVideoId"].ToString();
                        //to get the VideoId from Url 
                        if ((objVideo.VideoTypeId == null) || (objVideo.VideoTypeId == string.Empty))
                        {
                            if (objVideo.VideoUrl != null)
                            {
                                if (objVideo.VideoUrl != string.Empty)
                                {
                                    if (!objVideo.VideoUrl.Contains("&"))
                                    {
                                        objVideoGal.IdForDisplay = objVideo.VideoUrl.Substring(objVideo.VideoUrl.IndexOf("v=", 0) + 2, objVideo.VideoUrl.Length - objVideo.VideoUrl.IndexOf("v=", 0) - 2);
                                    }
                                    else
                                    {
                                        string strSubValue = objVideo.VideoUrl.Substring(objVideo.VideoUrl.IndexOf("v=", 0) + 2, objVideo.VideoUrl.Length - objVideo.VideoUrl.IndexOf("v=", 0) - 2);
                                        objVideoGal.IdForDisplay = strSubValue.Substring(0, strSubValue.IndexOf("&"));
                                    }
                                }
                            }
                        }
                        else if ((objVideo.VideoTypeId != null) || (objVideo.VideoTypeId != string.Empty))
                        {
                            objVideoGal.IdForDisplay = objVideo.VideoTypeId;
                        }
                        else
                        {
                            objVideoGal.IdForDisplay = string.Empty;
                        }

                        objVideoGal.Videos = objVideo;
                        objVideoGal.UserName = dsVideo.Tables[0].Rows[0]["UserName"].ToString();
                        //objVideoGal.TotalRecords = int.Parse(dsVideo.Tables[0].Rows[0]["TotalRecords"].ToString());
                        objVideoGal.Videos.CreatedBy = int.Parse(dsVideo.Tables[0].Rows[0]["CreatedBy"].ToString());
                        objVideoGal.NextRecordCount = int.Parse(dsVideo.Tables[0].Rows[0]["NextCount"].ToString());
                        objVideoGal.PrevRecordCount = int.Parse(dsVideo.Tables[0].Rows[0]["PrevCount"].ToString());
                        objVideoGal.IsAdmin = int.Parse(dsVideo.Tables[0].Rows[0]["IsAdmin"].ToString());
                        objVideoGal.RecordNumber = int.Parse(dsVideo.Tables[0].Rows[0]["RecordNumber"].ToString());
                        objVideoGal.TotalRecords = int.Parse(dsVideo.Tables[0].Rows[0]["TotalRecords"].ToString());
                        objVideoGal.CreatedDate = DateTime.Parse(dsVideo.Tables[0].Rows[0]["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");
                        objVideoGal.CommentCount = int.Parse(dsVideo.Tables[0].Rows[0]["CommentCount"].ToString());
                    }
                }
                return objVideoGal;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Method to Update Video Details
        /// </summary>
        /// <param name="objVid">Filled Video Entity</param>
        public object UpdateVideoDetails(object[] objVid)
        {
            Videos objVideo = (Videos)objVid[0];
            //object Identity = objVideo;
            object objReturn = new object();
            if (!Equals(objVideo, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {Videos.VideoEnum.UserId.ToString(),
                                         Videos.VideoEnum.UserTributeId.ToString(),
                                        Videos.VideoEnum.VideoId.ToString(),  
                                        Videos.VideoEnum.VideoTypeId.ToString(), 
                                        Videos.VideoEnum.VideoCaption.ToString(),
                                        Videos.VideoEnum.VideoDesc.ToString(),
                                        Videos.VideoEnum.VideoUrl.ToString(), 
                                        Videos.VideoEnum.ModifiedBy.ToString(), 
                                        Videos.VideoEnum.ModifiedDate.ToString()
                                    };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64, 
                                       DbType.Int64,
                                       DbType.Int64,
                                       DbType.String,
                                       DbType.String, 
                                       DbType.String, 
                                       DbType.String,
                                       DbType.Int64,
                                       DbType.DateTime,
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue =    {objVideo.UserId,
                                            objVideo.UserTributeId,
                                            objVideo.VideoId,
                                            objVideo.VideoTypeId, 
                                            objVideo.VideoCaption, 
                                            objVideo.VideoDesc,
                                            objVideo.VideoUrl, 
                                            objVideo.ModifiedBy, 
                                            objVideo.ModifiedDate,
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_UpdateVideoDetails", strParam, dbType, objValue);
                    objReturn = null;
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        Errors objError = new Errors();
                        objError.ErrorMessage = "Failure"; //ex.Message;
                        objReturn = objError;
                    }
                }
            }
            return objReturn;
        }

        /// <summary>
        /// Method to delete video. This sets the IsDeleted Field to true.
        /// </summary>
        /// <param name="objVid">Video Entity conataining VideoId and IsDeleted flag</param>
        public void DeleteVideo(object[] objVid)
        {
            Videos objVideo = (Videos)objVid[0];

            if (!Equals(objVideo, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {Videos.VideoEnum.VideoId.ToString(),
                                            Videos.VideoEnum.UserId.ToString(),
                                            Videos.VideoEnum.IsDeleted.ToString() };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64,
                                        DbType.Int64,
                                        DbType.Boolean };
                    //sets the values in the entity to the parameters
                    object[] objValue = { objVideo.VideoId,
                                            objVideo.UserId,
                                            objVideo.IsDeleted };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_DeleteVideo", strParam, dbType, objValue);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        Errors objError = new Errors();
                        objError.ErrorMessage = sqlEx.Message;
                        objVideo.CustomError = objError;
                    }
                }
            }
        }

        /// <summary>
        /// Method to get the video tribute details for the tribute
        /// </summary>
        /// <param name="objVideoGallery"></param>
        /// <returns></returns>
        public VideoGallery GetVideoTributeDetails(object[] objTributeId)
        {
            try
            {
                Videos objVideoId = (Videos)objTributeId[0];
                VideoGallery objVideoGal = new VideoGallery();
                if (!Equals(objVideoId, null))
                {
                    object[] objParam = { objVideoId.UserTributeId };
                    DataSet dsVideo = GetDataSet("usp_GetVideoTribute", objParam);

                    if (dsVideo.Tables[0].Rows.Count > 0)
                    {
                        Videos objVideo = new Videos();
                        objVideo.VideoId = int.Parse(dsVideo.Tables[0].Rows[0]["VideoId"].ToString());
                        objVideo.UserTributeId = int.Parse(dsVideo.Tables[0].Rows[0]["UserTributeId"].ToString());
                        objVideo.VideoTypeId = dsVideo.Tables[0].Rows[0]["VideoTypeId"].ToString();
                        objVideo.VideoUrl = dsVideo.Tables[0].Rows[0]["VideoUrl"].ToString();
                        objVideo.VideoCaption = dsVideo.Tables[0].Rows[0]["VideoCaption"].ToString();
                        objVideo.VideoDesc = dsVideo.Tables[0].Rows[0]["VideoDesc"].ToString();
                        objVideo.UserId = int.Parse(dsVideo.Tables[0].Rows[0]["UserId"].ToString());
                        objVideo.TributeVideoId = dsVideo.Tables[0].Rows[0]["TributeVideoId"].ToString();
                        //to get the VideoId from Url 
                        /*if ((objVideo.VideoTypeId == null) || (objVideo.VideoTypeId == string.Empty))
                        {
                            if (objVideo.VideoUrl != string.Empty || objVideo.VideoUrl != null)
                            {
                                if (!objVideo.VideoUrl.Contains("&"))
                                {
                                    objVideoGal.IdForDisplay = objVideo.VideoUrl.Substring(objVideo.VideoUrl.IndexOf("v=", 0) + 2, objVideo.VideoUrl.Length - objVideo.VideoUrl.IndexOf("v=", 0) - 2);
                                }
                                else
                                {
                                    string strSubValue = objVideo.VideoUrl.Substring(objVideo.VideoUrl.IndexOf("v=", 0) + 2, objVideo.VideoUrl.Length - objVideo.VideoUrl.IndexOf("v=", 0) - 2);
                                    objVideoGal.IdForDisplay = strSubValue.Substring(0, strSubValue.IndexOf("&"));
                                }
                            }
                        }
                        else
                        {
                            objVideoGal.IdForDisplay = objVideo.VideoTypeId;
                        }*/

                        objVideoGal.Videos = objVideo;
                        objVideoGal.UserName = dsVideo.Tables[0].Rows[0]["UserName"].ToString();
                        objVideoGal.Videos.CreatedBy = int.Parse(dsVideo.Tables[0].Rows[0]["CreatedBy"].ToString());
                        objVideoGal.CreatedDate = DateTime.Parse(dsVideo.Tables[0].Rows[0]["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");
                        objVideoGal.CommentCount = int.Parse(dsVideo.Tables[0].Rows[0]["CommentCount"].ToString());
                    }
                    else
                    {
                        objVideoGal = null;
                    }
                }
                return objVideoGal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to Update Video Tribute Details
        /// </summary>
        /// <param name="objVid">Filled Video Entity</param>
        public object UpdateVideoTributeDetails(object[] objVid)
        {
            Videos objVideo = (Videos)objVid[0];
            //object Identity = objVideo;
            object objReturn = new object();
            if (!Equals(objVideo, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {Videos.VideoEnum.UserId.ToString(),
                                         Videos.VideoEnum.UserTributeId.ToString(),
                                        Videos.VideoEnum.VideoId.ToString(),  
                                        Videos.VideoEnum.VideoCaption.ToString(),
                                        Videos.VideoEnum.VideoDesc.ToString(),
                                        Videos.VideoEnum.ModifiedBy.ToString(), 
                                        Videos.VideoEnum.ModifiedDate.ToString()
                                    };
                    //sets the types of parameters
                    DbType[] dbType = { DbType.Int64,
                                        DbType.Int64,
                                        DbType.Int64,
                                        DbType.String,
                                        DbType.String, 
                                        DbType.Int64,
                                        DbType.DateTime,
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue =    {objVideo.UserId,
                                            objVideo.UserTributeId,
                                            objVideo.VideoId,
                                            objVideo.VideoCaption, 
                                            objVideo.VideoDesc,
                                            objVideo.ModifiedBy, 
                                            objVideo.ModifiedDate,
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_UpdateVideoTributeDetails", strParam, dbType, objValue);
                    objReturn = null;
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        Errors objError = new Errors();
                        objError.ErrorMessage = "Failure"; //ex.Message;
                        objReturn = objError;
                    }
                }
            }
            return objReturn;
        }

        /// <summary>
        /// Method to get the list of tributes to add video tributes
        /// </summary>
        /// <param name="objVideo">Tribute entity containing user id</param>
        /// <returns>List of tributes</returns>
        public List<Tributes> GetListOfTributesForVideoTribute(object[] objVideo)
        {
            try
            {
                Tributes objTributes = (Tributes)objVideo[0];
                List<Tributes> objTributeList = new List<Tributes>();
                if (!Equals(objTributes, null))
                {
                    object[] objParam = { objTributes.UserTributeId };
                    DataSet dsTributes = GetDataSet("usp_GetTributesForVideoTribute", objParam);

                    if (dsTributes.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsTributes.Tables[0].Rows)
                        {
                            Tributes objTribute = new Tributes();
                            objTribute.TributeId = int.Parse(dr["UserTributeId"].ToString());
                            objTribute.UserTributeId = int.Parse(dr["UserId"].ToString());
                            objTribute.TributeName = dr["TributeName"].ToString();
                            objTribute.TributeType = int.Parse(dr["TributeType"].ToString());
                            objTribute.TributeUrl = dr["TributeUrl"].ToString();
                            objTribute.TypeDescription = dr["TypeDescription"].ToString();
                            objTribute.HavingVideoTribute = int.Parse(dr["VideoTributeCount"].ToString());
                            objTributeList.Add(objTribute);
                        }
                    }
                }
                return objTributeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get token details
        /// </summary>
        /// <param name="objToken">Video token entity containing token id</param>
        /// <returns>Filled VideoToken entity.</returns>
        public VideoToken GetTokenDetails(object[] objToken)
        {
            try
            {
                VideoToken objTokenId = (VideoToken)objToken[0];
                VideoToken objTokenDetails = new VideoToken(); //to return filled entity
                if (!Equals(objTokenId, null))
                {
                    object[] objParam = { objTokenId.TokenId };
                    DataSet dsTokenDetails = GetDataSet("usp_GetTokenDetails", objParam);

                    if (dsTokenDetails.Tables[0].Rows.Count > 0)
                    {
                        objTokenDetails.TokenId = dsTokenDetails.Tables[0].Rows[0]["TokenId"].ToString();
                        objTokenDetails.UserId = int.Parse(dsTokenDetails.Tables[0].Rows[0]["CreatedBy"].ToString());
                        objTokenDetails.FileName = dsTokenDetails.Tables[0].Rows[0]["FileName"].ToString();
                        objTokenDetails.Status = bool.Parse(dsTokenDetails.Tables[0].Rows[0]["Status"].ToString());
                        objTokenDetails.CreatedBy = int.Parse(dsTokenDetails.Tables[0].Rows[0]["CreatedBy"].ToString());
                        objTokenDetails.CreatedDate = DateTime.Parse(dsTokenDetails.Tables[0].Rows[0]["CreatedDate"].ToString());
                        objTokenDetails.IsActive = bool.Parse(dsTokenDetails.Tables[0].Rows[0]["IsActive"].ToString());
                        objTokenDetails.IsDeleted = bool.Parse(dsTokenDetails.Tables[0].Rows[0]["IsDeleted"].ToString());
                    }
                }
                return objTokenDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to delete video tribute when user deleteing it from the Video Upload page.
        /// </summary>
        /// <param name="objVid">Videos entity containing TributeId and UserId</param>
        public void DeleteVideoTribute(object[] objVid)
        {
            Videos objVideo = (Videos)objVid[0];

            if (!Equals(objVideo, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = { "TributeId", "UserId", "IsDeleted" };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64,
                                        DbType.Int64,
                                        DbType.Boolean };
                    //sets the values in the entity to the parameters
                    object[] objValue = { objVideo.UserTributeId,
                                            objVideo.UserId,
                                            objVideo.IsDeleted };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_DeleteVideoTribute", strParam, dbType, objValue);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        Errors objError = new Errors();
                        objError.ErrorMessage = sqlEx.Message;
                        objVideo.CustomError = objError;
                    }
                }
            }
        }

        /// <summary>
        /// Method to get the video tribute details for the tribute
        /// </summary>
        /// <param name="objVideoGallery"></param>
        /// <returns></returns>
        public Videos GetTributeDetails(int videoId)
        {
            try
            {
                Videos objVideo = new Videos();
                if (!Equals(videoId, null))
                {
                    object[] objParam = { videoId };
                    DataSet dsVideo = GetDataSet("usp_GetVideoTributeDetails", objParam);

                    if (dsVideo.Tables[0].Rows.Count > 0)
                    {
                        objVideo.TributeName = dsVideo.Tables[0].Rows[0]["TributeName"].ToString();
                        objVideo.IsTributeActive = bool.Parse((dsVideo.Tables[0].Rows[0]["IsActive"].ToString()));
                    }
                    else
                    {
                        objVideo = null;
                    }
                }

                return objVideo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetTributeEndDate(int tributeId)
        {
            string tributeEndDate = string.Empty;
            try
            {
                if (!tributeId.Equals(0))
                {
                    object[] objParam = { tributeId };
                    DataSet dsNotes = GetDataSet("usp_GetTributePackageDetailByUserTribute", objParam);

                    // to fill records in Note list
                    if (dsNotes.Tables[0].Rows.Count > 0)
                    {
                        if (!(dsNotes.Tables[0].Rows[0]["Enddate"] == null || dsNotes.Tables[0].Rows[0]["Enddate"].ToString() == ""))
                            tributeEndDate = dsNotes.Tables[0].Rows[0]["Enddate"].ToString();
                        else
                            tributeEndDate = "Never";
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tributeEndDate;
        }


        //LHK: tribute field for VideoTribute page
        public Tributes GetTributeFieldDetails(int tributeId)
        {
            try
            {
                Tributes objTrb = new Tributes();
                if (!Equals(tributeId, null))
                {
                    object[] objParam = { tributeId };
                    DataSet dsTribute = GetDataSet("usp_GetTributeOnTributeId", objParam);

                    if (dsTribute.Tables[0].Rows.Count > 0)
                    {
                        objTrb.TributeImage = dsTribute.Tables[0].Rows[0]["TributeImage"].ToString();
                        objTrb.TributeName = dsTribute.Tables[0].Rows[0]["TributeName"].ToString();
                        objTrb.TributeUrl = dsTribute.Tables[0].Rows[0]["TributeURL"].ToString();
                        objTrb.TypeDescription = dsTribute.Tables[0].Rows[0]["Email"].ToString();
                        objTrb.TributeType = int.Parse(dsTribute.Tables[0].Rows[0]["TributeType"].ToString());
                        objTrb.UserTributeId = int.Parse(dsTribute.Tables[0].Rows[0]["UserTributeId"].ToString());

                        if (!(dsTribute.Tables[0].Rows[0]["Date1"] == null || dsTribute.Tables[0].Rows[0]["Date1"].ToString() == ""))
                        objTrb.Date1 = DateTime.Parse(dsTribute.Tables[0].Rows[0]["Date1"].ToString());

                        if (!(dsTribute.Tables[0].Rows[0]["Date2"] == null || dsTribute.Tables[0].Rows[0]["Date2"].ToString() == ""))
                        objTrb.Date2 = DateTime.Parse(dsTribute.Tables[0].Rows[0]["Date2"].ToString());

                        if (!(dsTribute.Tables[0].Rows[0]["TributeUrl"] == null || dsTribute.Tables[0].Rows[0]["TributeUrl"].ToString() == ""))
                            objTrb.TributeUrl = dsTribute.Tables[0].Rows[0]["TributeUrl"].ToString();

                        if (!(dsTribute.Tables[0].Rows[0]["City"] == null || dsTribute.Tables[0].Rows[0]["City"].ToString() == ""))
                            objTrb.City = dsTribute.Tables[0].Rows[0]["City"].ToString();
                        else
                            objTrb.City = "";

                        objTrb.Attribute1 = dsTribute.Tables[0].Rows[0]["State"].ToString();
                        objTrb.Attribute2 = dsTribute.Tables[0].Rows[0]["Country"].ToString();
                        if (!(dsTribute.Tables[0].Rows[0]["IsOrderDVDChecked"] == null || dsTribute.Tables[0].Rows[0]["IsOrderDVDChecked"].ToString() == ""))
                            objTrb.IsOrderDVDChecked = bool.Parse(dsTribute.Tables[0].Rows[0]["IsOrderDVDChecked"].ToString());
                        else
                            objTrb.IsOrderDVDChecked = false;
                        if (!(dsTribute.Tables[0].Rows[0]["IsMemTributeBoxChecked"] == null || dsTribute.Tables[0].Rows[0]["IsMemTributeBoxChecked"].ToString() == ""))
                            objTrb.IsMemTributeBoxChecked = bool.Parse(dsTribute.Tables[0].Rows[0]["IsMemTributeBoxChecked"].ToString());
                        else
                            objTrb.IsMemTributeBoxChecked = false;
                        if (!(dsTribute.Tables[0].Rows[0]["LinkMemTributeId"] == null || dsTribute.Tables[0].Rows[0]["LinkMemTributeId"].ToString() == ""))
                            objTrb.LinkMemTributeId = int.Parse(dsTribute.Tables[0].Rows[0]["LinkMemTributeId"].ToString());
                        objTrb.IsPrivate = bool.Parse(dsTribute.Tables[0].Rows[0]["IsPrivate"].ToString());

                    }
                    else
                    {
                        objTrb = null;
                    }
                }
                return objTrb;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //LHK: for Video
        public Videos GetVideoDetailsOnUserTributeId(int _tributeId)
        {
            try
            {
                Videos objVd = new Videos();
                if (!Equals(_tributeId, null))
                {
                    object[] objParam = { _tributeId };
                    DataSet dsVd = GetDataSet("usp_GetVideoDetailsOnUserTributeId", objParam);

                    if (dsVd.Tables[0].Rows.Count > 0)
                    {
                        //objVd.CreatedBy = int.Parse(dsVd.Tables[0].Rows[0]["UserId"].ToString());
                        objVd.VideoId = int.Parse(dsVd.Tables[0].Rows[0]["VideoId"].ToString());
                        objVd.UserId = int.Parse(dsVd.Tables[0].Rows[0]["UserId"].ToString());
                        objVd.UserTributeId = int.Parse(dsVd.Tables[0].Rows[0]["UserTributeId"].ToString());
                        //objVd.VideoTypeId = dsVd.Tables[0].Rows[0]["VideoTypeId"].ToString();
                        objVd.TributeVideoId = dsVd.Tables[0].Rows[0]["TributeVideoId"].ToString();
                        //objVd.VideoUrl = dsVd.Tables[0].Rows[0]["VideoFile"].ToString();
                    }
                    else
                    {
                        objVd = null;
                    }
                }
                return objVd;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Methos to send Video tribute Headers details 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserBusiness GetHeaderDetailsOnUserId(int userId)
        {
            UserBusiness objUserBusiness = new UserBusiness();
            try
            {
                //implementation.
                // throw new NotImplementedException();

                if (!Equals(userId, null))
                {
                    object[] objParam = { userId };
                    DataSet dsBusinessUser = GetDataSet("usp_GetTributeHeaderDetails", objParam);

                    if (dsBusinessUser.Tables[0].Rows.Count > 0)
                    {
                        objUserBusiness.UserName = dsBusinessUser.Tables[0].Rows[0]["UserName"].ToString();
                        if (!(dsBusinessUser.Tables[0].Rows[0]["BusinessAddress"] == null || dsBusinessUser.Tables[0].Rows[0]["BusinessAddress"].ToString() == ""))
                        {
                            objUserBusiness.BusinessAddress = dsBusinessUser.Tables[0].Rows[0]["BusinessAddress"].ToString();
                        }
                        else
                        {
                            objUserBusiness.BusinessAddress = "";
                        }
                        if (!(dsBusinessUser.Tables[0].Rows[0]["City"] == null || dsBusinessUser.Tables[0].Rows[0]["City"].ToString() == ""))
                            objUserBusiness.Attribute1 = dsBusinessUser.Tables[0].Rows[0]["City"].ToString();
                        else
                            objUserBusiness.Attribute1 = "";

                        if (!(dsBusinessUser.Tables[0].Rows[0]["State"] == null || dsBusinessUser.Tables[0].Rows[0]["State"].ToString() == ""))
                            objUserBusiness.Attribute2 = dsBusinessUser.Tables[0].Rows[0]["State"].ToString();
                        else
                            objUserBusiness.Attribute2 = "";


                        if (!(dsBusinessUser.Tables[0].Rows[0]["Phone"].ToString() == null))
                        {
                            objUserBusiness.Phone = dsBusinessUser.Tables[0].Rows[0]["Phone"].ToString();
                        }
                        else
                        {
                            objUserBusiness.Phone = "";
                        }


                        if (!(dsBusinessUser.Tables[0].Rows[0]["HeaderBGColor"].ToString() == null))
                        {
                            objUserBusiness.HeaderBGColor = dsBusinessUser.Tables[0].Rows[0]["HeaderBGColor"].ToString();
                        }
                        else
                        {
                            objUserBusiness.HeaderBGColor = "";
                        }

                        if (!(dsBusinessUser.Tables[0].Rows[0]["WebSite"].ToString() == null))
                        {
                            objUserBusiness.Website = dsBusinessUser.Tables[0].Rows[0]["WebSite"].ToString();
                        }
                        else
                        {
                            objUserBusiness.Website = "";
                        }

                        if (!(dsBusinessUser.Tables[0].Rows[0]["HeaderLogo"].ToString() == null))
                        {
                            objUserBusiness.HeaderLogo = dsBusinessUser.Tables[0].Rows[0]["HeaderLogo"].ToString();
                        }
                        else
                        {
                            objUserBusiness.HeaderLogo = "";
                        }

                        if (!(dsBusinessUser.Tables[0].Rows[0]["IsAddressOn"] == null || dsBusinessUser.Tables[0].Rows[0]["IsAddressOn"].ToString()==""))
                        {
                            objUserBusiness.IsAddressOn = Convert.ToBoolean(dsBusinessUser.Tables[0].Rows[0]["IsAddressOn"].ToString());
                        }
                        else
                        {
                            objUserBusiness.IsAddressOn = false;
                        }

                        if (!(dsBusinessUser.Tables[0].Rows[0]["IsPhoneNoOn"] == null || dsBusinessUser.Tables[0].Rows[0]["IsPhoneNoOn"].ToString()==""))
                        {
                            objUserBusiness.IsPhoneNoOn = Convert.ToBoolean(dsBusinessUser.Tables[0].Rows[0]["IsPhoneNoOn"].ToString());
                        }
                        else
                        {
                            objUserBusiness.IsPhoneNoOn = false;
                        }

                        if (!(dsBusinessUser.Tables[0].Rows[0]["IsWebAddressOn"] == null || dsBusinessUser.Tables[0].Rows[0]["IsWebAddressOn"].ToString()==""))
                        {
                            objUserBusiness.IsWebAddressOn = Convert.ToBoolean(dsBusinessUser.Tables[0].Rows[0]["IsWebAddressOn"].ToString());
                        }
                        else
                        {
                            objUserBusiness.IsWebAddressOn = false;
                        }


                        if (!(dsBusinessUser.Tables[0].Rows[0]["DisplayCustomHeader"] == null || dsBusinessUser.Tables[0].Rows[0]["DisplayCustomHeader"].ToString() == ""))
                        {
                            objUserBusiness.DisplayCustomHeader = Convert.ToBoolean(dsBusinessUser.Tables[0].Rows[0]["DisplayCustomHeader"].ToString());
                        }
                        else
                        {
                            objUserBusiness.DisplayCustomHeader = false;
                        }

                        if (!(dsBusinessUser.Tables[0].Rows[0]["ObituaryLinkPage"].ToString()==""))
                        {
                            objUserBusiness.ObituaryLinkPage = dsBusinessUser.Tables[0].Rows[0]["ObituaryLinkPage"].ToString();
                        }
                        else
                        {
                            objUserBusiness.ObituaryLinkPage = "";
                        }
                        if (!(dsBusinessUser.Tables[0].Rows[0]["IsObUrlLinkOn"] == null || dsBusinessUser.Tables[0].Rows[0]["IsObUrlLinkOn"].ToString() == ""))
                        {
                            objUserBusiness.IsObUrlLinkOn = Convert.ToBoolean(dsBusinessUser.Tables[0].Rows[0]["IsObUrllinkOn"].ToString());
                        }
                        else
                        {
                            objUserBusiness.IsObUrlLinkOn = false;
                        }
                       
                        
                    }
                    else
                    {
                        objUserBusiness = null;
                    }
                }
                return objUserBusiness;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// this method returns the LinkMemTribute Id if exists
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserTributeId"></param>
        /// <returns></returns>
        public int? GetLinkVideoMemorialTribute(int? UserId, int UserTributeId)
        {
            try
            {
                int LinkVideoMemorialTributeId = 0;
                if (!Equals(UserTributeId, null))
                {
                    object[] objParam = { UserId, UserTributeId };
                    DataSet dsLink = GetDataSet("usp_GetLinkVideoMemorialTribute", objParam);

                    if (dsLink.Tables[0].Rows.Count > 0)
                    {
                        if (!(dsLink.Tables[0].Rows[0]["MemTributeId"].ToString() == ""))
                        {
                            LinkVideoMemorialTributeId = int.Parse(dsLink.Tables[0].Rows[0]["MemTributeId"].ToString());
                        }
                    } 
                }
                return LinkVideoMemorialTributeId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Users GetUserNameOnUserId(int _userId)
        {
            Users objUsers = new Users();
            try
            {
                if (!Equals(_userId, null))
                {
                    object[] objParam = { _userId };
                    DataSet dsUName = GetDataSet("usp_GetUserProfile", objParam);
                    if (dsUName.Tables[0].Rows.Count > 0)
                    {
                        if (!(dsUName.Tables[0].Rows[0]["UserName"].ToString() == ""))
                        {
                            objUsers.UserName = dsUName.Tables[0].Rows[0]["UserName"].ToString();
                        }
                    }
                    else
                        objUsers.UserName = "";
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
                    
            return objUsers;
        }

        public bool GetCustomHeaderVisible(string _tributeUrl, string ApplicationType)
        {
            bool isCustomHeaderOn = false;
            try
            {
                if (!Equals(_tributeUrl, null))
                {
                    object[] objParam = { _tributeUrl, ApplicationType };
                    DataSet dsUName = GetDataSet("usp_GetCustomHeaderVisible", objParam);
                    if (dsUName.Tables[0].Rows.Count > 0)
                    {
                        if (!(dsUName.Tables[0].Rows[0]["DisplayCustomHeader"].ToString() == ""))
                        {
                            isCustomHeaderOn = bool.Parse(dsUName.Tables[0].Rows[0]["DisplayCustomHeader"].ToString());
                        }
                    }
                }
                return isCustomHeaderOn;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return isCustomHeaderOn;
        }

        public bool GetCustomHeaderVisible(int _trbId)
        {
            bool isCustomHeaderOn = false;
            try
            {
                if (_trbId > 0)
                {
                    object[] objParam = { _trbId };
                    DataSet dsUName = GetDataSet("usp_GetCustomHeaderVisibleOnId", objParam);
                    if (dsUName.Tables[0].Rows.Count > 0)
                    {
                        if (!(dsUName.Tables[0].Rows[0]["DisplayCustomHeader"].ToString() == ""))
                        {
                            isCustomHeaderOn = bool.Parse(dsUName.Tables[0].Rows[0]["DisplayCustomHeader"].ToString());
                        }
                    }
                }
                return isCustomHeaderOn;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return isCustomHeaderOn;
        }

        public void UpdateVideoTributeImage(int _tributeId, string _videoTributeImage)
        {
            try
            {
                if (!Equals(_tributeId, null))
                {
                    //sets the parameters
                    string[] strParam = {Tributes.TributeEnum.TributeId.ToString(),
                                         Tributes.TributeEnum.TributeImage.ToString()
                                    };
                    //sets the types of parameters
                    DbType[] dbType = { DbType.Int64,                                        
                                        DbType.String,                                       
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue =    {_tributeId,
                                            _videoTributeImage
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_UpdateVideoTributeImage", strParam, dbType, objValue);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will update the tribute detail
        /// </summary>
        /// 
        /// <param name="objTribute">Stories object which contain the Tribute detail which user want to update
        /// </param>
        public void UpdateTributeDetail(Tributes objTribute)
        {
            try
            {
                if (objTribute != null)
                {
                    //sets the parameters
                    string[] strParam = { Tributes.TributeEnum.TributeId.ToString(),  
                                          Tributes.TributeEnum.TributeName.ToString(),  
                                          Tributes.TributeEnum.TributeImage.ToString(),  
                                          Tributes.TributeEnum.City.ToString(),  
                                          Tributes.TributeEnum.State.ToString(),  
                                          Tributes.TributeEnum.Country.ToString(),  
                                          Tributes.TributeEnum.Date1.ToString(),  
                                          Tributes.TributeEnum.Date2.ToString(),
                                          Tributes.TributeEnum.ModifiedBy.ToString(),  
                                          Tributes.TributeEnum.ModifiedDate.ToString(),  
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


        public void UpdateVideoTributeDetail(Tributes objTribute)
        {
            try
            {
                if (objTribute != null)
                {
                    //sets the parameters
                    string[] strParam = { Tributes.TributeEnum.TributeId.ToString(),  
                                          Tributes.TributeEnum.TributeName.ToString(),                                           
                                          Tributes.TributeEnum.City.ToString(),  
                                          Tributes.TributeEnum.State.ToString(),  
                                          Tributes.TributeEnum.Country.ToString(),  
                                          Tributes.TributeEnum.Date1.ToString(),  
                                          Tributes.TributeEnum.Date2.ToString(),
                                          Tributes.TributeEnum.ModifiedBy.ToString(),  
                                          Tributes.TributeEnum.ModifiedDate.ToString(),  
                                        };


                    //sets the types of parameters
                    DbType[] enumType = { DbType.Int64,
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
                                               objTribute.City, 
                                               objTribute.State,
                                               objTribute.Country,
                                               objTribute.Date1,
                                               objTribute.Date2,
                                               objTribute.ModifiedBy,
                                               objTribute.ModifiedDate 
                                             };
                    UpdateRecord("usp_UpdateVideoTributeDetail", strParam, enumType, objTributeVal);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool GetCustomHeaderDetail(int tributeId)
        {
            bool isCustomHeaderOn = false;
            try
            {
                if (!tributeId.Equals(0))
                {
                    object[] objParam = { tributeId };
                    DataSet dsUsers = GetDataSet("usp_GetCustomHeaderDetailOnTributeId", objParam);

                    // to fill records in Note list
                    if (dsUsers.Tables[0].Rows.Count > 0)
                    {
                        isCustomHeaderOn = bool.Parse(dsUsers.Tables[0].Rows[0]["displaycustomheader"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isCustomHeaderOn;
        }

        /// <summary>
        /// to GetCurrentVideos
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public int GetCurrentVideos(int tributeId)
        {
            int currentCount = 0;
            try
            {
                if (!tributeId.Equals(0))
                {
                    object[] objParam = { tributeId };
                    DataSet dsNotes = GetDataSet("usp_GetCurrentVideos", objParam);

                    // to fill records in Note list
                    if (dsNotes.Tables[0].Rows.Count > 0)
                    {
                        int.TryParse(dsNotes.Tables[0].Rows[0]["TotalVideos"].ToString(), out currentCount);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return currentCount;
        }
        public string GetOldTributeUrlOnTributeId(int _tributeId)
        {
            string OldTrbUrl = string.Empty;
            try
            {
                if (!_tributeId.Equals(0))
                {
                    object[] objParam = { _tributeId };
                    DataSet ds = GetDataSet("usp_GetOldTributeUrlOnTributeId", objParam);

                    // to fill records in Note list
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        OldTrbUrl = ds.Tables[0].Rows[0]["OldTributeURL"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OldTrbUrl;
        }
        public int GetUserIdByUserName(string _BusinessUserName, string ApplicationType)
        {
            int UserId = 0;
            try
            {
                if (! string.IsNullOrEmpty(_BusinessUserName))
                {
                    object[] objParam = { _BusinessUserName, ApplicationType };
                    DataSet ds = GetDataSet("usp_GetUserIdByUserName", objParam);

                    // to fill records in Note list
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int.TryParse(ds.Tables[0].Rows[0]["UserId"].ToString(), out UserId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return UserId;
        }
        #endregion

        #region IResourceAccess Members

        public void InsertRecord(object[] Params)
        {
            throw new NotImplementedException();
        }

        public void GetData(object[] Params)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecord(object[] Params)
        {
            throw new NotImplementedException();
        }

        public void Delete(object[] Params)
        {
            throw new NotImplementedException();
        }

        #endregion         
    }

}
