///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.IPaymentConfirmation.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Interface for the payment confirmation 
///                  page displayed after a tribute is sponsored.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Tribute.Views
{
    public interface IPaymentConfirmation
    {
        int TransactionId { get;}
        PaymentReceipt TransactionDetails { set; }
    }
}




