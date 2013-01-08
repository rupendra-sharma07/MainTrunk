///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Users.Views.IUserRegistration.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the User Registration
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.Users.Views
{
    public interface IUserRegistration
    {
        IList<Locations> Locations { set; }
        IList<Locations> States { set; }
        IList<ParameterTypesCodes> BusinessType { set; }
        string UserAvailablity { set; }
        string UserEmail { get;}
        // added by udham for Your Moments
        string ApplicationType { get; }
    }
}




