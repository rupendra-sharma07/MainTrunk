///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Notes.Views.NoteFullViewPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Note Full View.
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
//using TributesPortal.GuestBook;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
#endregion

namespace TributesPortal.Notes.Views
{
    public class NoteFullViewPresenter : Presenter<INoteFullView>
    {
        #region CLASS VARIABLES
        private NotesController _controller;
        //private GuestBookController _guestBookController = new GuestBookController();
        #endregion

        #region CONSTRUCTOR
        public NoteFullViewPresenter([CreateNew] NotesController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        public override void OnViewLoaded()
        {
            // TODO: Implement code that will be executed every time the view loads
        }

        public override void OnViewInitialized()
        {
            // TODO: Implement code that will be executed the first time the view loads
        }

        /// <summary>
        /// Method to load notes data
        /// </summary>
        /// <param name="objNote">Filled note entity</param>
        public void LoadNoteData(Note objNote, CommentTributeAdministrator objNoteComment)
        {
            Note objNoteDetails = _controller.GetNoteDetails(objNote);

            if (objNoteDetails.CreatedBy != null)
            {
                if (objNoteDetails.IsLocationHide) //if location is to be hidden.
                {
                    objNoteDetails.Location = string.Empty;
                }
                else
                {
                    if (objNoteDetails.City == string.Empty && objNoteDetails.State == string.Empty)
                        objNoteDetails.Location = "(" + objNoteDetails.Country + ")";
                    else
                    {
                        if (objNoteDetails.City == string.Empty && objNoteDetails.State != string.Empty)
                            objNoteDetails.Location = "(" + objNoteDetails.State + ", " + objNoteDetails.Country + ")";
                        else if (objNoteDetails.City != string.Empty && objNoteDetails.State == string.Empty)
                            objNoteDetails.Location = "(" + objNoteDetails.City + ", " + objNoteDetails.Country + ")";
                        else
                            objNoteDetails.Location = "(" + objNoteDetails.City + ", " + objNoteDetails.State + ", " + objNoteDetails.Country + ")";
                    }
                    //if (objNoteDetails.City == string.Empty && objNoteDetails.State == string.Empty)
                    //    objNoteDetails.Location = "(" + objNoteDetails.Country + ")";
                    //else
                    //    objNoteDetails.Location = "(" + objNoteDetails.City + ", " + objNoteDetails.State + ", " + objNoteDetails.Country + ")";
                }
                if (!(objNoteDetails.UserImage.StartsWith("http://") || objNoteDetails.UserImage.StartsWith("https://")))
                {
                    objNoteDetails.UserImage = CommonUtilities.GetPath()[2].ToString() + objNoteDetails.UserImage;
                }
            }
            this.View.NoteDetails = objNoteDetails; // _controller.GetNoteDetails(objNote);
            this.View.CommentCount = objNoteDetails.CommentCount;

            LoadComments(objNoteComment);

        }

        public void LoadComments(CommentTributeAdministrator objNoteComment)
        {
            //to get the comments list
            List<CommentTributeAdministrator> objComments = _controller.GetModuleComments(objNoteComment); // _guestBookController.CommentList(objNoteComment);

            //to change the \n from message to </br>
            List<CommentTributeAdministrator> objUpdatedComments = new List<CommentTributeAdministrator>();
            foreach (CommentTributeAdministrator obj in objComments)
            {
                CommentTributeAdministrator objComment = new CommentTributeAdministrator();
                objComment.City = obj.City;
                objComment.CommentId = obj.CommentId;
                objComment.CommentTypeId = obj.CommentTypeId;
                objComment.Country = obj.Country;
                objComment.CreatedBy = obj.CreatedBy;
                objComment.CreatedDate = obj.CreatedDate;
                objComment.CurrentPage = obj.CurrentPage;
                objComment.IsAdmin = obj.IsAdmin;
                objComment.IsPrivate = obj.IsPrivate;
                objComment.Message = obj.Message.Replace("\n", "</br>");
                objComment.PageSize = obj.PageSize;
                objComment.State = obj.State;
                objComment.TotalRecords = obj.TotalRecords;
                objComment.TributeId = obj.TributeId;
                objComment.TypeCodeId = obj.TypeCodeId;
                objComment.UserId = obj.UserId;
                if ((obj.UserImage.StartsWith("http://") || obj.UserImage.StartsWith("https://")))
                {
                    objComment.UserImage = obj.UserImage;
                }
                else
                {
                    objComment.UserImage = CommonUtilities.GetPath()[2].ToString() + obj.UserImage;
                }
                objComment.UserName = obj.UserName;
                objComment.UserType = obj.UserType;
                objComment.FacebookUid = obj.FacebookUid;

                if (obj.IsLocationHide)
                {
                    objComment.Location = string.Empty;
                }
                else
                {
                    if (obj.City == string.Empty && obj.State == string.Empty)
                        objComment.Location = "(" + obj.Country + ")";
                    else
                    {
                        if (obj.City == string.Empty && obj.State != string.Empty)
                            objComment.Location = "(" + obj.State + ", " + obj.Country + ")";
                        else if (obj.City != string.Empty && obj.State == string.Empty)
                            objComment.Location = "(" + obj.City + ", " + obj.Country + ")";
                        else
                            objComment.Location = "(" + obj.City + ", " + obj.State + ", " + obj.Country + ")";
                    }
                    //if (obj.City == string.Empty && obj.State == string.Empty)
                    //    objComment.Location = "(" + obj.Country + ")";
                    //else
                    //    objComment.Location = "(" + obj.City + ", " + obj.State + ", " + obj.Country + ")";
                }
                objUpdatedComments.Add(objComment);
            }

            this.View.Comments = objUpdatedComments;

            //to display the Message count
            this.View.RecordCount = GetMessageCount(objNoteComment.CurrentPage, objNoteComment.PageSize, objComments.Count, this.View.CommentCount);

            //to display the Paging
            this.View.DrawPaging = GetPaging(this.View.CommentCount, objNoteComment.PageSize, objNoteComment.CurrentPage);
        }

        /// <summary>
        /// Method to save comment on note
        /// </summary>
        /// <param name="objComment">Filled Comment entity</param>
        public void SaveComment(Comments objComment)
        {
            _controller.SaveComment(objComment);
        }

        /// <summary>
        /// Method to delete comment on note
        /// </summary>
        /// <param name="objComment">Filled Comment entity</param>
        public void DeleteComment(Comments objComment)
        {
            _controller.DeleteComment(objComment);
        }

        /// <summary>
        /// Method to create string for paging
        /// </summary>
        /// <param name="totalRecords">Total number of records</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="currentPage">Current size</param>
        /// <returns>string containing paging</returns>
        private string GetPaging(int totalRecords, int pageSize, int currentPage)
        {
            int pageCount = 0;
            if (totalRecords % pageSize == 0)
                pageCount = totalRecords / pageSize;
            else
                pageCount = (totalRecords / pageSize) + 1;

            CommonUtilities objUtilities = new CommonUtilities();
            return objUtilities.DrawPaging(currentPage, pageCount, "note.aspx");

            //return DrawPaging(currentPage, pageCount);
        }

        /// <summary>
        /// Method to get the method count text
        /// </summary>
        /// <param name="currentPage">Current page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="recordCount">Number of records to be displayed</param>
        /// <param name="totalRecords">Total number of records</param>
        /// <returns>String containing text to display</returns>
        private string GetMessageCount(int currentPage, int pageSize, int recordCount, int totalRecords)
        {
            int endCount = 0;
            string strMessage;
            int startCount = currentPage == 1 ? 1 : currentPage * pageSize - (pageSize - 1);

            if (recordCount < pageSize)
                endCount = currentPage * pageSize - (pageSize - recordCount);
            else
                endCount = currentPage * pageSize;

            if (recordCount > 1)
                strMessage = ResourceText.GetString("strMessages_GB") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_GB") + " " + endCount.ToString() + " " + ResourceText.GetString("strOf_GB") + " " + totalRecords;
            else
                strMessage = ResourceText.GetString("strMessage_GB") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_GB") + " " + endCount.ToString() + " " + ResourceText.GetString("strOf_GB") + " " + totalRecords;

            return strMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public string GetTributeEndDate(int tributeId)
        {
            return _controller.GetTributeEndDate(tributeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tributeId"></param>
        /// <returns></returns>
        public bool GetCustomHeaderDetail(int tributeId)
        {
            return _controller.GetCustomHeaderDetail(tributeId);
        }
        #endregion

    }//end class
}//end namespace




