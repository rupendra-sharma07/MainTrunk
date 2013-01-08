///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Notes.Views.TributeNotesPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Tribute Notes.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
#endregion

namespace TributesPortal.Notes.Views
{
    public class TributeNotesPresenter : Presenter<ITributeNotes>
    {
        #region CLASS VARIABLES
        private NotesController _controller;
        #endregion

        #region CONSTRUCTOR
        public TributeNotesPresenter([CreateNew] NotesController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Method to get the list of notes for tribute
        /// </summary>
        /// <param name="objNote">Filled Note entity.</param>
        public void GetTributeNotes(Note objNote)
        {
            List<Note> objNotesList = _controller.GetTributeNotes(objNote);
            //to update the text for location
            foreach (Note obj in objNotesList)
            {
                if (obj.IsLocationHide)
                {
                    obj.Location = string.Empty;
                }
                else
                {
                    if (obj.City == string.Empty && obj.State == string.Empty)
                        obj.Location = "(" + obj.Country + ")";
                    else
                    {
                        if (obj.City == string.Empty && obj.State != string.Empty)
                            obj.Location = "(" + obj.State + ", " + obj.Country + ")";
                        else if (obj.City != string.Empty && obj.State == string.Empty)
                            obj.Location = "(" + obj.City + ", " + obj.Country + ")";
                        else
                            obj.Location = "(" + obj.City + ", " + obj.State + ", " + obj.Country + ")";
                    }
                }
                if (obj.UserImage.StartsWith("http://") || obj.UserImage.StartsWith("https://"))
                {
                    obj.UserImage = obj.UserImage;
                }
                else
                {
                    obj.UserImage = CommonUtilities.GetPath()[2].ToString() + obj.UserImage;
                }
                //obj.UserImage = CommonUtilities.GetPath()[2].ToString() + obj.UserImage;
                if (obj.PostMessage.Length > (int.Parse(WebConfig.TextSize_TributeNotes)))
                    obj.PostMessage = obj.PostMessage.Substring(0, 255);
                //string[] strValue = obj.PostMessage.Split('<', obj.PostMessage.IndexOf('>'));
            }
            this.View.TributeNotesList = objNotesList;

            //if number of records are > 0 draw paging else set total record to 0.
            if (objNotesList.Count > 0)
            {
                this.View.TotalRecords = objNotesList[0].TotalRecords;
                this.View.DrawPaging = GetPaging(objNotesList[0].TotalRecords, objNote.PageSize, objNote.CurrentPage);
                this.View.RecordCount = GetNotesCount(objNote.CurrentPage, objNote.PageSize, objNotesList.Count, objNotesList[0].TotalRecords);
            }
            else
            {
                this.View.TotalRecords = 0;

            }
        }

        /// <summary>
        /// Method to delete note
        /// </summary>
        /// <param name="objNote">Note entity containing noteid</param>
        public void DeleteNote(Note objNote)
        {
            _controller.DeleteNote(objNote);
        }

        /// <summary>
        /// Method to get the method count text
        /// </summary>
        /// <param name="currentPage">Current page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="recordCount">Number of records to be displayed</param>
        /// <param name="totalRecords">Total number of records</param>
        /// <returns>String containing text to display</returns>
        private string GetNotesCount(int currentPage, int pageSize, int recordCount, int totalRecords)
        {
            int endCount = 0;
            string strMessage;
            int startCount = currentPage == 1 ? 1 : currentPage * pageSize - (pageSize - 1);

            if (recordCount < pageSize)
                endCount = currentPage * pageSize - (pageSize - recordCount);
            else
                endCount = currentPage * pageSize;

            if (recordCount > 1)
                strMessage = ResourceText.GetString("strNotes_TN") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_TN") + " " + endCount.ToString() + " " + ResourceText.GetString("strOf_TN") + " " + totalRecords;
            else
                strMessage = ResourceText.GetString("strNote_TN") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_TN") + " " + endCount.ToString() + " " + ResourceText.GetString("strOf_TN") + " " + totalRecords;

            return strMessage;
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

            return objUtilities.DrawPaging(currentPage, pageCount, "notes.aspx");
            //return DrawPaging(currentPage, pageCount);
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

        public int GetCurrentNotes(int _tributeId)
        {
            return _controller.GetCurrentNotes(_tributeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool GetCustomHeaderDetail(int tributeId)
        {
            return _controller.GetCustomHeaderDetail(tributeId);
        }
        #endregion
    }//end class
}//end namespace




