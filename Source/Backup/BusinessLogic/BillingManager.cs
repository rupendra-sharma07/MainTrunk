///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.BillingManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the methods associated with billing
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;
using System.Resources;
//using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using System.Transactions;
using System.Web.SessionState;
using System.Web.UI;
using System.Web;

namespace TributesPortal.BusinessLogic
{
    public class BillingManager
    {
        public IList<BillingHistory> BillingHistory(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            return objBillingResource.GetBillingHistory(objValue);
        }

        public IList<PaymentReceipt> PaymentReceipt(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            return objBillingResource.PaymentReceipt(objValue);
        }

        public void GetCreditCardDetails(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            objBillingResource.GetCreditCardDetails(objValue);
        }

        public IList<CreditCostMapping> GetCreditCostMapping()
        {
            BillingResource objBillingResource = new BillingResource();
            return objBillingResource.GetCreditCostMapping();
        }



        public void GetCreditPointCount(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            objBillingResource.GetCreditPointCount(objValue);
        }
        public void DeleteCreditCardDetails(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            objBillingResource.Delete(objValue);
        }

        public void UpdateRecord(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            objBillingResource.UpdateRecord(objValue);
        }

        public object InsertCCDetails(object[] objValue)
        {
            StringBuilder sb = new StringBuilder();
            UserRegistration objUserReg = new UserRegistration();
            BillingResource objBillingResource = new BillingResource();
            BillingHistory objBilling = new BillingHistory();
            EmailManager objEmailManager = new EmailManager();
            TributePackage objpackage = new TributePackage();
            // return objBillingResource.InsertRecord(objValue);
            UserCreditcardDetails objCCdetail = new UserCreditcardDetails();
            SessionValue objUser = new SessionValue();
            Tributes objTribute = new Tributes();
            String[] SponsorNameandMsgForEmail;

            object identity = new object();
            using (TransactionScope trans = new TransactionScope())
            {
                identity = objBillingResource.InsertRecord(objValue);
                //Transaction Commited
                trans.Complete();
            }
            objUserReg = (UserRegistration)objValue[0];
            if (objValue.Length > 1)
            {
                objUser = (SessionValue)objValue[1];
                objTribute = (Tributes)objValue[2];
                objpackage = (TributePackage)objValue[3];
                if (objValue[4] != null)
                    SponsorNameandMsgForEmail = (String[])objValue[4];
                else
                    SponsorNameandMsgForEmail = null;

                // Send the email to all the adminstrator 
                if ((identity != null)
                    && (int.Parse(identity.ToString()) != 0))
                {

                    if (objpackage.IsSponserHide)
                    {

                        if (SponsorNameandMsgForEmail[0] != string.Empty)
                        {
                            objEmailManager.SendSponsorMailsWithMessage(SponsorNameandMsgForEmail[0], SponsorNameandMsgForEmail[1], objTribute.TributeId, objTribute.TypeDescription, objTribute.TributeName, objTribute.TributeUrl, objpackage.EndDate != null ? DateTime.Parse(objpackage.EndDate.ToString()).ToString("MMMM dd, yyyy") : "Never", objpackage.PackageId);
                        }
                        //else if (objpackage.IsSponsor)
                        //{
                        //    objEmailManager.SendSponsorMails(objUser.UserType == 1 ? objUser.UserName : objUser.FirstName, objUser.UserEmail, objTribute.TributeId, objTribute.TypeDescription, objTribute.TributeName, objTribute.TributeUrl, objpackage.EndDate != null ? DateTime.Parse(objpackage.EndDate.ToString()).ToString("MMMM dd, yyyy") : "Never", objpackage.PackageId);
                        //}
                        else
                        {
                            if (objpackage.IsSponsor)
                            {
                                objEmailManager.SendSponsorMails(objUserReg.UserCreditcardDetails.CardholdersName, "Your " + WebConfig.ApplicationWord + "<" + WebConfig.NoreplyEmail + ">", objTribute.TributeId, objTribute.TypeDescription, objTribute.TributeName, objTribute.TributeUrl, objpackage.EndDate != null ? DateTime.Parse(objpackage.EndDate.ToString()).ToString("MMMM dd, yyyy") : "Never", objpackage.PackageId);
                            }
                            else
                            {
                                objEmailManager.SendSponsorMails("An anonymous person has", "Your " + WebConfig.ApplicationWord + "<" + WebConfig.NoreplyEmail + ">", objTribute.TributeId, objTribute.TypeDescription, objTribute.TributeName, objTribute.TributeUrl, objpackage.EndDate != null ? DateTime.Parse(objpackage.EndDate.ToString()).ToString("MMMM dd, yyyy") : "Never", objpackage.PackageId);
                            }
                        }
                        //objEmailManager.SendSponsorMails(objUser.UserName, objUser.UserEmail, objTribute.TributeId, objTribute.TypeDescription, objTribute.TributeName, objTribute.TributeUrl, objUserReg.UserCreditcardDetails.ExpirationDate.ToString("MMMM dd, yyyy"), objpackage.PackageId);

                    }


                }
            }
            return identity;
        }

