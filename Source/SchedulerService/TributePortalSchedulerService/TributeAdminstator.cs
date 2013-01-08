///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.Services.TributePortalSchedulerService.TributeAdminstator.cs
///Author          : 
///Creation Date   : 
///Description     : This file is used to perform the methods under the tribute portal scheduler service that
///                  runs everyday at a fixed time to send mails to users whose tribute are 
///                  about to expire or expired.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using TributePortal.SchedulerService.DataAccess;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;//vchnage added
using Sathish.ServiceScheduler;


//using TributePortalSchedulerService.com.optimalpayments.webservices;
using TributePortalSchedulerService.com.optimalpayments.test.webservices1;

namespace TributePortalSchedulerService
{
    public partial class TributeAdminstator : ServiceBase
    {
        public TributeAdminstator()
        {
            InitializeComponent();

        }

        /// <summary>
        /// This Method used for Tribute expiry
        /// </summary>
        /// <param name="intNumberOfDays"></param>
        public void NotifyTributeExpiryToUsersFree(int intNumberOfDays)
        {
            DataSet dsMailerList = new DataSet();
            ResourceAccess objResouce = new ResourceAccess();

            object[] objParam = { intNumberOfDays, 1 };
            dsMailerList = objResouce.GetDataSet("usp_GetTributeOwnerMailerList", objParam);

            if (dsMailerList.Tables[0].Rows.Count > 0)
            {
                //EventLog.WriteEntry(dsMailerList.Tables[0].Rows[0][0].ToString() + "-Record Found");

                EmailMessages objEmail = EmailMessages.Instance;
                string _EmailBody = string.Empty;
                int packageid = 0;//vchange line added
                for (int i = 0; i <= dsMailerList.Tables[0].Rows.Count - 1; i++)
                {
                    _EmailBody=ConfigurationSettings.AppSettings["ExpiryReminder"];
                    // vchange block added srart
                    packageid = Convert.ToInt32(dsMailerList.Tables[0].Rows[i]["packageid"]);
                    if (packageid == 3)
                    {
                        _EmailBody = _EmailBody.Replace("#FreeTrialPackage", "The free trial for");
                    }
                    else
                    {
                        _EmailBody = _EmailBody.Replace("#FreeTrialPackage", "The");
                    }
                    // vchange block added end
                    _EmailBody = _EmailBody.Replace("#FirstName", dsMailerList.Tables[0].Rows[i]["FirstName"].ToString());
                    _EmailBody = _EmailBody.Replace("#LastName", dsMailerList.Tables[0].Rows[i]["LastName"].ToString());
                    _EmailBody = _EmailBody.Replace("#TributeName", dsMailerList.Tables[0].Rows[i]["TributeName"].ToString());
                    _EmailBody = _EmailBody.Replace("#TributeUrl", dsMailerList.Tables[0].Rows[i]["TributeUrl"].ToString());
                    _EmailBody = _EmailBody.Replace("#ExpireDate",DateTime.Parse(dsMailerList.Tables[0].Rows[i]["EndDate"].ToString()).ToString("MMMM dd, yyyy"));
                    _EmailBody = _EmailBody.Replace("#TributeType", dsMailerList.Tables[0].Rows[i]["TributeTypeName"].ToString().ToLower().Replace("new baby", "newbaby"));
                    objEmail.SendMessages("Your Tribute<noreply@yourtribute.com>", dsMailerList.Tables[0].Rows[i]["Email"].ToString(), "The " + dsMailerList.Tables[0].Rows[i]["TributeName"].ToString().ToLower() + " Tribute expires soon...", _EmailBody, EmailMessages.TextFormat.Html.ToString());
                }
                objEmail = null;
            }
            EventLog.WriteEntry("My simple service Ended.");
        }

