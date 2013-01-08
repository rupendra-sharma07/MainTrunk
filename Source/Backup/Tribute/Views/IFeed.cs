///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.IFeed.cs
///Author          : Laxman Hari Kulshrestha
///Creation Date   : 3:13 PM 3/30/2011
///Description     : This is the Interface for the Feed pages.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using System.Collections;
namespace TributesPortal.Tribute.Views
{
    public interface IFeed
    {
        //IList<GetMyTributes> Mytributes
        IList<GetTributesFeed> ObjTributeList { get; set; }
        IList<ParameterTypesCodes> TributeTypes { set; }
        string TributeHomeUrl { get; set; }
        string TributeImageUrl { get; set; }
    }
}
