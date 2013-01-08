///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.EmailManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the methods associated with sending of emails
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.ResourceAccess;
using System.Web;

namespace TributesPortal.BusinessLogic
{
    public class EmailManager
    {
        string AdministratorMail = WebConfig.AdministratorMail;
        string TributeCreationAdmin = WebConfig.TributeCreationAdmin;

        //System.Configuration.ConfigurationManager.

        public void SendOwnerMails(string TributeOwner, string TributeId, string TributeName, int Packageid, string TributeType, string TributeUrl, bool Notifyme, string userName)
        {
            string _EmailBody = string.Empty;
            EmailMessages objEmail = EmailMessages.Instance;
            MailBodies objMail = new MailBodies();


            if(WebConfig.ApplicationType.ToLower()=="yourmoments")
            {
                if ((Packageid == 3) || (Packageid == 8))
                    _EmailBody = objMail.CreationFree(userName, TributeType, TributeName, TributeUrl, TributeId);
                else if ((Packageid == 2) || (Packageid == 5) || (Packageid == 7))
                {
                    if (Notifyme.Equals(true))
                        _EmailBody = objMail.Creation1YearAuto(userName, TributeType, TributeName, TributeUrl, TributeId);
                    else
                        _EmailBody = objMail.Creation1Year(userName, TributeType, TributeName, TributeUrl, TributeId);
                }
                else if ((Packageid == 1) || (Packageid == 4) || (Packageid == 6))
                    _EmailBody = objMail.CreationLifeTime(userName, TributeType, TributeName, TributeUrl, TributeId);
                //string _EmailBody = GetOwnerAdminBody(TributeId,TributeName);
                if (HttpContext.Current.Session["FromVideoTributeCreation"] == null)
                {
                    bool val = objEmail.SendMessages("Your " + WebConfig.ApplicationWord + " <" + TributeCreationAdmin + ">", TributeOwner, "Your " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " has been created", _EmailBody, EmailMessages.TextFormat.Html.ToString());
                }
                else
                {
                    bool val = objEmail.SendMessages("Your " + WebConfig.ApplicationWord + " <" + TributeCreationAdmin + ">", TributeOwner, "Your " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " has been created", _EmailBody, EmailMessages.TextFormat.Html.ToString());
                }
            }
            else
            {
                // Changes for new tribute packages in website upgrade phase 1 by UD
                if (Packageid == 3)
                {
                    _EmailBody = objMail.CreationFree(userName, TributeType, TributeName, TributeUrl, TributeId);
                }
                else if (Packageid == 8)
                {
                    _EmailBody = objMail.CreationObituaryFree(userName, TributeType, TributeName, TributeUrl, TributeId);
                }
                else if ((Packageid == 6) || (Packageid == 7))
                {
                    _EmailBody = objMail.CreationPremiumObituary(userName, TributeType, TributeName, TributeUrl, TributeId);
                }
                else if ((Packageid == 2) || (Packageid == 5))
                {
                    _EmailBody = objMail.CreationMemorialTribute1Year(userName, TributeType, TributeName, TributeUrl, TributeId);
                }
                else if ((Packageid == 1) || (Packageid == 4))
                {
                    _EmailBody = objMail.CreationMemorialLifeTime(userName, TributeType, TributeName, TributeUrl, TributeId);
                }


                if ((Packageid == 3) || (Packageid == 8) || (Packageid == 6) || (Packageid == 7))
                {
                    bool val = objEmail.SendMessages("Your " + WebConfig.ApplicationWord + " <" + TributeCreationAdmin + ">", TributeOwner, "Your Obituary has been created", _EmailBody, EmailMessages.TextFormat.Html.ToString());
                }
                else
                {
                    bool val = objEmail.SendMessages("Your " + WebConfig.ApplicationWord + " <" + TributeCreationAdmin + ">", TributeOwner, "Your Memorial Tribute has been created", _EmailBody, EmailMessages.TextFormat.Html.ToString());
                }   
            }

            
        }