        public object InsertCreditPointCCDetails(object[] objValue)
        {
            StringBuilder sb = new StringBuilder();
            UserRegistration objUserReg = new UserRegistration();
            BillingResource objBillingResource = new BillingResource();
            BillingHistory objBilling = new BillingHistory();
            EmailManager objEmailManager = new EmailManager();
            TributePackage objpackage = new TributePackage();
            // return objBillingResource.InsertRecord(objValue);
            UserCreditcardDetails objCCdetail = new UserCreditcardDetails();
            SessionValue objUser = new SessionValue();
            Tributes objTribute = new Tributes();

            object identity = new object();
            using (TransactionScope trans = new TransactionScope())
            {
                identity = objBillingResource.InsertRecord(objValue);
                //Transaction Commited
                trans.Complete();
            }
            return identity;
        }
        /// <summary>
        /// Method to send email to the list of users
        /// </summary>
        /// <param name="TribuetId">Tribute ID to get the list of admin</param>
        /// <param name="strSubject">Subject of the mail</param>
        public void SendEmail(int TribuetId, string strSubject, string strBody, string FromUserEmail)
        {
            StoryResource objStoryRes = new StoryResource();
            UserInfo objUser = new UserInfo();
            objUser = objStoryRes.GetTributeAdministrators(TribuetId, "");

            EmailMessages objEmail = EmailMessages.Instance;

            bool val = objEmail.SendMessages(FromUserEmail, objUser.UserEmail, strSubject, strBody, EmailMessages.TextFormat.Html.ToString());
        }


        public object InsertCurrentCreditPoints(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            object objBillingReturn = new object();
            using (TransactionScope trans = new TransactionScope())
            {
                objBillingReturn = objBillingResource.InsertCurrentCreditPoints(objValue);
                //Transaction Commited
                trans.Complete();
            }
            return objBillingReturn;
        }

        public object InsertPackageDetails(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();

            object objBillingReturn = new object();
            using (TransactionScope trans = new TransactionScope())
            {
                objBillingReturn = objBillingResource.InsertPackageDetails(objValue);
                //Transaction Commited
                trans.Complete();
            }
            return objBillingReturn;
        }
        public void TriputePackageInfo(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            objBillingResource.GetTriputePackageInfo(objValue);

        }


        public IList<ParameterTypesCodes> PaymentModes()
        {
            ParameterResource objParameterResource = new ParameterResource();
            return objParameterResource.PaymentModes();
        }
        public void UpdateAutoRenew(object[] Params)
        {
            BillingResource objBillingResource = new BillingResource();
            objBillingResource.UpdateAutoRenew(Params);
        }

        /// <summary>
        /// Method to get the payment receipt based on the tribute package id
        /// </summary>
        /// <param name="objValue">TributePackageId</param>
        /// <returns></returns>
        public IList<PaymentReceipt> GetPaymentReceipt(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            return objBillingResource.GetPaymentReceipt(objValue);
        }

        /// <summary>
        /// Method to get the transaction details based on  the package id
        /// </summary>
        /// <param name="objValue">Package id</param>
        /// <returns>Transaction details.</returns>
        public PaymentReceipt GetTransactionDetails(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            PaymentReceipt objTransactionDetails = new PaymentReceipt();
            objTransactionDetails = objBillingResource.GetTransactionDetails(objValue);
            SendTransactionEmail(objTransactionDetails);
            return objTransactionDetails;
        }

        public PaymentReceipt GetTransactionDetailsForEmail(int packId,int transactionId, object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            PaymentReceipt objTransactionDetails = new PaymentReceipt();
            objTransactionDetails = objBillingResource.GetTransactionDetails(objValue);
            SendCreationTransactionEmail(packId,transactionId, objTransactionDetails);
            return objTransactionDetails;

        }

        public PaymentReceipt GetTransactionDetailsForEmail(int packId, string transactionId, object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            PaymentReceipt objTransactionDetails = new PaymentReceipt();
            objTransactionDetails = objBillingResource.GetTransactionDetails(objValue);
            SendCreationTransactionEmail(packId, transactionId, objTransactionDetails);
            return objTransactionDetails;

        }

        private void SendTransactionEmail(int p, PaymentReceipt paymentReceipt)
        {
            throw new NotImplementedException();
        }

        /// Method to get the transaction details based on  the package id for Video tribute upgrade
        public PaymentReceipt GetVideoTrUpgradeTransactionDetails(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            PaymentReceipt objTransactionDetails = new PaymentReceipt();
            objTransactionDetails = objBillingResource.GetVideoTrUpgradeTransactionDetails(objValue);
            SendTransactionEmail(objTransactionDetails);
            return objTransactionDetails;
        }

        // Send Email on Credit purchase
        public PaymentReceipt GetCreditPtTransactionDetails(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            PaymentReceipt objTransactionDetails = new PaymentReceipt();
            objTransactionDetails = objBillingResource.GetCreditPtTransactionDetails(objValue);
            SendCreditPurchaseTransactionEmail(objTransactionDetails);
            //string subject = "";
            //string body = "";
            //SendEmail(objTransactionDetails.TributeId, "subject", "body", "Your Tribute <" + WebConfig.NoreplyEmail + ">");
            return objTransactionDetails;
        }

