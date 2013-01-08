///Copyright       : Copyright (c) Optimus Information
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.ModelPopup.GuestListFullDetails.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the full details of a guest in the guest list
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.Video.Views
{
   public interface IEditPersonalDetails
    {
        #region PROPERTIES

      // Properties for Personal detail section
        string TributeName { get; set; }        
        string Date1Day { get; set; }
        string Date1Month { get; set; }
        string Date1Year { get; set; }
        string Date2Day { get; set; }
        string Date2Month { get; set; }
        string Date2Year { get; set; }
        int Age { set; }
        string City { get; set; }
        string State { get; set; }
        string Country { get; set; }
    //    string Location { get; set; }
        IList<Locations> CountryList { set; }
        IList<Locations> StateList { set; }
        //bool isVisibleDate2 { set; }

       
      
        #endregion
    }
}
