///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Event.Views.IInviteGuest.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Event Invite Guest.
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

#endregion

namespace TributesPortal.Event.Views
{
    public interface IInviteGuest
    {
        #region PROPERTIES

        IList<GuestList> GuestList { get; }
        int EventID { get; }
        int UserID { get; }
        int TributeID { get; }
        bool IsAdmin { get; set;}
        int GuestCount { set;}
        string URL { get;}
        string FirstName { get;}
        string LastName { get;}
        string TributeName { get; }
        string TributeType { get; }
        string TributeURL { get; }
        int InvitationCategoryID { get; }
        IList<EventInvitationCategory> EventInvitationCategoryList { set; }
        IList<EventTheme> EventThemeList { set; }
        string EventThemePreview { set; }
        string EventMessage { get; }
        int EventThemeID { get; }


        #endregion
    }
}