        public PaymentReceipt GetVideoTributeTransactionDetails(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            PaymentReceipt objTransactionDetails = new PaymentReceipt();
            //if (HttpContext.Current.Session["ViaCreditCard"].Equals(false))
            //{
            objTransactionDetails = objBillingResource.GetVideoTributeTransactionDetails(objValue);
            //}
            //else 
            //{
            //objTransactionDetails = objBillingResource.GetTransactionDetails(objValue);


            SendVideoTributeCreationTransactionEmail(objTransactionDetails);
            //string subject = "";
            //string body = "";
            //SendEmail(objTransactionDetails.TributeId, "subject", "body", "Your Tribute <" + WebConfig.NoreplyEmail + ">");
            return objTransactionDetails;
        }

        private void SendTransactionEmail(PaymentReceipt transactionDetails)
        {
            StateManager stateManager = StateManager.Instance;
            string SentFrom = (string)stateManager.Get("SentFrom", StateManager.State.Session);
            string subject = "Order Receipt #" + transactionDetails.TransactionId.ToString();
            StringBuilder sbEmailBody = new StringBuilder();
            sbEmailBody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'>");
            sbEmailBody.Append("<p>Your " + WebConfig.ApplicationWord + " thanks you for your order. Please retain this receipt for your records.</p>");
            sbEmailBody.Append("<br/>");
            sbEmailBody.Append("<b>Order Information:</b><br/>");
            sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">" + WebConfig.ApplicationWordForInternalUse + " Name:</td><td>" + transactionDetails.Tributename.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">" + WebConfig.ApplicationWordForInternalUse + " Type:</td><td>" + transactionDetails.TypeDescription.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Package Type:</td><td>" + transactionDetails.Packagename.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Expiry Date:</td><td>" + transactionDetails.Enddate.Trim() + "</td></tr></table>");
            sbEmailBody.Append("<br/>");

            if (!(transactionDetails.CreditCardType.Trim() == null || transactionDetails.CreditCardType.Trim() == ""))
            {
                sbEmailBody.Append("<b>Payment Information:</b><br/>");
                sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Billing Date:</td><td>" + transactionDetails.StartDate.ToString("MMMM dd, yyyy").Trim() + "</td></tr>");

                sbEmailBody.Append("<tr><td width=\"200\">Payment Type:</td><td>" + transactionDetails.CreditCardType.Trim() + "</td></tr>");
            }
            // If the Video tribute Upgrade has to be executed for Normal tribute the Amount Paid would be integer
            //if (HttpContext.Current.Session["SentFrom"] == "VideoTributeSpons")
            if (transactionDetails.AmountPaidDouble != 0 || transactionDetails.AmountPaid != 0)
            {
                if (SentFrom == "VideoTributeSpons")
                {
                    sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>" + transactionDetails.AmountPaidDouble.ToString().Trim() + "</td></tr>");
                }
                // To Differentiate between normal Tribute Upgrade and Memorial video(Of Video)Tribute Upgradation
                else if (Math.Truncate(transactionDetails.AmountPaidDouble) == transactionDetails.AmountPaidDouble)
                {
                    sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>" + Math.Floor(transactionDetails.AmountPaidDouble).ToString().Trim() + "</td></tr>");
                }
                else
                {
                    sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>" + transactionDetails.AmountPaidDouble.ToString().Trim() + "</td></tr>");
                }
            }

            if (!(transactionDetails.CreditCardNo.Trim() == null || transactionDetails.CreditCardNo.Trim() == ""))
            {
                string strCredit = string.Empty;
                string ccnumber = TributePortalSecurity.Security.DecryptSymmetric(transactionDetails.CreditCardNo.Trim());
                for (int indexCredit = 0; indexCredit < ccnumber.Length - 4; indexCredit++)
                    strCredit += "X";
                sbEmailBody.Append("<tr><td width=\"200\">Credit Card:</td><td>" + strCredit + ccnumber.Substring(ccnumber.Length - 4) + "</td></tr>");
                sbEmailBody.Append("<tr><td width=\"200\">TransactionId:</td><td>" + transactionDetails.TransactionId.ToString().Trim() + "</td></tr></table>");
                sbEmailBody.Append("<br/>");
            }
            if (!(transactionDetails.Address.Trim() == null || transactionDetails.Address.Trim() == ""))
            {
                sbEmailBody.Append("<b>Billing Information:</b><br/>");
                sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Name:</td><td>" + transactionDetails.CardholdersName.Trim() + "</td></tr>");
                sbEmailBody.Append("<tr><td width=\"200\">Address:</td><td>" + transactionDetails.Address.Trim().Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay) + "</td></tr>");
            }
            if (!(transactionDetails.City.Trim() == null || transactionDetails.City.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">City:</td><td>" + transactionDetails.City.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.State.Trim() == null || transactionDetails.State.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">State/Province:</td><td>" + transactionDetails.State.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.Country.Trim() == null || transactionDetails.Country.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Country:</td><td>" + transactionDetails.Country.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.Zip.Trim() == null || transactionDetails.Zip.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Zip/Postal Code:</td><td>" + transactionDetails.Zip.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.Telephone.Trim() == null || transactionDetails.Telephone.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Telephone:</td><td>" + transactionDetails.Telephone.Trim() + "</td></tr></table>");
            }
            sbEmailBody.Append("<br/>");

            sbEmailBody.Append("<strong>If you have any questions about your order, please email Your " + WebConfig.ApplicationWord + " at <a href= mailto:" + WebConfig.BillingEmail + ">" + WebConfig.BillingEmail + "</a>.  with the transaction details listed above.</strong>");


            sbEmailBody.Append("</font>");


            EmailMessages objEmail = EmailMessages.Instance;
            bool val = objEmail.SendSponsorEmail("Your " + WebConfig.ApplicationWord + " <" + WebConfig.NoreplyEmail + ">", transactionDetails.SponsorEmailAddress, subject, sbEmailBody.ToString(), EmailMessages.TextFormat.Html.ToString());
        }

