///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.PaymentConfirmationPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for displaying the payment confirmation page 
///                  after the sponsoring of a tribute.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using System.Web;



#endregion

namespace TributesPortal.Tribute.Views
{
    public class PaymentConfirmationPresenter : Presenter<IPaymentConfirmation>
    {
        #region CLASS VARIABLES
        private TributeController _controller;
        #endregion

        #region CONSTRUCTOR
        public PaymentConfirmationPresenter([CreateNew] TributeController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        public override void OnViewLoaded()
        {
            // TODO: Implement code that will be executed every time the view loads
        }

        public override void OnViewInitialized()
        {
            object[] param = { View.TransactionId };
            this.View.TransactionDetails = _controller.GetTransactionDetails(param);
        }


        public void OnViewInitializedForVideoTribute()
        {
            object[] param = { View.TransactionId };
            this.View.TransactionDetails = _controller.GetVideoTrUpgradeTransactionDetails(param);
        }
        #endregion

    }
}




