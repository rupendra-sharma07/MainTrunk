///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Utilities.EmailMessages.cs
///Author          : 
///Creation Date   : 
///Description     : This file is used to send the mails from within the application
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
//using System.Web.Mail;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;

namespace TributesPortal.Utilities
{
    public class EmailMessages
    {

        public enum TextFormat
        {
            Text, Html
        }

        private EmailMessages()
        {

        }

        private static EmailMessages obj = null;

        public static EmailMessages Instance
        {
            get
            {
                if (obj == null)
                    obj = new EmailMessages();
                return obj;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFrom">Current User email addrerss</param>
        /// <param name="strTo">Recepsinest User email addrerss</param>
        /// <param name="strSubject">Subject of mail</param>
        /// <param name="strBody">Body of mail</param>        
        /// <param name="strFormat">Mail Format</param>
        /// <returns></returns>
        public bool SendMessages(string strFrom, string strTo, string strSubject, string strBody, string strFormat)
        {

            try
            {

                // Create a new blank MailMessage
                MailMessage msg = new System.Net.Mail.MailMessage();
                if (strFormat == "Text")
                    msg.IsBodyHtml = false;
                else
                    msg.IsBodyHtml = true;
                msg.To.Add(strTo);
                msg.From = new MailAddress(strFrom);
                msg.Subject = strSubject;
                msg.Body = strBody;

                //Add the Creddentials
                SmtpClient client = new SmtpClient();
                client.Host = WebConfig.MailServer;
                //client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;

                //Added by LHK; for SMTP changes
                bool allowNewCredentials = false;
                if (bool.TryParse(WebConfig.UseSMTPAdvanced, out allowNewCredentials))
                {
                    int port = 0;
                    string username = WebConfig.SmtpUsername;
                    string password = WebConfig.SmtpPassword;
                    string smtpServer = WebConfig.SMTPServer;
                    string addressFrom;
                    if (strFrom.ToLower().Contains("@yourtribute.") || strFrom.ToLower().Contains("@yourmoments."))
                      addressFrom = strFrom;
                    else
                        addressFrom = WebConfig.NoreplyEmail;

                    MailMessage objMessage = new MailMessage(addressFrom, strTo);
                    objMessage.IsBodyHtml = true;
                    //add reply to tag for mails fired other than YT /YM server emails
                    if (!(strFrom.ToLower().Contains("@yourtribute.") || strFrom.ToLower().Contains("@yourmoments.")))
                    {
                        MailAddress objSender = new MailAddress(strFrom);
                        objMessage.ReplyTo = objSender;
                    }
                    objMessage.Subject = strSubject;
                    objMessage.Body = strBody;
                    if (strFormat == "Text")
                        objMessage.IsBodyHtml = false;
                    else
                        objMessage.IsBodyHtml = true;

                    int.TryParse(WebConfig.SmtpPort.ToString(), out port);
                    SmtpClient objClient = new SmtpClient(smtpServer, port)
                    {Credentials = new NetworkCredential(username, password),EnableSsl = true};


                    objClient.Send(objMessage);
                    return true;
                    //LHk: till here
                }
                else
                {
                   

                    client.Send(msg);
                    return true;
                }

            }
            catch (Exception ex)
            {
                //string filepath = WebConfig.ErrorLogFilePath + "Log" + DateTime.Now.ToFileTimeUtc() + ".txt";
                ////string filepath = "log.txt";
                //if (!File.Exists(filepath))
                //    File.Create(filepath);

                //using (StreamWriter swriter = new StreamWriter(filepath))
                //{
                //    swriter.WriteLine(DateTime.Now.ToString() + ex.Message.ToString());
                //}

                // throw ex;
                string msg = ex.Message;
                return false;
            }


        }

        /// <summary>
        /// to send the order receipt mails to the user and client account in bcc
        /// </summary>
        /// <param name="strFrom"></param>
        /// <param name="strTo"></param>
        /// <param name="strSubject"></param>
        /// <param name="strBody"></param>
        /// <param name="strFormat"></param>
        /// <returns></returns>
        public bool SendSponsorEmail(string strFrom, string strTo, string strSubject, string strBody, string strFormat)
        {

            try
            {

                // Create a new blank MailMessage
                MailMessage msg = new System.Net.Mail.MailMessage();
                if (strFormat == "Text")
                    msg.IsBodyHtml = false;
                else
                    msg.IsBodyHtml = true;

                msg.To.Add(strTo);
                msg.From = new MailAddress(strFrom);
                msg.Bcc.Add(new MailAddress(WebConfig.SponsorEmail));
                msg.Subject = strSubject;
                msg.Body = strBody;

                //Add the Creddentials
                SmtpClient client = new SmtpClient();
                client.Host = WebConfig.MailServer;

               //Added by LHK; for SMTP changes
                bool allowNewCredentials = false;
                if (bool.TryParse(WebConfig.UseSMTPAdvanced, out allowNewCredentials))
                {
                    int port = 0;
                    string username = WebConfig.SmtpUsername;
                    string password = WebConfig.SmtpPassword;
                    string smtpServer = WebConfig.SMTPServer;
                    string smtpSponsor = WebConfig.SponsorEmail;

                    MailAddress strEmailBcc = new MailAddress(WebConfig.SponsorEmail.ToString(), WebConfig.ApplicationType.ToString() + "Bcc Sponsor");
                    MailMessage objMessage = new MailMessage(strFrom, strTo);
                    objMessage.Subject = strSubject;
                    if (strFormat == "Text")
                        objMessage.IsBodyHtml = false;
                    else
                        objMessage.IsBodyHtml = true;
                    objMessage.Body = strBody;
                    objMessage.Bcc.Add(strEmailBcc);
                    int.TryParse(WebConfig.SmtpPort.ToString(), out port);
                    SmtpClient objClient = new SmtpClient(smtpServer, port) { Credentials = new NetworkCredential(username, password), EnableSsl = true };
                    objClient.Send(objMessage);

                    //to send the Bcc mails to sponsor.
                    //MailMessage objBccMessage = new MailMessage(strFrom, WebConfig.SponsorEmail.ToString());
                    
                    //objBccMessage.Subject = strSubject;
                    //if (strFormat == "Text")
                    //    objBccMessage.IsBodyHtml = false;
                    //else
                    //    objBccMessage.IsBodyHtml = true;
                    //objBccMessage.Body = strBody;
                    //objClient.Send(objBccMessage);

                    return true;
                    //LHk: till here
                }
                else
                {

                    client.Send(msg);
                    return true;
                }

            }
            catch (Exception ex)
            {
                // throw ex;
                string msg = ex.Message;
                return false;
            }


        }


    }
}
