///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.CommentResources.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Comments
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using System.Data;


namespace TributesPortal.ResourceAccess
{
    public class CommentResources : PortalResourceAccess, IResourceAccess
    {


        public int RecordCount(object[] objSesion)
        {

            CommentTributeAdministrator objPageing = (CommentTributeAdministrator)objSesion[0];
            object[] objValue ={ objPageing.UserId, objPageing.TypeCodeId, objPageing.CommentTypeId, objPageing.TributeId }; 
            DataSet ds = new DataSet();
            ds = GetDataSet("usp_GetcommentsCount", objValue);
            int PagingCount = int.Parse(ds.Tables[0].Rows[0]["RecordCount"].ToString());

            return PagingCount;
        }
 
        /// <summary>
        /// Method to get comments for Guestbook.
        /// </summary>
        /// <param name="objSesion"></param>
        /// <returns></returns>
        public List<CommentTributeAdministrator> CommentList(object[] objSesion)
        {

            CommentTributeAdministrator objComAdmin = (CommentTributeAdministrator)objSesion[0];
            object[] objValue ={ objComAdmin.UserId, objComAdmin.TypeCodeId, objComAdmin.CommentTypeId, objComAdmin.TributeId, objComAdmin.CurrentPage, objComAdmin.PageSize };
            DataSet ds = new DataSet();
            ds = GetDataSet("usp_GetcommentListPageWise", objValue);
            int count = ds.Tables[0].Rows.Count;
            List<CommentTributeAdministrator> comAdmin = new List<CommentTributeAdministrator>();

            for (int i = 0; i < count; i++)
            {
                Nullable<Int64> _FacebookUid = null;
                int x = int.Parse(ds.Tables[0].Rows[i]["CommentId"].ToString());
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["FacebookUid"].ToString()))
                {
                    _FacebookUid = (Nullable<Int64>)ds.Tables[0].Rows[i]["FacebookUid"];
                }

