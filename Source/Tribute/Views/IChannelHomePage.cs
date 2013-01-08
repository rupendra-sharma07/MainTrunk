///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.IChannelHomePage.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the channel home page.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using System.Collections;


namespace TributesPortal.Tribute.Views
{
    public interface IChannelHomePage
    {
        IList<SearchTribute> FeaturedTributes { set; }
        
    }
}
