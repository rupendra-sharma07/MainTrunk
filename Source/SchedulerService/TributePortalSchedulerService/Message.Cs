///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.Services.TributePortalSchedulerService.EmailMessages.cs
///Author          : 
///Creation Date   : 
///Description     : This file is used to send mails to users whose tribute are 
///                  about to expire or expired.
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
//using System.Web.Mail;
using System.Net.Mail;

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
        /// to send mails to the customers
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
                client.Host = ConfigurationManager.AppSettings["MailServer"].ToString();
                client.Send(msg);
                return true;

            }
            catch
            {
                // throw ex;
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
                msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["SponsorEmail"].ToString()));
                msg.Subject = strSubject;
                msg.Body = strBody;

                //Add the Creddentials
                SmtpClient client = new SmtpClient();
                client.Host = ConfigurationManager.AppSettings["MailServer"].ToString();
                client.Send(msg);
                return true;

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
