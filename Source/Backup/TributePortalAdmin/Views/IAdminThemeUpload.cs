///Copyright       : Copyright (c) Optimus Information Inc. 
///Project         : Timeless Tributes
///File Name       : TributesPortal.TributePortalAdmin.Views.IAdminThemeUpload.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Tribute Summary Report
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.TributePortalAdmin.Views
{
    public interface IAdminThemeUpload
    {
        string TributeType { get; }
        int TributeTypeID { get; }
        int InvitationCategoryID { get; }
        string ThumbnailImagePath { get; set; }
        string PreviewImagePath { get; set; }
        string FullSizeImagePath { get; set; }
        IList<EventInvitationCategory> EventInvitationCategoryList{ set; }
        string InvitationCategoryName { get; set; }
        string EventThemeName { get; }
        string ThemeBackgroundColor { get; }
    }
}
