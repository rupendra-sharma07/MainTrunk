///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Notes.Views.INoteFullView.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Note Full View pages.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Notes.Views
{
    public interface INoteFullView
    {
        Note NoteDetails { set;}
        IList<CommentTributeAdministrator> Comments { set; }
        string DrawPaging { set;}
        int CommentCount { set; get; }
        string RecordCount { set;}
    }
}




