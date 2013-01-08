///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.MessagingSystemManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the basic Messaging System methods
///Audit Trail     : Date of Modification  Modified By         Description



#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
#endregion

namespace TributesPortal.BusinessLogic
{
    public partial class MessagingSystemManager
    {
        /// <summary>
        /// Method to send email to the list of users
        /// </summary>
        /// <param name="objUsers">EmailLink entity containing User name and email address</param>
        public void SendEmail(EmailLink objEmailInfo)
        {
            EmailMessages objEmail = EmailMessages.Instance;
            //StringBuilder sbToEmail = new StringBuilder();
            //string strMessageSubject = GetMessageSubject(objEmailInfo);
            //string strMessageBody = GetEmailBody(objEmailInfo);
            
            foreach (string strEmail in objEmailInfo.EmailTo)
            {
                bool val = objEmail.SendMessages(objEmailInfo.FromEmailAddress, strEmail, objEmailInfo.EmailSubject, objEmailInfo.EmailBody, EmailMessages.TextFormat.Html.ToString());
            }
        }

        /// <summary>
        /// Method to get the body part of email.
        /// </summary>
        /// <param name="objUserInfo">Filled EmailLink entity</param>
        /// <returns>HTML string of body part</returns>
        private string GetEmailBody(EmailLink objEmailInfo)
        {
            string strTypeName = objEmailInfo.TypeName;

            StringBuilder sbEmailbody = new StringBuilder();
            try
            {
                sbEmailbody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + objEmailInfo.FromUserName);
                if (objEmailInfo.TypeName == "GuestBook")
                    sbEmailbody.Append(" wants you to read a guestbook in the tribute.</p>");
                else
                    sbEmailbody.Append(" wants you to view the Tribute.</p>");
                
                sbEmailbody.Append("<p>To view the ");
                sbEmailbody.Append(strTypeName.ToLower()); 
                sbEmailbody.Append(", follow the link below:<br/>");

                //if (objEmailInfo.TypeName == "VideoGallery") //url for video gallery page
                //    sbEmailbody.Append("<a href='" + objEmailInfo.UrlToEmail + "?UserTributeId=1'>" + objEmailInfo.UrlToEmail + "</a>"); //TO DO: TributeId to be picked from session
                //else if (objEmailInfo.TypeName == "ManageVideo") //url for Manage Video page
                //    sbEmailbody.Append("<a href='" + objEmailInfo.UrlToEmail + "'>" + objEmailInfo.UrlToEmail + "</a>");
                //else if (objEmailInfo.TypeName == "GuestBook") //url for Guest Book page
                
                sbEmailbody.Append(objEmailInfo.UrlToEmail + "</p>");

                sbEmailbody.Append("<p>---<br/>");
                sbEmailbody.Append("Your Tribute Team</p></font>");
                
                //sbEmailbody.Append("Regards <br/>" + objEmailInfo.FromEmailAddress);
            }
            catch (Exception ex)
            {
                //throw ex.Message;
            }
            return sbEmailbody.ToString();
        }

        /// <summary>
        /// Method to get the subject line based on the page to be emailed
        /// </summary>
        /// <param name="objEmailInfo">Email link entity containing Type</param>
        /// <returns>Subject in string format.</returns>
        private string GetMessageSubject(EmailLink objEmailInfo)
        {
            StringBuilder sbEmailSubject = new StringBuilder();

            sbEmailSubject.Append(objEmailInfo.FromUserName);
            if (objEmailInfo.TypeName == "GuestBook")
                sbEmailSubject.Append(" wants you to read a guestbook on Your Tribute..." + "<br/>");
            else
                sbEmailSubject.Append(" wants you to view a Tribute");

            return sbEmailSubject.ToString();
        }
    }
}
