
///Copyright       : Copyright (c) Optimus Information
///Project         : Your Tributes
///File Name       : TributePortal.DevelopmentWebsite.Videos.VideoTribute.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This class defines the interface to be implemented  by  video type tribute pages  on the site
///Audit Trail     : Date of Modification  Modified By         Description



#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Video.Views
{
    public interface IVideoTribute
    {
        
        //LHK: for expiry message
        //string ErrorMessage { get; }

        //from story

        string TributeName { set; }
        string TributeImage { set; }
        int UserTributeId { get; set; }
        string TributeUserEmail { get; set; }
        bool IsPrivate { set; }
        
        DateTime? Date1 { set; }
        DateTime? Date2 { set; }
       
        int Age { set; }
        string City { get; set; }
        string State { get; set; }
        string Country { get; set; }
        //int? State { set; }
        //int Country { set; }
        bool IsOrderDVDChecked { set; }
        bool IsMemTributeBoxChecked { set; }
        int? LinkMemoTributeId { set; }
        int TributeType { set; }
        string Location { set; }


        //LHK: For video 20Nov2010

        int? VideoId { set; }
        int? VideoUserId { get; set; }
        //string VideoTypeId { set; }
        string TributeVideoId { set; }
        string TributeUrl { get; set; }
        //int CreatedBy { set; }
        //string VideoUrl { set; }
        //int UserId { set; }
        //List<Tributes> TributeDetails { set; }
        //List<Locations> LocationDetails { set; }
        // New propertirs added for Video  Tribute header by Agnesh
        //string BusinessAddress { set; }
        //string Phone { set; }
        //string HeaderBGColor { get; set; }
        //string HeaderLogo { set; }
        //string WebSite { set; }
        //bool IsAddressOn { set; }
        //bool IsPhoneOn { set; }
        //bool IsWebSiteOn { get; set; }
        bool IsCustomHeaderOn { get; set; }
        //string ObituaryURL { set; }
        //bool IsObituaryURLOn { set; }
        //string UserBussCity { get; set; }
        //string UserBussState { get; set; }

       
    }
}
