///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.GuestBook.Views.IGuestBook.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the GuestBook.
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
namespace TributesPortal.GuestBook.Views
{
    public interface IGuestBook
    {
        IList<CommentTributeAdministrator> Comments { set; }
        string RecordCount { set;}
        string DrawPaging { set;}
        string UserImage{set;}
        //IList<Paging> RecordCount { set; }
    }
}