                // Added new by rupendra to handle isLocationHide error
                bool bIsLocationHide = false;
                bool.TryParse(ds.Tables[0].Rows[i]["IsLocationHide"].ToString(),out bIsLocationHide);
                comAdmin.Add(new CommentTributeAdministrator(
                            int.Parse(ds.Tables[0].Rows[i]["CommentId"].ToString()),
                            int.Parse(ds.Tables[0].Rows[i]["UserId"].ToString()),
                            int.Parse(ds.Tables[0].Rows[i]["TypeCodeId"].ToString()),
                            int.Parse(ds.Tables[0].Rows[i]["CommentTypeId"].ToString()),
                            ds.Tables[0].Rows[i]["Message"].ToString(),
                            int.Parse(ds.Tables[0].Rows[i]["CreatedBy"].ToString()),
                            DateTime.Parse(ds.Tables[0].Rows[i]["CreatedDate"].ToString()).ToString("MMMM dd, yyyy"),
                            int.Parse(ds.Tables[0].Rows[i]["UserType"].ToString()),
                            ds.Tables[0].Rows[i]["userImage"].ToString(),
                            int.Parse(ds.Tables[0].Rows[i]["IsAdmin"].ToString()),
                            ds.Tables[0].Rows[i]["userName"].ToString(),
                            ds.Tables[0].Rows[i]["City"].ToString(),
                            ds.Tables[0].Rows[i]["State"].ToString(),
                            ds.Tables[0].Rows[i]["Country"].ToString(),
                            bIsLocationHide,
                            _FacebookUid

                            //Added new on 22 jun 2011 by rupendra to get the Table type from wich the comments are fetched
                            // Here 1 --> Comments , 2--> tblComments_New
                            ,ds.Tables[0].Rows[i]["tableType"].ToString()
                ));

            }
            return comAdmin;
        }

        /// <summary>
        /// Method to get the comments module wise
        /// </summary>
        /// <param name="objSesion"></param>
        /// <returns></returns>
        public List<CommentTributeAdministrator> GetModuleComments(object[] objSesion)
        {
            CommentTributeAdministrator objComAdmin = (CommentTributeAdministrator)objSesion[0];
            object[] objValue ={ objComAdmin.UserId, objComAdmin.TypeCodeName, objComAdmin.CommentTypeId, objComAdmin.TributeId, objComAdmin.CurrentPage, objComAdmin.PageSize };
            DataSet dsComments = GetDataSet("usp_GetModuleComments", objValue);
            
            List<CommentTributeAdministrator> objCommentList = new List<CommentTributeAdministrator>();

            foreach (DataRow dr in dsComments.Tables[0].Rows)
            {
                CommentTributeAdministrator objComment = new CommentTributeAdministrator();
                objComment.CommentId = int.Parse(dr["CommentId"].ToString());
                objComment.UserId = int.Parse(dr["UserId"].ToString());
                objComment.TypeCodeId = int.Parse(dr["TypeCodeId"].ToString());
                objComment.CommentTypeId = int.Parse(dr["CommentTypeId"].ToString());
                objComment.Message = dr["Message"].ToString();
                objComment.CreatedBy = int.Parse(dr["CreatedBy"].ToString());
                objComment.CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()).ToString("MMMM dd, yyyy");
                objComment.UserType = int.Parse(dr["UserType"].ToString());
                objComment.UserImage = dr["userImage"].ToString();
                objComment.IsAdmin = int.Parse(dr["IsAdmin"].ToString());
                objComment.UserName = dr["userName"].ToString();
                objComment.City = dr["City"].ToString();
                objComment.State = dr["State"].ToString();
                objComment.Country = dr["Country"].ToString();
                objComment.IsLocationHide = bool.Parse(dr["IsLocationHide"].ToString());
                objComment.FacebookUid = null;
                if (!string.IsNullOrEmpty(dr["FacebookUid"].ToString()))
                {
                    objComment.FacebookUid = (Nullable<Int64>)dr["FacebookUid"];
                }

                objCommentList.Add(objComment);
                objComment = null;
            }

            return objCommentList;
        }

         public void GetData(object[] objValue)
        {
            
        }
        public void InsertRecord(object[] Params)
        {
            try
            {
                Comments objComment = (Comments)Params[0];
                // Changed by rupendra on 21- jun-2011 to implement YT phase 4 enhancement 
                //string[] strParam = { "UserId", "TypeCodeId", "CommentTypeId", "Message", "IsPrivate", "CreatedBy", "IsActive", "IsDeleted" };
                //DbType[] enumDbType ={ DbType.Int64, DbType.Int64, DbType.Int64,DbType.String,DbType.Byte,DbType.Int64,DbType.Byte,DbType.Byte };
                //object[] objValue ={ objComment.UserId, objComment.TypeCodeId, objComment.CommentTypeId, objComment.Message, objComment.IsPrivate, objComment.CreatedBy, objComment.IsActive, objComment.IsDeleted };

                string[] strParam = { "UserId", "TypeCodeId", "CommentTypeId", "Message", "IsPrivate", "CreatedBy", "IsActive", "IsDeleted","UserName","UserType" };
                DbType[] enumDbType = { DbType.Int64, DbType.Int64, DbType.Int64, DbType.String, DbType.Byte, DbType.Int64, DbType.Byte, DbType.Byte,DbType.String,DbType.String  };
                object[] objValue = { objComment.UserId, objComment.TypeCodeId, objComment.CommentTypeId, objComment.Message, objComment.IsPrivate, objComment.CreatedBy, objComment.IsActive, objComment.IsDeleted, objComment.UserName,objComment.UserType };

                InsertRecord("usp_AddComments", strParam, enumDbType, objValue);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRecord(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Delete(object[] Params)
        {
            try
            {
                Comments objComment = (Comments)Params[0];
                // Changed on 23-jun-2011 for YT phase 4 by rupendra
                object[] objValue ={ objComment.CommentId, objComment.UserId, objComment.TableType };
                Delete("usp_deleteComment", objValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region IResourceAccess Members


        public object InsertDataAndReturnId(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Method to enter Module Comments
        /// </summary>
        /// <param name="Params">Filled Comment Entity.</param>
        public void InsertModuleComment(object[] Params)
        {
            try
            {
                Comments objComment = (Comments)Params[0];
                string[] strParam = { "UserId", "TypeCodeName", "CommentTypeId", "Message", "IsPrivate", "CreatedBy", "CreatedDate", "IsActive", "IsDeleted" };
                DbType[] enumDbType ={ DbType.Int64, DbType.String, DbType.Int64, DbType.String, DbType.Byte, DbType.Int64, DbType.DateTime, DbType.Byte, DbType.Byte };
                object[] objValue ={ objComment.UserId, objComment.CodeTypeName, objComment.CommentTypeId, objComment.Message, objComment.IsPrivate, objComment.CreatedBy, objComment.CreatedDate, objComment.IsActive, objComment.IsDeleted };
                InsertRecord("usp_InsertModuleComments", strParam, enumDbType, objValue);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
