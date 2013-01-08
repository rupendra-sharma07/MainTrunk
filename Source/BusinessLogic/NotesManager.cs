///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.NotesManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the basic methods for Notes
///Audit Trail     : Date of Modification  Modified By         Description



#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;
using TributesPortal.Utilities;
#endregion

namespace TributesPortal.BusinessLogic
{
    public partial class NotesManager
    {
        /// <summary>
        /// Method to send the request to Notes resource for the record insertion.
        /// </summary>
        /// <param name="objVid">Filled Notes Entity</param>
        /// <returns>Object containing the Identity or Error Message</returns>
        public void SaveNote(Note objNote)
        {
            NotesResource objNotesManager = new NotesResource();
            TributeResource objTributeResource = new TributeResource();
            Tributes objTribute = new Tributes();
            object[] param = { objNote };
            object noteId = objNotesManager.InsertDataAndReturnId(param);
            objNote.NotesId = int.Parse(noteId.ToString());
            //to send email
            objTribute.TributeId = int.Parse(objNote.UserTributeId.ToString());
            objTribute.TypeDescription = objNote.ModuleTypeName;
            object[] paramMail = { objTribute };
            List<UserInfo> objUser = objTributeResource.GetTributeAdministrators(paramMail);
            if (objUser.Count > 0)
            {
                SendEmail(objUser, objNote);
            }

            //Function to send the mail to the list of users who have added the Tribute in their list of favourites 
            List<UserInfo> objUserFav = objTributeResource.GetFavouriteTributeUsers(paramMail);
            if (objUserFav.Count > 0)
            {
                //As per discussion with Rupendra: will send the mail in "To" field. 
                //ie a comma separated list of users in the "to" field
                SendEmail(objUserFav, objNote);
            }
        }

        /// <summary>
        /// Method to get the list of notes for the selected tribute
        /// </summary>
        /// <param name="objNote">Note entity conatining tributeId</param>
        /// <returns>List of notes</returns>
        public List<Note> GetTributeNotes(Note objNote)
        {
            NotesResource objNotesResource = new NotesResource();
            object[] param = { objNote };
            return objNotesResource.GetTributeNotes(param);
        }

        /// <summary>
        /// Method to get the note detail based on the selected note
        /// </summary>
        /// <param name="objNote">Note entity containing note id</param>
        /// <returns>Filled note entity with the note details</returns>
        public Note GetNoteDetails(Note objNote)
        {
            NotesResource objNoteResource = new NotesResource();
            object[] param = { objNote };
            return objNoteResource.GetNoteDetail(param);
        }

        public void GetLastNoteForTribute(object[] objTributeId)
        {
            NotesResource objNoteResource = new NotesResource();
            objNoteResource.GetLastNoteForTribute(objTributeId);        
        }

        /// <summary>
        /// Method to update the note details
        /// </summary>
        /// <param name="objNote">Note entity containing notes data.</param>
        public void UpdateNote(Note objNote)
        {
            NotesResource objNoteResource = new NotesResource();
            object[] param = { objNote };
            objNoteResource.UpdateRecord(param);
        }

        /// <summary>
        /// Method to delete note
        /// </summary>
        /// <param name="objNote">Note entity containing noteid</param>
        public void DeleteNote(Note objNote)
        {
            NotesResource objNoteResource = new NotesResource();
            object[] param = { objNote };
            objNoteResource.Delete(param);
        }

        /// <summary>
        /// Method to send email to the list of users
        /// </summary>
        /// <param name="objUsers">User Info entity containing User name and email address</param>
        public void SendEmail(List<UserInfo> objUsers, Note objNotes)
        {
            EmailMessages objEmail = EmailMessages.Instance;
            foreach (UserInfo obj in objUsers)
            {
                StringBuilder sbToEmail = new StringBuilder();
                sbToEmail.Append(obj.UserEmail);
                sbToEmail.Append(",");
                bool val = objEmail.SendMessages("Your Tribute<" + WebConfig.NoreplyEmail + ">", sbToEmail.ToString(), GetEmailSubject(obj, objNotes), GetEmailBody(obj, objNotes), EmailMessages.TextFormat.Html.ToString());
            }
        }

        /// <summary>
        /// Method to get the body part of email.
        /// </summary>
        /// <param name="objUserInfo">Filled User Info entity</param>
        /// <returns>HTML string of body part</returns>
        private string GetEmailBody(UserInfo objUserInfo, Note objNotes)
        {
            StringBuilder sbBody = new StringBuilder();
            sbBody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>");
            sbBody.Append(objNotes.UserName);
            sbBody.Append(" added a new note in the ");
            sbBody.Append(objNotes.TributeName + " " + objNotes.TributeType + "  Tribute.</p>");
            sbBody.Append("<p>");
            sbBody.Append("To read the note, follow the link below:");
            sbBody.Append("<br/>");
            //sbBody.Append(objNotes.PathToVisit +  "/Notes/NoteFullView.aspx" + "?TributeId=" + objNotes.UserTributeId + "&TributeName=" + objNotes.TributeName + "&TributeType=" + objNotes.TributeType + "&TributeUrl=" + objNotes.TributeUrl + "&noteId=" + objNotes.NotesId + "&mode=link'>" + "Click here to visit the link</a>");
            string strLink = "http://" + objNotes.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objNotes.TributeUrl + "/note.aspx" + "?noteId=" + objNotes.NotesId;
            sbBody.Append("<a href='" + strLink + "'>" + strLink + "</a><p>");
            sbBody.Append("<p>---<br/>");
            sbBody.Append("Your Tribute Team</p></font>");

            return sbBody.ToString();
        }

        /// <summary>
        /// Method to get subject for email.
        /// </summary>
        /// <param name="objUserInfo"></param>
        /// <param name="objNotes"></param>
        /// <returns>Subject of email in string format.</returns>
        private string GetEmailSubject(UserInfo objUserInfo, Note objNotes)
        {
            return objNotes.UserName + " added a new note on Your Tribute...";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public string GetTributeEndDate(int tributeId)
        {
            NotesResource objNoteResource = new NotesResource();            
            return objNoteResource.GetTributeEndDate(tributeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public int GetCurrentNotes(int tributeId)
        {
            NotesResource objNoteResource = new NotesResource();
            return objNoteResource.GetCurrentNotes(tributeId);
        }

        public int GetCurrentEvents(int tributeId)
        {
            NotesResource objNoteResource = new NotesResource();
            return objNoteResource.GetCurrentEvents(tributeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool GetCustomHeaderDetail(int tributeId)
        {
            NotesResource objNoteResource = new NotesResource();
            return objNoteResource.GetCustomHeaderDetail(tributeId);
        }

    }//end class
}//end namespace