        /// <summary>
        /// This Method used for Tribute expiry
        /// </summary>
        /// <param name="intNumberOfDays"></param>
        public void NotifyTributeExpiryToUsersOneYear(int intNumberOfDays)
        {
            DataSet dsMailerList = new DataSet();
            ResourceAccess objResouce = new ResourceAccess();

            object[] objParam = { intNumberOfDays, 2 };
            dsMailerList = objResouce.GetDataSet("usp_GetTributeOwnerMailerList", objParam);

            if (dsMailerList.Tables[0].Rows.Count > 0)
            {
                //EventLog.WriteEntry(dsMailerList.Tables[0].Rows[0][0].ToString() + "-Record Found");

                EmailMessages objEmail = EmailMessages.Instance;
                string _EmailBody = string.Empty;
                int packageid = 0;//vchange line added
                for (int i = 0; i <= dsMailerList.Tables[0].Rows.Count - 1; i++)
                {
                    _EmailBody = ConfigurationSettings.AppSettings["ExpiryReminder"];
                    // vchange block added srart
                    packageid = Convert.ToInt32(dsMailerList.Tables[0].Rows[i]["packageid"]);
                    if (packageid == 3)
                    {
                        _EmailBody = _EmailBody.Replace("#FreeTrialPackage", "The free trial for");
                    }
                    else
                    {
                        _EmailBody = _EmailBody.Replace("#FreeTrialPackage", "The");
                    }
                    // vchange block added end
                    _EmailBody = _EmailBody.Replace("#FirstName", dsMailerList.Tables[0].Rows[i]["FirstName"].ToString());
                    _EmailBody = _EmailBody.Replace("#LastName", dsMailerList.Tables[0].Rows[i]["LastName"].ToString());
                    _EmailBody = _EmailBody.Replace("#TributeName", dsMailerList.Tables[0].Rows[i]["TributeName"].ToString());
                    _EmailBody = _EmailBody.Replace("#TributeUrl", dsMailerList.Tables[0].Rows[i]["TributeUrl"].ToString());
                    _EmailBody = _EmailBody.Replace("#ExpireDate", DateTime.Parse(dsMailerList.Tables[0].Rows[i]["EndDate"].ToString()).ToString("MMMM dd, yyyy"));
                    _EmailBody = _EmailBody.Replace("#TributeType", dsMailerList.Tables[0].Rows[i]["TributeTypeName"].ToString().ToLower().Replace("new baby", "newbaby"));
                    objEmail.SendMessages("Your Tribute<noreply@yourtribute.com>", dsMailerList.Tables[0].Rows[i]["Email"].ToString(), "The " + dsMailerList.Tables[0].Rows[i]["TributeName"].ToString().ToLower() + " Tribute expires soon...", _EmailBody, EmailMessages.TextFormat.Html.ToString());
                }
                objEmail = null;
            }
            EventLog.WriteEntry("My simple service Ended.");
        }

        /// <summary>
        /// This method used for Expired Tribute
        /// </summary>
        /// <param name="intNumberOfDays"></param>
        public void NotifyTributeExpiredToUser(int intNumberOfDays)
        {
            DataSet dsMailerList = new DataSet();
            ResourceAccess objResouce = new ResourceAccess();

            object[] objParam = { intNumberOfDays, 3 };
            dsMailerList = objResouce.GetDataSet("usp_GetTributeOwnerMailerList", objParam);

            if (dsMailerList.Tables[0].Rows.Count > 0)
            {
                //EventLog.WriteEntry(dsMailerList.Tables[0].Rows[0][0].ToString() + "-Record Found");

                EmailMessages objEmail = EmailMessages.Instance;
                string _EmailBody = string.Empty;
                int packageid = 0;//vchange line added
                for (int i = 0; i <= dsMailerList.Tables[0].Rows.Count - 1; i++)
                {
                    _EmailBody = ConfigurationSettings.AppSettings["ExpiredReminder"];
                    // vchange block added srart
                    if (packageid == 3)
                    {
                        _EmailBody = _EmailBody.Replace("#FreeTrialPackage", "The free trial for");
                    }
                    else
                    {
                        _EmailBody = _EmailBody.Replace("#FreeTrialPackage", "The");
                    }
                    // vchange block added end
                    _EmailBody = _EmailBody.Replace("#FirstName", dsMailerList.Tables[0].Rows[i]["FirstName"].ToString());
                    _EmailBody = _EmailBody.Replace("#LastName", dsMailerList.Tables[0].Rows[i]["LastName"].ToString());
                    _EmailBody = _EmailBody.Replace("#TributeName", dsMailerList.Tables[0].Rows[i]["TributeName"].ToString());
                    _EmailBody = _EmailBody.Replace("#TributeUrl", dsMailerList.Tables[0].Rows[i]["TributeUrl"].ToString());
                    _EmailBody = _EmailBody.Replace("#ExpireDate",DateTime.Parse(dsMailerList.Tables[0].Rows[i]["EndDate"].ToString()).ToString("MMMM dd, yyyy"));
                    _EmailBody = _EmailBody.Replace("#TributeType", dsMailerList.Tables[0].Rows[i]["TributeTypeName"].ToString().ToLower().Replace("new baby", "newbaby"));
                    objEmail.SendMessages("Your Tribute<noreply@yourtribute.com>", dsMailerList.Tables[0].Rows[i]["Email"].ToString(), "The " + dsMailerList.Tables[0].Rows[i]["TributeName"].ToString() + " Tribute Expired", _EmailBody, EmailMessages.TextFormat.Html.ToString());//vchange text updated
                }
                objEmail = null;
            }
            EventLog.WriteEntry("My simple service Ended.");
        }