        // Send mail- bussiness user creating tribute with credits
        private void SendCreationTransactionEmail(int packId,int TransactionId, PaymentReceipt transactionDetails)
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            string SentFrom = (string)stateManager.Get("SentFrom", StateManager.State.Session);
            string subject = "Order Receipt #" + TransactionId.ToString();
            StringBuilder sbEmailBody = new StringBuilder();
            sbEmailBody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'>");
            sbEmailBody.Append("<p>Your " + WebConfig.ApplicationWord + " thanks you for your order. Please retain this receipt for your records.</p>");
            sbEmailBody.Append("<br/>");
            sbEmailBody.Append("<b>Order Information:</b><br/>");
            sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">" + WebConfig.ApplicationWordForInternalUse + " Name:</td><td>" + transactionDetails.Tributename.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">" + WebConfig.ApplicationWordForInternalUse + " Type:</td><td>" + transactionDetails.TypeDescription.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Package Type:</td><td>" + transactionDetails.Packagename.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Expiry Date:</td><td>" + transactionDetails.Enddate.Trim() + "</td></tr></table>");
            sbEmailBody.Append("<br/>");

            sbEmailBody.Append("<b>Payment Information:</b><br/>");
            sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Billing Date:</td><td>" + transactionDetails.StartDate.ToString("MMMM dd, yyyy").Trim() + "</td></tr>");

            sbEmailBody.Append("<tr><td width=\"200\">Payment Type:</td><td>" + "Credit" + "</td></tr>");

            //Amount Billed
            string amtBilled = string.Empty;
            if (packId == 4)
                amtBilled = WebConfig.TributeLifeTimeCreditCost;
            else if (packId == 5)
                amtBilled = WebConfig.TributeYearlyCreditCost;
            else if (packId == 6)
                amtBilled = WebConfig.PhotoLifeTimeCreditCost;
            else if (packId == 7)
                amtBilled = WebConfig.PhotoYearlyCreditCost;

            sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>" + amtBilled + "</td></tr>");


            if (!(transactionDetails.CreditCardType.Trim() == null || transactionDetails.CreditCardType.Trim() == ""))
            {
                //sbEmailBody.Append("<b>Payment Information:</b><br/>");
                sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Billing Date:</td><td>" + transactionDetails.StartDate.ToString("MMMM dd, yyyy").Trim() + "</td></tr>");

                sbEmailBody.Append("<tr><td width=\"200\">Payment Type:</td><td>" + "Credits" + "</td></tr>");
            }
            else
            {
                sbEmailBody.Append("<tr><td width=\"200\">Account Name:</td><td>" + objSessionvalue.UserName.ToString() + "</td></tr></table>");
            }
            // If the Video tribute Upgrade has to be executed for Normal tribute the Amount Paid would be integer
            //if (HttpContext.Current.Session["SentFrom"] == "VideoTributeSpons")
            if (transactionDetails.AmountPaidDouble != 0 || transactionDetails.AmountPaid != 0)
            {
                if (SentFrom == "VideoTributeSpons")
                {
                    sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>" + transactionDetails.AmountPaidDouble.ToString().Trim() + "</td></tr>");
                }
                // To Differentiate between normal Tribute Upgrade and Memorial video(Of Video)Tribute Upgradation
                else if (Math.Truncate(transactionDetails.AmountPaidDouble) == transactionDetails.AmountPaidDouble)
                {
                    sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>" + Math.Floor(transactionDetails.AmountPaidDouble).ToString().Trim() + "</td></tr>");
                }
                else
                {
                    sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>" + transactionDetails.AmountPaid.ToString().Trim() + "</td></tr>");
                }
            }

            if (!(transactionDetails.CreditCardNo.Trim() == null || transactionDetails.CreditCardNo.Trim() == ""))
            {
                string strCredit = string.Empty;
                string ccnumber = TributePortalSecurity.Security.DecryptSymmetric(transactionDetails.CreditCardNo.Trim());
                for (int indexCredit = 0; indexCredit < ccnumber.Length - 4; indexCredit++)
                    strCredit += "X";
                sbEmailBody.Append("<tr><td width=\"200\">Credit Card:</td><td>" + strCredit + ccnumber.Substring(ccnumber.Length - 4) + "</td></tr>");
                sbEmailBody.Append("<tr><td width=\"200\">TransactionId:</td><td>" + transactionDetails.TransactionId.ToString().Trim() + "</td></tr></table>");
                sbEmailBody.Append("<br/>");
            }
            if (!(transactionDetails.Address.Trim() == null || transactionDetails.Address.Trim() == ""))
            {
                sbEmailBody.Append("<b>Billing Information:</b><br/>");
                sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Name:</td><td>" + transactionDetails.CardholdersName.Trim() + "</td></tr>");
                sbEmailBody.Append("<tr><td width=\"200\">Address:</td><td>" + transactionDetails.Address.Trim().Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay) + "</td></tr>");
            }
            if (!(transactionDetails.City.Trim() == null || transactionDetails.City.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">City:</td><td>" + transactionDetails.City.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.State.Trim() == null || transactionDetails.State.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">State/Province:</td><td>" + transactionDetails.State.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.Country.Trim() == null || transactionDetails.Country.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Country:</td><td>" + transactionDetails.Country.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.Zip.Trim() == null || transactionDetails.Zip.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Zip/Postal Code:</td><td>" + transactionDetails.Zip.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.Telephone.Trim() == null || transactionDetails.Telephone.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Telephone:</td><td>" + transactionDetails.Telephone.Trim() + "</td></tr></table>");
            }
            
            sbEmailBody.Append("<br/>");

            sbEmailBody.Append("<strong>If you have any questions about your order, please email Your " + WebConfig.ApplicationWord + " at <a href= mailto:" + WebConfig.BillingEmail + ">" + WebConfig.BillingEmail + "</a>.  with the transaction details listed above.</strong>");


            sbEmailBody.Append("</font>");


            EmailMessages objEmail = EmailMessages.Instance;
            bool val = objEmail.SendSponsorEmail("Your " + WebConfig.ApplicationWord + " <" + WebConfig.NoreplyEmail + ">", objSessionvalue.UserEmail.ToString(), subject, sbEmailBody.ToString(), EmailMessages.TextFormat.Html.ToString());

        }

        // Send mail- user creating tribute with CC payment.
        private void SendCreationTransactionEmail(int packId, string TransactionId, PaymentReceipt transactionDetails)
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            string SentFrom = (string)stateManager.Get("SentFrom", StateManager.State.Session);
            string subject = "Order Receipt #" + TransactionId.ToString();
            StringBuilder sbEmailBody = new StringBuilder();
            sbEmailBody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'>");
            sbEmailBody.Append("<p>Your " + WebConfig.ApplicationWord + " thanks you for your order. Please retain this receipt for your records.</p>");
            sbEmailBody.Append("<br/>");
            sbEmailBody.Append("<b>Order Information:</b><br/>");
            sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">" + WebConfig.ApplicationWordForInternalUse + " Name:</td><td>" + transactionDetails.Tributename.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">" + WebConfig.ApplicationWordForInternalUse + " Type:</td><td>" + transactionDetails.TypeDescription.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Package Type:</td><td>" + transactionDetails.Packagename.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Expiry Date:</td><td>" + transactionDetails.Enddate.Trim() + "</td></tr></table>");
            sbEmailBody.Append("<br/>");

            sbEmailBody.Append("<b>Payment Information:</b><br/>");
            sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Billing Date:</td><td>" + transactionDetails.StartDate.ToString("MMMM dd, yyyy").Trim() + "</td></tr>");

            sbEmailBody.Append("<tr><td width=\"200\">Payment Type:</td><td>" + transactionDetails.CreditCardType.Trim() + "</td></tr>");

            sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>" + transactionDetails.AmountPaidDouble.ToString().Trim() + "</td></tr>");


            if (!(transactionDetails.CreditCardType.Trim() == null || transactionDetails.CreditCardType.Trim() == ""))
            {
                //sbEmailBody.Append("<b>Payment Information:</b><br/>");
                sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Billing Date:</td><td>" + transactionDetails.StartDate.ToString("MMMM dd, yyyy").Trim() + "</td></tr>");

                sbEmailBody.Append("<tr><td width=\"200\">Payment Type:</td><td>" + transactionDetails.CreditCardType.Trim() + "</td></tr>");
            }
            else
            {
                sbEmailBody.Append("<tr><td width=\"200\">Account Name:</td><td>" + objSessionvalue.UserName.ToString() + "</td></tr></table>");
            }
            // If the Video tribute Upgrade has to be executed for Normal tribute the Amount Paid would be integer
            //if (HttpContext.Current.Session["SentFrom"] == "VideoTributeSpons")
            if (transactionDetails.AmountPaidDouble != 0 && transactionDetails.AmountPaid != 0)
            {
                if (SentFrom == "VideoTributeSpons")
                {
                    sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>" + transactionDetails.AmountPaidDouble.ToString().Trim() + "</td></tr>");
                }
                // To Differentiate between normal Tribute Upgrade and Memorial video(Of Video)Tribute Upgradation
                else if (Math.Truncate(transactionDetails.AmountPaidDouble) == transactionDetails.AmountPaidDouble)
                {
                    sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>" + Math.Floor(transactionDetails.AmountPaidDouble).ToString().Trim() + "</td></tr>");
                }
                else
                {
                    sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>" + transactionDetails.AmountPaid.ToString().Trim() + "</td></tr>");
                }
            }

            if (!(transactionDetails.CreditCardNo.Trim() == null || transactionDetails.CreditCardNo.Trim() == ""))
            {
                string strCredit = string.Empty;
                string ccnumber = TributePortalSecurity.Security.DecryptSymmetric(transactionDetails.CreditCardNo.Trim());
                for (int indexCredit = 0; indexCredit < ccnumber.Length - 4; indexCredit++)
                    strCredit += "X";
                sbEmailBody.Append("<tr><td width=\"200\">Credit Card:</td><td>" + strCredit + ccnumber.Substring(ccnumber.Length - 4) + "</td></tr>");
                sbEmailBody.Append("<tr><td width=\"200\">TransactionId:</td><td>" + transactionDetails.TransactionId.ToString().Trim() + "</td></tr></table>");
                sbEmailBody.Append("<br/>");
            }
            if (!(transactionDetails.Address.Trim() == null || transactionDetails.Address.Trim() == ""))
            {
                sbEmailBody.Append("<b>Billing Information:</b><br/>");
                sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Name:</td><td>" + transactionDetails.CardholdersName.Trim() + "</td></tr>");
                sbEmailBody.Append("<tr><td width=\"200\">Address:</td><td>" + transactionDetails.Address.Trim().Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay) + "</td></tr>");
            }
            if (!(transactionDetails.City.Trim() == null || transactionDetails.City.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">City:</td><td>" + transactionDetails.City.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.State.Trim() == null || transactionDetails.State.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">State/Province:</td><td>" + transactionDetails.State.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.Country.Trim() == null || transactionDetails.Country.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Country:</td><td>" + transactionDetails.Country.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.Zip.Trim() == null || transactionDetails.Zip.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Zip/Postal Code:</td><td>" + transactionDetails.Zip.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.Telephone.Trim() == null || transactionDetails.Telephone.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Telephone:</td><td>" + transactionDetails.Telephone.Trim() + "</td></tr></table>");
            }

            sbEmailBody.Append("<br/>");

            sbEmailBody.Append("<strong>If you have any questions about your order, please email Your " + WebConfig.ApplicationWord + " at <a href= mailto:" + WebConfig.BillingEmail + ">" + WebConfig.BillingEmail + "</a>.  with the transaction details listed above.</strong>");


            sbEmailBody.Append("</font>");


            EmailMessages objEmail = EmailMessages.Instance;
            bool val = objEmail.SendSponsorEmail("Your " + WebConfig.ApplicationWord + " <" + WebConfig.NoreplyEmail + ">", objSessionvalue.UserEmail.ToString(), subject, sbEmailBody.ToString(), EmailMessages.TextFormat.Html.ToString());

        }

        // Send Email on Credit purchase
        private void SendCreditPurchaseTransactionEmail(PaymentReceipt transactionDetails)
        {
            string subject = "Order Receipt #" + transactionDetails.TransactionId.ToString();
            StringBuilder sbEmailBody = new StringBuilder();
            sbEmailBody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'>");
            sbEmailBody.Append("<p>Your " + WebConfig.ApplicationWord + " thanks you for your order. Please retain this receipt for your records.</p>");
            sbEmailBody.Append("<br/>");
            sbEmailBody.Append("<b>Order Information:</b><br/>");
            sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Credits Ordered:</td><td>" + HttpContext.Current.Session["CreditPointSelected"].ToString().Trim() + "</td></tr></table>");

            sbEmailBody.Append("<br/>");


            sbEmailBody.Append("<b>Payment Information:</b><br/>");
            sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Billing Date:</td><td>" + transactionDetails.StartDate.ToString("MMMM dd, yyyy").Trim() + "</td></tr>");
            if (!(transactionDetails.CreditCardType.Trim() == null || transactionDetails.CreditCardType.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Payment Type:</td><td>" + transactionDetails.CreditCardType.Trim() + "</td></tr>");
            }
            sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>" + transactionDetails.AmountPaid.ToString().Trim() + "</td></tr>");

            if (!(transactionDetails.CreditCardNo.Trim() == null || transactionDetails.CreditCardNo.Trim() == ""))
            {
                string strCredit = string.Empty;
                string ccnumber = TributePortalSecurity.Security.DecryptSymmetric(transactionDetails.CreditCardNo.Trim());
                for (int indexCredit = 0; indexCredit < ccnumber.Length - 4; indexCredit++)
                    strCredit += "X";
                sbEmailBody.Append("<tr><td width=\"200\">Credit Card:</td><td>" + strCredit + ccnumber.Substring(ccnumber.Length - 4) + "</td></tr>");
                sbEmailBody.Append("<tr><td width=\"200\">TransactionId:</td><td>" + transactionDetails.TransactionId.ToString().Trim() + "</td></tr></table>");
                sbEmailBody.Append("<br/>");
            }

            sbEmailBody.Append("<br/>");


            sbEmailBody.Append("<b>Billing Information:</b><br/>");
            if (!(transactionDetails.Address.Trim() == null || transactionDetails.Address.Trim() == ""))
            {
                sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Name:</td><td>" + transactionDetails.CardholdersName.Trim() + "</td></tr>");
                sbEmailBody.Append("<tr><td width=\"200\">Address:</td><td>" + transactionDetails.Address.Trim().Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay) + "</td></tr>");
            }
            if (!(transactionDetails.City.Trim() == null || transactionDetails.City.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">City:</td><td>" + transactionDetails.City.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.State.Trim() == null || transactionDetails.State.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">State/Province:</td><td>" + transactionDetails.State.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.Country.Trim() == null || transactionDetails.Country.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Country:</td><td>" + transactionDetails.Country.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.Zip.Trim() == null || transactionDetails.Zip.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Zip/Postal Code:</td><td>" + transactionDetails.Zip.Trim() + "</td></tr>");
            }
            if (!(transactionDetails.Telephone.Trim() == null || transactionDetails.Telephone.Trim() == ""))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Telephone:</td><td>" + transactionDetails.Telephone.Trim() + "</td></tr></table>");
            }
            sbEmailBody.Append("<br/>");

            sbEmailBody.Append("<strong>If you have any questions about your order, please email Your " + WebConfig.ApplicationWord + " at <a href= mailto:" + WebConfig.BillingEmail + ">" + WebConfig.BillingEmail + "</a>.  with the transaction details listed above.</strong>");


            sbEmailBody.Append("</font>");


            EmailMessages objEmail = EmailMessages.Instance;
            bool val = objEmail.SendSponsorEmail("Your " + WebConfig.ApplicationWord + " <" + WebConfig.NoreplyEmail + ">", transactionDetails.SponsorEmailAddress, subject, sbEmailBody.ToString(), EmailMessages.TextFormat.Html.ToString());
        }


        private void SendVideoTributeCreationTransactionEmail(PaymentReceipt transactionDetails)
        {
            string subject = "Order Receipt #" + transactionDetails.TransactionId.ToString();
            StringBuilder sbEmailBody = new StringBuilder();
            sbEmailBody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'>");
            sbEmailBody.Append("<p>Your " + WebConfig.ApplicationWord + " thanks you for your order. Please retain this receipt for your records.</p>");
            sbEmailBody.Append("<br/>");
            sbEmailBody.Append("<b>Order Information:</b><br/>");
            sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">" + WebConfig.ApplicationWordForInternalUse + " Name:</td><td>" + transactionDetails.Tributename.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">" + WebConfig.ApplicationWordForInternalUse + " Type:</td><td>" + transactionDetails.TypeDescription.Trim() + " " + WebConfig.ApplicationWordForInternalUse + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Package Type:</td><td>" + transactionDetails.Packagename.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Expiry Date:</td><td>" + transactionDetails.Enddate.Trim() + "</td></tr></table>");
            sbEmailBody.Append("<br/>");

            sbEmailBody.Append("<b>Payment Information:</b><br/>");
            sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Billing Date:</td><td>" + transactionDetails.StartDate.ToString("MMMM dd, yyyy").Trim() + "</td></tr>");
            if (HttpContext.Current.Session["ViaCreditCard"].Equals(true))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Payment Type:</td><td>" + transactionDetails.CreditCardType.Trim() + "</td></tr>");
            }
            else
            {
                sbEmailBody.Append("<tr><td width=\"200\">Payment Type:</td><td>Credits</td></tr>");
            }
            if (HttpContext.Current.Session["ViaCreditCard"].Equals(true))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>" + transactionDetails.AmountPaid.ToString().Trim() + "</td></tr>");

            }
            else
            {
                if (transactionDetails.Packagename.Trim().ToString().Equals("1 Year"))
                {
                    sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>1 Credit</td></tr>");
                }
                else
                {
                    sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>2 Credits</td></tr>");

                }
            }
            if (HttpContext.Current.Session["ViaCreditCard"].Equals(false))
            {
                sbEmailBody.Append("<tr><td width=\"200\">Account Name:</td><td>" + transactionDetails.CardholdersName.Trim() + "</td></tr></table>");
            }
            if (HttpContext.Current.Session["ViaCreditCard"].Equals(true))
            {
                if (!(transactionDetails.CreditCardNo.Trim() == null || transactionDetails.CreditCardNo.Trim() == ""))
                {
                    string strCredit = string.Empty;
                    string ccnumber = TributePortalSecurity.Security.DecryptSymmetric(transactionDetails.CreditCardNo.Trim());
                    for (int indexCredit = 0; indexCredit < ccnumber.Length - 4; indexCredit++)
                        strCredit += "X";
                    sbEmailBody.Append("<tr><td width=\"200\">Credit Card:</td><td>" + strCredit + ccnumber.Substring(ccnumber.Length - 4) + "</td></tr>");
                    sbEmailBody.Append("<tr><td width=\"200\">TransactionId:</td><td>" + transactionDetails.TransactionId.ToString().Trim() + "</td></tr></table>");
                    sbEmailBody.Append("<br/>");
                }
                sbEmailBody.Append("<b>Billing Information:</b><br/>");
                if (!(transactionDetails.Address.Trim() == null || transactionDetails.Address.Trim() == ""))
                {
                    sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Name:</td><td>" + transactionDetails.CardholdersName.Trim() + "</td></tr>");
                    sbEmailBody.Append("<tr><td width=\"200\">Address:</td><td>" + transactionDetails.Address.Trim().Replace(WebConfig.AddressSeparator, WebConfig.AddressSeparatorDisplay) + "</td></tr>");
                }
                if (!(transactionDetails.City.Trim() == null || transactionDetails.City.Trim() == ""))
                {
                    sbEmailBody.Append("<tr><td width=\"200\">City:</td><td>" + transactionDetails.City.Trim() + "</td></tr>");
                }
                if (!(transactionDetails.State.Trim() == null || transactionDetails.State.Trim() == ""))
                {
                    sbEmailBody.Append("<tr><td width=\"200\">State/Province:</td><td>" + transactionDetails.State.Trim() + "</td></tr>");
                }
                if (!(transactionDetails.Country.Trim() == null || transactionDetails.Country.Trim() == ""))
                {
                    sbEmailBody.Append("<tr><td width=\"200\">Country:</td><td>" + transactionDetails.Country.Trim() + "</td></tr>");
                }
                if (!(transactionDetails.Zip.Trim() == null || transactionDetails.Zip.Trim() == ""))
                {
                    sbEmailBody.Append("<tr><td width=\"200\">Zip/Postal Code:</td><td>" + transactionDetails.Zip.Trim() + "</td></tr>");
                }
                if (!(transactionDetails.Telephone.Trim() == null || transactionDetails.Telephone.Trim() == ""))
                {
                    sbEmailBody.Append("<tr><td width=\"200\">Telephone:</td><td>" + transactionDetails.Telephone.Trim() + "</td></tr></table>");
                }
                sbEmailBody.Append("<br/>");


            }
            sbEmailBody.Append("<br/>");

            sbEmailBody.Append("<strong>If you have any questions about your order, please email Your " + WebConfig.ApplicationWord + " at <a href= mailto:" + WebConfig.BillingEmail + ">" + WebConfig.BillingEmail + "</a>.  with the transaction details listed above.</strong>");


            sbEmailBody.Append("</font>");


            EmailMessages objEmail = EmailMessages.Instance;
            bool val = objEmail.SendSponsorEmail("Your " + WebConfig.ApplicationWord + " <" + WebConfig.NoreplyEmail + ">", transactionDetails.SponsorEmailAddress, subject, sbEmailBody.ToString(), EmailMessages.TextFormat.Html.ToString());

        }


        public void UpdateCreditPointOfVideoTributeOwner(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            objBillingResource.UpdateCreditPointOfVideoTributeOwner(objValue);
        }

        public int GetLinkedVideoTributeId(object[] objValue)
        {
            BillingResource objBillingResource = new BillingResource();
            return objBillingResource.GetLinkedVideoTributeId(objValue);

        }

        public object SendSponsorEmailOnFreeUpgrade(object[] objValue)
        {
            StringBuilder sb = new StringBuilder();
            UserRegistration objUserReg = new UserRegistration();
            BillingResource objBillingResource = new BillingResource();
            BillingHistory objBilling = new BillingHistory();
            EmailManager objEmailManager = new EmailManager();
            TributePackage objpackage = new TributePackage();
            // return objBillingResource.InsertRecord(objValue);
            UserCreditcardDetails objCCdetail = new UserCreditcardDetails();
            SessionValue objUser = new SessionValue();
            Tributes objTribute = new Tributes();
            String[] SponsorNameandMsgForEmail;

            object identity = new object();
            //using (TransactionScope trans = new TransactionScope())
            //{
            //    identity = objBillingResource.InsertRecord(objValue);
            //    //Transaction Commited
            //    trans.Complete();
            //}
            objUserReg = (UserRegistration)objValue[0];
            if (objValue.Length > 1)
            {
                objUser = (SessionValue)objValue[1];
                objTribute = (Tributes)objValue[2];
                objpackage = (TributePackage)objValue[3];
                SponsorNameandMsgForEmail = (String[])objValue[4];
                // Send the email to all the adminstrator 

                if (objpackage.IsSponserHide)
                {
                    if (SponsorNameandMsgForEmail[0] != string.Empty)
                    {
                        objEmailManager.SendSponsorMailsWithMessage(SponsorNameandMsgForEmail[0], SponsorNameandMsgForEmail[1], objTribute.TributeId, objTribute.TypeDescription, objTribute.TributeName, objTribute.TributeUrl, objpackage.EndDate != null ? DateTime.Parse(objpackage.EndDate.ToString()).ToString("MMMM dd, yyyy") : "Never", objpackage.PackageId);
                    }
                 /* commented by Mohit  else if (objpackage.IsSponsor)
                    {
                        objEmailManager.SendSponsorMails(objUser.UserType == 1 ? objUser.UserName : objUser.FirstName, objUser.UserEmail, objTribute.TributeId, objTribute.TypeDescription, objTribute.TributeName, objTribute.TributeUrl, objpackage.EndDate != null ? DateTime.Parse(objpackage.EndDate.ToString()).ToString("MMMM dd, yyyy") : "Never", objpackage.PackageId);
                    } */
                    else
                    {
                        objEmailManager.SendSponsorMails("An anonymous person has", "Your " + WebConfig.ApplicationWord + "<" + WebConfig.NoreplyEmail + ">", objTribute.TributeId, objTribute.TypeDescription, objTribute.TributeName, objTribute.TributeUrl, objpackage.EndDate != null ? DateTime.Parse(objpackage.EndDate.ToString()).ToString("MMMM dd, yyyy") : "Never", objpackage.PackageId);
                    }
                    //objEmailManager.SendSponsorMails(objUser.UserName, objUser.UserEmail, objTribute.TributeId, objTribute.TypeDescription, objTribute.TributeName, objTribute.TributeUrl, objUserReg.UserCreditcardDetails.ExpirationDate.ToString("MMMM dd, yyyy"), objpackage.PackageId);

                }
            }

            return identity;
        }


        public int TributePackageId(int tributeId)
        {
            BillingResource objBillingResource = new BillingResource();
            return objBillingResource.GetTributePackageInfo(tributeId);
        }


        public int GetPackIdonPhotoId(int PhotoId)
        {
            BillingResource objBillingResource = new BillingResource();
            return objBillingResource.GetPackIdonPhotoId(PhotoId);
        }
    }
}
