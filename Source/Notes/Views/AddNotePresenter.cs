///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Notes.Views.AddNotePresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Add Note pages.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Notes.Views
{
    public class AddNotePresenter : Presenter<IAddNote>
    {
        #region CLASS VARIABLES
        private NotesController _controller;
        #endregion

        #region CONSTRUCTOR
        public AddNotePresenter([CreateNew] NotesController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        //public override void OnViewLoaded()
        //{
        //    // TODO: Implement code that will be executed every time the view loads
        //}

        //public override void OnViewInitialized()
        //{
        //    // TODO: Implement code that will be executed the first time the view loads
        //}

        /// <summary>
        /// Method to save Note in database
        /// </summary>
        public void SaveNote(Note objNote)
        {
            _controller.SaveNote(objNote);
        }

        public void GetNoteDetails(int noteId)
        {
            Note objNote = new Note();
            objNote.NotesId = noteId;
            this.View.NoteDetails = _controller.GetNoteDetails(objNote);
        }

        /// <summary>
        /// Method to update note details
        /// </summary>
        /// <param name="objNote">Filled note entity.</param>
        public void UpdateNote(Note objNote)
        {
            _controller.UpdateNote(objNote);
        }

        #endregion
    }
}