        /// <summary>
        /// This Tribute used to notify deleted tribute
        /// </summary>
        /// <param name="intNumberOfDays"></param>
        public void NotifyTributeDeleteToUserFree(int intNumberOfDays)
        {
            DataSet dsMailerList = new DataSet();
            ResourceAccess objResouce = new ResourceAccess();

            object[] objParam = { intNumberOfDays, 4 };
            dsMailerList = objResouce.GetDataSet("usp_GetTributeOwnerMailerList", objParam);

            if (dsMailerList.Tables[0].Rows.Count > 0)
            {
                //EventLog.WriteEntry(dsMailerList.Tables[0].Rows[0][0].ToString() + "-Record Found");

                EmailMessages objEmail = EmailMessages.Instance;
                string _EmailBody = string.Empty;
                int packageid = 0;//vchange line added
                for (int i = 0; i <= dsMailerList.Tables[0].Rows.Count - 1; i++)
                {
                    _EmailBody = ConfigurationSettings.AppSettings["DeleteNotification"];
                    // vchange block added srart
                    if (packageid == 3)
                    {
                        _EmailBody = _EmailBody.Replace("#FreeTrialPackage", "The free trial for");
                    }
                    else
                    {
                        _EmailBody = _EmailBody.Replace("#FreeTrialPackage", "The");
                    }
                    // vchange block added end
                    _EmailBody = _EmailBody.Replace("#FirstName", dsMailerList.Tables[0].Rows[i]["FirstName"].ToString());
                    _EmailBody = _EmailBody.Replace("#LastName", dsMailerList.Tables[0].Rows[i]["LastName"].ToString());
                    _EmailBody = _EmailBody.Replace("#TributeName", dsMailerList.Tables[0].Rows[i]["TributeName"].ToString());
                    _EmailBody = _EmailBody.Replace("#TributeUrl", dsMailerList.Tables[0].Rows[i]["TributeUrl"].ToString());
                    _EmailBody = _EmailBody.Replace("#ExpireDate",DateTime.Parse(dsMailerList.Tables[0].Rows[i]["EndDate"].ToString()).ToString("MMMM dd, yyyy"));
                    _EmailBody = _EmailBody.Replace("#TributeId", dsMailerList.Tables[0].Rows[i]["TributeId"].ToString());
                    _EmailBody = _EmailBody.Replace("#TributeType", dsMailerList.Tables[0].Rows[i]["TributeTypeName"].ToString().ToLower().Replace("new baby","newbaby"));
                    objEmail.SendMessages("Your Tribute<noreply@yourtribute.com>", dsMailerList.Tables[0].Rows[i]["Email"].ToString(), "The " + dsMailerList.Tables[0].Rows[i]["TributeName"].ToString() + " Tribute will be deleted soon...", _EmailBody, EmailMessages.TextFormat.Html.ToString());
                }
                objEmail = null;
            }
            EventLog.WriteEntry("My simple service Ended.");
        }

        /// <summary>
        /// This Tribute used to notify deleted tribute
        /// </summary>
        /// <param name="intNumberOfDays"></param>
        public void NotifyTributeDeleteToUserOneYear(int intNumberOfDays)
        {
            DataSet dsMailerList = new DataSet();
            ResourceAccess objResouce = new ResourceAccess();

            object[] objParam = { intNumberOfDays, 5 };
            dsMailerList = objResouce.GetDataSet("usp_GetTributeOwnerMailerList", objParam);

            if (dsMailerList.Tables[0].Rows.Count > 0)
            {
                //EventLog.WriteEntry(dsMailerList.Tables[0].Rows[0][0].ToString() + "-Record Found");

                EmailMessages objEmail = EmailMessages.Instance;
                string _EmailBody = string.Empty;
                int packageid = 0;//vchange line added
                for (int i = 0; i <= dsMailerList.Tables[0].Rows.Count - 1; i++)
                {
                    _EmailBody = ConfigurationSettings.AppSettings["DeleteNotification"];
                    // vchange block added srart
                    if (packageid == 3)
                    {
                        _EmailBody = _EmailBody.Replace("#FreeTrialPackage", "The free trial for");
                    }
                    else
                    {
                        _EmailBody = _EmailBody.Replace("#FreeTrialPackage", "The");
                    }
                    // vchange block added end
                    _EmailBody = _EmailBody.Replace("#FirstName", dsMailerList.Tables[0].Rows[i]["FirstName"].ToString());
                    _EmailBody = _EmailBody.Replace("#LastName", dsMailerList.Tables[0].Rows[i]["LastName"].ToString());
                    _EmailBody = _EmailBody.Replace("#TributeName", dsMailerList.Tables[0].Rows[i]["TributeName"].ToString());
                    _EmailBody = _EmailBody.Replace("#TributeUrl", dsMailerList.Tables[0].Rows[i]["TributeUrl"].ToString());
                    _EmailBody = _EmailBody.Replace("#ExpireDate", DateTime.Parse(dsMailerList.Tables[0].Rows[i]["EndDate"].ToString()).ToString("MMMM dd, yyyy"));
                    _EmailBody = _EmailBody.Replace("#TributeId", dsMailerList.Tables[0].Rows[i]["TributeId"].ToString());
                    _EmailBody = _EmailBody.Replace("#TributeType", dsMailerList.Tables[0].Rows[i]["TributeTypeName"].ToString().ToLower().Replace("new baby", "newbaby"));
                    objEmail.SendMessages("Your Tribute<noreply@yourtribute.com>", dsMailerList.Tables[0].Rows[i]["Email"].ToString(), "The " + dsMailerList.Tables[0].Rows[i]["TributeName"].ToString() + " Tribute will be deleted soon...", _EmailBody, EmailMessages.TextFormat.Html.ToString());
                }
                objEmail = null;
            }
            EventLog.WriteEntry("My simple service Ended.");
        }

