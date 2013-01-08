///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Notes.Views.ITributeNotes.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Tribute Notes pages.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Notes.Views
{
    public interface ITributeNotes
    {
        IList<Note> TributeNotesList { set; }
        int TotalRecords { set;}
        string DrawPaging { set;}
        string RecordCount { set;}
    }
}




