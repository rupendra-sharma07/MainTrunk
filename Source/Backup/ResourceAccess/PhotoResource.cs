///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.PhotoResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Photos
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
    public partial class PhotoResource : PortalResourceAccess, IResourceAccess
    {
        #region METHODS
        /// <summary>
        /// Method to create photo album
        /// </summary>
        /// <param name="objPhotoAlbum">Filled PhotoAlbum entity</param>
        /// <returns>object containing PhotoAlbumId</returns>
        public object AddPhotoAlbum(object[] objPhotoAlbum)
        {
            PhotoAlbum objAlbum = (PhotoAlbum)objPhotoAlbum[0];
            object identity;

            if (!Equals(objAlbum, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {PhotoAlbum.PhotoAlbumEnum.UserTributeId.ToString(), PhotoAlbum.PhotoAlbumEnum.UserId.ToString(),
                                            PhotoAlbum.PhotoAlbumEnum.PhotoAlbumCaption.ToString(), PhotoAlbum.PhotoAlbumEnum.PhotoAlbumDesc.ToString(),
                                            PhotoAlbum.PhotoAlbumEnum.CreatedBy.ToString(), PhotoAlbum.PhotoAlbumEnum.CreatedDate.ToString(),
                                            PhotoAlbum.PhotoAlbumEnum.IsActive.ToString(), PhotoAlbum.PhotoAlbumEnum.IsDeleted.ToString()
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64, DbType.Int64,
                                        DbType.String, DbType.String, 
                                        DbType.Int64, DbType.DateTime,
                                        DbType.Boolean, DbType.Boolean
                                       };
                    //sets the values in the entity to the parameters
                    object[] objValue = {objAlbum.UserTributeId, objAlbum.UserId,
                                            objAlbum.PhotoAlbumCaption, objAlbum.PhotoAlbumDesc,
                                            objAlbum.CreatedBy, objAlbum.CreatedDate,
                                            objAlbum.IsActive, objAlbum.IsDeleted
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    identity = base.InsertDataAndReturnId("usp_AddPhotoAlbum", strParam, dbType, objValue);
                    return identity;
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    throw sqlEx;
                }
            }
            else
                return null;
        }



        public object CheckUniquePhotoAlbum(string Photocaption,int tributeId)
        {
             object identity;

             if (!(string.IsNullOrEmpty(Photocaption)))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = { "@UserTributeId", "@PhotoAlbumCaption" };
                                         
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64,
                                        DbType.String
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = {tributeId, 
                                         Photocaption
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    identity = base.InsertDataAndReturnId("usp_CheckPhotoAlbum", strParam, dbType, objValue);
                    return identity;
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    throw sqlEx;
                }
            }
            else
                return null;
        }

        /// <summary>
        /// Method to add photos in the album
        /// </summary>
        /// <param name="objPhotos">Photo enityt containing photo data</param>
        public object AddPhotoToAlbum(object[] objPhotos)
        {
            List<Photos> objPhoto = (List<Photos>)objPhotos[0];
            List<object> objIdentity = new List<object>();
            foreach (Photos obj in objPhoto)
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {Photos.PhotoEnum.PhotoAlbumId.ToString(), 
                                            Photos.PhotoEnum.PhotoImage.ToString(), Photos.PhotoEnum.PhotoCaption.ToString(),
                                            Photos.PhotoEnum.PhotoDesc.ToString(), Photos.PhotoEnum.CreatedBy.ToString(), 
                                            Photos.PhotoEnum.CreatedDate.ToString(), Photos.PhotoEnum.IsActive.ToString(), 
                                            Photos.PhotoEnum.IsDeleted.ToString()
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64,
                                        DbType.String, DbType.String, 
                                        DbType.String, DbType.Int64, 
                                        DbType.DateTime, DbType.Boolean, 
                                        DbType.Boolean
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = {obj.PhotoAlbumId, 
                                            obj.PhotoImage, obj.PhotoCaption, 
                                            obj.PhotoDesc, obj.CreatedBy, 
                                            obj.CreatedDate, obj.IsActive, 
                                            obj.IsDeleted
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    objIdentity.Add(base.InsertDataAndReturnIdMinusIOVS("usp_AddPhoto", strParam, dbType, objValue));
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        throw sqlEx;
                    }
                }
            }
            return objIdentity[objIdentity.Count - 1];
        }

        /// <summary>
        /// Method to get the list of photo album for the selected tribute
        /// </summary>
        /// <param name="objPhotoGallery">Object containing PhotoAlbum entity for tributeid, page size and page number</param>
        /// <returns>List of PhotoAlbum entity.</returns>
        public List<PhotoAlbum> GetPhotoGalleryRecords(object[] objPhotoGallery)
        {
            try
            {
                PhotoAlbum objPhotoAlbum = (PhotoAlbum)objPhotoGallery[0];
                List<PhotoAlbum> objListPhotoAlbum = new List<PhotoAlbum>();

                //VideoGallery objVideoGal = (VideoGallery)objVideoGallery[0];
                //List<VideoGallery> objListVideo = new List<VideoGallery>();
                if (!Equals(objPhotoAlbum, null))
                {
                    object[] objParam = {objPhotoAlbum.UserTributeId,
                                            objPhotoAlbum.PageSize,
                                            objPhotoAlbum.PageNumber                                          
                                        };
                    DataSet dsPhotoGallery = GetDataSet("usp_GetPhotoGallery", objParam);

                    //to fill records in the Photo Gallery list
                    if (dsPhotoGallery.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsPhotoGallery.Tables[0].Rows)
                        {
                            PhotoAlbum objAlbum = new PhotoAlbum();
                            objAlbum.PhotoAlbumId = int.Parse(dr["PhotoAlbumId"].ToString());
                            objAlbum.UserTributeId = int.Parse(dr["UserTributeId"].ToString());
                            objAlbum.PhotoAlbumCaption = dr["PhotoAlbumCaption"].ToString();
                            objAlbum.PhotoImage = dr["PhotoImage"].ToString();
                            objAlbum.CreatedBy = int.Parse(dr["CreatedBy"].ToString());
                            objAlbum.CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString());
                            objAlbum.CreationDate = DateTime.Parse(dr["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");
                            objAlbum.ModificationDate = dr["ModifiedDate"] == DBNull.Value ? string.Empty : DateTime.Parse(dr["ModifiedDate"].ToString()).ToString("MMMM dd, yyyy");
                            //objAlbum.ModifiedDate = dr["ModifiedDate"].ToString() != null ? DateTime.Parse(dr["ModifiedDate"].ToString()).ToString("MMMM dd, yyyy") : null;
                            objAlbum.PhotoCount = int.Parse(dr["PhotoCount"].ToString());
                            objAlbum.UserName = dr["UserName"].ToString();
                            objAlbum.CommentCount = int.Parse(dr["CommentCount"].ToString());
                            objAlbum.TotalRecords = int.Parse(dr["TotalRecords"].ToString());

                            objListPhotoAlbum.Add(objAlbum);
                            objAlbum = null;
                        }
                    }
                }
                return objListPhotoAlbum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get the list of photos for the selected album.
        /// </summary>
        /// <param name="objPhotoGallery">Object containing PhotoAlbum entity for PhotoAlbumId</param>
        /// <returns>List of photos.</returns>
        public List<Photos> GetPhotos(object[] objPhotos)
        {
            try
            {
                Photos objGetPhotos = (Photos)objPhotos[0];
                List<Photos> objListPhotos = new List<Photos>();

                if (!Equals(objGetPhotos, null))
                {
                    object[] objParam = {objGetPhotos.PhotoAlbumId,
                                            objGetPhotos.PageSize,
                                            objGetPhotos.PageNumber,
                                            objGetPhotos.SortOrder
                                        };
                    DataSet dsPhotos = GetDataSet("usp_GetPhotos", objParam);
                    int totalRecords = 0;
                    //objGetPhotos.SortOrder
                    if (dsPhotos.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dsPhotos.Tables[0].Rows[0];
                        totalRecords = int.Parse(dr["TotalRecords"].ToString());
                    }
                    //to fill records in the Photo list
                    if (dsPhotos.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsPhotos.Tables[1].Rows)
                        {
                            Photos objPhoto = new Photos();
                            objPhoto.PhotoId = int.Parse(dr["UserPhotoId"].ToString());
                            objPhoto.PhotoAlbumId = int.Parse(dr["PhotoAlbumId"].ToString());
                            objPhoto.PhotoImage = dr["PhotoImage"].ToString();
                            objPhoto.CommentCount = int.Parse(dr["CommentCount"].ToString());
                            objPhoto.CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString());
                            objPhoto.TotalRecords = totalRecords;

                            objListPhotos.Add(objPhoto);
                            objPhoto = null;
                        }
                    }
                }
                return objListPhotos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Method to get the list of photos for the selected album.
        /// </summary>
        /// <param name="objPhotoGallery">Object containing PhotoAlbum entity for PhotoAlbumId</param>
        /// <returns>List of photos.</returns>
        public List<Photos> GetPhotosDateWise(object[] objPhotos)
        {
            try
            {
                Photos objGetPhotos = (Photos)objPhotos[0];
                List<Photos> objListPhotos = new List<Photos>();

                if (!Equals(objGetPhotos, null))
                {
                    object[] objParam = {objGetPhotos.ToDate,objGetPhotos.FromDate,
                                            objGetPhotos.PageSize,
                                            objGetPhotos.PageNumber
                                        };
                    DataSet dsPhotos = GetDataSet("usp_GetPhotosDatewise", objParam);

                    //to fill records in the Photo list
                    if (dsPhotos.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsPhotos.Tables[0].Rows)
                        {
                            Photos objPhoto = new Photos();
                            objPhoto.PhotoId = int.Parse(dr["UserPhotoId"].ToString());
                            objPhoto.PhotoAlbumId = int.Parse(dr["PhotoAlbumId"].ToString());
                            objPhoto.PhotoImage = dr["PhotoImage"].ToString();
                            objPhoto.CommentCount = int.Parse(dr["CommentCount"].ToString());
                            objPhoto.TotalRecords = int.Parse(dr["TotalRecords"].ToString());
                            objListPhotos.Add(objPhoto);
                            objPhoto = null;
                        }
                    }
                }
                return objListPhotos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Method to get the details of photo album.
        /// </summary>
        /// <param name="objPhotoGallery">Object containing PhotoAlbum entity for PhotoAlbumId</param>
        /// <returns>PhotoAlbumDetails.</returns>
        public PhotoAlbum GetPhotoAlbumDetail(object[] objPhotoAlbumId)
        {
            try
            {
                Photos objPhotoAlbum = (Photos)objPhotoAlbumId[0];
                PhotoAlbum objPhotoAlbumDetails = new PhotoAlbum();
                if (!Equals(objPhotoAlbum, null))
                {
                    object[] objParam = { objPhotoAlbum.PhotoAlbumId };
                    DataSet dsPhotoAlbumDetail = GetDataSet("usp_GetPhotoAlbumDetails", objParam);

                    //to fill records in the Video Gallery list
                    if (dsPhotoAlbumDetail.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsPhotoAlbumDetail.Tables[0].Rows)
                        {
                            objPhotoAlbumDetails.PhotoAlbumId = int.Parse(dr["PhotoAlbumId"].ToString());
                            objPhotoAlbumDetails.PhotoAlbumCaption = dr["PhotoAlbumCaption"].ToString();
                            objPhotoAlbumDetails.PhotoAlbumDesc = dr["PhotoAlbumDesc"].ToString();
                            objPhotoAlbumDetails.UserTributeId = int.Parse(dr["UserTributeId"].ToString());
                            objPhotoAlbumDetails.UserId = int.Parse(dr["UserId"].ToString());
                            objPhotoAlbumDetails.UserName = dr["UserName"].ToString();
                            objPhotoAlbumDetails.PhotoCount = int.Parse(dr["PhotoCount"].ToString());
                            objPhotoAlbumDetails.CreatedBy = int.Parse(dr["CreatedBy"].ToString());
                            objPhotoAlbumDetails.CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString());
                        }
                    }
                }
                return objPhotoAlbumDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// to GetCurrentPhotoAlbums
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public int GetCurrentPhotoAlbums(int tributeId)
        {
            int currentCount = 0;
            try
            {
                if (!tributeId.Equals(0))
                {
                    object[] objParam = { tributeId };
                    DataSet dsNotes = GetDataSet("usp_GetCurrentPhotoAlbum", objParam);

                    // to fill records in Note list
                    if (dsNotes.Tables[0].Rows.Count > 0)
                    {
                        int.TryParse(dsNotes.Tables[0].Rows[0]["TotalPhotoAlbum"].ToString(), out currentCount);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return currentCount;
        }

        /// <summary>
        /// Method to get the details of photo.
        /// </summary>
        /// <param name="objPhotoGallery">Object containing Photo entity for PhotoId</param>
        /// <returns>Photo details.</returns>
        public Photos GetPhotoDetail(object[] objPhotoId)
        {
            try
            {
                Photos objPhoto = (Photos)objPhotoId[0];

                if (!Equals(objPhoto, null))
                {
                    object[] objParam = { objPhoto.PhotoId, objPhoto.UserId };
                    DataSet dsPhotoDetail = GetDataSet("usp_GetPhotoDetails", objParam);

                    //to fill records in the Video Gallery list
                    if (dsPhotoDetail.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsPhotoDetail.Tables[0].Rows)
                        {
                            objPhoto.PhotoAlbumId = int.Parse(dr["PhotoAlbumId"].ToString());
                            objPhoto.PhotoId = int.Parse(dr["UserPhotoId"].ToString());
                            objPhoto.PhotoCaption = dr["PhotoCaption"].ToString();
                            objPhoto.PhotoDesc = dr["PhotoDesc"].ToString();
                            objPhoto.PhotoAlbumCaption = dr["PhotoAlbumCaption"].ToString();
                            objPhoto.PhotoImage = dr["PhotoImage"].ToString();
                            objPhoto.PrevRecordCount = int.Parse(dr["PrevCount"].ToString());
                            objPhoto.NextRecordCount = int.Parse(dr["NextCount"].ToString());
                            objPhoto.IsAdmin = int.Parse(dr["IsAdmin"].ToString());
                            objPhoto.TotalRecords = int.Parse(dr["TotalRecords"].ToString());
                            objPhoto.RecordNumber = objPhoto.TotalRecords - int.Parse(dr["RecordNumber"].ToString());
                            objPhoto.CommentCount = int.Parse(dr["CommentCount"].ToString());
                            objPhoto.UserName = dr["UserName"].ToString();
                            objPhoto.UserName = dr["UserName"].ToString();
                            objPhoto.CreatedBy = int.Parse(dr["CreatedBy"].ToString());
                            objPhoto.CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString());
                            objPhoto.CreationDate = DateTime.Parse(dr["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");
                        }
                    }
                }
                return objPhoto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to Update Photo Details
        /// </summary>
        /// <param name="objVid">Filled Photos Entity</param>
        public void UpdatePhotoDetails(object[] objPhotos)
        {
            Photos objPhoto = (Photos)objPhotos[0];

            if (!Equals(objPhoto, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {Photos.PhotoEnum.PhotoId.ToString(),
                                            Photos.PhotoEnum.PhotoCaption.ToString(),
                                            Photos.PhotoEnum.PhotoDesc.ToString(),
                                            Photos.PhotoEnum.ModifiedBy.ToString(),
                                            Photos.PhotoEnum.ModifiedDate.ToString()
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64,
                                        DbType.String,
                                        DbType.String, 
                                        DbType.Int64,
                                        DbType.DateTime,
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { objPhoto.PhotoId,
                                            objPhoto.PhotoCaption, 
                                            objPhoto.PhotoDesc, 
                                            objPhoto.ModifiedBy, 
                                            objPhoto.ModifiedDate,
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_UpdatePhotoDetails", strParam, dbType, objValue);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    throw sqlEx;
                }
            }
        }

        /// <summary>
        /// Method to delete photo. This sets the IsDeleted Field to true.
        /// </summary>
        /// <param name="objVid">Photo Entity containing PhotoId and IsDeleted flag</param>
        public void DeletePhoto(object[] objPhotos)
        {
            Photos objPhoto = (Photos)objPhotos[0];

            if (!Equals(objPhoto, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {Photos.PhotoEnum.PhotoId.ToString(),
                                            Photos.PhotoEnum.UserId.ToString(),
                                            Photos.PhotoEnum.IsDeleted.ToString(),
                                            Photos.PhotoEnum.ModifiedDate.ToString()};
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64,
                                        DbType.Int64,
                                        DbType.Boolean,
                                           DbType.DateTime};
                    //sets the values in the entity to the parameters
                    object[] objValue = { objPhoto.PhotoId,
                                            objPhoto.UserId,
                                            objPhoto.IsDeleted,
                                            objPhoto.ModifiedDate};

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_DeletePhoto", strParam, dbType, objValue);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    throw sqlEx;
                }
            }
        }

        /// <summary>
        /// Method to get the number of photos exists for the selected album.
        /// </summary>
        /// <param name="objPhotoAlbumId">PhotoAlbum entity containing photoalbumid.</param>
        /// <returns>object containing count for number of photos.</returns>
        public object GetPhotoCountForAlbum(object[] objPhotoAlbumId)
        {
            try
            {
                PhotoAlbum objPhotoAlbum = (PhotoAlbum)objPhotoAlbumId[0];
                object photoCount = null;
                if (!Equals(objPhotoAlbum, null))
                {
                    object[] objParam = { objPhotoAlbum.PhotoAlbumId };
                    DataSet dsPhotoCount = GetDataSet("usp_GetPhotoCount", objParam);
                    photoCount = dsPhotoCount.Tables[0].Rows[0]["PhotoCount"];
                }
                return photoCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to Update Photo Album Details
        /// </summary>
        /// <param name="objVid">Filled Photo Album Entity</param>
        public object UpdatePhotoAlbumDetails(object[] objPhotoAlbum)
        {
            PhotoAlbum objUpdateAlbum = (PhotoAlbum)objPhotoAlbum[0];
            object objReturn = new object();
            if (!Equals(objUpdateAlbum, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {PhotoAlbum.PhotoAlbumEnum.PhotoAlbumId.ToString(),
                                            PhotoAlbum.PhotoAlbumEnum.PhotoAlbumCaption.ToString(),
                                            PhotoAlbum.PhotoAlbumEnum.PhotoAlbumDesc.ToString(),
                                            PhotoAlbum.PhotoAlbumEnum.ModifiedBy.ToString(),
                                            PhotoAlbum.PhotoAlbumEnum.ModifiedDate.ToString()
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64,
                                        DbType.String,
                                        DbType.String, 
                                        DbType.Int64,
                                        DbType.DateTime,
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { objUpdateAlbum.PhotoAlbumId,
                                            objUpdateAlbum.PhotoAlbumCaption, 
                                            objUpdateAlbum.PhotoAlbumDesc, 
                                            objUpdateAlbum.ModifiedBy, 
                                            objUpdateAlbum.ModifiedDate,
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_UpdatePhotoAlbumDetails", strParam, dbType, objValue);
                    objReturn = null;
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objReturn = objError;
                    //return objReturn;
                }
                catch (Exception ex)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = "Failure"; //ex.Message;
                    objReturn = objError;
                    //return objReturn;
                }
            }
            return objReturn;
        }

        /// <summary>
        /// Method to delete photo album. This sets the IsDeleted Field to true.
        /// </summary>
        /// <param name="objVid">PhotoAlbum entity containing PhotoAlbumId and IsDeleted flag.</param>
        public void DeletePhotoAlbum(object[] objPhotoAlbum)
        {
            PhotoAlbum objDeleteAlbum = (PhotoAlbum)objPhotoAlbum[0];

            if (!Equals(objDeleteAlbum, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {PhotoAlbum.PhotoAlbumEnum.PhotoAlbumId.ToString(),
                                            PhotoAlbum.PhotoAlbumEnum.UserId.ToString(),
                                            PhotoAlbum.PhotoAlbumEnum.IsDeleted.ToString() };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64,
                                        DbType.Int64,
                                        DbType.Boolean };
                    //sets the values in the entity to the parameters
                    object[] objValue = { objDeleteAlbum.PhotoAlbumId,
                                            objDeleteAlbum.UserId,
                                            objDeleteAlbum.IsDeleted };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_DeletePhotoAlbum", strParam, dbType, objValue);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    throw sqlEx;
                }
            }
        }
        /// <summary>
        /// Method to get the list of photo images to display.
        /// </summary>
        /// <param name="objPhotoGallery">Object containing PhotoAlbum entity for PhotoAlbumId</param>
        /// <returns>List of photos.</returns>
        public List<Photos> GetPhotoImagesList(object[] objPhotos)
        {
            try
            {
                Photos objGetPhotos = (Photos)objPhotos[0];
                List<Photos> objListPhotoImagess = new List<Photos>();

                if (!Equals(objGetPhotos, null))
                {
                    object[] objParam = { objGetPhotos.PhotoAlbumId };
                    DataSet dsPhotoImages = GetDataSet("usp_GetPhotoImagesList", objParam);

                    //to fill records in the Photo list
                    if (dsPhotoImages.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsPhotoImages.Tables[0].Rows)
                        {
                            Photos objPhoto = new Photos();
                            objPhoto.PhotoImage = dr["PhotoImage"].ToString();

                            objListPhotoImagess.Add(objPhoto);
                            objPhoto = null;
                        }
                    }
                }
                return objListPhotoImagess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get the list of photo album for the selected tribute
        /// </summary>
        /// <param name="objPhotoGallery">Object containing PhotoAlbum entity for tributeid</param>
        /// <returns>List of PhotoAlbum entity.</returns>
        public List<PhotoAlbum> GetPhotoAlbumList(object[] objPhotoAlbumList)
        {
            try
            {
                PhotoAlbum objPhotoAlbum = (PhotoAlbum)objPhotoAlbumList[0];
                List<PhotoAlbum> objListOfPhotoAlbum = new List<PhotoAlbum>();

                //VideoGallery objVideoGal = (VideoGallery)objVideoGallery[0];
                //List<VideoGallery> objListVideo = new List<VideoGallery>();
                if (!Equals(objPhotoAlbum, null))
                {
                    object[] objParam = { objPhotoAlbum.UserTributeId };
                    DataSet dsPhotoAlbumList = GetDataSet("usp_GetPhotoAlbumListInTribute", objParam);

                    //to fill records in the Photo Gallery list
                    if (dsPhotoAlbumList.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsPhotoAlbumList.Tables[0].Rows)
                        {
                            PhotoAlbum objAlbum = new PhotoAlbum();
                            objAlbum.PhotoAlbumId = int.Parse(dr["PhotoAlbumId"].ToString());
                            objAlbum.PhotoAlbumCaption = dr["PhotoAlbumCaption"].ToString();

                            objListOfPhotoAlbum.Add(objAlbum);
                            objAlbum = null;
                        }
                    }
                }
                return objListOfPhotoAlbum;
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
                    object[] objParam = {tributeId };
                    DataSet dsNotes = GetDataSet("usp_GetTributePackageDetailByUserTribute", objParam);

                    // to fill records in Note list
                    if (dsNotes.Tables[0].Rows.Count > 0)
                    {
                        tributeEndDate = dsNotes.Tables[0].Rows[0]["Enddate"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tributeEndDate;
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
        #endregion

        #region IRESOURCEACCESS METHODS
        public void UpdateRecord(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public void Delete(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public void GetData(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public void InsertRecord(object[] objVid)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public object InsertDataAndReturnId(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        
    }//end class
}//end namespace