        /// <summary>
        /// This method used to update status of tribute
        /// </summary>
        /// <param name="intNumberOfDays"></param>
        /// <param name="DeleteTribute"></param>
        public void SetTributeStatus()
        {
            ResourceAccess objResouce = new ResourceAccess();
            objResouce.GetDataSet("usp_ChangeTributeStatus", null);
            objResouce = null;
        }

        /// <summary>
        /// This override method execute on start of windows service
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Tribute Portal Service Started");
            Scheduler sch = new Scheduler("TributeAdminstator");
            sch.SchedulerFired += new EventHandler(ServiceStart);
            sch.ScheduleDaily(ConfigurationSettings.AppSettings["WhenExecute"]);

        }

        /// <summary>
        /// This method is used to select Credit Card type
        /// </summary>
        /// <param name="CardType"></param>
        /// <returns></returns>
        private CardTypeV1 SelectCreditCardType(string CardType)
        {
            CardTypeV1 CcType = CardTypeV1.VI;

            if (CardType == "Visa")
            {
                CcType = CardTypeV1.VI;
            }
            if (CardType == "MasterCard")
            {
                CcType = CardTypeV1.MC;
            }
            if (CardType == "Discover")
            {
                CcType = CardTypeV1.DC;
            }
            if (CardType == "Amex")
            {
                CcType = CardTypeV1.AM;
            }
            return CcType;
        }