        public void SendAdminMails(int SendByUserId, string SendToUserId, string TributeId, string TributeName, string TributeType, string userName)
        {
            Tributes objTribute = new Tributes();
            objTribute.TributeId = int.Parse(TributeId);
            TributeManager objtrbmngr = new TributeManager();
            objtrbmngr.GetTributeSession(objTribute);

            EmailMessages objEmail = EmailMessages.Instance;
            // string fromMail = GetEmail(SendByUserId);
            string fromMail = "Your Tribute <" + WebConfig.NoreplyEmail + ">";
            string tomail = SendToUserId;

            MailBodies objMailbod = new MailBodies();
            string _EmailBody = objMailbod.AdminRequest(userName, TributeType, TributeName, objTribute.TributeUrl, TributeId, tomail);

            bool val = objEmail.SendMessages(fromMail, tomail, userName + " made you a " + WebConfig.ApplicationWordForInternalUse.ToLower() + " administrator...", _EmailBody, EmailMessages.TextFormat.Html.ToString());




        }

        public void SendAdminMails(string Adminmail, string OtherAdminMails, string TributeId, string TributeName, string TributeType, string TributeUrl, string Username)
        {

            UserInfoResource objUser = new UserInfoResource();
            EmailMessages objEmail = EmailMessages.Instance;
            string[] toemails = OtherAdminMails.Split(',');
            for (int i = 0; i < toemails.Length; i++)
            {
                MailBodies objMailbod = new MailBodies();
                string _EmailBody = objMailbod.AdminRequest(Username, TributeType, TributeName, TributeUrl, TributeId, toemails[i]);
                // string _EmailBody = GetAdminBody(TributeId, TributeName, toemails[i]);
                bool val = objEmail.SendMessages(Adminmail, toemails[i], Username + " made you a " + WebConfig.ApplicationWordForInternalUse.ToLower() + " administrator...", _EmailBody, EmailMessages.TextFormat.Html.ToString());


            }


        }

        private string GetOwnerAdminBody(string TributeId, string TributeName)
        {
            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);
            string _Emailbody = string.Empty;
            _Emailbody += "You have successfully created " + WebConfig.ApplicationWordForInternalUse.ToLower() + " " + TributeName;
            _Emailbody += "  <a href='http://" + Servername + "/Users/log_in.aspx?Tributeid=" + TributeId + "&TributeName=" + TributeName + "&Isowner=true' >Click here to go for Confirmation</a>";
            return _Emailbody;
        }

