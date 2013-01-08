///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.Services.TributePortalSchedulerService.PaymentGateWay.cs
///Author          : 
///Creation Date   : 
///Description     : This page is used to send data to the payment gateway to make payments to keep tributes online
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Data;
using System.Configuration;
using System.Web;
//using TributePortalSchedulerService.com.optimalpayments.webservices;
using TributePortalSchedulerService.com.optimalpayments.test.webservices1;


/// <summary>
/// Summary description for PaymentGateWay
/// </summary>
namespace TributesPortal.Utilities
{
    public class PaymentGateWay
    {
        public BillingDetailsV1 BillingDetailsV1;
        public static StateV1 State;
        public static CountryV1 Country;

        public PaymentGateWay()
        {
            BillingDetailsV1 = new BillingDetailsV1();
        }

        public bool PayYourBill(string strCardNumber, string strCCNumber, int strCardExpiryMonth, int strCardExpiryYear
             , string strAmount, CardTypeV1 CardType, string strFirstName, string strLastname,
             string strStreet, string strCity, StateV1 State, CountryV1 Country, string ZipCode,
             string strPhone, string strEmail, string strCustomerIp, out string confirmationId)
        {
            try
            {
            CCAuthRequestV1 ccAuthRequest = new CCAuthRequestV1();
            MerchantAccountV1 merchantAccount = new MerchantAccountV1();
            merchantAccount.accountNum = ConfigurationSettings.AppSettings["AccountNumber"].ToString();
            merchantAccount.storeID = ConfigurationSettings.AppSettings["StoteId"].ToString();
            merchantAccount.storePwd = ConfigurationSettings.AppSettings["StotePwd"].ToString();
            ccAuthRequest.merchantAccount = merchantAccount;
            ccAuthRequest.merchantRefNum = ConfigurationSettings.AppSettings["ReferancePrefix"].ToString() + "1122345";

            ccAuthRequest.amount = strAmount + ".00";
            CardV1 card = new CardV1();
            card.cardNum = strCardNumber;
            CardExpiryV1 cardExpiry = new CardExpiryV1();
            cardExpiry.month = strCardExpiryMonth;
            cardExpiry.year = strCardExpiryYear;
            card.cardExpiry = cardExpiry;
            card.cardType = CardType;
            card.cardTypeSpecified = true;
            card.cvdIndicator = 1;
            card.cvdIndicatorSpecified = true;
            card.cvd = strCCNumber;
            ccAuthRequest.card = card;
            BillingDetailsV1 billingDetails = new BillingDetailsV1();
            billingDetails.cardPayMethod = CardPayMethodV1.WEB; //WEB = Card Number Provided
            billingDetails.cardPayMethodSpecified = true;
            billingDetails.firstName = strFirstName;
            billingDetails.lastName = strLastname;
            billingDetails.street = strStreet;
            billingDetails.city = strCity;
            //billingDetails.Item = (object)State; // California
            //billingDetails.country = Country; // United States
            billingDetails.countrySpecified = false;
            billingDetails.zip = ZipCode;
            billingDetails.phone = strPhone;
            billingDetails.email = strEmail;
            ccAuthRequest.billingDetails = billingDetails;
            ccAuthRequest.previousCustomer = true;
            ccAuthRequest.previousCustomerSpecified = true;
            ccAuthRequest.customerIP = strCustomerIp;
            ccAuthRequest.productType = ProductTypeV1.M;
            //M = Both Digital and Physical(e.g., software downloaded followed by media
            //shipment)
            ccAuthRequest.productTypeSpecified = true;
            //Request a 3D Secure Lookup
            CardRiskServiceV1[] riskServices = { CardRiskServiceV1.TDS };
            ccAuthRequest.cardRiskService = riskServices;
            // Perform the Web Services call for the purchase
            CreditCardServiceV1 ccService = new CreditCardServiceV1();
            CCTxnResponseV1 ccTxnResponse = ccService.ccPurchase(ccAuthRequest);
            // Print out the result
            String responseTxt = ccTxnResponse.code + " - " + ccTxnResponse.decision + " - "
            + ccTxnResponse.description + Environment.NewLine;
            // Print out the PAReq and ACSUrl
            if ((ccTxnResponse.tdsResponse != null) &&
            (ccTxnResponse.tdsResponse.paymentRequest != null) &&
            (ccTxnResponse.tdsResponse.acsURL != null))
            {
                responseTxt += "PaReq: " + ccTxnResponse.tdsResponse.paymentRequest +
                Environment.NewLine +
                "ACSUrl: " + ccTxnResponse.tdsResponse.acsURL +
                Environment.NewLine;
            }
            responseTxt += "Details:" + Environment.NewLine;
            if (ccTxnResponse.detail != null)
            {
                for (int i = 0; i < ccTxnResponse.detail.Length; i++)
                {
                    responseTxt += " - " + ccTxnResponse.detail[i].tag + " - " +
                    ccTxnResponse.detail[i].value + Environment.NewLine;
                }
            }
            responseTxt = responseTxt.Replace("\n", Environment.NewLine);
            System.Console.WriteLine(responseTxt);
            if (DecisionV1.ACCEPTED.Equals(ccTxnResponse.decision))
            {
                confirmationId = ccTxnResponse.confirmationNumber;
                return true;
                //  return "Transaction Successful.";
            }
            else
            {
                confirmationId = string.Empty;
                return false;
                //  return ("Transaction Failed with decision: " + ccTxnResponse.decision);
            }
        }
        catch
        {
            confirmationId = string.Empty;
            return false;
        }
    }
}
}
