///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Users.Views.IBillingSummary.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the Billing Summary
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.Users.Views
{
    public interface IBillingSummary
    {
        IList<PaymentReceipt> PaymentReceipt { set; }
    }
}




