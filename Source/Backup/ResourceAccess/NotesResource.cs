///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.NotesResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Notess
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
    public partial class NotesResource : PortalResourceAccess, IResourceAccess
    {
        /// <summary>
        /// Method to insert notes in the database
        /// </summary>
        /// <param name="objNote">Filled Notes entity</param>
        public object InsertDataAndReturnId(object[] objNote)
        {
            Note objNotes = (Note)objNote[0];
            object identity = null;
            if (!Equals(objNotes, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {Note.NotesEnum.UserTributeId.ToString(), Note.NotesEnum.UserId.ToString(), 
                                       Note.NotesEnum.Title.ToString(), Note.NotesEnum.PostMessage.ToString(), 
                                        Note.NotesEnum.MessageWithoutHtml.ToString(),
                                        Note.NotesEnum.CreatedBy.ToString(), Note.NotesEnum.CreatedDate.ToString(),
                                        Note.NotesEnum.ModifiedBy.ToString(), Note.NotesEnum.ModifiedDate.ToString(),
                                        Note.NotesEnum.IsDeleted.ToString()
                                    };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64, DbType.Int64,
                                    DbType.String, DbType.String,
                                    DbType.String,
                                    DbType.Int64, DbType.DateTime, 
                                    DbType.Int64, DbType.DateTime,
                                    DbType.Boolean};
                    //sets the values in the entity to the parameters
                    object[] objValue = { objNotes.UserTributeId, objNotes.UserId,
                                            objNotes.Title, objNotes.PostMessage,
                                            objNotes.MessageWithoutHtml,
                                            objNotes.CreatedBy, objNotes.CreatedDate,
                                            objNotes.ModifiedBy, objNotes.ModifiedDate,
                                            objNotes.IsDeleted};

                    //sends request to insert record
                    //base.InsertRecord("usp_InsertNote", strParam, dbType, objValue);
                    //identity = base.InsertDataAndReturnId("usp_InsertNote", strParam, dbType, objValue);
                    identity = base.InsertDataAndReturnIdMinusIOVS("usp_InsertNote", strParam, dbType, objValue);
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return identity;
        }

        /// <summary>
        /// Method to get the list of notes for the selected tribute
        /// </summary>
        /// <param name="objTributeId">Note entity containing TributeId.</param>
        /// <returns>List of Notes.</returns>
        public List<Note> GetTributeNotes(object[] objTributeId)
        {
            try
            {
                Note objNote = (Note)objTributeId[0];
                List<Note> objNotesList = new List<Note>();

                if (!Equals(objNote, null))
                {
                    object[] objParam = { objNote.UserTributeId,
                                            objNote.PageSize,
                                            objNote.CurrentPage                                            
                                        };
                    DataSet dsNotes = GetDataSet("usp_GetTributeNotes", objParam);

                    // to fill records in Note list
                    if (dsNotes.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsNotes.Tables[0].Rows)
                        {
                            Note objNotes = new Note();
                            objNotes.NotesId = int.Parse(dr["NotesId"].ToString());
                            objNotes.UserTributeId = int.Parse(dr["UserTributeId"].ToString());
                            objNotes.Title = dr["Title"].ToString();
                            objNotes.PostMessage = dr["PostMessage"].ToString();
                            objNotes.CreatedBy = int.Parse(dr["CreatedBy"].ToString());
                            objNotes.CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString());
                            objNotes.CreationDate = DateTime.Parse(dr["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");
                            objNotes.CreationTime = DateTime.Parse(dr["CreatedDate"].ToString()).ToString("hh:mm tt").ToLower();
                            objNotes.CommentCount = int.Parse(dr["CommentCount"].ToString());
                            objNotes.UserName = dr["UserName"].ToString();
                            objNotes.UserImage = dr["UserImage"].ToString();
                            objNotes.City = dr["City"].ToString();
                            objNotes.State = dr["State"].ToString();
                            objNotes.Country = dr["Country"].ToString();
                            objNotes.TotalRecords = int.Parse(dsNotes.Tables[1].Rows[0]["TotalRecords"].ToString());
                            objNotes.IsLocationHide = bool.Parse(dr["IsLocationHide"].ToString());
                            objNotes.FacebookUid = null;
                            if (!string.IsNullOrEmpty(dr["FacebookUid"].ToString()))
                            {
                                objNotes.FacebookUid = (Nullable<Int64>)dr["FacebookUid"];
                            }
                            //add entity to list
                            objNotesList.Add(objNotes);
                            objNotes = null;
                        }
                    }
                }
                return objNotesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get the note detail
        /// </summary>
        /// <param name="objTributeId">Note entity containing note id.</param>
        /// <returns>Filled Note entity.</returns>
        public Note GetNoteDetail(object[] objNoteId)
        {
            try
            {
                Note objNote = (Note)objNoteId[0];
                Note objNotes = new Note();
                if (!Equals(objNote, null))
                {
                    object[] objParam = { objNote.NotesId };
                    DataSet dsNotes = GetDataSet("usp_GetFullNote", objParam);

                    // to fill records in Note list
                    if (dsNotes.Tables[0].Rows.Count > 0)
                    {
                     
                        objNotes.NotesId = int.Parse(dsNotes.Tables[0].Rows[0]["NotesId"].ToString());
                        objNotes.UserTributeId = int.Parse(dsNotes.Tables[0].Rows[0]["UserTributeId"].ToString());
                        objNotes.Title = dsNotes.Tables[0].Rows[0]["Title"].ToString();
                        objNotes.PostMessage = dsNotes.Tables[0].Rows[0]["PostMessage"].ToString();
                        objNotes.CreatedBy = int.Parse(dsNotes.Tables[0].Rows[0]["CreatedBy"].ToString());
                        objNotes.CreatedDate = DateTime.Parse(dsNotes.Tables[0].Rows[0]["CreatedDate"].ToString());
                        objNotes.CreationDate = DateTime.Parse(dsNotes.Tables[0].Rows[0]["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");
                        objNotes.CreationTime = DateTime.Parse(dsNotes.Tables[0].Rows[0]["CreatedDate"].ToString()).ToString("hh:mm tt").ToLower();
                        objNotes.UserName = dsNotes.Tables[0].Rows[0]["UserName"].ToString();
                        objNotes.UserImage = dsNotes.Tables[0].Rows[0]["UserImage"].ToString();
                        objNotes.FacebookUid = null;
                        if (!string.IsNullOrEmpty(dsNotes.Tables[0].Rows[0]["FacebookUid"].ToString()))
                        {
                            objNotes.FacebookUid = (Nullable<Int64>)dsNotes.Tables[0].Rows[0]["FacebookUid"];
                        }
                        objNotes.City = dsNotes.Tables[0].Rows[0]["City"].ToString();
                        objNotes.State = dsNotes.Tables[0].Rows[0]["State"].ToString();
                        objNotes.Country = dsNotes.Tables[0].Rows[0]["Country"].ToString();
                        objNotes.CommentCount = int.Parse(dsNotes.Tables[0].Rows[0]["CommentCount"].ToString());
                        objNotes.IsLocationHide = bool.Parse(dsNotes.Tables[0].Rows[0]["IsLocationHide"].ToString());
                    }
                }
                return objNotes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GetLastNoteForTribute(object[] objTributeId)
        {
            try
            {
                Note objNote = (Note)objTributeId[0];                
                if (!Equals(objNote, null))
                {
                    object[] objParam = { objNote.UserTributeId };
                    DataSet dsNotes = GetDataSet("GetLastNoteForTribute", objParam);

                    // to fill records in Note list
                    if (dsNotes.Tables[0].Rows.Count > 0)
                    {
                        objNote.NotesId = int.Parse(dsNotes.Tables[0].Rows[0]["NotesId"].ToString());
                        objNote.Title = dsNotes.Tables[0].Rows[0]["Title"].ToString();
                        objNote.PostMessage = dsNotes.Tables[0].Rows[0]["PostMessage"].ToString();
                        objNote.MessageWithoutHtml = dsNotes.Tables[0].Rows[0]["MessageWithoutHtml"].ToString();
                        objNote.CreatedBy = int.Parse(dsNotes.Tables[0].Rows[0]["CreatedBy"].ToString());
                        objNote.CreatedDate = DateTime.Parse(dsNotes.Tables[0].Rows[0]["CreatedDate"].ToString());                        
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to update the note details
        /// </summary>
        /// <param name="Params"></param>
        public void UpdateRecord(object[] Params)
        {
            Note objNote = (Note)Params[0];

            if (!Equals(objNote, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {Note.NotesEnum.NotesId.ToString(),  
                                        Note.NotesEnum.Title.ToString(), 
                                        Note.NotesEnum.PostMessage.ToString(),
                                        Note.NotesEnum.MessageWithoutHtml.ToString(),
                                        Note.NotesEnum.ModifiedBy.ToString(),
                                        Note.NotesEnum.ModifiedDate.ToString()
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64,
                                        DbType.String,
                                        DbType.String, 
                                        DbType.String,
                                        DbType.Int64, 
                                        DbType.DateTime
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { objNote.NotesId,
                                            objNote.Title, 
                                            objNote.PostMessage, 
                                            objNote.MessageWithoutHtml,
                                            objNote.ModifiedBy, 
                                            objNote.ModifiedDate
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    //base.UpdateRecord("usp_UpdateNote", strParam, dbType, objValue);
                    base.UpdateRecordMinusIovs("usp_UpdateNote", strParam, dbType, objValue);
                    
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        Errors objError = new Errors();
                        objError.ErrorMessage = sqlEx.Message;
                    }
                }
            }    
        }

        public void Delete(object[] Params)
        {
            try
            {
                Note objNote = (Note)Params[0];
                object[] objValue ={ objNote.NotesId, objNote.UserId };
                Delete("usp_DeleteNote", objValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region IRESOURCEACCESS METHODS
        public void GetData(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public void InsertRecord(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        /// <summary>
        /// to GetTributeEndDate
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
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
        /// to GetCurrentNotes
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public int GetCurrentNotes(int tributeId)
        {
            int currentNotes = 0;
            try
            {
                if (!tributeId.Equals(0))
                {
                    object[] objParam = { tributeId };
                    DataSet dsNotes = GetDataSet("usp_GetCurrentNotes", objParam);

                    // to fill records in Note list
                    if (dsNotes.Tables[0].Rows.Count > 0)
                    {
                        int.TryParse(dsNotes.Tables[0].Rows[0]["TotalNotes"].ToString(), out currentNotes);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return currentNotes;
        }

        /// <summary>
        /// to GetCurrentEvents
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public int GetCurrentEvents(int tributeId)
        {
            int currentCount = 0;
            try
            {
                if (!tributeId.Equals(0))
                {
                    object[] objParam = { tributeId };
                    DataSet dsNotes = GetDataSet("usp_GetCurrentEvents", objParam);

                    // to fill records in Note list
                    if (dsNotes.Tables[0].Rows.Count > 0)
                    {
                        int.TryParse(dsNotes.Tables[0].Rows[0]["TotalEvents"].ToString(), out currentCount);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return currentCount;
        }

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

    }//end class
}//end namespace

