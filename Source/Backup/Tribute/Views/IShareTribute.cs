///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.IShareTribute.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the sharing the tribute.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

#endregion
 
namespace TributesPortal.Tribute.Views
{
    public interface IShareTribute
    {
        #region PROPERTIES

        IList<GuestList> GuestList { get; }
        int UserID { get; }
        int TributeID { get; }
        bool IsAdmin { get; set;}
        int GuestCount { set;}
        string URL { get;}
        string EmailBody { get; }
        string EmailSubject { get;}
        string EmailFrom { get; }
        string PersonalMessage { get; }

        string TributeType { get; }
        int InvitationCategoryID { get; }
        IList<EventInvitationCategory> EventInvitationCategoryList { set; }
        IList<EventTheme> EventThemeList { set; }
        string EventThemePreview { set; }
        int EventThemeID { get; }

        #endregion
    }
}




