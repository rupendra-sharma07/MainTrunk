///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Photo.Views.IManagePhoto.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the manage Photo pages. 
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Photo.Views
{
    public interface IManagePhoto
    {
        int PhotoId {get;}
        int UserId {get;}
        string TributeName { get; }
        string TributeType { get; }
        string TributeUrl { get; }
        string PhotoCaption { get;}
        string PhotoDesc { get;}
        
        Photos PhotoDetails { set;}
    }
}




