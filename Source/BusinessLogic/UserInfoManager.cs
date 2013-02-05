///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.BusinessLogic.UserInfoManager.cs
///Author          : 
///Creation Date   : 
///Description     : This class helps to perform operations related to providing information about/to the users
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.ResourceAccess;
//using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;


//using System.Web.HttpContext

namespace TributesPortal.BusinessLogic
{
    public class UserInfoManager
    {
        string AdministratorMail = WebConfig.AdministratorMail;
        string ForgetPassAdmin = WebConfig.ForgetPassAdmin;

        public void UserLogin(GenralUserInfo _objGenralUserInfo)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objGenralUserInfo };
            objUser.CheckLogin(param);
        }

        public void CheckFacebookAccountAvailability(GenralUserInfo _objGenralUserInfo)
        {
            UserInfoResource objUser = new UserInfoResource();
            objUser.CheckFacebookAccountAvailability(_objGenralUserInfo);
        }

        public void UserSiteAdminLogin(GenralUserInfo _objGenralUserInfo)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objGenralUserInfo };
            objUser.SignInSiteAdmin(param);
        }
        public void UserSummaryReport(UsersSummaryReport objSummary, string applicationType)
        {
            UserInfoResource objUser = new UserInfoResource();
            objUser.UserSummaryReport(objSummary, applicationType);
        }
        public IList<ParameterTypesCodes> EmailNotifications(object[] param)
        {
            ParameterResource objParameterResource = new ParameterResource();
            return objParameterResource.EmailNotifications(param);
        }
        public void UpdateEmailNotofication(object[] param)
        {
            UserInfoResource objUserInfoResource = new UserInfoResource();
            objUserInfoResource.UpdateEmailNotofication(param);
        }
        //

        public void GetUserDetails(UserRegistration _objUserRegistration)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objUserRegistration };
            objUser.GetUserDetails(param);
        }

        //Mohit Gupta 31 jan 2011 For tribute upgrade
        public void GetUserDetailsFromEmail(UserRegistration _objUserRegistration, int tributeId)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objUserRegistration, tributeId };
            objUser.GetUserDetailsFromEmail(param);
        }

        public void GetUserCompleteDetails(UserRegistration _objUserRegistration)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objUserRegistration };
            objUser.GetUserCompleteDetails(param);
        }

        public void GetEmailNotofication(UserRegistration _objUserRegistration)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objUserRegistration };
            objUser.GetEmailNotofication(param);
        }
        public void UpdatePersonalDetails(UserRegistration _objUserRegistration)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objUserRegistration };
            //Transaction started
            using (TransactionScope trans = new TransactionScope())
            {
                objUser.UpdatePersonalDetails(param);
                //Transaction commited
                trans.Complete();
            }
        }
        public void UpdateFacebookAssociation(UserRegistration _objUserRegistration)
        {
            UserInfoResource objUser = new UserInfoResource();
            objUser.UpdateFacebookAssociation(_objUserRegistration);
        }

        public void RemoveFacebookAssociation(UserRegistration _objUserRegistration)
        {
            UserInfoResource objUser = new UserInfoResource();
            objUser.RemoveFacebookAssociation(_objUserRegistration);
        }

        public void UpdatePrivacySettings(UserRegistration _objUserRegistration)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objUserRegistration };
            objUser.UpdatePrivacySettings(param);
        }

        public void UserAvailability(UserRegistration _objUserRegistration)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objUserRegistration };
            objUser.UserAvailability(param);
        }

        /// <summary>
        /// Return Email Availablity. modified by udham 5 Oct, 2011
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public int EmailAvailable(string Email,string ApplicationType)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.EmailAvailable(Email,ApplicationType);
        }
        public void ConformAdmin(Tributes objTributesUserInfo, SessionValue objGenralUserInfo, bool _Accept)
        {
            UserInfoResource objUser = new UserInfoResource();
            if (_Accept == true)
            {
                object[] param = { objTributesUserInfo, objGenralUserInfo };
                objUser.ConformAdmin(param);
                SendEmailForAdminConfirmation(objGenralUserInfo, objTributesUserInfo);
            }
            /*else
            {
                object[] param ={ objTributesUserInfo, objGenralUserInfo };
                objUser.DeclineAdmin(param);
            }*/
        }

        public void GetTributeOnId(TributesUserInfo _objTributeUserinfo)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objTributeUserinfo };
            objUser.GetTributeOnId(param);
        }

        public void GetTributeByID(TributesUserInfo _objTributeUserinfo)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objTributeUserinfo };
            objUser.GetTributeByID(param);
        }


        /*public string GetEventName(int _EventId)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.GetEventName(_EventId);
        }*/
        public void CheckAndSendPassword(GenralUserInfo _objGenralUserInfo, bool _Reset)
        {

            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objGenralUserInfo };
            objUser.CheckAndSendPassword(param, _Reset);

            if (_objGenralUserInfo.CustomError == null)
            {
                EmailMessages objEmail = EmailMessages.Instance;
                //string _EmailBody = GetEmailBody(_objGenralUserInfo.RecentUsers);
                MailBodies objMail = new MailBodies();
                string Password = TributePortalSecurity.Security.DecryptSymmetric(_objGenralUserInfo.RecentUsers.UserPassword);
                string _EmailBody = objMail.ForgetPassword(_objGenralUserInfo.RecentUsers.UserName, Password);
                bool val = objEmail.SendMessages(ForgetPassAdmin, _objGenralUserInfo.RecentUsers.UserEmail, "Your Tribute Password Reminder", _EmailBody, EmailMessages.TextFormat.Html.ToString());
                // bool val = objEmail.SendMessages("mkumar@in.sopragroup.com", "mkumar@in.sopragroup.com", "Forgot password", _EmailBody, EmailMessages.TextFormat.Html.ToString());


            }

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

        public void ChangeEmailPassword(GenralUserInfo _objGenralUserInfo)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objGenralUserInfo };
            string usermail = GetEmail(_objGenralUserInfo.RecentUsers.UserID);
            try
            {
                string _EmailBody = string.Empty;
                string _Subject = string.Empty;
                if (objUser.ChangeEmailPassword(param) > 0)
                {
                    EmailMessages objEmail = EmailMessages.Instance;
                    //string _EmailBody = GetEmailBodyOnEmailAndPasswordChanged(_objGenralUserInfo.RecentUsers);
                    MailBodies objMail = new MailBodies();
                    if ((_objGenralUserInfo.RecentUsers.UserEmail.Length != 0) && (_objGenralUserInfo.RecentUsers.UserPassword.Length > 0))
                    {
                        _Subject = "Your Tribute Password and Email Reminder";
                        _EmailBody = objMail.ChangeEmailPassword(_objGenralUserInfo.RecentUsers.UserName, _objGenralUserInfo.RecentUsers.UserEmail, _objGenralUserInfo.RecentUsers.UserPassword);

                    }
                    else if ((_objGenralUserInfo.RecentUsers.UserEmail.Length == 0) && (_objGenralUserInfo.RecentUsers.UserPassword.Length != 0))
                    {
                        _Subject = "Your Tribute Password Reminder";
                        _EmailBody = objMail.ForgetPassword(_objGenralUserInfo.RecentUsers.UserName, _objGenralUserInfo.RecentUsers.UserPassword);
                    }
                    else if ((_objGenralUserInfo.RecentUsers.UserEmail.Length != 0) && (_objGenralUserInfo.RecentUsers.UserPassword.Length == 0))
                    {
                        _Subject = "Your Tribute Email Reminder";
                        _EmailBody = objMail.ChangeEmail(_objGenralUserInfo.RecentUsers.UserName, _objGenralUserInfo.RecentUsers.UserEmail);
                    }

                    bool val = objEmail.SendMessages(ForgetPassAdmin, usermail, _Subject, _EmailBody, EmailMessages.TextFormat.Html.ToString());


                }
            }
            catch (Exception ex)
            {
                Errors objError = new Errors();
                objError.ErrorMessage = ex.Message;
                _objGenralUserInfo.CustomError = objError;
            }
        }
        public void GetUserData(GenralUserInfo _objGenralUserInfo)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objGenralUserInfo };
            objUser.GetUserData(param);
        }

        private string GetEmailBodyForAdminAcceptDecline(SessionValue objUserInfo, string mail, bool _Accept)
        {
            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);

            string _Emailbody = string.Empty;
            try
            {
                if (_Accept == true)
                {
                    _Emailbody += "Your request for admin to " + objUserInfo.UserEmail + " Accepted";
                }
                else
                {
                    _Emailbody += "Your request for admin to " + mail + "Decline";
                }
                //_Emailbody += " <a href='http://" + Servername + "/log_in.aspx'>Click here to go for Login Page</a>";
                _Emailbody += " <a href='http://" + WebConfig.TopLevelDomain + "/log_in.aspx'>Click here to go for Login Page</a>";
            }
            catch (Exception e1)
            {
                throw e1;
            }
            return _Emailbody;
        }

        private string GetEmailBodyOnEmailAndPasswordChanged(UserInfo objUserInfo)
        {
            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);
            string _Emailbody = string.Empty;
            try
            {
                _Emailbody += "Your Email and Password updated Successfuly<Br/>";
                _Emailbody += "New Password:" + TributePortalSecurity.Security.DecryptSymmetric(objUserInfo.UserPassword) + "<br/>";
                //_Emailbody += " <a href='http://" + Servername + "/log_in.aspx'>Click here to go for Login Page</a>";
                _Emailbody += " <a href='http://" + WebConfig.TopLevelDomain + "/log_in.aspx'>Click here to go for Login Page</a>";
            }
            catch (Exception e1)
            {
                //throw e1.Message;
            }
            return _Emailbody;
        }

        private string GetEmailBody(UserInfo objUserInfo)
        {
            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);
            string _Emailbody = string.Empty;
            try
            {
                _Emailbody += "User Name : " + objUserInfo.UserName + "<br/>";
                _Emailbody += "Pssword : " + TributePortalSecurity.Security.DecryptSymmetric(objUserInfo.UserPassword) + "<br/>";
                _Emailbody += "First Name : " + objUserInfo.FirstName + "<br/>";
                _Emailbody += "Last Name : " + objUserInfo.LastName + "<br/>";
                _Emailbody += "User Type : " + objUserInfo.UserTypeDescription + "<br/>";
                //_Emailbody += " <a href='http://" + Servername + "/log_in.aspx'>Click here to go for Login Page</a>";
                _Emailbody += " <a href='http://" + WebConfig.TopLevelDomain + "/log_in.aspx'>Click here to go for Login Page</a>";
            }
            catch (Exception e1)
            {

            }
            return _Emailbody;
        }

        private string GetEmailAccountBody(Users objUserInfo)
        {
            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);
            StringBuilder objstr = new StringBuilder();

            if (objUserInfo.UserType == 1)
            {
                objstr.Append("<p style='font-size: 16px; font-family:Lucida Sans;'><b>Welcome " +
                              objUserInfo.FirstName + " " + objUserInfo.LastName + ".</b></p>");
                objstr.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>Thank you for registering with Your Tribute. With Your Tribute you can <br/>");
                objstr.Append("contribute messages, photos, videos and other content to Tributes created by <br/>");
                objstr.Append("others. You can also create your own Tribute and invite friends and family <br/>");
                objstr.Append("to share their memories.</p>");
                objstr.Append("<p style='font-size: 14px; font-family:Lucida Sans;'>");
                if(objUserInfo.Email!=string.Empty)
                {
                    objstr.Append("<b>Your Email : </b>" + objUserInfo.Email + "<br/>");
                }
                if (!objUserInfo.Password.Equals(string.Empty))
                {
                    objstr.Append("<b>Your Password</b> : " + TributePortalSecurity.Security.DecryptSymmetric(objUserInfo.Password) +
                                  "<br/>");
                }
                objstr.Append("<b>Account Login:</b> <a href='http://" + WebConfig.TopLevelDomain + "/log_in.aspx'>http://" +
                              WebConfig.TopLevelDomain + "/log_in.aspx</a></p>");
                objstr.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Start off on the right track</p>");
                objstr.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>Read our ");
                objstr.Append("<a href='http://support.yourtribute.com/forums/94844-how-to-guides'>How-To Guides</a>");
                objstr.Append(" and <a href='http://support.yourtribute.com/forums/97320-faqs'>FAQs");
                objstr.Append("</a> for helpful information on how to use Your <br/>");
                objstr.Append("Tribute. Still need help? Email our friendly ");
                objstr.Append("<a href='http://support.yourtribute.com/anonymous_requests/new'>Customer Support Team</a>.</p>");
                objstr.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Keep up with Your Tribute</p>");
                objstr.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
                objstr.Append("View our <a href='http://blog.yourtribute.com/'>Blog</a> and sign up to receive our ");
                objstr.Append("<a href='http://eepurl.com/rlagp'>NewsLetter</a> for helpful tips, feature <br/>");
                objstr.Append("updates, company news and special offers. You can also follow us on your <br/>");
                objstr.Append("favorite social websites: <a href='http://www.facebook.com/yourtribute'>Facebook</a>,");
                objstr.Append(" <a href='http://www.twitter.com/yourtribute'>Twitter</a>,");
                objstr.Append(" <a href='https://plus.google.com/u/0/109473191564708020938/posts'>Google+</a>");
                objstr.Append(" and <a href='http://pinterest.com/yourtribute/'>Pinterest</a>.</p>");
                objstr.Append("<p style='font-size: 14px; font-family:Lucida Sans;'><br/>Sincerely,<br/>The Your Tribute Team</p>");
                
            }
            else
            {
                
                objstr.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>Welcome " +
                              objUserInfo.FirstName + " " + objUserInfo.LastName + "</p>");
                objstr.Append("<p>Thank you for registering with Your Tribute!</p>");
                objstr.Append("<p>Please keep this email in case you forget your account information.<br/>");
                if (objUserInfo.UserName != string.Empty)
                {
                    objstr.Append("Username: " + objUserInfo.UserName + "<br/>");
                }
                else
                {
                    objstr.Append("Username: " + objUserInfo.Email + "<br/>");
                }
                if (!objUserInfo.Password.Equals(string.Empty))
                {
                    objstr.Append("Password: " + TributePortalSecurity.Security.DecryptSymmetric(objUserInfo.Password) +
                                  "<br/>");
                }
                //objstr.Append("User Profile: <a href='http:" + Servername + "/Users/log_in.aspx'>http://www." + WebConfig.TopLevelDomain + "/" + objUserInfo.UserName + " </a></p>");
                objstr.Append("User Profile: <a href='http://" + WebConfig.TopLevelDomain + "/log_in.aspx'>http://" +
                              WebConfig.TopLevelDomain + "/log_in.aspx</a></p>");
                objstr.Append("<p>With a Your Tribute membership you can:<br/>");
                objstr.Append("• <a href='http://" + WebConfig.TopLevelDomain +
                              "/log_in.aspx?PageName=TributeCreation'>Create a tribute</a>  to celebrate a significant event or a special someone<br/>");
                objstr.Append(
                    "• Collaborate with friends and family—leave messages in the guestbook, share photos and videos, send virtual gifts, and receive event invites and updates<br/></p>");
                objstr.Append("• <a href='http://" + WebConfig.TopLevelDomain +
                              "/features.aspx'>Take a Tour</a> to learn more about Your Tribute, or find Help at the bottom of any page. ");
                objstr.Append("<p>To get started, please sign in to your account at <a href='http://" +
                              WebConfig.TopLevelDomain + "/log_in.aspx'>http://www." + WebConfig.TopLevelDomain + "</a>" +
                              ".</p>");
                objstr.Append("<p>-----<br/>");
                objstr.Append("Your Tribute Team</p></font>");
            }
            return objstr.ToString();

        }

        private void SendMail(UserRegistration _objGenralUserInfo, int Userid, UserInfoResource objUser)
        {


            string SIGNUPAdmin = WebConfig.SIGNUPAdmin;
            EmailMessages objEmail = EmailMessages.Instance;
            _objGenralUserInfo.Users.FirstName = _objGenralUserInfo.UserBusiness == null ? _objGenralUserInfo.Users.FirstName : _objGenralUserInfo.UserBusiness.CompanyName;
            _objGenralUserInfo.Users.LastName = _objGenralUserInfo.UserBusiness == null ? _objGenralUserInfo.Users.LastName : string.Empty;
            string _EmailBody = GetEmailAccountBody(_objGenralUserInfo.Users);
            bool val = objEmail.SendMessages("Your Tribute<" + SIGNUPAdmin + ">", _objGenralUserInfo.Users.Email, "Welcome to Your Tribute", _EmailBody, EmailMessages.TextFormat.Html.ToString());
            if (val == true)
            {
                //MailMessage _objMailMessage = new MailMessage();
                //_objMailMessage.SendByUserId = Userid;
                //_objMailMessage.Subject = "User Registration";
                //_objMailMessage.Body = _EmailBody;
                //_objMailMessage.SendToUserId = Userid;
                //_objMailMessage.SendDate = DateTime.Now;
                //_objMailMessage.Status = 1;
                //_objMailMessage.RecievedDate = DateTime.Now;
                //_objMailMessage.CreatedBy = Userid;
                //_objMailMessage.CreatedDate = DateTime.Now;
                //_objMailMessage.ModifiedBy = Userid;
                //_objMailMessage.ModifiedDate = DateTime.Now;
                //_objMailMessage.IsActive = true;
                //_objMailMessage.IsDeleted = false;
                //object[] _ParamMail ={ _objMailMessage };
                //objUser.SaveEmail(_ParamMail);

                //if (_objMailMessage.CustomError != null)
                //{
                //    _objGenralUserInfo.CustomError = _objMailMessage.CustomError;
                //}

            }

        }

        //private string SetInternal()
        //{
        //    StringBuilder objstrb = new StringBuilder();
        //    //objstrb.Append("<P>"<P>);
        //    //        "Welcome to Your Tribute! 

        //    //Now that you have registered you can begin collaborating with friends and family: 
        //    //• leave messages in their guestbooks
        //    //• share photos and videos
        //    //• send virtual gifts
        //    //• receive event invites and updates

        //    //You can also:
        //    //• create your own tribute to celebrate a significant event or a special someone
        //    //• send private messages—simply click on another member’s name to open the profile and communicate one-to-one
        //    //• upload a profile picture of yourself—you can upload a picture, an icon, or a drawing, and it will show up wherever you add content on Your Tribute. To add a profile picture, click the ""My Profile"" link in the top navigation bar and then click the ""Edit My Profile"" button

        //    //Take a Tour to learn more about Your Tribute, or find Help at the bottom of any page.  

        //    //----
        //    //Your Tribute Team
        //    //"


        //}

        public object SavePersonalAccount(UserRegistration _UserRegistration)
        {

            UserInfoResource objUserinfo = new UserInfoResource();
            object[] param = { _UserRegistration };

            using (TransactionScope trans = new TransactionScope())
            {
                objUserinfo.SaveUserAccount(param).ToString();
                //Transaction Completed
                if (_UserRegistration.CustomError == null)
                {
                    trans.Complete();
                }
            }

            if (_UserRegistration.CustomError == null &&
                !_UserRegistration.Users.Email.Equals(string.Empty) &&
                !_UserRegistration.Users.Password.Equals(string.Empty))
            {
                SendMail(_UserRegistration, _UserRegistration.Users.UserId, objUserinfo);
            }
            return _UserRegistration.Users.UserId;
        }

        /// <summary>
        /// Method to check if user is Administrator of Tribute.
        /// Added By: Gaurav Puri
        /// </summary>
        /// <param name="objUserInfo">UserAdminOwnerInfo entity containing UserId and TributeId</param>
        /// <returns>True/False</returns>
        public bool IsUserAdmin(UserAdminOwnerInfo objUserInfo)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { objUserInfo };
            return objUser.IsUserAdmin(param);
        }

        /// <summary>
        /// Method to check if user is Owner of Type(Video, Photo etc.)
        /// Added By: Gaurav Puri
        /// </summary>
        /// <param name="objUserInfo">UserAdminOwnerInfo entity containing UserId, TypeId and TypeName</param>
        /// <returns>True/False</returns>
        public bool IsUserOwner(UserAdminOwnerInfo objUserInfo)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { objUserInfo };
            return objUser.IsUserOwner(param);
        }

        public List<GetMyTributes> GetMyTributes(object[] _MyTributes)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.GetMyTributes(_MyTributes);
        }

        public Users GetUserDetailsOnUserId(int userId)
        {
            UserInfoResource objUserInfoResource = new UserInfoResource();
            return objUserInfoResource.GetUserDetailsOnUserId(userId);
        }

        public List<GetMyTributes> GetMyTribute(object[] _MyTributes)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.GetMyTribute(_MyTributes);
        }
        public List<GetMyTributes> GetMyFavourites(object[] _MyTributes)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.GetMyFavourites(_MyTributes);
        }
        public void UpdateEmailAlerts(object[] _Tributes)
        {
            UserInfoResource objUser = new UserInfoResource();
            objUser.UpdateEmailAlerts(_Tributes);
        }
        public void UpdateFavouriteEmailAlert(object[] _Tributes)
        {
            UserInfoResource objUser = new UserInfoResource();
            objUser.UpdateFavouriteEmailAlert(_Tributes);
        }

        public void DeleteMyFavourite(object[] _Tributes)
        {
            UserInfoResource objUser = new UserInfoResource();
            objUser.DeleteMyFavourite(_Tributes);
        }
        public List<MailMessage> GetMailMessage(object[] objValue)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.GetMailMessage(objValue);
        }
        public List<MailMessage> GetMailThread(object[] objValue)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.GetMailThread(objValue);
        }

        public List<MailMessage> GetuserSentMessages(object[] objValue)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.GetuserSentMessages(objValue);
        }
        public void UpdateMessageStstus(object[] Params)
        {
            UserInfoResource objUser = new UserInfoResource();
            objUser.UpdateMessageStstus(Params);
        }
        public void DeleteMessages(object[] Params)
        {
            UserInfoResource objUser = new UserInfoResource();
            objUser.DeleteMessages(Params);
        }
        public void DeleteSentMessages(object[] Params)
        {
            UserInfoResource objUser = new UserInfoResource();
            objUser.DeleteSentMessages(Params);
        }


        public void DeleteTributeAdminis(object[] Params)
        {
            UserInfoResource objUser = new UserInfoResource();
            objUser.DeleteTributeAdminis(Params);
        }


        public List<Events> GetMyEvents(object[] objValue)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.GetMyEvents(objValue);
        }
        public void GetUserProfile(object[] objValue)
        {
            UserInfoResource objUser = new UserInfoResource();
            objUser.GetUserProfile(objValue);
        }
        //public IList<Locations> Locations(Locations locaton)
        //{

        //    LocationResource objLocationResource = new LocationResource();
        //    return objLocationResource.LocationList(locaton);
        //}

        public int UserInboxCount(object[] objValue)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.UserInboxCount(objValue);
        }
        public Events GetEventName(int EventID)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.GetEventName(EventID);
        }

        /// <summary>
        ///  This method will call the UserInfoResource Resource class method for saving the message
        /// Added By Parul Jain
        /// </summary>
        /// <param name="objTributeParam">A object which contain the welcome message which wants to save</param>
        public void SaveMessage(UserBusiness objBusinessUser,string ApplicationType)
        {
            try
            {
                UserInfoResource objUser = new UserInfoResource();
                objUser.SaveMessage(objBusinessUser, ApplicationType);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        ///  This method will call the UserInfoResource Resource class method for saving the company logo
        /// Added By Parul Jain
        /// </summary>
        /// <param name="objTributeParam">A object which contain the company logo which wants to save</param>
        public void SaveImage(UserBusiness objBusinessUser)
        {
            try
            {
                UserInfoResource objUser = new UserInfoResource();
                objUser.SaveImage(objBusinessUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This method will call the UserInfoResource Resource class method for getting the tribute Listing for the
        /// Business user.
        /// Added By Parul Jain
        /// </summary>
        /// <param name="objTributeParam">This is the SearchTribute object which contain the Parameter 
        /// to get the tribute list - Sort Order, Tribuet Type and User ID</param>
        /// <returns>This method will return the List of Tribute</returns>
        public List<SearchTribute> GetBusinessUserTributeList(SearchTribute objTributeParam, string ApplicationType)
        {
            try
            {
                UserInfoResource objUser = new UserInfoResource();

                // Replace wildcard character (*, ?) by the (% and _)
                if (objTributeParam.SearchString != "")
                {
                    String searchString = objTributeParam.SearchString.Replace('*', '%');

                    if (!searchString.Contains("%"))
                    {
                        string newsearchString = "%" + searchString + "%";
                        searchString = newsearchString;
                    }

                    objTributeParam.ChangeSearchString = searchString.Replace('?', '_');
                }
                else
                {
                    objTributeParam.ChangeSearchString = "%";
                }

                return objUser.GetBusinessUserTributeList(objTributeParam, ApplicationType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Gift Resource access class method to get the Image list for the passed Tribute Type
        /// Added By Parul Jain
        /// </summary>
        /// <returns>This method will return the Gifts object which contain the list of Image</returns>
        public List<GiftImage> GetImage()
        {
            try
            {
                UserInfoResource objUser = new UserInfoResource();

                return objUser.GetImage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to check if business user exists of not based on the username.
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns>If users exists return userid else returns empty string.</returns>
        public string CheckBusinessUser(string userName)
        {
            UserInfoResource objUserInfoResource = new UserInfoResource();
            return objUserInfoResource.CheckBusinessUser(userName);
        }

        /// <summary>
        /// Method to send email to user on confirmation of add as administrator.
        /// </summary>
        /// <param name="objUserDetails">SessionValue containing logged in user details.</param>
        /// <param name="objTributeDetails">Tribute details.</param>
        private void SendEmailForAdminConfirmation(SessionValue objUserDetails, Tributes objTributeDetails)
        {
            EmailMessages objEmail = EmailMessages.Instance;
            string strSubject = "Your Tribute Administrator Confirmation.";
            string strUrl = "http://" + objTributeDetails.TypeDescription.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objTributeDetails.TributeUrl;
            StringBuilder sbBody = new StringBuilder();
            sbBody.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>");
            sbBody.Append("Congratulations " + (objUserDetails.FirstName != string.Empty ? (objUserDetails.FirstName + " " + objUserDetails.LastName) : objUserDetails.UserName));
            sbBody.Append(",</p>");
            sbBody.Append("<p>You were successfully added as an administrator to the " + objTributeDetails.TributeName + " " + objTributeDetails.TypeDescription + " Tribute!</p>");
            sbBody.Append("<p><strong>Managing Your " + objTributeDetails.TypeDescription + " Tribute</strong><br/>");
            sbBody.Append("• Sign in to your account at <a href='http://www." + WebConfig.TopLevelDomain + "'>http://www." + WebConfig.TopLevelDomain + "</a><br/>");
            sbBody.Append("• View your tribute at <a href='" + strUrl + "'>" + strUrl + "</a><br/>");
            sbBody.Append("• Add, modify or delete content at any time—in your <a href='http://" + WebConfig.TopLevelDomain + "/log_in.aspx'>“My Profile”</a> area, you can add additional administrators to assist in managing your tribute and choose to be notified by email when visitors add content to your tribute <br/>");
            sbBody.Append("•  Find help, managing your tribute or adding content, at the bottom of any page");
            sbBody.Append("</p>");
            sbBody.Append("<p><Strong>Getting Started</Strong><br/>");
            sbBody.Append("We recommend that you begin by adding content to your tribute:<br/>");
            sbBody.Append("1. Create the <a href='" + strUrl + "/story.aspx'>Story</a>—communicate information about the people <br/>");
            sbBody.Append("2. Post an <a href='" + strUrl + "/events.aspx'>Event</a>—invite guests using the RSVP feature <br/>");
            sbBody.Append("3. Add <a href='" + strUrl + "/photos.aspx'>Photos</a> and <a href='" + strUrl + "/videos.aspx'>Videos</a>—share with your friends and family </p>");
            sbBody.Append("<p>-----<br/>");
            sbBody.Append("Your Tribute Team");
            sbBody.Append("</p></font>");
            bool val = objEmail.SendMessages("Your Tribute<" + WebConfig.NoreplyEmail + ">", objUserDetails.UserEmail, strSubject, sbBody.ToString(), EmailMessages.TextFormat.Html.ToString());
        }

        #region<< Storing Session Values >>

        /// <summary>
        /// Method to insert Session
        /// </summary>
        public void InsertSession(SessionValue _objSessionValue, string strId)
        {
            UserInfoResource objUser = new UserInfoResource();
            object[] param = { _objSessionValue, strId };

            //  System.Decimal identity = (System.Decimal)objUser.InsertSessionValues(param);
            int identity = Convert.ToInt32(objUser.InsertSessionValues(param));

            _objSessionValue.ID = int.Parse(identity.ToString());
            //UserId 
            objUser.InsertSessionKeyValues(_objSessionValue.ID, "UserId", _objSessionValue.UserId.ToString());
            //UserName 
            objUser.InsertSessionKeyValues(_objSessionValue.ID, "UserName", _objSessionValue.UserName);
            //FirstName 
            objUser.InsertSessionKeyValues(_objSessionValue.ID, "FirstName", _objSessionValue.FirstName);
            //LastName
            objUser.InsertSessionKeyValues(_objSessionValue.ID, "LastName", _objSessionValue.LastName);
            //Email
            objUser.InsertSessionKeyValues(_objSessionValue.ID, "Email", _objSessionValue.UserEmail);
            //UserType
            objUser.InsertSessionKeyValues(_objSessionValue.ID, "UserType", _objSessionValue.UserType.ToString());
            //UserTypeDescription
            objUser.InsertSessionKeyValues(_objSessionValue.ID, "UserTypeDescription", _objSessionValue.UserTypeDescription);
            //IsUsernameVisiable
            objUser.InsertSessionKeyValues(_objSessionValue.ID, "IsUsernameVisiable", _objSessionValue.IsUsernameVisiable.ToString());
            //Added by rupendra to get logged in user image
            //UserImage
            if (_objSessionValue.UserImage != null)
                objUser.InsertSessionKeyValues(_objSessionValue.ID, "UserImage", _objSessionValue.UserImage.ToString());
            else
                objUser.InsertSessionKeyValues(_objSessionValue.ID, "UserImage", "");
            // Added by Varun to store NoRedirection boolean
            objUser.InsertSessionKeyValues(_objSessionValue.ID, "NoRedirection", _objSessionValue.NoRedirection.ToString());
        }

        /// <summary>
        /// Method to Get session detail.
        /// </summary>
        public List<SessionValue> GetSessionDetail(string SessionValues)
        {
            UserInfoResource objUserInfoResource = new UserInfoResource();
            return objUserInfoResource.GetSessionDetail(SessionValues);
        }

        //TEST
        public void DeleteSessionKeyDetails(String SessionID)
        {
            UserInfoResource objUser = new UserInfoResource();
            objUser.DeleteSession(SessionID);
        }

        public void SendEmailtoNewAdmin(Tributes objTributesUserInfo, SessionValue objGenralUserInfo, bool _Accept)
        {
            SendEmailForAdminConfirmation(objGenralUserInfo, objTributesUserInfo);
        }

        #endregion<< Storing Session Values >>

        public IList<GetTributesFeed> GetTributesFeed(object[] param)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.GetTributesFeed(param);
        }

        /// <summary>
        /// To get the creation date of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DateTime GetUserCreationDate(int userId)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.GetUserCreationDate(userId);
        }

        public IList<GetTributesFeed> GetYourTributeFeedOnTributeName(object[] objparam)
        {
             UserInfoResource objUser = new UserInfoResource();
             return objUser.GetYourTributeFeedOnTributeName(objparam);
        }

        public IList<GetTributesFeed> GetYourTributesFeed(object[] objparam)
        {
             UserInfoResource objUser = new UserInfoResource();
             return objUser.GetYourTributesFeed(objparam);
        }

        public int GetTotalActiveObituaries(int _businessUserId)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.GetTotalActiveObituaries(_businessUserId);
        }

        public int GetTotalActiveObituariesOnTributeName(object[] objprm)
        {
            UserInfoResource objUser = new UserInfoResource();
            return objUser.GetTotalActiveObituariesOnTributeName(objprm);
        }

        public void GetPackIdonPhotoId(int PhotoId)
        {
            throw new NotImplementedException();
        }
    }
}
