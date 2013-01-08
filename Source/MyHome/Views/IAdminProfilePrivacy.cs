///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.IAdminProfilePrivacy.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Admin Profile Privacy.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.MyHome.Views
{
    public interface IAdminProfilePrivacy
    {
        string BannerMessage { get;set;}
        int UserId { get;set;}
        bool IsUsernameVisiable { get;set;}
        bool AllowIncomingMsg { get;set;}
        bool IsLocationHide { get;set;}
        bool IsVisitCountHide { get; set; }
    }
}
