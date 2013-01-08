///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Video.Views.IVideoUpload.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Video Upload
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Video.Views
{
    public interface IVideoUpload
    {
        int TributeId { get;}
        string TokenId { get;}
        string VideoCaption { get;}
        string VideoDesc { get;}
        string VideoTributeId { get;}
        string TributeName { get; }
        string TributeUrl { get; }
        string TributeType { get; }

        List<Tributes> TributesList { set;}

        UserRegistration UserDetails { get; set;}
        int UserId { get; set;}
        VideoToken TokenDetails { get; set;}
        
    }
}




