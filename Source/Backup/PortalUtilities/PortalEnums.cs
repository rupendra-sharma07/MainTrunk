///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Utilities.PortalEnums.cs
///Author          : 
///Creation Date   : 
///Description     : This file is used to maintain the enums used within the application
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TributesPortal.Utilities
{
    public class PortalEnums
    {
        public enum ResponseStatus
        {
            Success = 101,
            Fail = 102,
        }

        public enum TributeTypeEnum
        {
            Memorial,
            Wedding,
            Anniversary,
            Birthday,

            [Description("New Baby")]
            NewBaby,

            Graduation,
            All
        }

        public enum EventAttending
        {
            [Description("Awaiting Response")]
            AwaitingResponse,
            Attending,
            [Description("Maybe Attending")]
            MaybeAttending,
            [Description("Not Attending")]
            NotAttending,
        }

        public enum SessionValueEnum
        {
            objSessionvalue,
            TributeSession,
            TributeList,
            ShareTributeEmail,
            SearchPageName
        }

        public enum TributeContentEnum
        {
            Tribute,
            Story,
            Gift,
            Event,
            ManageEvent,
            EventFullView,
            InviteGuest,
            SearchResult,
            AdvanceSearch,
            ShareTribute,
            GuestBook,
            Notes,
            Photo,
            Video
        }

        public enum PageNameEnum
        {
            story_story_aspx,
            gift_gift_aspx,
            event_event_aspx,
            event_eventfullview_aspx,
            event_manageevent_aspx,
            event_inviteguest_aspx,
            tribute_advancesearch_aspx,
            tribute_searchresult_aspx,
            tribute_alltribute_aspx,
            tribute_sharetribute_aspx
        }

        public enum AdminInfoEnum
        {
            UserAdminInfo_Story,
            UserAdminInfo_Gift,
            UserAdminInfo_Event,
            UserAdminInfo_ManageEvent,
            UserAdminInfo_EventFullView,
            UserAdminInfo_InviteGuest,
            UserAdminInfo_Search
        }

        public enum ModuleName
        {
            GuestBook,
            Notes,
            Photo,
            Video
        }

        public enum SearchEnum
        {
            Advance,
            Basic,
            Search,
            Tributes
        }

        public enum Sorting
        {
            DESC,
            ASC
        }

        public enum EventStateEnum
        {
            EventImageURL,
            Event_Admin
        }
    }
}