        /// <summary>
        /// This method used for AutoRenewal for One Year
        /// </summary>
        public void AutoRenewalForOneYearPackage()
        {
            DataSet dsMailerList = new DataSet();
            DataSet dsMailerList1 = new DataSet();//vchange line added
            bool _transaction = true;
            PaymentGateWay objPay = new PaymentGateWay();
            ResourceAccess objResouce = new ResourceAccess();
            EmailMessages objEmail = EmailMessages.Instance;
            string _EmailBody=string.Empty;
            string confirmationId = string.Empty;//vchange line added
            string amount = string.Empty;//vchange line added
             object[] objParam = new object[3];
             object[] objParam2 = { null };
             try
             {
            
             dsMailerList = objResouce.GetDataSet("usp_AutoRenewalCustomerList", null);
            
            
                 if (dsMailerList.Tables[0].Rows.Count > 0)
                 {
                     for (int i = 0; i <= dsMailerList.Tables[0].Rows.Count - 1; i++)
                     {
                         //This code will execute on server 
                         /*_transaction = objPay.PayYourBill(dsMailerList.Tables[0].Rows[i]["CreditCardNo"].ToString(), dsMailerList.Tables[0].Rows[i]["CVC"].ToString(),
                             int.Parse(DateTime.Now.Month.ToString()), int.Parse(DateTime.Now.Year.ToString()),
                             "20.00", SelectCreditCardType(dsMailerList.Tables[0].Rows[i]["CreditCardType"].ToString()),
                             dsMailerList.Tables[0].Rows[i]["FirstName"].ToString(), dsMailerList.Tables[0].Rows[i]["Lastname"].ToString(), "TimelessTribute BackOffice",
                         dsMailerList.Tables[0].Rows[i]["City"].ToString(), StateV1.CA, CountryV1.US, dsMailerList.Tables[0].Rows[i]["Zip"].ToString(),
                         dsMailerList.Tables[0].Rows[i]["Telephone"].ToString(), dsMailerList.Tables[0].Rows[i]["Email"].ToString(),
                         Environment.MachineName.ToString());*/
                         //This code will execute on local  
                         /* _transaction = true;*/

                         //vchange added satarted
                         try
                         {
                             int Couponamount = 0;

                             amount = ConfigurationSettings.AppSettings["OneyearAmount"];
                             Couponamount = Convert.ToInt32(amount.Substring(1, amount.Length - 1));

                             _transaction = objPay.PayYourBill(TributePortalSecurity.Security.DecryptSymmetric(dsMailerList.Tables[0].Rows[i]["CreditCardNo"].ToString()), TributePortalSecurity.Security.DecryptSymmetric(dsMailerList.Tables[0].Rows[i]["CVC"].ToString()),
                               Convert.ToInt32(dsMailerList.Tables[0].Rows[i]["ExpiryMonth"]), Convert.ToInt32(dsMailerList.Tables[0].Rows[i]["ExpiryYear"]), 
                               Couponamount.ToString(), SelectCreditCardType(dsMailerList.Tables[0].Rows[i]["CreditCardType"].ToString()),
                               dsMailerList.Tables[0].Rows[i]["FirstName"].ToString(), dsMailerList.Tables[0].Rows[i]["Lastname"].ToString(), dsMailerList.Tables[0].Rows[i]["Address"].ToString().Replace(ConfigurationSettings.AppSettings["AddressSeparator"],ConfigurationSettings.AppSettings["AddressSeparatorDisplay"]),
                           dsMailerList.Tables[0].Rows[i]["City"].ToString(), StateV1.CA, CountryV1.US, dsMailerList.Tables[0].Rows[i]["Zip"].ToString(),
                           dsMailerList.Tables[0].Rows[i]["Telephone"].ToString(), dsMailerList.Tables[0].Rows[i]["Email"].ToString(),
                           Environment.MachineName.ToString(), out confirmationId);

                          //   _transaction = objPay.PayYourBill(dsMailerList.Tables[0].Rows[i]["CreditCardNo"].ToString(), dsMailerList.Tables[0].Rows[i]["CVC"].ToString(),
                          //    int.Parse(DateTime.Now.Month.ToString()), int.Parse(DateTime.Now.Year.ToString()),
                          //    Couponamount.ToString(), SelectCreditCardType(dsMailerList.Tables[0].Rows[i]["CreditCardType"].ToString()),
                          //    dsMailerList.Tables[0].Rows[i]["FirstName"].ToString(), dsMailerList.Tables[0].Rows[i]["Lastname"].ToString(), "TimelessTribute BackOffice",
                          //dsMailerList.Tables[0].Rows[i]["City"].ToString(), StateV1.CA, CountryV1.US, dsMailerList.Tables[0].Rows[i]["Zip"].ToString(),
                          //dsMailerList.Tables[0].Rows[i]["Telephone"].ToString(), dsMailerList.Tables[0].Rows[i]["Email"].ToString(),
                          //Environment.MachineName.ToString(), out confirmationId);
                             //vchange added end
                             if (_transaction)
                             {
                                 EventLog.WriteEntry("Payment Recived");
                                 objParam[0] = dsMailerList.Tables[0].Rows[i]["TributePackageId"].ToString();
                                 objParam[1] = Couponamount.ToString();// vchnage "20";
                                 objParam[2] = confirmationId.Length == 0 ? 0 : int.Parse(confirmationId);

                                 //objResouce.GetDataSet("usp_RenewTributePackage", objParam);
                                 //vchnage code added satrt
                                 dsMailerList1 = objResouce.GetDataSet("usp_RenewTributePackage", objParam);
                                 if (dsMailerList1.Tables[0].Rows.Count > 0)
                                 {
                                         //vchnage code added end
                                         objEmail = EmailMessages.Instance;
                                         _EmailBody = ConfigurationSettings.AppSettings["MailAfterRenewal"];

                                         _EmailBody = _EmailBody.Replace("#FirstName", dsMailerList.Tables[0].Rows[i]["FirstName"].ToString());
                                         _EmailBody = _EmailBody.Replace("#LastName", dsMailerList.Tables[0].Rows[i]["LastName"].ToString());
                                         _EmailBody = _EmailBody.Replace("#TributeName", dsMailerList.Tables[0].Rows[i]["TributeName"].ToString());
                                         _EmailBody = _EmailBody.Replace("#TributeUrl", dsMailerList.Tables[0].Rows[i]["TributeUrl"].ToString());
                                         _EmailBody = _EmailBody.Replace("#RenewalDate", DateTime.Parse(dsMailerList.Tables[0].Rows[i]["EndDate"].ToString()).ToString("MMMM dd, yyyy"));
                                         _EmailBody = _EmailBody.Replace("#TributeType", dsMailerList.Tables[0].Rows[i]["TributeTypeName"].ToString().ToLower());
                                         objEmail.SendMessages("Your Tribute<noreply@yourtribute.com>", dsMailerList.Tables[0].Rows[i]["Email"].ToString(), "The " + dsMailerList.Tables[0].Rows[i]["TributeName"].ToString() + " Tribute renewed.", _EmailBody, EmailMessages.TextFormat.Html.ToString());

                                         ///vchnage code added start
                                         object[] param ={ dsMailerList1.Tables[0].Rows[0][0].ToString() };
                                         GetTransactionDetails(param);
                                      
                                 }
                             }
                             else
                             {
                                 //NotifyRenwealFailed()
                                 EventLog.WriteEntry("Payment not recevied");
                                 objEmail = EmailMessages.Instance;
                                 _EmailBody = ConfigurationSettings.AppSettings["MailRenewalFailed"];
                                 _EmailBody = _EmailBody.Replace("#FirstName", dsMailerList.Tables[0].Rows[i]["FirstName"].ToString());
                                 _EmailBody = _EmailBody.Replace("#LastName", dsMailerList.Tables[0].Rows[i]["LastName"].ToString());
                                 _EmailBody = _EmailBody.Replace("#TributeName", dsMailerList.Tables[0].Rows[i]["TributeName"].ToString());
                                 _EmailBody = _EmailBody.Replace("#TributeUrl", dsMailerList.Tables[0].Rows[i]["TributeUrl"].ToString());
                                 _EmailBody = _EmailBody.Replace("#TributeType", dsMailerList.Tables[0].Rows[i]["TributeTypeName"].ToString().ToLower());
                                 objEmail.SendMessages("Your Tribute<noreply@yourtribute.com>", dsMailerList.Tables[0].Rows[i]["Email"].ToString(), "Auto-Renewal of the " + dsMailerList.Tables[0].Rows[i]["TributeName"].ToString() + " Tribute has failed...", _EmailBody, EmailMessages.TextFormat.Html.ToString());
                             }
                         }
                         catch
                         {
                             // NotifyRenwealFailed()
                             EventLog.WriteEntry("Exception occured,Payment not recevied");
                             objEmail = EmailMessages.Instance;
                             _EmailBody = ConfigurationSettings.AppSettings["MailRenewalFailed"];
                             _EmailBody = _EmailBody.Replace("#FirstName", dsMailerList.Tables[0].Rows[i]["FirstName"].ToString());
                             _EmailBody = _EmailBody.Replace("#LastName", dsMailerList.Tables[0].Rows[i]["LastName"].ToString());
                             _EmailBody = _EmailBody.Replace("#TributeName", dsMailerList.Tables[0].Rows[i]["TributeName"].ToString());
                             _EmailBody = _EmailBody.Replace("#TributeUrl", dsMailerList.Tables[0].Rows[i]["TributeUrl"].ToString());
                             _EmailBody = _EmailBody.Replace("#TributeType", dsMailerList.Tables[0].Rows[i]["TributeTypeName"].ToString().ToLower());
                             objEmail.SendMessages("Your Tribute<noreply@yourtribute.com>", dsMailerList.Tables[0].Rows[i]["Email"].ToString(), "Auto-Renewal of the " + dsMailerList.Tables[0].Rows[i]["TributeName"].ToString() + " Tribute has failed...", _EmailBody, EmailMessages.TextFormat.Html.ToString());
                             ///vchnage code added end
                         }

                         objEmail = null;
                     }
                 }
             }
             catch (Exception ex)
             {
                 EventLog.WriteEntry(ex.Message.ToString());
             }
            
        }

