///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.Utilities.PaymentGateWay.cs
///Author          : 
///Creation Date   : 
///Description     : This page is used to send data to the payment gateway to make payments to keep tributes online
///Audit Trail     : Date of Modification  Modified By         Description
///

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web;

//using com.optimalpayments.test.webservices;//COMDIFF: comment this and uncomment the following line
using com.optimalpayments.webservices;


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
        public static CardTypeV1 CardType;

        public PaymentGateWay()
        {
            BillingDetailsV1 = new BillingDetailsV1();
        }

        public bool PayYourBill(string strCardNumber, string strCCNumber, int strCardExpiryMonth, int strCardExpiryYear
             , string strAmount, CardTypeV1 CardType, string strFirstName, string strLastname,
             string strStreet, string strCity, StateV1 State, CountryV1 Country, string ZipCode,
             string strPhone, string strEmail, string strCustomerIp, out string confirmationId, out string errorMesg)
        {
            try
            {
                CCAuthRequestV1 ccAuthRequest = new CCAuthRequestV1();
                MerchantAccountV1 merchantAccount = new MerchantAccountV1();
                merchantAccount.accountNum = WebConfig.AccountNumber;
                merchantAccount.storeID = WebConfig.StoteId;
                merchantAccount.storePwd = WebConfig.StotePwd;
                ccAuthRequest.merchantAccount = merchantAccount;
                ccAuthRequest.merchantRefNum = WebConfig.ReferancePrefix + DateTime.Now.ToString();

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
                    errorMesg = ccTxnResponse.description;
                    return true;
                    //  return "Transaction Successful.";
                }
                else
                {
                    confirmationId = string.Empty;
                    errorMesg = ccTxnResponse.description;
                    return false;
                    //  return ("Transaction Failed with decision: " + ccTxnResponse.decision);
                }
            }
            catch
            {
                throw new ApplicationException("PAYMENT");
                confirmationId = string.Empty;
                errorMesg = "Transaction Failed. Please try again later.";
                return false;
                
            }

        }

        public bool PayYourBill(string strCardNumber, string strCCNumber, int strCardExpiryMonth, int strCardExpiryYear
             , double strAmount, CardTypeV1 CardType, string strFirstName, string strLastname,
             string strStreet, string strCity, StateV1 State, CountryV1 Country, string ZipCode,
             string strPhone, string strEmail, string strCustomerIp,  string confirmationId,  string errorMesg)
        {
            try
            {
                #region Optimal payment code
                //CCAuthRequestV1 ccAuthRequest = new CCAuthRequestV1();
                //MerchantAccountV1 merchantAccount = new MerchantAccountV1();
                //merchantAccount.accountNum = WebConfig.AccountNumber;
                //merchantAccount.storeID = WebConfig.StoteId;
                //merchantAccount.storePwd = WebConfig.StotePwd;
                //ccAuthRequest.merchantAccount = merchantAccount;
                //ccAuthRequest.merchantRefNum = WebConfig.ReferancePrefix + DateTime.Now.ToString();

                //ccAuthRequest.amount = strAmount.ToString();
                //CardV1 card = new CardV1();
                //card.cardNum = strCardNumber;
                //CardExpiryV1 cardExpiry = new CardExpiryV1();
                //cardExpiry.month = strCardExpiryMonth;
                //cardExpiry.year = strCardExpiryYear;
                //card.cardExpiry = cardExpiry;
                //card.cardType = CardType;
                //card.cardTypeSpecified = true;
                //card.cvdIndicator = 1;
                //card.cvdIndicatorSpecified = true;
                //card.cvd = strCCNumber;
                //ccAuthRequest.card = card;
                //BillingDetailsV1 billingDetails = new BillingDetailsV1();
                //billingDetails.cardPayMethod = CardPayMethodV1.WEB; //WEB = Card Number Provided
                //billingDetails.cardPayMethodSpecified = true;
                //billingDetails.firstName = strFirstName;
                //billingDetails.lastName = strLastname;
                //billingDetails.street = strStreet;
                //billingDetails.city = strCity;
                ////billingDetails.Item = (object)State; // California
                ////billingDetails.country = Country; // United States
                //billingDetails.countrySpecified = false;
                //billingDetails.zip = ZipCode;
                //billingDetails.phone = strPhone;
                //billingDetails.email = strEmail;
                //ccAuthRequest.billingDetails = billingDetails;
                //ccAuthRequest.previousCustomer = true;
                //ccAuthRequest.previousCustomerSpecified = true;
                //ccAuthRequest.customerIP = strCustomerIp;
                //ccAuthRequest.productType = ProductTypeV1.M;
                ////M = Both Digital and Physical(e.g., software downloaded followed by media
                ////shipment)
                //ccAuthRequest.productTypeSpecified = true;
                ////Request a 3D Secure Lookup
                //CardRiskServiceV1[] riskServices = { CardRiskServiceV1.TDS };
                //ccAuthRequest.cardRiskService = riskServices;
                //// Perform the Web Services call for the purchase
                //CreditCardServiceV1 ccService = new CreditCardServiceV1();
                //CCTxnResponseV1 ccTxnResponse = ccService.ccPurchase(ccAuthRequest);
                //// Print out the result
                //String responseTxt = ccTxnResponse.code + " - " + ccTxnResponse.decision + " - "
                //+ ccTxnResponse.description + Environment.NewLine;
                //// Print out the PAReq and ACSUrl
                //if ((ccTxnResponse.tdsResponse != null) &&
                //(ccTxnResponse.tdsResponse.paymentRequest != null) &&
                //(ccTxnResponse.tdsResponse.acsURL != null))
                //{
                //    responseTxt += "PaReq: " + ccTxnResponse.tdsResponse.paymentRequest +
                //    Environment.NewLine +
                //    "ACSUrl: " + ccTxnResponse.tdsResponse.acsURL +
                //    Environment.NewLine;
                //}
                //responseTxt += "Details:" + Environment.NewLine;
                //if (ccTxnResponse.detail != null)
                //{
                //    for (int i = 0; i < ccTxnResponse.detail.Length; i++)
                //    {
                //        responseTxt += " - " + ccTxnResponse.detail[i].tag + " - " +
                //        ccTxnResponse.detail[i].value + Environment.NewLine;
                //    }
                //}
                //responseTxt = responseTxt.Replace("\n", Environment.NewLine);
                //System.Console.WriteLine(responseTxt);
                //if (DecisionV1.ACCEPTED.Equals(ccTxnResponse.decision))
                //{
                //    confirmationId = ccTxnResponse.confirmationNumber;
                //    errorMesg = ccTxnResponse.description;
                //    return true;
                //    //  return "Transaction Successful.";
                //}
                //else
                //{
                //    confirmationId = string.Empty;
                //    errorMesg = ccTxnResponse.description;
                //    return false;
                //    //  return ("Transaction Failed with decision: " + ccTxnResponse.decision);
                //}

                #endregion

                #region BeanStream code
                errorMesg = "";
                confirmationId = "";
                var trnOrderNumber = Guid.NewGuid().ToString().Substring(0, 10);
                var errorPage = "http://localhost:4941/DevelopmentWebsite/Create.aspx";  //ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "tributes/TributeCreation.aspx";
                var approvedPage = "http://localhost:4941/DevelopmentWebsite/Create.aspx?AccountType=3";//ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "tributes/TributeCreation.aspx";
                var declinedPage = "http://localhost:4941/DevelopmentWebsite/Create.aspx?AccountType=3";//ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "tributes/TributeCreation.aspx";
                var username = ConfigurationManager.AppSettings["BeanUserName"];
                var password = ConfigurationManager.AppSettings["BeanUserPwd"];
                var month = strCardExpiryMonth.ToString().Length > 0 && strCardExpiryMonth.ToString().Length == 1 ? "0" + strCardExpiryMonth.ToString() : strCardExpiryMonth.ToString();
                var year = strCardExpiryYear.ToString().Length > 0 && strCardExpiryYear.ToString().Length == 4 ? strCardExpiryYear.ToString().Substring(2, 2) : strCardExpiryYear.ToString();

                var redirectedURL = ConfigurationManager.AppSettings["BeanStreamUrl"]
                    + "?merchant_id=" + ConfigurationManager.AppSettings["MerchantId"]
                    //+ "&requestType=" + ConfigurationManager.AppSettings["requestType"]
                    + "&trnType=" + ConfigurationManager.AppSettings["trnType"]
                    + "&trnOrderNumber=" + trnOrderNumber
                    + "&trnAmount=" + strAmount
                    + "&trnCardOwner=" + strFirstName + " " + strLastname
                    + "&trnCardNumber=" + strCardNumber
                    + "&trnExpMonth=" + month
                    + "&trnExpYear=" + year
                    + "&ordName=" + strFirstName + " " + strLastname
                    + "&ordAddress1=" + strStreet
                    + "&ordCity=" + strCity
                    + "&ordProvince=" + State
                    + "&ordCountry=" + Country
                    + "&ordPostalCode=" + ZipCode
                    + "&ordPhoneNumber=" + strPhone
                    + "&ordEmailAddress=" + strEmail
                    + "&errorPage=" + errorPage
                    + "&approvedPage=" + approvedPage
                    + "&declinedPage=" + declinedPage
                    + "&trnCardCvd=" + strCCNumber
                    + "&username=" + username
                    + "&password=" + password;
                HttpContext.Current.Response.Redirect(redirectedURL);
                return true;
                #endregion
            }
            catch
            {
                throw new ApplicationException("PAYMENT");
                //confirmationId = string.Empty;
                errorMesg = "Transaction Failed. Please try again later.";
                return false;

            }

        }

        #region BeanStream

        // New added to Call BeanStream server

        public string PayYourBill(string strCardNumber, string strCCNumber, int strCardExpiryMonth, int strCardExpiryYear
                 , double strAmount, CardTypeV1 CardType, string strFirstName, string strLastname,
                 string strStreet, string strCity, StateV1 State, CountryV1 Country, string ZipCode,
                 string strPhone, string strEmail, string strCustomerIp, out string confirmationId, out string errorMesg, out bool bIsSuccess)
        {
            try
            {
                #region BeanStream code
                errorMesg = "";
                confirmationId = "";
                bIsSuccess = false;
                var trnOrderNumber = Guid.NewGuid().ToString().Substring(0, 10);

                var errorPage = "";
                var approvedPage = "";
                var declinedPage = "";
                if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                {
                    errorPage = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "moments/MomentsCreation.aspx";
                    approvedPage = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "moments/MomentsCreation.aspx";
                    declinedPage = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "moments/MomentsCreation.aspx";
                }
                else
                {
                    errorPage = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "tributes/TributeCreation.aspx";
                    approvedPage = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "tributes/TributeCreation.aspx";
                    declinedPage = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"] + "tributes/TributeCreation.aspx";
                }
                var username = ConfigurationManager.AppSettings["BeanUserName"];
                var password = ConfigurationManager.AppSettings["BeanUserPwd"];
                var month = strCardExpiryMonth.ToString().Length > 0 && strCardExpiryMonth.ToString().Length == 1 ? "0" + strCardExpiryMonth.ToString() : strCardExpiryMonth.ToString();
                var year = strCardExpiryYear.ToString().Length > 0 && strCardExpiryYear.ToString().Length == 4 ? strCardExpiryYear.ToString().Substring(2, 2) : strCardExpiryYear.ToString();

                var redirectedURL = ConfigurationManager.AppSettings["BeanStreamUrl"]
                    + "?merchant_id=" + ConfigurationManager.AppSettings["MerchantId"]
                    + "&requestType=" + ConfigurationManager.AppSettings["requestType"]
                    + "&trnType=" + ConfigurationManager.AppSettings["trnType"]
                    + "&trnOrderNumber=" + trnOrderNumber
                    + "&trnAmount=" + strAmount
                    + "&trnCardOwner=" + strFirstName + " " + strLastname
                    + "&trnCardNumber=" + strCardNumber
                    + "&trnExpMonth=" + month
                    + "&trnExpYear=" + year
                    + "&ordName=" + strFirstName + " " + strLastname
                    + "&ordAddress1=" + strStreet
                    + "&ordCity=" + strCity
                    + "&ordProvince=" + State
                    + "&ordCountry=" + Country
                    + "&ordPostalCode=" + ZipCode
                    + "&ordPhoneNumber=" + strPhone
                    + "&ordEmailAddress=" + strEmail
                    + "&errorPage=" + errorPage
                    + "&approvedPage=" + approvedPage
                    + "&declinedPage=" + declinedPage
                    + "&trnCardCvd=" + strCCNumber
                    + "&username=" + username
                    + "&password=" + password;

                // Caliing Beanstream server
                string sResponceText = CallBeanStream(redirectedURL);
                if (!string.IsNullOrEmpty(sResponceText) && sResponceText.Contains("trnApproved=1"))
                    bIsSuccess = true;
                else if (!string.IsNullOrEmpty(sResponceText) && sResponceText.Contains("trnApproved=0"))
                    bIsSuccess = false;

                var sResponseArr = sResponceText.Split('&');
                if (sResponseArr.Length >= 2)
                    confirmationId = sResponseArr[1].Split('=')[1];
                else confirmationId = "0";

                return sResponceText;
                #endregion
            }
            catch (Exception ex)
            {
                throw new ApplicationException("PAYMENT");
            }


        }

        // Function to call BeanStream server
        public string CallBeanStream(string sRequestString)
        {
            //throw new NotImplementedException();
            System.Net.WebRequest _objReq = System.Net.WebRequest.Create(sRequestString);
            //System.Net.WebResponse _objResponce = _objReq.GetResponse();
            using (System.Net.WebResponse response = _objReq.GetResponse())
            using (System.IO.Stream responseStream = response.GetResponseStream())
            using (System.IO.StreamReader reader = new System.IO.StreamReader(responseStream))
            {
                Console.WriteLine(((System.Net.HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.

                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                return responseFromServer;
            }

        }
        

        #endregion

    }
}
