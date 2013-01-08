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
///File Name       : TributesPortal.Notes.NotesController.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Controller class for the files under Notes.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;
#endregion

namespace TributesPortal.Notes
{
    public class NotesController
    {
        #region CONSTRUCTOR
        public NotesController()
        {
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Method to Save note to database
        /// </summary>
        /// <param name="objNote">Filled Note entity</param>
        public void SaveNote(Note objNote)
        {
            FacadeManager.NotesManager.SaveNote(objNote);
        }

        /// <summary>
        /// Method to get the list of notes for the selected tribute
        /// </summary>
        /// <param name="objNote">Note entity containing TributeId, Page size and Current page</param>
        /// <returns>List of Tribute Notes</returns>
        public List<Note> GetTributeNotes(Note objNote)
        {
            return FacadeManager.NotesManager.GetTributeNotes(objNote);
        }

        /// <summary>
        /// Method to get the details of notes for the selected tribute
        /// </summary>
        /// <param name="objNote">Note entity containing Note Id</param>
        /// <returns>Note entity containing details</returns>
        public Note GetNoteDetails(Note objNote)
        {
            return FacadeManager.NotesManager.GetNoteDetails(objNote);
        }

        /// <summary>
        /// Method to get the list of comments for Notes
        /// </summary>
        /// <param name="objSession">Filled CommentTributeAdmin Entity</param>
        /// <returns>List of comments for selected Note</returns>
        public List<CommentTributeAdministrator> GetModuleComments(CommentTributeAdministrator objSession)
        {
            return FacadeManager.CommentMgr.GetModuleComments(objSession);
        }

        /// <summary>
        /// Method to save comment for note
        /// </summary>
        /// <param name="Comment">Filled Comment Entity.</param>
        public void SaveComment(Comments Comment)
        {
            FacadeManager.CommentMgr.InsertModuleComment(Comment);
        }

        /// <summary>
        /// Method to update note
        /// </summary>
        /// <param name="Comment">Filled note entity.</param>
        public void UpdateNote(Note objNote)
        {
            FacadeManager.NotesManager.UpdateNote(objNote);
        }

        /// <summary>
        /// Method to delete note
        /// </summary>
        /// <param name="objNote">Note entity containing noteid</param>
        public void DeleteNote(Note objNote)
        {
            FacadeManager.NotesManager.DeleteNote(objNote);
        }

        /// <summary>
        /// Method to delete comment
        /// </summary>
        /// <param name="objComment">Comment entity containing comment id</param>
        public void DeleteComment(Comments objComment)
        {
            FacadeManager.CommentMgr.DeleteComment(objComment);
        }
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public string GetTributeEndDate(int tributeId)
        {
            return FacadeManager.NotesManager.GetTributeEndDate(tributeId);
        }

        public int GetCurrentNotes(int tributeId)
        {
            return FacadeManager.NotesManager.GetCurrentNotes(tributeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool GetCustomHeaderDetail(int tributeId)
        {
            return FacadeManager.NotesManager.GetCustomHeaderDetail(tributeId);
        }
        #endregion
    }//end class
}//end namespace