        private string GetAdminBody(string TributeId, string TributeName, string email)
        {
            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);
            string _Emailbody = string.Empty;
            _Emailbody += "You are requested to become admin of " + WebConfig.ApplicationWordForInternalUse + " " + TributeName;
            _Emailbody += "  <a href='http://" + Servername + "/Users/log_in.aspx?Tributeid=" + TributeId + "&TributeName=" + TributeName + "&Email=" + email + "' >Click here to go for Confirmation</a>";
            return _Emailbody;
        }

        public void SendMail(int SendByUserId, int SendToUserId, string Subject, string _EmailBody)
        {

            MailMessage _objMailMessage = new MailMessage();
            _objMailMessage.SendByUserId = SendByUserId;
            _objMailMessage.Subject = Subject;
            _objMailMessage.Body = _EmailBody;
            _objMailMessage.SendToUserId = SendToUserId;
            _objMailMessage.SendDate = DateTime.Now;
            _objMailMessage.Status = 1;
            _objMailMessage.RecievedDate = DateTime.Now;
            _objMailMessage.CreatedBy = SendByUserId;
            _objMailMessage.CreatedDate = DateTime.Now;
            _objMailMessage.ModifiedBy = SendByUserId;
            _objMailMessage.ModifiedDate = DateTime.Now;
            _objMailMessage.IsActive = true;
            _objMailMessage.IsDeleted = false;
            object[] _ParamMail = { _objMailMessage };
            UserInfoResource objUser = new UserInfoResource();

            if (objUser.SaveEmail(_ParamMail) > 0)
            {
                EmailMessages objEmail = EmailMessages.Instance;

                //to get user details of from user id
                UserRegistration objFromUserInfo = new UserRegistration();
                objFromUserInfo = (UserRegistration)GetUserDetails(SendByUserId);
                //to get user details of TO user id
                UserRegistration objToUserInfo = new UserRegistration();
                objToUserInfo = (UserRegistration)GetUserDetails(SendToUserId);
                if (objToUserInfo.EmailNotification.MessagesNotify)
                {
                    bool val = objEmail.SendMessages("Your " + WebConfig.ApplicationWord + " <" + WebConfig.NoreplyEmail + ">", objToUserInfo.Users.Email, GetEmailSubject(objFromUserInfo), GetEmailBody(objFromUserInfo, objToUserInfo, Subject, _EmailBody), EmailMessages.TextFormat.Html.ToString());
                }
            }
        }


        public void SendMailReply(int SendByUserId, int SendToUserId, string Subject, string _EmailBody, int Messageid)
        {
            EmailMessages objEmail = EmailMessages.Instance;
            //string fromMail = GetEmail(SendByUserId);
            //string tomail = GetEmail(SendToUserId);
            //bool val = objEmail.SendMessages(fromMail, tomail, Subject, _EmailBody, EmailMessages.TextFormat.Html.ToString());.

            //to get user details of from user id
            UserRegistration objFromUserInfo = new UserRegistration();
            objFromUserInfo = (UserRegistration)GetUserDetails(SendByUserId);
            //to get user details of TO user id
            UserRegistration objToUserInfo = new UserRegistration();
            objToUserInfo = (UserRegistration)GetUserDetails(SendToUserId);

            bool val = objEmail.SendMessages("Your " + WebConfig.ApplicationWord + " <" + WebConfig.NoreplyEmail + ">", objToUserInfo.Users.Email, GetEmailSubject(objFromUserInfo), GetEmailBody(objFromUserInfo, objToUserInfo, Subject, _EmailBody), EmailMessages.TextFormat.Html.ToString());

            MailMessage _objMailMessage = new MailMessage();
            _objMailMessage.SendByUserId = SendByUserId;
            _objMailMessage.Subject = Subject;
            _objMailMessage.Body = _EmailBody;
            _objMailMessage.SendToUserId = SendToUserId;
            _objMailMessage.SendDate = DateTime.Now;
            _objMailMessage.Status = 1;
            _objMailMessage.RecievedDate = DateTime.Now;
            _objMailMessage.CreatedBy = SendByUserId;
            _objMailMessage.CreatedDate = DateTime.Now;
            _objMailMessage.ModifiedBy = SendByUserId;
            _objMailMessage.ModifiedDate = DateTime.Now;
            _objMailMessage.IsActive = true;
            _objMailMessage.IsDeleted = false;
            _objMailMessage.MessageId = Messageid;
            object[] _ParamMail = { _objMailMessage };
            UserInfoResource objUser = new UserInfoResource();
            objUser.SaveEmailReply(_ParamMail);

        }

        private string GetEmail(int UserId)
        {
            string email = string.Empty;
            Users objusr = new Users();
            objusr.UserId = UserId;
            UserRegistration objUserReg = new UserRegistration();
            objUserReg.Users = objusr;
            object[] objparam = { objUserReg };
            UserInfoResource objinfo = new UserInfoResource();
            objinfo.GetUserDetails(objparam);
            if (objUserReg.Users != null)
                email = objUserReg.Users.Email;
            return email;

        }

        /// <summary>
        /// Method to get subject for the message to be sent when user sends message to inbox
        /// </summary>
        /// <param name="objFromUser">User Id</param>
        /// <returns>Returns string of subject line.</returns>
        private string GetEmailSubject(UserRegistration objFromUser)
        {
            StringBuilder sbBody = new StringBuilder();

            if (objFromUser.Users.UserType == 1)
            {
                if (objFromUser.Users.FirstName == string.Empty)
                    sbBody.Append(objFromUser.Users.UserName);
                else
                    sbBody.Append(objFromUser.Users.FirstName + " " + objFromUser.Users.LastName);
            }
            else
            {
                sbBody.Append(objFromUser.Users.FirstName);
            }

            //if (objFromUser.Users.FirstName != string.Empty && objFromUser.Users.LastName != string.Empty)
            //    sbBody.Append(objFromUser.Users.FirstName + " " + objFromUser.Users.LastName);
            //else
            //    sbBody.Append(objFromUser.Users.UserName);

            sbBody.Append(" sent you a message on Your " + WebConfig.ApplicationWord + "...");

            return sbBody.ToString();
        }

        /// <summary>
        /// Method to get email body for the message to be sent when user sends message to inbox
        /// </summary>
        /// <param name="objFromUser">From user details</param>
        /// <param name="objToUser">To user details</param>
        /// <param name="strSubject">Subject of message entered by user</param>
        /// <param name="strMessage">Message entered by ujser</param>
        /// <returns>String of email body.</returns>
        private string GetEmailBody(UserRegistration objFromUser, UserRegistration objToUser, string strSubject, string strMessage)
        {
            StringBuilder sbBody = new StringBuilder();

            sbBody.Append("Dear ");
            if (objToUser.Users.UserType == 1)
            {
                if (objToUser.Users.FirstName == string.Empty)
                    sbBody.Append(objToUser.Users.UserName);
                else
                    sbBody.Append(objToUser.Users.FirstName + " " + objToUser.Users.LastName);
            }
            else
            {
                sbBody.Append(objToUser.Users.FirstName);
            }
            //if (objToUser.Users.FirstName != string.Empty && objToUser.Users.LastName != string.Empty)
            //    sbBody.Append(objToUser.Users.FirstName + " " + objToUser.Users.LastName);
            //else
            //    sbBody.Append(objToUser.Users.UserName);

            sbBody.Append(",");
            sbBody.Append("<br/>");
            sbBody.Append("<br/>");

            if (objFromUser.Users.UserType == 1)
            {
                if (objFromUser.Users.FirstName == string.Empty)
                    sbBody.Append(objFromUser.Users.UserName);
                else
                    sbBody.Append(objFromUser.Users.FirstName + " " + objFromUser.Users.LastName);
            }
            else
            {
                sbBody.Append(objFromUser.Users.FirstName);
            }

            sbBody.Append(" sent you a message...");
            sbBody.Append("<br/>");
            sbBody.Append("<br/>");
            //sbBody.Append("-------------");
            sbBody.Append("<br/>");
            sbBody.Append("Subject: " + strSubject);
            sbBody.Append("<br/>");
            sbBody.Append("<br/>");
            if (strMessage.Length > 100)
                sbBody.Append(strMessage.Substring(0, 100) + "...");
            else
            {
                strMessage = strMessage.Replace("\n", "<br/>");
                sbBody.Append(strMessage);
            }

            sbBody.Append("<br/>");
            //sbBody.Append("-------------");
            sbBody.Append("<br/>");
            sbBody.Append("To read and reply to the message, follow the link below:");
            sbBody.Append("<br/>");
            string strLink = "http://www." + WebConfig.TopLevelDomain + "/inbox.aspx";
            sbBody.Append("<a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx?mode=inbox'>" + strLink + "</a>");
            sbBody.Append("<br/>");
            sbBody.Append("<br/>");
            sbBody.Append("-------");
            sbBody.Append("<br/>");
            sbBody.Append("Your " + WebConfig.ApplicationWord + "");

            return sbBody.ToString();
        }

        /// <summary>
        /// Method to get user details.
        /// </summary>
        /// <param name="userId">Userid</param>
        /// <returns>UserRegistration entity containing user details.</returns>
        private UserRegistration GetUserDetails(int userId)
        {
            Users objFromUser = new Users();
            objFromUser.UserId = userId;
            UserRegistration objUserReg = new UserRegistration();
            objUserReg.Users = objFromUser;
            object[] objparam = { objUserReg };
            UserInfoResource objFromUserInfo = new UserInfoResource();
            objFromUserInfo.GetUserDetails(objparam);
            return objUserReg;
        }

        //New Methods added for Sponsor email functionality.
        public void SendSponsorMails(String Sponsor, string FromUserEmail, int TributeId, string TributeType, string TributeName, string TributeUrl, string Expiry, int PackageId)
        {
            string _EmailBody = string.Empty;
            EmailMessages objEmail = EmailMessages.Instance;
            MailBodies objMail = new MailBodies();
            StoryResource objStoryRes = new StoryResource();
            UserInfo objUser = new UserInfo();
            objUser = objStoryRes.GetTributeAdministrators(TributeId, "");
            if (PackageId == 2 || PackageId == 5 || PackageId == 7)
            {
                _EmailBody = objMail.TributeSponsor1Year(Sponsor, TributeId, TributeType, TributeName, TributeUrl, Expiry);
            }
            if (PackageId == 1 || PackageId == 4 || PackageId == 6)
                _EmailBody = objMail.TributeSponsorLife(Sponsor, TributeId, TributeType, TributeName, TributeUrl, Expiry);
            if (Sponsor != null && Sponsor != String.Empty)
            {
                // For the anonymous email when the sponsor does not want to show who he is to the tribute owner
                if (Sponsor.Contains("An anonymous person"))
                {
                    bool val = objEmail.SendMessages(FromUserEmail, objUser.UserEmail, "Someone has sponsored the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + "...", _EmailBody, EmailMessages.TextFormat.Html.ToString());
                }
                else
                {
                    bool val = objEmail.SendMessages(FromUserEmail, objUser.UserEmail, Sponsor + " sponsored the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + "...", _EmailBody, EmailMessages.TextFormat.Html.ToString());
                }
            }
        }

        public void SendSponsorMailsAnno(String Sponsor, int TributeId, string TributeType, string TributeName, string TributeUrl, string Expiry, int PackageId)
        {
            string _EmailBody = string.Empty;
            EmailMessages objEmail = EmailMessages.Instance;
            MailBodies objMail = new MailBodies();
            StoryResource objStoryRes = new StoryResource();
            UserInfo objUser = new UserInfo();
            objUser = objStoryRes.GetTributeAdministrators(TributeId, "");
            if (PackageId == 2 || PackageId == 5 || PackageId == 7)
            {
                _EmailBody = objMail.TributeSponsorAnon1Year(TributeType, TributeName, TributeUrl, Expiry);
            }
            if (PackageId == 1 || PackageId == 4 || PackageId == 6)
                _EmailBody = objMail.TributeSponsorAnonLife(TributeType, TributeName, TributeUrl, Expiry);

            bool val = objEmail.SendMessages("Your " + WebConfig.ApplicationWord + "<" + WebConfig.NoreplyEmail + ">", objUser.UserEmail, "An Anonymous user has sponsored the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + "...", _EmailBody, EmailMessages.TextFormat.Html.ToString());
        }

        public void SendSponsorMailsWithMessage(String Sponsor, String Message, int TributeId, string TributeType, string TributeName, string TributeUrl, string Expiry, int PackageId)
        {
            string _EmailBody = string.Empty;
            EmailMessages objEmail = EmailMessages.Instance;
            MailBodies objMail = new MailBodies();
            StoryResource objStoryRes = new StoryResource();
            UserInfo objUser = new UserInfo();
            objUser = objStoryRes.GetTributeAdministrators(TributeId, "");
            if (PackageId == 2 || PackageId == 5 || PackageId == 7)
            {
                _EmailBody = objMail.TributeSponsor1YearWithMessage(Sponsor, Message, TributeType, TributeName, TributeUrl, Expiry);
            }
            if (PackageId == 1 || PackageId == 4 || PackageId == 6)
                _EmailBody = objMail.TributeSponsorLifeWithMessage(Sponsor, Message, TributeType, TributeName, TributeUrl, Expiry);

            bool val = objEmail.SendMessages("Your " + WebConfig.ApplicationWord + "<" + WebConfig.NoreplyEmail + ">", objUser.UserEmail, Sponsor + " has sponsored the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + "...", _EmailBody, EmailMessages.TextFormat.Html.ToString());
        }

    }
}