        //vchnage code added end

        /// <summary>
        /// Method to get the transaction details based on  the package id
        /// </summary>
        /// <param name="objValue">Package id</param>
        /// <returns>Transaction details.</returns>
        public PaymentReceipt GetTransactionDetails(object[] objValue)
        {
            //BillingResource objBillingResource = new BillingResource();
            PaymentReceipt objTransactionDetails = new PaymentReceipt();
            objTransactionDetails = GetTransactionDetail(objValue);
            SendTransactionEmail(objTransactionDetails);
            //string subject = "";
            //string body = "";
            //SendEmail(objTransactionDetails.TributeId, "subject", "body", "Your Tribute <" + WebConfig.NoreplyEmail + ">");
            return objTransactionDetails;
        }

        /// <summary>
        /// Method to get the Transaction details for the payment
        /// </summary>
        /// <param name="objValue">Package Id</param>
        /// <returns>Get the details of transaction.</returns>
        public PaymentReceipt GetTransactionDetail(object[] objValue)
        {
            ResourceAccess objResouce = new ResourceAccess();
            PaymentReceipt objPaymentReceipt = new PaymentReceipt();
            //PaymentReceipt objpay = null;
            try
            {
                DataSet ds = objResouce.GetDataSet("usp_GetTransactionDetails", objValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        objPaymentReceipt = (new PaymentReceipt(int.Parse(ds.Tables[0].Rows[i]["TransactionId"].ToString()),
                                                                 ds.Tables[0].Rows[i]["Tributename"].ToString(),
                                                                 ds.Tables[0].Rows[i]["TypeDescription"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Packagename"].ToString(),
                                                                 (ds.Tables[0].Rows[i]["Enddate"].ToString().Length != 0 ? Convert.ToDateTime(ds.Tables[0].Rows[i]["Enddate"]).ToString("MMMM dd, yyyy") : "Never"),
                            //,
                                                                 ds.Tables[0].Rows[i]["CardholdersName"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Address"].ToString(),
                                                                 ds.Tables[0].Rows[i]["City"].ToString(),
                                                                 ds.Tables[0].Rows[i]["State"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Country"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Zip"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Telephone"].ToString(),
                                                                 DateTime.Parse(ds.Tables[0].Rows[i]["StartDate"].ToString()),
                                                                 ds.Tables[0].Rows[i]["CreditCardType"].ToString(),
                                                                 ds.Tables[0].Rows[i]["CreditCardNo"].ToString(),
                                                                 int.Parse(ds.Tables[0].Rows[i]["AmountPaid"].ToString()),
                                                                 ds.Tables[0].Rows[i]["SponsorEmailAddress"].ToString(),
                                                                 Convert.ToBoolean(ds.Tables[0].Rows[i]["IsAutomaticRenew"].ToString())));
                    }
                }
                return objPaymentReceipt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SendTransactionEmail(PaymentReceipt transactionDetails)
        {
            string subject = "Order Receipt #" + transactionDetails.TransactionId.ToString();
            StringBuilder sbEmailBody = new StringBuilder();
            sbEmailBody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'>");
            sbEmailBody.Append("<p>Your Tribute thanks you for your order. Please retain this receipt for your records.</p>");
            sbEmailBody.Append("<br/>");
            sbEmailBody.Append("<b>Order Information:</b><br/>");
            sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Tribute Name:</td><td>" + transactionDetails.Tributename.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Tribute Type:</td><td>" + transactionDetails.TypeDescription.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Package Type:</td><td>" + transactionDetails.Packagename.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Expiry Date:</td><td>" + transactionDetails.Enddate.Trim() + "</td></tr></table>");
            sbEmailBody.Append("<br/>");

            sbEmailBody.Append("<b>Payment Information:</b><br/>");
            sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Billing Date:</td><td>" + transactionDetails.StartDate.ToString("MMMM dd, yyyy").Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Payment Type:</td><td>" + transactionDetails.CreditCardType.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Amount Billed:</td><td>" + transactionDetails.AmountPaid.ToString().Trim() + "</td></tr>");
            string strCredit = string.Empty;
            string ccnumber = TributePortalSecurity.Security.DecryptSymmetric(transactionDetails.CreditCardNo.Trim());
            //comment for server
            //string ccnumber = transactionDetails.CreditCardNo.Trim();

            for (int indexCredit = 0; indexCredit < ccnumber.Length - 4; indexCredit++)
                strCredit += "X";
            sbEmailBody.Append("<tr><td width=\"200\">Credit Card:</td><td>" + strCredit + ccnumber.Substring(ccnumber.Length - 4) + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">TransactionId:</td><td>" + transactionDetails.TransactionId.ToString().Trim() + "</td></tr></table>");
            sbEmailBody.Append("<br/>");

            sbEmailBody.Append("<b>Billing Information:</b><br/>");
            sbEmailBody.Append("<table border='0' width='100%' style=\"font-family:Lucida Sans; font-size:12px\"><tr><td width=\"200\">Name:</td><td>" + transactionDetails.CardholdersName.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Address:</td><td>" + transactionDetails.Address.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">City:</td><td>" + transactionDetails.City.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">State/Province:</td><td>" + transactionDetails.State.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Country:</td><td>" + transactionDetails.Country.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Zip/Postal Code:</td><td>" + transactionDetails.Zip.Trim() + "</td></tr>");
            sbEmailBody.Append("<tr><td width=\"200\">Telephone:</td><td>" + transactionDetails.Telephone.Trim() + "</td></tr></table>");
            sbEmailBody.Append("<br/>");

            sbEmailBody.Append("<strong>If you have any questions about your order, please email Your Tribute at <a href='#'>" + ConfigurationSettings.AppSettings["BillingEmail"] + "</a>, with the transaction details listed above.</strong>");

            sbEmailBody.Append("</font>");


            EmailMessages objEmail = EmailMessages.Instance;
            objEmail.SendSponsorEmail("Your Tribute<noreply@yourtribute.com>", transactionDetails.SponsorEmailAddress, subject, sbEmailBody.ToString(), EmailMessages.TextFormat.Html.ToString());
        }

        //vchnage code added end


        /// <summary>
        /// This Method used to send email to users for credit card exipration 
        /// </summary>
        public void NotifyCreditCardExpirationDate(int intNumberOfDays)
        {
            DataSet dsMailerList = new DataSet();
            ResourceAccess objResouce = new ResourceAccess();

            object[] objParam = { intNumberOfDays };
            dsMailerList = objResouce.GetDataSet("usp_CreditCardExpiredCustomerList", objParam);

            if (dsMailerList.Tables[0].Rows.Count > 0)
            {
                //EventLog.WriteEntry(dsMailerList.Tables[0].Rows[0][0].ToString() + "-Record Found");

                EmailMessages objEmail = EmailMessages.Instance;
                string _EmailBody = string.Empty;
                for (int i = 0; i <= dsMailerList.Tables[0].Rows.Count - 1; i++)
                {
                    _EmailBody = _EmailBody = ConfigurationSettings.AppSettings["CreditCardExpiryReminder"];
                    _EmailBody = _EmailBody.Replace("#FirstName", dsMailerList.Tables[0].Rows[i]["FirstName"].ToString());
                    _EmailBody = _EmailBody.Replace("#LastName", dsMailerList.Tables[0].Rows[i]["LastName"].ToString());
                    _EmailBody = _EmailBody.Replace("#TributeName", dsMailerList.Tables[0].Rows[i]["TributeName"].ToString());
                    _EmailBody = _EmailBody.Replace("#TributeUrl", dsMailerList.Tables[0].Rows[i]["TributeUrl"].ToString());
                    _EmailBody = _EmailBody.Replace("#ExpireDate",DateTime.Parse(dsMailerList.Tables[0].Rows[i]["EndDate"].ToString()).ToString("MMMM dd, yyyy"));
                    _EmailBody = _EmailBody.Replace("#TributeType", dsMailerList.Tables[0].Rows[i]["TributeTypeName"].ToString().ToLower());
                    _EmailBody = _EmailBody.Replace("#CCExpireReminder", Convert.ToString(intNumberOfDays));
                    objEmail.SendMessages("Your Tribute<noreply@yourtribute.com>", dsMailerList.Tables[0].Rows[i]["Email"].ToString(), "The " + dsMailerList.Tables[0].Rows[i]["TributeName"].ToString() + " Your Tribute CC Expiry Notice", _EmailBody, EmailMessages.TextFormat.Html.ToString());
                }
                objEmail = null;
            }
            EventLog.WriteEntry("My simple service Ended.");
        }

        private void ServiceStart(object sender, EventArgs e)
        {
            int intDays = 0;
            int notifyFreeReminderDays = Convert.ToInt32(ConfigurationManager.AppSettings["NotifyFreeReminder"]);
            int notifyYearlyReminderDays = Convert.ToInt32(ConfigurationManager.AppSettings["NotifyYearlyReminder"]);
            int expireTributeReminderDays = Convert.ToInt32(ConfigurationManager.AppSettings["ExpireTributeReminder"]);
            int deleteFreeReminderDays = Convert.ToInt32(ConfigurationManager.AppSettings["DeleteFreeReminder"]);
            int deleteYearlyReminderDays = Convert.ToInt32(ConfigurationManager.AppSettings["DeleteYearlyReminder"]);
            int ccExpireReminderDays = Convert.ToInt32(ConfigurationManager.AppSettings["CCExpireReminder"]);

            //Log the Service execution start time
            EventLog.WriteEntry("Service Executation Start at " + DateTime.Now.ToString());

            EventLog.WriteEntry("NotifyTributeExpiryToUsersFree Executation Started");
            NotifyTributeExpiryToUsersFree(notifyFreeReminderDays);
            EventLog.WriteEntry("NotifyTributeExpiryToUsersFree Executation Ended");

            EventLog.WriteEntry("NotifyTributeExpiryToUsersOneYear Executation Started");
            NotifyTributeExpiryToUsersOneYear(notifyYearlyReminderDays);
            EventLog.WriteEntry("NotifyTributeExpiryToUsersOneYear Executation Ended");

            EventLog.WriteEntry("NotifyTributeExpiredToUser Executation Started");
            NotifyTributeExpiredToUser(expireTributeReminderDays);
            EventLog.WriteEntry("NotifyTributeExpiredToUser Executation Ended");

            EventLog.WriteEntry("NotifyTributeDeleteToUserFree Executation Started");
            NotifyTributeDeleteToUserFree(deleteFreeReminderDays);
            EventLog.WriteEntry("NotifyTributeDeleteToUserFree Executation Ended");

            EventLog.WriteEntry("NotifyTributeDeleteToUserOneYear Executation Started");
            NotifyTributeDeleteToUserOneYear(deleteYearlyReminderDays);
            EventLog.WriteEntry("NotifyTributeDeleteToUserOneYear Executation Ended");

            EventLog.WriteEntry("AutoRenewalForOneYearPackage Executation Started");
            AutoRenewalForOneYearPackage();
            EventLog.WriteEntry("AutoRenewalForOneYearPackage Executation Ended");

            EventLog.WriteEntry("NotifyCreditCardExpirationDate Executation Started");
            NotifyCreditCardExpirationDate(ccExpireReminderDays);
            EventLog.WriteEntry("NotifyCreditCardExpirationDate Executation Ended");

            EventLog.WriteEntry("SetTributeStatus Executation Started");
            SetTributeStatus();
            EventLog.WriteEntry("SetTributeStatus Executation Ended");

            //Log the Service execution end time
            EventLog.WriteEntry("Service Executation Ended at " + DateTime.Now.ToString());
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
