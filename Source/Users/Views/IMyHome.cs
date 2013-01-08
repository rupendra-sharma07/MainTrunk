///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Users.Views.IMyHome.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the My Home
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.Users.Views
{
    public interface IMyHome
    {
         int UserId { get;}
        int SendbyUserId { get;}
        string Subject { get;}
        string EmailBody { get;}
        IList<Events> MyEvents { set;}
        IList<GetMyTributes> Mytributes { set;}
        IList<GetMyTributes> MyFavourites { set;}
        IList<MailMessage> UserInbox { set;}
        IList<MailMessage> UserSentItem { set;}        
        IList<ParameterTypesCodes> TributeTypes { set; }
        IList<ParameterTypesCodes> TributeTypes2 { set; }  
        
        
        //System.Int32 TributeId { get;set;}
        //System.String TributeName { get;set;}
        //System.String TypeDescription { get;set;}
        //System.DateTime StartDate { get;set;}
        //System.String Enddate { get;set;}
        //System.Int32 Visit { get;}
        //System.Boolean EmailAlert { get;set;}
    }
}
